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
using System.IO;
using SPELIB;

namespace SPEWEB.F_11_RawInv
{
    public partial class AllIndentIsuList : System.Web.UI.Page
    {

        ProcessAccess _DataEntry = new ProcessAccess();
        ProcessAccess _userData = new ProcessAccess();

        public static string memono = "", centrid = "", custid = "", memono1 = "", InvDate = "", Desc = "", Rescode = "", Date = "";
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Indent Issue LIST ";

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                this.CommonButton();
            }

        }
        //page pre init()
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click1);
            //  ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).Checked += new EventHandler(chkBoxN_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("href", "../F_11_RawInv/Material_Issue?Type=Entry&genno=");
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("target", "_blank");
            

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


        }
        protected void lnkbtnNew_Click1(object sender, EventArgs e)
        {

            string qType = this.Request.QueryString["Type"].ToString();
            if (qType == "Entry")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_07_Inv/Material_Issue?Type=Entry&genno=" + "', target='_blank');</script>";
            }

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.GetIndentIssueList();
        }






        private string GetcompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetIndentIssueList()
        {
            string comcod = this.GetcompCode();
            string frmDate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_MATERIAL_ISSUE", "RPTINDENTITEMSUMMARY", frmDate, todate, "", "");
            var replist = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>();
            if (replist == null)
            {
                this.gvPromData.DataSource = null;
                this.gvPromData.DataBind();
                return;
            }

            ViewState["tblPromTop"] = replist;
            Session["PromMatHistory"] = replist;
            this.Data_Bind();
        }
        public void Data_Bind()
        {
            var lst = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromTop"];

            this.gvPromData.DataSource = lst;
            this.gvPromData.DataBind();

        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void gvPromData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPromData.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void lbtnChalan_Click(object sender, EventArgs e)
        {
            //this.LblMsg.Visible = true;
            //string comcod = this.GetcompCode();
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            //int index = row.RowIndex;

            //string centrid = ((Label)this.gvPromData.Rows[index].FindControl("lblgvcentrid")).Text.ToString();
            //string repmemo = ((Label)this.gvPromData.Rows[index].FindControl("lblgvRepNo")).Text.ToString();
            //string delvdat= System.DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();
            //DataSet ds = _DataEntry.GetTransInfo(comcod, "SP_REPORT_SALES_RETURN", "REPLCEDELVUPDATE", centrid, repmemo, userid, delvdat, "", "", "", "");
            //if (ds == null)
            //{
            //    this.LblMsg.Text = "Update Invalid";
            //    return;
            //}
            //else
            //{
            //    this.LblMsg.Text = ds.Tables[0].Rows[0]["msg"].ToString();
            //    this.GetIndentIssueList();
            //}

        }

        protected void gvPromData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HypRDDoPrint");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("BtnEdit");
                HyperLink hlink4 = (HyperLink)e.Row.FindControl("BtnAudit");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string issueno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issueno")).ToString();
                string issuedat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "issuedat")).ToString("dd-MMM-yyyy");
                string apstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "apstatus"));
                string authstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "authstatus"));


                hlink3.NavigateUrl = "~/F_21_GAcc/Print?Type=IssueChallan&comcod=" + comcod + "&issueno=" + issueno + "&issuedat=" + issuedat;

                if (apstatus == "False" )
                {
                    hlink1.NavigateUrl = "~/F_11_RawInv/Material_Issue?Type=Approve&genno=" + issueno +"&actcode=" + authstatus;
                    hlink1.Target = "blank";
                }

                if (authstatus == "False" )
                {
                    hlink4.NavigateUrl = "~/F_11_RawInv/Material_Issue?Type=Audit&genno=" + issueno;
                    hlink4.Target = "blank";
                }
            }
        }

        protected void LbtnDelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                return;
            }
            
            string comcod = this.GetcompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;

            string isuno = ((Label)this.gvPromData.Rows[index].FindControl("lblissueno")).Text.ToString();
            DataSet res = _DataEntry.GetTransInfo(comcod, "SP_REPORT_MATERIAL_ISSUE", "DEL_IND_ISSUE", isuno, "", "", "", "", "", "");
            if (res == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delect Invalid');", true);

                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ res.Tables[0].Rows[0]["msg"].ToString() + "');", true);

              
                this.GetIndentIssueList();
            }

        }
    }
}