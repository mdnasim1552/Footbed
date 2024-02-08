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
using SPERDLC;


namespace SPEWEB.F_15_DPayReg
{
    public partial class AccOnlinePaymntold : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //string date = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtValDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtpaymentdate.Text = System.DateTime.Today.AddDays(7).ToString("dd.MM.yyyy");
                //this.txtpaymentdate.Text = 
                this.GetProjectName();
                this.GetPartyName();
                this.GetBillNature();
                this.GetRescode();
                this.TableCreate();
                this.GetBillNo();

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();

            string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            string pactode = "%";
            if (BillList.Length > 0)
            {
                pactode = ((((DataTable)ViewState["BillAmt"]).Select("valfield='" + BillList + "'"))[0]["pactcode"]).ToString();
            }
            string txtSProject = "%" + this.txtsrchProject.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETPROJECTNAME", txtSProject, pactode, "", "", "", "", "", "", "");

            ViewState["HeadAcc1"] = ds1.Tables[0];
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();

            //----Show Resource code and Specification Code------------// 

            DataTable dt01 = ds1.Tables[0];
            string search1 = this.ddlProject.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;


            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.lblDetHead.Visible = true;
                this.txtsrchRes.Visible = true;
                this.ibtnRes.Visible = true;
                this.ddlRescode.Visible = true;

