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
using SPERDLC;

namespace SPEWEB.F_19_EXP
{
    public partial class AllCollection : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ManProd") ? "" : "Collection List";
                this.GetBuyer();
                this.selectview();
                this.txtAppDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            string qType = this.Request.QueryString["Type"].ToString();
            if (qType == "All")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("href", "../F_19_EXP/MoneyReceipt2.aspx?Type=Entry&genno=&centrid=&sircode=");
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("target", "_blank");
            }

            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
            // ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {



        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();

        }
        private void selectview()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "All":

                    this.Multivew.ActiveViewIndex = 0;
                    break;

            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "All":

                    this.GetCollectionList();
                    break;
                case "ManProd":

                    break;
            }


        }
        private void GetCollectionList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtFdate = this.txtfromdate.Text.ToString();
            string txttdate = this.txttodate.Text.ToString();

            string buyerid = ""; // this.ddlAccProject.SelectedValue.ToString();
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    buyerid += item.Value;
                }
            }

            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_REPORT_EXPORT", "COLLECTIONTOPLIST", txtFdate, txttdate, buyerid, " ", "", "", "");

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.BO_Collection>();


            ViewState["tblColl"] = list;
            if (list == null)
            {
                return;
            }
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "All":
                    List<SPEENTITY.C_19_Exp.BO_Collection> manlst = (List<SPEENTITY.C_19_Exp.BO_Collection>)ViewState["tblColl"];
                    this.gvCollectionAll.DataSource = manlst;
                    this.gvCollectionAll.DataBind();
                    if (manlst.Count == 0)
                        return;
                    this.FooterCalculation();
                    //Session["Report1"] = gvCollectionAll;
                    //((HyperLink)this.gvCollectionAll.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;

            }

        }

        private void FooterCalculation()
        {
            var lst = (List<SPEENTITY.C_19_Exp.BO_Collection>)ViewState["tblColl"];

            if (lst.Count == 0)
                return;

            ((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFfcamt")).Text = ((lst.Select(p => p.fcamt).Sum() == 0.00) ? 0.00 : lst.Select(p => p.fcamt).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFtrnamount")).Text = ((lst.Select(p => p.amount).Sum() == 0.00) ? 0.00 : lst.Select(p => p.amount).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFfcbnkcharge")).Text = ((lst.Select(p => p.fcbnkcharge).Sum() == 0.00) ? 0.00 : lst.Select(p => p.fcbnkcharge).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFvatamt")).Text = ((lst.Select(p => p.vatamt).Sum() == 0.00) ? 0.00 : lst.Select(p => p.vatamt).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFcglamt")).Text = ((lst.Select(p => p.cglamt).Sum() == 0.00) ? 0.00 : lst.Select(p => p.cglamt).Sum()).ToString("#,##0;(#,##0); ");


        }
        protected void gvCollectionAll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lbtnEdit");
                HyperLink pLink = (HyperLink)e.Row.FindControl("LbtnPrint");
                LinkButton lnkCheck = (LinkButton)e.Row.FindControl("lnkCheck");

                string grpmemo = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "memono")).ToString();
                string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                string custid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "custid")).ToString();
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                string comcod = this.GetCompCode();
                //string ptype = this.Request.QueryString["Type"].ToString();
                hlink3.NavigateUrl = "~/F_19_EXP/MoneyReceipt2.aspx?Type=Edit&genno=" + grpmemo + "&centrid=" + "&sircode=" + custid;
                hlink3.Target = "blank";
                pLink.NavigateUrl = "~/F_19_EXP/ExpPrint.aspx?Type=MoneyReceipt&mrno=" + grpmemo;
                pLink.Target = "blank";


                if (vounum != "00000000000000")
                {
                    hlink3.Visible = false;
                    lnkCheck.Visible = false;
                }
                //HyperLink Lbtn = (HyperLink)e.Row.FindControl("LbtnApp");
                //string status = ((Label)e.Row.FindControl("LblApstats")).Text.ToString();
                //string mgrrno = ((Label)e.Row.FindControl("lvgrrno")).Text.ToString();

                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");


                //hlink1.NavigateUrl = "~/F_15_Pro/Print.aspx?Type=ReqPrint&genno=" + mgrrno;




                //if (status == "False")
                //{
                //    Lbtn.NavigateUrl = "~/F_15_Pro/ProductionManually.aspx?Type=Approve&genno=" + mgrrno;
                //    Lbtn.Target = "blank";

                //}
                //else
                //{
                //    Lbtn.Text = "<span class='glyphicon glyphicon-lock'></span>";
                //    Lbtn.CssClass = "btn btn-xs btn-danger";
                //    Lbtn.ToolTip = "Approved";
                //}


            }
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }


        protected void lnkCheck_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string AppDate = this.txtAppDate.Text;



            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string centrid = ((Label)gvCollectionAll.Rows[index].FindControl("lblgvcentrid")).Text.ToString();
            string Memono = ((Label)gvCollectionAll.Rows[index].FindControl("lblgvMemono")).Text.ToString();

            bool result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "APPCOLLECTION", centrid, Memono, AppDate, userid, Terminal, Sessionid, Posteddat);

            if (result == true)
            {
                //this.ImportInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Successfully');", true);
            }
        }



    }
}