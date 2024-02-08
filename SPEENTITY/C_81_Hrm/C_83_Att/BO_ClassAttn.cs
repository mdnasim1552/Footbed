using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_83_Att
{
    public class BO_ClassAttn
    {
         [Serializable]
        public class EmpSatausLate
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public DateTime stdtimein { get; set; }
            public DateTime stdtimeout { get; set; }
            public DateTime actualin { get; set; }
            public DateTime actualout { get; set; }
            public string empdeptid { get; set; }
            public string empdept { get; set; }
            public string empnam { get; set; }
            public string empdsg { get; set; }
            public string addtime2 { get; set; }
            public string addday { get; set; }
            public DateTime wintime { get; set; }
            public DateTime wouttime { get; set; }
            public EmpSatausLate() { }
        }
        [Serializable]
        public class DailyAttenCHLGroupWize
        {
            public string grp { get; set; }
            public string company { get; set; }
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string grpdesc { get; set; }
            public string deptdesc { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string sectionid { get; set; }
            public string desig { get; set; }
            public string empdsg { get; set; }
            public string empname { get; set; }
            public DateTime offintime { get; set; }
            public DateTime offouttime { get; set; }
            public DateTime intime { get; set; }
            public DateTime outtime { get; set; }
            public string late { get; set; }
            public string status { get; set; }
            public string eleave { get; set; }
            public string companyname { get; set; }
            public string desigadept { get; set; }
            public string rmrks { get; set; }
            public string remarks { get; set; }
            public double lpmday { get; set; }
            public double lcmday { get; set; }
            public double lvcurm { get; set; }
            public double today { get; set; }
            public double abscurm { get; set; }
            public DailyAttenCHLGroupWize() { }
        }
        [Serializable]
        public class MonthlyLateAttdendace
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string compnany { get; set; }
            public string companyname { get; set; }
            public string deptid { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string sectionname { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public double col1 { get; set; }
            public double col2 { get; set; }
            public double col3 { get; set; }
            public double col4 { get; set; }
            public double col5 { get; set; }
            public double col6 { get; set; }
            public double col7 { get; set; }
            public double col8 { get; set; }
            public double col9 { get; set; }
            public double col10 { get; set; }
            public double col11 { get; set; }
            public double col12 { get; set; }
            public double col13 { get; set; }
            public double col14 { get; set; }
            public double col15 { get; set; }
            public double col16 { get; set; }
            public double col17 { get; set; }
            public double col18 { get; set; }
            public double col19 { get; set; }
            public double col20 { get; set; }
            public double col21 { get; set; }
            public double col22 { get; set; }
            public double col23 { get; set; }
            public double col24 { get; set; }
            public double col25 { get; set; }
            public double col26 { get; set; }
            public double col27 { get; set; }
            public double col28 { get; set; }
            public double col29 { get; set; }
            public double col30 { get; set; }
            public double col31 { get; set; }
            public double totallate { get; set; }
            public MonthlyLateAttdendace()
            {
                    
            }
        }

        [Serializable]
        public class DayAttn
        {
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string intime { get; set; }
            public string outtime { get; set; }
            public string offintime { get; set; }
            public string offouttime { get; set; }
            public string lnchintime { get; set; }
            public string lnchouttime { get; set; }
            public double addhour { get; set; }

            public DayAttn()
            {
            }
        }
        [Serializable]
        public class EmpDayAttnSumry
        {
            public string idcardno { get; set; }
            public string deptid { get; set; }
            public string deptdesc { get; set; }
            public string section { get; set; }
            public string secid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string offintime { get; set; }
            public string offouttime { get; set; }
            public string intime { get; set; }
            public string outtime { get; set; }
            public string late { get; set; }
            public string status { get; set; }
            public EmpDayAttnSumry()
            {
            }
        }
        [Serializable]
        public class AttenClass
        {
            public string empid { get; set; }
            public string machid { get; set; }
            public string idcardno { get; set; }
            public string intime { get; set; }
            public string outtime { get; set; }
            public string offintime { get; set; }
            public string offoutime { get; set; }
            public string lnintime { get; set; }
            public string lnoutime { get; set; }
            public string addhour { get; set; }
        }
        //a.comcod, a.secid,secdesc=ISNULL(b.sirdesc,''), a.present,a.leave,a.absent1,a.hoyday,total
        [Serializable]
        public class DayAttnSumry
        {
            public string comcod { get; set; }
            public string company { get; set; }
            public string companyname { get; set; }
            public string deptid { get; set; }
            public string deptname { get; set; }
            public string secid { get; set; }
            public string secdesc { get; set; }
            public decimal present { get; set; }
            public decimal leave { get; set; }
            public decimal absent1 { get; set; }
            public decimal holiday { get; set; }
            public decimal total { get; set; }

            public DayAttnSumry()
            {
            }
        }
        //totalemp, totalmale, totalfemale, totalpremale, totalprefemale, totalpresent, totalabsent, totalleave
        [Serializable]
        public class DailyAttnSummary
        {
            public double totalemp { get; set; }
            public double totalmale { get; set; }
            public double totalfemale { get; set; }
            public double totalpremale { get; set; }
            public double totalprefemale { get; set; }
            public double totalpresent { get; set; }
            public double totalabsent { get; set; }
            public double totalleave { get; set; }
            public DailyAttnSummary()
            {
            }
        }

        [Serializable]
        public class EclAttMnthWise
        {
            public string wintime { get; set; }
            public string actualin { get; set; }
            public string actualout { get; set; }
            public string leav { get; set; }
            public double dedtimePenal1 { get; set; }
            public string actTimehour { get; set; }
            public EclAttMnthWise()
            {

            }
        }

        [Serializable]
        public class ListData
        {
            public List<EclAttMnthWise> lst1 { get; set; }

        }

        [Serializable]
        public class Shifting01
        {
            public DateTime date { get; set; }
            public DateTime officein { get; set; }
            public DateTime officeout { get; set; }
        }
        [Serializable]
        public class EmpAttendncLog
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string desg { get; set; }
            public string depart { get; set; }
            public string depname { get; set; }
            public string idcard { get; set; }
            public string machine { get; set; }
            public string faceId { get; set; }
            public string linedesc { get; set; }
            public DateTime cdate { get; set; }
            public string logs { get; set; }
        }



        [Serializable]
        public class DailyLateAndAbsent
        {
            public string comcod { get; set; }
            public string empid { get; set; }        
            public string company { get; set; }
            public string companyname { get; set; }
            public string secid { get; set; }
            public string secsion { get; set; }
            public string pactcode { get; set; }
            public string department { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string designation { get; set; }
            public string dayid { get; set; }
            public string dayname { get; set; }
            public string intime { get; set; }
            public string offintime { get; set; }
            public string latetime { get; set; }
            public string remarks { get; set; }
            public string linecode { get; set; }
            public string fline { get; set; }
            public string mobile { get; set; }
            public int totalday { get; set; }
        }
        [Serializable]
        public class DailyLateAndAbsentFb
        {
            public string comcod { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string lineid { get; set; }
            public string line { get; set; }
            public string empid { get; set; }
            public string desigid { get; set; }
            public string absday { get; set; }
            public DateTime lpredate { get; set; }
            public string idcardno { get; set; }
            public string machineid { get; set; }
            public string empname { get; set; }
            public DateTime joindate { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public byte[] empimg { get; set; }
            public string empmobile { get; set; }
           
        }

        [Serializable]
        public class EclassAttApp
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string designation { get; set; }
            public string department { get; set; }
            public string empname { get; set; }
            public string pactcode { get; set; }
            public string dayid { get; set; }
            public string daynam { get; set; }
            public DateTime offintime { get; set; }
            public string remarks { get; set; }
            public string idcardno { get; set; }
            public string absaprbyid { get; set; }
            public string absapruser { get; set; }




        }
        [Serializable]
        public class EmpAttnIdWise
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string empid { get; set; }
            public string empnam { get; set; }
            public string machid { get; set; }
            public string idcardno { get; set; }
            public string spotid { get; set; }
            public DateTime stdtimein { get; set; }
            public DateTime stdtimeout { get; set; }
            public DateTime stdlunchtime { get; set; }
            public DateTime actualin { get; set; }
            public DateTime actualout { get; set; }
            public string actTimehour { get; set; }
            public double dedtimePenal1 { get; set; }
            public string dedtimeLunc { get; set; }
            public string dedout { get; set; }
            public string addhour { get; set; }
            public string addoffhour { get; set; }
            public string ActualWhour { get; set; }
            public string lateappv { get; set; }
            public string absstatus { get; set; }
            public string LeaveST { get; set; }
            public string attnhour { get; set; }
            public string attnminute { get; set; }
            public string actualattnminute { get; set; }
            public string attntimeminute { get; set; }
            public string addtime2 { get; set; }
            public DateTime wintime { get; set; }
            public DateTime wouttime { get; set; }
            public string latetime { get; set; }
            public string earlytime { get; set; }
            public string overtime { get; set; }
            public string lockfl { get; set; }
            public string addday { get; set; }
            public string empdept { get; set; }
            public string empdsg { get; set; }
            public string leav { get; set; }
            public string lt { get; set; }
            public DateTime joindate { get; set; }
            public string remarks { get; set; }

        }

        [Serializable]
        public class EmpAttendence
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string cardno { get; set; }
            public string desig { get; set; }
            public string dept { get; set; }
            public string deptname { get; set; }
            public string empdsgid { get; set; }
            public string empdsg { get; set; }
            public string addday { get; set; }
            public string addtime1 { get; set; }
            public string addtime2 { get; set; }
            public string empnam { get; set; }
            public string idcardno { get; set; }
            public string addtime3 { get; set; }
            public string empdeptid { get; set; }
            public string empdept { get; set; }
            public string sectionid { get; set; }
            public string col1 { get; set; }
            public string col1o { get; set; }
            public string col2 { get; set; }
            public string col2o { get; set; }
            public string col3 { get; set; }
            public string col3o { get; set; }
            public string col4 { get; set; }
            public string col4o { get; set; }
            public string col5 { get; set; }
            public string col5o { get; set; }
            public string col6 { get; set; }
            public string col6o { get; set; }
            public string col7 { get; set; }
            public string col7o { get; set; }
            public string col8 { get; set; }
            public string col8o { get; set; }
            public string col9 { get; set; }
            public string col9o { get; set; }
            public string col10 { get; set; }
            public string col10o { get; set; }
            public string col11 { get; set; }
            public string col11o { get; set; }
            public string col12 { get; set; }
            public string col12o { get; set; }
            public string col13 { get; set; }
            public string col13o { get; set; }
            public string col14 { get; set; }
            public string col14o { get; set; }
            public string col15 { get; set; }
            public string col15o { get; set; }
            public string col16 { get; set; }
            public string col16o { get; set; }
            public string col17 { get; set; }
            public string col17o { get; set; }
            public string col18 { get; set; }
            public string col18o { get; set; }
            public string col19 { get; set; }
            public string col19o { get; set; }
            public string col20 { get; set; }
            public string col20o { get; set; }
            public string col21 { get; set; }
            public string col21o { get; set; }
            public string col22 { get; set; }
            public string col22o { get; set; }
            public string col23 { get; set; }
            public string col23o { get; set; }
            public string col24 { get; set; }
            public string col24o { get; set; }
            public string col25 { get; set; }
            public string col25o { get; set; }
            public string col26 { get; set; }
            public string col26o { get; set; }
            public string col27 { get; set; }
            public string col27o { get; set; }
            public string col28 { get; set; }
            public string col28o { get; set; }
            public string col29 { get; set; }
            public string col29o { get; set; }
            public string col30 { get; set; }
            public string col30o { get; set; }
            public string col31 { get; set; }
            public string col31o { get; set; }
            public string present { get; set; }
            public string absnt { get; set; }
            public string late { get; set; }
            public string earnlev { get; set; }
            public string siclev { get; set; }
            public string casuallev { get; set; }
            public string withpaylev { get; set; }
            public string holyday { get; set; }
            public string dayout { get; set; }
            public string tpayable { get; set; }
            public string onduty { get; set; }
            public DateTime joindate { get; set; }
            public string fine { get; set; }
            public double ttlv { get; set; }
            public double ttlwh { get; set; }
            public double tot { get; set; }
            public double tcot { get; set; }
            public double teot { get; set; }
            public double tfot { get; set; }

        }
        [Serializable]
        public class EmpAttendenceFB
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string cardno { get; set; }
            public string desig { get; set; }
            public string dept { get; set; }
            public string deptname { get; set; }
            public string empdsgid { get; set; }
            public string empdsg { get; set; }
            public string addday { get; set; }
            public string addtime1 { get; set; }
            public string addtime2 { get; set; }
            public string empnam { get; set; }
            public string idcardno { get; set; }
            public string addtime3 { get; set; }
            public string empdeptid { get; set; }
            public string empdept { get; set; }
            public string sectionid { get; set; }
            public string section { get; set; }
            public string col1 { get; set; }
            public string col1o { get; set; }
            public string col2 { get; set; }
            public string col2o { get; set; }
            public string col3 { get; set; }
            public string col3o { get; set; }
            public string col4 { get; set; }
            public string col4o { get; set; }
            public string col5 { get; set; }
            public string col5o { get; set; }
            public string col6 { get; set; }
            public string col6o { get; set; }
            public string col7 { get; set; }
            public string col7o { get; set; }
            public string col8 { get; set; }
            public string col8o { get; set; }
            public string col9 { get; set; }
            public string col9o { get; set; }
            public string col10 { get; set; }
            public string col10o { get; set; }
            public string col11 { get; set; }
            public string col11o { get; set; }
            public string col12 { get; set; }
            public string col12o { get; set; }
            public string col13 { get; set; }
            public string col13o { get; set; }
            public string col14 { get; set; }
            public string col14o { get; set; }
            public string col15 { get; set; }
            public string col15o { get; set; }
            public string col16 { get; set; }
            public string col16o { get; set; }
            public string col17 { get; set; }
            public string col17o { get; set; }
            public string col18 { get; set; }
            public string col18o { get; set; }
            public string col19 { get; set; }
            public string col19o { get; set; }
            public string col20 { get; set; }
            public string col20o { get; set; }
            public string col21 { get; set; }
            public string col21o { get; set; }
            public string col22 { get; set; }
            public string col22o { get; set; }
            public string col23 { get; set; }
            public string col23o { get; set; }
            public string col24 { get; set; }
            public string col24o { get; set; }
            public string col25 { get; set; }
            public string col25o { get; set; }
            public string col26 { get; set; }
            public string col26o { get; set; }
            public string col27 { get; set; }
            public string col27o { get; set; }
            public string col28 { get; set; }
            public string col28o { get; set; }
            public string col29 { get; set; }
            public string col29o { get; set; }
            public string col30 { get; set; }
            public string col30o { get; set; }
            public string col31 { get; set; }
            public string col31o { get; set; }
            public double present { get; set; }
            public double absnt { get; set; }
            public double late { get; set; }
            public string earnlev { get; set; }
            public string siclev { get; set; }
            public string casuallev { get; set; }
            public string withpaylev { get; set; }
            public string holyday { get; set; }
            public string dayout { get; set; }
            public string tpayable { get; set; }
            public string onduty { get; set; }
            public DateTime joindate { get; set; }
            public string lineid { get; set; }
            public string fline { get; set; }
            public double ttlv { get; set; }
            public double ttlwh { get; set; }
            public double tot { get; set; }
            public double tcot { get; set; }
            public double teot { get; set; }
            public double tfot { get; set; }
            public double ttlot { get; set; }
            public double gssal { get; set; }

        }

        //comcod, deptid, secid, desig,  empid,  idcardno, deptname, section, desigid, empname,  offintime, tstatus,absnt,
        //offouttime, intime, lnchintime, lnchouttime, outtime, leave, absnt, dedout, addhour,absaprstatus, joindate,

        [Serializable]
        public class EmpMissAttn
        {
            public string comcod { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desig { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string empname { get; set; }
            public DateTime offintime { get; set; }
            public DateTime offouttime { get; set; }
            public DateTime intime { get; set; }
            public DateTime lnchintime { get; set; }
            public DateTime lnchouttime { get; set; }
            public DateTime outtime { get; set; }
            public string leave { get; set; }
            public string absnt { get; set; }
            public double dedout { get; set; }
            public double addhour { get; set; }
            public string absaprstatus { get; set; }
            public string tstatus { get; set; }
            public DateTime joindate { get; set; }
            public string linecod { get; set; }
            public string linedesc { get; set; }
            public EmpMissAttn() { }
        }

        //select a.comcod, a.empid, idcard=e.gdatat, a.company,  a.deptid, a.secid, a.desigid, a.desig, empname=b.empname , deptdesc=c.sirdesc, section=d.sirdesc

        [Serializable]
        public class EmpAbsentInf
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string idcard { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string deptdesc { get; set; }
            public string section { get; set; }
            public DateTime intime { get; set; }
            public string late { get; set; }
            public string line { get; set; }
            public string linecod { get; set; }
            public EmpAbsentInf() { }
        }

        [Serializable]
        public class RptAttnAftrLeave
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string deptdesc { get; set; }
            public string section { get; set; }
            public string fline { get; set; }
            public string linecod { get; set; }
            public DateTime lastdatelv { get; set; }
            public string empstatus { get; set; }
            public string leavtype { get; set; }
            public string leavreason { get; set; }

            public RptAttnAftrLeave() { }
        }

        [Serializable]

        //comcod, dayid, empid, idcardno, empgread, stdtimein, stdtimeout, actualout, actualin,  wintime, wouttime, lateinmin, earlyexit, ovtmin, leav,  dstatus, empnam, 
        //empdsg, tstatus, joindate, empdeptid, empdept, empsecid, empsec, fline, empstatus, attnstatus, remarks, inoroutpunch, twrkday, tlday, tlvday, thday, tabsday, ttot, ttpsnt


        public class EmpJobCard01
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            //public DateTime stdtimein { get; set; }
            //public DateTime stdtimeout { get; set; }
            //public DateTime stdlunchtime { get; set; }
            //public DateTime stdlunchouttime { get; set; }
            public DateTime actualin { get; set; }
            public DateTime actualout { get; set; }
            //public string actTimehour { get; set; }
            //public double dedtimePenal1 { get; set; }
            //public string ActualWhour { get; set; }
            //public DateTime attnhour { get; set; }
            //public double attnminute { get; set; }
            //public double actualattnminute { get; set; }
            //public double attntimeminute { get; set; }
            //public double addtime2 { get; set; }
            public DateTime wintime { get; set; }
            public DateTime wouttime { get; set; }
            //public double addday { get; set; }
            public string leav { get; set; }
            public string empnam { get; set; }
            public string company { get; set; }
            public string empdeptid { get; set; }
            public string companyname { get; set; }
            public string empdept { get; set; }
            public string empdsg { get; set; }
            public double lateinmin { get; set; }
            public double earlyexit { get; set; }
            public double ovtmin { get; set; }
            public int tovtmin { get; set; }
            public double twrkday { get; set; }
            public double tlday { get; set; }
            public double tlvday { get; set; }
            public double thday { get; set; }
            public double tabsday { get; set; }
            public double ttot { get; set; }
            public double ttpsnt { get; set; }
            public string tstatus { get; set; }
            public string dstatus { get; set; } 
            public string attnstatus { get; set; }
            public string remarks { get; set; }
            public string joindate { get; set; }
            public string empgread { get; set; }
            public string empsecid { get; set; }
            public string empsec { get; set; }
            public string fline { get; set; }
            public string empstatus { get; set; }
            public string inoroutpunch { get; set; }
            public EmpJobCard01() { }
        }
        // comcod, empid, twrkday, tlday, tlvday, thday, tabsday, ttot from #tblemplablainf 
        [Serializable]
        public class EmpJobCard02
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public double twrkday { get; set; }
            public double tlday { get; set; }
            public double tlvday { get; set; }
            public double thday { get; set; }
            public double tabsday { get; set; }
            public double ttot { get; set; }
            public double ttpsnt { get; set; }
            public EmpJobCard02() { }
        }

        [Serializable]
        public class EmpJobCard03
        {
            public string company { get; set; }
            public string companyname { get; set; }
            public EmpJobCard03() { }
        }

        [Serializable]
        public class MonthlyPresent
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string secid { get; set; }
            public string desig { get; set; }
            public string section { get; set; }
            public string empname { get; set; }
            public double present { get; set; }
            public double absnt { get; set; }
            public double holiday { get; set; }
            public double late { get; set; }
            public double noleav { get; set; }
            public double wd { get; set; }
            public MonthlyPresent() { }
        }

        [Serializable]
        public class DailyAttenSummary
        {
            // IQBAL NAYAN
            public string comcod { get; set; }
            public string seccod { get; set; }
            public string linecode { get; set; }
            public string degcod { get; set; }
            public double male { get; set; }
            public double female { get; set; }
            public double psent { get; set; }
            public double absnt { get; set; }
            public double leav { get; set; }
            public double heldmpower { get; set; }
            public double reqmpower { get; set; }
            public double short1 { get; set; }
            public double overmpower { get; set; }
            public double abspersnt { get; set; }
            public string secdesc { get; set; }
            public string lindesc { get; set; }
            public string degdesc { get; set; }
            public double newemp { get; set; }
            public string remarks { get; set; }
            public string  grp { get; set; }
            public string depcode { get; set; }
            public string depdesc { get; set; }
            public DailyAttenSummary() { }
        }

        [Serializable]
        public class DailyAttenSummarySkillWise
        {
            // MURSALAT
            public string comcod { get; set; }
            public string empcode { get; set; }
            public string emptype { get; set; }
            public string seccod { get; set; }
            public string secdesc { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public double budsup { get; set; }
            public double budline { get; set; }
            public double budop { get; set; }
            public double btotal { get; set; }
            public double psup { get; set; }
            public double pline { get; set; }
            public double pw1 { get; set; }
            public double pw2 { get; set; }
            public double pw3 { get; set; }
            public double ptotal { get; set; }
            public double asup { get; set; }
            public double aline { get; set; }
            public double aw1 { get; set; }
            public double aw2 { get; set; }
            public double aw3 { get; set; }
            public double atotal { get; set; }
            public DailyAttenSummarySkillWise() { }
        }
        [Serializable]
        public class EmpInfo
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string empnamebn { get; set; }
            public string desig { get; set; }
            public string fathnme { get; set; }
            public string mothnme { get; set; }
            public string company { get; set; }
            public string department { get; set; }
            public string deptnamebn { get; set; }
            public string section { get; set; }
            public string netpay { get; set; }
            public string bsal { get; set; }
            public string hrent { get; set; }
            public string cven { get; set; }
            public string mallow { get; set; }
            public string foodallow { get; set; }
            public string joiningdat { get; set; }
            public string dob { get; set; }
            public string nid { get; set; }
            public string idcardno { get; set; }
            public string religionbn { get; set; }
            public string mrtlstatusbn { get; set; }
            public string genderbn { get; set; }
            public string gssal { get; set; }
            public string sectionbn { get; set; }
            public string floorline { get; set; }
            public string floordesc { get; set; }
            public string sgrade { get; set; }
            public string gradesc { get; set; }
            public string mobile { get; set; }
            public string education { get; set; }
            public string experience { get; set; }
            public string postoffice { get; set; }
            public string postofficedesc { get; set; }
            public string district { get; set; }
            public string upzila { get; set; }
            public string distdesc { get; set; }
            public string updesc { get; set; }
            public string permaadd { get; set; }
            public EmpInfo() { }
        }

        [Serializable]
        public class Empstatus
        {
            public string comcod { get; set; }
            public string company { get; set; }
            public string department { get; set; }
            public string section { get; set; }
            public string empid { get; set; }
            public double netpay { get; set; }
            public string idcard { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string refno { get; set; }
            public DateTime joindate { get; set; }
            public string retiredate { get; set; }
            public string acadeg { get; set; }
            public string passyear { get; set; }
            public string condate { get; set; }
            public string tecst { get; set; }
            public string companyname { get; set; }
            public string sectionname { get; set; }
            public string fline { get; set; }
            public string skillcode { get; set; }
            public string skilldesc { get; set; }
            public string bldgrpdesc { get; set; }
            public string mobile { get; set; }
            public string religion { get; set; }
            public string maritalstatus { get; set; }
            public DateTime dobdat { get; set; }
            public Empstatus() { }
        }

        [Serializable]
        public class EmpBonusheet
        {
            public string comcod { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public string empid { get; set; }
            public DateTime joindate { get; set; }
            public string bankacno { get; set; }
            public double duration { get; set; }
            public double desigid { get; set; }
            public double bsal { get; set; }
            public double gssal { get; set; }
            public double perbon { get; set; }
            public double bonamt { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double cashamta { get; set; }
            public string idcard { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public double durationday { get; set; }
            public string linecod { get; set; }
            public string linedesc { get; set; }
            public double foodalw { get; set; }
            public EmpBonusheet() { }
        }

        [Serializable]
        public class RptEmpMissPunch
        {
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string desigid { get; set; }
            public string empdesig { get; set; }
            public string deptid { get; set; }
            public string empdept { get; set; }
            public string sectionid { get; set; }
            public string empsection { get; set; }
            public string intime { get; set; }
            public string outtime { get; set; }
            public string lineid { get; set; }
            public string fline { get; set; }
            public RptEmpMissPunch() { }
        }

        [Serializable]
        public class RptDayWiseOTSheet
        {
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string desigid { get; set; }
            public string empdesig { get; set; }
            public string deptid { get; set; }
            public string empdept { get; set; }
            public string sectionid { get; set; }
            public string empsection { get; set; }
            public string linecode { get; set; }
            public string fline { get; set; }
            public string offouttime { get; set; }
            public string intime { get; set; }
            public string outtime { get; set; }
            public double cardot { get; set; }
            public double extraot { get; set; }
            public double totalfot { get; set; }
            public double totalot { get; set; }
            public int totalmin { get; set; }
            public RptDayWiseOTSheet() { }
        }

        [Serializable]
        public class EMonAtten
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string section { get; set; }
            public string joindate { get; set; }
            public string desigid { get; set; }
            public string gradeid { get; set; }
            public string lineid { get; set; }
            public int present { get; set; }
            public int latecount { get; set; }
            public string timeinsecs { get; set; }
            public int earlv { get; set; }
            public int ab { get; set; }
            public int wh { get; set; }
            public int fh { get; set; }
            public int sph { get; set; }
            public int adh { get; set; }
            public int cl { get; set; }
            public int sl { get; set; }
            public int ml { get; set; }
            public int el { get; set; }
            public int wpl { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string companyname { get; set; }
            public string sectionname { get; set; }
            public string deptname { get; set; }
            public string line { get; set; }
            public string grade { get; set; }
            public int totaldays { get; set; }
            public decimal dailyavgovt { get; set; }

        }

        [Serializable]
        public class EmpDayAttnSumry03
        {
            public string depid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string companyname { get; set; }
            public string company { get; set; }
            public string deptdesc { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string offintime { get; set; }
            public string offouttime { get; set; }
            public string intime { get; set; }
            public string outtime { get; set; }
            public string late { get; set; }
            public string status { get; set; }
            public string mleave { get; set; }
            public string leave { get; set; }
            public string absday { get; set; }
            public string holiday { get; set; }
            public string wholiday { get; set; }
            public string present { get; set; }
            public string joindate { get; set; }
            public string lineid { get; set; }
            public string line { get; set; }
            public string ovthour { get; set; }
            public EmpDayAttnSumry03(){}
        }

        [Serializable]
        public class DailyAttnSummaryCatWise
        {
            public int rowid { get; set; }
            public string emptype { get; set; } 
            public string depcode { get; set; }
            public string seccod { get; set; }
            public string grp { get; set; }
            public string linecode { get; set; }
            public double male { get; set; }
            public double female { get; set; }
            public double psent { get; set; }
            public double absnt { get; set; }
            public double leav { get; set; }
            public double matleave { get; set; }            
            public double heldmpower { get; set; }
            public double reqmpower { get; set; }
            public double short1 { get; set; }
            public double overmpower { get; set; }
            public double abspersnt { get; set; }
            public string emptypedesc { get; set; }
            public string depdesc { get; set; }
            public string secdesc { get; set; }
            public string lindesc { get; set; }
            public double newemp { get; set; }
            public string remarks { get; set; }
            public double acpresent { get; set; }
            public double gatepass { get; set; }
            public double actman { get; set; }
            public double absntprcnt { get; set; }
            public double psentprcnt { get; set; }
            public double leavprcnt { get; set; }
            public DailyAttnSummaryCatWise() { }
        }


        [Serializable]
        public class DailyPresent
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string company { get; set; }
            public string companyname { get; set; }
            public string secid { get; set; }
            public string secsion { get; set; }
            public string deptid { get; set; }
            public string deptname { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public DateTime intime { get; set; }
            public DateTime offintime { get; set; }
            public string latetime { get; set; }
            public string remarks { get; set; }
            public string lineid { get; set; }
            public string line { get; set; }

            public string mobile { get; set; }
            
        }
       

        [Serializable]
        public class RptMonAttnCountSum
        {
            public string secid { get; set; }
            public string section { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public int p1 { get; set; }
            public int p2 { get; set; }
            public int p3 { get; set; }
            public int p4 { get; set; }
            public int p5 { get; set; }
            public int p6 { get; set; }
            public int p7 { get; set; }
            public int p8 { get; set; }
            public int p9 { get; set; }
            public int p10 { get; set; }
            public int p11 { get; set; }
            public int p12 { get; set; }
            public int p13 { get; set; }
            public int p14 { get; set; }
            public int p15 { get; set; }
            public int p16 { get; set; }
            public int p17 { get; set; }
            public int p18 { get; set; }
            public int p19 { get; set; }
            public int p20 { get; set; }
            public int p21 { get; set; }
            public int p22 { get; set; }
            public int p23 { get; set; }
            public int p24 { get; set; }
            public int p25 { get; set; }
            public int p26 { get; set; }
            public int p27 { get; set; }
            public int p28 { get; set; }
            public int p29 { get; set; }
            public int p30 { get; set; }
            public int p31 { get; set; }
            public int totalprsnt { get; set; }

            public int a1 { get; set; }
            public int a2 { get; set; }
            public int a3 { get; set; }
            public int a4 { get; set; }
            public int a5 { get; set; }
            public int a6 { get; set; }
            public int a7 { get; set; }
            public int a8 { get; set; }
            public int a9 { get; set; }
            public int a10 { get; set; }
            public int a11 { get; set; }
            public int a12 { get; set; }
            public int a13 { get; set; }
            public int a14 { get; set; }
            public int a15 { get; set; }
            public int a16 { get; set; }
            public int a17 { get; set; }
            public int a18 { get; set; }
            public int a19 { get; set; }
            public int a20 { get; set; }
            public int a21 { get; set; }
            public int a22 { get; set; }
            public int a23 { get; set; }
            public int a24 { get; set; }
            public int a25 { get; set; }
            public int a26 { get; set; }
            public int a27 { get; set; }
            public int a28 { get; set; }
            public int a29 { get; set; }
            public int a30 { get; set; }
            public int a31 { get; set; }
            public int totalabsnt { get; set; }

            public int l1 { get; set; }
            public int l2 { get; set; }
            public int l3 { get; set; }
            public int l4 { get; set; }
            public int l5 { get; set; }
            public int l6 { get; set; }
            public int l7 { get; set; }
            public int l8 { get; set; }
            public int l9 { get; set; }
            public int l10 { get; set; }
            public int l11 { get; set; }
            public int l12 { get; set; }
            public int l13 { get; set; }
            public int l14 { get; set; }
            public int l15 { get; set; }
            public int l16 { get; set; }
            public int l17 { get; set; }
            public int l18 { get; set; }
            public int l19 { get; set; }
            public int l20 { get; set; }
            public int l21 { get; set; }
            public int l22 { get; set; }
            public int l23 { get; set; }
            public int l24 { get; set; }
            public int l25 { get; set; }
            public int l26 { get; set; }
            public int l27 { get; set; }
            public int l28 { get; set; }
            public int l29 { get; set; }
            public int l30 { get; set; }
            public int l31 { get; set; }
            public int totalleave { get; set; }
            public RptMonAttnCountSum()
            {
            }
        }

    }
}