﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using SPEENTITY;

namespace SPEENTITY.C_19_Exp
{
    
   
    public class SalesInvoice_BL
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();
        public DataSet ShowAll()
        {
            string comcod = ObjCommon.GetCompCode();
            string userid = ObjCommon.GetUserCode();
            return _ProAccess.GetTransInfo(comcod, "dbo_Sales.SP_ENTRY_SALES_INVOICE", "GETCENTER", userid);
        }



        public DataSet GetCompanyData(string comcod, string userid)
        {
            return _ProAccess.GetTransInfo(comcod, "dbo_Sales.SP_ENTRY_SALES_INVOICE", "GETCENTER", userid);
        }
        //public DataSet ShowRoom()
        //{
        //    string comcod = ObjCommon.GetCompCode();
        //    string userid = ObjCommon.GetUserCode();
        //    return _ProAccess.GetTransInfo(comcod, "dbo_Sales.SP_ENTRY_SALES_INVOICE", "GETCENTER", userid);
        //}
        public DataSet Curreny()
        {
            string comcod = ObjCommon.GetCompCode();
            return _ProAccess.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETCURRENCY");
        }
        public List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList> GetCustomerInfo(string Date)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList>();

            string comcod = ObjCommon.GetCompCode();
            string userid = ObjCommon.GetUserCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_Sales.SP_ENTRY_SALES_INVOICE", "GETCUSTINFO", Date, userid, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.DebtorList details = new SPEENTITY.C_19_Exp.Sales_BO.DebtorList(dr["actcode"].ToString(), dr["custid"].ToString(),
                    dr["custname"].ToString(), dr["curcode"].ToString(), Convert.ToDouble(dr["limit"].ToString()),
                    Convert.ToDouble(dr["duesamt"].ToString()), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(),
                    Convert.ToDouble(dr["ballimit"].ToString()), Convert.ToDouble(dr["proamt"].ToString()), dr["custname2"].ToString(), dr["custaddr"].ToString(), dr["regcode"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        public List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList> GetCustomerInfoGrp(string comcod, string Date, string userid, string Compsal)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList>();


            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_Sales.SP_ENTRY_GROUP_SALES_INFO", "GETCUSTINFOGROUP", Date, userid, Compsal, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.DebtorList details = new SPEENTITY.C_19_Exp.Sales_BO.DebtorList(dr["actcode"].ToString(), dr["custid"].ToString(),
                    dr["custname"].ToString(), dr["curcode"].ToString(), Convert.ToDouble(dr["limit"].ToString()),
                    Convert.ToDouble(dr["duesamt"].ToString()), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(),
                    Convert.ToDouble(dr["ballimit"].ToString()), Convert.ToDouble(dr["proamt"].ToString()), dr["custname2"].ToString(), dr["custaddr"].ToString(), dr["regcode"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        public List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList> GetCustomerInfo(string comcod, string userid,string Date)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.DebtorList>();

         
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_Sales.SP_ENTRY_SALES_INVOICE", "GETCUSTINFO", Date, userid, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.DebtorList details = new SPEENTITY.C_19_Exp.Sales_BO.DebtorList(dr["actcode"].ToString(), dr["custid"].ToString(),
                    dr["custname"].ToString(), dr["curcode"].ToString(), Convert.ToDouble(dr["limit"].ToString()),
                    Convert.ToDouble(dr["duesamt"].ToString()), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), Convert.ToDouble(dr["ballimit"].ToString()),
                    Convert.ToDouble(dr["proamt"].ToString()), dr["custname2"].ToString(), dr["custaddr"].ToString(), dr["regcode"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        public List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> GetProductInv(string Date)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.ProductList>();

            string comcod = ObjCommon.GetCompCode();
            string userid = ObjCommon.GetUserCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_sales.SP_ENTRY_SALES_INVOICE", "SHOWPRODUCTPRICE", Date, userid, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.ProductList details = new SPEENTITY.C_19_Exp.Sales_BO.ProductList(dr["actcode"].ToString(), dr["prcod"].ToString(),
                    dr["prdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["avlablqty"].ToString()),
                    Convert.ToDouble(dr["tmprice"].ToString()), Convert.ToDouble(dr["price"].ToString()), Convert.ToBoolean(dr["wastatus"].ToString()),
                    dr["unit"].ToString(), dr["prodscode"].ToString(), dr["taxcode"].ToString(), dr["ordqty"].ToString(), dr["promqty"].ToString(),
                    Convert.ToDouble(dr["amount"].ToString()), dr["prtdesc"].ToString(), Convert.ToDouble(dr["upordqty"].ToString()), Convert.ToDouble(dr["balstkqyt"].ToString()),0.00);
                lst.Add(details);
            }

            return lst;
        }
        public List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> GetProduct(string Date, string Centrid)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.ProductList>();

