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


namespace SPEWEB.F_21_GAcc
{
    public partial class AccPurchase : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Accounts";
                this.CommonButton();


                CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtntotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);

            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }



        private void CommonButton()
        {
          
          
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";


            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

        }


        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("billnar", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void LoadBillCombo()
        {

            string comcod = this.GetCompCode();
            string Billno = (this.Request.QueryString["genno"].Length == 0) ?  "%" : this.Request.QueryString["genno"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBILlACC", Billno, "", "", "", "", "", "", "", "");
            this.ddlBillList.Items.Clear();
            this.ddlBillList.DataTextField = "textfield";
            this.ddlBillList.DataValueField = "billno";
            this.ddlBillList.DataSource = ds1.Tables[0];
            this.ddlBillList.DataBind();
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                          0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.LoadBillCombo();
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
               

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            ViewState.Remove("tblt01");
            this.CreateTable();
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = true;
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
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

           

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }




            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Debit Amount must be Equal Credit Amount');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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



            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
            //Existing   Purchase No  

            if (dgv2.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select at least one Bill');", true);
                return;

            }


            for (int i = 0; i < dgv2.Rows.Count; i++)
            {

                string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();
                DataSet ds4;
                if (i == 0)
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPURBILL", billno, "", "", "", "", "", "", "", "");

                else if (((Label)this.dgv2.Rows[i - 1].FindControl("lblBillno")).Text.Trim() == billno)
                    continue;

                else
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPURBILL", billno, "", "", "", "", "", "", "", "");


                if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Voucher No already Existing in this Bill No');", true);
                    return;
                }

            }

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
                string billno2 = "XXXXXXXXXXXXXX";

                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());

                    double trnamt = (Dramt - Cramt);
                    string trnremarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();
                    if (trnamt != 0)
                    {
                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                                actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);

                            return;
                        }

                        if (billno2 != billno)
                        {
                            string advAmount = Convert.ToDouble("0" + this.txtAdvanced.Text).ToString();
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEPURBILL",
                                    billno, vounum, advAmount, "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);

                                return;
                            }
                            billno2 = billno;
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Purchase Update";
                    string eventdesc = "Update Purchase Bill";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }



        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string curvoudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=accVou&vounum=" + vounum + "', target='_blank');</script>";
            
        }
        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            int TblRowIndex2;
            //this.lblmsg1.Text = "";
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {
                TblRowIndex2 = (this.dgv2.PageSize) * (this.dgv2.PageIndex) + j;
                double CrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text = CrAmt.ToString("#,##0.000;(#,##0.000); ");
                tbl1.Rows[TblRowIndex2]["trncram"] = CrAmt;
            }
            ViewState["tblt01"] = tbl1;
        }
        protected void lbtntotal_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
            //ViewState.Remove("tblt01");
            //dgv2.DataSource = null;
            //dgv2.DataBind();
            //CreateTable();
            //this.lbtnSelectBill_Click(null, null);
        }
        private void GridColoumnVisible()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {

                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                string mACTCODE = tbl1.Rows[TblRowIndex2]["actcode"].ToString();
                if (ASTUtility.Left(mACTCODE, 2) != "23")
                    ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).ReadOnly = true;
            }


        }
        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            this.GridColoumnVisible();
            calculation();


        }
        protected void lbtnSelectBill_Click(object sender, EventArgs e)
        {

            try
            {
                string comcod = this.GetCompCode();
                string billid = this.ddlBillList.SelectedValue.ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCPURCHASES", billid,
                              "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                DataTable tblt01 = (DataTable)ViewState["tblt01"];

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                    string dgResCode = dt1.Rows[i]["rescode"].ToString();
                    string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                    string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                    string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                    string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                    double dgTrnrate = 0.00;
                    double dgTrnQty = Convert.ToDouble(dt1.Rows[i]["billqty"]);
                    if (Convert.ToDouble(dt1.Rows[i]["billqty"]) > 0)
                    {
                        dgTrnrate = Convert.ToDouble(dt1.Rows[i]["Dr"]) / Convert.ToDouble(dt1.Rows[i]["billqty"]);
                    }

                    double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                    double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["Cr"]);
                    string dgTrnRemarks = dt1.Rows[i]["billid"].ToString();
                    string dgBillnarr = dt1.Rows[i]["billnar"].ToString();

                    DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'");
                    if (dr2.Length > 0)
                    {

                        return;

                    }

                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dgAccCode;
                    dr1["subcode"] = dgResCode;
                    dr1["spclcode"] = dgSpclCode;
                    dr1["actdesc"] = dgAccDesc;
                    dr1["subdesc"] = dgResDesc;
                    dr1["spcldesc"] = dgSpclDesc;
                    dr1["trnqty"] = dgTrnQty;
                    dr1["trnrate"] = dgTrnrate;
                    dr1["trndram"] = dgTrnDrAmt;
                    dr1["trncram"] = dgTrnCrAmt;
                    dr1["trnrmrk"] = dgTrnRemarks;
                    dr1["billno"] = dgTrnRemarks;
                    dr1["billnar"] = dgBillnarr;
                    tblt01.Rows.Add(dr1);
                }
                //if (tblt01.Rows.Count == 0)
                //    return;
                ViewState["tblt01"] = HiddenSameData(tblt01);
                //dgv2.DataSource = tblt01;
                //dgv2.DataBind();
                //calculation();
                this.Data_Bind();
                this.txtCurrntlast6.ReadOnly = false;
            }
            catch (Exception ex)
            {

            }
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
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




        protected void imgSearchBillno_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
        }
    }
}