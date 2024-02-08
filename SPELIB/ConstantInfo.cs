using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SPELIB
{
    public static class ConstantInfo
    {
        public static bool LogStatus =true;
        public static DataTable WebObjTable()
        {
            DataTable tblObj = new DataTable();
            tblObj.Columns.Add("frmid1", Type.GetType("System.String"));
            tblObj.Columns.Add("frmid", Type.GetType("System.String"));
            tblObj.Columns.Add("floc", Type.GetType("System.String"));
            tblObj.Columns.Add("frmname", Type.GetType("System.String"));
            tblObj.Columns.Add("qrytype", Type.GetType("System.String"));
            tblObj.Columns.Add("dscrption", Type.GetType("System.String"));
            tblObj.Columns.Add("modulename", Type.GetType("System.String"));
            tblObj.Columns.Add("chkper", Type.GetType("System.String"));
            tblObj.Columns.Add("entry", Type.GetType("System.String"));
            tblObj.Columns.Add("printable", Type.GetType("System.String"));
            tblObj.Columns.Add("deleteCk", Type.GetType("System.String"));
            tblObj.Columns.Add("EditCk", Type.GetType("System.String"));
            tblObj.Columns.Add("qrytype1", Type.GetType("System.String"));

            DataColumn[] keys = new DataColumn[] { tblObj.Columns["frmid"] };
            tblObj.PrimaryKey = keys;

            #region General Part
            // 01. Merchandising Module
            #region
            tblObj.Rows.Add(new Object[] { "0101000", "0101001", "", "SalesCodeBook?", "Type=Sales", "Customer Information Code Book", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0101000", "0101002", "F_01_Mer", "PurCustInfo?", "Type=Entry", "Customer Details Information", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0101000", "0101003", "F_01_Mer", "PurCustInfo?", "Type=CusDetails", "Customer Details Information From Code", "Merchandising", "False", "False", "False", "False", "False", "" });

            
            //Entry Screen

            tblObj.Rows.Add(new Object[] { "0102000", "0101005", "F_01_Mer", "SampleInquiryLIst?", "Type=Sample", "Sample Inquiry List", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101006", "F_01_Mer", "SampleInquiry?", "Type=Entry", "Sample Inquiry Entry", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101007", "F_01_Mer", "SampleInquiry?", "Type=Edit", "Sample Inquiry Edit", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101008", "F_01_Mer", "ConsumptionSheet?", "Type=Entry", "Consumption Sheet Entry", "Merchandising", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101009", "F_01_Mer", "ConsumptionSheet?", "Type=PreCosting", "Pre-Costing Sheet Entry", "Merchandising", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101010", "F_01_Mer", "OrderDetails?", "Type=Entry", "Order Details Input", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101011", "F_01_Mer", "OrderDetails?", "Type=Approved", "Order Details- Approved", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101012", "F_01_Mer", "OrderDetails?", "Type=Reorder", "Entry Re-Order Details", "Merchandising", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0102000", "0101013", "F_01_Mer", "SampleInquiry?", "Type=Approv", "Sample Inquiry Approval", "Merchandising", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0102000", "0101014", "F_01_Mer", "ConsumptionSheet?", "Type=ConApp", "Consumption Sheet Approved", "Merchandising", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101015", "F_01_Mer", "ConsumptionSheet?", "Type=PreCostingApp", "Pre-Costing Sheet Approved ", "Merchandising", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101016", "F_01_Mer", "ConsMatStoring?", "Type=All", "Department Wise Material Analysis", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101018", "F_01_Mer", "MerchandProcessEdit", "", "Merchandising Process Edit", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101019", "F_01_Mer", "ConsumptionSizeAdd", "", "Consumption Material Size Assorment", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101020", "F_01_Mer", "ConsMatStoring?", "Type=Matgrp", "Material Group Wise Pre-Analysis", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101021", "F_01_Mer", "ConsMatStoring?", "Type=Brand", "Brand Wise Pre-Analysis", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101022", "F_01_Mer", "ConvertMaterialAssortment?", "Type=ConvMatAsort", "Convertable Materials Assortment", "Merchandising", "False", "False", "False", "False", "False", "&actcode=&dayid=" });
            tblObj.Rows.Add(new Object[] { "0102000", "0101023", "F_01_Mer", "ConvJobPoEntry?", "Type=Entry", "Job Work Po Entry", "Merchandising", "False", "False", "False", "False", "False", "&actcode=&dayid=" });

            

            tblObj.Rows.Add(new Object[] { "0103000", "0103004", "F_01_Mer", "RptOrdAppSheet?", "Type=BomApp", "BOM Approved List", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103005", "F_03_CostABgd", "RptLCStuatus?Type=PeriodicOrderSt", "", "Periodic Order Status", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103006", "F_01_Mer", "RptOrdAppSheet?", "Type=costdiff", "Pre Costing Vs Post Costing Report", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103007", "F_01_Mer", "RptOrdAppSheet?", "Type=PendBom", "Pending BOM List", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103008", "F_01_Mer", "SampleInquiryLIst?", "Type=Summary", "Costing Summary Report", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103009", "F_01_Mer", "OrderExSheet", "", "Order Execution Sheet", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103010", "F_01_Mer", "RptOrdAppSheet?", "Type=BuyerWiseSamp", "Buyer Wise Article List", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103011", "F_01_Mer", "RptPfiInvList?", "Type=PfiRpt", "Proforma Invoice List", "Merchandising", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0103000", "0103012", "F_03_CostABgd", "SalesContact?", "Type=Entry", "Create Proforma Invoice", "Cost & Budget", "False", "False", "False", "False", "False", "&genno=&actcode=&dayid=&sircode=" });





            tblObj.Rows.Add(new Object[] { "0151000", "0151001", "F_01_Mer", "RptMerChanInterface?", "Type=Merchan", "Merchandising", "Merchandising- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0151000", "0151002", "F_01_Mer", "RptMerChanInterface?", "Type=PD", "Product Development", "Merchandising- Smartface", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0161000", "0161001", "F_01_Mer", "OrderInformation", "", "Order & Shipment", "Merchandising- Smartboard", "False", "False", "False", "False", "False", "" });
            #endregion

            //02. Business Planning
            #region Business Planning
            //One Time Input
            tblObj.Rows.Add(new Object[] { "0201000", "0201001", "F_02_Busi", "BgdCodeBook", "", "Business Planning Code", "Business Planning", "False", "False", "False", "False", "False", "" });


            //Entry Screen
            tblObj.Rows.Add(new Object[] { "0202000", "0202001", "F_02_Busi", "YearlyPlanningBudget?", "Type=Yearly&rType=Income", "Yearly Budget", "Business Planning", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0202000", "0202002", "F_02_Busi", "YearlyPlanningBudget?", "Type=Yearly&rType=Cash", "Cash Budget", "Business Planning", "False", "False", "False", "False", "False", "" });



            //General Report
            tblObj.Rows.Add(new Object[] { "0203000", "0203001", "F_02_Busi", "YearlyPlanningBudget?", "Type=BgdAmtBasis", "Yearly Budget Amount Basis", "Business Planning", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0203000", "0203002", "F_02_Busi", "YearlyPlanningBudget?", "Type=BgdQtyBasis", "Yearly Budget Qty Basis", "Business Planning", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0203000", "0203003", "F_02_Busi", "YearlyPlanningBudget?", "Type=BgdIncome", "Yearly Budgeted Income Statement", "Business Planning", "False", "False", "False", "False", "False", "" });


            #endregion

            //03. Cost & Budget
            #region Cost & Budget


            //One Time Input
            tblObj.Rows.Add(new Object[] { "0301000", "0301001", "F_21_GAcc", "AccSubCodeBook?", "InputType=GenCode", "General Code", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0301000", "0301002", "F_21_GAcc", "AccSubCodeBook?", "InputType=ProdCode", "Product Code", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0301000", "0301003", "F_21_GAcc", "AccSubCodeBook?", "InputType=CostCode", "Cost Code", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            
            //tblObj.Rows.Add(new Object[] { "0301000", "0301005", "", "LCGenCodeBook", "", "LC Information Code", "Budget", "False", "False", "False", "False", "False", "" });


            //Entry Screen
            tblObj.Rows.Add(new Object[] { "0302000", "0302001", "F_03_CostABgd", "StdCostSheet?", "InputType=CostAnnaSemi", "Analysis Sheet", "Cost & Budget", "False", "False", "False", "False", "False", "&actcode=" });
            //tblObj.Rows.Add(new Object[] { "0302000", "0302002", "", "EntryMasterLC?", "Type=0", "LC Information", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0302000", "0302003", "", "EntryMasterLC?", "Type=1", "Order Details", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0302000", "0302004", "", "EntryMasterLC?", "Type=3", "Resource Selection", "Cost & Budget", "False","False","False", "False", "False" });
            //tblObj.Rows.Add(new Object[] { "0302000", "0302005", "", "EntryMasterLC?", "Type=2&Module=Com", "Budget Preparation", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0302000", "0302006", "", "LCInformation", "", "lC Information", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0302000", "0302007", "", "BgdMaster?", "InputType=BgdMain", "Local Purchase Budget", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0302000", "0302008", "", "MonthlySalesBudget?", "Type=Yearly", "Yearly Budget", "Cost & Budget", "False","False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0302000", "0302009", "F_03_CostABgd", "SalesContact?", "Type=Entry", "Create Proforma Invoice", "Cost & Budget", "False", "False", "False", "False", "False", "&genno=&actcode=&dayid=&sircode=" });
            tblObj.Rows.Add(new Object[] { "0302000", "0302010", "F_03_CostABgd", "SalesContact?", "Type=Edit", "Edit Proforma Invoice", "Cost & Budget", "False", "False", "False", "False", "False", "&genno=&actcode=&dayid=&sircode=" });
            
            //tblObj.Rows.Add(new Object[] { "0302000", "0302011", "", "RptLCDetailsStatus?", "Type=All", "Order List", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            
            //tblObj.Rows.Add(new Object[] { "0302000", "0302013", "", "BgdPreparation?", "Type=Entry", "Resource Requirements", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0302000", "0302014", "F_03_CostABgd", "MlcMatReq?", "Type=Entry", "Material Requirement Against Order (BOM)", "Cost & Budget", "False", "False", "False", "False", "False", "&actcode=" });

            tblObj.Rows.Add(new Object[] { "0302000", "0302016", "F_03_CostABgd", "MlcMatReq?", "Type=Approve", "Material Requirement Against Order (BOM) Approve", "Cost & Budget", "False", "False", "False", "False", "False", "&actcode=" });
            tblObj.Rows.Add(new Object[] { "0302000", "0302017", "F_03_CostABgd", "ProdBudget?", "Type=EntrySemi", "Production Budget", "Cost & Budget", "False", "False", "False", "False", "False", "" });



            //General Report
            tblObj.Rows.Add(new Object[] { "0303000","0303001", "F_03_CostABgd", "AnalysisMatList", "", "Concluded Analysis Material List", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0303000", "0303002", "", "RptMlcStatus01?", "Type=LCBBLC", " Master LC - BBLC ", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0303000", "0303003", "", "RptMlcStatus01?", "Type=LCOrderDetails", " Master LC - Order Details ", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0303000", "0303004", "", "RptMlcStatus01?", "Type=LCOrder", " Master LC Order Status", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0303000", "0303005", "", "RptMlcStatus01?", "Type=LCOverall", " Master LC Overall ", "Cost & Budget", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "0303000", "0303007", "", "MonthlySalesBudget?", "Type=BgdAmtBasis", "Yearly Budget Amount Basis", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0303000", "0303008", "", "MonthlySalesBudget?", "Type=BgdQtyBasis", "Yearly Budget Qty Basis", "Cost & Budget", "False", "False", "False", "False", "False", "" });




            //Unkown

            //tblObj.Rows.Add(new Object[] { "03090", "MslcGInf?", "Type=Lease", "Entry LC Lease Information", "Commercial", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "03091", "MslcGInf?", "Type=Exfac", "Entry Shipline IProduction And Shipmentnformation", "Commercial", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "03100", "MlcStausRpt?", "Type=LCStatus", " Master LC Order Status", "Commercial", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "03180", "RptExportStatus?", "Type=Entry", "Entry Purchase Projection", "Commercial", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "03200", "MlcStausRpt?", "Type=YrnPurchase", "Yarn Purchase", "Cost & Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "03210", "PCOption", "", "Entry PC Option", "Cost & Budget", "False", "False", "False", "False", "False", "" });


            #endregion

            //04. Sample Development
            #region Sample Development
            tblObj.Rows.Add(new Object[] { "0402000", "0402001", "F_04_Sampling", "SamSampleInquiry?", "Type=Entry", "Sample Development Inquiry", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0451000", "0451001", "F_04_Sampling", "SamplingInterface?", "Type=Entry", "Sample Development & CBD Smartface", "Sampling", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0402000", "0402010", "F_04_Sampling", "SamConsumptionSheet?", "Type=Entry", "Consumption Sheet Entry", "Sampling", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0402000", "04020", "F_04_Sampling", "SamConsumptionSheet?", "Type=PGEntry", "Consumption Sheet Entry(PD Guide)", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0402000", "0402020", "F_04_Sampling", "SamConsumptionSheet?", "Type=PreCosting", "Pre-Costing Sheet Entry", "Sampling", "False", "False", "False", "False", "False", "" });          

            tblObj.Rows.Add(new Object[] { "0402000", "0402030", "F_04_Sampling", "SamConsumptionSheet?", "Type=ConApp", "Consumption Sheet Approved", "Sampling", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0402000", "0402035", "F_04_Sampling", "SamConsumptionSheet?", "Type=PGApp", "Consumption Sheet Approved(PD Guide)", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0402000", "0402040", "F_04_Sampling", "SamConsumptionSheet?", "Type=PreCostingApp", "Pre-Costing Sheet Approved ", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0403000", "0403043", "F_04_Sampling", "RptPdBook?", "Type=PdBook", "PD Book Report", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0403000", "0403044", "F_04_Sampling", "RptPdBook?", "Type=SamReport", "Date Wise Sample Report", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0403000", "0403045", "F_04_Sampling", "SamTagPrint?", "Type=TagPrint", "Sample Tag Print", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0403000", "0403046", "F_04_Sampling", "RptPdBook?", "Type=PdBookEntry", "PD Book Information Entry", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0403000", "0403047", "F_04_Sampling", "RptPdBook?", "Type=KnChkList", "Knife Check List", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0403000", "0403048", "F_04_Sampling", "SamTagPrint?", "Type=PackingList", "Sample Packing List", "Sampling", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0403000", "0403049", "F_01_Mer", "SampleInquiryLIst?", "Type=SummaryForPD", "All Article Wise Report", "Sampling", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0402000", "0402041", "F_04_Sampling", "SamSampleInquiry?", "Type=Edit", "Sample Development Inquiry", "Settings", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0402000", "0402042", "F_04_Sampling", "SamSampleInquiry?", "Type=Approv", "Sample Development Inquiry(Approved)", "Settings", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "0402000", "0402043", "F_04_Sampling", "DepartmentAlocation?", "Type=DeptAloc", "Department Allocation", "Sampling", "False", "False", "False", "False", "False", "" });
            
            #endregion

            //05. Production And Shipment
            #region
            //One Time Input

            tblObj.Rows.Add(new Object[] { "0501000","0501001","", "GeneralCodeBook?", "Type=All", "Planning Code Book", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0501000","0501002", "", "RptLCStuatus?", "Type=OrdrArchive", "Periodic Order Archive Status", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0501000","0501003", "F_05_ProShip", "MasterCalendarSetup?", "Type=mstrcalendar", "Master Calendar Setup", "Plannig", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0501000", "0501004", "F_05_ProShip", "MasterCalendarSetup?", "Type=plancalendar", "Production Planning Calendar Setup", "Plannig", "False", "False", "False", "False", "False", "" });


            //Entry Screen
            tblObj.Rows.Add(new Object[] { "0502000", "0502001", "F_05_ProShip", "LCPlanInformation?", "Type=ArtCapacity", "Article Capacity Plan", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0502000", "0502002", "F_05_ProShip", "ExportPlanVsAchiv?", "Type=Entry", "Production & Shipment Plan", "Production & Shipment", "False", "False", "False", "False", "False", "&actcode=&sircode=" });
            tblObj.Rows.Add(new Object[] { "0502000", "0502003", "F_05_ProShip", "ProductionPlan?", "Type=Entry", "Daywise Production Plan", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0502000", "0502004", "F_05_ProShip", "ArticleWiseLot?", "Type=Entry", "Article Wise Lot ", "Production & Shipment", "False", "False", "False", "False", "False", "genno=&actcode=&dayid=" });
            tblObj.Rows.Add(new Object[] { "0502000", "0502005", "F_05_ProShip", "ProcessBasePlan?", "Type=Entry", "Process Base Production Plan", "Production & Shipment", "False", "False", "False", "False", "False", "" });
           


            // General Report

            tblObj.Rows.Add(new Object[] { "0503000", "0503001", "", "RptExPlanAchiv", "", "Plan Vs Achievement - Individual Order(Details)", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503002", "", "RptExPlanAchivAll", "", "Plan Vs Achievement - All Order", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503003", "", "RptExPlVsAchivSumm", "", "Plan Vs Achievement - Individual Order(Summary)", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503004", "", "RptCriticalorder", "", "Critical Path Of Order", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503005", "F_05_ProShip", "OrderSheetPlan?", "Type=Entry", "Order Sheet Plan", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503006", "F_01_Mer", "RptOrdAppSheet?", "Type=OrdPlan", "Order Plan Report", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503007", "F_05_ProShip", "RptOrderStatus?", "Type=OrdStatus", "Order Status Report", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503009", "F_05_ProShip", "RptOrderStatus?", "Type=MatMaster", "Material Master Report", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0503000", "0503010", "F_05_ProShip", "RptProcessBasePlan?", "Type=Daywise", "Day Wise Process Base Plan", "Production & Shipment", "False", "False", "False", "False", "False", "" });
       




            #endregion


            //06. Industrial Enginnering
            #region
            //One Time Input

            //tblObj.Rows.Add(new Object[] { "0501000", "0501001", "", "GeneralCodeBook?", "Type=All", "Planning Code Book", "Production & Shipment", "False", "False", "False", "False", "False", "" });
         

            //Entry Screen
            tblObj.Rows.Add(new Object[] { "0602000", "0602001", "F_05_ProShip", "LCPlanInformation?", "Type=ArtCapacity", "Article Capacity Plan", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0602000", "0602002", "F_05_ProShip", "LCPlanInformation?", "Type=ArtEffiency", "Article Efficiency Setup", "Plannig", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0602000", "0602003", "F_05_ProShip", "LCPlanInformation?", "Type=SmvCalculation", "Article SMV Calculation", "Plannig", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0602000", "0602004", "F_05_ProShip", "RptSkilMatrix?", "Type=WrkWisCapMon", "Work Wise Capacity Monitoring", "Production & Shipment", "False", "False", "False", "False", "False", "" });

            // General Report

            tblObj.Rows.Add(new Object[] { "0603000", "0603001", "F_05_ProShip", "RptProcessBasePlan?", "Type=ArtclLrnCurv", "Article Learning Curve Report", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0603000", "0603002", "F_05_ProShip", "RptOrderStatus?", "Type=SMVsheet", "Article Wise Summary Sheet", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0603000", "0603003", "F_05_ProShip", "RptSkilMatrix?", "Type=SkilMatrix", "Employee Skill Matrix Report", "Production & Shipment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0603000", "0603004", "F_05_ProShip", "LCPlanInformation?", "Type=ArtLayout", "Article Wise Layout Paper", "Plannig", "False", "False", "False", "False", "False", "" });           
            tblObj.Rows.Add(new Object[] { "0603000", "0603005", "F_05_ProShip", "RptSkilMatrix?", "Type=RptWrkWisCapMon", "Work Wise Capacity Monitoring Report", "Production & Shipment", "False", "False", "False", "False", "False", "" });





            #endregion

            //07. Finance Module

            #region ImportModule


            //Entry Screen

            tblObj.Rows.Add(new Object[] { "0702000","0702001","", "FinanceBgdEntry?", "Type=ProRevenue", "Sales Budget Entry", "Import", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0702000", "0702002", "", "FinanceBgdEntry?", "Type=ProCost", "Cost Budget Entry", "Import", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0702000", "0702003", "", "FinanceBgdEntry?", "Type=ProEquity", "Equity Budget Entry", "Import", "False", "False", "False", "False", "False", "" });


            //General Report

            //tblObj.Rows.Add(new Object[] { "0703000", "0703001", "", "","", "Sales Budget Reoprt", "Import",  "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0703000", "0703002", "", "","", "Cost Budget Reoprt", "Import",  "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0703000", "0703003", "", "","", "Variance with Actual Reoprt", "Import", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0703000", "0703004", "", "EntryYearlySalAndColl", "", "Expected Collection Next 12 Month", "Import", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0703000", "0703005", "", "RptMonWiseBBLCPay?", "Type=BBLCPay", "Cash Outflow BBLC Status", "Import", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0703000", "0703006", "", "RptMonWiseBBLCPay?", "Type=InvColl", "Cash Infow Export Status", "Import", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0703000", "0703007", "", "RptMonWiseBBLCPay?", "Type=CollVsPay", "Cash Flow- Export Vs BBLC", "Import", "False", "False", "False", "False", "False", "" });

            #endregion

            //09. Commercial
            #region

            //One Time Input
            tblObj.Rows.Add(new Object[] { "0901000", "0901001", "", "SalesCodeBook?", "Type=Procurement", "Supplier Information Field ", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0901000", "0901002", "", "PurSupplierinfo?", "sircode=", "Supplier Information", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0901000", "0901003", "F_21_GAcc", "AccSubCodeBook?", "InputType=BBLCCode", "BBLC Opening Code", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0901000", "0901004", "F_09_Commer", "PurSupplierinfo?", "Type=SupDetails", "Supplier Information From Details Code", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0901000", "0901005", "F_21_GAcc", "AccSubCodeBook?", "InputType=SupplierCode", "Supplier Code", "Commercial", "False", "False", "False", "False", "False", "" });



            //Entry Screen

            //tblObj.Rows.Add(new Object[] { "0902000", "0902001", "", "BBLCInfo?", "InputType=Entry", "BBLC Information", "Commercial", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "0902000", "0902002", "F_09_Commer", "PurWrkOrderEntry?", "InputType=OrderEntry", "Work Order", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902003", "F_09_Commer", "PurBillEntry", "", "Bill Confirmation", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902004", "F_09_Commer", "LcReceive?", "Type=Entry", "L/C Receive Form", "Commercial", "False", "False", "False", "False", "False", "&comcod=&actcode=&centrid=&genno=" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902005", "F_09_Commer", "LcReceive?", "Type=Edit", "L/C Receive Edit Form", "Commercial", "False", "False", "False", "False", "False", "&comcod=&actcode=&centrid=&genno=" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902006", "F_09_Commer", "LcQcRecv?", "Type=Entry", "L/C Qc Form Entry", "Commercial", "False", "False", "False", "False", "False", "&comcod=&actcode=&centrid=&genno=" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902007", "F_09_Commer", "LcQcRecv?", "Type=Edit", "L/C Qc Form Edit", "Commercial", "False", "False", "False", "False", "False", "&comcod=&actcode=&centrid=&genno=" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902008", "F_09_Commer", "LCCostingDetails?", "Type=Entry", "L/C Costing Form", "Commercial", "False", "False", "False", "False", "False", "&comcod=&actcode=" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902009", "F_09_Commer", "LcQcRecv?", "Type=Approve", "L/C Qc Form Approve", "Commercial", "False", "False", "False", "False", "False", "&comcod=&actcode=&centrid=&genno=" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902010", "F_13_CWare", "PurReqEntry02?", "InputType=LCEntry", "Create Import Requisition", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902011", "F_09_Commer", "LCAllInfo?", "Type=All", "All L/C Opening", "Import Procurement", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902012", "F_09_Commer", "LCOpening?", "Type=Open", "L/C Details Information", "Import Procurement", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0902000", "0902013", "F_09_Commer", "RptLCStatus?", "Type=LCCostingPreset", "L/C Pre Costing Entry", "Import Procurement", "False", "False", "False", "False", "False", "&actcode=" });




            //General Report
            tblObj.Rows.Add(new Object[] { "0903000", "0903001","",   "RptOrderStatus?", "Type=OrderWise", "Order Wise Supply - Summary", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903002", "", "RptOrderWiseSupDet", "", "Order Wise Supply - Details", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903003", "", "RptOrderStatus?", "Type=SupWise", "Supplier Wise BBLC Position", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903004", "F_09_Commer", "RptOrderVsReceive?", "Type=Req", "Requistion Vs. Received", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903005", "F_09_Commer", "RptPurchaseStatus?", "Type=Purchase&Rpt=Purchasetrk", "Purchase Tracking", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903006", "", "RptDateWiseReq?", "Type=PeriodPurchase", "Periodic Purchase Tracking", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903007", "", "RptDateWiseReq?", "Type=PendingStatus", "Pending Status", "Commercial", "False", "False", "False", "False", "False", "" });

           


            tblObj.Rows.Add(new Object[] { "0903000", "0903008", "F_09_Commer", "RptPurchaseStatus?", "Type=Purchase&Rpt=DaywPur", "Day Wise Purchase", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903009", "F_09_Commer", "RptPurchaseStatus?", "Type=Purchase&Rpt=PurSum", "Purchase Summary", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903010", "", "BillTracking", "", "Bill Tracking", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903011", "F_09_Commer", "RptWorkOrderVsSupply?", "Type=OrdVsSup", "Work Order-Supplier Wise", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903012", "F_09_Commer", "RptOrderVsReceive?", "Type=OrderVsRec", "BOM Vs. Received", "Commercial", "False", "False", "False", "False", "False", "" });

            

            tblObj.Rows.Add(new Object[] { "0903000", "0903013", "", "RptLCStatus?", "Type=LCCosting", "L/C Costing Report", "Import Procurement", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903014", "", "RptLCPosition?", "Type=LCPosition", "L/C Status Reports", "Import Procurement", "False", "False", "False", "False" });

            tblObj.Rows.Add(new Object[] { "0903000", "0903015", "", "RptSalSummery?", "Type=LcCost", "L/C Overall Cost", "Import Procurement", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903016", "", "RptLCStatus?", "Type=LCVari", "L/C Variance Reports", "Import Procurement", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903017", "", "RptSalSummery?", "Type=LcReceive", "L/C Receive Report", "Import Procurement", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903018", "", "RptPurchaseStatus?", "Type=Purchase&Rpt=IndSup", "Purchase History-Supplier Wise", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903019", "", "RptLCStatus?", "Type=LCRecvCon", "L/C Receive Consignment wise", "Import Procurement", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903020", "F_09_Commer", "RptWorkOrderVsSupply?", "Type=SeasonSummary", "Season Wise Supply Summary", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903021", "F_09_Commer", "RptSeasonWiseOrder?", "Type=SeasonBySeason", "Season by Season Supplier's Summary", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903022", "F_09_Commer", "RptWorkOrderVsSupply?", "Type=LeadTime", "Raw Materials Supply Lead Time", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903023", "F_09_Commer", "RptSeasonWiseOrder?", "Type=SeasonOverview", "Season Overview Of Materials", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903024", "F_09_Commer", "RptSeasonWiseOrder?", "Type=PriceVariance", "Material Price Variance Report", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903025", "F_09_Commer", "RptOrderVsReceive?", "Type=BomMatSummary", "BOM Wise Materials Summary", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "0903000", "0903026", "F_09_Commer", "RptOrderVsReceive?", "Type=SesonWiseBom", "Season Wise BOM Status", "Commercial", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "0951000", "0951001", "F_09_Commer", "RptLCInterface", "", "Import/Foreign", "Commercial- Smartface", "False", "False", "False", "False", "False", "" });

            //Unkown

            //tblObj.Rows.Add(new Object[] { "09020", "MlcStausRpt?", "Type=LCMarge", "LC Marge Status", "Commercial", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "09130", "RptWorkOrderVsSupply?", "Type=OrdVsSup", "Work Order-Supplier Wise", "Procurement", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "09131", "RptWorkOrderVsSupply?", "Type=OrderTk", "Order Tracking", "Procurement", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "09150", "RptSupListWithMat", "", "Supplier List with Materials", "Procurement", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "09190", "RptWorkOrderStatus?", "Type=WorkIOrdStatus", "Work Order Status", "Procurement", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "09191", "RptWorkOrderStatus?", "Type=DetailsWorkIOrdStatus", "Work Order Details", "Procurement", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "09202", "RptWorkOrderVsSupply?", "Type=OrderTk", "Order Tracking", "Procurement", "False", "False", "False", "False", "False", "" });



            //tblObj.Rows.Add(new Object[] { "03050", "ReportMLCProduction?", "Type=ProStatus", "Production Status", "Production", "False" ,"False","False"});
            //tblObj.Rows.Add(new Object[] { "03051", "ReportMLCProduction?", "Type=ProVsStock", "Production Vs Stock", "Production", "False" ,"False","False"});
            //tblObj.Rows.Add(new Object[] { "03070", "RptOrdrStk", "", "Order Stock Status", "Production", "False","False","False" });
            //tblObj.Rows.Add(new Object[] { "03080", "ProductionProcess?", "Type=ProStart", "Production Process- Start", "Production", "False","False","False" });
            //tblObj.Rows.Add(new Object[] { "03081", "ProductionProcess?", "Type=ProTransfer", "Production  Process", "Production", "False","False","False" });
            //tblObj.Rows.Add(new Object[] { "03082", "ProductionProcess?", "Type=ProProcess", "Production  Process Report", "Production", "False" ,"False","False"});
            

            #endregion

            //10. Procurement

            #region

             
            //One Time Input
            tblObj.Rows.Add(new Object[] { "1001000", "1001001", "F_13_CWare", "StoreInformation", "", "Store Information Setup", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1001000", "1001002", "F_09_Commer", "PurMktSurvey?", "Type=SurveyLink", "Survey Link", "Procurement", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "1001000", "1001003", "F_13_CWare", "PRCodeBook", "", "Store Information Code", "Procurement", "False", "False", "False", "False", "False", "" });


            //tblObj.Rows.Add(new Object[] { "1001000", "1001004", "", "PurMktSurvey02?", "Type=Approved", "Comparative Statement- Approval", "Procurement", "False", "False", "False", "False", "False", "" });


            //Entry Screen

            tblObj.Rows.Add(new Object[] { "1002000", "1002003", "F_10_Procur", "PurMktSurvey02?", "Type=Create", "Comparative Statement-Create", "Local Procurement-Operations", "False", "False", "False", "False", "False", "&comcod=&genno=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002004", "F_10_Procur", "PurMktSurvey02?", "Type=Check", "Comparative Statement-Check", "Local Procurement-Operations", "False", "False", "False", "False", "False", "&comcod=&genno=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002005", "F_10_Procur", "PurMktSurvey02?", "Type=Approved", "Comparative Statement- Approval", "Local Procurement-Operations", "False", "False", "False", "False", "False", "&comcod=&genno=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002006", "F_10_Procur", "PurMktSurvey02?", "Type=Audit", "Comparative Statement- Audit Check", "Local Procurement-Operations", "False", "False", "False", "False", "False", "&comcod=&genno=" });

            //"01. Store Requisition", "F_13_CWare/PurReqEntry02?InputType=FxtAstEntry", "", true, "" });

            tblObj.Rows.Add(new Object[] { "1002000", "1002007", "F_13_CWare", "PurReqEntry02?", "InputType=FxtAstEntry", "Create Requisition", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002008", "F_13_CWare", "PurReqEntry02?", "InputType=MoldReqEntry", "Mold Requisition Entry", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            //tblObj.Rows.Add(new Object[] { "1002000", "1002008", "", "PurAprovEntry?", "InputType=PurProposal", "Order Process (Local Purchase)", "Procurement", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "1002000", "1002009", "", "PurAprovEntry?", "InputType=PurApproval", "Order Approval (Local Purchase)", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002010", "", "PurWrkOrderEntryL?", "InputType=OrderEntry", "Purchase Order", "Procurement", "False", "False", "False", "False", "False", "&genno=&actcode=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002011", "", "PurWrkOrderEntryL?", "InputType=OrderApprove", "Purchase Order Approval", "Procurement", "False", "False", "False", "False", "False", "&genno=&actcode=" });

            //tblObj.Rows.Add(new Object[] { "1002000", "1002011", "", "PurMRREntryLocal?", "Type=Entry&actcode=&genno=", "Materials Receive (Local Purchase)", "Procurement", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "1002000", "1002012", "", "PurBillEntryLocal?", "Type=BillEntry", "Bill Confirmation (Local Purchase)", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002013", "", "PurOpenigBill?", "Type=Entry", "Supplier Opening Bill", "Local Procurement-Operations", "False", "False", "False", "False", "False", "&comcod=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002014", "F_10_Procur", "ImportApproval?", "Type=Approved", "Import Approval Approved", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002015", "F_10_Procur", "ImportApproval?", "Type=Entry", "Import Approval Entry", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002016", "F_13_CWare", "PurReqEntry02?", "InputType=FxtAstAuth", "Requisition Checked", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002017", "F_13_CWare", "PurReqEntry02?", "InputType=ReqReview", "Requisition Review", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002018", "F_13_CWare", "PurProcessEdit", "", "Purchase Force Edit", "Procurement", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "1002000", "1002019", "F_10_Procur", "ReqAdjstmntList?", "Type=ReqAdjst", "Requisition Adjustment List", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002020", "F_13_CWare", "PurReqEntry02?", "InputType=ReqEdit", "Requisition Edit", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            
            tblObj.Rows.Add(new Object[] { "1002000", "1002021", "F_10_Procur", "PurchaseReturn?", "Type=Entry", "Purchase Return Entry", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1002000", "1002022", "F_10_Procur", "PurReturnList?", "Type=RetList", "Purchase Return List", "Procurement", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });


            //General Report


            //tblObj.Rows.Add(new Object[] { "1003000", "1003002", "", "RptPurInterface", "", "Purchase Interface Foreign", "Procurement", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "1003000", "1003004", "F_10_Procur", "RptMatPurHistory", "", "Purchase History-Material Wise", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1003000", "1003005", "F_10_Procur", "RptSupplierDueStatus?", "Type=SupOutStan", "Supplier Outstanding Statement", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1063000", "1003006", "F_10_Procur", "RptMataWisePO?", "Type=MatWisePO", "Materials Wise PO Report", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1063000", "1003007", "F_10_Procur", "RptMataWisePO?", "Type=WeeklyPlanWiseMat", "Weekly Plan Wise Material Report", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1063000", "1003008", "F_09_Commer", "RptSeasonWiseOrder?", "Type=MasterPOR", "Master PO Report", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1063000", "1003009", "F_10_Procur", "RptSupplierDueStatus?", "Type=IQCInspection", "IQC Inspection Report", "Procurement", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "1051000", "1051001", "F_10_Procur", "RptPurInterfaceLocal", "", "Local Purchase", "Procurement- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1061000", "1061001", "F_10_Procur", "PurInformation", "", "Purchase & Payment", "Procurement-Smartboard", "False", "False", "False", "False", "False", "" });

            #endregion


            // 11. Raw Material Inventory

            #region
            //One Time Input
            tblObj.Rows.Add(new Object[] { "1101000", "1101001", "F_11_RawInv", "MiniStockInput?", "Type=Entry", "Stock Label Input- Material", "Raw materials inventory", "False", "False", "False", "False" });
            

            //Entry Screen
            tblObj.Rows.Add(new Object[] { "1102000", "1102001", "F_15_Pro", "PurMRREntry?", "Type=Entry", "Materials Receive", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102002", "F_11_RawInv", "PBMatIssueSingle?", "Type=Entry", "Material Issue-Auto", "Inventory", "False", "False", "False", "False", "False", "&genno=&actcode=&reptype=NORMAL" });
            
            tblObj.Rows.Add(new Object[] { "1102000", "1102003", "F_11_RawInv", "EntryMatIssue", "", "Material Issue", "Inventory", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "1102000", "1102004", "F_11_RawInv", "SamplingMatIssue?", "Type=Entry", "Material Issue(Sampling)", "Inventory", "False", "False", "False", "False", "False", "" });

            // tblObj.Rows.Add(new Object[] { "1102000", "1102004", "", "", "", "", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102005", "F_10_Procur", "PurMRQCEntry?", "Type=Entry", "Materials QC (Local Purchase)", "Procurement", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102006", "F_10_Procur", "PurMRQCEntry?", "Type=Edit", "Materials QC (Local Purchase) Edit", "Procurement", "False", "False", "False", "False", "False", "&actcode=&genno=" });



            tblObj.Rows.Add(new Object[] { "1102000", "1102007", "F_11_RawInv", "AllIndentIsuList?", "Type=Entry", "Common Material Issue List", "Raw materials inventory", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102023", "F_11_RawInv", "PurInterComMatTransfer", "", "Inter Company Material Transfer", "Raw materials inventory", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102008", "F_11_RawInv", "Material_Issue?", "Type=Entry", "Common Material Issue", "Raw materials inventory", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102009", "F_11_RawInv", "Material_Issue?", "Type=Approve", "Common Material Issue Approve", "Raw materials inventory", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102025", "F_11_RawInv", "Material_Issue?", "Type=Audit", "Common Material Issue Audit", "Raw materials inventory", "False", "False", "False", "False" });

            tblObj.Rows.Add(new Object[] { "1102000", "1102010", "F_10_Procur", "PurMRQCEntry?", "Type=Approve", "Materials QC (Local Purchase) Approve", "Raw materials inventory", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102011", "F_15_Pro", "ProdPlanTopSheet?", "Type=ManProdRM", "Manual Production Material Issue List", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102012", "F_15_Pro", "ProductionManually?", "Type=EntryRM", "Production Material Entry Manually", "Production", "False", "False", "False", "False", "False", "&genno=" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102013", "F_15_Pro", "ProductionManually?", "Type=ApproveRM", "Production Material Approve Manually", "Production", "False", "False", "False", "False", "False", "&genno=" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102014", "F_11_RawInv", "StockAdjstmnt?", "Type=Entry", "Material Stock Adjustment Entry", "Raw materials inventory", "False", "False", "False", "False", "False", "&centrid=&batchcode=&date=" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102015", "F_11_RawInv", "StockAdjstmnt?", "Type=Approve", "Material Stock Adjustment Approve", "Raw materials inventory", "False", "False", "False", "False", "False", "&centrid=&batchcode=&date=" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102016", "F_15_Pro", "ProdPlanTopSheet?", "Type=MatStockAdj", "Material Stock Adjustment List", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102017", "F_11_RawInv", "StoreMtTrnsReqEntry?", "Type=Entry", " Materials Transfer  ", "Raw Materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102018", "F_11_RawInv", "StoreMtTrnsReqEntry?", "Type=approve", "Materials Transfer Requisition Approve ", "Raw Materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102019", "F_11_RawInv", "StoreMtTrnsReqEntry?", "Type=Edit", "Materials Transfer Requisition Edit ", "Raw materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102020", "F_11_RawInv", "StoreMtTrnsReqEntry?", "Type=LoanEntry", " Materials Loan Requisition  ", "Raw Materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102021", "F_11_RawInv", "StoreMtTrnsReqEntry?", "Type=RetEntry", " Materials Return Requisition ", "Raw Materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102022", "F_11_RawInv", "MaterialIssueSuplierWise?", "Type=Entry", "Material Issue Supplier Wise ", "Raw Materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1102000", "1102024", "F_11_RawInv", "StoreMtTrnsReqEntry?", "Type=JobTrans", "Job Work Mat. Trans", "Raw Materials inventory", "False", "False", "False", "False", "False", "" });

            //General Report
            tblObj.Rows.Add(new Object[] { "1103000", "1103001", "F_11_RawInv", "InvReport?", "InputType=General", "Inventory Report - General", "Raw materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103002", "F_11_RawInv", "InvReport?", "InputType=QuantityB", "Inventory Report - Quantity Basis", "Raw materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103003", "F_11_RawInv", "InvReport?", "InputType=AmountB", "Inventory Report - Amount Basis", "Raw materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103004", "F_10_Procur", "RptSupplierDueStatus?", "Type=PromMatHis", "Indent Materials Distribution", "Raw materials inventory", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103005", "F_11_RawInv", "RptIndProStock?", "Type=MatHis", "Materilas History", "Raw materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103006", "F_11_RawInv", "MaterialsTransfer?", "Type=Material", "Material Transfer", "Raw materials inventory- Smartface", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "1151000", "1151001", "F_11_RawInv", "RptWareHouseInterface", "", "Warehouse", "Raw materials inventory- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103007", "F_11_RawInv", "InvReport?", "InputType=MatUnused", "Periodic Material Unused Report", "Raw materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103008", "F_11_RawInv", "InvReport?", "InputType=OrdwiseStk", "Article Wise Material Stock", "Raw materials inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1103000", "1103010", "F_11_RawInv", "RptMaterialTrans?", "Type=MatTransfer", "Materials Transfer Report", "Raw materials inventory", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "1151000", "1151009", "F_11_RawInv", "RawMattInterface", "", "Materials Transfer Smartface", "Raw materials inventory", "False", "False", "False", "False" });

            #endregion

            //13. Central Warehouse
            #region
            //tblObj.Rows.Add(new Object[] { "13010", "PurReqEntry?", "InputType=FxtAstEntry", "Store Requisition", "Central Warehouse", "False","False","False" });

            //tblObj.Rows.Add(new Object[] { "13010", "PurReqEntry02?", "InputType=FxtAstEntry", "Store Requisition", "Central Warehouse", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "13020", "CentralStore", "", "Materials Store (Central)", "Central Warehouse", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "13030", "MatTransfer02", "", "Material Issue", "Central Warehouse", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "1361000", "1361001", "F_13_CWare", "QCDashboard", "", "Quality Smartboard", "Management", "False", "False", "False", "False", "False", "" });


            #endregion
            //15. Production Monitoring
            #region

            //Entry Screen

            tblObj.Rows.Add(new Object[] { "1502000", "1502001", "F_15_Pro", "ProductionProcess?", "Type=ProStart", "Production Process- Start", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=&sircode=&date=" });
            //tblObj.Rows.Add(new Object[] { "1502000", "1502002", "", "EntryProTarget", "", "Production Target", "Production", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "1502000", "1502003", "", "EntryProduction", "", "Production Entry", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502004", "F_15_Pro", "ProdReq?", "Type=Entry", "Production Requisition", "Production", "False", "False", "False", "False", "False", "&actcode=" });

            //tblObj.Rows.Add(new Object[] { "1502000", "1502005", "", "EmpProdProcess", "", "Process Wise Emp Setup", "Production", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "1502000", "1502006", "", "EntryDailyProduction?", "Type=Entry", "Hourly Production Entry", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502007", "F_15_Pro", "ProdEntry?", "Type=Entry", "Goods Received Entry", "Production", "False", "False", "False", "False", "False", "&actcode=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502008", "F_15_Pro", "ProdPlanTopSheet?", "Type=PlanNo", "Production Plan Top Sheet-1", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502009", "F_15_Pro", "AddProdReq?", "Type=addreq", "Production Additional Requisition", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=&dayid=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502010", "F_15_Pro", "ProdReqSemi?", "Type=EntrySemi", "Production Requisition Semi", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502011", "F_15_Pro", "PBMatIssueSemi?", "Type=EntrySemi", "Material Issue For Semi Production", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502012", "F_15_Pro", "ProductionPlan?", "Type=EntrySemi", "Semi Production Entry", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502013", "F_15_Pro", "ProQCEntry?", "Type=EntrySemi", "Production QC Entry", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502014", "F_15_Pro", "ProStoreReceive?", "Type=EntrySemi", "Production Store Receive- Semi FG", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502015", "F_15_Pro", "ProStoreReceive?", "Type=EntrySemiRej", "Store Received Entry(Rejection)- Semi FG", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502016", "F_15_Pro", "ReProductionEntry?", "Type=EntrySemi", "Re-Production Entry (Semi)", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502018", "F_15_Pro", "MatAvailability?", "Type=SemiFG", "Materials Availability-SemiFG", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502019", "F_15_Pro", "MatAvailabilityFG?", "Type=FG", "Materials Availability-FG", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502020", "F_15_Pro", "MatRetInWIP?", "Type=Entry", "Materials Return in WIP Entry", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502021", "F_15_Pro", "MatRetInWIP?", "Type=Approved", "Materials Return in WIP Approved", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502022", "F_15_Pro", "ProdPlanTopSheet?", "Type=Datewise", "Production Plan Top Sheet-2", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502023", "F_15_Pro", "ProductionProcess?", "Type=ProQc", "Production IQC Check", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=&sircode=&date=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502024", "F_15_Pro", "ProductionManually?", "Type=Entry", "Production Entry Manually", "Production", "False", "False", "False", "False", "False", "&genno=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502025", "F_15_Pro", "ProductionManually?", "Type=Approve", "Production Manually Approve", "Production", "False", "False", "False", "False", "False", "&genno=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502026", "F_15_Pro", "ProdPlanTopSheet?", "Type=ManProd", "Manual Production Topsheet", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502027", "F_15_Pro", "ProProcessEdit", "", "Production Process Edit", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502028", "F_15_Pro", "AddProdReq?", "Type=commonreq", "Common Material Requisition", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=&dayid=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502029", "F_15_Pro", "FloorMatIssue?", "Type=Entry", "Floor Material Issue Entry", "Production", "False", "False", "False", "False", "False", "&actcode=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502030", "F_15_Pro", "ProdEntry?", "Type=Edit", "Goods Received Edit", "Production", "False", "False", "False", "False", "False", "&actcode=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502031", "F_15_Pro", "ProcWiseProd", "", "Process Wise Product", "Production", "False", "False", "False", "False", "False", "&actcode=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502032", "F_15_Pro", "ProductionManually?", "Type=FGAdjst", "Finish Goods Adjustment", "Production", "False", "False", "False", "False", "False", "&genno=" });
            tblObj.Rows.Add(new Object[] { "1502000", "1502033", "F_15_Pro", "AddProdReq?", "Type=ReCutMatReq", "Recutting Additional Material Requisition", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=&dayid=" });



            //General Report
            tblObj.Rows.Add(new Object[] { "1503000", "1503001","", "RptProtarVsAchieve?", "Type=IndProduc", "Production Status(Order Wise)", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503002", "", "RptProtarVsAchieve?", "Type=RptAllPro", "Production Status-All(Order Wise)", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503003", "", "RptProtarVsAchieve?", "Type=Protvach", "Daily Line Wise Production Report", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503004", "", "RptOrdrStk", "", "Order Stock Status", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503005", "F_15_Pro", "ProductionProcess?", "Type=ProTransfer", "Production  Process (Individual Order)", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=&sircode=&date=" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503006", "F_15_Pro", "ProductionProcess?", "Type=ProProcess", "Production  Process (All Order)", "Production", "False", "False", "False", "False", "False", "&actcode=&genno=&sircode=&date=" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503007", "", "RptOProVsShip?", "Type=ProVsCons&Module=Com", "Production Vs Consumption", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503008", "F_03_CostABgd", "RptLCStuatus?Type=PeriodicProdSt", "", "Periodic Production Status", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503009", "", "RptOrderProShipAll", "", "Order, Production Shipment - All Orders", "Production", "False", "False", "False", "False", "False", "" });
            
            tblObj.Rows.Add(new Object[] { "1503000", "1503011", "", "RptProdProcess", "", "Production  Process (All Order)", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503012", "F_15_Pro", "RptProductionConsumption?", "Type=Daywise", "Day Wise Material Consumption Report", "Production", "False", "False", "False", "False", "False", "" });



            tblObj.Rows.Add(new Object[] { "1551000", "1551001", "F_15_Pro", "ProductionInterfaceSemi?", "Type=SemiFG", "Production Semi", "Production- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1551000", "1551002", "F_15_Pro", "ProductionInterface?", "Type=FG", "Production", "Production- Smartface", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "1561000", "1561002", "F_15_Pro", "ProductionInfo", "", "Production", "Production-Smartboard", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503013", "F_15_Pro", "DateWiseMatIssue?", "Type=DateWise", "Date Wise Material Issue", "Production- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503014", "F_15_Pro", "RptProduction?", "Type=BalSheet", "Daily Production Balance Sheet", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503015", "F_15_Pro", "RptProduction?", "Type=QltyNdPrd", "Daily Quality And Productivity Report", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503016", "F_15_Pro", "RptProduction?", "Type=ProductionReport", "Monthly Production Analytical Report", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503017", "F_15_Pro", "RptProduction?", "Type=SizeBalSheet", "Size Production Balance Sheet", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503018", "F_15_Pro", "RptProduction?", "Type=DefParChart", "Defect Pareto Chart", "Production", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1503000", "1503019", "F_15_Pro", "RptProduction?", "Type=OrderDefect", "Order-Defect Reject/Repair Report", "Production", "False", "False", "False", "False", "False", "" });
            //Unkown

            //mnuTbl1.Rows.Add(new Object[] { "0327000000", "24. Process Wise Emp Setup", "F_15_Pro/EmpProdProcess", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0328000000", "25. Hourly Production Entry", "F_15_Pro/EntryDailyProduction?Type=Entry", "", true, "" });



            //tblObj.Rows.Add(new Object[] { "15010", "ReportMLCProduction?", "Type=ProStatus", "Production Status", "Production", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "15040", "RptProdProcess", "", "Production  Tracking", "Production", "False", "False", "False", "False", "False", "" });


            #endregion

            // 17 Finished Goods Inventory

            #region

            //General Report
            tblObj.Rows.Add(new Object[] { "170300", "1703001", "F_17_GFInv", "FGInvReport?", "InputType=General", "Inventory Report - General", "Inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "170300", "1703021", "F_17_GFInv", "FGInvReport?", "InputType=Location", "Location wise Inventory Report", "Inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "170300", "1703022", "F_17_GFInv", "FGInvReport?", "InputType=Summary", "Receive And Shipment Summary Report", "Inventory", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "170300", "1703023", "F_17_GFInv", "FGInspectionEntry?", "Type=Entry", "Finish Goods Inspection Data Entry", "Inventory", "False", "False", "False", "False", "False", "" });

            #endregion

            //19. Export

            #region


            //Entry Screen
            //tblObj.Rows.Add(new Object[] { "1902000","1902001","", "EntryExportDocs", "", "Export", "Export", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "1902000", "1902002", "", "EntryRLZ", "", "Export Realization", "Export", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "1902000", "1902003", "", "SalesInterface", "", "Sales Interface", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902004", "F_19_EXP", "ExportMgt?", "Type=Entry", "Export New", "Commercial", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            //tblObj.Rows.Add(new Object[] { "1902000", "1902005", "F_19_EXP", "ExportReturn?", "Type=Entry", "Export Return", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902006", "F_19_EXP", "ExportMgt?", "Type=Approve", "Export Approve", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902007", "F_19_EXP", "AllCollection?", "Type=All", "Collection", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902008", "F_19_EXP", "DayWiseSalesEntry?", "Type=SalEntry", "Day Wise Sales Entry", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902009", "F_03_CostABgd", "MLCInfoEntry?", "Type=Entry", "L/C General Information", "Export", "False", "False", "False", "False", "False", "&actcode=&dayid=" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902010", "F_19_EXP", "DelvChallan?", "Type=Entry", "Delivery Challan Entry", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=&sircode=" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902011", "F_19_EXP", "DelvChallan?", "Type=Approve", "Delivery Challan Approve", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=&sircode=" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902012", "F_19_EXP", "ExportSample?", "Type=Entry", " Sample Export Entry", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            //tblObj.Rows.Add(new Object[] { "1902000", "1902013", "F_19_EXP", "ExportMgt?", "Type=Edit", "Export Edit", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902013", "F_19_EXP", "DelvChallan?", "Type=Edit", "Delivery Challan Edit", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=&sircode=" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902014", "F_19_EXP", "CreatePackList?", "Type=Entry", "CreatePackList", "Commercial", "False", "False", "False", "False", "False", "&actcode=&genno=&date" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902015", "F_19_EXP", "ExportReturn?", "Type=Entry", "Export Return", "Export", "False", "False", "False", "False", "False", "&actcode=&genno=&date" });
            tblObj.Rows.Add(new Object[] { "1902000", "1902016", "F_19_EXP", "ExportRetList?", "Type=ExpList", "Export Return List", "Export", "False", "False", "False", "False", "False", "" });
           

           //General Report
           //tblObj.Rows.Add(new Object[] { "1903000","1903001","", "ExportRLZ", "", "Export Realization & Incentive", "Commercial", "False", "False", "False", "False", "False", "" });
           //tblObj.Rows.Add(new Object[] { "1903000", "1903002", "", "MlcStausRpt?", "Type=LCexport", "Export  Status Information", "Commercial", "False", "False", "False", "False", "False", "" });
           //tblObj.Rows.Add(new Object[] { "1903000", "1903003", "", "RptExportStatus?", "Type=Report", "Export Document Status", "Commercial", "False", "False", "False", "False", "False", "" });
           //tblObj.Rows.Add(new Object[] { "1903000", "1903004", "", "RptExportStatus?", "Type=Projection", "Purchase Projection Status", "Commercial", "False", "False", "False", "False", "False", "" });
           tblObj.Rows.Add(new Object[] { "1903000", "1903005", "", "RptOProVsShip?", "Type=OrdProVsShip&Module=Com", "Order, Production Vs Shipment", "Commercial", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "1903000", "1903006", "", "EntryRLZ", "", "Realization Status", "Commercial", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903007", "", "SalesRealCertificate", "", "Sales- Realization Certificate", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903008", "F_19_EXP", "DayWiseSalesEntry?", "Type=SalRep", "Day Wise Sales Report", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903009", "F_19_EXP", "MoneyReceipt2?", "Type=Entry", "Money Receipt Entry", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903010", "F_19_EXP", "MoneyReceipt2?", "Type=Edit", "Money Receipt Edit", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903011", "F_19_EXP", "RptDaywiseShipment?", "Type=shipment", "Day wise Shipment Report", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903012", "F_19_EXP", "RptDaywiseShipment?", "Type=summary", "Day Wise Export Statement Report", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903013", "F_19_EXP", "RptDaywiseShipment?", "Type=unrealization", " Export Statement Unrealization Report", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903014", "F_19_EXP", "RptDaywiseShipment?", "Type=ShipmentPlan", "Day Wise Shipment Plan Summary", "Export", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "1903000", "1903015", "F_19_EXP", "RptDaywiseShipment?", "Type=IncntvDclr", "Incentive Declaration Report", "Export", "False", "False", "False", "False", "False", "" });



            tblObj.Rows.Add(new Object[] { "1951000", "1951001", "F_19_EXP", "RptExportInterface", "", "Export", "Export- Smartface", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "1961000", "1961001", "F_19_EXP", "SalesInformation", "", "Export & Realization", "Export-Smartboard", "False", "False", "False", "False", "False", "" });

            //Unkown,..
            //tblObj.Rows.Add(new Object[] { "07030", "RptPCOption", "", "PC Option Status", "Commercial", "False" ,"False","False"});
            //tblObj.Rows.Add(new Object[] { "19040", "EntryRLZ", "", " Realization Status", "Commercial", "False", "False", "False", "False", "False", "" });




            #endregion

            //20. Buyer's Interface

            // 21. Account

            #region
            //One Time Input
            tblObj.Rows.Add(new Object[] { "2101000", "2101001", "F_21_GAcc", "AccCodeBook?", "InputType=Accounts", "Account Code", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "2101000", "2101002", "", "AccSubCodeBook?", "InputType=Res", "Details Code", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "2101000", "2101003", "", "AccSubCodeBook?", "InputType=Assets", "Assets Details", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "2101000", "2101004", "", "AccSubCodeBook?", "InputType=Liabilities", "Liabilities Details", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "2101000", "2101005", "", "AccSubCodeBook?", "InputType=HOverhead", "H.O Overhead Details", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "2101000", "2101006", "", "AccGenCodeBook", "", "Final Accounts(Client Choice)", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "2101000", "2101007", "", "ResGenCodeBook", "", "Resource Code(Client Choice)", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101008", "F_21_GAcc", "AccOpening", "", "Accounts Opening", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101009", "F_21_GAcc", "AccSubCodeBook?", "InputType=ResCodePrint", "Details Code", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101010", "F_21_GAcc", "AccBankLimit", "", "Bank Limit Information", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101011", "F_21_GAcc", "AccMonthlyBgd", "", "Working Budget", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101012", "", "RptAccPaySlip", "", "Pay Slip", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101013", "F_21_GAcc", "AccResourceCode?", "Type=Matcode", "Materials Code Opening", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101014", "F_21_GAcc", "AccResourceCodeOpnStk?", "Type=Matcode", "Material Code book with Opening stock", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2101000", "2101015", "F_21_GAcc", "AccResourceCode?", "Type=MatPriceSumm", "Material Price Summary", "Accounts", "False", "False", "False", "False", "False", "" });




            //Entry Screen

            tblObj.Rows.Add(new Object[] { "2102000", "2102001", "F_21_GAcc", "GeneralAccounts?", "Mod=Accounts", "Voucher Entry", "Accounts", "False", "False", "False", "False", "False", "&vounum=" });

            tblObj.Rows.Add(new Object[] { "2102000", "2102005", "F_21_GAcc", "AccPayment?", "tcode=99&tname=Payment Voucher&Type=Acc", "Post Dated Cheque(Issue)", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102006", "F_21_GAcc", "AccPayment?", "tcode=99&tname=Deposit Voucher&Type=Acc", "Post Dated Cheque(Received)", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102007", "F_21_GAcc", "AccPurchase?", "Type=Entry", "Purchase Update", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102008", "F_21_GAcc", "AccIncomeOfOrd?", "Type=Entry", "Export Update", "Account", "False", "False", "False", "False", "False", "&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102009", "F_21_GAcc", "AccTransfer?", "Type=Entry", "Transfer Journal", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102010", "", "AccInterComVoucher", "", "InterCompany Payment", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102011", "F_21_GAcc", "AccPayUpdate?", "Type=AccIsu", "Post Dated Cheque Update(Issued)", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102012", "F_21_GAcc", "AccPayUpdate?", "Type=AccRec", "Post Dated Cheque Update(Received)", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102013", "F_21_GAcc", "AccBankRecon?Type=Acc", "", "Bank Reconcilaition", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102014", "", "RptBankCheque?", "Type=ChqInHand", "Cash/Cheque In Hand Print", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102015", "", "RptBankCheque?", "Type=PostChqInHand", "List Of Post Dated Cheque", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102016", "", "RptBankCheque?", "Type=CollChqSt", "Collection History", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102017", "F_15_DPayReg", "AccOnlinePaymentRa?", "Type=ChequeReady", "Cheque Ready", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102018", "F_15_DPayReg", "AccOnlinePaymentRa?", "Type=ChequeApproval", "Cheque Approval", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102019", "F_15_DPayReg", "AccOnlinePaymentRa?", "Type=ChequePayment", "Cheque Payment", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102020", "F_15_DPayReg", "ChequeSignSheet?", "Type=Acc", "Cheque SignSheet", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102021", "F_15_DPayReg", "AccOnlinePaymentApp?", "Type=ChequePayment", "Payment Approval", "Accounts", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "2102000", "2102022", "F_34_Mgt", "OtherReqEntry?", "Type=OreqEntry", "Create Fund Requisition", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102023", "F_34_Mgt", "OtherReqEntry?", "Type=OreqApproved", "Fund Requisition First Approval", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102024", "F_34_Mgt", "OtherReqEntry?", "Type=OreqPrint", "Fund Requisition Approval Print", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102026", "F_34_Mgt", "OtherReqEntry?", "Type=OreqEdit", "Edit Fund Requisition", "Accounts", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "2102000", "2102025", "F_21_GAcc", "AllVoucherTopSheet", "", "Voucher 360 <sup>0", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "2102000", "2102026", "", "AccPurchase?", "Type=Entry", "Purchase Accounts ", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102027", "F_21_GAcc", "SuplierPayment?", "tcode=99&tname=Payment Voucher&Mod=Accounts", "Supplier Payment Voucher", "Accounts", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "2102000", "2102028", "F_21_GAcc", "AccIsuUpdate?", "Type=Entry", "Issue Update", "False", "False", "False", "False", "False", "&comcod=&genno=&date=" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102029", "F_21_GAcc", "AccProductionJV?", "Type=Entry", "Production Update", "False", "False", "False", "False", "False", "&comcod=&genno=&date=" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102030", "F_21_GAcc", "AccProductionJVManual?", "Type=Entry", "Production Manual Update", "Accounts", "False", "False", "False", "False", "False", "&comcod=&genno=&date=" });
            tblObj.Rows.Add(new Object[] { "2102000", "2102031", "F_21_GAcc", "AccPurchaseFor?", "Type=Entry", "Import Purchase Voucher", "Accounts", "False", "False", "False", "False", "False", "&comcod=&genno=&date=" });

            tblObj.Rows.Add(new Object[] { "2102000", "2102032", "F_21_GAcc", "AccIndentUpdate?", "Type=Entry", "Account Indent Update", "Accounts", "False", "False", "False", "False", "False", "&comcod=&genno=&date=" });
           

            //General Report

            tblObj.Rows.Add(new Object[] { "2103000", "2103001", "F_21_GAcc", "AccLedgerAll", "", "Ledger-All", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103002", "F_21_GAcc", "AccLedger?", "Type=Ledger&RType=GLedger", "Ledger-01", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103003", "F_21_GAcc", "AccLedger?", "Type=SubLedger", " Subsidiary Ledger", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103004", "F_21_GAcc", "AccControlSchedule?", "Type=Type01", "Accounts Control Schedule - 01", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103005", "F_21_GAcc", "AccControlSchedule?", "Type=Type02", "Accounts Control Schedule - 02", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103006", "F_21_GAcc", "AccDetailsSchedule", "", "Account Details Schedule", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103007", "F_21_GAcc", "AccFinalReports?", "RepType=PS", "LC Report", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103008", "F_21_GAcc", "RptAccSpLedger?", "Type=DetailLedger", "Special Ledger", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103009", "", "RptLCVariance", "", "Income Statement - Budget Vs. Actual", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103010", "", "IncomeReduced", "", "Variance Analysis", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103011", "F_21_GAcc", "RptTransactionSearch02", "", "Transaction Search - 02", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103012", "F_21_GAcc", "RptAccTranSearch", "", "Transaction Search", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103013", "F_21_GAcc", "RptAccDTransaction?", "Type=Accounts&TrMod=DTran", "Cash & Bank Transaction", "Accounts", "False", "False", "False", "False", "False", "" }); //&&TrMod=DTran
            tblObj.Rows.Add(new Object[] { "2103000", "2103014", "F_21_GAcc", "RptAccDayTransData", "", "Daily transaction", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103015", "F_21_GAcc", "RptAccDTransaction?", "Type=Accounts&TrMod=Fflow", "Fund Flow", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103016", "F_21_GAcc", "AccTrialBalance?", "Type=BankPosition", "Bank Position", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103017", "F_21_GAcc", "AccTrialBalance?", "Type=BankPosition02", "Bank Position 02", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103018", "", "RptAllAccDTransaction", "", "Transaction with Post Dated Cheque", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103019", "F_21_GAcc", "RptAccDTransBankSt", "", "Bank Reconcilaition Statement", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103020", "F_21_GAcc", "RptAccSpLedger?", "Type=ASPayment", "Supplier Overall Position", "Account", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "2103000", "2103021", "F_15_DPayReg", "AccOnlinePaymnt?", "Type=Entry", "Account Online Paymnt", "Accounts", "False", "False", "False", "False", "False", "&comcod=" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103022", "F_15_DPayReg", "RptBillStatusInf?", "Type=Entry", "BILL STATUS INFORMATION", "Accounts", "False", "False", "False", "False", "False", "&comcod=" });



            tblObj.Rows.Add(new Object[] { "2103000", "2103024", "F_21_GAcc", "TransectionPrint?", "Type=AccVoucher&Mod=Accounts", "Voucher Print", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103025", "F_21_GAcc", "TransectionPrint?", "Type=AccCheque&Mod=Accounts", "Cheque Print", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103026", "F_21_GAcc", "TransectionPrint?", "Type=AccPostDatChq", "Post Dated Cheque Print", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103027", "", "RptBankCheque?", "Type=ToDayIssChq", "Cheque Issued", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103028", "", "RptBankCheque?", "Type=PayChqCl", "Cheque History", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103029", "", "RptBBLCPayStatus", "", "BBLC Payment Status", "Procurement", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103030", "", "RptAccPayUpdate?", "Type=GroupWiseChqIssued", "Cheque Issued- Group Wise", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103031", "", "RptAccPDCStatus?", "Type=DayWisePDC", "PDC Issue Status", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103032", "", "RptAccPayUpdate?", "Type=ChqIsssued", "Day Wise Issued(Cheque Date)", "Account", "False", "False", "False", "False", "False", "" });
           


            /////////////////////
            tblObj.Rows.Add(new Object[] { "2103000", "2103033", "F_21_GAcc", "AccLedger?", "Type=Ledger&RType=MLedger", "Ledger-02", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103034", "F_21_GAcc", "RptAccDTransaction?", "Type=Accounts&TrMod=IssuedVsCollect", "Receipts & Payment(Actual)", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103035", "", "AccTrialBalance?", "Type=TBConsolidated", "Trial Balance (Consolidated)", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103036", "", "ProjTrialBalanc?", "Type=TrailBal2", "Trial Balance 2", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103037", "", "AccTrialBalance?", "Type=HOTB", "Head Office Trial Balance", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103038", "", "ProjTrialBalanc?", "Type=PrjTrailBal", "Project Trial Balance", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103039", "", "RptBankCheque?", "Type=CashFlow", "Statement of Cash Flow", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103040", "", "RptBankCheque?", "Type=FundFlow", "Statement of Fund Flow", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103041", "", "RptBankCheque?", "Type=CashFlow02", "Statement of Cash Flow 02", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103042", "", "AccTrialBalance?", "Type=BalConfirmation", "Balance Confirmation", "Management Accounts", "False", "False", "False", "False", "False", "" });
            
            tblObj.Rows.Add(new Object[] { "2103000", "2103044", "F_21_GAcc", "AccTrialBalance?", "Type=Mains", "Trial Balance", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103045", "F_21_GAcc", "RptAccDTransaction?", "Type=Accounts&TrMod=ProTrans", "Daily Transaction -Individual", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103046", "F_21_GAcc", "SupCustTaxVat?", "Type=Details", "Supplier Tax & Vat ", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103047", "F_21_GAcc", "RptChequeIssuedList", "", "Cheque Register", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103048", "F_21_GAcc", "RptGeneralReport", "", "General Requisition Report", "Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2103000", "2103049", "F_21_GAcc", "RptBillRegister", "", "Bill Register Report", "Accounts", "False", "False", "False", "False", "False", "" });




            tblObj.Rows.Add(new Object[] { "2151000", "2151001", "F_15_DPayReg", "GenBillInterface", "", "General Bill", "Accounts- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2151000", "2151002", "F_15_DPayReg", "BillRegInterface", "", "Bill Register", "Accounts- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "2151000", "2151003", "F_23_MAcc", "AccountInterface", "", "Accounts", "Accounts- Smartface", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "2161000", "2161003", "F_23_MAcc", "AccDashBoard", "", "Accounts", "Accounts-Smartboard", "False", "False", "False", "False", "False", "" });
           
       
            //Unkown..

            //tblObj.Rows.Add(new Object[] { "21081", "AccSubCodeBook?", "InputType=Overhead", "Overhead & Land Code", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "21085", "AccSubCodeBook?", "InputType=Wrkschedule", "Work Schedule", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "21086", "AccSubCodeBook?", "InputType=UnitCode", "New Unit Code", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "21087", "AccSubCodeBook?", "InputType=customer", "Previous Customer", "Accounts", "False", "False", "False", "False", "False", "" });
            ////tblObj.Rows.Add(new Object[] { "08089", "AccSubCodeBook?", "InputType=Supplier", "Supplier Code", "Accounts", "False","False","False" });

            ////tblObj.Rows.Add(new Object[] { "08161", "AccFinalReports?", "RepType=BE", "Budget Vs Expensis", "Accounts", "False","False","False" });
            ////tblObj.Rows.Add(new Object[] { "08170", "SalesDetailsSchedule", "", "Sales Details Schedule", "Accounts", "False" ,"False","False"});
            //tblObj.Rows.Add(new Object[] { "21200", "AccFinalReports?", "RepType=SPC", "Project Report-Specifition", "Accounts", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "21210", "RptPrjCostPerSFT?", "Type=RemainingCost", "Remaining Project Cost", "Accounts", "False", "False", "False", "False", "False", "" });
            ////tblObj.Rows.Add(new Object[] { "08230", "RptAccBudget?", "Type=WbgdVsAc", "Working Budget Vs. Achievement", "Accounts", "False" ,"False","False"});
            ////tblObj.Rows.Add(new Object[] { "08231", "RptAccBudget?", "Type=WbgdVsAcDetials", "Working Budget Vs. Achievement Details", "Accounts", "False","False","False" });

            //tblObj.Rows.Add(new Object[] { "21242", "AccPurchaseBBLC", "", " Purchase BBLC", "Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "21243", "RptAccSpLedger", "", "Special Ledger", "Account", "False", "False", "False", "False", "False", "" });
            ////tblObj.Rows.Add(new Object[] { "08245", "AccIncomeOfOrd", "", "Acc. Income Of LC", "Account", "False","False","False" });

            //tblObj.Rows.Add(new Object[] { "21270", "RptBankCheque?", "Type=ChquedepPrint", "Cheque Deposit Print", "Account", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "21305", "LcCodeBook", "", "Project Code Book", "Accounts", "False", "False", "False", "False", "False", "" });




            #endregion



            // 19. Audit
            #region Audit
            //tblObj.Rows.Add(new Object[] { "24001", "MisSECCodeBook", "", "SECURITY & EXCHANES COMMISION CODE", "Audit", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "24010", "EntrySEC", "", "Compliance of SEC", "Audit", "False", "False", "False", "False", "False", "" });
            #endregion

            //25. Marketing
            #region Marketing


            #endregion

            // 27. Fixed Assets

            #region

            //one Time Input
            tblObj.Rows.Add(new Object[] { "2701000", "2701001", "F_27_Fxt", "FxtAsstCodeBook", "", "Fixed Assets Code Book", "Fixed Assets", "False", "False", "False", "False", "False", "" });


            // Entry Screen
            tblObj.Rows.Add(new Object[] { "2702000", "2702001", "F_27_Fxt", "FxtAssetRegister", "", "Fixed Asset Entry ", "Fixed Assets", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "27010", "2702002", "F_27_Fxt", "FxtAsstTransfer", "", "Transfer", "Fixed Assets", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "27070", "2702003", "F_27_Fxt", "EntryDepCharge", "", "Depreciation Cost(Calculation)", "Fixed Assets", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "27040", "2702004", "F_27_Fxt", "DepreciationCharge", "", "Depreciation Charge", "Fixed Assets", "False", "False", "False", "False", "False", "" });



            // General Report
            tblObj.Rows.Add(new Object[] { "2703000", "2703001", "F_27_Fxt", "RptFixAsset02", "", "Fixed Asset Report ", "Fixed Assets", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "27050", "2703002", "F_27_Fxt", "RptFxtAsstStatus?", "Type=Fix", "Fixed Assets Status", "Fixed Assets", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "27060", "2703003", "F_27_Fxt", "RptFxtAsstStatus?", "Type=DepCost", "Schedule Of Fixed Assets", "Fixed Assets", "False", "False", "False", "False", "False", "" });



            //tblObj.Rows.Add(new Object[] { "27001", "FxtAstGinf", "", "General Information ", "Fixed Assets", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "27020", "RptFxtAsstBillRent", "", "Rent Bill ", "Fixed Assets", "False", "False", "False", "False", "False", "" });


            
            #endregion

            

            // 31 MIS 

            #region
            //One time Input
            tblObj.Rows.Add(new Object[] { "3101000", "3101001", "", "RptMisProIncomeExe?", "Type=BgdIncomeLCWise", "Budgeted Income - LC Wise", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101002", "", "RptMisProIncomeExe?", "Type=BgdIncomeOrderWise", "Budgeted Income -Order Wise", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101003", "", "RptMisMasterBgd?", "Type=SrAUtilities", "Sources & Utilization of Fund- Cash Basis", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101004", "", "RptMisMasterBgd?", "Type=SrAUtilitiesFF", "Sources And Utilization of Fund-Acural Basis", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101005", "", "RptMisMasterBgd?", "Type=BgdVsExpenses", "Budget VS Expenses", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101006", "", "RptMisMasterBgd?", "Type=SalesVsColection", "Sales Vs Collection", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101007", "", "RptMisMasterBgd?", "Type=ColVsExpenses", "Collection VS Expenses", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3101000", "3101008", "", "BankBalance", "", "Bank Balance as of to-day", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101009", "F_21_GAcc", "RptAccDTransaction?", "Type=Accounts&TrMod=RecPay", "Receipts & Payment(Honoured)", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101010", "F_21_GAcc", "AccFinalReports?", "RepType=BS", "Balance Sheet", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101011", "F_21_GAcc", "AccFinalReports?", "RepType=SPBS", "Special Balance Sheet", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101012", "", "RptMisMasterBgd?", "Type=CostOfFund", "Cost Of Fund", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3101000", "3101013", "", "RptSalesDuPeriod", "", "Income Statement(Investment basis)", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101014", "F_21_GAcc", "AccFinalReports?", "RepType=IPRJ", "Income Statement (Individual LC)", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3101000", "3101015", "", "RptInComeStExe", "", "Income Statement (Execution)", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101016",  "", "RptLCVariance", "", "Income Statement - Budget Vs. Actual", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101017",  "", "IncomeReduced", "", "Variance Analysis", "Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101018", "", "RptInComeStProdBasis", "", "Income Statement - Production Basis", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101019", "F_21_GAcc", "AccFinalReports?", "RepType=SHEQUITY", "Statement Of Share Holders Equity", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101020", "", "RptBankCheque?", "Type=CashFlow", "Statement of Cash Flow", "Management Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101021", "", "EntrySEC", "", "Compliance of SEC", "Audit", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101022", "", "EntryFinResult", "", "Financial Result(5 Years)", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101023", "", "FinCodeBook02", "", "Financial Code Input", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101024", "", "MisFinNoteCodeBook","", "Account Policies Input", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3101000", "3101025", "", "RptBankCheque?", "Type=FinNote", "Explanatory Notes to the Financial Statements", "Management Accounts", "False", "False", "False", "False", "False", "" });
        

            //Entry Screen

            tblObj.Rows.Add(new Object[] { "3102000", "3102001", "", "RptMisMasterBgd?", "Type=MasterBgd", "Master Budgeted Individual LC", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3102000", "3102002", "", "RptProjectStatus?", "Type=LCStatus", "LC Status ", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3102000", "3102003", "", "RptAccCollVsClearance?", "Type=MonSales", "Sales - All Project", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3102000", "3102004", "", "RptAccCollVsClearance?", "Type=MonCollection", "Collection(Actual)", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3102000", "3102005", "", "RptAccCollVsClearance?", "Type=MonCollHonoured", "Collection(Honoured)", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3102000", "3102006", "", "RptAccCollVsClearance?", "Type=MonReceipt", "Real Collection", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3102000", "3102007", "", "RptAccCollVsClearance?", "Type=MonPayment", "Month Wise  Real Payment(Project Wise)", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3102000", "3102008", "", "RptAccCollVsClearance?", "Type=MonPaymentSumm", "Month Wise  Real Payment-Summary", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3102000", "3102009", "", "RptAccCollVsClearance?", "Type=MonPaymentDet", "Month Wise  Real Payment(Cost Wise)", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3102000", "3102010", "", "RptRealInOutFlow?", "Type=RealFlow", "Month Wise  Real Inflow & Outflow", "MIS", "False", "False", "False", "False", "False", "" });
            


            //General Report
            tblObj.Rows.Add(new Object[] { "3103000", "3103001", "F_21_GAcc", "AccFinalReports?", "RepType=IS", "Income Statement", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103002", "", "RptMisMasterBgd?", "Type=ProDetails", "Comparative Expenses-LC ", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103003", "", "RptMisMasterBgd?", "Type=ProExpenses", "Comparative Expenses- H/O", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103004", "", "RptMisMasterBgd?", "Type=ComProCost", "Comparative LC Cost", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103005", "", "RptMisProIncomeExe?", "Type=PrjExecution", "Work Execuation - All LC", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103006", "", "RptMisProIncomeExe?", "Type=ConBgdVsExe", "Budget Vs Execution. All LC", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103007", "", "RptImpExeStatus?", "Type=MatEva", "Purchase Evaluation", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103008", "", "RptPrjCostPerSFT?", "Type=ProTarVsAchievement", "Construction Target Vs. Achievement", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103009", "F_21_GAcc", "AccTrialBalance?", "Type=Details", "Details Balance Sheet", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3103000", "3103010", "", "RptConstruProgress", "", "Floor Wise Construction Progress", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103011", "", "ProjectSummary", "", "LC Summary -At a glance", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103012", "", "RptLCInfoataglance", "", "LC Status - At a glance", "MIS", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3103000", "3103013", "", "RptLCProaLossAcount", "", "Income Statement(On Production Basis)", "MIS", "False", "False", "False", "False", "False", "" });
            
            tblObj.Rows.Add(new Object[] { "3103000", "3103015", "", "RptAccIncome?", "Type=IncomeMonthly", "Cost Details (12 Month)", "MIS", "False", "False", "False", "False", "False", "" });



            tblObj.Rows.Add(new Object[] { "3191000", "3191001", "F_31_Mis", "RptAllDashboard?", "Type=Merchandiser", "Merchandiser", "MIS- Analysis Graph", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3191000", "3191002", "F_31_Mis", "RptAllDashboard?", "Type=Purchase", "Purchase", "MIS- Analysis Graph", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3191000", "3191003", "F_31_Mis", "RptAllDashboard?", "Type=ProductionRMG", "Production", "MIS- Analysis Graph", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3191000", "3191004", "F_31_Mis", "RptAllDashboard?", "Type=ExRelz", "Export Realization", "MIS- Analysis Graph", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3191000", "3191005", "F_31_Mis", "RptAllDashboard?", "Type=Accounts", "Accounts", "MIS- Analysis Graph", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "3191000", "3191001", "F_31_Mis", "RptAllDashboard?", "Type=Sales", "Sales", "MIS- Dashboard", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3191000", "3191001", "F_31_Mis", "RptAllDashboard?", "Type=Production", "Production- MFG", "MIS- Dashboard", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "3191000", "3191020", "", "AllGraph", "", "Overall Graph", "MIS- Analysis Graph", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "3199000", "3199001", "", "CompanyOverAllReport", "", "All in One", "MIS- All in One", "False", "False", "False", "False", "False", "" });


            #endregion

            //33. Documentaion Module
            #region Doc
            tblObj.Rows.Add(new Object[] { "3301000", "3301001", "F_33_Doc", "DocUpload?", "Type=Entry", "Document Entry", "Document Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3301000", "3301002", "F_33_Doc", "DocUpload?", "Type=Edit", "Document Edit", "Document Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3301000", "3301003", "F_33_Doc", "DocCodeBook?", "Type=Entry", "Document Code Book", "Document Management", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "3303000", "3303001", "F_33_Doc", "ShowAllDoc?", "Type=Rpt", "Show Document Activity", "Document Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3351000", "3351002", "F_33_Doc", "RptDocInterface", "", "Document Management Smartface", "Document Management", "False", "False", "False", "False", "False", "" });




            #endregion

            //34. Management
            #region
            //One Time Input
            tblObj.Rows.Add(new Object[] { "3401000", "3401001", "F_21_GAcc", "AccPayment?", "tcode=99&tname=Payment Voucher&Type=Mgt", "Post Dated Cheque(Issue)", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401002", "F_21_GAcc", "AccPayment?", "tcode=99&tname=Deposit Voucher&Type=Mgt", "Post Dated Cheque(Received)", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401003", "F_34_Mgt", "AccProjectCode", "", "L/C Code Book", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401004", "F_34_Mgt", "AccUserCash", "", "Cash & Bank Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401005", "F_34_Mgt", "AccBankRecon?Type=Mgt", "", "Bank Reconcilaition", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401006", "", "MSLCStatus", "", "LC Status", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401999", "F_34_Mgt", "AddressDetails", "", "Address Basic Information", "Settings", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "3401000", "3401011", "F_34_Mgt", "ShareEquity", "", "Share Equity", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401012", "F_34_Mgt", "AccGenCodeBook", "", "Display Code", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401013", "F_34_Mgt", "CodeLink", "", "Code Link(BS)", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401014", "F_34_Mgt", "CodeLinkCf", "", "Code Link(CF)", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401015", "F_34_Mgt", "SalesCodeBook?", "Type=All", "General Code Book", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401016", "F_34_Mgt", "UnitConvert", "", "Unit Conversion", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401017", "F_34_Mgt", "AccCurCodeBook", "", "Currency Code Book", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401018", "F_34_Mgt", "AccConversion", "", "Currency Conversion", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401019", "F_34_Mgt", "AccWIPCode?", "Type=FgSeWIP", "Semi WIP Opening", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401020", "F_34_Mgt", "SignatoryEntry", "", "Signatory Entry", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3401000", "3401021", "F_34_Mgt", "CodeLinkHR", "", "HR Code Link", "Settings", "False", "False", "False", "False", "False", "" });



            tblObj.Rows.Add(new Object[] { "3402000", "3402004", "F_21_GAcc", "GeneralAccounts?", "Mod=Management", "Voucher Edit", "Settings", "False", "False", "False", "False", "False", "&vounum=" });
            tblObj.Rows.Add(new Object[] { "3402000", "3402008", "F_21_GAcc", "TransectionPrint?", "Type=AccVoucher&Mod=Management", "Voucher Delete", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3402000", "3402009", "F_13_CWare", "PurReqEntry02?InputType=FxtAstApproval", "", "Requisition Approval", "Settings", "False", "False", "False", "False", "False", "&comcod=&actcode=&genno=" });
            tblObj.Rows.Add(new Object[] { "3402000", "3402011", "F_10_Procur", "PurWrkOrderEntryL?", "InputType=OrderEdit", "Purhcase Order Edit", "Settings", "False", "False", "False", "False", "False", "&genno=&actcode=" });
            tblObj.Rows.Add(new Object[] { "3402000", "3402014", "F_34_Mgt", "RptGroupTask?", "Type=GTaskRpt", "Group Task Report", "Settings", "False", "False", "False", "False" });            //General Report
            
            
            
            tblObj.Rows.Add(new Object[] { "3403000", "3403001", "F_34_Mgt", "UserLoginfrmPtl", "", "Comp. Page Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403002", "F_34_Mgt", "UserLoginfrm", "", "User Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403003", "F_34_Mgt", "AccUserCash", "", "Cash & Bank Permission", "Settings", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403004", "F_34_Mgt", "Tranlimitdate", "", "Transaction Limit Day", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403005", "F_34_Mgt", "UserImage", "", "User Image Upload", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403006", "F_34_Mgt", "RptUserLogDetails?", "Type=Entry", "Entry, Edit & Delete Record", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403007", "F_34_Mgt", "RptUserLogStatus", "", "User Log Information", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403008", "F_34_Mgt", "DataBackup", "", "Auto Database Backup", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403009", "F_34_Mgt", "DashBoardAll", "", "Smartboard All", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403010", "F_34_Mgt", "HrLeaveApprovalForm", "", "HR Leave Approval Setup", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403011", "F_34_Mgt", "PayrollLink?", "Type=Hr", "Department Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403012", "F_34_Mgt", "ProjectLink", "", "Store Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403013", "F_34_Mgt", "PayrollLink?", "Type=Production", "Production Department Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403014", "F_34_Mgt", "PayrollLink?", "Type=Process", "Production Process Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403016", "F_34_Mgt", "CACodeLink", "", "Chart of Accounts Permission", "Settings", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403017", "F_34_Mgt", "MailRecvSetUp", "", "Mail Receiver Setup", "Settings", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "3403000", "3403020", "F_34_Mgt", "HREmpJoblocation?", "Type=Entry", "Job Location Permission", "Settings", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "3451000", "3451001", "F_34_Mgt", "RptAdminInterface", "", "Management", "Settings- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "3403000", "3403015", "F_34_Mgt", "UserfrmGroup", "", "Group User Management", "Settings", "False", "False", "False", "False", "False", "" });



            //Unkown...

            //Entry Screen

            //tblObj.Rows.Add(new Object[] { "3402000", "3402010", "", "PurAprovEntry?", "InputType=ProposalEdit", "Order Approval Edit (Local Purchase)", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3402000", "3402012", "", "PurMRREntryLocal?", "Type=Mgt", "MRR Edit (Local Purchase)", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3402000", "3402013", "", "PurBillEntryLocal?", "Type=BillEdit", "Bill Edit (Local Purchase)", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3402000", "3402001", "", "PurReqEntry?", "InputType=Approval", "Requisition Approval", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3402000", "3402002", "", "PurReqEntry?", "InputType=ReqEdit", "Requisition Edit", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3402000", "3402003", "", "PurWrkOrderEntry?", "InputType=OrderEdit", "Work Order", "Management", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "3401000", "3401007", "", "EntryMasterLC?", "Type=2&Module=Management", "Budget Approval", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3401000", "3401008", "", "ProsclntCodeBook", "", "Client Code", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3401000", "3401009", "", "MktTeamCodeBook?Type=MktTeam", "", "Sales Team", "Marketing", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3401000", "3401010", "", "TransferClient", "", "Transfer Client Information", "Marketing", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "34095", "BgdMaster?", "InputType=BgdSub", "Local Purchase Budget Approval", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "34480", "RptEngInterface", "", "General Bill Interface", "Procurement ", "False", "False"});

            //tblObj.Rows.Add(new Object[] { "34492",  "OtherReqEntry?", "Type=FinalAppr", "Requisition Final Approval", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "34493",  "OtherReqEntry?", "Type=OreqPrint", "Payment Order", "Budget", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "34430", "EntryFinResult", "", "Financial Result(5 Years)", "Management", "False", "False", "False", "False", "False", "" });

            #endregion


            #endregion

            #region KPI

            // 05. My Marketing Interface(Sales)
            //#region My  Interface
            tblObj.Rows.Add(new Object[] { "4702000", "4702001", "F_47_Kpi", "KpiCodeBook", "", "KPI Code Book", "KPI Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "4702000", "4702002", "F_47_Kpi", "KpiTargetEntry", "", "KPI Target Entry", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3902000", "3902010", "ClientDetail?", "Type=Client", "Client Details", "My Marketing Interface(Sales)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3902000", "3902020", "EmpKpiEntry04?", "Type=Sales", "Daily Job Execution", "My Marketing Interface(Sales)", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "3903000", "3903001", "EmpKpiEntry03?", "Type=Client", "Daily Job Execution", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3903000", "3903010", "RptEmpMonthWiseEva03?", "Type=IndEmp", "Month Wise Evaluation", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3903000", "3903020", "RptMIS02?", "Type=EvaonProBasis", "Evaluation on Project", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3903000", "3903021", "RptMIS02?", "Type=EmpEvaluation", "Employee Evaluation", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3903000", "3903022", "RptMIS02?", "Type=EmpHistory&History=ALL", "History(All Employee)", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3903000", "3903023", "RptMIS02?", "Type=EmpHistory&History=Individual", "Individual History", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3903000", "3903024", "RptMIS02?", "Type=IndEmpHistory", "Month Wise Evaluation Details", "MIS", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "3903000", "3903025", "EmpKpiEntry04?", "Type=All", "KPI Entry", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });


            //tblObj.Rows.Add(new Object[] { "3903000", "3903026", "RptEmpHistory02?", "Type=IndEmp", "Employee History", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "" });



            ////tblObj.Rows.Add(new Object[] { "0502020", "EmpKpiEntry?", "Type=Client", "Daily Job Execution", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05030", "RptEmpKpiRes", "", "KPI Result", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05040", "RptEmpEvaSheet?", "Type=IndEmp", "Evaluation(Individual)", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05050", "RptMktAppointment?", "Type=Todaysdis&UType=Client", "Todays Appoinment", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05051", "RptMktAppointment?", "Type=NextApp&UType=Client", "Next Appointment", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05052", "RptMktAppointment?", "Type=DiscussHis&UType=Client", "Client History", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05053", "RptMktAppointment?", "Type=SalePerformance&UType=Client", "Sales Performance", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05054", "RptMktAppointment?", "Type=OffPerformance&UType=Client", "Officer's Performance", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05060", "TransferClient?", "Type=Mkt", "Client Transfer", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05070", "PrjCompFlowchart", "", "Completaion Flow Chart ", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05080", "RptEmpMonthWiseEva?", "Type=IndEmp", "Month Wise Evaluation", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "05090", "RptEmpMonthWiseEvaDet?", "Type=IndEmp", "Month Wise Evaluation Details", "My Marketing Interface", "False", "False", "False", "False", "False", "", "False" });
            //#endregion




            //// 07. My Marketing Interface (General)
            //#region My  Interface General

            //tblObj.Rows.Add(new Object[] { "4702000", "4702001", "EmpKpiEntry04?", "Type=General", "Daily Job Execution", "My Marketing Interface(General)", "False", "False", "False", "False", "False", "" });


            ////tblObj.Rows.Add(new Object[] { "07001", "EmpKpiEntry02?", "Type=Client", "Daily Job Execution", "My Marketing Interface(Land)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "07010", "RptEmpMonthWiseEva02?", "Type=IndEmp", "Month Wise Evaluation", "My Marketing Interface(Land)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "07020", "RptEmpHistory?", "Type=IndEmp", "Employee History", "My Marketing Interface(Land)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "07030", "RptMIS?", "Type=EvaonProBasis", "Evaluation on Project", "MIS", "False", "False", "False", "False", "False", "", "False" });

            ////tblObj.Rows.Add(new Object[] { "07032", "RptMIS?", "Type=EmpHistory&History=ALL", "History(All Employee)", "MIS", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "07033", "RptMIS?", "Type=EmpHistory&History=Individual", "Individual History", "MIS", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "07034", "RptMIS?", "Type=IndEmpHistory", "Month Wise Evaluation Details", "MIS", "False", "False", "False", "False", "False", "", "False" });

            //#endregion



            //// 10. My Marketing Interface CR
            //#region My Interface Marketing
            //tblObj.Rows.Add(new Object[] { "4803000", "4803001", "RptEmpMonthWiseEva04?", "Type=IndEmp", "Month Wise Evaluation", "My Marketing Interface(CR)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "4803000", "4803010", "RptEmpEvaSheet04?", "Type=IndEmp", "KPI Evaluation (All)", "My Marketing Interface(CR)", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "4802000", "4802020", "EmpKpiEntry04?", "Type=CR", "Daily Job Execution", "My Marketing Interface(CR)", "False", "False", "False", "False", "False", "" });
            //#endregion

            //// 09. My Marketing Interface Marketing
            //#region My  Interface Marketing
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903001", "EmpKpiEntry03?", "Type=Client", "Daily Job Execution", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903010", "RptEmpMonthWiseEva03?", "Type=IndEmp", "Month Wise Evaluation", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903020", "RptMIS02?", "Type=EvaonProBasis", "Evaluation on Project", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903021", "RptMIS02?", "Type=EmpEvaluation", "Employee Evaluation", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903022", "RptMIS02?", "Type=EmpHistory&History=ALL", "History(All Employee)", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903023", "RptMIS02?", "Type=EmpHistory&History=Individual", "Individual History", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903024", "RptMIS02?", "Type=IndEmpHistory", "Month Wise Evaluation Details", "MIS", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "0903000", "0903025", "EmpKpiEntry04?", "Type=All", "KPI Entry", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });


            ////tblObj.Rows.Add(new Object[] { "0903000", "0903026", "RptEmpHistory02?", "Type=IndEmp", "Employee History", "My Marketing Interface(Marketing)", "False", "False", "False", "False", "False", "", "False" });

            //#endregion


            //// 13. My interface Legal
            //#region My interface legal

            //tblObj.Rows.Add(new Object[] { "4902000", "4902001", "EmpKpiEntry04?", "Type=Legal", "Daily Job Execution", "My Marketing Interface(Legal)", "False", "False", "False", "False", "False", "" });
            //// tblObj.Rows.Add(new Object[] { "13010", "EmpStdKpi", "", "Mis-Department(All)", "My interface legal", "False", "False", "False", "False", "False", "", "False" });
            ////tblObj.Rows.Add(new Object[] { "13001", "EmpStdKpi", "", "Monthly KPI Target", "KPI Legal", "False", "False", "False", "False", "False", "", "False" });
            //tblObj.Rows.Add(new Object[] { "4903000", "4903002", "RptEmpEvaSheetLeg?", "Type=Leg", "KPI Evaluation", "My Marketing Interface(Legal)", "False", "False", "False", "False", "False", "" });


            //#endregion
            //// 75. Management 
            //#region KPI Management

            //tblObj.Rows.Add(new Object[] { "7502000", "7502001", "EmpStdKpi", "", "Monthly KPI Target", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7501000", "7501002", "GenCodeBook", "", "Code Book", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7501000", "7501010", "TeamSeriCode", "", "Team Code", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502020", "ProjectLink", "", "Center Link", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502030", "UserImage", "", "User Image Upload", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503040", "RptUserLogDetails", "", "Entry, Edit & Cancellation Record", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503050", "RptUserLogStatus", "", "User Log Information", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503060", "RptMktAppointment?", "Type=Todaysdis&UType=Client", "Todays Appoinment", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503061", "RptMktAppointment?", "Type=NextApp&UType=Client", "Next Appointment", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503062", "RptMktAppointment?", "Type=DiscussHis&UType=Mgt", "Client History", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503063", "RptMktAppointment?", "Type=SalePerformance&UType=Mgt", "Sales Performance", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503064", "RptMktAppointment?", "Type=OffPerformance&UType=Mgt", "Officer's Performance", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502070", "TransferClient?", "Type=Mgt", "Client Transfer", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502080", "EmpKpiEntry02?", "Type=Mgt", "Daily Job Execution(Land)", "KPI Management", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "7502000", "7502100", "EntryProjectActive", "", "Center Activation ", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502110", "EmpKpiEntry?", "Type=Mgt", "Daily Job Execution(Sales )", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502120", "EmpKpiEntry03?", "Type=Mgt", "Daily Job Execution(Marketing)", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503130", "RptEmpMonthWiseEva03?", "Type=Mgt", "Month Wise Evaluation", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503140", "RptEmpHistory02?", "Type=Mgt", "Employee History", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503150", "RptEmpMonthWiseEvaDet?", "Type=Mgt", "Month Wise Evaluation Details", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503160", "RptEmpMonthWiseEva04?", "Type=Mgt", "Month Wise Evaluation", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7502170", "ProjectCode?", "Type=Project", "Center Code ", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7502180", "DeptActivitiesCode", "", "Activities Code", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503190", "RptEmpEvaSheet?", "Type=Mgt", "KPI Evaluation (All)", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503200", "RptEmpEvaGraph", "", "Individual KPI(Multi Graph)", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503210", "RptClientDis", "", "Client Discussion History", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7502220", "EmpKpiEntry?", "Type=Mgt", "Daily Job Execution", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503230", "RptProWiseClOffered?", "Type=SalesDeamnd", "Sales Demand Analysis", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503240", "RptProWiseClOffered?", "Type=SalesDeci", "Sales Decision", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503250", "RptProWiseClOffered?", "Type=Capacity", "Client Capacity Analysis", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503260", "RptMktAppointment?", "Type=AllOffPerformance&UType=Mgt", "Officer Performance(ALL)", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503270", "RptMonTarVsAch?", "Type=dSaleVsColl", "Daily Sales & Collection Status", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503280", "RptEmpMonthWiseEva?", "Type=Mgt", "Month Wise Evaluation", "KPI Management", "False", "False", "False", "False", "False", "" });


            //tblObj.Rows.Add(new Object[] { "7502000", "7502290", "DeptWiseEmpList", "", "Monthly KPI Target", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502300", "ActivitiesCode?", "Type=Dept", "Department & Section", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502310", "ActivitiesCode?", "Type=Activities", "Work List", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502311", "ActivitiesCode?", "Type=Weightage", "Weightage", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502330", "ActivitiesCode?", "Type=DeptList", "Department Wise Work List", "KPI Management", "False", "False" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502340", "EmpKpiEntry04All?", "Type=Mgt", "Daily Job Execution(All)", "KPI Management", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "7502000", "7502350", "EmpKpiEntry04?", "Type=Mgt", "Daily Job Execution", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503360", "RptEmpEvaSheetGen?", "Type=Mgt", "KPI Evaluation (All)", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503370", "RptDeptWiseEmpAcitviteis", "", "Daily Department Work", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503380", "RptDeptWEmpPendActivities", "", "Pending Work-Department Wise", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503390", "RptDeptEvaSheet?", "Type=DeptTarVAch", "Company Summary Report", "KPI Management", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "7503000", "7503400", "EmpStdKpiCR", "", "Monthly KPI Target", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7503000", "7503410", "RptEmpEvaSheet04?", "Type=Mgt", "KPI Evaluation (All)", "KPI Management", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "7502000", "7502420", "KpiSetupLegal", "", "Monthly KPI Target", "KPI Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "7502000", "7502430", "ClientDetail?", "Type=Mgt", "Client Details (MGT)", "KPI Management", "False", "False", "False", "False", "False", "" });


            //#endregion


            #endregion

            #region HRM

            //81 Rec  HR
            //One Time Input
            tblObj.Rows.Add(new Object[] { "8001000", "8001006", "F_81_Hrm/F_81_Rec", "RecHRCodeBook", "", "HR Rec.Code Book", "Recruitment", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8001000", "8001010", "", "RecAccCodeBook?", "InputType=Accounts", "Company Code", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8001000", "8001011", "F_81_Hrm/F_81_Rec", "RecAccSubCodeBook?", "InputType=Accounts", "Applicant Code", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8001000", "8001035", "F_81_Hrm/F_81_Rec", "AppLetCodeBook?", "Type=AppLetter", "Appointment Letter Code", "Recruitment", "False", "False", "False", "False", "False", "" });
           
         
            //82. Appointment
            tblObj.Rows.Add(new Object[] { "8001000", "8001002", "F_21_GAcc", "AccCodeBook?", "InputType=Appointment", "Account Code Book", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8001000", "8001003", "F_21_GAcc", "AccSubCodeBook?", "InputType=Appointment", "Employee Code", "General Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8001000", "8001004", "F_81_Hrm/F_82_App", "HRCodeBook", "", "Information Code Book", "Appointment", "False", "False", "False", "False","False" });
            tblObj.Rows.Add(new Object[] { "8001000", "8001007", "F_81_Hrm/F_82_App", "HRDesigCode", "", "Designation Code Book", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8001000", "8001008", "F_81_Hrm/F_82_App", "EmpAccNoUpload", "", "Employee Ac No Upload", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8001000", "8001009", "F_81_Hrm/F_82_App", "EmpSkillSheet", "", "Employee Skill Grade Setup", "Appointment", "False", "False", "False", "False", "False", "" });


            //Entry Screen
            tblObj.Rows.Add(new Object[] { "8002000", "8002001", "F_81_Hrm/F_81_Rec", "ApplicantInfo", "", "Applicent Information", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002420", "F_81_Hrm/F_81_Rec", "ShortListing?", "Type=SList", "Sort Listing", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002021", "F_81_Hrm/F_81_Rec", "ShortListing?", "Type=IResult", "Interview Result Input", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002822", "F_81_Hrm/F_81_Rec", "ShortListing?", "Type=Fselection", "Final Selection Input", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002025", "", "LetterDefault?", "Type=10002&Entry=", "Letter Of Appointment Create", "Recruitment", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002025", "", "LetterOfAppoinment?", "Type=LCreate", "Letter Of Appointment Create", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002026", "F_81_Hrm/F_81_Rec", "LetterOfAppoinment?", "Type=LRpt", "Letter Of Appointment", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002027", "", "LetterDefault?", "Type=10005&Entry=", "Joining Letter", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002028", "F_81_Hrm/F_81_Rec", "LetterOfAppoinment?", "Type=JCreate", "Joining Letter Create", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002130", "F_81_Hrm/F_81_Rec", "JobAdvertisement?", "Type=Entry", "Job Advertisement Entry", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002031", "F_81_Hrm/F_81_Rec", "JobAdvertisement?", "Type=App", "Job Advertisement Approve", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8001005", "F_21_GAcc", "AccSubCodeBook?", "InputType=Dept", "Department Code", "General Accounts", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002079", "F_81_Hrm/F_82_App", "EmpEntry01?", "Type=Entry", "Employee Personal Information", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002043", "F_81_Hrm/F_82_App", "EmpEntry02?", "Type=Entry", "Employee Personal Information", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002126", "F_81_Hrm/F_82_App", "HREmpEntry?", "Type=Aggrement", "Employee Agreement", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002007", "F_81_Hrm/F_82_App", "ImgUpload?", "Type=Entry", "Employee Image Upload", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002128", "F_81_Hrm/F_82_App", "HREmpEntry?", "Type=Officetime", "Office Time Setup", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002012", "F_81_Hrm/F_82_App", "HREmpEntry?", "Type=shifttime", "Shift/Roster Setup", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002013", "F_81_Hrm/F_82_App", "HRSpecialDaySetup", "", "Special Day Setup", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002120", "F_81_Hrm/F_82_App", "EmpAcaRecord", "", "Academic Record", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002040", "F_81_Hrm/F_82_App", "EmpEntryForm", "", "Employee Entry", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002041", "F_81_Hrm/F_82_App", "EmpBirthDeath?", "Type=EmpBirthdayList", "Employee Birthday", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002042", "F_81_Hrm/F_82_App", "HREmpEntry02?", "Type=Aggrement", "Employee Agreement", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002601", "F_81_Hrm/F_83_Att", "HREmpOffDays", "", "Employee Off Days", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002982", "F_81_Hrm/F_83_Att", "HREmpAbsCt", "", "Absent Count", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002118", "F_81_Hrm/F_83_Att", "EmpPresentOpening?", "Type=LeaveOpening", "Employee Present Opening", "Attendance", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "8002000", "8002116", "F_81_Hrm/F_83_Att", "HRDailyAttenManually?", "Type=Daily", "Daily Attendance - Manually", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002117", "F_81_Hrm/F_83_Att", "HRDailyAttenManually?", "Type=DateWise", "Daily Attendance - Manually(Date Wise)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002010", "F_81_Hrm/F_83_Att", "HRDailyAtten", "", "Daily Attendance - System", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002334", "F_81_Hrm/F_83_Att", "HREmpNotAllow", "", "Overtime Not Allow & OT Deduction", "Attendance", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8002000", "8002320", "F_81_Hrm/F_83_Att", "EmpDaillyAbsent", "", "Daily Absent", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002030", "F_81_Hrm/F_83_Att", "HREmpLWP", "", "LWP Count", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002032", "F_81_Hrm/F_81_Rec", "ManPowerBudgeted?", "Type=Entry", "Man Power Budgeted Entry", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002033", "", "LetterDefault?", "Type=10002&Entry=Apprv", "Letter Of Appointment Appr", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002034", "", "LetterDefault?", "Type=10005&Entry=Apprv", "Joining Letter Appr", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002035", "F_81_Hrm/F_82_App", "HRMShiftPlanBackup?", "Type=Entry", "Shift Plan Backup", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002036", "F_81_Hrm/F_82_App", "HRMShiftChanger", "", "Shift Working Changer", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002037", "F_81_Hrm/F_83_Att", "HRPunchEntryManually", "", "Punch Entry (Manually)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002244", "F_81_Hrm/F_92_Mgt", "AllEmpList", "", "All Employee List", "Recruitment", "False", "False", "False", "False", "False", "" });
            //attendance
            tblObj.Rows.Add(new Object[] { "8002000", "8002045", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary?", "Type=MonthlyLateAtten", "Employee Monthly Late Attendance(Report)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002246", "", "LetterDefault?", "Type=10003&Entry=Offer Letter For General", "Offer Letter Create", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002247", "F_81_Hrm/F_92_Mgt", "AllEmpIDCard", "", "Print Employee ID Card", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002050", "F_81_Hrm/F_83_Att", "EmpMonLateApproval?", "Type=MLateAppDay", "Employee Monthly Late Approval", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002051", "F_81_Hrm/F_83_Att", "EmpMonLateApproval?", "Type=MPunchAppDay", "Monthly Punch Approval", "Attendance", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002052", "", "EmpMonLateApproval?", "Type=MabsentApp", "Monthly Absent  Approval", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003060", "F_81_Hrm/F_83_Att", "RptDeptEmpDailyAttendance?", "Type=DailyAtten", "Employee Daily Attendance- Department", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002070", "F_81_Hrm/F_83_Att", "HREmpMonthlyAtten", "", "Monthly  Attendance - Manually", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002080", "F_81_Hrm/F_83_Att", "EmpEarlyLeaveApproval", "", "Early Leave Approval", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002000", "F_81_Hrm/F_83_Att", "HRDailyAttenUpload", "", "Daily Attendance - Upload", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002101", "F_81_Hrm/F_83_Att", "HRDailyAttenUpload02", "", "Daily Attendance", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002212", "F_81_Hrm/F_83_Att", "EmpMonLateApproval?", "Type=MnthabsentApp", "Monthly Absent Approval", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002213", "F_81_Hrm/F_89_Pay", "EmpMonthSummary?", "Type=salary", "Monthly Attendance Statement", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002214", "F_81_Hrm/F_89_Pay", "EmpMonthSummary?", "Type=salati", "AIT Purpose Salary Statement", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002215", "F_81_Hrm/F_83_Att", "DailyEmpPunchModification", "", "Daily Missing Punch Modification", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002216", "F_81_Hrm/F_83_Att", "HRGeneralDayRemove", "", "General Day Attn. Remove", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002217", "F_81_Hrm/F_83_Att", "HRGeneralDayAdjustment", "", "General Day Adjustment", "Attendance", "False", "False", "False", "False", "False", "" });

            //84 Lea
            tblObj.Rows.Add(new Object[] { "8002000", "8002201", "F_81_Hrm/F_84_Lea", "HREmpLeave?", "Type=FLeaveApp", "Leave Application Form(Manual)", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002352", "F_81_Hrm/F_84_Lea", "HREmpLeave?", "Type=LeaveApp", "Leave Application (Online)", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002563", "F_81_Hrm/F_84_Lea", "HREmpLeave?", "Type=LeaveRule", "Comp. Leave Rule", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002574", "F_81_Hrm/F_84_Lea", "HRLeaveOpening", "", "Earn Leave Opening", "Leave", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8002000", "8002022", "F_81_Hrm/F_84_Lea", "MyLeave?", "Type=User", "Leave Application online (Ind)", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002023", "F_81_Hrm/F_84_Lea", "MyLeave?", "Type=Mgt", "Leave Application online", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002024", "F_81_Hrm/F_84_Lea", "EmpLeaveInfo?", "Type=Leave", "Over all Leave Status", "Leave", "False", "False", "False", "False", "False", "" });
            

            //85  Loan 
            tblObj.Rows.Add(new Object[] { "8002000", "8002501", "F_81_Hrm/F_85_Lon", "EmpLoanInfo", "", "Loan Installment", "Loan", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002992", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=loan", "Loan Deduction", "Loan", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002363", "F_81_Hrm/F_85_Lon", "EmpLoanStatus", "", "Employee Loan Status", "Loan", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002994", "F_81_Hrm/F_85_Lon", "EmpDeducOther", "", "Employee Deduction other", "Loan", "False", "False", "False", "False", "False", "" });

            //86 Allowances
            tblObj.Rows.Add(new Object[] { "8002000", "8002611", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=Overtime", "Overtime Allowance", "Allowances", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002602", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=Holiday", "Holiday Allowance 01", "Allowances", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002603", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=Mobile", "Mobile Bill", "Allowances", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002604", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=Lencashment", "Leave Encashment", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002605", "F_81_Hrm/F_86_All", "HollydayCt", "", "Holiday Allowance 02", "Allowances", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002606", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=CarSub", "Car & Subsistance Allowance", "Allowances", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002607", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=MobLst", "Employee Mobile List", "Allowances", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002608", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=EarnLeave", "Earn Leave Entry", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002609", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=Lencashment02", "Leave Encashment 02", "Leave", "False", "False", "False", "False", "False", "" });

            //87 Transfer
            tblObj.Rows.Add(new Object[] { "8002000", "8002701", "F_81_Hrm/F_87_Tra", "HREmpTransfer", "", "Employee Transfer", "Transfer", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002705", "", "HREmpTransfer02", "", "Employee Transfer(Sales)", "Transfer", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8002000", "8002710", "F_81_Hrm/F_91_ACR", "ACRCodeBook", "", "ACR CodeBook", "ACR", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002711", "F_81_Hrm/F_91_ACR", "EmpEntryACR", "", "ACR Entry", "ACR", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002712", "F_81_Hrm/F_91_ACR", "EmpPerAppraisal", "", "Employee Performance Appraisal", "ACR", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002713", "F_81_Hrm/F_91_ACR", "EmpPerAppraisal_2", "", "Employee Performance Appraisal-2", "ACR", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002714", "", "HRInterComEmpTrns", "", "Inter Company Employee Transfer", "ACR", "False", "False", "False", "False", "False", "" });


            // 93 Annual Increment
            tblObj.Rows.Add(new Object[] { "8002000", "8002750", "F_81_Hrm/F_93_AnnInc", "AnnualIncrement", "", "Annual Increment", "Increment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002751", "F_81_Hrm/F_93_AnnInc", "HrIncrementUpdate", "", "Annual Increment Updated", "Increment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002752", "F_81_Hrm/F_93_AnnInc", "AnnualIncrModification", "", "Annual Increment Modification", "Increment", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8002000", "8002805", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary?", "Type=OvertimeSalary", "Overtime Allowance ", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002806", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary?", "Type=OvertimeSalary02", "Overtime Allowance 02 ", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002808", "F_81_Hrm/F_89_Pay", "EmpAllow", "", "Monthly Allowance", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002809", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=BankPayment", "Bank Payment", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002810", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=arrear", "Arrear Salary", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002811", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=otherearn", "Other Earning", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002812", "F_81_Hrm/F_85_Lon", "EmpOvertime?", "Type=OtherDeduction", "Other Deduction", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002814", "F_81_Hrm/F_89_Pay", "EmpBankSalary?", "Type=Entry", "Salary Transfer Statement", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002815", "F_81_Hrm/F_89_Pay", "EmpBankSalaryEOT?", "Type=Entry", "EOT Salary Transfer Statement", "PayRoll", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "8002000", "8002851", "", "GeneralAccounts?", "tcode=99&tname=Payment Voucher", "Payment Voucher", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002852", "", "GeneralAccounts?", "tcode=99&tname=Deposit Voucher", "Deposit Voucher", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002853", "", "GeneralAccounts?", "tcode=99&tname=Journal Voucher", "Journal Voucher", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002854", "F_81_Hrm/F_90_PF", "AccProFund", "", "PF Account (View/Edit)", "PF Account", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "8002000", "8002855", "", "AccBankRecon", "", "Bank Reconcilation", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002856", "", "AccInterComVoucher", "", "InterCompany Payment", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002857", "", "AccOpening", "", "Accounts Opening", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002859", "F_81_Hrm/F_90_PF", "AccLedger?", "Type=Ledger", "Ledger", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002860", "F_81_Hrm/F_90_PF", "AccLedger?", "Type=SubLedger", " Subsidiary Ledger", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002861", "", "AccControlSchedule", "", "Account Control Schedule", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002862", "", "AccDetailsSchedule", "", "Account Details Schedule", "PF Account", "False", "False", "False", "False" });
            
            
            //tblObj.Rows.Add(new Object[] { "8002000", "8002869", "", "AccCodeBook?", "InputType=Accounts", "Account Code Book", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002870", "F_81_Hrm/F_92_Mgt", "HRGenOffdaySetup", "", "General off Day Setup", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002901", "F_81_Hrm/F_92_Mgt", "HREmpConfirmation?", "Type=Entry", "Employee Confirmation", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002902", "F_81_Hrm/F_92_Mgt", "RetiredEmployee", "", "Employee Separation", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002903", "F_81_Hrm/F_92_Mgt", "EmpPro", "", "Promotion", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002904", "F_81_Hrm/F_92_Mgt", "HREmpConfirmation?", "Type=Rpt", "Employee Confirmation (Date Wise)", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002905", "F_81_Hrm/F_92_Mgt", "HREmpConfirm", "", "Employee Confirmed Report", "Management", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8002000", "8002909", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=joiningRpt", "Joining Report Summary", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002910", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=JoinigdWise", "New Joiners List", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002911", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=EmpList", "Employee List", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002912", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=TransList", "Employee Transfer List", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002913", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=PenEmpCon", "Salary Review List (Six Month)", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002914", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=EmpCon", "Employee Confirmation", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002915", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=Manpower", "Employee Manpower Report", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002917", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=EmpHold", "Employee Hold List", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002918", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=EmpGradeADesig", "Grade & Designation Wise  Salary Detail", "Management", "False", "False", "False", "False" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002919", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=EmpListPic", "Employee List (With Picture)", "Management", "False", "False", "False", "False" });

            tblObj.Rows.Add(new Object[] { "8002000", "8002916", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=SepType", "Employee Separation Report", "Management", "False", "False", "False", "False", "False", "" }); 
            tblObj.Rows.Add(new Object[] { "8002000", "8002920", "F_81_Hrm/F_92_Mgt", "EmpHold?", "Type=EmpSalHold", "Employee Salary Hold ", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002929", "F_81_Hrm/F_92_Mgt", "EmpHold?", "Type=EmpBonHold", "Employee Bonus Hold ", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002923", "F_34_Mgt", "AccUserModule", "", "Module Permission", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002924", "F_81_Hrm/F_89_Pay", "EmpBankSalary?", "Type=Mgt", "Salary Transfer Statement", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8002000", "8002925", "", "UserImage", "", "User Image Upload", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002926", "F_81_Hrm/F_92_Mgt", "AllEmpList", "", "All Employee List", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002928", "F_81_Hrm/F_92_Mgt", "InterfaceAtt", "", "Smartface Attendance", "Management", "False", "False", "False", "False", "False", "" });
            
            tblObj.Rows.Add(new Object[] { "8002000", "8002931", "F_81_Hrm/F_84_Lea", "EmpLvApproval?", "Type=App", "HR Leave Smartface", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002932", "F_81_Hrm/F_84_Lea", "EmpLvApproval?", "Type=Ind", "HR Leave Smartface Ind", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002935", "F_81_Hrm/F_92_Mgt", "OverTimePolicy", "", "Over Time Policy", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002936", "F_81_Hrm/F_92_Mgt", "EmpSettlement?", "Type=Entry", "Employee Settlement Entry", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002937", "F_81_Hrm/F_92_Mgt", "EmpSettlement?", "Type=Approve", "Employee Settlement Approve", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002938", "F_81_Hrm/F_92_Mgt", "RptSettlementStatus", "", "Employee Settlement Top Sheet", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002942", "F_81_Hrm/F_92_Mgt", "DeviceIPSetup", "", "IP Setup", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002939", "F_81_Hrm/F_92_Mgt", "HREmpShiftSetup", "", "Shift Plan Register", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8002000", "8002940", "F_81_Hrm/F_89_Pay", "EmpBankSalaryEOT?", "Type=Mgt", "EOT Salary Transfer Statement", "Management", "False", "False", "False", "False", "False", "" });



            tblObj.Rows.Add(new Object[] { "8002000", "8002941", "F_81_Hrm/F_90_PF", "PFOpening?", "Type=PFOpen", "PF Opening", "PF Account", "False", "False", "False", "False", "False", "" });

            //tblObj.Rows.Add(new Object[] { "8102000", "8002941", "PFOpening?", "Type=PFOpen", "PF Opening", "PF Account", "False", "False", "False" });


            //General Report
            tblObj.Rows.Add(new Object[] { "8003000", "8003015", "F_81_Hrm/F_81_Rec", "RptApplicantInfo", "", "Applicant Information", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003240", "F_81_Hrm/F_81_Rec", "RptRecruitment?", "Type=JobAdvertise", "Job Advertisement", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003241", "F_81_Hrm/F_81_Rec", "RptRecruitment?", "Type=SortListing", "Sort Listing Process", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003242", "F_81_Hrm/F_81_Rec", "RptRecruitment?", "Type=InterviewResult", "Interview Result", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003243", "F_81_Hrm/F_81_Rec", "RptRecruitment?", "Type=FinalSelect", "Final Selection", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003245", "F_81_Hrm/F_82_App", "RptMyInterface?", "Type=empid", "My Smartface", "Recruitment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003009", "F_81_Hrm/F_82_App", "RptEmpInformation?", "Type=Services", "Employee Services  Period", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003030", "F_81_Hrm/F_82_App", "RptEmpInformation?", "Type=EmpAllInfo", "Employee Information", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003007", "F_81_Hrm/F_83_Att", "RptAttendenceSheet", "", "Attendance Information", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003008", "F_81_Hrm/F_83_Att", "DailyAttenSummary?", "Type=AttnSum", "Daily Attendance Summary", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003011", "F_81_Hrm/F_83_Att", "DailyAttenSummary?", "Type=AttnSum2", "Daily Attendance Summary with Skill", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003012", "F_81_Hrm/F_83_Att", "DailyAttenSummary?", "Type=AttnSum3", "Daily Attendance Summary (Direct/Indirect)", "Attendance", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003013", "F_81_Hrm/F_83_Att", "RptEmpPunch", "", "Employee Punch Missing Report", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003014", "F_81_Hrm/F_83_Att", "DailyAttenSummary?", "Type=AttnCatSum", "Daily Attendance Summary (Category Wise)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003017", "F_81_Hrm/F_83_Att", "EmpDaillyAbsent?", "Type=AttnAftrAbs", "Daily Present After Absent ", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003018", "F_81_Hrm/F_83_Att", "EmpDaillyAbsent?", "Type=AttnAftrLeave", "Daily Present After Leave ", "Attendance", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "8003000", "8003040", "F_81_Hrm/F_83_Att", "RptEmpDailyAttendance?", "Type=DailyAtten", "Employee Daily Attendance(Report)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003041", "F_81_Hrm/F_83_Att", "RptEmpDailyAttendance?", "Type=DailyOverTime", "Employee Daily Overtime(Report)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003042", "F_81_Hrm/F_83_Att", "RptEmpDailyAttendance?", "Type=MgtDailyAtten", "Daily Tea Meeting Attendance ", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003044", "F_81_Hrm/F_83_Att", "RptEmpDailyAttendance02?", "Type=DailyAtten", "Employee Daily Attendance(Branch Wise)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003090", "F_81_Hrm/F_83_Att", "RptEmpDailyAttendance?", "Type=AttendanceSummary", "Employee Attendance Summary", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003098", "F_81_Hrm/F_83_Att", "RptEmpMonthlyAbscent?", "Type=abscsummary", "Monthly Absent Summary", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003091", "F_81_Hrm/F_83_Att", "RptEmpJobCard?", "Type=Card2Hrs", "Job Card", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003092", "F_81_Hrm/F_83_Att", "RptEmpJobCard?", "Type=Card3Hrs", "(Job Card)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003093", "F_81_Hrm/F_83_Att", "RptEmpJobCard?", "Type=CardAllHrs", "Job (Time Card)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003094", "F_81_Hrm/F_83_Att", "RptEmpJobCard?", "Type=CardReal", "Employee Job Card (All)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003095", "F_81_Hrm/F_89_Pay", "RptDayWiseOTSheet", "", "Employee Day Wise OT Sheet", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003096", "F_81_Hrm/F_89_Pay", "RptMonthWiseOTSheet", "", "Employee Month Wise OT Sheet", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003097", "F_81_Hrm/F_83_Att", "RptMonAttenSummary?", "Type=DayWise", "Monthly Attn. Count Summary (Day Wise)", "Attendance", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003045", "F_81_Hrm/F_83_Att", "RptMonAttenSummary?", "Type=LineWise", "Monthly Attn. Count Summary (Line Wise)", "Attendance", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003010", "F_81_Hrm/F_84_Lea", "RptHREmpLeave?", "Type=EmpLeaveSt", "Employee Leave Status", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003016", "F_81_Hrm/F_84_Lea", "RptHREmpLeaveReg", "", "Employee Leave Register Report", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003020", "F_81_Hrm/F_84_Lea", "RptEmpLeaveStatus02?", "Type=EmpLeaveStatus", "Employee Leave- Company Wise", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003021", "F_81_Hrm/F_84_Lea", "RptEmpLeaveStatus02?", "Type=MonWiseLeave", "Employee Leave- Month Wise", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003022", "F_81_Hrm/F_84_Lea", "RptHREmpLeave?", "Type=EmpLeaveSt02", "Employee Leave Status 02", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003023", "F_81_Hrm/F_84_Lea", "RptMLPaymentSheet", "", "Maternity Benefit Payment Sheet", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003024", "F_81_Hrm/F_84_Lea", "RptEarnLvBankACStmnt", "", "Earn Leave A/C Statement", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003025", "F_81_Hrm/F_84_Lea", "RptEarnLvPaymentReq", "", "Earn Leave Payment Requisition", "Leave", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003035", "F_81_Hrm/F_83_Att", "RptEmpAttenSummary?", "Type=AttnMon", "Attendance Summary (Month Wise)", "Attendance", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "8003000", "8003752", "F_81_Hrm/F_93_AnnInc", "RptIncrement", "", " Increment Status", "Increment", "False", "False", "False", "False", "False", "" });
            // 97 MIS
            
            //88 Reg/Termination

            //89. PayRoll 
            tblObj.Rows.Add(new Object[] { "8003000", "8003801", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=Salary&Entry=Payroll", "Salary Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003802", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=Bonus&Entry=Payroll", "Festival Bonus", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003803", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=Payslip", "Pay Slip", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003804", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=Signature", "Signature Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003805", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=SalaryHold&Entry=Payroll", "Hold Salary Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003833", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=SalaryReg&Entry=Payroll", "Resign Salary Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003835", "F_81_Hrm/F_89_Pay", "RptDayWiseEmploySalary", "", "Month Wise Employee Salary Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003807", "F_81_Hrm/F_83_Att", "RptHREmpStatus?", "Type=Payroll", "Employee Status", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003806", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=SalaryOT&Entry=Payroll", "Salary OT Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003813", "F_81_Hrm/F_89_Pay", "RptSalarySummary?", "Type=SalSum", "Salary Summary", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003815", "F_81_Hrm/F_89_Pay", "RptSalSummaryDetails", "", "Details Salary Summary", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003816", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=SalSummary", "Salary Summary 02", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003817", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=CashSalary", "Salary Statement (Cash)", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003818", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=SalLACA", "Monthly Loan,Adv.,Cell,Arrear Data Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8003000", "8003819", "", "RptSalSummary02?", "Type=RPTENVELOP", "Envelop Print", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003820", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=CashBonus", "Bonus Sheet (Cash)", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003821", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=BonusSummary", "Bonus Requisition", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003822", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=BonPaySlip", "Pay Slip (Bonus) ", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003830", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=SalSummary02", "Salary Summery 04", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003831", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=SalSummaryOV", "Salary Summery Over Time", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003832", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=BonusSummary02", "Bonus Summary 02", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003834", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=SalSummary02Reg", "Resign Salary Summery 04", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003890", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=SalSummaryEot", "Monthly Salary Summary (EOT)", "PayRoll", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003823", "F_81_Hrm/F_89_Pay", "RptBankStatement?", "Type=Bnkstmntcwise", "Bank Statement - Department Wise", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003824", "F_81_Hrm/F_89_Pay", "RptBankStatement?", "Type=Bnkstmtbnkwise", "Bank Statement - Bank Wise", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003825", "F_81_Hrm/F_89_Pay", "RptSalarySummary?", "Type=SalSum02", "Salary Summary", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003826", "F_81_Hrm/F_82_App", "RptMyInterface?", "Type=Empid", "My Smartface", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003840", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary02?", "Type=OvertimeSal&Entry=Mgt", "EOT Salary Sheet", "Management", "False", "False", "False", "False", "False", "" });
            

            tblObj.Rows.Add(new Object[] { "8003000", "8003904", "F_81_Hrm/F_83_Att", "RptHREmpStatus?", "Type=Approval", "Employee Status(Approval)", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003905", "F_81_Hrm/F_82_App", "RptEmpInformation?", "Type=EmpDyInfo", "Need Base Report", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003906", "F_81_Hrm/F_82_App", "RptEmpInformation?", "Type=EmpDyInfo02", "Need Base Report 01", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003907", "", "RptUserLogDetails", "", "Entry, Edit Record", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003919", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=Salary&Entry=Mgt", "Salary Sheet", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003920", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=SalaryReg&Entry=Mgt", "Resign Salary Sheet", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003928", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=SalaryHold&Entry=Mgt", "Hold Salary Sheet", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003921", "F_81_Hrm/F_92_Mgt", "RptEmpStatus03?", "Type=GradeWiseEmp", "Grade Wise Employee Details", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003922", "F_81_Hrm/F_92_Mgt", "RpEmpIncPro?", "Type=Increment", "Increment (All Employee)", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003927", "F_81_Hrm/F_82_App", "RptMyInterface?", "Type=Empid", "My Smartface", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8003000", "8003950", "", "RpHRtPayroll?", "Type=OvertimeSal&Entry=Mgt", "Over Time Salary", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003955", "F_81_Hrm/F_89_Pay", "RpHRtPayroll?", "Type=Bonus&Entry=Mgt", "Festival Bonus-Approved", "Management", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8003000", "8003956", "", "HREmpConFrmExtnds", "", "Employee Confirmation Extends", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003957", "F_81_Hrm/F_81_Rec", "ManPowerBudgetedVsActual", "", "Man Power Budgeted Vs. Actual", "Appointment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003958", "F_81_Hrm/F_89_Pay", "RptSalSummary02?", "Type=MonSalSum", "Monthly Salary Summery", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003883", "F_81_Hrm/F_89_Pay", "RptSalSummary03", "", "Monthly Salary Summary (Salary, Wages & Over Time)", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003959", "F_81_Hrm/F_89_Pay", "RptEmpEOTSignature?", "Type=EOTSign", "Workers Daily Signature EOT", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003836", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary02?", "Type=OvertimeSal&Entry=PayRoll", "EOT Salary Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003837", "F_81_Hrm/F_82_App", "EmpLineSheet", "", "Employee Floor Line Setup", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003838", "F_81_Hrm/F_89_Pay", "SubsistanceBonus", "", "Subsistance Bonus Allowance", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003839", "F_81_Hrm/F_83_Att", "RptMacAttendence?", "Type=machine", "Machine Wise Daily Attendence log", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003841", "F_81_Hrm/F_83_Att", "RptHREmpStatus?", "Type=All", "Employee Information Report", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003842", "F_81_Hrm/F_82_App", "RptEmpInformation?", "Type=AllDoc", "Employee All Document Print", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003843", "F_81_Hrm/F_81_Rec", "EmpCustDocPrint", "", "Employee Custom Document Print", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003844", "F_81_Hrm/F_82_App", "RptEmpInformation02?", "Type=AllDoc", "Employee All Document Print(FB)", "Management", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003845", "F_81_Hrm/F_82_App", "RptEmpInformation03?", "Type=AllDoc", "Employee Resign & Leave", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003846", "F_81_Hrm/F_82_App", "RptEmpInformation02?", "Type=Datewise", "Employee All Document Datewise Print(FB)", "Management", "False", "False", "False", "False", "False", "" });
            
            //tblObj.Rows.Add(new Object[] { "8003000", "8003847", "F_81_Hrm/F_82_App", "RptempIncrInformation?", "Type=AllDoc", "Employee Increment Letter", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003848", "F_81_Hrm/F_82_App", "RptEmpPromotionLetter?", "Type=Promotion", "Employee Increment & Promotion Letter", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003849", "F_81_Hrm/F_82_App", "EmpAppPart", "", "Employee Application Sheet(FB)", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003850", "F_81_Hrm/F_82_App", "RptEmpRegisterReport?", "Type=registerreport", "Employee Register Report", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003851", "F_81_Hrm/F_82_App", "RptEmpSkillReport?", "Type=skillreport", "Employee Skill Report", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003852", "F_81_Hrm/F_92_Mgt", "EmpStatus02?", "Type=RptAging", "Employee Aging Report", "Appointment", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003860", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary03?", "Type=Overtimesheet&Entry=PayRoll", "Monthly OT Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "8003000", "8003863", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary03?", "Type=Overtimeofsheet&Entry=PayRoll", "Monthly Off Day OT Sheet ", "PayRoll", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003862", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary03?", "Type=Overtimesheet2&Entry=PayRoll", "Monthly Extra OT Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });

            tblObj.Rows.Add(new Object[] { "8003000", "8003864", "F_81_Hrm/F_89_Pay", "RptSalaryRequisiton", "", "Salary Requisition", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003865", "F_81_Hrm/F_89_Pay", "RptBankAccStatement?", "Type=BankAccStatmnt", "Bank A/C Statement", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003867", "F_81_Hrm/F_89_Pay", "RptEmpFoodAllowance?", "Type=BreakFast", "Breakfast Payment Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003868", "F_81_Hrm/F_89_Pay", "RptEmpFoodAllowance?", "Type=NightBill", "Night Bill Payment Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003869", "F_81_Hrm/F_89_Pay", "RptOverTimeRequisition?", "Type=SecWise", "Daily Overtime Requisition Summary", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003866", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary03?", "Type=Overtimesheetcom&Entry=PayRoll", "Monthly Compliance OT Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003870", "F_81_Hrm/F_89_Pay", "RptMonHolidayAllow?", "Type=MonthlyHolidayAllow", "Monthly Holiday Allowance", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003871", "F_81_Hrm/F_86_All", "RptAllCertificate", "", "Employee All Certificate", "PayRoll", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "8002000", "8002825", "F_81_Hrm/F_92_Mgt", "LetterInterface", "", "HR Smartface", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003760", "F_81_Hrm/F_97_MIS", "RptMgtInterface", "", "Management Smartface (HR)", "Increment", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003872", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary03?", "Type=MonIndOTSum", "Month Wise Individual Overtime Summary", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003873", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary03?", "Type=DayTotOTSum", "Day Wise Total Overtime Summary", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003874", "F_81_Hrm/F_92_Mgt", "RpEmpIncPro?", "Type=Promotion", "Periodic Promotion List", "Management", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003875", "F_81_Hrm/F_90_PF", "RptAccProFund", "", "PF Account (Yearly)", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003876", "F_81_Hrm/F_90_PF", "RptMonthlyProFund", "", "Monthly PF Payment Sheet", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003877", "F_81_Hrm/F_89_Pay", "EmpOverTimeSalary03?", "Type=MonSecOTSum", "Monthly OT Summary (Section Wise)", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003878", "F_81_Hrm/F_90_PF", "RptMonthlyProFund?", "Type=MonthlyPF", "Monthly PF", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003879", "F_81_Hrm/F_89_Pay", "RptFinalSettlementPaySheet", "", "Final Settlement Payment Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003880", "F_81_Hrm/F_89_Pay", "RptEmpFoodAllowance?", "Type=TransAllow", "Transport Allowance Payment Sheet", "PayRoll", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003881", "F_81_Hrm/F_90_PF", "RptAccProFund?", "Type=PFAcc", "PF Account", "PF Account", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8003000", "8003882", "F_81_Hrm/F_89_Pay", "RptFestivalBonus", "", "Festival Bonus Report", "PayRoll", "False", "False", "False", "False", "False", "" });

            //Interface
            tblObj.Rows.Add(new Object[] { "8051000", "8051001", "F_81_Hrm/F_92_Mgt", "InterfaceHR", "", "HRM Pending", "Increment- Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "8051000", "8051002", "F_81_Hrm/F_92_Mgt", "InterfaceLeavApp", "", "Leave Smartface", "Management", "False", "False", "False", "False", "False", "" });


            //Dashboard
            tblObj.Rows.Add(new Object[] { "8061000", "8061002", "F_81_Hrm/F_97_MIS", "HRMDashBoard", "", "HRM Smartboard", "Management", "False", "False", "False", "False", "False", "" });


            /////////////////
            #endregion


            tblObj.Rows.Add(new Object[] { "99999", "UserLoginfrm", "", "User Permission", "Admin", "False", "False", "False", "False", "False", "" });
            return tblObj;
        }
        public static DataTable WebObjTableGroupACC()
        {
            DataTable tblObj = new DataTable();
            tblObj.Columns.Add("frmid", Type.GetType("System.String"));
            tblObj.Columns.Add("frmname", Type.GetType("System.String"));
            tblObj.Columns.Add("qrytype", Type.GetType("System.String"));
            tblObj.Columns.Add("dscrption", Type.GetType("System.String"));
            tblObj.Columns.Add("modulename", Type.GetType("System.String"));
            tblObj.Columns.Add("chkper", Type.GetType("System.String"));
            tblObj.Columns.Add("entry", Type.GetType("System.String"));
            tblObj.Columns.Add("printable", Type.GetType("System.String"));

            DataColumn[] keys = new DataColumn[] { tblObj.Columns["frmid"] };
            tblObj.PrimaryKey = keys;
            //35 Group Account
            tblObj.Rows.Add(new Object[] { "35001", "RptAccRecPayment?", "Type=RecAndPayment", "Receipts & Payment A/C", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35002", "RptAccRecPayment?", "Type=BankBalance", "Bank Balance", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35003", "RptAccRecPayment?", "Type=Schedule", "Schedule", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35004", "RptAccRecPayment?", "Type=TrialBalance", "TrialBalance", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35005", "RptAccRecPayment?", "Type=IncomeStatement", "Income Statement", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35006", "RptAccRecPayment?", "Type=BalanceSheet", "Balance Sheet", "Group", "False", "False", "False", "False", "False", "" });


            tblObj.Rows.Add(new Object[] { "35010", "RptGrpAccDailyTransaction?", "Type=GrpDTransaction", "Daily Transaction", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35011", "RptGrpAccDailyTransaction?", "Type=GrpWBudVsAchv", "Working Budget Vs. Achievement", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35020", "RptSalarySummary?", "Type=SalSum", "Salary Summary", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35030", "RptgroupAttendance", "", "Attendance Summary", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35040", "RptMonthlyLate", "", "Monthly Late Attendance Information", "Leave", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35050", "RptWeekPresence", "", "Weekly Presence Graph", "Group", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "35060", "RptEmpMonthLeave", "", "Employee Monthly leave Information", "Leave", "False", "False", "False", "False", "False", "" });
            
            /////////////////////   
            tblObj.Rows.Add(new Object[] { "99999", "UserLoginfrm", "", "User Permission", "Admin", "False", "False", "False", "False", "False", "" });
            return tblObj;

        }


        #region  mgtactiviteis
        public static DataTable WebObjTableMgtActivities()
        {
            DataTable tblObj = new DataTable();
            tblObj.Columns.Add("frmid", Type.GetType("System.String"));
            tblObj.Columns.Add("frmname", Type.GetType("System.String"));
            tblObj.Columns.Add("qrytype", Type.GetType("System.String"));
            tblObj.Columns.Add("dscrption", Type.GetType("System.String"));
            tblObj.Columns.Add("modulename", Type.GetType("System.String"));
            tblObj.Columns.Add("chkper", Type.GetType("System.String"));
            tblObj.Columns.Add("entry", Type.GetType("System.String"));
            tblObj.Columns.Add("printable", Type.GetType("System.String"));

            DataColumn[] keys = new DataColumn[] { tblObj.Columns["frmid"] };
            tblObj.PrimaryKey = keys;
            //34 management

            tblObj.Rows.Add(new Object[] { "34001", "RptMisDailyActiviteis", "", "Management Interface", "Management Smartface", "False", "False", "False", "False", "False", "" });
            tblObj.Rows.Add(new Object[] { "99999", "UserLoginfrm", "", "User Permission", "Management Smartface", "False", "False", "False", "False", "False", "" });
            return tblObj;

        }

        #endregion  mgtactiviteis


        #region  grpmgtinterface
        public static DataTable WebObjTableGrpMgtInterface()
        {
            DataTable tblObj = new DataTable();
            tblObj.Columns.Add("frmid", Type.GetType("System.String"));
            tblObj.Columns.Add("frmname", Type.GetType("System.String"));
            tblObj.Columns.Add("qrytype", Type.GetType("System.String"));
            tblObj.Columns.Add("dscrption", Type.GetType("System.String"));
            tblObj.Columns.Add("modulename", Type.GetType("System.String"));
            tblObj.Columns.Add("chkper", Type.GetType("System.String"));
            tblObj.Columns.Add("entry", Type.GetType("System.String"));
            tblObj.Columns.Add("printable", Type.GetType("System.String"));

            DataColumn[] keys = new DataColumn[] { tblObj.Columns["frmid"] };
            tblObj.PrimaryKey = keys;
            //46 Group Management Interface

            tblObj.Rows.Add(new Object[] { "46001", "RptGrpMisDailyActiviteis", "", "Management Interface", "Management Smartface", "False", "False", "False", "False", "False", "" });

            return tblObj;

        }

        #endregion  grpmgtinterface


        #region
        public static DataTable WebObjTableHR()
        {
            DataTable tblObj = new DataTable();
            tblObj.Columns.Add("frmid", Type.GetType("System.String"));
            tblObj.Columns.Add("frmname", Type.GetType("System.String"));
            tblObj.Columns.Add("qrytype", Type.GetType("System.String"));
            tblObj.Columns.Add("dscrption", Type.GetType("System.String"));
            tblObj.Columns.Add("modulename", Type.GetType("System.String"));
            tblObj.Columns.Add("chkper", Type.GetType("System.String"));
            tblObj.Columns.Add("entry", Type.GetType("System.String"));
            tblObj.Columns.Add("printable", Type.GetType("System.String"));

            DataColumn[] keys = new DataColumn[] { tblObj.Columns["frmid"] };
            tblObj.PrimaryKey = keys;



            //tblObj.Rows.Add(new Object[] { "99999", "UserLoginfrm", "", "User Permission", "False" });
            return tblObj;
        }
        #endregion


        #region Menu
        public static DataTable MenuTable(string ModulID)
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("itemcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemdesc", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemurl", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemtips", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemslct", Type.GetType("System.Boolean"));
            mnuTbl1.Columns.Add("fbold", Type.GetType("System.String"));
            //00 00 00 00 00

            switch (ModulID)
            {
                case "01":   // <asp:ListItem Value="01BGD">01. Mercendising Module</asp:ListItem>
                    Menu01MER(mnuTbl1);
                    break;
                case "02":   // <asp:ListItem Value="01BGD">01. Business Module</asp:ListItem>
                    MenuBUS(mnuTbl1);
                    break;

                case "04":   // <asp:ListItem Value="02PUR">02. Procurement</asp:ListItem>
                    Menu04Sampling(mnuTbl1);
                    break;


                case "03":   // <asp:ListItem Value="02PUR">02. Procurement</asp:ListItem>
                    Menu03CostAndBgd(mnuTbl1);
                    break;
                case "05":   // <asp:ListItem Value="03INV">03. Inventory</asp:ListItem>
                    Menu05PROSHIP(mnuTbl1);
                    break;
                case "06":   // <asp:ListItem Value="03INV">03. Inventory</asp:ListItem>
                    Menu06IE(mnuTbl1);
                    break;
                case "07":   // <asp:ListItem Value="03INV">03. Inventory</asp:ListItem>
                    Menu07Import(mnuTbl1);
                    break;

                case "09":   // <asp:ListItem Value="03INV">03. Inventory</asp:ListItem>
                    Menu09COMMER(mnuTbl1);
                    break;
                case "10":   // <asp:ListItem Value="03INV">03. Inventory</asp:ListItem>
                    Menu10PCURE(mnuTbl1);
                    break;

                case "11":   //Inventory 
                    Menu11INV(mnuTbl1);
                    break;

                 case "13":   // <asp:ListItem Value="03INV">03. Inventory</asp:ListItem>
                    Menu13CENWAR(mnuTbl1);
                    break;

                 case "15":   //Production
                    Menu15PRD(mnuTbl1);
                    break;


                 case "17":   //Inventory
                    Menu17FINF(mnuTbl1);
                    break;

                 case "19":   //Inventory
                    Menu19EXP(mnuTbl1);
                    break;

                 case "20":   //Buyer
                    Menu20BUYER(mnuTbl1);
                    break;

                case "21":  //
                    Menu21GACC(mnuTbl1);
                    break;

                case "23":   
                    Menu23MACC(mnuTbl1);
                    break;

                case "24":  // Audit
                    MenuAudit(mnuTbl1);
                    break;

                case "25":   // 
                    Menu25MKT(mnuTbl1);
                    break;
                case "26": // Alert & Notification
                    MenuAANOT(mnuTbl1);
                    break;

                case "27":   // 
                    Menu27FXT(mnuTbl1);
                    break;

                case "29":  
                    Menu29DACT(mnuTbl1);
                    break;

                case "31":   
                    Menu31MIS(mnuTbl1);
                    break;
                case "32":
                    MenuInputMod(mnuTbl1); // Step of Operation
                    break;

                case "33":   
                    Menu33DOC(mnuTbl1);
                    break;

                case "34":   
                    Menu34MGT(mnuTbl1);
                    break;

                case "35":
                    Menu35GroppACC(mnuTbl1);
                    break;

                case "36":// Group Mis
                    MenuGrMgtInterface(mnuTbl1);
                    break;


                case "47":   // General
                    MenuMyPageGen(mnuTbl1);
                    break;



                case "48":   // My Interface(CR)
                    MenuMyPageCR(mnuTbl1);
                    break;


                case "49":   // My interface Legal
                    MenuMyPageleg(mnuTbl1);
                    break;

                case "58":   // Evalution Part
                    MenuOPM(mnuTbl1);
                    break;

                case "75":   // Management (Sales)
                    MenuMGT(mnuTbl1);
                    break;


                case "80":
                    MenuAllHR(mnuTbl1); //All HRM
                    break;

                case "79":   // KPI
                    MenuAllKPI(mnuTbl1);
                    break;

            }

            mnuTbl1.Rows.Add(new Object[] { "0600000000", "Home", "ASITDefault", "", true });
            mnuTbl1.Rows.Add(new Object[] { "0700000000", "Log Out", "LogIn", "", true });
            //mnuTbl1.Rows.Add(new Object[] { "0602000000", "View Help Documents", "HTMLPage1.htm", "", true });
            //mnuTbl1.Rows.Add(new Object[] { "0603000000", "Change User Login", "LogOutInfo", "", true });
            //javascript:window.close();
            mnuTbl1.Rows.Add(new Object[] { "0800000000", "Exit", "javascript:window.close();", "", true });

            return mnuTbl1;
        }

        #region General
        private static void Menu01MER(DataTable mnuTbl1)
        {
            

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Information Code Book", "F_34_Mgt/SalesCodeBook?Type=Sales", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "01. Customer Details Information", "F_01_Mer/PurCustInfo?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "01. Customer Details Information From Code", "F_01_Mer/PurCustInfo?Type=CusDetails&sircode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
           
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "", "", "", true, "" });
         
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "03. Sample Inquiry List", "F_01_Mer/SampleInquiryLIst?Type=Sample", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "04. Consumption Sheet", "F_01_Mer/ConsumptionSheet?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "05. Pre-Costing Sheet", "F_01_Mer/ConsumptionSheet?Type=PreCosting&actcode=&genno=&sdino=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "06. Order Execution Sheet", "F_01_Mer/OrderExSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "07. Department wise Material Analysis", "F_01_Mer/ConsMatStoring?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "08. Material Group wise Pre-Analysis", "F_01_Mer/ConsMatStoring?Type=Matgrp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "09. Merchandising Process Edit", "F_01_Mer/MerchandProcessEdit", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "10. Consumption Material Size Assorment", "F_01_Mer/ConsumptionSizeAdd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "11. Brand wise Pre-Analysis", "F_01_Mer/ConsMatStoring?Type=Brand", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "12. Convertable Materials Assortment", "F_01_Mer/ConvertMaterialAssortment?Type=ConvMatAsort&actcode=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "13. Create Proforma Invoice", "F_03_CostABgd/SalesContact?Type=Entry&genno=&actcode=&dayid=&sircode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "14. Job Work PO Entry", "F_01_Mer/ConvJobPoEntry?Type=Entry&actcode=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "03. Merchandiser Smartface", "F_01_Mer/RptMerChanInterface?Type=Merchan", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "03. Merchandiser Smartface-PD", "F_01_Mer/RptMerChanInterface?Type=PD", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "03. Sample Inquiry List", "F_01_Mer/SampleInquiryLIst?Type=Sample", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. Plan Vs Achievement - Individual Order", "F_05_ProShip/RptExPlanAchiv", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "08. BOM Approved List", "F_01_Mer/RptOrdAppSheet?Type=BomApp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "06. Periodic Order Status", "F_03_CostABgd/RptLCStuatus?Type=PeriodicOrderSt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "07. Pre Costing Vs Post Costing Report", "F_01_Mer/RptOrdAppSheet?Type=costdiff", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "08. Pending BOM List", "F_01_Mer/RptOrdAppSheet?Type=PendBom", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "09. Costing Summary Report", "F_01_Mer/SampleInquiryLIst?Type=Summary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "10. Buyer Wise Article List", "F_01_Mer/RptOrdAppSheet?Type=BuyerWiseSamp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "11. Proforma Invoice List", "F_01_Mer/RptPfiInvList?Type=PfiRpt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }

        private static void MenuBUS(DataTable mnuTbl1)
        {



            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Business Planning Code", "F_02_Busi/BgdCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Yearly Budget", "F_02_Busi/YearlyPlanningBudget?Type=Yearly", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Yearly Budget", "F_02_Busi/YearlyPlanningBudget?Type=Yearly&rType=Income", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Cash Budget", "F_02_Busi/YearlyPlanningBudget?Type=Yearly&rType=Cash", "", true, "" });


            
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", false, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", false, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });

            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Yearly Budget Amount Basis", "F_02_Busi/YearlyPlanningBudget?Type=BgdAmtBasis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Yearly Budget Qty Basis", "F_02_Busi/YearlyPlanningBudget?Type=BgdQtyBasis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Yearly Budgeted Income Statement", "F_02_Busi/YearlyPlanningBudget?Type=BgdIncome", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        private static void Menu03CostAndBgd(DataTable mnuTbl1)
        {
        

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0301000000", "Entry LC Lease Information", "F_03_CostABgd/MslcGInf?Type=Lease", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "Entry Shipline Information", "F_03_CostABgd/MslcGInf?Type=Lease", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0303000000", "Entry Purchase Projection", "F_03_CostABgd/RptExportStatus?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. General Code", "F_21_GAcc/AccSubCodeBook?InputType=GenCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. Product Code", "F_21_GAcc/AccSubCodeBook?InputType=ProdCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "03. Cost Code", "F_21_GAcc/AccSubCodeBook?InputType=CostCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "04. Master LC Code", "F_21_GAcc/AccCodeBook?InputType=Accounts", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0208000000", "05. LC Information Code", "F_03_CostABgd/LCGenCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            //

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Analysis Sheet", "F_03_CostABgd/StdCostSheet?InputType=CostAnnaSemi&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Production Budget", "F_03_CostABgd/ProdBudget?Type=EntrySemi", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Create Proforma Invoice", "F_03_CostABgd/SalesContact?Type=Entry&genno=&actcode=&dayid=&sircode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0303000000", "01. LC Information", "F_03_CostABgd/EntryMasterLC?Type=0", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0304000000", "02. Order Details", "F_03_CostABgd/EntryMasterLC?Type=1", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0305000000", "03. Resource Selection", "F_03_CostABgd/EntryMasterLC?Type=3", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0306000000", "04. Budget Preparation", "F_03_CostABgd/EntryMasterLC?Type=2&Module=Com", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0308000000", "05. Local Purchase Budget", "F_03_CostABgd/BgdMaster?InputType=BgdMain", "", true, "" });
            
            //mnuTbl1.Rows.Add(new Object[] { "0309000000", "06. Yearly Budget", "F_03_CostABgd/MonthlySalesBudget?Type=Yearly", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0311000000", "05. Order List", "F_03_CostABgd/RptLCDetailsStatus?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0312000000", "06. Resource Requirements", "F_03_CostABgd/BgdPreparation?Type=Entry&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "07. Material Requirement Against Order (BOM)", "F_03_CostABgd/MlcMatReq?Type=Entry&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Budgeted Income Statement", "F_03_CostABgd/MLCOrdrCostRpt", "", true, "" });  
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Concluded Analysis Material List", "F_03_CostABgd/AnalysisMatList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Master LC - Order", "F_03_CostABgd/RptMlcStatus01?Type=LCOrder", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Master LC - BBLC", "F_03_CostABgd/RptMlcStatus01?Type=LCBBLC", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Master LC - Order Details", "F_03_CostABgd/RptMlcStatus01?Type=LCOrderDetails", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0405000000", "05. Master LC Overall Status", "F_03_CostABgd/RptMlcStatus01?Type=LCOverall", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Yearly Budget Amount Basis", "F_03_CostABgd/MonthlySalesBudget?Type=BgdAmtBasis", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Yearly Budget Qty Basis", "F_03_CostABgd/MonthlySalesBudget?Type=BgdQtyBasis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "Transactions Inputs", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0201000000", "Master LC Information", "F_03_CostABgd/EntryMasterLC?Type=0", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0202000000", "Order Details Information", "F_03_CostABgd/EntryMasterLC?Type=1", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0203000000", "Resource Selection", "F_03_CostABgd/EntryMasterLC?Type=3", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0204000000", "Costing", "F_03_CostABgd/EntryMasterLC?Type=2", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0205000000", "BBLC Information", "F_03_CostABgd/EntryBack2BackLC", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0206000000", "Export", "F_03_CostABgd/EntryExportDocs", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0207000000", "LC Marge", "F_03_CostABgd/EntryMLCMrge", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0209000000", "Yarn Requisition", "F_03_CostABgd/EntryYarn", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0210000000", "Entry PC Option", "F_03_CostABgd/PCOption", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0211000000", "Entry Realization", "F_03_CostABgd/EntryRLZ", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "One Time Inputs", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0301000000", "Entry LC Lease Information", "F_03_CostABgd/MslcGInf?Type=Lease", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "Entry Shipline Information", "F_03_CostABgd/MslcGInf?Type=Lease", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0303000000", "Entry Purchase Projection", "F_03_CostABgd/RptExportStatus?Type=Entry", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0305000000", "User Permission", "F_03_CostABgd/UserLoginfrm", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0401000000", "Master LC Order Status", "F_03_CostABgd/MlcStausRpt?Type=LCStatus", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0402000000", "LC Marge Status", "F_03_CostABgd/MlcStausRpt?Type=LCMarge", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0403000000", "Master LC Cost", "F_03_CostABgd/MLCOrdrCostRpt", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0404000000", "BBLC Order", "F_03_CostABgd/BBLCOrderReport", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0405000000", "Fabric Requisition Status", "F_03_CostABgd/RptFabYarn?Type=Fabric", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0406000000", "Yarn Requisition Status", "F_03_CostABgd/RptFabYarn?Type=Yarn", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0407000000", "PC Option Status", "F_03_CostABgd/RptPCOption", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0408000000", "Realization Status", "F_03_CostABgd/EntryRLZ", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0409000000", "Export  Status Information", "F_03_CostABgd/MlcStausRpt?Type=LCexport", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0410000000", "Yarn Purchase", "F_03_CostABgd/MlcStausRpt?Type=YrnPurchase", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0411000000", "Export Document Status", "F_03_CostABgd/RptExportStatus?Type=Report", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0412000000", "Purchase Projection Status", "F_03_CostABgd/RptExportStatus?Type=Projection", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0413000000", "Order, Production Vs Shipment", "F_03_CostABgd/RptOProVsShip?Type=OrdProVsShip&Module=Com", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0414000000", "Production Vs Consumption", "F_03_CostABgd/RptOProVsShip?Type=ProVsCons&Module=Com", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });



        }


        private static void Menu04Sampling(DataTable mnuTbl1)
        {
           

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01 Code Book", "F_34_Mgt/SalesCodeBook?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });




            mnuTbl1.Rows.Add(new Object[] { "0301000000", "03. Ptototype Sample Inquiry", "F_04_Sampling/SamSampleInquiry?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "04. Department Allocation", "F_04_Sampling/DepartmentAlocation?Type=DeptAloc", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "05. PD Book Information Entry", "F_04_Sampling/RptPdBook?Type=PdBookEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "06. Knife Check List", "F_04_Sampling/RptPdBook?Type=KnChkList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. PD Book Report", "F_04_Sampling/RptPdBook?Type=PdBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Date Wise Sample Report", "F_04_Sampling/RptPdBook?Type=SamReport", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Sample Tag Print", "F_04_Sampling/SamTagPrint?Type=TagPrint", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Sample Packing List", "F_04_Sampling/SamTagPrint?Type=PackingList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "05. All Article Wise Report", "F_01_Mer/SampleInquiryLIst?Type=SummaryForPD", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }
        private static void Menu05PROSHIP(DataTable mnuTbl1)
        {

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Planning Code Book", "F_05_ProShip/GeneralCodeBook?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. Master Calendar Setup", "F_05_ProShip/MasterCalendarSetup?Type=mstrcalendar&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "03. Periodic Order Archive Status", "F_03_CostABgd/RptLCStuatus?Type=OrdrArchive", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "02. Production Planning Calendar Setup", "F_05_ProShip/MasterCalendarSetup?Type=plancalendar&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

         
            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Article Capacity Plan", "F_05_ProShip/LCPlanInformation?Type=ArtCapacity", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Production & Shimpent Plan", "F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode=&sircode=", "", true, "" });
        
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "08. Article Wise Lot", "F_05_ProShip/ArticleWiseLot?Type=Entry&genno=&actcode=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "08. Process Base Production Plan", "F_05_ProShip/ProcessBasePlan?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });

         
            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Plan Vs Achievement - Individual Order(Details)", "F_05_ProShip/RptExPlanAchiv", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Plan Vs Achievement - Individual Order(Summary)", "F_05_ProShip/RptExPlVsAchivSumm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Plan Vs Achievement - All Order", "F_05_ProShip/RptExPlanAchivAll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Critical Path of Order", "F_05_ProShip/RptCriticalorder", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "05. Order Plan Report", "F_01_Mer/RptOrdAppSheet?Type=OrdPlan", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "06. Daily Line Wise  Production Report", "F_15_Pro/RptProtarVsAchieve?Type=Protvach", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "07. Order Status Report", "F_05_ProShip/RptOrderStatus?Type=OrdStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Day Wise Process Base Plan", "F_05_ProShip/RptProcessBasePlan?Type=Daywise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Material Master Report", "F_05_ProShip/RptOrderStatus?Type=MatMaster", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }

        private static void Menu06IE(DataTable mnuTbl1)
        {

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Planning Code Book", "F_05_ProShip/GeneralCodeBook?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Article Capacity Plan", "F_05_ProShip/LCPlanInformation?Type=ArtCapacity", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Article Efficiency Setup", "F_05_ProShip/LCPlanInformation?Type=ArtEffiency", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Article SMV Calculation", "F_05_ProShip/LCPlanInformation?Type=SmvCalculation", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. Work Wise Capacity Monitoring", "F_05_ProShip/RptSkilMatrix?Type=WrkWisCapMon", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0401000000", "09. Article Learning Curve Report", "F_05_ProShip/RptProcessBasePlan?Type=ArtclLrnCurv", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "10. Article Wise Summary Sheet", "F_05_ProShip/RptOrderStatus?Type=SMVsheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "11. Employee Skill Matrix Report", "F_05_ProShip/RptSkilMatrix?Type=SkilMatrix", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "12. Article Wise Layout Paper", "F_05_ProShip/LCPlanInformation?Type=ArtLayout", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "13. Work Wise Capacity Monitoring Report", "F_05_ProShip/RptSkilMatrix?Type=RptWrkWisCapMon", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        private static void Menu07Import(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. All L/C Opening", "F_09_Commer/LCAllInfo?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. L/C Receive Entry", "F_09_Commer/LcReceive?Type=Entry&comcod=&actcode=&centrid=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. L/C QC Entry", "F_09_Commer/LcQcRecv?Type=Entry&comcod=&actcode=&centrid=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "03. L/C Costing Entry", "F_09_Commer/LCCostingDetails?Type=Entry&comcod=&actcode=", "", true, "" }); 
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            

            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Import Smartface", "F_09_Commer/RptLCInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "04. L/C Costing Report", "F_09_Commer/RptLCStatus?Type=LCCosting", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "04. L/C Status Report", "F_09_Commer/RptLCPosition?Type=LCPosition", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. L/C Overall Cost", "F_09_Commer/RptSalSummery?Type=LcCost", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. L/C Variance Reports", "F_09_Commer/RptLCStatus?Type=LCVari", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "04. L/C Receive Report", "F_09_Commer/RptSalSummery?Type=LcReceive", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        private static void Menu09COMMER(DataTable mnuTbl1) 
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Information Field", "F_34_Mgt/SalesCodeBook?Type=Procurement", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. Supplier Information", "F_09_Commer/PurSupplierinfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "03. BBLC Opening Code", "F_21_GAcc/AccSubCodeBook?InputType=BBLCCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "04. BBLC Information Code", "F_21_GAcc/AccSubCodeBook?InputType=BBLCinfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "05. Supplier Code", "F_21_GAcc/AccSubCodeBook?InputType=SupplierCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "02. Supplier Information", "F_09_Commer/PurSupplierinfo?Type=SupDetails&sircode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });        
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. BBLC Information", "F_09_Commer/BBLCInfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Work Order ", "F_09_Commer/PurWrkOrderEntry?InputType=OrderEntry&genno=&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Bill Confirmation", "F_09_Commer/PurBillEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. All L/C Opening", "F_09_Commer/LCAllInfo?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "05. L/C Pre Costing Entry", "F_09_Commer/RptLCStatus?Type=LCCostingPreset&actcode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });

            
            
            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Order Wise Supply - Summary", "F_09_Commer/RptOrderStatus?Type=OrderWise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Order Wise Supply - Details", "F_09_Commer/RptOrderWiseSupDet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Supplier Wise BBLC Position", "F_09_Commer/RptOrderStatus?Type=SupWise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Requistion Vs. Received", "F_09_Commer/RptOrderVsReceive?Type=Req", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. BOM Vs. Received", "F_09_Commer/RptOrderVsReceive?Type=OrderVsRec", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "05. Purchase Tracking", "F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "07. Periodic Purchase Tracking", "F_09_Commer/RptDateWiseReq?Type=PeriodPurchase", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Pending Status", "F_09_Commer/RptDateWiseReq?Type=PendingStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Day Wise Purchase", "F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "09. Purchase Summary", "F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=PurSum", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "10. Bill Tracking", "F_09_Commer/BillTracking", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "11. Work Order-Supplier Wise", "F_09_Commer/RptWorkOrderVsSupply?Type=OrdVsSup", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "09. Purchase History-Supplier Wise", "F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=IndSup", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "01. Import Smartface", "F_09_Commer/RptLCInterface", "", true, "" });
            

            mnuTbl1.Rows.Add(new Object[] { "0415000000", "04. L/C Costing Report", "F_09_Commer/RptLCStatus?Type=LCCosting", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "04. L/C Status Report", "F_09_Commer/RptLCPosition?Type=LCPosition", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "04. L/C Overall Cost", "F_09_Commer/RptSalSummery?Type=LcCost", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "04. L/C Variance Reports", "F_09_Commer/RptLCStatus?Type=LCVari", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "04. L/C Receive Report", "F_09_Commer/RptSalSummery?Type=LcReceive", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0420000000", "04. L/C Receive Consignment Wise", "F_09_Commer/RptLCStatus?Type=LCRecvCon", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "04. Season Wise Supply Summary", "F_09_Commer/RptWorkOrderVsSupply?Type=SeasonSummary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "05. Season by Season Supplier's Summary", "F_09_Commer/RptSeasonWiseOrder?Type=SeasonBySeason", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "06. Raw Materials Supply Lead Time", "F_09_Commer/RptWorkOrderVsSupply?Type=LeadTime", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "07. Season Overview Of Materials", "F_09_Commer/RptSeasonWiseOrder?Type=SeasonOverview", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "08. Material Price Variance Report", "F_09_Commer/RptSeasonWiseOrder?Type=PriceVariance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "09. BOM Wise Materials Summary", "F_09_Commer/RptOrderVsReceive?Type=BomMatSummary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "10. Season Wise BOM Status", "F_09_Commer/RptOrderVsReceive?Type=SesonWiseBom", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

         


            



            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "Transactions Inputs", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0201000000", "Received Against BBLC", "F_03_Pro/EntryRecvBBLC", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0202000000", "Entry Production Status", "F_03_Pro/EntryProduction", "", true, "" });          
            //mnuTbl1.Rows.Add(new Object[] { "0204000000", "Material Issue", "F_03_Pro/EntryMatIssue", "", true });
            //mnuTbl1.Rows.Add(new Object[] { "0205000000", "Production Process- Start", "F_03_Pro/ProductionProcess?Type=ProStart", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0206000000", "Production Process", "F_03_Pro/ProductionProcess?Type=ProTransfer", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "One Time Inputs", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0301000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0401000000", "Production Status", "F_03_Pro/ReportMLCProduction?Type=ProStatus", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0402000000", "Production Vs Stock", "F_03_Pro/ReportMLCProduction?Type=ProVsStock", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0403000000", "Order Stock Status", "F_03_Pro/RptOrdrStk", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0404000000", "Production Process Report", "F_03_Pro/ProductionProcess?Type=ProProcess", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0405000000", "Production  Tracking", "F_03_Pro/RptProdProcess", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });

        }
        private static void Menu10PCURE(DataTable mnuTbl1)
        {

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Store Information Code", "F_13_CWare/PRCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "01. Store Information Setup", "F_13_CWare/StoreInformation", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "02. Survey Link", "F_09_Commer/PurMktSurvey?Type=SurveyLink", "", true, "" });

            
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

         
            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Create Requisition", "F_13_CWare/PurReqEntry02?InputType=FxtAstEntry&comcod=&actcode=&genno=", "", true, "" });    
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Requisition Review", "F_13_CWare/PurReqEntry02?InputType=ReqReview&comcod=&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Requisition Checked", "F_13_CWare/PurReqEntry02?InputType=FxtAstAuth&comcod=&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. Supplier Opening Bill", "F_10_Procur/PurOpenigBill?Type=Entry&comcod=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "05. Mold Requisition Entry", "F_13_CWare/PurReqEntry02?InputType=MoldReqEntry&comcod=&actcode=&genno=", "", true, "" });
            
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "Local Purchase", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "06. Requisition Edit", "F_13_CWare/PurReqEntry02?InputType=ReqReview&comcod=&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "07. Purchase Order", "F_10_Procur/PurWrkOrderEntryL?InputType=OrderEntry&genno=&actcode=", "", true, "" });          
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "08. Materials Receive", "F_10_Procur/PurMRREntryLocal?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "09. Bill Confirmation", "F_10_Procur/PurBillEntryLocal?Type=BillEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "10. Comparative Statement-Create", "F_10_Procur/PurMktSurvey02?Type=Create&comcod=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "11. Comparative Statement-Check", "F_10_Procur/PurMktSurvey02?Type=Check&comcod=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "12. Comparative Statement- Audit Check", "F_10_Procur/PurMktSurvey02?Type=Audit&comcod=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "13. Comparative Statement- Approval", "F_10_Procur/PurMktSurvey02?Type=Approved&comcod=&genno=", "", true, "" });     
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "14. Purchase Force Edit", "F_10_Procur/PurProcessEdit", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "15. Requisition Adjustment List", "F_10_Procur/ReqAdjstmntList?Type=ReqAdjst", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "16. Purchase Return Entry", "F_10_Procur/PurchaseReturn?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "17. Purchase Return List", "F_10_Procur/PurReturnList?Type=RetList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });

            

            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Purchase Smartboard", "F_10_Procur/PurInformation", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Purchase Smartface Foreign", "F_10_Procur/RptPurInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "02. Purchase Smartface Local", "F_10_Procur/RptPurInterfaceLocal", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "03. Purchase History-Material Wise", "F_10_Procur/RptMatPurHistory", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. Materials Wise PO Report", "F_09_Commer/RptMataWisePO?Type=MatWisePO", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "05. Weekly Plan Wise Material Report", "F_09_Commer/RptMataWisePO?Type=WeeklyPlanWiseMat", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "06. Supplier Outstanding Statement", "F_10_Procur/RptSupplierDueStatus?Type=SupOutStan", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Master PO Report", "F_09_Commer/RptSeasonWiseOrder?Type=MasterPOR", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. IQC Inspection Report", "F_10_Procur/RptSupplierDueStatus?Type=IQCInspection", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        private static void Menu11INV(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });

            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Stock Label Input- Material", "F_11_RawInv/MiniStockInput?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

          

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Materials Receive", "F_15_Pro/PurMRREntry?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Materials QC", "F_10_Procur/PurMRQCEntry?Type=Entry&actcode=&genno=", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Material Issue-Auto", "F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=&actcode=&reptype=NORMAL", "", true });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. Material Issue", "F_11_RawInv/EntryMatIssue", "", true });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "04. Material Issue Supplier Wise", "F_11_RawInv/MaterialIssueSuplierWise?Type=Entry&genno=&actcode=&reptype=NORMAL", "", true });
            
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "05. Transfer Requisition", "F_11_RawInv/StoreMtTrnsReqEntry?Type=Entry&genno=", "", true, "" });
            
            
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "14. Common Material Issue List", "F_11_RawInv/AllIndentIsuList?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "14. Manual Production Material Issue List", "F_15_Pro/ProdPlanTopSheet?Type=ManProdRM", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "08. Material Stock Adjustment Entry", "F_11_RawInv/StockAdjstmnt?Type=Entry&centrid=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "14. Material Stock Adjustment List", "F_15_Pro/ProdPlanTopSheet?Type=MatStockAdj", "", true, "" });
            
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "14. Inter Company Material Transfer", "F_11_RawInv/PurInterComMatTransfer", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "15. Job Work Mat. Trans", "F_11_RawInv/StoreMtTrnsReqEntry?Type=JobTrans&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "15. Material Transfer", "F_11_RawInv/MaterialsTransfer?Type=Material", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Inventory Report - General", "F_11_RawInv/InvReport?InputType=General", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Inventory Report - Quantity Basis", "F_11_RawInv/InvReport?InputType=QuantityB", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Inventory Report - Amount Basis", "F_11_RawInv/InvReport?InputType=AmountB", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. Materials History ", "F_11_RawInv/RptIndProStock?Type=MatHis&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "05. Indent Materials Distribution ", "F_10_Procur/RptSupplierDueStatus?Type=PromMatHis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "06. Warehouse Smartface", "F_11_RawInv/RptWareHouseInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Periodic Material Unused Report", "F_11_RawInv/InvReport?InputType=MatUnused", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Article Wise Material Stock", "F_11_RawInv/InvReport?InputType=OrdwiseStk", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "09. Materials Transfer Smartface", "F_11_RawInv/RawMattInterface", "", true, "" });
     
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "10. Materials Transfer Report", "F_11_RawInv/RptMaterialTrans?Type=MatTransfer", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000","", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }
        private static void Menu13CENWAR(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Create Requisition", "F_13_CWare/PurReqEntry02?InputType=FxtAstEntry&comcod=&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "02. Material Issue", "F_13_CWare/MatTransfer02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Materials Store (Central)", "F_13_CWare/CentralStore", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }

        private static void Menu15PRD(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

          

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Production Process- Start", "F_15_Pro/ProductionProcess?Type=ProStart&actcode=&genno=&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Production Process", "F_15_Pro/ProductionProcess?Type=ProTransfer&actcode=&genno=&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Production Requisition", "F_15_Pro/ProdReq?Type=Entry&actcode=&genno=", "", true, "" }); 
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. Production Target", "F_15_Pro/EntryProTarget", "", true, "" }); 
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "05. Production Entry", "F_15_Pro/EntryProduction", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "05. Goods Received Entry", "F_15_Pro/ProdEntry?Type=Entry&actcode=&genno=&sircode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "06. Production Plan Top Sheet-1", "F_15_Pro/ProdPlanTopSheet?Type=PlanNo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "07. Production Additional Requisition", "F_15_Pro/AddProdReq?Type=addreq&actcode=&genno=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "08. Production Plan Top Sheet-2", "F_15_Pro/ProdPlanTopSheet?Type=Datewise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "09. Production IQC Check", "F_15_Pro/ProductionProcess?Type=ProQc&actcode=&genno=&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "09. Finish Goods Adjustment", "F_15_Pro/ProductionManually?Type=FGAdjst&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "08. Manual Production Topsheet", "F_15_Pro/ProdPlanTopSheet?Type=ManProd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "10. Production Process Edit", "F_15_Pro/ProProcessEdit", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "11. Floor Material Issue Entry", "F_15_Pro/FloorMatIssue?Type=Entry&genno=&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "05. Process Wise Product", "F_15_Pro/ProcWiseProd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "11. Materials Return in WIP Entry", "F_15_Pro/MatRetInWIP?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "12. Materials Return in WIP Approved", "F_15_Pro/MatRetInWIP?Type=Approved", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "07. Common Mat. Requisition", "F_15_Pro/AddProdReq?Type=commonreq&actcode=&genno=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "07. Recutting Additional Mat. Requisition", "F_15_Pro/AddProdReq?Type=ReCutMatReq&actcode=&genno=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "01. Production Status-All (Order Wise)", "F_15_Pro/RptProtarVsAchieve?Type=RptAllPro", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Production Status (Order Wise)", "F_15_Pro/RptProtarVsAchieve?Type=IndProduc", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Daily Line Wise  Production Report", "F_15_Pro/RptProtarVsAchieve?Type=Protvach", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Day Wise Production", "F_15_Pro/ReportMLCProduction?Type=ProStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "05. Order Stock Status", "F_15_Pro/RptOrdrStk", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "06. Production Process (Individual Order)", "F_15_Pro/ProductionProcess?Type=ProProcess&actcode=&genno=&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Production  Process (All Order)", "F_15_Pro/RptProdProcess", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "09. Periodic Production Status", "F_03_CostABgd/RptLCStuatus?Type=PeriodicProdSt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "10. Order, Production Shipment - All Orders", "F_15_Pro/RptOrderProShipAll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "12. Daily Production Balance Sheet", "F_15_Pro/RptProduction?Type=BalSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "13. Size Production Balance Sheet", "F_15_Pro/RptProduction?Type=SizeBalSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "14. Daily Quality And Productivity Report", "F_15_Pro/RptProduction?Type=QltyNdPrd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "15. Monthly Production Analytical Report", "F_15_Pro/RptProduction?Type=ProductionReport", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "Production Materials Reports", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Production Vs Consumption", "F_03_CostABgd/RptOProVsShip?Type=ProVsCons&Module=Com", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "10. Day Wise Material Consumption Report", "F_15_Pro/RptProductionConsumption?Type=Daywise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "01. Date Wise Material Issue", "F_15_Pro/DateWiseMatIssue?Type=DateWise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "QC/QA Reports", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0421800000", "16. Defect Pareto Chart", "F_15_Pro/RptProduction?Type=DefParChart", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421900000", "17. Order-Defect Reject/Repair Report", "F_15_Pro/RptProduction?Type=OrderDefect", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }

        private static void Menu17FINF(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Product Received", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Product Issue", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "01. Finish Goods Inspection Data Entry", "F_17_GFInv/FGInspectionEntry?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });

            
            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. FG Inventory Report - General", "F_17_GFInv/FGInvReport?InputType=General", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Location Wise Inventory Report", "F_17_GFInv/FGInvReport?InputType=Location", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. FG Inventory Report - Amount Basis", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "02. Receive And Shipment Summary Report", "F_17_GFInv/FGInvReport?InputType=Summary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "","", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }

        private static void Menu19EXP(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });


          

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
           // mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Export", "F_19_EXP/EntryExportDocs", "", true, "" });
          //  mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Export Realization & Incentive", "F_19_EXP/ExportRLZ", "", true, "" });
          //  mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Sales Interface", "F_19_EXP/SalesInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. Export New", "F_19_EXP/ExportMgt?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "05. Collection", "F_19_EXP/AllCollection?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "06. Day Wise Sales Entry", "F_19_EXP/DayWiseSalesEntry?Type=SalEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "07. L/C General Information", "F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "08. Delivery Challan Entry", "F_19_EXP/DelvChallan?Type=Entry&actcode=&genno=&sircode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "09. Sample Export Entry", "F_19_EXP/ExportSample?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "10. Create Packing List", "F_19_EXP/CreatePackList?Type=Entry&actcode=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "11. Export Return", "F_19_EXP/ExportReturn?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "12. Export Return List", "F_19_EXP/ExportRetList?Type=ExpList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


         
            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Export Document", "F_19_EXP/EntryExportDocs", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Export Realization & Incentive", "F_19_EXP/ExportRLZ", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Export  Status Information", "F_03_CostABgd/MlcStausRpt?Type=LCexport", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Export Document Status", "F_19_EXP/RptExportStatus?Type=Report", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0405000000", "05. Purchase Projection Status", "F_03_CostABgd/RptExportStatus?Type=Projection", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "06. Order, Production Vs Shipment", "F_03_CostABgd/RptOProVsShip?Type=OrdProVsShip&Module=Com", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "07. Sales- Realization Certificate", "F_19_EXP/SalesRealCertificate", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "08. Day Wise Sales Report", "F_19_EXP/DayWiseSalesEntry?Type=SalRep", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "09. Export Smartface", "F_19_EXP/RptExportInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "10. Day wise Shipment Details Report", "F_19_EXP/RptDaywiseShipment?Type=shipment", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "11. Day Wise Export Statement", "F_19_EXP/RptDaywiseShipment?Type=summary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "12. Export Statement Unrealization Report", "F_19_EXP/RptDaywiseShipment?Type=unrealization", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "13. Day Wise Shipment Plan Summary", "F_19_EXP/RptDaywiseShipment?Type=ShipmentPlan", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "14. Incentive Declaration Report", "F_19_EXP/RptDaywiseShipment?Type=IncntvDclr", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }
        private static void Menu20BUYER(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });




            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
     
        private static void Menu21GACC(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Accounts Code", "F_21_GAcc/AccCodeBook?InputType=Accounts", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. Details Code", "F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "03. Materials Code Opening", "F_21_GAcc/AccResourceCode?Type=Matcode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "04. Material Code book with Opening stock", "F_21_GAcc/AccResourceCodeOpnStk?Type=Matcode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "05. Material Price Summary", "F_21_GAcc/AccResourceCode?Type=MatPriceSumm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "08. Accounts Opening", "F_21_GAcc/AccOpening", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "10. Bank Limit Information", "F_21_GAcc/AccBankLimit", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "11. Working Budget", "F_21_GAcc/AccMonthlyBgd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

           

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "Voucher Entry", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "01. Voucher 360 <sup>0", "F_21_GAcc/AllVoucherTopSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "01. Voucher Entry", "F_21_GAcc/GeneralAccounts?Mod=Accounts&vounum=", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0304000000", "08. Supplier Payment Voucher", "F_21_GAcc/SuplierPayment?tcode=99&tname=Payment Voucher&Mod=Accounts", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0306000000", "06. Post Dated Cheque(Issue)", "F_21_GAcc/AccPayment?tcode=99&tname=Payment Voucher&Type=Acc", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "06. Post Dated Cheque(Received)", "F_21_GAcc/AccPayment?tcode=99&tname=Deposit Voucher&Type=Acc", "", true, "" });
            

            mnuTbl1.Rows.Add(new Object[] { "0309000000", "Voucher Update", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "01. Local Purchase Voucher", "F_21_GAcc/AccPurchase?Type=Entry&comcod=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "01. Local Purchase Voucher", "F_21_GAcc/AccPurchase?Type=Entry&comcod=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "11. Import Purchase Voucher", "F_21_GAcc/AccPurchaseFor?Type=Entry&comcod=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "13. Issue Update", "F_21_GAcc/AccIsuUpdate?Type=Entry&comcod=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "14. Production Update", "F_21_GAcc/AccProductionJV?Type=Entry&comcod=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "14. Production Manual Update", "F_21_GAcc/AccProductionJVManual?Type=Entry&comcod=&genno=&date=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "14. Account Indent Update", "F_21_GAcc/AccIndentUpdate?Type=Entry&comcod=&genno=&date=", "", true, "" });
           

            mnuTbl1.Rows.Add(new Object[] { "0316000000", "02. Export Bill Update", "F_21_GAcc/AccIncomeOfOrd?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "03. Transfer Journal", "F_21_GAcc/AccTransfer?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "04. InterCompany Payment", "F_21_GAcc/AccInterComVoucher", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "05. Post Dated Cheque Update(Issue)", "F_21_GAcc/AccPayUpdate?Type=AccIsu", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "06. Post Dated Cheque Update(Received)", "F_21_GAcc/AccPayUpdate?Type=AccRec", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "07. Bank Reconcilaition", "F_21_GAcc/AccBankRecon?Type=Acc", "", true, "" });
            
            //Edited
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "Requisition Information", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "01. Create Fund Requisition", "F_34_Mgt/OtherReqEntry?Type=OreqEntry&actcode=&genno=&comcod=&date1=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "02. Fund Requisition First Approval", "F_34_Mgt/OtherReqEntry?Type=OreqApproved&actcode=&genno=&comcod=&date1=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "03. Fund Requisition Approval Print", "F_34_Mgt/OtherReqEntry?Type=OreqPrint&actcode=&genno=&comcod=&date1=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "04. Edit Fund Requisition", "F_34_Mgt/OtherReqEntry?Type=OreqEdit&actcode=&genno=&comcod=&date1=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });




            mnuTbl1.Rows.Add(new Object[] { "0330000000", "C.03 Receipts Information", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "01. List Of Post Dated Cheque", "F_21_GAcc/RptBankCheque", "", true, "" }); /*?Type = PostChqInHand*/
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "02. Cash/Cheque In Hand Print", "F_21_GAcc/RptBankCheque?Type=ChqInHand", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "03. Cheque Deposit Print", "F_21_GAcc/RptBankCheque?Type=ChquedepPrint", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "04. Collection History", "F_21_GAcc/RptBankCheque?Type=CollChqSt", "", true, "" });
            
           
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });





            //Report

            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "C.01 General Reports", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "01. Trial Balance", "F_21_GAcc/AccTrialBalance?Type=Mains", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "01. General Ledger", "F_21_GAcc/AccLedgerAll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "03. Subsidiary Ledger", "F_21_GAcc/AccLedger?Type=SubLedger", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. Accounts Control Schedule-01", "F_21_GAcc/AccControlSchedule?Type=Type01", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "05. Accounts Control Schedule-02", "F_21_GAcc/AccControlSchedule?Type=Type02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "06. Accounts Details Schedule", "F_21_GAcc/AccDetailsSchedule", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. LC Report", "F_21_GAcc/AccFinalReports?RepType=PS", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Special Ledger", "F_21_GAcc/RptAccSpLedger?Type=DetailLedger", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "09. Income Statement - Budget Vs. Actual", "F_21_GAcc/RptLCVariance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "10. Variance Analysis", "F_21_GAcc/IncomeReduced", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "11. Income Statement(Individual Order)", "F_21_GAcc/AccFinalReports?RepType=IPRJ", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "12. Transaction Search - 02", "F_21_GAcc/RptTransactionSearch02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "13. Transaction Search", "F_21_GAcc/RptAccTranSearch", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "05. Notes:Financial Postion", "F_21_GAcc/AccTrialBalance?Type=Details", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "06. Notes:Comprehensive Income", "F_21_GAcc/AccTrialBalance?Type=INDetails", "", true, "" });

            
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "C.04 Payments Inforamtion", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "01. Voucher Print", "F_21_GAcc/TransectionPrint?Type=AccVoucher&Mod=Accounts", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "02. Cheque Print", "F_21_GAcc/TransectionPrint?Type=AccCheque&Mod=Accounts", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "03. Post Dated Cheque Print", "F_21_GAcc/TransectionPrint?Type=AccPostDatChq", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "04. Cheque Issued ", "F_21_GAcc/RptBankCheque?Type=ToDayIssChq", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "05. Cheque History", "F_21_GAcc/RptBankCheque?Type=PayChqCl", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "06. BBLC Payment Status", "F_09_Commer/RptBBLCPayStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "07. Cheque Issued- Group Wise", "F_21_GAcc/RptAccPayUpdate?Type=GroupWiseChqIssued", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "08. Day Wise Issued(Cheque Date)", "F_21_GAcc/RptAccPayUpdate?Type=ChqIsssued", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "09. PDC Issue Status", "F_21_GAcc/RptAccPDCStatus?Type=DayWisePDC", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0427000000", "C.02 Receipts & Payments", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "02. Receipts & Payment(Honoured)", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=RecPay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "01. Cash & Bank Transaction", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=DTran", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "02. Daily transaction", "F_21_GAcc/RptAccDayTransData", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "03. Fund Flow", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=Fflow", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "04. Bank Position", "F_21_GAcc/AccTrialBalance?Type=BankPosition", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "05. Bank Position 02", "F_21_GAcc/AccTrialBalance?Type=BankPosition02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "06. Transaction with Post Dated Cheque", "F_21_GAcc/RptAllAccDTransaction", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "07. Bank Reconcilaition Statement", "F_21_GAcc/RptAccDTransBankSt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "08. Bill Register", "F_15_DPayReg/BillRegInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "09. Account Online Payment", "F_15_DPayReg/AccOnlinePaymnt?Type=Entry&comcod=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "09. BILL STATUS INFORMATION", "F_15_DPayReg/RptBillStatusInf?Type=Entry&comcod=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "09. Accounts Smartface", "F_23_MAcc/AccountInterface", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0440000000", "06. General Bill Smartface", "F_14_DPayReg/GenBillInterface", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0441000000", "07. Supplier Tax & Vat", "F_21_GAcc/SupCustTaxVat?Type=Details", "", true, "" });

           

            mnuTbl1.Rows.Add(new Object[] { "0442000000", "07. Cheque Register", "F_21_GAcc/RptChequeIssuedList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "08. Supplier Overall Position", "F_21_GAcc/RptAccSpLedger?Type=ASPayment", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "08. General Requisition Report", "F_21_GAcc/RptGeneralReport", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "08. Bill Register Report", "F_21_GAcc/RptBillRegister", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        

        }

        private static void Menu23MACC(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Accounts Smartboard", "F_23_MAcc/AccDashBoard", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });




            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "01. Ledger-02", "F_21_GAcc/AccLedger?Type=Ledger&RType=MLedger", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "02. Receipts & Payment(Actual)", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=IssuedVsCollect", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "03. Receipts & Payment(Honoured)", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=RecPay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "04. Trial Balance", "F_21_GAcc/AccTrialBalance?Type=Mains", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "05. Trial Balance (Consolidated)", "F_21_GAcc/AccTrialBalance?Type=TBConsolidated", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "06. Trial Balance 2", "F_31_Mis/ProjTrialBalanc?Type=TrailBal2", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "07. Head Office Trial Balance", "F_21_GAcc/AccTrialBalance?Type=HOTB", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "08. Details Balance Sheet", "F_21_GAcc/AccTrialBalance?Type=Details", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "09. Project Trial Balance", "F_31_Mis/ProjTrialBalanc?Type=PrjTrailBal", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "10. Statement of Cash Flow", "F_21_GAcc/RptBankCheque?Type=CashFlow", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "11. Statement of Fund Flow", "F_21_GAcc/RptBankCheque?Type=FundFlow", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "12. Balance Confirmation", "F_21_GAcc/AccTrialBalance?Type=BalConfirmation", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });



        }
        public static void MenuAudit(DataTable mnuTbl1)
        {


            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Security & Exchanges Commision Code", "F_24_Audit/MisSECCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "11. Compliance of SEC", "F_19_Audit/EntrySEC", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Project Budget Approval", "F_04_Bgd/BgdPrjAna?InputType=BgdSub", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Land Overhead & Other Budget Approval", "F_04_Bgd/BgdMaster?InputType=BgdSub", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Construction Level", "F_04_Bgd/BgdLevelRate?Type=Level", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "05. Bill Confirmation", "F_14_Pro/PurBillEntry?Type=BillEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0407000000", "06. Daily transaction", "F_21_GAcc/RptAccDayTransData", "", true, "" });//&&TrMod=DTran
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Receipts & Payment(Honoured)", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=RecPay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Day Wise Sales", "F_22_Sal/RptSaleSoldunsoldUnit?Type=RptDayWSale", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "09. Day Wise Collection", "F_22_Sal/RptTransactionSt?Type=TransDateWise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "10. Registration Clearence", "F_22_Sal/RptSalInterest?Type=registration", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0412000000", "11. Compliance of SEC", "F_19_Audit/EntrySEC", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });



        }

        private static void Menu25MKT(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Client Code", "F_25_Mkt/ProsclntCodeBookInd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. Letter Creation", "F_25_Mkt/MktTeamCodeBook?Type=MktLetter", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "03. Basic Information Field", "F_25_Mkt/MktGenCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "04. Payment ProPosal Code", "F_22_Sal/SalesPaymentCodeBook?Type=Mkt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Client Details Information", "F_25_Mkt/MktClientInfo?Type=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. New Client Discussion", "F_25_Mkt/ToDaysAppointment?Type=NewClient&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Previous Client Discussion", "F_25_Mkt/ToDaysAppointment?Type=PreClient&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. Sales Permission", "F_25_Mkt/MktSalsPaymentPro", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "Management Input", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "04. Client Details Information", "F_25_Mkt/MktClientInfo?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "05. New Client Discussion", "F_25_Mkt/ToDaysAppointment?Type=NewClient&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "06. Previous Client Discussion", "F_25_Mkt/ToDaysAppointment?Type=PreClient&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Todays Discussion", "F_25_Mkt/RptMktAppointment?Type=Todaysdis&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Client History", "F_25_Mkt/RptMktAppointment?Type=DiscussHis&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Next Appointment", "F_25_Mkt/RptMktAppointment?Type=NextApp&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Officer's Performance", "F_25_Mkt/RptMktAppointment?Type=OffPerformance&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "05. Client Offered Individual Unit", "F_25_Mkt/RptClOfferedInUnit", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "06. Customer Note Sheet", "F_22_Sal/RptSalInterest?Type=CustNoteSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Customer Application", "F_22_Sal/RptSalInterest?Type=CustApp", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Send Letter", "F_25_Mkt/RptMktAppointment?Type=ClientLetter&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "09. Send Letter(Online)", "F_25_Mkt/RptMktAppointment?Type=SendOnlineLetter&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "10. Prospective Client", "F_25_Mkt/RptMktAppointment?Type=ProsClient&UType=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "11. Client's Birthday", "F_25_Mkt/RptFindClient?Type=ClientBrthDay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "12. Client's Marriage Day", "F_25_Mkt/RptFindClient?Type=ClientMrgDay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "Management Reports", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "13. Todays Discussion", "F_25_Mkt/RptMktAppointment?Type=Todaysdis&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "14. Client History", "F_25_Mkt/RptMktAppointment?Type=DiscussHis&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "15. Next Appointment", "F_25_Mkt/RptMktAppointment?Type=NextApp&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "16. Officer's Performance", "F_25_Mkt/RptMktAppointment?Type=OffPerformance&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "17. Sales Performance", "F_25_Mkt/RptMktAppointment?Type=SalePerformance&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "18. Send Letter", "F_25_Mkt/RptMktAppointment?Type=ClientLetter&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "19. Send Letter(Online)", "F_25_Mkt/RptMktAppointment?Type=SendOnlineLetter&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "20. Prospective Client", "F_25_Mkt/RptMktAppointment?Type=ProsClient&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

        }
        private static void MenuAANOT(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0200000000", " ", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });







            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Alert & Notification", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Bussiness Planning", "F_26_Alert/GenPage?Type=01", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Merchandising", "F_26_Alert/GenPage?Type=02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. Cost & Budget", "F_26_Alert/GenPage?Type=03", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "04. Production & Shipment Plan", "F_26_Alert/GenPage?Type=04", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "05. Finance Module", "F_26_Alert/GenPage?Type=05", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "06. Commercial", "F_26_Alert/GenPage?Type=06", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "07. Raw Material Inventoryn", "F_26_Alert/GenPage?Type=07", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "08. Central Wire House", "F_26_Alert/GenPage?Type=08", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "09. Production Monitoring", "F_26_Alert/GenPage?Type=09", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "10.  Finished Goods Inventory", "F_26_Alert/GenPage?Type=10", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "11.  Export Status", "F_26_Alert/GenPage?Type=11", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "12. General Accounts", "F_26_Alert/GenPage?Type=12", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "13. Management Accounts", "F_26_Alert/GenPage?Type=13", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "14. Marketing", "F_26_Alert/GenPage?Type=14", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "15. Fixed Assets Management", "F_26_Alert/GenPage?Type=15", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "16. Daily Activities Evaluation", "F_26_Alert/GenPage?Type=16", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "17. MIS Module", "F_26_Alert/GenPage?Type=17", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "18. Documentation Module", "F_26_Alert/GenPage?Type=18", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "19. HR Management", "F_26_Alert/GenPage?Type=19", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "20. All Module", "F_26_Alert/GenPage?Type=All", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0400000000", " ", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }
        private static void Menu27FXT(DataTable mnuTbl1)
        {

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Fixed Assets Code Book", "F_27_Fxt/FxtAsstCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });


            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Fixed Asset Entry", "F_27_Fxt/FxtAssetRegister", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. General Information", "F_27_Fxt/FxtAstGinf", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "02. Transfer", "F_27_Fxt/FxtAsstTransfer", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0304000000", "03. Rent Bill", "F_27_Fxt/RptFxtAsstBillRent", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "03. Depreciation Charge", "F_27_Fxt/DepreciationCharge", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "04. Depreciation Charge(Calculation)", "F_27_Fxt/EntryDepCharge", "", true, "" });
       
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });





            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });

            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Fixed Asset Report", "F_27_Fxt/RptFixAsset02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Fixed Assets Status", "F_27_Fxt/RptFxtAsstStatus?Type=Fix", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Schedule Of Fixed Assets", "F_27_Fxt/RptFxtAsstStatus?Type=DepCost", "", true, "" });
         
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }

        private static void Menu29DACT(DataTable mnuTbl1)
        {
            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Business Planning", "F_29_DAct/GenCodeBook?Type=01", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "02. Merchandising", "F_29_DAct/GenCodeBook?Type=02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "03. Cost & Budget", "F_29_DAct/GenCodeBook?Type=03", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "04. Production & Shipment Plan", "F_29_DAct/GenCodeBook?Type=04", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "05. Finance", "F_29_DAct/GenCodeBook?Type=05", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "06. Commercial", "F_29_DAct/GenCodeBook?Type=06", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "07. Raw Material Inventory", "F_29_DAct/GenCodeBook?Type=06", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "08. Central Wire House", "F_29_DAct/GenCodeBook?Type=07", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "09. Production Monitoring", "F_29_DAct/GenCodeBook?Type=08", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "10. Finished Goods Inventory", "F_29_DAct/GenCodeBook?Type=09", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", " 11. Export Status", "F_29_DAct/GenCodeBook?Type=10", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "11. Export", "F_29_DAct/GenCodeBook?Type=11", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "12. Buyer's Smartface", "F_29_DAct/GenCodeBook?Type=12", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "13. Accounts", "F_29_DAct/GenCodeBook?Type=13", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "14. Marketing", "F_29_DAct/GenCodeBook?Type=14", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "15. Fixed Assets", "F_29_DAct/GenCodeBook?Type=15", "", true, "" });
           
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "17. HR", "F_29_DAct/GenCodeBook?Type=17", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "18. Admin Depatrment", "F_29_DAct/GenCodeBook?Type=18", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "19. Utility", "F_29_DAct/GenCodeBook?Type=19", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "20. Customer Service", "F_29_DAct/GenCodeBook?Type=20", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "21. Other Code Book", "F_29_DAct/GenCodeBook?Type=21", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Daily Activities(Department Wise)", "F_29_DAct/EntryDailyActivities?Type=EntryDeptAct", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Daily Activities(Individual Wise)", "F_29_DAct/EntryDailyActivities?Type=EntryIndAct", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });




            //mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        private static void Menu31MIS(DataTable mnuTbl1)
        {


            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "Company Information", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "02. Budgeted Income - Order Wise", "F_31_Mis/RptMisProIncomeExe?Type=BgdIncomeOrderWise", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0204000000", "Accounts Summary", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "03. Sources & Utilization - Cash Basis", "F_31_Mis/RptMisMasterBgd?Type=SrAUtilities", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "04. Sources & Utilization- Acural Basis", "F_31_Mis/RptMisMasterBgd?Type=SrAUtilitiesFF", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "05. Budget VS Expenses-All LC", "F_31_Mis/RptMisMasterBgd?Type=BgdVsExpenses", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "06. Sales VS Collection-All LC", "F_31_Mis/RptMisMasterBgd?Type=SalesVsColection", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "07. Collection VS Expenses-All LC", "F_31_Mis/RptMisMasterBgd?Type=ColVsExpenses", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "08. Head Office overhead-last 12 Month", "F_31_Mis/RptMisMasterBgd?Type=ProExpenses", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "09. Accounts details at a glance", "F_21_GAcc/AccTrialBalance?Type=Details", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "11. Receipts & Payment(Honoured)", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=RecPay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "13. Special Balance Sheet", "F_21_GAcc/AccFinalReports?RepType=SPBS", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "14. Cost Of Fund", "F_31_Mis/RptMisMasterBgd?Type=CostOfFund", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "20. Variance Analysis", "F_21_GAcc/IncomeReduced", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "21. Income Statement - Production Basis", "F_31_Mis/RptInComeStProdBasis", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0224000000", "Financial Statement(IAS & BAS)", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "01. Statement Of Financial Position", "F_21_GAcc/AccFinalReports?RepType=BS", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "02. Statement Of Comprehensive Income", "F_21_GAcc/AccFinalReports?RepType=IS", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "03. Statement Of Share Holder's Equity ", "F_21_GAcc/AccFinalReports?RepType=SHEQUITY", "", true, "" });
          

            mnuTbl1.Rows.Add(new Object[] { "0228000000", "05. Statement Of Cash Flow  ", "F_21_GAcc/RptBankCheque?Type=CashFlow", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "06. Accounting Policies", "F_21_GAcc/RptBankCheque?Type=FinNote", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "07. Explanatory notes to the Financial Statement", "F_21_GAcc/AccTrialBalance?Type=Mains", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "08. Compliance of SEC", "F_24_Audit/EntrySEC", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "09. Financial Results (5 Years)", "F_34_Mgt/EntryFinResult", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "10. Accounting Policies Input", "F_31_Mis/MisFinNoteCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "11. Financial Code Input", "F_07_Fin/FinCodeBook02", "", true, "" }); 
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });






            //mnuTbl1.Rows.Add(new Object[] { "0300000000", "Project Information", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "05. LC Status Report", "F_31_Mis/RptProjectStatus?Type=LCStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "13. Real Inflow & Outflow", "F_21_GAcc/RptRealInOutFlow?Type=RealFlow", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "14. Production Dash Board", "F_13_ProdMon/ProductionInfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0400000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Budgeted Income Statement", "F_03_CostABgd/MLCOrdrCostRpt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Master LC Order Status", "F_03_CostABgd/MlcStausRpt?Type=LCStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. BBLC Order", "F_03_CostABgd/BBLCOrderReport", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", false, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. Production  Tracking", "F_15_Pro/RptProdProcess", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. FG Inventory Report - General", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "09. Order, Production Vs Shipment", "F_03_CostABgd/RptOProVsShip?Type=OrdProVsShip&Module=Com", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "MIS Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "01.Income Statement(On Production Basis)", "F_31_Mis/RptLCProaLossAcount", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "02. LC Status - At a glance", "F_31_Mis/RptLCInfoataglance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "03. Income Statement - Budget Vs. Actual", "F_21_GAcc/RptLCVariance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "04. Variance Analysis", "F_21_GAcc/IncomeReduced", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "05. Order, Production Shipment - All Orders", "F_15_Pro/RptOrderProShipAll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "06. BBLC Payment Status", "F_09_Commer/RptBBLCPayStatus", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0421000000", "16. Cost Details (12 Month)", "F_62_Mis/RptAccIncome?Type=IncomeMonthly", "", true, "" });

            
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

          
        }
        private static void MenuInputMod(DataTable mnuTbl1)
        {

            

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Quotation ID", "F_01_Mer/MerPRCodeBook?BookName=Project", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "04. Accounts Code", "F_21_GAcc/AccCodeBook?InputType=Accounts", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "02. Details Code", "F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "03. L/C Code", "F_34_Mgt/AccProjectCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0211000000", "Other's", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "05. Information Field", "F_34_Mgt/SalesCodeBook?Type=Procurement", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "06. Supplier Information", "F_09_Commer/PurSupplierinfo?sircode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

         
            mnuTbl1.Rows.Add(new Object[] { "0300000000", "Operational Menu", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Business Planning", "F_02_Busi/YearlyPlanningBudget?Type=Yearly&rType=Income", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Pre-Cost Analysis List", "F_01_Mer/RptOrderStatus?Type=DataBank", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Quotation Create", "F_01_Mer/MerLCAnalysis?Type=Create", "", true, "" });
           // mnuTbl1.Rows.Add(new Object[] { "0303000000", "03. CS Create", "F_09_Commer/PurMktSurvey02?Type=Create&genno=", "", true, "" });
           // mnuTbl1.Rows.Add(new Object[] { "0303000000", "04. CS Approval", "F_09_Commer/PurMktSurvey02?Type=Approved&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "05. Pre-Cost Analysis Approval", "F_01_Mer/MerLCAnalysis?Type=Approved&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "06. Create Multi Code", "F_34_Mgt/AccProjectCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "07. Article Capacity Plan", "F_05_ProShip/LCPlanInformation?Type=ArtCapacity", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0307000000", "08. LC Information", "F_03_CostABgd/EntryMasterLC?Type=0", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0308000000", "09. Order Details", "F_03_CostABgd/EntryMasterLC?Type=1", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "10. Analysis Sheet", "F_03_CostABgd/StdCostSheet?InputType=CostAnnaSemi&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "01. Production Budget", "F_03_CostABgd/ProdBudget?Type=EntrySemi", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0311000000", "11. Budget Preparation", "F_03_CostABgd/EntryMasterLC?Type=2&Module=Com", "", true, "" });

           
          



            mnuTbl1.Rows.Add(new Object[] { "0312000000", "Planning", "", "", false, "b" });            
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "12. Production & Shimpent Plan", "F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode=&sircode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0315000000", "Procurement", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "14. Material Requisition", "F_15_Pro/PurReqEntry?InputType=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "15. Requisition Approval", "F_15_Pro/PurReqEntry?InputType=Approval&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "16. BBLC Information", "F_09_Commer/BBLCInfo?InputType=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "18. Materials Receive", "F_15_Pro/PurMRREntry?Type=Entry&actcode=&genno=", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0321000000", "07. Materials QC", "F_10_Procur/PurMRQCEntry?Type=Entry&actcode=&genno=", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0322000000", "19. Bill Confirmation", "F_09_Commer/PurBillEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "20. BBLC Update", "F_21_GAcc/AccPurchase", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "21. Payment Voucher", "F_21_GAcc/GeneralAccounts?Mod=Accounts&vounum=", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0325000000", "Production", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "22. Production Requisition", "F_15_Pro/ProdReq?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "23. Material Issue-Auto", "F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=&actcode=&reptype=NORMAL", "", true });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "21. Line Wise Production Target", "F_15_Pro/EntryProTarget", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "26. Production Process- Start", "F_15_Pro/ProductionProcess?Type=ProStart&actcode=&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "27. Production Process", "F_15_Pro/ProductionProcess?Type=ProTransfer&actcode=&sircode=&date=&dayid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "28. Production Entry", "F_15_Pro/EntryProduction", "", true, "" });
            

            mnuTbl1.Rows.Add(new Object[] { "0333000000", "Export", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "29. Export", "F_19_EXP/EntryExportDocs", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "30. Export Bill Update", "F_21_GAcc/AccIncomeOfOrd?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "31. Export Realization & Incentive", "F_19_EXP/ExportRLZ", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "32. Export Return", "F_19_EXP/ExportReturn?Type=Entry&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "33. Export Return List", "F_19_EXP/ExportRetList?Type=ExpList", "", true, "" });
            
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "34. InterCompany Payment", "F_21_GAcc/AccInterComVoucher", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0339000000", "CS", "", "", false, "b" });

            mnuTbl1.Rows.Add(new Object[] { "0340000000", "08. Comparative Statement-Create", "F_10_Procur/PurMktSurvey02?Type=Create&comcod=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "09. Comparative Statement-Check", "F_10_Procur/PurMktSurvey02?Type=Check&comcod=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "10. Comparative Statement- Audit Check", "F_10_Procur/PurMktSurvey02?Type=Audit&comcod=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "11. Comparative Statement- Approval", "F_10_Procur/PurMktSurvey02?Type=Approved&comcod=&genno=", "", true, "" });

 
           
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });




            mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Report", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Business Plan (12 Month)", "F_02_Busi/YearlyPlanningBudget?Type=BgdIncome", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Proposal Submission", "F_01_Mer/RptMerLCAnalysis?Type=Report&pactcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Budgeted Income Statement", "F_03_CostABgd/MLCOrdrCostRpt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0404000000", "05. Order Data Bank", "F_01_Mer/RptOrderStatus?Type=DataBank", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0405000000", "06. Order, Production Shipment - All Orders", "F_15_Pro/RptOrderProShipAll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "07. LC Status Summary", "F_31_Mis/RptProjectStatus?Type=LCStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "08. Order Status - At a glance(1)", "F_31_Mis/RptLCInfoataglance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "09. Order Status - At a glance(2)", "F_05_ProShip/RptExPlanAchiv", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "10. Inventory Report - General", "F_11_RawInv/InvReport?InputType=General", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "11. FG Inventory Report - General", "F_17_GFInv/FGInvReport?InputType=General", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "12. Daily Line Wise  Production Report", "F_15_Pro/RptProtarVsAchieve?Type=Protvach", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "13. Production Process (Individual Order)", "F_15_Pro/ProductionProcess?Type=ProProcess&actcode=&sircode=&date=&dayid=", "", true, "" });
           
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "14. BBLC Payment Status", "F_09_Commer/RptBBLCPayStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "15. Income Statement - Budget Vs. Actual", "F_21_GAcc/RptLCVariance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "16. Materials History", "F_11_RawInv/RptIndProStock?Type=MatHis&sircode=&date=&dayid=", "", true, "" });




            mnuTbl1.Rows.Add(new Object[] { "0415000000", " Financial Statement", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "01. Statement Of Financial Position", "F_21_GAcc/AccFinalReports?RepType=BS", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "02. Statement Of Comprehensive Income", "F_21_GAcc/AccFinalReports?RepType=IS", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "03. Statement Of Share Holder's Equity", "F_21_GAcc/AccFinalReports?RepType=SHEQUITY", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "04. Statement Of Cash Flow ", "F_21_GAcc/RptBankCheque?Type=CashFlow", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "05. Working Budget Vs. Achievement", "F_21_GAcc/RptAccBudget?Type=WbgdVsAc", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "06. Receipts & Payment", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=RecPay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "07. Trial Balance", "F_21_GAcc/AccTrialBalance?Type=Mains", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0423000000", "08. Daily transaction- All", "F_21_GAcc/RptAccDayTransData", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "09. Daily Transaction- Individual", "F_21_GAcc/RptAccDTransaction?Type=Accounts&TrMod=ProTrans", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "01. Ledger-All", "F_21_GAcc/AccLedgerAll", "", true, "" });
            
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "11. Special Ledger", "F_21_GAcc/RptAccSpLedger?Type=DetailLedger", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "12. Accounts Control Schedule", "F_21_GAcc/AccControlSchedule?Type=Type01", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "13. Accounts Details Schedule", "F_21_GAcc/AccDetailsSchedule", "", true, "" });
            
           
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }

        private static void Menu33DOC(DataTable mnuTbl1)
        {
            mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Document Code Book", "F_33_Doc/DocCodeBook?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Document Entry", "F_33_Doc/DocUpload?Type=Entry&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Show Document Activity", "F_33_Doc/ShowAllDoc?Type=Rpt&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Document Management Smartface", "F_33_Doc/RptDocInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

        }
        private static void Menu34MGT(DataTable mnuTbl1)
        {


            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "03 L/C Code Book", "F_34_Mgt/AccProjectCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "19 Share Equity", "F_34_Mgt/ShareEquity", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "23 General Code Book", "F_34_Mgt/SalesCodeBook?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "24 Currency Code Book", "F_34_Mgt/AccCurCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "24 Currency Conversion", "F_34_Mgt/AccConversion", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "24 Unit Conversion", "F_34_Mgt/UnitConvert", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "20 Display Code", "F_34_Mgt/AccGenCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "21 Code Link(BS)", "F_34_Mgt/CodeLink", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "22 Code Link(CF)", "F_34_Mgt/CodeLinkCf", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "23 Signatory Entry", "F_34_Mgt/SignatoryEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "24 HR Code Link", "F_34_Mgt/CodeLinkHR", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Edit/Delete", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "11. Requisition Approval", "F_13_CWare/PurReqEntry02?InputType=FxtAstApproval&comcod=&actcode=&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "13. Purchase Order Edit", "F_10_Procur/PurWrkOrderEntryL?InputType=OrderEdit&genno=&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "05.Voucher Edit", "F_21_GAcc/GeneralAccounts?Mod=Management&vounum=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "09. Voucher Delete", "F_21_GAcc/TransectionPrint?Type=AccVoucher&Mod=Management", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "01. Post Dated Cheque(Issue)", "F_21_GAcc/AccPayment?tcode=99&tname=Payment Voucher&Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "02. Post Dated Cheque(Received)", "F_21_GAcc/AccPayment?tcode=99&tname=Deposit Voucher&Type=Mgt", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0310000000", "05. Bank Reconcilaition", "F_21_GAcc/AccBankRecon?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "06. LC Status", "F_34_Mgt/MSLCStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "Group Task", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "16. Group Task Report", "F_34_Mgt/RptGroupTask?Type=GTaskRpt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "Management Report", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "11. Management Smartface", "F_34_Mgt/RptAdminInterface", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Setup", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Company Page Permission ", "F_34_Mgt/UserLoginfrmPtl", "", true, "" });
            
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. User Permission", "F_34_Mgt/UserLoginfrm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Cash & Bank Permission", "F_34_Mgt/AccUserCash", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "06. Address Basic Information", "F_34_Mgt/AddressDetails", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "03. Chart of Accounts Permission", "F_34_Mgt/CACodeLink", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "03. Store Permission", "F_34_Mgt/ProjectLink", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. Transaction Limit Day", "F_34_Mgt/Tranlimitdate", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "05. User Image Upload", "F_34_Mgt/UserImage", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0407000000", "06. Entry, Edit & Delete Record", "F_34_Mgt/RptUserLogDetails?Type=Entry&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "07. User Log Information", "F_34_Mgt/RptUserLogStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "08. Auto Database Backup", "F_34_Mgt/DataBackup", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "09. Smartboard All", "F_34_Mgt/DashBoardAll", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0411000000", "05. HR Leave Approval Setup", "F_81_Hrm/F_92_Mgt/HrLeaveApprovalForm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "01. Department Permission", "F_81_Hrm/F_92_Mgt/PayrollLink?Type=Hr", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "01. Job Location Permission", "F_81_Hrm/F_92_Mgt/HREmpJoblocation?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "11. User Module Setup", "F_34_Mgt/AccUserModule", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "01. Production Department Permission", "F_81_Hrm/F_92_Mgt/PayrollLink?Type=Production", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "01. Production Process Permission", "F_81_Hrm/F_92_Mgt/PayrollLink?Type=Process", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "01. Group User Management", "F_34_Mgt/UserfrmGroup", "", true, "" });
          
           
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "12. Mail Receiver Setup", "F_34_Mgt/MailRecvSetUp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });
        }

        //Group
        public static void Menu35GroppACC(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0200000000", "Group Accounts", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Receipts & Payment A/C", "F_35_GrAcc/RptAccRecPayment?Type=RecAndPayment", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. Daily Transaction", "F_35_GrAcc/RptGrpAccDailyTransaction?Type=GrpDTransaction", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "03. Bank Balance", "F_35_GrAcc/RptAccRecPayment?Type=BankBalance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "04. Schedule", "F_35_GrAcc/RptAccRecPayment?Type=Schedule", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "05. Trial Balance", "F_35_GrAcc/RptAccRecPayment?Type=TrialBalance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "06. Income Statement", "F_35_GrAcc/RptAccRecPayment?Type=IncomeStatement", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "07. Balane Sheet", "F_35_GrAcc/RptAccRecPayment?Type=BalanceSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "12. User Permission", "F_35_GrAcc/UserLoginfrm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0220000000", "Group HR", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "01. Attendance Summary", "F_81_Hrm/F_99_MgtAct/RptgroupAttendance", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "02. Weekly Presence Graph", "F_81_Hrm/F_83_Att/RptWeekPresence", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "03. Monthly Late Attendance Information", "F_81_Hrm/F_83_Att/RptMonthlyLate", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "04. Employee Monthly Leave Information", "F_81_Hrm/F_84_Lea/RptEmpMonthLeave", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "05. Salary Summary", "F_81_Hrm/F_89_Pay/RptSalarySummary?Type=SalSum", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0300000000", "Individual Company", "", "", false, "" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Working Budget Vs. Achievement", "F_35_GrAcc/RptGrpAccDailyTransaction?Type=GrpWBudVsAchv", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



         
            mnuTbl1.Rows.Add(new Object[] { "0400000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

        }

        public static void MenuGrMgtInterface(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0200000000", "Individual Company", "", "", false, "" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0300000000", "Group Accounts", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. User Permission", "F_36_GrMgtInter/UserLoginfrm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0400000000", "Report", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Management Smartface", "F_35_GrAcc/RptGrpMisDailyActiviteis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        #endregion

        #region KPI
        private static void MenuMyPageGen(DataTable mnuTbl1)
        {
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. KPI Code Book", "F_47_Kpi/KpiCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. KPI Target Entry", "F_47_Kpi/KpiTargetEntry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Daily Job Execution", "F_39_MyPage/EmpKpiEntry04?Type=General", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });






            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });







        }
        private static void MenuMyPageMar(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0201000000", "" +
                "01. KpiTargetEntry", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Daily Job Execution", "F_39_MyPage/EmpKpiEntry03?Type=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. Kpi Entry", "F_39_MyPage/EmpKpiEntry04?Type=All", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });





            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Month Wise Evaluation", "F_39_MyPage/RptEmpMonthWiseEva03?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Employee History", "F_39_MyPage/RptEmpHistory02?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "02. Individual History", "F_39_MyPage/RptMIS02?Type=EmpHistory&History=Individual", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });







        }

        private static void MenuMyPageCR(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Daily Job Execution", "F_39_MyPage/EmpKpiEntry04?Type=CR", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. Evaluation", "F_39_MyPage/RptEmpEvaSheet04?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "01. Month Wise Evaluation", "F_39_MyPage/RptEmpMonthWiseEva03?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "02. Employee History", "F_39_MyPage/RptEmpHistory02?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "02. Individual History", "F_39_MyPage/RptMIS02?Type=EmpHistory&History=Individual", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });







        }

        private static void MenuMyPageleg(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Daily Job Execution", "F_39_MyPage/EmpKpiEntry04?Type=Legal", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });





            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

        }
        private static void MenuOPM(DataTable mnuTbl1)
        {

            //mnuTbl1.Rows.Add(new Object[] { "0200000000", "One Time Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0300000000", "Transactions Inputs", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. SMS Notification", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "02. E-Mail Notification", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });




            mnuTbl1.Rows.Add(new Object[] { "0400000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        private static void MenuMISLeg(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0200000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });



            mnuTbl1.Rows.Add(new Object[] { "0300000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. Daily Job Execution", "F_39_MyPage/EmpKpiEntry04?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. KPI Evaluation", "F_39_MyPage/RptEmpEvaSheetLeg?Type=Leg", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });


        }
        private static void MenuMGT(DataTable mnuTbl1)
        {

            mnuTbl1.Rows.Add(new Object[] { "0200000000", "Sales", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "01. Control Code", "F_64_Mgt/DeptActivitiesCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "02. Client Code", "F_64_Mgt/GenCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "03. Team Code", "F_64_Mgt/TeamSeriCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "04. Project Code", "F_64_Mgt/ProjectCode?Type=Project", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "05. Project Activation", "F_64_Mgt/EntryProjectActive", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "General", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "01. Department & Section", "F_64_Mgt/ActivitiesCode?Type=Dept", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "02. Work List", "F_64_Mgt/ActivitiesCode?Type=Activities", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "03. Weightage", "F_64_Mgt/ActivitiesCode?Type=Weightage", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "04. Department Wise Work List", "F_64_Mgt/ActivitiesCode?Type=DeptList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "06. Department Wise Employee", "F_64_Mgt/DeptWiseEmpList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "Legal", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "01. Court & Other Activities", "F_64_Mgt/ActivitiesCode?Type=Coutaotheract", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "02. Case Code", "F_64_Mgt/ProjectCode?Type=Case", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0218000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0223000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0224000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "05.Angular JS", "F_64_Mgt/entryAngularjs", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0249000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0250000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0256000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0257000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0258000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0259000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0260000000", "", "", "", true, "" });






            mnuTbl1.Rows.Add(new Object[] { "0301000000", "Target", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "01. Monthly Target(Sales)", "F_47_Kpi/EmpStdKpi", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "02. Monthly Target(General)", "F_64_Mgt/DeptWiseEmpList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "03. Monthly Target(CR)", "F_47_Kpi/EmpStdKpiCR", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "04. Monthly Target(Legal)", "F_21_Kpi/KpiSetupLegal", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "05. Daily Job Execution", "F_39_MyPage/EmpKpiEntry04?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "06. Daily Job Execution(All Department)", "F_39_MyPage/EmpKpiEntry04All?Type=Mgt", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0307000000", "Operation", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "Sales", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "02. Client Transfer", "F_39_MyPage/TransferClient?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "General", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0311000000", "02. Daily Department Work", "F_32_Mis/RptDeptWiseEmpAcitviteis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "03. Pending Work-Department Wise", "F_32_Mis/RptDeptWEmpPendActivities", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0341000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0357000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "", "", "", true, "" });

            //tblObj.Rows.Add(new Object[] { "08125", "CustOthMoneyReceipt?", "Typq=Management", "Money Receipt -Finishing Project", "Customer Care", "False", "False", "False", "False", "False", "" });


            mnuTbl1.Rows.Add(new Object[] { "0400000000", "Sales", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "01. KPI Evaluation (All)", "F_47_Kpi/RptEmpEvaSheet?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0402000000", "02. Individual KPI(Multi Graph)", "F_62_Mis/RptEmpEvaGraph", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Client Discussion History", "F_62_Mis/RptClientDis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "04. Sales Demand Analysis", "F_62_Mis/RptProWiseClOffered?Type=SalesDeamnd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "05. Sales Decision", "F_32_Mis/RptProWiseClOffered?Type=SalesDeci", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "06. Client Capacity Analysis", "F_32_Mis/RptProWiseClOffered?Type=Capacity", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "07. Daily Sales & Collection Status", "F_32_Mis/RptMonTarVsAch?Type=dSaleVsColl", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "My Smartface", "", "", false, "" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "01. Month Wise Evaluation", "F_47_Kpi/RptEmpMonthWiseEva?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "02. Month Wise Evaluation Details", "F_47_Kpi/RptEmpMonthWiseEvaDet?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "03. Todays Appoinment", "F_32_Mis/RptMktAppointment?Type=Todaysdis&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "04. Next Appointment", "F_32_Mis/RptMktAppointment?Type=NextApp&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "05. Client History", "F_32_Mis/RptMktAppointment?Type=DiscussHis&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "06. Sales Person History", "F_32_Mis/RptMktAppointment?Type=OffPerformance&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "07. Sales Performance", "F_32_Mis/RptMktAppointment?Type=SalePerformance&UType=Mgt", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0416000000", "General", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "01. KPI Evaluation(All Employee)", "F_39_MyPage/RptEmpEvaSheetGen?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "02. Company Summary Report", "F_39_MyPage/RptDeptEvaSheet?Type=DeptTarVAch", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "Legal", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "01. KPI Evaluation", "F_39_MyPage/RptEmpEvaSheetLeg?Type=Leg", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "Admin Permission", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "01. User Permission", "F_64_Mgt/UserLoginfrm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "02. Project Permission", "F_64_Mgt/ProjectLink", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "03. User Image Upload", "F_64_Mgt/UserImage", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "04. Entry, Edit & Cancellation Record", "F_64_Mgt/RptUserLogDetails", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "05. User Log Information", "F_64_Mgt/RptUserLogStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0450000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0459000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0460000000", "", "", "", true, "" });

        }

        #endregion 

        #region  HRM
        public static void MenuAllHR(DataTable mnuTbl1)
        {
            //One Time input
            // A. Recruitment
            mnuTbl1.Rows.Add(new Object[] { "0201000000", "A. Recruitment", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "01. Information Code", "F_81_Hrm/F_81_Rec/RecHRCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0203000000", "02. Appointment Letter Code", "F_81_Hrm/F_81_Rec/AppLetCodeBook?Type=AppLetter", "", true, "" });

            // A. Appointment
            mnuTbl1.Rows.Add(new Object[] { "0204000000", "B. Appointment", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "01. Department Code", "F_21_GAcc/AccSubCodeBook?InputType=Dept", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "02. Employees Code", "F_21_GAcc/AccSubCodeBook?InputType=Appointment", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0207000000", "03. Information Code", "F_81_Hrm/F_82_App/HRCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "04. Designation Code Book", "F_81_Hrm/F_82_App/HRDesigCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "05. Employee Entry", "F_81_Hrm/F_82_App/EmpEntryForm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "06. Academic Record", "F_81_Hrm/F_82_App/EmpAcaRecord", "", true, "" });



            // D. Leave Monitoring
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "C. Leave Monitoring", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "01. Comp. Leave Rule", "F_81_Hrm/F_84_Lea/HREmpLeave?Type=LeaveRule", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "02. Earn Leave Opening", "F_81_Hrm/F_84_Lea/HRLeaveOpening", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "", "", "", true, "" });



            // D. P.F Account
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "D. P.F Account", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "01. Accounts Code", "F_81_Hrm/F_82_App/AccCodeBook?InputType=Accounts", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "02. Accounts Opening", "F_81_Hrm/F_90_PF/AccOpening", "", true, "" });

            // I. ACR(Annual Confidential Report)

            // mnuTbl1.Rows.Add(new Object[] { "0217000000", "E. ACR(Annual Confidential Report)", "", "", false, "mb" });
            //mnuTbl1.Rows.Add(new Object[] { "0218000000", "01. ACR CodeBook", "F_81_Hrm/F_91_ACR/ACRCodeBook", "", true, "" });


            // I. Management System
            mnuTbl1.Rows.Add(new Object[] { "0219000000", "F. Management System", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "01. General off Day Setup", "F_81_Hrm/F_92_Mgt/HRGenOffdaySetup", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "01. Employee Confirmation", "F_81_Hrm/F_92_Mgt/HREmpConfirmation?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "01. Employee Confirmation (Date Wise)", "F_81_Hrm/F_92_Mgt/HREmpConfirmation?Type=Rpt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "02. Promotion", "F_81_Hrm/F_92_Mgt/EmpPro", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "03. Employee Salary Hold", "F_81_Hrm/F_92_Mgt/EmpHold?Type=EmpSalHold", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0220000000", "03. Employee Bonus Hold", "F_81_Hrm/F_92_Mgt/EmpHold?Type=EmpBonHold", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0221000000", "04. Employee Separation", "F_81_Hrm/F_92_Mgt/RetiredEmployee", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0222000000", "", "", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0224000000", "06. Employee List", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=EmpList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0225000000", "06. Employee List (With Picture)", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=EmpListPic", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0226000000", "07. Employee Transfer List", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=TransList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0227000000", "08. Salary Review List (Six Month)", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=PenEmpCon", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0228000000", "09. Employee Confirmation", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=EmpCon", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0229000000", "10. Employee Manpower Report", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=Manpower", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0230000000", "11. Employee Separation Report", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=SepType", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0231000000", "12. Employee Hold List", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=EmpHold", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0232000000", "13. Grade & Designation Wise  Salary Detail", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=EmpGradeADesig", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0233000000", "14. Grade Wise Employee Details", "F_81_Hrm/F_92_Mgt/RptEmpStatus03?Type=GradeWiseEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0234000000", "15. Increment (All Employee)", "F_81_Hrm/F_92_Mgt/RpEmpIncPro?Type=Increment", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0235000000", "16. Entry, Edit Record", "F_81_Hrm/F_92_Mgt/RptUserLogDetails", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0236000000", "17. Salary Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Salary&Entry=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "18. Resign Salary Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=SalaryReg&Entry=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0237000000", "18. Hold Salary Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=SalaryHold&Entry=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0238000000", "14. EOT Salary Sheet", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary02?Type=OvertimeSal&Entry=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0239000000", "19. Festival Bonus", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Bonus&Entry=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0240000000", "13. Salary Transfer Statement", "F_81_Hrm/F_89_Pay/EmpBankSalary?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0241000000", "20. EOT Salary Transfer Statement", "F_81_Hrm/F_89_Pay/EmpBankSalaryEOT?Type=Mgt", "", true, "" }); 
            mnuTbl1.Rows.Add(new Object[] { "0242000000", "02. Employee Status", "F_81_Hrm/F_83_Att/RptHREmpStatus?Type=Approval", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0243000000", "03. Employee Services Period Information", "F_81_Hrm/F_82_App/RptEmpInformation?Type=Services", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0244000000", "04. Joining Report Summary", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=joiningRpt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "05. New Joiners List", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=JoinigdWise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0246000000", "06. Over Time Policy", "F_81_Hrm/F_92_Mgt/OverTimePolicy", "", true, "" });
          
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "03. Employee Settlement Top Sheet", "F_81_Hrm/F_92_Mgt/RptSettlementStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "03. Periodic Promotion List", "F_81_Hrm/F_92_Mgt/RpEmpIncPro?Type=Promotion", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "03. IP Setup", "F_81_Hrm/F_92_Mgt/DeviceIPSetup", "", true, "" });
            





            ////K. Annual Increment
            //mnuTbl1.Rows.Add(new Object[] { "0243000000", "K. Annual Increment", "", "", false, "mb" });

            //mnuTbl1.Rows.Add(new Object[] { "0244000000", "01.Increment Status", "F_81_Hrm/F_93_AnnInc/RptIncrement", "", true, "" });
            //Smartface
            mnuTbl1.Rows.Add(new Object[] { "0245000000", "L. Smartface", "", "", false, "mb" });

            mnuTbl1.Rows.Add(new Object[] { "0246000000", "31. All Employee List", "F_81_Hrm/F_92_Mgt/AllEmpList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0247000000", "31. My Smartface", "F_81_Hrm/F_82_App/RptMyInterface?Type=Empid&empid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0248000000", "32. Smartface Attendance", "F_81_Hrm/F_92_Mgt/InterfaceAtt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0251000000", "32. HR Smartface", "F_81_Hrm/F_92_Mgt/LetterInterface", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0249000000", "M. Need Base Report", "", "", false, "mb" });

            mnuTbl1.Rows.Add(new Object[] { "0250000000", "01. Need Base Report", "F_81_Hrm/F_82_App/RptEmpInformation?Type=EmpDyInfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0252000000", "02. Need Base Report 01", "F_81_Hrm/F_82_App/RptEmpInformation?Type=EmpDyInfo02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0253000000", "03. Employee Floor Line Setup", "F_81_Hrm/F_82_App/EmpLineSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0254000000", "04. Employee Ac No Upload", "F_81_Hrm/F_82_App/EmpAccNoUpload", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0255000000", "05. Employee Skill Grade Setup", "F_81_Hrm/F_82_App/EmpSkillSheet", "", true, "" });

            //  tblObj.Rows.Add(new Object[] { "8001000", "8001009", "", "EmpSkillSheet", "", "Employee Skill Grade", "Appointment", "False", "False", "False", "False", "False", "" });

            //Input Operational menu

            // A. Recruitment
            mnuTbl1.Rows.Add(new Object[] { "0301000000", "A. Recruitment", "", "", false, "mb" });

            //mnuTbl1.Rows.Add(new Object[] { "0301000000", "01. HRM Smartboard", "F_81_Hrm/F_97_MIS/HRMDashBoard", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0359000000", "01. Manpower Budgeted Entry", "F_81_Hrm/F_81_Rec/ManPowerBudgeted?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "01. Manpower  Requisition", "F_81_Hrm/F_81_Rec/JobAdvertisement?Type=Entry&genno=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "02. Manpower  Requisition App", "F_81_Hrm/F_81_Rec/JobAdvertisement?Type=App&genno=", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0302000000", "01. Job Advertisement Input", "F_81_Hrm/F_81_Rec/JobAdvertisement", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "03. Sort Listing", "F_81_Hrm/F_81_Rec/ShortListing?Type=SList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "04. Interview Result Input", "F_81_Hrm/F_81_Rec/ShortListing?Type=IResult", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "05. Final Selection Input", "F_81_Hrm/F_81_Rec/ShortListing?Type=Fselection", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "06. Appointment Letter Create", "LetterDefault?Type=10002&Entry=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "06. Appointment Letter Approved", "LetterDefault?Type=10002&Entry=Apprv", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0360000000", "07. Joining Letter Create", "LetterDefault?Type=10005&Entry=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0361000000", "07. Joining Letter Approve", "LetterDefault?Type=10005&Entry=Apprv", "", true, "" });
            //LetterDefault? Type = 10015 & Entry = Salary Certificate for current employees

            // B. Appointment
            mnuTbl1.Rows.Add(new Object[] { "0308000000", "B. Appointment", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0309000000", "01. Personal Information", "F_81_Hrm/F_82_App/EmpEntry01?Type=Entry&empid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "02. Employee Agreement", "F_81_Hrm/F_82_App/HREmpEntry?Type=Aggrement&emptype=&empid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0320000000", "03. Employee Image Upload", "F_81_Hrm/F_82_App/ImgUpload?Type=Entry", "", true, "" });

            //Attendance System
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "C. Attendance System", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "01. Employee Off Days", "F_81_Hrm/F_83_Att/HREmpOffDays", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "02. Absent Count", "F_81_Hrm/F_83_Att/HREmpAbsCt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "02. Punch Entry (Manually)", "F_81_Hrm/F_83_Att/HRPunchEntryManually", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "03. Employee Present Opening", "F_81_Hrm/F_83_Att/EmpPresentOpening?Type=LeaveOpening", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0315000000", "03. Daily Attendance - Manually", "F_81_Hrm/F_83_Att/HRDailyAttenManually?Type=Daily", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "03. Daily Attendance - Manually(Date Wise)", "F_81_Hrm/F_83_Att/HRDailyAttenManually?Type=DateWise", "", true, "" });
           



            mnuTbl1.Rows.Add(new Object[] { "0317000000", "04. Daily Attendance - Upload", "F_81_Hrm/F_83_Att/HRDailyAttenUpload", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "05. Daily Attendance - System", "F_81_Hrm/F_83_Att/HRDailyAtten", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "39. Overtime Not Allow & OT Deduction", "F_81_Hrm/F_83_Att/HREmpNotAllow", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "03. Monthly Attendance - Manually", "F_81_Hrm/F_83_Att/HREmpMonthlyAtten", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0316000000", "07. Shift Plan Register", "F_81_Hrm/F_92_Mgt/HREmpShiftSetup", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0319000000", "06. Office Time Setup", "F_81_Hrm/F_82_App/HREmpEntry?Type=Officetime&emptype=&empid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0353000000", "07. Shift/Roster Setup", "F_81_Hrm/F_82_App/HREmpEntry?Type=shifttime&emptype=&empid=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "08. Shift Plan Backup", "F_81_Hrm/F_82_App/HRMShiftPlanBackup?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "09. Shift Working Changer", "F_81_Hrm/F_82_App/HRMShiftChanger", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0356000000", "10. Special Day Setup", "F_81_Hrm/F_82_App/HRSpecialDaySetup", "", true, "" });



            

            mnuTbl1.Rows.Add(new Object[] { "0320000000", "08.  Daily Absent", "F_81_Hrm/F_83_Att/EmpDaillyAbsent", "", true, "" });//"07. Daily Attendance - System", "F_81_Hrm/F_83_Att/HRDailyAtten", "", true, "" });      
            mnuTbl1.Rows.Add(new Object[] { "0321000000", "09. LWP Count", "F_81_Hrm/F_83_Att/HREmpLWP", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0322000000", "10. Employee Monthly Late Approval", "F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MLateAppDay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0323000000", "11. Monthly Punch Approval", "F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MPunchAppDay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0324000000", "12. Monthly Absent Approval", "F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MnthabsentApp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0325000000", "13. Early Leave Approval", "F_81_Hrm/F_83_Att/EmpEarlyLeaveApproval", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0362000000", "14. Daily Missing Punch Modification", "F_81_Hrm/F_83_Att/DailyEmpPunchModification", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0363000000", "15. General Day Attn. Remove", "F_81_Hrm/F_83_Att/HRGeneralDayRemove", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0364000000", "16. General Day Adjustment", "F_81_Hrm/F_83_Att/HRGeneralDayAdjustment", "", true, "" });

            // D. Leave Monitoring
            mnuTbl1.Rows.Add(new Object[] { "0326000000", "D. Leave Monitoring", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0327000000", "01. Leave Application Form (Manual)", "F_81_Hrm/F_84_Lea/HREmpLeave?Type=FLeaveApp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0328000000", "02. Leave Application (Online)", "F_81_Hrm/F_84_Lea/HREmpLeave?Type=LeaveApp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0329000000", "03. Leave Application online (Ind)", "F_81_Hrm/F_84_Lea/MyLeave?Type=User&genno=&actcode=", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0330000000", "03. HR Leave Smartface", "F_81_Hrm/F_92_Mgt/InterfaceLeavApp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0331000000", "03. HR Leave Approval", "F_81_Hrm/F_84_Lea/EmpLvApproval?Type=App", "", true, "" });



            // E. Loan Monitoring  //"", "", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0332000000", "E. Loan Monitoring", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0333000000", "01. Loan Installment", "F_81_Hrm/F_85_Lon/EmpLoanInfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "", "", "", true, "" });// "02. Loan Deduction", "F_81_Hrm/F_86_All/EmpOvertime?Type=loan", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0335000000", "03. Deduction Other", "F_81_Hrm/F_85_Lon/EmpDeducOther", "", true, "" });




            // F. Allowances
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "F. Allowances", "", "", false, "mb" });
           
            mnuTbl1.Rows.Add(new Object[] { "0337000000", "04. Mobile Bill", "F_81_Hrm/F_86_All/EmpOvertime?Type=Mobile", "", true, "" });
            
            mnuTbl1.Rows.Add(new Object[] { "0334000000", "01. Overtime Allowance", "F_81_Hrm/F_86_All/EmpOvertime?Type=Overtime", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0335000000", "02. Holiday Allowance 01", "F_81_Hrm/F_86_All/EmpOvertime?Type=Holiday", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0336000000", "03. Holiday Allowance 02", "F_81_Hrm/F_86_All/HollydayCt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "04. Car & Subsistance Allowance", "F_81_Hrm/F_86_All/EmpOvertime?Type=CarSub", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0338000000", "05. Employee Mobile List", "F_81_Hrm/F_86_All/EmpOvertime?Type=MobLst", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0361000000", "06. Earn Leave Entry", "F_81_Hrm/F_86_All/EmpOvertime?Type=EarnLeave", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0362000000", "07. Leave Encashment", "F_81_Hrm/F_86_All/EmpOvertime?Type=Lencashment", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0363000000", "08. Leave Encashment 02", "F_81_Hrm/F_86_All/EmpOvertime?Type=Lencashment02", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0361000000", "04. Overtime Allowance 02", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary?Type=OvertimeSalary02", "", true, "" });

            // F. Transfer
            mnuTbl1.Rows.Add(new Object[] { "0339000000", "G. Transfer", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0340000000", "01. Employee Transfer", "F_81_Hrm/F_87_Tra/HREmpTransfer", "", true, "" });

            // F. Payroll System

            mnuTbl1.Rows.Add(new Object[] { "0341000000", "H. Payroll System", "", "", false, "mb" });

 
            mnuTbl1.Rows.Add(new Object[] { "0342000000", "01. Bank Payment", "F_81_Hrm/F_86_All/EmpOvertime?Type=BankPayment", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0343000000", "02. Loan Deduction", "F_81_Hrm/F_86_All/EmpOvertime?Type=loan", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0344000000", "03. Deduction", "F_81_Hrm/F_86_All/EmpOvertime?Type=OtherDeduction", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0345000000", "04. Arrear Salary", "F_81_Hrm/F_86_All/EmpOvertime?Type=arrear", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0346000000", "05. Other Earning", "F_81_Hrm/F_86_All/EmpOvertime?Type=otherearn", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0347000000", "06. Salary Day Adjustment", "F_81_Hrm/F_86_All/EmpOvertime?Type=dayadj", "", true, "" });


            // G. P.F Account

            mnuTbl1.Rows.Add(new Object[] { "0348000000", "I. P.F Account", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0348000000", "01. PF Opening", "F_81_Hrm/F_90_PF/PFOpening?Type=PFOpen", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0349000000", "02. PF Account (View/Edit)", "F_81_Hrm/F_90_PF/AccProFund", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0350000000", "03. Payment Voucher", "F_81_Hrm/F_90_PF/GeneralAccounts?tcode=99&tname=Payment Voucher", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0351000000", "04. Deposit Voucher", "F_81_Hrm/F_90_PF/GeneralAccounts?tcode=99&tname=Deposit Voucher", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0352000000", "05. Journal Voucher", "F_81_Hrm/F_90_PF/GeneralAccounts?tcode=99&tname=Journal Voucher", "", true, "" });
            // mnuTbl1.Rows.Add(new Object[] { "0353000000", "05. Individual Payment Schedule Of P.F", "F_81_Hrm/F_90_PF/RptPFIndvPay", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0354000000", "06. Bank Reconcilation", "F_81_Hrm/F_90_PF/AccBankRecon", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "07. InterCompany Payment", "F_81_Hrm/F_90_PF/AccInterComVoucher", "", true, "" });

            //tblObj.Rows.Add(new Object[] { "8102083", "GeneralAccounts?", "tcode=99&tname=Payment Voucher", "Payment Voucher", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8102084", "GeneralAccounts?", "tcode=99&tname=Deposit Voucher", "Deposit Voucher", "PF Account", "False", "False", "False", "False", "False", "" });
            //tblObj.Rows.Add(new Object[] { "8102085", "GeneralAccounts?", "tcode=99&tname=Journal Voucher", "Journal Voucher", "PF Account", "False", "False", "False", "False", "False", "" });



            // H. ACR(Annual Confidential Report)

            mnuTbl1.Rows.Add(new Object[] { "0354000000", "J. ACR(Annual Confidential Report)", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0355000000", "01. Employee Performance Appraisal", "F_81_Hrm/F_91_ACR/EmpPerAppraisal", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0356000000", "01. Employee Performance Appraisal-2", "F_81_Hrm/F_91_ACR/EmpPerAppraisal_2", "", true, "" });


            // I. Annual Increment

            mnuTbl1.Rows.Add(new Object[] { "0357000000", "L. Annual Increment", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "01. Annual Increment", "F_81_Hrm/F_93_AnnInc/AnnualIncrement", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0359000000", "02. Annual Increment Updated", "F_81_Hrm/F_93_AnnInc/HrIncrementUpdate", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0358000000", "03. Annual Increment Modification", "F_81_Hrm/F_93_AnnInc/AnnualIncrModification", "", true, "" });


            //Report
            // A. Recruitment 
            mnuTbl1.Rows.Add(new Object[] { "0401000000", "A. Recruitment", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "01. Man Power Budgeted Vs. Actual", "F_81_Hrm/F_81_Rec/ManPowerBudgetedVsActual", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0402000000", "01. Job Advertisement", "F_81_Hrm/F_81_Rec/RptRecruitment?Type=JobAdvertise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "02. Sort Listing Process", "F_81_Hrm/F_81_Rec/RptRecruitment?Type=SortListing", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "03. Interview Result", "F_81_Hrm/F_81_Rec/RptRecruitment?Type=InterviewResult", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "04. Final Result", "F_81_Hrm/F_81_Rec/RptRecruitment?Type=FinalSelect", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "05. Letter Of Appointment", "F_81_Hrm/F_81_Rec/LetterOfAppoinment?Type=LRpt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0447000000", "05. Joining Letter", "F_81_Hrm/F_81_Rec/LetterOfAppoinment?Type=JRpt", "", true, "" });
            
            // B. Appointment
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "B. Appointment", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "01. Employee Information", "F_81_Hrm/F_82_App/RptEmpInformation?Type=EmpAllInfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "02. Print Employee ID Card", "F_81_Hrm/F_92_Mgt/AllEmpIDCard", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0461000000", "04. Employee Confirm Report", "F_81_Hrm/F_92_Mgt/HREmpConfirm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0462000000", "05. Employee All Document Print", "F_81_Hrm/F_82_App/RptEmpInformation?Type=AllDoc", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0463000000", "06. Employee Custom Document Print", "F_81_Hrm/F_81_Rec/EmpCustDocPrint", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0464000000", "07. Employee All Document Print(FB)", "F_81_Hrm/F_82_App/RptEmpInformation02?Type=AllDoc", "", true, "" });
    
            mnuTbl1.Rows.Add(new Object[] { "0465000000", "08. Employee All Document Datewise Print(FB)", "F_81_Hrm/F_82_App/RptEmpInformation02?Type=Datewise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0466000000", "08. Employee Application Sheet(FB)", "F_81_Hrm/F_82_App/EmpAppPart", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0466000000", "08. Employee Resign & Leave", "F_81_Hrm/F_82_App/RptEmpInformation03?Type=AllDoc", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0467000000", "09. Employee Increment Letter", "F_81_Hrm/F_82_App/RptEmpIncrInformation?Type=AllDoc", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0467000000", "10. Employee Increment & Promotion Letter", "F_81_Hrm/F_82_App/RptEmpPromotionLetter?Type=Promotion", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0467000000", "11. Employee Register Report", "F_81_Hrm/F_82_App/RptEmpRegisterReport?Type=registerreport", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0467000000", "12. Employee Skill Report", "F_81_Hrm/F_82_App/RptEmpSkillReport?Type=skillreport", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0468000000", "13. Employee Aging Report", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=RptAging", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0469000000", "14. Employee All Certificate", "F_81_Hrm/F_86_All/RptAllCertificate", "", true, "" });

            //c. Attendance System
            mnuTbl1.Rows.Add(new Object[] { "0408000000", "C. Attendance System", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "01. Attendance Information", "F_81_Hrm/F_83_Att/RptAttendenceSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "02. Employee Daily Attendance", "F_81_Hrm/F_83_Att/RptEmpDailyAttendance?Type=DailyAtten", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "03. Job Card", "F_81_Hrm/F_83_Att/RptEmpJobCard?Type=Card2Hrs", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "04. (Job Card)", "F_81_Hrm/F_83_Att/RptEmpJobCard?Type=Card3Hrs", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "05. Job (Time Card)", "F_81_Hrm/F_83_Att/RptEmpJobCard?Type=CardAllHrs", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "06. Employee Job Card (All)", "F_81_Hrm/F_83_Att/RptEmpJobCard?Type=CardReal", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "07. Employee Punch Missing Report", "F_81_Hrm/F_83_Att/RptEmpPunch", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "08. Employee Day Wise OT Sheet", "F_81_Hrm/F_89_Pay/RptDayWiseOTSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "09. Employee Month Wise OT Sheet", "F_81_Hrm/F_89_Pay/RptMonthWiseOTSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "10. Attendance Summary (Month Wise)", "F_81_Hrm/F_83_Att/RptEmpAttenSummary?Type=AttnMon", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0412000000", "03. Employee Promotion", "F_81_Hrm/F_83_Att/RptPromotion", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "04. Daily Attendance Summary", "F_81_Hrm/F_83_Att/DailyAttenSummary?Type=AttnSum", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "01. Machine Wise Daily Attendence log", "F_81_Hrm/F_83_Att/RptMacAttendence?Type=machine", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0415000000", "01. Daily Attendance Summary with Skill", "F_81_Hrm/F_83_Att/DailyAttenSummary?Type=AttnSum2", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416000000", "01. Daily Attendance Summary (Category Wise)", "F_81_Hrm/F_83_Att/DailyAttenSummary?Type=AttnCatSum", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "05. Daily Attendance Summary(Direct / Indirect)", "F_81_Hrm/F_83_Att/DailyAttenSummary?Type=AttnSum3", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0455000000", "06. Daily Present After Absent", "F_81_Hrm/F_83_Att/EmpDaillyAbsent?Type=AttnAftrAbs", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "07. Daily Present After Leave", "F_81_Hrm/F_83_Att/EmpDaillyAbsent?Type=AttnAftrLeave", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416100000", "08. Employee Attendance Summary", "F_81_Hrm/F_83_Att/RptEmpDailyAttendance?Type=AttendanceSummary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416200000", "09. Monthly Absent Summary", "F_81_Hrm/F_83_Att/RptEmpMonthlyAbscent?Type=abscsummary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416300000", "10. Monthly Attn. Count Summary (Day Wise)", "F_81_Hrm/F_83_Att/RptMonAttenSummary?Type=DayWise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0416400000", "11. Monthly Attn. Count Summary (Line Wise)", "F_81_Hrm/F_83_Att/RptMonAttenSummary?Type=LineWise", "", true, "" });




            mnuTbl1.Rows.Add(new Object[] { "0417000000", "08. Department Wise Employee List", "F_81_Hrm/F_83_Att/RptEmpDailyAttendance?Type=deptlist", "", true, "" });
            // d. Leave Monitoring
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "D. Leave Monitoring", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "01. Employee Leave Status", "F_81_Hrm/F_84_Lea/RptHREmpLeave?Type=EmpLeaveSt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "02. Employee Leave Status 02", "F_81_Hrm/F_84_Lea/RptHREmpLeave?Type=EmpLeaveSt02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "03. Leave Status- Company Wise", "F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02?Type=EmpLeaveStatus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "04. Employee Leave- Month Wise", "F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02?Type=MonWiseLeave", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "05. Over all Leave Status", "F_81_Hrm/F_84_Lea/EmpLeaveInfo?Type=Leave", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0424000000", "06. Employee Leave Register Report", "F_81_Hrm/F_84_Lea/RptHREmpLeaveReg", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "07. Maternity Benefit Payment Sheet", "F_81_Hrm/F_84_Lea/RptMLPaymentSheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "08. Earn Leave A/C Statement", "F_81_Hrm/F_84_Lea/RptEarnLvBankACStmnt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "09. Earn Leave Payment Requisition", "F_81_Hrm/F_84_Lea/RptEarnLvPaymentReq", "", true, "" });





            // E. Loan Monitoring
            mnuTbl1.Rows.Add(new Object[] { "0422300000", "E. Loan Monitoring", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0423400000", "01. Employee Loan Status", "F_81_Hrm/F_85_Lon/EmpLoanStatus", "", true, "" });



            



            // F. Payroll System
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "F. Payroll System", "", "", false, "mb" });

            mnuTbl1.Rows.Add(new Object[] { "0426000000", "General Reports", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "01. Salary Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Salary&Entry=Payroll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "02. Resign Salary Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=SalaryReg&Entry=Payroll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "03. Hold Salary Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=SalaryHold&Entry=Payroll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "04. Salary OT Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=SalaryOT&Entry=Payroll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "19. EOT Salary Sheet", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary02?Type=OvertimeSal&Entry=Payroll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "14. Monthly OT Sheet", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary03?Type=Overtimesheet&Entry=PayRoll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "14. Monthly Extra OT Sheet", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary03?Type=Overtimesheet2&Entry=PayRoll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "14. Monthly Off Day OT Sheet", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary03?Type=Overtimeofsheet&Entry=PayRoll", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0453000000", "14. Monthly Compliance OT Sheet", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary03?Type=Overtimesheetcom&Entry=PayRoll", "", true, "" });
           
            
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "15. Monthly Holiday Allowance", "F_81_Hrm/F_89_Pay/RptMonHolidayAllow?Type=MonthlyHolidayAllow", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0428000000", "02. Festival Bonus", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Bonus&Entry=Payroll", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0429000000", "03. Employee Status", "F_81_Hrm/F_83_Att/RptHREmpStatus?Type=Payroll", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "04. Pay Slip", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Payslip", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0431000000", "12. Employee Hold List", "F_81_Hrm/F_92_Mgt/EmpStatus02?Type=EmpHold", "", true, "" });

            
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "06. Signature Sheet", "F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Signature", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "07. Salary Statement (Cash)", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=CashSalary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "08. Bonus Sheet (Cash)", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=CashBonus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "09. Monthly Salary Summary", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=MonSalSum", "", true, "" });//"09. Salary 
            mnuTbl1.Rows.Add(new Object[] { "0435000000", "09. Monthly Salary Summary (Salary, Wages & Over Time)", "F_81_Hrm/F_89_Pay/RptSalSummary03", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0435000000", "09. Salary Summary", "F_81_Hrm/F_89_Pay/RptSalarySummary?Type=SalSum", "", true, "" });//"09. Salary 
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "10. Salary Summary 02", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=SalSummary", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0437000000", "11. Salary Summary 03", "F_81_Hrm/F_89_Pay/RptSalarySummary?Type=SalSum02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "12. Bonus Requisition", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=BonusSummary", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "12. Festival Bonus Report", "F_81_Hrm/F_89_Pay/RptFestivalBonus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "13. Salary Transfer Statement", "F_81_Hrm/F_89_Pay/EmpBankSalary?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "12. Monthly Salary Summary (EOT)", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=SalSummaryEot", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0440000000", "14. Details Salary Summary", "F_81_Hrm/F_89_Pay/RptSalSummaryDetails", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0441000000", "15. Monthly Loan,Adv,Cell,Arrear Data Sheet", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=SalLACA", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0442000000", "16. Bank Statement - Company Wise", "F_81_Hrm/F_89_Pay/RptBankStatement?Type=Bnkstmntcwise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0443000000", "17. Bank Statement - Bank Wise", "F_81_Hrm/F_89_Pay/RptBankStatement?Type=Bnkstmtbnkwise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0444000000", "18. Employee Salary Sheet and Leave Status", "F_81_Hrm/F_89_Pay/RptEmpLeaveStatus", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0445000000", "19. Monthly Salary Summery", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=MonSalSum", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "13. EOT Salary Transfer Statement", "F_81_Hrm/F_89_Pay/EmpBankSalaryEOT?Type=Entry", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "18. Workers Daily Signature EOT", "F_81_Hrm/F_86_All/RptEmpEOTSignature?Type=EOTSign", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "19. Subsistance Bonus Allowance", "F_81_Hrm/F_89_Pay/SubsistanceBonus", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0445000000", "13. MONTH WISE EMPLOYEE SALARY ", "F_81_Hrm/F_89_Pay/RptDayWiseEmploySalary", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0446000000", "14. Employee Information Report ", "F_81_Hrm/F_83_Att/RptHREmpStatus?Type=All", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0447000000", "15. Month Wise Individual Overtime Summary", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary03?Type=MonIndOTSum", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0448000000", "16. Day Wise Total Overtime Summary", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary03?Type=DayTotOTSum", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0449000000", "17. Monthly OT Summary (Section Wise)", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary03?Type=MonSecOTSum", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0450000000", "18. Salary Requisition", "F_81_Hrm/F_89_Pay/RptSalaryRequisiton", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0451000000", "19. Daily Overtime Requisition Summary", "F_81_Hrm/F_89_Pay/RptOverTimeRequisition?Type=SecWise", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0452000000", "20. Bank A/C Statement", "F_81_Hrm/F_89_Pay/RptBankAccStatement?Type=BankAccStatmnt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0453000000", "21. Breakfast Payment Sheet", "F_81_Hrm/F_89_Pay/RptEmpFoodAllowance?Type=BreakFast", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0454000000", "22. Night Bill Payment Sheet", "F_81_Hrm/F_89_Pay/RptEmpFoodAllowance?Type=NightBill", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0455000000", "23. Resign Salary Summary 04", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=SalSummary02Reg", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0456000000", "24. AIT Purpose Salary Statement", "F_81_Hrm/F_89_Pay/EmpMonthSummary?Type=salati", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0457000000", "25. Final Settlement Payment Sheet", "F_81_Hrm/F_89_Pay/RptFinalSettlementPaySheet", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0458000000", "26. Transport Allowance Payment Sheet", "F_81_Hrm/F_89_Pay/RptEmpFoodAllowance?Type=TransAllow", "", true, "" });



            //mnuTbl1.Rows.Add(new Object[] { "0442000000", "18. Overtime Allowance", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary?Type=OvertimeSalary", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0443000000", "19. Overtime Allowance 02", "F_81_Hrm/F_89_Pay/EmpOverTimeSalary?Type=OvertimeSalary02", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0444000000", "20. Pay Slip (Bonus)", "F_81_Hrm/F_89_Pay/RptSalSummary02?Type=BonPaySlip", "", true, "" });


            //G P.F Account
            mnuTbl1.Rows.Add(new Object[] { "0446000000", "G. P.F Account", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0446100000", "04. PF Account", "F_81_Hrm/F_90_PF/RptAccProFund?Type=PFAcc", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446100000", "05. PF Account (Yearly)", "F_81_Hrm/F_90_PF/RptAccProFund", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446200000", "06. Monthly PF", "F_81_Hrm/F_90_PF/RptMonthlyProFund?Type=MonthlyPF", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0446300000", "07. Monthly PF Payment Sheet", "F_81_Hrm/F_90_PF/RptMonthlyProFund", "", true, "" });


            //H Annual Increment
            mnuTbl1.Rows.Add(new Object[] { "0480000000", "H. Annual Increment", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0481000000", "01. Increment Status", "F_81_Hrm/F_93_AnnInc/RptIncrement", "", true, "" });

        }

        public static void MenuAllKPI(DataTable mnuTbl1)
        {
            // One Time Input

            // Managment
            mnuTbl1.Rows.Add(new Object[] { "0202000000", "A. Managment", "", "", false, "mb" });

            //mnuTbl1.Rows.Add(new Object[] { "0204000000", "01. Control Code", "F_64_Mgt/DeptActivitiesCode", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0205000000", "02. Client Code", "F_64_Mgt/GenCodeBook", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0206000000", "03. Team Code", "F_64_Mgt/TeamSeriCode", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0207000000", "02. Client Details (MGT) ", "F_39_MyPage/ClientDetail?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0208000000", "02. Client Details ", "F_39_MyPage/ClientDetail?Type=Client", "", true, "" });

            //mnuTbl1.Rows.Add(new Object[] { "0207000000", "04. Center Code", "F_64_Mgt/ProjectCode?Type=Project", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0208000000", "05. Center Activation", "F_64_Mgt/EntryProjectActive", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0209000000", "B. General", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0210000000", "01. Department & Section", "F_64_Mgt/ActivitiesCode?Type=Dept", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0211000000", "02. Work List", "F_64_Mgt/ActivitiesCode?Type=Activities", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0212000000", "03. Weightage", "F_64_Mgt/ActivitiesCode?Type=Weightage", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0213000000", "04. Department Wise Work List", "F_64_Mgt/ActivitiesCode?Type=DeptList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0214000000", "06. Department Wise Employee", "F_64_Mgt/DeptWiseEmpList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0215000000", "C. Legal", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0216000000", "01. Court & Other Activities", "F_64_Mgt/ActivitiesCode?Type=Coutaotheract", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0217000000", "02. Case Code", "F_64_Mgt/ProjectCode?Type=Case", "", true, "" });




            //==================================================================================
            // Input Part


            // My Interface Genereal
            mnuTbl1.Rows.Add(new Object[] { "0302000000", "A. My Interface(CR)", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0303000000", "01. Daily Job Execution", "F_39_MyPage/EmpKpiEntry03?Type=Client", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0304000000", "02. Kpi Entry", "F_39_MyPage/EmpKpiEntry04?Type=All", "", true, "" });
            // My Interface(Legal)
            mnuTbl1.Rows.Add(new Object[] { "0305000000", "B. My Interface(Legal)", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0306000000", "01. Daily Job Execution", "F_39_MyPage/EmpKpiEntry04?Type=General", "", true, "" });


            // Managment
            mnuTbl1.Rows.Add(new Object[] { "0307000000", "C. Managment", "", "", false, "mb" });

            mnuTbl1.Rows.Add(new Object[] { "0309000000", "01. Monthly Target(Primary Sales)", "F_47_Kpi/EmpStdKpi", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0310000000", "01. Monthly Target(Secondary Sales)", "F_47_Kpi/EmpStdKpiSec", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0311000000", "02. Monthly Target(General)", "F_64_Mgt/DeptWiseEmpList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0312000000", "03. Monthly Target(CR)", "F_47_Kpi/EmpStdKpiCR", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0313000000", "04. Monthly Target(Legal)", "F_21_Kpi/KpiSetupLegal", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0314000000", "05. Daily Job Execution", "F_39_MyPage/EmpKpiEntry04?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0315000000", "06. Daily Job Execution(All Department)", "F_39_MyPage/EmpKpiEntry04All?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0316000000", "02. Client Transfer", "F_39_MyPage/TransferClient?Type=Mgt", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0315000000", "General", "", "", false, "b" });
            mnuTbl1.Rows.Add(new Object[] { "0317000000", "02. Daily Department Work", "F_32_Mis/RptDeptWiseEmpAcitviteis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0318000000", "03. Pending Work-Department Wise", "F_32_Mis/RptDeptWEmpPendActivities", "", true, "" });







            //==================================================================================
            // Reports
            // My Interface Genereal


            mnuTbl1.Rows.Add(new Object[] { "0403000000", "A. My Interface Genereal", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0404000000", "01. Evaluation", "F_39_MyPage/RptEmpEvaSheet04?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0405000000", "01. Month Wise Evaluation", "F_39_MyPage/RptEmpMonthWiseEva03?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0406000000", "02. Employee History", "F_39_MyPage/RptEmpHistory02?Type=IndEmp", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0407000000", "02. Individual History", "F_39_MyPage/RptMIS02?Type=EmpHistory&History=Individual", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0408000000", "B. Sales", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0409000000", "01. KPI Evaluation (All)", "F_47_Kpi/RptEmpEvaSheet?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0410000000", "02. Individual KPI(Multi Graph)", "F_62_Mis/RptEmpEvaGraph", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0411000000", "03. Client Discussion History", "F_62_Mis/RptClientDis", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0412000000", "04. Sales Demand Analysis", "F_62_Mis/RptProWiseClOffered?Type=SalesDeamnd", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0413000000", "05. Sales Decision", "F_32_Mis/RptProWiseClOffered?Type=SalesDeci", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0414000000", "06. Client Capacity Analysis", "F_32_Mis/RptProWiseClOffered?Type=Capacity", "", true, "" });
            //mnuTbl1.Rows.Add(new Object[] { "0415000000", "07. Daily Sales & Collection Status", "F_32_Mis/RptMonTarVsAch?Type=dSaleVsColl", "", true, "" });

            mnuTbl1.Rows.Add(new Object[] { "0416000000", "C. My Interface", "", "", false, "" });
            mnuTbl1.Rows.Add(new Object[] { "0417000000", "01. Month Wise Evaluation", "F_47_Kpi/RptEmpMonthWiseEva?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0418000000", "02. Month Wise Evaluation Details", "F_47_Kpi/RptEmpMonthWiseEvaDet?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0419000000", "03. Todays Appoinment", "F_32_Mis/RptMktAppointment?Type=Todaysdis&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0420000000", "04. Next Appointment", "F_32_Mis/RptMktAppointment?Type=NextApp&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0421000000", "05. Client History", "F_32_Mis/RptMktAppointment?Type=DiscussHis&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0422000000", "06. Sales Person History", "F_32_Mis/RptMktAppointment?Type=OffPerformance&UType=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0423000000", "07. Sales Performance", "F_32_Mis/RptMktAppointment?Type=SalePerformance&UType=Mgt", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0424000000", "General", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0425000000", "01. KPI Evaluation(All Employee)", "F_39_MyPage/RptEmpEvaSheetGen?Type=Mgt", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0426000000", "02. Company Summary Report", "F_39_MyPage/RptDeptEvaSheet?Type=DeptTarVAch", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0427000000", "02. Company Summary Report (Primary & Secondary)", "F_39_MyPage/RptDeptEvaSheetPaSec?Type=DeptTarVAch", "", true, "" });


            //mnuTbl1.Rows.Add(new Object[] { "0427000000", "Legal", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0428000000", "01. KPI Evaluation", "F_39_MyPage/RptEmpEvaSheetLeg?Type=Leg", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0429000000", "D. Admin Permission", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0430000000", "01. User Permission", "F_64_Mgt/UserLoginfrm", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0431000000", "02. Center Permission", "F_64_Mgt/ProjectLink", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0432000000", "03. User Image Upload", "F_64_Mgt/UserImage", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0433000000", "04. Entry, Edit & Cancellation Record", "F_64_Mgt/RptUserLogDetails", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0434000000", "05. User Log Information", "F_64_Mgt/RptUserLogStatus", "", true, "" });


            mnuTbl1.Rows.Add(new Object[] { "0435000000", "E. MIS", "", "", false, "mb" });
            mnuTbl1.Rows.Add(new Object[] { "0436000000", "01. Need Base Report", "F_81_Hrm/F_82_App/RptEmpInformation?Type=EmpDyInfo", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0437000000", "02. Need Base Report 01", "F_81_Hrm/F_82_App/RptEmpInformation?Type=EmpDyInfo02", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0403000000", "03. Employee Birthday", "F_81_Hrm/F_82_App/EmpBirthDeath?Type=EmpBirthdayList", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0438000000", "04. Employeement History", "F_81_Hrm/F_82_App/RptEmpInformation?Type=Services", "", true, "" });
            mnuTbl1.Rows.Add(new Object[] { "0439000000", "03. Management Smartface (HR)", "F_81_Hrm/F_97_MIS/RptMgtInterface", "", true, "" });


        }

        #endregion
        
       #endregion



    }
}
