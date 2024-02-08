using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;

namespace SPEWEB.F_03_CostABgd
{
    public partial class ProdBudget : System.Web.UI.Page
    {
        ProcessAccess Budget = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //if (comcod == "6101")
                //{
                //    return;
                //}

                this.CommonButton();
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION BUDGET INFORMATION";

                this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtTDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //IMPORTANT
                ViewState["isBgtQty"] = false;

                //this.Targetno();
                //this.lblmsg.Visible = false;
            }

            //System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~") + "\\ExcelFile\\");

            //foreach (FileInfo file in di.GetFiles())
            //{
            //    file.Delete();
            //}
            //foreach (DirectoryInfo dir in di.GetDirectories())
            //{
            //    dir.Delete(true);
            //}
            //if (fileuploadExcel.HasFile)
            //{
            //    try
            //    {


            //        Session.Remove("XcelProdData");

            //        string connString = "";
            //        string StrFileName = string.Empty;
            //        if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
            //        {
            //            StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
            //            string StrFileType = fileuploadExcel.PostedFile.ContentType;
            //            int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
            //            if (IntFileSize <= 0)
            //            {

            //                return;
            //            }
            //            else
            //            {
            //                string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
            //                string[] filePaths = Directory.GetFiles(savelocation);
            //                foreach (string filePath in filePaths)
            //                    File.Delete(filePath);
            //                fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);

            //            }
            //        }

            //        string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
            //        string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();

            //        string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);


            //        if (strFileType.Trim() == ".xls")
            //        {
            //            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //        }
            //        else if (strFileType.Trim() == ".xlsx")
            //        {

            //            connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
            //        }
            //        //
            //        string query = "SELECT [Product_code],[Qty] FROM [Sheet1$]";
            //        OleDbConnection conn = new OleDbConnection(connString);
            //        if (conn.State == ConnectionState.Closed)
            //            conn.Open();
            //        OleDbCommand cmd = new OleDbCommand(query, conn);
            //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        da.Fill(ds);

            //        DataView dv = ds.Tables[0].DefaultView;
            //        dv.RowFilter = ("Product_code <> ''");
            //        DataTable dt = dv.ToTable();



            //        Session["XcelProdData"] = dt;// ds.Tables[0];
            //        da.Dispose();
            //        conn.Close();
            //        conn.Dispose();
            //        btnexcuplosd_Click(null, null);
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(LbtnSync_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;lnkbtnNew
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Text = "Sync";

        }

        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {
            //var listbudgt = (DataTable)ViewState["tblProduct"];
            //var listbudgt1 = listbudgt.DataTableToList<MFGOBJ.C_03_StdCost.ProBudget.ListBudgetData>();
            //var listexdata = (DataTable)Session["XcelProdData"];
            //var listexdata1 = listexdata.DataTableToList<MFGOBJ.C_03_StdCost.ProBudget.ListProductExData>();

            //foreach (var item in listbudgt1)
            //{

            //    try
            //    {

            //        item.bgdwqty = listexdata1.Find(X => X.Product_code == item.sirtdes).Qty;
            //    }
            //    catch (Exception)
            //    {

            //        item.bgdwqty = 0;
            //    }


            //}
            //var lstbg = new List<MFGOBJ.C_03_StdCost.ProBudget.ListBudget>();
            //foreach (var item in listbudgt1)
            //{
            //    if (item.bgdwqty == 0)
            //        continue;
            //    lstbg.Add(new MFGOBJ.C_03_StdCost.ProBudget.ListBudget() { prodcode = item.prodcode, proddesc = item.proddesc, proddesc1=item.proddesc1, produnit = item.produnit, bgdwqty = item.bgdwqty, stqty = item.stqty, targetqty = item.targetqty, nproqty = item.nproqty });
            //}
            //DataTable tbl1 = ASITUtility03.ListToDataTable(lstbg);

            //ViewState["tblbbudget"] = tbl1;
            //this.Data_Bind();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        //private void Targetno()
        //{
        //    if (this.lbtnOk.Text == "New")
        //        return;
        //    string comcod = this.GetCompCode();
        //    string txtserch = this.txtProTarget.Text + "%";
        //    string CurDate1 = Convert.ToDateTime(this.txtbgddate.Text.Trim()).ToString("dd-MMM-yyyy");
        //    DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "PBMGETPREVMSTLIST", CurDate1,
        //                  txtserch, "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    this.ddlProTarget.Items.Clear();
        //    this.ddlProTarget.DataTextField = "mstno1";
        //    this.ddlProTarget.DataValueField = "mstno";
        //    this.ddlProTarget.DataSource = ds1.Tables[0];
        //    this.ddlProTarget.DataBind();
        //}

        private void GetProdBudgetNo()
        {
            string comcod = this.GetCompCode();
            string Date = this.txtbgddate.Text.Trim();
            DataSet ds2 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETPROBUDGETNO", Date, "", "", "", "", "", "", "", "");
            string Bpno = ds2.Tables[0].Rows[0]["bpno"].ToString();
            this.lblBpn.Text = Bpno;
        }
        private void PreReqList()
        {
            ViewState.Remove("tblproinfo");
            string comcod = this.GetCompCode();
            string Type = (this.Request.QueryString["Type"] == "Entry") ? "41%" : "04%";

            DataSet ds2 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETPREPBMNUMBER", Type, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                this.ddlPreProlist.DataSource = ds2.Tables[0];
                this.ddlPreProlist.DataTextField = "pbmno1";
                this.ddlPreProlist.DataValueField = "pbmno";
                this.ddlPreProlist.DataBind();
                ViewState["tblproinfo"] = ds2.Tables[0];
                ds2.Dispose();
                //this.lblTarget.Text = ds2.Tables[0].Rows[0]["mstno"].ToString();

            }
        }
        protected void lnkPBPN_Click(object sender, EventArgs e)
        {

        }

