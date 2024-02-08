using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SPELIB;

namespace SPEENTITY.C_15_Pro
{
    public class BL_Emp_Setup
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();

        public List<SPEENTITY.C_15_Pro.BO_Emp_Setup.EClassEmpProc> ShowProcessList(string comcod)
        {
            List<SPEENTITY.C_15_Pro.BO_Emp_Setup.EClassEmpProc> lst = new List<SPEENTITY.C_15_Pro.BO_Emp_Setup.EClassEmpProc>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPROPROCESS", "", "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_15_Pro.BO_Emp_Setup.EClassEmpProc EmpPro = new C_15_Pro.BO_Emp_Setup.EClassEmpProc(dr["sircode"].ToString(), dr["sirdesc"].ToString());
                lst.Add(EmpPro);
            }

            return lst;

        }
    }
}
