using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB
{
    public partial class LetterFormat : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CommonButton();
                var type = this.Request.QueryString["Entry"].ToString().Trim();
                string type1 = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = date;
                GetWorkStation();
                GetAllOrganogramList();



                this.GetLettPattern();
                string titale = this.Request.QueryString["Entry"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = titale;
                // ddlEmployee_SelectedIndexChanged(null, null);

            }

        }
        private void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnsave_Click);
        }

        private void LoadRefNo()
        {
            string comcod = this.GetCompCode();
            string qtype = this.Request.QueryString["Type"].ToString();
            string callType = "";
            string date = this.txttodate.Text;
            if (qtype == "9402" || qtype == "9403")
            {
                callType = "GETREFNO";
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", callType, date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.lblRef.Text = ds1.Tables[0].Rows[0]["referno"].ToString();
        }
        private void GetSelected()
        {
            string comcod = this.GetCompCode();

            string qtype = this.Request.QueryString["Type"].ToString();
            string callType = "";
            if (qtype == "10002")
            {
                callType = "GETCANENAME";// from Adv 

            }
            else if (qtype == "9402" || qtype == "9403")
            {
                callType = "GETINSJOINEMPLIST";// from Instant Join

            }


            else
            {
                callType = "GETCANENAME";

            }


            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";

            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";


            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";



            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", callType, qtype, CompanyName, division, projectcode, section, "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[0];
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = ("section like '" + qtype + "%'");

            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = dv1.ToTable();
            this.ddlEmployee.DataBind();



            //this.ddlCat.DataTextField = "section";
            //this.ddlCat.DataValueField = "pactcode";
            //this.ddlCat.DataSource = dv1.ToTable ();
            //// this.ddlCat.Enabled = false;
            //this.ddlCat.DataBind();


            ViewState["empinfo"] = ds1;
        }
        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

            string type1 = this.Request.QueryString["Type"].ToString().Trim();
            if (type1 == "10002" || type1 == "9402" || type1 == "9403")
            {
                var empid = this.ddlEmployee.SelectedValue.ToString();

                var ds = (DataSet)ViewState["empinfo"];
                if (ds == null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                    return;
                }

                DataTable dt1 = ds.Tables[0].Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("empid='" + empid + "'");
                dt1 = dv.ToTable();

                ViewState["name"] = dt1.Rows[0]["empname1"];
                ViewState["section"] = dt1.Rows[0]["section"];
                ViewState["desig"] = dt1.Rows[0]["desig"];


                //this.ddlCat.DataTextField = "dptdesc";
                //this.ddlCat.DataValueField = "empid";
                //this.ddlCat.DataSource = dt1;
                //// this.ddlCat.Enabled = false;
                //this.ddlCat.DataBind();



            }
            else
            {
                var empid = this.ddlEmployee.SelectedValue.ToString();

                var ds = (DataSet)ViewState["empinfo"];
                if (ds == null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                    return;
                }
                DataTable dt1 = ds.Tables[0].Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("empid='" + empid + "'");
                dt1 = dv.ToTable();

                ViewState["name"] = dt1.Rows[0]["empname1"];
                ViewState["section"] = dt1.Rows[0]["section"];
                ViewState["desig"] = dt1.Rows[0]["desig"];
            }


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.txtml.Text = "";
            if (chkpre.Checked)
            {
                //this.PreviousD();
            }
            else
            {
                this.ShowView();
            }
        }
        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            this.txtml.Text = this.data(type);


        }
        protected void chkpre_CheckedChanged(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //if (!chkpre.Checked)
            //    return;
            //string type = this.Request.QueryString["Type"].ToString().Trim();
            //string comcod = this.GetCompCode();
            //string empid = this.ddlEmployee.SelectedValue.ToString();

            //string forJobCand = "";
            //if (type == "10003" || type == "10004" || type == "10005")
            //{
            //    forJobCand = "GETLETTERJOBCAND";
            //}


            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", "%", type, forJobCand, "", "", "", "", "", "");
            //this.ddlPrevious.DataTextField = "empname";
            //this.ddlPrevious.DataValueField = "EMPID";
            //this.ddlPrevious.DataSource = ds3.Tables[0];
            //this.ddlPrevious.DataBind();
            //ds3.Dispose();
        }
        protected void ddlPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cod = this.ddlPrevious.SelectedValue.ToString().Trim();

            this.ddlEmployee.ClearSelection();
            this.ddlEmployee.Items.FindByValue(cod).Selected = true;
        }
        protected void GetLettPattern()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            this.data(type);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected string data(string type01)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string compname = hst["compname"].ToString();
            string usrid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetCompCode();

            //created by nahid for dynamic html value
            //this.LblGrpCompany.Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            string comaddress = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();

            string imgpge = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
            byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(imgpge));
            string complogo = Convert.ToBase64String(imageArray);



            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "SHOWUSERSIGN", usrid, "", "", "", "");
            DataTable dt1 = ds3.Tables[0];
            string empid = this.ddlEmployee.SelectedValue.ToString();

            string type1 = this.Request.QueryString["Type"].ToString().Trim();

            string calltype = (Request.QueryString["Type"].ToString() == "9402") ? "SALARY" : (Request.QueryString["Type"].ToString() == "9403") ? "SALARY" :
                Request.QueryString["Type"].ToString() == "94023" ? "SALARY"
                : "GETEMPSALINFO";

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", calltype, empid, "", "", "", "", "", "", "", "");

            if (ds5.Tables[0].Rows.Count == 0 || ds5 == null)
            {
                return "";
            }


            //DataTable dtsalery = ds5.Tables[0];
            //DataView dvr = new DataView ();
            //DataTable dtr1 = new DataTable ();
            //dtr1 = dtsalery;
            //dvr = dtr1.DefaultView;
            //dvr.RowFilter = ("gcod like '040%'");
            //dtr1 = dvr.ToTable ();
            //DataTable dtsal = dtr1;
            //double SalAdd = Convert.ToDouble ((Convert.IsDBNull (dtsal.Compute ("sum(gval)", "")) ? 0.00 : dtsal.Compute ("sum(gval)", "")));
            double SalAdd = Convert.ToDouble(ds5.Tables[0].Rows[0]["gssal"]);
            string ttlsalary = SalAdd.ToString("#,##0.00;(#,##0.00); ");
            string inwords = "In Word: " + ASTUtility.Trans(SalAdd, 2);
            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table style='width:400px' border = '1'>");

            //Building the Header row.
            html.Append("<tr><th style='width:100px;font-weight:bold'>Desctiption</th><th style='width:100px;font-weight:bold'>Value</th></tr>");

            //Building the Data rows.
            //foreach (DataRow dr1 in dtsal.Rows)
            //{
            //    html.Append ("<tr>");
            //    html.Append ("<td style='width:100px;'>" + dr1["gdesc"].ToString () + "</td>" + "<td style='width:80px; text-align:right'>" + Convert.ToDouble (dr1["gval"]).ToString ("#,##0.00;(#,##0.00); ") + "</td>");
            //    html.Append ("</tr>");
            //}
            //html.Append ("<tr><th style='width:100px;font-weight:bold'>Total</th><th style='width:100px;font-weight:bold; text-align:right'>" + ttlsalary + "</th></tr>");
            ////Table end.
            //html.Append ("</table>");
            string tablesale = html.ToString();

            DataSet dtempinf = (DataSet)ViewState["empinfo"];
            DataView dv = dtempinf.Tables[0].DefaultView;
            dv.RowFilter = ("empid='" + empid + "'");
            DataTable dtempinf_ = dv.ToTable();
            string jdate = "", idCard = "";


            if (type1 == "10015" || type1 == "10012")
            {
                jdate = Convert.ToDateTime(dtempinf_.Rows[0]["jdate"]).ToString("dd-MMM-yyy");
                idCard = dtempinf_.Rows[0]["idcard"].ToString();

            }

            string lbody = string.Empty;
            // string empid=hst["empid"].ToString();
            string name = (string)ViewState["name"];
            string Desig = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["desig"].ToString();//(string)ViewState["desig"];
            string depart = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["dptdesc"].ToString();//(string)ViewState["section"];
            string dptdesc = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["secname"].ToString();//(string)ViewState["section"];        
            string grddesc = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["desig"].ToString();//(string)ViewState["desig"];
            string usrdesig = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["desig"].ToString();
            string usersign = (dt1.Rows.Count == 0) ? "" : Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
            string uname = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["empname"].ToString();
            string refno = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["refno"].ToString();//(string)ViewState["desig"];
            string joindate = (dtempinf_.Rows.Count == 0) ? "" : Convert.ToDateTime(dtempinf_.Rows[0]["joindate"]).ToString("dd-MMM-yyyy");
            string todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string appdate = (dtempinf_.Rows.Count == 0) ? "" : Convert.ToDateTime(dtempinf_.Rows[0]["appdate"]).ToString("dd-MMM-yyyy");//(string)ViewState["desig"];
            string TxtRef = (Request.QueryString["Type"].ToString() == "10005") ? refno : this.lblRef.Text; ;


            //for worker salary data--
            //bsal,hrent,cven,mallow, gssal
            string bsal = (ds5.Tables[1].Rows[0]["bsal"].ToString() == "0" ? "" : Convert.ToDouble(ds5.Tables[1].Rows[0]["bsal"]).ToString("#,##0.00;(#,##0.00); "));
            string hrent = (ds5.Tables[1].Rows[0]["hrent"].ToString() == "0" ? "" : Convert.ToDouble(ds5.Tables[1].Rows[0]["hrent"]).ToString("#,##0.00;(#,##0.00); "));
            string cven = (ds5.Tables[1].Rows[0]["cven"].ToString() == "0" ? "" : Convert.ToDouble(ds5.Tables[1].Rows[0]["cven"]).ToString("#,##0.00;(#,##0.00); "));
            string mallow = (ds5.Tables[1].Rows[0]["mallow"].ToString() == "0" ? "" : Convert.ToDouble(ds5.Tables[1].Rows[0]["mallow"]).ToString("#,##0.00;(#,##0.00); "));
            string gssal = (ds5.Tables[1].Rows[0]["gssal"].ToString() == "0" ? "" : Convert.ToDouble(ds5.Tables[1].Rows[0]["gssal"]).ToString("#,##0.00;(#,##0.00); "));

            string empname = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["empname1"].ToString();
            string fname = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["fname"].ToString();
            string mname = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["mname"].ToString();
            string spousename = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["spousename"].ToString();
            string bdate = (dtempinf_.Rows.Count == 0) ? "" : Convert.ToDateTime(dtempinf_.Rows[0]["bdate"]).ToString("dd-MMM-yyyy");
            string padd = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["padd"].ToString();
            string idcard = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["idcard"].ToString();
            switch (type01)
            {
                //"<img class='Companylogo' src='Image/LOGO8701.PNG' />";
                case "10001":
                    lbody = "<p><strong>Ref: SPL/HR/Appt/16/524</strong></p><p><strong>Nov 20, 2016</strong></p><p><strong>Abdullah Al Noman</strong></p><p><strong>C/O: Abdul Bashar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Boro Hossainput, PO: Banglabazar-3822,</strong></p><p><strong>PS: Begumganj, Dist: Noakhali&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong>Subject: <u>Letter of Appointment.</u></strong></p><p>&nbsp;</p><p>Dear <strong>Mr. Abdullah</strong>,</p><p>&nbsp;With reference to your application and subsequent interview with us, we have the pleasure to inform you that the Management of Star Paradise Ltd is pleased to appoint you as <strong>&ldquo;Field Officer&rdquo;</strong> in our Company <strong>Star Paradise Ltd </strong>on the following terms and conditions:</p><p>&nbsp;</p><p><strong>Commencement and nature of Appointment:</strong></p><ol><li>Your appointment is effective from <strong>Nov 20, 2016</strong>.</li><li>Your appointment is initially on probation basis for 6 (Six) months from the date of your joining. On satisfactory completion of your probationary period, your service may be confirmed. Otherwise, your probationary period may be extended for such a period as may be decided upon by the Management.</li><li>Your place of posting shall be at <strong>'Comilla &ndash; Chandpur&rdquo;</strong> and you shall work under the supervision of <strong>Divisional Head</strong>.</li></ol><p>&nbsp;</p><ol start='2'><li><strong>Placement, Compensation and other benefits:</strong></li><li>You will be entitled to Festival Bonuses as per Company policy, which are two (02) Eid Bonus, 100% of your Gross Salary each.</li><li>Your monthly salary as in &ldquo;Annexure-A&rdquo; only, which includes all your perquisites and allowances. Compensation will be governed by the rules of the Company on the subject, as applicable and / or amended hereafter.</li><li>Personal Income Tax, if any will be on your account and will be deducted each month by the company at source at the time of monthly salary disbursement for onward submission to the relevant Income Tax authorities.</li><li>You are not to disclose / discuss your salary with anyone related to this organization and keep it strictly confidential.</li><li>This is a position of full time and continuous responsibilities and will not engage yourself any Part-time work, profession or employment without written permission from the management.</li><li>You will be entitled with other benefits of the organization time to time as per the Company policy.</li></ol><p>&nbsp;</p><ol start='3'><li><strong>Duties and responsibilities:</strong></li><li>You will carry on with the duties and responsibilities entrusted to you and also the duties and responsibilities that may be entrusted to you by the Management from time to time. You will require working late hours whenever necessary for the greater interest of the organization.</li><li>You have to abide by all instructions and orders issued by the management in good spirit.</li><li>You will retire attaining the age limit fixed by the Bangladesh Govt. through Bangladesh Labor Act.</li></ol><p>&nbsp;</p><ol start='4'><li><strong>Transfer:</strong></li><li>Your Service is transferable from one project to another project of the Company for the greater interest of the Organization.</li><li>The Management may change your designation, duties and responsibilities from time to time as they think fit and proper without disturbing salary and allowances.</li></ol><p>&nbsp;</p><ol start='5'><li><strong>Termination of Service:</strong></li><li>The Management reserves the right to terminate your service at any time without assigning any reason, if your work, attitude or behavior not found satisfactory.</li><li>Either party may however, terminate the contract of employment by giving a notice period of 60 (Sixty) days in writing or in lieu thereof an equivalent of two months&rsquo; basic salary, will have to be paid by the company / surrendered by you in case of failure in giving two months&rsquo; prior notice after confirmation of service.</li><li>During the probation period, either party may terminate the contract of employment with 30 (Thirty) days prior notice.</li><li>When you intend to resign you will have to handover official charges to the nominated person of the Company.</li></ol><p>&nbsp;</p><ol start='6'><li><strong>Confidentiality:</strong></li></ol><p>You shall not, at any time, during the continuance or even after the cessation of your employment hereunder, disclose or divulge or make public, except on legal obligations, either directly to any person, firm or company or use for yourself any trade secret or confidential, technical knowledge, formula, process, compositions, ideas or documents, concerning the business and affairs of the company or any of its dealings, transactions or affairs which you may have acquired from the company or have to your knowledge during the course of and incidental to your employment. If you disclose any such information to any other person(s) or organization, the Company shall prosecute against you for such breach of code of conducts, as it considers necessary to protect its interest and enforce its rights.</p><p>&nbsp;</p><h2>Annexure &ndash; A</h2><p>&nbsp;</p><p><strong>Dear Mr. Abdullah,</strong></p><p>&nbsp;</p><p>You shall be placed at <strong>Grade-2</strong>, the monthly Gross salary of <strong>Tk. 6,000.00 (Six Thousand)</strong> only which is broken down as follows:</p><p>&nbsp;</p><p>&nbsp;<strong><u>Particulars &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; In Taka</u></strong></p><p><strong><u>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3,600.00&nbsp;&nbsp;</u></strong></p><p><strong><u>House Rent &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;1,800.00</u></strong></p><p><strong><u>Conveyance Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;330.00</u></strong></p><p><strong><u>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;270.00</u></strong></p><p><strong><u>Total Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;6,000.00 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;(Six Thousand)</u></strong></p><p>&nbsp;</p><p>If you are agreeable to the aforesaid offer, please acknowledge this letter by way of formal acceptance and return immediately for further action.</p><p>&nbsp;We have the pleasure in welcoming you and sincerely hope that our company will get benefited from your service.</p><p>&nbsp;</p><p>For <strong>Star Paradise Ltd</strong>,</p><p>&nbsp;</p><p>&nbsp;<strong>(Moshiur Hossain Uday)</strong></p><p><strong>Managing Director</strong></p><p>&nbsp;</p><p>I<strong>, Abdullah Al Noman</strong> have fully understood the contents of the letter of appointment and willingly agree to abide by the terms and conditions as stipulated herein above.</p><p>&nbsp;</p><p>&nbsp;</p><p>______________________</p><p>Signature of the Employee</p><p>&nbsp;</p><p>Date: __________________</p><p><strong>&nbsp;</strong></p><p><strong>Copy to:</strong></p><ol><li>HRIS</li><li>Personal File</li></ol>";
                    break;
                //Joining letter for factory    
                case "9402":
                    lbody = "<div class='letbody'><div class='pntHeadJon'><p><p><p>রেফারেন্স:  " + TxtRef + "</p><p><strong> তারিখঃ  " + todate + " </strong></p><p><strong>" + name + "</strong></p><p><strong>গ্রাম ঃ বাহাদুরপুর   &nbsp;&nbsp;        পোষ্ট ঃ মির্জাপুর </strong></p><p><strong>থানা ঃ গাজীপুর সদর &nbsp;&nbsp;    জেলা ঃ গাজীপুর&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong><strong>&nbsp;</strong></p></div><p><strong class='text-center'>নিয়োগপত্র</strong></p><p>&nbsp;</p><p>জনাব <strong>" + name + "</strong>,<p></p></p><p>আমরা আপনাকে <strong> " + comnam + " </strong> এ <strong> " + depart + " </strong> বিভাগের <strong>" + dptdesc + "</strong> সেকশনের অধীনে <strong>  " + Desig + "</strong> পদে নিয়োগ দিতে পেরে আনন্দিত । আপনার জন্য নিম্নেবর্ণিত শর্তসমূহ প্রযোজ্য হবে যা আগামী <strong>" + joindate + "</strong> তারিখ হতে কার্যকর হবে।</p><p>&nbsp;</p><p>১। আপনার নিয়োগের তারিখ হতে আপনাকে প্রতি মাসে সর্বসাকুল্যে <strong> " + ttlsalary + "/-" + inwords + "</strong> প্রদান করা হবে।তাছাড়াও কোম্পানীর নীতিমালা অনুযায়ী আপনাকে বছরে দুই বার উৎসব ভাতা প্রদান করা হবে।</p><p><p></p>২। ক)আপনার ক্ষেত্রে প্রযোজ্য অন্যান্য সুযোগ সুবিধা ও নিয়ম কানুন কোম্পানী কর্তৃক সংযুক্ত চাকুরীর শর্তাবলীতে উল্লেখ করা হয়েছে। উল্লেখিত চাকুরীর শর্তাবলী ও নিয়মকানুন সংশোধিত হলে তা আপনার ক্ষেত্রে আরোপিত হবে।&nbsp;&nbsp;<p></p>খ)আপনি আপনার বেতন ও অন্যান্য শর্তাবলী কোম্পানীর অন্য করো নিকট প্রকাশ করবেন না।</p><p></p><p>৩। আপনি আপনার নিয়োগের তারিখ হতে ৬ (ছয়) মাস পর্যন্ত শিক্ষানবিশ থাকবেন। আপনার শিক্ষানবিশ কালীন সময়ে কোন প্রকার কারণ ব্যতিরেকে এবং কোন প্রকার ক্ষতিপূরণ ছাড়া আপনার চাকুরী সমাপ্ত হতে পারে।</p><p></p><p>৪। সফলভাবে শিক্ষানবিশকাল অতিবাহিত করলে আপনার নিয়োগ স্থায়ী হবে । </p><p></p><p>৫। আপনি সুইং সুপারভাইজার পদে নিয়োগ লাভ করেছেন এবং প্রোডাকাশন অফিসার এর নিকট রিপোর্ট করবেন।</p><p></p><p>৬। আপনার চাকুরী স্থায়ী হবার পর কারখানার প্রচলিত নিয়ম অনুসারে নোটিশ প্রদান করে অথবা নোটিশ মেয়াদের পরিবর্তে সমপরিমান বেতন পরিশোধ করে কো¤পানী আপনার চাকুরীর অবসান ঘটাতে পারে। আপনিও দুই মাসের নোটিশ প্রদান করে অথবা উক্ত নোটিশের পরিবর্তে দুই মাসের বেতন ক্ষতিপূরণ দিয়ে আপনার চাকুরীর অবসান ঘটাতে পারবেন।</p><p></p><p>৭। আপনার আয়ের উপর যদি কোন কর প্রযোজ্য হয়ে থাকে তাহলে তা আপনার মাসিক বেতন হতে কর্তন করা হবে।</p><p></p><p>৮। কর্তৃপক্ষ চাইলে আপনাকে বাংলাদেশের অভ্যন্তরে যে কোন স্থানে বদলি করতে পারবেন।</p><p></p><p>৯। এই চুক্তির শর্ত এমন যে, আপনি আপনার চাকুরী করা কালীন সময়ে অথবা চাকুরী শেষে কোন অননুমোদিত ব্যক্তির নিকট কোন তথ্য, ঘটনা, কোম্পানীর ব্যাবসা সংক্রান্ত কোন নথি, চুক্তি, অর্থ, পরিকল্পনা অথবা ষ্টাফদের বেতন সম্পর্কে কোন কিছু প্রকাশ বা আলোচনা করবেন না। এই শর্ত মানতে ব্যর্থ হলে আপনি বিশ্বাস ভঙ্গের দায়ে অভিযুক্ত হবেন এবং এর ফলে আপনার চাকুরীচ্যুতি হতে পারে এবং/ অথবা আইন অনুযায়ী আপনাকে দোষী সাব্যস্ত করা হবে।</p><p></p><p>১০। আপনার বুঝা উচিত যে, আপনার নিয়োগের নিশ্চয়তা আপনার চাকুরীর আবেদনের তথ্যের সত্যতা যাচাইয়ের উপর নির্ভরশীল।<p></p> আপনি যদি এই পত্রে উল্লেখিত শর্তসাপেক্ষের ভিত্তিতে এই প্রস্তাব গ্রহন করতে সম্মত হন তাহলে আপনাকে এই পত্রের ডুপ্লিকেট কপিটি স্বাক্ষর করে জমা দেওয়ার জন্য অনুরোধ করছি।<p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><strong>আমরা আপনার নির্বাচনকে অভিনন্দন জানাচ্ছি এবং <strong> " + comnam + " </strong> এ আপনাকে স্বাগত জানাচ্ছি।</strong> </p><p></p><p>আপনার বিশ্বস্ত,</p><p>&nbsp;</p><table border='0' style='border:none;'><tr><td><p></p><p></p><p>--------------------------------</p>  <p class='pUname'><strong>বিপুল বরন ঘোষ মোঃ নুরুজ্জামান </strong></p> <p class='pUname'><strong>(ম্যানেজার মানব সম্পদ ও কমপ্লায়েন্স)</strong></p></td><td> <p></p><p></p><p></p><p>------------------------</p><p class='pUname'><strong> মোঃ নুরুজ্জামান</p> <p class='pUname'><strong> (হেড অফ বিজনেস)</strong></p></td></tr></table><p></p><p></p><p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p>আমি, -------------------------------------------, নিম্নেবর্ণিত স্বাক্ষরকারী নিশ্চয়তা প্রদান করছি যে এই নিয়োগ পত্রের ১ হতে ১০ পর্যন্ত উল্লেখিত শর্ত সাপেক্ষে একমত হয়ে <strong> " + comnam + " </strong> এ নিয়োগ গ্রহন করছি।</p><p></p><p>স্বাক্ষরঃ ...........................</p><p>তারিখঃ</p></div>";
                    break;
                case "9403":
                    lbody = "<div class='letbody'><div class='pntHeadJon'><p><span style='font-size: 5pt;'><strong>নিয়োগপত্র</strong></span></p><table style='border: none;'" +
                                                                                                                                                                           " border='0'><tbody><tr><td><p><span style='font-size: 5pt;'>নাম</span></p></td><td><p><span style='font-size: 5pt;'>:" + empname + "</span></p>" +
                                                                                                                                                                           "</td><td><p><span style='font-size: 5pt;'>পিতার নাম</span></p></td><td><p><span style='font-size: 5pt;'>:" + fname + "</span></p></td></tr>" +
    "<tr><td><p><span style='font-size: 5pt;'>মাতার নাম</span></p></td><td><p><span style='font-size: 5pt;'>:" + mname + "</span></p></td>" +
    "<td><p><span style='font-size: 5pt;'>স্বামী/স্ত্রীর নাম</span></p></td><td><p><span style='font-size: 5pt;'>:" + spousename + "</span></p></td></tr>" +
    "<tr><td><p><span style='font-size: 5pt;'>কার্ড নং</span></p></td><td><p><span style='font-size: 5pt;'>:" + idcard + "</span></p></td><td>" +
    "<p><span style='font-size: 5pt;'>জন্ম তারিখ</span></p></td><td><p><span style='font-size: 5pt;'>:" + bdate + "</span></p></td></tr>" +
    "<tr><td><p><span style='font-size: 5pt;'>স্থায়ী ঠিকানা</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p></td>" +

    "<td><p><span style='font-size: 5pt;'>বর্তমান ঠিকানা</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p></td></tr>" +


    "<tr><td><p><span style='font-size: 5pt;'>গ্রাম</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p></td>" +

    "<td><p><span style='font-size: 5pt;'>গ্রাম</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p>" +
    "</td></tr><tr><td><p><spans tyle='font-size: 5pt;'>থানা</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p>" +
    "</td><td><p><span style='font-size: 5pt;'>থানা</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p>" +
    "</td></tr><tr><td><p><span style='font-size: 5pt;'>ডাকঘর</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p>" +
    "</td><td><p><span style='font-size: 5pt;'>ডাকঘর</span></p></td><td><p><span style='font-size: 5pt;'>:-----------------------</span></p></td></tr>" +
     "<tr><td><p><span style='font-size: 5pt;'>জেলা</span></p></td><td><p><span style='font-size: 5pt;'>:" + padd + "</span></p>" +
    "</td><td><p><span style='font-size: 5pt;'>জেলা</span></p></td><td><p><span style='font-size: 5pt;'>:" + padd + "</span></p></td>" +
    "</tr></tbody></table>" +
        "<p style='text-align: justify; line-height: 13px !important;'><span style='font-size: 5pt; line-height: 13px !important;'>আপনার আবেদন পত্র ও স্বাক্ষাৎকারের প্রেক্ষিতে কাজের উপযুক্ততা বিবেচনায়,আপনাকে ইং তারিখ থেকে নং গ্রেডে&nbsp; পদে সেকশনের ০৩ (তিন) " +
                                        "মাসের প্রবেশন পিরিয়ডের জন্য মাসিক মজুরী ভিত্তিতে নিয়োগ করা হল। প্রবেশন পিরিয়ডে আপনার দক্ষতা ও আচরনের মান নির্নয় করা না গেলে আরও(৩)তিন মাস প্রবেশন পিরিয়ড বর্ধিত করা যেতে পারে। উল্লেখ্য যে," +
                                        " প্রবেশন পিরিয়ড শেষে আপনাকে শ্রম আইন ২০০৬ ধারা-৪ এর উপধারা-৭ অনুযায়ী স্থায়ীকরন করা হবে। আপনার মজুরীর বিন্যাস নিম্নরুপ: &nbsp;<strong> &nbsp; &nbsp;" +
                                        " &nbsp; &nbsp;</strong></span><span style='font-size: 5pt;'><strong>&nbsp;</strong></span></p></div>" +
                                        "<table style='font-size: 5pt;' border='1'><thead><tr><th><p><span style='font-size: 5pt;'>মূল মজুরী </span></p></th>" +
    "<th><p><span style='font-size: 5pt;'> বাড়ীভাড়া ভাতা </span></p></th><th><p><span style='font-size: 5pt;'> চিকিৎসা ভাতা </span></p></th>" +
    "<th><p><span style='font-size: 5pt;'> যাতায়াত ভাতা </span></p></th><th><p><span style='font-size: 5pt;'> মাসিক মোট মজুরী </span></p></th>" +

    "</thead></tr><tbody><tr  border='1' style='text-align:right;'><td>" + bsal + "</td><td>" + hrent + "</td><td>" + mallow + "</td> <td>" + cven + "</td><td>" + gssal + "</td></tr></tbody></table>" +
    "<p style='text-align: justify; line-height: 13px;'><span style='font-size: 5pt;'>চাকুরীর শর্তাবলী ঃ </span><br /><span style='font-size: 5pt;'>১) আপনাকে দৈনিক কমপক্ষে ০৮(আট) ঘন্টা কাজ করতে হবে। ০৮(আট) ঘন্টার " + "অতিরিক্ত কাজের জন্য(স্ব-ইচ্ছায়)প্রচলিত শ্রম আইন অনুযায়ী ওভারটাইম ভাতা পাবেন। আপনি দৈনিক কমপক্ষে ১(এক) ঘন্টা আহার বিরতি পাবেন। আপানাকে একটি পরিচয় পত্র তথা পাঞ্চ কার্ড দেয়া হবে। ফ্যাক্টরীতে আসা ও যওয়ার সময় কার্ড পাঞ্চ মেশিনে পাঞ্চ করে হাজিরার " +
                                                                                                                               "সময় " + "রের্কড করতে হবে যা থেকে মাসিক হাজিরা এবং অতিরিক্ত কাজের সময় হিসাব করা হবে।</span><br /><span style='font-size: 5pt;'>২) দৈনিক নির্দিষ্ট সময়ের অতিরিক্ত সময় কাজের জন্য আপনি মূল মজুরীর সাধারন হারের দ্বিগুন হারে ভাতা পাইবেন। অর্থ্যাৎ মূলমজুরী / ২০৮ ঘন্টা দ্ধ ২ দ্ধ মোট অতিরিক্ত কর্ম ঘন্টা।</span><br /><span style='font-size: 5pt;'>৩) আপনার মাসিক মজুরী পরবর্তী মাসের ০৭(সাত) কর্মদিবসের মধ্যে প্রদান করা হইবে এবং মজুরী ও ওভারটাইম বাবদ সমুদয় পাওনা একইসাথে একই তারিখে পরিশোধ করা হবে।</span><br /><span style='font-size: 5pt;'>৪) আপনি প্রতি বৎসর পূর্ন মজুরীতে বর্ণিত হারে ছুটি পাবেন :- ক) নৈমিত্তিক ছুটি-১০ দিন খ) অসুস্থতাজনিত ছুটি-১৪ দিন গ) উৎসব ছুটি- নূন্যতম ১১ দিন ঘ) বাৎসরিক ছুটি-১৮ দিন কাজ করার জন্য ০১ (এক) দিন প্রাপ্ত হবেন। ঙ) মহিলা শ্রমিকের ক্ষেত্রে মাতৃত্বকালীন ছুটি ১৬ সপ্তাহ (একটানা কমপক্ষে ০৬ মাস চাকুরী পূর্ণ করলে এই সুবিধা ভোহ করতে পারবেন। যদি দুই বা ততোধিক সন্তান জীবিত থাকে তবে মাতৃত্বকালীন সুবিধা পাবেননা,কিন্তু মাতৃত্বকালীন ছুটি পাবেন।এখানে উল্লেখ থাকে যে, উক্ত ছুটি পীড়া ছুটি ও বার্ষিক ছুটি হইতে কর্তৃপক্ষের নিকট লিখিত আবেদন ও অনুমোদনের মাধ্যমে সমন্বয় করা হবে।</span><br /><span style='font-size: 5pt;'>৫) সাপ্তাহিক ও উৎসব ছুটি ব্যতীত মাসের বাকী সকল কর্ম দিবসে কর্মস্থলে উপস্থিত থাকলে কোম্পানীর প্রচলিত নিয়ম অনুযায়ী আপনি হাজিরা বোনাস পাবেন। </span><br /><span style='font-size: 5pt;'>৬) কোম্পানীর প্রচলিত নিয়ম অনুযায়ী আপনি বৎসওে দুইটি উৎসব বোনাস পাবেন।</span><br /><span style='font-size: 5pt;'>৭) বাংলাদেশ শ্রম আইন অনুযায়ী আপনার বার্ষিক বেতন বা মজুরী বৃদ্ধি হইবে। তবে কোম্পানীর প্রচলিত নিয়মানুযায়ী আপনার পদোন্নতি হবে।</span><br /><span style='font-size: 5pt;'>৮) বাংলাদেশ শ্রম আইন অনুযায়ী আপনাকে গ্রুপ বীমার অন্তর্ভূক্ত করা হবে।</span><br /><span style='font-size: 5pt;'>৯) আপনি চাকুরী হইতে ইস্তফা দিতে চাইলে,অস্থায়ী শ্রমিকের ক্ষেত্রে ৩০(ত্রিশ) দিনের এবং চাকুরী স্থায়ী হওয়ার ৬০(ষাট)দিনের লিখিত নোটিশ বা নোটিশের পরিবর্তে নোটিশ মেয়াদের জন্য মজুরীর সমপরিমান অর্থ প্রদান করে চাকুরী থেকে ইস্তফা নিতে পারেন। অন্যদিকে কর্তৃপক্ষ আপনার চাকুরীর অবসান কারতে চাইলে প্রবেশন পিরিয়ডে আপনার কাজ কর্ম আচার-আচরন প্রভূতি সন্তোষজনক বিবেচিত না হইলে কর্তৃপক্ষ যেকোন সময় কোন প্রকার নোটিশ ছাড়া এবং কোন প্রকার ক্ষতিপূরন ছাড়াই আপনাকে চাকুরী হতে অব্যাহতি দিতে পারিবেন। চাকুরী স্থায়ী হওয়ার পর ১২০(একশত বিশ) দিনের লিখিত নোটিশ বা নোটিশের পরিবর্তে নোটিশ মেয়াদের জন্য মজুরী প্রদান করে চাকুরী থেকে অবসান করতে পারবেন।</span><br /><span style='font-size: 5pt;'>১০) কারখানা লে-অফ, ছাঁটাই, ছাঁটাইকৃত শ্রমিক পূনঃনিয়োগ, শ্রমিক কর্মচ্যুতি, চাকরীর অবসান ইত্যাদি ও এর ক্ষতিপূরন এবং অবসরজনিত সুবিধা,মৃত্যুজনিত সুবিধা ইত্যাদি বাংলাদেশ শ্রম আইন ও কোম্পানীর প্রচলিত নীতিমালা অনুযায়ী প্রযোজ্য হবে।</span><br /><span style='font-size: 5pt;'>১১) আপনি চাকুরী হতে ইস্তফা গ্রহনকালে অত্র কোম্পানীতে অবিচ্ছিন্নভাবে ০৫(পাঁচ) বছরের বেশী কিন্তু ১০(দশ) বছরের কম সময় চাকুরীর করলে প্রতি পূর্ণ বছরের জন্য ১৪(চৌদ্দ) দিনের এবং ১০(দশ) বছর বা তদুর্ধ্ব সময় চাকুরী করলে প্রতি পূর্ণ বছরের জন্য ৩০(ত্রিশ) দিনের মজুরী ক্ষতিপূরন হিসাবে আপনাকে প্রদান করা হবে। </span><br /><span style='font-size: 5pt;'>১২) অত্র প্রতিষ্ঠানে কর্মরত থাকাকালীন আপনি অন্য কোথাও প্রত্যেক্ষ বা পরোক্ষভাবে কোন চাকুরী গ্রহন করতে পারবেনা। চাকুরীতে কর্মরত থাকাকাকলীন সময়ে আপনি অত্র প্রতিষ্ঠানের ব্যবসা সংক্রান্ত গোপনীয় তথ্যাদি কোন ব্যক্তি,ব্যবসা প্রতিষ্ঠান বা অন্য কারো নিকট প্রকাশ করতে পারবেনা। আপনার চাকুরীর পরিসমাপ্তি ঘটলে আপনি এই কোম্পানীর সমস্ত কাগজপত্র,দালিলাদি অথবা অন্য কোন বস্তু যেমন আই,ডি,কার্ড,ইউনিফরম,কাজের সরঞ্জামাদি, ইত্যাদি আপনার হেফাজতে থাকলে,সেই সকল দ্রব্যাদি ফেরত দিবেন। কোম্পানীর ব্যবসা সংক্রান্ত কোন কাগজ পত্রের নকল অথবা অংশ বিশেষ আপনার নিকট থাকলে তা ফেরত দিতে বাধ্য থাকবেন।</span><br /><span style='font-size: 5pt;'>১৩) আপনার চাকুরী কোম্পানী কতৃক জারীকৃত বিধি-বিধান বাংলাদেশের প্রচলিত শ্রম আইন দ্বারা পরিচালিত হবে। বিভিন্ন সময়ে কোম্পানীর ব্যবস্থপনা পরিষদ কোম্পানীর গঠন,প্রকৃতি,নিয়ম-নীতি, শৃঙ্খলা সংক্রান্ত যে সব দিক নির্দেশনা এবং সিদ্ধান্ত দিবেন, আপনি তা সর্বদা মেনে চলতে বাধ্য থাকবেন। সেই সঙ্গে কোম্পানীর ক্রেতাদের বিভিন্ন সময় প্রদত্ত নীতিমালা নিয়ম &ndash;কানুন সঠিক ভাবে মেনে চলবেন। আপনি যদি কখনও কোনরূপ অসদাচরনের জন্য দোষী প্রমানিত হন,তবে কতৃপক্ষ বাংলাদেশ শ্রমআইন ২০০৬/(২০১৩) ও শ্রমবিধি ২০১৫ অনুযায়ী যে কোন ধরনের  কোম্পানীর আান্ত ব্যবস্থা গ্রহন করিতে পারিবেন।</span><br /><span style='font-size: 5pt;'>১৪) বিনামূল্যে কোম্পানীর চিকিৎসা কেন্দ্রে চিকিৎসা সুবিধা গ্রহন করতে পারবেন। তাছাড়া কর্তৃপক্ষ আপনার শারীরিক বা মানসিক অক্ষমতা বা অব্যাহত ভগ্ন স্বাস্থ্যের সুস্থ্যতা পরীক্ষার জন্য কোম্পানীর রেজিস্টার্ড চিকিৎসকের নিকট পাঠাতে পারিবেন। আপনার বয়স প্রত্যয়নের ক্ষেত্রে শিক্ষাগত যোগ্যতার সনদপত্র ও জাতীয় পরিচয়পত্রের জন্ম তারিখই সঠিক বলিয়া বিবেচিত হইবে এবং শারীরিক ও মানসিক সুস্থ্যতার ক্ষেত্রে রেজিস্টার্ড ডাক্তারের মতামতই চূড়ান্ত বলে গন্য হইবে।</span><br /><span style='font-size: 5pt;'>১৫) দেশের আইন-শৃঙ্খলা বাহিনীর প্রয়োজনে কর্তৃপক্ষ কোম্পানীতে প্রদত্ত আপনার সকল তথ্য প্রদান করিতে পারিবেন।</span><br /><span style='font-size: 5pt;'>১৬) কোম্পানী কোন বিষয়ে ট্রেনিং প্রদান করলে, উক্ত বিষয় সংশ্লিষ্ট দায়িত্ব পালন করতে দিলে তা করতে বাধ্য থাকিবেন। </span><br /><span style='font-size: 5pt;'>১৭) কারখানার অভ্যন্তরে কর্মরত অবস্থায় কোন প্রকার দূর্ঘটনা ঘটলে কর্তৃপক্ষ বিনা মুল্যে চিকিৎসা সেবা প্রদান করবেন।</span><br /><span style='font-size: 5pt;'>১৮) অন্যান্য বিষয়াদি ও সুযোগ-সুবিধা যা এই্ পত্রে বর্ণিত হয় নাই, তা বাংলাদেশ শ্রম আইন ২০০৬,২০১৩ ও শ্রমবিধিমালা ২০১৫ অনুযায়ী কোম্পানীর যাবতীয় সুবিধাদি পাবেন।</span><br /><span style='font-size: 5pt;'>১৯) আপনার জীবন বৃত্তান্তে প্রদত্ত স্থায়ী বা বর্তমান ঠিকানার পররিবর্তন করলে তা সাথে সাথে কর্তৃপক্ষকে লিখিতভাবে অবহিত করতে হবে। তবে উল্লেখ থাকে যে, নিরাপত্তা বিভাগ, লোডার, স্টোর বা ভান্ডার সেকশন ও প্যাকিং সেকশনের সকল শ্রমিকদের ক্ষেত্রে তাদের স্থায়ী ঠিকানার ০২ (দুই) জন ব্যক্তির নাম, ঠিাকানা ও মোবাইল প্রদান করতে হবে, যাহাতে পরবর্তীতে কর্তৃপক্ষ তাহাদের সাথে যোগাযোগের মাধ্যমে আপনার সম্পর্কে অবগত হইতে পারেন।</span><br /><span style='font-size: 5pt;'>২০) আপনার বর্তমান কর্মস্থলে হইবে এডিসন ফুটওয়্যার লিঃ তালতলী,হোতাপাড়া, গাজীপুর। বর্তমানে প্রদত্ত মজুরী ও সুযোগ সুবিধায় কোম্পানীর স্বার্থে প্রয়োজনবোধ এই মালিকের অধীনে অন্য যে কোন কারখানায় বা প্রকল্পে/প্রতিষ্ঠানে (বাংলাদেশের মধ্যে) বা এই প্রতিষ্ঠানের যে কোন বিভাগে বা শাখায় নিয়োজিত করতে পারবেন বা বদলি" + " করতে পারবেন।</span><br /><br/><br/><br/><span style='font-size: 5pt;'>......................................................................</span><br/><span style='font-size: 5pt;'>অনুমোদনকারী (মানব সম্পদ বিভাগ)</span><br /><br/></p>" +
                            "<span style='font-size: 5pt;'>আমি অত্র কোম্পানীর নিয়োগপত্রে বর্ণিত সকল শর্তাবলী সম্পূর্ণভাবে অবগত(পড়িয়া/শুনিয়া/বুঝিয়া) হইয়া, কারো প্ররোচনা ব্যতিরেকে কোনপ্রকার জবরদস্তি ছাড়া স্বেচ্ছায় ও স্বজ্ঞানে উপরোক্ত সকল " + "শর্তসমূহ মানিয়া উল্লিখিত ইউনিট ও সেকশনে কাজে যোগদান করিলাম এবং এই নিয়োগপত্রে স্বাক্ষর ও টিপসই প্রদান করতঃ ১ কপি নিয়োগপত্র গ্রহন করিলাম।</span>&nbsp;</p><table  border='1' style='text-align:center;'><tbody>" +
    "<tr><td width='40'<p>&nbsp;</p></td><td width='111'><p>&nbsp;</p></td><td width='110'><p>&nbsp;</p></td><td width='60'><p>&nbsp;</p></td></tr><tr>" +
    "<td width='40'><p>&nbsp;</p></td><td width='111'><p>&nbsp;</p></td><td width='110'><p>&nbsp;</p></td><td width='60'><p>&nbsp;</p></td></tr><tr><td colspan='2'" + "width='151'><p><strong>টিপসই</strong></p></td><td colspan='2' width='169'><p><strong>স্বাক্ষর</strong></p></td></tr></tbody></table></div>";
                    break;
                //Joining Letter for factory    
                case "10005":
                    lbody = "<div class='printHeader pntHeadJon'><p><br><br><br>Date: " + cdate + "</p><p><strong> Mr.Md.Nafiz Alam </strong></p><p><strong>Deputy General Manager</p><p>Human Resources Department</p><p>Edison Footwear Ltd.</p><p>Rangs Babylonia (level 6)</p><p>246, Bir Uttam Mir Shawkat Road, </p><p>Tejgaon, Dhaka 1208.</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong class='subj'>Subject: <u>Joining Letter</u></strong></p><p>&nbsp;</p> <p></br></br></br><br></br></br></br></p><div class='letbody'><p>Dear <strong>Sir</strong>,</p><p>I have honour to submit my joining report for the post of " + Desig + " in <stron> " + dptdesc + " </strong> department today the <stron>" + todate + " </strong> in respect to your appointment letter dated " + appdate + " Ref# " + TxtRef + ".</p>Yours sincerely,<p><br><br><br><br><br><br><p class='pImage'></p>  <p class='pUname'><strong>" + name + "</p> <p class='pUname'><strong>" + Desig + "</strong></p><p><br><br><br><br><br><br> Full Name in Capital Letters</p></div>";
                    break;

                //////offer later for sales department;
                ////case "10003":
                ////    lbody = "<p><strong>SPL/HR/Ofr/16/586</strong></p><p><strong>" + name + "</strong></p><p><strong>Vill: Nalua, PO: Afalkait</strong></p><p><strong>PS: Bekergang, Dist: Barisal</strong></p><p><strong>Sub: Offer for Employment</strong></p><p>Dear <strong>" + name + "</strong></p><p>With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong> " + Desig + "</strong> in <strong>" + comnam + "</strong> at <strong>" + dptdesc + ", " + depart + " </strong> with effect from <strong> " + cdate +" </strong>.</p><p>&nbsp;The Letter of Appointment will be issued soon.</p><p>&nbsp;</p><p>&nbsp;Please bring the following papers on the date of joining:</p><ol><li>Photocopy of all academic certificates</li><li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer</li><li>Passport size photograph -3</li><li>Photocopy of passport /Photocopy of National ID Card</li><li>Salary Certificate/ Bank Statement</li><li>300 (100*3) Tk Stamp Paper</li></ol><p>&nbsp;</p><p>&nbsp;</p><p>Yours Sincerely,</p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + "Moshiur Hossain Uday" + "</p> <p class='pUname'><strong>" + "Managing Director" + "</strong></p>";

                ////    break;
                //////offer Later For general
                ////case "10004":
                ////    lbody = "<p><strong>SPL/HR/Ofr/16/559</strong></p><p><strong>" + name + "</strong></p><p><strong>Vill: West Vashanchar, PO: Ambikapur</strong></p><p><strong>PS: Faridpur, Dist: Faridpur</strong></p><p>&nbsp;</p><p>&nbsp;Dear <strong>" + name + "</strong></p><p>&nbsp;</p><p>&nbsp;<span style='text-decoration: underline;'><strong>Offer for Employment</strong></span></p><p>&nbsp;With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong>&ldquo;Plant Manager-Pole Project&rdquo; </strong>in <strong>" + comnam + "</strong>at <strong>Factory </strong>with effect from <strong>1 January,</strong><strong> 2017</strong></p><p>The Letter of Appointment will be issued soon.</p><p>&nbsp;</p><p>Please bring the following papers on the date of joining:</p><ol><li>Photocopy of all academic certificates</li><li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer</li><li>Passport size photograph -3</li><li>Photocopy of passport /Photocopy of National ID Card</li><li>Salary Certificate/ Bank Statement</li></ol><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp; &nbsp; Yours Sincerely,</p>"; //<p>&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;</p><p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p><strong>&nbsp;Managing Director</strong></p>";
                ////    break;

                ////// extension letter
                ////case "10006":
                ////    lbody = "<p><strong>Ref. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : SPL/HR/Exten/511/15</strong></p><p>Date &nbsp;&nbsp;&nbsp;&nbsp; : December 1, 2016</p><p>&nbsp;<strong>" + name + "</strong></p><p><strong>Staff ID # 124</strong></p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject: <strong>Unsatisfactory Performance during Probationary Period.</strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;Please be informed that after a careful review of your performance during probation period, it is found that you have failed to show the satisfactory performance, which is required for your position. Due to your unsatisfactory performance, we are unable to confirm you after six months&rsquo; probation period.</p><p>Your supervisor has suggested areas for improvement in your probationary assessment sheet, and it is expected that you will seek every opportunity to improve the highlighted areas.Therefore, through this letter you are placed under observation for <strong>3 months</strong> beginning from, <strong>01.11.2016.</strong> After completion of the observation period, your performance will re-evaluated by your supervisor. If you fail to show satisfactory performance during that period, your service may be no longer required for the organization.</p><p>&nbsp;</p><p>Thank you and hope you will make every efforts to improve yourself within <strong>next three months</strong>.</p><p>&nbsp;</p><p><strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p>&nbsp;CC:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HRIS</p><p>Personal File&nbsp;</p>";
                ////    break;
                //////promotion letter
                ////case "10007":
                ////    lbody = "<p style='text-align: center;'>&nbsp;</p><h3 style='text-align: center;'><span style='text-decoration: underline;'><strong>Private &amp; Confidential</strong></span></h3><p>&nbsp;<strong>Ref: SPL/HR/Prom/489/16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p><p><strong>16 November, 2016</strong></p><p><strong>&nbsp;</strong><strong>" + name + ", ID: 278</strong></p><p>" + Desig + ",</p><p>" + depart + "</p><p>&nbsp;<strong>Subject: Promotion</strong></p><p>&nbsp;Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;We are pleased to inform you that, the company have decided to promote you to the position of <strong><u>Junior Territory Sales Manager</u></strong> recognition of your performance, effective December 1, 2016.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 360px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 360px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 360px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 360px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 360px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>We acknowledge your excellent performance and congratulate you on your well-deserved promotion. We hope you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;<strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p><strong><u>Copy to:</u></strong></p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRIS</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal File</p>";
                ////    break;
                //////increment letter
                ////case "10008":
                ////    lbody = "<p style='text-align: center;'>&nbsp;</p><p style='padding-left: 360px;'>&nbsp;</p><h3 style='text-align: center;'><span style='text-decoration: underline;'><strong>&nbsp;</strong><strong>Private &amp; Confidential</strong></span></h3><p>&nbsp;</p><p><strong>REF: SPL/HR/INCREMENT/16</strong></p><p><strong>Date: July 12, 2016</strong></p><p>&nbsp;<strong>" + name + ", ID: 101</strong></p><p><strong>" + Desig + ",</strong></p><p>" + depart + "</p><p><strong>Factory.</strong></p><p>&nbsp;</p><p><strong>Subject: Increment of Salary</strong></p><p>&nbsp;</p><p><strong>Dear " + name + ",</strong></p><p>&nbsp;We are pleased to inform you that the management has decided to review your monthly gross salary in recognition of your performance during the year 2015-2016, effective from <strong>July 01, 2016</strong>.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 330px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 330px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 330px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 330px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 330px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>&nbsp;We acknowledge your good performance and hope that you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;We wish you all the best and look forward to better performance in future.</p><p>&nbsp;</p><p>With best regards</p>";//<p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p>Managing Director</p><p>&nbsp;CC: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Personal File</p><p>HRIS</p><p>&nbsp;</p>";
                ////    break;
                //////transfer lettre
                ////case "10009":
                ////    lbody = "<p>&nbsp;<strong>Ref: SPL/HR/TL/558/16</strong></p><p><strong>December 10, 2016</strong></p><p><strong>Mr.</strong> <strong>" + name + "</strong> <strong>(ID # 202)</strong></p><p><strong>TSM,</strong><strong>Dhaka South</strong></p><p>&nbsp;</p><p>&nbsp;Subject: <strong><u>Transfer of Service </u></strong></p><p><br /> <strong>Dear Mr. </strong><strong>" + name + "</strong>,</p><p>&nbsp;In consideration of the exigencies of the Company, it has been decided to transfer you from <strong>Dhaka South (Munshigonj) to Faridpur (Barishal) </strong>effective from <strong>20 Dec, 2016</strong>.</p><p>&nbsp;Please note that the terms and conditions of your service shall not be changed due to this transfer.</p><p>&nbsp;It is expected that you will continue to provide your best services in achieving business goals and objectives of the Company in the days to come.</p><p>&nbsp;Wish you a happy career in Star Paradise Limited.</p><p>&nbsp;</p><p>Yours sincerely<br /> &nbsp;</p>"; //<p>&nbsp;_______________________</p><p><strong>Ridwan Rouf Khan</strong></p><p>Assistant Manager-Human Resources</p><p>&nbsp;&nbsp; C.C</p><ul><li>HRIS</li><li>Personal File</li></ul><p>&nbsp;</p>";
                ////    break;
                //////acceptance of resignation
                ////case "10010":
                ////    lbody = "<p>July 13, 2016</p><p>&nbsp;<strong>" + name + ", ID: 106</strong></p><p>CMO,</p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject:<strong><u> Acceptance of Resignation</u></strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>This with reference to your letter dated <strong>July 1, 2016</strong> in which you have expressed your inability to continue your service with the organization. We would like to inform you that the management has accepted your resignation with effect from <strong>July 11, 2016.</strong></p><p>Accordingly, you will be released from your work at the close of business of <strong>July 10, 2016</strong> subject to a clearance certificate being issued to you by the concerned departments to the effect that you do not owe to <strong>Fidelity Holdings Ltd</strong> any outstanding dues and or any liabilities thereof.</p><p>You are, also, requested to submit the Identity Card and others official things to HR Department to facilitate your quick clearance from the service.</p><p>We take this opportunity to wish you well and success in all your future endeavors.</p><p>&nbsp;</p><p>&nbsp;</p><p>Sincerely,</p>"; //<p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p>&nbsp;Managing Director</p><p>&nbsp;</p><p>Copy:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; MD</p><p>HRIS</p><p>Personal File</p>";
                ////    break;
                //////release letter
                ////case "10011":
                ////    lbody = "<p><strong>June 04, 2015</strong></p><p><strong>" + name + "</strong></p><p>" + Desig + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Accounts&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>" + comnam + "</p><p><strong>&nbsp;</strong></p><p>&nbsp;</p><h3 style=" + "text-align: center;" + "><span style=" + "text-decoration: underline;" + "><strong>RELEASE LETTER</strong></span></h3><p>&nbsp;</p><p>&nbsp;<strong>Dear Mr. </strong><strong>" + name + "</strong><strong>,</strong></p><p><strong>&nbsp;</strong>With reference to, your letter dated <strong>March 05, 2015;</strong> you are hereby released from the service of <strong>" + comnam + "</strong> as at close of business on <strong>April 30, 2015</strong>.</p><p>&nbsp;We wish you all success in life.</p><p>&nbsp;</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;</p><p><strong>Ridwan Rouf Khan</strong></p><p><strong>Assistant Manager-Human Resources.</strong></p>";
                ////    break;


                //////experence certificate for current employee
                ////case "10012":
                ////    lbody = "<div class='printHeader'> <p style='text-align:right'> <br /><br /><br /><br /><br /><br /><br />  Date: " + cdate + "</p><p class='cfHead'>To Whom it May Concern</p><p></br></br></br><br></br></br></br></p><p> This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " </strong> -  <strong>" + dptdesc + ", " + " <strong> Employee ID No. " + idCard + " </strong> working in " + comnam + " from <strong>" + jdate + " to till date</strong>.</p><p>The organization has never found any serious misconduct from his end and has no objection recommending him.</p><p>&nbsp;</p><p>I wish him all round success.</p><p>&nbsp;</p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                ////    break;
                ///////Salary certificate
                ////case "10015":
                ////    lbody = "<div class='printHeader'> <p style='text-align:right'><br /><br /><br /><br /><br /><br /><br />Date: " + cdate + "</p><p class='cfHead'>To Whom it May Concern</p><p></br></br></br><br></br></br></br></p><p>This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " </strong> -  <strong>" + dptdesc + ", " + "</strong> has been working as a regular employee in our company since  <strong>" + jdate + "</strong>.</p><p class='pMargin'><strong>Breakdown of his monthly salary stands as follow:</strong></p><div style='width:400px; margin:0 auto;'>" + tablesale + "</div><p>&nbsp;</p><p><strong>" + inwords + "</strong></p><p>&nbsp;</p> <p class='pFottext'>I hereby certify that the above mentioned information is correct and accurate to the best of my knowledge it also noticeable that company will not be responsible for any sort of loan or personal transection of the above mentioned employee </p>  <br><br><hr width='20%'><p class=''><strong><u><b>HR & Administrator</b></u></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                ////    break;



                default:
                    break;
            }
            return lbody;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {



            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string user = hst["usrid"].ToString();
            // string cat1 = "";
            // string type1 = this.Request.QueryString["Type"].ToString().Trim();
            // if (type1 == "10003" || type1 == "10004" || type1 == "10005" || type1 == "10020")
            // {
            //     cat1 = this.ddlCat.SelectedValue;
            // }
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            // var empid = this.ddlEmployee.SelectedValue.ToString();
            // string TxtRef = this.TxtRef.Text;
            //var strval = this.txtml.Text;

            // var type = this.Request.QueryString["Type"].ToString().Trim();
            // var date = this.txttodate.Text;
            // string comcod = this.GetCompCode();
            // DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPLOADLETTER", empid, type, strval, date, cat1, user, TxtRef, "", "");
            // if (ds3.Tables[0].Rows.Count > 0 || ds3 != null)
            // {
            //     ((Label)this.Master.FindControl("lblmsg")).Text = "Save Successfully";

            // }
            // else
            // {
            //     ((Label)this.Master.FindControl("lblmsg")).Text = "Fail";
            // }


            // if (type1 == "10020")
            // {
            //     ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Send Data Bank";
            //     ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
            //     //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false; 
            // }



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

        protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string type1 = this.Request.QueryString["Type"].ToString();
            if (type1 == "9402" || type1 == "9403")
            {
                this.LoadRefNo();
                this.GetSelected();
                //this.ddlCat.Visible = true;
                //this.lblcat.Visible = true;
                //this.txtml.Visible = false;
            }

            else
            {
                //this.GetEmployee();
                //this.ddlCat.Visible = false;
                //this.lblcat.Visible = false;
            }
        }
    }
}