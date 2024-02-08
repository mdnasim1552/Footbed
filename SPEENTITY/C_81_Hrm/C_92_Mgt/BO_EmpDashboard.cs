using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPEENTITY;

namespace SPEENTITY.C_81_Hrm.F_92_Mgt
{
    public class BO_EmpDashboard
    {
         [Serializable]
        public class empInfo
        {
            public string comcod { set; get; }
            public string rowid { set; get; }
            public string company { set; get; }
            public string secid { set; get; }
            public string desigid { set; get; }
            public string empid { set; get; }
            public string idcardno { set; get; }
            public string companyname { set; get; }
            public string section { set; get; }
            public string desig { set; get; }
            public string empname { set; get; }
            public DateTime joindate { set; get; }
            public DateTime Idcardvaldate { set; get; }
            public DateTime birthdate { set; get; }
            public string slength { set; get; }
            public byte[] empimage { set; get; }
            public string empimage1 { set; get; }
            public byte[] empsign { get; set; }
            public byte[] mangerempsign { get; set; }
            public string empaddress { get; set; }
            public string mangmobile { get; set; }
            public string compadd { get; set; }
            public string deptcode { set; get; }
            public string bloodg { set; get; }
            public empInfo() { }
        }

         [Serializable]
         public class empInfoBangla
         {
             public string comcod { set; get; }
             public string rowid { set; get; }
             public string company { set; get; }
             public string secid { set; get; }
             public string desigid { set; get; }
             public string empid { set; get; }
             public string idcardno { set; get; }
             public string hrcomname { set; get; }
             public string hrcomadd { set; get; }
            public string companyname { set; get; }
            public string companynameBn { set; get; }
            public string companyaddbn { set; get; }
            public string section { set; get; }
             public string desig { set; get; }
             public string desigBn { set; get; }
            public string empname { set; get; }
            public string empnameBn { set; get; }
             public string joindate { set; get; }
             public string joindateng { set; get; }
             public string Idcardvaldate { set; get; }
             public string birthdate { set; get; }
             public string slength { set; get; }
             public byte[] empimage { set; get; }
             public string empimage1 { set; get; }
             public byte[] empsign { get; set; }
             public string empsign1 { get; set; }
            public byte[] mangerempsign { get; set; }
             public string empaddress { get; set; }
             public string mangmobile { get; set; }
             public string compadd { get; set; }
             public string deptcode { set; get; }
             public string deptdesc { set; get; }
             public string bloodg { set; get; }
             public string nid { set; get; }
             public string predistname { set; get; }
             public string perupzilname { set; get; }
             public string preupzilname { set; get; }
             public string prepostname { set; get; }
             public string previllname { set; get; }
             public string perdistname { set; get; }
             public string pervillname { set; get; }
            public string perpostname { set; get; }
            public string nationalid { set; get; }
            public string worktype { set; get; }
            public string empmobile { set; get; }            
            public empInfoBangla() { }
         }

        [Serializable]

        public class EclDashLeave
        {
            public string descrip { set; get; }
            public string empid { set; get; }
            public double entitle { set; get; }
            public double std { set; get; }
            public double actual { set; get; }
            public double exaces { set; get; }
            public double bal { set; get; }
            public double appl { set; get; }
            public double approved { set; get; }
            public double total { set; get; }

            public EclDashLeave() { }
            public EclDashLeave(string descrip, string empid, double entitle, double std, double actual, double exaces, double bal, double appl, double approved, double total)
            {
                this.descrip = descrip;
                this.empid = empid;
                this.entitle = entitle;
                this.std = std;
                this.actual = actual;
                this.exaces = exaces;
                this.bal = bal;
                this.appl = appl;
                this.approved = approved;
                this.total = total;
            }

        }

        [Serializable]
        public class hrmbassicinfo
        {
            public string comcod { set; get; }
            public string gph { set; get; }
            public string gcod { set; get; }
            public string gval { set; get; }
            public string empid { set; get; }
            public string gdesc { set; get; }
            public string gdesc1 { set; get; }
            public string gdesc2 { set; get; }
            public hrmbassicinfo() { }
        }
        [Serializable]

        public class hrmAcademicInfo
        {
            public string gcod { get; set; }
            public string subcode { get; set; }
            public string majsubcode { get; set; }
            public string gval { get; set; }
            public string degree { get; set; }
            public string egroup { get; set; }
            public string instcode { get; set; }
            public string resultcode { get; set; }
            public string markorgrade { get; set; }
            public string pyear { get; set; }
            public string result { get; set; }
            public string majsubject { get; set; }
            public string brcode { get; set; }
            public string board { get; set; }
            public string institute { get; set; }
            public string scale { get; set; }

