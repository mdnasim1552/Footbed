using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_92_Mgt
{
    public class EClassHrInterface
    {
        [Serializable]
        public class HrInterfaceLeave {

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
            public DateTime applydate { get; set; }
            public DateTime lrstrtdat { get; set; }
            public DateTime lrentdat { get; set; }


        }


        [Serializable]
        public class EclassSepEmployee
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string empnamebn { get; set; }
            public string empname1 { get; set; }
            public string septype { get; set; }
            public string septypedesc { get; set; }
            public DateTime retdat { get; set; }
            public DateTime effectdate { get; set; }
            public DateTime joindat { get; set; }
            public string idno { get; set; }
            public string designation { get; set; }
            public string designationbn { get; set; }
            public string deptcode { get; set; }
            public string deptname { get; set; }
            public string deptnamebn { get; set; }
            public string section { get; set; }
            public string sectionbn { get; set; }
            public string servleng { get; set; }
            public string servlenban { get; set; }
            public DateTime billdate { get; set; }
            public bool aprvstatus { get; set; }
            public double ttlamt { get; set; }
            public string daysconmonth { get; set; }
            public string noticeduration { get; set; }
            public string noticedurban { get; set; }
            public string refno { get; set; }


        }
        [Serializable]
        public class EclassEmployeeLst
        {
            public string comcod { get; set; }
            public string fcompcod { get; set; }
            public string fcompname { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string lineid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string line { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public DateTime joindate { get; set; }
            public string slength { get; set; }
            public string grade { get; set; }
            public double attnbon { get; set; }
            public double grossal { get; set; }
            public DateTime birthdate { get; set; }
            public string sex { get; set; }
            public DateTime sepdate { get; set; }
            public string mobno { get; set; }
            public string spcause { get; set; }
            public string empstatus { get; set; }
            public string refid { get; set; }
            public string empage { get; set; }
            public string gradeid { get; set; }
            public string remarks { get; set; }
            public string empimage { get; set; }
            public string gender { get; set; }

            public EclassEmployeeLst() { }

        }
        public class EclassEmployeeTransLst
        {
            public string comcod { get; set; }
            public string fcompcod { get; set; }
            public string fcompname { get; set; }
            public string tcompcod { get; set; }
            public string tcompname { get; set; }
            public string tfprjcode { get; set; }
            public string tfdeptname { get; set; }

            public string ttprjcode { get; set; }
            public string ttdeptname { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public DateTime tdate { get; set; }
            public string fdesigid { get; set; }
            public string fdesig { get; set; }
            public string tdesigid { get; set; }
            public string tdesig { get; set; }
            public string rmrks { get; set; }

            public EclassEmployeeTransLst() { }

        }
        [Serializable]
        public class EclassSttlemntInfo
        {
            public string hrgcod { get; set; }
            public string hrgdesc { get; set; }
            public string partdesc { get; set; }
            public string unit { get; set; }
            public double amount { get; set; }
            public double numofday { get; set; }
            public double perday { get; set; }
            public double ttlamt { get; set; }
            public string remarks { get; set; }

        }
        [Serializable]
        public class EclassSettCompanyInfo
        {
            public string company { get; set; }
            public string refno { get; set; }
            public string companyname { get; set; }
            public string companyadd { get; set; }
            public EclassSettCompanyInfo() { }
        }

        [Serializable]
        public class SummarySalarySheet
        {
            public string comcod { get; set; }
            public double bankemp { get; set; }
            public double cashemp { get; set; }
            public double toemp { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double toamt { get; set; }
            public string sectionname { get; set; }
            public SummarySalarySheet() { }
        }

        public class BO_EmpConfirm
        {
            //a.comcod, a.cardno, a.empid, a.empname, a.desig, a.joindat, a.condat, a.refno, remarks='', chkmv='0',
            //    bsal=isnull(c.bsal,0.00), hrent=isnull(c.hrent,0.00), mrent=isnull(c.mallow,0.00), 
            //    food=isnull(c.foodalw,0.00), conven=isnull(cven,0.00), totalsal=isnull((bsal+hrent+mallow+foodalw),0.00), tkinword=convert(nvarchar(250), ''), empstatus='0'
            public string comcod { get; set; }
            public string cardno { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string compnameeng { get; set; }
            public string compaddeng { get; set; }
            public string deptname { get; set; }
            public DateTime joindat { get; set; }
            public DateTime condat { get; set; }
            public string remarks { get; set; }
            public Boolean chkmv { get; set; }
            public double bsal { get; set; }
            public double hrent { get; set; }
            public double mrent { get; set; }
            public double food { get; set; }
            public double conven { get; set; }
            public double totalsal { get; set; }
            public string totalsalInword { get; set; }
            public string tkinword { get; set; }
            public string signatory { get; set; }
            public string signname { get; set; }
            public string signdesig { get; set; }
            public double oldsal { get; set; }
            public string oldsalInword { get; set; }
            public string increSalInword { get; set; }
            public double increSal { get; set; }
            public string joblocadd { get; set; }
            public string slno { get; set; }



        }
        public class EmpPromotion
        {

            public string comcod { get; set; }
            public string empid { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string gradeid { get; set; }
            public string desigid { get; set; }
            public DateTime prodate { get; set; }
            public string presal { get; set; }
            public DateTime joindate { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string grade { get; set; }
            public string desig { get; set; }
            public string linecod { get; set; }
            public string linedesc { get; set; }
            public string pgcod { get; set; }
            public string rgcod { get; set; }
            public string pgdesc { get; set; }

        }


        public class RptEmpConfirmation
        {
            public string rowid { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string grade { get; set; }
            public double gssal { get; set; }
            public DateTime joindate { get; set; }
            public DateTime condate { get; set; }
            public string companyname { get; set; }
            public string department { get; set; }
            public string section { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public string remarks { get; set; }
            public string postedbyid { get; set; }
            public string posteduser { get; set; }

            public RptEmpConfirmation () { }
        }

        [Serializable]
        public class RptEmpSeparation
        {

            public string comcod { get; set; }
            public string company { get; set; }
            public string compname { get; set; }
            public string deptcode { get; set; }
            public string dept { get; set; }
            public string section { get; set; }
            public string secname { get; set; }
            public string idcardno { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string linecod { get; set; }
            public string lindesc { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public DateTime spdate { get; set; }
            public string sptype { get; set; }
            public string spdesc { get; set; }
            public DateTime joiningdate { get; set; }
            public string duration { get; set; }
            public double gssal { get; set; }
            public string sempid { get; set; }
            public string signatory { get; set; }
            public string signadesig { get; set; }
            public string refno { get; set; }
            public string mobileno { get; set; }

            public RptEmpSeparation()
            {
                    
            }
        }

    }
}
