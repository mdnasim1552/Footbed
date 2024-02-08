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
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_15_DPayReg
{
    public partial class ChequeSignSheet : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;
        // public static string lblVounum = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.CommonButton();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.Bankcode();
                this.ColumnVisible();

                ((Label)this.Master.FindControl("lblTitle")).Text = "Cheque Issued";
                

                this.lnkOk_Click(null, null);
            }

        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnUpdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strScript = "window.close();";
            ScriptManager.RegisterStartupScript(this, typeof(string), "key", strScript, true);
        }
        private void ColumnVisible()
        {
            if (this.Request.QueryString["Type"] == "Acc")
            {

                this.dgv1.Columns[7].Visible = false;
                this.dgv1.Columns[8].Visible = false;
            }
            else
            {
                this.dgv1.Columns[7].Visible = true;
                this.dgv1.Columns[8].Visible = true;
            }
        }
        private void GetBillList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string srchproject = "%" + this.txtserchmrf.Text.Trim() + "%";
            string pactcode = (this.ddlProject.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProject.SelectedValue.ToString();

            //string todat = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string paydat = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETISSUEWISECHK", pactcode, srchproject, paydat, "", "", "", "", "", "");
            if (ds2 == null)
                return;



            

            this.ddlBillList.DataTextField = "textfield";
            this.ddlBillList.DataValueField = "valefield";
            this.ddlBillList.DataSource = ds2.Tables[0];
            this.ddlBillList.DataBind();
            this.ddlBillList.SelectedValue= this.Request.QueryString["genno"].ToString();


            this.ShowData();

        }

        private void ChequeNo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string bankname = this.ddlBankName.SelectedValue.ToString();
            string flag = "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "TOPCHEQUE", bankname, flag, "", "", "", "", "", "", "");
            this.ddlcheque.DataTextField = "chequeno";
            this.ddlcheque.DataValueField = "chequeno";
            this.ddlcheque.DataSource = ds1.Tables[0];
            this.ddlcheque.DataBind();
            this.ddlcheque_SelectedIndexChanged(null, null);


        }
        private void Refrsh()
        {
            
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

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string srchproject =  "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETACTCODENAME", srchproject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();




        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            //this.btnUpdate.Visible = true;
            // this.previousnar();
            this.GetBillList();
            this.ChequeNo();
            //this.ShowData();
            this.PnlNarration.Visible = true;



        }

        private void previousnar()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string ConAccHead = this.ddlBankName.SelectedValue.ToString();
            //string VNo1 = (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B");
            // string VNo2 = (VNo1 == "J" ? "V" : (lblTitle.Contains("Payment") ? "D" : (lblTitle.Contains("Contra") ? "T" : "C")));
            string VNo3 = (ConAccHead.Substring(0, 4) == "1901" ? "CD" : "BD");
            string date = this.txtdate.Text.Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "LASTNARRATION", VNo3, date, "", "", "", "", "", "", "");

            //string vounum=ds4.Tables[0].Rows[0]["vounum"].ToString();
            if (ds4.Tables[0].Rows.Count == 0)
                this.txtNarration.Text = "";

            else
                this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
        }

        private void ShowData()
        {

           
            try
            {
                Session.Remove("tbChqSign");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string SrchRefno = "%" + this.txtserchmrf.Text.Trim() + "%";
                string Issueno = this.ddlBillList.SelectedValue.ToString();

                string pactcode = ((this.ddlProject.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProject.SelectedValue.ToString()) + "%";
                string RptType = (this.Request.QueryString["Type"] == "Acc") ? "Acc" : "Mgt";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "SHOWCHQSIGN", Date, pactcode, "", "", SrchRefno, RptType, Issueno, "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }



                Session["tbChqSign"] = this.HiddenSameDate(ds1.Tables[0]);



                this.txtPayto.Text = ds1.Tables[0].Rows[0]["payto"].ToString();

                if (ds1.Tables[1].Rows[0]["billno"].ToString() == "GBL")
                {
                    this.txtNarration.Text = ds1.Tables[0].Rows[0]["narr"].ToString();


                }

                else
                {
                    this.previousnar();
                }
                //   Session["UserLog"] = ds1.Tables[2];

                this.Data_Bind();



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }

        protected void lstBillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowData();
            //this.previousnar();


        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbChqSign"];
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();



            if (dt.Rows.Count > 0)
            {

                string amount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ? 0.00 : dt.Compute("Sum(amount)", ""))).ToString("#,##0;(#,##0); -");
                ((Label)this.dgv1.FooterRow.FindControl("lblgvFchqamt")).Text = amount;
            }












        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string slnum = dt1.Rows[0]["slnum"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            string rescode = dt1.Rows[0]["rescode"].ToString();

            int j;



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["slnum"].ToString() == slnum && dt1.Rows[0]["actcode"].ToString() == actcode && dt1.Rows[j]["rescode"].ToString() == rescode)
                {
                    //dt1.Rows[j]["actdesc"] = "";
                    // dt1.Rows[j]["resdesc"] = "";
                    dt1.Rows[j]["apamt"] = 0.00;
                }
                else
                {
                    if (dt1.Rows[j]["slnum"].ToString() == slnum)
                    {
                        dt1.Rows[j]["apamt"] = 0.00;
                    }

                    //if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    //{
                    //    dt1.Rows[j]["actdesc"] = "";
                    //}

                    //if (dt1.Rows[j]["rescode"].ToString() == rescode)
                    //{
                    //    dt1.Rows[j]["resdesc"] = "";
                    //}

                }

                slnum = dt1.Rows[j]["slnum"].ToString();
                actcode = dt1.Rows[j]["actcode"].ToString();
                rescode = dt1.Rows[j]["rescode"].ToString();
            }





            return dt1;


        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tbltopage"];
            double cramt = Convert.ToDouble(((DataTable)Session["tbltopage"]).Rows[0]["cramt"]);
            ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = cramt.ToString("#,##0;-#,##0; ");
        }


        protected DateTime GetBackDate()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string vounum = lblVoun.Text;
            string value = this.ChboxPayee.Checked ? "A/C Payee" : " ";
            string chqno = this.txtRefNum.Text;//this.ddlPostDatedCheque.SelectedValue.Substring(14);

            if (chkCrVou.Checked)
            {
                if (this.chkPrint.Checked)


                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_21_GAcc/Print.aspx?Type=Cheque&vounum=" + vounum + "&payee=" + value + "', target='_blank');</script>";

                else
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_21_GAcc/Print.aspx?Type=accVou&vounum=" + vounum + "', target='_blank');</script>";

                }

                ///this.Printvoucher();
            }
            else
            {
                if (this.chkPrint.Checked)
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_21_GAcc/Print.aspx?Type=PosdatCheque&vounum=" + vounum + "&payee=" + value + "&chequeno=" + chqno + "', target='_blank');</script>";


                }
                else
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_21_GAcc/Print.aspx?Type=PostDatVou&vounum=" + vounum + "', target='_blank');</script>";

                }
            }


        }



        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkAccdesc1 = (HyperLink)e.Row.FindControl("hlnkAccdesc1");



                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string subcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();
                string subdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "resdesc")).ToString();


                if (subcode != "000000000000")
                {
                    hlnkAccdesc1.NavigateUrl = "~/F_15_DPayReg/LinkAccSpLedger.aspx?Type=DetailLedger&Date1=" + Convert.ToDateTime(this.txtdate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&sirdesc=" + subdesc;



                }
                else
                {
                    hlnkAccdesc1.NavigateUrl = "~/F_15_DPayReg/LinkAccLedger.aspx?Type=Ledger&RType=GLedger&Date1=" + Convert.ToDateTime(this.txtdate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&actcode=" + actcode;



                }

            }
        }


        protected void dgv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void dgv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.dgv1.EditIndex = e.NewEditIndex;
            this.Data_Bind();


            try
            {
                string comcod = this.GetCompCode();
                string ttsrch = "%";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string UserId = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Cactcode = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvCactCod")).Text.Trim();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCONACCHEAD", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count == 0)
                    return;

                DropDownList ddl2 = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlCactcode");
                ddl2.DataTextField = "cactdesc";
                ddl2.DataValueField = "cactcode";
                ddl2.DataSource = ds2.Tables[0];
                ddl2.DataBind();
                ddl2.SelectedValue = Cactcode;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }






        }
        protected void dgv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tbChqSign"];

            string Cactcode = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlCactcode")).SelectedValue.ToString();
            string Cactdesc = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlCactcode")).SelectedItem.Text.Trim();
            string Chequeno = ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvChqNo")).Text.Trim();

            double Amount = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvAmount")).Text.Trim());
            string Chqdate = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblgvChqdate")).Text.Trim();
            //string Narration = ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("lblgvNarr")).Text.Trim();


            int index = (this.dgv1.PageIndex) * this.dgv1.PageSize + e.RowIndex;

            tbl1.Rows[index]["cactcode"] = Cactcode;
            tbl1.Rows[index]["cactdesc"] = Cactdesc;
            tbl1.Rows[index]["chequeno"] = Chequeno;
            tbl1.Rows[index]["amount"] = Amount;
            //  tbl1.Rows[index]["narr"] = Narration;

            Session["tbChqSign"] = tbl1;
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tbChqSign_Update();
            this.Data_Bind();
        }

        private void Session_tbChqSign_Update()
        {
            DataTable tbl1 = (DataTable)Session["tbChqSign"];
            int index;
           
            for (int j = 0; j < this.dgv1.Rows.Count; j++)
            {

                //string Cactcode = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlCactcode")).SelectedValue.ToString();
                //string Cactdesc = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlCactcode")).SelectedItem.Text.Trim();
                string Chequeno = ((TextBox)this.dgv1.Rows[j].FindControl("txtgvChqNo")).Text.Trim();

                double Amount = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvAmount")).Text.Trim()));
                //Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvAmount")).Text.Trim());
                //string Chqdate = ((Label)this.dgv1.Rows[j].FindControl("lblgvChqdate")).Text.Trim();
                string Chqdate =Convert.ToDateTime(((TextBox)this.dgv1.Rows[j].FindControl("lblgvChqdate")).Text.Trim()).ToString("dd-MMM-yyyy");


                index = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + j;

                //tbl1.Rows[index]["cactcode"] = Cactcode;
                //tbl1.Rows[index]["cactdesc"] = Cactdesc;
                tbl1.Rows[index]["chequeno"] = Chequeno;
                tbl1.Rows[index]["amount"] = Amount;
                tbl1.Rows[index]["chqdate"] = Chqdate;
                //tbl1.Rows[index]["narr"] = Narration;
                //tbl1.Rows[index]["payto"] = Payto;


            }
            Session["tbChqSign"] = tbl1;


            
        }



        private void Bankcode()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string UserId = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);

                string ttsrch =  "%";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCONACCHEAD", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count == 0)
                    return;
                this.ddlBankName.DataTextField = "cactdesc";
                this.ddlBankName.DataValueField = "cactcode";
                this.ddlBankName.DataSource = ds2.Tables[0];
                this.ddlBankName.DataBind();

                this.ChequeNo();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
        protected void imgbtnSrchBank_Click(object sender, EventArgs e)
        {
            this.Bankcode();
        }





        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChequeNo();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //  DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

               

                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                    return;
                }
                this.Session_tbChqSign_Update();
                //this.CheckValue();
                string acvounum = "";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();

                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

                string slnum = ddlBillList.SelectedValue.ToString().Substring(0, 9);//code.Substring(0, 9);
                string actcode = "";
                string rescode = "";
                //string chequeno = code.Substring(33).ToString();
                string chqdateTA = "";
                string chequeno = this.txtRefNum.Text.Trim();// this.ddlcheque.SelectedValue.ToString();
                string cactcode = this.ddlBankName.SelectedValue.ToString();
                string vtcode = "";
                /////////////////////////////////////////////////////////
                DataTable dt = (DataTable)Session["tbChqSign"];
                DataTable dt1 = dt.Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("slnum='" + slnum + "'");
                dt1 = dv.ToTable();


                // Check Existing Voucher


                DataSet dschk = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPAYID", slnum, "", "", "", "", "", "", "", "");
                if (dschk.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Voucher No already Existing in this Payment ID');", true);
                    return;
                }






                /////////////////////////////////////////////////

                foreach (DataRow dr2 in dt1.Rows)
                {

                    if (Convert.ToDouble(dr2["amount"]) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input Amount);", true);
                      
                        return;
                    }
                    //////////////////////////////////////////////////
                    if (Convert.ToDouble(dr2["apamt1"]) < Convert.ToDouble(dr2["amount"]))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Amount Equal or Below Aproved  Amount');", true);
                        return;
                    }

                }




                string voudat = ASTUtility.DateFormat(this.txtdate.Text);
                DateTime Bdate = this.GetBackDate();
                bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                    return;
                }

                if (chkCrVou.Checked)
                {
                    try
                    {
                        

                        DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                        if (ds2.Tables[0].Rows.Count == 0)
                        {
                            return;

                        }

                        DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                        if (txtopndate >= Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Voucher Date Must  Be Greater then Opening Date');", true);
                            return;



                        }


                        string ConAccHead = this.ddlBankName.SelectedValue.ToString();
                        string vactcode = dt1.Rows[0]["actcode"].ToString();
                        string VNo1 = (vactcode.Substring(0, 2) == "19" || vactcode.Substring(0, 2) == "29") ? "C" : ConAccHead.Substring(0, 4) == "1901" ? "C" : "B";
                        string VNo2 = (vactcode.Substring(0, 2) == "19" || vactcode.Substring(0, 2) == "29") ? "T" : "D";
                        string VNo3 = Convert.ToString(VNo1 + VNo2);
                        vtcode = (VNo3 == "CT") ? "92" : "99";


                        string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
                        DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                        DataTable dt4 = ds4.Tables[0];
                        acvounum = dt4.Rows[0]["couvounum"].ToString();
                        lblVoun.Text = dt4.Rows[0]["couvounum"].ToString();
                        string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();

                    }
                    catch (Exception ex)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

                    }

                }
                else
                {
                    DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETNEWVOUCHER", voudat, "PV", "", "", "", "", "", "", "");
                    Session["NEWVOU"] = ds5.Tables[0];
                    DataTable dt12 = (DataTable)Session["NEWVOU"];

                    if (this.Request.QueryString["Type"] == "Acc")
                    {

                        acvounum = dt12.Rows[0]["couvounum"].ToString();

                    }
                    else
                    {
                        foreach (DataRow dr2 in dt1.Rows)
                        {
                            acvounum = dr2["newvocnum"].ToString();

                        }
                    }
                    lblVoun.Text = dt12.Rows[0]["couvounum"].ToString();
                }



                /////////////////////////////////////////////////////////
                string vounarration1 = "";
                string vounarration2 = "";
                //for (int i = 0; i < dt1.Rows.Count; i++)
                //{
                //    vounarration1 = dt1.Rows[0]["narr"].ToString();

                //    vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                //    vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);

                //}


                vounarration1 = this.txtNarration.Text;

                vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                string mAPROVDAT = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

                //Log Entry
                string tblPostedByid = "";
                string tblPostedtrmid = "";
                string tblPostedSession = "";
                string tblPosteddat = "01-Jan-1900";
                string PostedByid = (this.Request.QueryString["Type"] == "Acc") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["Type"] == "Acc") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["Type"] == "Acc") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["Type"] == "Acc") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["Type"] == "Acc") ? "" : userid;
                string Editdat = (this.Request.QueryString["Type"] == "Acc") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
                //string voutype = "Online Payment Voucher";
                string voutype = "";
                if (chkCrVou.Checked)
                {
                    try
                    {
                        voutype = (ASTUtility.Left(acvounum, 2) == "JV" ? "Journal Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "CT" ? "Contra Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "CD" ? "Cash Payment Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "BD" ? "Bank Payment Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "CC" ? "Cash Deposit Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher"))))));




                        string payto = this.txtPayto.Text;

                        string vouno = acvounum.Substring(0, 2);




                        if ((this.Request.QueryString["Type"] == "Acc"))
                        {


                            if ((vouno == "BD" || vouno == "CT") && cactcode.Substring(0, 4) != "1901")
                            {

                                if (chequeno == "")
                                    ;
                                else
                                {
                                    chqdateTA =Convert.ToDateTime(dt1.Rows[0]["chqdate"]).ToString("dd-MMM-yyyy");

                                    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHEQUENOCHECK", chequeno, "", "", "", "", "", "", "", "");
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('This Cheque no is already exist.');", true);
                                        return;

                                    }

                                }




                            }
                        }
                        


                        //-----------Update Transaction B Table-----------------//
                        bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, voudat, chequeno, "", vounarration1,
                                        vounarration2, voutype, vtcode, "EDIT", PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, payto, slnum, chqdateTA, "", "", "");
                        if (!resultb)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                           
                            return;
                        }
                        //-----------Update Transaction A Table-----------------//
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            slnum = dt1.Rows[i]["slnum"].ToString();
                            actcode = dt1.Rows[i]["actcode"].ToString();
                            rescode = dt1.Rows[i]["rescode"].ToString();
                            string spclcode = "000000000000";
                            string trnqty = "0.00";
                            string trnamt = Convert.ToDouble(dt1.Rows[i]["amount"]).ToString();
                            string trnremarks = "";// dt1.Rows[i]["refno"].ToString();
                            string recndt = "01-Jan-1900";
                            string rpcode = "";
                            string billno = dt1.Rows[i]["billno"].ToString();



                            bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, actcode, rescode, cactcode,
                                           voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, "", "");
                            if (!resulta)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                                return;
                            }
                            bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATEPROAPP", slnum, actcode, rescode, acvounum, "", "", "", "", "", "", "", "", "", "", "");

                        }



                        if ((ASTUtility.Left(acvounum, 2) == "BD") || (ASTUtility.Left(acvounum, 2) == "CT"))
                        {
                            bool resultd = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATECHQLIST", cactcode, chequeno, acvounum, "", "",
                                           "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
                    }
                }
                ////////////////////////////////////////////PDC----------------------------------------------------------------------------
                else
                {
                    try
                    {





                        string vouno = acvounum.Substring(0, 2);




                        if ((this.Request.QueryString["Type"] == "Acc"))
                        {




                            if (chequeno == "")
                                ;
                            else
                            {
                                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHEQUENOCHECK", chequeno, "", "", "", "", "", "", "", "");
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('This Cheque no is already exist.');", true);
                                    return;

                                }

                            }




                        }



                        voutype = "Payment Voucher";
                        //-----------Update Payment B Table-----------------//
                        bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INOFUPOLACPMNTB", acvounum, voudat, vounarration1,
                                        vounarration2, voutype, "99", "EDIT", PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "", "");
                        if (!resultb)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                        //-----------Update Online Payment  A Table-----------------//



                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            slnum = dt1.Rows[i]["slnum"].ToString();
                            actcode = dt1.Rows[i]["actcode"].ToString();
                            rescode = dt1.Rows[i]["rescode"].ToString();
                            //chequeno = dt1.Rows[i]["chequeno"].ToString();

                            string chequedate = Convert.ToDateTime(dt1.Rows[i]["chqdate"]).ToString("dd-MMM-yyyy");
                            string Dramt = Convert.ToDouble(dt1.Rows[i]["amount"]).ToString();
                            string trnremarks = dt1.Rows[i]["refno"].ToString();// +dt.Rows[i]["remarks"].ToString();
                            string payto = dt1.Rows[i]["payto"].ToString();
                            string billno = dt1.Rows[i]["billno"].ToString();


                            bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INOFUPOLACPMNTA", acvounum, actcode, rescode, chequeno, cactcode,
                                           voudat, Dramt, chequedate, trnremarks, "99", payto, slnum, "00000000000000", billno, "");
                            if (!resulta)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                                return;
                            }

                            bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATEPROAPP", slnum, actcode, rescode, acvounum, "", "", "", "", "", "", "", "", "", "", "");




                        }




                        bool resultd = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATECHQLIST", cactcode, chequeno, acvounum, "", "",
                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");


                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
                    }

                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    string slnum1 = dt.Rows[i]["slnum"].ToString();
                    string chequeno1 = dt.Rows[i]["chequeno"].ToString();

                    if (slnum == slnum1)
                    {


                        dt.Rows[i]["newvocnum"] = acvounum.Substring(0, 2) + acvounum.Substring(6, 2) + "-" + acvounum.Substring(8);
                        //dt1.Rows[i]["recndt"] = recondat;

                    }

                }
                Session["tbChqSign"] = dt;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                //this.btnUpdate.Visible = false;

                ((LinkButton)this.Master.FindControl("lnkPrint")).Focus();
                string eventdesc = "Voucher: " + " Dated: " + this.txtdate.Text.Trim();
                string eventdesc2 = "";//this.txtNarration.Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, eventdesc2);

                this.Data_Bind();





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
        protected void ddlcheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlcheque.Items.Count == 0)
                return;
            this.txtRefNum.Text = this.ddlcheque.SelectedItem.Text;
        }
    }
}