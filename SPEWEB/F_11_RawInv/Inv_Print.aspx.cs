using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_11_RawInv
{
    public partial class Inv_Print : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                
                case "MatReqIntrfce":
                    this.lnkPrint(null, null);
                    break;
               
                case "GatePass":
                    this.PrintGatePass();
                    break;
            }
        }


        private void PrintGatePass()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string gatepNo = Request.QueryString["genno"].ToString();
            string curntDate = DateTime.Now.ToString("dd-MMM-yyyy");

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPURGERPASSINFO", gatepNo, curntDate, "", "", "", "", "", "", "");

            if (ds1 == null) return;

            if (ds1.Tables[0].Rows.Count == 0) return;

            DataTable dtGetPass = ds1.Tables[0];
            DataTable dtGetPass2 = ds1.Tables[1];

            var lst = dtGetPass.DataTableToList<SPEENTITY.C_11_RawInv.RptMatTransfer>();

            string orderqty = lst.Sum(x => x.getpqty).ToString("#,##0.00;(#,##0.00); ");

            string receiver = "";
            string receiveradd = "";
            string getpdat = "";
            if (dtGetPass2.Rows.Count > 0)
            {
                receiver = dtGetPass2.Rows[0]["receiver"].ToString();
                receiveradd = dtGetPass2.Rows[0]["recvadd"].ToString();
                getpdat = Convert.ToDateTime(dtGetPass2.Rows[0]["getpdat"]).ToString("dd-MMM-yyyy");
            }

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_11_RawInv.RptMatTransfer", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptitle", "GATE PASS"));
            Rpt1.SetParameters(new ReportParameter("serialno", gatepNo));
            Rpt1.SetParameters(new ReportParameter("orderqty", orderqty));
            Rpt1.SetParameters(new ReportParameter("receiver", receiver));
            Rpt1.SetParameters(new ReportParameter("receiveradd", receiveradd));
            Rpt1.SetParameters(new ReportParameter("getpdat", getpdat));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }


        protected void lnkPrint(object sender, EventArgs e)
        {
            string CurDate1 = this.Request.QueryString["date"].ToString();
            string mtreqno = this.Request.QueryString["genno"].ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mtreqno, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            var lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.MtrReqDetails>();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Reqno = ds1.Tables[0].Rows[0]["mtreqno"].ToString();
            string tfpactdesc = ds1.Tables[1].Rows[0]["tfpactdesc"].ToString();
            string ttpactdesc = ds1.Tables[1].Rows[0]["ttpactdesc"].ToString();
            
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptMtrReqInfo", lst1, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("from", tfpactdesc));
            rpt1.SetParameters(new ReportParameter("to", ttpactdesc));
            rpt1.SetParameters(new ReportParameter("Reqno", Reqno));
            rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(CurDate1).ToString("dd-MMM-yyyy")));

            string Type = mtreqno.Substring(0, 3);
            switch (Type)
            {
                case "MTR":
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Material Requsition"));
                    break;
                case "LNR":
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Create Loan Requsition"));
                    break;
                case "RetEntry":
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Create Return Requsition"));
                    break;

            }
            
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_Self');</script>";


        }
    }
}