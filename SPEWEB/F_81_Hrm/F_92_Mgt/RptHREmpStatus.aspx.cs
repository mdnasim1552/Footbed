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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RptHREmpStatus : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess("ASITHRM");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.rbtnlst.SelectedIndex = 0;
                this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetProjectName();

            }
        }



        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_PAYROLL", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }
        private void SectionName()
        {

            string comcod = this.GetComCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }

        protected void imgbtnProSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnSecSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.SectionName();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            Session.Remove("tblEmpst");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetComCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            int index = Convert.ToInt32(this.rbtnlst.SelectedIndex.ToString());
            string calltype = "";
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 5:
                case 6:
                    calltype = "RPTEMPSTATUS";
                    break;


                case 4:
                    calltype = "RPTTEREMPSTATUS";
                    break;

                case 7:
                    calltype = "RPTCONFIRMATIONDUE";
                    break;


            }
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_REPORT_HR_EMPSTATUS", calltype, projectcode, section, frmdate, todate, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvEmpStatus.DataSource = null;
                this.gvEmpStatus.DataBind();
                return;

            }


            Session["tblEmpst"] = ds3.Tables[0];
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string refno = dt1.Rows[0]["refno"].ToString();
            string section = dt1.Rows[0]["section"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["refno"].ToString() == refno && dt1.Rows[j]["section"].ToString() == section)
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                    section = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["refdesc"] = "";
                    dt1.Rows[j]["sectionname"] = "";
                }

                else
                {



                    if (dt1.Rows[j]["refno"].ToString() == refno)
                    {
                        dt1.Rows[j]["refdesc"] = "";
                    }

                    if (dt1.Rows[j]["section"].ToString() == section)
                    {
                        dt1.Rows[j]["sectionname"] = "";

                    }
                    refno = dt1.Rows[j]["refno"].ToString();
                    section = dt1.Rows[j]["section"].ToString();

                }

            }
            return dt1;


        }


        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblEmpst"];
            DataView dv;
            int index = Convert.ToInt32(this.rbtnlst.SelectedIndex.ToString());
            switch (index)
            {
                case 0:
                    break;

                case 1:
                    dv = dt.DefaultView;
                    dv.RowFilter = ("tecst='yes'");
                    dt = dv.ToTable();
                    break;
                case 2:
                    dv = dt.DefaultView;
                    dv.RowFilter = ("tecst='no' or tecst=''");
                    dt = dv.ToTable();
                    break;

                case 3:
                    string txtdeg = this.txtDegree.Text.Trim() + "%";
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acadeg like '" + txtdeg + "'");
                    dt = dv.ToTable();
                    break;

                case 5:
                    DateTime frmdate = Convert.ToDateTime(this.txtfromdate.Text);
                    DateTime todate = Convert.ToDateTime(this.txttodate.Text);
                    dv = dt.DefaultView;
                    dv.RowFilter = ("joindate >= '" + frmdate + "' and joindate<= '" + todate + "'");
                    dt = dv.ToTable();
                    break;

                case 6:
                    string txtdesig = this.txtDesig.Text.Trim() + "%";
                    dv = dt.DefaultView;
                    dv.RowFilter = ("desig like '" + txtdesig + "'");
                    dt = dv.ToTable();
                    break;


                case 7:
                    break;


            }

            dt = this.HiddenSameData(dt);
            Session["tblEmpst"] = dt;
            this.gvEmpStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpStatus.DataSource = dt;
            this.gvEmpStatus.DataBind();
            this.gvEmpStatus.Columns[1].Visible = (this.ddlProjectName.SelectedValue == "000000000000") ? true : false;
            this.gvEmpStatus.Columns[10].Visible = (this.rbtnlst.SelectedIndex == 4) ? true : false;
            this.gvEmpStatus.Columns[11].Visible = (this.rbtnlst.SelectedIndex == 7) ? true : false;
            this.FooterCalculation();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblEmpst"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvEmpStatus.FooterRow.FindControl("lgvFNetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);


            if (this.rbtnlst.SelectedIndex == 0)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "All Employee list with academic Qualification";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                //string comcod = this.GetComeCode();
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
            }
            else if (this.rbtnlst.SelectedIndex == 1)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Employee List-Technical Person";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }
            else if (this.rbtnlst.SelectedIndex == 2)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Employee List-Non Technical Person";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }
            else if (this.rbtnlst.SelectedIndex == 3)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Employee List Academic Degree Wise";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }
            else if (this.rbtnlst.SelectedIndex == 4)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptRetiredEmployee();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Retired Employee List";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }
            else if (this.rbtnlst.SelectedIndex == 5)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Joining Date Wise Employee List";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"; ;
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }
            else if (this.rbtnlst.SelectedIndex == 6)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Designation Wise Employee List";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }

            else if (this.rbtnlst.SelectedIndex == 7)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptEmpConfirmation();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy") + ")";
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }

            this.lblprint.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvEmpStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void rbtnlst_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lblfrmdate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.txtfromdate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.lbltodate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.txttodate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.txtDegree.Visible = (this.rbtnlst.SelectedIndex == 3);
            this.txtDesig.Visible = (this.rbtnlst.SelectedIndex == 6);
            this.lblimg.Visible = (this.rbtnlst.SelectedIndex == 3) || (this.rbtnlst.SelectedIndex == 6);
        }
    }
}