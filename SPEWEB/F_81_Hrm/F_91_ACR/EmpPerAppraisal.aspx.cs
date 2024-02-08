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

namespace SPEWEB.F_81_Hrm.F_91_ACR
{
    public partial class EmpPerAppraisal : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
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
                this.GetLastPerNumber();
                this.GetEmployeeName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetEmployeeName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtEmpSrc.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETEMPNAME", txtSProject, "", "", "", "", "", "", "", "");
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
                if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                    return;
                }

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
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
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
                this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim().Substring(13);
                this.ddlEmpName.Visible = false;
                this.lblEmpname.Visible = true;
                this.ddlEmpName.Enabled = false;
                this.lblprelist.Visible = false;
                this.txtPreViousList.Visible = false;
                this.ibtnPreList.Visible = false;
                this.ddlPreList.Visible = false;
                this.ShowPerformance();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.lblmsg.Text = "";
            this.ddlPreList.Items.Clear();
            this.gvPerAppraisal.DataSource = null;
            this.gvPerAppraisal.DataBind();
            this.ddlEmpName.Visible = true;
            this.lblEmpname.Visible = false;
            this.ddlEmpName.Enabled = true;
            this.lblprelist.Visible = true;
            this.txtPreViousList.Visible = true;
            this.ibtnPreList.Visible = true;
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
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            ViewState["tblper"] = ds1.Tables[0];


            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["perdate"]).ToString("dd-MMM-yyyy");
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                this.ddlEmpName.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
                this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim();

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
                dt.Rows[i]["sgval1"] = sgval1;
                dt.Rows[i]["sgval2"] = sgval2;
                dt.Rows[i]["sgval3"] = sgval3;
                dt.Rows[i]["sgval4"] = sgval4;

            }
            Session["tblusrper"] = dt;



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string mPerNo = this.ddlPreList.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEACRINFO", mPerNo, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[1];
            ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_91_ACR.RptEmpPerAppraisal();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtRefno = rptstate.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            txtRefno.Text = "Ref: " + dt.Rows[0]["refno"].ToString();
            TextObject txtPerdate = rptstate.ReportDefinition.ReportObjects["txtPerdate"] as TextObject;
            txtPerdate.Text = "Date: " + Convert.ToDateTime(dt.Rows[0]["perdate"]).ToString("dd-MMM-yyyy");

            TextObject txtEmpName = rptstate.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            txtEmpName.Text = dt.Rows[0]["empname"].ToString();
            TextObject txtEmpIdcard = rptstate.ReportDefinition.ReportObjects["txtEmpIdcard"] as TextObject;
            txtEmpIdcard.Text = dt.Rows[0]["empidcardno"].ToString();
            TextObject txtEmpDesig = rptstate.ReportDefinition.ReportObjects["txtEmpDesig"] as TextObject;
            txtEmpDesig.Text = dt.Rows[0]["empdesig"].ToString();
            TextObject txtEmpjoindate = rptstate.ReportDefinition.ReportObjects["txtEmpjoindate"] as TextObject;
            txtEmpjoindate.Text = Convert.ToDateTime(dt.Rows[0]["empjoindate"]).ToString("dd-MMM-yyyy");
            TextObject txtEmpEvaperiod = rptstate.ReportDefinition.ReportObjects["txtEmpEvaperiod"] as TextObject;
            txtEmpEvaperiod.Text = dt.Rows[0]["evaperiod"].ToString();
            TextObject txtEmpPreSal = rptstate.ReportDefinition.ReportObjects["txtEmpPreSal"] as TextObject;
            txtEmpPreSal.Text = Convert.ToDouble(dt.Rows[0]["empgssal"]).ToString("#,##0;(#,##0); ");
            TextObject txtEmpLasIncamADate = rptstate.ReportDefinition.ReportObjects["txtEmpLasIncamADate"] as TextObject;
            txtEmpLasIncamADate.Text = dt.Rows[0]["incamtadate"].ToString();


            TextObject txtSupName = rptstate.ReportDefinition.ReportObjects["txtSupName"] as TextObject;
            txtSupName.Text = dt.Rows[0]["sname"].ToString();
            TextObject txtSupIdcard = rptstate.ReportDefinition.ReportObjects["txtSupIdcard"] as TextObject;
            txtSupIdcard.Text = dt.Rows[0]["sidcardno"].ToString();
            TextObject txtSupDesig = rptstate.ReportDefinition.ReportObjects["txtSupDesig"] as TextObject;
            txtSupDesig.Text = dt.Rows[0]["sdesig"].ToString();
            TextObject txtSupSection = rptstate.ReportDefinition.ReportObjects["txtSupSection"] as TextObject;
            txtSupSection.Text = dt.Rows[0]["ssection"].ToString();
            TextObject txtSupSerlength = rptstate.ReportDefinition.ReportObjects["txtSupSerlength"] as TextObject;
            txtSupSerlength.Text = dt.Rows[0]["sserlength"].ToString();
            TextObject txtSupPreSal = rptstate.ReportDefinition.ReportObjects["txtSupPreSal"] as TextObject;
            txtSupPreSal.Text = Convert.ToDouble(dt.Rows[0]["sgssal"]).ToString("#,##0;(#,##0); ");

            rptstate.SetDataSource(ds1.Tables[0]);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
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
                    for (int j = 1; j <= 4; j++)
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
    }
}