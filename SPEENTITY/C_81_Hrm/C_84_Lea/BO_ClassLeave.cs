using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_84_Lea
{
    public class BO_ClassLeave
    {
        [Serializable]
        public class EmpLeaveInfo
        {

           
            public string empid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string idcardno { get; set; }
            public string joindate { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string section { get; set; }
            public string desigadept { get; set; }
            public string companyname { get; set; }
            public double enleave { get; set; }
            public double cleave { get; set; }
            public double total { get; set; }
            public double sleave { get; set; }
            public double matlev { get; set; }
            public double wpaylev { get; set; }
            public double trainlev { get; set; }
            public double patelev { get; set; }
            public double hajjlev { get; set; }
            


            public EmpLeaveInfo() { }
        }
        [Serializable]
        public class RptMonWiseEmpLeave
        {

            public string comcod { get; set; }
            public string company { get; set; }
            public string empid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string idcardno { get; set; }
            public string joindate { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string section { get; set; }
            public string desigadept { get; set; }
            public string companyname { get; set; }
            public double enleave { get; set; }
            public double cleave { get; set; }
            public double balleave { get; set; }
            public double sleave { get; set; }
            public double lv1 { get; set; }
            public double lv2 { get; set; }
            public double lv3 { get; set; }
            public double lv4 { get; set; }
            public double lv5 { get; set; }
            public double lv6 { get; set; }
            public double lv7 { get; set; }
            public double lv8 { get; set; }
            public double lv9 { get; set; }
            public double lv10 { get; set; }
            public double lv11 { get; set; }
            public double lv12 { get; set; }
            public double lvavailed { get; set; }

            public RptMonWiseEmpLeave() { }
        }
        [Serializable]
        public class RptMonWiseEmpLeaveReg
        {

            public string comcod { get; set; }
            public double oleave { get; set; }
            public double ecleave { get; set; }
            public double esleave { get; set; }
            public string rreason { get; set; }
            public string lreason { get; set; }
            public double eleave { get; set; }
            public string aprvdat { get; set; }
            public double bcleave { get; set; }
            public double bsleave { get; set; }
           

            public RptMonWiseEmpLeaveReg() { }
        }

        [Serializable]
        public class RptEmpLeavStatus
        {

            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string empid { get; set; }
            public string gcod { get; set; }
            public double opleave { get; set; }
            public double enleave { get; set; }
            public double enjleave { get; set; }
            public double balleave { get; set; }
            public string descrip { get; set; }
            public string aplydat { get; set; }
            public string strtdat { get; set; }
            public string enddat { get; set; }
            public double lvday { get; set; }
            public string lreason { get; set; }
            public string gdesc { get; set; }
            public RptEmpLeavStatus() { }
        }

        [Serializable]
        public class EmpLeaveStatus
        {
            public int rowid { get; set; }
            public string comcod { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public string joindate { get; set; }
            public double gssal { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double opleave { get; set; }
            public double enleave { get; set; }
            public double enjleave { get; set; }
            public double balleave { get; set; }
            public string lstrtdat { get; set; }
            public string descrip { get; set; }

            public EmpLeaveStatus() { }
        }
        [Serializable]
        public class EmpLeaveStatusFb
        {
            public int rowid { get; set; }
            public string comcod { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public double opleave { get; set; }
            public double enballeave { get; set; }
            public double enleave { get; set; }
            public double enjleave { get; set; }
            public double clballeave { get; set; }
            public double clenleave { get; set; }
            public double clenjleave { get; set; }
            public double slballeave { get; set; }
            public double slenleave { get; set; }
            public double slenjleave { get; set; }
            public double mlballeave { get; set; }
            public double mlenleave { get; set; }
            public double mlenjleave { get; set; }


            public EmpLeaveStatusFb() { }
        }
        [Serializable]
        public class LvApproval
        {
            public string comcod { get; set; }
            public string ltrnid { get; set; }
            public string lvtype { get; set; }
            public string gcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public DateTime strtdat { get; set; }
            public DateTime enddat { get; set; }
            public DateTime aplydat { get; set; }
            public string desig { get; set; }
            public string deptanme { get; set; }
            public double duration { get; set; }

        }
        [Serializable]

        public class GetLvId
        {
            public string ltrnid { get; set; }
        }

        [Serializable]
        public class LvApllication
        {

            public string ltrnid { get; set; }
            public string empid { get; set; }
            public string gcod { get; set; }
            public string strtdat { get; set; }
            public string enddat { get; set; }
            public string aplydat { get; set; }
            public string lreason { get; set; }
            public string lrmarks { get; set; }
            public string aprdat { get; set; }
            public string addlentime { get; set; }
            public string denameadesig { get; set; }

            //public LvApllication() { }
            //public LvApllication(string ltrnid, string empid, string gcod, string strtdat, string enddat, string aplydat, string lreason,
            //    string lrmarks, string aprdat, string addlentime, string denameadesig)
            //{
            //    this.ltrnid = ltrnid;
            //    this.empid = empid;
            //    this.gcod = gcod;
            //    this.strtdat = strtdat;
            //    this.enddat = enddat;
            //    this.aplydat = aplydat;
            //    this.lreason = lreason;
            //    this.lrmarks = lrmarks;
            //    this.addlentime = addlentime;
            //    this.denameadesig = denameadesig;
            //    this.aprdat = aprdat;                
            //}
        }



        [Serializable]
        public class LvStatus
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double entitle { get; set; }
            public double permonth { get; set; }
            public double pbal { get; set; }
            public double ltaken { get; set; }
            public double balleave { get; set; }
            public double tltakreq { get; set; }
            public string lenjoydt1 { get; set; }

            public string lenjoydt2 { get; set; }
            public double lenjoyday { get; set; }
            public string appdate { get; set; }
            public string applydate { get; set; }
            public double appday { get; set; }
            public double applyday { get; set; }
            public LvStatus() { }
            public LvStatus(string gcod, double entitle, double permonth, double pbal, double ltaken, double balleave, double tltakreq, string lenjoydt1,
                string lenjoydt2, double lenjoyday, string appdate, string applydate, double appday, double applyday)
            {
                this.gcod = gcod;
                this.entitle = entitle;
                this.permonth = permonth;
                this.pbal = pbal;
                this.ltaken = ltaken;
                this.balleave = balleave;
                this.tltakreq = tltakreq;
                this.lenjoydt1 = lenjoydt1;
                this.lenjoydt2 = lenjoydt2;
                this.lenjoyday = lenjoyday;
                this.appdate = appdate;
                this.applydate = applydate;
                this.appday = appday;
                this.applyday = applyday;

            }

        }


        [Serializable]
        public class LvRecentApplStatus
        {
            public string grpsl { get; set; }
            public string grpdesc { get; set; }
            public string ltrnid { get; set; }
            public string gcod { get; set; }
            public string aplydat { get; set; }
            public string lstrtdat { get; set; }
            public string lenddat { get; set; }
            public double enjoyday { get; set; }
            public string lreason { get; set; }
            public string lrmarks { get; set; }
            public string gdesc { get; set; }

            public LvRecentApplStatus() { }
            public LvRecentApplStatus(string grpsl, string grpdesc, string ltrnid, string gcod, string aplydat, string lenddat, double enjoyday, string lreason, string lrmarks, string gdesc)
            {
                this.grpsl = grpsl;
                this.grpdesc = grpdesc;
                this.ltrnid = ltrnid;
                this.gcod = gcod;
                this.aplydat = aplydat;
                this.lenddat = lenddat;
                this.enjoyday = enjoyday;
                this.lreason = lreason;
                this.lrmarks = lrmarks;
                this.gdesc = gdesc;
            }
        }

        [Serializable]


        public class LvResonList
        {
            public string hrgcod { get; set; }
            public string hrgdesc { get; set; }
        }



        public class ListData
        {
            public List<LvApllication> lst0 { get; set; }
            public List<LvStatus> lst1 { get; set; }
            public List<LvRecentApplStatus> lst2 { get; set; }
            public List<LvResonList> lst3 { get; set; }
            public List<GetLvId> lst4 { get; set; }


        }

        [Serializable]
        public class LeaveApp
        {


            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gdescb { get; set; }
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

        //comcod,   company, grade, deptid, secid, desigid, noemp, empid, idcardno,companyname,  gradedesc, department, section, desig,  empname, gssal, 
        //joindate, leave, tabst, late, jdat,serperiod, divdesc
        [Serializable]
        public class EmpSalInf
        {
            public string comcod { get; set; }
            public string company { get; set; }
            public string grade { get; set; }
            public string deptid { get; set; }
            public string empid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string gradedesc { get; set; }
            public double gssal { get; set; }
            public DateTime joindate { get; set; }
            public double leave { get; set; }
            public double tabst { get; set; }
            public double late { get; set; }
            public string serperiod { get; set; }
            public string idcardno { get; set; }
            public string jdat { get; set; }
            public string divdesc { get; set; }
            public string companyname { get; set; }
            public string department { get; set; }
            public string section { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public double noemp { get; set; }
           
            public EmpSalInf() { }
        }
        [Serializable]
        public class EmpLeanChasment
        {
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string section { get; set; }
            public string gradedesc { get; set; }
            public DateTime joindate { get; set; }
            public string linedesc { get; set; }
            public double grossal { get; set; }
            public DateTime cutoffdate { get; set; }
            public double enjleave { get; set; }
            public double absday { get; set; }
            public double workdayoff { get; set; }
            public double flghday { get; set; }
            public double totlwrkday { get; set; }
            public double physiclday { get; set; }
            public double eleave { get; set; }
            public double ecleave { get; set; }
            public double ecleaveamt { get; set; }
            public EmpLeanChasment() { }
        }
        [Serializable]
        public class EmpMobLst
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string mobileno { get; set; }
            public string status { get; set; }
            public string idcard { get; set; }
            public string empname { get; set; }
            public string empdesig { get; set; }
            public string deptname { get; set; }
            public EmpMobLst() { }
        }
        [Serializable] // Create by Md Ibrahim Khalil Date 22/12/2021
        public class RptEmpLeavStatusInfoEng
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gdescb { get; set; }
            public double entitle { get; set; }
            public double permonth { get; set; }
            public double pbal { get; set; }
            public double ltaken { get; set; }
            public double tltakreq { get; set; }
            public DateTime lenjoydt1 { get; set; }
            public DateTime lenjoydt2 { get; set; }
            public double lenjoyday { get; set; }
            public DateTime appdate { get; set; }
            public double appday { get; set; }
            public DateTime applydate { get; set; }
            public double applyday { get; set; }
            public DateTime lrstrtdat { get; set; }
            public DateTime lrentdat { get; set; }
            public double balleave { get; set; }
            public DateTime strtdat { get; set; }
            public DateTime enddat { get; set; }
            public DateTime aplydat { get; set; }
            public DateTime aprdat { get; set; }
            public string  lapplied { get; set; }
            public string lreason { get; set; }
            public string lrmarks { get; set; }
            public string denameadesig { get; set; }
            public RptEmpLeavStatusInfoEng() { }

        }

        [Serializable]
        public class RptEmpLeavStatus02
        {
            public string rowid { get; set; }
            public string gcod { get; set; }
            public string grp { get; set; }
            public double leavent { get; set; }
            public double leaveenj { get; set; }
            public double balleave { get; set; }
            public string lreason { get; set; }
            public string aprvdat { get; set; }
            public string strtdat { get; set; }
            public string enddat { get; set; }
            public string leavedays { get; set; }
            public string gdesc { get; set; }
            public RptEmpLeavStatus02() { }
        }


        [Serializable]
        public class RptMatLeavePaySheet
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string empnamebn { get; set; }
            public string deptid { get; set; }
            public string deptnamebn { get; set; }
            public string section { get; set; }
            public string sectionname { get; set; }
            public string sectionnamebn { get; set; }
            public double desigid { get; set; }
            public string desig { get; set; }
            public string desigbn { get; set; }
            public string idcardno { get; set; }
            public DateTime joindate { get; set; }
            public string servlength { get; set; }
            public string servlenban { get; set; }
            public double noofchild { get; set; }
            public string linecode { get; set; }
            public string linedescbn { get; set; }
            public string leaveid { get; set; }
            public string tkinword { get; set; }
            public DateTime startdat { get; set; }
            public DateTime enddat { get; set; }
            public DateTime aplydat { get; set; }
            public DateTime aprovdat { get; set; }
            public string leavreason { get; set; }
            public string paymonth { get; set; }
            public double payday { get; set; }
            public double netpay { get; set; }
            public double perdayavg { get; set; }
            public double payable { get; set; }
            public double earnlvamt { get; set; }
            public double occbonus { get; set; }
            public double totalpayable { get; set; }
            public double prepfdeduct { get; set; }
            public double postpfdeduct { get; set; }
            public double pfamt { get; set; }
            public double netpayable { get; set; }
            public double attnbonus { get; set; }
            public bool chk { get; set; }
            public string acno { get; set; }
            public string issuedate { get; set; }
            public string preginfdate { get; set; }
            public string probdeldate { get; set; }
            public string pfmon1 { get; set; }
            public string pfmon2 { get; set; }
            public string pfmon3 { get; set; }
            public string pfmon4 { get; set; }
            public RptMatLeavePaySheet() { }
          
        }

    }
}
