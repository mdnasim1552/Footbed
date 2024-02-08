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
  public  class UserManOrder
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

        #region SalesDash_Board


        public List<SPEENTITY.C_31_Mis.EClassOrder.EClassYear> ShowYearly(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.EClassOrder.EClassYear> lst = new List<SPEENTITY.C_31_Mis.EClassOrder.EClassYear>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ORDERINFOYEAR", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.EClassOrder.EClassYear Yearly = new C_31_Mis.EClassOrder.EClassYear(dr["yearid"].ToString(), Convert.ToDouble(dr["ordramt"]), Convert.ToDouble(dr["shipamt"]));
                lst.Add(Yearly);
            }

            return lst;

        }

        public List<SPEENTITY.C_31_Mis.EClassOrder.EClassWeekly> ShowWeekly(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.EClassOrder.EClassWeekly> lst = new List<SPEENTITY.C_31_Mis.EClassOrder.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ORDERINFOWEEK", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.EClassOrder.EClassWeekly Weekly = new C_31_Mis.EClassOrder.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["woamt1"]),
                    Convert.ToDouble(dr["wsamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["woamt2"]), Convert.ToDouble(dr["wsamt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["woamt3"]), Convert.ToDouble(dr["wsamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["woamt4"]),
                    Convert.ToDouble(dr["wsamt4"]));
                lst.Add(Weekly);
            }

            return lst;

        }
        public List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> ShowMonthly(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> lst = new List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly>();
            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ORDERINFYEARMONTH", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly Monthly = new C_31_Mis.EClassOrder.EClassMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), Convert.ToDouble(dr["ordramt"]), Convert.ToDouble(dr["shipamt"]), Convert.ToDouble(dr["balshipamt"]), Convert.ToDouble(dr["collamt"]), 
                    Convert.ToDouble(dr["balcollamt"]));
                lst.Add(Monthly);
            }

            return lst;

        }

        public List<SPEENTITY.C_31_Mis.EClassOrder.EClassDayWise> ShowDayWise(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.EClassOrder.EClassDayWise> lst = new List<SPEENTITY.C_31_Mis.EClassOrder.EClassDayWise>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISESALES", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.EClassOrder.EClassDayWise DayWise = new C_31_Mis.EClassOrder.EClassDayWise(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
                    dr["custid"].ToString(), dr["custdesc"].ToString(), dr["memono1"].ToString(), dr["memono"].ToString(), dr["memodat"].ToString(),
                    dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["itmamt"]), Convert.ToDouble(dr["vat"]), Convert.ToDouble(dr["invdis"]));
                lst.Add(DayWise);
            }

            return lst;

        }

        public List<SPEENTITY.C_31_Mis.EClassOrder.EClassDayWiseColl> ShowDayWiseColl(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.EClassOrder.EClassDayWiseColl> lst = new List<SPEENTITY.C_31_Mis.EClassOrder.EClassDayWiseColl>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISECOLL", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.EClassOrder.EClassDayWiseColl DayWise = new C_31_Mis.EClassOrder.EClassDayWiseColl(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
                    dr["custid"].ToString(), dr["custdesc"].ToString(), dr["mrslno1"].ToString(), dr["mrslno"].ToString(), dr["mrdat"].ToString(),
                    dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["amount"]));
                lst.Add(DayWise);
            }

            return lst;

        }

        #endregion
    }
}
