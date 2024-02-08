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
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_10_Procur
{
    public partial class RptMatPurHistory : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "PURCHASE HISTORY - MATERIAL WISE";
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                if (this.ddlProjectName.Items.Count == 0)
                {
                    this.GetProjectName();
                    this.GetMaterial();
                }


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            //this.GetSupplier();
        }

        private void GetMaterial()
        {
            string comcod = this.GetComeCode();
            string txtfindMat =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIALHISTORY", txtfindMat, "", "", "", "", "", "", "", "");
            this.ddlMaterialName.DataTextField = "rsirdesc";
            this.ddlMaterialName.DataValueField = "rsircode";
            this.ddlMaterialName.DataSource = ds1.Tables[0];
            this.ddlMaterialName.DataBind();

        }

        protected void imgbtnFindMat_Click(object sender, EventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var mattName = this.ddlMaterialName.SelectedItem.Text.Trim().Substring(14);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["MatPurHis"];
            var rptlist = dt.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.HisMaterial>();
            LocalReport Rpt1a = new LocalReport();

            Rpt1a = RptSetupClass.GetLocalReport("R_10_Procur.RptHisMaterial", rptlist, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("mattName", mattName));
            Rpt1a.SetParameters(new ReportParameter("rptitle", "PURCHASE HISTORY - MATERIAL WISE"));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #region old
            //ReportDocument rptstate = new RMGiRPT.R_11_Pro.RptMatPurHistory();
            //TextObject rpttxtMaterial = rptstate.ReportDefinition.ReportObjects["txtMaterial"] as TextObject;
            //rpttxtMaterial.Text = this.ddlMaterialName.SelectedItem.Text.Trim().Substring(14) + "         " + this.lblUnit.Text.Trim();
            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd/MM/yyyy");
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource(dt);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Purchase History";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlProjectName.SelectedItem.ToString() + " From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd/MM/yyyy");
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }

        protected void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                this.FromD.Visible = false;
            }
            else
            {
                this.FromD.Visible = true;
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("MatPurHis");
            string comcod = this.GetComeCode();
            string proname = ddlProjectName.SelectedValue.Substring(0, 12).ToString();
            string matname = this.ddlMaterialName.SelectedValue.Substring(0, 12).ToString();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string uptoDate = (this.chkDate.Checked) ? "OpDate" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATPURHISTORY", proname, matname, frmdate, todate, uptoDate, "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatPurHis.DataSource = null;
                this.gvMatPurHis.DataBind();
                return;
            }
            this.gvMatPurHis.Columns[5].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            this.lblUnit.Text = ds1.Tables[1].Rows[0]["sirunit"].ToString();
            Session["MatPurHis"] = this.HiddenSameDate(ds1.Tables[0]);
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase History";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void Data_Bind()
        {
            this.gvMatPurHis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatPurHis.DataSource = (DataTable)Session["MatPurHis"];
            this.gvMatPurHis.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["MatPurHis"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvMRRQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                                0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string mrrno = dt1.Rows[0]["mrrno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["mrrno"].ToString() == mrrno))
                {
                    mrrno = dt1.Rows[j]["mrrno"].ToString();
                    dt1.Rows[j]["mrrno1"] = "";
                    dt1.Rows[j]["mrrdat1"] = "";
                }

                else
                {
                    mrrno = dt1.Rows[j]["mrrno"].ToString();
                }

            }
            return dt1;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
        protected void ImgBtnMatName_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
        protected void gvMatPurHis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatPurHis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}