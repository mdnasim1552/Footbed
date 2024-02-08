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
    public class UserManGenAccount
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();


        



        //public List<SPEENTITY.C_21_Acc.EClassSalesDetails> ShowSalesDetails(string date)
        //{
        //    List<SPEENTITY.C_21_Acc.EClassSalesDetails> lst = new List<SPEENTITY.C_21_Acc.EClassSalesDetails>();

        //    string comcod = ObjCommon.GetCompCode();
        //    string CallType = (comcod == "3306") ? "RPTSALESDETAILS02" : "RPTSALESDETAILS";
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_ACCOUNTS_SALES", CallType, date, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_21_Acc.EClassSalesDetails details = new SPEENTITY.C_21_Acc.EClassSalesDetails(dr["actdesc"].ToString(), Convert.ToDouble(dr["advsale"].ToString()), Convert.ToDouble(dr["sale"].ToString()), Convert.ToDouble(dr["received"].ToString()), Convert.ToDouble(dr["recagsal"].ToString()), Convert.ToDouble(dr["receivable"].ToString()), Convert.ToDouble(dr["costam"].ToString()), Convert.ToDouble(dr["margin"].ToString()));
        //        lst.Add(details);
        //    }

        //    //if (dr.NextResult())
        //    //{ 
            
        //    //}
        //    return lst;



        //}




        #region  voucher

        public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> GetResHead(string actcode, string filter, string srchoption)
        {
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();

            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, filter, srchoption, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead details = new SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead(dr["rescode"].ToString(), dr["resdesc"].ToString(), dr["resdesc1"].ToString(), dr["resunit"].ToString());
                lst.Add(details);
            }
           
           
            return lst;



        }

        public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> GetActHead(string filter, string acthead, string vounum)
        {
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>();

            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", filter, acthead, vounum, "", "", "", "", "", "");
            
            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead details = new SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead(dr["actcode"].ToString(), dr["actdesc"].ToString(), dr["actdesc1"].ToString(), dr["actelev"].ToString(), dr["acttype"].ToString());
                lst.Add(details);
            }
           
           
            return lst;



        }


        


        #endregion


        #region pf


        public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> GetResHead1(string actcode, string filter, string srchoption)
        {
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();

            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "PFGETRESCODE", actcode, filter, srchoption, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead details = new SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead(dr["rescode"].ToString(), dr["resdesc"].ToString(), dr["resdesc1"].ToString(), dr["resunit"].ToString());
                lst.Add(details);
            }


            return lst;



        }



        public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> GetResHeadREQ(string actcode, string filter, string srchoption)
        {
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();

            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "OTHERGRP", actcode, filter, srchoption, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead details = new SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead(dr["rescode"].ToString(), dr["resdesc"].ToString(), dr["resdesc1"].ToString(), dr["resunit"].ToString());
                lst.Add(details);
            }


            return lst;



        }



        



        #endregion
    }
}
