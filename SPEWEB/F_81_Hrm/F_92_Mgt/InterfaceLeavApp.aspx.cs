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
using System.IO;
using System.Drawing;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class InterfaceLeavApp : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE Smartface";//
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txFdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                // this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = Convert.ToDateTime(this.txFdate.Text).AddMonths(2).AddDays(-1).ToString("dd-MMM-yyyy");

                this.RadioButtonList1.SelectedIndex = 0;
                this.pnlInt.Visible = true;
                //this.SaleRequRpt();

                lbtnOk_Click(null, null);
                //this.RadioButtonList1_SelectedIndexChanged(null, null);

            }
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
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lnkInterface_Click(object sender, EventArgs e)
        {
            this.pnlInt.Visible = true;
            this.pnlReport.Visible = false;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.pnlInt.Visible = false;
            this.pnlReport.Visible = true;
            if (ASTUtility.Left(comcod, 1) == "7")
            {
                this.pnlTrade.Visible = true;
            }
            else
            {
                this.plnMgf.Visible = true;
            }
        }
        private void SaleRequRpt()
        {
            Session.Remove("tblaproved");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string empid = hst["empid"].ToString();//hst["deptcode"].ToString();
            string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "GETLEAVEREQUEST", fDate, tDate, usrid, empid, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[1].Rows[0]["tcount"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Request</div></div></div>";
           
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[1].Rows[0]["reqcount"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Process</div></div></div>";
           
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[1].Rows[0]["appcount"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Approval</div></div></div>";
           

            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[1].Rows[0]["tappcount"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> Confirmed</div></div></div>";
            

            Session["tbltotalLeav"] = ds1.Tables[0];
            Session["tblaproved"] = ds1.Tables[2];
            Session["tbleaproved"] = ds1.Tables[3];

            // All Order
            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            //dv = dt.DefaultView;
            // dv.RowFilter=("usrid='" + usrid + "'");
            //dv.Sort = ("Supcode");
            this.Data_Bind("gvLvReq", dt);


            //In-process
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            //dv.RowFilter = ("sostatus = 'In-process' or  sostatus = 'Request' ");
            dv.RowFilter = ("lvstatus='Request' ");
            this.Data_Bind("gvInprocess", dv.ToTable());



            //Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'In-process' ");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvApproved", dv.ToTable());

            //Confirm
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'Approved' ");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvConfirm", dv.ToTable());
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.pnlallReq.Visible = true;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["style"] = "lblactive blink_me";
                    break;

                case "1":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = true;
                    this.PnlApp.Visible = false;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["style"] = "lblactive blink_me";
                    break;

                case "2":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = true;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["style"] = "lblactive blink_me";
                    break;

                case "3":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.PnlConfrm.Visible = true;
                    this.RadioButtonList1.Items[3].Attributes["style"] = "lblactive blink_me";
                    break;
            }
        }


        private void Data_Bind(string gv, DataTable dt)
        {


            switch (gv)
            {
                case "gvLvReq":
                    this.gvLvReq.DataSource = (dt);
                    this.gvLvReq.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvInprocess":
                    this.gvInprocess.DataSource = (dt);
                    this.gvInprocess.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvApproved":
                    if (dt.Rows.Count == 0)
                        return;
                    this.gvApproved.DataSource = (dt);
                    this.gvApproved.DataBind();

                    break;
                case "gvConfirm":
                    this.gvConfirm.DataSource = (dt);
                    this.gvConfirm.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;


            }




        }



        protected void gvInprocess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HylvPrint");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                LinkButton hlink4 = (LinkButton)e.Row.FindControl("lnkbtnDel");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();

                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "susrid")).ToString();

                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;
                hlink3.Enabled = (userid == suserid) ? true : false;
                hlink3.Attributes["style"] = (userid == suserid) ? "color:blue;" : " color:red;";
                hlink4.Visible = (userid == suserid) ? true : false;


            }
        }
        protected void gvApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HylvPrint");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                LinkButton hlink4 = (LinkButton)e.Row.FindControl("lnkbtnfDel");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "susrid")).ToString();

                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string refno1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno1")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");

                DataTable dt = (DataTable)Session["tblaproved"];
                DataTable dte = (DataTable)Session["tbleaproved"];


                DataRow[] dr1 = dt.Select("usrid='" + usrid + "' and centrid='" + refno1 + "'");
                DataRow[] dre = dte.Select("usrid='" + usrid + "'");
                hlink3.Enabled = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
                hlink3.Attributes["style"] = (dre.Length > 0) ? "color:blue;" : ((dr1.Length > 0) ? "color:blue;" : " color:red;");
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;
                hlink4.Visible = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
            }
        }

        protected void lnkbtnDel_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            int index = row.RowIndex;

            string empid = ((Label)gvInprocess.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string lblltrnid = ((Label)gvInprocess.Rows[index].FindControl("lblltrnid")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETEREQLIEAVE", lblltrnid, empid, userid, "", "", "", "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Delete Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Delete Fail')", true);
            }


        }
        protected void lnkbtnfDel_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            int index = row.RowIndex;

            string delStep = "FC";//FinalApprovalCancel

            string empid = ((Label)gvApproved.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string lblltrnid = ((Label)gvApproved.Rows[index].FindControl("lblltrnid")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETEREQLIEAVE1", lblltrnid, empid, userid, delStep, "", "", "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Delete Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Delete Fail')", true);
            }


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string empid = ((Label)gvLvReq.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string idcardno = ((Label)gvLvReq.Rows[index].FindControl("lgidcard")).Text.ToString();

            string lblltrnid = ((Label)gvLvReq.Rows[index].FindControl("lblltrnid")).Text.ToString();
            string empname = ((Label)gvLvReq.Rows[index].FindControl("lblgvempname")).Text.ToString();
            string desg = ((Label)gvLvReq.Rows[index].FindControl("lgdesig")).Text.ToString();
            string lblgvaplydat = ((Label)gvLvReq.Rows[index].FindControl("lblgvaplydat")).Text.ToString();
            string txtLeavLreasons = ((Label)gvLvReq.Rows[index].FindControl("lblgvaplydat")).Text.ToString();
            string deptname = ((Label)gvLvReq.Rows[index].FindControl("lgdeptanme")).Text.ToString();
            string date = ((Label)gvLvReq.Rows[index].FindControl("lblgvstrtdat")).Text.ToString();
            string location = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, date, "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];    // dv.ToTable();

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.HrInterfaceLeave>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptHRInterfaceLeave", lst, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("empname", empname));
            rpt1.SetParameters(new ReportParameter("deptname", deptname));
            rpt1.SetParameters(new ReportParameter("empid", idcardno));
            rpt1.SetParameters(new ReportParameter("desg", desg));
            rpt1.SetParameters(new ReportParameter("location", location));

            rpt1.SetParameters(new ReportParameter("RptTitle", "HR Leave Interface"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);


        }
        protected void HylvPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string empid = ((Label)gvApproved.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string lblltrnid = ((Label)gvApproved.Rows[index].FindControl("lblltrnid")).Text.ToString();
            string empname = ((Label)gvApproved.Rows[index].FindControl("lblgvempname")).Text.ToString();
            string disgnation = ((Label)gvApproved.Rows[index].FindControl("lgdesig")).Text.ToString();
            string lblgvaplydat = ((Label)gvApproved.Rows[index].FindControl("lblgvaplydat")).Text.ToString();
            string txtLeavLreasons = ((Label)gvApproved.Rows[index].FindControl("lblgvaplydat")).Text.ToString();
            string aplydat = "";


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            dv.RowFilter = ("appday>0");
            DataTable dt = dv.ToTable();
            ReportDocument rptTest = new RMGiRPT.R_81_Hrm.R_84_Lea.EmpLeavApp();
            TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            txtRptComName.Text = comnam;
            TextObject txtRptCompAdd = rptTest.ReportDefinition.ReportObjects["txtRptCompAdd"] as TextObject;
            txtRptCompAdd.Text = comadd;
            TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            txtRecordNo.Text = lblltrnid;
            TextObject txtRecordNo1 = rptTest.ReportDefinition.ReportObjects["txtRecordNo1"] as TextObject;
            txtRecordNo1.Text = lblltrnid;
            TextObject txtleaveday = rptTest.ReportDefinition.ReportObjects["txtleaveday"] as TextObject;
            txtleaveday.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days";

            TextObject txtldatefrm = rptTest.ReportDefinition.ReportObjects["txtldatefrm"] as TextObject;
            txtldatefrm.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            TextObject txtldateto = rptTest.ReportDefinition.ReportObjects["txtldateto"] as TextObject;
            txtldateto.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy");
            TextObject txtlday = rptTest.ReportDefinition.ReportObjects["txtlday"] as TextObject;
            txtlday.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ");

            TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            txtEmpName.Text = empname;
            TextObject txtEmpName1 = rptTest.ReportDefinition.ReportObjects["txtEmpName1"] as TextObject;
            txtEmpName1.Text = empname;
            TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            txtDesig.Text = disgnation;
            TextObject txtDesig1 = rptTest.ReportDefinition.ReportObjects["txtDesig1"] as TextObject;
            txtDesig1.Text = disgnation;
            //TextObject txtApprdate = rptTest.ReportDefinition.ReportObjects["txtApprdate"] as TextObject;
            //txtApprdate.Text = Convert.ToDateTime(this.txtApprdate.Text).ToString("dd-MMM-yyyy");
            TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            txtApplydate.Text = Convert.ToDateTime(lblgvaplydat).ToString("dd-MMM-yyyy");
            TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            rpttxtReasons.Text = txtLeavLreasons;
            TextObject txttitlelappslip = rptTest.ReportDefinition.ReportObjects["txttitlelappslip"] as TextObject;
            txttitlelappslip.Text = "Leave Approval Slip";
            TextObject txtAppDays = rptTest.ReportDefinition.ReportObjects["txtAppDays"] as TextObject;
            txtAppDays.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days " + dt.Rows[0]["gdesc"].ToString() + " from " + Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            rptTest.SetDataSource(((DataTable)Session["tblleavest"]));
            Session["Report1"] = rptTest;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvLvReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string logempid = hst["empid"].ToString();

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                //LinkButton hlink4 = (LinkButton)e.Row.FindControl("lnkbtnDel");


                
                //string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno1")).ToString();
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "susrid")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();



                hlink1.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave?Type=User&genno=" + ltrnid + "&actcode=Edit";

                hlink1.Visible = (empid == logempid) ? true : false;
                //  hlink1.Attributes["style"] = (empid == logempid) ? "background:blue;" : " background:red;";
                //hlink4.Visible = (userid == suserid) ? true : false;

            }

        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            string empname = "";
            string cardno = "";
            string department = "";
            string desg = "";
            string joindate = "";
            string section = "";
            string line = "";


            string frmdate = "";
            string todate = "";
            string joinafter = "";
            string totaldays = "";
            string leavetype = "";

            //string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            //string reason = this.txtLeavLreasons.Text.Trim(); ;
            //string addentime = this.txtaddofenjoytime.Text.Trim();
            //string remarks = this.txtLeavRemarks.Text.Trim();
            //string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();
            //string emptype = this.ddlWstation.SelectedItem.Text;


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string empid = ((Label)gvLvReq.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string IdCardNo = ((Label)gvLvReq.Rows[index].FindControl("lgidcard")).Text.ToString();
            string aplydat = ((Label)gvLvReq.Rows[index].FindControl("lblgvaplydat")).Text.ToString();
            
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", company, pactcode, IdCardNo, dept, "", "", "", "", "");
            //DataRow[] dr1 = ds1.Tables[0].Select("empid='" + empid + "'");

            //string totalleav = "Total Days" + "2";
            //if (dr1.Length > 0)
            //{
            //    cardno = dr1[0]["idcardno"].ToString();
            //    empname = dr1[0]["empname1"].ToString();
            //    department = dr1[0]["dept"].ToString();
            //    desg = dr1[0]["desig"].ToString();
            //    joindate = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");
            //    section = dr1[0]["section"].ToString();
            //    line = dr1[0]["line"].ToString();

            //}

            string year = System.DateTime.Today.ToString("yyyy");
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, year, "", "", "", "", "", "");

            var list = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatusInfoEng>();
            DataTable dt2 = (DataTable)ViewState["tblprelinf"];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                frmdate = Convert.ToDateTime(dt2.Rows[0]["strtdat"]).ToString("dd/MM/yyyy");
                todate = Convert.ToDateTime(dt2.Rows[0]["enddat"]).ToString("dd/MM/yyyy");
                joinafter = Convert.ToDateTime(dt2.Rows[0]["enddat"]).AddDays(1).ToString("dd/MM/yyyy");
                double total = (Convert.ToDateTime(dt2.Rows[0]["enddat"]) - Convert.ToDateTime(dt2.Rows[0]["strtdat"])).TotalDays + 1;
                totaldays = total.ToString();
                leavetype = dt2.Rows[0]["gcod"].ToString();

            }
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLeavAppFormEng", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("line", line));
            Rpt1.SetParameters(new ReportParameter("companyname", comname));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " LEAVE APPLICATION FORM"));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("empname", empname));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));

            Rpt1.SetParameters(new ReportParameter("idcard", cardno));
            Rpt1.SetParameters(new ReportParameter("datofjoin", joindate));
            Rpt1.SetParameters(new ReportParameter("desig", desg));
            Rpt1.SetParameters(new ReportParameter("department", department));
            Rpt1.SetParameters(new ReportParameter("sect", section));
            //Rpt1.SetParameters(new ReportParameter("totaldays", totalleav));

            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("todate", todate));
            Rpt1.SetParameters(new ReportParameter("joinafter", joinafter));

            //Rpt1.SetParameters(new ReportParameter("applydat", applydat));
            //Rpt1.SetParameters(new ReportParameter("reason", reason));
            //Rpt1.SetParameters(new ReportParameter("addentime", addentime));
            //Rpt1.SetParameters(new ReportParameter("remarks", remarks));
            //Rpt1.SetParameters(new ReportParameter("emptype", emptype));
            //Rpt1.SetParameters(new ReportParameter("dnameadesig", dnameadesig));
            Rpt1.SetParameters(new ReportParameter("totaldays", totaldays));
            Rpt1.SetParameters(new ReportParameter("leavetype", leavetype));





            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
    }
}