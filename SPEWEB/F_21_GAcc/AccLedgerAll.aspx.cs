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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccLedgerAll : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");


                ((Label)this.Master.FindControl("lblTitle")).Text = "General Ledger";
                this.rbtnLedger.SelectedIndex = 0;
                this.rbtnLedger_SelectedIndexChanged(null, null);
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        protected void rbtnLedger_SelectedIndexChanged(object sender, EventArgs e)
        {

            string type = this.rbtnLedger.SelectedValue.ToString();
            switch (type)
            {

                case "Ledger":
                    if (this.txtDateFrom.Text.Trim().Length == 0)
                    {
                        double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                        this.txtDateFrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                        this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                    }
                    this.rbtnLedger.SelectedIndex = 0;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.rbtnList1.SelectedIndex = 0;
                    this.Panel1.Visible = false;
                    this.Panel2.Visible = false;
                    this.IbtnSearchAcc_Click(null, null);
                    break;
                case "SubLedger":
                    if (this.txtDateFrom.Text.Trim().Length == 0)
                    {
                        double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                        this.txtDateFrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                        this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                    }
                    this.ibtnFindRes_Click(null, null);
                    this.Panel1.Visible = true;
                    this.Panel2.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.rbtnList1.SelectedIndex = 0;
                    this.ddlConAccResHead.Items.Clear();
                    this.dgv2.DataSource = null;
                    this.dgv2.DataBind();
                    break;

                case "DetailLedger":
                    this.txtDateFromSp.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txtDatetoSp.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.ibtnFindResSP_Click(null, null);
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ddlConAccResHead.Items.Clear();
                    this.gvSpledger.DataSource = null;
                    this.gvSpledger.DataBind();
                    break;


            }

        }
        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string filter =  "%";
                DataSet ds1 = new DataSet();
                if (rbtnLedger.SelectedValue.ToString() == "SubLedger")
                {

                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEADWITHRES", filter, "", "", "", "", "", "", "", "");
                }

                else
                {
                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD01", filter, "", "", "", "", "", "", "", "");

                }
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string comcod = hst["comcod"].ToString();
            string filter =  "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCRESHEAD", actcode, filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //DataTable dt1 = ds1.Tables[0];
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataBind();


        }
        protected void ibtnFindResSP_Click(object sender, EventArgs e)
        {
            this.GetResList();
        }
        private void GetResList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter =  "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSPLGACCRESLIST", "%", filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlRescode.DataTextField = "resdesc1";
            this.ddlRescode.DataValueField = "rescode";
            this.ddlRescode.DataSource = ds1.Tables[0];
            this.ddlRescode.DataBind();


        }

        protected void lnkShowLedger_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("StoreTable");
            DataSet ds2 = this.GetDataForReport();
            DataTable dt = ds2.Tables[0];
            if (dt.Rows.Count == 0)
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                return;
            }
            Session["StoreTable"] = dt;
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Ledger";
                string eventdesc = "Show Ledger";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
            //string grp=
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {



                if ((dt.Rows[i]["vounum"]).ToString().Trim() == "TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "BALANCE")
                    continue;
                if ((dt.Rows[i]["grp"]).ToString().Trim() == "C")
                    break;

                if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                {
                    dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                    cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                    balamt = balamt + (dramt - cramt);
                    dt.Rows[i]["balamt"] = balamt;
                }
            }
            return dt;


        }

        private void HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return;
            string grp = dt1.Rows[0]["grp"].ToString();
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                {

                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    //dt1.Rows[j]["refnum"] = "";
                }

                if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";

                }
                if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                }

                grp = dt1.Rows[j]["grp"].ToString();
                vounum = dt1.Rows[j]["vounum1"].ToString();
            }

            this.dgv2.DataSource = dt1;
            this.dgv2.DataBind();
            //this.dgv2.Columns[0].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") || (this.chkqty.Checked);
            this.dgv2.Columns[6].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") && (this.chkqty.Checked);
            this.dgv2.Columns[7].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") && (this.chkqty.Checked);
            Session["Report1"] = dgv2;
            ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl =
                "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }
        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string date1 = this.txtDateFrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string Narration = (this.rbtnList1.SelectedIndex == 0) ? "" : "WithoutNar";
            DataSet ds1 = new DataSet();

            if (rbtnLedger.SelectedValue.ToString() == "SubLedger")
            {
                string rescode = this.ddlConAccResHead.SelectedValue.ToString();
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", actcode, date1, date2, rescode, Narration, "", "", "", "");

            }

            else
            {
                //string calltype = (Request.QueryString["RType"].ToString() == "GLedger") ? "ACCOUNTSLEDGERWC" : "ACCOUNTSLEDGER";
                string ltype = ""; //(Request.QueryString["RType"].ToString() == "GLedger") ? "Without Cancel" : "";
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGER", actcode, date1, date2, "", Narration, "", ltype, "", "");
            }

            return ds1;
        }
        protected void lnkShowSPLedger_Click(object sender, EventArgs e)
        {
            this.ShowDetailLedger();
        }
        private void ShowDetailLedger()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFromSp.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDatetoSp.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlRescode.SelectedValue.ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvSpledger.DataSource = null;
                this.gvSpledger.DataBind();
                return;
            }
            DataTable dt = HiddenSameDataSp(ds1.Tables[0]);
            DataTable dt1 = BalCalculationSp(dt);
            Session["tblspledger"] = dt1;
            this.gvSpledger.DataSource = dt1;
            this.gvSpledger.DataBind();
            //this.FooterCal();


        }
        private DataTable HiddenSameDataSp(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string vounum = dt1.Rows[0]["vounum"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();



            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {

                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["vounum"].ToString() == vounum)
                    {

                        dt1.Rows[j]["vounum"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();
                }

            }



            return dt1;

        }

        private DataTable BalCalculationSp(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double opnam, dramt, cramt, bbalamt = 0.00;
            string actcode = dt.Rows[0]["actcode"].ToString();
            //string grp=
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                if ((dt.Rows[i]["actcode"]).ToString().Trim() != actcode)
                {
                    bbalamt = 0.00;
                }
                actcode = dt.Rows[i]["actcode"].ToString();

                if ((dt.Rows[i]["vounum"]).ToString().Trim() == "SUB TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "BALANCE")
                    continue;



                //if (((dt.Rows[i]["actcode"]).ToString().Trim()).Length == 12)
                //{
                opnam = Convert.ToDouble(dt.Rows[i]["opam"]);
                dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                bbalamt = bbalamt + (opnam + dramt - cramt);
                dt.Rows[i]["clsam"] = bbalamt;
                //}


            }
            return dt;


        }
        protected void gvSpledger_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum");
                //Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmount");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmount");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmount");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AB")
                {
                    hlink.Font.Bold = true;
                    // OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;
                    ClAmt.Font.Bold = true;
                    hlink.Style.Add("text-align", "right");
                }
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvvounum");
            string voucher = ((HyperLink)e.Row.FindControl("HLgvvounum")).Text.ToString();
            if (voucher.Trim().Length == 14)
            {
                if (ASTUtility.Left(voucher, 2) == "PV" || ASTUtility.Left(voucher, 2) == "DV")
                {
                    hlink1.NavigateUrl = "RptAccVouher02.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
                else
                {
                    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher + "&comcod=" + comcod;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
            }

            //if (voucher.Substring(0,2)=="BC"|| voucher.Substring(0,2)=="BD"|| voucher.Substring(0,2)=="CC"|| voucher.Substring(0,2)=="CD"|| voucher.Substring(0,2)=="JV"|| voucher.Substring(0,2)=="CT")  
            //    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
        }
        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14 && ASTUtility.Left(mVOUNUM.Trim(), 2) != "PV")
            {
                //hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                //hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
                hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + mVOUNUM + "&comcod=" + comcod; ;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            if (this.chkqty.Checked == true && rbtnLedger.SelectedValue.ToString() == "SubLedger")
            {
                this.PrintLedgerWithQty();
            }
            else if (rbtnLedger.SelectedValue.ToString() == "DetailLedger")
            {
                this.PrintDetailLedger();
            }
            else
            {
                this.PrintLedger();
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Ledger";
                string eventdesc = "Print Ledger";
                string eventdesc2 = "From: " + this.txtDateFrom.Text + " To: " + this.txtDateto.Text;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void PrintDetailLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptsl = new RMGiRPT.R_21_GAcc.RPTSpecialLedger();
            DataTable dt = (DataTable)Session["tblspledger"];




            TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "(From " + Convert.ToDateTime(this.txtDateFromSp.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatetoSp.Text).ToString("dd-MMM-yyyy") + ")";
            TextObject rpttxtAccDesc = rptsl.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            rpttxtAccDesc.Text = "Account Description: " + this.ddlRescode.SelectedItem.ToString().Substring(13);
            TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            Session["Report1"] = rptsl;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintLedgerWithQty()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            RMGiRPT.R_21_GAcc.RptAccSLedger rptstk = new RMGiRPT.R_21_GAcc.RptAccSLedger();
            string Resdesc = "SUBSIDIARY HEAD: " + this.ddlConAccResHead.SelectedItem.Text.Substring(13);
            DataTable dt = (DataTable)Session["StoreTable"];
            if (dt == null)
                return;
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            rpttxtAccDesc.Text = "ACCOUNT HEAD: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            TextObject txtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
            txtResDesc.Text = Resdesc;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource((DataTable)Session["StoreTable"]);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Headertitle = this.ddlConAccHead.SelectedItem.Text.ToString().Substring(13);
            string dateft = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")";
            string supln = "";
            if (rbtnLedger.SelectedValue.ToString() == "SubLedger")
            {
                string rescode = this.ddlConAccResHead.SelectedItem.Text.Trim().ToString().Substring(13);
                supln = "Sub Head:" + rescode;
            }

            DataTable dt = (DataTable)Session["StoreTable"];

            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccVoucher.AccLeadger>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptAccountLdger", lst, null, null);
            //rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("dateft", dateft));
            rpt1.SetParameters(new ReportParameter("Headertitle", "Accounts Head:" + Headertitle));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Accounts LEDGER" + " " + dateft));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("SupplierN", supln));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}