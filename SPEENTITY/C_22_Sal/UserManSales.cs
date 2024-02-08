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

namespace SPEENTITY.C_22_Sal
{
    public class UserManSales
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

        //#region  Commercial Proposal


        //public List<SPEENTITY.C_22_Sal.EClassCustomer> GetCustomer(string pactcode, string Serchoption)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassCustomer> lst = new List<SPEENTITY.C_22_Sal.EClassCustomer>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETCUSTOMER", pactcode, Serchoption, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassCustomer details = new SPEENTITY.C_22_Sal.EClassCustomer(dr["custcode"].ToString(), dr["custname"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassProduct> GetProduct(string Serchoption, string Type)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassProduct> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassProduct>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETGETPRODUCT", Serchoption, Type, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassProduct details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassProduct(dr["sircode"].ToString(), dr["sirdesc"].ToString(),
        //            dr["sirunit"].ToString(), Convert.ToDouble(dr["rate"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}


        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassGroup> GetGroup(string Serchoption)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassGroup> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassGroup>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETGROUP", Serchoption, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassGroup details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassGroup(dr["mattype"].ToString(), dr["mattypedesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassMaterial> GetMaterial(string Serchoption)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassMaterial> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassMaterial>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETPROMATREIALIST", Serchoption, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassMaterial details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassMaterial(dr["sircode"].ToString(), dr["sirdesc"].ToString(), dr["sirunit"].ToString(), Convert.ToDouble(dr["price"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassLastComPro> GetLastCProNo(string Date)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassLastComPro> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassLastComPro>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETLASTCPRONO", Date, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassLastComPro details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassLastComPro(dr["maxsprono"].ToString(), dr["maxsprono1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassSalProInfo> GetSalProInfo(string sprono)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassSalProInfo> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassSalProInfo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETSALPROINFO", sprono, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassSalProInfo details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassSalProInfo(dr["procode"].ToString(), 
        //            dr["mattype"].ToString(), dr["rsircode"].ToString(), dr["prodesc"].ToString(), dr["mattypedesc"].ToString(), dr["rsirdesc"].ToString(), 
        //            dr["rsirunit"].ToString(), Convert.ToDouble(dr["qty"].ToString()), Convert.ToDouble(dr["prorate"].ToString()),
        //            Convert.ToDouble(dr["proamt"].ToString()), dr["prtdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassTermAndCondition> GetTermAndCon(string sprono)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassTermAndCondition> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassTermAndCondition>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETPROTERMACON", sprono, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassTermAndCondition details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassTermAndCondition(dr["termsid"].ToString(), dr["termssubj"].ToString(), dr["termsdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassSalPro> GetSalPro(string Orderno)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassSalPro> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassSalPro>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETSALEPRO", Orderno, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassSalPro details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassSalPro(dr["sprono1"].ToString(), 
        //            Convert.ToDateTime(dr["sprodate"].ToString()), dr["pactcode"].ToString(), dr["refno"].ToString(), dr["narration"].ToString(),
        //            dr["usircode"].ToString(), Convert.ToDouble(dr["ovdis"]), dr["pbody"].ToString(), dr["phead"].ToString(), dr["tdesc"].ToString(), dr["attn"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassComProposal.EClassPreComPro> GetPreComProNo(string Date)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassComProposal.EClassPreComPro> lst = new List<SPEENTITY.C_22_Sal.EClassComProposal.EClassPreComPro>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETPREVIOUSLIST", Date, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassComProposal.EClassPreComPro details = new SPEENTITY.C_22_Sal.EClassComProposal.EClassPreComPro(dr["sprono"].ToString(), dr["sprono1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //#endregion

        //#region SalesOrder

        //public List<SPEENTITY.C_22_Sal.EClassProject> GetProject(string Serchoption)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassProject> lst = new List<SPEENTITY.C_22_Sal.EClassProject>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_COMMPROPOSAL", "GETPROJETNAME", Serchoption, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassProject details = new SPEENTITY.C_22_Sal.EClassProject(dr["actcode"].ToString(), dr["actdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassResource> GetResource(string Serchoption, string pactcode)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassResource> lst = new List<SPEENTITY.C_22_Sal.EClassResource>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETMATREIALIST", Serchoption, pactcode, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassResource details = new SPEENTITY.C_22_Sal.EClassResource(dr["sircode"].ToString(), dr["sirdesc"].ToString(), dr["sirunit"].ToString(), Convert.ToDouble(dr["price"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassSpecification> GetSpecification(string Serchoption)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassSpecification> lst = new List<SPEENTITY.C_22_Sal.EClassSpecification>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETSPECIFICATIONLIST", Serchoption, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassSpecification details = new SPEENTITY.C_22_Sal.EClassSpecification(dr["msircode"].ToString(), dr["spcfcod"].ToString(), dr["spcfdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassProposalNo> GetSalProNo(string Serchoption, string pactcode)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassProposalNo> lst = new List<SPEENTITY.C_22_Sal.EClassProposalNo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GESALPRONO", Serchoption, pactcode, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassProposalNo details = new SPEENTITY.C_22_Sal.EClassProposalNo(dr["sprono"].ToString(), dr["sprono1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}


        //public List<SPEENTITY.C_22_Sal.EClassLastOrder> GetLastOrderNo(string Date)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassLastOrder> lst = new List<SPEENTITY.C_22_Sal.EClassLastOrder>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETLASTSORDERNO", Date, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassLastOrder details = new SPEENTITY.C_22_Sal.EClassLastOrder(dr["maxsorderno"].ToString(), dr["maxsorderno1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassOrderInfo> GetSaleOrderInfo(string Orderno)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassOrderInfo> lst = new List<SPEENTITY.C_22_Sal.EClassOrderInfo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETSALEORDINFO", Orderno, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassOrderInfo details = new SPEENTITY.C_22_Sal.EClassOrderInfo(dr["rsircode"].ToString(), dr["rsirdesc"].ToString(), dr["rsirunit"].ToString(), Convert.ToDouble(dr["qty"].ToString()), Convert.ToDouble(dr["orderrate"].ToString()), Convert.ToDouble(dr["orderamt"].ToString()), dr["sprono"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassProDetails> GetProDetails(string sprono)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassProDetails> lst = new List<SPEENTITY.C_22_Sal.EClassProDetails>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETSALPRODETAILS", sprono, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassProDetails details = new SPEENTITY.C_22_Sal.EClassProDetails(dr["sprono"].ToString(), dr["rsircode"].ToString(), dr["rsirdesc"].ToString(), dr["rsirunit"].ToString(), Convert.ToDouble(dr["qty"].ToString()), Convert.ToDouble(dr["proamt"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassOrder> GetSaleOrder(string Orderno)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassOrder> lst = new List<SPEENTITY.C_22_Sal.EClassOrder>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETSALEORDER", Orderno, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassOrder details = new SPEENTITY.C_22_Sal.EClassOrder(dr["sorderno1"].ToString(), Convert.ToDateTime(dr["sorddate"].ToString()), dr["pactcode"].ToString(), dr["refno"].ToString(), dr["narration"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassPreOrder> GetPreOrderNo(string Date)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassPreOrder> lst = new List<SPEENTITY.C_22_Sal.EClassPreOrder>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETPREVIOUSLIST", Date, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassPreOrder details = new SPEENTITY.C_22_Sal.EClassPreOrder(dr["sorderno"].ToString(), dr["sorderno1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassOrderSInfo> GetSaleOrdShortInfo(string Orderno)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassOrderSInfo> lst = new List<SPEENTITY.C_22_Sal.EClassOrderSInfo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_GENSALSMGT", "RPTSALESINFO", Orderno, "", "", "", "", "", "", "", "");


        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassOrderSInfo details = new SPEENTITY.C_22_Sal.EClassOrderSInfo(dr["sorderno1"].ToString(), Convert.ToDateTime(dr["sorddate"].ToString()),
        //              dr["sodrefno"].ToString(), dr["boffhead1"].ToString(), dr["boffdet1"].ToString(),
        //                dr["boffph1"].ToString(), dr["boffhead2"].ToString(), dr["boffdet2"].ToString(), dr["boffph2"].ToString(), dr["boffhead3"].ToString(),
        //                dr["boffdet3"].ToString(), dr["boffph3"].ToString(), dr["custname"].ToString(), dr["cussadd"].ToString(), dr["custphone"].ToString(), dr["custfax"].ToString(), dr["atten"].ToString(), dr["emailaweb"].ToString(), dr["pactdesc"].ToString(), dr["narration"].ToString(), dr["vesname"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}


        //#endregion
        //#region Delivery

        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassLastDel> GetLastDelNo(string Date, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassLastDel> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassLastDel>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETLASTDELNO", Date, Centrid, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassLastDel details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassLastDel(dr["maxsdelno"].ToString(), dr["maxsdelno1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;
        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder> GetSalesOrder(string Date, string SrchOption, string StoreID)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETBALORDER", Date, SrchOption, StoreID, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder(dr["sorderno"].ToString(),
        //            dr["sorderno1"].ToString(), dr["pactcode"].ToString(), dr["custcode"].ToString(), dr["custdesc"].ToString(), dr["teamdesc"].ToString(),
        //            dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["sorddate"].ToString(), dr["astatus"].ToString(), dr["cusaddr"].ToString(),
        //            dr["phone"].ToString(), dr["narration"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder> GetSalesOrderGrp(string comcod, string Date, string SrchOption, string StoreID)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder>();
            
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETBALORDER", Date, SrchOption, StoreID, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassSalesOrder(dr["sorderno"].ToString(),
        //            dr["sorderno1"].ToString(), dr["pactcode"].ToString(), dr["custcode"].ToString(), dr["custdesc"].ToString(), dr["teamdesc"].ToString(),
        //            dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["sorddate"].ToString(), dr["astatus"].ToString(), dr["cusaddr"].ToString(),
        //            dr["phone"].ToString(), dr["narration"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassDelDetails> GetDelDetails(string delno, string Centrid)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassDelDetails> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassDelDetails>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETDELDETAILS", delno, Centrid, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassDelDetails details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassDelDetails(dr["rsircode"].ToString(),
        //                dr["rsirdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), dr["rsirunit"].ToString(), Convert.ToDouble(dr["sorderqty"].ToString()),
        //                Convert.ToDouble(dr["utodelqty"].ToString()), Convert.ToDouble(dr["balqty"].ToString()), Convert.ToDouble(dr["delqty"].ToString()),
        //                Convert.ToDouble(dr["avlablqty"].ToString()), dr["wastatus"].ToString(), Convert.ToDouble(dr["promqty"].ToString()),
        //                Convert.ToDouble(dr["dpromqty"].ToString()), Convert.ToDouble(dr["tqty"].ToString()), Convert.ToDouble(dr["stockqty"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrdAppDetails> GetAppDetails(string appno, string Centrid)
        //{


        


        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrdAppDetails> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrdAppDetails>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETORDAPPDETAILS", appno, Centrid, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassOrdAppDetails details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassOrdAppDetails(dr["pactcode"].ToString(), dr["orderno"].ToString(), dr["orderno1"].ToString(), dr["rsircode"].ToString(),
        //                dr["rsirdesc"].ToString(),  dr["rsirunit"].ToString(), Convert.ToDouble(dr["ordrqty"].ToString()),
        //                Convert.ToDouble(dr["ordraqty"].ToString()), Convert.ToDouble(dr["balty"].ToString()), Convert.ToDouble(dr["aprovqty"].ToString()),
        //                Convert.ToDouble(dr["ordamt"].ToString()),  Convert.ToDouble(dr["orddis"].ToString()),  Convert.ToDouble(dr["freeqty"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}


        
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassGetDelivery> GetDelivery(string Orderno, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassGetDelivery> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassGetDelivery>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETDELIVERY", Orderno, Centrid, "", "", "", "", "", "", "");


        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassGetDelivery details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassGetDelivery(dr["sdelno1"].ToString(), 
        //            Convert.ToDateTime(dr["sdeldate"].ToString()), dr["sorderno1"].ToString(), dr["refno"].ToString(), dr["narration"].ToString(), 
        //            dr["sorderno"].ToString(), dr["pactdesc"].ToString(), dr["sactcode"].ToString(), dr["sircode"].ToString(), dr["sirdesc"].ToString(),
        //            dr["transdet1"].ToString(), dr["transdet2"].ToString(), dr["transdet3"].ToString(), dr["transdet3"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassPreDelivery> GetPreDeliveryNp(string Date, string Srchoption, string Store)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassPreDelivery> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassPreDelivery>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETPREDELLIST", Date, Srchoption, Store, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassPreDelivery details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassPreDelivery(dr["sdelno"].ToString(), dr["sdelno1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo> GetOrderInfo(string delno, string Actcode, string Date, string Scan)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETORDINFO", delno, Actcode, Date, Scan, "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo(dr["rsircode"].ToString(), dr["rsirdesc"].ToString(),
        //                dr["batchcode"].ToString(), dr["batchdesc"].ToString(), dr["rsirunit"].ToString(), Convert.ToDouble(dr["sorderqty"].ToString()),
        //                Convert.ToDouble(dr["utodelqty"].ToString()), Convert.ToDouble(dr["balqty"].ToString()), Convert.ToDouble(dr["delqty"].ToString()),
        //                Convert.ToDouble(dr["avlablqty"].ToString()), (dr["wastatus"]).ToString(), Convert.ToDouble(dr["promqty"].ToString()),
        //                Convert.ToDouble(dr["dpromqty"].ToString()), Convert.ToDouble(dr["tqty"].ToString()), Convert.ToDouble(dr["stockqty"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo> GetOrderInfoGrp(string comcod, string delno, string Actcode, string Date, string Scan)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo>();
            
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETORDINFO", delno, Actcode, Date, Scan, "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo(dr["rsircode"].ToString(), dr["rsirdesc"].ToString(),
        //                dr["batchcode"].ToString(), dr["batchdesc"].ToString(), dr["rsirunit"].ToString(), Convert.ToDouble(dr["sorderqty"].ToString()),
        //                Convert.ToDouble(dr["utodelqty"].ToString()), Convert.ToDouble(dr["balqty"].ToString()), Convert.ToDouble(dr["delqty"].ToString()),
        //                Convert.ToDouble(dr["avlablqty"].ToString()), (dr["wastatus"]).ToString(), Convert.ToDouble(dr["promqty"].ToString()),
        //                Convert.ToDouble(dr["dpromqty"].ToString()), Convert.ToDouble(dr["tqty"].ToString()), Convert.ToDouble(dr["stockqty"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassStore> GetStore(string srchoption)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassStore> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassStore>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "GETCENTERCODE", srchoption, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassStore details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassStore(dr["actcode"].ToString(), dr["actdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassStore> GetStoreGrp(string comcod, string srchoption)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassStore> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassStore>();
            
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "GETCENTERCODE", srchoption, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassStore details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassStore(dr["actcode"].ToString(), dr["actdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEI> GetIMEIDETAILS(string procode, string packno, string actcode)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEI> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEI>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_02", "GETPROPACKDETAIIS", procode, packno, actcode, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEI details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEI(dr["rsircode"].ToString(), dr["batchcode"].ToString(),
        //            dr["batchdesc"].ToString(), dr["packno"].ToString(), dr["mimei"].ToString(), dr["simei"].ToString(), dr["rsirdesc"].ToString(),Convert.ToInt16(dr["seq"]));
        //        lst.Add(details);
        //    }

        //    return lst;
        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEIPOS> GetPOSIMEIDETAILS(string packno)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEIPOS> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEIPOS>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER_03", "GETPOSDELIVERY", packno, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEIPOS details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEIPOS(dr["rsircode"].ToString(), dr["batchcode"].ToString(),
        //            dr["batchdesc"].ToString(), dr["packno"].ToString(), dr["mimei"].ToString(), dr["simei"].ToString(), dr["rsirdesc"].ToString(), Convert.ToInt16(dr["seq"]),
        //            Convert.ToDouble(dr["stprice"].ToString()), Convert.ToDouble(dr["tax"].ToString()), Convert.ToDouble(dr["disamt"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;
        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEITrans> GetIMEITransfer(string packno, string actcode)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEITrans> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEITrans>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_OPENING_CHALN", "GETTRANSIMEI", packno, actcode, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEITrans details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassProIMEITrans(dr["rsircode"].ToString(), dr["batchcode"].ToString(),
        //            dr["batchdesc"].ToString(), dr["packno"].ToString(), dr["mimei"].ToString(), dr["simei"].ToString(), dr["rsirdesc"].ToString(), Convert.ToInt16(dr["seq"]),
        //            Convert.ToDouble(dr["stprice"].ToString()), Convert.ToDouble(dr["wsale"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;
        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassRetIMEI> GetRetIMEI(string packno, string Pactcode, string orderno)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassRetIMEI> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassRetIMEI>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_RETURN", "CHKIMEI", packno, Pactcode, orderno, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassRetIMEI details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassRetIMEI(dr["sdelno"].ToString(),dr["rsircode"].ToString(), dr["packno"].ToString(),
        //            dr["mimei"].ToString(), dr["simei"].ToString(), dr["rsirdesc"].ToString(), Convert.ToInt16(dr["seq"]), dr["batchcode"].ToString(), dr["batchdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;
        //}
        //public List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo2> GetOrderInfo2(string delno, string Actcode, string Date)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo2> lst = new List<SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo2>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETORDINFO", delno, Actcode, Date, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo2 details = new SPEENTITY.C_22_Sal.EClassDelivery.EClassOrderInfo2(dr["rsircode"].ToString(), dr["rsirdesc"].ToString(),
        //                dr["batchcode"].ToString(), dr["batchdesc"].ToString(), dr["rsirunit"].ToString(), Convert.ToDouble(dr["sorderqty"].ToString()),
        //                Convert.ToDouble(dr["utodelqty"].ToString()), Convert.ToDouble(dr["balqty"].ToString()), Convert.ToDouble(dr["delqty"].ToString()),
        //                Convert.ToDouble(dr["avlablqty"].ToString()), (dr["wastatus"]).ToString(), Convert.ToDouble(dr["promqty"].ToString()),
        //                Convert.ToDouble(dr["dpromqty"].ToString()), Convert.ToDouble(dr["stockqty"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        
        //#endregion

        //#region Invoice

        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDues> GetDues(string Centrid, string custid, string Saledate, string Memono)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDues> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDues>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_SALSMGT", "PREDUES", Centrid, custid, Saledate, Memono, "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassDues details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassDues(Convert.ToDouble(dr["dues"].ToString()),
        //            Convert.ToDouble(dr["vat"].ToString()), Convert.ToDouble(dr["collamt"].ToString()), Convert.ToDouble(dr["inddis"].ToString()));
        //        lst.Add(details);
        //    }
        //    return lst;
        //}

        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassLastInv> GetLastInvNo(string Date, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassLastInv> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassLastInv>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_INVOICE", "GETLASTINVNO", Date, Centrid, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassLastInv details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassLastInv(dr["maxsinvno"].ToString(), dr["maxsinvno1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;
        //}
        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassSalesOrder> GetInvSalesOrder(string Date, string SrchOption, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassSalesOrder> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassSalesOrder>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_ORDER", "GETPREORDERLIST", Date, SrchOption, Centrid, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassSalesOrder details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassSalesOrder(dr["orderno"].ToString(), dr["orderno2"].ToString(),
        //                dr["centrid"].ToString(), dr["centrdesc"].ToString(), dr["custcode"].ToString(), dr["custdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassGetInv> GetInvoice(string invno, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassGetInv> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassGetInv>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETINVOICE", invno, Centrid, "", "", "", "", "", "", "");


        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassGetInv details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassGetInv(dr["memono1"].ToString(), Convert.ToDateTime(dr["memodat"].ToString()),
        //            dr["refno"].ToString(), dr["narration"].ToString(), dr["paymnttrm"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDelNo> GetDelNo(string Delno, string Srchoption, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDelNo> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDelNo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_INVOICE", "GETPREDELLIST", Delno, Srchoption, Centrid, "", "", "", "", "", "");


        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassDelNo details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassDelNo(dr["sdelno"].ToString(), dr["sdelno1"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassInvDetails> GetInvDetails(string invno, string Centrid)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassInvDetails> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassInvDetails>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETINVOICEDETAILS", invno, Centrid, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassInvDetails details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassInvDetails(dr["sdelno"].ToString(), dr["sdelno1"].ToString(),
        //                dr["rsircode"].ToString(), Convert.ToDouble(dr["invqty"].ToString()), Convert.ToDouble(dr["invrate"].ToString()), Convert.ToDouble(dr["invamt"].ToString()),
        //                dr["rsirdesc"].ToString(), dr["rsirunit"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["invdis"].ToString()),
        //                Convert.ToDouble(dr["invvat"].ToString()), dr["teamcode"].ToString(), dr["centrid"].ToString(), dr["centrdesc"].ToString(), Convert.ToDouble(dr["ovdis"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDelInfo> GetDelInfo(string delno, string Centrid)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDelInfo> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassDelInfo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_INVOICE", "SHOWINVDETAILS", delno, Centrid, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassDelInfo details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassDelInfo(dr["sdelno"].ToString(), dr["sdelno1"].ToString(),
        //            dr["rsircode"].ToString(), Convert.ToDouble(dr["invqty"].ToString()), Convert.ToDouble(dr["invrate"].ToString()), Convert.ToDouble(dr["invamt"].ToString()),
        //            dr["rsirdesc"].ToString(), dr["rsirunit"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), Convert.ToDouble(dr["invdis"].ToString()),
        //            Convert.ToDouble(dr["invvat"].ToString()), dr["teamcode"].ToString(), dr["centrid"].ToString(), dr["centrdesc"].ToString(), Convert.ToDouble(dr["ovdis"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassPreInvoice> GetPreInvNo(string Date, string srchoption, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassPreInvoice> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassPreInvoice>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "GETPREINVLIST", Date, srchoption, Centrid, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassPreInvoice details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassPreInvoice(dr["sinvno"].ToString(), dr["sinvno1"].ToString(),
        //            dr["orderno"].ToString(), dr["orderno2"].ToString(), dr["centrid"].ToString(), dr["centrdesc"].ToString(), dr["custcode"].ToString(), dr["custdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}





        //public List<SPEENTITY.C_22_Sal.EClassInvoice.EClassInvInfo> GetInvDetailsInfo(string invno)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassInvoice.EClassInvInfo> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.EClassInvInfo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_GENSALSMGT", "RPTINVOICEINFO", invno, "", "", "", "", "", "", "", "");


        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.EClassInvInfo details = new SPEENTITY.C_22_Sal.EClassInvoice.EClassInvInfo(dr["sinvno1"].ToString(), Convert.ToDateTime(dr["sinvdate"].ToString()),
        //                dr["paymnttrm"].ToString(), dr["sodrefno"].ToString(), dr["delno1"].ToString(), dr["boffhead1"].ToString(), dr["boffdet1"].ToString(),
        //                dr["boffph1"].ToString(), dr["boffhead2"].ToString(), dr["boffdet2"].ToString(), dr["boffph2"].ToString(), dr["boffhead3"].ToString(),
        //                dr["boffdet3"].ToString(), dr["boffph3"].ToString(), dr["cactdesc"].ToString(), dr["acno"].ToString(), dr["bankinfo"].ToString(),
        //                dr["custname"].ToString(), dr["cussadd"].ToString(), dr["custphone"].ToString(), dr["custfax"].ToString(), dr["atten"].ToString(), dr["emailaweb"].ToString(), dr["pactdesc"].ToString(), dr["narration"].ToString(), dr["vesname"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}


        //#endregion

        //#region Stock
        //public List<SPEENTITY.C_22_Sal.EClassInvoice.Product> GetProductInfo(string date, string centreid, string grp, string Type)
        //{


        //    List<SPEENTITY.C_22_Sal.EClassInvoice.Product> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.Product>();
        //    //string comcod = "8107";
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_FG_INV_02", "GETSTOCK", date, centreid, grp, Type, "", "", "", "", "");

        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.Product products = new C_22_Sal.EClassInvoice.Product(dr["comcod"].ToString(),  
        //            dr["prcod"].ToString(), dr["prodesc"].ToString(), dr["unit"].ToString(), Convert.ToDouble(dr["inqty"].ToString()), Convert.ToDouble(dr["salqty"].ToString()),
        //            Convert.ToDouble(dr["qty"].ToString()), Convert.ToDouble(dr["trnsqty"].ToString()), Convert.ToDouble(dr["trinqty"].ToString()),
        //            Convert.ToDouble(dr["retqty"].ToString()), Convert.ToDouble(dr["ordqty"].ToString()), Convert.ToDouble(dr["freeqty"].ToString()),
        //            Convert.ToDouble(dr["ofreeqty"].ToString()), Convert.ToDouble(dr["invstkqty"].ToString()), Convert.ToDouble(dr["grstkqty"].ToString()),
        //            Convert.ToDouble(dr["delqty"].ToString()), Convert.ToDouble(dr["convqty"].ToString())); 
        //        lst.Add(products);
        //    }

        //    return lst;
        //}

        //public List<SPEENTITY.C_22_Sal.EClassInvoice.Centre> GetCentreList()
        //{


        //    List<SPEENTITY.C_22_Sal.EClassInvoice.Centre> lst = new List<SPEENTITY.C_22_Sal.EClassInvoice.Centre>();
        //    //string comcod = "8107";
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_FG_INV_02", "GETDEPOID", "", "", "", "", "", "", "", "", "");

        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassInvoice.Centre centre = new C_22_Sal.EClassInvoice.Centre(dr["actcode"].ToString(), dr["actdesc"].ToString());
        //        lst.Add(centre);
        //    }

        //    return lst;


        //}
        //#endregion

        //#region Vat


        //public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassVaT> ShowVatCal(string Date1, string Date2)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassSales_02.EClassVaT> lst = new List<SPEENTITY.C_22_Sal.EClassSales_02.EClassVaT>();

        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_SALSMGT_03", "RPTVATCALCULATION", Date1, Date2, "", "", "", "", "", "", "");

        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassSales_02.EClassVaT vat = new C_22_Sal.EClassSales_02.EClassVaT(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
        //            dr["custid"].ToString(), dr["custdesc"].ToString(), dr["memono1"].ToString(), dr["vounum1"].ToString(), dr["memodat"].ToString(),
        //            Convert.ToDouble(dr["itmamt"]), Convert.ToDouble(dr["vat"]), Convert.ToDouble(dr["vatper"]), Convert.ToDouble(dr["invdis"]), Convert.ToDouble(dr["invdisper"]));
        //        lst.Add(vat);
        //    }

        //    return lst;

        //}

        //#endregion

        #region SalesDash_Board


        public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear> ShowYearly(string comcod, string Date1)
        {
            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear> lst = new List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear>();

           // string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALESINFOYEAR", Date1, "", "", "", "", "", "", "","");

            while (dr.Read())
            {
                SPEENTITY.C_22_Sal.EClassSales_02.EClassYear Yearly = new C_22_Sal.EClassSales_02.EClassYear(dr["yearid"].ToString(), Convert.ToDouble(dr["samt"]), Convert.ToDouble(dr["collamt"]), Convert.ToDouble(dr["balance"]));
                lst.Add(Yearly);
            }

            return lst;

        }

        public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassWeekly> ShowWeekly(string comcod, string Date1)
        {
            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassWeekly> lst = new List<SPEENTITY.C_22_Sal.EClassSales_02.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALESINFOWEEK", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_22_Sal.EClassSales_02.EClassWeekly Weekly = new C_22_Sal.EClassSales_02.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wsamt1"]),
                    Convert.ToDouble(dr["wcamt1"]), Convert.ToDouble(dr["wbal1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wsamt2"]), Convert.ToDouble(dr["wcamt2"]),
                    Convert.ToDouble(dr["wbal2"]),dr["wcode3"].ToString(), Convert.ToDouble(dr["wsamt3"]), Convert.ToDouble(dr["wcamt3"]), Convert.ToDouble(dr["wbal3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wsamt4"]),
                    Convert.ToDouble(dr["wcamt4"]), Convert.ToDouble(dr["wbal4"]));
                lst.Add(Weekly);
            }

            return lst;

        }
        public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly> ShowMonthly(string comcod, string Date1)
        {
            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly> lst = new List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALESINFYEARMONTH", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly Monthly = new C_22_Sal.EClassSales_02.EClassMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), Convert.ToDouble(dr["ttlsalamt"]), Convert.ToDouble(dr["collamt"]), Convert.ToDouble(dr["bal"]), Convert.ToDouble(dr["targtsaleamt"]), Convert.ToDouble(dr["tarcollamt"]), Convert.ToDouble(dr["targtsaleamtcore"]), Convert.ToDouble(dr["tarcollamtcore"]), Convert.ToDouble(dr["ttlsalamtcore"]), Convert.ToDouble(dr["collamtcrore"]));
                lst.Add(Monthly);
            }

            return lst;

        }

        public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise> ShowDayWise(string comcod, string Date1)
        {
            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise> lst = new List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISESALES", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise DayWise = new C_22_Sal.EClassSales_02.EClassDayWise(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
                    dr["custid"].ToString(), dr["custdesc"].ToString(), dr["memono1"].ToString(), dr["memono"].ToString(), dr["memodat"].ToString(),
                    dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["itmamt"]), Convert.ToDouble(dr["vat"]), Convert.ToDouble(dr["invdis"]),
                    Convert.ToDouble(dr["collamt"]), Convert.ToDouble(dr["netamt"]));
                lst.Add(DayWise);
            }

            return lst;

        }

        public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl> ShowDayWiseColl(string comcod, string Date1)
        {
            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl> lst = new List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISECOLL", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl DayWise = new C_22_Sal.EClassSales_02.EClassDayWiseColl(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
                    dr["custid"].ToString(), dr["custdesc"].ToString(), dr["mrslno1"].ToString(), dr["mrslno"].ToString(), dr["mrdat"].ToString(),
                    dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["amount"]));
                lst.Add(DayWise);
            }

            return lst;

        }

        #endregion

        //#region Sales_Return

        //public List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet> GetLastRetNo(string Date, string Centrid)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet> lst = new List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_RETURN", "LASTRETNO", Date, Centrid, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet LastRet = new SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet(dr["maxno"].ToString(), dr["maxno1"].ToString());
        //        lst.Add(LastRet);
        //    }

        //    return lst;
        //}
        //public List<SPEENTITY.C_22_Sal.EClassReturn.EClassGetInvoice> GetInvoiceNo(string Date, string Centrid, string srchorder, string Custid, string CurDate2)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassReturn.EClassGetInvoice> lst = new List<SPEENTITY.C_22_Sal.EClassReturn.EClassGetInvoice>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_RETURN", "GETINVOICENO", Date, Centrid, srchorder, Custid, CurDate2, "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassReturn.EClassGetInvoice GetInv = new SPEENTITY.C_22_Sal.EClassReturn.EClassGetInvoice(dr["invno"].ToString(), dr["invno1"].ToString(),
        //            dr["custcode"].ToString(), dr["custdesc"].ToString(), dr["orderno"].ToString(), dr["teamcode"].ToString(), dr["teamdesc"].ToString());
        //        lst.Add(GetInv);
        //    }

        //    return lst;
        //}

        //public List<SPEENTITY.C_22_Sal.EClassReturn.EClassShowInvoice> ShowInvInfo(string mInvoice, string Centrid, string ReturnNo)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassReturn.EClassShowInvoice> lst = new List<SPEENTITY.C_22_Sal.EClassReturn.EClassShowInvoice>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_RETURN", "GETINVOICEINFO", mInvoice, Centrid, ReturnNo, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassReturn.EClassShowInvoice ShowInv = new SPEENTITY.C_22_Sal.EClassReturn.EClassShowInvoice(dr["memono"].ToString(), dr["memono1"].ToString(), dr["sdelno"].ToString(), dr["sdelno1"].ToString(),
        //            dr["centrid"].ToString(), dr["rsircode"].ToString(), dr["rsirdesc"].ToString(), dr["batchcode"].ToString(), dr["batchdesc"].ToString(), dr["rsirunit"].ToString(),
        //            dr["custcode"].ToString(), Convert.ToDouble(dr["invqty"]), Convert.ToDouble(dr["retqty"]), Convert.ToDouble(dr["invrate"]),
        //            Convert.ToDouble(dr["invamt"]), Convert.ToDouble(dr["retamt"]), Convert.ToDouble(dr["invdis"]), Convert.ToDouble(dr["retindis"]), Convert.ToDouble(dr["invvat"]),
        //            Convert.ToDouble(dr["retvat"]), Convert.ToDouble(dr["ovdis"]), Convert.ToDouble(dr["retovdis"]), dr["remarks"].ToString(), dr["teamcode"].ToString(), dr["taxcode"].ToString(), Convert.ToDouble(dr["freeqty"]), Convert.ToDouble(dr["retfreeqty"]), Convert.ToDouble(dr["repackamt"]), Convert.ToDouble(dr["caramt"]));
        //        lst.Add(ShowInv);

        //    }

        //    return lst;
        //}

        //public List<SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet> GetPreReturn(string Centrid, string Date, string Date2)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet> lst = new List<SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_RETURN", "GETPRERETURN", Centrid, Date, Date2, "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet details = new SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet(dr["retmemo"].ToString(), dr["retmemo1"].ToString(),
        //                dr["custcode"].ToString(), dr["custdesc"].ToString(), dr["invno"].ToString(), dr["invno1"].ToString(), dr["orderno"].ToString(), dr["teamcode"].ToString(), dr["teamdesc"].ToString());
        //        lst.Add(details);
        //    }

        //    return lst;



        //}

        //public List<SPEENTITY.C_22_Sal.EClassReturn.EClassReturnNo> GetReturnNo(string Centrid, string ReturnNo)
        //{
        //    List<SPEENTITY.C_22_Sal.EClassReturn.EClassReturnNo> lst = new List<SPEENTITY.C_22_Sal.EClassReturn.EClassReturnNo>();
        //    string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALES_RETURN", "GETRETURNNO", Centrid, ReturnNo, "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        SPEENTITY.C_22_Sal.EClassReturn.EClassReturnNo details = new SPEENTITY.C_22_Sal.EClassReturn.EClassReturnNo(dr["retmemo1"].ToString(), Convert.ToDateTime(dr["retdat"].ToString()));
        //        lst.Add(details);
        //    }

        //    return lst;



        //}
       
        //#endregion


        //public DataSet GetIMEIinfo(string Centrid, string Orderno)
        //{
        //    string comcod = ObjCommon.GetCompCode();
        //    return _ProAccess.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "GETDELNOFRMIMEI", Centrid, Orderno);
        //} 

        
    }
}
