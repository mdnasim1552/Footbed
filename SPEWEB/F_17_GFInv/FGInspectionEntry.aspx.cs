using System;
using SPELIB;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_17_GFInv
{
    public partial class FGInspectionEntry : System.Web.UI.Page
    {
        ProcessAccess GetData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "Entry") ? "Finish Goods Inspection Data Entry" : "";
                this.CommonButton();
                this.GetSeason();
                this.DdlSeason_SelectedIndexChanged(null, null);
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
        }

        public void CommonButton()
        {
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetSeason()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = GetData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";
            }

            this.DdlSeason_SelectedIndexChanged(null, null);
        }

        protected void GetProductionLine()
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds1 = GetData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPRODSETUPINFO", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                string frmProcess = "800100101022";

                DataTable dt = new DataTable();
                DataView dv = ds1.Tables[1].AsDataView();

                dv.RowFilter = "prodprocess = " + frmProcess + "";
                dt = dv.ToTable();

                this.ddlToProcess.DataTextField = "sirdesc";
                this.ddlToProcess.DataValueField = "sircode";
                this.ddlToProcess.DataSource = dt;
                this.ddlToProcess.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 = GetData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblordstyle"] = ds1.Tables[0];
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();

            this.ddlOrderList.DataTextField = "styledesc2";
            this.ddlOrderList.DataValueField = "stylecode2";
            this.ddlOrderList.DataSource = dt1;
            this.ddlOrderList.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lbtnOk.Text == "Ok")
                {
                    this.lbtnOk.Text = "New";
                    this.PnlFGIEntry.Visible = true;
                    this.DdlSeason.Enabled = false;
                    this.ddlOrderList.Enabled = false;

                    this.lblOrderNo.Text = String.Empty;
                    this.lblCustomer.Text = String.Empty;
                    this.ArticleName.Text = String.Empty;
                    this.lblColor.Text = String.Empty;
                    this.lblOrdQty.Text = String.Empty;
                    this.txtInspQty.Text = String.Empty;
                    this.lblFailQty.Text = String.Empty;
                    this.lblPassedQty.Text = String.Empty;
                    this.lblFtprRatio.Text = String.Empty;
                    this.GetProductionLine();
                    this.Data_Bind();
                    return;
                }
                this.lbtnOk.Text = "Ok";
                this.PnlFGIEntry.Visible = false;
                this.DdlSeason.Enabled = true;
                this.ddlOrderList.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void Save_Value()
        {   
            double criticalFail = Convert.ToDouble("0" + this.txtCritical.Text);
            double MajorFail = Convert.ToDouble("0" + this.txtMajor2.Text);
            double MinorFail = Convert.ToDouble("0" + this.txtMinor2.Text);
            double totalFail = criticalFail + MajorFail + MinorFail;
            this.lblFailQty.Text = totalFail.ToString("#,##0.00;(#,##0.00); ");
            double insQty = Convert.ToDouble("0" + this.txtInspQty.Text);

            double passQty = insQty - totalFail;
            this.lblPassedQty.Text = passQty.ToString("#,##0.00;(#,##0.00); ");

            if (insQty != 0)
            {
                double ratio = (passQty / insQty) * 100;
                this.lblFtprRatio.Text = ratio.ToString("#,##0.00") + " %";
            }

        }

        private string[] BreakString(string input, int chunkSize)
        {
            string[] Arr = new string[input.Length/chunkSize];

            try
            {
                int n = 0;

                for (int i = 0; i < input.Length; i += chunkSize)
                {
                    string chunk = input.Substring(i, Math.Min(chunkSize, input.Length - i));

                    Arr[n] = chunk;
                    n++;
                }

            }
            catch(Exception ex)
            {

            }
            return Arr;
        }

        private void Data_Bind()
        {
            try
            {
                string comcod = this.GetComCode();
                string mlccod = this.ddlOrderList.SelectedValue.Substring(0, 12);
                string dayid = this.ddlOrderList.SelectedValue.Substring(36, 8);

                DataSet ds2 = GetData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_QC", "GET_ORDER_FINAL_INSPECTION_INFO", mlccod, dayid, "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables.Count < 0)
                {
                    return;
                }

                if (ds2.Tables[0].Rows.Count < 1)
                {
                    return;
                }
                string totallinecode = ds2.Tables[0].Rows[0]["linecode"].ToString();

                string[] linecodes = BreakString(totallinecode, 12);

                foreach (ListItem item in ddlToProcess.Items)
                {
                    for(int i=0; i< linecodes.Length; i++)
                    {
                        if(item.Value == linecodes[i])
                        {
                            item.Selected = true;
                        }
                    }
                }

                ViewState["FGInpection"] = ds2.Tables[0];
                DataTable dt2 = (DataTable)ViewState["FGInpection"];
                var list1 = dt2.DataTableToList<SPEENTITY.C_17_GFInv.FGInspection>();
                if (list1.Count == 0)
                    return;

                this.lblOrderNo.Text = list1[0].orderno;
                this.lblOrdQtyDemo.Text = list1[0].ordrqty.ToString();
                this.lblOrdQty.Text = list1[0].ordrqty.ToString("#,##0.00;(#,##0.00); ");
                this.lblCustomer.Text = list1[0].buyername;
                this.ArticleName.Text = list1[0].styledesc;
                this.lblColor.Text = list1[0].colordesc;
                this.TxtInspectBy.Text = list1[0].insbyname;
                this.txtInspDate.Text = (list1[0].insbydate.ToString("dd-MMM-yyyy") == "01-Jan-1900") ? (System.DateTime.Today.ToString("dd-MMM-yyyy")) : (list1[0].insbydate.ToString("dd-MMM-yyyy"));

                this.txtInspQty.Text = list1[0].insqty.ToString();
                this.txtCritical.Text = list1[0].fcritical.ToString();
                this.txtMajor2.Text = list1[0].fmajor.ToString();
                this.txtMinor2.Text = list1[0].fminor.ToString();
                this.lblFailQty.Text = list1[0].failqty.ToString();
                this.lblPassedQty.Text = list1[0].passqty.ToString();
                this.DDLResult.SelectedValue = list1[0].result.ToString();
                this.TxtRemarks.Text = list1[0].remarks.ToString();

                if (DDLResult.SelectedValue == "False")
                {
                    this.DDLResult.BackColor = System.Drawing.Color.OrangeRed;
                    this.DDLResult.ForeColor = System.Drawing.Color.White;
                    this.DDLResult.Font.Bold = true;
                }
                else
                {
                    this.DDLResult.BackColor = System.Drawing.Color.LimeGreen;
                    this.DDLResult.ForeColor = System.Drawing.Color.White;
                    this.DDLResult.Font.Bold = true;
                }

                double passedQty = list1[0].passqty; ;
                double insQty = list1[0].insqty;
                if (insQty != 0)
                {
                    double ratio = (passedQty / insQty) * 100;
                    this.lblFtprRatio.Text = ratio.ToString("#,##0.00") + " %";
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["FGInpection"];
            double orderQty = Convert.ToDouble(dt.Rows[0]["ordrqty"]);
            double insQty = Convert.ToDouble("0" + this.txtInspQty.Text);

            if (orderQty < insQty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Inspection Qty cannot be more than the Order Qty!');", true);
                return;
            }
            else
            {
                this.Save_Value();
            }
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Save_Value();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComCode();
                string postedByid = hst["usrid"].ToString();
                string postedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                string mlccod = this.ddlOrderList.SelectedValue.Substring(0, 12);
                string dayid = this.ddlOrderList.SelectedValue.Substring(36, 8);
                string inspectionby = TxtInspectBy.Text.Trim().ToString();
                string inspectionDate = txtInspDate.Text.Trim().ToString();
                string remarks = TxtRemarks.Text.Trim().ToString();
                string inspResult = DDLResult.SelectedValue.ToString();
                string inspectQty = txtInspQty.Text.Trim().ToString();
                inspectQty = inspectQty == "" ? "0" : inspectQty;
                string failQty = lblFailQty.Text.Trim().ToString();
                failQty = failQty == "" ? "0" : failQty;
                string CriticQty = txtCritical.Text.Trim().ToString();
                CriticQty = CriticQty == "" ? "0" : CriticQty;
                string majorQty = txtMajor2.Text.Trim().ToString();
                majorQty = majorQty == "" ? "0" : majorQty;
                string minorQty = txtMinor2.Text.Trim().ToString();
                minorQty = minorQty == "" ? "0" : minorQty;

                string allowmajorqty = txtMajor.Text;
                allowmajorqty = allowmajorqty == "" ? "0" : allowmajorqty;
                string allowminorqty = txtMinor.Text;
                allowminorqty = allowminorqty == "" ? "0" : allowminorqty;
                string linecod = "";

                foreach(ListItem item in ddlToProcess.Items)
                {
                    if (item.Selected)
                    {
                        linecod += item.Value;
                    }
                }

                bool result = GetData.UpdateTransInfo(comcod, "SP_REPORT_PRODUCTION_QC", "UPDATE_FINAL_INSPECTION_INFO", mlccod, dayid, inspectQty, inspectionby, inspectionDate, remarks, inspResult, failQty, CriticQty, majorQty, minorQty, postedByid, postedDate, allowmajorqty, allowminorqty, linecod);
                if (!result)
                    return;

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                this.Data_Bind();
            }

            catch(Exception ex) 
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;
            }

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string footer = compname + ", Session: " + session + ", User: " + username + ", Time: " + printdate;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptTile = "Finish Goods Inspection Data Entry";

            string linedesc = "";
            foreach (ListItem item in ddlToProcess.Items)
            {
                if (item.Selected)
                {
                    linedesc += item.Text + ". ";
                }
            }


            DataTable dt3 = (DataTable)ViewState["FGInpection"];
            if (dt3 == null)
                return;

            var list3 = dt3.DataTableToList<SPEENTITY.C_17_GFInv.FGInspection>();
            LocalReport Rpt1a = new LocalReport();
            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_17_GFInv.RptFGInspectionEntry", list3, null, null);

            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("linedesc", linedesc));
            Rpt1a.SetParameters(new ReportParameter("rptTile", rptTile));
            Rpt1a.SetParameters(new ReportParameter("footer", footer));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}