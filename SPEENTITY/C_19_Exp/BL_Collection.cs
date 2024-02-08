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

namespace SPEENTITY.C_19_Exp
           
{
    public class BL_Collection
    {
        ProcessAccess _userData = new ProcessAccess();
        Common ObjCommon = new Common();

        public List<SPEENTITY.C_19_Exp.BO_Collection> GetCollectionList(string frmDate, string todate)
        {
            List < SPEENTITY.C_19_Exp.BO_Collection > lst= new List<SPEENTITY.C_19_Exp.BO_Collection>();
            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _userData.GetSqlReader(comcod, "[dbo].[SP_REPORT_EXPORT]", "COLLECTIONTOPLIST", frmDate, todate,"","","","",
                "","","");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.BO_Collection details = new SPEENTITY.C_19_Exp.BO_Collection(dr["centrid"].ToString(), 
                    dr["custid"].ToString(), dr["centrdesc"].ToString(), dr["memono"].ToString(), dr["name"].ToString(), dr["chqno"].ToString(),
                    Convert.ToDouble(dr["fcamt"].ToString()), Convert.ToDouble(dr["amount"].ToString()), Convert.ToDouble(dr["fcbnkcharge"].ToString()),
                    Convert.ToDouble(dr["vatamt"].ToString()), Convert.ToDouble(dr["cglamt"].ToString()), dr["vounum"].ToString(), dr["mrdat"].ToString(), 
                    dr["bnknam"].ToString(), dr["memono1"].ToString(), dr["paydat"].ToString(), dr["username"].ToString(), dr["bbranch"].ToString(),
                    dr["paytype"].ToString(), dr["voudat"].ToString(), dr["remarks"].ToString());
                lst.Add(details);


            }

            return lst;
        }

    }
}
