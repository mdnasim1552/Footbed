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


namespace SPEENTITY
{

    public class UserManagerMyPage 
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();


        public UserManagerMyPage() 
        {
        
        }

        public List<SPEENTITY.C_39_MyPage.EClassShowKPIData> ShowKPIData(string Procedure, string Calltype, string Empid, string YmonID)
        {
            List<SPEENTITY.C_39_MyPage.EClassShowKPIData> lst = new List<SPEENTITY.C_39_MyPage.EClassShowKPIData>();
         
            string comcod = ObjCommon.GetHRCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, Empid, YmonID, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_39_MyPage.EClassShowKPIData details = new SPEENTITY.C_39_MyPage.EClassShowKPIData(dr["kpigrpdesc"].ToString(), Convert.ToDouble(dr["stdkpival"]),
                    Convert.ToDouble(dr["stdtarget"]), Convert.ToDouble(dr["actual"]), Convert.ToDouble(dr["mparcnt"]));
                lst.Add(details);
            }

            return lst;

        }

    }
}
