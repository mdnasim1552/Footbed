using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY;
using SPEENTITY.C_22_Sal;

namespace SPEWEB.F_23_MAcc
{
    public partial class AccountInterface : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "ACCOUNTS Smartface";//
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                Hashtable hst = (Hashtable)Session["tblLogin"];

                //this.plncop.Visible = true;
                //this.GetCompanyName();


                txtdate_TextChanged(null, null);
                //this.RadioButtonList1_SelectedIndexChanged(null, null);

            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {

            txtdate_TextChanged(null, null);


        }
        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = ASTUtility.Right(hst["usrid"].ToString(), 3);
            string comcod = hst["mcomcod"].ToString();
            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_03", "GETCOMPNAME", usrid, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "comname";
            this.ddlCompany.DataValueField = "comcod";
            this.ddlCompany.DataSource = ds.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany.SelectedValue = this.GetCompCode();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["mcomcod"].ToString();
            if (comcod != "0000")
            {
                this.AccInterface();
            }

        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.pnlInterAcc.Visible = true;
            this.pnlAcc.Visible = false;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.pnlInterAcc.Visible = false;
            this.pnlAcc.Visible = true;
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.AccInterface();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.AccInterface();
        }
        protected void btnSetup_Click(object sender, EventArgs e)
        {
            //this.PnlSalesSetup.Visible = true;
            //this.PnlInt.Visible = false;
            //this.pnlReprots.Visible = true;
            //this.plnMgf.Visible = false;
            //hello

        }

        private void AccInterface()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];


            string comcod = this.GetCompCode();
            string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            
            //exportcount, insentcount, lccount, lpurcount, missucount, procount , mtrncount, pdccount, bnkrecount, expretcount
            this.RadioButtonList1.Items[0].Text = "Export" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["exportcout"].ToString() + "</span>";
            this.RadioButtonList1.Items[1].Text = "Collection" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["insentcount"].ToString() + "</span>";
            this.RadioButtonList1.Items[2].Text = "L/C " + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["lccount"].ToString() + "</span>";
            this.RadioButtonList1.Items[3].Text = "Purchase " + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["lpurcount"].ToString() + "</span>";
            this.RadioButtonList1.Items[4].Text = "Issue" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["missucount"].ToString() + "</span>";
            this.RadioButtonList1.Items[5].Text = "Production" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["procount"].ToString() + "</span>";
            this.RadioButtonList1.Items[6].Text = "Reconcilation" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["bnkrecount"].ToString() + "</span>";
            this.RadioButtonList1.Items[7].Text = "PDC" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["pdccount"].ToString() + "</span>";
            this.RadioButtonList1.Items[8].Text = "Mat. Transfer" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["mtrncount"].ToString() + "</span>";
            this.RadioButtonList1.Items[9].Text = "E.Return" + "<span class='lbldata counter'>" + ds1.Tables[10].Rows[0]["expretcount"].ToString() + "</span>";

            //Update export
            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            this.Data_Bind("gvSalesUpdate", dt);


            //Update Insentive
            dt = ((DataTable)ds1.Tables[1]).Copy();
            this.Data_Bind("gvCollUpdate", dt);



            //Approved
            dt = ((DataTable)ds1.Tables[2]).Copy();
            this.Data_Bind("gvLcUpdate", dt);

            //Purchase
            dt = ((DataTable)ds1.Tables[3]).Copy();
            this.Data_Bind("gvPurchase", dt);

            //Issue
            dt = ((DataTable)ds1.Tables[4]).Copy();
            this.Data_Bind("gvIssue", dt);
            //Production
            dt = ((DataTable)ds1.Tables[5]).Copy();
            this.Data_Bind("gvProd", dt);

            //dt = ((DataTable)ds1.Tables[6]).Copy();
            //this.Data_Bind("gvChallan", dt);
            ///Trasnfer
            dt = ((DataTable)ds1.Tables[7]).Copy();
            this.Data_Bind("gvMatTrasfer", dt);
            //PDC
            dt = ((DataTable)ds1.Tables[8]).Copy();
            this.Data_Bind("dgPdc", dt);
            ////Bank Rec
            dt = ((DataTable)ds1.Tables[6]).Copy();
            this.Data_Bind("gvBankRec", dt);


            //Sales Return
            dt = ((DataTable)ds1.Tables[0]).Copy();
            this.Data_Bind("gvSRet", dt);



            ////Service
            //dt = ((DataTable)ds1.Tables[11]).Copy();
            //this.Data_Bind("gvService", dt);

            //dt = ((DataTable)ds1.Tables[12]).Copy();
            //this.Data_Bind("gvSalAdj", dt);

            //ViewState["tblunposted"] = ds1.Tables[13];
            //dt = ((DataTable)ds1.Tables[13]).Copy();
            //this.Data_Bind("gvAccUnPosted", dt);

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "0":
                    this.pnlUpSales.Visible = true;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;

                case "1":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = true;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "2":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = true;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "3":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = true;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "4":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = true;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "5":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = true;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "6":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = true;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "7":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = true;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[7].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "8":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                   // this.PanelChallan.Visible = true;
                    this.PnlMTras.Visible = true;
                    this.PnlSRet.Visible = false;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[8].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "9":
                    this.pnlUpSales.Visible = false;
                    this.pnlUpColl.Visible = false;
                    this.PanlUpLc.Visible = false;
                    this.pnlPurchase.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.PanelProduction.Visible = false;
                    this.PnlBRec.Visible = false;
                    this.PnlPDC.Visible = false;
                    this.PanelChallan.Visible = false;
                    this.PnlMTras.Visible = false;
                    this.PnlSRet.Visible = true;
                    this.PnlSer.Visible = false;
                    this.PnlSAdj.Visible = false;
                    this.pnlUnPost.Visible = false;
                    this.RadioButtonList1.Items[9].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                    //case "10":
                    //    this.pnlUpSales.Visible = false;
                    //    this.pnlUpColl.Visible = false;
                    //    this.PanlUpLc.Visible = false;
                    //    this.pnlPurchase.Visible = false;
                    //    this.PanelIssue.Visible = false;
                    //    this.PanelProduction.Visible = false;
                    //    this.PnlBRec.Visible = false;
                    //    this.PnlPDC.Visible = false;
                    //    this.PanelChallan.Visible = false;
                    //    this.PnlMTras.Visible = false;
                    //    this.PnlSRet.Visible = true;
                    //    this.PnlSer.Visible = false;
                    //    this.PnlSAdj.Visible = false;
                    //    this.pnlUnPost.Visible = false;
                    //    this.RadioButtonList1.Items[10].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    //    break;
                    //case "11":
                    //    this.pnlUpSales.Visible = false;
                    //    this.pnlUpColl.Visible = false;
                    //    this.PanlUpLc.Visible = false;
                    //    this.pnlPurchase.Visible = false;
                    //    this.PanelIssue.Visible = false;
                    //    this.PanelProduction.Visible = false;
                    //    this.PnlBRec.Visible = false;
                    //    this.PnlPDC.Visible = false;
                    //    this.PanelChallan.Visible = false;
                    //    this.PnlMTras.Visible = false;
                    //    this.PnlSRet.Visible = false;
                    //    this.PnlSer.Visible = true;
                    //    this.PnlSAdj.Visible = false;
                    //    this.pnlUnPost.Visible = false;
                    //    this.RadioButtonList1.Items[11].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    //    break;
                    //case "12":
                    //    this.pnlUpSales.Visible = false;
                    //    this.pnlUpColl.Visible = false;
                    //    this.PanlUpLc.Visible = false;
                    //    this.pnlPurchase.Visible = false;
                    //    this.PanelIssue.Visible = false;
                    //    this.PanelProduction.Visible = false;
                    //    this.PnlBRec.Visible = false;
                    //    this.PnlPDC.Visible = false;
                    //    this.PanelChallan.Visible = false;
                    //    this.PnlMTras.Visible = false;
                    //    this.PnlSRet.Visible = false;
                    //    this.PnlSer.Visible = false;
                    //    this.PnlSAdj.Visible = true;
                    //    this.pnlUnPost.Visible = false;
                    //    this.RadioButtonList1.Items[12].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    //    break;
                    //case "13":
                    //    this.pnlUpSales.Visible = false;
                    //    this.pnlUpColl.Visible = false;
                    //    this.PanlUpLc.Visible = false;
                    //    this.pnlPurchase.Visible = false;
                    //    this.PanelIssue.Visible = false;
                    //    this.PanelProduction.Visible = false;
                    //    this.PnlBRec.Visible = false;
                    //    this.PnlPDC.Visible = false;
                    //    this.PanelChallan.Visible = false;
                    //    this.PnlMTras.Visible = false;
                    //    this.PnlSRet.Visible = false;
                    //    this.PnlSer.Visible = false;
                    //    this.PnlSAdj.Visible = false;
                    //    this.pnlUnPost.Visible = true;
                    //    this.RadioButtonList1.Items[13].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    //    break;
            }
        }
        protected void gvSalesUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "invno")).ToString();
                string genno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString("dd-MMM-yyyy");

                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_21_GAcc/AccIncomeOfOrd?Type=Entry&actcode=" + actcode + "&delvtrm=&date=" + date;

            }
        }
        protected void gvCollUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                //string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                //string recvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recvno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_21_GAcc/AccSales?Type=Entry&comcod=" + comcod;

            }
        }
        protected void gvLcUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                //string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string rcvdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "rcvdate")).ToString("dd-MMM-yyyy");

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_21_GAcc/AccPurchaseFor?Type=Entry&comcod=" + comcod + "&genno=" + billno + "&date=" + rcvdate;

            }
        }
        protected void gvPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                //string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string billdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "billdat")).ToString("dd-MMM-yyyy");

                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&genno=" + billno + "&date=" + billdat;

                hlink2.NavigateUrl = "~/F_21_GAcc/AccPurchase?Type=Entry&comcod=" + comcod + "&genno=" + billno + "&date=" + billdat;

            }
        }
        protected void gvIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string genno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "issdat")).ToString("dd-MMM-yyyy");

                hlink2.NavigateUrl = "~/F_21_GAcc/AccIsuUpdate?Type=Entry&comcod=" + comcod + "&genno=" + genno + "&date=" + date;

            }
        }
        protected void gvProd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string genno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prodid")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "proddate")).ToString("dd-MMM-yyyy");

                hlink2.NavigateUrl = "~/F_21_GAcc/AccProductionJVManual?Type=Entry&comcod=" + comcod + "&genno=" + genno.Trim() + "&date=" + date;

            }
        }
        protected void gvChallan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string chlnno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "chlnno")).ToString();
                string chlndat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "chlndat")).ToString("dd-MMM-yyyy");
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_15_Acc/AccChalanTransfer?Type=Entry&comcod=" + comcod + "&genno=" + chlnno + "&date=" + chlndat;

            }
        }
        protected void gvMatTrasfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string trnno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "trnno")).ToString();
                //string recvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recvno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_21_GAcc/AccTransfer?Type=Entry&genno="+ trnno + "&comcod=" + comcod + "";

            }
        }
        protected void gvSRet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
            //    HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
            //    string retmemo = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "retmemo")).ToString();
            //     string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "retdat")).ToString("dd-MMM-yyyy");
            //    //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

            //    //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

            //     hlink2.NavigateUrl = "~/F_15_Acc/AccRetSalesJournal.aspx?Type=Entry&comcod=" + comcod + "&genno=" + retmemo + "&date=" + date;

            //}
        }

        protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                string recvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recvno")).ToString();
                string Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "execdate")).ToString("dd-MMM-yyyy");


                hlink2.NavigateUrl = "~/F_15_Acc/AccServJournal?Type=Entry&comcod=" + comcod + "&actcode=" + centrid + "&genno=" + recvno + "&date=" + Date;

            }
        }
        protected void gvSalAdj_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                LinkButton delbtn = (LinkButton)e.Row.FindControl("btnDelOrder");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                //string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string cnno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "memono")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "mrdat")).ToString("dd-MMM-yyyy");

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_15_Acc/AccSalesAdjUpdate?Type=Entry&comcod=" + comcod + "&genno=" + cnno + "&date=" + date;
                if (delbtn != null)
                    delbtn.CommandArgument = cnno;
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string centrid = dt1.Rows[0]["invno"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["invno"].ToString() == centrid)
                {
                    centrid = dt1.Rows[j]["invno"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                }

                else
                    centrid = dt1.Rows[j]["invno"].ToString();
            }

            return dt1;
        }
        private DataTable HiddenSameData2(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["mlccod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mlccod"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["mlcdesc"] = "";
                }

                else
                    pactcode = dt1.Rows[j]["mlccod"].ToString();
            }

            return dt1;
        }
        private void Data_Bind(string gv, DataTable dt)
        {

            //comcod, invno, mlccod, styleid,  invdate,   ordrqty, rate, actdesc, buyername
            switch (gv)
            {
                case "gvSalesUpdate":
                    this.gvSalesUpdate.DataSource = HiddenSameData(dt);
                    this.gvSalesUpdate.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    for (int i = 0; i < gvSalesUpdate.Rows.Count; i++)
                    {
                        string Centrid = ((Label)gvSalesUpdate.Rows[i].FindControl("lblgvcentrid")).Text.Trim();
                        string Orderno = ((Label)gvSalesUpdate.Rows[i].FindControl("lgcorderno")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvSalesUpdate.Rows[i].FindControl("lnkbtnPrint");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Centrid + Orderno;
                    }

                    break;

                case "gvCollUpdate":
                    this.gvCollUpdate.DataSource = HiddenSameData2(dt);
                    this.gvCollUpdate.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    //((Label)this.gvCollUpdate.FooterRow.FindControl("lblINAmtTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cramt)", "")) ?
                    //0 : dt.Compute("sum(cramt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvCollUpdate.FooterRow.FindControl("lblINPTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(vatamt)", "")) ?
                    //0 : dt.Compute("sum(vatamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvCollUpdate.FooterRow.FindControl("lblINPTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                    //0 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //for (int i = 0; i < gvCollUpdate.Rows.Count; i++)
                    //{
                    //    string Centrid = ((Label)gvCollUpdate.Rows[i].FindControl("lblgvINcentrid")).Text.Trim();
                    //    string Orderno = ((Label)gvCollUpdate.Rows[i].FindControl("lgINcorderno")).Text.Trim();

                    //    LinkButton lbtn1 = (LinkButton)gvCollUpdate.Rows[i].FindControl("lnkbtnPrintIN");
                    //    if (lbtn1 != null)
                    //        if (lbtn1.Text.Trim().Length > 0)
                    //            lbtn1.CommandArgument = Centrid + Orderno;

                    //    LinkButton lbtn2 = (LinkButton)gvCollUpdate.Rows[i].FindControl("btnDelOrder");
                    //    if (lbtn2 != null)
                    //        if (lbtn2.Text.Trim().Length > 0)
                    //            lbtn2.CommandArgument = Centrid + Orderno;
                    //}

                    break;
                case "gvLcUpdate":

                    this.gvLcUpdate.DataSource = HiddenSameData2(dt);
                    this.gvLcUpdate.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvPurchase":

                    this.gvPurchase.DataSource = dt;
                    this.gvPurchase.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    break;
                case "gvIssue":

                    this.gvIssue.DataSource = dt;
                    this.gvIssue.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvProd":

                    this.gvProd.DataSource = dt;
                    this.gvProd.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "dgPdc":
                    this.dgPdc.DataSource = dt;
                    this.dgPdc.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    return;

                case "gvBankRec":
                    this.gvBankRec.DataSource = dt;
                    this.gvBankRec.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    return;
                //case "gvChallan":

                //    this.gvChallan.DataSource = HiddenSameData(dt);
                //    this.gvChallan.DataBind();

                //    if (dt.Rows.Count == 0)
                //        return;
                //    break;
                case "gvMatTrasfer":

                    this.gvMatTrasfer.DataSource = dt;
                    this.gvMatTrasfer.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvSRet":

                    this.gvSRet.DataSource = HiddenSameData(dt);
                    this.gvSRet.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                    //case "gvService":

                    //    this.gvService.DataSource = HiddenSameData(dt);
                    //    this.gvService.DataBind();

                    //    if (dt.Rows.Count == 0)
                    //        return;
                    //    break;
                    //case "gvSalAdj":

                    //    this.gvSalAdj.DataSource = HiddenSameData(dt);
                    //    this.gvSalAdj.DataBind();

                    //    if (dt.Rows.Count == 0)
                    //        return;
                    //    break;
                    //case "gvAccUnPosted":

                    //    this.gvAccUnPosted.DataSource = (dt);
                    //    this.gvAccUnPosted.DataBind();

                    //    if (dt.Rows.Count == 0)
                    //        return;
                    //    break;


            }




        }


        protected void dgPdc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                //Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {


            //int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
            //this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);

            //try
            //{


            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = (hst["mcomcod"].ToString() == "0000") ? hst["comcod"].ToString() : this.ddlCompany.SelectedValue.ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");




            //    DataSet ds2 = accData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTCUSTINFORMATION", orderno, centrid, "", "", "", "", "", "", "");
            //    if (ds2 == null)
            //        return;
            //    ReportDocument rptSOrder = new ReportDocument();
            //    //string qType = this.Request.QueryString["Type"].ToString();
            //    //if (qType == "dNote")
            //    //{
            //    //    rptSOrder = new RMGiRPT.R_23_SaM.RptSalDelNoteZelta();
            //    //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //    //    txtHeader.Text = "DELIVERY NOTE";
            //    //}
            //    //else
            //    //{
            //    rptSOrder = new RMGiRPT.R_23_SaM.RptSalOrdrZelta();
            //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //    txtHeader.Text = "SALES ORDER";
            //    // }


            //    TextObject txtrptcomp = rptSOrder.ReportDefinition.ReportObjects["Company"] as TextObject;
            //    txtrptcomp.Text = comnam;



            //    TextObject txtCompAdd = rptSOrder.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //    txtCompAdd.Text = comadd;

            //    TextObject txtsaledate = rptSOrder.ReportDefinition.ReportObjects["txtsaledate"] as TextObject;
            //    txtsaledate.Text = ds2.Tables[2].Rows[0]["orderdat"].ToString().Trim();

            //    TextObject txtCust = rptSOrder.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //    txtCust.Text = ds2.Tables[2].Rows[0]["name"].ToString().Trim();

            //    TextObject txtAdd = rptSOrder.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //    txtAdd.Text = ds2.Tables[2].Rows[0]["addr"].ToString().Trim();

            //    TextObject txtPhone = rptSOrder.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //    txtPhone.Text = ds2.Tables[2].Rows[0]["phone"].ToString().Trim();

            //    TextObject txtTrans = rptSOrder.ReportDefinition.ReportObjects["txtTrans"] as TextObject;
            //    txtTrans.Text = ds2.Tables[0].Rows[0]["courie"].ToString().Trim();

            //    TextObject txtStore = rptSOrder.ReportDefinition.ReportObjects["txtStore"] as TextObject;
            //    txtStore.Text = ds2.Tables[2].Rows[0]["storename"].ToString().Trim();


            //    TextObject txtCode = rptSOrder.ReportDefinition.ReportObjects["txtCode"] as TextObject;
            //    txtCode.Text = ds2.Tables[2].Rows[0]["sirtdes"].ToString().Trim();

            //    TextObject txtOrdTime = rptSOrder.ReportDefinition.ReportObjects["txtOrdTime"] as TextObject;
            //    txtOrdTime.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["posteddat"]).ToString("hh:mm:ss tt").Trim();

            //    TextObject txtRemarks = rptSOrder.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            //    txtRemarks.Text = (ds2.Tables[2].Rows[0]["narration"]).ToString().Trim();

            //    TextObject txtsaleNo = rptSOrder.ReportDefinition.ReportObjects["txtsaleNo"] as TextObject;
            //    txtsaleNo.Text = orderno;
            //    //BALANCE 

            //    DataTable dt = ds2.Tables[0];
            //    DataTable dt2 = ds2.Tables[1];
            //    DataTable dt3 = ds2.Tables[2];

            //    double oStdAmt, Dipsamt, ordAmt, balAmt;
            //    oStdAmt = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(dues)", "")) ? 0.00 : dt3.Compute("sum(dues)", "")));
            //    ordAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ? 0.00 : dt.Compute("sum(tamount)", "")));
            //    Dipsamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ? 0.00 : dt2.Compute("sum(paidamt)", "")));

            //    balAmt = (oStdAmt + ordAmt) - Dipsamt;
            //    //if (qType == "All")
            //    //{
            //    TextObject txtOutStdBal = rptSOrder.ReportDefinition.ReportObjects["txtOutStdBal"] as TextObject;
            //    txtOutStdBal.Text = oStdAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtDipositeAmt = rptSOrder.ReportDefinition.ReportObjects["txtDipositeAmt"] as TextObject;
            //    txtDipositeAmt.Text = Dipsamt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtOrderAmt = rptSOrder.ReportDefinition.ReportObjects["txtOrderAmt"] as TextObject;
            //    txtOrderAmt.Text = ordAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtBalanceAmt = rptSOrder.ReportDefinition.ReportObjects["txtBalanceAmt"] as TextObject;
            //    txtBalanceAmt.Text = balAmt.ToString("#,##0.00;(#,##0.00);");
            //    //}

            //    TextObject txtAppby = rptSOrder.ReportDefinition.ReportObjects["txtAppby"] as TextObject;
            //    txtAppby.Text = ds2.Tables[2].Rows[0]["appby"].ToString().Trim();

            //    TextObject txtPreBy = rptSOrder.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            //    txtPreBy.Text = ds2.Tables[0].Rows[0]["usrname"].ToString().Trim();

            //    TextObject txtuserinfo = rptSOrder.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptSOrder.SetDataSource(ds2.Tables[0]);

            //    // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

            //    // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource(ds2.Tables[1]);


            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptSOrder.SetParameterValue("ComLogo", ComLogo);

            //    Session["Report1"] = rptSOrder;

            //    this.lblprintstkl.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 "PDF" + "', target='_blank');</script>";


            //    Common.LogStatus("Order", "Order Print", "Order No: ", orderno + " - " + centrid);

            //}
            //catch (Exception ex)
            //{

            //}

        }




        protected void gvAccUnPosted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkVoucherEdit");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                if (vounum.Substring(0, 2) == "BD" || vounum.Substring(0, 2) == "CD")
                    hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts?tcode=99&tname=Payment Voucher&Mod=Management&vounum=" + vounum;
                else if (vounum.Substring(0, 2) == "BC" || vounum.Substring(0, 2) == "CC")
                    hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts?tcode=99&tname=Deposit Voucher&Mod=Management&vounum=" + vounum;

                else if (vounum.Substring(0, 2) == "JV")
                    hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts?tcode=99&tname=Journal Voucher&Mod=Management&vounum=" + vounum;
                else

                    hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts?tcode=92&tname=Contra Voucher&Mod=Management&vounum=" + vounum;





            }
        }

        protected void lbtnVoucherApp_Click(object sender, EventArgs e)
        {



            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)ViewState["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            dt.Rows[index]["chkmv"] = "True";


            //this.lblmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = (hst["mcomcod"].ToString() == "0000") ? hst["comcod"].ToString() : this.ddlCompany.SelectedValue.ToString();
            string vounum = dt.Rows[index]["vounum"].ToString();

            string ApprovedByid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Approvedtrmid = hst["compname"].ToString();
            string ApprovedSession = hst["session"].ToString();
            string Approvedddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            // Existing Voucher

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER_02", "GETEXISTPOSTEDVOUCHER", vounum, "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                //this.lblmsg.Text = "Voucher Already Posted";
                return;

            }


            bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER_02", "UPUNPOSTEDVOUCHER", vounum, ApprovedByid, Approvedtrmid, ApprovedSession, Approvedddat,
                               "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resultb)
            {
                //this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                return;
            }
            //this.lblmsg.Text = "Updated Successfully";
            Session.Remove("tblunposted");
            DataView dv = dt.DefaultView;
            ViewState["tblunposted"] = dv.ToTable();
            this.Data_Bind("gvAccUnPosted", dv.ToTable());
        }
        protected void lbtnDeleteVoucher_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)ViewState["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;



            //this.lblmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = (hst["mcomcod"].ToString() == "0000") ? hst["comcod"].ToString() : this.ddlCompany.SelectedValue.ToString();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string vounum = dt.Rows[index]["vounum"].ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER_02", "DELETEVOUCHERUNPOSTED", vounum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == false)
            {
                //this.lblmsg.Text = "Data Is Not Deleted";
                return;

            }



            //this.lblmsg.Text = "Updated Successfully";
            this.Data_Bind("gvAccUnPosted", dt);


        }
        protected void btnDelCSChk(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(Code, 12).ToString();
            string cnno = ASTUtility.Right(Code, 14).ToString();
            string Type = Code.Substring(14).ToString();
            if (cnno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = (hst["mcomcod"].ToString() == "0000") ? hst["comcod"].ToString() : this.ddlCompany.SelectedValue.ToString();

            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "GET_PAYMENTINF_INFO", centrid, cnno);
            this.XmlDataInsert(centrid, cnno, ds);


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "REVPAYMENT", centrid, cnno, "", "", "");

            if (result == true)
            {
                AccInterface();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Delete Successfully');", true);
            }
            Common.LogStatus("Accounts Interface", "CN Delete", "CN No: ", cnno);
        }
        private string XmlDataInsert(string centrid, string cnno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = (hst["mcomcod"].ToString() == "0000") ? hst["comcod"].ToString() : this.ddlCompany.SelectedValue.ToString();
            string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            ds.Tables[0].Columns.Add("delbyid", typeof(string));
            ds.Tables[0].Columns.Add("delseson", typeof(string));
            ds.Tables[0].Columns.Add("deldate", typeof(DateTime));

            ds.Tables[0].Rows[0]["delbyid"] = usrid;
            ds.Tables[0].Rows[0]["delseson"] = session;
            ds.Tables[0].Rows[0]["deldate"] = Date;


            ds1.Merge(ds.Tables[0]);
            ds1.Tables[0].TableName = "tbl1";


            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, centrid, cnno);

            if (!resulta)
            {

                //return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
    }
}