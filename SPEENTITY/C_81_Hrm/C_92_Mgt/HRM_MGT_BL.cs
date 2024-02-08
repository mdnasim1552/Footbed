using SPELIB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_92_Mgt
{
    public class HRM_BL
    {
        ProcessAccess _userData = new ProcessAccess();
        public List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> ShowEmployeeStatus(string comcod, string Date)
        {
            List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> list = new List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus>();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "HRM_EMPLOYEE_STATUS", Date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus EmpStatus = new C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus(dr["statusdesc"].ToString(), Convert.ToInt32(dr["noofemp"]));
                list.Add(EmpStatus);
            }

            return list;
        }
        public List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> ShowYestAttnStatus(string comcod, string Date)
        {
            List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> list = new List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus>();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "HRM_YEST_ATTN_STATUS", Date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus EmpStatus = new C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus(dr["statusdesc"].ToString(), Convert.ToInt32(dr["noofemp"]));
                list.Add(EmpStatus);
            }

            return list;
        }
        public List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> ShowTodayAttnStatus(string comcod, string Date)
        {
            List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> list = new List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus>();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "HRM_TODAY_ATTN_STATUS", Date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus EmpStatus = new C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus(dr["statusdesc"].ToString(), Convert.ToInt32(dr["noofemp"]));
                list.Add(EmpStatus);
            }

            return list;
        }        
    }
}