            public hrmAcademicInfo()
            {

            }
        }

        [Serializable]

        // comcod=@Comp1, a.gcod, a.gval, comname=isnull(b.comname, ''), desig=isnull(b.desig, ''), duration=isnull(b.duration,'')

        public class hrmEmpRecordInfo
        {
            public string gcod { get; set; }
            public string gval { get; set; }
            public string comname { get; set; }
            public string desig { get; set; }
            public string duration { get; set; }

            public hrmEmpRecordInfo()
            {

            }
        }

        [Serializable]

        public class hrmEmpSalaryinfo
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double gval { get; set; }
            public double percnt { get; set; }



            public hrmEmpSalaryinfo()
            {

            }
        }

        [Serializable]

        public class EclDashServHistory
        {
            public string companyname { set; get; }
            public string descrip { get; set; }
            public string date { get; set; }
            public double presal { get; set; }
            public double incrsal { get; set; }
            public double tosalary { get; set; }
            public string desig { get; set; }
            public string section { get; set; }
            public double growth { get; set; }
            public int monthdif { get; set; }


            public EclDashServHistory() { }

            public EclDashServHistory(string companyname, string descrip, string date, double presal, double incrsal, double tosalary, string desig, string section, double growth, int monthdif)
            {
                this.companyname = companyname;
                this.descrip = descrip;
                this.date = date;
                this.presal = presal;
                this.incrsal = incrsal;
                this.tosalary = tosalary;
                this.desig = desig;
                this.section = section;
                this.growth = growth;
                this.monthdif = monthdif;
            }



        }


        [Serializable]
        public class EclLeavGraph
        {
            public string gcod { get; set; }
            public string descrip { get; set; }
            public double val { get; set; }
            public EclLeavGraph() { }
            public EclLeavGraph(string gcod, string descrip, double val)
            {
                this.gcod = gcod;
                this.descrip = descrip;
                this.val = val;
            }


        }


        [Serializable]
        public class EclATTGraph
        {
            public string gcod { get; set; }
            public string descrip { get; set; }
            public double val { get; set; }
            public double prcval { get; set; }
            public string daysa { get; set; }
            public EclATTGraph() { }
            public EclATTGraph(string gcod, string descrip, double val, double prcval, string daysa)
            {
                this.gcod = gcod;
                this.descrip = descrip;
                this.val = val;
                this.prcval = prcval;
                this.daysa = daysa;
            }


        }

        [Serializable]
        public class EclAttMnthHist
        {

            public string mname { get; set; }
            public double intime { get; set; }
            public double lte { get; set; }
            public double eleave { get; set; }
            public double leave { get; set; }
            public double absdat { get; set; }
            public double holidya { get; set; }
            public double total { get; set; }
            public EclAttMnthHist() { }
            public EclAttMnthHist(string mname, double intime, double lte, double eleave, double leave, double absdat, double holidya, double total)
            {
                this.mname = mname;
                this.intime = intime;
                this.lte = lte;
                this.eleave = eleave;
                this.leave = leave;
                this.absdat = absdat;
                this.holidya = holidya;
                this.total = total;
            }


        }

        [Serializable]
        public class ListData
        {
            public List<EclDashLeave> lst1 { get; set; }
            public List<EclLeavGraph> lst2 { get; set; }
            public List<EclDashServHistory> lst3 { get; set; }
            public List<EclATTGraph> lst4 { get; set; }
            public List<EclAttMnthHist> lst5 { get; set; }
            //office time information
            public List<hrmEmpSalaryinfo> lst6 { get; set; }
        }

        [Serializable]
        public class EmpPerAppraisal01
        {
     //       select comcod,  empsection,  empid,   empidcardno,  empjoindate,  empname, empdesig,   empgssal, incamtadate,  svisorid,  ssection,  sidcardno, sname,
     //sdesig,   sgssal,  sserlength, perdate, refno, evaperiod='' from #tblfinempasupinfo

            public string comcod { get; set; }
            public string empsection { get; set; }
            public string empid { get; set; }
            public string empidcardno { get; set; }
            public string empjoindate { get; set; }
            public string empname { get; set; }
            public string empdesig { get; set; }
            public double empgssal { get; set; }
            public string incamtadate { get; set; }
            public string svisorid { get; set; }
            public string ssection { get; set; }
            public string sidcardno { get; set; }
            public string sname { get; set; }
            public string sdesig { get; set; }
            public double sgssal { get; set; }
            public string sserlength { get; set; }
            public string perdate { get; set; }
            public string refno { get; set; }
            public string evaperiod { get; set; }
            public string gradedes { get; set; }      
            public decimal incment { get; set; }
            public decimal grspay { get; set; }
        }

