using Microsoft.Reporting.WinForms;
using SPEENTITY.C_10_Procur;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_10_Procur
{
    public partial class PurchaseReturn : System.Web.UI.Page
    {
        //UserManSales objUserService = new UserManSales();
        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Return Entry";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                //this.Visiable();
                this.GetStore();
                this.GetSupplier();
                this.GetInfo();
                this.GetItems();


                //if (this.Request.QueryString["Type"].ToString() == "Approved")
                //{
                //    this.PreviousList();
                //}
                if (Request.QueryString.AllKeys.Contains("genno") == true && this.Request.QueryString["genno"].Length > 0)
                {
                    this.ddlStore.SelectedValue = this.Request.QueryString["centrid"].ToString();
                    this.txtCurDate.Text = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
                    this.PreviousList();
                }
                this.CommonButton();
            }
        }
        //private void Visiable()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    if (ASTUtility.Left(comcod, 1) == "7")
        //    {
        //        this.lblIMEI.Visible = true;
        //        this.txtcaroriemi.Visible = true;
        //        this.pnlImEI.Visible = true;
        //    }
        //}
        private void CommonButton()
        {
            //    ((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            //    ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            //    ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //    ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //    ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //    ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

            //    ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //    ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //    ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //    ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //    Hashtable hst = (Hashtable)Session["tblLogin"];


            //    string qType = this.Request.QueryString["Type"].ToString();
            //    if (qType == "Entry")
            //    {

            //        ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //        ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //    }
            //    else
            //    {
            //        ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            //        ((LinkButton)this.Master.FindControl("lnkbtnNew")).Text = "Approved";

            //    }

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(prevPage);
        //}

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(Approved_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        private void GetInfo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETCRINF", "99%", "%", "", "", "", "", "");
                ViewState["tblSupinf"] = ds1.Tables[0];

                GetSupplier();
            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error:" + ex.Message;
            }
        }

        private void GetStore()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string HeaderCode = "15%";
            string filter = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            dt1.Rows.Add(comcod, "000000000000", "----All-----", "-----All-----");

            this.ddlStore.DataSource = dt1;
            this.ddlStore.DataTextField = "actdesc1";
            this.ddlStore.DataValueField = "actcode";
            this.ddlStore.DataBind();
        }

        private void GetSupplier()
        {
            //DataTable dt = ((DataTable)ViewState["tblSupinf"]).Copy();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            //string filter = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", "%", "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            //dt1.Rows.Add(comcod, "000000000000", "All", "All");

            this.ddlSupplier.DataSource = dt1;
            this.ddlSupplier.DataTextField = "ssirdesc1";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataBind();

            this.ddlSupplier_SelectedIndexChanged(null, null);

        }


        protected void Approved_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Notifi", "Notifications('ERROR','You have no permission!','warning');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            string rDate = this.GetStdDate(this.txtCurDate.Text.Trim());


            List<SPEENTITY.C_10_Procur.EClassPur.EClassShowInvoice> lst = (List<SPEENTITY.C_10_Procur.EClassPur.EClassShowInvoice>)ViewState["tblPurReturn"];
            if (lst.Count == 0)
                return;
            string mRETNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(6, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string sactcode = this.ddlStore.SelectedValue.ToString();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_SALES_RETURN", "APPRETURN", sactcode, mRETNO, usrid, sessionid, trmid, PostedDat, rDate);
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Notifi", "Notifications('ERROR','" + purData.ErrorObject["Msg"].ToString() + "','warning');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "Notifi", "Notifications('SUCCESS','Approved!','success');", true);
        }


        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd-MMM-yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + Date1.Substring(3, 3) + "-" + Date1.Substring(7, 4);
            return Date1;
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetItems();
        }
        private void GetItems()
        {
            string StoreID = this.ddlStore.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            string supplier = this.ddlSupplier.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ONLY_MATERIAL_LIST ", "04%", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblitems"] = ds2.Tables[0];

            this.ddlIItems.DataSource = ds2.Tables[0];
            DataView dv = ds2.Tables[0].DefaultView;
            this.ddlIItems.DataTextField = "sirdesc";
            this.ddlIItems.DataValueField = "sircode";

            DataTable dt = dv.ToTable(true, "sircode", "sirdesc");
            this.ddlIItems.DataSource = dt;
            this.ddlIItems.DataBind();
            ddlIItems_SelectedIndexChanged(null, null);

        }

        private void GetSpecifications()
        {

            try
            {
                DataTable lst = (DataTable)ViewState["tblitems"];

                if (lst.Rows.Count == 0)
                    return;
                string rsircode = this.ddlIItems.SelectedValue.ToString();
                DataView dv = lst.DefaultView;
                dv.RowFilter = "rsircode='" + rsircode + "'";
                this.ddlSpecf.DataTextField = "spcfdesc";
                this.ddlSpecf.DataValueField = "spcfcod";
                this.ddlSpecf.DataSource = dv.ToTable();
                this.ddlSpecf.DataBind();

            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error:" + ex.Message;
            }

        }


        private void PreviousList()
        {
            string Centrid = this.ddlStore.SelectedValue.ToString();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_PRE_PUR_RETURN", Centrid, CurDate1, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlPrevList.DataTextField = "pretmemo1";
            this.ddlPrevList.DataValueField = "pretmemo";
            this.ddlPrevList.DataSource = ds2.Tables[0];
            this.ddlPrevList.DataBind();


            ViewState["tblPreList"] = ds2.Tables[0]; ;

            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPrevList.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.lbtnOk_Click(null, null);
            }

        }

        protected void imgSearchOrder_Click(object sender, EventArgs e)
        {
            this.GetItems();
        }

        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                ViewState.Remove("tblPurReturn");
                ViewState.Remove("tblimei");
                this.LbtnAdd.Visible = false;
                this.lblSpecifications.Visible = false;
                this.lblSalesOrder.Visible = false;
                this.ddlIItems.Visible = false;
                this.ddlSpecf.Visible = false;
                this.ddlIItems.Enabled = true;
                this.ddlStore.Enabled = true;
                //this.lblPrevious.Visible = true;
                //this.txtSrchPrevious.Visible = true;
                this.imgPreVious.Visible = true;
                this.ddlPrevList.Visible = true;
                this.ddlPrevList.Items.Clear();
                //this.rpprodetails.DataSource = null;
                //this.rpprodetails.DataBind();
                this.lblCurNo1.Text = "RET" + DateTime.Today.ToString("MM") + "-";
                this.txtCurDate.Enabled = true;
                this.txtBillNarr.Text = "";
                this.gvRetInfo.EditIndex = -1;
                this.gvRetInfo.DataSource = null;
                this.gvRetInfo.DataBind();

                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.Panel2.Visible = true;
                this.LblCstmRef.Visible = false;
                this.txtCustomRef.Visible = false;

                this.ddlSupplier.Enabled = true;
                return;
            }
            this.LbtnAdd.Visible = true;
            this.lblSpecifications.Visible = true;
            this.lblSalesOrder.Visible = true;
            this.ddlIItems.Visible = true;
            this.ddlSpecf.Visible = true;
            this.ddlStore.Enabled = false;
            this.imgPreVious.Visible = false;
            this.ddlPrevList.Visible = false;
            this.txtCurNo2.ReadOnly = true;
            this.Panel2.Visible = true;
            this.LblCstmRef.Visible = true;
            this.txtCustomRef.Visible = true;

            this.ddlSupplier.Enabled = false;
            this.lbtnOk.Text = "New";
            this.Get_Info();
        }


        protected void Data_Bind()
        {

            try
            {
                List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn> lst = (List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>)ViewState["tblPurReturn"];
                this.gvRetInfo.DataSource = lst;
                this.gvRetInfo.DataBind();
                //this.FooterCalCulation(lst);
                //this.txtOvDis.Text = lst[0].ovdis.ToString("#,##0;(#,##0); ");
            }
            catch (Exception e)
            {

                //this.lblmsg.Visible = true;
                //this.lblmsg.Text = e.Message;

            }
        }


        private void FooterCalCulation(List<SPEENTITY.C_10_Procur.EClassPur.EClassShowInvoice> lst)
        {

            if (lst.Count == 0)
                return;

            ((Label)this.gvRetInfo.FooterRow.FindControl("lblgvFinvAmt")).Text = (lst.Select(p => p.invamt).Sum() == 0.00) ? "" : lst.Select(p => p.invamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvRetInfo.FooterRow.FindControl("lblFretamt")).Text = (lst.Select(p => p.retamt).Sum() == 0.00) ? "" : lst.Select(p => p.retamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvRetInfo.FooterRow.FindControl("lblgvFinvdis")).Text = (lst.Select(p => p.invdis).Sum() == 0.00) ? "" : lst.Select(p => p.invdis).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvRetInfo.FooterRow.FindControl("lblgvrFetindis")).Text = (lst.Select(p => p.retindis).Sum() == 0.00) ? "" : lst.Select(p => p.retindis).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvRetInfo.FooterRow.FindControl("lblgvFInvvat")).Text = (lst.Select(p => p.invvat).Sum() == 0.00) ? "" : lst.Select(p => p.invvat).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvRetInfo.FooterRow.FindControl("lblgvFRetvat")).Text = (lst.Select(p => p.retvat).Sum() == 0.00) ? "" : lst.Select(p => p.retvat).Sum().ToString("#,##0;(#,##0); ");


            double ovDis = lst[0].ovdis;
            double RetAmt = lst.Select(p => p.retamt).Sum();
            double InvAmt = lst.Select(p => p.invamt).Sum();
            //this.txtRetOvDis.Text = ((InvAmt == 0) ? 0.00 : (ovDis * RetAmt) / InvAmt).ToString("#,##0;(#,##0); ");


        }

        class SumClass
        {
            public string rsircode { get; set; }
            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public double retqty { get; set; }

        }

        
        protected void GetSaRetNo()
        {
            string comcod = this.GetCompCode();
            string Centrid = this.ddlStore.SelectedValue.ToString();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string mInvoiceNo = "NEWSRET";
            if (this.ddlPrevList.Items.Count > 0)
                mInvoiceNo = this.ddlPrevList.SelectedValue.ToString();
            if (mInvoiceNo == "NEWSRET")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LAST_PUR_RET_NO", CurDate1, Centrid);

                if (ds2 == null)
                    return;
                this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);

                this.ddlPrevList.DataTextField = "maxno1";
                this.ddlPrevList.DataValueField = "maxno";
                this.ddlPrevList.DataSource = ds2.Tables[0];
                this.ddlPrevList.DataBind();

            }
        }


        protected void Get_Info()
        {
            string Centrid = this.ddlStore.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string mReturn = "NEWPRET";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mReturn = this.ddlPrevList.SelectedValue.ToString();

            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_PURCHASE_RETURN_INFO", Centrid, mReturn);

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>();


            //List<SPEENTITY.C_10_Procur.EClassPur.EClassShowInvoice> lst = objUserService.ShowInvInfo(mInvoice, Centrid, mReturn);

            if (lst == null)
                return;
            ViewState["tblPurReturn"] = lst;

            this.Data_Bind();
            if (mReturn == "NEWPRET")
            {

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LAST_PUR_RET_NO", CurDate1, Centrid);

                if (lst == null)
                    return;
                this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                //this.GetInvoiceInfo();
                return;
            }


            //this.ddlStore.SelectedValue = lstord[0].sactcode.ToString().Trim();
            //this.txtRefno.Text = lstord[0].refo.ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["pretmemo1"].ToString().Substring(0, 6);
            this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["pretmemo1"].ToString().Substring(6, 5);

            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["pretdat"]).ToString("dd-MMM-yyyy");
            this.txtBillNarr.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();
            this.txtCustomRef.Text = ds1.Tables[1].Rows[0]["custmchlln"].ToString();

            this.ddlStore.Text = ds1.Tables[1].Rows[0]["centrid"].ToString();
            this.ddlSupplier.Text = ds1.Tables[1].Rows[0]["ssircode"].ToString();

        }


        protected void SaveValue()
        {
            try
            {
                string comcod = this.GetCompCode();
                List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn> lst = (List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>)ViewState["tblPurReturn"];

                int index;
                for (int j = 0; j < this.gvRetInfo.Rows.Count; j++)
                {
                    double itmqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRetInfo.Rows[j].FindControl("lblgvitmqty")).Text.Trim()));
                    double rate = Convert.ToDouble(ASTUtility.ExprToValue(((TextBox)this.gvRetInfo.Rows[j].FindControl("Txtrate")).Text.Trim()));
                    double itmAmt = itmqty * rate;

                    index = (this.gvRetInfo.PageIndex) * this.gvRetInfo.PageSize + j;

                    lst[index].itmqty = itmqty;
                    lst[index].rate = rate;
                    lst[index].itmamt = itmAmt;
                }


                ViewState["tblPurReturn"] = lst;
            }

            catch (Exception ex)
            {

            }
        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission!');", true);

                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            this.SaveValue();
            List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn> lst = (List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>)ViewState["tblPurReturn"];

            if (this.ddlPrevList.Items.Count == 0)
                this.GetSaRetNo();
            string mRETNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("yyyy") + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string mRETDAT = this.GetStdDate(this.txtCurDate.Text.Trim());
            string sactcode = this.ddlStore.SelectedValue.ToString();
            string refno = this.txtRefno.Text.Trim();
            string mNAR = this.txtBillNarr.Text.Trim();
            string challan = this.txtCustomRef.Text.Trim();
            string ssircode = this.ddlSupplier.SelectedValue.ToString();

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPDATE_PURCHASE_RETURN", "RETPURB",
                             sactcode, mRETNO, mRETDAT, ssircode, usrid, sessionid, trmid, PostedDat, mNAR, challan, "", "", "", "",
                             "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('ERROR','" + purData.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }

            foreach (SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn c1 in lst)
            {
                string rsircode = c1.rsircode;
                string batchcode = c1.batchcode;
                string spcfcod = c1.spcfcod;
                double qty = Convert.ToDouble(c1.itmqty);
                double Amt = Convert.ToDouble(c1.itmamt);

                if (qty > 0)

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPDATE_PURCHASE_RETURN", "RETPURA", sactcode, mRETNO, rsircode, batchcode, spcfcod,
                                qty.ToString(), Amt.ToString());

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('ERROR','" + purData.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully!');", true);

                }
            }
        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            Data_Bind();
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetItems();
        }

        protected void ddlIItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mResCode = this.ddlIItems.SelectedValue.ToString() + "%";
            this.ddlSpecf.Items.Clear();
            string comcod = this.GetCompCode();
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION ", mResCode, "", "", "", "", "", "", "", "");

            DataView dv1 = ds3.Tables[0].DefaultView;

            DataTable dt = dv1.ToTable(true, "spcfcod", "spcfdesc");
            dt.Rows.Add("000000000000", "None");

            Session["tblresRes"] = ds3.Tables[0];

            this.ddlSpecf.DataTextField = "spcfdesc";
            this.ddlSpecf.DataValueField = "spcfcod";
            this.ddlSpecf.DataSource = dt;
            this.ddlSpecf.DataBind();
            this.ddlSpecf.SelectedValue = "000000000000";
            this.ddlSpecf_SelectedIndexChanged(null, null);


            this.GetSpecifications();
        }

        protected void ddlSpecf_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string matsircode = this.ddlIItems.SelectedValue;
            string matspecfcod = this.ddlSpecf.SelectedValue;
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSUPLRRESLISTMATWISE", matsircode, matspecfcod, "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count <= 0)
            {
                return;
            }
        }

        protected void gvRetInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvRetInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void LbtnAdd_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn> lst = (List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>)ViewState["tblPurReturn"];
            if (lst == null)
                return;
            string storid = this.ddlStore.SelectedValue.ToString();
            string stordesc = this.ddlStore.SelectedItem.ToString();
            string Supplier = this.ddlSupplier.SelectedValue.ToString();
            string rsircode = this.ddlIItems.SelectedValue.ToString();
            string rsirdesc = this.ddlIItems.SelectedItem.ToString();
            string spcfcod = this.ddlSpecf.SelectedValue.ToString();
            string spcfdesc = this.ddlSpecf.SelectedItem.ToString();
            DataTable dt = (DataTable)Session["tblresRes"];
            DataRow[] dr = dt.Select("sircode='" + rsircode + "' and spcfcod='" + spcfcod + "'");
            var list = lst.FindAll(p => p.rsircode == rsircode && p.spcfcod == spcfcod);
            if (list.Count == 0)
            {
                lst.Add(new SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn
                {
                    rsircode = rsircode,
                    spcfcod = spcfcod,
                    itmqty = 0.00,
                    rate = Convert.ToDouble(dr[0]["rate"]),
                    itmamt = 0.00,
                    rsirdesc = rsirdesc,
                    spcfdesc = spcfdesc,
                    centrid = storid,
                    centrdesc = stordesc,
                    ssircode = Supplier,
                    rsirunit = dr[0]["sirunit"].ToString(),
                    batchcode = "000000000000",
                    batchdesc = "",
                    remarks = "",
                });
            }


            ViewState["tblPurReturn"] = lst;
            this.Data_Bind();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".PNG")).AbsoluteUri;

            string returnNo = this.lblCurNo1.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("yyyy") + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string returnDate = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            string custmChlln = this.txtCustomRef.Text.Trim();
            string Narration = this.txtBillNarr.Text.Trim();

            //var lst = ds.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>();
            var lst = (List<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>)ViewState["tblPurReturn"];
            if (lst.Count == 0)
                return;

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptPurchaseReturn", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("ComLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Material Transfer Note"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("returndate", returnDate));
            rpt1.SetParameters(new ReportParameter("returnno", returnNo));
            rpt1.SetParameters(new ReportParameter("custmchlln", custmChlln));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
    }
}

