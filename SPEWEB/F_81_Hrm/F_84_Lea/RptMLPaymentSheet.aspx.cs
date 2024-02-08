using Microsoft.Reporting.WinForms;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptMLPaymentSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Maternity Benefit Payment Sheet";
                this.GetMLEmployee();
                this.CreateDataTable();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.divChkEmp.Visible = false;
                this.divAddEmp.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.gvMLPaymentSheet.DataSource = null;
                this.gvMLPaymentSheet.DataBind();
                return;
            }

            this.divChkEmp.Visible = true;
            this.chkAddEmp.Checked = false;
            this.lbtnOk.Text = "New";
            this.GetMLEmployee();
        }

        private void GetMLEmployee()
        {
            try
            {
                Session.Remove("tblmlemployee");
                string comcod = this.GetCompCode();
                string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
                string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETMLEMPLOYEE", Fdate, Tdate, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = ds1.Tables[0];
                this.ddlEmployee.DataBind();

                Session["tblempmldet"] = ds1.Tables[0];
                ds1.Dispose();

                this.Data_Bind();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAddEmp.Checked)
                {
                    this.divAddEmp.Visible = true;
                    DataTable dt1 = (DataTable)Session["tblempmldet"];
                    Session.Remove("tblemp");
                    this.CreateDataTable();
                    DataTable dt = (DataTable)Session["tblemp"];
                    this.ddlEmployee.Items.Clear();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        string empid = dr1["empid"].ToString();
                        if (dt.Select("empid='" + empid + "'").Length == 0)
                        {

                            DataRow dra = dt.NewRow();
                            dra["empid"] = dr1["empid"].ToString();
                            dra["idcard"] = dr1["idcardno"].ToString();
                            dra["empname"] = dr1["idcardno"].ToString() + "-" + dr1["empname"].ToString();
                            dt.Rows.Add(dra);
                        }
                    }

                    Session.Remove("tblempmldet");
                    Session.Remove("tbladdEmpstatus");
                    DataTable dt2 = dt1.Copy();
                    Session["tbladdEmpstatus"] = dt2;
                    DataTable dt3 = dt1.Clone();
                    Session["tblempmldet"] = dt3;

                    this.ddlEmployee.DataTextField = "empname";
                    this.ddlEmployee.DataValueField = "empid";
                    this.ddlEmployee.DataSource = dt;
                    this.DataBind();

                    this.Data_Bind();
                }
                else
                {
                    this.divAddEmp.Visible = false;
                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }
        protected void lnkbtnAddEmp_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblempmldet"];
                DataTable dtadd = (DataTable)Session["tbladdEmpstatus"];
                string empid = this.ddlEmployee.SelectedValue.ToString();
                DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                if (dr1.Length == 0)
                {
                    DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                    dt.ImportRow(dra[0]);
                }
                else
                {
                    string empDetails = "Employee : " + dr1[0]["idcardno"].ToString() + " - " + dr1[0]["empname"].ToString() + " already exist!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + empDetails + "');", true);
                }

                DataView dv = dt.DefaultView;
                dv.Sort = ("deptid, section");
                Session["tblempmldet"] = dv.ToTable();
                this.Data_Bind();

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }

        private void CreateDataTable()
        {
            if (Session["tblemp"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("empid", Type.GetType("System.String"));
                dt.Columns.Add("empname", Type.GetType("System.String"));
                dt.Columns.Add("idcard", Type.GetType("System.String"));
                Session["tblemp"] = dt;

            }
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblempmldet"];
            this.gvMLPaymentSheet.DataSource = dt;
            this.gvMLPaymentSheet.DataBind();
        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvMLPaymentSheet.HeaderRow.FindControl("chkAll")).Checked)
            {

                for (i = 0; i < this.gvMLPaymentSheet.Rows.Count; i++)
                {
                    ((CheckBox)this.gvMLPaymentSheet.Rows[i].FindControl("chkIndv")).Checked = true;
                }

            }

            else
            {
                for (i = 0; i < this.gvMLPaymentSheet.Rows.Count; i++)
                {
                    ((CheckBox)this.gvMLPaymentSheet.Rows[i].FindControl("chkIndv")).Checked = false;

                }

            }
        }

        private void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblempmldet"];
                //Looping For Checked Status
                int i;
                for (i = 0; i < this.gvMLPaymentSheet.Rows.Count; i++)
                {
                    bool chkitm = ((CheckBox)this.gvMLPaymentSheet.Rows[i].FindControl("chkIndv")).Checked;
                    if (chkitm == true)
                    {
                        dt.Rows[i]["chk"] = "1";
                        dt.Rows[i]["preginfdate"] = ((TextBox)this.gvMLPaymentSheet.Rows[i].FindControl("lblgvPregInformDate")).Text;
                        dt.Rows[i]["probdeldate"] = ((TextBox)this.gvMLPaymentSheet.Rows[i].FindControl("lblgvProbDelDate")).Text;

                    }
                    else
                    {
                        dt.Rows[i]["chk"] = "0";
                    }
                }

                Session["tblempmldet"] = dt;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comaddf"].ToString();
            string comnam = hst["comnam"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblempmldet"];

            var lst = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMatLeavePaySheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptMatLvPaymentSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Maternity Benefit Payment Sheet"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkbtnPrintPrenatal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnambn"].ToString();
            string comcod = this.GetCompCode();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;            
            DataTable dt = (DataTable)Session["tblempmldet"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chk='1'");
            DataTable dtn = dv.ToTable();
            var list = dtn.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMatLeavePaySheet>();

            string tkInWord = "";           
            foreach (SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMatLeavePaySheet lst  in list)
            {               
                tkInWord = ASTUtility.TransBN(lst.netpayable, 2).Replace("(","").Replace(")",""); 
                lst.tkinword = tkInWord;

                string date = Convert.ToDateTime(lst.issuedate.ToString()).ToString("dd-MMM-yyyy");
                lst.issuedate = (ASITUtility02.NumBn(date.Substring(0, 2)) + "-" + ASITUtility02.GetMonthName(date.Substring(3, 3)) + "-" + ASITUtility02.NumBn(date.Substring(6))).ToString();
                string month = lst.paymonth.ToString().Substring(4);
                string year = lst.paymonth.ToString().Substring(0, 4);
                lst.paymonth = ASITUtility02.GetMonthNameDigit(month) + "-" + ASITUtility02.NumBn(year);
                lst.preginfdate = (ASITUtility02.NumBn(lst.preginfdate.Substring(0, 2)) + "-" + ASITUtility02.GetMonthName(lst.preginfdate.Substring(3, 3)) + "-" + ASITUtility02.NumBn(lst.preginfdate.Substring(6))).ToString();
                lst.probdeldate = (ASITUtility02.NumBn(lst.probdeldate.Substring(0, 2)) + "-" + ASITUtility02.GetMonthName(lst.probdeldate.Substring(3, 3)) + "-" + ASITUtility02.NumBn(lst.probdeldate.Substring(6))).ToString();
                lst.pfmon1 = ASITUtility02.GetMonthName(Convert.ToDateTime(lst.pfmon1).ToString("MMM"));
                lst.pfmon2 = ASITUtility02.GetMonthName(Convert.ToDateTime(lst.pfmon2).ToString("MMM"));
            }

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptMatLeavePaySheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", "উলুসারা, কালিয়াকৈর ,গাজীপুর। "));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "প্রসব পূর্বকালীন (PRENATAL)"));
            Rpt1.SetParameters(new ReportParameter("preposttxt", "প্রসব পূর্বকালীন"));
            Rpt1.SetParameters(new ReportParameter("footertxt", "প্রসব পূর্ববর্তী"));
            Rpt1.SetParameters(new ReportParameter("probDelDate", ""));
            Rpt1.SetParameters(new ReportParameter("tkInWord", tkInWord));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "প্রস্ততকারক" : "অফিসার (মানব সম্পদ ও পে.রোল)"));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "উপ-ব্যবস্থাপক (মানব সম্পদ ও প্রশাসন)" : "উপ-ব্যবস্থাপক (মানব সম্পদ)"));
            Rpt1.SetParameters(new ReportParameter("sign3", comcod == "5305" ? "উপ-ব্যবস্থাপক হিসাব" : "উপ-ব্যবস্থাপক (হিসাব)"));
            Rpt1.SetParameters(new ReportParameter("sign4", comcod == "5305" ? "উপ-ব্যবস্থাপক (সাস্টেনিবিলিটি এন্ড সি এস আর)" : "ফ্যাক্টরী ইনচার্জ"));
            Rpt1.SetParameters(new ReportParameter("sign5", comcod == "5305" ? "গ্রুপ জেনারেল ম্যানেজার" : "গ্রুপ জি এম"));

            Session["Report1"] = Rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }

        protected void lnkbtnPrintPostnatal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnambn"].ToString();
            string comcod = this.GetCompCode();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblempmldet"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chk='1'");
            DataTable dtn = dv.ToTable();
            var list = dtn.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMatLeavePaySheet>();

            string tkInWord = "";
            foreach (SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMatLeavePaySheet lst in list)
            {
                tkInWord = ASTUtility.TransBN(lst.netpayable, 2).Replace("(", "").Replace(")", "");
                lst.tkinword = tkInWord;

                string date = Convert.ToDateTime(lst.issuedate.ToString()).ToString("dd-MMM-yyyy");
                lst.issuedate = (ASITUtility02.NumBn(date.Substring(0, 2)) + "-" + ASITUtility02.GetMonthName(date.Substring(3, 3)) + "-" + ASITUtility02.NumBn(date.Substring(6))).ToString();
                string month = lst.paymonth.ToString().Substring(4);
                string year = lst.paymonth.ToString().Substring(0, 4);
                lst.paymonth = ASITUtility02.GetMonthNameDigit(month) + "-" + ASITUtility02.NumBn(year);
                lst.preginfdate = (ASITUtility02.NumBn(lst.preginfdate.Substring(0, 2)) + "-" + ASITUtility02.GetMonthName(lst.preginfdate.Substring(3, 3)) + "-" + ASITUtility02.NumBn(lst.preginfdate.Substring(6))).ToString();
                lst.probdeldate = (ASITUtility02.NumBn(lst.probdeldate.Substring(0, 2)) + "-" + ASITUtility02.GetMonthName(lst.probdeldate.Substring(3, 3)) + "-" + ASITUtility02.NumBn(lst.probdeldate.Substring(6))).ToString();
                lst.pfmon1 = ASITUtility02.GetMonthName(Convert.ToDateTime(lst.pfmon3).ToString("MMM"));
                lst.pfmon2 = ASITUtility02.GetMonthName(Convert.ToDateTime(lst.pfmon4).ToString("MMM"));
            }

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptMatLeavePaySheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", "উলুসারা, কালিয়াকৈর ,গাজীপুর। "));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "প্রসব পরবর্তীকালীন (POSTNATAL)"));
            Rpt1.SetParameters(new ReportParameter("preposttxt", "প্রসব পরবর্তীকালীন"));
            Rpt1.SetParameters(new ReportParameter("footertxt", "প্রসব পরবর্তী"));
            Rpt1.SetParameters(new ReportParameter("probDelDate", ""));
            Rpt1.SetParameters(new ReportParameter("tkInWord", tkInWord));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "প্রস্ততকারক" : "অফিসার (মানব সম্পদ ও পে.রোল)"));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "উপ-ব্যবস্থাপক (মানব সম্পদ ও প্রশাসন)" : "উপ-ব্যবস্থাপক (মানব সম্পদ)"));
            Rpt1.SetParameters(new ReportParameter("sign3", comcod == "5305" ? "উপ-ব্যবস্থাপক হিসাব" : "উপ-ব্যবস্থাপক (হিসাব)"));
            Rpt1.SetParameters(new ReportParameter("sign4", comcod == "5305" ? "উপ-ব্যবস্থাপক (সাস্টেনিবিলিটি এন্ড সি এস আর)" : "ফ্যাক্টরী ইনচার্জ"));
            Rpt1.SetParameters(new ReportParameter("sign5", comcod == "5305" ? "গ্রুপ জেনারেল ম্যানেজার" : "গ্রুপ জি এম"));

            Session["Report1"] = Rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
    }
}