        [Serializable]
        public class EmpPerAppraisal02
           {
            public string comcod { get; set; }
            public string gcod { get; set; }
            public string sgcod1 { get; set; }
            public string sgcod2 { get; set; }
            public string sgcod3 { get; set; }
            public string sgcod4 { get; set; }
            public string sgcod5 { get; set; }
            public string sgval1 { get; set; }
            public string sgval2 { get; set; }
            public string sgval3 { get; set; }
            public string sgval4 { get; set; }
            public string sgval5 { get; set; }
            public string gdesc { get; set; }
            public string dgdesc { get; set; }
            public string sgdesc1 { get; set; }
            public string sgdesc2 { get; set; }
            public string sgdesc3 { get; set; }
            public string sgdesc4 { get; set; }
            public string sgdesc5 { get; set; }
            public string smgdesc1 { get; set; }
            public string smgdesc2 { get; set; }
            public string smgdesc3 { get; set; }
            public string smgdesc4 { get; set; }
            public string smgdesc5 { get; set; }
            public double wagemark { get; set; }
            public double wavgp { get; set; }
           }

        [Serializable]
        public class EmpOffDaySetup
        {
            public string comcod { get; set; }
            public string deptcod { get; set; }
            public string monthid { get; set; }
            public string sdate { get; set; }
            public string sdate1 { get; set; }
            public string reason { get; set; }
            public string wekend { get; set; }
            public string holiday { get; set; }
            public string notes { get; set; }
        }


        [Serializable]
        public class AllEmpInformationGrpwise
        {
            public string comcod { set; get; }
            public string rowid { set; get; }
            public string company { set; get; }
            public string secid { set; get; }
            public string desigid { set; get; }
            public string empid { set; get; }
            public string idcardno { set; get; }
            public string companyname { set; get; }
            public string compaddeng { set; get; }
            public string compnameeng { set; get; }

            public string companynameBn { set; get; }
            public string companyaddbn { set; get; }
            public string section { set; get; }
            public string sectioneng { set; get; }
           
            public string desig { set; get; }
            public string desigeng { set; get; }
            public string empname { set; get; }
            public string empnamebn { set; get; }
            public string fathname { set; get; }
            public string fathnamebn { set; get; }
            public string mothname { set; get; }
            public string mothnamebn { set; get; }
            public string spounam { set; get; }
            public string spounambn { set; get; }       
            public string joindateng { set; get; }
            public string joindate { set; get; }
            public string jointdat { set; get; }
            public DateTime jointdat1 { set; get; }
            public string birthdate { set; get; }
            public DateTime birthdateEng { set; get; }
            public string slength { set; get; }
            public byte[] empimage { set; get; }
            public byte[] empsign { get; set; }
            public byte[] mangerempsign { get; set; }
            public string empaddress { get; set; }
            public string mangmobile { get; set; }
            public string compadd { get; set; }
            public string deptcode { set; get; }        
            public string deptname { set; get; }        
            public string nid { set; get; }
            public string empgrad { set; get; }
            public string predistname { set; get; }
            public string preupzilname { set; get; }
            public string prepostname { set; get; }
            public string previllname { set; get; }
            public string perdistname { set; get; }
            public string perdistneng { set; get; }
            public string perupzilname { set; get; }
            public string perupzeng { set; get; }
            public string perpostname { set; get; }
            public string pervillname { set; get; }
          
            public string nationalid { set; get; }
            public double bsal { get; set; }
            public double hrent { get; set; }
            public double mrent { get; set; }
            public double conven { get; set; }
            public double food { get; set; }
            public double totalsal { get; set; }
            public string tkinword { get; set; }
            public string jobloc { get; set; }
            public string jobdesc { get; set; }
            public string empstatus { get; set; }
            public string empsl { get; set; }
            
            public string provperiod { get; set; }
            public string signatorydesc { get; set; }
            public string signatorydescbn { get; set; }
            public string signdesigen { get; set; }
            public string signdesigbn { get; set; }
            public string acno { get; set; }
            public string bankname { get; set; }
            public string bankadd { get; set; }
            public double empage { get; set; }
            public string empreligion { get; set; }
            public string empedu { get; set; }
            public string empheight { get; set; }
            public string empweight { get; set; }
            public string empcontact { get; set; }
            public string empmail { get; set; }
            public string empmaritalstatus { get; set; }
            public string worktype { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public string linedescbn { get; set; }

            public void calculate()
            {
                empsl = empsl.Substring(0, 3) + "-" + empsl.Substring(4);
            }
            
            public AllEmpInformationGrpwise() 
            {
                 

            }
        }

    }
}