        private void GetMatGroup()
        {
            Session.Remove("tblresleb2");

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.Budget.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_RESCODE_LEVEL2_ISSUE", "04%", "", userid, "", "", "", "", "", "");
            Session["tblresleb2"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void SelectMatList()
        {
            DataTable dt = ((DataTable)Session["tblresleb2"]).Copy();

            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "sircode";
            this.ddlcatagory.DataSource = dt;
            this.ddlcatagory.DataBind();
        }

        
        private void GetProduct()
        {
            string comcod = this.GetCompCode();
            string CompGroup = this.ddlcatagory.SelectedValue.ToString() == "0000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            string filter = CompGroup + "%";
            string date = Convert.ToDateTime(this.txtbgddate.Text).ToString("dd-MMM-yyyy");
            string spName = (this.Request.QueryString["Type"] == "Entry") ? "SP_ENTRY_BATCH_BUDGET" : "SP_ENTRY_BATCH_BUDGET_02";
            string callType = (this.Request.QueryString["Type"] == "Entry") ? "GETMAINPROCODEREQ" : "GETSEMIPRO";
            DataSet ds3 = Budget.GetTransInfo(comcod, spName, callType, filter, date, "", "", "", "", "", "", "");
            this.ddlProduct.DataSource = ds3.Tables[0];
            this.ddlProduct.DataTextField = "proddesc";
            this.ddlProduct.DataValueField = "prodcode";
            this.ddlProduct.DataBind();
            ViewState["tblProduct"] = ds3.Tables[0];
            ViewState["tblProductBackUp"] = ds3.Tables[0];
            ds3.Dispose();

        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            //ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            //ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");

            if (ds1 == null)
                return;

            ddlSeason.DataTextField = "gdesc";
            ddlSeason.DataValueField = "gcod";
            ddlSeason.DataSource = ds1.Tables[0];
            ddlSeason.DataBind();
            ddlSeason.Items.Add(new ListItem { Value = "00000", Text = "---All---" });
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.ddlSeason.SelectedValue = season;
            }
            else
            {
                this.ddlSeason.SelectedValue = "00000";
            }
        }


        private void GetLC_Order()
        {
            string comcod = GetCompCode();

            string findseason = (this.ddlSeason.SelectedValue.ToString() == "00000") ? "%" : this.ddlSeason.SelectedValue.ToString() + "%";
            string srch = "1601%";

            //if (this.Request.QueryString["actcode"].ToString().Length > 0)
            //{
            //    srch = this.Request.QueryString["actcode"].ToString() + "%";
            //}

            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", srch, "%", findseason, "", "", "", "", "", "");

            if (ds1 == null)
                return;

            DataView dv = ds1.Tables[0].DefaultView;
            if (Request.QueryString.AllKeys.Contains("dayid"))
            {
                if (this.Request.QueryString["dayid"].ToString().Length > 0)
                {
                    dv.RowFilter = "dayid='" + this.Request.QueryString["dayid"].ToString() + "'";
                }
            }
            this.ddlBatch.DataTextField = "styledesc2";
            this.ddlBatch.DataValueField = "stylecode2";
            this.ddlBatch.DataSource = dv.ToTable();
            this.ddlBatch.DataBind();

            //if (this.Request.QueryString["actcode"].ToString().Length > 0)
            //{
            //    this.ddlBatch.SelectedValue = this.Request.QueryString["actcode"].ToString();
            //    this.ddlBatch_SelectedIndexChanged(null, null);
            //    this.lbtnOk_Click(null, null);
            //}

            //this.ddlBatch_SelectedIndexChanged(null, null);

        }

        protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLC_Order();
        }

        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProduct();
        }

        private void GetAllProduct()
        {

            string batchno = this.lblBpn.Text.Substring(0, 14);
            string comcod = this.GetCompCode();
            string filter = "%";
            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETPROCODE", filter, batchno, "", "", "", "", "", "", "");
            this.ddlAllProduct.DataSource = ds3.Tables[0];
            this.ddlAllProduct.DataTextField = "proddesc";
            this.ddlAllProduct.DataValueField = "prodcode";
            this.ddlAllProduct.DataBind();
            ViewState["tblProduct"] = ds3.Tables[0];
            ds3.Dispose();
            this.Data_Bind();


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            if (ASTUtility.Left(this.GetCompCode(), 2) == "61")
            {
                return;
            }
            int View = this.rbtnlist.SelectedIndex;
            switch (View)
            {
                case 0:
                    this.PrintProdBudgetedReport();
                    break;
                case 2:
                    this.PrintReport();
                    break;
            }

        }
        private void PrintReport()
        {

            if (this.ddlAllProduct.SelectedValue.ToString() == "000000000000")
                this.PrintBudgetedReport();
            else
                this.PrintBudgetedReportIndev();


        }
        protected void PrintBudgetedReport()
        {
            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                //DataTable dt = (DataTable)ViewState["tblbbudget"];

                //ReportDocument rptinfo = new MFGRPT.R_03_StdCost.rptBdgReport();

                //TextObject rpttxtlblBpn = rptinfo.ReportDefinition.ReportObjects["txtProdno"] as TextObject;
                //rpttxtlblBpn.Text = "PBM No: " + this.lblBpn.Text.Trim().ToString();
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rptinfo.SetDataSource(dt);
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }
        protected void PrintProdBudgetedReport()
        {
            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                //DataTable dt = (DataTable)ViewState["tblbbudget"];

                //ReportDocument rptinfo = new MFGRPT.R_03_StdCost.rptMonProdBgd();

                //TextObject rpttxtCompName = rptinfo.ReportDefinition.ReportObjects["CompName"] as TextObject;
                //rpttxtCompName.Text = comnam;
                //TextObject rpttxtdate = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //rpttxtdate.Text = "Date: " + this.txtbgddate.Text;

                //TextObject rpttxtMst = rptinfo.ReportDefinition.ReportObjects["txtMst"] as TextObject;
                //rpttxtMst.Text = this.ddlPreProlist.SelectedItem.Text.Substring(23).ToString();

                //TextObject rpttxtlblBpn = rptinfo.ReportDefinition.ReportObjects["txtProdno"] as TextObject;
                //rpttxtlblBpn.Text = "PBM No: " + this.lblBpn.Text.Trim().ToString();
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rptinfo.SetDataSource(dt);
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

            }

        }
        protected void PrintBudgetedReportIndev()
        {
            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                //DataTable dt = (DataTable)ViewState["tblbbudget"];

                //ReportDocument rptinfo = new MFGRPT.R_03_StdCost.rptBdgReportIndiv();
                //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtProcess"] as TextObject;
                //rpttxtVoutype.Text = this.ddlPreProlist.SelectedItem.Text.Substring(23).ToString();
                //TextObject rpttxtlblBpn = rptinfo.ReportDefinition.ReportObjects["txtProdno"] as TextObject;
                //rpttxtlblBpn.Text = "PBM No:  " + this.lblBpn.Text.Trim().ToString();
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //TextObject txtProductName = rptinfo.ReportDefinition.ReportObjects["txtProductName"] as TextObject;
                //txtProductName.Text = "Product: " + this.ddlAllProduct.SelectedItem.Text.Substring(14).ToString();
                //rptinfo.SetDataSource(dt);
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (ASTUtility.Left(this.GetCompCode(), 2) == "61")
            {
                return;
            }
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //return;

                this.rbtnlist.Visible = true;
                //this.lblPrePBNo.Visible = false;
                //this.txtPre.Visible = false;
                this.imgbtnPreSearch.Visible = false;
                this.ddlPreProlist.Visible = false;
                this.GetProdBudgetNo();
                if (ddlPreProlist.Items.Count > 0)
                {
                    string pbmno = this.ddlPreProlist.SelectedValue.ToString();
                    this.txtbgddate.Text = Convert.ToDateTime((((DataTable)ViewState["tblproinfo"]).Select("pbmno='" + pbmno + "'"))[0]["bgddat"]).ToString("dd-MMM-yyyy");
                    this.txtFDate.Text = Convert.ToDateTime((((DataTable)ViewState["tblproinfo"]).Select("pbmno='" + pbmno + "'"))[0]["sdate"]).ToString("dd-MMM-yyyy");
                    this.txtTDate.Text = Convert.ToDateTime((((DataTable)ViewState["tblproinfo"]).Select("pbmno='" + pbmno + "'"))[0]["edate"]).ToString("dd-MMM-yyyy");
                    //this.ddlBatch.SelectedValue = (((DataTable)Session["tblproinfo"]).Select("pbmno='" + pbmno + "'"))[0]["bactcode"].ToString();

                    //this.lblBatch.Text = this.ddlBatch.SelectedItem.Text.Trim();
                    // this.lblProTarget.Text = (((DataTable)Session["tblproinfo"]).Select("pbmno='" + pbmno + "'"))[0]["mstno"].ToString();
                    this.lblBpn.Text = (this.ddlPreProlist.SelectedValue.ToString()).Substring(0, 14);
                    this.txtbgddate.Enabled = false;
                    this.txtFDate.Enabled = false;
                    this.txtTDate.Enabled = false;

                    //this.ddlProTarget.Visible = false;
                    //this.lblProTarget.Visible = true;
                }
                else
                    this.txtbgddate.Enabled = true;

                this.txtFDate.Enabled = true;
                this.txtTDate.Enabled = true;
                this.GetMatGroup();
                this.SelectMatList();
                this.chkboxSync.Visible = true;

                return;
            }

            this.lbtnOk.Text = "Ok";
            //this.ddlBatch.Visible = true;
            //this.lblBatch.Visible = false;
            //this.ddlProTarget.Visible = true;
            //this.lblProTarget.Visible = false;
            this.rbtnlist.Visible = false;
            this.rbtnlist.SelectedIndex = -1;
            this.MultiView1.ActiveViewIndex = -1;
            this.gvBudget.DataSource = null;
            this.gvBudget.DataBind();
            this.gvBudgetRate.DataSource = null;
            this.gvBudgetRate.DataBind();
            this.gvBudgetRpt.DataSource = null;
            this.gvBudgetRpt.DataBind();

            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.ddlPreProlist.Items.Clear();
            //this.lblPrePBNo.Visible = true;
            //this.txtPre.Visible = true;
            this.imgbtnPreSearch.Visible = true;
            this.ddlPreProlist.Visible = true;
            this.txtbgddate.Enabled = true;
            this.txtFDate.Enabled = true;
            this.txtTDate.Enabled = true;
            ddlSeason.Enabled = true;
            ddlBatch.Enabled = true;

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void rbtnlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int View = this.rbtnlist.SelectedIndex;
            switch (View)
            {
                case 0:
                    this.GetProduct();
                    this.ShowProduct();
                    this.MultiView1.ActiveViewIndex = View;
                    break;

                case 1:
                    this.ShowBgdRate();
                    this.MultiView1.ActiveViewIndex = View;
                    break;

                default:
                    //this.GetAllProduct();
                    this.ShowBudgetedIncome();
                    this.MultiView1.ActiveViewIndex = View;
                    break;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }

        private void ShowProduct()
        {
            ViewState.Remove("tblbbudget");
            string comcod = this.GetCompCode();
            string batchno = this.lblBpn.Text.Substring(0, 14);
            string date = Convert.ToDateTime(this.txtbgddate.Text).ToString("dd-MMM-yyyy");
            //string protarget = this.lblTarget.Text.Substring(0, 14);
            //string date = Convert.ToDateTime(this.lblTarget.Text.Substring(15)).ToString("dd-MMM-yyyy"); //Convert.ToDateTime(this.txtbgddate.Text).ToString("dd-MMM-yyyy");
            string spName = (this.Request.QueryString["Type"] == "Entry") ? "SP_ENTRY_BATCH_BUDGET" : "SP_ENTRY_BATCH_BUDGET_02";
            string callType = (this.Request.QueryString["Type"] == "Entry") ? "SWOWPRODUCT" : "SHOWSEMIPRO";

            DataSet ds3 = Budget.GetTransInfo(comcod, spName, callType, batchno, date, "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvBudget.DataSource = null;
                this.gvBudget.DataBind();
                return;
            }
            ViewState["tblbbudget"] = ds3.Tables[0];
            ViewState["tblbbudgetlog"] = ds3.Tables[1];

            if (ds3.Tables[0].Rows.Count == 0)
            {
                this.lbtnSelectAll.Visible = true;
            }
            else
            {
                this.lbtnSelectAll.Visible = false;
            }

            ds3.Dispose();
            this.Data_Bind();
        }

        private void ShowBgdRate()
        {
            ViewState.Remove("tblbbudget");
            string comcod = this.GetCompCode();
            string batchno = this.lblBpn.Text.Substring(0, 14);
            string date = Convert.ToDateTime(this.txtbgddate.Text).ToString("dd-MMM-yyyy");
            string Code = (this.Request.QueryString["Type"] == "Entry") ? "41%" : "04%";
            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "SWOWBUDGETEDRES", batchno, Code, date, "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvBudgetRate.DataSource = null;
                this.gvBudgetRate.DataBind();
                return;
            }

            ViewState["tblbbudget"] = HiddenSameData01(ds3.Tables[0]);
            //Session["tblbbudget"] = ds3.Tables[0];
            ds3.Dispose();
            this.Data_Bind();

        }

        private void ShowBudgetedIncome()
        {
            ViewState.Remove("tblbbudget");
            string comcod = this.GetCompCode();

            string batchno = this.lblBpn.Text.Substring(0,14) ; //this.txtBpn.Text.Trim();

            string procode = this.ddlAllProduct.SelectedValue.ToString() == "000000000000000000000000" ? "%" : this.ddlAllProduct.SelectedValue.ToString() + "%";
            string Code = (this.Request.QueryString["Type"] == "Entry") ? "41%" : "04%";
            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "RPTBATCHBUDGET", batchno, procode, Code, "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvBudgetRpt.DataSource = null;
                this.gvBudgetRpt.DataBind();
                return;
            }
            ViewState["tblbbudget"] = HiddenSameData(ds3.Tables[0]);
            ds3.Dispose();
            this.Data_Bind();
        }
        private DataTable HiddenSameData01(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string odcode = dt1.Rows[0]["odcode"].ToString();
            // string procode = dt1.Rows[0]["procode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["odcode"].ToString() == odcode)
                {
                    odcode = dt1.Rows[j]["odcode"].ToString();
                    dt1.Rows[j]["oddesc"] = "";
                }
                else
                {
                    odcode = dt1.Rows[j]["odcode"].ToString();
                }

            }
            return dt1;

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string procode = dt1.Rows[0]["procode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["procode"].ToString() == procode)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["prodesc"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["procode"].ToString() == procode)
                    {
                        dt1.Rows[j]["prodesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    grp = dt1.Rows[j]["grp"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();

                }

            }
            return dt1;

        }

        protected void imgbtnsrchProduct_Click(object sender, EventArgs e)
        {
            this.GetProduct();
        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.SaveValue();
            string ProdCode = this.ddlProduct.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblbbudget"];
            DataRow[] dr = dt.Select(" prodcode='" + ProdCode + "'");
            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["prodcode"] = ProdCode;//
                dr1["scode"] = (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["sirtdes"];
                dr1["proddesc"] = this.ddlProduct.SelectedItem.Text.Trim();
                dr1["prodcode1"] = (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["prodcode1"];
                dr1["proddesc1"] = (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["proddesc1"];
                dr1["produnit"] = (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["produnit"];
                dr1["targetqty"] = (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["targetqty"];
                dr1["stqty"] = (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["stqty"];
                dr1["nproqty"] = (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["nproqty"];
                dr1["bgdwqty"] = (comcod == "8305") ? (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["stdqty"] : 0.00;
                
                dr1["bgdwqty"] = (bool)ViewState["isBgtQty"] ? (((DataTable)ViewState["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["stqty"] : dr1["bgdwqty"];
                
                dt.Rows.Add(dr1);

            }
            ViewState["tblbbudget"] = dt;

            this.Data_Bind();
        }


        private void SaveValue()
        {
            int View = this.rbtnlist.SelectedIndex;
            DataTable dt = (DataTable)ViewState["tblbbudget"];
            int TblRowIndex, i;

            switch (View)
            {
                case 0:
                    for (i = 0; i < this.gvBudget.Rows.Count; i++)
                    {
                        double bgdqty = Convert.ToDouble("0" + ((TextBox)this.gvBudget.Rows[i].FindControl("txtgvbgdQty")).Text.Trim());
                        TblRowIndex = (gvBudget.PageIndex) * gvBudget.PageSize + i;
                        dt.Rows[TblRowIndex]["bgdwqty"] = bgdqty;
                    }
                    break;

                case 1:
                    for (i = 0; i < this.gvBudgetRate.Rows.Count; i++)
                    {
                        double bgdqty = Convert.ToDouble("0" + ((Label)this.gvBudgetRate.Rows[i].FindControl("lblgvRateBgdQty")).Text.Trim());
                        double bgdrate = Convert.ToDouble("0" + ((TextBox)this.gvBudgetRate.Rows[i].FindControl("txtgvbgdRate")).Text.Trim());
                        TblRowIndex = (gvBudgetRate.PageIndex) * gvBudgetRate.PageSize + i;
                        dt.Rows[TblRowIndex]["bgdrat"] = bgdrate;
                        dt.Rows[TblRowIndex]["bgdam"] = (bgdqty * bgdrate);
                    }
                    break;
            }

            ViewState["tblbbudget"] = dt;
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblbbudget"];
            int View = this.rbtnlist.SelectedIndex;
            switch (View)
            {
                case 0:
                    this.gvBudget.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBudget.DataSource = dt;
                    this.gvBudget.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.gvBudget.FooterRow.FindControl("lblgvFbgdQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdwqty)", "")) ?
                         0 : dt.Compute("sum(bgdwqty)", ""))).ToString("#,##0;(#,##0); ");

                    //if (dt.Rows.Count == 0)
                    //    return;
                    //Session["Report1"] = gvBudget;
                    //((HyperLink)this.gvBudget.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


                    break;

                case 1:
                    this.gvBudgetRate.DataSource = dt;
                    this.gvBudgetRate.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    Session["Report1"] = gvBudgetRate;
                    ((HyperLink)this.gvBudgetRate.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;

                default:
                    //DataTable dt1 = (DataTable)ViewState["tblProduct"];
                    this.gvBudgetRpt.DataSource = dt;
                    this.gvBudgetRpt.DataBind();
                    break;

            }


        }
        //protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.SaveValue();
        //    this.gvBudget.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}
        protected void gvBudget_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblbbudget"];
            string pbnno = this.lblBpn.Text.Substring(0, 14); //this.txtBpn.Text.Trim();
                                                                                         //string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string rescode = ((Label)this.gvBudget.Rows[e.RowIndex].FindControl("lblgvProdCode")).Text.Trim();
            bool result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "DELETEPRO",
                        pbnno, rescode, "", "");


            if (result)
            {

                int rowindex = (this.gvBudget.PageSize) * (this.gvBudget.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("prodcode<>''");
                ViewState["tblbbudget"] = dv.ToTable();
                this.Data_Bind();
            }
            else
            {
                //this.lmsg.Text = "Delete Data Unsuccessfully";
            }
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {

            this.SaveValue();

            string comcod = this.GetCompCode();
            DataTable tbl2 = (DataTable)ViewState["tblbbudget"];
            //string BatchCode = this.ddlBatch.SelectedValue.ToString();
            string pbnno = this.lblBpn.Text;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dtuser = (DataTable)ViewState["tblbbudgetlog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
            string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (dtuser.Rows.Count == 0) ? "" : userid;// (this.Request.QueryString["type"] == "Entry") ? tblEditByid : (tblEditByid == "") ? userid : tblEditByid;
            string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");// (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : (tblEditDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblEditDat;
            string sDate = this.txtFDate.Text;
            string tDate = this.txtTDate.Text;

            DataTable dtt = (DataTable)ViewState["tblProduct"];

            string mlccod = "";
            string dayid = "";

            if (chkboxSync.Checked == true)
            {
                 mlccod = ddlBatch.SelectedValue.Substring(0, 12);
                 dayid = ddlBatch.SelectedValue.Substring(36, 8);
            }

            double tBgdQty = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(bgdwqty)", "")) ? 0 : tbl2.Compute("sum(bgdwqty)", "")));

            if (tBgdQty == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input Qty');", true);


                return;
            }
            //-------------
            string date = Convert.ToDateTime(this.txtbgddate.Text.Trim()).ToString("dd-MMM-yyyy");
            bool result = false;
            result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEBGDWRKLOG", pbnno, date, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, sDate, tDate, "", "", "", "", "", mlccod, dayid);


            // bool result=false;
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {

                string ProdCode = tbl2.Rows[i]["prodcode"].ToString();
                double Bgdqty = Convert.ToDouble(tbl2.Rows[i]["bgdwqty"].ToString().Trim());
                if (Bgdqty > 0)
                {
                    result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEBUDGET", "000000000000", pbnno, ProdCode, Bgdqty.ToString(), date, "", "", "", "", "", "", "", "", "", "");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Budget.ErrorObject["Msg"].ToString() + "');", true);

                        return;
                    }
                }
            }
            //string mstno = this.lblProTarget.Text.Substring(0, 14).Trim().ToString();
            //result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "UPDATEMSALTARGET", pbnno, mstno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


        }
        //protected void gvBudgetRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.SaveValue();
        //    this.gvBudgetRate.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}
        protected void lbtnTotalRate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void lbtnFinalRateUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();

            string comcod = this.GetCompCode();
            DataTable tbl2 = (DataTable)ViewState["tblbbudget"];
            //string BatchCode = this.ddlBatch.SelectedValue.ToString();
            ///-------------------------
            string Batchno = this.lblBpn.Text.Substring(0, 14); //this.txtBpn.Text.Trim();
                                                                                           //if (this.ddlProcess.SelectedValue.Length > 1)
                                                                                           //{
                                                                                           //    BatchCode = this.ddlProcess.SelectedValue.ToString();
                                                                                           //}
            bool result = false;
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {

                string Rescode = tbl2.Rows[i]["rsircode"].ToString();
                string spcfcode = tbl2.Rows[i]["spcfcod"].ToString();
                string BgdRate = Convert.ToDouble(tbl2.Rows[i]["bgdrat"].ToString().Trim()).ToString();
                string bgdqty = Convert.ToDouble(tbl2.Rows[i]["bgdqty"].ToString().Trim()).ToString();

                result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEBGDRESRAT", spcfcode, Batchno, Rescode, BgdRate, bgdqty, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Budget.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }



            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


        }
        protected void gvBudgetRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lblgvResDescRpt");
                Label amt = (Label)e.Row.FindControl("lblgvBgdamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    groupdesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");
                }
            }
        }


        protected void imgbtnsrchAllProduct_Click(object sender, EventArgs e)
        {
            this.GetAllProduct();
        }
        protected void lbtnSelect1_Click(object sender, EventArgs e)
        {
            this.ShowBudgetedIncome();
        }


        protected void imgbtnPreSearch_Click(object sender, EventArgs e)
        {
            if (ASTUtility.Left(this.GetCompCode(), 2) == "61")
            {
                return;
            }
            this.PreReqList();
        }

        //protected void imgBtnTarget_Click(object sender, EventArgs e)
        //{
        //    this.Targetno();
        //}
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string ProdCode = this.ddlProduct.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)ViewState["tblbbudget"];
            DataTable tbl2 = (DataTable)ViewState["tblProduct"];
            DataRow[] dr = tbl1.Select("prodcode='" + ProdCode + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["scode"] = tbl2.Rows[i]["sirtdes"].ToString();
                    dr1["prodcode1"] = tbl2.Rows[i]["prodcode1"].ToString();
                    dr1["proddesc1"] = tbl2.Rows[i]["proddesc1"].ToString();
                    dr1["prodcode"] = tbl2.Rows[i]["prodcode"].ToString();
                    dr1["proddesc"] = tbl2.Rows[i]["proddesc"].ToString();
                    dr1["produnit"] = tbl2.Rows[i]["produnit"].ToString();
                    dr1["targetqty"] = Convert.ToDouble(tbl2.Rows[i]["targetqty"]).ToString();
                    dr1["stqty"] = Convert.ToDouble(tbl2.Rows[i]["stqty"]).ToString();
                    dr1["nproqty"] = Convert.ToDouble(tbl2.Rows[i]["nproqty"]).ToString();
                    dr1["bgdwqty"] = (bool)ViewState["isBgtQty"] ? tbl2.Rows[i]["stqty"] : 0.00;
                    tbl1.Rows.Add(dr1);
                }
                ViewState["tblbbudget"] = tbl1;
            }
            this.Data_Bind();
        }

        //protected void ddlPreProlist_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string pbmno = this.ddlPreProlist.SelectedValue.ToString();
        //    this.lblTarget.Text = (((DataTable)Session["tblproinfo"]).Select("pbmno='" + pbmno + "'"))[0]["mstno"].ToString();
        //}
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void gvBudgetRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvstockqty = (Label)e.Row.FindControl("lblgvstockqty");

                double stockqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "stockqty"));

                if (stockqty < 1)
                {
                    lblgvstockqty.Style.Add("color", "red");


                }

            }
        }

        protected void chkboxSync_CheckedChanged(object sender, EventArgs e)
        {
            if (chkboxSync.Checked == true)
            {
                this.LblSeason.Visible = true;
                this.ddlSeason.Visible = true;
                this.lblMasterLc.Visible = true;
                this.ddlBatch.Visible = true;
                this.cellSyncBtn.Visible = true;

                this.GetSeason();
                this.GetLC_Order();
            }
            else
            {
                this.LblSeason.Visible = false;
                this.ddlSeason.Visible = false;
                this.lblMasterLc.Visible = false;
                this.ddlBatch.Visible = false;
                this.cellSyncBtn.Visible = false;
            }
        }

        //protected void LbtnSync_Click(object sender, EventArgs e)
        //{
        //    if (chkboxSync.Checked == true) 
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        return;
        //    }

        //}

        protected void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                string masterLvNo = ddlBatch.SelectedValue;
                string comcod = this.GetCompCode();

                DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_03", "GET_MATERIAL_BY_SYNC", masterLvNo);

                if(ds3.Tables[0].Rows.Count > 0 && ds3 != null)
                {
                    this.ddlProduct.DataSource = ds3.Tables[0];
                    this.ddlProduct.DataTextField = "proddesc";
                    this.ddlProduct.DataValueField = "prodcode";
                    this.ddlProduct.DataBind();
                    ViewState["tblProduct"] = ds3.Tables[0];
                    ViewState["isBgtQty"] = true;
                    ds3.Dispose();
                    ddlSeason.Enabled = false;
                    ddlBatch.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No sync material found!');", true);
                    DataTable dt1 = (DataTable)ViewState["tblProductBackUp"];
                    this.ddlProduct.DataSource = dt1;
                    this.ddlProduct.DataTextField = "proddesc";
                    this.ddlProduct.DataValueField = "prodcode";
                    this.ddlProduct.DataBind();
                    ViewState["tblProduct"] = dt1;
                    ViewState["isBgtQty"] = false;
                    ds3.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}