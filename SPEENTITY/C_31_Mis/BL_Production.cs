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
    public class BL_Production
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();
        public List<SPEENTITY.C_31_Mis.BO_Production.EClassYear> ShowYearly(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.BO_Production.EClassYear> lst = new List<SPEENTITY.C_31_Mis.BO_Production.EClassYear>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODYEAR", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.BO_Production.EClassYear Yearly = new C_31_Mis.BO_Production.EClassYear(dr["yearid"].ToString(), Convert.ToDouble(dr["protar"]), Convert.ToDouble(dr["proact"]));
                lst.Add(Yearly);
            }

            return lst;

        }

        public List<SPEENTITY.C_31_Mis.BO_Production.EClassWeekly> ShowWeekly(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.BO_Production.EClassWeekly> lst = new List<SPEENTITY.C_31_Mis.BO_Production.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODWEEKLY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.BO_Production.EClassWeekly Weekly = new C_31_Mis.BO_Production.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wtamt1"]),
                    Convert.ToDouble(dr["waamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wtamt2"]), Convert.ToDouble(dr["waamt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["wtamt3"]), Convert.ToDouble(dr["waamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wtamt4"]),
                    Convert.ToDouble(dr["waamt4"]));
                lst.Add(Weekly);
            }

            return lst;

        }
        public List<SPEENTITY.C_31_Mis.BO_Production.EClassMonthly> ShowMonthly(string comcod, string Date1)
        {
            List<SPEENTITY.C_31_Mis.BO_Production.EClassMonthly> lst = new List<SPEENTITY.C_31_Mis.BO_Production.EClassMonthly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODMONTHLY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.BO_Production.EClassMonthly Monthly = new C_31_Mis.BO_Production.EClassMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), Convert.ToDouble(dr["protar"]), Convert.ToDouble(dr["proact"]));
                lst.Add(Monthly);
            }

            return lst;

        }


        public List<SPEENTITY.C_31_Mis.BO_Production.EClassDayWise> ShowDayWiseProTar(string comcod, string frmdate, string todate)
        {
            List<SPEENTITY.C_31_Mis.BO_Production.EClassDayWise> lst = new List<SPEENTITY.C_31_Mis.BO_Production.EClassDayWise>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISEPROTAR", frmdate, todate, "", "", "", "", "", "", "");

             


            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.BO_Production.EClassDayWise DayWise = new C_31_Mis.BO_Production.EClassDayWise(Convert.ToDateTime(dr["protdate"].ToString()), Convert.ToDouble(dr["protar"].ToString()),
                    dr["mlcdesc"].ToString(), dr["orderdesc"].ToString(), dr["curdesc"].ToString());
                lst.Add(DayWise);
            }

            return lst;

        }

        public List<SPEENTITY.C_31_Mis.BO_Production.EClassDayWiseProAct> ShowDayWiseProAct(string comcod, string frmdate, string todate)
        {
            List<SPEENTITY.C_31_Mis.BO_Production.EClassDayWiseProAct> lst = new List<SPEENTITY.C_31_Mis.BO_Production.EClassDayWiseProAct>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISEPROACT", frmdate, todate, "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_31_Mis.BO_Production.EClassDayWiseProAct DayWise = new C_31_Mis.BO_Production.EClassDayWiseProAct(Convert.ToDateTime(dr["producdat"].ToString()), Convert.ToDouble(dr["proact"].ToString()),
                    dr["mlcdesc"].ToString(), dr["orderdesc"].ToString(), dr["curdesc"].ToString());
                lst.Add(DayWise);
            }

            return lst;

        }
    }
}
