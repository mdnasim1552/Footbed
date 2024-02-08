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
using SPELIB;
using SPEENTITY.C_22_Sal;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccIncomeOfOrd : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        //Sales_BO lst1 = new Sales_BO();
        Common CommonClass = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                CreateTable();
                this.ShowDelMode();
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Export Bill Update";
                this.CommonButton();
                this.CurrencyInf();
                GetInvoiceNo();

                if (Request.QueryString["date"].ToString() != "01-Jan-1900")
                {
                    this.txtdate.Text = this.Request.QueryString["date"].ToString();
                    this.txtMDate.Text = this.Request.QueryString["date"].ToString();
                    this.txtMDate.Enabled = false;
                }
                else
                {
                    this.txtMDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                }

            }

        }


        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnForward_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtntotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);

            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }



        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";


        }

        private void ShowDelMode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds6 = accData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETDELMODE", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            this.ddlDelMode.DataTextField = "gdesc";
            this.ddlDelMode.DataValueField = "gcod";
            this.ddlDelMode.DataSource = ds6.Tables[0];
            this.ddlDelMode.DataBind();

            if (Request.QueryString["delvtrm"].ToString() != "")
            {
                this.ddlDelMode.SelectedValue = this.Request.QueryString["delvtrm"].ToString();
                this.ddlDelMode.Enabled = false;
            }

        }

        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;
            this.ddlCurrency.DataValueField = "curcode";
            this.ddlCurrency.DataTextField = "curdesc";
            this.ddlCurrency.DataSource = lstCurryDesc;
            this.ddlCurrency.DataBind();





            this.ddlCurrency_SelectedIndexChanged(null, null);
        }
        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string tcode = "001";
            string fcode = this.ddlCurrency.SelectedValue.ToString();

            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            var List = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode)).ToList();

            double method = (List.Count > 0) ? List[0].conrate : 0;

            this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");


        }


        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("rescode", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("resdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcfdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            //tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("fcamtdr", Type.GetType("System.Double"));
            tblt01.Columns.Add("fcamtcr", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("invno", Type.GetType("System.String"));
            tblt01.Columns.Add("sizeid", Type.GetType("System.String"));
            tblt01.Columns.Add("rate", Type.GetType("System.Double"));
            ViewState["tblt01"] = tblt01;
        }


        private void GetInvoiceNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtdate.Text.Trim().Substring(0, 11);
            string Invoiceno = (Request.QueryString["actcode"].ToString() != "") ? this.Request.QueryString["actcode"].ToString() : "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETILCINVNO", date, Invoiceno, "", "", "", "", "", "", "");
            this.ddlInvList.Items.Clear();
            this.ddlInvList.DataTextField = "textfield";
            this.ddlInvList.DataValueField = "invno";
            this.ddlInvList.DataSource = ds1.Tables[0];
            this.ddlInvList.DataBind();

            if (Request.QueryString["actcode"].ToString() != "")
            {
                this.ddlInvList.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlInvList.Enabled = false;
            }
        }
        protected void imgSearchInvoiceno_Click(object sender, EventArgs e)
        {
            this.GetInvoiceNo();
        }



        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;

            //((Label)this.dgv2.FooterRow.FindControl("lblTgvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trnqty)", "")) ?
            //            0.00 : dt2.Compute("Sum(trnqty)", ""))).ToString("#,##0.00;(#,##0.00); - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                        0.00 : dt2.Compute("Sum(trndram)", ""))).ToString("#,##0.00;(#,##0.00); - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblTgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))).ToString("#,##0.00;(#,##0.00); - ");

            ((Label)this.dgv2.FooterRow.FindControl("lblgvFfcamtdr")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(fcamtdr)", "")) ?
                        0.00 : dt2.Compute("Sum(fcamtdr)", ""))).ToString("#,##0.00;(#,##0.00); - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblTgvfcamtcr")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(fcamtcr)", "")) ?
                        0.00 : dt2.Compute("Sum(fcamtcr)", ""))).ToString("#,##0.00;(#,##0.00); - ");

        }

        protected void ibtnvounu_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string VNo3 = "JV";
            string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
            this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
            this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);
            string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();


        }
        private void GetVouCherNumber()
        {
            try
            {

                string comcod = this.GetCompCode();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                //if (txtopndate==Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                //    ;
                if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Voucher Date Must  Be Greater then Opening Date');", true);

                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }
        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            string voudat1 = ASTUtility.DateFormat(this.txtdate.Text);
            DateTime Bdate;
            bool dcon;
            Bdate = this.GetBackDate();
            dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat1));
            if (!dcon)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Issue Date is equal or less Current Date');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            this.GetVouCherNumber();
            string voudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";
            double ExRate = Convert.ToDouble(this.lblConRate.Text);
            string Curcode = this.ddlCurrency.SelectedValue.ToString();

            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//
                string invno2 = "XXXXXXXXXXXXXX";
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCode")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string Sizeid = ((Label)this.dgv2.Rows[i].FindControl("lblSizeid")).Text.Trim();
                    double trnqty = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvQty")).Text.Trim());
                    double Dramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((Label)this.dgv2.Rows[i].FindControl("lblgvRemarks")).Text.Trim();
                    string invno = ((Label)this.dgv2.Rows[i].FindControl("lblTrnno")).Text.Trim();

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            actcode, rescode, cactcode, voudat, trnqty.ToString(), trnremarks, vtcode, trnamt, spclcode, "", "", Sizeid, "", "");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                    if (invno2 != invno)
                    {

                        string DelMode = this.ddlDelMode.SelectedValue.ToString();
                        string DelDate = this.txtMDate.Text;
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATESHIPINF01",
                                invno, vounum, ExRate.ToString(), DelMode, DelDate, Curcode, "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                        invno2 = invno;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHERPUR",
                                        vounum, "", "", "", "", "", "", "", "");

                ReportDocument rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher();
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                txtCompanyName.Text = comnam;
                TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
        protected void lbtntotal_Click(object sender, EventArgs e)
        {
            //ViewState["tblt01"]
            ViewState.Remove("tblt01");
            dgv2.DataSource = null;
            dgv2.DataBind();
            CreateTable();
            this.lbtnSelectInv_Click(null, null);
        }
        protected void lbtnSelectInv_Click(object sender, EventArgs e)
        {
            this.dgv2.Columns[5].HeaderText = "Dr. Amount </br> (" + this.ddlCurrency.SelectedItem.Text + ")";
            this.dgv2.Columns[6].HeaderText = "Cr. Amount </br> (" + this.ddlCurrency.SelectedItem.Text + ")";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string invno = this.ddlInvList.SelectedValue.ToString();
            double ExRate = Convert.ToDouble(this.lblConRate.Text);
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCLCINCOME", invno,
                          ExRate.ToString(), "", "", "", "", "", "", "");
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);

            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string invno1 = dt1.Rows[i]["invno"].ToString();
                string actcode = dt1.Rows[i]["actcode"].ToString();
                string rescode = dt1.Rows[i]["rescode"].ToString();
                string dgSpclCode = dt1.Rows[i]["spclcode"].ToString();
                string dgSizeid = dt1.Rows[i]["sizeid"].ToString();
                DataRow[] dr = tblt01.Select("invno='" + invno1 + "' and actcode='" + actcode + "' and rescode='" + rescode + "' and spclcode='" + dgSpclCode + "' and sizeid='" + dgSizeid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dt1.Rows[i]["actcode"].ToString();
                    dr1["actdesc"] = dt1.Rows[i]["actdesc"].ToString();
                    dr1["rescode"] = dt1.Rows[i]["rescode"].ToString();
                    dr1["spclcode"] = dt1.Rows[i]["spclcode"].ToString();
                    dr1["sizeid"] = dt1.Rows[i]["sizeid"].ToString();
                    dr1["resdesc"] = dt1.Rows[i]["resdesc"].ToString();
                    dr1["spcfdesc"] = dt1.Rows[i]["spcfdesc"].ToString();
                    dr1["trnqty"] = Convert.ToDouble(dt1.Rows[i]["trnqty"]);
                    dr1["trndram"] = Convert.ToDouble(dt1.Rows[i]["dr"]);
                    dr1["trncram"] = Convert.ToDouble(dt1.Rows[i]["cr"]);
                    dr1["fcamtdr"] = Convert.ToDouble(dt1.Rows[i]["fcamtdr"]);
                    dr1["fcamtcr"] = Convert.ToDouble(dt1.Rows[i]["fcamtcr"]);
                    dr1["trnrmrk"] = dt1.Rows[i]["invno"].ToString();
                    dr1["invno"] = dt1.Rows[i]["invno"].ToString();
                    dr1["rate"] = dt1.Rows[i]["rate"].ToString();
                    tblt01.Rows.Add(dr1);
                }
            }
            if (tblt01.Rows.Count == 0)
                return;
            ViewState["tblt01"] = tblt01;
            dgv2.DataSource = tblt01;
            dgv2.DataBind();
            calculation();



            this.txtCurrntlast6.ReadOnly = false;



        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();

                }

            }
            return dt1;

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                // this.lblcurVounum.Visible = true;
                //this.txtcurrentvou.Visible = true;
                //this.txtCurrntlast6.Visible = true;
                this.Panel1.Visible = true; ;
                this.lblRefNum.Visible = true;
                this.txtRefNum.Visible = true;
                this.lblSrInfo.Visible = true;
                this.txtSrinfo.Visible = true;
                this.lblNaration.Visible = true;
                this.txtNarration.Visible = true;
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                this.txtNarration.Text = "Export Update";
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                //this.lblcurVounum.Visible = false;
                //this.txtcurrentvou.Visible = false;
                //this.txtCurrntlast6.Visible = false;
                this.Panel1.Visible = false; ;
                this.lblRefNum.Visible = false;
                this.txtRefNum.Visible = false;
                this.lblSrInfo.Visible = false;
                this.txtSrinfo.Visible = false;
                this.lblNaration.Visible = false;
                this.txtNarration.Visible = false;
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                this.GetInvoiceNo();

                this.txtRefNum.Text = "";
                this.txtSrinfo.Text = "";
                this.txtNarration.Text = "";
                DataTable dt = (DataTable)ViewState["tblt01"];
                dt.Rows.Clear();
                ViewState["tblt01"] = dt;
            }
        }


    }
}