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
using System.Collections.Generic;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccDepJournal : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        static string prevPage = String.Empty;
        UserService userSer = new UserService();


        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');"); DEPRECIATION Accounts

            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                ((Label)this.Master.FindControl("lblTitle")).Text = "Depreciation Voucher Update";

                this.lbldate.Text = this.Request.QueryString["Date2"].ToString();
                CreateTable();
                this.GetDepreciation();
                this.CommonButton();
                this.imgbtnAcc_Click(null, null);
            }

        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ////((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ////((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;

            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
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
            tblt01.Columns.Add("Dr", Type.GetType("System.Double"));
            tblt01.Columns.Add("Cr", Type.GetType("System.Double"));
            ViewState["tblt01"] = tblt01;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler();
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnRecal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).Checked += new EventHandler(chkBoxN_Click);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //Response.Redirect(prevPage);
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(Dr)", "")) ?
                         0.00 : dt2.Compute("Sum(Dr)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(Cr)", "")) ?
                        0.00 : dt2.Compute("Sum(Cr)", ""))), 2);
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }





        private void GetVouCherNumber()
        {
            try
            {

                string comcod = this.GetCompCode();
                this.lblmsg.Text = "";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(this.lbldate.Text.Trim().Substring(0, 11)))
                {
                    this.lblmsg.Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.lbldate.Text.Substring(0, 11).Trim();
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }



        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {


            this.lblmsg.Visible = true;

            if ((accData.ToDramt) != (accData.ToCramt))
            {
                this.lblmsg.Text = "Debit Amount must be Equal Credit Amount";
                return;
            }



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
            //Existing   Purchase No  




            this.GetVouCherNumber();
            string voudat = this.lbldate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Depreciation Journal Voucher";
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
                    this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Transaction A Table-----------------//


                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = "0";
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());

                    double trnamt = (Dramt - Cramt);

                    if (trnamt != 0)
                    {
                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                                actcode, rescode, cactcode, voudat, trnqty, "", vtcode, trnamt.ToString(), spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }


                    }





                }





                bool resultD = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSORUPDEPVOU", vounum,
                            voudat, "", "", "", "", "", "", "", "", "", "", "", "", "");




                this.lblmsg.Text = "Update Successfully.";
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
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
                this.lblmsg.Text = "Error:" + ex.Message;
            }

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //      this.lnkOk_Click(null, null);

            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string curvoudat = this.lbldate.Text.Substring(0, 11);
            //    string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
            //            this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            //    //string vounum = this.ddlPrivousVou.SelectedValue.ToString();
            //    DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
            //    if (_ReportDataSet == null)
            //        return;
            //    DataTable dt = _ReportDataSet.Tables[0];
            //    if (dt.Rows.Count == 0)
            //        return;
            //    double dramt, cramt;
            //    dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
            //    cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



            //    if (dramt > 0 && cramt > 0)
            //    {
            //        TAmount = cramt;

            //    }
            //    else if (dramt > 0 && cramt <= 0)
            //    {
            //        TAmount = dramt;
            //    }
            //    else
            //    {
            //        TAmount = cramt;
            //    }

            //    DataTable dt1 = _ReportDataSet.Tables[1];
            //    string Vounum = dt1.Rows[0]["vounum"].ToString();
            //    string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            //    string refnum = dt1.Rows[0]["refnum"].ToString();
            //    string voutype = dt1.Rows[0]["voutyp"].ToString();
            //    string venar = dt1.Rows[0]["venar"].ToString();
            //    string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
            //    // string Type = this.CompanyPrintVou();
            //    ReportDocument rptinfo = new ReportDocument();
            //    rptinfo = new MFGRPT.R_15_Acc.rptPrintVoucherZelta();

            //    rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
            //    TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //    txtCompanyName.Text = comnam;
            //    TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //    txtcAdd.Text = comadd;
            //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //    vounum1.Text = "Voucher No.: " + vounum;
            //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //    date.Text = "Voucher Date: " + voudat;
            //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //    rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //    TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
            //    voutype1.Text = voutype;
            //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //    naration.Text = "Narration: " + venar;

            //    //TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
            //    //txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
            //    TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            //    rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

            //    TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "";
            //        string eventdesc = "Print Voucher";
            //        string eventdesc2 = vounum;
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }
            //    //string comcod = this.GetComeCode();
            //    //string comcod = hst["comcod"].ToString();
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptinfo.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptinfo;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //}
            //catch (Exception ex)
            //{
            //    this.lblmsg.Text = "Error:" + ex.Message;
            //}
        }
        private void GetDepreciation()
        {
            string comcod = this.GetCompCode();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "DEPRECIATION", frmdate,
                          todate, "", "", "", "", "", "", "");
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

                //if (Convert.ToDouble(dt1.Rows[i]["billqty"]) > 0)
                //{
                //    dgTrnrate =  Convert.ToDouble(dt1.Rows[i]["Dr"])/Convert.ToDouble(dt1.Rows[i]["billqty"]) ;
                //}

                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["dr"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["cr"]);

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
                dr1["Dr"] = dgTrnDrAmt;
                dr1["Cr"] = dgTrnCrAmt;
                tblt01.Rows.Add(dr1);
            }
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();

        }


        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            this.GridColoumnVisible();
            calculation();


        }

        private void GridColoumnVisible()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {

                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                string mRSIRCODE = tbl1.Rows[TblRowIndex2]["subcode"].ToString();
                if (ASTUtility.Left(mRSIRCODE, 2) != "97")
                    ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).ReadOnly = true;
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





        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblt01"];
            double todramt = 0, tocramt = 0;
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {
                string Supplier = ((Label)this.dgv2.Rows[j].FindControl("lblResCod")).Text.Trim();
                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                if (ASTUtility.Left(Supplier, 2) == "99")
                {
                    dt1.Rows[TblRowIndex2]["trncram"] = todramt - tocramt;
                    todramt = 0; tocramt = 0;
                    continue;
                }

                double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                tocramt = tocramt + cramt;

                dt1.Rows[TblRowIndex2]["trncram"] = cramt;

            }
            ViewState["tblt01"] = dt1;
            this.Data_Bind();



        }
        protected void imgbtnAcc_Click(object sender, EventArgs e)
        {
            this.LoadAccCombo();
        }
        private void LoadAccCombo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Serchlsdno = "%" + this.txtAccSch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER_2", "GETACCCODE", Serchlsdno, "", "", "", "", "", "", "", "");
            this.ddlActCode.Items.Clear();
            this.ddlActCode.DataTextField = "actdesc";
            this.ddlActCode.DataValueField = "actcode";
            this.ddlActCode.DataSource = ds1.Tables[0];
            this.ddlActCode.DataBind();

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>();

            ViewState["HeadAcc1"] = list;
            this.ddlActCode_SelectedIndexChanged(null, null);
        }
        protected void ddlActCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlActCode.BackColor = System.Drawing.Color.Pink;

            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)ViewState["HeadAcc1"];
            string search1 = this.ddlActCode.SelectedValue.ToString().Trim();
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst1 = lst.FindAll((p => p.actcode == search1));

            if (lst1[0].actelev.ToString() == "2")
            {
                this.lbldetails.Visible = true;
                this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;


                string actcode = this.ddlActCode.SelectedValue.Substring(0, 2);
                this.GetResCode();
            }
            else
            {
                this.lbldetails.Visible = false;
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.ddlresuorcecode.Items.Clear();
            }
        }
        private void GetResCode()
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = this.ddlActCode.SelectedValue.ToString();
                string filter1 = "%" + this.txtserchReCode.Text.Trim() + "%";

                string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();


                string SearchInfo = "";
                //var lst = (List<MFGOBJ.C_21_Acc.EClassDB_BO.EClassAcountsHead>)ViewState["tblsalinc"];

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lstacc = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)ViewState["HeadAcc1"];

                string search1 = this.ddlActCode.SelectedValue.ToString().Trim();

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lstacc1 = lstacc.FindAll((p => p.actcode == search1));


                string type = lstacc1[0].acttype.ToString().Trim();
                if (type.Length > 0)
                {

                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {


                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHead(actcode, filter1, SearchInfo);
                ViewState["HeadRsc1"] = lst;

                this.ddlresuorcecode.DataSource = lst;
                this.ddlresuorcecode.DataTextField = "resdesc1";
                this.ddlresuorcecode.DataValueField = "rescode";
                this.ddlresuorcecode.DataBind();
                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst1 = lst.FindAll((p => p.rescode == oldRescode));
                if (lst1.Count > 0)
                {
                    this.ddlresuorcecode.SelectedValue = oldRescode;


                }




                this.txtserchReCode.Text = "";
                string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst2 = lst.FindAll((p => p.rescode == seaRes));
                if (lst2.Count == 0)
                    return;


            }


            catch (Exception ex)
            {

                this.lblmsg.Text = ex.Message;
            }


        }
        protected void lbtnSelectTrns_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            string mActCode = this.ddlActCode.SelectedValue.ToString();
            string mResCode = this.ddlresuorcecode.SelectedValue.ToString();
            string Specification = "000000000000";
            DataRow[] dr2 = tbl1.Select("actcode = '" + mActCode + "' and subcode = '" + mResCode + "' and spclcode='" + Specification + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["actcode"] = this.ddlActCode.SelectedValue.ToString();
                dr1["subcode"] = this.ddlresuorcecode.SelectedValue.ToString();
                dr1["spclcode"] = "000000000000";
                dr1["actdesc"] = this.ddlActCode.SelectedItem.Text.Trim().Substring(14);
                dr1["subdesc"] = this.ddlresuorcecode.SelectedItem.Text.Trim().Substring(14);
                dr1["spcldesc"] = "";
                dr1["Dr"] = 0.00;
                dr1["Cr"] = 0.00;
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblt01"] = this.HiddenSameData(tbl1);
            this.Data_Bind();
        }
        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            int TblRowIndex2;
            //this.lblmsg1.Text = "";
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {
                TblRowIndex2 = (this.dgv2.PageSize) * (this.dgv2.PageIndex) + j;
                double DrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text = DrAmt.ToString("#,##0.000;(#,##0.000); ");
                tbl1.Rows[TblRowIndex2]["Dr"] = DrAmt;
            }
            ViewState["tblt01"] = tbl1;
        }
        protected void lbtnRecal_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
        protected void dgv2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblt01"];

            string actcode = ((Label)this.dgv2.Rows[e.RowIndex].FindControl("lblAccCod")).Text.Trim();
            string rescode = ((Label)this.dgv2.Rows[e.RowIndex].FindControl("lblResCod")).Text.Trim();

            if (rescode == "229900101001")
            {
                return;
            }


            int rowindex = (this.dgv2.PageSize) * (this.dgv2.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode<>''");
            ViewState["tblt01"] = dv.ToTable();
            this.Data_Bind();
        }
    }

}