            string comcod = ObjCommon.GetCompCode();
            string userid = ObjCommon.GetUserCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETPRODUCT", Date, userid, Centrid, "", "", "", "", "", ""); 
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.ProductList details = new SPEENTITY.C_19_Exp.Sales_BO.ProductList(dr["actcode"].ToString(), dr["prcod"].ToString(),
                    dr["prdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["avlablqty"].ToString()),
                    Convert.ToDouble(dr["tmprice"].ToString()), Convert.ToDouble(dr["price"].ToString()), Convert.ToBoolean(dr["wastatus"].ToString()),
                    dr["unit"].ToString(), dr["prodscode"].ToString(), dr["taxcode"].ToString(), dr["ordqty"].ToString(), dr["promqty"].ToString(),
                    Convert.ToDouble(dr["amount"].ToString()), dr["prtdesc"].ToString(), Convert.ToDouble(dr["upordqty"].ToString()), Convert.ToDouble(dr["balstkqyt"].ToString()), Convert.ToDouble(dr["disinper"].ToString()));
                lst.Add(details);
            }

            return lst;
        }
        public List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> GetProductGrp(string comcod, string Date, string Centrid, string userid)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.ProductList>();
            
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETPRODUCT", Date, userid, Centrid, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.ProductList details = new SPEENTITY.C_19_Exp.Sales_BO.ProductList(dr["actcode"].ToString(), dr["prcod"].ToString(),
                    dr["prdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["avlablqty"].ToString()),
                    Convert.ToDouble(dr["tmprice"].ToString()), Convert.ToDouble(dr["price"].ToString()), Convert.ToBoolean(dr["wastatus"].ToString()),
                    dr["unit"].ToString(), dr["prodscode"].ToString(), dr["taxcode"].ToString(), dr["ordqty"].ToString(), dr["promqty"].ToString(),
                    Convert.ToDouble(dr["amount"].ToString()), dr["prtdesc"].ToString(), Convert.ToDouble(dr["upordqty"].ToString()), Convert.ToDouble(dr["balstkqyt"].ToString()), Convert.ToDouble(dr["disinper"].ToString()));
                lst.Add(details);
            }

            return lst;
        }
        public List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory> GetProcatGrp(string comcod, string Date, string Centrid, string userid)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory>();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETPRODUCT", Date, userid, Centrid, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory details = new SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory(dr["actcode"].ToString(), dr["prcod"].ToString(),
                    dr["prdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["avlablqty"].ToString()),
                    Convert.ToDouble(dr["tmprice"].ToString()), Convert.ToDouble(dr["price"].ToString()), Convert.ToBoolean(dr["wastatus"].ToString()),
                    dr["unit"].ToString(), dr["prodscode"].ToString(), dr["taxcode"].ToString(), dr["ordqty"].ToString(), dr["promqty"].ToString(), dr["procat"].ToString(), dr["procatdesc"].ToString(), dr["prosubcat"].ToString(), dr["prosubcatdesc"].ToString(), Convert.ToDouble(dr["amount"].ToString()));
                lst.Add(details);
            }

            return lst;
        }
        public List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory> GetProcat(string Date, string Centrid)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory>();

            string comcod = ObjCommon.GetCompCode();
            string userid = ObjCommon.GetUserCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETPRODUCT", Date, userid, Centrid, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory details = new SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory(dr["actcode"].ToString(), dr["prcod"].ToString(),
                    dr["prdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["avlablqty"].ToString()),
                    Convert.ToDouble(dr["tmprice"].ToString()), Convert.ToDouble(dr["price"].ToString()), Convert.ToBoolean(dr["wastatus"].ToString()),
                    dr["unit"].ToString(), dr["prodscode"].ToString(), dr["taxcode"].ToString(), dr["ordqty"].ToString(), dr["promqty"].ToString(), dr["procat"].ToString(), dr["procatdesc"].ToString(), dr["prosubcat"].ToString(), dr["prosubcatdesc"].ToString(), Convert.ToDouble(dr["amount"].ToString()));
                lst.Add(details);
            }

            return lst;
        }



        public List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> GetProduct(string comcod, string userid, string Date, string Centrid)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.ProductList> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.ProductList>();

          
          
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETPRODUCT", Date, userid, Centrid, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.ProductList details = new SPEENTITY.C_19_Exp.Sales_BO.ProductList(dr["actcode"].ToString(), dr["prcod"].ToString(),
                    dr["prdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["avlablqty"].ToString()),
                    Convert.ToDouble(dr["tmprice"].ToString()), Convert.ToDouble(dr["price"].ToString()), Convert.ToBoolean(dr["wastatus"].ToString()),
                    dr["unit"].ToString(), dr["prodscode"].ToString(), dr["taxcode"].ToString(), dr["ordqty"].ToString(), dr["promqty"].ToString(),
                    Convert.ToDouble(dr["amount"].ToString()), dr["prtdesc"].ToString(), Convert.ToDouble(dr["upordqty"].ToString()), Convert.ToDouble(dr["balstkqyt"].ToString()),0.00);
                lst.Add(details);
            }

            return lst;
        }

        public List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory> GetProcat(string comcod, string userid, string Date, string Centrid)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory>();

           
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETPRODUCT", Date, userid, Centrid, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory details = new SPEENTITY.C_19_Exp.Sales_BO.prowithcaragory(dr["actcode"].ToString(), dr["prcod"].ToString(),
                    dr["prdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["avlablqty"].ToString()),
                    Convert.ToDouble(dr["tmprice"].ToString()), Convert.ToDouble(dr["price"].ToString()), Convert.ToBoolean(dr["wastatus"].ToString()),
                    dr["unit"].ToString(), dr["prodscode"].ToString(), dr["taxcode"].ToString(), dr["ordqty"].ToString(), dr["promqty"].ToString(), dr["procat"].ToString(), dr["procatdesc"].ToString(), dr["prosubcat"].ToString(), dr["prosubcatdesc"].ToString(), Convert.ToDouble(dr["amount"].ToString()));
                lst.Add(details);
            }

            return lst;
        }
        
        
        
        public List<SPEENTITY.C_19_Exp.Sales_BO.LastInvNo> GetLastInv(string Date, string Center)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.LastInvNo> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.LastInvNo>();

            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_Sales.SP_ENTRY_SALES_INVOICE", "GETLASTINVNO", Date, Center, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.LastInvNo details = new SPEENTITY.C_19_Exp.Sales_BO.LastInvNo(dr["maxsinvno"].ToString(), dr["maxsinvno1"].ToString());
                lst.Add(details);
            }

            return lst;
        }
        public List<SPEENTITY.C_19_Exp.Sales_BO.SalesExp_BO> GetSalesExp(string Date)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.SalesExp_BO> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.SalesExp_BO>();
            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_SALSMGT_03", "RPTSALESVSEXP", Date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.SalesExp_BO details = new SPEENTITY.C_19_Exp.Sales_BO.SalesExp_BO(dr["actcode"].ToString(), dr["actdesc"].ToString(),
                    Convert.ToDouble(dr["salupamt"]), Convert.ToDouble(dr["salcuamt"]), Convert.ToDouble(dr["expupamt"]), Convert.ToDouble(dr["excuamt"]), Convert.ToDouble(dr["salpercent"]),
                    Convert.ToDouble(dr["exppercent"]));

                lst.Add(details);

            }
            return lst;

        }

        public DataSet GetInstallmentCodes()
        { 
            // List<SPEENTITY.C_19_Exp.Sales_BO.InstllmentCodeDetails> inslist = new List<SPEENTITY.C_19_Exp.Sales_BO.InstllmentCodeDetails>(); 
            string comcod = ObjCommon.GetCompCode();
             DataSet ds = _ProAccess.GetTransInfo(comcod, "dbo_Sales.SP_ENTRY_SALES_INVOICE", "GETINSTCODE");
            //inslist = ds.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.Sales_BO.InstllmentCodeDetails>();
            return ds;
        }

        public DataSet GetDataSetForUpdate(string CompCode, DateTime MemoDate1, string centrid, string memono, string MemoRef1, string MemoNar1, string Orderno,
            double invdis, string custid, string teamcode, string postedbyid, string postseson, string postrmid, DateTime posteddat,
            List<SPEENTITY.C_19_Exp.Sales_BO.ShowInvItem> ListViewItemTable1)
        {
            //comcod, centrid, memono, memodat, refno, narration, postedbyid, postseson, postrmid, orderno, paymnttrm, invdis, custid, posteddat, teamcode, freepro
            DataSet ds1 = new DataSet("dsst");
            DataTable tbl1b = new DataTable("tblb");
            tbl1b.Columns.Add("comcod", typeof(String));
            tbl1b.Columns.Add("centrid", typeof(String));
            tbl1b.Columns.Add("memono", typeof(String));
            tbl1b.Columns.Add("memodat", typeof(String));
            tbl1b.Columns.Add("custid", typeof(String));
            tbl1b.Columns.Add("teamcode", typeof(String));
            tbl1b.Columns.Add("orderno", typeof(String));
            tbl1b.Columns.Add("invdis", typeof(Decimal));
            tbl1b.Columns.Add("refno", typeof(String));
            tbl1b.Columns.Add("narration", typeof(String));
            tbl1b.Columns.Add("paymnttrm", typeof(String));
            tbl1b.Columns.Add("postedbyid", typeof(String));
            tbl1b.Columns.Add("postseson", typeof(String));
            tbl1b.Columns.Add("postrmid", typeof(String));
            tbl1b.Columns.Add("posteddat", typeof(String));
            tbl1b.Columns.Add("freepro", typeof(String));

            DataRow dr1b = tbl1b.NewRow();
            dr1b["comcod"] = CompCode;
            dr1b["centrid"] = centrid;
            dr1b["memono"] = memono;
            dr1b["memodat"] = MemoDate1;
            dr1b["custid"] = custid;
            dr1b["teamcode"] = teamcode;
            dr1b["orderno"] = Orderno;
            dr1b["invdis"] = invdis;
            dr1b["refno"] = MemoRef1;
            dr1b["narration"] = MemoNar1;
            dr1b["paymnttrm"] = "";
            dr1b["postedbyid"] = postedbyid;
            dr1b["postseson"] = postseson;
            dr1b["postrmid"] = postrmid;
            dr1b["posteddat"] = posteddat;
            dr1b["freepro"] = "0";
                        
            tbl1b.Rows.Add(dr1b);
            ds1.Tables.Add(tbl1b);

            DataTable tbl1a = new DataTable("tbla");
            tbl1a.Columns.Add("comcod", typeof(String));
            tbl1a.Columns.Add("centrid", typeof(String));
            tbl1a.Columns.Add("memono", typeof(String));
            tbl1a.Columns.Add("prcod", typeof(String));
            tbl1a.Columns.Add("batchcode", typeof(String));
            tbl1a.Columns.Add("sdelno", typeof(String));
            tbl1a.Columns.Add("itmqty", typeof(Decimal));
            tbl1a.Columns.Add("itmamt", typeof(Decimal));
            tbl1a.Columns.Add("itmdis", typeof(Decimal));
            tbl1a.Columns.Add("vat", typeof(Decimal));
            tbl1a.Columns.Add("frqty", typeof(Decimal));
            tbl1a.Columns.Add("taxcode", typeof(String));
            tbl1a.Columns.Add("wardate", typeof(String));

           

            foreach (var item1a in ListViewItemTable1)
            {
                DataRow dr1a = tbl1a.NewRow();
                dr1a["comcod"] = CompCode;
                dr1a["centrid"] = item1a.actcode;
                dr1a["memono"] = memono;
                dr1a["prcod"] = item1a.subcode;
                dr1a["batchcode"] = item1a.batchcode;
                dr1a["sdelno"] = item1a.sdelno;
                dr1a["itmqty"] = item1a.trnqty;
                dr1a["itmamt"] = item1a.amount;
                dr1a["itmdis"] = item1a.discount;
                dr1a["vat"] = item1a.taxamt;
                dr1a["frqty"] = item1a.frqty;
                dr1a["taxcode"] = item1a.taxcode;
                dr1a["wardate"] = DateTime.Now.AddMonths(3).ToString("dd-MMM-yyyy hh:mm:ss tt");
                tbl1a.Rows.Add(dr1a);
            }
            ds1.Tables.Add(tbl1a);
            return ds1;
        }

        public List<SPEENTITY.C_19_Exp.Sales_BO.SalesLedger_BO> GetSalesLedger(string Store, string Date1, string Date2)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.SalesLedger_BO> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.SalesLedger_BO>();
            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "SALESLEDGER", Store, Date1, Date2, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.SalesLedger_BO details = new SPEENTITY.C_19_Exp.Sales_BO.SalesLedger_BO(dr["centrid"].ToString(), dr["custid"].ToString(), dr["custdesc"].ToString(),
                    Convert.ToDouble(dr["itmamt"]), Convert.ToDouble(dr["itmdis"]), Convert.ToDouble(dr["invdis"]), Convert.ToDouble(dr["totaldis"]), Convert.ToDouble(dr["vat"]),
                    Convert.ToDouble(dr["totalrecamt"]), Convert.ToDouble(dr["freeamt"]));

                lst.Add(details);

            }
            return lst;

        }

        public List<SPEENTITY.C_19_Exp.Sales_BO.EClassGetAllInv> ShowAllInv(string Date1, string Date2, string Type, string Circode, string regcode, string arecode, string tericode)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.EClassGetAllInv> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.EClassGetAllInv>();
            string userid = ObjCommon.GetUserCode();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_sales.SP_ENTRY_SALES_INVOICE", "GETPREALLINV", Date1, Date2, userid, Type, Circode, regcode, arecode, tericode, "");

            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.EClassGetAllInv vat = new C_19_Exp.Sales_BO.EClassGetAllInv(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
                    dr["custid"].ToString(), dr["custdesc"].ToString(), dr["memono"].ToString(), dr["memono1"].ToString(), dr["vounum1"].ToString(), dr["memodat"].ToString(),
                    Convert.ToDouble(dr["itmamt"]), Convert.ToDouble(dr["vat"]), Convert.ToDouble(dr["invdis"]), Convert.ToDouble(dr["vatper"]),
                    Convert.ToDouble(dr["invdisper"]), dr["payvounum"].ToString(), dr["teamcode"].ToString(), dr["teamdesc"].ToString(), dr["tercode"].ToString(), dr["territory"].ToString());
                lst.Add(vat);
            }

            return lst;

        }
        public List<SPEENTITY.C_19_Exp.Sales_BO.SalMargin> Margin()
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.SalMargin> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.SalMargin>();
            string userid = ObjCommon.GetUserCode();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_sales.SP_ENTRY_SALES_INVOICE", "GETSALESMARGIN", "", "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_19_Exp.Sales_BO.SalMargin vat = new SPEENTITY.C_19_Exp.Sales_BO.SalMargin(dr["comcod"].ToString(), dr["userid"].ToString(),
                    Convert.ToInt16(dr["orderid"]), Convert.ToDouble(dr["minamt"]), Convert.ToDouble(dr["maxamt"]), Convert.ToDouble(dr["dispar"]));
                lst.Add(vat);
            }
            return lst;

        }
        //public List<MFGOBJ.MfgEntityDataBase.EntityGenCode.SalPromotion> Sale_Promotion(string Date)
        //{
        //    List<MFGOBJ.MfgEntityDataBase.EntityGenCode.SalPromotion> lst = new List<MFGOBJ.MfgEntityDataBase.EntityGenCode.SalPromotion>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALE_PROMOTION", "GETPROMOTION", Date, "", "", "", "", "", "", "", "");

        //    while (dr.Read())
        //    {
        //        MFGOBJ.MfgEntityDataBase.EntityGenCode.SalPromotion vat = new MfgEntityDataBase.EntityGenCode.SalPromotion(dr["comcod"].ToString(), dr["prodcode"].ToString(),
        //            Convert.ToDouble(dr["srate"]), Convert.ToDouble(dr["prrate"]), Convert.ToDouble(dr["qty"]), Convert.ToDouble(dr["promqty"]));
        //        lst.Add(vat);
        //    }
        //    return lst;
        //}
        public DataSet PreCurYearData(string Date)
        {
            string comcod = ObjCommon.GetCompCode();
            return _ProAccess.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTSALECUPREYEAR", Date);
        }

        //Proceed Realization Sheet

        public List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate> GetProcRealSheet(string fdate, string tdate, string rptype)
        {
            List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate> lst = new List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate>();

            string comcod = ObjCommon.GetCompCode();
           
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_EXPORT", "PROCEEDREALIZATIONSHEET", fdate, tdate, rptype, "", "", "", "", "", "");
            while (dr.Read())
            {
                  
                SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate details = new SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate(
                    dr["comcod"].ToString(), dr["invno"].ToString(), dr["invrefno"].ToString(), dr["mlccod"].ToString(),
                    dr["buyerid"].ToString(),  Convert.ToDouble(dr["invexrate"].ToString()),
                    Convert.ToDouble(dr["ordramt"].ToString()), Convert.ToDouble(dr["ordramtbdt"].ToString()),
                    Convert.ToDouble(dr["salefc"].ToString()), Convert.ToDouble(dr["rcvfc"].ToString()), 
                    Convert.ToDouble(dr["marginfc"].ToString()), Convert.ToDouble(dr["exmacfc"].ToString()), 
                    Convert.ToDouble(dr["erqacfc"].ToString()), Convert.ToDouble(dr["cmfc"].ToString()), 
                    Convert.ToDouble(dr["fcbnkcharge"].ToString()), Convert.ToDouble(dr["recvexrate"].ToString()),
                    Convert.ToDouble(dr["salesamt"].ToString()), Convert.ToDouble(dr["marginamt"].ToString()), 
                    Convert.ToDouble(dr["exmacamt"].ToString()), Convert.ToDouble(dr["erqacamt"].ToString()), 
                    Convert.ToDouble(dr["cmamt"].ToString()), Convert.ToDouble(dr["bnkchargeamt"].ToString()),
                    dr["buyerdesc"].ToString(), dr["lcdesc"].ToString(), Convert.ToDouble(dr["exgainlossamt"].ToString()), Convert.ToDouble(dr["duesfc"].ToString()), dr["invno1"].ToString(), dr["curdesc"].ToString(), dr["cursymbol"].ToString());
                lst.Add(details);
            }

            return lst;
        }
    }
}
