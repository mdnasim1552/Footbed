using Microsoft.Reporting.WinForms;
using SPEENTITY.C_19_Exp;
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
    public partial class PurReturnList : System.Web.UI.Page
    {
        //private int PageSize = 5;
        ProcessAccess InvData = new ProcessAccess();
        SalesInvoice_BL viewSales = new SalesInvoice_BL();
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


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Return List";
                this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
                this.ConfirmMessage.Visible = false;
            }
        }

        private void CommonButton()
        {

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "View";
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Request";


            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "RetList")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "RetList")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("href", "../F_10_Procur/PurchaseReturn?Type=Entry&centrid=&date=&genno=");

            }
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("target", "_blank");

            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnAll_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click);

        }



        //protected void lnkbtnNew_Click(object sender, EventArgs e)
        //{
        //    if (this.Request.QueryString["Type"].ToString() == "prodman")
        //    {
        //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_13_ProdMon/ProductionManually?Type=Entry&genno=" + "', target='_blank');</script>";
        //    }

        //}

        //private void SelectView()
        //{
        //    string type = this.Request.QueryString["Type"].ToString();
        //    if (type == "RetList")
        //    {
        //        this.MultiView.ActiveViewIndex = 0;
        //    }
        //}


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void Get_Purchase_ReturnList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtFdate = this.txtFromDate.Text.ToString();
            string txttdate = this.txtToDate.Text.ToString();

            DataSet ds1 = InvData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_PUR_RETURN_LIST", txtFdate, txttdate, "", " ", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            ViewState["purreturnlist"] = ds1.Tables[0];

            this.Data_Bind();

        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "RetList")
            {

                DataTable dt1 = ((DataTable)ViewState["purreturnlist"]).Copy();
                if (dt1.Rows.Count == 0)
                    return;
                this.GvPurReturn.DataSource = dt1;
                this.GvPurReturn.DataBind();
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "RetList")
            {
                this.Get_Purchase_ReturnList();
            }

        }

        private List<SPEENTITY.C_10_Procur.EClassPur.EclassMttLst> HiddenSameData(List<SPEENTITY.C_10_Procur.EClassPur.EclassMttLst> lst)
        {
            if (lst.Count == 0)
                return lst;

            int i = 0;
            string procode = lst[0].actdesc;

            foreach (SPEENTITY.C_10_Procur.EClassPur.EclassMttLst lst1 in lst)
            {

                if (i == 0)
                {
                    i++;
                    continue;
                }
                else if (lst1.actdesc == procode)
                {
                    lst[i].actdesc = "";
                }
                else
                {

                    if (lst1.actdesc == procode)
                        lst[i].actdesc = "";

                    procode = lst1.actdesc;
                }

                i++;
            }
            return lst;

        }

        //protected void lnkprint_Click(object sender, EventArgs e)
        //{
        //    string type = this.Request.QueryString["Type"].ToString();

        //    switch (type)
        //    {
        //        case "RetList":
        //            this.PrintPurchaseReturn();
        //            break;
        //    }

        //}

        //private void PrintPurchaseReturn()
        //{
        //    return;
        //}

        //protected void lnkbtnPrint_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    int index = row.RowIndex;
        //    string comcod = this.GetCompCode();
        //    Hashtable hst = (Hashtable)Session["tbllogin"];
        //    string comnam = hst["comnam"].ToString();  //company name
        //    string comadd = hst["comadd1"].ToString();  //address
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();

        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string mgrrno = ((Label)this.gvprodman.Rows[index].FindControl("lvgrrno")).Text.ToString();
        //    DataSet mgrrdata = InvData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODMINFO", mgrrno, "", "", "", "");

        //    var list2 = mgrrdata.Tables[2].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.EclassMttLst>();
        //    var list = mgrrdata.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.EClassSalProInfo>();

        //    string batch = list[0].batchdesc;

        //    var list1 = mgrrdata.Tables[1].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.EclassProdManList>();

        //    string Proposal = list1[0].mgrrno;
        //    string date = list1[0].mgrrdate.ToString("dd-MMM-yyyy");
        //    string refno = list1[0].refno;
        //    string supcenter = list1[0].stordesc;
        //    string suplname = list1[0].suplname;
        //    string remarks = list1[0].remarks;
        //    var list2N = HiddenSameData(list2);
        //    LocalReport rpt1 = new LocalReport();
        //    //rpt1 = SPERDLC.RptSetupClass1.GetLocalReport("RD_11_Pro.RptManualProdPur", list, HiddenSameData(list2), null);

        //    rpt1.SetParameters(new ReportParameter("comnam", comnam));
        //    rpt1.SetParameters(new ReportParameter("comadd", comadd));
        //    rpt1.SetParameters(new ReportParameter("batch", batch));
        //    rpt1.SetParameters(new ReportParameter("date", date));
        //    rpt1.SetParameters(new ReportParameter("refno", refno));
        //    rpt1.SetParameters(new ReportParameter("remarks", "Remarks: " + remarks));
        //    rpt1.SetParameters(new ReportParameter("supcenter", supcenter));
        //    rpt1.SetParameters(new ReportParameter("suplname", suplname));
        //    rpt1.SetParameters(new ReportParameter("Proposal", Proposal));


        //    rpt1.SetParameters(new ReportParameter("RptTitle", "Manual Product Purchase Report"));

        //    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
        //    Session["Report1"] = rpt1;

        //    ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRdLc('PDF');", true);
        //}

        protected void GvPurReturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink LnkEdit = (HyperLink)e.Row.FindControl("LnkEdit");
                LinkButton lbtnapprov = (LinkButton)e.Row.FindControl("LbtnApp");
                string centrid = ((Label)e.Row.FindControl("lgcCentrid")).Text.ToString();
                string retno = ((Label)e.Row.FindControl("lgcPurRetno")).Text.ToString();
                string date = ((Label)e.Row.FindControl("lgcRetDate")).Text.ToString();
                string vounum = ((Label)e.Row.FindControl("lgvVounum")).Text.ToString();
                if (vounum == "00000000000000")
                {
                    lbtnapprov.Enabled = true;
                    LnkEdit.Enabled = true;
                }
                else
                {
                    lbtnapprov.Text = "<i class='fa fa-lock'></i>";
                    lbtnapprov.CssClass = "btn btn-xs btn-danger";
                    lbtnapprov.Enabled = false;
                    LnkEdit.Enabled = false;
                }
                LnkEdit.NavigateUrl = "~/F_10_Procur/PurchaseReturn?Type=Entry&centrid=" + centrid + "&date=" + date + "&genno=" + retno;
                LnkEdit.Target = "blank";

            }
        }

        protected void LbtnApp_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            string comcod = this.GetCompCode();
            int index = row.RowIndex;
            string centrid = ((Label)this.GvPurReturn.Rows[index].FindControl("lgcCentrid")).Text.ToString();
            string retmemo = ((Label)this.GvPurReturn.Rows[index].FindControl("lgcPurRetno")).Text.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string appovedat = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = InvData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PURCHASE_RETURN_APPROVE", centrid, retmemo, usrid
                , sessionid, trmid, PostedDat, appovedat, "", "", "", "", "", "", "", "");
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully!');", true);

            }
            this.Data_Bind();
        }

        protected void LbtnPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string centerid = ((Label)this.GvPurReturn.Rows[index].FindControl("lgcCentrid")).Text.Trim();
            string returnno = ((Label)this.GvPurReturn.Rows[index].FindControl("lgcPurRetno")).Text.Trim();
            string returnDate = Convert.ToDateTime(((Label)this.GvPurReturn.Rows[index].FindControl("lgcRetDate")).Text.Trim()).ToString("dd-MMM-yyyy");
            

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".PNG")).AbsoluteUri;

            DataSet ds = InvData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_PURCHASE_RETURN_INFO", centerid, returnno);
            string custmChlln = ds.Tables[1].Rows[0]["custmchlln"].ToString();
            if (ds == null) return;
            if (ds.Tables[0].Rows.Count == 0) return;

            var lst = ds.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.EclassPurReturn>();
            if (lst.Count == 0)
                return;

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptPurchaseReturn", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("ComLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Material Purchase Return Note"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            rpt1.SetParameters(new ReportParameter("returndate", returnDate));
            rpt1.SetParameters(new ReportParameter("returnno", returnno));
            rpt1.SetParameters(new ReportParameter("custmchlln", custmChlln));

            Session["Report1"] = rpt1;

            ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRdLc('PDF');", true);

        }

    }
}