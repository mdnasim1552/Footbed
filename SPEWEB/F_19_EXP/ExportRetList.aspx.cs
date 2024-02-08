using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System.Collections;
using SPEENTITY.C_19_Exp;

namespace SPEWEB.F_19_EXP
{
    public partial class ExportRetList : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Export Return List";
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

            if (type == "ExpList")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "ExpList")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("href", "../F_19_EXP/ExportReturn?Type=Entry&actcode=&date=&genno=");

            }
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("target", "_blank");

            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnAll_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click);

        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void Get_Export_ReturnList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtFdate = this.txtFromDate.Text.ToString();
            string txttdate = this.txtToDate.Text.ToString();

            DataSet ds1 = InvData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_RETURN_LIST", txtFdate, txttdate);

            if (ds1 == null)
            {
                return;
            }

            ViewState["expreturnlist"] = ds1.Tables[0];

            this.Data_Bind();

        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "ExpList")
            {

                DataTable dt1 = ((DataTable)ViewState["expreturnlist"]).Copy();
                if (dt1.Rows.Count == 0)
                    return;
                this.GvExpReturn.DataSource = dt1;
                this.GvExpReturn.DataBind();
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "ExpList")
            {
                this.Get_Export_ReturnList();
            }

        }

        private List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExportList> HiddenSameData(List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExportList> lst)
        {
            if (lst.Count == 0)
                return lst;

            int i = 0;
            string procode = lst[0].actdesc;

            foreach (SPEENTITY.C_19_Exp.EClassExpBO.EclassExportList lst1 in lst)
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

        
        protected void GvExpReturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink LnkEdit = (HyperLink)e.Row.FindControl("LnkEdit");
                LinkButton lbtnapprov = (LinkButton)e.Row.FindControl("LbtnApp");
                string mlccode = ((Label)e.Row.FindControl("lblStore")).Text.ToString();
                string retno = ((Label)e.Row.FindControl("lblRetNo")).Text.ToString();
                string date = ((Label)e.Row.FindControl("lblRetDate")).Text.ToString();
                //string vounum = ((Label)e.Row.FindControl("lgvVounum")).Text.ToString();
                //if (vounum == "00000000000000")
                //{
                //    lbtnapprov.Enabled = true;
                //    LnkEdit.Enabled = true;
                //}
                //else
                //{
                //    lbtnapprov.Text = "<i class='fa fa-lock'></i>";
                //    lbtnapprov.CssClass = "btn btn-xs btn-danger";
                //    lbtnapprov.Enabled = false;
                //    LnkEdit.Enabled = false;
                //}

                LnkEdit.NavigateUrl = "~/F_19_EXP/ExportReturn?Type=Entry&actcode=" + mlccode + "&date=" + date + "&genno=" + retno;
                LnkEdit.Target = "blank";

            }
        }

        protected void LbtnApp_Click(object sender, EventArgs e)
        {
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //string comcod = this.GetCompCode();
            //int index = row.RowIndex;
            //string centrid = ((Label)this.GvExpReturn.Rows[index].FindControl("lgcCentrid")).Text.ToString();
            //string retmemo = ((Label)this.GvExpReturn.Rows[index].FindControl("lgcExpRetno")).Text.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string usrid = hst["usrid"].ToString();
            //string sessionid = hst["session"].ToString();
            //string trmid = hst["compname"].ToString();
            //string appovedat = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //bool result = InvData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PURCHASE_RETURN_APPROVE", centrid, retmemo, usrid
            //    , sessionid, trmid, PostedDat, appovedat, "", "", "", "", "", "", "", "");
            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully!');", true);

            //}
            //this.Data_Bind();
        }

        protected void LbtnPrint_Click(object sender, EventArgs e)
        {
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;

            //string centerid = ((Label)this.GvExpReturn.Rows[index].FindControl("lgcCentrid")).Text.Trim();
            //string returnno = ((Label)this.GvExpReturn.Rows[index].FindControl("lgcExpRetno")).Text.Trim();
            //string returnDate = Convert.ToDateTime(((Label)this.GvExpReturn.Rows[index].FindControl("lgcRetDate")).Text.Trim()).ToString("dd-MMM-yyyy");


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".PNG")).AbsoluteUri;

            //DataSet ds = InvData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_PURCHASE_RETURN_INFO", centerid, returnno);
            //string custmChlln = ds.Tables[1].Rows[0]["custmchlln"].ToString();
            //if (ds == null) return;
            //if (ds.Tables[0].Rows.Count == 0) return;

            //var lst = ds.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.EClassExpBOReturn>();
            //if (lst.Count == 0)
            //    return;

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptPurchaseReturn", lst, null, null);
            //rpt1.EnableExternalImages = true;

            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("ComLogo", comLogo));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "Export Return Note"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("returndate", returnDate));
            //rpt1.SetParameters(new ReportParameter("returnno", returnno));
            //rpt1.SetParameters(new ReportParameter("custmchlln", custmChlln));

            //Session["Report1"] = rpt1;

            //ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRdLc('PDF');", true);

        }
    }
}