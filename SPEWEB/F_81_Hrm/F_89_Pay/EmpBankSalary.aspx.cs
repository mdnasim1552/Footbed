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
using SPERDLC;
using Microsoft.Reporting.WinForms;
using SPEENTITY;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpBankSalary : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.ddlBankSt.SelectedValue = "0";
                this.GetSepType();
                this.GetYearMonth();                
                this.GetMonth();
                this.GetBankName();
                this.GetLineddl();
                this.GetWorkStation();
                this.GetAllOrganogramList();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Salary Transfer Statement";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void EmployeeStatus_click(object sender, EventArgs e)
        {
            string empstatus = this.ddlEmpStatus.SelectedValue;
            if (empstatus == "1")
            {
                this.divSepType.Visible = true;
            }
            else
            {
                this.divSepType.Visible = false;
            }
        }
        private void GetSepType()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GET_SEAPRATION_TYPE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            this.ddlSepType.DataTextField = "hrgdesc";
            this.ddlSepType.DataValueField = "hrgcod";
            this.ddlSepType.DataSource = ds1.Tables[0];
            this.ddlSepType.DataBind();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            Session["lstwrkstation"] = lst;
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");

            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }
        private void GetDivision()
        {
            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

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

            string comcod = GetComeCode();
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
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
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
        private void GetMonth()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4301":
                case "4305":
                    this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
                    break;

                default:
                    this.ddlyearmon.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
                    break;


            }


        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetBankName()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBANKNAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }


            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1.Tables[0];
            this.ddlBankName.DataBind();
        }
        private void GetLineddl()
        {
            string comcod = GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            switch (ddlBankSt.SelectedValue)
            {
                case "0":
                    this.divChkEmp.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowBankPayment();
                    break;
                case "1":
                    this.ShowBankPayment();
                    break;

                case "2":
                    break;
            }


        }


        private void ShowBankPayment()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string bankname = this.ddlBankName.SelectedValue.ToString();
            string date = this.ddlyearmon.SelectedValue.ToString().Trim();
            string saltype = "";
            string secbank = this.chkSecondary.Checked ? "Seconday" : "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "BANKLOCK", date, bankname, saltype,"", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            this.lblComSalTransLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();
            string comnam = (this.ddlWstation.SelectedValue.ToString()=="000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string DepCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string banklock = "";
            string ottype = "";
            string CallType = (this.chkBonus.Checked) ? "EMPBONBANKPAYINFO" : "EMPBANKPAYINFO";
            string emptype = this.ddlEmpStatus.SelectedValue.ToString();
            string linecode = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string septype = this.ddlSepType.SelectedValue == "00000" ? "75%" : this.ddlSepType.SelectedValue +"%";
            DataSet ds2 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", CallType, null, null, null, date, bankname, banklock, DepCode, comnam, divison, section, 
                saltype, ottype, emptype, secbank, linecode, septype, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvBankPayment.DataSource = null;
                this.gvBankPayment.DataBind();
                return;
            }

            DataTable dt = ds2.Tables[0];
            Session["tblover"] = dt;
            this.Data_Bind();
        }



        private void Data_Bind()
        {

            try
            {
                DataTable dt = (DataTable)Session["tblover"];
                this.gvBankPayment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvBankPayment.DataSource = dt;
                this.gvBankPayment.DataBind();

                if (dt.Rows.Count != 0)
                {
                    this.FooterCalculation();
                    Session["Report1"] = gvBankPayment;
                    ((HyperLink)this.gvBankPayment.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                }
               
                if (Request.QueryString["Type"].ToString() == "Entry")
                {
                    if (dt.Rows.Count > 0)
                    {
                        ((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Checked = (this.lblComSalTransLock.Text == "True") ? true : false;
                        ((LinkButton)this.gvBankPayment.FooterRow.FindControl("lbtSalUpdate")).Visible = (((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Checked) ? false : true;
                        ((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Enabled = false;
                    }

                }
                else
                {
                    ((LinkButton)this.gvBankPayment.FooterRow.FindControl("lbtSalUpdate")).Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                    : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            switch (ddlBankSt.SelectedValue)
            {
                case "0":
                    this.PrintBankStatement();
                    break;

                case "1":
                    this.PrintRDLCFordLetter();
                    //this.PrintForwardingLetter();
                    break;

                case "2":
                    //this.PrintAccountTrans();
                    break;
            }
        }


        private void PrintBankStatement()
        {

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                default:
                    this.PrintrptBankStatement();
                    break;


            }



        }
        private void PrintrptBankStatement()
        {         
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string userid = hst["usrid"].ToString();
            string comname = hst["comnam"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.ddlyearmon.SelectedValue.ToString().Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.ddlyearmon.SelectedValue.ToString().Substring(4));
            DataTable dt = (DataTable)Session["tblover"];
            //string bonDate = Convert.ToDateTime(dt.Rows[0]["bondate"]).ToString("dd-MMM-yyyy");
            string compAdd = this.GetCompAddress();
            string title = "";
            if(chkBonus.Checked && chkEidFitr.Checked)
            {
                title = "Festival Bonus (EID-UL-FITR) Payment Sheet - "+ month +"-" + year;
            }
            else if(chkBonus.Checked)
            {
                title = "Festival Bonus (EID-UL-AZHA) Payment Sheet - "+ month +"-" + year;
            }
            else
            {
                title =  "SALARY & WAGES PAYMENT SHEET - "+ month +" " + year;
            }
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.BankStatement>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptBankStatementBonus", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("compName", userid == "5305139" ? "Footbed Footwear Ltd." : comname)); //Audit user
            Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", title));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string GetCompAddress()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compAddFact = hst["comaddf"].ToString();
            string compAddHO = hst["comadd1"].ToString();
            string compAdd = "";
            string bankCode = this.ddlBankName.SelectedValue.ToString();
            switch (bankCode)
            {
                case "190300010001": //Rocket Accounts- FB
                case "190300020001": //Rocket Accounts-Foodbed
                    compAdd = compAddFact;
                    break;

                default:
                    compAdd = compAddHO;
                    break;
            }
            return compAdd;
        }

        private void PrintRDLCFordLetter()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd/MM/yyyy");
            string bankname = dt.Rows[0]["bankname"].ToString(); //this.ddlBankName.SelectedItem.Text.Trim();
            string banksl = dt.Rows[0]["banksl"].ToString();
            string addr = dt.Rows[0]["bankaddr"].ToString();
            string year = this.ddlyearmon.SelectedValue.ToString().Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.ddlyearmon.SelectedValue.ToString().Substring(4));
            string bacc = this.ddlBankName.SelectedItem.Text.Substring(9).Trim();
           

            var lst = dt.DataTableToList<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.BankFord>();
            LocalReport Rpt1 = new LocalReport();

            double tAmt0 = lst.Select(p => p.amt).Sum();
            double tAmt = lst.Select(p => p.amt).Sum();

            switch (comcod) 
            {

                case "5305":
                case "5306":
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptBankFordLetterFB", lst, null, null);
                    Rpt1.SetParameters(new ReportParameter("BankAdd", bankname + "\n" + addr));
                    Rpt1.SetParameters(new ReportParameter("subject", (this.chkBonus.Checked) ? "Bonus " : ("Subject: Disbursement of Staff Salary A/C of " + comnam)));
                    //Rpt1.SetParameters(new ReportParameter("Det1", "You are requested to transfer below mention amount from  A/C no. " + banksl + " to the under mentioned A/Cs:"));
                    Rpt1.SetParameters(new ReportParameter("Det1", "Reference to the above, may we bring your kind notice that, we are submitting here with the list of our staff along with their name, A/C No & the salary amount for your kind perusal."));
                    Rpt1.SetParameters(new ReportParameter("Det2", "In the above circumstances, we are requesting you to please transfer our staffs salary " + month + "-" + year + " amount as per enclosure in 04(four) sheets ot their respective accounts by debit our CD A/C No-."));
                    Rpt1.SetParameters(new ReportParameter("Det3", "Your Kind co operation in this regards will be highly solicited."));
                    Rpt1.SetParameters(new ReportParameter("Det4", "Thanking in anticipation"));
                    Rpt1.SetParameters(new ReportParameter("company", comnam));
                    break;

                default:

                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptBankFordLetter", lst, null, null);
                    Rpt1.SetParameters(new ReportParameter("BankAdd", bankname + "\n" + addr));
                    Rpt1.SetParameters(new ReportParameter("subject", (this.chkBonus.Checked) ? "Bonus " : ("Subject: Request to disburse the Salary month of  " + month + " " + year + ".")));
                    //Rpt1.SetParameters(new ReportParameter("Det1", "You are requested to transfer below mention amount from  A/C no. " + banksl + " to the under mentioned A/Cs:"));
                    Rpt1.SetParameters(new ReportParameter("Det1", "We would request you please arrange to disburse the net payable Amount of TK,"));
                    Rpt1.SetParameters(new ReportParameter("Det2", "Company Name- Edison Footware Limited."));
                    Rpt1.SetParameters(new ReportParameter("Det3", "Your fast good action would be highly appreciated."));
                    Rpt1.SetParameters(new ReportParameter("Det4", "Thanks & Regards"));
                    break;



            }
           
            Rpt1.SetParameters(new ReportParameter("Amount", tAmt.ToString("#,##0.00;(#,##0.00) ")));
            Rpt1.SetParameters(new ReportParameter("bacc", bacc));
            Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(tAmt), 2)));
            Rpt1.SetParameters(new ReportParameter("Cdate", printdate));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void gvBankPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBankPayment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;

            for (int i = 0; i < this.gvBankPayment.Rows.Count; i++)
            {
                string empid = ((Label)this.gvBankPayment.Rows[i].FindControl("lgempid")).Text.Trim();
                string acno = ((Label)this.gvBankPayment.Rows[i].FindControl("lgvBACNo")).Text.Trim();
                double amount = Convert.ToDouble("0" + ((Label)this.gvBankPayment.Rows[i].FindControl("lblgvAmt")).Text.Trim());

                rowindex = (this.gvBankPayment.PageSize) * (this.gvBankPayment.PageIndex) + i;

                dt.Rows[rowindex]["empid"] = empid;
                dt.Rows[rowindex]["acno"] = acno;
                dt.Rows[rowindex]["amt"] = amount;


            }

            Session["tblover"] = dt;


        }
        protected void lbtSalUpdate_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            string bankcode = this.ddlBankName.SelectedValue.ToString();
            string monthid = this.ddlyearmon.SelectedValue.ToString();
            DataTable dt1 = (DataTable)Session["tblover"];
            string saltype = "";

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string empid = dt1.Rows[i]["empid"].ToString();
                string acno = dt1.Rows[i]["acno"].ToString();
                double amount = Convert.ToDouble(dt1.Rows[i]["amt"].ToString());

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "BANKTRANSFER", monthid, bankcode, empid, acno, amount.ToString(), saltype);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Salary Transfer Updated Successfully');", true);
            }


            string Banklock = (((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Checked) ? "1" : "0";
            bool result1 = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPBANKLOCK", monthid, bankcode, Banklock, saltype, "", "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
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

        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddEmp.Checked)
            {
                DataTable dt1 = (DataTable)Session["tblover"];
                this.divAddEmp.Visible = true;
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
                        dra["idcard"] = dr1["idcard"].ToString();
                        dra["empname"] = dr1["idcard"].ToString() + "-" + dr1["empname"].ToString();
                        dt.Rows.Add(dra);
                    }
                }
                Session.Remove("tblover");
                Session.Remove("tbladdemppay");
                DataTable dt2 = dt1.Copy();
                Session["tbladdemppay"] = dt2;
                DataTable dt3 = dt1.Clone();
                Session["tblover"] = dt3;

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = dt;
                this.DataBind();

                //GridData Bind
                this.Data_Bind();

            }
            else
            {
                this.divAddEmp.Visible = false;
            }
        }
        
        protected void lbtnAddEmployee_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            DataTable dtadd = (DataTable)Session["tbladdemppay"];
            string empid = this.ddlEmployee.SelectedValue.ToString();
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");
            if (dr1.Length == 0)
            {
                DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                dt.ImportRow(dra[0]);
            }
            else
            {
                string existempdet = "Employee : " + dr1[0]["idcard"].ToString() + " - " + dr1[0]["empname"].ToString() + " already existed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);
            }

            DataView dv = dt.DefaultView;
            dv.Sort = ("refno,idcard");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();


        }
    }
}