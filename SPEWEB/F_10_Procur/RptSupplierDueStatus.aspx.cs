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
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_10_Procur
{
    public partial class RptSupplierDueStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string Type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "SupOutStan") ? "Suppliers Outstanding Statement" 
                                                                  : (Type == "IQCInspection") ? "IQC Inspection Report" : "Indent Materials Distribution";
                
                this.CommonButton();
                this.ShowView();

            }
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //this.gvPromData.Columns[6].HeaderText = hst["territory"].ToString();

        }

        private void ShowView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "SupOutStan":
                    this.ddlSuplier.Visible = true;
                    this.lblSupl.Visible = true;
                    this.GetSupplier();
                    this.Multiview1.ActiveViewIndex = 0;

                    break;

                case "PromMatHis":
                    this.GetProjectName();
                    GetEmployeeList();
                    this.Multiview1.ActiveViewIndex = 1;
                    this.lblSupl.Visible = false;
                    this.ddlSuplier.Visible = false;
                    this.ChkSum.Visible = false;
                    break;

                case "IQCInspection":
                    this.ddlSuplier.Visible = true;
                    this.lblSupl.Visible = true;
                    this.ChkSum.Visible = false;
                    this.GetSupplier();
                    this.Multiview1.ActiveViewIndex = 2;
                    break;

            }
        }

        private void GetEmployeeList()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETEMPNAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlEmpList.Items.Clear();
                return;
            }

            this.ddlEmpList.DataTextField = "sirdesc";
            this.ddlEmpList.DataValueField = "sircode";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            ds1.Dispose();

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = (hst["usrid"].ToString());
            string comcod = this.GetComeCode();

            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "1[15]%", "FxtAst", "", userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            DataTable dt2 = ds2.Tables[0];
            DataRow dr2 = dt2.NewRow();
            dr2["actdesc1"] = "All";
            dr2["actcode"] = "000000000000";
            dt2.Rows.Add(dr2);
            dt2.DefaultView.Sort = "actcode";

            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = dt2.DefaultView.ToTable();
            this.ddlProject.DataBind();

        }

        private void GetSupplier()
        {

            string comcod = this.GetComeCode();
            string mSrchTxt = "%";

            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlSuplier.DataTextField = "ssirdesc1";
            this.ddlSuplier.DataValueField = "ssircode";
            this.ddlSuplier.DataSource = ds2.Tables[0];
            this.ddlSuplier.DataBind();
            this.ddlSuplier.SelectedValue = "000000000000";

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

        private void ShowSupDueStatus()
        {

            ViewState.Remove("tblstatus");
            string comcod = this.GetComeCode();
            string Fdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string rescode = (this.ddlSuplier.SelectedValue.ToString() == "000000000000") ? "99%" : this.ddlSuplier.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "RPTSUPOUTSTSTATEMENT", Fdate, todate, rescode, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvSupDueStatus.DataSource = null;
                this.gvSupDueStatus.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger();", true);
                return;
            }
            //DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            ViewState["tblstatus"] = ds1.Tables[0];
            this.Data_Bind();

        }


        private void ShowPromHisStatus()
        {
            ViewState.Remove("tblPromHis");
            ViewState.Remove("tblPromHis_summ");
            string comcod = this.GetComeCode();
            string Fdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProject.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProject.SelectedValue.ToString() + "%";
            string person = this.ddlEmpList.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlEmpList.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MATERIAL_ISSUE", "RPTINDENTITEM", Fdate, todate, pactcode, person, "", "", "", "", "");

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>();
            var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>();
            var lst2 = ds1.Tables[2].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>();

            ViewState["tblPromHis"] = HiddenSameDataProm(lst);
            ViewState["tblPromHis_summ"] = lst1;
            ViewState["tblPromHisteamsum"] = lst2;

            this.Data_Bind();

        }

        private List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory> HiddenSameDataProm(List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string issueno = "";
            foreach (SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory c1 in lst3)
            {
                if (i == 0)
                {
                    issueno = c1.issueno;
                    i++;
                    continue;

                }
                if (c1.issueno == issueno)
                {
                    c1.idcard = "";
                    //   c1.issuedat = "";
                    // c1.memodat ="";
                    c1.empname = "";
                    c1.territory = "";
                    c1.issueno = "";

                }
                else
                {
                    issueno = c1.issueno;
                }
            }

            return lst3;

        }

        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "SupOutStan":
                    if (this.ChkSum.Checked == false)
                    {
                        this.gvSupDueStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvSupDueStatus.DataSource = (DataTable)ViewState["tblstatus"];
                        this.gvSupDueStatus.DataBind();
                        this.FooterCalculation();
                    }
                    else
                    {
                        DataTable dt = (DataTable)ViewState["tblstatus1"];
                        this.gvOutstndSum.Columns[3].HeaderText = System.DateTime.Today.AddMonths(-3).ToString("MMM-yyyy");
                        this.gvOutstndSum.Columns[4].HeaderText = System.DateTime.Today.AddMonths(-2).ToString("MMM-yyyy");
                        this.gvOutstndSum.Columns[5].HeaderText = System.DateTime.Today.AddMonths(-1).ToString("MMM-yyyy");

                        this.gvOutstndSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvOutstndSum.DataSource = (DataTable)ViewState["tblstatus1"];
                        this.gvOutstndSum.DataBind();
                        ((Label)(this.gvOutstndSum.FooterRow.FindControl("lblgvCurDue"))).Text = Convert.ToDouble(dt.Compute("SUM(curdue)", string.Empty)).ToString("#,##0;(#,##0); ");
                        ((Label)(this.gvOutstndSum.FooterRow.FindControl("lblgvpm1tt"))).Text = Convert.ToDouble(dt.Compute("SUM(payment1)", string.Empty)).ToString("#,##0;(#,##0); ");
                        ((Label)(this.gvOutstndSum.FooterRow.FindControl("lblgvpm2tt"))).Text = Convert.ToDouble(dt.Compute("SUM(payment2)", string.Empty)).ToString("#,##0;(#,##0); ");
                        ((Label)(this.gvOutstndSum.FooterRow.FindControl("lblgvpm3tt"))).Text = Convert.ToDouble(dt.Compute("SUM(payment3)", string.Empty)).ToString("#,##0;(#,##0); ");
                    }
                    break;

                case "PromMatHis":
                    var lst = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHis"];
                    this.gvPromData.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPromData.DataSource = lst;//(DataTable)ViewState["tblPromHis"];
                    this.gvPromData.DataBind();

                    this.gvPromSumm.DataSource = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHis_summ"];
                    this.gvPromSumm.DataBind();
                    this.gvpromhisteam.DataSource = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHisteamsum"];
                    this.gvpromhisteam.DataBind();
                    this.FooterCalculation();

                    if (lst.Count == 0)
                        return;
                    Session["Report1"] = gvPromData;
                    ((HyperLink)this.gvPromData.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;

                case "IQCInspection":

                    this.gvIQCReport.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvIQCReport.DataSource = (DataTable)ViewState["tblIQCRpt"];
                    this.gvIQCReport.DataBind();

                    break;


            }

        }

        private void FooterCalculation()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "SupOutStan":

                    DataTable dt = (DataTable)ViewState["tblstatus"];
                    if (dt.Rows.Count == 0)
                        return;

                    ((Label)this.gvSupDueStatus.FooterRow.FindControl("lblgvFopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                    0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0.00; (#,##0.00); ");
                    ((Label)this.gvSupDueStatus.FooterRow.FindControl("lblgvFcram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                                    0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSupDueStatus.FooterRow.FindControl("lblgvFpurret")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(purret)", "")) ?
                                    0 : dt.Compute("sum(purret)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSupDueStatus.FooterRow.FindControl("lblgvFnetpur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpur)", "")) ?
                                    0 : dt.Compute("sum(netpur)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSupDueStatus.FooterRow.FindControl("lblgvFdram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                                    0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSupDueStatus.FooterRow.FindControl("lblgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                                    0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "PromMatHis":
                    var lst = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHis"];
                    if (lst.Count == 0)
                        return;

                    ((Label)this.gvPromData.FooterRow.FindControl("lblFissueamt")).Text = (lst.Select(p => p.issueamt).Sum() == 0.00) ? "0" : lst.Select(p => p.issueamt).Sum().ToString("#,##0.00;(#,##0.00); ");

                    var lst2 = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHis_summ"];
                    if (lst2.Count == 0)
                        return;

                    ((Label)this.gvPromSumm.FooterRow.FindControl("lblFissueamt")).Text = (lst2.Select(p => p.issueamt).Sum() == 0.00) ? "0" : lst2.Select(p => p.issueamt).Sum().ToString("#,##0.00;(#,##0.00); ");

                    var lst3 = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHisteamsum"];
                    if (lst3.Count == 0)
                        return;

                    ((Label)this.gvpromhisteam.FooterRow.FindControl("lblFissueamt")).Text = (lst3.Select(p => p.issueamt).Sum() == 0.00) ? "0" : lst3.Select(p => p.issueamt).Sum().ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvpromhisteam.FooterRow.FindControl("lblFissueqty")).Text = (lst3.Select(p => p.issueqty).Sum() == 0.00) ? "0" : lst3.Select(p => p.issueqty).Sum().ToString("#,##0.00;(#,##0.00); ");
                    break;

            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "SupOutStan":
                    this.SumOutstandingPrint();
                    break;

                case "PromMatHis":
                    this.PromHisStatusPrint();
                    break;

                case "IQCInspection":
                    this.IQCInspReportPrint();
                    break;

            }
        }

        private void SumOutstandingPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string rptDt = "Date From : " + txtfromdate + " To : " + txttodate + " ";

            DataTable dt = (DataTable)ViewState["tblstatus1"];
            var list = dt.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SuppOutStndStatmnt>();
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptSumOutstdng", list, null, null);
            string rpttitle = "Supplier's Outstanding Position";

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("daterange", rptDt));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", rpttitle));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PromHisStatusPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            //this.lblCir.Text = hst["circle"].ToString();
            //this.lblReg.Text = hst["area"].ToString();
            //this.lblArea.Text = hst["region"].ToString();
            //this.lbltrri.Text = hst["territory"].ToString();

            string circle = ""; //this.ddlcircle.SelectedItem.ToString();
            string region = ""; //this.ddlRegi.SelectedItem.ToString();
            string area = ""; //this.ddlArea.SelectedItem.ToString();
            string territory = ""; //this.ddlterri.SelectedItem.ToString();

            string comcod = hst["comcod"].ToString();
            string formDate = txtFDate.Text;
            string toDate = txttodate.Text;
            string comnam = hst["comnam"].ToString();  //company name
            string comadd = hst["comadd1"].ToString();  //address
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var lst = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHis"];
            var lst2 = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHis_summ"];
            var lst3 = (List<SPEENTITY.C_10_Procur.EClassProcur.PromMatHistory>)ViewState["tblPromHisteamsum"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_11_Pro.RptPromatDistribution", lst, lst2, lst3);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compnam", comnam));
            rpt1.SetParameters(new ReportParameter("rtpTitle", "Promotional Materials Distribution Report"));
            rpt1.SetParameters(new ReportParameter("formDate", formDate));
            rpt1.SetParameters(new ReportParameter("toDate", toDate));
            rpt1.SetParameters(new ReportParameter("circle", hst["circle"].ToString()));
            rpt1.SetParameters(new ReportParameter("area", hst["area"].ToString()));
            rpt1.SetParameters(new ReportParameter("region", hst["region"].ToString()));
            rpt1.SetParameters(new ReportParameter("territory", hst["territory"].ToString()));
            rpt1.SetParameters(new ReportParameter("circleD", circle));
            rpt1.SetParameters(new ReportParameter("areaD", area));
            rpt1.SetParameters(new ReportParameter("regionD", region));
            rpt1.SetParameters(new ReportParameter("territoryD", territory));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void IQCInspReportPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string daterange = "Date From: " + txtfromdate + " To: " + txttodate + " ";
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rpttitle = "IQC Inspection Report";

            DataTable dt = (DataTable)ViewState["tblIQCRpt"];
            var list = dt.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.IQCInspectionReport>();
            
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptIQCInspection", list, null, null);
            

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("daterange", daterange));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", rpttitle));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "SupOutStan":
                    this.ShowSupDueStatus();
                    break;

                case "PromMatHis":
                    this.ShowPromHisStatus();
                    break;

                case "IQCInspection":
                    this.ShowIQCInspReport();
                    break;

            }
        }

        private void ShowIQCInspReport()
        {
            string comcod = this.GetComeCode();
            string Fdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string supplier = (this.ddlSuplier.SelectedValue.ToString() == "000000000000") ? "99%" : this.ddlSuplier.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_QC", "IQC_INSPECTION_REPORT", Fdate, todate, supplier, "", "", "", "", "", "");
            
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvIQCReport.DataSource = null;
                this.gvIQCReport.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger();", true);
                return;
            }

            ViewState["tblIQCRpt"] = ds1.Tables[0];
            this.Data_Bind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvWorkOrdHisRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSupDueStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvPromData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPromData.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvSupDueStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = this.GetComeCode();
            //string mACTCODE = ((Label)e.Row.FindControl("lblgvAccode")).Text;
            string mRESCODE = ((Label)e.Row.FindControl("lblgvrescode")).Text;
            string mTRNDAT1 = this.txtFDate.Text;
            string mTRNDAT2 = this.txttodate.Text;

            hlink1.NavigateUrl = "~/F_15_Acc/AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + "260100010001" + "&rescode=" + mRESCODE +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }

        protected void ChkSum_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSum.Checked == true)
            {
                this.gvOutstndSum.Visible = true;
                this.gvSupDueStatus.Visible = false;
                this.txtFDate.Enabled = false;
                this.txttodate.Enabled = false;
                this.ddlSuplier.Enabled = false;
                ViewState.Remove("tblstatus1");
                string comcod = this.GetComeCode();
                string Fdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string rescode = (this.ddlSuplier.SelectedValue.ToString() == "000000000000") ? "99%" : this.ddlSuplier.SelectedValue.ToString() + "%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SUPPLIER_OUTSTANDING_POSITION", Fdate, todate, rescode, "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.gvOutstndSum.DataSource = null;
                    this.gvOutstndSum.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger();", true);
                    return;
                }
                //DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
                ViewState["tblstatus1"] = ds1.Tables[0];
                this.Data_Bind();
            }
            else
            {
                this.txtFDate.Enabled = true;
                this.txttodate.Enabled = true;
                this.ddlSuplier.Enabled = true;
                this.gvOutstndSum.Visible = false;
                this.gvSupDueStatus.Visible = true;
                this.ShowSupDueStatus();
            }
        }

        protected void gvIQCReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvRslt = (Label)e.Row.FindControl("lblgvRslt");

                string result = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qcstatus")).ToString();

                if (result == "Failed")
                {
                    lblgvRslt.Attributes.Add("class", "badge badge-danger");
                }
                else
                {
                    lblgvRslt.Attributes.Add("class", "badge badge-success");
                }
            }
        }
    }
}
