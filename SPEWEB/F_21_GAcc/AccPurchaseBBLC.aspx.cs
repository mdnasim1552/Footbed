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


namespace SPEWEB.F_21_GAcc
{
    public partial class AccPurchaseBBLC : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.LoadMrrCombo();
                CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
            }

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
            tblt01.Columns.Add("trncrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnratefc", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("mrrno", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
        }


        private void LoadMrrCombo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETMRRBBLC", "", "", "", "", "", "", "", "", "");
            this.ddlMRRList.Items.Clear();
            this.ddlMRRList.DataTextField = "textfield";
            this.ddlMRRList.DataValueField = "mrrno";
            this.ddlMRRList.DataSource = ds1.Tables[0];
            this.ddlMRRList.DataBind();
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblt01"];
            if (dt2.Rows.Count == 0)
                return;

            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                        0.00 : dt2.Compute("Sum(trndram)", ""))).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))).ToString("#,##0.00;(#,##0.00); - ");

        }

        protected void ibtnvounu_Click(object sender, ImageClickEventArgs e)
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
            this.txtLastVou.Text = pvno1.Substring(0, 2) + pvno1.Substring(6, 2) + "-" + pvno1.Substring(8, 6);

        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtcurrentvou.Text.Trim() == "")
            {
                this.lblmsg.Text = "Please insert Voucher no !!!";
                return;
            }
            //string cactcode = this.ddlConAccHead.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
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
                        vounarration1, vounarration2, voutype, vtcode, edit, "", "", "", "", "", "");
                if (!resultb)
                {
                    this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Transaction A Table-----------------//
                string Mrrno2 = "XXXXXXXXXXXXXX";

                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    string Mrrno = ((Label)this.dgv2.Rows[i].FindControl("lblMrrno")).Text.Trim();

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                    if (Mrrno2 != Mrrno)
                    {
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEBLCRECV01",
                                Mrrno, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                        Mrrno2 = Mrrno;
                    }
                }
                this.lblmsg.Text = "Update Successfully.";

                //this.lnkFinalUpdate.Enabled = false;
                //this.txtcurrentvou.Enabled = false;
                //this.txtCurrntlast6.Enabled = false;

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
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

                //ReportDocument rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //Session["Report1"] = rptinfo;
                //this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }
        }

        protected void lbtnSelectMRR_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mrrno = this.ddlMRRList.SelectedValue.ToString();// ((LinkButton)dgv1.Rows[Rowid].FindControl("lnkgvbill")).Text;
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PURCHASESBBLC", mrrno,
                          "", "", "", "", "", "", "", "");
            DataTable dt1 = HiddenSameData(ds1.Tables[0]);
            DataTable tblt01 = (DataTable)Session["tblt01"];


            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string mrrno1 = dt1.Rows[i]["mrrno"].ToString();
                string actcode = dt1.Rows[i]["actcode"].ToString();
                string rescode = dt1.Rows[i]["rescode"].ToString();
                DataRow[] dr = tblt01.Select("mrrno='" + mrrno1 + "' and  actcode='" + actcode + "' and subcode='" + rescode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dt1.Rows[i]["actcode"].ToString();
                    dr1["subcode"] = dt1.Rows[i]["rescode"].ToString();
                    dr1["spclcode"] = dt1.Rows[i]["spcode"].ToString();
                    dr1["actdesc"] = dt1.Rows[i]["actdesc"].ToString(); // dgAccCode + "-" + dgAccDesc;
                    dr1["subdesc"] = dt1.Rows[i]["resdesc"].ToString(); // dgResCode + "-" + dgResDesc;
                    dr1["spcldesc"] = "";
                    dr1["trnqty"] = Convert.ToDouble(dt1.Rows[i]["qty"]);
                    dr1["trncrate"] = Convert.ToDouble(dt1.Rows[i]["conrat"]);
                    dr1["trnratefc"] = Convert.ToDouble(dt1.Rows[i]["ratefc"]);
                    dr1["trnrate"] = Convert.ToDouble(dt1.Rows[i]["rate"]);
                    dr1["trndram"] = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                    dr1["trncram"] = Convert.ToDouble(dt1.Rows[i]["Cr"]);
                    dr1["trnrmrk"] = dt1.Rows[i]["mrrno"].ToString();
                    dr1["mrrno"] = dt1.Rows[i]["mrrno"].ToString();
                    tblt01.Rows.Add(dr1);
                }
            }
            if (tblt01.Rows.Count == 0)
                return;
            Session["tblt01"] = tblt01;
            dgv2.DataSource = tblt01;
            dgv2.DataBind();
            calculation();
            //this.ibtnvounu.Visible = true;
            this.lnkFinalUpdate.Visible = true;
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
                this.ibtnvounu.Visible = true;
                this.lbllstVouno.Visible = true;
                this.txtLastVou.Visible = true;
                this.lblcurVounum.Visible = true;
                this.txtcurrentvou.Visible = true;
                this.txtCurrntlast6.Visible = true;
                this.lblMRRList.Visible = true;
                this.ddlMRRList.Visible = true;
                this.lbtnSelectMRR.Visible = true;
                this.lblRefNum.Visible = true;
                this.txtRefNum.Visible = true;
                this.lblSrInfo.Visible = true;
                this.txtSrinfo.Visible = true;
                this.lblNaration.Visible = true;
                this.txtNarration.Visible = true;
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ibtnvounu.Visible = false;
                this.lbllstVouno.Visible = false;
                this.txtLastVou.Visible = false;
                this.lblcurVounum.Visible = false;
                this.txtcurrentvou.Visible = false;
                this.txtCurrntlast6.Visible = false;
                this.lblMRRList.Visible = false;
                this.ddlMRRList.Visible = false;
                this.lbtnSelectMRR.Visible = false;
                this.lblRefNum.Visible = false;
                this.txtRefNum.Visible = false;
                this.lblSrInfo.Visible = false;
                this.txtSrinfo.Visible = false;
                this.lblNaration.Visible = false;
                this.txtNarration.Visible = false;
                this.lnkFinalUpdate.Visible = false;
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                this.LoadMrrCombo();
                this.lblmsg.Text = "";
                this.txtRefNum.Text = "";
                this.txtSrinfo.Text = "";
                this.txtNarration.Text = "";
                DataTable dt = (DataTable)Session["tblt01"];
                dt.Rows.Clear();
                Session["tblt01"] = dt;
            }

        }
    }
}