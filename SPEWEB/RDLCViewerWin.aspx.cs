using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Reporting.WinForms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections.Generic;
using System.Web;

namespace SPEWEB
{
    public partial class RDLCViewerWin : System.Web.UI.Page
    {
        LocalReport rpt1 = new LocalReport();
        public List<LocalReport> reportList = new List<LocalReport>();
        private LocalReport rt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PrintOpt"] == null)
                return;

            string PrtOpt = Request.QueryString["PrintOpt"].ToString();
            switch (PrtOpt)
            {

                //case "HTML":
                //    this.RptHtml();GenerateMergedPDF()
                //    break;
                case "PDF":
                    this.RptRDLCPDF();
                    break;
                case "MERGEPDF":
                    this.GenerateMergedPDF();
                    break;
                case "WORD":
                    this.RptMSWord();
                    break;
                case "EXCEL":
                    this.RptMSExcel();
                    break;
                case "DOWNLOAD":
                    this.Export();
                    break;

                case "GRIDTOEXCEL":
                    this.ExportGridToExcel();
                    break;
            }
        }
        protected void LoadReportSceleton()
        {
            rt = new LocalReport();
            rt = (LocalReport)Session["Report1"];
        }
        protected void LoadReportSceleton(LocalReport report)
        {
            rt = new LocalReport();
            rt = report;
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            if (rpt1 != null)
            {
               // rpt1.Close();
                rpt1.Dispose();
                GC.Collect();
            }
        }

        protected void RptRDLCPDF()
        {
            LoadReportSceleton();
            string reportType = "PDF";
            string deviceInfo =
                    "<DeviceInfo>" +
                   "  <OutputFormat>" + reportType + "</OutputFormat>" +
                   //"<EmbedFonts>None</EmbedFonts>" +5
                   "</DeviceInfo>";
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension = string.Empty;
            byte[] bytes = rt.Render(reportType, deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "Application/pdf";
            Response.BinaryWrite(bytes);
        }
        protected void RptHtml()
        {
          
        }
        protected void RptMSWord()
        {
            LoadReportSceleton();
            string title = "Download";
            ReportParameterInfoCollection ps;
            ps = rt.GetParameters();
            if (ps.Count() > 0)
            {
                // ReportParameter paramV = new ReportParameter();
                foreach (ReportParameterInfo p in ps)
                {
                    if (p.Name.ToLower() == "rpttitle")
                    {
                        //paramV.Name = p.Name;
                        //paramV.Values.Add(p.Values[0]);
                        title = p.Values[0];
                        break;
                    }

                }
            }
           
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;           
            byte[] bytes = rt.Render(
               "Word", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            filename = string.Format("{0}.{1}", "ExportToWord", "doc");
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename="+ title + ".doc");
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
             }
        protected void RptMSExcel()
        {
            

            LoadReportSceleton();
            string reportType = "Excel";
            string deviceInfo =

                   "<DeviceInfo>" +
                   "  <OutputFormat>" + reportType + "</OutputFormat>" +
                   "</DeviceInfo>";
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension = string.Empty;
            byte[] bytes = rt.Render(reportType, deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(bytes);

            //FileStream fs = new FileStream("d:\\report1.xls", FileMode.Create);
            ////create Excel file
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();   
        }
        protected void Export()
        {
            LoadReportSceleton();
            string title = "Download";
            ReportParameterInfoCollection ps;
            ps = rt.GetParameters();
            if (ps.Count() > 0)
            {
               // ReportParameter paramV = new ReportParameter();
                foreach (ReportParameterInfo p in ps)
                {
                    if (p.Name.ToLower() == "rpttitle")
                    {
                        //paramV.Name = p.Name;
                        //paramV.Values.Add(p.Values[0]);
                        title = p.Values[0];
                        break;
                    }                  

                }
            }
            string reportType = "PDF";
            string deviceInfo =

                   "<DeviceInfo>" +
                   "  <OutputFormat>" + reportType + "</OutputFormat>" +
                   //"<EmbedFonts>None</EmbedFonts>" +
                   "</DeviceInfo>";
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension = string.Empty;
            byte[] bytes = rt.Render(reportType, deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "Application/pdf";           
            Response.AddHeader("content-disposition", "attachment; filename=" + title + ".pdf");            
            Response.BinaryWrite(bytes);
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

        protected void GenerateMergedPDF()
        {
            reportList =(List<LocalReport>)Session["Report1"];
            List<MemoryStream> pdfStreams = new List<MemoryStream>();

            foreach (LocalReport report in reportList)
            {
                LoadReportSceleton(report); // Load report for each data item

                string reportType = "PDF";
                string deviceInfo = "<DeviceInfo>" +
                                    "<OutputFormat>" + reportType + "</OutputFormat>" +
                                    "</DeviceInfo>";

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension = string.Empty;

                byte[] bytes = rt.Render(reportType, deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                // Create a new MemoryStream and write the PDF content to it
                MemoryStream pdfStream = new MemoryStream(bytes);
                pdfStreams.Add(pdfStream); // Add the MemoryStream to the list
            }

            using (MemoryStream mergedStream = new MemoryStream())
            {
                // Merge the individual PDFs into a single MemoryStream
                using (Document document = new Document())
                {
                    PdfCopy copy = new PdfCopy(document, mergedStream);
                    document.Open();

                    foreach (MemoryStream pdfStream in pdfStreams)
                    {
                        pdfStream.Position = 0; // Reset the position of the MemoryStream
                        PdfReader reader = new PdfReader(pdfStream);
                        copy.AddDocument(reader);
                        reader.Close();
                    }

                    copy.Close();
                    document.Close();
                }
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "inline; filename=MergedPDF.pdf");

                // Write the merged PDF data to the response
                Response.BinaryWrite(mergedStream.ToArray());
                Response.End();
                
            }
        }




    }
}