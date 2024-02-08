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
   public class UserManReceiptaPayment
    {

        ProcessAccess _userData = new ProcessAccess();
        Common ObjComcod = new Common();

        public List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.batchGrp> showBatchGrp(string grp)
        {
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.batchGrp> lst = new List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.batchGrp>();
            string comcod = ObjComcod.GetCompCode();

            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_ENTRY_RECEIPTAPAYMENT", "BATCHGRP", grp, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.EClassReceiptaPayment.batchGrp grplist = new SPEENTITY.C_19_Exp.EClassReceiptaPayment.batchGrp(dr["actcode"].ToString().Trim(), dr["actdesc"].ToString().Trim());
                lst.Add(grplist);
            }
            return lst;
        }

        public List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankaCash> GetBankaCash()
        {
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankaCash> lst = new List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankaCash>();
            string comcod = ObjComcod.GetCompCode();

            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_ENTRY_RECEIPTAPAYMENT", "GETBANKORCASH", "", "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankaCash grplist = new SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankaCash(dr["bankcode"].ToString().Trim(), dr["bankdesc"].ToString().Trim());
                lst.Add(grplist);
            }
            return lst;
        }

        public List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDeborCreditor> GetDeborCreditor(string grp)
        {
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDeborCreditor> lst = new List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDeborCreditor>();
            string comcod = ObjComcod.GetCompCode();

            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_ENTRY_RECEIPTAPAYMENT", "GETDEBORCREDITOR", grp, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDeborCreditor grplist = new SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDeborCreditor(dr["rescode"].ToString().Trim(), dr["resdesc"].ToString().Trim(), dr["curcode"].ToString().Trim());
                lst.Add(grplist);
            }
            return lst;
        }
        //public List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> GetDeborBill(string Pactcode)
        //{
        //    List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = new List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>();
        //    string comcod = ObjComcod.GetCompCode();

        //    SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_EXPORT", "GETDEBTORBILL", Pactcode, "", "", "", "", "", "", "", "");

        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill grplist = new SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill( dr["actcode"].ToString(),
        //            dr["rescode"].ToString(), Convert.ToDateTime(dr["voudat"].ToString()), dr["isunum"].ToString(), dr["isunum1"].ToString(), 
        //            Convert.ToDouble(dr["billam"].ToString()), Convert.ToDouble(dr["hbalam"].ToString()), Convert.ToDouble(dr["balam"].ToString()),
        //            Convert.ToDouble(dr["receiptam"].ToString()), dr["chk"].ToString(), Convert.ToDouble(dr["allocamt"]), 
        //            Convert.ToDouble(dr["vatamt"].ToString()), dr["curcod"].ToString(), Convert.ToDouble(dr["convrate"].ToString()),
        //            dr["curdesc"].ToString(), Convert.ToDouble(dr["bdtamount"].ToString()), Convert.ToDouble(dr["fcbnkcharge"].ToString()),
        //            Convert.ToDouble(dr["invbdtamt"].ToString()), Convert.ToDouble(dr["cglamt"].ToString()), dr["invrefno"].ToString(), 
        //            Convert.ToDouble(dr["colconvrate"].ToString()));
        //        lst.Add(grplist);
                
        //    }
        //    return lst;
        //}

        public List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassCreditorBill> GetCreditorBill(string Type)
        {
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassCreditorBill> lst = new List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassCreditorBill>();
            string comcod = ObjComcod.GetCompCode();

            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_ENTRY_RECEIPTAPAYMENT", "GETCREDITORBAL", Type, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassCreditorBill grplist = new SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassCreditorBill(dr["actcode"].ToString(), dr["rescode"].ToString(), Convert.ToDateTime(dr["voudat"].ToString()), dr["gsttype"].ToString()
                    , dr["isunum"].ToString(), dr["pactdesc"].ToString(), Convert.ToDouble(dr["billam"].ToString()), Convert.ToDouble(dr["hbalam"].ToString()), 
                    Convert.ToDouble(dr["balam"].ToString()), Convert.ToDouble(dr["receiptam"].ToString()), Convert.ToDouble(dr["sgd"].ToString()),
                    dr["chk"].ToString(), dr["curdesc"].ToString());
                lst.Add(grplist);
                
            }
            return lst;
        }


       

    }
}
