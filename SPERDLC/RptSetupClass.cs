
using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace SPERDLC
{
    public class RptSetupClass
    {
        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("SPERDLC." + RptName + ".rdlc");
            LocalReport Rpt1a = new LocalReport();
            Rpt1a.DisplayName = RptName;
            Rpt1a.LoadReportDefinition(stream1);
            Rpt1a.DataSources.Clear();
            switch (Rpt1a.DisplayName.Trim())
            {

                #region R_01_Mer
                case "R_01_Mer.RptOrderStatus": Rpt1a = SetRptOrderStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptLCAnalysis": Rpt1a = SetRptLCAnalysis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptSampleInqTopSheet": Rpt1a = SetRptSampleInqTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptSampleEntry": Rpt1a = SetRptSampleEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptConsumptionSheet": Rpt1a = SetRptConsumptionSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptConsumptionSheetFB": Rpt1a = SetRptConsumptionSheetFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptPreCostingSheet": Rpt1a = SetRptPreCostingSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_01_Mer.RptPreCostingSheetFB": Rpt1a = SetRptPreCostingSheetFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptCommonCostingSheet": Rpt1a = SetRptCommonCostingSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptCommonConsSheet": Rpt1a = SetRptCommonConsSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptCostingSummary": Rpt1a = SetRptCostingSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptAllArtclSum": Rpt1a = SetRptAllArtclSum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_01_Mer.RptOrderSheet": Rpt1a = SetRptOrderSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptMerForceEdit": Rpt1a = SetRptMerForceEdit(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptBOMApprvList": Rpt1a = SetRptBOMApprvList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptPfiList": Rpt1a = SetRptPfiList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptShippingMark": Rpt1a = SetShippingMark(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_Mer.RptShippingMarkV2": Rpt1a = SetShippingMarkV2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion

                #region  R_03_CostABgd
                case "R_03_CostABgd.RptMLCOrderCost": Rpt1a = SetRptMLCOrderCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptProformaInv": Rpt1a = SetRptProformaInv(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptProformaInvFb": Rpt1a = SetRptProformaInv(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptProformaInvCCC": Rpt1a = SetRptProformaInvCCC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptOrderStatus": Rpt1a = SetRptOrderrStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptSalesCon": Rpt1a = SetRptRptSalesCon(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptStdSheet": Rpt1a = SetStandardCostSheetAll(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptMatReqImport": Rpt1a = SetRptMatReqImport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptMatReqImportFB": Rpt1a = SetRptMatReqImport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptMatReqImportFBPakingInfo": Rpt1a = SetRptMatReqImportFBPakingInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptMatReqLocal": Rpt1a = SetRptMatReqLocal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptBomApproved": Rpt1a = SetRptBomApproved(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptPreOrderStatus": Rpt1a = SetRptPreOrderStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptPreProdStatus": Rpt1a = SetRptPreProdStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_03_CostABgd.RptPreProdStatusDetails": Rpt1a = SetRptPreProdStatusDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                #endregion

                #region  R_05_ProShip
                case "R_05_ProShip.RptExportPlanVsAchiv": Rpt1a = SetRptExportPlanVsAchiv(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptMatMaster": Rpt1a = SetRptMatMaster(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptArticleWiseLot": Rpt1a = SetRptArticleWiseLot(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptProcessBaseProdPlan": Rpt1a = SetRptProcessBaseProdPlan(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptArticleLayout": Rpt1a = SetRptArticleLayout(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptArticleSMVPrint": Rpt1a = SetRptArticleLayout(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptWrkCapMon": Rpt1a = SetRptWrkCapMon(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptOrderStatus": Rpt1a = RptOrderStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion

                #region  R_04_Samp
                case "R_04_Samp.RptSampleInterfaceInquery": Rpt1a = SetRptSampleInterfaceInquery(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptConsumptionSheetFB": Rpt1a = SetRptPreSamCostingSheetFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptPreSampCostingSheetFB": Rpt1a = SetRptPreSampCostingSheetFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptPdBook": Rpt1a = SetRptPdBook(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptKnifEntry": Rpt1a = SetRptKnifEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptSamReport": Rpt1a = SetRptSamReport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptSamTagPrint": Rpt1a = SetRptSamTagPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptSampleProdRequisition": Rpt1a = SetRptSampleProdRequisition(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Samp.RptPackListPrint": Rpt1a = SetRptPackListPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region  F_09_Commer
                case "R_09_Commer.RptBBLCPayStatus": Rpt1a = SetRptBBLCPayStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptBBlcInfo": Rpt1a = SetRptBBlcInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLCPosition": Rpt1a = SetRptLCPosition(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLCPositionSummary": Rpt1a = SetRptLCPosition(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLcInfowithReq": Rpt1a = RptLcInfowithReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLCCost": Rpt1a = RptRptLCCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptRequisitionVsReceived": Rpt1a = SetRptRequisitionVsReceived(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLCRecInfo": Rpt1a = SetRptLCQCInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLCQc": Rpt1a = SetRptLCCosting(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptMatBBLInfo": Rpt1a = SetRptBBLMatInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptBOMVsReceived": Rpt1a = RptBOMVsReceived(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptBOMVsReceivedMaterials": Rpt1a = RptBOMVsReceivedMaterials(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptBOMVsReceivedSpecification": Rpt1a = RptBOMVsReceivedSpecification(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptWorkOrderVsSupply": Rpt1a = RptWorkOrderVsSupply(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptWorkOrderVsSupply2": Rpt1a = RptWorkOrderVsSupply2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptWorkOrderVsSupply3": Rpt1a = RptWorkOrderVsSupply3(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptIndSupPurchase": Rpt1a = RptIndSupPurchase(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptMatWisePO": Rpt1a = SetRptMatWisePO(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptMatWisePOSummery": Rpt1a = SetRptMatWisePOSummery(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptIncomingMatInspctFrmt0": Rpt1a = SetRptIncomingMatInspctFrmt0(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptIncomingMatInspctFrmt1": Rpt1a = SetRptIncomingMatInspctFrmt1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptIncomingMatInspctFrmt2": Rpt1a = SetRptIncomingMatInspctFrmt2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptIncomingMatInspctFrmt3": Rpt1a = SetRptIncomingMatInspctFrmt3(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptSeasonWiseSupplySummary": Rpt1a = SetRptSeasonWiseSupplySummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptSeasonBySeasonSupplySummary": Rpt1a = SetRptSeasonBySeasonSupplySummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptSeasonOverviewOfMaterials": Rpt1a = SetRptSeasonOverviewOfMaterials(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptRawMatSupLeadTime": Rpt1a = SetRptRawMatSupLeadTime(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLCAllInfo": Rpt1a = SetRptLCAllInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptBomWiseMatSummary": Rpt1a = SetRptBomWiseMatSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptDayWisPrchse": Rpt1a = SetRptDayWisPrchse(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptLcCosting": Rpt1a = SetRptLcCosting2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptMatPriceVariance": Rpt1a = RptMatPriceVariance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptMatSpcfPriceVariance": Rpt1a = RptMatSpcfPriceVariance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptPurMktSurvey": Rpt1a = RptPurMktSurvey(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptPOShipmentLog": Rpt1a = RptPOShipmentLog(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_Commer.RptWeekPlanWiseMat": Rpt1a = RptWeekPlanWiseMat(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region  R_10_Procur
                //case "R_10_Procur.RptPurMktSurvey": Rpt1a = SetRptPurMktSurveySup(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_10_Procur.RptPurMktSurvey02": Rpt1a = SetRptPurMktSurveySup02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptPurMktSurvey03": Rpt1a = SetRptPurMktSurveySup03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval": Rpt1a = SetRptImportApproval(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval2": Rpt1a = SetRptImportApproval2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval23": Rpt1a = SetRptImportApproval2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval2FB": Rpt1a = SetRptImportApproval2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval2DoubleUnit": Rpt1a = SetRptImportApproval2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval2Accessories": Rpt1a = SetRptImportApproval2Accessories(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval2Outsol": Rpt1a = SetRptImportApproval2Accessories(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptImportApproval2MasterCtn": Rpt1a = SetRptImportApproval2Accessories(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptHisMaterial": Rpt1a = SetRptHisMaterial(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptDayWisePurchase": Rpt1a = SetRptDayWisePurchase(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptPurchaseReturn": Rpt1a = SetRptPurchaseReturn(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptSumOutstdng": Rpt1a = SetRptSumOutstdng(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptMasterPOReport": Rpt1a = SetRptMasterPOReport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_10_Procur.RptIQCInspection": Rpt1a = SetRptIQCInspection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region  F_15_DPayReg
                case "R_15_DPayReg.RptOtherReq": Rpt1a = SetRptOtherReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion

                #region  F_15_Pro
                case "R_15_Pro.RptOrdProShipAll": Rpt1a = SetRptOrdProShipAll(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptProtarVsAchieve": Rpt1a = SetRptProtarVsAchieve(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptProdinfo": Rpt1a = SetRptProdinfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptProdProcess": Rpt1a = SetRptProdProcess(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptProdReq": Rpt1a = SetRptProdReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptProdReqApp": Rpt1a = SetRptProdReqApp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptProductionManually": Rpt1a = SetRptProductionManually(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptGetDateWiseMatIssueInfo": Rpt1a = SetRptGetDateWiseMatIssueInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptDateWiseMaterialShowIssuewise": Rpt1a = SetRptDateWiseMaterialShowIssuewise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptDPRWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptBOMWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptDayWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptSummerWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptDailyProdBalSheet": Rpt1a = SetRptDailyProdBalSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptSizeProdBalSheet": Rpt1a = SetRptSizeProdBalSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptQltyAndProductivity": Rpt1a = SetRptQltyAndProductivity(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptMatIssueDetails": Rpt1a = SetRptMatIssueDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptMntlyProdAnalytical": Rpt1a = SetRptMntlyProdAnalytical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptDefectParChart": Rpt1a = SetRptDefectParChart(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_Pro.RptDefectOrder": Rpt1a = SetRptDefectOrder(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                //#region  R_15_DPayReg
                //case "R_15_DPayReg.RptRequistionStatus": Rpt1a = SetRptRequistionStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //#endregion
                #region  F_11_RawInv
                case "R_11_RawInv.RptCentralStore": Rpt1a = SetRptCentralStore(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptCentralStoreSub": Rpt1a = SetRptCentralStore(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurcReq": Rpt1a = SetRptPurReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurcReqFB2": Rpt1a = SetRptPurReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurcReqFB": Rpt1a = SetRptPurReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_11_RawInv.RptPurchaseOrder": Rpt1a = SetRptPurchaseOrder(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurMRR": Rpt1a = SetRptPurMRR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurMRRFB": Rpt1a = SetRptPurMRRFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurMktSurvey02": Rpt1a = SetRptPurMktSurvey02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurcReqLocal": Rpt1a = SetRptPurReqLocal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurcReqLocalFB": Rpt1a = SetRptPurReqLocal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPurMktSurvey": Rpt1a = SetRptPurMktSurvey(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_11_RawInv.RptPOLocal": Rpt1a = SetRptPOLocal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPOLocalFB": Rpt1a = SetRptPOLocal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPOLocalFBNew": Rpt1a = SetRptPOLocal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPOLocalFBJobWork": Rpt1a = SetRptPOLocalFBJobWork(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptLocalApproval2Outsol": Rpt1a = SetRptLocalApproval2Outsol(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptMaterialIssue": Rpt1a = SetRptMaterialIssue(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RpttMatUnused": Rpt1a = SetRpttMatUnused(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptMtrReqInfo": Rpt1a = SetRptMtrReqInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptMaterialIssueStatus": Rpt1a = SetRptMaterialIssueStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptInvQuantityBasis": Rpt1a = SetRptInvQuantityBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptMatTransfer": Rpt1a = SetRptMatTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPeriodMatTransfer": Rpt1a = SetRptMatTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_11_RawInv.RptPeriodMatTransDetails": Rpt1a = SetRptMatTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion

                #region  F_17_GFInv
                case "R_17_GFInv.RptFGCentralStore": Rpt1a = SetRptFGCentralStore(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_GFInv.RptLocation": Rpt1a = SetRptFGCentralStore(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_GFInv.RptShipmentSummary": Rpt1a = SetRptFGCentralStore(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_GFInv.RptFGInspectionEntry": Rpt1a = RptFGInspectionEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region  F_19_Exp
                case "R_19_Exp.RptCommInvoice": Rpt1a = SetRptCommercialInvoic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptCommInvoiceFBFrmt1": Rpt1a = SetRptCommInvoiceFBFrmt1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptCommInvoiceFBFrmt2": Rpt1a = SetRptCommInvoiceFBFrmt2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptCommInvoiceFBFrmt3": Rpt1a = SetRptCommInvoiceFBFrmt3(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptPackingList": Rpt1a = SetRptPackingList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptPackingListFBFrmt2": Rpt1a = SetRptPackingListFBFrmt2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptPackingListFBFrmt1": Rpt1a = SetRptPackingListFBFrmt1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptPackingListFb": Rpt1a = SetRptPackingList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptGSP": Rpt1a = SetRptGSP(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptBenefiDecla": Rpt1a = SetRptBenefiDecla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptBillOfExchange": Rpt1a = SetRptBillOfExchange(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptFrdLetter": Rpt1a = SetRptFrdLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_ProShip.RptExportPlanInEdit": Rpt1a = SetRptExportPlanInEdit(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptDeliveryChallanEdison": Rpt1a = SetRptDeliveryChallan(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptDeliveryChallanEdison2": Rpt1a = SetRptDeliveryChallanEdison2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptDeliveryChallanFBFrmt2": Rpt1a = SetRptDeliveryChallanFBFrmt2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptSalesRealizeCer": Rpt1a = SetRptSalesRealizeCer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptSalesRealizeCert2": Rpt1a = SetRptSalesRealizeCert2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptExportRealization": Rpt1a = SetRptExportRealization(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptShipment": Rpt1a = SetRptShipment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptExportSummery": Rpt1a = SetRptExportSummery(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptExportUnrealization": Rpt1a = SetRptExportUnrealization(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_19_Exp.RptIncntvDeclaration": Rpt1a = SetRptIncntvDeclaration(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                //

                #endregion

                #region  F_21_GAcc
                case "R_21_GAcc.RptShareQty": Rpt1a = SetRptShareQty(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptTrialBl1": Rpt1a = SetAccTrialBl1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptAccDetTrialBalance": Rpt1a = SetRptAccDetTrialBalance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptLCCostVsEx": Rpt1a = SetRptLCCostVsEx(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptPrintVoucherSP": Rpt1a = SetRptPrintVoucherSP(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptPrintVoucherSPFB": Rpt1a = SetRptPrintVoucherSP(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptCheque2": Rpt1a = SetRptCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptCashFlow": Rpt1a = SetRptCashFlow(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptAccountLdger": Rpt1a = SetRptAccountLdger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptDaliyTrans": Rpt1a = SetRptDaliyTrans(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptAllTrans": Rpt1a = SetRptAllTrans(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptMaterialsCodeBook": Rpt1a = SetRptMaterialsCodeBook(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptMaterialsCodeBookFB": Rpt1a = SetRptMaterialsCodeBook(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptAccountsOpening": Rpt1a = SetRptAccountsOpening(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptAccOpLev2": Rpt1a = SetRptAccOpLev2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptSpLedger": Rpt1a = SetRptSpLedger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptSupCusTxVt": Rpt1a = SetRptSupCusTxVt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptAccDetailsScdl": Rpt1a = SetRptAccDetailsScdl(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.ListOfIssuedCheque": Rpt1a = SetListOfIssuedCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptBankReconciliation": Rpt1a = SetRptBankReconciliation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptMatPriceSummary": Rpt1a = SetRptMatPriceSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptBillRegister": Rpt1a = SetRptBillRegister(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptGeneralBill": Rpt1a = SetRptGeneralBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_GAcc.RptCashBankStatement": Rpt1a = SetRptCashBankStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion

                #region  F_23_SaM
                case "RD_23_SaM.RptissueDelvBR": Rpt1a = SetRptissueDelvBR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_23_SaM.RptIndProStock": Rpt1a = SetRptIndProStock(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #region  F_27_Fxt
                case "RD_27_Fxt.RptSchdofFxtAssets": Rpt1a = SetRptSchdofFxtAssets(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_27_Fxt.RptFixAssetDepartment": Rpt1a = RptFixAssetDepartment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_27_Fxt.RptFixAssetEquipment": Rpt1a = RptFixAssetDepartment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_27_Fxt.RptFixAssetUser": Rpt1a = RptFixAssetDepartment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #endregion

                #region R_33_DOC
                case "R_33_Doc.RptDocInformation": Rpt1a = SetRptDocInformation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region R_34_Mgt
                case "R_34_Mgt.RptBillSticker": Rpt1a = SetRptBillSticker(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region RD_62_Mis
                case "RD_62_Mis.RptIncomeStatement12": Rpt1a = SetRptIncomeStatement12(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region F_81_HRM

                #region Rec
                case "RD_81_HRM.RD_81_Rec.RptManpowerBudgt": Rpt1a = SetRptManpowerBudgt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptAllDocBN": Rpt1a = SetRptAllDocBN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEmpApplicationForm": Rpt1a = SetRptEmpApplicationForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptAppoinmentLetterFB": Rpt1a = SetRptAllDocBN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptAppoinmentLetterFBNonOT": Rpt1a = SetRptAllDocBN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptAppoinmentLetterExc": Rpt1a = SetRptAppoinmentLetterExec(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptAppoinmentLetterNonExc": Rpt1a = SetRptAppoinmentLetterNonExc(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptAppoinmentLetterNonOT": Rpt1a = SetRptAppoinmentLetterNonExc(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_82_App.RptAppsPart": Rpt1a = SetRptAppsPart(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmsalcertificate": Rpt1a = SetRptEmsalcertificate(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_81_Rec.RptLetterJoiningExc": Rpt1a = SetRptJoiningLetterExc(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptConfirmletter": Rpt1a = SetRptConfirmletter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptBankOpening": Rpt1a = SetRptBankOpening(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptBankOpeningFootbed": Rpt1a = SetRptBankOpening(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptWorkerConfirmLetterFB": Rpt1a = SetRptWorkerConfirmLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptWorkerConfirmLetter": Rpt1a = SetRptWorkerConfirmLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMultiFB": Rpt1a = SetRptWorkerConfirmLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMulti": Rpt1a = SetRptWorkerConfirmLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptCPFMulti": Rpt1a = SetRptCPF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptMonthWiseAbscent": Rpt1a = SetRptMonthWiseAbscent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptCPFMultiEN": Rpt1a = SetRptCPF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptCPF2Multi": Rpt1a = SetRptCPF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptCPF2MultiEN": Rpt1a = SetRptCPF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptCPF3Multi": Rpt1a = SetRptCPF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptCPF3MultiEN": Rpt1a = SetRptCPF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSalarySumFB": Rpt1a = SetRptSalarySumFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEvaluationFormStaff": Rpt1a = SetRptEvaluationForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEvaluationFormWorker": Rpt1a = SetRptEvaluationForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEnvelopeBan": Rpt1a = SetRptEnvelope(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEnvelopeEng": Rpt1a = SetRptEnvelope(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEnvelopePresentBan": Rpt1a = SetRptEnvelopePresent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEnvelopePresentEng": Rpt1a = SetRptEnvelopePresent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptOfficeEnvelop": Rpt1a = SetRptEnvelopePresent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptTrainingForm": Rpt1a = SetRptEmpApplicationForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptAgeForm": Rpt1a = SetRptEmpApplicationForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptNomineeForm": Rpt1a = SetRptEmpApplicationForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptProbEvaluationForm": Rpt1a = SetRptProbEvaluationForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region appoinment
                case "RD_81_HRM.RD_82_App.RptEmpLeaveLetter": Rpt1a = SetRptEmpLeaveLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpLeaveLetterMulti": Rpt1a = SetRptEmpLeaveLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpResignLetter": Rpt1a = SetRptEmpResignLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpResignLetterMulti": Rpt1a = SetRptEmpResignLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpSelfSupport": Rpt1a = SetRptEmpSelfSupport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpSelfSupportMulti": Rpt1a = SetRptEmpSelfSupport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpIncrletter": Rpt1a = SetRptEmpPromotionletter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpPromotionletter": Rpt1a = SetRptEmpPromotionletter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpIncrLetterBan": Rpt1a = SetRptEmpPromotionletter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpPromLetterBan": Rpt1a = SetRptEmpPromotionletter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpIncrPromLetterBan": Rpt1a = SetRptEmpPromotionletter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;               
                case "RD_81_HRM.RD_82_App.RptEmpRegisterReport": Rpt1a = SetRptEmpRegisterReport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpSkillReport": Rpt1a = SetRptEmpSkillReport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpMatFirstLetter": Rpt1a = SetRptEmpMatFirstLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_82_App.RptEmpMatSecondLetter": Rpt1a = SetRptEmpMatSecondLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                //G. P.F ACCOUNT
                case "RD_81_HRM.RD_84_Lea.RptLeaveApp": Rpt1a = SetRptLeaveApp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptLeaveFormBang": Rpt1a = SetRptLeaveFormBang(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptEmpLeavStatus": Rpt1a = SetRptEmpLeavStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptMonWiseLeaveBR": Rpt1a = SetRptMonWiseLeaveBR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptLeavAppFormEng": Rpt1a = SetRptLeavAppFormEng(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptLeavAppFormBn": Rpt1a = SetRptLeavAppFormBn(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptHREmpLeaveReg": Rpt1a = SetRptHREmpLeaveReg(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptEmpLeavStatus02": Rpt1a = SetRptEmpLeavStatus02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_86_All.RptMobLst": Rpt1a = SetRptMobLst(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptMatLeavePaySheet": Rpt1a = SetRptMatLeavePaySheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "RD_81_HRM.RD_89_Pay.RptOverTimeSal": Rpt1a = SetRptOverTimeSal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptOverTimeSalFB": Rpt1a = SetRptOverTimeSal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSalaryOTFB": Rpt1a = SetRptOverTimeSal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptHRInterfaceLeave": Rpt1a = SetRptHRInterfaceLeave(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpSattelment": Rpt1a = SetRptEmpSattelment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptPromotion": Rpt1a = SetRptPromotion(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptNOC": Rpt1a = SetRptNOC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEXCer": Rpt1a = SetRptNOC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptClearance": Rpt1a = SetRptNOC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptRessig": Rpt1a = SetRptNOC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpPendingConfirmation": Rpt1a = SetRptEmpPendingConfirmation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_89_Pay.RptSalarySheet": Rpt1a = SetRptSalarySheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptExecutiveSal": Rpt1a = SetRptExecutiveSal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptWorkerSalFB": Rpt1a = SetRptWorkerSalFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptWorkerSalFootbed": Rpt1a = SetRptWorkerSalFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSalaryNONOTFB": Rpt1a = SetRptSalaryNONOTFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptMonIndOTSumFB": Rpt1a = SetRptOverTimeSal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_89_Pay.RptPaymSlipBangla": Rpt1a = SetRptPaymSlipBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptPaySlipExe": Rpt1a = RptPaySlipExe(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptStaffSalary": Rpt1a = SetRptStaffSalary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptManPowerBudgtActual": Rpt1a = RptManPowerBudgtActual(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptCarSubAllowance": Rpt1a = SetRptCarSubAllowance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_Hrm.RD_89_Pay.RptEmpMonthSumm": Rpt1a = SetRptEmpMonthSumm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_Hrm.RD_89_Pay.RptEmpMonthSummFB": Rpt1a = SetRptEmpMonthSumm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RdlcAITPurpose": Rpt1a = SetRdlcAITPurpose(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptAitEmpCertificate": Rpt1a = SetRptAitEmpCertificate(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptExecutiveSalaryFB": Rpt1a = SetRptExecutiveSalaryFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSalSumEOT": Rpt1a = SetRptSalSumEOT(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptMonHolidayAllowance": Rpt1a = SetRptMonHolidayAllowance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptWorkerFestBonusFB": Rpt1a = SetRptWorkerFestBonusFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptDayTotOTSum": Rpt1a = SetRptDayTotOTSum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBankStatement": Rpt1a = SetRptBankStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBankStatementBonus": Rpt1a = SetRptBankStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptMonthWiseOTSheet": Rpt1a = SetRptMonthWiseOTSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSalRequisition": Rpt1a = SetRptSalRequisition(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSalRequisitionFootbed": Rpt1a = SetRptSalRequisition(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBankAccStatement": Rpt1a = SetRptBankStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBreakFastBill": Rpt1a = SetRptBreakFastBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBreakFastBillFB": Rpt1a = SetRptBreakFastBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBreakFastBillFootbed": Rpt1a = SetRptBreakFastBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptNightBill": Rpt1a = SetRptBreakFastBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptNightBillFB": Rpt1a = SetRptBreakFastBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptNightBillFootbed": Rpt1a = SetRptBreakFastBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptIncrementStatus": Rpt1a = SetRptIncrementStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptMonOTSumSectionWise": Rpt1a = SetRptMonOTSumSectionWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSecWiseOTReqSummary": Rpt1a = SetRptSecWiseOTReqSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptMatLvPaymentSheet": Rpt1a = SetRptMatLvPaymentSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptFinalSettPayment": Rpt1a = SetRptFinalSettPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptMonthlySalDataSheet": Rpt1a = SetRptMonthlySalDataSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptTransAllowance": Rpt1a = SetRptTransAllowance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptFestivalBonus": Rpt1a = SetRptFestivalBonus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                // att
                case "RD_81_HRM.RD_83_Att.RptDailyAttenEmp": Rpt1a = SetRptDailyAttenEmp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyLateAtt": Rpt1a = SetRptDailyLateAtt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyLateAttFB": Rpt1a = SetRptDailyLateAtt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyLateAttFactoryFB": Rpt1a = SetRptDailyLateAtt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptMonAttendance": Rpt1a = SetRptMonAttendance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptMonAttendanceFB": Rpt1a = SetRptMonAttendanceFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpDayAttnSumry": Rpt1a = SetRptEmpDayAttnSumry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAttSum": Rpt1a = SetRptDailyAttSum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptHRMonthlyLateSum": Rpt1a = SetRptHRMonthlyLateSum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.rptMonthyLateAttnEmp": Rpt1a = SetrptMonthyLateAttnEmp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.rptMonthyEarlyLeaveEmp": Rpt1a = SetrptMonthyEarlyLeaveEmp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpShiftAll": Rpt1a = SetRptEmpShiftAll(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptAttnLog": Rpt1a = SetRptAttnLog(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptAttnShifting": Rpt1a = SetRptAttnShifting(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptAttendanceApp": Rpt1a = SetRptAttendanceAp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpAttenSummary": Rpt1a = SetRptMonAttenSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptMonAttenSummary": Rpt1a = SetRptMonAttenSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAttnAftrLeave": Rpt1a = SetRptDailyAttnAftrLeave(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptMonAttnCountSumSecWise": Rpt1a = SetRptMonAttnCountSumSecWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "RD_81_HRM.RD_84_Lea.RptLeaveFormEng": Rpt1a = SetRptLeaveFormEng(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptAllEmpLeavStatus": Rpt1a = SetRptAllEmpLeavStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptAllEmpLeavStatusFb": Rpt1a = SetRptAllEmpLeavStatusFb(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.EmpLeaveInfo": Rpt1a = SetEmpLeaveInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSalarySummarySheet01": Rpt1a = SetRptSalarySummarySheet01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBankFordLetter": Rpt1a = SetRptBankFordLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpConfirm": Rpt1a = SetRptEmpConfirm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptTransList": Rpt1a = SetRptTransList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAbsent": Rpt1a = SetRptDailyAbsent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyPresent": Rpt1a = SetRptDailyPresent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAbsentFb": Rpt1a = SetRptDailyAbsentFb(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAbsentImgFb": Rpt1a = SetRptDailyAbsentFb(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptMonthlyAbsentFb": Rpt1a = SetRptDailyAbsentFb(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCard": Rpt1a = SetRptEmpIDCard(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardBangla": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardBanglaFB": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardBanglaFootbed": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardBanFootbed": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardStafFB": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardStafFootbed": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardExeFB": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardExeFootbed": Rpt1a = SetRptEmpIDCardBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpIDCardStaff": Rpt1a = SetRptEmpIDCardStaff(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptFactorySalaryCash": Rpt1a = SetRptFactorySalaryCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpMissAttn": Rpt1a = SetRptEmpMissAttn(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpDailyAbdent": Rpt1a = SetRptEmpDailyAbdent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpDailyPresentAbdent": Rpt1a = SetRptEmpDailyAbdent(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpJobCard": Rpt1a = SetRptEmpJobCard(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptMonthSalSum": Rpt1a = SetRptMonthSalSum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptWorkerPaySlip": Rpt1a = SetRptWorkerPaySlip(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptWorkerPaySlipFB": Rpt1a = SetRptWorkerPaySlipFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptWorkerDailyEot": Rpt1a = SetRptWorkerDailyEot(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpJobCardAudit": Rpt1a = SetRptEmpJobCardAudit(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpJobCardFBMulti": Rpt1a = SetRptEmpJobCardFBMulti(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpJobCardRealFB": Rpt1a = SetRptEmpJobCardFBMulti(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptExecutiveJobCardFB": Rpt1a = SetRptEmpJobCardFBMulti(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptEmpJobCardRealMonAttn": Rpt1a = SetRptEmpJobCardFBMulti(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_90_PF.RptMonthlyPF": Rpt1a = SetRptMonthlyPF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_90_PF.RptMonthlyPFFB": Rpt1a = SetRptMonthlyPFF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_90_PF.RptMonthlyPFPaySheetFB": Rpt1a = SetRptMonthlyPFF(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_97_MIS.RptEmpSalInform": Rpt1a = SetRptEmpSalInform(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_89_Pay.RptFactResignSalCash": Rpt1a = SetRptFactResignSalCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptResignSalSheet": Rpt1a = SetRptResignSalSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEmpNomList": Rpt1a = SetRptEmpNomList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.DailyAttenSumry": Rpt1a = SetDailyAttenSumry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.DailyAttenSumryFB": Rpt1a = SetDailyAttenSumryFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.DailyAttenSumryFootbed": Rpt1a = SetDailyAttenSumryFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_83_Att.RptDailyAttenSummarySkillWise": Rpt1a = SetRptDailyAttenSummarySkillWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAttenSummaryFB": Rpt1a = SetRptDailyAttenSummaryFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAttnSumCatFB": Rpt1a = SetRptDailyAttnSumCatFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDailyAttnSumCatFootbed": Rpt1a = SetRptDailyAttnSumCatFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "RD_81_HRM.RD_83_Att.RptDailyAttenSummary": Rpt1a = SetRptDailyAttenSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptHRAllEmpStatus": Rpt1a = SetRptHRAllEmpStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptHRAllEmpInfo": Rpt1a = SetRptHRAllEmpInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptEmpBonusInfo": Rpt1a = SetRptEmpBonusInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptEmpBonusCash": Rpt1a = SetRptEmpBonusCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptMissInPunch": Rpt1a = SetRptMissInPunch(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptMissOutPunch": Rpt1a = SetRptMissOutPunch(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDoubtfulPunch": Rpt1a = SetRptDoubtfulPunch(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptDayWiseOTSheet": Rpt1a = SetRptDayWiseOTSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_92_MGT.RptFirstLetter": Rpt1a = SetRptFirstLatter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptSecondLetter": Rpt1a = SetRptSecondLatter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptThirdLetter": Rpt1a = SetRptThirdLatter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptSubsisBonusAllowance": Rpt1a = SetRptSubsisBonusAllowance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptLencashment": Rpt1a = SetRptLencashment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptEmpMobBill": Rpt1a = SetRptEmpMobBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptEmpMobBillFbNew": Rpt1a = SetRptEmpMobBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptEmpMobBillFbSum": Rpt1a = SetRptEmpMobBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEmpAllInformationENG": Rpt1a = SetRptEmpAllInformationENG(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptEmpAllInformationBN": Rpt1a = SetRptEmpAllInformationBN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpSattelmentBangla": Rpt1a = SetRptEmpSattelmentBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_93_AnnInc.RptEmpIncrFb": Rpt1a = SetRptEmpIncrFb(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpList": Rpt1a = SetRptEmpList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpListInactive": Rpt1a = SetRptEmpListInactive(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpListFB": Rpt1a = SetRptEmpListFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptActiveEmpListFB": Rpt1a = SetRptActiveEmpListFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptActiveEmpListWPic": Rpt1a = SetRptActiveEmpListFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptInActiveEmpListFB": Rpt1a = SetRptInActiveEmpListFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptInActiveEmpListWPic": Rpt1a = SetRptInActiveEmpListFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpSattelmentFB": Rpt1a = SetRptEmpSattelmentFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpSattlementBanFB": Rpt1a = SetRptEmpSattelmentFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpAging": Rpt1a = SetRptEmpList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_92_MGT.RptEmpSepList": Rpt1a = SetRptEmpSepList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_86_All.RptEmpArrSalary": Rpt1a = SetRptEmpArrSalary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_86_All.RptMedicalCertificate": Rpt1a = SetRptMedicalCertificate(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_83_Att.RptHRAllEmpStatusFB": Rpt1a = SetRptHRAllEmpStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_89_Pay.RptBankFordLetterFB": Rpt1a = SetRptBankFordLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_89_Pay.RptEmpBonusSummaryFB": Rpt1a = SetRptEmpBonusSummaryFB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_86_All.RptOtherDeduction": Rpt1a = SetRptOtherDeduction(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_86_All.RptEarnLeave": Rpt1a = SetRptEarnLeave(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_86_All.RptEarnLvPaySheet": Rpt1a = SetRptEarnLeave(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptEnLvBankAccStmnt": Rpt1a = SetRptEarnLeave(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_84_Lea.RptEarnLvPayReq": Rpt1a = SetRptEarnLvPayReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "RD_81_HRM.RD_93_AnnInc.RptAnnualIncrement": Rpt1a = SetRptAnnualIncrement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_93_AnnInc.RptPropAnnualIncrement": Rpt1a = SetRptAnnualIncrement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_86_All.RptEmpTifinList": Rpt1a = SetRptEmpTifinList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RD_81_HRM.RD_81_Rec.RptManPowerBgdState": Rpt1a = SetRptManPowerBgdState(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;




                    #endregion
            }

            Rpt1a.Refresh();
            return Rpt1a;
        }

        private static LocalReport SetShippingMark(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.ShippingMark>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetShippingMarkV2(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.ShippingMark>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMatMaster(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_05_ProShip.EClassPlanning.RptMaterialMaster>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_05_ProShip.EClassPlanning.BOM>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptArticleWiseLot(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptProcessBaseProdPlan(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_05_ProShip.EClassPlanning.RptProcessBaseProdPlan>)rptDataSet));
            return rpt1a;
        }
        
        private static LocalReport SetRptArticleLayout(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_05_ProShip.EClassPlanning.ArticleLayoutClass>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptWrkCapMon(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_05_ProShip.EClassPlanning.EWrkCapMon>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport RptOrderStatus(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_05_ProShip.EClassPlanning.OrderStatusRpt>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport RptBOMVsReceived(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BOMvsReceidved>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptBOMVsReceivedMaterials(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BOMvsReceidved>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptBOMVsReceivedSpecification(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BOMvsReceidved>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptWorkOrderVsSupply(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.WorkOrderVsSupply>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptWorkOrderVsSupply2(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.WorkOrderVsSupply>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptWorkOrderVsSupply3(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.WorkOrderVsSupply>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptIndSupPurchase(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.IndSupPurchase>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMatWisePO(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_Matwisepo>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMatWisePOSummery(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_Matwisepo>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptIncomingMatInspctFrmt0(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.RptMatInspctReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptIncomingMatInspctFrmt1(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.RptMatInspctReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptIncomingMatInspctFrmt2(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.RptMatInspctReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptIncomingMatInspctFrmt3(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.RptMatInspctReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptSeasonWiseSupplySummary(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object rptDataSet3)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptSeasonSummary>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_09_Commer.RptCountrySummary>)rptDataSet2));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_09_Commer.RptSeasonSummary>)rptDataSet3));
            return rpt1a;
        }

        private static LocalReport SetRptSeasonBySeasonSupplySummary(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object rptDataSet3)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptSeasonBySeasonSupplierSummary>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_09_Commer.SeasonSum>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptSeasonOverviewOfMaterials(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object rptDataSet3)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptSeasonOverviewOfMaterials>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptRawMatSupLeadTime(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptRawMatSupLeadTime>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_09_Commer.RptSupLeadTimeSummary>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptLCAllInfo(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.AllLCInfolist>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptBomWiseMatSummary(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List< SPEENTITY.C_09_Commer.BomWiseMatSummary>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptDayWisPrchse(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptDayWisPrchse>)rptDataSet));
            return rpt1a;
        }
        
        private static LocalReport SetRptLcCosting2(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.EClassLCCosting>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_09_Commer.EClassLCCosting>)rptDataSet2));
            //rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_09_Commer.EClassLCCosting>)userDataset3));
            return rpt1a;
        }

        private static LocalReport RptMatPriceVariance(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptMatPriceVariance>)rptDataSet));
            return rpt1a;
        }
        
        private static LocalReport RptMatSpcfPriceVariance(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptMatPriceVariance>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport RptPurMktSurvey(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.RptPurMktSurvey1>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_09_Commer.RptPurMktSurvey2>)rptDataSet2));

            return rpt1a;
        }

        private static LocalReport RptPOShipmentLog(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.POShipLog>)rptDataSet));

            return rpt1a;
        }

        private static LocalReport RptWeekPlanWiseMat(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_WeeklyWiseMat>)rptDataSet));

            return rpt1a;
        }

        private static LocalReport SetRptInActiveEmpListFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptActiveEmpListFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptConfirmletter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.BO_EmpConfirm>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptBankOpening(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptJoiningLetterExc(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptSampleInterfaceInquery(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptAppoinmentLetterExec(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)

        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptAppoinmentLetterNonExc(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>)rptDataSet));

            return rpt1a;
        }
        private static LocalReport SetRptAppsPart(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>)rptDataSet));

            return rpt1a;
        }
        private static LocalReport SetRptEmsalcertificate(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));

            return rpt1a;
        }
        private static LocalReport SetRptDocInformation(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_34_Mgt.EclassDocinformation>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_34_Mgt.EclassDocFilesInformation>)rptDataSet2));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_34_Mgt.EclassDocNotes>)userDataset));


            return rpt1a;
        }
        private static LocalReport SetRptEmpIncrFb(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            return rpt1a;
        }
        private static LocalReport SetRptEmpList(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpSepList(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.RptEmpSeparation>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpListInactive(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptEmpListFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptCPF(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptMonthWiseAbscent(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmpMonthwiseAbscent>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptWorkerConfirmLetter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEvaluationForm(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEnvelope(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEnvelopePresent(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpArrSalary(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.EmpArrearSalaryList>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpTifinList(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptManPowerBgdState(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.ManPowerBgdState>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMedicalCertificate(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.EmpAllInfo>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptExportPlanVsAchiv(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.PerOrderStatus.ExportPlanVsAchiv>)rptDataSet));

            return rpt1a;
        }
        private static LocalReport SetRptHisMaterial(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.HisMaterial>)rptDataSet));

            return rpt1a;
        }
        private static LocalReport SetRptDayWisePurchase(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.DayWisePurchase>)rptDataSet));

            return rpt1a;
        }

        private static LocalReport SetRptPurchaseReturn(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptSumOutstdng(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.SuppOutStndStatmnt>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptIQCInspection(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.IQCInspectionReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMasterPOReport(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.MasterOReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptEmpSattelmentBangla(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)rptDataSet2));
            return rpt1a;
        }
        private static LocalReport SetRptEmpSattelmentFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)rptDataSet2));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)userDataset));
            return rpt1a;
        }
        private static LocalReport SetRptEmpAllInformationENG(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformation>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptEmpAllInformationBN(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptLencashment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeanChasment>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptEmpMobBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.EmpMobBillInfo>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport SetRptLCCosting(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.LCCostingPrint>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptBBLMatInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.EClassBBLMatInfo>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptLCQCInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_AllLCInfo.LCQCPrint>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptRequisitionVsReceived(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_Meterails>)RptDataSet));

            return Rpt1a;
        }


        private static LocalReport SetRptGetDateWiseMatIssueInfo(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matdetails>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matsummary>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptDayWiseMatCons(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.DayWiseMatCons.DPRWiseMattCons>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptDailyProdBalSheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.RptDailyProdBalanceSheet>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptSizeProdBalSheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.RptSizeProdBalanceSheet>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptQltyAndProductivity(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.RptQltyNdProd>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_15_Pro.BO_Production.RptQltyNdProd2>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptMatIssueDetails(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.MatIssueDetailsReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMntlyProdAnalytical(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.RptMonthlyProdAnalyticalReport>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptDefectParChart(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.EclassDefectParChart>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptDefectOrder(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.EclassDefectOrder>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptDateWiseMaterialShowIssuewise(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matsummaryreport>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matdetailsreport>)rptDataSet2));
            return rpt1a;
        }



        private static LocalReport SetRptExportRealization(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankData>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptShipment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptExportSummery(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportSummery>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptExportUnrealization(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportSummery>)RptDataSet));
            return Rpt1a;
        }
        
        private static LocalReport SetRptIncntvDeclaration(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptIncntvDeclaration>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptRptLCCost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.EClassLCCosting>)RptDataSet));

            return Rpt1a;
        }


        private static LocalReport SetRptProductionManually(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualiProCost>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptSubsisBonusAllowance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.ECurSubAllowance>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalarySumFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.MonthlySalSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalSumEOT(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.MonthlySalSummaryEOT>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonHolidayAllowance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.MonHolidayAllownace>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptWorkerFestBonusFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptFestivalBonus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDayTotOTSum(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.DayTotOTSum>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBankStatement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.BankStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBreakFastBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptFoodAllowance>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptTransAllowance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptTransAllowance>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptFestivalBonus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptFestBonus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptIncrementStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptIncrement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonOTSumSectionWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptMonthlyOTSumSectionWise>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSecWiseOTReqSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptOTReqSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthWiseOTSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalRequisition(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpPerAppraisal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.EmpPerAppraisal01>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.EmpPerAppraisal02>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptThirdLatter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.BO_EmpSep.EmpSep01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSecondLatter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.BO_EmpSep.EmpSep01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFirstLatter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.BO_EmpSep.EmpSep01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptNOC(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.BO_EmpSep.EmpSep01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpPendingConfirmation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.RptEmpConfirmation>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRpttMatUnused(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.MatUnused>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptMtrReqInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.MtrReqDetails>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptMaterialIssue(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EclassMaterialIssue>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport SetRptMaterialIssueStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EclassMaterialIssue>)RptDataSet));
            return Rpt1a;
        }
        
        private static LocalReport SetRptInvQuantityBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.RptInvQtyBasis>)RptDataSet));
            return Rpt1a;
        }
        
        private static LocalReport SetRptMatTransfer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.RptMatTransfer>)RptDataSet));
            return Rpt1a;
        }
       
        //private static LocalReport SetRptPerMatTransfer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        //{
        //    Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.RptMatTransfer>)RptDataSet));
        //    return Rpt1a;
        //}

        private static LocalReport SetRptProdReqApp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan1>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan3>)RptDataSet2));

            return Rpt1a;
        }
        private static LocalReport SetRptEmpBonusCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpBonusheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpBonusInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpBonusheet>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptHRAllEmpStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.Empstatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptHRAllEmpInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMissInPunch(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMissOutPunch(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDoubtfulPunch(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDayWiseOTSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptDayWiseOTSheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalesRealizeCer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate>)RptDataSet));
            return Rpt1a;
        }
        
        private static LocalReport SetRptSalesRealizeCert2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyAttenSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttenSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyAttenSummarySkillWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttenSummarySkillWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyAttenSummaryFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttenSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDailyAttnSumCatFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttnSummaryCatWise>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDeliveryChallan(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDeliveryChallanEdison2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDeliveryChallanFBFrmt2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetDailyAttenSumry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DayAttnSumry>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetDailyAttenSumryFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DayAttnSumry>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttnSummary>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptExportPlanInEdit(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan1>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan2>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan3>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetListOfIssuedCheque(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.ListOfIssuedCheque>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBankReconciliation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.RptBankReconciliation>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMatPriceSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.RptMatPriceSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillRegister(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassDB_BO.OnlineBillReg>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptGeneralBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassGenReq>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCashBankStatement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassDB_BO.CashBankStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBomApproved(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EClassLC.BomApproval01>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_03_CostABgd.EClassLC.BomApproval02>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_03_CostABgd.EClassLC.BomApproval03>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptPreOrderStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.PerOrderStatus.PerOrdrStat>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPreProdStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.PerOrderStatus.PerProdStat>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPreProdStatusDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.PerOrderStatus.PerProdStatDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpNomList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.Empnomineelist>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptResignSalSheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptFactResignSalCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpSalInform(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpSalInf>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthlyPF(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_90_PF.BO_ClassPF>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthlyPFF(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_90_PF.BO_ClassPF>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptEmpJobCardAudit(LocalReport Rpt1a, object RptDataSet, object rptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard01>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard02>)rptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard03>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpJobCardFBMulti(LocalReport Rpt1a, object RptDataSet, object rptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptExecutiveJobCardFB(LocalReport Rpt1a, object RptDataSet, object rptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard01>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard02>)rptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard03>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptWorkerDailyEot(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.EclassEmpEOT>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptWorkerPaySlip(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptWorkerPaySlipFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthSalSum(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.EclassMonthSalSummary>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.EclassMonthSalSummary>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpJobCard(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard01>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard02>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard03>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpDailyAbdent(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAbsentInf>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDailyAttnAftrLeave(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptAttnAftrLeave>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonAttnCountSumSecWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptMonAttnCountSum>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccDetailsScdl(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.AccDetailsSchedule>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptIncomeStatement12(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_32_Mis.EClassSales_02.IncomStatment12>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSupCusTxVt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.SupCustTxVt>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSpLedger(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.AccSpLedger>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProdReq(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProdReqPrint>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProditmPrint>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpMissAttn(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpMissAttn>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptAccOpLev2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.AccOpLevel2>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccountsOpening(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.AccOpening>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFactorySalaryCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptImportApproval(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_10_Procur.EClassProcur.VendorInfo>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptImportApproval2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptImportApproval2Accessories(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>)UserDataset));
            return Rpt1a;
        }
        
        private static LocalReport SetRptLocalApproval2Outsol(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.EClassPurchaseOrdr>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseOrderTerms>)RptDataSet2));
            //Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpIDCardStaff(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfo>)RptDataSet2));
            return Rpt1a;
        }


        private static LocalReport SetRptMaterialsCodeBook(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccounts.ResCodeBook>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_21_Acc.EClassAccounts.Unit>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpIDCardBangla(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfoBangla>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpIDCard(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfo>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyAbsent(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptDailyPresent(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyPresent>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptDailyAbsentFb(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsentFb>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport SetRptTransList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeTransLst>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptEmpConfirm(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.BO_EmpConfirm>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport SetRptBankFordLetter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.BankFord>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpBonusSummaryFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.BonusSummary>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptOtherDeduction(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.RptOtherDedction>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEarnLeave(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.RptEarnLeaveEnCashment>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEarnLvPayReq(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptEarnLvPayRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAnnualIncrement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptAnnualIncrement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptLcInfowithReq(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BO_LCOpening>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_09_Commer.BO_LCGenInfo>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptAitEmpCertificate(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRdlcAITPurpose(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalarySummarySheet01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.SummarySalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMerForceEdit(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassMerForceEdit>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBOMApprvList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.OrderDetails>)RptDataSet));
            return Rpt1a;
        }
        
        private static LocalReport SetRptPfiList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.RptPfiList>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptOrderSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.OrderDetails>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassPartInOrder>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptLCPosition(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.EClassLCStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCommonCostingSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.CommonMterailsCal>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCommonConsSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.CommonMterailsCal>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCostingSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EClassCostingSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAllArtclSum(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EClassCostingSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMatReqImport(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)UserDataset));


            return Rpt1a;
        }
        private static LocalReport SetRptMatReqImportFBPakingInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)UserDataset));


            return Rpt1a;
        }

        private static LocalReport SetRptMatReqLocal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpAitCertificate(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptAitPurpose(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpMonthSumm(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.EmpMonthSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetEmpLeaveInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAllEmpLeavStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAllEmpLeavStatusFb(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatusFb>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptLeaveFormEng(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveApp>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillSticker(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_34_Mgt.OthReqStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDailyLateAtt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptMonthyEarlyLeaveEmp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpSatausLate>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptMonthyLateAttnEmp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpSatausLate>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptHRMonthlyLateSum(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.MonthlyLateAttdendace>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpDayAttnSumry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpDayAttnSumry>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDailyAttSum(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpDayAttnSumry03>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonAttendance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendence>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonAttendanceFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendenceFB>)RptDataSet));
            return Rpt1a;
        }
        //private static LocalReport SetRptDailyAbsent(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        //{
        //    Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_34_Mgt.OthReqStatus>)RptDataSet));
        //    return Rpt1a;
        //}

        private static LocalReport SetRptAttendanceAp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EclassAttApp>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonAttenSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EMonAtten>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptAttnShifting(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.Shifting01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAttnLog(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendncLog>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpShiftAll(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendence>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyAttenEmp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttnIdWise>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPreCostingSheetFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassConsumption>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPreSamCostingSheetFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassConsumptionFB>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPreCostingSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassConsumption>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassCommonCost>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptPdBook(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassPdBook>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptKnifEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassKnifEntry>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSamReport(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassSamReport>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSamTagPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.SamTagPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPackListPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PackList>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSampleProdRequisition(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.RptSamProdReq1>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.RptSamProdReq2>)RptDataSet2));
            return Rpt1a;
        }
        
        private static LocalReport SetRptPreSampCostingSheetFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassConsumptionFB>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassCommonCostSam>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<SPEENTITY.C_01_Mer.EclassCurinfo>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptConsumptionSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassConsumption>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConsumptionSheetFB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassConsumption>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport RptManPowerBudgtActual(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.ManPowerBudgtActual>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptSalarySheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptExecutiveSal(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptWorkerSalFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptSalaryNONOTFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptExecutiveSalaryFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptPaymSlipBangla(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptPaySlipExe(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptStaffSalary(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptCarSubAllowance(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.ECurSubAllowance>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpSattelment(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)rptDataSet2));
            return rpt1a;
        }
        private static LocalReport SetRptPromotion(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmpPromotion>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpApplicationForm(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptProbEvaluationForm(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptAllDocBN(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptEmpMatFirstLetter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpMatSecondLetter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpLeaveLetter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpResignLetter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpSelfSupport(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptEmpIncrletter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeIncrInfo>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpPromotionletter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeePromotionInfo>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpRegisterReport(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeRegister>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpSkillReport(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeSkill>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptManpowerBudgt(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.ManPowerBudgt>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptPurMktSurvey(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.MkrServay02>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_10_Procur.EClassProcur.MkrServay03>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptPOLocal(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.EClassPurchaseOrdr>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseOrderTerms>)rptDataSet2));
            return rpt1a;
        }
        
        private static LocalReport SetRptPOLocalFBJobWork(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.EClassPurchaseOrdr>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseOrderTerms>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptPurReqLocal(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.RptRequisition>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptPurMktSurveySup03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_10_Procur.EClassProcur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptPurMktSurveySup02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_10_Procur.EClassProcur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_10_Procur.EClassProcur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptHRInterfaceLeave(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.HrInterfaceLeave>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptLeaveApp(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveApp>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMonWiseLeaveBR(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeave>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptLeavAppFormEng(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatusInfoEng>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptLeavAppFormBn(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatusInfoEng>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptHREmpLeaveReg(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeaveReg>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptEmpLeavStatus(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatus>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpLeavStatus02(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatus02>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMatLeavePaySheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMatLeavePaySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMatLvPaymentSheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMatLeavePaySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptFinalSettPayment(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptFinalSettPaySheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMonthlySalDataSheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonSalaryDataSheet>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMobLst(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpMobLst>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptLeaveFormBang(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveApp>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptOverTimeSal(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.OverTimeSal>)rptDataSet));
            return rpt1a;
        }
        //Created By Md.Ibrahim Khalil
        private static LocalReport SetRptProdProcess(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptProdinfo(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdDetails>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetStandardCostSheetAll(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EClassLC.StdCostSheetRPT>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptRptSalesCon(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EClassLC.BudgetResource>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptOrderrStatus(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EClassLC.LcDetailsInfo>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptPurMktSurvey02(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.ComparativeStatementCreate>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_11_RawInv.EClassPurchase.ComparativeStatementCreate>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptPurMRR(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseMRR>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptPurMRRFB(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseMRR>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptPurchaseOrder(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.EClassPurchaseOrdr>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseOrderTerms>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptPurReq(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.EClassPurchase.RptRequisition>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptFrdLetter(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassFrdletter>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptBillOfExchange(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptBenefiDecla(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptGSP(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptPackingList(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptPackingListFBFrmt2(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptPackingListFBFrmt1(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptCommercialInvoic(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptCommInvoiceFBFrmt3(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptCommInvoiceFBFrmt2(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptCommInvoiceFBFrmt1(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptProformaInv(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_03_CostABgd.ProformaInvTrms>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptProformaInvCCC(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<SPEENTITY.C_03_CostABgd.ProformaInvTrms>)rptDataSet2));
            return rpt1a;
        }

        private static LocalReport SetRptCheque2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Payee", hshParm["Payee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            return Rpt1a;
        }
        private static LocalReport SetRptCheque(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Payee", hshParm["Payee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptAllTrans(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.AccDTransactrtionAll>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptDaliyTrans(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.AccDTransaction>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptAccountLdger(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.AccLeadger>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptCashFlow(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassCashFlowD>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptPrintVoucherSP(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EclassPrintVoucherBr>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptLCCostVsEx(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.MLCCost>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProtarVsAchieve(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.WorkVsAchievment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFGCentralStore(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_17_GFInv.FGCenterStorec>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptFGInspectionEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_17_GFInv.FGInspection>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccDetTrialBalance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable reportP = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("companyname", (string)reportP["companyname"]));
            Rpt1a.SetParameters(new ReportParameter("Header", (string)reportP["Header"]));
            Rpt1a.SetParameters(new ReportParameter("date", (string)reportP["date"]));
            Rpt1a.SetParameters(new ReportParameter("txtuserinfo", (string)reportP["txtuserinfo"]));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.DtlOfBalanceSheet1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetAccTrialBl1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable reportP = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("companyname", (string)reportP["companyname"]));
            Rpt1a.SetParameters(new ReportParameter("frmdate", (string)reportP["frmdate"]));
            Rpt1a.SetParameters(new ReportParameter("todate", (string)reportP["todate"]));
            Rpt1a.SetParameters(new ReportParameter("txtHeader", (string)reportP["txtHeader"]));
            Rpt1a.SetParameters(new ReportParameter("txtdate", (string)reportP["txtdate"]));
            Rpt1a.SetParameters(new ReportParameter("txtuserinfo", (string)reportP["txtuserinfo"]));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetTrialBl", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.AccTrialBl1>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptBBlcInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.EClassBBLCInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBBLCPayStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_09_Commer.BBLCPayStatus>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptShareQty(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_21_Acc.EClassAccVoucher.Shequity>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptCentralStore(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_11_RawInv.CentralStore>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptOtherReq(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_34_Mgt.OthReqStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptOrdProShipAll(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_15_Pro.BO_Production.OrdProdShipment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMLCOrderCost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_03_CostABgd.LcOrdercosting>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptLCAnalysis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.LCAnalysis>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptOrderStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.OrderStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSampleInqTopSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EClassSampleInq>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSampleEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptissueDelvBR(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_22_Sal.Sales_BO.EissueDelvPrint>)rptDataSet));
            return rpt1a;
        }
        
        private static LocalReport SetRptIndProStock(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_22_Sal.Sales_BO.RptIndProStockModel>)rptDataSet));
            return rpt1a;
        }
        
        private static LocalReport SetRptSchdofFxtAssets(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_27_Fxt.RtpeFixAssetsSchu>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport RptFixAssetDepartment(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<SPEENTITY.C_27_Fxt.FixedAssetRpt>)rptDataSet));
            return rpt1a;
        }

    }
}