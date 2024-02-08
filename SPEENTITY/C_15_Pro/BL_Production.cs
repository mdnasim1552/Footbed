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
    public class BL_Production
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();
        public List<SPEENTITY.C_15_Pro.BO_Production.EClassYear> ShowYearly(string comcod, string Date1)
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EClassYear> lst = new List<SPEENTITY.C_15_Pro.BO_Production.EClassYear>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODYEAR", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_15_Pro.BO_Production.EClassYear Yearly = new C_15_Pro.BO_Production.EClassYear(dr["yearid"].ToString(), Convert.ToDouble(dr["bgdamt"]), Convert.ToDouble(dr["proamt"]));
                lst.Add(Yearly);
            }

            return lst;

        }

        public List<SPEENTITY.C_15_Pro.BO_Production.EClassWeekly> ShowWeekly(string comcod, string Date1)
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EClassWeekly> lst = new List<SPEENTITY.C_15_Pro.BO_Production.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODWEEKLY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_15_Pro.BO_Production.EClassWeekly Weekly = new C_15_Pro.BO_Production.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wsamt1"]),
                    Convert.ToDouble(dr["wcamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wsamt2"]), Convert.ToDouble(dr["wcamt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["wsamt3"]), Convert.ToDouble(dr["wcamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wsamt4"]),
                    Convert.ToDouble(dr["wcamt4"]));
                lst.Add(Weekly);
            }

            return lst;

        }
        public List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly> ShowMonthly(string comcod, string Date1)
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly> lst = new List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODMONTHLY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_15_Pro.BO_Production.EClassMonthly Monthly = new C_15_Pro.BO_Production.EClassMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), Convert.ToDouble(dr["bgdamt"]), Convert.ToDouble(dr["proamt"]));
                lst.Add(Monthly);
            }

            return lst;

        }


        public List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise> ShowDayWise(string comcod, string Date1)
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise> lst = new List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODTARDAYILY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_15_Pro.BO_Production.EClassDayWise DayWise = new C_15_Pro.BO_Production.EClassDayWise(dr["pbdate"].ToString(), dr["pbno"].ToString(),
                    dr["preqno"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), dr["rsircode"].ToString(), dr["rsirdesc"].ToString(),
                    Convert.ToDouble(dr["rsqty"]), Convert.ToDouble(dr["preqamt"]), Convert.ToDouble(dr["bgdrat"]));
                lst.Add(DayWise);
            }

            return lst;

        }

        public List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe> ShowDayWiseExe(string comcod, string Date1)
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe> lst = new List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PRODEXEDAYILY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe DayWise = new C_15_Pro.BO_Production.EClassDayWiseExe(dr["batchcode"].ToString(), dr["batchdesc"].ToString(),
                    dr["storid"].ToString(), dr["centrdesc"].ToString(), dr["itmcod"].ToString(), dr["rsirdesc"].ToString(), dr["prodate"].ToString(),
                    dr["vounum1"].ToString(), Convert.ToDouble(dr["proqty"]), Convert.ToDouble(dr["proamt"]), Convert.ToDouble(dr["rate"]));
                lst.Add(DayWise);
            }

            return lst;

        }
    }
    [Serializable]
    public class EclassProdPlanSummary
    {
        public string linecode { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string stylecode { get; set; }
        public string styledesc { get; set; }
        public double qty1 { get; set; }
        public double qty2 { get; set; }
        public double qty3 { get; set; }
        public double qty4 { get; set; }
        public double qty5 { get; set; }
        public double qty6 { get; set; }
        public double qty7 { get; set; }
        public double qty8 { get; set; }
        public double qty9 { get; set; }
        public double qty10 { get; set; }
        public double qty11 { get; set; }
        public double qty12 { get; set; }
        public double qty13 { get; set; }
        public double qty14 { get; set; }
        public double qty15 { get; set; }
        public double qty16 { get; set; }
        public double qty17 { get; set; }
        public double qty18 { get; set; }
        public double qty19 { get; set; }
        public double qty20 { get; set; }
        public double qty21 { get; set; }
        public double qty22 { get; set; }
        public double qty23 { get; set; }
        public double qty24 { get; set; }
        public double qty25 { get; set; }
        public double qty26 { get; set; }
        public double qty27 { get; set; }
        public double qty28 { get; set; }

        public double qty29 { get; set; }
        public double qty30 { get; set; }
        public double qty31 { get; set; }

    }
    [Serializable]
    public class DepWisReqApp
    {
        public string linecode { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string stylecode { get; set; }
        public string styledesc { get; set; }
        public double qty1 { get; set; }
        public double qty2 { get; set; }
        public double qty3 { get; set; }
        public double qty4 { get; set; }
        public double qty5 { get; set; }
        public double qty6 { get; set; }
        public double qty7 { get; set; }
        public double qty8 { get; set; }
        public double qty9 { get; set; }
        public double qty10 { get; set; }
        public double qty11 { get; set; }
        public double qty12 { get; set; }
        public double qty13 { get; set; }
        public double qty14 { get; set; }
        public double qty15 { get; set; }
        public double qty16 { get; set; }
        public double qty17 { get; set; }
        public double qty18 { get; set; }
        public double qty19 { get; set; }
        public double qty20 { get; set; }
        public double qty21 { get; set; }
        public double qty22 { get; set; }
        public double qty23 { get; set; }
        public double qty24 { get; set; }
        public double qty25 { get; set; }
        public double qty26 { get; set; }
        public double qty27 { get; set; }
        public double qty28 { get; set; }

        public double qty29 { get; set; }
        public double qty30 { get; set; }
        public double qty31 { get; set; }

    }
}
