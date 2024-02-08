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

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpJobCard : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                GetJobLocation();
                GetWorkStation();
                GetAllOrganogramList();

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE JOB CARD";

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent eventPFL-000020660
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetJobLocation()
        {
            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
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
            Session["hrcompnameadd"] = lst;
            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {
            try
            {
                string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
                string comcod = GetComCode();
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
            catch (Exception ex)
            {

            }
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

            this.listProject.DataTextField = "actdesc";
            this.listProject.DataValueField = "actcode";
            this.listProject.DataSource = lst1;
            this.listProject.DataBind();
            this.listProject.SelectedValue = "000000000000";

        }



        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
            this.imgbtnEmpName_Click(null, null);

        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void imgbtnEmpName_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void BtnChckResign_CheckedChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        private void GetEmpName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComCode();
            string company = ((ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptid = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (listProject.SelectedValue.ToString() == "000000000000") ? "%" : listProject.SelectedValue.ToString() + "%";
            string txtSEmployee = "%%";
            string resignstatus = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
            string frmdate = this.txtfromdate.Text;
            string todate = this.txttodate.Text;
            string jobloccode = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAMEJOBCARD", company, deptid, txtSEmployee, section, division,
                resignstatus, frmdate, todate, userid, jobloccode);
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();

            Session["tblemp"] = ds3.Tables[0];
        }


        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.settEmpInfo.Visible = true;
            this.GetJobCard();
            this.ShowImage();

        }

        private void GetJobCard()
        {
            try
            {
                string comcod = this.GetComCode();
                string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string joblocation = this.ddlJobLocation.SelectedValue.ToString();
                string empIdMulti = "";
                foreach (ListItem items in ddlEmpName.Items)
                {
                    if (items.Selected)
                    {
                        empIdMulti += items.Value;
                    }
                }
                string type = "";
                string qtype = this.Request.QueryString["Type"].ToString();
                switch (comcod)
                {
                    case "5301":
                    case "5305":
                    case "5306":
                        type = "EMPATTNIDWISEAUDIT2HRSOT";
                        break;

                    default:
                        type = this.Request.QueryString["Type"].ToString() == "Card2Hrs" ? "EMPATTNIDWISEAUDIT" :
                               this.Request.QueryString["Type"].ToString() == "Card3Hrs" ? "EMPATTNIDWISEAUDIT" :
                               this.Request.QueryString["Type"].ToString() == "CardAllHrs" ? "EMPATTNIDWISEAUDIT" :
                               this.Request.QueryString["Type"].ToString() == "CardReal" ? "EMPATTNIDWISEAUDIT" : "";
                        break;
                }

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", type, frmdate, todate, empIdMulti, qtype, "", "", "", "", "");
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }

                this.lblcompname.Text = (ds1.Tables[2].Rows.Count > 0 ? ds1.Tables[2].Rows[0]["companyname"].ToString() : "");
                this.lblname.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["empnam"].ToString() : "");
                this.lbldpt.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["empdept"].ToString() : "");
                this.lbldesg.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["empdsg"].ToString() : "");
                this.lblcard.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["idcardno"].ToString() : "");
                this.lblIntime.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimein"]).ToString("hh:mm tt") : "");
                this.lblout.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimeout"]).ToString("hh:mm tt") : "");
                this.lblwork.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["twrkday"]).ToString("#, ##0;(#, ##0);") : "");
                this.lblLate.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["tlday"]).ToString("#, ##0;(#, ##0);") : "");
                this.lblLeave.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["tlvday"]).ToString("#, ##0;(#, ##0);") : "");
                this.lblAbsent.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["tabsday"]).ToString("#, ##0;(#, ##0);") : "");
                this.lblHoliday.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["thday"]).ToString("#, ##0;(#, ##0);") : "");
                this.lblOvtime.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["ttot"]).ToString("#, ##0.00;(#, ##0.00);") : "");

                Session["tblempdatewise"] = ds1.Tables[0];
                Session["tbljob01"] = ds1.Tables[1];
                Session["tbljob02"] = ds1.Tables[2];

                this.RptMyAttenView.DataSource = ds1;
                this.RptMyAttenView.DataBind();
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }
        private void ShowImage()
        {
            string idcard = "";
            string comcod = this.GetComCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataTable dt1 = (DataTable)Session["tblemp"];
            if (dt1.Rows.Count > 0)
                idcard = dt1.Select("empid='" + empid + "'")[0]["idcardno"].ToString();
            
            string empImg = "";
            switch (comcod)
            {
                case "5305"://FB
                    empImg = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                    break;

                case "5306"://Footbed
                    empImg = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                    break;

                default:
                    empImg = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                    break;
            }

            this.EmpImg.ImageUrl = empImg;

            #region Image Retrieve from DB
            //string comcod = this.GetComCode();
            //ProcessAccess HRData = new ProcessAccess();
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //Session.Remove("tblEmpimg");

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "SHOWIMG", empid, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
            //    return;
            //}

            //Session["tblEmpimg"] = ds1.Tables[0];
            ////this.EmpImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgEmp";
            //byte[] image = (byte[])ds1.Tables[0].Rows[0]["empimage"];

            //this.EmpImg.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(image);
            #endregion

        }
        protected void RptMyAttenView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {


                string ahleave = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "leav")).ToString();
                string inoroutpunch = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "inoroutpunch")).ToString();
                Label lblacintime = ((Label)e.Item.FindControl("lblacintime"));
                DateTime offimein = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimein"));
                DateTime offouttim = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimeout"));


                DateTime actualin = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualin"));
                DateTime actualout = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualout"));

                //Intime or out time
                if (inoroutpunch == "In Punch")
                {
                    ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                }
                else if (inoroutpunch == "Out Punch")
                {
                    ((Label)e.Item.FindControl("lblactualin")).Visible = false;

                }

                if (ahleave == "AB")

                {
                    lblacintime.Attributes["style"] = "background-color:red; color:white;";

                }
                else if (ahleave == "WH" || ahleave == "FH" || ahleave=="SPH" || ahleave=="ADH")
                {
                    lblacintime.Attributes["style"] = "background-color:yellow;";
                }


                else if (ahleave == "El" || ahleave == "CL" || ahleave == "SL" || ahleave == "ML" || ahleave == "LWP" || ahleave == "LFT" || ahleave == "PL" || ahleave == "HL" || ahleave == "AL" || ahleave == "L")
                {
                    lblacintime.Attributes["style"] = "background-color:blue; color:white;";
                }



                if (ahleave == "AB" || ahleave == "FH" || ahleave == "SPH" || ahleave == "ADH" || ahleave == "L")
                {
                    ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                    ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                    ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";

                }

            }



            if (e.Item.ItemType == ListItemType.Footer)
            {

            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            switch (comcod)
            {
                case "5305": //Fb footwear              
                case "5306": //Fb footwear              
                    this.PrintJobCardFB();
                    break;
                case "5301": // Edison
                    this.PrintJobCardGen();
                    break;
                default:
                    this.PrintJobCardGen();
                    break;
            }
        }

        private void PrintJobCardFB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string compName = hst["comnam"].ToString();
            string userid = hst["usrid"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string rptDt = "Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim().ToString()).ToString("MMMM yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string auditComLogo = new Uri(Server.MapPath(@"~\Image\LOGO5306.jpg")).AbsoluteUri;

            //For Multiple Employee
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string empIdMulti = "";
            foreach (ListItem items in ddlEmpName.Items)
            {
                if (items.Selected)
                {
                    empIdMulti += items.Value;
                }
            }

            empIdMulti = empIdMulti == "000000000000" ? "" : empIdMulti;
            string type = "";
            string qtype = this.Request.QueryString["Type"].ToString();
            switch (comcod)
            {
                case "5301":
                case "5305":
                case "5306":
                    type = "EMPATTNIDWISEAUDIT2HRSOTPRINT";
                    break;

                default:
                    type = "EMPATTNIDWISEAUDIT2HRSOTPRINT";
                    break;
            }


            string company = ((ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (listProject.SelectedValue.ToString() == "000000000000") ? "%" : listProject.SelectedValue.ToString() + "%";
            string maxOT = this.txtMaxOTHours.Text.Trim();
            string resignstatus = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS_02", type, frmdate, todate, empIdMulti, qtype, resignstatus, company, division, deptcode, section, maxOT);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard01>();
            string rptType = this.Request.QueryString["Type"].ToString();
            string Dept = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            LocalReport rpt1 = new LocalReport();
            switch (Dept)
            {
                case "9401": //EXECUTIVE EMPLOYEES(FB)
                case "9402": //EXECUTIVE EMPLOYEES(Footbed)
                case "9411": //Supporting Staff- FB(HO)
                case "9412": //Supporting Staff- Foodbed(HO)
                case "9413": //FACTORY STAFF (FB-Non OT Based)
                case "9415": //FACTORY STAFF (Footbed-Non OT Based)

                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptExecutiveJobCardFB", list, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("RptTitle", "JOB CARD"));
                    break;

                case "0000": //(All Company Multy)
                case "9403": //FACTORY WORKER(FB)
                case "9404": //FACTORY WORKER
                case "9405": //FACTORY WORKER
                case "9406": //FACTORY WORKER
                case "9407": //FACTORY WORKER
                case "9408": //FACTORY WORKER(Footbed)
                case "9409": //FACTORY WORKER
                case "9410": //FACTORY WORKER
                case "9416": //FACTORY STAFF (Foodbed-OT Based)
                    switch (rptType)
                    {
                        case "Card2Hrs":
                        case "Card3Hrs":
                        case "CardAllHrs":
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpJobCardFBMulti", list, null, null);
                            rpt1.EnableExternalImages = true;
                            rpt1.SetParameters(new ReportParameter("RptTitle", "JOB CARD"));
                            break;

                        default:
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpJobCardRealFB", list, null, null);
                            rpt1.EnableExternalImages = true;
                            rpt1.SetParameters(new ReportParameter("RptTitle", "TIME CARD"));
                            break;
                    }

                    break;
            }

            rpt1.SetParameters(new ReportParameter("comnam", userid == "5305139" ? "Footbed Footwear Ltd." : compName)); //Audit user
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("ComLogo", userid == "5305139" ? auditComLogo : ComLogo)); //Audit user
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintJobCardGen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            string rptDt = "Date: " + this.txtfromdate.Text.Trim().ToString() + " To " + this.txttodate.Text.Trim().ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblempdatewise"];
            DataTable dt1 = (DataTable)Session["tbljob01"];
            DataTable dt2 = (DataTable)Session["tbljob02"];

            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard01>();
            var list1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard02>();
            var list2 = dt2.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard03>();
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpJobCardAudit", list, list1, list2);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Job Card Report"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("empjdate", dt.Rows[0]["joindate"].ToString()));
            rpt1.SetParameters(new ReportParameter("empid", dt.Rows[0]["idcardno"].ToString()));
            rpt1.SetParameters(new ReportParameter("empnam", dt.Rows[0]["empnam"].ToString()));
            rpt1.SetParameters(new ReportParameter("empdsg", dt.Rows[0]["empdsg"].ToString()));
            rpt1.SetParameters(new ReportParameter("empdept", dt.Rows[0]["empdept"].ToString()));
            rpt1.SetParameters(new ReportParameter("ttpsnt", Convert.ToDouble(dt1.Rows[0]["ttpsnt"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("tlvday", Convert.ToDouble(dt1.Rows[0]["tlvday"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("tabsday", Convert.ToDouble(dt1.Rows[0]["tabsday"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("tlday", Convert.ToDouble(dt1.Rows[0]["tlday"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("ttot", Convert.ToDouble(dt1.Rows[0]["ttot"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("fline", Convert.ToString(dt.Rows[0]["fline"]).ToString()));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        //protected void chkMaxOT_CheckedChanged(object sender, EventArgs e)
        //{
        //    string comcod = this.GetComCode();
        //    string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");           
        //    string empIdMulti = "";
        //    string qtype = this.Request.QueryString["Type"].ToString();           
        //    string company = ((ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
        //    string division = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
        //    string deptcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
        //    string section = (listProject.SelectedValue.ToString() == "000000000000") ? "%" : listProject.SelectedValue.ToString() + "%";
        //    string maxOT = this.chkMaxOT.Checked ? "maxOT" : "";
        //    string resignstatus = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
        //    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS_02", "EMPATTNIDWISEAUDIT2HRSOTPRINT", frmdate, todate, empIdMulti, qtype, resignstatus, company, division, deptcode, section, maxOT);
        //    if (ds1 == null)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
        //        return;
        //    }

        //    ViewState["tblempjobcard"] = ds1.Tables[0];

        //    //Employee Max OT Wise
        //    this.ddlEmpName.DataTextField = "empname";
        //    this.ddlEmpName.DataValueField = "empid";
        //    this.ddlEmpName.DataSource = ds1.Tables[1];
        //    this.ddlEmpName.DataBind();
        //}
    }
}