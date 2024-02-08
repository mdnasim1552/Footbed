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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptEmpLeaveStatus02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "EmpLeaveStatus") ? "Employee Leave Status"
                    : (this.Request.QueryString["Type"].ToString() == "MonWiseLeave") ? "Employee Leave Status(Month Wise)" : "";
                this.ViewSaction();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetJobLocation();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void ViewSaction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    switch (GetCompCode())
                    {
                        case "5305":
                        case "5306":

                            this.MultiView1.ActiveViewIndex = 2;
                            break;

                        default:
                            this.MultiView1.ActiveViewIndex = 0;
                            break;
                    }

                    break;

                case "MonWiseLeave":
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;




            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }







        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }


        private void ShowData()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    switch (GetCompCode())
                    {
                        case "5305":
                        case "5306":

                            this.ShowEmpLeaveStatusFB();
                            break;

                        default:
                            this.ShowEmpLeaveStatus();
                            break;
                    }
                    
                    break;

                case "MonWiseLeave":
                    this.ShowMonLeave();
                    break;




            }



        }


        private void ShowEmpLeaveStatus()
        {
            try

            {
                Session.Remove("tblover");
                string comcod = this.GetCompCode();
                string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
                string deptcode = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string dtcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (listProject.SelectedValue.ToString() == "000000000000") ? "%" : listProject.SelectedValue.ToString();
                string frmdate = this.txtfrmDate.Text.Trim();
                string todate = this.txttoDate.Text.Trim();
                string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "RPTCOMLEAVESTATUS", company, deptcode, section, frmdate, todate, "", dtcode, Empcode, "");
                if (ds2 == null)
                {

                    this.gvLeaveStatus.DataSource = null;
                    this.gvLeaveStatus.DataBind();
                    return;
                }
                Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
                this.Data_Bind();
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);

            }
        }
        private void ShowEmpLeaveStatusFB()
        {

            try

            {
                Session.Remove("tblover");
                string comcod = this.GetCompCode();
                string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
                string deptcode = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string dtcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (listProject.SelectedValue.ToString() == "000000000000") ? "%" : listProject.SelectedValue.ToString();
                string frmdate = this.txtfrmDate.Text.Trim();
                string todate = this.txttoDate.Text.Trim();
                string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "RPTCOMLEAVESTATUS02", company, deptcode, section, frmdate, todate, "", dtcode, Empcode, "");
                if (ds2 == null)
                {

                    this.gvLeaveStatusFb.DataSource = null;
                    this.gvLeaveStatusFb.DataBind();
                    return;
                }
                Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);

            }
        }
        private void ShowMonLeave()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%");
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%");
            string department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%");
            string secid = ((this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%");
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "%" : this.ddlJobLocation.SelectedValue.ToString() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "RPTYEARLYEMPLEAVE", Company, div, department, frmdate, todate, Empcode, secid, jobLocation, "");
            if (ds2 == null)
            {
                this.gvMonEmpLeave.DataSource = null;
                this.gvMonEmpLeave.DataBind();
                return;
            }
            Session["tblover"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;



            string empid = dt1.Rows[0]["empid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["empid"].ToString() == empid)
                {

                    dt1.Rows[j]["section"] = "";
                    dt1.Rows[j]["empname"] = "";
                    dt1.Rows[j]["idcardno"] = "";
                    dt1.Rows[j]["desig"] = "";
                    dt1.Rows[j]["section"] = "";
                    dt1.Rows[j]["joindate"] = "";
                    dt1.Rows[j]["gssal"] = 0;
                    dt1.Rows[j]["rowid"] = 0;
                    dt1.Rows[j]["lstrtdat"] = "";

                }

                else
                {



                    if (dt1.Rows[j]["empid"].ToString() == empid)
                    {
                        dt1.Rows[j]["empname"] = "";
                        dt1.Rows[j]["idcardno"] = "";
                        dt1.Rows[j]["desig"] = "";
                        dt1.Rows[j]["section"] = "";
                        dt1.Rows[j]["joindate"] = "";
                        dt1.Rows[j]["gssal"] = 0;
                        dt1.Rows[j]["rowid"] = 0;
                        dt1.Rows[j]["lstrtdat"] = "";

                    }
                }



                empid = dt1.Rows[j]["empid"].ToString();
            }
            return dt1;

        }




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblover"];
            //DataTable dt2 = dt.AsEnumerable()
            //      .Where(row => row.Field<Decimal>("lvavailed") > 0).CopyToDataTable();


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    switch (GetCompCode())
                    {
                        case "5305":
                        case "5306":

                            this.gvLeaveStatusFb.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                            this.gvLeaveStatusFb.DataSource = dt;
                            this.gvLeaveStatusFb.DataBind();
                            break;

                        default:
                            this.gvLeaveStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                            this.gvLeaveStatus.DataSource = dt;
                            this.gvLeaveStatus.DataBind();
                            break;
                    }
                    
                    break;

                case "MonWiseLeave":
                    for (int i = 5; i < 17; i++)
                        this.gvMonEmpLeave.Columns[i].Visible = false;

                    DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
                    DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
                    for (int i = 5; i < 17; i++)
                    {
                        if (datefrm > dateto)
                            break;
                        this.gvMonEmpLeave.Columns[i].Visible = true;
                        this.gvMonEmpLeave.Columns[i].HeaderText = datefrm.ToString("MMM");
                        datefrm = datefrm.AddMonths(1);
                    }

                    this.gvMonEmpLeave.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMonEmpLeave.DataSource = dt;
                    this.gvMonEmpLeave.DataBind();
                    break;

            }

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    switch (GetCompCode())
                    {
                        case "5305":
                        case "5306":

                            this.PrintEmpLeaveStatusFb();
                            break;

                        default:
                            this.PrintEmpLeaveStatus();
                            break;
                    }

                    
                    break;

                case "MonWiseLeave":
                    this.PrintMonLeave();
                    break;




            }




        }

        private void PrintEmpLeaveStatusFb()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatusFb>();
            //foreach (var item in list)
            //{

            //    if (item.joindate != "")
            //    {
            //        var valInt = item.joindate.Split('.');


            //        var val1 = valInt[0];
            //        var val3 = valInt[2];
            //        var val2 = GetMonthName(valInt[1]);
            //        item.joindate = val1 + "-" + val2 + "-" + val3;
            //    }
            //}
            LocalReport Rpt1 = new LocalReport();


            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptAllEmpLeavStatusFb", list, null, null);

            Rpt1.SetParameters(new ReportParameter("companyname", comname));
            //  Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Leave Status"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + frmdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #region old
            //ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_84_Lea.RptAllEmpLeavStatus();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //CompName.Text = this.ddlWstation.SelectedItem.Text;
            //TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtaddress.Text = comadd;
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtccaret.Text = "Period: " + frmdate + " To " + todate;

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rpcp.SetDataSource(dt);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion

        }
        private void PrintEmpLeaveStatus()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatus>();
            foreach (var item in list)
            {

                if (item.joindate != "")
                {
                    var valInt = item.joindate.Split('.');


                    var val1 = valInt[0];
                    var val3 = valInt[2];
                    var val2 = GetMonthName(valInt[1]);
                    item.joindate = val1 + "-" + val2 + "-" + val3;
                }

            }
            LocalReport Rpt1 = new LocalReport();


            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptAllEmpLeavStatus", list, null, null);

            Rpt1.SetParameters(new ReportParameter("companyname", comname));
            //  Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Increment Information Status"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + frmdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #region old
            //ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_84_Lea.RptAllEmpLeavStatus();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //CompName.Text = this.ddlWstation.SelectedItem.Text;
            //TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtaddress.Text = comadd;
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtccaret.Text = "Period: " + frmdate + " To " + todate;

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rpcp.SetDataSource(dt);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion

        }
        private void PrintMonLeave()
        {
            string comcod = this.GetCompCode();   //GetComeCode();
            switch (comcod)
            {
                case "3315":
                case "4101":

                    this.PrintEmpMonLeaveAssure();
                    break;
                default:
                    this.PrintMonLeaveGen();                   //PrintMonLeave();
                    break;

            }

        }

        private void PrintEmpMonLeaveAssure()

        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");

            //ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_84_Lea.RptMonWiseEmpLeaveAssure_();
            ////TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            ////CompName.Text = this.ddlCompanyName.SelectedItem.Text;
            ////TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtaddress.Text = comadd;
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtccaret.Text = "(From  " + frmdate + " To " + todate + ")";


            //DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rpcp.ReportDefinition.ReportObjects["txtlv" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rpcp.SetDataSource(dt);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMonLeaveGen()
        {

            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string subtitle = "Employee Leave - Month Wise";
            string userinf = ASTUtility.Concat(comname, username, printdate);
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");


            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeave>();

            foreach (var item in list)
            {

                if (item.joindate != "")
                {
                    var valInt = item.joindate.Split('.');


                    var val1 = valInt[0];
                    var val3 = valInt[2];
                    var val2 = GetMonthName(valInt[1]);
                    item.joindate = val1 + "-" + val2 + "-" + val3;
                }

            }
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptMonWiseLeaveBR", list, null, null);
            string subtitle_date = "(From  " + frmdate + " To " + todate + ")";


            Rpt1.SetParameters(new ReportParameter("comnam", comname));
            Rpt1.SetParameters(new ReportParameter("subtitle", subtitle));
            Rpt1.SetParameters(new ReportParameter("userinf", userinf));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("subtitle_date", subtitle_date));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_84_Lea.RptMonWiseEmpLeave();

            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtccaret.Text = "(From  " + frmdate + " To " + todate + ")";


            //DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rpcp.ReportDefinition.ReportObjects["txtlv" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rpcp.SetDataSource(dt);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        public string NumBn(string num)
        {
            string stringNum = "";

            char[] dtae = num.ToCharArray();
            foreach (var item in dtae)
            {

                switch (item)
                {
                    case '0': stringNum += "০"; break;
                    case '1': stringNum += "১"; break;
                    case '2': stringNum += "২"; break;
                    case '3': stringNum += "৩"; break;
                    case '4': stringNum += "৪"; break;
                    case '5': stringNum += "৫"; break;
                    case '6': stringNum += "৬"; break;
                    case '7': stringNum += "৭"; break;
                    case '8': stringNum += "৮"; break;
                    case '9': stringNum += "৯"; break;
                }



            }
            return stringNum;
        }
        public string GetMonthName(string name)
        {
            return name.Replace("1", "Jan").Replace("2", "Feb").Replace("3", "Mar").
                Replace("4", "Apr").Replace("5", "May").Replace("6", "Jun").Replace("7", "Jul").
                Replace("8", "Aug").Replace("9", "Sep").Replace("10", "Oct").Replace("11", "Nov").
                Replace("12", "Dec");

        }
        protected void gvLeaveStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLeaveStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvLeaveStatusFB_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLeaveStatusFb.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        
        protected void gvLeaveStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label Description = (Label)e.Row.FindControl("lblgvDescription");
                Label opnleave = (Label)e.Row.FindControl("lblgvopnleave");
                Label leaveentitled = (Label)e.Row.FindControl("lblgvleaveentitled");
                Label leaveenjoy = (Label)e.Row.FindControl("lblgvleaveenjoy");
                Label leavebal = (Label)e.Row.FindControl("lblgvleavebal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {

                    Description.Font.Bold = true;
                    opnleave.Font.Bold = true;
                    leaveentitled.Font.Bold = true;
                    leaveenjoy.Font.Bold = true;
                    leavebal.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }
        }
        protected void gvMonEmpLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonEmpLeave.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];
            if (lst == null)
                return;
            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
            lst1.Add(all);

            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";

            this.ddlDivision_SelectedIndexChanged(null, null);

        }

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];


            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";

            this.ddlDept_SelectedIndexChanged(null, null);

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];


            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.listProject.DataTextField = "actdesc";
            this.listProject.DataValueField = "actcode";
            this.listProject.DataSource = lst1;
            this.listProject.DataBind();
            this.listProject.SelectedValue = "000000000000";

        }

        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string jobLocCode = "87";
            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            this.ddlJobLocation.DataTextField = "hrgdesc";
            this.ddlJobLocation.DataValueField = "hrgcod";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
    }
}