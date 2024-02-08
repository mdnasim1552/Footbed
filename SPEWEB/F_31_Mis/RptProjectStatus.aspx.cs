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
using CrystalDecisions.CrystalReports.Engine;
using SPELIB;

namespace SPEWEB.F_31_Mis
{
    public partial class RptProjectStatus : System.Web.UI.Page
    {
        ProcessAccess prjData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.SelectView();
                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "LCStatus") ? "LC Status" : "";


                this.lbtnShow_Click(null, null);
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LCStatus":
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    //this.txtfromdate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


            }
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "LCStatus":
                    this.ShowMonLCStatus();
                    break;

            }


        }


        private void ShowMonLCStatus()
        {
            Session.Remove("tbLcStatus");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
            string FcAmt = (this.chkFcAmt.Checked) ? "FcAmt" : "";
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_LC_STATUS", "RPTMONLCSTATUS", frmdate, dd2value, FcAmt, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMonPorStatus.DataSource = null;
                this.gvMonPorStatus.DataBind();
                return;

            }

            Session["tbLcStatus"] = ds1.Tables[0];
            ViewState["tblresdesc"] = ds1.Tables[1];
            ViewState["tblresdesc1"] = ds1.Tables[2];
            ds1.Dispose();
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "LCStatus":
                    DataTable dtpname = (DataTable)ViewState["tblresdesc"];
                    int j = 8;
                    for (int i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvMonPorStatus.Columns[j].HeaderText = dtpname.Rows[i]["resdesc"].ToString();
                        j++;
                        if (j == 14)
                            break;


                    }
                    DataTable dtpname1 = (DataTable)ViewState["tblresdesc1"];
                    int k = 18;
                    for (int i = 0; i < dtpname1.Rows.Count; i++)
                    {

                        this.gvMonPorStatus.Columns[k].HeaderText = dtpname1.Rows[i]["resdesc"].ToString();
                        k++;
                        if (k == 26)
                            break;


                    }

                    this.gvMonPorStatus.DataSource = (DataTable)Session["tbLcStatus"];
                    this.gvMonPorStatus.DataBind();
                    Session["Report1"] = gvMonPorStatus;
                    if (((DataTable)Session["tbLcStatus"]).Rows.Count > 0)
                        ((HyperLink)this.gvMonPorStatus.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooteCalculation();
                    break;


            }


        }
        private void FooteCalculation()
        {
            DataTable dt = (DataTable)Session["tbLcStatus"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LCStatus":
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ? 0.00 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFshiqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shiqty)", "")) ? 0.00 : dt.Compute("sum(shiqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFseqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(seqty)", "")) ? 0.00 : dt.Compute("sum(seqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tramt)", "")) ? 0.00 : dt.Compute("sum(tramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r1)", "")) ? 0.00 : dt.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r2)", "")) ? 0.00 : dt.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r3)", "")) ? 0.00 : dt.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tcost)", "")) ? 0.00 : dt.Compute("sum(tcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r4)", "")) ? 0.00 : dt.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r5)", "")) ? 0.00 : dt.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r6)", "")) ? 0.00 : dt.Compute("sum(r6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r7)", "")) ? 0.00 : dt.Compute("sum(r7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r8)", "")) ? 0.00 : dt.Compute("sum(r8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r9)", "")) ? 0.00 : dt.Compute("sum(r9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r10)", "")) ? 0.00 : dt.Compute("sum(r10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r1)", "")) ? 0.00 : dt.Compute("sum(r11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r12)", "")) ? 0.00 : dt.Compute("sum(r12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r13)", "")) ? 0.00 : dt.Compute("sum(r13)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r14)", "")) ? 0.00 : dt.Compute("sum(r14)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r15)", "")) ? 0.00 : dt.Compute("sum(r15)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r16)", "")) ? 0.00 : dt.Compute("sum(r16)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r17)", "")) ? 0.00 : dt.Compute("sum(r17)", ""))).ToString("#,##0;(#,##0); ");
                    // ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r18)", "")) ? 0.00 : dt.Compute("sum(r18)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r19)", "")) ? 0.00 : dt.Compute("sum(r19)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r20)", "")) ? 0.00 : dt.Compute("sum(r20)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toramt)", "")) ? 0.00 : dt.Compute("sum(toramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFnetposition")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 : dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }





        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LCStatus":
                    this.PrintMonProStatus();
                    break;
            }


        }

        private void PrintMonProStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbLcStatus"];

            ReportDocument rptmprost = new RMGiRPT.R_31_Mis.RptLCStatus();
            TextObject rptComp = rptmprost.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptComp.Text = comnam;
            TextObject rptdate = rptmprost.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptdate.Text = "As on Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            DataTable dtrname = (DataTable)ViewState["tblresdesc"];
            int j = 1;
            for (int i = 0; i < dtrname.Rows.Count; i++)
            {

                TextObject rpttxth = rptmprost.ReportDefinition.ReportObjects["r" + j.ToString()] as TextObject;
                rpttxth.Text = dtrname.Rows[i]["resdesc"].ToString();
                j++;
                if (j == 7)
                    break;


            }

            DataTable dtrname1 = (DataTable)ViewState["tblresdesc1"];
            int k = 7;
            for (int i = 0; i < dtrname1.Rows.Count; i++)
            {

                TextObject rpttxth = rptmprost.ReportDefinition.ReportObjects["r" + k.ToString()] as TextObject;
                rpttxth.Text = dtrname1.Rows[i]["resdesc"].ToString();
                k++;
                if (j == 14)
                    break;


            }

            TextObject txtuserinfo = rptmprost.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptmprost.SetDataSource(dt);
            Session["Report1"] = rptmprost;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }


    }
}