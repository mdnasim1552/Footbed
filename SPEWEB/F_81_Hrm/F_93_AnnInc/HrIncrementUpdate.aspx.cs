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
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_93_AnnInc
{
    public partial class HrIncrementUpdate : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtdate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE INCREMENT UPDATE";

                GetWorkStation();
                GetAllOrganogramList();
                this.CommonButton();

            }

        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).ToolTip = "Final Update Increment";
            string ws = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            if (ws == "9401")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Update Car/Sub Allow.";

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(LnkButton_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";

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
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";

            this.ddlDept_SelectedIndexChanged(null, null);

        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
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
            this.GetIncrementList();
        }


        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void GetIncrementList()
        {

            string comcod = GetCompCode();
            string mREQDAT = this.GetStdDate(this.txtdate.Text);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENTNO", mREQDAT, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.ddlIncList.Items.Clear();
                return;

            }

            this.ddlIncList.DataTextField = "incrno1";
            this.ddlIncList.DataValueField = "incrno";
            this.ddlIncList.DataSource = ds2.Tables[0];
            this.ddlIncList.DataBind();

        }

        protected void imgbtnIncrementList_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
                this.GetIncrementList();
        }

        protected void LnkButton_Click(object sender, EventArgs e)
        {
            LinkButton lnk = ((LinkButton)this.Master.FindControl("lnkbtnHisprice")) as LinkButton;

            lnk.PostBackUrl = "~/F_81_Hrm/F_93_AnnInc/lnkpagecarsuballow.aspx";

        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();

            string cutdate = this.GetStdDate(this.txtdate.Text);
            string incrno = this.ddlIncList.SelectedValue.ToString();
            string mREQDAT = this.GetStdDate(this.txtdate.Text);
            string workstation = ASTUtility.Left(this.ddlWstation.SelectedValue.ToString(), 4);
            string calltype = (GetCompCode() == "5305" || GetCompCode() == "5306") ? "APPROVE_INC_SALARY" : "UPDATEINCSALARY";
            try
            {
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tblAnnInc"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double grossal = 0;
                    string empid = dt.Rows[i]["empid"].ToString();
                    string empType = dt.Rows[i]["seccode"].ToString();
                    double carsubamt = Math.Round(Convert.ToDouble("0" + dt.Rows[i]["carsubamt"].ToString()), 0);
                    double subamt = Math.Round(Convert.ToDouble("0" + dt.Rows[i]["subamt"].ToString()), 0);
                    //double subamt=Convert.ToDouble( dt.Rows[i]["subamt"].ToString());


                    grossal = Math.Round(Convert.ToDouble(dt.Rows[i]["grossal"]) + Convert.ToDouble(dt.Rows[i]["finincamt"]), 0);

                    if (workstation == "9401")
                    {
                        //   subamt = (subamt + carsubamt) ;
                        grossal = grossal - subamt - carsubamt;

                    }


                    //grossal = Convert.ToDouble(dt.Rows[i]["grossal"].ToString()) + Convert.ToDouble(dt.Rows[i]["finincamt"].ToString());

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", calltype, empid,
                                                    grossal.ToString(), empType, "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "UPDATEEMPINCREMENT", incrno, empid,
                                                  mREQDAT, userid, Terminal, Sessionid, "", "", "", "", "", "", "", "", "", "");
                    }

                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Increment Updated Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.lnkbtnShow.Text = "New";
                this.ddlIncList.Enabled = false;
                this.ShowIncrementInfo();
                return;
            }

            this.lnkbtnShow.Text = "Ok";
            this.GetIncrementList();
            this.ddlIncList.Enabled = true;
            this.gvAnnIncre.DataSource = null;
            this.gvAnnIncre.DataBind();
        }

        private void ShowIncrementInfo()
        {

            string comcod = this.GetCompCode();
            string preincreno = this.ddlIncList.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENT", preincreno, "", "", "", "", "", "", "", "");

            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblAnnInc"] = dt;
            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string seccode = dt1.Rows[0]["seccode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["seccode"].ToString() == seccode)
                {

                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                        dt1.Rows[j]["deptname"] = "";
                    if (dt1.Rows[j]["seccode"].ToString() == seccode)
                        dt1.Rows[j]["section"] = "";
                }

                deptcode = dt1.Rows[j]["deptcode"].ToString();
                seccode = dt1.Rows[j]["seccode"].ToString();

            }
            return dt1;
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            this.gvAnnIncre.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAnnIncre.DataSource = dt;
            this.gvAnnIncre.DataBind();
            this.FooterCal();
        }


        protected void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFpresal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incamt)", "")) ? 0.00 : dt.Compute("sum(incamt)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFfinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finincamt)", "")) ? 0.00 : dt.Compute("sum(finincamt)", ""))).ToString("#,##0;(#,##0);");


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comaddf"].ToString();
            string comnam = hst["comnam"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblAnnInc"];
            string txtDate = GetStdDate(this.txtdate.Text);
            string prevYear = Convert.ToDateTime(txtDate).AddYears(-1).ToString("yyyy");
            string curYear = ASTUtility.Right(this.txtdate.Text, 4);
            string empType = this.GetEmpType();

            var lst = dt1.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptAnnualIncrement>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_93_AnnInc.RptAnnualIncrement", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("prevYear", prevYear));
            Rpt1.SetParameters(new ReportParameter("curYear", curYear));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Proposed Increment for " + empType + " , " + curYear));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private string GetEmpType()
        {
            string empType = "";
            string wrkStation = this.ddlWstation.SelectedValue.Substring(0, 4);
            switch (wrkStation)
            {
                case "9401":
                case "9402":
                case "9411":
                case "9412":
                case "9413":
                case "9414":
                case "9415":
                case "9416":
                case "9420":
                case "9422":
                    empType = "Staffs";
                    break;

                case "9403":
                case "9408":
                    empType = "Worker";
                    break;

                default:
                    empType = "All";
                    break;
            }
            return empType;
        }
        protected void gvAnnIncre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAnnIncre.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}