﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
namespace SPEWEB.F_81_Hrm.F_85_Lon
{
    public partial class EmpDeducOther : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN INFORMATION";
                // this.GetLoanNo();
                this.GetEmplist();
                this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


            }
        }

        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void GetDEDNo()
        {
            string comcod = this.GetComeCode();
            string mDEDNO = "NEWDED";
            if (this.ddlPrevLoanList.Items.Count > 0)
                mDEDNO = this.ddlPrevLoanList.SelectedValue.ToString();

            string date = this.txtCurDate.Text; ;
            if (mDEDNO == "NEWDED")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTDEDNO", date, "", "", "", "", "", "", "", "");

                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);

                    this.ddlPrevLoanList.DataTextField = "maxlnno1";
                    this.ddlPrevLoanList.DataValueField = "maxlnno";
                    this.ddlPrevLoanList.DataSource = ds3.Tables[0];
                    this.ddlPrevLoanList.DataBind();
                }
            }

        }
        private void GetEmplist()
        {

            string comcod = this.GetComeCode();
            string txtEmpname = this.txtsrchEmp.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, "", "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();

        }


        private void GetPreLnlist()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREDED", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevLoanList.DataTextField = "lnno1";
            this.ddlPrevLoanList.DataValueField = "lnno";
            this.ddlPrevLoanList.DataSource = ds1.Tables[0];
            this.ddlPrevLoanList.DataBind();
        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblEmpName.Text = this.ddlEmpList.SelectedItem.Text.Trim();
                this.lbtnPrevLoanList.Visible = false;
                this.ddlPrevLoanList.Visible = false;
                this.ddlEmpList.Visible = false;
                this.lblEmpName.Visible = true;
                this.chkAddIns.Checked = false;
                this.chkVisible.Checked = false;
                this.chkVisible.Visible = true;
                this.ShowLoanInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.lblEmpName.Text = "";
            this.lblmsg.Text = "";
            this.ddlPrevLoanList.Items.Clear();
            this.lbtnPrevLoanList.Visible = true;
            this.ddlPrevLoanList.Visible = true;
            this.ddlEmpList.Visible = true;
            this.txtCurDate.Enabled = true;
            this.lblEmpName.Visible = false;
            this.chkAddIns.Visible = false;
            this.lbtnAddInstallment.Visible = false;
            this.chkVisible.Visible = false;
            this.pnlloan.Visible = false;
            this.gvloan.DataSource = null;
            this.gvloan.DataBind();
        }



        private void ShowLoanInfo()
        {
            ViewState.Remove("tblln");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mDEDNo = "NEWDED";
            if (this.ddlPrevLoanList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                this.chkAddIns.Visible = true;
                this.chkVisible.Visible = false;
                mDEDNo = this.ddlPrevLoanList.SelectedValue.ToString();
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEDLNINFO", mDEDNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblln"] = ds1.Tables[0];



            if (mDEDNo == "NEWDED")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTDEDNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);
                return;
            }
            ViewState["tblln1"] = ds1.Tables[1];
            this.ddlEmpList.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
            this.lblEmpName.Text = this.ddlEmpList.SelectedItem.Text.Trim();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["lnno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["lnno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["lndate"]).ToString("dd-MMM-yyyy");
            this.Data_DataBind();




        }

        private void Data_DataBind()
        {

            this.gvloan.DataSource = (DataTable)ViewState["tblln"];
            this.gvloan.DataBind();
            this.FooterCalculation((DataTable)ViewState["tblln"]);



        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvloan.FooterRow.FindControl("gvlFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblln1"];
            DataTable dt = (DataTable)ViewState["tblln"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string lnno = "";
            if (this.ddlPrevLoanList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                lnno = this.ddlPrevLoanList.SelectedValue.ToString();
            }
            DataRow[] dr = dt1.Select("lnno='" + lnno + "'");
            if (dr.Length != 0)
            {

                DataRow dr1 = dt.NewRow();
                //dr1["comcod"] = hst["comcod"].ToString();
                //dr1["gcod"] = this.ddlInstallment.SelectedValue.ToString();
                //dr1["gval"] = "T";
                //dr1["gdesc"] = this.ddlInstallment.SelectedItem.Text.Trim();
                //dr1["pactcode"] = this.ddlProjectName.SelectedValue.ToString();
                //dr1["usircode"] = this.lblCode.Text.Trim();
                dr1["lndate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr1["lnamt"] = 0;
                dt.Rows.Add(dr1);
            }

            Session["tblln"] = dt;
            this.gvloan.DataSource = dt;
            this.gvloan.DataBind();
            this.lbtnTotal_Click(null, null);

        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {

            this.pnlloan.Visible = this.chkVisible.Checked;

        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {

            this.pnlloan.Visible = false;
            this.chkVisible.Checked = false;
            DataTable dt = (DataTable)ViewState["tblln"];
            DataView dv = dt.DefaultView;
            DataTable dt1 = new DataTable();
            dt1 = dt.Clone();

            double toamt = Convert.ToDouble("0" + this.txtToamt.Text.Trim());
            double lnamt = Convert.ToDouble("0" + this.txtinsamt.Text.Trim());
            int dur = Convert.ToInt32(this.ddlMonth.SelectedValue.ToString());
            string date = this.txtstrdate.Text.Trim();
            string lndate;
            DataRow dr1;
            for (int i = 0; i < 500; i++)
            {


                if (toamt > 0)
                {
                    lnamt = (toamt > lnamt) ? lnamt : toamt;

                    if (i == 0)
                    {


                        dr1 = dt1.NewRow();
                        lndate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                        dr1["lndate"] = lndate;
                        dr1["lnamt"] = lnamt;
                        dt1.Rows.Add(dr1);
                        toamt = toamt - lnamt;
                        continue;
                    }

                    dr1 = dt1.NewRow();
                    lndate = Convert.ToDateTime(dt1.Rows[i - 1]["lndate"].ToString()).AddMonths(dur).ToString("dd-MMM-yyyy");
                    dr1["lndate"] = lndate;
                    dr1["lnamt"] = lnamt;
                    dt1.Rows.Add(dr1);
                    toamt = toamt - lnamt;
                }
                else
                {
                    break;

                }
            }

            ViewState["tblln"] = dt1;
            this.Data_DataBind();

        }
        protected void chkAddIns_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddIns.Checked)
            {
                this.lbtnAddInstallment.Visible = true;
            }
            else
            {
                this.lbtnAddInstallment.Visible = false;
            }
        }
        protected void lblResList_DataBinding(object sender, EventArgs e)
        {

        }
        protected void lbtnPrevLoanList_Click(object sender, EventArgs e)
        {
            this.GetPreLnlist();
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmplist();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblln"];
            for (int i = 0; i < this.gvloan.Rows.Count; i++)
            {

                string Insdate = Convert.ToDateTime(((TextBox)this.gvloan.Rows[i].FindControl("txtgvinstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string InsAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvloan.Rows[i].FindControl("gvtxtamt")).Text.Trim())).ToString();
                dt.Rows[i]["lndate"] = Insdate;
                dt.Rows[i]["lnamt"] = InsAmt;
            }
            ViewState["tblln"] = dt;
            this.Data_DataBind();

        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string comcod = this.GetComeCode();
                if (this.ddlPrevLoanList.Items.Count == 0)
                    this.GetDEDNo();

                DataTable dt = (DataTable)ViewState["tblln"];
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string lnno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string empid = this.ddlEmpList.SelectedValue.ToString();
                string toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString();
                bool result;
                //Delete Loaninfo
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEDEDLNINFO", lnno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEDEDLN", "LNINFB", lnno, curdate, toamt, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string lndate = dt.Rows[i]["lndate"].ToString();
                    string lnamt = Convert.ToDouble(dt.Rows[i]["lnamt"]).ToString();
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEDEDLN", "LNINFA", lnno, empid, lndate, lnamt, "",
                        "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
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