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
    public partial class AccPurchaseFor : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        static string prevPage = String.Empty;
        public static double TAmount;

        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                // prevPage = Request.UrlReferrer.ToString();
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Foreign Purchase Accounts";
                this.CommonButton();
                CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                this.LoadBillCombo();
                //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;

               
            }

        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click); 
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
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
            string Billno =  "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBILLACCLC", Billno, "", "", "", "", "", "", "", "");
            this.ddlBillList.Items.Clear();
            this.ddlBillList.DataTextField = "textfield";
            this.ddlBillList.DataValueField = "billno";
            this.ddlBillList.DataSource = ds1.Tables[0];
            this.ddlBillList.DataBind();
            string genno = this.Request.QueryString["genno"].ToString();
            if (genno.Length > 0)
            {
                this.ddlBillList.SelectedValue = genno;
                this.txtdate.Text = this.Request.QueryString["date"].ToString();
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
            ((TextBox)this.gvPurFor.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.gvPurFor.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



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


            }

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")

            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                //this.ibtnvounu.Visible = true;
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            Session.Remove("tblt01");
            this.CreateTable();
            this.LoadBillCombo();
            this.gvPurFor.DataSource = null;
            this.gvPurFor.DataBind();
            //this.lnkFinalUpdate.Enabled = true;
            this.txtcurrentvou.Enabled = true;
            this.txtCurrntlast6.Enabled = true;

            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
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
           
            string voudat = this.txtdate.Text.Substring(0, 11);
            DateTime Bdate = this.GetBackDate();
            bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
            if (!dcon)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Issue Date is equal or less Current Date');", true);
                return;
            }


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string LCNumber = this.ddlBillList.SelectedValue.ToString();
            string cactcode = "000000000000";
            //string spclcode = "000000000000";
            this.GetVouCherNumber();
            //string voudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                  this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            //string voudat = this.txtdate.Text.Substring(0, 11);
            string refnum = "";
            string srinfo = "";
            //string trnremarks = "LCXXXXXXXXXXXX";
            string vounarration1 = "";
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
            string voutype = "Import Journal Voucher";

            string vtcode = "01";
            string edit = "";
            string TgvDrAmt = ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTDramt")).Text;
            string TgvCrAmt = ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTCramt")).Text;
            if (vouno == "JV" && TgvDrAmt != TgvCrAmt)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Dr. Amount not equals to Cr. Amount.');", true);
                return;
            }
            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE_01", vounum, voudat, refnum, srinfo, vounarration1, vounarration2, voutype, vtcode, edit, "", "", "", "", "", "");
                if (!resultb)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }
                string grrno2 = "XXXXXXXXXXXXXXX";
                //-----------Update Transaction A Table-----------------//
                for (int i = 0; i < gvPurFor.Rows.Count; i++)
                {

                    string actcode = ((Label)this.gvPurFor.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.gvPurFor.Rows[i].FindControl("lblResCod")).Text.Trim();

                    string trnqty = Convert.ToDouble("0" + ((Label)this.gvPurFor.Rows[i].FindControl("lblgvqty")).Text.Trim()).ToString();
                    double Dramt = ASTUtility.StrPosOrNagative(((Label)this.gvPurFor.Rows[i].FindControl("lblgvDramt")).Text.Trim());
                    double Cramt = ASTUtility.StrPosOrNagative(((Label)this.gvPurFor.Rows[i].FindControl("lblgvCramt")).Text.Trim());
                    string trnremarks = ((Label)this.gvPurFor.Rows[i].FindControl("lblgvRemarks")).Text.Trim();
                    string grrno = ((Label)this.gvPurFor.Rows[i].FindControl("lblgrrno")).Text.Trim();
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    //string spclcode = "000000000000";
                    string spclcode = (ASTUtility.Left(actcode, 2) == "17") ? ASTUtility.Right(LCNumber, 12) : "000000000000";

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE_01", vounum, actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);

                        return;
                    }

                    if (grrno2 != grrno)
                    {


                        string storeid = ((Label)this.gvPurFor.Rows[i].FindControl("lblstoreid")).Text.Trim();
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEPURLC",
                                grrno, storeid, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);

                            return;
                        }
                        grrno2 = grrno;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
             

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

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

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
               
                if (this.ddlBillList.Items.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select LC Number');", true);
                    return;
                }
                //this.Panel1.Visible = true;

                string grrno = this.ddlBillList.SelectedValue.ToString().Substring(0, 15);
                string storeid = this.ddlBillList.SelectedValue.ToString().Substring(15, 12);
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETLCACCVOU", grrno, storeid, "", "", "", "", "", "", "");
                DataTable dt1 = ds2.Tables[0];
                Session["DCInfo"] = HiddenSameData(dt1);
                this.gvPurFor.DataSource = dt1;
                this.gvPurFor.DataBind();
                //this.Panel2.Visible = true;
                ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTDramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(Dramt)", "")) ?
                0.00 : dt1.Compute("Sum(Dramt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
                ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTCramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(Cramt)", "")) ?
                0.00 : dt1.Compute("Sum(Cramt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
    }
}