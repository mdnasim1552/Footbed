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
using System.Net.Mail;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.IO;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class AllEmpIDCard : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                Session.Remove("tblEmpstatus");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.chkwithoutdate.Checked = true;
                this.GetWorkStation();
                this.GetAllOrganogramList();

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = "01" + this.txtDate.Text.Trim().Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(this.txtDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


                ((Label)this.Master.FindControl("lblTitle")).Text = "All Employe Details";


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            ViewState["lstWstation"] = lst;
        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
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
            this.ddlDivision.SelectedValue = "000000000000";
            this.ddlDivision.DataBind();

            this.ddlDivision_SelectedIndexChanged(null, null);

        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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
            this.ddlDept.SelectedValue = "000000000000";
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);

        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
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
            //lst1.Add()

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();

            this.ddlSection.SelectedValue = "000000000000";
            ddlSection_SelectedIndexChanged(null, null);
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetLine();



        }


        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.getemp();
        }

        private void GetLine()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlLine.DataTextField = "hrgdesc";
            this.ddlLine.DataValueField = "hrgcod";
            this.ddlLine.DataSource = ds3;
            this.ddlLine.DataBind();
            this.ddlLine.SelectedValue = "00000";
            ViewState["tbllineddl"] = ds3.Tables[0];
            this.ddlLine_SelectedIndexChanged(null, null);
        }



        private void LoadData()
        {
            DataTable dt2 = (DataTable)Session["tblEmpstatus"];
            ListViewEmpAll.DataSource = dt2;
            ListViewEmpAll.DataBind();
        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem listViewDataItem = e.Item as ListViewDataItem;
                HtmlGenericControl divControl = e.Item.FindControl("EmpAll") as HtmlGenericControl;
                DataRowView dataRow = ((DataRowView)listViewDataItem.DataItem);
                divControl.Attributes.Add("ID", dataRow["idcardno"].ToString());
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            switch (GetCompCode())
            {
                case "5305":
                case "5306":
                    this.IdCardPrintFB();
                    break;

                default:
                    this.GetEmpList();
                    this.IdCardPrint();
                    break;
            }
        }
        public string GetBanglaNumber(int number)
        {
            return string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
        }
        protected void IdCardPrintFB()
        {
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string userid = hst["usrid"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string comaddf = hst["comaddf"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string auditComLogo = new Uri(Server.MapPath(@"~\Image\LOGO5306.jpg")).AbsoluteUri;
            string ComLogoText = new Uri(Server.MapPath(@"~\Image\LOGOTEXT" + comcod + ".png")).AbsoluteUri;
            string gmSign = new Uri(Server.MapPath(@"~\Image\GMSign.png")).AbsoluteUri;

            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string line = (this.ddlLine.SelectedValue.ToString() == "00000" ? "" : this.ddlLine.SelectedValue.ToString()) + "%";
            string from = this.txtDate.Text.ToString();
            string to = this.txttoDate.Text.ToString();          

            string empid = "";
            string[] sempid = this.ddlPEmpName.Text.Trim().Split(',');
            if (sempid[0].Substring(0, 3) == "000")
                empid = "";
            else
                foreach (ListItem item in ddlPEmpName.Items)
                {
                    if (item.Selected)
                    {
                        empid += item.Value;
                    }
                }

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTALLEMPLISTFACTORYCOMMON", emptype, div, empid, deptname, section, from, to, line, "");
            string date = "";

            var lst1 = ds4.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfoBangla>();
            if (lst1.Count == 0)
                return;

            //Image from folder with path
            foreach (var item in lst1)
            {
                string idcard = item.idcardno.ToString();
                string empImg = "";
                string empSign = "";
                switch (comcod)
                {
                    case "5305"://FB
                        empImg = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                        empSign = "~/Upload/HRM/EmpSignFB/" + idcard + ".jpg";
                        break;

                    case "5306"://Footbed
                        empImg = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                        empSign = "~/Upload/HRM/EmpSignFBF/" + idcard + ".jpg";
                        break;

                    default:
                        empImg = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                        empSign = "~/Upload/HRM/EmpSign/" + idcard + ".jpg";
                        break;
                }
                FileInfo ImgFile = new FileInfo(Server.MapPath(empImg));
                FileInfo SignFile = new FileInfo(Server.MapPath(empSign));
                if (ImgFile.Exists)
                {

                    switch (comcod)
                    {
                        case "5305"://FB
                            item.empimage1 = new Uri(Server.MapPath(@"~\Upload\HRM\EmpImgFB\" + idcard + ".jpg")).AbsoluteUri;
                            break;

                        case "5306"://Footbed
                            item.empimage1 = new Uri(Server.MapPath(@"~\Upload\HRM\EmpImgFBF\" + idcard + ".jpg")).AbsoluteUri;
                            break;

                        default:
                            item.empimage1 = new Uri(Server.MapPath(@"~\Upload\HRM\EmpImgFB\" + idcard + ".jpg")).AbsoluteUri;
                            break;
                    }
                }
                if (SignFile.Exists)
                {
                    switch (comcod)
                    {
                        case "5305"://FB
                            item.empsign1 = new Uri(Server.MapPath(@"~\Upload\HRM\EmpSignFB\" + idcard + ".jpg")).AbsoluteUri;
                            break;

                        case "5306"://Footbed
                            item.empsign1 = new Uri(Server.MapPath(@"~\Upload\HRM\EmpSignFBF\" + idcard + ".jpg")).AbsoluteUri;
                            break;

                        default:
                            item.empsign1 = new Uri(Server.MapPath(@"~\Upload\HRM\EmpSign\" + idcard + ".jpg")).AbsoluteUri;
                            break;
                    }
                }
            }

            string laimg = lst1[0].empsign.ToString();
            string Auimg = lst1[0].mangerempsign.ToString();

            LocalReport rpt1 = new LocalReport();
            int formindex = this.DdlIdFormat.SelectedIndex;
            switch (formindex)
            {
                //Executive
                case 0:
                    switch (comcod)
                    {
                        case "5305":
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardExeFB", lst1, null, null);
                            rpt1.EnableExternalImages = true;
                            break;

                        default:
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardExeFootbed", lst1, null, null);
                            rpt1.EnableExternalImages = true;
                            break;
                    }

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("comaddf", comaddf));
                    rpt1.SetParameters(new ReportParameter("ComLogoText", ComLogoText));
                    break;

                //Staff
                case 1:
                    date = GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(DateTime.Today).ToString("dd"))) + "-" + ASITUtility02.GetMonthName((Convert.ToDateTime(DateTime.Today).ToString("MMM"))) + "-" + 
                        GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(DateTime.Today).ToString("yyyy")));
                    switch (comcod)
                    {
                        case "5305":
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardStafFB", lst1, null, null);
                            rpt1.EnableExternalImages = true;
                            break;

                        default:
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardStafFootbed", lst1, null, null);
                            rpt1.EnableExternalImages = true;
                            break;
                    }

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("comaddf", comaddf));
                    rpt1.SetParameters(new ReportParameter("gmSign", gmSign));
                    rpt1.SetParameters(new ReportParameter("ComLogoText", ComLogoText));
                    break;

                //Worker
                case 2:                   
                    date = GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(DateTime.Today).ToString("dd"))) + "-" + ASITUtility02.GetMonthName((Convert.ToDateTime(DateTime.Today).ToString("MMM"))) + "-" +
                         GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(DateTime.Today).ToString("yyyy")));
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardBanglaFB", lst1, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("comnam", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
                    rpt1.SetParameters(new ReportParameter("comadd", comaddbn));
                    rpt1.SetParameters(new ReportParameter("gmSign", gmSign));
                    rpt1.SetParameters(new ReportParameter("telephoneNo", comcod == "5305" ? "০১৬১৬৩৪০০৫০" : "০১৬১৬৩৩৯৯১৮"));

                    //Previous Individual Report Comment 2022-09-14
                    //else if(comcod=="5306")
                    //{
                    //    comadd = "উলুসারা,কালিয়াকৈর,গাজীপুর।";
                    //    date = GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(DateTime.Today).ToString("dd"))) + "-" + GetMonthName((Convert.ToDateTime(DateTime.Today).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(DateTime.Today).ToString("yyyy")));
                    //    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardBanglaFootbed", lst1, null, null);
                    //    rpt1.EnableExternalImages = true;
                    //    rpt1.SetParameters(new ReportParameter("comnam", comnambn));
                    //    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    //    rpt1.SetParameters(new ReportParameter("gmSign", gmSign));
                    //}                         
                    break;

            }

            string laimg1 = new Uri(Server.MapPath(laimg)).AbsoluteUri;
            string Auimg1 = new Uri(Server.MapPath(Auimg)).AbsoluteUri;
            rpt1.SetParameters(new ReportParameter("Logo", userid == "5305139" ? auditComLogo : ComLogo)); //Audit user
            rpt1.SetParameters(new ReportParameter("issuedat", date));
            rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("laimg1", laimg1));
            rpt1.SetParameters(new ReportParameter("Auimg1", Auimg1));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void IdCardPrint()
        {
            //IQBAL NAYAN
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //var lst = (List<MFGOBJ.C_22_Sal.BO_TopOrder.TopOrderList>)ViewState["tblgltransection"];
            string company = (this.ddlWstation.SelectedValue.Substring(0, 4).ToString() == "0000") ? "%" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString() + "%";

            DataTable dt1 = (DataTable)Session["tblEmpstatus"];






            var lst1 = new List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfoBangla>();
            var lst = new List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfo>();

            switch (company)
            {
                case "9403%":
                    lst1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfoBangla>();
                    break;
                default:
                    lst = dt1.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfo>();
                    break;
            }

            string laimg = "";
            string Auimg = "";

            switch (company)
            {
                case "9403%":
                    laimg = lst1[0].empsign.ToString();
                    Auimg = lst1[0].mangerempsign.ToString();
                    break;
                default:
                    laimg = lst[0].empsign.ToString();
                    Auimg = lst[0].mangerempsign.ToString();
                    break;
            }

            LocalReport rpt1 = new LocalReport();

            switch (company)
            {
                case "9401%":
                case "9402%":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardStaff", lst, lst, null);
                    break;
                case "9403%":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCardBangla", lst1, lst1, null);
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpIDCard", lst, lst, null);
                    break;
            }

            rpt1.EnableExternalImages = true;
            string laimg1 = new Uri(Server.MapPath(laimg)).AbsoluteUri;
            string Auimg1 = new Uri(Server.MapPath(Auimg)).AbsoluteUri;
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("comnam", comnambn));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("laimg1", laimg1));
            rpt1.SetParameters(new ReportParameter("Auimg1", Auimg1));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void getemp()
        {

            string comcod = this.GetCompCode();
            string company = (this.ddlWstation.SelectedValue.Substring(0, 4).ToString() == "0000") ? "%" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString() + "%";
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7).ToString() + "%";
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9).ToString() + "%";
            string sectioncode = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string line = ((this.ddlLine.SelectedValue.ToString() == "00000") ? "70" : this.ddlLine.SelectedValue.ToString()) + "%";
            string txtEmpname = "%";
            string from = this.txtDate.Text.ToString();
            string to = this.txttoDate.Text.ToString();
            string chkwithouddat = this.chkwithoutdate.Checked ? "without" : "";



            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLISTIDCARD", sectioncode, txtEmpname, company, div, deptid, line, from, to, chkwithouddat);
            this.ddlPEmpName.DataTextField = "empname";
            this.ddlPEmpName.DataValueField = "empid";
            this.ddlPEmpName.DataSource = ds1.Tables[0];
            this.ddlPEmpName.DataBind();



        }

        private void GetEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString();
            string empid = (this.ddlPEmpName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlPEmpName.SelectedValue.ToString() + "%";
            string from = this.txtDate.Text.ToString();
            string to = this.txttoDate.Text.ToString();
            string line = ((this.ddlLine.SelectedValue.ToString() == "00000") ? "70" : this.ddlLine.SelectedValue.ToString()) + "%";

            //string empid = this.ddlPEmpName.SelectedValue.ToString() + "%";
            //string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            //string DesigTo = this.ddlToDesig.SelectedValue.ToString();

            DataSet ds4;

            switch (emptype)
            {
                case "9403%":
                    ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTALLEMPLISTFACTORYBANGAL", emptype, div, empid, deptname, section, from, to, line, "");
                    if (ds4 == null)
                    {
                        return;
                    }
                    break;
                default:
                    ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTALLEMPLISTWITHPIC", emptype, div, empid, deptname, section, from, to, line, "");
                    if (ds4 == null)
                    {
                        return;
                    }
                    break;
            }

            Session["tblEmpstatus"] = (ds4.Tables[0]);
            // this.LoadData();


        }

        protected void ibtnFindEmp_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.getemp();
        }
        //protected void ddlPEmpName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetEmpList();
        //}


    }
}