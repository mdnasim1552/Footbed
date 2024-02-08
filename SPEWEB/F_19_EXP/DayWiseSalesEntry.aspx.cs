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
using SPELIB;
using SPERDLC;
using System.Data.OleDb;
using System.IO;

namespace SPEWEB.F_19_EXP
{
    public partial class DayWiseSalesEntry : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text =
                    (type == "SalEntry") ? "Day Wise Sales Entry" : "Day Wise Sales Report";
                this.CommonButton();
                this.GetBuyer();
                this.selectview();

            }
            if (fileuploadExcel.HasFile)
            {

                ViewState.Remove("tblupdata");

                string connString = "";
                string StrFileName = string.Empty;
                if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                {
                    StrFileName =
                        fileuploadExcel.PostedFile.FileName.Substring(
                            fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = fileuploadExcel.PostedFile.ContentType;
                    int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {
                        //  this.lmsg.Text = "Uploading Fail";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' file Uploading failed');", true);
                        return;
                    }


                    else
                    {
                        string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                        string[] filePaths = Directory.GetFiles(savelocation);
                        foreach (string filePath in filePaths)
                            File.Delete(filePath);



                        fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                        //this.lmsg.Text = "Uploading Successfully";


                    }
                }

                string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                string path = apppath + "ExcelFile\\" + StrFileName;
                //string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);


                //Connection String to Excel Workbook
                if (strFileType.Trim() == ".xls")
                {
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path +
                                 ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\";";
                }
                else if (strFileType.Trim() == ".xlsx")
                {

                    connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                 ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                }

                //string query =
                //    "SELECT [inv_no],[inv_man],[bl_awb_no],[bl_awb_dt],[blawb_mat_dt], [ho_cr],[bank_rlzd_dt],[rlzd_amt],[c_f],[truk],[doc_ dt],[dhl_no],[dhl_dt],[cost_gsp],[cost_bl] FROM [Sheet1$]";
                string query = "SELECT *  FROM [Sheet1$]";

                OleDbConnection conn = new OleDbConnection(connString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //DataView dv = ds.Tables[0].DefaultView;
                //dv.RowFilter = ("LC_Number <> ''");
                //DataTable dt = dv.ToTable();
                //ViewState["tblupdata"] = dt;

                ViewState["tblupdata"] = ds.Tables[0];
                // this.DataInsert();
                da.Dispose();
                conn.Close();
                conn.Dispose();

            }

        }

        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            if (this.Request.QueryString["Type"].ToString() == "SalEntry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            }

            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();

        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblsales"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tblsales";
            //DataSet dsewew = ProData.GetTransInfoNew(comcod, "SP_ENTRY_EXPORT", "UPDATE_SALES_ADDITIONAL_INFO", ds, null, null);

            //return;
            bool result =
                ProData.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT", "UPDATE_SALES_ADDITIONAL_INFO", ds, null, null);
            if (result)
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            
        }

        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblsales"];
            for (int i = 0; i < this.gvSales.Rows.Count; i++)
            {

                string blawbno = ((TextBox)gvSales.Rows[i].FindControl("lgcBlawbno")).Text.ToString();
                string cf = Convert.ToDouble("0" + ((TextBox)gvSales.Rows[i].FindControl("Txtgvcf")).Text.Trim())
                    .ToString();
                string blawbdt = ((((TextBox)gvSales.Rows[i].FindControl("TxtBlawbdt")).Text).Length == 0)
                    ? "01-Jan-1900"
                    : Convert.ToDateTime(((TextBox)gvSales.Rows[i].FindControl("TxtBlawbdt")).Text)
                        .ToString("dd-MMM-yyyy"); // +"01-Jan-1900";
                string blawbrldt = ((((TextBox)gvSales.Rows[i].FindControl("TxtBlawbrldt")).Text).Length == 0)
                    ? "01-Jan-1900"
                    : Convert.ToDateTime(((TextBox)gvSales.Rows[i].FindControl("TxtBlawbrldt")).Text)
                        .ToString("dd-MMM-yyyy");
                string hocrdt = ((((TextBox)gvSales.Rows[i].FindControl("TxtHoCrdt")).Text).Length == 0)
                    ? "01-Jan-1900"
                    : Convert.ToDateTime(((TextBox)gvSales.Rows[i].FindControl("TxtHoCrdt")).Text).ToString("dd-MMM-yyyy");
                string rlzddt = ((((TextBox)gvSales.Rows[i].FindControl("Txtrizddt")).Text).Length == 0)
                    ? "01-Jan-1900"
                    : Convert.ToDateTime(((TextBox)gvSales.Rows[i].FindControl("Txtrizddt")).Text).ToString("dd-MMM-yyyy");
                string truk = Convert.ToDouble("0" + ((TextBox)gvSales.Rows[i].FindControl("Txtgvtruk")).Text.Trim())
                    .ToString();
                string docsubdt = ((((TextBox)gvSales.Rows[i].FindControl("txtDocSubDate")).Text).Length == 0)
                    ? "01-Jan-1900"
                    : Convert.ToDateTime(((TextBox)gvSales.Rows[i].FindControl("txtDocSubDate")).Text)
                        .ToString("dd-MMM-yyyy");
                string dhldt = ((((TextBox)gvSales.Rows[i].FindControl("txtDhlDate")).Text).Length == 0)
                    ? "01-Jan-1900"
                    : Convert.ToDateTime(((TextBox)gvSales.Rows[i].FindControl("txtDhlDate")).Text)
                        .ToString("dd-MMM-yyyy");
                string dhlno = ((TextBox)gvSales.Rows[i].FindControl("TxtgvDhlno")).Text.ToString();
                string gspcost = Convert.ToDouble("0" + ((TextBox)gvSales.Rows[i].FindControl("TxtgvCostGsp")).Text.Trim())
                    .ToString();
                string blcost = Convert.ToDouble("0" + ((TextBox)gvSales.Rows[i].FindControl("TxtgvBlCost")).Text.Trim())
                    .ToString();

                dt.Rows[i]["blawbno"] = blawbno;
                dt.Rows[i]["blawbdt"] = blawbdt;
                dt.Rows[i]["blawbrldt"] = blawbrldt;
                dt.Rows[i]["hocrdt"] = hocrdt;
                dt.Rows[i]["rlzddt"] = rlzddt;
                dt.Rows[i]["cf"] = cf;
                dt.Rows[i]["truk"] = truk;
                dt.Rows[i]["docsubdt"] = docsubdt;
                dt.Rows[i]["dhldt"] = dhldt;
                dt.Rows[i]["dhlno"] = dhlno;
                dt.Rows[i]["gspcost"] = gspcost;
                dt.Rows[i]["blcost"] = blcost;

            }

            ViewState["tblsales"] = dt;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void selectview()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "SalEntry":
                    this.Multivew.ActiveViewIndex = 0;
                    break;
                case "SalRep":
                    this.Multivew.ActiveViewIndex = 1;
                    break;
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "SalEntry":
                case "SalRep":
                    this.GetDayWiseInv();
                    break;
            }
        }

        private void GetDayWiseInv()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtFdate = this.txtfromdate.Text.ToString();
            string txttdate = this.txttodate.Text.ToString();
            string buyerid = ""; // this.ddlAccProject.SelectedValue.ToString();
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    buyerid += item.Value;
                }
            }
            string PType = (this.ChckStatus.Checked == true) ? "" : "InvDate";

            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_DAYWISE_INVOICE_DETAILS_INFO", txtFdate,
                txttdate, "%", buyerid, PType, "", "");
            if (ds1 == null)
            {
                return;
            }

            ViewState["tblsales"] = ds1.Tables[0];
            this.Data_Bind();
            this.selectview();
        }


        private void Data_Bind()
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString();
                DataTable dt = (DataTable)ViewState["tblsales"];
                switch (type)
                {
                    case "SalEntry":
                        this.gvSales.DataSource = dt;
                        this.gvSales.DataBind();
                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvSales;
                        //((HyperLink) this.gvSales.HeaderRow.FindControl("hlbtntbCdataExelE")).NavigateUrl =
                        //    "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        break;
                    case "SalRep":
                        this.gvSalesreport.DataSource = dt;
                        this.gvSalesreport.DataBind();
                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvSalesreport;
                        //((HyperLink) this.gvSalesreport.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl =
                        //    "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        break;

                }
            }
            catch (Exception ex)
            {

            }


        }

        //private void FooterCalculation() 
        //{
        //  var lst = (List<RMGiEntity.C_15_Pro.EclassProdPlanSummary>)ViewState["tblprodplan"];

        //    if (lst.Count == 0)
        //        return;

        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr01")).Text = ((lst.Select(p => p.qty2).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty2).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr02")).Text = ((lst.Select(p => p.qty2).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty2).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr03")).Text = ((lst.Select(p => p.qty3).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty3).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr04")).Text = ((lst.Select(p => p.qty4).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty4).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr05")).Text = ((lst.Select(p => p.qty5).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty5).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr06")).Text = ((lst.Select(p => p.qty6).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty6).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr07")).Text = ((lst.Select(p => p.qty7).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty7).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr08")).Text = ((lst.Select(p => p.qty8).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty8).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr09")).Text = ((lst.Select(p => p.qty9).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty9).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr10")).Text = ((lst.Select(p => p.qty10).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty10).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr11")).Text = ((lst.Select(p => p.qty11).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty11).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr12")).Text = ((lst.Select(p => p.qty12).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty12).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr13")).Text = ((lst.Select(p => p.qty13).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty13).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr14")).Text = ((lst.Select(p => p.qty14).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty14).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr15")).Text = ((lst.Select(p => p.qty15).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty15).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr16")).Text = ((lst.Select(p => p.qty16).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty16).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr17")).Text = ((lst.Select(p => p.qty17).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty17).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr18")).Text = ((lst.Select(p => p.qty18).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty18).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr19")).Text = ((lst.Select(p => p.qty19).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty19).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr20")).Text = ((lst.Select(p => p.qty20).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty20).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr21")).Text = ((lst.Select(p => p.qty21).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty21).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr22")).Text = ((lst.Select(p => p.qty22).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty22).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr23")).Text = ((lst.Select(p => p.qty23).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty23).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr24")).Text = ((lst.Select(p => p.qty24).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty24).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr25")).Text = ((lst.Select(p => p.qty25).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty25).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr26")).Text = ((lst.Select(p => p.qty26).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty26).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr27")).Text = ((lst.Select(p => p.qty27).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty27).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr28")).Text = ((lst.Select(p => p.qty28).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty28).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr29")).Text = ((lst.Select(p => p.qty29).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty29).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr30")).Text = ((lst.Select(p => p.qty30).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty30).Sum()).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr31")).Text = ((lst.Select(p => p.qty31).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty31).Sum()).ToString("#,##0;(#,##0); ");


        //}


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }


        protected void LbtnAdjust_OnClick(object sender, EventArgs e)
        {
            DataTable tempdt = (DataTable)ViewState["tblupdata"];
            if (tempdt == null || tempdt.Rows.Count == 0)
                return;
            DataTable dt = (DataTable)ViewState["tblsales"];
            foreach (DataRow dr in dt.Rows)
            {

                DataRow[] rows = tempdt.Select("inv_no ='" + dr["invno"] + "' AND inv_man ='" + dr["invrefno"] + "'");

                if (rows.Length > 0)
                {
                    dr["blawbno"] = rows[0]["bl_awb_no"];
                    dr["blawbdt"] = rows[0]["bl_awb_dt"];
                    dr["blawbrldt"] = rows[0]["blawb_mat_dt"];
                    dr["hocrdt"] = rows[0]["ho_cr _dt"];
                    dr["rlzddt"] = rows[0]["bank_rlzd_dt"];
                    dr["cf"] = Convert.ToDouble("0" + rows[0]["c_f"]);
                    dr["truk"] = Convert.ToDouble("0" + rows[0]["truk"]);
                    dr["docsubdt"] = rows[0]["doc_dt"];
                    dr["dhlno"] = rows[0]["dhl_no"];
                    dr["dhldt"] = (rows[0]["dhl_dt"].ToString().Length > 00) ? rows[0]["dhl_dt"].ToString() : "01-Jan-1900";
                    dr["gspcost"] = rows[0]["cost_gsp"];
                    dr["blcost"] = rows[0]["cost_bl"];



                }
            }

            ViewState["tblsales"] = dt;
            this.Data_Bind();
        }


    }
}