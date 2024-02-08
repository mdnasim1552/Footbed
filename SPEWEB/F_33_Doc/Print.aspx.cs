using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.Collections;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SPEWEB.F_33_Doc
{
    public partial class Print : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "AppPrint":
                    this.DocumentFinalPrint();
                    break;
                            }


        }
      
        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void DocumentFinalPrint()
        {

            string docno = this.Request.QueryString["docno"].ToString();        

            Hashtable hst = (Hashtable)Session["tblLogin"];

        
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_DOC", "DOC_DETAILS_INFORMATION", docno, "",
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
               
                return;
            }

            DataTable dt = ds1.Tables[0];
            List<SPEENTITY.C_34_Mgt.EclassDocinformation> lst = dt.DataTableToList<SPEENTITY.C_34_Mgt.EclassDocinformation>();
            List<SPEENTITY.C_34_Mgt.EclassDocFilesInformation> filelist = ds1.Tables[1].DataTableToList<SPEENTITY.C_34_Mgt.EclassDocFilesInformation>();
            List<SPEENTITY.C_34_Mgt.EclassDocNotes> notes = ds1.Tables[2].DataTableToList<SPEENTITY.C_34_Mgt.EclassDocNotes>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_33_Doc.RptDocInformation", lst, filelist, HiddenSameValue(notes));
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("buyer", ds1.Tables[1].Rows[0]["buyername"].ToString()));
            //rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds1.Tables[1].Rows[0]["inqdat"]).ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("inqnum", "INQUERY NO: " + ds1.Tables[1].Rows[0]["inqno1"]));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Document Details Information"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=PDF', target='_self');</script>";
        }

        private List<SPEENTITY.C_34_Mgt.EclassDocNotes> HiddenSameValue(List<SPEENTITY.C_34_Mgt.EclassDocNotes> lst)
        {
            if (lst.Count == 0)
            {
                return lst;
            }
            string gcod = "";
            int i = 0;
            foreach(SPEENTITY.C_34_Mgt.EclassDocNotes list in lst)
            {
                if (gcod == list.gcod)
                {
                    lst[i].username = "";
                    lst[i].aprvdat = "";
                    
                }
                else
                {
                    gcod = list.gcod;
                }
                i++;
            }
            return lst;
        }


    }
}