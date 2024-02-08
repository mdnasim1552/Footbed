using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPERDLC.RD_81_HRM.RD_89_Pay
{
    public class RpHRtPayroll
    {
        [Serializable]
        public class SalarySheet
        {
            public string comcod { get; set; }
            public int rowid { get; set; }
            public string empid { get; set; }
            public string grade { get; set; }
            public DateTime joindate { get; set; }
            public double wd { get; set; }
            public double wjd { get; set; }
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
            public string empname { get; set; }
            public string refno { get; set; }
            public string refdesc { get; set; }
            public string sirdesc { get; set; }
            public string sectionname { get; set; }
            public double asloanins { get; set; }
            public double asloanbal { get; set; }
            public double asloanbal2 { get; set; }
            public double gssal1 { get; set; }
            public double dalday { get; set; }
            public double sdedamt { get; set; }
            public double aritax { get; set; }
            public double arpfund { get; set; }
            public string jobloc { get; set; }
            public string costcent { get; set; }
            public double spcded { get; set; }
            public double yrincamt { get; set; }


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
            public BankFord() { }
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
    }
}
