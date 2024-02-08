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

namespace SPEENTITY.C_31_Mis
{
    public class UserManageMis
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();
        public UserManageMis()
        { }

        //public List<SPEENTITY.C_27_Mis.EClassMisBO.BEPCalculation> ShowBepCal(string Date1, string Date2)
        //{
        //    List<SPEENTITY.C_27_Mis.EClassMisBO.BEPCalculation> lst = new List<SPEENTITY.C_27_Mis.EClassMisBO.BEPCalculation>();

        //    //SPEENTITY.C_27_Mis.EClassMisBO.BEPCalculation details = new SPEENTITY.C_27_Mis.EClassMisBO.BEPCalculation();

        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_ACC_MGT", "RPTBEPCAL", Date1, Date2, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_27_Mis.EClassMisBO.BEPCalculation details = new SPEENTITY.C_27_Mis.EClassMisBO.BEPCalculation(dr["grp"].ToString(),
        //               dr["actcode"].ToString(), dr["actdesc"].ToString(), Convert.ToDouble(dr["amt1"]), Convert.ToDouble(dr["amt2"]), Convert.ToDouble(dr["amt3"]),
        //               Convert.ToDouble(dr["amt4"]), Convert.ToDouble(dr["amt5"]), Convert.ToDouble(dr["amt6"]), Convert.ToDouble(dr["amt7"]), Convert.ToDouble(dr["amt8"]),
        //               Convert.ToDouble(dr["amt9"]), Convert.ToDouble(dr["amt10"]), Convert.ToDouble(dr["amt11"]), Convert.ToDouble(dr["amt12"]));
        //        lst.Add(details);
        //    }
        //    return lst;

        //}
        [Serializable]
        public class EclassBalSheetSum
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public double noncuram { get; set; }
            public double curam { get; set; }
            public double equityam { get; set; }
            public double noncurlia { get; set; }
            public double curlia { get; set; }
            public double toasset { get; set; }
            public double tolib { get; set; }
        }


        [Serializable]
        public class RatioAnalysis
        {
           
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rdesc { get; set; }
            public double rstd { get; set; }
            public double ratio { get; set; }
        }
    }
}
