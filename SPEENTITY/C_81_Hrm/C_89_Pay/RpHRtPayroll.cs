using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.RD_81_Hrm.RD_89_Pay
{
    public class RpHRtPayroll
    {
        [Serializable]
        //rowid=0, a.comcod, a.empid, a.grade, a.joindate,  a.wd, a.wjd, a.absday, a.wld, a.acat, a.tabday, a.netwday, a.loanbal, a.asloanbal, a.loanbal2, a.asloanbal2, a.bsal, a.salary, a.hrent,  a.cven, a.mallow, a.otall, a.arsal, a.pickup, a.fuel, a.entaint,   a.mcell,  a.incent, a.empcont,	a.oth, a.pfund,  a.itax,  a.adv, a.fallded, a.mbillded,  a.othded, a.dallow, a.teallow, a.oallow, a.ohour, a.hallow, a.elallow, a.mbill,  a.lwided, a.tothdeduc, a.arded,a.loanins, a.asloanins, a.gssal, a.gssal1, a.salpday, a.gspay, a.gspay1, a.absded, a.tallow, a.tdeduc, a.dedday, a.dalday, a.ddaya10, a.dday10amt, a.mcallow, a.mcadj,  a.othallow, a.othearn, a.sdedamt, elftam=0.00, ellfthour='00:00',	a.netpay, a.netpayatax, a.bankacno, a.bankamt, a.cashamt, 	thday, lwpday, a.idcard, a.section, a.desigid, a.desig, a.empname, a.refno, refdesc=b.sirdesc,  sectionname=c.sirdesc, a.presal, emptype , fallow, arpfund, aritax, jobloc

        public class SalarySheet
        {
            public string comcod { get; set; }
            public int rowid { get; set; }
            public string empid { get; set; }
            public string grade { get; set; }
            public DateTime joindate { get; set; }
            public string joindateb { get; set; }
            public double wd { get; set; }
            public double wjd { get; set; }
            public double absday { get; set; }
            public double wld { get; set; }
            public double acat { get; set; }
            public double tabday { get; set; }
            public double netwday { get; set; }
            public double loanbal { get; set; }
            public double asloanbal { get; set; }
            public double loanbal2 { get; set; }
            public double asloanbal2 { get; set; }
            public double bsal { get; set; }
            public double salary { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double otall { get; set; }
            public double arsal { get; set; }
            public double pickup { get; set; }
            public double fuel { get; set; }
            public double entaint { get; set; }
            public double mcell { get; set; }
            public double incent { get; set; }
            public double empcont { get; set; }
            public double oth { get; set; }
            public double pfund { get; set; }
            public double itax { get; set; }
            public double adv { get; set; }
            public double fallded { get; set; }
            public double mbillded { get; set; }
            public double othded { get; set; }
            public double dallow { get; set; }
            public double teallow { get; set; }
            public double oallow { get; set; }
            public double ohour { get; set; }
            public double hallow { get; set; }
            public double elallow { get; set; }
            public double mbill { get; set; }
            public double lwided { get; set; }
            public double tothdeduc { get; set; }
            public double arded { get; set; }
            public double loanins { get; set; }
            public double gssal { get; set; }
            public double salpday { get; set; }
            public double gspay { get; set; }
            public double gspay1 { get; set; }
            public double gspay2 { get; set; }
            public double absded { get; set; }
            public double tallow { get; set; }
            public double tdeduc { get; set; }
            public double dedday { get; set; }
            public double ddaya10 { get; set; }
            public double dday10amt { get; set; }
            public double mcallow { get; set; }
            public double mcadj { get; set; }
            public double othallow { get; set; }
            public double othearn { get; set; }
            public double elftam { get; set; }
            public string ellfthour { get; set; }
            public double netpayatax { get; set; }
            public double netpay { get; set; }
            public string bankacno { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double thday { get; set; }
            public double lwpday { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }

            public string desigban { get; set; }
            public string empname { get; set; }
            public string empnameban { get; set; }
            public string refno { get; set; }
            public string refdesc { get; set; }
            public string refdescban { get; set; }
            public string sirdesc { get; set; }
            public string sectionname { get; set; }
            public string sectionnameb { get; set; }
            public double asloanins { get; set; }
            //public double asloanbal { get; set; }
            //public double asloanbal2 { get; set; }
            public double gssal1 { get; set; }
            public double dalday { get; set; }
            public double sdedamt { get; set; }
            public string emptype { get; set; }
            public double fallow { get; set; }
            public double arpfund { get; set; }
            public double aritax { get; set; }
            public string jobloc { get; set; }
            public double elv { get; set; }
            public double mlv { get; set; }
            public double splv { get; set; }
            public double flv { get; set; }
            public double clv { get; set; }
            public double slv { get; set; }
            public double whlv { get; set; }
            public double splvFrmOffday { get; set; }
            public double adlv { get; set; }
            public double bonusamt { get; set; }
            public string costcent { get; set; }
            public string empgrade { get; set; }
            public double orate { get; set; }
            public double leavday { get; set; }
            public double ttlhday { get; set; }
            public double ttlpsnt { get; set; }
            public double ttlabs { get; set; }
            public double tworkday { get; set; }
            public double weekdy { get; set; }
            public string fline { get; set; }
            public string flineban { get; set; }
            public string bankcod { get; set; }
            public string bankname { get; set; }
            public double carallow { get; set; }
            public double suballow { get; set; }
            public double utiallow { get; set; }
            public double exgssal { get; set; }
            public double exnetpay { get; set; }
            public string empstatus { get; set; }
            public double spcded { get; set; }
            public double stamp { get; set; }
            public double yrincamt { get; set; }
            public string linecode { get; set; }

        }
        //a.CL,a.EL,a.SL,a.ml,a.wpl,a.lft,a.pl,a.hl,a.pmd,a.delday
        [Serializable]
        public class EmpMonthSummary
        {
            public string comcod { get; set; }
            public int rowid { get; set; }
            public string empid { get; set; }
            public string grade { get; set; }
            public DateTime joindate { get; set; }
            public decimal wd { get; set; }
            public decimal wjd { get; set; }
            public decimal absday { get; set; }
            public decimal wld { get; set; }
            public decimal acat { get; set; }
            public decimal tabday { get; set; }
            public decimal netwday { get; set; }
            public decimal loanbal { get; set; }
            public decimal loanbal2 { get; set; }
            public decimal bsal { get; set; }
            public decimal salary { get; set; }
            public decimal hrent { get; set; }
            public decimal cven { get; set; }
            public decimal mallow { get; set; }

            public decimal otall { get; set; }
            public decimal arsal { get; set; }
            public decimal pickup { get; set; }
            public decimal fuel { get; set; }
            public decimal entaint { get; set; }
            public decimal mcell { get; set; }
            public decimal incent { get; set; }
            public decimal empcont { get; set; }
            public decimal oth { get; set; }
            public decimal pfund { get; set; }
            public decimal itax { get; set; }
            public decimal adv { get; set; }
            public decimal fallded { get; set; }
            public decimal mbillded { get; set; }
            public decimal othded { get; set; }

            public decimal dallow { get; set; }
            public decimal teallow { get; set; }
            public decimal oallow { get; set; }
            public decimal ohour { get; set; }
            public decimal hallow { get; set; }
            public decimal elallow { get; set; }
            public decimal mbill { get; set; }
            public decimal lwided { get; set; }
            public decimal tothdeduc { get; set; }
            public decimal arded { get; set; }
            public decimal loanins { get; set; }
            public decimal gssal { get; set; }
            public decimal salpday { get; set; }
            public decimal gspay { get; set; }
            public decimal gspay1 { get; set; }
            public decimal absded { get; set; }

            public decimal tallow { get; set; }
            public decimal tdeduc { get; set; }
            public decimal dedday { get; set; }
            public decimal ddaya10 { get; set; }
            public decimal dday10amt { get; set; }
            public decimal mcallow { get; set; }
            public decimal mcadj { get; set; }
            public double othallow { get; set; }
            public decimal othearn { get; set; }
            public decimal elftam { get; set; }
            public string ellfthour { get; set; }
            public decimal netpayatax { get; set; }
            public decimal netpay { get; set; }
            public string bankacno { get; set; }
            public decimal bankamt { get; set; }
            public decimal cashamt { get; set; }
            public decimal thday { get; set; }
            public decimal lwpday { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string refno { get; set; }
            public string refdesc { get; set; }
            public string sirdesc { get; set; }
            public string sectionname { get; set; }
            public decimal asloanins { get; set; }
            public decimal asloanbal { get; set; }
            public decimal asloanbal2 { get; set; }
            public decimal gssal1 { get; set; }
            public decimal dalday { get; set; }
            public decimal sdedamt { get; set; }
            public decimal cl { get; set; }
            public decimal el { get; set; }
            public decimal sl { get; set; }
            public decimal ml { get; set; }
            public decimal wpl { get; set; }
            public decimal lft { get; set; }
            public decimal pl { get; set; }
            public decimal hl { get; set; }
            public decimal pmd { get; set; }
            public double empgrade { get; set; }
            public double orate { get; set; }
            public double bonusamt { get; set; }
        }

        // a.comcod, a.empid,empname,idcard,section, desigid, desig, refno ,a.bsal ,a.hrent ,a.cven ,a.mallow ,a.gsal  ,a.gsal1 ,a.itax ,a.cashamt ,a.bankamt
        [Serializable]
        public class aitpurpose
        {
            public string comcod { get; set; }
            public string monthid { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string refno { get; set; }
            public decimal bsal { get; set; }
            public decimal hrent { get; set; }
            public decimal cven { get; set; }
            public decimal mallow { get; set; }
            public decimal gsal { get; set; }
            public decimal gsal1 { get; set; }
            public decimal itax { get; set; }
            public decimal cashamt { get; set; }
            public decimal bankamt { get; set; }
            public decimal incent { get; set; }
            public decimal adv { get; set; }
            public DateTime posteddat { get; set; }
            public string challanno { get; set; }
            public decimal bonamt { get; set; }



        }
        [Serializable]
        public class DayTotOTSum
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public DateTime daysname { get; set; }
            public double wkovthr { get; set; }
            public double wkcomplaincehr { get; set; }
            public double wkextrahr { get; set; }
            public double stovthr { get; set; }
            public double stcomplaincehr { get; set; }
            public double stextrahr { get; set; }
            public double ttovthr { get; set; }
            public double ttcomplaincehr { get; set; }
            public double ttextrahr { get; set; }
            
        }

        [Serializable]
        public class BankFord
        {
            public string idcard { get; set; }
            public string empid { get; set; }
            public string monthid { get; set; }
            public string empname { get; set; }
            public string bankcode { get; set; }
            public string banksl { get; set; }
            public string bankaddr { get; set; }
            public string section { get; set; }
            public string detname { get; set; }
            public string acno { get; set; }
            public double amt { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }

        }

        [Serializable]
        public class LeaveApp
        {


            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double entitle { get; set; }
            public double permonth { get; set; }
            public double pbal { get; set; }
            public double ltaken { get; set; }
            public double balleave { get; set; }
            public double tltakreq { get; set; }
            public DateTime lenjoydt1 { get; set; }
            public DateTime lenjoydt2 { get; set; }
            public double lenjoyday { get; set; }
            public DateTime appdate { get; set; }
            public double applyday { get; set; }
            public double appday { get; set; }


        }

        [Serializable]
        public class OverTimeSal
        {
            public int rowid { get; set; }
            public string comcod { get; set; }
            public string emptype { get; set; }
            public string refno { get; set; }
            public string refno1 { get; set; }
            public string desigid { get; set; }
            public decimal otrate { get; set; }
            public decimal offrate { get; set; }
            public decimal osrate { get; set; }
            public string secid { get; set; }
            public string gradeid { get; set; }
            public string grade { get; set; }
            public string empid { get; set; }
            public decimal bsal { get; set; }
            public decimal gssal1 { get; set; }
            public decimal hrent { get; set; }
            public decimal cven { get; set; }
            public decimal mallow { get; set; }
            public decimal ohour { get; set; }
            public decimal osday { get; set; }
            public decimal offday { get; set; }
            public decimal otamount { get; set; }
            public decimal offamount { get; set; }
            public decimal osamount { get; set; }
            public string emptypedesc { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string sectionname { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }
            public DateTime joindate { get; set; }
            public string desig { get; set; }
            public decimal otoffday { get; set; }
            public decimal otoffrate { get; set; }
            public decimal otoffamount { get; set; }
            public decimal netamt { get; set; }
            public string bankacno { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public string fline { get; set; }
            public string linecode { get; set; }
            public double foodalw { get; set; }
            public double presnday { get; set; }
            public double ttlabsnt { get; set; }
            public double offdaycnt { get; set; }
            public double ttlleave { get; set; }
            public string xmlcol1 { get; set; }
            public double tovtmin { get; set; }
            public OverTimeSal()
            {

            }

        }


        [Serializable]
        public class ECurSubAllowance
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string empid { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public double grsalry1 { get; set; }
            public double bsal { get; set; }
            public double gsalary { get; set; }
            public double carallow { get; set; }
            public string subbonus { get; set; }
            public double suballowance { get; set; }
            public string joinday { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public DateTime joindate { get; set; }
            public string grdesc { get; set; }
            public string costcnt { get; set; }
            public string jobdesc { get; set; }
            public double subbasic { get; set; }
            public string duration { get; set; }
            public double perbon { get; set; }
            public double bonamt { get; set; }
            public string hidegs { get; set; }
            public double arcallow { get; set; }
            public double netpay { get; set; }
            public double asallow { get; set; }
            public ECurSubAllowance() { }

        }

        [Serializable]
        public class SummarySalarySheet
        {
            public double comcod { get; set; }
            public double bankemp { get; set; }
            public double cashemp { get; set; }
            public double toemp { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double toamt { get; set; }
            public string sectionname { get; set; }
            public SummarySalarySheet() { }
        }

        [Serializable]
        public class EclassMonthSalSummary
        {
            public string grp { get; set; }
            public string emptype { get; set; }
            public string empcat { get; set; }
            public string partcod { get; set; }
            public string partdesc { get; set; }
            public double manpower { get; set; }
            public double bankpay { get; set; }
            public double caspay { get; set; }
            public double total { get; set; }
            public EclassMonthSalSummary() { }
        }
        [Serializable]
        public class EclassEmpEOT
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string secid { get; set; }
            public string section { get; set; }
            public string idcardno { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string line { get; set; }
            public DateTime intime { get; set; }
            public DateTime outtime { get; set; }
            public DateTime outtime1 { get; set; }
            public double tovtmin { get; set; }
            public double ovthour { get; set; }
            public double ovtmin { get; set; }

        }

        //comcod, deptid, secid, empid,  desigid, desig,  empname, idcardno,  grsalry1, bsal, gsalary, carallow,  subbonus, suballowance,joinday,  deptname, section , joindate, 
        //grdesc, costcnt, jobdesc,subbasic, duration=durationday,durationday, perbon, bonamt,hidegs=0.

        [Serializable]
        public class SubBonusAllowance
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string empid { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public double grsalry1 { get; set; }
            public double bsal { get; set; }
            public double gsalary { get; set; }
            public double carallow { get; set; }
            public string subbonus { get; set; }
            public double suballowance { get; set; }
            public string joinday { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public DateTime joindate { get; set; }
            public string grdesc { get; set; }
            public string costcnt { get; set; }
            public string jobdesc { get; set; }
            public double subbasic { get; set; }
            public string duration { get; set; }
            public double perbon { get; set; }
            public double bonamt { get; set; }
            public string hidegs { get; set; }
            public SubBonusAllowance() { }
        }


        [Serializable]
        public class MonthlySalSummary
        {
            public string monthid { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public double nofemployee { get; set; }
            public double wd { get; set; }
            public double absday { get; set; }
            public double wld { get; set; }
            public double acat { get; set; }
            public double tabday { get; set; }
            public double netwday { get; set; }
            public double loanbal { get; set; }
            public double loanbal2 { get; set; }
            public double bsal { get; set; }
            public double salary { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double arsal { get; set; }
            public double pickup { get; set; }
            public double fuel { get; set; }
            public double entaint { get; set; }
            public double mcell { get; set; }
            public double incent { get; set; }
            public double empcont { get; set; }
            public double oth { get; set; }
            public double pfund { get; set; }
            public double itax { get; set; }
            public double adv { get; set; }
            public double othded { get; set; }
            public double arded { get; set; }
            public double dallow { get; set; }
            public double oallow { get; set; }
            public double ohour { get; set; }
            public double thday { get; set; }
            public double hallow { get; set; }
            public double ohallowth { get; set; }
            public double elallow { get; set; }
            public double mbill { get; set; }
            public double lwpday { get; set; }
            public double lwided { get; set; }
            public double loanins { get; set; }
            public double gssal { get; set; }
            public double salpday { get; set; }
            public double gspay { get; set; }
            public double absded { get; set; }
            public double tallow { get; set; }
            public double tdeduc { get; set; }
            public double tothdeduc { get; set; }
            public double dedday { get; set; }
            public double dalday { get; set; }
            public double sdedamt { get; set; }
            public double elftam { get; set; }
            public string ellfthour { get; set; }
            public double gspay1 { get; set; }
            public double gspay2 { get; set; }
            public double netpay { get; set; }
            public double netpayatax { get; set; }
            public double mcadj { get; set; }
            public double othallow { get; set; }
            public double othearn { get; set; }
            public double mcallow { get; set; }
            public double teallow { get; set; }
            public double cashamt { get; set; }
            public double bankamt { get; set; }
            public double finincamt { get; set; }
            public double bonusamt { get; set; }
            public double fallded { get; set; }
            public double fallow { get; set; }
            public double stamp { get; set; }
            public string refdesc { get; set; }
  
            public string sectionname { get; set; }
            public MonthlySalSummary() { }
        }

        [Serializable]
        public class MonthlySalSummaryEOT
        {
            public string comcod { get; set; }
            public string dept { get; set; }
            public string refno { get; set; }
            public string refno1 { get; set; }
            public string section { get; set; }
            public int noofemployee { get; set; }
            public double otrate { get; set; }
            public double offrate { get; set; }
            public double osrate { get; set; }
            public double bsal { get; set; }
            public double gssal1 { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double ohour { get; set; }
            public double osday { get; set; }
            public double offday { get; set; }
            public double otamount { get; set; }
            public double offamount { get; set; }
            public double osamount { get; set; }
            public double otoffday { get; set; }
 
            public double otoffrate { get; set; }
            public double otoffamount { get; set; }
            public double netamt { get; set; }
            public double foodalw { get; set; }
            public double offdhour { get; set; }
            public double offdamount { get; set; }
            public double totalotamt { get; set; }
            public double totalothour { get; set; }
            public double tnpayable { get; set; }
   
            public double eotdues { get; set; }
            public double offdotdues { get; set; }
            public double othdeduc { get; set; }
            public MonthlySalSummaryEOT() { }
        }

        [Serializable]
        public class MonHolidayAllownace
        {
            public string companyid { get; set; }
            public string empid { get; set; }
            public string deptid { get; set; }
            public string sectionid { get; set; }
            public string lineid { get; set; }
            public string idcardno { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string line { get; set; }
            public string designation { get; set; }
            public string empname { get; set; }
            public DateTime joindate { get; set; }
            public double offdays { get; set; }
            public double gross { get; set; }
            public double hoallowamt { get; set; }
            public string accno { get; set; }
            public MonHolidayAllownace() { }
        }


        [Serializable]
        public class RptFestivalBonus
        {
            public string companyid { get; set; }
            public string deptid { get; set; }
            public string section { get; set; }
            public string empid { get; set; }
            public DateTime joindate { get; set; }
            public string bankacno { get; set; }
            public string desigid { get; set; }
            public double duration { get; set; }
            public double bsal { get; set; }
            public double gssal { get; set; }
            public double perbon { get; set; }
            public double bonamt { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double cashamta { get; set; }
            public double foodalw { get; set; }
            public string idcard { get; set; }
            public string desig { get; set; }
            public string desigbn { get; set; }
            public string grade { get; set; }
            public string emptype { get; set; }
            public string empname { get; set; }
            public string deptnamebn { get; set; }
            public string sectionname { get; set; }
            public string sectionnamebn { get; set; }
            public double durationday { get; set; }
            public string linecod { get; set; }
            public string linedesc { get; set; }
            public string linedescbn { get; set; }
            public RptFestivalBonus() { }
        }

        [Serializable]
        public class RptFestBonus
        {
            public string company { get; set; }
            public string emptype { get; set; }
            public string divid { get; set; }
            public string division { get; set; }
            public string deptid { get; set; }
            public string deptname { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public DateTime joindate { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public double bsal { get; set; }
            public double gssal { get; set; }
            public double perbon { get; set; }
            public double bonamt { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public string linecode { get; set; }
            public string fline { get; set; }
            public RptFestBonus() { }
        }

        [Serializable]
        public class BonusSummary
        {
            // comcod , monthid, refno , section, refdesc, sectionname, noofemp, bsal, gssal, bonamt
          
            public string gp { get; set; }
            public string company { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public string companyname { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }            
            public double noofemp { get; set; }
            public double bsal { get; set; }
            public double gssal { get; set; }
            public double perbon { get; set; }
            public double bonamt { get; set; }
            public double manualamt { get; set; }
            public double totalbonamt { get; set; }
            public string remarks { get; set; }

            public BonusSummary() { }
        }


        [Serializable]
        public class RptMonWiseOTSheet
        {
            public string dayid { get; set; }
            public DateTime adate { get; set; }
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
            public double totalot { get; set; }
            public RptMonWiseOTSheet() { }
        }

        [Serializable]
        public class RptMonWiseOTDesc
        {
            public string secid { get; set; }
            public string section { get; set; }
            public RptMonWiseOTDesc() { }
        }

        [Serializable]
        public class RptAnnualIncrement
        {
            public string companycod { get; set; }
            public string deptcode { get; set; }
            public string seccode { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public DateTime joindate { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string desig { get; set; }
            public string remarks { get; set; }
            public string grade { get; set; }
            public string signatory { get; set; }
            public string prodesigid { get; set; }
            public string prodesig { get; set; }
            public string hrprodesigid { get; set; }
            public string hrprodesig { get; set; }
            public string joblocid { get; set; }
            public string joblocation { get; set; }
            public double inpercnt { get; set; }
            public double incamt { get; set; }
            public double pinincamt { get; set; }
            public double hrpromincamt { get; set; }
            public double finincamt { get; set; }
            public double incamtprevyr { get; set; }
            public double carsubamt { get; set; }
            public double subamt { get; set; }
            public double maingrossal { get; set; }
            public double grossal { get; set; }
            public double recaramt { get; set; }
            public double salaftrincmnt { get; set; }
            public string lastproyear { get; set; }
            public RptAnnualIncrement() { }
        }
        [Serializable]
        public class RptMonSalaryDataSheet
        {
            //monthid, refno , section,desigid,empid,idcard, empname, refdesc, sectionname ,desig, wd, absday, wld, acat , bsal , hrent, cven, mallow, arsal, pickup, fuel,entaint, 
			//mcell, incent, oth, pfund, itax, adv, othded, dallow, oallow, ohour, hallow, elallow, mbill, lwided, loanins, gssal, salpday, gspay, absded, tallow, 
			//tdeduc, dedday, sdedamt, netpay,  mcadj, othallow, othearn
            public string monthid { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcard { get; set; }
            public string empname { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public string desig { get; set; }
            public int wd { get; set; }
            public int absday { get; set; }
            public int wld { get; set; }
            public int acat { get; set; }
            public double arsal { get; set; }
            public double mcell { get; set; }
            public double pfund { get; set; }
            public double itax { get; set; }
            public double adv { get; set; }
            public double othded { get; set; }
            public double mbill { get; set; }
            public double loanins { get; set; }
            public double tallow { get; set; }
            public double othallow { get; set; }
            public double othearn { get; set; }
            public RptMonSalaryDataSheet() { }
        }
    }
}





