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

namespace SPEWEB.F_27_Fxt
{
    public partial class RptFxtAsstStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "";

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Fix") ? "FIXED ASSETS STATUS" : "Schedule Of Fixed Assets";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.rbtnList1.SelectedIndex = 0;
                this.gvVisibility();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
            //if (this.ddlProjectName.Items.Count == 0 )
            //{
            //    this.GetProjectName(); 
            //}
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void gvVisibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Fix":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DepCost":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.txtFromdate.Text = Convert.ToDateTime(DateTime.Now.AddMonths(-1)).ToString("dd-MMM-yyyy");
                    this.txtTodate.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
                    break;

            }
        }


        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;


        }

        private void GetProjectName()
        {
            string comcod = this.GetComcod();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GetProjectName", txtSProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }

            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.TransferInfo();
        }

        private void TransferInfo()
        {
            Session.Remove("tbltransfer");
            string comcod = this.GetComcod();
            string ProjectName = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string hzero = (this.ChkBalance.Checked == true) ? "hzero" : "";
            string calltype = (this.rbtnList1.SelectedIndex == 0) ? "RPTBALRESOURCE" : (this.rbtnList1.SelectedIndex == 1) ? "RPTBALRESOURCEWDETAILS" : "RPTFXTASTVALUE";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", calltype, ProjectName, date, hzero, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }




            this.grvacc.Columns[1].Visible = (this.ddlProjectName.SelectedValue == "000000000000") ? true : false;
            this.grvacc.Columns[4].Visible = (calltype == "RPTBALRESOURCE");
            this.grvacc.Columns[6].Visible = (calltype == "RPTBALRESOURCE");
            this.grvacc.Columns[9].Visible = (calltype == "RPTFXTASTVALUE");
            this.grvacc.Columns[10].Visible = (calltype == "RPTFXTASTVALUE");
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tbltransfer"] = dt;
            this.grvacc.DataSource = dt; ;
            this.grvacc.DataBind();
            this.FooterCalculation(dt);
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else
                {



                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";
                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                    {
                        dt1.Rows[j]["rsirdesc"] = "";

                    }
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();

                }

            }
            return dt1;


        }

        private void FooterCalculation(DataTable dt)
        {
            if (this.rbtnList1.SelectedIndex == 2)
            {

                ((Label)this.grvacc.FooterRow.FindControl("lblFoterAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Fix":
                    if (this.rbtnList1.SelectedIndex == 0)
                    {
                        this.rptFxtAsstStatusDetails();
                    }
                    if (this.rbtnList1.SelectedIndex == 1)
                    {
                        this.rptFxtAsstStatusWiD();
                    }
                    else
                    {
                        this.PrintFxtAstValue();
                    }
                    break;

                case "DepCost":
                    // this.printDepreciation();
                    this.RptDepreciation();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = type;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        private void rptFxtAsstStatusWiD()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbltransfer"];
            //ReportDocument rptsale = new RMGiRPT.R_27_Fxt.rptFxtAsstStatusWD();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void rptFxtAsstStatusDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbltransfer"];
            //ReportDocument rptsale = new RMGiRPT.R_27_Fxt.rptFxtAsstStatusDetails();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintFxtAstValue()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbltransfer"];
            //ReportDocument rptsale = new RMGiRPT.R_27_Fxt.rptFxtAsstValue();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptDepreciation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblDepcost"];

            if(dt.Rows.Count == 0)
            {
                return;
            }


            var lst = dt.DataTableToList<SPEENTITY.C_27_Fxt.RtpeFixAssetsSchu>();

            string Date = "Period: " + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            string Head1 = "Balance as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string Head2 = "Total as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            string Head3 = "Depreciation as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string Head4 = "W.D Values as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rptscs = new LocalReport();
            rptscs = SPERDLC.RptSetupClass.GetLocalReport("RD_27_Fxt.RptSchdofFxtAssets", lst, null, null);

            rptscs.EnableExternalImages = true;
            rptscs.SetParameters(new ReportParameter("ComName", comnam));
            rptscs.SetParameters(new ReportParameter("RptTitle", "Schedule Of Fixed Assets"));
            rptscs.SetParameters(new ReportParameter("Date", Date));
            rptscs.SetParameters(new ReportParameter("Head1", Head1));
            rptscs.SetParameters(new ReportParameter("Head2", Head2));
            rptscs.SetParameters(new ReportParameter("Head3", Head3));
            rptscs.SetParameters(new ReportParameter("Head4", Head4));
            rptscs.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rptscs.SetParameters(new ReportParameter("ComLogo", ComLogo));



            Session["Report1"] = rptscs;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void printDepreciation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblDepcost"];

            //ReportDocument rptsale = new RMGiRPT.R_27_Fxt.RptDeprectionCost();//.rptFxtAsstValue();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptDate.Text = "Period: " + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            //int dateDife = ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));
            //int dateDife1 = dateDife + 1;
            //TextObject rpttxtDays = rptsale.ReportDefinition.ReportObjects["txtDays"] as TextObject;
            //rpttxtDays.Text = "Days : " + dateDife1.ToString();

            //TextObject txtBalance = rptsale.ReportDefinition.ReportObjects["txtBalance"] as TextObject;
            //txtBalance.Text = "Balance as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            //TextObject txtTotal = rptsale.ReportDefinition.ReportObjects["txtTotal"] as TextObject;
            //txtTotal.Text = "Total as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            //TextObject txtDepr = rptsale.ReportDefinition.ReportObjects["txtDepr"] as TextObject;
            //txtDepr.Text = "Depreciation as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            //TextObject txtWD = rptsale.ReportDefinition.ReportObjects["txtWD"] as TextObject;
            //txtWD.Text = "W.D Values as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");

            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void grDep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grDep.PageIndex = e.NewPageIndex;
            this.grDep_DataBind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grDep_DataBind();
        }
        private void grDep_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblDepcost"];

            this.grDep.Columns[3].HeaderText = "Balance as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[7].HeaderText = "Total as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            this.grDep.Columns[9].HeaderText = "Depreciation as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[14].HeaderText = "W.D Values as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");

            this.grDep.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());

            this.grDep.DataSource = tbl1;
            this.grDep.DataBind();
            this.FooterRowCal();

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblDepcost");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frdate = Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTDEPRECIATION", frdate, todate, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                this.grDep.DataSource = null;
                this.grDep.DataBind();
                return;
            }


            this.txtDays.Visible = true;
            this.txtDays.Text = "Days: " + Convert.ToDouble(ds1.Tables[1].Rows[0]["cday"]).ToString("#,##0;(#,##0);");
            Session["tblDepcost"] = (DataTable)ds1.Tables[0];
            this.grDep_DataBind();

        }
        private void FooterRowCal()
        {
            DataTable dt = (DataTable)Session["tblDepcost"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grDep.FooterRow.FindControl("lgvFTOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                 0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTAddition")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ?
                                   0 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFsalesdec")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saleam)", "")) ?
                                 0 : dt.Compute("sum(saleam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.grDep.FooterRow.FindControl("lgvFTDisposal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disam)", "")) ?
                                  0 : dt.Compute("sum(disam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ?
                                   0 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepOpen")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opndep)", "")) ?
                                   0 : dt.Compute("sum(opndep)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFadjment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adjam)", "")) ?
                                   0 : dt.Compute("sum(adjam)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.grDep.FooterRow.FindControl("lgvFTcurDepop")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curdepop)", "")) ?
                                   0 : dt.Compute("sum(curdepop)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.grDep.FooterRow.FindControl("lgvFTcurDepcur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curpdepcur)", "")) ?
                                  0 : dt.Compute("sum(curpdepcur)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(todep)", "")) ?
                                   0 : dt.Compute("sum(todep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTCBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                                   0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");



        }

    }

}