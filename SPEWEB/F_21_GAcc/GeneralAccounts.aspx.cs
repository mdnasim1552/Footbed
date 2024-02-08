using System;
using System.Collections;
using System.Collections.Generic;
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
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_22_Sal;
using System.Data.SqlClient;

namespace SPEWEB.F_21_GAcc
{
    public partial class GeneralAccounts : System.Web.UI.Page
    {
        public static double TAmount;

        ProcessAccess accData = new ProcessAccess();
        UserService userSer = new UserService();
        AutoCompleted AutoData = new AutoCompleted();

        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = (Convert.ToBoolean(dr1[0]["entry"]));

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Attributes.Add("onClick", " javascript:return confirm('You sure you want to Save the record?');");
                Session.Remove("tblvoucher");
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CalendarExtender1.EndDate = System.DateTime.Today;
                this.TableCreate();
                this.GetRecAndPayto();
                this.CompanyPost();
                this.CurrencyInf();
                this.CommonButton();
                this.LoadAcccombo();
                this.GetAccHead();
                this.VoucherType();


                string vounum = this.Request.QueryString["vounum"].ToString().Trim();
                if (vounum.Length > 0)
                {
                    this.ddlvoucher.SelectedValue = vounum.Substring(0, 2);
                    this.GetPriviousVoucher();
                }

                string selectedvoucher = ddlvoucher.SelectedValue;

