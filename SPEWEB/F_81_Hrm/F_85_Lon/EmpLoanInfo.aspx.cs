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

namespace SPEWEB.F_81_Hrm.F_85_Lon
{
    public partial class EmpLoanInfo : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN INFORMATION";

                this.GetAllOrganogramList();
                this.GetWorkStation();
                this.GetLoanlist();
                this.CommonButton();
                this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;



        }
       
            private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetLNNo()
        {
            string comcod = this.GetComeCode();
            string mLNNO = "NEWLN";
            if (this.ddlPrevLoanList.Items.Count > 0)
                mLNNO = this.ddlPrevLoanList.SelectedValue.ToString();

            string date = this.txtCurDate.Text; ;
            if (mLNNO == "NEWLN")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTLOANNO", date, "", "", "", "", "", "", "", "");

                if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                    return;
                }
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


        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
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
        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
            this.GetEmplist();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmplist();
        }

        private void GetEmplist()
        {
            string comcod = this.GetComeCode();

            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";

            string IdCard = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME", IdCard, emptype, division, dpt, section, "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
              
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                return;
            }

            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();

        }

        private void GetLoanlist()
        {

            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLLOANLIST", "", "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                return;
            }
            this.ddlLoanType.DataTextField = "hrgdesc";
            this.ddlLoanType.DataValueField = "hrgcod";
            this.ddlLoanType.DataSource = ds1.Tables[0];
            this.ddlLoanType.DataBind();

        }
        private void GetPreLnlist()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GetPrevLN", curdate, "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                return;
            }
            this.ddlPrevLoanList.DataTextField = "lnno1";
            this.ddlPrevLoanList.DataValueField = "lnno";
            this.ddlPrevLoanList.DataSource = ds1.Tables[0];
            this.ddlPrevLoanList.DataBind();
        }

        protected void lbtnPrevLoanList_Click(object sender, EventArgs e)
        {
            this.GetPreLnlist();
        }


        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmplist();
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
            string mLNNo = "NEWLN";
            if (this.ddlPrevLoanList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                this.chkAddIns.Visible = true;
                this.chkVisible.Visible = false;
                mLNNo = this.ddlPrevLoanList.SelectedValue.ToString();
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNINFO", mLNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                return;
            }

            ViewState["tblln"] = ds1.Tables[0];



            if (mLNNo == "NEWLN")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTLOANNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                    return;
                }
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
            this.ddlLoanType.SelectedValue = ds1.Tables[1].Rows[0]["loantype"].ToString();
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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {

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



        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlloan.Visible = this.chkVisible.Checked;

        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string comcod = this.GetComeCode();
                if (this.ddlPrevLoanList.Items.Count == 0)
                    this.GetLNNo();

                DataTable dt = (DataTable)ViewState["tblln"];
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string lnno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string empid = this.ddlEmpList.SelectedValue.ToString();
                string toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString();
                string LoanType = this.ddlLoanType.SelectedValue.ToString();
                bool result;
                //Delete Loaninfo
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETELNINFO", lnno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATELN", "LNINFB", lnno, curdate, toamt, "", LoanType, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string lndate = dt.Rows[i]["lndate"].ToString();
                    string lnamt = Convert.ToDouble(dt.Rows[i]["lnamt"]).ToString();
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATELN", "LNINFA", lnno, empid, lndate, lnamt, "",
                        "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }


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
    }
}