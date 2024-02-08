using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_19_EXP
{
    public partial class PackingListDetails : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Packing List details";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string packplanref = (Request.QueryString["packplanref"]).ToString();
                DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "SHIPMENT_PLAN_REPORT_DETAILS", packplanref);
                if (ds == null)
                    return;
                //size start
                for (int i = 11; i < 26; i++)
                    this.gvPacking.Columns[i].Visible = false;

                int indexx = 1;
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.gvPacking.Columns[indexx + 10].Visible = true;
                    this.gvPacking.Columns[indexx + 10].HeaderText = ds.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
                    indexx++;
                }
                this.gvPacking.EditIndex = -1;

                //size end
                this.gvPacking.DataSource = ds;
                this.gvPacking.DataBind();

                this.lblRefNo.Text = "Reference No.: " + packplanref;

                Session["Report1"] = gvPacking;

                ((HyperLink)this.gvPacking.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl =
                    "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                ViewState["ExportToExcelFile"] = ds.Tables[0];
                ViewState["SizeDesc"] = ds.Tables[1];
            }
        }


        protected void PopulateTemplateExcelFile(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["ExportToExcelFile"];
            DataTable sizeDescTable = (DataTable)ViewState["SizeDesc"];
            if (dataTable.Rows.Count == 0)
            {
                return;
            }

            string templateFilePath = Server.MapPath("~/Excel_Files/Template_FB.xlsx"); // Path to the template Excel file
            string newFilePath = Server.MapPath("~/Excel_Files/DataTableExport.xlsx");  // Change the file name as needed

            File.Copy(templateFilePath, newFilePath, true);

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(newFilePath, true))
            {
                SheetData sheetData = spreadsheetDocument.WorkbookPart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();
                var rowsToDelete = sheetData.Elements<Row>().Skip(3).ToList();

                foreach (var row in rowsToDelete)
                {
                    row.Remove();
                }
                //Row row1 = new Row();
                //Cell cell = new Cell { DataType = CellValues.String, CellValue = new CellValue("Packing List details") };
                //row1.Append(cell);
                //sheetData.Append(row1);

                Row row2 = new Row();
                Cell cell = new Cell { DataType = CellValues.String, CellValue = new CellValue("Reference No.: " + Request.QueryString["packplanref"]), };
                
                cell.StyleIndex = (UInt32Value)1U;

                row2.Append(cell);

                sheetData.Append(row2);

                Row row3 = new Row();
                List<string> columnHeadersList = new List<string>
                {
                    "SL", "Crtn No.", "Art No.", "HS Code", "Forma", "Customer Order/PO","Customer Ref./Style", "Color", "Bar / EAN / Supplier Code", "Label Type/Department", "Order No", 
                    "Qty Pairs", "Total Ctns", "Total pairs", "Cartoon Measurement", "G.W./Cartoon", "Total G.W.", "Total N.W.", "N.W./Cartoon", "CBM",
                };
                int index = 11;
                foreach (DataRow row in sizeDescTable.Rows)
                {
                    columnHeadersList.Insert(index, row["SizeDesc"].ToString());
                    index++;
                }
                foreach (var item in columnHeadersList)
                {
                    cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(item) };

                    cell.StyleIndex = (UInt32Value)1U;

                    row3.Append(cell);
                }

                sheetData.Append(row3);

                List<List<string>> arrayList = new List<List<string>>();

                int k = 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string sl = k.ToString();
                    string crtnno = dataRow["crtnno"].ToString();
                    string styledesc = dataRow["styledesc"].ToString();
                    string hscode = dataRow["hscode"].ToString();
                    string lformadesc = dataRow["lformadesc"].ToString();
                    string custordno = dataRow["custordno"].ToString();
                    string custrefno = dataRow["custrefno"].ToString();
                    string colordesc = dataRow["colordesc"].ToString();
                    string barcodrefno = dataRow["barcodrefno"].ToString();
                    string lebltype = dataRow["lebltype"].ToString();
                    string orderno = dataRow["orderno"].ToString();
                    
                    string Qtypairs = decimal.TryParse(dataRow["Qtypairs"].ToString(), out decimal v20Decimal) && v20Decimal != 0 ? ((int)v20Decimal).ToString() : null;
                    string ttlcrtns = decimal.TryParse(dataRow["ttlcrtns"].ToString(), out decimal v21Decimal) && v21Decimal != 0 ? ((int)v21Decimal).ToString() : null;
                    string totlprs = decimal.TryParse(dataRow["totlprs"].ToString(), out decimal v22Decimal) && v22Decimal != 0 ? ((int)v22Decimal).ToString() : null;
                    string boxmeas = dataRow["boxmeas"].ToString();
                    string grswgtpercrtn = decimal.TryParse(dataRow["grswgtpercrtn"].ToString(), out decimal v24Decimal) && v24Decimal != 0 ? v24Decimal.ToString("0.00") : null;
                    string ttlgrswgt = decimal.TryParse(dataRow["ttlgrswgt"].ToString(), out decimal v25Decimal) && v25Decimal != 0 ? v25Decimal.ToString("0.00") : null;
                    string ttlnetwgt = decimal.TryParse(dataRow["ttlnetwgt"].ToString(), out decimal v26Decimal) && v26Decimal != 0 ? v26Decimal.ToString("0.00") : null;
                    string netwgtpercrtn = decimal.TryParse(dataRow["netwgtpercrtn"].ToString(), out decimal v27Decimal) && v27Decimal != 0 ? v27Decimal.ToString("0.00") : null;
                    string cbm = dataRow["cbm"].ToString();

                    List<string> list1 = new List<string> 
                    {
                        sl, crtnno, styledesc, hscode, lformadesc, custordno, custrefno, colordesc, barcodrefno, lebltype, orderno,
                        Qtypairs, ttlcrtns, totlprs, boxmeas, grswgtpercrtn, ttlgrswgt, ttlnetwgt, netwgtpercrtn, cbm
                    };

                    int i = 1;

                    foreach (DataRow row in sizeDescTable.Rows)
                    {
                        string sizeqty = decimal.TryParse(dataRow["s" + i].ToString(), out decimal x) && x != 0 ? ((int)x).ToString() : null;
                        list1.Insert(11+(i-1), sizeqty);
                        i++;
                    }

                    arrayList.Add(list1);
                    k++;
                }


                foreach (var arrayItem in arrayList)
                {
                    Row row4 = new Row();

                    foreach (string modifiedItem in arrayItem)
                    {
                        if (int.TryParse(modifiedItem, out int intValue))
                        {
                            cell = new Cell(new CellValue(intValue.ToString())) { DataType = CellValues.Number };
                        }

                        else if (double.TryParse(modifiedItem, out double doubleValue))
                        {
                            cell = new Cell(new CellValue(doubleValue.ToString())) { DataType = CellValues.Number };
                        }

                        else
                        {
                            cell = new Cell(new InlineString(new Text(modifiedItem))) { DataType = CellValues.InlineString };
                        }

                        cell.StyleIndex = (UInt32Value)1U;

                        row4.Append(cell);
                    }

                    sheetData.Append(row4);
                }

                spreadsheetDocument.WorkbookPart.WorksheetParts.First().Worksheet.Save();
            }

            string script = "window.open('" + ResolveUrl("~/Excel_Files/DataTableExport.xlsx") + "','_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "OpenWindow", script, true);
        }

        protected void ibtnSync_Click(object sender, EventArgs e)
        {

        }

        protected void ibtnRefSync_Click(object sender, EventArgs e)
        {

        }
    }
}