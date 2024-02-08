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
    public partial class AccSales : System.Web.UI.Page
    {
        public static string Narration = "";
        public static string RefNo = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Collection Update";
                if (this.ddlConAccHead.Items.Count > 0)
                    return;
                this.LoadAcccombo();
                this.GetProjectName();

                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txttodate.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");

                //this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = "000000000000";

        }
        private void LoadAcccombo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string txtsrchconcode = this.txtScrchConCode.Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD", txtsrchconcode, "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }

        }
        private void Refrsh()
        {
            this.lblmsg.Text = "";
        }
        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
        }
        protected void txtEntryDate_TextChanged(object sender, EventArgs e)
        {
            this.txtEntryDate.BackColor = System.Drawing.Color.Aqua;
        }
        protected void lnkOk0_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;
            try
            {
                this.Refrsh();
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "REPORTACCSALES", frmdate, todate, pactcode, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblMrr"] = ds1.Tables[0];
                DataTable dt1 = ds1.Tables[0];
                this.HiddenSameDate(dt1);
                this.CalculatrGridTotal();

            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error :" + ex.Message;
                this.lblmsg.Text = "Available Data Not in Position";
            }

        }


        private void HiddenSameDate(DataTable dt1)
        {

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }

            this.Data_Bind();
        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tblMrr"];
            ((Label)this.dgv1.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dttotal.Compute("Sum(cramt)", "")) ?
          0.00 : dttotal.Compute("Sum(cramt)", ""))).ToString("#,##0.00;-#,##0.00; ");
            ((Label)this.dgv1.FooterRow.FindControl("lgvFoncharge")).Text = Convert.ToDouble((Convert.IsDBNull(dttotal.Compute("Sum(vatamt)", "")) ?
        0.00 : dttotal.Compute("Sum(vatamt)", ""))).ToString("#,##0;-#,##0; ");
        }
        protected void lnkVouOk_Click(object sender, EventArgs e)
        {

        }

        //private void UPdateMrinf(string vounum)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string Vounum = vounum;

        //    for (int i = 0; i < this.dgv1.Rows.Count; i++)
        //    {

        //        bool chkmr = ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked;
        //        if (chkmr == true)
        //        {
        //            string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
        //            string rescode = ((Label)this.dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
        //            string Mrno = ((Label)this.dgv1.Rows[i].FindControl("lgvmrno")).Text.Trim();
        //            string Chequeno = ((Label)this.dgv1.Rows[i].FindControl("lgvCheNo")).Text.Trim();
        //            string cactcode = ((Label)this.dgv1.Rows[i].FindControl("lgvcactcode")).Text.Trim();


        //            bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEEMRINF", actcode, rescode, Mrno, Chequeno, vounum,
        //                            cactcode, "", "", "", "", "", "", "", "", "");
        //            if (!resulta)
        //            {
        //                this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
        //                return;
        //            }
        //        }
        //    }


        //}
        private void vounum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.lblmsg.Text = "";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;

            }

            DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

            if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
            {
                this.lblmsg.Text = "Voucher Date Must  Be Greater then Opening Date";
                return;

            }
            double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : "C"));
            string VNo3 = Convert.ToString(VNo1 + VNo2);
            string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            Session["NEWVOUNUM"] = dt4;
        }


        protected void ibtnFindConCode_Click(object sender, EventArgs e)
        {
            this.LoadAcccombo();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {


        }
        private void Data_Bind()
        {
            this.dgv1.DataSource = (DataTable)Session["tblMrr"];
            this.dgv1.DataBind();
            DataTable dt1 = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string pactcode = ((Label)dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
                string usircode = ((Label)dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
                string mrno = ((Label)dgv1.Rows[i].FindControl("lgvmrno")).Text.Trim();
                string Memono = ((Label)dgv1.Rows[i].FindControl("lgvMemono")).Text.Trim();
                string cheqno = ((Label)dgv1.Rows[i].FindControl("lgvCheNo")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)dgv1.Rows[i].FindControl("lbok");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = pactcode + usircode + mrno + Memono + cheqno;
            }
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                string Sirialno = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvsirialno")).Text.Trim();
                dt.Rows[i]["sirialno"] = Sirialno;
                dt.Rows[i]["chkmv"] = chkmr;
                ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblMrr"] = dt;
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
        protected void lbok_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg.Text = "You have no permission";
            //    return;
            //}



            this.CheckValue();

            string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
            DateTime Bdate = this.GetBackDate();
            bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(entrydate));
            if (!dcon)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                return;
            }
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Terminal = hst["trmid"].ToString();
                string Sessionid = hst["session"].ToString();
                string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
                string pactcode = code.Substring(0, 12).ToString();
                string usircode = code.Substring(12, 12).ToString();
                string mrno = code.Substring(24, 14).ToString();
                string Memono = code.Substring(38, 14).ToString();
                string cheqno = (code.Length > 52) ? code.Substring(52).ToString() : "";

                DataTable dt = (DataTable)Session["tblMrr"];
                DataRow[] dr = dt.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "' and mrno='" + mrno + "' and memono='" + Memono + "' and chqno='" + cheqno + "'");
                if (dr.Length == 0)
                    return;

                double tqty = Convert.ToDouble(dr[0]["qty"].ToString());
                double dramt = Convert.ToDouble(dr[0]["dramt"].ToString());
                double cramt = Convert.ToDouble(dr[0]["cramt"].ToString());
                string trnRemarks = dr[0]["urmrks"].ToString().Trim();
                string Chk = dr[0]["chkmv"].ToString();
                string trnamt = Convert.ToString(dramt - cramt);
                string cactcode = dr[0]["cactcode"].ToString();
                double vatamt = Convert.ToDouble(dr[0]["vatamt"].ToString());
                double allocamt = Convert.ToDouble(dr[0]["allocamt"].ToString());
                double cglamt = Convert.ToDouble(dr[0]["cglamt"].ToString());
                if (Chk == "False")
                {
                    this.lblmsg.Text = "Please Check CheckBox";
                    return;
                }
                ///////////-- Create Auto JV-----------//////////////////
                //DataSet ds7 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "CREATEAUTOJV", mrno, pactcode, usircode, "", "", "", "", "", "");


                /////////////////--------------------------------------------------
                //Existing MR

                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGCOLUP", mrno, pactcode, usircode, Memono, cheqno, cactcode, "", "", "");
                if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    this.lblmsg.Text = "Voucher No already Existing in this MR No";
                    return;
                }

                //return;
                /////////Get New Voucher-----------------------------       

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
                {
                    this.lblmsg.Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }
                double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                string ConAccHead = (cactcode == "000000000000") ? this.ddlConAccHead.SelectedValue.ToString() : cactcode;
                string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
                string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : "C"));
                string VNo3 = Convert.ToString(VNo1 + VNo2);
                DataSet dsv = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = dsv.Tables[0];
                Session["NEWVOUNUM"] = dt4;
                DataTable dt12 = (DataTable)Session["NEWVOUNUM"];




                ///-----------------------------------------------------///////////////////
                //string type = dr[0]["paydesc"].ToString();
                //if (type == "CASH")
                //{
                Narration = dr[0]["mrno"].ToString() + " Date:" + Convert.ToDateTime(dr[0]["paydate"]).ToString("dd.MM.yyyy") + "; ";
                //}

                //else
                //{
                //    RefNo = dr[0]["chqno"].ToString() + ", ";
                //    Narration = "Mrr" + dr[0]["mrno"].ToString() + ", " + "Date:" + Convert.ToDateTime(dr[0]["paydate"]).ToString("dd.MM.yyyy") + ", " + dr[0]["bankname"].ToString() + ", " + dr[0]["bbranch"].ToString() + "; ";

                //}
                int Reflenght = RefNo.Length;
                if (Reflenght > 0)
                {
                    RefNo = RefNo.Substring(0, Reflenght - 2);
                }
                int lenght = Narration.Length;
                Narration = Narration.Substring(0, lenght - 2);
                /////////---------------------------------------
                string vounum = dt12.Rows[0]["couvounum"].ToString();
                string voudat = this.txtEntryDate.Text.Substring(0, 11);
                string refnum = RefNo;
                string srinfo = dr[0]["sirialno"].ToString().Trim();
                string vounarration1 = Narration;
                string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                string vouno = vounum.Substring(0, 2).ToString();
                string voutype = (vouno == "JV" ? "Journal Voucher" :
                                    (vouno == "CD" ? "Cash Payment Voucher" :
                                    (vouno == "BD" ? "Bank Payment Voucher" :
                                    (vouno == "CC" ? "Cash Deposit Voucher" :
                                    (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
                string vtcode = "99";
                string edit = "EDIT";

                dr[0]["newvocnum"] = vounum.Substring(0, 2).ToString() + vounum.Substring(6, 2).ToString() + "-" + vounum.Substring(8).ToString();


                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Transaction A Table-----------------//

                string spclcode = "000000000000";

                bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, "18" + ASTUtility.Right(pactcode, 10), usircode, ConAccHead,
                                voudat, tqty.ToString(), trnRemarks, vtcode, trnamt, spclcode, voudat, "", mrno, "", ""); //
                if (!resulta)
                {
                    this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }


                ////////////////////////////////////////////////
                this.lblmsg.Text = "Update Successfully.";

                this.Data_Bind();
                this.CheckValue();

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Collection Update";
                //    string eventdesc = "Update Collection";
                //    string eventdesc2 = vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}


                // Update  MRR------------------------------------
                resulta = accData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEEMRINF", pactcode, usircode, mrno, cheqno, vounum,
                        cactcode, Memono, "", "", "", "", "", "", "", "");
                if (!resulta)
                {
                    this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }



                //////////////////////////////// BankCharge Part/////////////////////////////////////////////


                if (vatamt > 0.00)
                {

                    voutype = (ASTUtility.Left(cactcode, 4) == "1901") ? "CD" : "BD";
                    DataSet ds6 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, voutype, "", "", "", "", "", "", "");
                    string bcvounum = ds6.Tables[0].Rows[0]["couvounum"].ToString();

                    string bcvouno1 = bcvounum.Substring(0, 2).ToString();
                    string voutype1 = (bcvouno1 == "JV" ? "Journal Voucher" :
                                        (bcvouno1 == "CD" ? "Cash Payment Voucher" :
                                        (bcvouno1 == "BD" ? "Bank Payment Voucher" :
                                        (bcvouno1 == "CC" ? "Cash Deposit Voucher" :
                                        (bcvouno1 == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));


                    //-----------Update Transaction B Table-----------------//
                    bool resultd = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", bcvounum, voudat, refnum, srinfo, vounarration1,
                                    vounarration2, voutype1, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                    if (!resultd)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                    //-----------Update Transaction A Table-----------------//

                    //string spclcode = "000000000000";
                    //bool resulta;


                    bool resulte = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", bcvounum, "710100020001", usircode, ConAccHead,
                                    voudat, tqty.ToString(), trnRemarks, vtcode, vatamt.ToString(), spclcode, voudat, "", mrno, "", "");

                    if (!resulte)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }


                    // Update  MRR------------------------------------
                    resulte = accData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEBCHARGEMRINF", pactcode, usircode, mrno, cheqno, bcvounum,
                            ConAccHead, "", "", "", "", "", "", "", "", "");
                    if (!resulte)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }

                }


                //////////////////////////////// Gain/Loss Part/////////////////////////////////////////////


                //if (cglamt > 0.00)
                //{

                //    voutype = "JV";
                //    DataSet ds6 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, voutype, "", "", "", "", "", "", "");
                //    string bcvounum = ds6.Tables[0].Rows[0]["couvounum"].ToString();

                //    string bcvouno1 = bcvounum.Substring(0, 2).ToString();
                //    string voutype1 = "Journal Voucher" ;


                //    //-----------Update Transaction B Table-----------------//
                //    bool resultd = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", bcvounum, voudat, refnum, srinfo, vounarration1,
                //                    vounarration2, voutype1, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                //    if (!resultd)
                //    {
                //        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                //        return;
                //    }
                //    //-----------Update Transaction A Table-----------------//


                //    bool resulte;


                //    resulte = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", bcvounum, pactcode, usircode, ConAccHead,
                //                    voudat, "0", trnRemarks, vtcode, cglamt.ToString(), spclcode, voudat, "", mrno, "", "");
                //    resulte = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", bcvounum, "810300010001", usircode, ConAccHead,
                //                    voudat, "0", trnRemarks, vtcode, cglamt.ToString(), spclcode, voudat, "", mrno, "", "");

                //    if (!resulte)
                //    {
                //        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                //        return;
                //    }


                //    // Update  MRR------------------------------------
                //    resulte = accData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEBCHARGEMRINF", pactcode, usircode, mrno, cheqno, bcvounum,
                //            ConAccHead, "", "", "", "", "", "", "", "", "");
                //    if (!resulte)
                //    {
                //        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                //        return;
                //    }

                //}

                //////////////////////////////// Fund Transfer Part/////////////////////////////////////////////


                if (allocamt > 0.00)
                {
                    DataSet dsal = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "GETRELZAMTALLOC", mrno, "", "", "", "", "", "", "", "");

                    voutype = "BD";
                    DataSet ds6 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, voutype, "", "", "", "", "", "", "");
                    string bcvounum = ds6.Tables[0].Rows[0]["couvounum"].ToString();

                    string bcvouno1 = bcvounum.Substring(0, 2).ToString();
                    string voutype1 = (bcvouno1 == "JV" ? "Journal Voucher" :
                                        (bcvouno1 == "CD" ? "Cash Payment Voucher" :
                                        (bcvouno1 == "BD" ? "Bank Payment Voucher" :
                                        (bcvouno1 == "CC" ? "Cash Deposit Voucher" :
                                        (bcvouno1 == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));


                    //-----------Update Transaction B Table-----------------//
                    bool resultd = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", bcvounum, voudat, refnum, srinfo, vounarration1,
                                    vounarration2, voutype1, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                    if (!resultd)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                    //-----------Update Transaction A Table-----------------//
                    bool resulteb;
                    foreach (DataRow dr1 in dsal.Tables[0].Rows)
                    {
                        string actcode = dr1["actcode"].ToString();
                        string rescode = dr1["rescode"].ToString();
                        string bcactcode = dr1["cactcode"].ToString();

                        string mrslno = dr1["mrslno"].ToString();
                        double bdtamt = Convert.ToDouble(dr1["bdtamt"].ToString());

                        resulteb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", bcvounum, actcode, rescode, bcactcode,
                                    voudat, "0.00", trnRemarks, vtcode, bdtamt.ToString(), spclcode, voudat, "", mrno, "", "");


                        if (!resulteb)
                        {
                            this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                    }






                    // Update  MRR B------------------------------------
                    resulteb = accData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEALLRLZMRINF", mrno, bcvounum, "", "", "",
                            "", "", "", "", "", "", "", "", "", "");
                    if (!resulteb)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }

                    this.lblmsg.Text = "Update Successfully.";




                    if (ConstantInfo.LogStatus == true)
                    {
                        string eventtype = "Collection Update";
                        string eventdesc = "Update Collection";
                        string eventdesc2 = bcvounum;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    }
                }

            }



            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }




        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
    }
}