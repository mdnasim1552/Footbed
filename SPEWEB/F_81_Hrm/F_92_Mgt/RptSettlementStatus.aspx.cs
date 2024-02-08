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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.Text.RegularExpressions;
using SPELIB;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RptSettlementStatus : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess feaData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Settlement Top Sheet";

                //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).ToolTip = "New Employee Settlement";
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtdateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                GetWorkStation();
                GetAllOrganogramList();
                this.GetJobLocation();
                this.lnkbtnSerOk_Click(null, null);

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkBtnAdd_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("href", "../../F_81_Hrm/F_92_Mgt/EmpSettlement?Type=Entry&actcode=");
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("target", "_blank");

        }



        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }
        private void ShowValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComCode();
            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = this.txtdateto.Text.ToString();
            string empType = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string jobLocation = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string callType = this.GetCallType();
            DataSet ds2 = feaData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", callType, null, null, null, fdate, tdate, empType, div, Dept, section, jobLocation, userid, "");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvSettInfo.DataSource = null;
                this.gvSettInfo.DataBind();
                return;
            }

            ViewState["tblSetTopInfo"] = ds2.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>();

            this.Data_Bind();

        }
        private string GetCallType()
        {
            string comcod = this.GetComCode();
            string callType = "";
            switch (comcod)
            {
                case "5305"://FB Footwear
                case "5306"://Footbed Footwear
                    callType ="SHOW_SEPERATED_EMP_FB";
                    break;

                default:
                    callType="SHOW_SEPERATED_EMP";
                    break;
            }
            return callType;
        }
        private void Data_Bind()
        {

            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["tblSetTopInfo"];

            this.gvSettInfo.DataSource = sttlmntinfo;
            this.gvSettInfo.DataBind();

            this.FooterCal();
        }

        private void FooterCal()
        {
            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["tblSetTopInfo"];

            if(sttlmntinfo.Count>0)
            ((Label)this.gvSettInfo.FooterRow.FindControl("lblgvFttlamt")).Text = sttlmntinfo.Sum(p => p.ttlamt).ToString("#,##0.00;(#,##0.00); ");

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetComCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fdate = this.txtDatefrom.Text.ToString();
            //string tdate = this.txtdateto.Text.ToString();
            //string ToFrDate = "(From :" + fdate + " To " + tdate + ")";
            //DataTable dt = (DataTable)ViewState["tblSetTopInfo"];

            //var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.OrderStatus>();

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptOrderStatus", lst, null, null);
            //rpt1.EnableExternalImages = true;

            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("ToFrDate", ToFrDate));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "Order Status"));
            //rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            ////rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            ////rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void gvSettInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                LinkButton lnkPrint = (LinkButton)e.Row.FindControl("HypRDDoPrint");

                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).Trim().ToString();
                string apstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aprvstatus"));


                if (apstatus == "False")
                {
                    lnkEdit.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/EmpSettlement?Type=Entry&actcode=" + empid;
                    lnkPrint.Enabled = false;
                }
                else
                {
                    lnkEdit.Text = "<i class='fa fa-lock' aria-hidden='true'></i>";
                    lnkEdit.CssClass = "btn btn-xs btn-danger";
                    lnkEdit.ToolTip = "Approved";
                    lnkPrint.Enabled = true;
                }

                //lnkELink.NavigateUrl = "~/F_01_Mer/MerPRCodeBook?BookName=Project";


            }

        }




        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void HypRDDoPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empId = ((Label)this.gvSettInfo.Rows[index].FindControl("lblgvempid")).Text.ToString();
            switch (comcod)
            {
                case "5305"://FB Footwear
                case "5306"://FB Footwear
                    this.PrintEmpSettFB(empId);
                    break;

                default:
                    this.PrintEmpSettGen(empId);
                    break;
            }
        }
        private void PrintEmpSettFB(string sEmpId)
        {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string approvStatus = "True";
            string txtDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_SEPERATED_EMP", approvStatus, "", "", "", "", "", "", "");
            if (ds2 == null || ds2.Tables[0].Rows.Count ==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + "Please Approve First!" + "');", true);
                return;
            }
            var emplist1 = ds2.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>();
            var emplist = emplist1.FindAll(p => p.empid==sEmpId);
            var deptcode = emplist[0].deptcode.Substring(0, 4);
            DataSet ds3 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_EMP_SETTLEMENT_INFO_FB", sEmpId, "0", "", "", "", "", "", "");
            var list = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();

            var list1 = list.FindAll(p => p.hrgcod.Substring(0, 3) == "351");//Salary Info
            var list2 = list.FindAll(p => p.hrgcod.Substring(0, 3) == "352");//Benefits & Dues
            var list3 = list.FindAll(p => p.hrgcod.Substring(0, 3) == "353");//Deduction                    

            string empName = emplist[0].empname.ToString();
            string noticeduration = emplist[0].noticeduration.ToString();
            string empId = emplist[0].idno.ToString();
            string empDesig = emplist[0].designation.ToString();
            string empDept = emplist[0].deptname.ToString();
            string empSection = emplist[0].section.ToString();
            string joinDate = emplist[0].joindat.ToString("dd-MMM-yyyy");
            string sepDate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            string effDate = emplist[0].effectdate.ToString("dd-MMM-yyyy");
            string serLength = emplist[0].servleng.ToString();
            string daysConMonth = emplist[0].daysconmonth.ToString();
            var gssal = (list1.FindAll(s => s.hrgcod == "35101").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var bssal = (list1.FindAll(s => s.hrgcod == "35102").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var onedaygssal = (list1.FindAll(s => s.hrgcod == "35105").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var onedaybssal = (list1.FindAll(s => s.hrgcod == "35106").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            double netAmount = (list2.Sum(p => p.ttlamt) - list3.Sum(p => p.ttlamt));
            double netpay = (netAmount < 0) ? netAmount * -1 : netAmount;

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattelmentFB", list1, list2, list3);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "FINAL SETTLEMENT BILL"));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("gssal", gssal));
            Rpt1.SetParameters(new ReportParameter("bssal", bssal));
            Rpt1.SetParameters(new ReportParameter("onedaygssal", onedaygssal));
            Rpt1.SetParameters(new ReportParameter("onedaybssal", onedaybssal));
            Rpt1.SetParameters(new ReportParameter("netAmount", netAmount.ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("tkInWords", ASTUtility.Trans(netpay, 2)));
            Rpt1.SetParameters(new ReportParameter("empName", empName));
            Rpt1.SetParameters(new ReportParameter("empId", empId));
            Rpt1.SetParameters(new ReportParameter("empDesig", empDesig));
            Rpt1.SetParameters(new ReportParameter("empDept", empDept));
            Rpt1.SetParameters(new ReportParameter("empSection", empSection));
            Rpt1.SetParameters(new ReportParameter("joinDate", joinDate));
            Rpt1.SetParameters(new ReportParameter("sepDate", sepDate));
            Rpt1.SetParameters(new ReportParameter("effDate", effDate));
            Rpt1.SetParameters(new ReportParameter("serLength", serLength));
            Rpt1.SetParameters(new ReportParameter("daysConMonth", daysConMonth));
            Rpt1.SetParameters(new ReportParameter("noticeduration", noticeduration));

            Session["Report1"] = Rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        private void PrintEmpSettGen(string empId)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            var emplistall = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["tblSetTopInfo"];
            var emplist = emplistall.FindAll(p => p.empid == empId);
            var deptcode = emplist[0].deptcode.Substring(0, 4);
            string comcod = this.GetComCode();
            DataSet ds3 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_EMP_SETTLEMENT_INFO", empId, deptcode, "", "", "", "", "", "");
            var lst1 = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            var list1 = lst1.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
            var list2 = lst1.FindAll(p => p.hrgcod.Substring(0, 3) == "352");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string billDate = emplist[0].billdate.ToString("dd-MMM-yyyy");
            billDate = GetMonthName(GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(billDate).ToString("dd"))) + "-" + (Convert.ToDateTime(billDate).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(billDate).ToString("yyyy")));

            string name = emplist[0].empname.ToString();
            string Desgin = emplist[0].designation.ToString();
            string Id = emplist[0].idno.ToString();
            string Section = emplist[0].deptname.ToString();
            string jobseperation = emplist[0].septypedesc.ToString();
            string joining = emplist[0].joindat.ToString("dd-MMM-yyyy");
            joining = GetMonthName(GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(joining).ToString("dd"))) + "-" + (Convert.ToDateTime(joining).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(joining).ToString("yyyy")));
            string sepdate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            sepdate = GetMonthName(GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(sepdate).ToString("dd"))) + "-" + (Convert.ToDateTime(sepdate).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(sepdate).ToString("yyyy")));
            double netamount = Convert.ToDouble("0" + (lst1.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - lst1.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)));
            string servicelength = emplist[0].servleng.ToString();
            double netpay = Convert.ToDouble(netamount);

            string inword = ASTUtility.TransBN(netpay, 2).ToString().Trim().Replace(" মাত্র )", ""); ;

            LocalReport rpt1 = new LocalReport();
            var EmpType = emplist[0].deptcode.Substring(0, 4);
            if (EmpType == "9403" || EmpType == "9402")
            {
                comnam = "এডিসন ফুটওয়্যার লিমিটেড:"; //hst["comnam"].ToString();
                comadd = "তালতলী, মির্জাপুর ,গাজীপুর";
                servicelength = emplist[0].servleng.ToString();
                string[] words = servicelength.Split(' ');
                char[] numbers;
                var i = 0;
                foreach (var item in words)
                {
                    if (item != null)
                    {
                        Regex reg = new Regex("[0-9]");

                        if (!reg.IsMatch(item))
                        {
                            switch (item)
                            {

                                case "month":
                                    words[i] = "মাস "; break;
                                case "days":
                                    words[i] = "দিন "; break;
                                case "Year":
                                    words[i] = "বছর "; break;
                            }
                        }
                        else
                        {
                            numbers = item.ToCharArray();
                            var count = 0;
                            foreach (var n in numbers)
                            {
                                switch (n)
                                {

                                    case '1':
                                        numbers[count] = '১'; break;
                                    case '2':
                                        numbers[count] = '২'; break;
                                    case '3':
                                        numbers[count] = '৩'; break;
                                    case '4':
                                        numbers[count] = '৪'; break;
                                    case '5':
                                        numbers[count] = '৫'; break;
                                    case '6':
                                        numbers[count] = '৬'; break;
                                    case '7':
                                        numbers[count] = '৭'; break;
                                    case '8':
                                        numbers[count] = '৮'; break;
                                    case '9':
                                        numbers[count] = '৯'; break;
                                    case '0':
                                        numbers[count] = '০'; break;
                                }
                                count++;
                            }
                            words[i] = "";
                            foreach (var num in numbers)
                            {
                                words[i] += num.ToString();
                            }

                        }
                    }

                    i++;
                }
                servicelength = "";
                foreach (var item in words)
                {
                    servicelength += item + " ";

                }
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattelmentBangla", list1, list2, null);
            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattelment", list1, list2, null);
            }
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", ""));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("netamount", netamount.ToString("#,##0.00;(#,##0.00); ")));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            rpt1.SetParameters(new ReportParameter("billDate", billDate));
            rpt1.SetParameters(new ReportParameter("name", name.ToString().Trim()));
            rpt1.SetParameters(new ReportParameter("Desgin", Desgin));
            rpt1.SetParameters(new ReportParameter("Id", Id));
            rpt1.SetParameters(new ReportParameter("Section", Section));
            rpt1.SetParameters(new ReportParameter("jobseperation", jobseperation));
            rpt1.SetParameters(new ReportParameter("joining", joining));
            rpt1.SetParameters(new ReportParameter("sepdate", sepdate));
            rpt1.SetParameters(new ReportParameter("servicelength", servicelength));
            rpt1.SetParameters(new ReportParameter("inwords", inword));

            Session["Report1"] = rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        public string GetMonthName(string name)
        {
            return name.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্রুয়ারী").Replace("Mar", "মার্চ").
                Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
                Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").
                Replace("Dec", "ডিসেম্বর");

        }
        public string GetBanglaNumber(int number)
        {
            return string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetComCode();
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
            string comcod = GetComCode();
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

            string comcod = GetComCode();
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
            string comcod = GetComCode();
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
        private void GetJobLocation()
        {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();

        }
    }
}