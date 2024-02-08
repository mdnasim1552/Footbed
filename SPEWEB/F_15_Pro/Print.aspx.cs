using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPEENTITY;
using SPELIB;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_15_Pro
{
    public partial class Print : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        ProcessAccess Budget = new ProcessAccess();
        Common Common = new Common();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "strcv":
                    PrintStoreReceive();
                    break;
                case "reqapp":
                    PrintReqApproval();
                    break;
                case "prodman":
                    ProdManuPrint_Rdlc();
                    break;

                case "proreq":
                    ProdMatReqPrint();
                    break;
                    
                case "PrintReqMulti":
                    PrintMultipleRequisition();
                    break;
                    
                case "SamProdReqMulti":
                    PrintMultipleSampleProdRequisition();
                    break;


            }


        }

        private void ProdMatReqPrint()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();

            string actcode = this.Request.QueryString["pbmno"] == "" ? "" : this.Request.QueryString["pbmno"];
            string DPRno = this.Request.QueryString["dprno"] == "" ? "" : this.Request.QueryString["dprno"];
            string pbdate = this.Request.QueryString["pbdate"] == "" ? "" : this.Request.QueryString["pbdate"];
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string footer = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataSet ds = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RPT_PRODUCTION_REQUISTION", actcode, DPRno, pbdate, "", "", "", "");

            
            if (ds == null)
                return;

            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            var rptlist = dt1.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProdReqPrint>();
            var rptlist2 = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProditmPrint>();
            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_15_Pro.RptProdReq", rptlist, rptlist2, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("date", "Date :" + pbdate));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1a.SetParameters(new ReportParameter("pbmno", rptlist[0].actdesc));
            Rpt1a.SetParameters(new ReportParameter("DPRno", DPRno));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_Self');</script>";
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void PrintStoreReceive()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string orderid = this.Request.QueryString["actcode"] == "" ? "" : this.Request.QueryString["actcode"];
            string preqno = this.Request.QueryString["genno"] == "" ? "" : this.Request.QueryString["genno"];
            string prodid = this.Request.QueryString["prodno"] == "" ? "" : this.Request.QueryString["prodno"];

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "RPTPRODDETAILS", prodid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GET_PROD_INFO", orderid, prodid, preqno, "", "", "", "");
            if (ds2 == null)
                return;
            DataTable dt = ds1.Tables[0];
            DataTable dt1 = ds2.Tables[0];


            var lst = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdDetails>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptProdinfo", lst, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Goods Received Information"));
            rpt1.SetParameters(new ReportParameter("prodid", prodid));
            rpt1.SetParameters(new ReportParameter("refno", ds2.Tables[2].Rows[0]["refno"].ToString()));
            //rpt1.SetParameters(new ReportParameter("mlcno", mlcno));
            rpt1.SetParameters(new ReportParameter("orderno", orderid));
            rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds2.Tables[2].Rows[0]["prddate"]).ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("client", ds2.Tables[3].Rows[0]["buyername"].ToString()));
            rpt1.SetParameters(new ReportParameter("brand", ds2.Tables[3].Rows[0]["brand"].ToString()));
            rpt1.SetParameters(new ReportParameter("color", ds2.Tables[3].Rows[0]["colordesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("article", ds2.Tables[3].Rows[0]["article"].ToString()));
            rpt1.SetParameters(new ReportParameter("sizernge", ds2.Tables[3].Rows[0]["sizerange"].ToString()));
            rpt1.SetParameters(new ReportParameter("trialordr", ds2.Tables[3].Rows[0]["trialordr"].ToString()));
            rpt1.SetParameters(new ReportParameter("trialordrqty", ""));
            rpt1.SetParameters(new ReportParameter("tordrqty", Convert.ToDouble(ds2.Tables[3].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ")));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
        private void PrintReqApproval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");


            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            //string comcod = this.GetCompCode();
            string reqno = this.Request.QueryString["genno"].ToString();
            string mlccod = this.Request.QueryString["actcode"].ToString();
            string process = this.Request.QueryString["process"].ToString() == "" ? "%" : this.Request.QueryString["process"].ToString() + "%";
            string reqdate = this.Request.QueryString["reqdate"].ToString();
            string reqtype = this.Request.QueryString["reqtype"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "GET_DEPARTMENT_WISE_REQUISITION", mlccod, reqno, process, reqtype, "", "", "", "", "");
            if (ds1 == null)
                return;

            var lst1 = ds1.Tables[2].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan1>();
            var lst2 = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan3>();

            string ordrtype = comcod == "5601" ? "Trial Order No" : "Order No";
            string lblordrqty = comcod == "5601" ? "Trial Order" : "Order Qty";
            //string deptname = ds1.Tables[0].Rows[0]["procname"].ToString();


            DataTable dt1 = ds1.Tables[1];
            DataTable dt3 = ds1.Tables[3];
            string orderno = comcod == "5601" ? dt1.Rows[0]["trialordr"].ToString() : dt1.Rows[0]["orderno"].ToString();
            //string imgurl = new Uri(Server.MapPath(dt2.Rows[0]["images"].ToString())).AbsoluteUri;



            //   double tAmt = lst1.Select(p => p.totlprs).Sum();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptProdReqApp", lst1, lst2, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("reqno", "Req No: " + reqno));
            rpt1.SetParameters(new ReportParameter("reqdate", "Req Date: " + reqdate));
            rpt1.SetParameters(new ReportParameter("deptname", "Department Name: " + dt1.Rows[0]["deptdesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Department wise Requisition Approve"));
            rpt1.SetParameters(new ReportParameter("buyername", dt1.Rows[0]["buyername"].ToString()));
            rpt1.SetParameters(new ReportParameter("colordesc", dt1.Rows[0]["colordesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("sizerange", dt1.Rows[0]["sizerange"].ToString()));
            rpt1.SetParameters(new ReportParameter("colororderqty", dt1.Rows[0]["colororderqty"].ToString()));
            rpt1.SetParameters(new ReportParameter("brand", dt1.Rows[0]["brand"].ToString()));
            rpt1.SetParameters(new ReportParameter("article", dt1.Rows[0]["article"].ToString()));
            rpt1.SetParameters(new ReportParameter("lblordrqty", lblordrqty));
            rpt1.SetParameters(new ReportParameter("ordrqty", dt1.Rows[0]["ordrqty"].ToString()));
            rpt1.SetParameters(new ReportParameter("ordrtype", ordrtype));
            rpt1.SetParameters(new ReportParameter("trialorderno", orderno));
            //rpt1.SetParameters(new ReportParameter("images", imgurl));
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), dt3.Rows[i]["sizedesc"].ToString()));

            }

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }

        public void ProdManuPrint_Rdlc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();

            string prodno1 = this.Request.QueryString["genno"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "GET_PRODUCTION_MANUALLY_INFO", prodno1, "", "", "", "", "", "", "", "");

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>();

            string prodno = ds1.Tables[1].Rows[0]["mgrrno1"].ToString();
            string orderName = ds1.Tables[0].Rows[0]["mlcdesc"].ToString();
            string narration = ds1.Tables[1].Rows[0]["remarks"].ToString();
            string date1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mgrrdate"]).ToString("dd-MMM-yyyy");

            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            LocalReport rpt1 = new LocalReport();


            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualiProCost> lst1 = ds1.Tables[2].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassManualiProCost>();



            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptProductionManually", lst, lst1, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Production Manually Report"));

            rpt1.SetParameters(new ReportParameter("prodno", prodno));
            rpt1.SetParameters(new ReportParameter("orderno", orderName));
            rpt1.SetParameters(new ReportParameter("narration", narration));
            rpt1.SetParameters(new ReportParameter("date", date1));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



        }


        public void PrintMultipleRequisition()
        {
            string order = Request.QueryString["mlccod"].ToString();
            string mlccod = order.Substring(0, 12);
            string dayid = order.Substring(12);
            string reqno = Request.QueryString["reqno"].ToString();
            string pbdate = Request.QueryString["pbdate"].ToString();
            string printfrmt = Request.QueryString["printfrmt"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string footer = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "RPT_PRODUCTION_REQUISTION_MULTI", reqno, mlccod, dayid, "", "", "");

            if (ds == null)
                return;

            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            var rptlist = dt1.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProdReqPrint>();
            var rptlist2 = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProditmPrint>();


            string reqno2 = "";
            while (true)
            {
                string reqnoFrmated = reqno.Substring(0, 14);
                reqnoFrmated = reqnoFrmated.Substring(0, 3) + reqnoFrmated.Substring(7, 2) + "-" + reqnoFrmated.Substring(9, 5);

                reqno2 += reqnoFrmated+", ";
                reqno = reqno.Substring(14);

                if (reqno.Length == 0) break;
            }

            reqno2 = reqno2.Trim().Substring(0, reqno2.Length - 2);

            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_15_Pro.RptProdReq", rptlist, rptlist2, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("date", "Date :" + pbdate));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1a.SetParameters(new ReportParameter("pbmno", rptlist[0].actdesc));
            Rpt1a.SetParameters(new ReportParameter("fbordno", dt.Rows[0]["fbordno"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("DPRno", reqno2));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + printfrmt + "', target='_self');</script>";
        }


        public void PrintMultipleSampleProdRequisition()
        {
            string sdino = Request.QueryString["genno"].ToString();
            string printfrmt = Request.QueryString["printfrmt"].ToString();

            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "SAMPLING_MAT_REQUISITION_ITEMS", sdino, "", "");

            if (ds1 == null) return;
            if (ds1.Tables[0].Rows.Count == 0 || ds1.Tables[1].Rows.Count == 0) return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string footer = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var rptlist = ds1.Tables[0].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.RptSamProdReq1>();
            var rptlist2 = ds1.Tables[1].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.RptSamProdReq2>();


            string sdinoFormatted = "";
            while (true)
            {
                string reqnoFrmated = sdino.Substring(0, 14);
                reqnoFrmated = reqnoFrmated.Substring(0, 3) + reqnoFrmated.Substring(7, 2) + "-" + reqnoFrmated.Substring(9, 5);

                sdinoFormatted += reqnoFrmated + ", ";
                sdino = sdino.Substring(14);

                if (sdino.Length == 0) break;
            }

            sdinoFormatted = sdinoFormatted.Trim().Substring(0, sdinoFormatted.Length - 2);

            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_04_Samp.RptSampleProdRequisition", rptlist, rptlist2, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("rptTitle", "Sample Production Requisition"));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1a.SetParameters(new ReportParameter("sdino", sdinoFormatted));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + printfrmt + "', target='_self');</script>";


        }
    }

}