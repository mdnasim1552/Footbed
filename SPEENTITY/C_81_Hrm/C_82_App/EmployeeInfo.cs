using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_82_App
{
    public class EmployeeInfo
    {
         [Serializable]
        public class EmployeeInfoBangla
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string empnamebn { get; set; }
            public string joindat { get; set; }
            public DateTime confrmdat { get; set; }
            public DateTime confrmdat1 { get; set; }
            public string  section { get; set; }
            public string linedesc { get; set; }
            public string linedescb { get; set; }
            public string sectiondesc { get; set; }
            public string desigcod { get; set; }
            public string desigdesc { get; set; }
            public string promdesig { get; set; }
            public string desigeng { get; set; }
            public string grade { get; set; }
            public string deptdesc { get; set; }
            public string deptdesceng { get; set; }
            public string idcard { get; set; }
            public string fathername { get; set; }
            public string mothname { get; set; }
            public string mothername { get; set; }
            public string dob { get; set; }
            public string ageyears { get; set; }            
            public string bloodgrp { get; set; }
            public string gender { get; set; }
            public string genderdesc { get; set; }
            public double noofchild { get; set; }
            public string emgncyconper { get; set; }
            public string emgncyconphone { get; set; }
            public string phone { get; set; }
            public string religion { get; set; }
            public string meritstatus { get; set; }            
            public string predistname { get; set; }
            public string predisteng { get; set; }
            public string preupzilname { get; set; }
            public string preupzileng { get; set; }
            public string prepostname { get; set; }
            public string previllname { get; set; }
            public string perdistname { get; set; }
            public string perdisteng { get; set; }
            public string perupzilname { get; set; }
            public string perupzileng { get; set; }
            public string perpostname { get; set; }
            public string pervillname { get; set; }
            public string nominname { get; set; }
            public string nominrelation { get; set; }
            public string nominnid { get; set; }
            public string nominvillage { get; set; }
            public string nomindist { get; set; }
            public string nominupazila { get; set; }
            public string nominpost { get; set; }
            public double nominage { get; set; }
            public double nominpercnt { get; set; }
            public string spousename { get; set; }
            public double bsal { get; set; }
            public double hrent { get; set; }
            public double medical { get; set; }
            public double conveyance { get; set; }
            public double foodallow { get; set; }
            public double grssal { get; set; }
            public double pf { get; set; }
            public double other { get; set; }
            public double totaldeduc { get; set; }
            public double incramt { get; set; }
            public string inword { get; set; }
            public DateTime sepdate { get; set; }            
            public string secdesceng { get; set; }
            public string servlength { get; set; }
            public string bankname { get; set; }
            public string bankadd { get; set; }
            public string acno { get; set; }
            public string signatorydesc { get; set; }
            public string signdesigen { get; set; }
            public string periodeval { get; set; }
            public byte[] empimage { set; get; }
            public DateTime lstrtdat { get; set; }
            public DateTime lenddat { get; set; }
            public DateTime njoindat { get; set; }
            public DateTime fltrissudat { get; set; }
            public DateTime investdate { get; set; }
            public DateTime secltrissudat { get; set; }
            public EmployeeInfoBangla() { }
        }

        [Serializable]
        public class EmployeeIncrInfo
        {
            public string empid { get; set; }
            public string idcard { get; set; }
            public string desig { get; set; }
            public string signatory { get; set; }
            public string signname { get; set; }
            public string singdesg { get; set; }
            public string section { get; set; }
            public string dept { get; set; }
            public string empname { get; set; }
            public double grossal { get; set; }
            public DateTime incrdate { get; set; }
            public double finincamt { get; set; }
            public string predesig { get; set; }
            public double incamt { get; set; }
            public string refno { get; set; }
            public string strGross { get; set; }
            public string strIncAm { get; set; }
            public string Gross { get; set; }
            public DateTime prodate { get; set; }

            public EmployeeIncrInfo() { }
        }


        [Serializable]
        public class EmployeePromotionInfo
        {
            public string company { get; set; }
            public string empid { get; set; }
            public string idcard { get; set; }
            public string predesig { get; set; }
            public string pastdesig { get; set; }
            public string predesigbn { get; set; }
            public string pastdesigbn { get; set; }
            public string signatory { get; set; }
            public string signname { get; set; }
            public string singdesg { get; set; }
            public string section { get; set; }
            public string sectionbn { get; set; }
            public string deptid { get; set; }
            public string dept { get; set; }
            public string deptbn { get; set; }
            public string empname { get; set; }
            public string empnamebn { get; set; }
            public double prevgssal { get; set; }
            public double prevbsal { get; set; }
            public double prevhrent { get; set; }
            public double curgssal { get; set; }
            public double curbsal { get; set; }
            public double curhrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double fallow { get; set; }
            public double grossal { get; set; }
            public DateTime incrdate { get; set; }
            public DateTime promdate { get; set; }
            public double incamt { get; set; }
            public double finincamt { get; set; }
            public string refno { get; set; }
            public string strGross { get; set; }
            public string strIncAm { get; set; }
            public string Gross { get; set; }
            public string details { get; set; }
            public string desig { get; set; }
            public string desigbn { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public string linedescbn { get; set; }
            public EmployeePromotionInfo() { }
        }


        [Serializable]
        public class EmployeeRegister
        {
            public string empid { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string dept { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string father { get; set; }
            public string mother { get; set; }
            public string pervil { get; set; }
            public string perupazila { get; set; }
            public string perdistrict { get; set; }
            public string postoffice { get; set; }
            public string nid { get; set; }
            public string gender { get; set; }
            public DateTime doj { get; set; }
            public DateTime dob { get; set; }
            public string age { get; set; }
            public string wholiday { get; set; }
            public string offintime { get; set; }
            public string offouttime { get; set; }
            public string shiftname { get; set; }
            public string lintime { get; set; }
            public string louttime { get; set; }
            public string grade { get; set; }
            public string eleave { get; set; }
            public string ltake { get; set; }
            public string offhour { get; set; }
            public string lnchour { get; set; }


            public EmployeeRegister() { }
        }


        public class EmpMonthwiseAbscent
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string idcard { get; set; }
            public string empname { get; set; }
            public DateTime workingday { get; set; }
            public string desig { get; set; }
            public string secid { get; set; }
            public string section { get; set; }
            public string line { get; set; }
            public string linedesc { get; set; }
            public string deptid { get; set; }
            public string joblocid { get; set; }
            public string joblocdesc { get; set; }
            public int totalemp { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
        }


        [Serializable]
        public class EmployeeSkill
        {
            public string empid { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string dept { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public DateTime doj { get; set; }
            public string grade { get; set; }
            public string linedesc { get; set; }
            public double grossal { get; set; }
   
            public EmployeeSkill() { }
        }

        [Serializable]
        public class SuspensionEmpInfo
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string gcod { get; set; }
            public string desig { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public string name { get; set; }
            public string idno { get; set; }
            public SuspensionEmpInfo()
            {

            }
        }
        [Serializable]
        public class SuspensionEmpAbsInfo
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string paldate { get; set; }
            public string empid { get; set; }
            public string estatus { get; set; }
            public int absents { get; set; }
            public SuspensionEmpAbsInfo()
            {

            }
        }
    }
}