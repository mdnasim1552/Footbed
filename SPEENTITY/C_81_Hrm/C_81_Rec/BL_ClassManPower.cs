using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPELIB;
using System.Data.SqlClient;

namespace SPEENTITY.C_81_Hrm.C_81_Rec
{
   public class BL_ClassManPower
    {
        ProcessAccess _ProAccess = new ProcessAccess();


        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetBgdTypelist( string comcod)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETBUDGETEDTYPE", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetEmpTypelist(string comcod)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETTYPEOFEMPLOYES", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }
        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> GetWstation(string comcod, string usrid)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETACCESSORGANOGRAMLIST", "94%", "%%", usrid, "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1 TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1(sdr["actcode"].ToString(), sdr["actdesc"].ToString(),
                sdr["hrcomname"].ToString(), sdr["hrcomadd"].ToString(), sdr["hrcomnameb"].ToString(), sdr["hrcomaddb"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> GETORGANOGRAMALLLIST(string comcod, string userid)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETORGANOGRAMALLLIST", "94%", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }


        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassLine> GETLine(string comcod, string CompanyName, string division, string deptname, string section)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassLine> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassLine>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETLINE", CompanyName, division, deptname, section, "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassLine obj = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassLine(sdr["linecode"].ToString(), sdr["linedesc"].ToString());
                lst.Add(obj);

            }

            return lst;
        }

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> GetDptlist(string comcod, string usrid)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", "94%", "%%", usrid,  "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList> GetSectionlist(string comcod, string dptcode)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", dptcode, "%%", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList(sdr["sectionname"].ToString(), sdr["section"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }
        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList> GetDisgnation(string comcod, string grade)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDESIGNATION", grade, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList(sdr["designation"].ToString(), sdr["desigcod"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetEmpGradelist(string comcod)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETEMPGRADE", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }


        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetCommonHRgcod(string comcod, string hrgcod)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMMONHRGCOD", hrgcod, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }


        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.JobLocation> GetJobLocation(string comcod, string userid)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.JobLocation> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.JobLocation>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETEMPLOYEEJOBLOCATION", userid, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.JobLocation TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.JobLocation(sdr["loccode"].ToString(), sdr["location"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        //Get Employee Type

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> GetEmpType(string comcod, string usrid)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "GETEMPTYPE", "94%", usrid, "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> GetDivision(string comcod,string workstation)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "GETDIVISION", workstation, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }


        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> GetDept(string comcod, string workstation)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "GETDEPARTMENT", workstation, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }


        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> GetSection(string comcod, string workstation)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "GETSECTION", workstation, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassWStDivDeptASection(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }

        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassGrade> GetGrade(string comcod)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassGrade> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassGrade>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "GETGRADE", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassGrade TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassGrade(sdr["grade"].ToString(), sdr["gradedesc"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }


        public List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation> GetDesignation(string comcod)
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation> lst = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "GETSSHANGERDESIGNATION", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation TypeInfo = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation(sdr["desigid"].ToString(), sdr["desig"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }


    }
}
