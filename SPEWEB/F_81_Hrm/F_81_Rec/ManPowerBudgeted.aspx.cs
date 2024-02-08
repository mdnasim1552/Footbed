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
using System.IO;

namespace SPEWEB.F_81_Hrm.F_81_Rec
{
    public partial class ManPowerBudgeted : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                string qtype = this.Request.QueryString["type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (qtype == "Entry" ? "Manpower Budget Entry" : "Manpower Budget Approved");


                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.CommonButton();
                this.GetYear();
                this.GetLineDDL();
                this.GetBgdtData();
            }

        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = true;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Text = "Budgeted Lock";

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateResReq_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnReCalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((CheckBox)this.Master.FindControl("CheckBox1")).Checked += new EventHandler(lnkPrint_Click);
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            //lst1.Add()

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();

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
            GetSectionList();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }

        private void GetBgdtData()
        {
            string comcod = this.GetCompCode();
            string year = this.ddlYear.SelectedValue.ToString().Trim();
            DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETBGDTDATA", null, null, null, year);
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvBudgeted.DataSource = null;
                this.gvBudgeted.DataBind();
                return;
            }

            Session["tbladdbgdt"] = HiddenSameData(ds.Tables[0]);
            this.Data_Bind();

        }
        private void GetYear()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETYEAR", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlYear.DataTextField = "year1";
            this.ddlYear.DataValueField = "year1";
            this.ddlYear.DataSource = ds1.Tables[0];
            this.ddlYear.DataBind();
            this.ddlYear.SelectedValue = System.DateTime.Today.Year.ToString();
            ds1.Dispose();
        }
        private void GetLineDDL()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        //protected void gvBudgeted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvBudgeted.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}

        protected void lnkBtnDelManBgd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tbladdbgdt"];
                string comcod = this.GetCompCode();
                int rowIndex = ((GridViewRow)((LinkButton)(sender)).NamingContainer).RowIndex;
                int index = (this.gvBudgeted.PageSize * this.gvBudgeted.PageIndex) + rowIndex;
                string bgdYear = dt.Rows[index]["bgdyear"].ToString();
                string secId = dt.Rows[index]["seccode"].ToString();
                string lineCode = dt.Rows[index]["linecode"].ToString();
                bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "DELETEBGDTDATA", bgdYear, secId, lineCode, "", "", "", "", "", "");
                if(result)
                {
                    dt.Rows.RemoveAt(index);
                    Session["tbladdbgdt"] = dt;
                    this.Data_Bind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Budget Deleted Successfully');", true);
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)Session["tbladdbgdt"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvBudgeted.Rows.Count; j++)
            {
                string reson = ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtgvReason")).Text.Trim();
                double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));

                TblRowIndex2 = (this.gvBudgeted.PageSize) * (this.gvBudgeted.PageIndex) + j;

                tbl1.Rows[TblRowIndex2]["numpeople"] = txtpeople == 0 ? 0.00 : txtpeople;
                tbl1.Rows[TblRowIndex2]["reson"] = reson;

            }
            Session["tbladdbgdt"] = tbl1;
        }

        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save_Value();
                DataTable tbl1 = (DataTable)Session["tbladdbgdt"];
                string bgdYear = this.ddlYear.SelectedValue.ToString();
                string wscode = this.ddlWstation.SelectedValue.ToString();
                string divcode = this.ddlDivision.SelectedValue.ToString();
                string dptcode = this.ddlDept.SelectedValue.ToString();
                string seccode = this.ddlSection.SelectedValue.ToString();
                string linecode = this.ddlempline.SelectedValue.ToString();

                DataRow[] dr3 = tbl1.Select("bgdYear = '" + bgdYear + "' and wscode='" + wscode + "' and divcode='" + divcode + "' and dptcode='" + dptcode + "' and seccode='" + seccode + "' and linecode='" + linecode + "'");

                if (dr3.Length == 0)
                {

                    DataRow dr1 = tbl1.NewRow();
                    dr1["bgdyear"] = bgdYear;
                    dr1["wscode"] = this.ddlWstation.SelectedValue.ToString();
                    dr1["wsdesc"] = this.ddlWstation.SelectedItem.Text.Trim();
                    dr1["divcode"] = this.ddlDivision.SelectedValue.ToString();
                    dr1["divdesc"] = this.ddlDivision.SelectedItem.Text.Trim();
                    dr1["dptcode"] = this.ddlDept.SelectedValue.ToString();
                    dr1["deptdesc"] = this.ddlDept.SelectedItem.Text.Trim();
                    dr1["seccode"] = this.ddlSection.SelectedValue.ToString();
                    dr1["sectdesc"] = this.ddlSection.SelectedItem.Text.Trim();
                    dr1["linecode"] = this.ddlempline.SelectedValue.ToString();
                    dr1["linedesc"] = this.ddlempline.SelectedItem.Text.Trim();
                    dr1["numpeople"] = 0;
                    tbl1.Rows.Add(dr1);

                }

                Session["tbladdbgdt"] = this.HiddenSameData(tbl1);
                this.Data_Bind();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string wscode = dt1.Rows[0]["wscode"].ToString();
            string divcode = dt1.Rows[0]["divcode"].ToString();
            string deptcode = dt1.Rows[0]["dptcode"].ToString();
            string seccode = dt1.Rows[0]["seccode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["wscode"].ToString() == wscode)
                {                    
                    dt1.Rows[j]["wsdesc"] = "";
                }
                if (dt1.Rows[j]["divcode"].ToString() == divcode)
                {
                    dt1.Rows[j]["divdesc"] = "";
                }
                if (dt1.Rows[j]["dptcode"].ToString() == deptcode)
                {
                    dt1.Rows[j]["deptdesc"] = "";
                }
                if (dt1.Rows[j]["seccode"].ToString() == seccode)
                {
                    dt1.Rows[j]["sectdesc"] = "";
                }

                wscode = dt1.Rows[j]["wscode"].ToString();
                divcode = dt1.Rows[j]["divcode"].ToString();
                deptcode = dt1.Rows[j]["dptcode"].ToString();
                seccode = dt1.Rows[j]["seccode"].ToString();
            }

            return dt1;
        }

        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tbladdbgdt"];
            this.gvBudgeted.DataSource = tbl1;
            this.gvBudgeted.DataBind();

        }

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string bgdYear = this.ddlYear.SelectedValue.ToString();
                string posteddat = System.DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss");
                string apprbyid = hst["usrid"].ToString();
                string apprdat = System.DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss");
                this.Save_Value();

                DataTable dt = (DataTable)Session["tbladdbgdt"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string secId = dt.Rows[i]["seccode"].ToString();
                    double numOfEmp = Convert.ToDouble(dt.Rows[i]["numpeople"].ToString());
                    string linecode = dt.Rows[i]["linecode"].ToString();
                    string reason = dt.Rows[i]["reson"].ToString();
                   
                    if(numOfEmp > 0)
                    {
                        bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "INSERTBGDTDATA", bgdYear, apprbyid, apprdat, posteddat, secId, linecode, numOfEmp.ToString(), reason);
                        if (!result)
                        {
                            string msg = HRData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                            return;
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Manpower Budget Updated successfully');", true);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void btnReCalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tbladdbgdt"];
            if (dt == null)
                return;

            var lstsum = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.ManPowerBudgt>();
            string year = lstsum[0].bgdyear.ToString();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptManpowerBudgt", lstsum, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Budgeted Manpower-" + year));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + 
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}