                //this.lblqty.Visible = true;
                //this.txtqty.Visible = true;
                //this.lblrate.Visible = true;
                //this.txtrate.Visible = true;
                //this.txtqty.Text = "";
                //this.txtrate.Text = "";
                string actcode = this.ddlProject.SelectedValue.ToString().Substring(0, 2);
                if (actcode == "18" || actcode == "24" || actcode == "25" || actcode == "25" || actcode == "18" || this.ddlRescode.Items.Count == 0)
                    this.GetRescode();
            }
            else
            {
                this.lblDetHead.Visible = false;
                this.txtsrchRes.Visible = false;
                this.ibtnRes.Visible = false;
                this.ddlRescode.Visible = false;

                //this.lblqty.Visible = false;
                //this.txtqty.Visible = false;
                //this.lblrate.Visible = false;
                //this.txtrate.Visible = false;
                //this.txtqty.Text = "";
                //this.txtrate.Text = "";

                this.ddlRescode.Items.Clear();


            }
            //---------------------------------------------//
            this.txtsrchProject.Text = "";




        }
        private void GetPartyName()
        {

            string comcod = this.GetCompCode();
            string SearchParty = "%" + this.txtSrhParty.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETPARTYNAME", SearchParty, "", "", "", "", "", "", "", "");
            this.ddlPartyName.DataTextField = "prdesc";
            this.ddlPartyName.DataValueField = "prcode";
            this.ddlPartyName.DataSource = ds1.Tables[0];
            this.ddlPartyName.DataBind();
            ds1.Dispose();
        }
        private void GetBillNo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = this.ddlProject.SelectedValue.ToString();
                string supcode = this.ddlRescode.SelectedValue.ToString();
                string ttsrch = "%" + this.txtsrchBillno.Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNO", ttsrch, "", "", "", "", "", "", "", "");
                this.ddlBillList.DataSource = ds1.Tables[0];
                this.ddlBillList.DataTextField = "textfield";
                this.ddlBillList.DataValueField = "valfield";
                this.ddlBillList.DataBind();

                ViewState["BillAmt"] = ds1.Tables[0];
            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }

        }
        private void GetBillNature()
        {
            string comcod = this.GetCompCode();
            string srchBillnature = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNATURE", srchBillnature, "", "", "", "", "", "", "", "");
            this.ddlBillNature.DataTextField = "rpdesc";
            this.ddlBillNature.DataValueField = "rpcode";
            this.ddlBillNature.DataSource = ds1.Tables[0];
            this.ddlBillNature.DataBind();
            ds1.Dispose();
        }
        private void GetRescode()
        {
            string comcod = this.GetCompCode();
            string srchRes = "%" + this.txtsrchRes.Text + "%";
            string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            string rescode = "%";
            if (BillList.Length > 0)
            {
                rescode = ((((DataTable)ViewState["BillAmt"]).Select("valfield='" + BillList + "'"))[0]["sircode"]).ToString() + "%";
            }
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETRESCODE", srchRes, rescode, "", "", "", "", "", "", "");
            this.ddlRescode.DataTextField = "resdesc";
            this.ddlRescode.DataValueField = "rescode";
            this.ddlRescode.DataSource = ds1.Tables[0];
            this.ddlRescode.DataBind();
            ds1.Dispose();
        }


        private void TableCreate()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("slnum", Type.GetType("System.String"));
            tblt01.Columns.Add("rcvdate", Type.GetType("System.String"));
            tblt01.Columns.Add("billnature", Type.GetType("System.String"));
            tblt01.Columns.Add("billndesc", Type.GetType("System.String"));
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rescode", Type.GetType("System.String"));
            tblt01.Columns.Add("resdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("paycode", Type.GetType("System.String"));
            tblt01.Columns.Add("paydesc", Type.GetType("System.String"));
            tblt01.Columns.Add("refno", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("billno1", Type.GetType("System.String"));
            tblt01.Columns.Add("valdate", Type.GetType("System.String"));
            tblt01.Columns.Add("apppaydate", Type.GetType("System.String"));
            tblt01.Columns.Add("amt", Type.GetType("System.Double"));
            tblt01.Columns.Add("advamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("netamt", Type.GetType("System.Double"));
            ViewState["tblpayment"] = tblt01;


        }
        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
            this.ddlProject.Focus();

        }
        protected void ibtnFindParty_Click(object sender, ImageClickEventArgs e)
        {
            this.GetPartyName();
            this.ddlPartyName.Focus();
        }
        protected void ibtnnature_Click(object sender, ImageClickEventArgs e)
        {
            this.GetBillNature();
            this.ddlBillNature.Focus();
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lblAddToTable_Click(object sender, EventArgs e)
        {

        }

        protected void txtReceiveDate_TextChanged(object sender, EventArgs e)
        {
            this.txtReceiveDate.Text = ASTUtility.DateInVal(this.txtReceiveDate.Text);
            //this.txtRefno.Focus();
        }
        protected void txtpaymentdate_TextChanged(object sender, EventArgs e)
        {
            this.txtpaymentdate.Text = ASTUtility.DateInVal(this.txtpaymentdate.Text);
            this.lbtnAddTable.Focus();
        }
        protected void lbtnAddTable_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            if (this.txtRefno.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
                return;
            }
            if (this.txtBillAmount.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Bill Amount');", true);
                return;
            }
            string refno = this.txtRefno.Text.Trim();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DUPREFNO", refno, "", "", "", "", "", "", "", "");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This reference number all ready exist');", true);
                return;
                //this.lmsg.Text = "This reference number all ready exist" + refno;
            }
            DataTable dt = (DataTable)ViewState["tblpayment"];
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr2 = dt.Select("refno = '" + refno + "'");
                if (dr2.Length != 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This reference number all ready exist');", true);
                    return;
                }

            }


            string slnum = (dt.Rows.Count == 0) ? this.GetSlNum() : this.IncrmentSlNum();
            this.lblslnum.Text = slnum;
            DataRow dr1 = dt.NewRow();
            dr1["slnum"] = slnum;
            dr1["rcvdate"] = this.txtReceiveDate.Text;
            dr1["billnature"] = this.ddlBillNature.SelectedValue.ToString();
            dr1["billndesc"] = this.ddlBillNature.SelectedItem.Text.Trim();
            dr1["actcode"] = this.ddlProject.SelectedValue.ToString();
            dr1["actdesc"] = this.ddlProject.SelectedItem.Text.Trim();
            dr1["rescode"] = (this.ddlRescode.SelectedValue.ToString().Length < 12) ? "000000000000" : this.ddlRescode.SelectedValue.ToString();
            dr1["resdesc"] = (this.ddlRescode.SelectedValue.ToString().Length < 12) ? "" : this.ddlRescode.SelectedItem.Text.Trim();
            dr1["paycode"] = this.ddlPartyName.SelectedValue.ToString();
            dr1["paydesc"] = this.ddlPartyName.SelectedItem.Text.Trim();
            dr1["refno"] = this.txtRefno.Text.Trim();
            dr1["billno"] = this.ddlBillList.SelectedValue.ToString();
            dr1["billno1"] = this.ddlBillList.SelectedValue.ToString();
            dr1["apppaydate"] = this.txtpaymentdate.Text;
            dr1["valdate"] = this.txtValDate.Text;
            dr1["amt"] = ASTUtility.StrPosOrNagative(this.txtBillAmount.Text.Trim());
            dr1["advamt"] = ASTUtility.StrPosOrNagative(this.txtAdvAmt.Text.Trim());
            dr1["netamt"] = (ASTUtility.StrPosOrNagative(this.txtBillAmount.Text.Trim())) - (ASTUtility.StrPosOrNagative(this.txtAdvAmt.Text.Trim()));
            dt.Rows.Add(dr1);
            ViewState["tblpayment"] = dt;
            this.Data_Bind();
            this.txtReceiveDate.Focus();


            this.txtRefno.Text = "";
            this.txtBillAmount.Text = "";
            this.txtsrchRes.Text = "";
            this.txtsrchBillno.Text = "";
            this.txtSrhParty.Text = "";
            this.txtsrchBill.Text = "";
            this.txtAdvAmt.Text = "";
            this.GetBillNo();
        }

        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            this.gvPayment.DataSource = tbl1;
            this.gvPayment.DataBind();

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amt)", "")) ? 0.00 : tbl1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");

            }



        }

        private string GetSlNum()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETSLNUM", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["slnum"].ToString();
        }


        private string IncrmentSlNum()
        {
            //string isunum="000000000";
            string slnum = (Convert.ToInt32(this.lblslnum.Text.Trim()) + 1).ToString();
            return (ASTUtility.Right(("000000000" + slnum), 9));



        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lmsg.Text = "You have no permission";
                return;
            }
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;

                string slnum = this.GetSlNum();
                this.lblslnum.Text = slnum;
                foreach (DataRow dr in dt1.Rows)
                {
                    string rcvdate = ASTUtility.DateFormat(dr["rcvdate"].ToString());
                    string refno = dr["refno"].ToString().Trim();
                    string actcode = dr["actcode"].ToString();
                    string rescode = dr["rescode"].ToString();
                    string paycode = dr["paycode"].ToString();
                    string billno = dr["billno"].ToString();
                    string billnature = dr["billnature"].ToString();
                    double amt = Convert.ToDouble("0" + dr["amt"].ToString());
                    double advamt = Convert.ToDouble("0" + dr["advamt"].ToString());
                    string valdate = ASTUtility.DateFormat(dr["valdate"].ToString());
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());
                    if (amt > 0)
                    {
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INSERTORUPONLINEPAY", slnum, rcvdate, refno, actcode, paycode, billnature, amt.ToString(),
                                                                   apppaydate, rescode, billno, advamt.ToString(), valdate, "", "", "");

                        if (result == false)
                        {
                            this.lmsg.Text = "Updated Failed";
                            return;
                        }
                        else
                        {
                            this.lmsg.Text = "Updated Successfully";
                        }
                    }
                    else
                    {
                        this.lmsg.Text = "Bill Amount Empty";
                    }

                    slnum = this.IncrmentSlNum();
                    this.lblslnum.Text = slnum;
                }


                ((LinkButton)this.gvPayment.FooterRow.FindControl("lbtnUpdate")).Enabled = false;
                this.lbtnRefresh.Focus();

                //Log Report





            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Errp:" + ex.Message;
            }

        }


        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                tbl1.Rows[i]["refno"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvref")).Text.Trim();
                tbl1.Rows[i]["amt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvbillamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["advamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAdvamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["netamt"] = Convert.ToDouble("0" + ((Label)this.gvPayment.Rows[i].FindControl("lblgvNetamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["valdate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvValdate")).Text.Trim();
                tbl1.Rows[i]["apppaydate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpaymentdate")).Text.Trim();

            }
            ViewState["tblpayment"] = tbl1;

        }
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblpayment");
            this.TableCreate();
            this.gvPayment.DataSource = null;
            this.gvPayment.DataBind();
            this.lmsg.Text = "";

        }
        protected void ibtnRes_Click(object sender, ImageClickEventArgs e)
        {
            this.GetRescode();
        }
        protected void ibtnBillNo_Click(object sender, ImageClickEventArgs e)
        {
            this.GetBillNo();
        }

        protected void ddlBillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            this.txtBillAmount.Text = Convert.ToDouble((((DataTable)ViewState["BillAmt"]).Select("valfield='" + BillList + "'"))[0]["amt"]).ToString("#,##0;(#,##0); ");
            this.txtRefno.Text = ((((DataTable)ViewState["BillAmt"]).Select("valfield='" + BillList + "'"))[0]["billref"]).ToString();
            this.GetProjectName();
            this.GetRescode();
        }
        protected void txtValDate_TextChanged(object sender, EventArgs e)
        {
            this.txtValDate.Text = ASTUtility.DateInVal(this.txtValDate.Text);
            this.txtRefno.Focus();
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProject.BackColor = System.Drawing.Color.Pink;
            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = this.ddlProject.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.lblDetHead.Visible = true;
                this.txtsrchRes.Visible = true;
                this.ibtnRes.Visible = true;
                this.ddlRescode.Visible = true;
                this.GetRescode();






            }
            else
            {
                this.lblDetHead.Visible = false;
                this.txtsrchRes.Visible = false;
                this.ibtnRes.Visible = false;
                this.ddlRescode.Visible = false;
                this.ddlRescode.Items.Clear();

            }
        }
    }
}