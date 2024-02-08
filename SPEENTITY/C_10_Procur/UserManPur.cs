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

namespace SPEENTITY.C_10_Procur
{

    public class UserManPur
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

        ///----------BL----

        #region DashBoard_Purchase
        public List<SPEENTITY.C_10_Procur.EClassPur.EClassYear> ShowPurYearly(string comcod, string date)
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EClassYear> list = new List<SPEENTITY.C_10_Procur.EClassPur.EClassYear>();
           // string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PURCHASWINFOYEAR", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_10_Procur.EClassPur.EClassYear yearly = new SPEENTITY.C_10_Procur.EClassPur.EClassYear(dr["yearid"].ToString(), Convert.ToDouble(dr["ttlamt"].ToString()),
                        Convert.ToDouble(dr["purpay"].ToString()));
                list.Add(yearly);
            }
            return list;


        }


        public List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> ShowPurMonth(string comcod, string date)
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> list2 = new List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly>();
            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PURCHASEINFYEARMONTH", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_10_Procur.EClassPur.EClassMonthly monthly = new SPEENTITY.C_10_Procur.EClassPur.EClassMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(),
                        Convert.ToDouble(dr["ttlsalamt"]), Convert.ToDouble(dr["tpayamt"]), Convert.ToDouble(dr["ttlsalamtcore"]), Convert.ToDouble(dr["tpayamtcore"])    );
                list2.Add(monthly);
            }
            return list2;

        }


        public List<SPEENTITY.C_10_Procur.EClassPur.EClassWeekly> ShowPurWeekly(string comcod, string date1)
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EClassWeekly> lst = new List<SPEENTITY.C_10_Procur.EClassPur.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PURCHASEINFOWEEK", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_10_Procur.EClassPur.EClassWeekly Weekly = new SPEENTITY.C_10_Procur.EClassPur.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wpamt1"]),
                  Convert.ToDouble(dr["wpayamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wpamt2"]), Convert.ToDouble(dr["wpayamt2"]), dr["wcode3"].ToString(),
                   Convert.ToDouble(dr["wpamt3"]), Convert.ToDouble(dr["wpayamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wpamt4"]), Convert.ToDouble(dr["wpayamt4"]));
                lst.Add(Weekly);
            }
            return lst;
        }

        public List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur> ShowPurDayWise(string comcod, string date1)
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur> lst = new List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISEBILL", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur Pur = new SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
                  dr["rsircode"].ToString(), dr["rsirdesc"].ToString(),dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
                   dr["billno"].ToString(), dr["billdate1"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["billamt"]));
                lst.Add(Pur);
            }
            return lst;
        }

        public List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay> ShowPayDayWise(string comcod, string date1)
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay> lst = new List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISEPAY", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay Pay = new SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
                  dr["cactcode"].ToString(), dr["cactdesc"].ToString(), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
                   dr["billno"].ToString(), dr["voudat"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["payamt"]));
                lst.Add(Pay);
            }
            return lst;
        }

        #endregion
    }
}
