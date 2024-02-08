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

namespace SPEWEB.F_81_Hrm.F_90_PF
{
    public partial class GeneralAccounts : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            lnkFinalUpdate.Attributes.Add("onClick",
           " javascript:return confirm('You sure you want to Save the record?');");
            if (this.ddlConAccHead.Items.Count > 0)
                return;

            this.LoadAcccombo();
            this.TableCreate();

            this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy (ddd)");

            ((Label)this.Master.FindControl("lblTitle")).Text = Request.QueryString["tname"].ToString();
            string vcode = Request.QueryString["tcode"];


            if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment"))
            {
                this.dgv1.Columns[10].Visible = false;
                this.lblCramt.Visible = false;
                this.txtCrAmt.Visible = false;
            }
            else if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit"))
            {
                this.dgv1.Columns[9].Visible = false;
                this.lblDramt.Visible = false;
                this.txtDrAmt.Visible = false;
            }
            else
            {
                this.lblcontrolAccHead.Visible = false;
                this.ddlConAccHead.Visible = false;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetPriviousVoucher()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            string vtcode = this.Request.QueryString["tcode"].ToString().Trim();
            string date = this.txtEntryDate.Text.Substring(0, 11);

            string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "C" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
            //string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "T" : "C")));
            // string VNo2 = (VNo1 == "J" ? "V" : (this.lblGeneralAcc.Text.Contains("Payment") ? "D" : "C"));
            string VNo3 = Convert.ToString(VNo1 + VNo2);
            string vounum = "%%";
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRIVOUSVOUCHER", VNo3, vtcode, date, vounum, "", "", "", "", "");

            this.ddlPrivousVou.DataSource = ds5.Tables[0];
            this.ddlPrivousVou.DataTextField = "vounum1";
            this.ddlPrivousVou.DataValueField = "vounum";
            this.ddlPrivousVou.DataBind();



        }
        private void TableCreate()
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
            Session["tblt01"] = tblt01;
            DataTable tblt02 = new DataTable();
            tblt02.Columns.Add("actcode", Type.GetType("System.String"));
            tblt02.Columns.Add("subcode", Type.GetType("System.String"));
            tblt02.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt02.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt02.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt02.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt02.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt02.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt02.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt02.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt02.Columns.Add("trnrmrk", Type.GetType("System.String"));
            Session["tblt02"] = tblt02;
            //actcode,subcode,spclcode,actdesc,subdesc,spcldesc,trnqty,trnrate,trndram,trncram,trnrmrk
        }
        private void LoadAcccombo()
        {
            // try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD", "", "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
                //this.GetPriviousVoucher();
            }
            // catch (Exception ex)
            {
                //     this.lblmsg.Text = "Error:" + ex.Message;
            }

        }
        protected void lnkAcccode_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = "%" + this.txtserceacc.Text + "%";
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", filter, accconhead, "", "", "", "", "", "", "");
            DataTable dt2 = ds2.Tables[0];
            Session["HeadAcc1"] = ds2.Tables[0];
            this.ddlacccode.DataSource = dt2;
            this.ddlacccode.DataTextField = "actdesc1";
            this.ddlacccode.DataValueField = "actcode";
            this.ddlacccode.DataBind();
            //----Show Resource code and Specification Code------------// 

            DataTable dt01 = (DataTable)Session["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;
                this.txtSearchSpeci.Visible = true;
                this.lnkSpecification.Visible = true;
                this.ddlSpclinf.Visible = true;
                this.lblqty.Visible = true;
                this.txtqty.Visible = true;
                this.lblrate.Visible = true;
                this.txtrate.Visible = true;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
            }
            else
            {
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;
                this.lblqty.Visible = false;
                this.txtqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtrate.Visible = false;
                this.ddlSpclinf.Items.Clear();
                this.ddlresuorcecode.Items.Clear();
                this.txtqty.Text = "";
                this.txtrate.Text = "";
            }
            //---------------------------------------------//
            this.txtserceacc.Text = "";
        }
        protected void lnkRescode_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter1 = this.txtserchReCode.Text + "%";
            DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", filter1, "", "", "", "", "", "", "", "");
            DataTable dt3 = ds3.Tables[0];
            this.ddlresuorcecode.DataSource = dt3;
            this.ddlresuorcecode.DataTextField = "resdesc1";
            this.ddlresuorcecode.DataValueField = "rescode";
            this.ddlresuorcecode.DataBind();
            this.txtserchReCode.Text = "";
        }
        protected void lnkSpecification_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rescode = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            string filter2 = "%" + this.txtSearchSpeci.Text + "%";
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSPCILINFCODE", filter2, rescode, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            this.ddlSpclinf.DataSource = dt4;
            this.ddlSpclinf.DataTextField = "spdesc1";
            this.ddlSpclinf.DataValueField = "spcod";
            this.ddlSpclinf.DataBind();
            this.txtSearchSpeci.Text = "";
        }
        protected void Calculate_Rate()
        {
            double Qty1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
            if (Qty1 == 0)
                return;

            double DrAmt2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
            double CrAmt2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtCrAmt.Text.Trim()));
            this.txtrate.Text = ((DrAmt2 + CrAmt2) / Qty1).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void ddlresuorcecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlresuorcecode.BackColor = System.Drawing.Color.Pink;
        }
        protected void ddlSpclinf_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSpclinf.BackColor = System.Drawing.Color.Pink;
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkOk.Text == "Ok")
            {
                if (this.ddlPrivousVou.Items.Count > 0)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");

                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);


                    this.dgv1.DataSource = dt;


                    if (dt.Rows.Count == 0)
                        return;

                    this.dgv1.DataBind();
                    this.CalculatrGridTotal();
                    //-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];

                    if (vounum.Substring(0, 2).ToString() != "JV")
                    {
                        this.ddlConAccHead.SelectedValue = dtedit.Rows[0]["cactcode"].ToString();
                    }
                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy (ddd)");
                    this.txtRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
                    this.txtSrinfo.Text = dtedit.Rows[0]["srinfo"].ToString();
                    this.txtNarration.Text = dtedit.Rows[0]["venar"].ToString();
                    this.ddlConAccHead.Enabled = false;
                    this.txtEntryDate.Enabled = false;
                    //-------------------------------------------------//
                    this.Panel4.Visible = true;
                    this.lbllstVouno.Visible = false;
                    this.txtLastVou.Visible = false;
                    this.lblcurVounum.Text = "Edit Voucher No.";
                    string cvno1 = this.ddlPrivousVou.SelectedValue.ToString().Substring(0, 8);
                    this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                    this.txtCurrntlast6.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(8);
                    //this.txtcurrentvou.BackColor = System.Drawing.Color.Aqua;
                    //this.txtCurrntlast6.BackColor = System.Drawing.Color.Aqua;
                    this.txtCurrntlast6.Enabled = false;
                }
                else
                {
                    this.ddlConAccHead.Enabled = true;
                    this.txtEntryDate.Enabled = true;
                    this.txtCurrntlast6.Enabled = true;
                    this.ibtnvounu.Visible = true;
                }
                this.lnkPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.Panel2.Visible = true;
                this.lnkFinalUpdate.Enabled = true;
                this.lnkOk.Text = "New";
                //this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
                //this.txtEntryDate.BackColor = System.Drawing.Color.Aqua;
            }
            else
            {
                this.lnkOk.Text = "Ok";
                this.txtCurrntlast6.Enabled = false;
                this.ibtnvounu.Visible = false;
                this.Panel2.Visible = false;
                this.Panel4.Visible = false;
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;
                this.lblqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtqty.Visible = false;
                this.txtrate.Visible = false;
                dgv1.DataSource = null;
                dgv1.DataBind();
                this.ddlPrivousVou.Items.Clear();
                //this.ibtnvounu.Visible = true; 
                this.lbllstVouno.Visible = true;
                this.txtLastVou.Visible = true;
                this.lblcurVounum.Text = "Current Voucher";
                this.txtcurrentvou.Text = "";
                this.txtCurrntlast6.Text = "";
                //this.txtCurrntlast6.Enabled = true;
                this.lnkPrivVou.Visible = true;
                this.ddlPrivousVou.Visible = true;
                this.ddlConAccHead.Enabled = true;
                this.txtEntryDate.Enabled = true;

                this.ddlacccode.BackColor = System.Drawing.Color.White;
                this.ddlresuorcecode.BackColor = System.Drawing.Color.White;
                this.ddlSpclinf.BackColor = System.Drawing.Color.White;
                this.Refrsh();

            }
        }
        private void Refrsh()
        {
            this.lblmsg.Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.ddlacccode.Items.Clear();
            this.ddlSpclinf.Items.Clear();
            this.ddlresuorcecode.Items.Clear();
            this.txtserceacc.Text = "";
            this.txtserchReCode.Text = "";
            this.txtSearchSpeci.Text = "";
            this.txtDrAmt.Text = "";
            this.txtCrAmt.Text = "";
            this.txtqty.Text = "";
            this.txtrate.Text = "";
            this.txtremarks.Text = "";
            this.txtSrinfo.Text = "";
            this.txtRefNum.Text = "";
            this.txtNarration.Text = "";
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


        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
            //this.GetPriviousVoucher();
        }

        protected void lnkOk0_Click(object sender, EventArgs e)
        {
            this.Calculate_Rate();
            try
            {
                //----------------Add Data Into Grid--------------------------//
                this.Panel4.Visible = true;
                string AccCode = this.ddlacccode.SelectedValue.ToString();
                string ResCode = this.ddlresuorcecode.SelectedValue.ToString();
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);
                string SpclCode = this.ddlSpclinf.SelectedValue.ToString();
                SpclCode = (SpclCode.Length < 12 ? "000000000000" : SpclCode);
                string AccDesc = this.ddlacccode.SelectedItem.Text.Trim();
                string ResDesc = (ResCode == "000000000000" ? "" : this.ddlresuorcecode.SelectedItem.Text.Trim());
                string SpclDesc = (SpclCode == "000000000000" ? "" : this.ddlSpclinf.SelectedItem.Text.Trim().Substring(13));
                double TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
                double Trnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
                double TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
                double TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtCrAmt.Text.Trim()));
                string TrnRemarks = this.txtremarks.Text.Trim();
                DataTable tblt01 = (DataTable)Session["tblt01"];
                DataTable tblt02 = (DataTable)Session["tblt02"];
                DataTable tblt03 = new DataTable();
                tblt01.Rows.Clear();
                tblt02.Rows.Clear();
                tblt03.Rows.Clear();

                for (int i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    string dgAccCode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string dgResCode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();

                    //-----------If Repetation ---------------------------------------------------------//
                    if (dgAccCode + dgResCode == AccCode + ResCode)
                    {
                        ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text = SpclCode;
                        ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text = SpclDesc;
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = TrnQty.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = Trnrate.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text = TrnRemarks;
                        //this.CalculatrGridTotal();
                        //return;
                    }
                    else
                    {
                        //--------------------------------------------------------------------------------//
                        string dgSpclCode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                        string dgAccDesc = ((Label)this.dgv1.Rows[i].FindControl("lblAccdesc")).Text.Trim();
                        string dgResDesc = ((Label)this.dgv1.Rows[i].FindControl("lblResdesc")).Text.Trim();
                        string dgSpclDesc = ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text.Trim();
                        double dgTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                        double dgTrnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                        double dgTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                        double dgTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                        string dgTrnRemarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                        //actcode subcode actdesc subdesc trnqty trnrate trndram trncram trnrmrk 
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
                        tblt01.Rows.Add(dr1);
                    }
                }
                DataRow dr2 = tblt01.NewRow();
                dr2["actcode"] = AccCode;
                dr2["subcode"] = ResCode;
                dr2["spclcode"] = SpclCode;
                dr2["actdesc"] = AccDesc;
                dr2["subdesc"] = ResDesc;
                dr2["spcldesc"] = SpclDesc;
                dr2["trnqty"] = TrnQty;
                dr2["trnrate"] = Trnrate;
                dr2["trndram"] = TrnDrAmt;
                dr2["trncram"] = TrnCrAmt;
                dr2["trnrmrk"] = TrnRemarks;
                tblt01.Rows.Add(dr2);
                //--------------** Remove Duplicate Value **----------------------------//
                //--** Only Actdesc remove actcod not remove from grid **---------------// 
                DataView dv1 = tblt01.DefaultView;
                dv1.Sort = "actcode";
                tblt03 = dv1.ToTable();
                string AccDesc1 = null;
                for (int j = 0; j < tblt03.Rows.Count; j++)
                {
                    DataRow dr3 = tblt02.NewRow();
                    dr3["actcode"] = tblt03.Rows[j]["actcode"].ToString();
                    dr3["subcode"] = tblt03.Rows[j]["subcode"].ToString();
                    dr3["spclcode"] = tblt03.Rows[j]["spclcode"].ToString();
                    string tserch = tblt03.Rows[j]["actcode"].ToString();
                    if (tserch == AccDesc1 || tserch == "")
                    {
                        dr3["actdesc"] = "";
                    }
                    else
                    {
                        dr3["actdesc"] = tblt03.Rows[j]["actdesc"].ToString();
                        AccDesc1 = tblt03.Rows[j]["actcode"].ToString();
                    }

                    dr3["subdesc"] = tblt03.Rows[j]["subdesc"].ToString();
                    dr3["spcldesc"] = tblt03.Rows[j]["spcldesc"].ToString();
                    dr3["trnqty"] = Convert.ToDouble(tblt03.Rows[j]["trnqty"].ToString());
                    dr3["trnrate"] = Convert.ToDouble(tblt03.Rows[j]["trnrate"].ToString());
                    dr3["trndram"] = Convert.ToDouble(tblt03.Rows[j]["trndram"].ToString());
                    dr3["trncram"] = Convert.ToDouble(tblt03.Rows[j]["trncram"].ToString());
                    dr3["trnrmrk"] = tblt03.Rows[j]["trnrmrk"].ToString();
                    tblt02.Rows.Add(dr3);

                }
                //---------------------------------------------//
                dgv1.DataSource = tblt02;


                //dgv1.DataSource = tblt01;
                dgv1.DataBind();
                this.CalculatrGridTotal();
                //this.ddlacccode.BackColor = System.Drawing.Color.Beige;
                //this.ddlresuorcecode.BackColor = System.Drawing.Color.Beige;
                //this.ddlSpclinf.BackColor = System.Drawing.Color.Beige;
                this.txtDrAmt.Text = "";
                this.txtCrAmt.Text = "";
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                this.txtremarks.Text = "";
            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error :" + ex.Message;
            }

        }
        protected void CalculatrGridTotal()
        {
            double TQty = 0.00;
            double TRate = 0.00;
            double TDrAmt = 0.00;
            double TCrAmt = 0.00;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                double dg1TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                double dg1TrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double dg1TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dg1TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                TQty += dg1TrnQty;
                TRate += dg1TrnRate;
                TDrAmt += dg1TrnDrAmt;
                TCrAmt += dg1TrnCrAmt;
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = dg1TrnQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = dg1TrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = dg1TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = dg1TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
            }
            if (this.dgv1.Rows.Count > 0)
            {
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvQty")).Text = TQty.ToString("#,##0.00;(#,##0.00); - ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvRate")).Text = TRate.ToString("#,##0.00;(#,##0.00); - ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text = TDrAmt.ToString("#,##0.00;(#,##0.00); - ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text = TCrAmt.ToString("#,##0.00;(#,##0.00); - ");
            }
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.CalculatrGridTotal();
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            this.CalculatrGridTotal();
            if (this.txtcurrentvou.Text.Trim() != "")
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                //string cactcode = this.ddlConAccHead.SelectedValue.ToString();

                string voudat = this.txtEntryDate.Text.Substring(0, 11);
                // string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                string refnum = this.txtRefNum.Text.Trim();
                string srinfo = this.txtSrinfo.Text;
                string vounarration1 = this.txtNarration.Text.Trim();
                string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
                string voutype = (vouno == "JV" ? "Journal Voucher" :
                                 (vouno == "CD" ? "Cash Payment Voucher" :
                                 (vouno == "BD" ? "Bank Payment Voucher" :
                                 (vouno == "CC" ? "Cash Deposit Voucher" :
                                 (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
                string cactcode = (vouno == "JV" ? "000000000000" : this.ddlConAccHead.SelectedValue.ToString());
                string vtcode = Request.QueryString["tcode"];
                string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");
                string TgvDrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text;
                string TgvCrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text;
                if (vouno == "JV" && TgvDrAmt != TgvCrAmt)
                {
                    this.lblmsg.Text = "Dr. Amount not equals to Cr. Amount.";
                    return;
                }
                try
                {
                    //-----------Update Transaction B Table-----------------//
                    bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                                    vounarration2, voutype, vtcode, edit, "", "", "", "", "", "");
                    if (!resultb)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                    //-----------Update Transaction A Table-----------------//
                    for (int i = 0; i < dgv1.Rows.Count; i++)
                    {
                        string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                        string rescode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                        string spclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                        string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                        double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                        double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                        string trnamt = Convert.ToString(Dramt - Cramt);
                        string trnremarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, actcode, rescode, cactcode,
                                       voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                    }
                    this.lblmsg.Text = "Update Successfully.";
                    //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                    this.lnkFinalUpdate.Enabled = false;
                    this.GetPriviousVoucher();
                    this.txtcurrentvou.Text = "";
                    this.txtCurrntlast6.Text = "";

                }
                catch (Exception ex)
                {
                    this.lblmsg.Text = "Error:" + ex.Message;
                }
            }
            else
            {
                this.lblmsg.Text = "Please Get Vocher No.";
            }
        }
        protected void ddlacccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlacccode.BackColor = System.Drawing.Color.Pink;
            DataTable dt01 = (DataTable)Session["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;
                this.txtSearchSpeci.Visible = true;
                this.lnkSpecification.Visible = true;
                this.ddlSpclinf.Visible = true;
                this.lblqty.Visible = true;
                this.txtqty.Visible = true;
                this.lblrate.Visible = true;
                this.txtrate.Visible = true;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
            }
            else
            {
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;
                this.lblqty.Visible = false;
                this.txtqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtrate.Visible = false;
                this.ddlSpclinf.Items.Clear();
                this.ddlresuorcecode.Items.Clear();
                this.txtqty.Text = "";
                this.txtrate.Text = "";
            }
        }
        protected void ibtnvounu_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : "C"));
            string VNo3 = Convert.ToString(VNo1 + VNo2);
            string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
            this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
            this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);
            string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();
            this.txtLastVou.Text = pvno1.Substring(0, 2) + pvno1.Substring(6, 2) + "-" + pvno1.Substring(8, 6);

            //this.ddlPrivousVou.BackColor = System.Drawing.Color.Aqua;
        }


        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {



                case "4101":
                case "4315":
                    vouprint = "VocherPrint5";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }
        private void PrintVoucher()
        {
            try
            {
                if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                    this.lnkOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                //string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                if (dt.Rows.Count == 0)
                    return;
                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                DataTable dt1 = _ReportDataSet.Tables[1];
                string Vounum = dt1.Rows[0]["vounum"].ToString();
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                string refnum = dt1.Rows[0]["refnum"].ToString();
                string voutype = dt1.Rows[0]["voutyp"].ToString();
                string venar = dt1.Rows[0]["venar"].ToString();
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                string Type = this.CompanyPrintVou();
                //ReportDocument rptinfo = new ReportDocument();
                //if (Type == "VocherPrint")
                //{
                //    rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher();
                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher1();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text = "";
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;

                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher2();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum2.Text = "";
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //}
                //else if (Type == "VocherPrint3")
                //{
                //    rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher3();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text = " ";
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;

                //}
                //else if (Type == "VocherPrint5")
                //{
                //    //rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher5();
                //    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    //txtisunum1.Text = "" ;
                //    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    //txtPosteddate1.Text = "Entry Date: " + Posteddat;

                //}
                //else
                //{
                //    rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher4();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum3.Text = "";
                //    TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate3.Text = "Entry Date: " + Posteddat;
                //}


                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No.: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = "Voucher Date: " + voudat;
                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //rpttxtPartyName.Text = "";
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Narration: " + venar;

                ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


                ////string comcod = this.GetComeCode();
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                this.lblmsg.Visible = true;
                this.lblmsg.Text = "Error:" + ex.Message;
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintVoucher();
        }


        protected void lnkPrivVou_Click(object sender, EventArgs e)
        {
            this.GetPriviousVoucher();
        }
    }

}