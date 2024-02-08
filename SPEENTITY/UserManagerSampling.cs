using SPELIB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY
{
   public class UserManagerSampling
    {
        ProcessAccess _ProAccess = new ProcessAccess();

        public List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> GetGenCode(string comcod)
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> lst = new List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>();
            
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETGENERALCODE", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode objgencode = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode(dr["gcod"].ToString(), dr["gdesc"].ToString());
                lst.Add(objgencode);
            }

            return lst;
        }

        public List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup> GetGroup(string comcod)
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup> lst = new List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup>();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETGROUP", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup objmat = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup(Convert.ToInt32(dr["Id"].ToString()).ToString(), dr["grp"].ToString(), dr["grpdesc"].ToString());
                lst.Add(objmat);
            }

            return lst;
        }


        public List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> GetMaterail(string comcod)
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> lst = new List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial>();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETMATERIAL", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial objmat = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial(dr["sircode"].ToString(), dr["sirdesc"].ToString(), dr["sirunit"].ToString());
                lst.Add(objmat);
            }

            return lst;
        }


    }
}
