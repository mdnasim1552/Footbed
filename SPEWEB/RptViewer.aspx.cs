﻿using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB
{
    public partial class RptViewer : System.Web.UI.Page
    {
        ReportDocument rpt1 = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PrintOpt"] == null)
                return;

            string PrtOpt = Request.QueryString["PrintOpt"].ToString();
            switch (PrtOpt)
            {
                case "HTML":
                    this.RptHtml();
                    break;
                case "PDF":
                    this.RptPDF();
                    break;
                case "WORD":
                    this.RptMSWord();
                    break;
                case "EXCEL":
                    this.RptMSExcel();
                    break;
                case "GRIDTOEXCEL":
                    this.ExportGridToExcel();
                    break;
            }
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            if (rpt1 != null)
            {
                rpt1.Close();
                rpt1.Dispose();
                GC.Collect();
            }
        }


        protected void RptPDF()
        {
            rpt1 = (ReportDocument)Session["Report1"];
            MemoryStream oStream;
            oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

        }
        protected void RptHtml()
        {
            rpt1 = (ReportDocument)Session["Report1"];
            MemoryStream oStream;
            oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.HTML40);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/html";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
        }

        protected void RptMSWord()
        {
            rpt1 = (ReportDocument)Session["Report1"];
            MemoryStream oStream;
            oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/msword"; //application/msword
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
        }
        protected void RptMSExcel()
        {
            rpt1 = (ReportDocument)Session["Report1"];
            MemoryStream oStream;
            oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel"; //application/msword
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
        }
        protected void ExportGridToExcel()
        {
            try
            {
                //this.form1.Controls.Remove(this.CRViewer1);
                GridView GridView1 = (GridView)Session["Report1"];
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=DataTable.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                this.form1.Controls.Add(GridView1);
                GridView1.RenderControl(hw);


                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception Ex)
            {
                //Label1.Text = Ex.Message;
                return;
            }
        }





    }
}