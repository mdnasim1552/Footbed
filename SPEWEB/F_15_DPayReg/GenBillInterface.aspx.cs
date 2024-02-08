using System;
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
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_15_DPayReg
{
    public partial class GenBillInterface : System.Web.UI.Page
    {
        // public static string recvno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus = "";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = "General Bill Smartface";//

                this.PnlInt.Visible = true;

                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtdate.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                this.RadioButtonList1.SelectedIndex = 0;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.Countqty();

                this.lbtnOk_Click(null, null);
                HyperLink hyp1 = (HyperLink)this.HyperLink1 as HyperLink;

                hyp1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqEntry&prjcode=&genno=&comcod=" + comcod;


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "5":
                    this.PrintToDaysApproval();
                    break;

                default:
                    break;


            }



        }

        private void PrintRequistionStatus()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)ViewState["tbladdbgdt"];
            if (dt == null)
                return;


            var lstsum = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.ManPowerBudgtActual>();

            // RMGiEntity.C_15_DPayReg

            //  string year = lstsum[0].bgdyear.ToString();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_15_DPayReg.RptRequistionStatus", lstsum, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "BILL STICKER"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            rpt1.SetParameters(new ReportParameter("date", ""));
            rpt1.SetParameters(new ReportParameter("VenName", ""));
            rpt1.SetParameters(new ReportParameter("VenBillno", ""));
            rpt1.SetParameters(new ReportParameter("VDate", ""));
            rpt1.SetParameters(new ReportParameter("BillAmount", ""));

            rpt1.SetParameters(new ReportParameter("PurOrderno", ""));
            rpt1.SetParameters(new ReportParameter("PurDate", ""));
            rpt1.SetParameters(new ReportParameter("MRNo", ""));

            rpt1.SetParameters(new ReportParameter("PerparedBy", ""));
            rpt1.SetParameters(new ReportParameter("CheckedBy", ""));
            rpt1.SetParameters(new ReportParameter("MRNo", ""));

            rpt1.SetParameters(new ReportParameter("NetPayable", ""));




            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintToDaysApproval()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptToDaysApproval();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = todate;

            //DataTable dt = (DataTable)Session["tbldate1"];
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "faprvdat = '" + todate + "'";
            //dt = dv.ToTable();



            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);

            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            lbtnOk_Click(null, null);


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.Countqty();
            this.reqStatus();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }


        private void Countqty()
        {

            string comcod = this.GetCompCode();

            string frdate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "BILLREGISTER", frdate, "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];

            ViewState["tblcount"] = dt;
            this.DataCountShow();

        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    //Requistion status

                    this.pnlReqInfo.Visible = true;
                    this.pnlPendapp.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;

                    this.reqStatus();
                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #D0DECA; display:block;";
                    //this.RadioButtonList1.Items[0].Attributes.Add("class","lblactive");  f9f9f9   189697
                    //("class", "hidden");
                    // this.RadioButtonList1.Items[0].Attributes.CssStyle.ToString() = "lblactive";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background-color:#13A6A8; font-size:16px; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px;  width:30px;";   

                    break;

                case "1":
                    this.pnlReqInfo.Visible = true;
                    this.pnlPendapp.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;

                    this.reqStatus();

                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #D0DECA; display:block;";
                    break;
                case "2":

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = true;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;

                    this.reqStatus();
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #D0DECA; display:block;";

                    break;

                case "3":

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.pnlFinalApp.Visible = true;
                    this.pnlpayOrder.Visible = false;
                    this.PnlreqInfo1.Visible = false;

                    this.reqStatus();
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #D0DECA; display:block;";

                    break;


                case "4":
                    this.pnlFinalApp.Visible = false;

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.pnlpayOrder.Visible = true;
                    this.PnlreqInfo1.Visible = false;

                    this.reqStatus();
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #D0DECA; display:block;";

                    break;

                case "5":
                    //Requistion statusPnlreqInfo1

                    this.PnlreqInfo1.Visible = true;
                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;

                    this.reqStatus();
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #D0DECA; display:block;";
                    break;






            }
        }

        private void DataCountShow()
        {
            DataTable dt = (DataTable)ViewState["tblcount"];


            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["reqstatus"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class='lbldata2'>" + "Requistion Status" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["apprst"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "Approval" + "</span>";
            //this.RadioButtonList1.Items[3].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["fapp"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "Final Approval" + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["apprst"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "CS" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["payorder"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "Payment Due" + "</span>";
            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["todayppval"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "ToDays Approval" + "</span>";


        }

        private void reqStatus()
        {
            string comcod = this.GetCompCode();
            // string frmdate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "BILLREGISTER", todate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            Session["tbldate"] = ds1.Tables[1];
            Session["tbldate1"] = ds1.Tables[2];



            // All Recive
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[1]).Copy();
            // dt1 = ((DataTable)ds1.Tables[2]).Copy();

            dv = dt.DefaultView;
            this.Data_Bind("gvReqInfo", dv.ToTable());



            //dt = ((DataTable)ds1.Tables[1]).Copy();
            dv.RowFilter = ("aprvbyid = '' and appamt <= 0");
            dv = dt.DefaultView;
            this.Data_Bind("gvPenApproval", dv.ToTable());


            dv.RowFilter = ("faprvbyid = '' and appamt > 0 and aprvbyid <>''");
            dv = dt.DefaultView;
            this.Data_Bind("gvFinlApproval", dv.ToTable());


            dv.RowFilter = ("balamt > 0 and faprvbyid<>''");
            dv = dt.DefaultView;
            this.Data_Bind("gvPayOrder", dv.ToTable());


            this.Data_Bind("gvReqInfo1", dt);
        }


        private void Data_Bind(string gv, DataTable dt)
        {
            //string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataTable dt1 = (DataTable)Session["tbldate1"];


            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            switch (gv)
            {
                case "gvReqInfo":
                    this.gvReqInfo.DataSource = dt;
                    this.gvReqInfo.DataBind();

                    if (value == "1" || value == "2")
                    {
                        gvReqInfo.Columns[13].Visible = true;
                    }
                    else
                    {
                        gvReqInfo.Columns[13].Visible = false;

                    }
                    break;

                case "gvPenApproval":
                    this.gvPenApproval.DataSource = dt;
                    this.gvPenApproval.DataBind();

                    //((Label)this.gvPenApproval.FooterRow.FindControl("gvLblTtlReq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reqamt)", "")) ?
                    //            0 : dt.Compute("sum(reqamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "gvFinlApproval":
                    this.gvFinlApproval.DataSource = dt;
                    this.gvFinlApproval.DataBind();
                    break;

                case "gvPayOrder":
                    this.gvPayOrder.DataSource = dt;
                    this.gvPayOrder.DataBind();
                    break;

                case "gvReqInfo1":

                    DataView dv = new DataView();
                    dv = dt1.DefaultView;
                    dv.RowFilter = ("faprvdat = '" + todate + "'");
                    this.gvReqInfo1.DataSource = dv.ToTable();
                    this.gvReqInfo1.DataBind();
                    break;

            }

        }

        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;

            string pactcode = dt.Rows[0]["pactcode"].ToString();
            string reqdat1 = dt.Rows[0]["reqdat1"].ToString();


            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["pactcode"].ToString() == pactcode && dt.Rows[j]["reqdat1"].ToString() == reqdat1)
                {
                    pactcode = dt.Rows[j]["pactcode"].ToString();
                    dt.Rows[j]["pactdesc"] = "";
                    reqdat1 = dt.Rows[j]["reqdat1"].ToString();
                    dt.Rows[j]["reqdat1"] = "";

                }

                else
                    pactcode = dt.Rows[j]["pactcode"].ToString();
                reqdat1 = dt.Rows[j]["reqdat1"].ToString();

            }

            return dt;
        }


        protected void gvReqInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlkQutation");
                //HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                //HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint11");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string date = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString();


                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }



                //string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
                //string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

                //hlink1.NavigateUrl = "~/F_14_DPayReg/LinkQutaAttached";//&comcod=" + comcod + "&pactcode=" + pactcode + "&reqno=" + reqno;

                //hlink2.NavigateUrl = "~/F_20_Service/RecProductEntry?Type=Entry";
                //hlink2.ToolTip = "Create New";

                hlink1.NavigateUrl = "~/F_14_DPayReg/LinkQutaAttached?Type=QutAttached&reqno=" + reqno + "&app=0" + "&comcod=" + comcod;
                hlink2.NavigateUrl = "~/F_21_GAcc/Print?Type=OreqPrint&genno=" + reqno + "&date1=" + date;
                //hlink2.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqPrint&actcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod;


            }
        }
        protected void gvPenApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlkQutation");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                HyperLink hlink4 = (HyperLink)e.Row.FindControl("HyInprPrintfapproved");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string date = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString();



                TableCell cell = e.Row.Cells[10];
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "suserid")).ToString();
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();

                //hlink1.Attributes["style"] = (userid == suserid) ? "background:blue;" : " background:red;";

                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqApproved&actcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod;
                hlink3.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqEdit&actcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod + "&date1=" + date;

                hlink2.NavigateUrl = "~/F_14_DPayReg/LinkQutaAttached?Type=QutAttached&reqno=" + reqno + "&app=1" + "&comcod=" + comcod;
                hlink4.NavigateUrl = "~/F_21_GAcc/Print?Type=OreqPrint&genno=" + reqno + "&date1=" + date;


            }

        }

        protected void gvFinlApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HyInprPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlkQutationfapp");
                HyperLink lnkbtnEntry = (HyperLink)e.Row.FindControl("lnkbtnEntry");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();



                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }

                HyInprPrint.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqPrint&actcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod;
                lnkbtnEntry.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=FinalAppr&actcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod;
                hlink2.NavigateUrl = "~/F_14_DPayReg/LinkQutaAttached?Type=QutAttached&reqno=" + reqno + "&app=1" + "&comcod=" + comcod;



            }
        }

        protected void gvPayOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");



                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }

                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqPrint&actcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod;



            }
        }

        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");

                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }


                //hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqEntry";//&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;


            }
        }
        protected void btnDelOrderfapproved_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvPenApproval.Rows[rowindex].FindControl("lblgvreqnopapr")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEOTHERREQ", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.Countqty();
                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

                //  dt.Rows[rowindex].Delete();
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);

            }


            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tblmatissue");
            //ViewState["tblmatissue"] = dv.ToTable();
            //this.grvissue_DataBind();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Labour Issue Information";
            //    string eventdesc = "Delete Labour";
            //    string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Sub Contractor Name: " +
            //            this.ddlcontractorlist.SelectedItem.Text.Substring(14) + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
            //            ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
            //            ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

        }


        protected void btnDelOrder_Click(object sender, EventArgs e)
        {


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvFinlApproval.Rows[rowindex].FindControl("lblgvreqnoFnApp")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEOTHERREQ", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.Countqty();
                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

                //  dt.Rows[rowindex].Delete();
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);

            }



        }
        protected void LbtnInt_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = true;
            this.PnlRep.Visible = false;
            this.PnlSet.Visible = false;
        }
        protected void LbtnSetting_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = false;
            this.PnlRep.Visible = false;
            this.PnlSet.Visible = true;


        }
        protected void LbtnRep_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = false;
            this.PnlRep.Visible = true;
            this.PnlSet.Visible = false;

        }



    }
}