                if(selectedvoucher == "BD" || selectedvoucher == "CD")
                {
                    this.lblCurr.Visible = true;
                    this.ddlCurrency.Visible = true;
                    this.HyperLink1.Visible = true;
                    this.lblConv.Visible = true;
                    this.lblConRate.Visible = true;
                }

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkAcCodebook_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkDetails_Click);



        }
        private void CommonButton()
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = true;
            //((CheckBox)this.Master.FindControl("CheckBox1")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Accounts Code";
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Details Code";
            ((CheckBox)this.Master.FindControl("chkBoxN")).Text = "Cheque Print";
            ((CheckBox)this.Master.FindControl("CheckBox1")).Text = "A/C Payee";


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetBalanceInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = ddlacccode.SelectedValue.ToString();
            string rescode = ddlresuorcecode.Items.Count == 0 ? "000000000000" : ddlresuorcecode.SelectedValue.ToString();
            string date1 = System.DateTime.Now.ToString("dd-MMM-yyyy");

            string acHead = this.ddlConAccHead.SelectedValue.ToString();


            DataSet dsdata = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "BANKACCOUNTBALANCE", acHead);



            string CALLTYPE = (rescode == "000000000000") ? "ACCOUNTSLEDGERSUM" : "ACCOUNTSLEDGERSUB";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", CALLTYPE, actcode, date1, date1, rescode, "", "", "", "", "");

            DataTable dt = ds1.Tables[2];

            DataTable dtbnk = dsdata.Tables[0];
            this.bankBalance.Text = dtbnk.Rows.Count==0?"": ("Tk " + Convert.ToDouble(dtbnk.Rows[0]["balam"]).ToString("#,##0;(#,##0); ") + "/-");

            this.lblBalance.Text =dt.Rows.Count==0?"": (Convert.ToDouble(dt.Rows[0]["balam"]).ToString("#,##0;(#,##0); ") + "/-");



        }

        private void VoucherTypeSelected()
        {
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            string vno = ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "JV" : ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "CT" :
                           ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "PA" : "RP";


            switch (vno)
            {
                case "JV":
                    this.ddlvoucher.SelectedValue = "JV";
                    this.ddlvoucher.Visible = false;
                    break;


                case "PA":
                    this.ddlvoucher.Items.Remove("JV");
                    this.ddlvoucher.Items.Remove("BC");
                    this.ddlvoucher.Items.Remove("CC");
                    this.ddlvoucher.Items.Remove("CT");
                    break;
                case "RP":
                    this.ddlvoucher.Items.Remove("JV");
                    this.ddlvoucher.Items.Remove("BD");
                    this.ddlvoucher.Items.Remove("CD");
                    this.ddlvoucher.Items.Remove("CT");
                    break;

                case "CT":
                    this.ddlvoucher.SelectedValue = "CT";
                    this.ddlvoucher.Visible = false;
                    break;


                default:
                    break;
            }


        }


        private void CompanyPost()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "3332":

                    this.chkpost.Checked = true;
                    break;

                default:
                    this.chkpost.Checked = false;
                    break;
            }


        }


        public void GetRecAndPayto()
        {
            Session.Remove("tblrecandPayto");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            AutoData.GetRecAndPayto(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", "", "", "", "", "", "", "", "", "");

        }

       
        protected void lnkAcCodebook_Click(object sender, EventArgs e)
        {
            //this.CalculatrGridTotal();
        }
        protected void lnkDetails_Click(object sender, EventArgs e)
        {
            //this.CalculatrGridTotal();
        }

        private void Visibility()
        {
            if (this.Request.QueryString["Mod"] == "Accounts")
            {
                this.lblPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.txtScrchPre.Visible = false;
            }

            else
            {
                // this.ibtnFindPrv_Click(null, null);

            }

        }

        //private string CompanyPrintVou()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string vouprint = "";
        //    switch (comcod)
        //    {

        //        case "2305":
        //            vouprint = "VocherPrint4";
        //            break;

        //        case "3306":
        //        case "3307":
        //        case "3308":
        //            vouprint = "VocherPrint1";
        //            break;
        //        case "3305":
        //        case "3310":
        //        case "3311":
        //            vouprint = "VocherPrint2";
        //            break;
        //        case "3309":
        //            vouprint = "VocherPrint3";
        //            break;
        //        default:
        //            vouprint = "VocherPrint";
        //            break;
        //    }
        //    return vouprint;
        //}
        private void GetPriviousVoucher()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            //string date = this.txtEntryDate.Text.Substring(0, 11);

            //string VNo3 = this.ddlvoucher.SelectedValue.ToString();
            //string vtcode = (VNo3 == "CT") ? "92" : "99";
            //string vounum = "%" + this.txtScrchPre.Text + "%";
            //DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRIVOUSVOUCHER", VNo3, vtcode, date, vounum, "", "", "", "", "");

            //this.ddlPrivousVou.DataSource = ds5.Tables[0];
            //this.ddlPrivousVou.DataTextField = "vounum1";
            //this.ddlPrivousVou.DataValueField = "vounum";
            //this.ddlPrivousVou.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();

            string date = this.txtEntryDate.Text.Substring(0, 11);



            string VNo3 = this.ddlvoucher.SelectedValue.ToString();
            string vtcode = (VNo3 == "CT") ? "92" : "99";

            string vounum = (this.Request.QueryString["vounum"].ToString().Trim() != "" ? this.Request.QueryString["vounum"].ToString().Trim() : "%" + this.txtScrchPre.Text.Trim()) + "%";
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRIVOUSVOUCHER", VNo3, vtcode, date, vounum, "", "", "", "", "");

            this.ddlPrivousVou.DataSource = ds5.Tables[0];
            this.ddlPrivousVou.DataTextField = "vounum1";
            this.ddlPrivousVou.DataValueField = "vounum";
            this.ddlPrivousVou.DataBind();

            this.ddlvoucher_SelectedIndexChanged(null, null);
        }
        protected void ddlvoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.ddlvoucher.SelectedItem.Value;

            if (value == "BD" || value == "CD")
            {
                this.lblCurr.Visible = true;
                this.ddlCurrency.Visible = true;
                this.HyperLink1.Visible = true;
                this.lblConv.Visible = true;
                this.lblConRate.Visible = true;
            }
            else
            {
                this.lblCurr.Visible = false;
                this.ddlCurrency.Visible = false;
                this.HyperLink1.Visible = false;
                this.lblConv.Visible = false;
                this.lblConRate.Visible = false;
            }

            this.VoucherType();
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
            tblt01.Columns.Add("recndt", Type.GetType("System.String"));
            tblt01.Columns.Add("rpcode", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("unit", Type.GetType("System.String"));

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
            tblt02.Columns.Add("recndt", Type.GetType("System.String"));
            tblt02.Columns.Add("rpcode", Type.GetType("System.String"));
            tblt02.Columns.Add("billno", Type.GetType("System.String"));
            tblt02.Columns.Add("unit", Type.GetType("System.String"));

            Session["tblt02"] = tblt02;
            //actcode,subcode,spclcode,actdesc,subdesc,spcldesc,trnqty,trnrate,trndram,trncram,trnrmrk
        }
        private void LoadAcccombo()
        {
            try
            {
                Session.Remove("tblbank");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string ttsrch =  "%";
                string UserId = hst["usrid"].ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONHEADPAONPAY", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
                Session["tblbank"] = ds1.Tables[0];
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        private void GetBillNo()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = this.ddlacccode.SelectedValue.ToString();
                string supcode = this.ddlresuorcecode.SelectedValue.ToString();
                string ttsrch =  "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBILLNO", pactcode, supcode, ttsrch, "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);

                    return;
                }
                this.ddlBillList.DataSource = ds1.Tables[0];
                this.ddlBillList.DataTextField = "textfield";
                this.ddlBillList.DataValueField = "valfield";
                this.ddlBillList.DataBind();
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        private void GetAccHead()
        {
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            string vtname = this.ddlvoucher.SelectedItem.Text;
            string vounum = ((vtname == "Contra Voucher") ? "CT" : (vtname == "Journal Voucher" ? "JV" : ""));

            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>();
            lst = userSer.GetActHead("%", accconhead, vounum);




            Session["HeadAcc1"] = lst;
        }
        protected void lnkAcccode_Click(object sender, EventArgs e)
        {


            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)Session["HeadAcc1"];

            string vounum = this.ddlvoucher.SelectedValue.ToString();


            lst = (vounum == "JV") ? lst : lst.FindAll((p => p.actcode != this.ddlConAccHead.SelectedValue.ToString()));

            string SrchProject = "";// this.txtserceacc.Text.Trim();


            if (SrchProject.Length > 0)
            {

                IEnumerable<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> ProjectQuery;

                if (SrchProject.All(char.IsDigit))
                {
                    int len = SrchProject.Length;
                    ProjectQuery = (from project in lst
                                    where project.actcode.Substring(0, len).Equals(SrchProject)
                                    orderby project.actcode ascending
                                    select project);

                }

                else
                {

                    ProjectQuery = (from project in lst
                                    where project.actdesc.ToUpper().Contains(SrchProject.ToUpper())
                                    orderby project.actcode ascending
                                    select project);
                }


                this.ddlacccode.DataTextField = "actdesc";
                this.ddlacccode.DataValueField = "actcode";
                this.ddlacccode.DataSource = ProjectQuery;
                this.ddlacccode.DataBind();

            }


            else
            {
                this.ddlacccode.DataTextField = "actdesc";
                this.ddlacccode.DataValueField = "actcode";
                this.ddlacccode.DataSource = lst;
                this.ddlacccode.DataBind();
            }


            //----Show Resource code and Specification Code------------// 

            // DataTable dt01 = (DataTable)Session["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst1 = lst.FindAll((p => p.actcode == search1));

            if (lst1.Count == 0)
                return;


            if (lst1[0].actelev.ToString() == "2")
            {
                //this.lblrescode.Visible = true;
                //this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.HylSubAcc.Visible = true;
                this.ddlresuorcecode.Visible = true;

                //this.lblspecification.Visible = true;
                //this.txtSearchSpeci.Visible = true;
                this.lnkSpecification.Visible = true;
                this.ddlSpclinf.Visible = true;

                this.lblqty.Visible = true;
                this.txtqty.Visible = true;
                this.lblrate.Visible = true;
                this.txtrate.Visible = true;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                string actcode = this.ddlacccode.SelectedValue.ToString().Substring(0, 2);
                // if (actcode == "18" || actcode == "24" || actcode == "25" || actcode == "25" || actcode == "18" || this.ddlresuorcecode.Items.Count==0)
                this.GetResCode();
            }
            else
            {
                //this.lblrescode.Visible = false;
                //this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.HylSubAcc.Visible = false;
                this.ddlresuorcecode.Visible = false;

                //this.lblspecification.Visible = false;
                //this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;

                this.lblqty.Visible = false;
                this.txtqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtrate.Visible = false;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                this.ddlSpclinf.Items.Clear();
                this.ddlresuorcecode.Items.Clear();


            }
            //---------------------------------------------//
            // this.txtserceacc.Text = "";
        }
        protected void lnkRescode_Click(object sender, EventArgs e)
        {
            this.GetResCode();
        }

        private void GetResCode()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = this.ddlacccode.SelectedValue.ToString();
                string filter1 = "%"; //+ this.txtserchReCode.Text.Trim() + "%";

                string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();


                string SearchInfo = "";
                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lstacc = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)Session["HeadAcc1"];

                string search1 = this.ddlacccode.SelectedValue.ToString().Trim();

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lstacc1 = lstacc.FindAll((p => p.actcode == search1));

                //if (lst1.Count == 0)
                //    return;

                //DataRow[] drac = dt01.Select("actcode='" + search1 + "'");
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

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = '" + ar1 + "' ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHead(actcode, filter1, SearchInfo);
                Session["HeadRsc1"] = lst;

                this.ddlresuorcecode.DataSource = lst;
                this.ddlresuorcecode.DataTextField = "resdesc1";
                this.ddlresuorcecode.DataValueField = "rescode";
                this.ddlresuorcecode.DataBind();
                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst1 = lst.FindAll((p => p.rescode == oldRescode));
                if (lst1.Count > 0)
                {
                    this.ddlresuorcecode.SelectedValue = oldRescode;


                }
                this.ddlresuorcecode_SelectedIndexChanged(null, null);




                //this.txtserchReCode.Text = "";
                string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst2 = lst.FindAll((p => p.rescode == seaRes));
                if (lst2.Count == 0)
                    return;

                if (ASTUtility.Left(lst2[0].rescode, 1) == "9")
                {
                    //this.lblbillno.Visible = true;
                    //this.txtserchBill.Visible = true;
                    this.lnkBillNo.Visible = true;
                    this.ddlBillList.Visible = true;


                }
                else
                {
                    //this.lblbillno.Visible = false;
                    //this.txtserchBill.Visible = false;
                    this.lnkBillNo.Visible = false;
                    this.ddlBillList.Visible = false;
                    this.ddlBillList.Items.Clear();

                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }



           
        }








        protected void lnkSpecification_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = this.ddlacccode.SelectedValue.ToString();
            string rescode = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            string filter2 =  "%";
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSPCILINFCODE", filter2, rescode, actcode, "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            this.ddlSpclinf.DataSource = dt4;
            this.ddlSpclinf.DataTextField = "spdesc1";
            this.ddlSpclinf.DataValueField = "spcod";
            this.ddlSpclinf.DataBind();
            //this.txtSearchSpeci.Text = "";
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
            //this.txtserchReCode.Text = "";
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>)Session["HeadRsc1"];
            string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst2 = lst.FindAll((p => p.rescode == seaRes));
            if (lst2.Count == 0)
                return;

            if (ASTUtility.Left(lst2[0].rescode, 1) == "9")
            {
                //this.lblbillno.Visible = true;
                //this.txtserchBill.Visible = true;
                this.lnkBillNo.Visible = true;
                this.ddlBillList.Visible = true;


            }
            else
            {
                //this.lblbillno.Visible = false;
                //this.txtserchBill.Visible = false;
                this.lnkBillNo.Visible = false;
                this.ddlBillList.Visible = false;
                this.ddlBillList.Items.Clear();

            }


            lnkSpecification_Click(null, null);
            this.GetBillNo();
            this.GetBalanceInfo();

        }
        protected void ddlSpclinf_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSpclinf.BackColor = System.Drawing.Color.Pink;
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {

            if (this.lnkOk.Text == "Ok" || this.jVOkbtn.Text == "Ok")
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                // string newvoumum = "NEW";
                string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                DataSet _NewDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
                Session["UserLog"] = _NewDataSet.Tables[1];
                Session.Remove("tblvoucher");
                if (this.ddlPrivousVou.Items.Count > 0)
                {
                    //DataSet _NewDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", newvoumum, "", "", "", "", "", "", "", "");
                    //Session["UserLog"] = _NewDataSet.Tables[1];

                    DataTable dtuser = (DataTable)Session["UserLog"];
                    string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
                    string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "EDITVOUCHER" : (this.chkpost.Checked) ? "EDITUNVOUCHER" : "EDITVOUCHER";
                    vounum = this.ddlPrivousVou.SelectedValue.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", CallType, vounum, "", "", "", "", "", "", "", "");
                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);
                    //this.dgv1.DataSource = dt;
                    Session["tblvoucher"] = dt;
                    if (dt.Rows.Count == 0)
                        return;

                    this.Data_Bind();
                    //this.dgv1.DataBind();

                    Session["UserLog"] = _EditDataSet.Tables[1];
                    //-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];

                    if (vounum.Substring(0, 2).ToString() != "JV")
                    {
                        //this.txtScrchConCode.Text = "";
                        //this.txtScrchConCode.Visible = true;
                        this.ibtnFindConCode.Visible = true;
                        this.LoadAcccombo();
                        this.ddlConAccHead.SelectedValue = dtedit.Rows[0]["cactcode"].ToString();
                    }
                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                    this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
                    this.txtRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
                    this.txtSrinfo.Text = dtedit.Rows[0]["srinfo"].ToString();
                    this.txtPayto.Text = dtedit.Rows[0]["payto"].ToString();
                    this.txtBankNam.Text = dtedit.Rows[0]["banknam"].ToString();

                    this.txtNarration.Text = dtedit.Rows[0]["venar"].ToString();
                    //   this.txtEntryDate.Enabled = false;
                    //-------------------------------------------------//
                    this.pnlNarration.Visible = true;
                    this.lblcurVounum.Text = "Edit Voucher No.";
                    string cvno1 = this.ddlPrivousVou.SelectedValue.ToString().Substring(0, 8);
                    this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                    this.txtCurrntlast6.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(8);
                    this.txtCurrntlast6.Enabled = false;
                    this.CalculatrGridTotal();

                }
                else
                {

                    this.txtEntryDate.Enabled = true;
                    this.txtCurrntlast6.Enabled = true;


                    // Previous Nerration
                    double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                    string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
                    string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "C" :
                        (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
                    string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "T" : "C")));
                    string VNo3 = Convert.ToString(VNo1 + VNo2);

                    string date = this.txtEntryDate.Text;

                    DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "LASTNARRATION", VNo3, date, "", "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows.Count == 0)
                        this.txtNarration.Text = "";
                    else
                        this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
                    //---------------------

                    this.GetVouCherNumber();

                    if (VNo3 == "BD" || VNo3 == "CT")
                    {
                        this.ChequeNo();
                    }

                }
                this.lblPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.txtScrchPre.Visible = false;

                if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit"))
                {
                   // this.txtScrchConCode.Visible = true;
                    //this.ibtnFindConCode.Visible = false;

                    //((CheckBox)this.Master.FindControl("chkBoxN")).Checked = false;
                    this.chkPrint.Checked = false;

                }
                // ((CheckBox)this.Master.FindControl("chkBoxN")).Visible
                this.chkPrint.Visible = ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? true : ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? true
                         : ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit") ? true : false;
                this.Panel2.Visible = true;

                this.lnkOk.Text = "New";
                this.jVOkbtn.Text = "New";
                //this.txtserceacc.Focus();
                this.lnkAcccode_Click(null, null);
                GetBalanceInfo();
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = true;
            }
            else
            {
                this.lnkOk.Text = "Ok";
                this.jVOkbtn.Text = "Ok";


                this.txtCurrntlast6.Enabled = false;
                //((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.Panel2.Visible = false;
                //this.lblrescode.Visible = false;
                //this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.HylSubAcc.Visible = false;
                this.ddlresuorcecode.Visible = false;
                //this.txtSearchSpeci.Visible = false;
                //this.lblspecification.Visible = false;
                //this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;
                this.lblqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtqty.Visible = false;
                this.txtrate.Visible = false;
                //this.txtserchBill.Visible = false;
                this.lnkBillNo.Visible = false;
                this.ddlBillList.Visible = false;
                this.pnlNarration.Visible = false;
                dgv1.DataSource = null;
                dgv1.DataBind();

                if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit"))
                {
                    //this.txtScrchConCode.Visible = true; 
                    // this.ibtnFindConCode.Visible = true;
                    //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
                    this.chkPrint.Visible = false;
                }
                if (this.Request.QueryString["Mod"] == "Accounts")
                {
                    this.lblPrivVou.Visible = false;
                    this.ddlPrivousVou.Visible = false;
                    this.ibtnFindPrv.Visible = false;
                    this.txtScrchPre.Visible = false;

                }
                else
                {
                    this.lblPrivVou.Visible = true;
                    this.ddlPrivousVou.Visible = true;
                    this.ibtnFindPrv.Visible = true;
                    this.txtScrchPre.Visible = true;

                }
                this.lblcurVounum.Text = "Voucher No.";
                this.txtcurrentvou.Text = "";
                this.txtCurrntlast6.Text = "";
                this.txtEntryDate.Enabled = true;
                this.txtRefNum.Text = "";
                this.txtBankNam.Text = "";
                this.ddlcheque.Items.Clear();
                this.ddlacccode.BackColor = System.Drawing.Color.White;
                this.ddlresuorcecode.BackColor = System.Drawing.Color.White;
                this.ddlSpclinf.BackColor = System.Drawing.Color.White;
                //this.txtScrchConCode.Focus();
                this.Refrsh();

            }
        }
        private void ChequeNo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = this.ddlConAccHead.SelectedValue.ToString();
            string flag = "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "TOPCHEQUE", txtSProject, flag, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
               
                return;
            }
            this.ddlcheque.DataTextField = "chequeno";
            this.ddlcheque.DataValueField = "chequeno";
            this.ddlcheque.DataSource = ds1.Tables[0];
            Session["tblchequeno"] = ds1.Tables[0];
            this.ddlcheque.DataBind();
            this.ddlcheque_SelectedIndexChanged(null, null);

        }
        private void Refrsh()
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Text = "";
            //this.lblmsg.Visible = false;

            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.ddlPrivousVou.Items.Clear();
            this.ddlacccode.Items.Clear();
            this.ddlSpclinf.Items.Clear();
            this.ddlresuorcecode.Items.Clear();
            this.ddlBillList.Items.Clear();
            //this.txtserceacc.Text = "";
            //this.txtserchReCode.Text = "";
            //this.txtSearchSpeci.Text = "";
            this.txtDrAmt.Text = "";
            this.txtCrAmt.Text = "";
            this.txtqty.Text = "";
            this.txtrate.Text = "";
            this.txtremarks.Text = "";
            this.txtSrinfo.Text = "";
            this.txtRefNum.Text = "";
            this.txtPayto.Text = "";
            this.txtNarration.Text = "";
            this.lblisunum.Text = "";
            //this.txtBankNam.Text = "";

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
                this.pnlNarration.Visible = true;
                string AccCode = this.ddlacccode.SelectedValue.ToString();
                string ResCode = this.ddlresuorcecode.SelectedValue.ToString();
                string Billno = this.ddlBillList.Items.Count > 0 ? this.ddlBillList.SelectedValue.ToString() : "";
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);


                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>)Session["HeadRsc1"];
                var resunit = (lst == null) ? "" : ((lst.FindAll(l => l.rescode == ResCode).Count == 0) ? "" : lst.FindAll(l => l.rescode == ResCode)[0].resunit);

                string actlev = ((List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)Session["HeadAcc1"]).FindAll(p => p.actcode == AccCode)[0].actelev.ToString();

                // string actlev = (((DataTable)Session["HeadAcc1"]).Select("actcode='" + AccCode + "'"))[0]["actelev"].ToString();
                if (actlev == "2")
                {
                    if (ResCode == "000000000000")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Details Head');", true);
                        //this.txtserchReCode.Focus();
                        return;

                    }

                }



                string billno = this.ddlBillList.SelectedValue.ToString();

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
                int cindex = -1;
                for (int i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    string dgAccCode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string dgResCode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string dgBillno = ((TextBox)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();
                    string dgspclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string dgSpclCode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string dgAccDesc = ((Label)this.dgv1.Rows[i].FindControl("lblAccdesc")).Text.Trim();
                    string dgResDesc = ((Label)this.dgv1.Rows[i].FindControl("lblResdesc")).Text.Trim();
                    string dgSpclDesc = ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text.Trim();
                    string gvunit = ((Label)this.dgv1.Rows[i].FindControl("lbgvunit")).Text.Trim();
                    double dgTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                    double dgTrnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                    double dgTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                    double dgTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                    string dgTrnRemarks = ((Label)this.dgv1.Rows[i].FindControl("lblgvRemarks")).Text.Trim();
                    string recndt = ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text;
                    string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text;
                    string billno1 = ((TextBox)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text;

                    //-----------If Repetation ---------------------------------------------------------//
                    if (dgAccCode + dgResCode + dgBillno + dgspclcode == AccCode + ResCode + Billno + SpclCode)
                    {

                        ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text = SpclCode;
                        ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text = SpclDesc;
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = dgTrnQty.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = dgTrnrate.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = dgTrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = dgTrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.dgv1.Rows[i].FindControl("lblgvRemarks")).Text = TrnRemarks;
                        ((TextBox)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text = billno;
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
                        dr1["recndt"] = recndt;
                        dr1["rpcode"] = rpcode;
                        dr1["billno"] = billno1;
                        dr1["unit"] = gvunit;
                        tblt01.Rows.Add(dr1);
                        cindex = i;

                    }
                    else
                    {

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
                        dr1["recndt"] = recndt;
                        dr1["rpcode"] = rpcode;
                        dr1["billno"] = billno1;
                        dr1["unit"] = gvunit;
                        tblt01.Rows.Add(dr1);
                    }
                }

                // New Row Add 

                string aresbandspclcode = AccCode + ResCode + Billno + SpclCode;
                DataRow[] drad = tblt01.Select("actcode+subcode+billno+spclcode='" + aresbandspclcode + "'");
                if (drad.Length == 0)
                {
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
                    dr2["recndt"] = "";
                    dr2["rpcode"] = "";
                    dr2["billno"] = billno;
                    dr2["unit"] = resunit;
                    tblt01.Rows.Add(dr2);


                }







                //--------------** Remove Duplicate Value **----------------------------//
                //--** Only Actdesc remove actcod not remove from grid **---------------// 
                //DataView dv1 = tblt01.DefaultView;
                //dv1.Sort = "actcode";
                //tblt03 = dv1.ToTable();

                tblt03 = tblt01;

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
                    dr3["recndt"] = tblt03.Rows[j]["recndt"].ToString();
                    dr3["billno"] = tblt03.Rows[j]["billno"].ToString();
                    dr3["unit"] = tblt03.Rows[j]["unit"].ToString();
                    tblt02.Rows.Add(dr3);

                }

                Session.Remove("tblvoucher");
                Session["tblvoucher"] = tblt02;
                //---------------------------------------------//
                dgv1.DataSource = tblt02;
                dgv1.DataBind();


                // Index Coloring 
                if (cindex >= 0)
                    this.dgv1.Rows[cindex].Attributes["style"] = "background-color:#3399FF; font-weight:bold;";
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
               

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

            //    string billno = this.ddlBillList.SelectedValue.ToString();

            //    string SpclCode = this.ddlSpclinf.SelectedValue.ToString();
            //    SpclCode = (SpclCode.Length < 12 ? "000000000000" : SpclCode);
            //    string AccDesc = this.ddlacccode.SelectedItem.Text.Trim();
            //    string ResDesc = (ResCode == "000000000000" ? "" : this.ddlresuorcecode.SelectedItem.Text.Trim());
            //    string SpclDesc = (SpclCode == "000000000000" ? "" : this.ddlSpclinf.SelectedItem.Text.Trim().Substring(13));
            //    double TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
            //    double Trnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
            //    double TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
            //    double TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtCrAmt.Text.Trim()));
            //    string TrnRemarks = this.txtremarks.Text.Trim();
            //    DataTable tblt01 = (DataTable)Session["tblt01"];
            //    DataTable tblt02 = (DataTable)Session["tblt02"];
            //    DataTable tblt03 = new DataTable();
            //    tblt01.Rows.Clear();
            //    tblt02.Rows.Clear();
            //    tblt03.Rows.Clear();

            //    for (int i = 0; i < this.dgv1.Rows.Count; i++)
            //    {
            //        string dgAccCode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
            //        string dgResCode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
            //        string dgBillno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();
            //        string dgspclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
            //       // string gvunit = ((Label)this.dgv1.Rows[i].FindControl("lbgvunit")).Text.Trim();
            //        //-----------If Repetation ---------------------------------------------------------//
            //        if (dgAccCode + dgResCode + dgBillno + dgspclcode == AccCode + ResCode + Billno + SpclCode)
            //        {
            //            ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text = SpclCode;
            //            ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text = SpclDesc;
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = TrnQty.ToString("#,##0.00;(#,##0.00); ");
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = Trnrate.ToString("#,##0.00;(#,##0.00); ");
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
            //            ((Label)this.dgv1.Rows[i].FindControl("lblgvRemarks")).Text = TrnRemarks;
            //            ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text = billno;
            //          //  ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text = billno;


            //            //this.CalculatrGridTotal();
            //            //return;
            //        }
            //        else
            //        {
            //            //--------------------------------------------------------------------------------//
            //            string dgSpclCode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
            //            string dgAccDesc = ((Label)this.dgv1.Rows[i].FindControl("lblAccdesc")).Text.Trim();
            //            string dgResDesc = ((Label)this.dgv1.Rows[i].FindControl("lblResdesc")).Text.Trim();
            //            string dgSpclDesc = ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text.Trim();
            //            double dgTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
            //            double dgTrnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
            //            double dgTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
            //            double dgTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
            //            string dgTrnRemarks = ((Label)this.dgv1.Rows[i].FindControl("lblgvRemarks")).Text.Trim();
            //            string recndt = ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text;
            //            string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text;
            //            string billno1 = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text;
            //            //actcode subcode actdesc subdesc trnqty trnrate trndram trncram trnrmrk 
            //            DataRow dr1 = tblt01.NewRow();
            //            dr1["actcode"] = dgAccCode;
            //            dr1["subcode"] = dgResCode;
            //            dr1["spclcode"] = dgSpclCode;
            //            dr1["actdesc"] = dgAccDesc;
            //            dr1["subdesc"] = dgResDesc;
            //            dr1["spcldesc"] = dgSpclDesc;
            //            dr1["trnqty"] = dgTrnQty;
            //            dr1["trnrate"] = dgTrnrate;
            //            dr1["trndram"] = dgTrnDrAmt;
            //            dr1["trncram"] = dgTrnCrAmt;
            //            dr1["trnrmrk"] = dgTrnRemarks;
            //            dr1["recndt"] = recndt;
            //            dr1["rpcode"] = rpcode;
            //            dr1["billno"] = billno1;
            //            tblt01.Rows.Add(dr1);
            //        }
            //    }
            //    DataRow dr2 = tblt01.NewRow();
            //    dr2["actcode"] = AccCode;
            //    dr2["subcode"] = ResCode;
            //    dr2["spclcode"] = SpclCode;
            //    dr2["actdesc"] = AccDesc;
            //    dr2["subdesc"] = ResDesc;
            //    dr2["spcldesc"] = SpclDesc;
            //    dr2["trnqty"] = TrnQty;
            //    dr2["trnrate"] = Trnrate;
            //    dr2["trndram"] = TrnDrAmt;
            //    dr2["trncram"] = TrnCrAmt;
            //    dr2["trnrmrk"] = TrnRemarks;
            //    dr2["recndt"] = "";
            //    dr2["rpcode"] = "";
            //    dr2["billno"] = billno;
            //    tblt01.Rows.Add(dr2);
            //    //--------------** Remove Duplicate Value **----------------------------//
            //    //--** Only Actdesc remove actcod not remove from grid **---------------// 
            //    //DataView dv1 = tblt01.DefaultView;
            //    //dv1.Sort = "actcode";
            //    //tblt03 = dv1.ToTable();

            //    tblt03 = tblt01;

            //    string AccDesc1 = null;
            //    for (int j = 0; j < tblt03.Rows.Count; j++)
            //    {
            //        DataRow dr3 = tblt02.NewRow();
            //        dr3["actcode"] = tblt03.Rows[j]["actcode"].ToString();
            //        dr3["subcode"] = tblt03.Rows[j]["subcode"].ToString();
            //        dr3["spclcode"] = tblt03.Rows[j]["spclcode"].ToString();
            //        string tserch = tblt03.Rows[j]["actcode"].ToString();
            //        if (tserch == AccDesc1 || tserch == "")
            //        {
            //            dr3["actdesc"] = "";
            //        }
            //        else
            //        {
            //            dr3["actdesc"] = tblt03.Rows[j]["actdesc"].ToString();
            //            AccDesc1 = tblt03.Rows[j]["actcode"].ToString();
            //        }

            //        dr3["subdesc"] = tblt03.Rows[j]["subdesc"].ToString();
            //        dr3["spcldesc"] = tblt03.Rows[j]["spcldesc"].ToString();
            //        dr3["trnqty"] = Convert.ToDouble(tblt03.Rows[j]["trnqty"].ToString());
            //        dr3["trnrate"] = Convert.ToDouble(tblt03.Rows[j]["trnrate"].ToString());
            //        dr3["trndram"] = Convert.ToDouble(tblt03.Rows[j]["trndram"].ToString());
            //        dr3["trncram"] = Convert.ToDouble(tblt03.Rows[j]["trncram"].ToString());
            //        dr3["trnrmrk"] = tblt03.Rows[j]["trnrmrk"].ToString();
            //        dr3["recndt"] = tblt03.Rows[j]["recndt"].ToString();
            //        dr3["billno"] = tblt03.Rows[j]["billno"].ToString();
            //        tblt02.Rows.Add(dr3);

            //    }

            //    Session.Remove("tblvoucher");
            //    Session["tblvoucher"] = tblt02;
            //    //---------------------------------------------//
            //    dgv1.DataSource = tblt02;


            //    dgv1.DataSource = tblt01;
            //    //this.Data_Bind();
            //    dgv1.DataBind();
            //    this.CalculatrGridTotal();
            //    //this.ddlacccode.BackColor = System.Drawing.Color.Beige;
            //    //this.ddlresuorcecode.BackColor = System.Drawing.Color.Beige;
            //    //this.ddlSpclinf.BackColor = System.Drawing.Color.Beige;
            //    this.txtDrAmt.Text = "";
            //    this.txtCrAmt.Text = "";
            //    this.txtqty.Text = "";
            //    this.txtrate.Text = "";
            //    this.txtremarks.Text = "";
            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblvoucher"];
            dgv1.DataSource = dt;
            dgv1.DataBind();
            string comcod = this.GetCompCode();
            string billno = dt.Rows[0]["billno"].ToString();
            string trnrmrk = dt.Rows[0]["trnrmrk"].ToString();
            switch (comcod)
            {
                case "3330":
                case "3333":
                    break;



                default:
                    // ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (billno == "" && trnrmrk == "");

                    break;



            }



            // ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnUpdateBill")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");

            this.CalculatrGridTotal();
        }
        protected void CalculatrGridTotal()
        {
            double TQty = 0.00;
            double TRate = 0.00;
            double TDrAmt = 0.00;
            double TCrAmt = 0.00;
            DataTable dt = (DataTable)Session["tblvoucher"];

            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                double dg1TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                double dg1TrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double dg1TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dg1TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                TQty = dg1TrnQty;
                //TRate += dg1TrnRate;
                TDrAmt += dg1TrnDrAmt;
                TCrAmt += dg1TrnCrAmt;
                dg1TrnCrAmt = dg1TrnDrAmt > 0 ? 0.00 : dg1TrnCrAmt;
                TRate = (TQty == 0) ? 0 : Math.Abs(dg1TrnDrAmt - dg1TrnCrAmt) / TQty;
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = dg1TrnQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = TRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = dg1TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = dg1TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");

                dt.Rows[i]["trnqty"] = dg1TrnQty;
                dt.Rows[i]["trnrate"] = TRate;
                dt.Rows[i]["trndram"] = dg1TrnDrAmt;
                dt.Rows[i]["trncram"] = dg1TrnCrAmt;

            }

            Session["tblvoucher"] = dt;

            if (this.dgv1.Rows.Count > 0)
            {
                //((TextBox)this.dgv1.FooterRow.FindControl("txtTgvQty")).Text = TQty.ToString("#,##0.00;(#,##0.00); ");
                //((TextBox)this.dgv1.FooterRow.FindControl("txtTgvRate")).Text = TRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text = TDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text = TCrAmt.ToString("#,##0.00;(#,##0.00); ");
            }

            double dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text);
            double cramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text);
            string vouchertype = this.txtcurrentvou.Text == "" ? "" : this.txtcurrentvou.Text.Substring(0, 2);
            this.lblInword.Text = (vouchertype == "JV") ? "" : dramt > 0 ? ASTUtility.Trans(dramt, 2) : cramt > 0 ? ASTUtility.Trans(cramt, 2) : "";
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.CalculatrGridTotal();
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
                case "9101":
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
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}


            this.CalculatrGridTotal();

            //if (this.txtcurrentvou.Text.Trim() != "")
            //{

            double ToDramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text.Trim());
            double ToCramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text.Trim());

            if (ToDramt == 0 && ToCramt == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Amount is not Available');", true);

                return;
            }

            //Log Entry

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Mod"] == "Accounts") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["Mod"] == "Accounts") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["Mod"] == "Accounts") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["Mod"] == "Accounts") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["Mod"] == "Accounts") ? "" : userid;
            string Editdat = (this.Request.QueryString["Mod"] == "Accounts") ? "01-Jan-1900" : System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string EditTermId = Terminal;
            string pounaction = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["pounaction"].ToString();
            string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string aprvtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            string aprvseson = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
            string aprvdat = (dtuser.Rows.Count == 0) ? "01-jan-1900" : dtuser.Rows[0]["aprvdat"].ToString();
            string rbankname = this.txtBankNam.Text.Trim();

            string Payto = this.txtPayto.Text.Trim();
            string isunum = this.lblisunum.Text.Trim();


            //string EditByid = (this.Request.QueryString["Mod"] == "Accounts") ? "" : (tblEditByid == "") ? userid : tblEditByid;


            string voudat = ASTUtility.DateFormat(this.txtEntryDate.Text);
            DateTime Bdate;
            bool dcon;
            Bdate = this.GetBackDate();
            if ((this.Request.QueryString["Mod"] == "Accounts"))
            {
                dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                    return;
                }
                this.GetVouCherNumber();
            }
            else
            {

                if ((this.Request.QueryString["Mod"] == "Management"))
                {
                    string comlimit = this.Companylimit();
                    if (comlimit.Length > 0)
                    {

                        dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                        if (!dcon)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is Equal or Greater then Transaction Limt');", true);
                            return;
                        }

                    }

                    if (this.txtCurrntlast6.Enabled)
                        this.GetVouCherNumber();
                };


            }

            //string voudat = this.txtEntryDate.Text.Substring(0, 11);





            //    if ((this.Request.QueryString["Mod"] == "Accounts"))
            //    {
            //        DateTime Bdate = this.GetBackDate();
            //        bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
            //        if (!dcon)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
            //            return;
            //        }
            //        this.GetVouCherNumber();
            //    }
            //    else
            //    {

            //        if ((this.Request.QueryString["Mod"] == "Management"))
            //        {

            //            if (this.txtCurrntlast6.Enabled)
            //                this.GetVouCherNumber();
            //        };


            // }
            //Ref Number
            // this.CheeckRefNumber();


            //Voucher Date Change

            if ((this.Request.QueryString["Mod"] == "Management"))
            {
                string cvounum = this.ddlPrivousVou.SelectedValue.ToString();

                DateTime frmdate, todate, tvoudat;
                frmdate = Convert.ToDateTime(cvounum.Substring(6, 2) + "/01/" + cvounum.Substring(2, 4));
                todate = Convert.ToDateTime(frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy") + " 12:00:00  AM");
                tvoudat = Convert.ToDateTime(voudat);


                if (tvoudat >= frmdate && tvoudat <= todate)
                    ;
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher can be eidited during the date range of that particular month');", true);
                    return;

                }


            };



            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                            this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            //string recivedbank = this.txtBankNam.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
            string vtcode = (vouno == "CT") ? "92" : "99";
            string voutype = (vtcode == "92") ? "Contra Voucher" : (vouno == "JV" ? "Journal Voucher" :
                             (vouno == "CD" ? "Cash Payment Voucher" :
                             (vouno == "BD" ? "Bank Payment Voucher" :
                             (vouno == "CC" ? "Cash Deposit Voucher" :
                             (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));


            string cactcode = (vouno == "JV" ? "000000000000" : this.ddlConAccHead.SelectedValue.ToString());

            string Vou2 = ASTUtility.Left(this.txtcurrentvou.Text.Trim(), 2);
            switch (Vou2)
            {
                case "BD":
                case "CT":


                    if (this.txtRefNum.Text.Trim() == "" && cactcode.Substring(0, 4) != "1901")
                    {


                        DataTable dt = (DataTable)Session["tblvoucher"];

                        var results = (from srchrow in dt.AsEnumerable()
                                       where srchrow.Field<string>("actcode").Substring(0, 2) == "71"
                                       select srchrow);
                        dt = results.AsDataView().ToTable();


                        if (dt.Rows.Count > 0)
                        {

                            ;

                        }



                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
                            return;
                        }


                    }
                    break;
            }





            string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");
            string TgvDrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text;
            string TgvCrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text;
            if (vouno == "JV" && TgvDrAmt != TgvCrAmt)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Dr. Amount not equals to Cr. Amount.');", true);
                return;
            }
            try
            {


                if ((this.Request.QueryString["Mod"] == "Accounts"))
                {
                    if ((vouno == "BD" || vouno == "CT") && cactcode.Substring(0, 4) != "1901")
                    {

                        if (refnum.Any(char.IsLetter))

                        { }

                        else
                        {
                            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHEQUENOCHECK", refnum, "", "", "", "", "", "", "", "");

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('This Cheque no is already exist.');", true);
                                return;

                            }

                        }




                    }
                }


                //string pounaction = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["pounaction"].ToString();
                //string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
                //string aprvtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
                //string aprvseson = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
                //string aprvdat = (dtuser.Rows.Count == 0) ? "01-jan-1900" : dtuser.Rows[0]["aprvdat"].ToString(); 


                string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "ACVUPDATE02" : (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";


                //-----------Update Transaction B Table-----------------//

                string curcode = "";
                string conrate = "0.00";
                if(vouno=="BD" || vouno == "CD")
                {
                    curcode = this.ddlCurrency.SelectedValue.Trim();
                    conrate = Convert.ToDecimal(this.lblConRate.Text.Trim()).ToString();
                }

                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, rbankname, curcode, conrate, "");


                if (!resultb)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ accData.ErrorObject["Msg"].ToString() + "');", true);
                   
                    return;
                }

                //-----------//Update Transaction A Table//

                string dtrnrmrks = ((Label)this.dgv1.Rows[0].FindControl("lblgvRemarks")).Text.Trim();
                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((Label)this.dgv1.Rows[i].FindControl("lblgvRemarks")).Text.Trim();
                    trnremarks = (vounum.Substring(0, 2) == "JV" && dtrnrmrks.Contains("PBL")) ? dtrnrmrks : trnremarks;
                    string recndt = (this.Request.QueryString["Mod"] == "Accounts") ? ((cactcode.Substring(0, 4) == "1901") ? voudat : ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text.Trim()) : ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text.Trim();
                    string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text.Trim();
                    string billno = ((TextBox)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();

                    if ((Dramt + Cramt) > 0)
                    {
                        bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                                  voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                        if (!resulta)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }

                    }

                }

                if ((ASTUtility.Left(vounum, 2) == "BD") || (ASTUtility.Left(vounum, 2) == "CT"))
                {
                    bool resultd = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATECHQLIST", cactcode, refnum, vounum, "", "",
                                   "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = false;

                //switch (comcod)
                //{
                //    case "3336":
                //    case "3337":
                //    case "3101":
                //        this.PrintVoucher();
                //        break;
                //    default:
                //        break;

                //}


                if (ConstantInfo.LogStatus == true)
                {

                    string eventdesc = "Voucher: " + this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim() + " Dated: " + this.txtEntryDate.Text.Trim();
                    string eventdesc2 = this.txtNarration.Text.Trim();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), voutype, eventdesc, eventdesc2);

                }


                //if (comcod == "3101" || comcod == "3316" || comcod == "3317")
                //{
                //    if (vouno == "JV")
                //        this.lnkPrint_Click(null, null);
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
            //}
            //else
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Please Get Vocher No.";
            //}
        }

        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }

        private void CheeckRefNumber()
        {
            string Vounum = ASTUtility.Left(this.txtcurrentvou.Text.Trim(), 2);
            switch (Vounum)
            {
                case "BD":
                case "BC":
                case "CT":
                    if (this.txtRefNum.Text.Trim() == "")
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
                    break;
            }




        }
        protected void ddlacccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlacccode.BackColor = System.Drawing.Color.Pink;

            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)Session["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst1 = lst.FindAll((p => p.actcode == search1));




            //DataTable dt01 = (DataTable)Session["HeadAcc1"];
            //string search1 = this.ddlacccode.SelectedValue.ToString().Trim();fup
            //DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");


            if (lst1[0].actelev.ToString() == "2")
            {
                //this.lblrescode.Visible = true;
                //this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.HylSubAcc.Visible = true;
                this.ddlresuorcecode.Visible = true;



                //this.lblspecification.Visible = true;
                //this.txtSearchSpeci.Visible = true;
                this.lnkSpecification.Visible = true;
                this.ddlSpclinf.Visible = true;

                this.lblqty.Visible = true;
                this.txtqty.Visible = true;
                this.lblrate.Visible = true;
                this.txtrate.Visible = true;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                string actcode = this.ddlacccode.SelectedValue.Substring(0, 2);
                this.GetResCode();
            }
            else
            {
                //this.lblrescode.Visible = false;
                //this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.HylSubAcc.Visible = false;
                this.ddlresuorcecode.Visible = false;

                //this.lblspecification.Visible = false;
                //this.txtSearchSpeci.Visible = false;
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


        private void GetVouCherNumber()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

               

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Voucher Date Must  Be Greater then Opening Date');", true);
                    return;



                }

                double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
                string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "C" :
                    (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));

                string VNo3 = this.ddlvoucher.SelectedValue.ToString().Trim();
                string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();

                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);
                string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }


        }

        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;


                case "3330":
                    vouprint = "VocherPrint6";
                    break;


                case "3332":

                    vouprint = "VocherPrintIns";
                    break;


                //case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;


                default:
                    vouprint = "VocherPrintMod";
                    break;
            }
            return vouprint;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

            string acpayestatus = ((CheckBox)this.Master.FindControl("CheckBox1")).Checked ? "A/C Payee" : " ";

            //  ((CheckBox)this.Master.FindControl("chkBoxN")).Checked)
            if (this.chkPrint.Checked)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=Cheque&vounum=" + vounum + "&payee=" + acpayestatus + "', target='_blank');</script>";
            }

            else
            {

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=accVou&vounum=" + vounum + "', target='_blank');</script>";
            }
        }


        protected void ibtnFindConCode_Click(object sender, EventArgs e)
        {
            this.LoadAcccombo();
        }
        protected void ibtnFindPrv_Click(object sender, EventArgs e)
        {
            this.GetPriviousVoucher();
        }
        protected void lblPrivVou_Click(object sender, EventArgs e)
        {

        }
        protected void lnkBillNo_Click(object sender, EventArgs e)
        {
            this.GetBillNo();
        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkAccdesc1 = (HyperLink)e.Row.FindControl("hlnkAccdesc1");



                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string subcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode")).ToString();
                string subdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subdesc")).ToString();


                if (subcode != "000000000000")
                {
                    if (hlnkAccdesc1 != null)
                        hlnkAccdesc1.NavigateUrl = "LinkAccSpLedger.aspx?Type=DetailLedger&Date1=" + Convert.ToDateTime(this.txtEntryDate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&sirdesc=" + subdesc;



                }
                else
                {
                    if (hlnkAccdesc1 != null)
                        hlnkAccdesc1.NavigateUrl = "LinkAccLedger.aspx?Type=Ledger&RType=GLedger&Date1=" + Convert.ToDateTime(this.txtEntryDate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&actcode=" + actcode;



                }

            }
        }
        protected void ddlcheque_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.ddlcheque.Items.Count == 0)
                return;
            this.txtRefNum.Text = this.ddlcheque.SelectedItem.Text;
        }
        protected void dgv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {




            DataTable dt = (DataTable)Session["tblvoucher"];
            DataTable dtuser = (DataTable)Session["UserLog"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();

            string voudat = ASTUtility.DateFormat(this.txtEntryDate.Text);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                           this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string actcode = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblAccCod")).Text.Trim();
            string rescode = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblResCod")).Text.Trim();
            string spclcode = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblSpclCod")).Text.Trim();
            string billno = ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("lblgvBillno")).Text.Trim();
            string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "DELETEVOUITEM" : (this.chkpost.Checked) ? "DELETEVOUUNITEM" : "DELETEVOUITEM";

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, spclcode, billno, userid, Terminal, Posteddat, "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ accData.ErrorObject["Msg"].ToString() + "');", true);
                return;

            }
            int rowindex = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();


            DataView dv = dt.DefaultView;
            Session.Remove("tblvoucher");
            Session["tblvoucher"] = dv.ToTable();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Deleted Successfully');", true);
            dgv1.DataSource = dv.ToTable();
            dgv1.DataBind();
            this.CalculatrGridTotal();






        }
        protected void chkCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCopy.Visible = this.chkCopy.Checked;
        }
        protected void lbtnCopyVoucher_Click(object sender, EventArgs e)
        {

            Session.Remove("tblvoucher");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dtuser = (DataTable)Session["UserLog"];
            string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "EDITVOUCHER" : (this.chkpost.Checked) ? "EDITUNVOUCHER" : "EDITVOUCHER";
            string vounum = this.ddlcopyvoucher.SelectedValue.ToString();
            DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", CallType, vounum, "", "", "", "", "", "", "", "");
            DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);
            Session["tblvoucher"] = dt;
            dgv1.DataSource = dt;
            dgv1.DataBind();
            this.CalculatrGridTotal();
            this.chkCopy.Checked = false;
            this.chkCopy_CheckedChanged(null, null);



        }
        protected void ibtnCopyVoucher_Click(object sender, EventArgs e)
        {
            this.GetCopyVoucher();
        }

        private void GetCopyVoucher()
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
            string vounum = "%" + this.txtScrchcopyvoucher.Text + "%";
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRIVOUSVOUCHER", VNo3, vtcode, date, vounum, "", "", "", "", "");

            this.ddlcopyvoucher.DataSource = ds5.Tables[0];
            this.ddlcopyvoucher.DataTextField = "vounum1";
            this.ddlcopyvoucher.DataValueField = "vounum";
            this.ddlcopyvoucher.DataBind();


        }
        protected void dgv1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.dgv1.EditIndex = e.NewEditIndex;
            this.Data_Bind();


            string comcod = this.GetCompCode();
            int rowindex = (dgv1.PageSize) * (this.dgv1.PageIndex) + e.NewEditIndex;
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            string actcode = ((DataTable)Session["tblvoucher"]).Rows[rowindex]["actcode"].ToString();
            string subcode = ((DataTable)Session["tblvoucher"]).Rows[rowindex]["subcode"].ToString();
            //double txtgvQty = Convert.ToDouble("0"+((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvQty")).Text.Trim());


            //double txtgvRate =Convert.ToDouble("0"+ ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvRate")).Text.Trim());
            //double txtgvCrAmt = Convert.ToDouble("0" + ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvCrAmt")).Text.Trim());

            //double txtgvDrAmt = Convert.ToDouble("0" + ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvDrAmt")).Text.Trim());

            DropDownList ddlgrdacccode = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlgrdacccode");


            ViewState["gindex"] = e.NewEditIndex;
            string SearchProject = "%";//+ ((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserceacc")).Text.Trim() + "%";


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", SearchProject, accconhead, "", "", "", "", "", "", "");
            DataTable dt2 = ds2.Tables[0];
            ViewState["HeadAcc1"] = ds2.Tables[0];

            ddlgrdacccode.DataTextField = "actdesc1";
            ddlgrdacccode.DataValueField = "actcode";
            ddlgrdacccode.DataSource = dt2;
            ddlgrdacccode.DataBind();
            ddlgrdacccode.SelectedValue = actcode;




            //ddlgrdresouce.SelectedValue = actcode; 
            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = ddlgrdacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            DropDownList ddlgrdresouce = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode");

            if (dr1[0]["actelev"].ToString() == "2")
            {


                ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvreshead")).Visible = true;
                //((LinkButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = true;
                ((DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = true;

                string SearchResourche = "%"; // +((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Text.Trim() + "%";

                DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, SearchResourche, "", "", "", "", "", "", "");
                DataTable dt3 = ds3.Tables[0];
                Session["HeadRsc1"] = ds3.Tables[0];

                ddlgrdresouce.DataTextField = "resdesc1";
                ddlgrdresouce.DataValueField = "rescode";
                ddlgrdresouce.DataSource = dt3;
                ddlgrdresouce.DataBind();
                ddlgrdresouce.SelectedValue = subcode;
                ddlgrdresouce.Focus();






            }
            else
            {

                ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvreshead")).Visible = false;
                //((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Visible = false;
                //((LinkButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = false;
                ((DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = false;
                ddlresuorcecode.Items.Clear();

            }
            //----------------------------------------------------//
            //((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserceacc")).Text = "";
            ((DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Focus();

        }
        protected void dgv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblvoucher"];
            int rowindex = (int)ViewState["gindex"];
            string actcode = ((DataTable)Session["tblvoucher"]).Rows[rowindex]["actcode"].ToString();
            string subcode = ((DataTable)Session["tblvoucher"]).Rows[rowindex]["subcode"].ToString();
            string spclcode = ((DataTable)Session["tblvoucher"]).Rows[rowindex]["spclcode"].ToString();
            string billnoold = ((DataTable)Session["tblvoucher"]).Rows[rowindex]["billno"].ToString(); ;



            DataRow[] dr2 = dt.Select("actcode = '" + actcode + "' and subcode='" + subcode + "'");
            string ResCode = "";
            if (dr2.Length > 0)
            {
                dr2[0]["actcode"] = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
                ResCode = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedValue.ToString();
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);
                dr2[0]["subcode"] = ResCode;
                dr2[0]["actdesc"] = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedItem.Text;
                dr2[0]["subdesc"] = ResCode == "000000000000" ? "" : ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedItem.Text;
                dr2[0]["trndram"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvDrAmt")).Text.Trim()));
                dr2[0]["trncram"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvCrAmt")).Text.Trim()));
                dr2[0]["billno"] = ((TextBox)this.dgv1.Rows[rowindex].FindControl("lblgvBillno")).Text.Trim();




            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            DataTable dtuser = (DataTable)Session["UserLog"];
            string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string actcodeold = ((Label)this.dgv1.Rows[rowindex].FindControl("lblAccCod")).Text.Trim();
            string rescodeold = ((Label)this.dgv1.Rows[rowindex].FindControl("lblResCod")).Text.Trim();
            string spclcodeold = ((Label)this.dgv1.Rows[rowindex].FindControl("lblSpclCod")).Text.Trim();


            double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvDrAmt")).Text.Trim());
            double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvCrAmt")).Text.Trim());
            string trnamt = Convert.ToString(Dramt - Cramt);
            string trnremarks = ((Label)this.dgv1.Rows[rowindex].FindControl("lblgvRemarks")).Text.Trim();
            string recndt = ((Label)this.dgv1.Rows[rowindex].FindControl("lblrecndat")).Text.Trim();
            string rpcode = ((Label)this.dgv1.Rows[rowindex].FindControl("lblgvrpcode")).Text.Trim();
            string billno = ((TextBox)this.dgv1.Rows[rowindex].FindControl("lblgvBillno")).Text.Trim();
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");




            string voudat = ASTUtility.DateFormat(this.txtEntryDate.Text);
            //string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(6, 4) +
            //                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                           this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string vtcode = Request.QueryString["tcode"];
            string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
            string cactcode = (vouno == "JV" ? "000000000000" : this.ddlConAccHead.SelectedValue.ToString());
            string actcode1 = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
            string subcode1 = ResCode;
            //string cheqno1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvChequeno")).Text;
            //string chequedate1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvChequeDate")).Text;
            string cramt1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvCrAmt")).Text.Trim())).ToString();
            //string rmrks1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvRemarks")).Text;
            string qty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvQty")).Text.Trim()).ToString();
            // string payto1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvPayto")).Text;
            //string billno1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvBillno")).Text; ;
            //string insofissueno1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvinsissueno")).Text;
            // string acvounum = dr2[0]["acvounum"].ToString() == "" ? "00000000000000" : dr2[0]["acvounum"].ToString();
            // comcod, vounum, actcode, rescode, cactcode, voudat,  trnqty, trnrmrk, vtcode, trnam,spclcode,  vactive, rowdate, recndt,  rpcode, billno, userid, editdat,edittrmid

            string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "UPDATE_ACCOUNTVOUCHER" : (this.chkpost.Checked) ? "UPDATE_ACCOUNTVOUCHERUNPOST" : "UPDATE_ACCOUNTVOUCHER";
            //bool resulta = false;


            bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode1, subcode1, cactcode,
                                  voudat, qty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, userid, userdate, Terminal, actcodeold, rescodeold, billnoold, "", "", "", "", "", "", "");

            //bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02", vounum, actcodeold, rescodeold, cactcode,
            //                      voudat, qty, trnremarks, vtcode, trnamt, spclcodeold, recndt, rpcode, billno, userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");



            //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATE_ACCOUNTVOUCHER", vounum, actcode1, subcode1, "000000000000", "",
            //                    cactcode, voudat, cramt1, qty, "", "", "", "", "", "");




            if (!resulta)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
              
                return;
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



            this.dgv1.EditIndex = -1;


            Session["tblvoucher"] = HiddenSameData(dt);
            DataView dv = dt.DefaultView;
            dv.Sort = "actcode,subcode,spclcode";
            dt = dv.ToTable();

            this.Data_Bind();



        }
        protected void dgv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void ddlgrdacccode_SelectedIndexChanged(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).Text;
            DropDownList ddlgrdresouce = (DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode");
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {
                ((Label)this.dgv1.Rows[rowindex].FindControl("lblgvreshead")).Visible = true;
                ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = true;
                this.GetgrdResource();

            }

            else
            {
                ((Label)this.dgv1.Rows[rowindex].FindControl("lblgvreshead")).Visible = false;
                ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = false;
                ddlgrdresouce.Items.Clear();


            }

        }

        private void GetgrdResource()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();



                int rowindex = (int)ViewState["gindex"];
                DropDownList ddlactcode = (DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode");
                DropDownList ddlgrdresouce = (DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode");

                string actcode = ddlactcode.SelectedValue.ToString();
                string filter1 = "%";

                string oldRescode = (ddlgrdresouce.Items.Count == 0) ? "" : ddlgrdresouce.SelectedValue.ToString();


                string SearchInfo = "";
                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lstacc = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)Session["HeadAcc1"];

                // string search1 = ddlactcode.SelectedValue.ToString();    

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lstacc1 = lstacc.FindAll((p => p.actcode == actcode));

                //if (lst1.Count == 0)
                //    return;

                //DataRow[] drac = dt01.Select("actcode='" + search1 + "'");
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
                Session["HeadRsc1"] = lst;
                ddlgrdresouce.DataTextField = "resdesc1";
                ddlgrdresouce.DataValueField = "rescode";
                ddlgrdresouce.DataSource = lst;
                ddlgrdresouce.DataBind();
                ddlgrdresouce.Focus();


                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst1 = lst.FindAll((p => p.rescode == oldRescode));
                if (lst1.Count > 0)
                {
                    ddlgrdresouce.SelectedValue = oldRescode;


                }

            }


            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }


        private void VoucherType()
        {
            string vounum = this.ddlvoucher.SelectedItem.Text;

            string voutype = this.ddlvoucher.SelectedValue.ToString();
            this.CashABankDataBind();
            this.Visibility();
            this.GetAccHead();
            ((Label)this.Master.FindControl("lblTitle")).Text = this.ddlvoucher.SelectedItem.Text;
            this.lblPayto.Text = ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit") ? "Received From: " : "Pay To: ";

            if (vounum.Contains("Payment") || (vounum.Contains("Contra")))
            {
                this.dgv1.Columns[12].Visible = true;
                this.dgv1.Columns[13].Visible = false;
                this.lblCramt.Visible = false;
                this.txtCrAmt.Visible = false;
                this.lblDramt.Visible = true;
                this.txtDrAmt.Visible = true;

                //this.lblcontrolAccHead.Visible = true;
                this.ddlConAccHead.Visible = true;
                //this.txtScrchConCode.Visible = true;
                this.ibtnFindConCode.Visible = true;
            }
            else if (vounum.Contains("Deposit"))
            {

                this.dgv1.Columns[12].Visible = false;
                this.dgv1.Columns[13].Visible = true;
                this.lblCramt.Visible = true;
                this.txtCrAmt.Visible = true;
                this.lblDramt.Visible = false;
                this.txtDrAmt.Visible = false;

                //this.lblcontrolAccHead.Visible = true;
                this.ddlConAccHead.Visible = true;
                //this.txtScrchConCode.Visible = true;
                this.ibtnFindConCode.Visible = true;
            }

            else if (vounum.Contains("Journal"))
            {
                this.dgv1.Columns[12].Visible = true;
                this.dgv1.Columns[13].Visible = true;
                this.lblCramt.Visible = true;
                this.txtCrAmt.Visible = true;
                this.lblDramt.Visible = true;
                this.txtDrAmt.Visible = true;

                //this.lblcontrolAccHead.Visible = false;
                this.ddlConAccHead.Visible = false;
                //this.txtScrchConCode.Visible = false;
                this.ibtnFindConCode.Visible = false;
            }

            else
            {
                this.dgv1.Columns[12].Visible = true;
                this.dgv1.Columns[13].Visible = true;
                this.lblCramt.Visible = true;
                this.txtCrAmt.Visible = true;
                this.lblDramt.Visible = true;
                this.txtDrAmt.Visible = true;

                //this.lblcontrolAccHead.Visible = true;
                this.ddlConAccHead.Visible = true;
                //this.txtScrchConCode.Visible = true;
                this.ibtnFindConCode.Visible = true;
            }

        }


        private void CashABankDataBind()
        {
            DataTable dt = ((DataTable)Session["tblbank"]).Copy();
            DataView dv = dt.DefaultView;
            //Session["tblbank"] = ds1.Tables[0];

            string vounum = this.ddlvoucher.SelectedValue.ToString();

            if (vounum == "CC" || vounum == "CD")
            {

                dv.RowFilter = ("actcode like '1901%'");

            }
            else if (vounum == "BC" || vounum == "BD" || vounum == "PV")
            {

                dv.RowFilter = ("actcode like '1902%' or  actcode like '29%' ");
            }

            this.ddlConAccHead.DataSource = dv.ToTable();
            this.ddlConAccHead.DataTextField = "actdesc1";
            this.ddlConAccHead.DataValueField = "actcode";
            this.ddlConAccHead.DataBind();

        }

        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;
            this.ddlCurrency.DataValueField = "curcode";
            this.ddlCurrency.DataTextField = "curdesc";
            this.ddlCurrency.DataSource = lstCurryDesc;
            this.ddlCurrency.DataBind();

            if (this.Request.QueryString["InputType"] == "FxtAstEntry")
            {
                this.ddlCurrency.SelectedValue = "001";
                this.ddlCurrency.Enabled = false;
            }
            this.ddlCurrency_SelectedIndexChanged(null, null);
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tcode = "001";
                string fcode = this.ddlCurrency.SelectedValue.ToString();
                List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

                var experiment = ViewState["tblcur"];

                double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;

                this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
                //double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));
            }
            catch (Exception ex)
            {

            }
        }
    }
}