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
namespace SPEWEB.F_21_GAcc
{
    public partial class TransectionPrint : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string Type = Request.QueryString["Type"].Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "AccVoucher" ? "voucher print" : (Type == "AccCheque" ? "Cheque Print" : "Post Dated Cheque Print")) + " Information ";
                this.SetView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SetView()
        {
            string Type = Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccVoucher":
                    this.rbtnList1.SelectedIndex = 0;
                    this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lstVouname.Visible = this.lstVouname.Items.Count > 0;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lmsg.Text = "";
                    //this.lnkbtnDelVoucher.Visible = false;
                    break;

                case "AccCheque":
                    this.rbtCprintList.SelectedIndex = 0;
                    this.CompanyCheckPrint();
                    this.txtfromdatec.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodatec.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetVouNum();
                    break;
                case "AccPostDatChq":
                    //this.rbtCprintList.SelectedIndex = 0;
                    //this.CompanyCheckPrint();
                    this.txtfromdatec1.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodatec1.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    this.PostDatChqGetVouNum();
                    break;


            }

        }
        protected void lnkbtnVouOk_Click(object sender, EventArgs e)
        {
            string Type = Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccVoucher":
                    this.printVou();
                    break;

            }
        }

        private void printVou()
        {

            Session.Remove("tblvoucher");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int index1 = Convert.ToInt32(this.rbtnList1.SelectedIndex);
            string Voutype = ((index1 == 0) ? "B" : (((index1 == 1) ? "C" : ((index1 == 2) ? "J" : ((index1 == 3) ? "PV" : ((index1 == 4) ? "All" : "D"))))));
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Caltype = ((index1 == 5) ? "DELETEDVOUCHER" : "GETVOUCHER");
            string cheqqueno = this.txtSearchChequeno.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", Caltype, Voutype, frmdate, todate, cheqqueno, "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.lstVouname.Items.Clear();
                return;
            }
            Session["tblvoucher"] = ds1.Tables[0];

            this.lstVouname.DataTextField = "vounum1";
            this.lstVouname.DataValueField = "vounum";
            this.lstVouname.DataSource = ds1.Tables[0];
            this.lstVouname.DataBind();
            this.lstVouname.Visible = this.lstVouname.Items.Count > 0;
            if (Request.QueryString["Mod"].Trim() == "Management")
            {
                if (this.lstVouname.Items.Count > 0)
                {
                    this.lstVouname.SelectedIndex = 0;
                    this.lnkbtnDelVoucher.Visible = true;
                }
                else
                {
                    this.lnkbtnDelVoucher.Visible = false;
                }

            }

        }

        private string Companylimit()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string limit = "";
            switch (comcod)
            {

                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    limit = "";
                    break;


                case "1301":
                case "2301":
                case "3301":
                    limit = "limit";
                    break;

                default:
                    limit = "limit";
                    break;
            }
            return limit;
        }

        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }
        protected void lnkbtnDelVoucher_Click(object sender, EventArgs e)
        {
            this.lmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string vounum = this.lstVouname.SelectedValue.ToString();
            bool result;
            string comlimit = this.Companylimit();
            if (comlimit.Length > 0)
            {





                string voudat = Convert.ToDateTime((((DataTable)Session["tblvoucher"]).Select("vounum='" + vounum + "'"))[0]["voudat"]).ToString("dd-MMM-yyyy");
                DateTime Bdate = this.GetBackDate();
                bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is Equal or Greater then Transaction Limt');", true);
                    return;
                }

            }

            if (ASTUtility.Left(vounum, 2) == "PV" || ASTUtility.Left(vounum, 2) == "DV")
            {
                // result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELPVDVVOU", vounum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                return;

            }
            else
            {
                result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEVOUCHER", vounum, userid, Terminal, Sessionid, "", "", "", "", "", "", "", "", "", "", "");
            }
            if (result == false)
            {
                this.lmsg.Text = "Data Is Not Deleted";

            }
            else
            {
                this.lmsg.Text = "Deleted  Successfully";
                this.lnkbtnVouOk_Click(null, null);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Delete Voucher";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

        }
        private void CompanyCheckPrint()
        {
            //  this.rbtSalSheet.Visible = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {
                case "2305": //Ru Land
                    this.rbtCprintList.SelectedIndex = 1;
                    break;
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = Request.QueryString["Type"].Trim();
            string vounum = this.lstVouname.SelectedValue.ToString();
            string vouforcheque = this.ddlChkVouNo.SelectedValue.ToString();
            string acpayestatus = this.ChboxPayee.Checked ? "A/C Payee" : " ";
            switch (Type)
            {
                case "AccVoucher":
                    if (this.rbtnList1.SelectedIndex == 3)
                    {

                        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=PostDatVou&vounum=" + vounum + "', target='_blank');</script>";

                        // this.PostVouPrint();
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=accVou&vounum=" + vounum + "', target='_blank');</script>";


                    }
                    break;

                case "AccCheque":

                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=Cheque&vounum=" + vouforcheque + "&payee=" + acpayestatus + "', target='_blank');</script>";

                    //this.PrintCheque();

                    break;

                case "AccPostDatChq":
                    string postvounum = this.ddlPostDatedCheque.SelectedValue.Substring(0, 14);
                    string chqno = this.ddlPostDatedCheque.SelectedValue.Substring(14);

                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=PosdatCheque&vounum=" + postvounum + "&payee=" + acpayestatus + "&chequeno=" + chqno + "', target='_blank');</script>";

                    // this.RptPostDatChq();
                    break;
            }


        }




        private void GetVouNum()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chqno = "%" + this.txtSearchCheqno.Text + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdatec.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETVOUCHERFORCHK", "", frmdate, todate, chqno, "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.ddlChkVouNo.Items.Clear();
                return;

            }

            DataView dv = ds4.Tables[0].DefaultView;
            dv.RowFilter = ("chk ='True'");
            this.ddlChkVouNo.DataTextField = "vounum1";
            this.ddlChkVouNo.DataValueField = "vounum";
            this.ddlChkVouNo.DataSource = dv.ToTable();
            this.ddlChkVouNo.DataBind();
        }
        private void PostDatChqGetVouNum()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chqno = "%" + this.txtSearchPCheqno.Text + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdatec1.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec1.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETVOUCHERFOR_POSTDAT_CHQ", "", frmdate, todate, chqno, "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.ddlPostDatedCheque.Items.Clear();
                return;
            }

            //DataView dv = ds4.Tables[1].DefaultView;
            //dv.RowFilter = ("chk ='True'");

            this.ddlPostDatedCheque.DataTextField = "vounum1";
            this.ddlPostDatedCheque.DataValueField = "vounum";
            this.ddlPostDatedCheque.DataSource = ds4.Tables[1];
            this.ddlPostDatedCheque.DataBind();
        }


        protected void lnkbtnChkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnChkOk.Text == "Ok")
            {
                this.lnkbtnChkOk.Text = "New";
                this.GetVouNum();
                this.ShowCheque();

            }
            else
            {
                this.lmsg01.Text = "";
                this.lnkbtnChkOk.Text = "Ok";
                this.gvCheque.DataSource = null;
                this.gvCheque.DataBind();

            }




        }

        private void ShowCheque()
        {
            Session.Remove("tblchkprint");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Voutype = "BD";
            string frmdate = Convert.ToDateTime(this.txtfromdatec.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec.Text).ToString("dd-MMM-yyyy");
            string chqno = "%" + this.txtSearchCheqno.Text + "%";
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETVOUCHERFORCHK", Voutype, frmdate, todate, chqno, "", "", "", "", "");


            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.gvCheque.DataSource = null;
                this.gvCheque.DataBind();
                return;
            }

            Session["tblchkprint"] = ds4.Tables[0];
            this.LoadGrid();


        }
        private void ShowPostDatCheque()
        {
            Session.Remove("tblchkprint");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Voutype = "PV";
            string frmdate = Convert.ToDateTime(this.txtfromdatec1.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec1.Text).ToString("dd-MMM-yyyy");
            string chqno = "%" + this.txtSearchPCheqno.Text + "%";
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETVOUCHERFOR_POSTDAT_CHQ", Voutype, frmdate, todate, chqno, "", "", "", "", "");


            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.gvPostDatCheq.DataSource = null;
                this.gvPostDatCheq.DataBind();
                return;
            }

            Session["tblchkprint"] = ds4.Tables[0];
            this.LoadGrid();


        }
        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblchkprint"];
            string Type = Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccCheque":
                    this.gvCheque.DataSource = dt;
                    this.gvCheque.DataBind();
                    break;
                case "AccPostDatChq":
                    this.gvPostDatCheq.DataSource = dt;
                    this.gvPostDatCheq.DataBind();
                    break;

            }


        }

        protected void gvCheque_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvCheque.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblchkprint"];
            int Rowid;
            string Type = Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccCheque":
                    for (int i = 0; i < this.gvCheque.Rows.Count; i++)
                    {

                        string payto = ((TextBox)gvCheque.Rows[i].FindControl("txtgvPayto")).Text.Trim();
                        Rowid = (this.gvCheque.PageSize) * (this.gvCheque.PageIndex) + i;
                        dt.Rows[Rowid]["payto"] = payto;

                    }
                    break;
                case "AccPostDatChq":
                    for (int i = 0; i < this.gvPostDatCheq.Rows.Count; i++)
                    {

                        string payto = ((TextBox)gvPostDatCheq.Rows[i].FindControl("txtgvPayto1")).Text.Trim();
                        Rowid = (this.gvPostDatCheq.PageSize) * (this.gvPostDatCheq.PageIndex) + i;
                        dt.Rows[Rowid]["payto"] = payto;

                    }
                    break;

            }
            Session["tblchkprint"] = dt;
        }



        protected void lnkbntUpPayto_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lmsg01.Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblchkprint"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string vounum = dt.Rows[i]["vounum"].ToString();
                string payto = dt.Rows[i]["payto"].ToString();

                bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEACTRNB",
                              vounum, payto, "", "", "", "", "", "", "", "", "", "", "", "", "");



                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Updated Voucher";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

            this.lmsg01.Text = "Updated Successfully";
            this.GetVouNum();
            this.LoadGrid();

        }




        protected void btnPostDatChqOk_Click(object sender, EventArgs e)
        {
            if (this.btnPostDatChqOk.Text == "Ok")
            {
                this.btnPostDatChqOk.Text = "New";
                this.PostDatChqGetVouNum();
                this.ShowPostDatCheque();

            }
            else
            {
                this.lmsg02.Text = "";
                this.btnPostDatChqOk.Text = "Ok";
                this.gvPostDatCheq.DataSource = null;
                this.gvPostDatCheq.DataBind();

            }
        }
        protected void gvPostDatCheq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvPostDatCheq.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void lnkbntUpPayto1_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lmsg02.Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblchkprint"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string vounum = dt.Rows[i]["vounum"].ToString().Substring(0, 14);
                string chequeno = dt.Rows[i]["chequeno"].ToString();
                string payto = dt.Rows[i]["payto"].ToString();

                bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATEACPMNTA",
                              vounum, chequeno, payto, "", "", "", "", "", "", "", "", "", "", "", "");


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Updated Voucher";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

            this.lmsg02.Text = "Updated Successfully";
            this.PostDatChqGetVouNum();
            this.LoadGrid();
        }
        protected void imgbtnSearchChq_Click(object sender, EventArgs e)
        {
            this.GetVouNum();
        }
        protected void imgbtnSearchPChq_Click(object sender, EventArgs e)
        {
            this.PostDatChqGetVouNum();
        }
    }
}