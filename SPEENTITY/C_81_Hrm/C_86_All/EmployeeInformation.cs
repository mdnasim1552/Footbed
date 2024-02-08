using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_86_All
{
    public class EmployeeInformation
    {
        [Serializable]
        public class EmpAllInfo
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string empnamebn { get; set; }
            public DateTime jointdat { get; set; }
            public DateTime confrmdat { get; set; }
            public string section { get; set; }
            public string sectiondesc { get; set; }
            public string desigcod { get; set; }
            public string desigdesc { get; set; }
            public string desigeng { get; set; }
            public string grade { get; set; }
            public string deptcode { get; set; }
            public string deptdesc { get; set; }
            public string deptdesceng { get; set; }
            public string idcard { get; set; }
            public string fathername { get; set; }
            public string mothername { get; set; }
            public DateTime dob { get; set; }
            public string gender { get; set; }
            public string genderdesc { get; set; }
            public string peraddress { get; set; }
            public string predistname { get; set; }
            public string preupzilname { get; set; }
            public string prepostname { get; set; }
            public string previllname { get; set; }
            public string perdistname { get; set; }
            public string perupzilname { get; set; }
            public string perpostname { get; set; }
            public string pervillname { get; set; }
            public string linedesc { get; set; }
            public EmpAllInfo() { }
        }
        [Serializable]
        public class EmpMobBillInfo
        {
            public string comcod { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string empid { get; set; }
            public string totaluser { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public string gcod { get; set; }
            public double mbillamt { get; set; }
            public double payamt { get; set; }
            public double actamt { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string phone { get; set; }
            public string joblocation { get; set; }
            public string joblocdesc { get; set; }
            
            public EmpMobBillInfo() { }
        }

        [Serializable]
        public class RptOtherDedction
        {
            public string deptid { get; set; }
            public string secid { get; set; }
            public string empid { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public double lvded { get; set; }
            public double arded { get; set; }
            public double saladv { get; set; }
            public double mbillded { get; set; }
            public double fallded { get; set; }
            public double otherded { get; set; }
            public double spcded { get; set; }
            public double toamt { get; set; }
            public double gssal { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string linecode { get; set; }
            public string fline { get; set; }

            public RptOtherDedction() { }
        }

        [Serializable]
        public class RptEarnLeaveEnCashment
        {
            public string company { get; set; }
            public string deptid { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string sectionname { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public DateTime joindate { get; set; }
            public double mon1 { get; set; }
            public double mon2 { get; set; }
            public double mon3 { get; set; }
            public double mon4 { get; set; }
            public double mon5 { get; set; }
            public double mon6 { get; set; }
            public double mon7 { get; set; }
            public double mon8 { get; set; }
            public double mon9 { get; set; }
            public double mon10 { get; set; }
            public double mon11 { get; set; }
            public double mon12 { get; set; }
            public double totalpresent { get; set; }
            public double eleave { get; set; }
            public double elenjoyed { get; set; }
            public double payeleave { get; set; }
            public double eneleave { get; set; }
            public double gssal { get; set; }
            public double onedaysal { get; set; }
            public double netpayable { get; set; }
            public string acno { get; set; }
            public double leavenamt { get; set; }
            public RptEarnLeaveEnCashment()
            {

            }
        }

    }
}
