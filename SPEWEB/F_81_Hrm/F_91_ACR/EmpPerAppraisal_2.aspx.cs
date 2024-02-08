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
using SPERDLC;
using Microsoft.Reporting.WinForms;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_91_ACR
{
    public partial class EmpPerAppraisal_2 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE PERFORMANCE APPRAISAL";
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetWorkStation();
                this.GetLastPerNumber();
                //   this.GetEmployeeName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetWorkStation()
        {

            string comcod = GetComeCode();
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
            this.GetAllOrganogramList();
            this.GetDivision();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
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
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
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
            this.ddlSection_SelectedIndexChanged(null, null);
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmployeeName();
        }
        private void GetEmployeeName()
        {

            string comcod = this.GetComeCode();
            string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptcode = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string dtcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : ddlSection.SelectedValue.ToString() + "%";
            string txtSEmployee = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, dtcode, txtSEmployee, section, deptcode, "ALL", "", "", "");
            if (ds3 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            ds3.Dispose();


        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPerNumber()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
                mPerNo = this.ddlPreList.SelectedValue.ToString();

            string mProDAT = this.txtCurDate.Text.Trim();
            if (mPerNo == "NEWPER")
            {
                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETLASTPERNO", mProDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6, 5);
                    this.ddlPreList.DataTextField = "maxperno1";
                    this.ddlPreList.DataValueField = "maxperno";
                    this.ddlPreList.DataSource = ds2.Tables[0];
                    this.ddlPreList.DataBind();
                }
            }
        }

        private void GetLastPerNumber()
        {

            string comcod = this.GetComeCode();
            string date = this.txtCurDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETLASTPERNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6);
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetEmployeeName();
        }
        protected void ibtnPreList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETPREPERNO", curdate, "", "", "", "", "", "", "", "");

            this.ddlPreList.DataTextField = "perno1";
            this.ddlPreList.DataValueField = "perno";
            this.ddlPreList.DataSource = ds2.Tables[0];
            this.ddlPreList.DataBind();
            ds2.Dispose();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim().Substring(13);
                //this.ddlEmpName.Visible = false;
                //this.lblEmpname.Visible = true;
                this.ddlEmpName.Enabled = false;
                this.lblprelist.Visible = false;
                //this.txtPreViousList.Visible = false;
                //this.ibtnPreList.Visible = false;
                this.ddlPreList.Visible = false;
                this.ShowPerformance();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.lblmsg.Text = "";
            this.ddlPreList.Items.Clear();
            this.gvPerAppraisal.DataSource = null;
            this.gvPerAppraisal.DataBind();
            //this.ddlEmpName.Visible = true;
            //this.lblEmpname.Visible = false;
            this.ddlEmpName.Enabled = true;
            this.lblprelist.Visible = true;
            //this.txtPreViousList.Visible = true;
            //this.ibtnPreList.Visible = true;
            this.ddlPreList.Visible = true;
            this.txtCurDate.Enabled = true;
        }

        private void ShowPerformance()

        {
            ViewState.Remove("tblper");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mPerNo = this.ddlPreList.SelectedValue.ToString();
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETPERINFO", mPerNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblper"] = ds1.Tables[0];


            //if (mPerNo == "NEWPER")
            //{
            //    ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_ACR_EMPLOYEE", "GETLASTPERNO", CurDate1, "", "", "", "", "", "", "", "");
            //    if (ds1 == null)
            //        return;
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
            //        this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6, 5);
            //    }
            //    return;
            //}

            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["perdate"]).ToString("dd-MMM-yyyy");
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                this.ddlEmpName.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
                //this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim();

            }

            this.Data_DataBind();


        }

        private void Data_DataBind()
        {

            this.gvPerAppraisal.DataSource = (DataTable)ViewState["tblper"];
            this.gvPerAppraisal.DataBind();

        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblper"];
            for (int i = 0; i < this.gvPerAppraisal.Rows.Count; i++)
            {
                string sgval1 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval1")).Checked) ? "True" : "False";
                string sgval2 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval2")).Checked) ? "True" : "False";
                string sgval3 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval3")).Checked) ? "True" : "False";
                string sgval4 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval4")).Checked) ? "True" : "False";
                string sgval5 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval5")).Checked) ? "True" : "False";


                double sgval10 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvPerAppraisal.Rows[i].FindControl("lblgvWge")).Text.Trim()));




                dt.Rows[i]["sgval1"] = sgval1;
                dt.Rows[i]["sgval2"] = sgval2;
                dt.Rows[i]["sgval3"] = sgval3;
                dt.Rows[i]["sgval4"] = sgval4;
                dt.Rows[i]["sgval5"] = sgval5;

                //dt.Rows[i]["sgvalMark"] = sgval10 * 5 / sgval10;



            }
            Session["tblusrper"] = dt;



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.RptEmpPerAppraisalNewPrint();
        }

        private void RptEmpPerAppraisalNewPrint()
        {
            //DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();  //company name
            string comadd = hst["comadd1"].ToString();  //address
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string mPerNo = this.ddlPreList.SelectedValue.ToString();
            string date = this.txtCurDate.Text;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 : dt.Compute("sum(netamt)", "")));

            //var dt1 = (DataTable)Session["tblSalSum"];
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEACRINFO", mPerNo, "", "", "", "", "", "", "", "");

            var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.EmpPerAppraisal01>();
            var lst2 = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.EmpPerAppraisal02>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_91_ACR.RptEmpPerAppraisal", lst1, lst2, null);

            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rtpTitle", "Annual Performance Apprasal"));
            rpt1.SetParameters(new ReportParameter("Cdate", "Date: " + date));
            rpt1.SetParameters(new ReportParameter("incrmnt", lst1[0].incment.ToString("#,##0; ")));
            rpt1.SetParameters(new ReportParameter("grspay", lst1[0].grspay.ToString("#,##0; ")));

            //rpt1.SetParameters(new ReportParameter("InWards", "In Word: " + ASTUtility.Trans(toamt, 2)));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void lbtnUpPerAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblmsg.Visible = true;
                this.SaveValue();
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblper"];
                if (this.ddlPreList.Items.Count == 0)
                    this.GetPerNumber();
                string empid = this.ddlEmpName.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string prono = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string refno = this.txtrefno.Text.Trim();
                bool result = false;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEPERB", prono, empid, curdate, refno, "",
                  "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcod = dt.Rows[i]["gcod"].ToString();
                    string sgcod = "";
                    for (int j = 1; j <= 5; j++)
                    {
                        sgcod = Convert.ToString("0" + j);
                        bool chkgval = Convert.ToBoolean(dt.Rows[i]["sgval" + j.ToString()]);
                        if (chkgval)
                            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEPERA", prono, gcod, sgcod, "",
                         "", "", "", "", "", "", "", "", "", "", "");
                        if (!result)
                            return;
                        if (chkgval)
                            break;
                    }


                }
                this.lblmsg.Text = "Updated Successfully";

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error: " + ex.Message;

            }

        }
        protected void gvPerAppraisal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //CheckBox ch = (CheckBox)e.Row.FindControl("lblgvsgval1");


            }
        }

    }
}