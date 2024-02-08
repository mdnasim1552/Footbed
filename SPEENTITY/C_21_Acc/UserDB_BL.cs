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


namespace SPEENTITY.C_21_Acc
{
    public class UserDB_BL
    {
        ProcessAccess _userData = new ProcessAccess();
        Common ObjComcod = new Common();

        public List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly> ShowYearAcc(string comcod, string Date)
        {
            List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly> lst = new List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly>();
            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ACCINFOYEAR", Date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly Yearly = new C_21_Acc.EClassDB_BO.EClassAccYearly(dr["yearid"].ToString(), Convert.ToDouble(dr["dram"]), Convert.ToDouble(dr["cram"]));
                lst.Add(Yearly);
            }

            return lst;
        }

        public List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccMonthly> ShowMonthlyAcc(string comcod, string Date)
        {
            List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccMonthly> lst = new List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccMonthly>();
           // string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ACCINFOYMONTH", Date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccMonthly Monthly = new C_21_Acc.EClassDB_BO.EClassAccMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), 
                    Convert.ToDouble(dr["dram"]), Convert.ToDouble(dr["cram"]), Convert.ToDouble(dr["dramcore"]), Convert.ToDouble(dr["cramcore"]));
                lst.Add(Monthly);
            }

            return lst;
        }

        public List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccWeekly> ShowWeeklyAcc(string comcod, string Date1)
        {
            List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccWeekly> lst = new List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccWeekly>();

            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ACCINFOYDAILY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccWeekly Weekly = new SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wpamt1"]),
                    Convert.ToDouble(dr["wramt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wpamt2"]), Convert.ToDouble(dr["wramt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["wpamt3"]), Convert.ToDouble(dr["wramt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wpamt4"]),
                    Convert.ToDouble(dr["wramt4"]), Convert.ToDouble(dr["brec"]), Convert.ToDouble(dr["bpay"]));
                lst.Add(Weekly);
            }

            return lst;

        }
    }
}
