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

    public class UserManagerLCM 
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();


        public UserManagerLCM() 
        {
        
        }

        public List<SPEENTITY.C_09_Commer.EClassLCCode> GetLCCode(string Procedure, string Calltype, string LCSch)
        {
            List<SPEENTITY.C_09_Commer.EClassLCCode> lst = new List<SPEENTITY.C_09_Commer.EClassLCCode>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, LCSch, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_09_Commer.EClassLCCode details = new SPEENTITY.C_09_Commer.EClassLCCode(dr["actcode"].ToString(), dr["actdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }



        public List<SPEENTITY.C_09_Commer.EClassLCCosting> ShowLCCosting(string Procedure, string Calltype, string LCnumber, string Label)
        {
            List<SPEENTITY.C_09_Commer.EClassLCCosting> lst = new List<SPEENTITY.C_09_Commer.EClassLCCosting>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, LCnumber, Label, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_09_Commer.EClassLCCosting details = new SPEENTITY.C_09_Commer.EClassLCCosting(dr["grp"].ToString(),
                        dr["rescode"].ToString(), dr["resdesc"].ToString(), dr["unit"].ToString(), Convert.ToDouble(dr["qty"]), Convert.ToDouble(dr["fcrate"]), Convert.ToDouble(dr["fcamt"]),
                         Convert.ToDouble(dr["bdamt"]), Convert.ToDouble(dr["tparcent"]), dr["lcdate"].ToString(), dr["bankname"].ToString(), dr["currency"].ToString(),
                    dr["expdate"].ToString(), Convert.ToDouble(dr["conrate"]), dr["supname"].ToString());
                lst.Add(details);
            }

            return lst;

        }


        public List<SPEENTITY.C_09_Commer.EClassLCStatus> ShowLCStatus(string Procedure, string Calltype, string LCnumber, string fromdate, string todate)
        {
            List<SPEENTITY.C_09_Commer.EClassLCStatus> lst = new List<SPEENTITY.C_09_Commer.EClassLCStatus>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, LCnumber, fromdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_09_Commer.EClassLCStatus details = new SPEENTITY.C_09_Commer.EClassLCStatus(dr["grp"].ToString(), dr["actcode"].ToString(), dr["actdesc"].ToString(), dr["rescod"].ToString(),
                    dr["resdesc"].ToString(), Convert.ToDouble(dr["ordrqty"]), Convert.ToDouble(dr["rate"]), Convert.ToDouble(dr["lcamt"]), Convert.ToDouble(dr["lcval"]), dr["lcdate"].ToString(),dr["shipdate"].ToString(), dr["shipardate"].ToString(), dr["deldate"].ToString(), dr["expdate"].ToString(), dr["rcvdate"].ToString(), dr["lcstatus"].ToString(), dr["fileno"].ToString(), dr["lpaytcode"].ToString(), dr["lpaytrm"].ToString(),
                    dr["befnam"].ToString(), dr["blno"].ToString(), 
                    dr["bldat"].ToString(), dr["etadat"].ToString(), dr["beno"].ToString(), dr["bedat"].ToString(), dr["docpos"].ToString(),
                    dr["cnf"].ToString(), dr["paydat"].ToString(), dr["dtrmcod"].ToString(), dr["deltrm"].ToString(), dr["delmodcod"].ToString(),
                    dr["delmod"].ToString(), dr["remarks"].ToString(), dr["unit"].ToString());


                lst.Add(details);
            }

            return lst;

        }
        

    }
}
