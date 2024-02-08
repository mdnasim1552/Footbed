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
    public partial class AccProductionJV : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Production Update";
                this.txtdate.Text = (this.Request.QueryString["date"].Length == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy ddd") : this.Request.QueryString["date"].ToString();

                CreateTable();
                this.LoadBillCombo();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("prodid", Type.GetType("System.String"));
            tblt01.Columns.Add("nar", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }
        }
        private void LoadBillCombo()
        {

            string comcod = this.GetCompCode();
            //string mrslno = this.txtmrslno.Text.Trim() + "%";
            string mrslno = "%%";
            string prodNO = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRODUCTION", mrslno, prodNO, "", "", "", "", "", "", "");
            this.ddlProduList.Items.Clear();
            this.ddlProduList.DataTextField = "textfield";
            this.ddlProduList.DataValueField = "prodid";
            this.ddlProduList.DataSource = ds1.Tables[0];
            this.ddlProduList.DataBind();
            if (this.Request.QueryString["genno"].ToString().Length != 0)
            {
                this.lbtnOk_Click(null, null);
                this.lbtnSelectBill_Click(null, null);
                this.ibtnvounu_Click(null, null);
            }
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                          0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((Label)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((Label)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }

        protected void ibtnvounu_Click(object sender, ImageClickEventArgs e)
        {

            try
            {

                string comcod = this.GetCompCode();
                //this.lblmsg.Text = "";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    //this.lblmsg.Text = "Voucher Date Must  Be Greater then Opening Date";
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


            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                this.pnlPrevBill.Visible = true;
                this.pnlSelectBill.Visible = true;
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            Session.Remove("tblt01");
            this.CreateTable();
            //this.LoadBillCombo();
            //this.lblmsg.Text = "";
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
            this.lnkFinalUpdate.Enabled = true;
            this.txtcurrentvou.Enabled = true;
            this.txtCurrntlast6.Enabled = true;
            this.ibtnvounu.Visible = false;
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            //this.lblmsg.Visible = false;
            this.pnlPrevBill.Visible = false;
            this.pnlSelectBill.Visible = false;
        }

        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            //this.lblmsg.Visible = true;
            if (this.txtcurrentvou.Text.Trim() == "")
            {
                //this.lblmsg.Text = "Please insert Voucher no !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please insert Voucher no !!!');", true);
                return;
            }

            string voudat = this.txtdate.Text.Substring(0, 11);

            DateTime Bdate = this.GetBackDate();
            bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
            if (!dcon)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                return;
            }

            if (accData.ToDramt != accData.ToCramt)
            {
                //this.lblmsg.Text = "Debit Amount must be Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Debit Amount must be Equal Credit Amount');", true);
                return;

            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Production Journal Voucher";
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
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//
                string prodid2 = "XXXXXXXXXXXXXX";

                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    string prodid = ((Label)this.dgv2.Rows[i].FindControl("lblprodid")).Text.Trim();

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                    if (prodid2 != prodid)
                    {
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEACCPORDUCTION",
                                prodid, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                        prodid2 = prodid;
                    }
                }
                //this.lblmsg.Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully.');", true);
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;

            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }

        }
        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string vouprint = "";
            switch (comcod)
            {

                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnSelectBill_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string prodid = this.ddlProduList.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRODUCTIONINFO", prodid,
                          "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            DataTable tblt01 = (DataTable)Session["tblt01"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                string dgResCode = dt1.Rows[i]["rescode"].ToString();
                string dgSpclCode = dt1.Rows[i]["spclcode"].ToString();
                string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                double dgTrnrate = Convert.ToDouble(dt1.Rows[i]["trnrate"]);
                double dgTrnQty = Convert.ToDouble(dt1.Rows[i]["trnqty"]);
                //if (Convert.ToDouble(dt1.Rows[i]["trnqty"]) > 0)
                //{
                //    dgTrnrate = Convert.ToDouble(dt1.Rows[i]["trndram"]) / Convert.ToDouble(dt1.Rows[i]["trnqty"]);
                //}

                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["trndram"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["trncram"]);
                string dgTrnRemarks = dt1.Rows[i]["prodid"].ToString();
                string dgmrnarr = dt1.Rows[i]["nar"].ToString();

                DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and rescode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'");
                if (dr2.Length > 0)
                {

                    return;

                }

                DataRow dr1 = tblt01.NewRow();
                dr1["actcode"] = dgAccCode;
                dr1["rescode"] = dgResCode;
                dr1["spclcode"] = dgSpclCode;
                dr1["actdesc"] = dgAccDesc;
                dr1["resdesc"] = dgResDesc;
                dr1["spcfdesc"] = dgSpclDesc;
                dr1["trnqty"] = dgTrnQty;
                dr1["trnrate"] = dgTrnrate;
                dr1["trndram"] = dgTrnDrAmt;
                dr1["trncram"] = dgTrnCrAmt;
                dr1["trnrmrk"] = dgTrnRemarks;
                dr1["prodid"] = dgTrnRemarks;
                dr1["nar"] = dgmrnarr;
                tblt01.Rows.Add(dr1);
            }
            //if (tblt01.Rows.Count == 0)
            //    return;
            Session["tblt01"] = HiddenSameData(tblt01);
            dgv2.DataSource = tblt01;
            dgv2.DataBind();
            calculation();
            this.ibtnvounu.Visible = true;
            this.txtCurrntlast6.ReadOnly = false;
            //this.txtRefNum.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            //this.txtSrinfo.Text = ds1.Tables[1].Rows[0]["chqno"].ToString();
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





        protected void imgSearchMRno_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
        }
    }
}