using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.Data.OleDb;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_93_AnnInc
{
    public partial class AnnualIncrModification : System.Web.UI.Page
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
                this.GetSignatory();
                this.GetDesignation();
                this.imgbtnPreList_Click(null,null);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Annual Increment Modification";


            }           
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetSignatory()
        {
            string comcod = this.GetComeCode();
            DataSet tblSignatory = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPSIGNAME", "80", "%", "%", "%", "", "", "", "", "");
            if (tblSignatory == null)
                return;

            Session["tblsignatory"] = tblSignatory.Tables[0];
        }

        private void GetDesignation()
        {
            string comcod = this.GetComeCode();
            DataSet dsdesig = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETEMPGRADE", "", "", "", "", "", "", "", "", "");
            if (dsdesig == null)
                return;

            Session["tbldesignation"] = dsdesig.Tables[0];
        }

        private void GetPreviousList()
        {
            string comcod = GetComeCode();
            string mREQDAT = this.GetStdDate(this.txtdate.Text);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREVDATEDINCREMENTNO", mREQDAT, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.ddlPrevIncList.Items.Clear();
                return;

            }

            this.ddlPrevIncList.DataTextField = "incrno1";
            this.ddlPrevIncList.DataValueField = "incrno";
            this.ddlPrevIncList.DataSource = ds2.Tables[0];
            this.ddlPrevIncList.DataBind();

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }



        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "New")
            {
                this.lblCurIncrNo.Text = "";
                this.txtCurIncrNo.Text = "";
                this.ddlPrevIncList.Enabled = true;
                this.ddlPrevIncList.Items.Clear();
                this.lnkbtnShow.Text = "Ok";
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();               
                this.txtdate.Enabled = true;
                return;
            }

            this.ddlPrevIncList.Enabled = false;
            this.lnkbtnShow.Text = "New";         
            this.ShowInc();
        }
        private void ShowInc()
        {
            string comcod = this.GetComeCode();
            string preincreno = this.ddlPrevIncList.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENT", preincreno, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }

            this.lblCurIncrNo.Text = ds2.Tables[1].Rows[0]["incrno1"].ToString().Substring(0, 6);
            this.txtCurIncrNo.Text = ds2.Tables[1].Rows[0]["incrno1"].ToString().Substring(6, 5);
            this.txtdate.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["incrdate"].ToString()).ToString("dd.MM.yyyy");           

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblAnnInc"] = dt;
            this.LoadGrid();
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

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        private void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                int TblRowIndex;
                for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
                {
                    double carallow = Convert.ToDouble("0" + ((Label)this.gvAnnIncre.Rows[i].FindControl("lgvprecarsubamt")).Text.Trim());
                    double suballow = Convert.ToDouble("0" + ((Label)this.gvAnnIncre.Rows[i].FindControl("lgvprsubamt")).Text.Trim());
                    double presal = Convert.ToDouble("0" + ((Label)this.gvAnnIncre.Rows[i].FindControl("lgvpreamt")).Text.Trim());
                    double incpercnt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("lgvincpercnt")).Text.Trim());
                    double incamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvincamt")).Text.Trim());
                    double hrpromincamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvhrpromincamt")).Text.Trim());
                    double fincamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvfinamount")).Text.Trim());
                    string remarks = ((TextBox)this.gvAnnIncre.Rows[i].FindControl("TxtRemarks")).Text.Trim();
                    string Signatory = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("DdlSignatory")).SelectedValue.Trim().ToString();
                    string grade = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("ddlGrade")).SelectedValue.Trim().ToString();
                    string promdesigid = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("ddlDesignation")).SelectedValue.Trim().ToString();
                    string hrpromdesigid = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("ddlDesignation1")).SelectedValue.Trim().ToString();


                    TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;

                    incpercnt = presal == 0 ? presal : incpercnt > 0 ? incpercnt : Math.Round(((incamt * 100) / presal), 2);
                    incamt = incamt > 0 ? incamt : incpercnt > 0 ? (presal * 0.01 * incpercnt) : 0.00;
                    dt.Rows[TblRowIndex]["inpercnt"] = incpercnt;
                    dt.Rows[TblRowIndex]["incamt"] = incamt;
                    dt.Rows[TblRowIndex]["pinincamt"] = incamt;
                    dt.Rows[TblRowIndex]["finincamt"] = fincamt;
                    dt.Rows[TblRowIndex]["remarks"] = remarks;
                    dt.Rows[TblRowIndex]["carsubamt"] = carallow;
                    dt.Rows[TblRowIndex]["subamt"] = suballow;
                    dt.Rows[TblRowIndex]["hrpromincamt"] = hrpromincamt;
                    dt.Rows[TblRowIndex]["signatory"] = Signatory;
                    dt.Rows[TblRowIndex]["grade"] = grade;
                    dt.Rows[TblRowIndex]["prodesigid"] = promdesigid;
                    dt.Rows[TblRowIndex]["hrprodesigid"] = hrpromdesigid;
                }
                Session["tblAnnInc"] = dt;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }


        }
        private void LoadGrid()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                this.gvAnnIncre.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvAnnIncre.DataSource = dt;
                this.gvAnnIncre.DataBind();
                this.FooterCal();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
        protected void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFpresal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incamt)", "")) ? 0.00 : dt.Compute("sum(incamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFPinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pinincamt)", "")) ? 0.00 : dt.Compute("sum(pinincamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFhrpromincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrpromincamt)", "")) ? 0.00 : dt.Compute("sum(hrpromincamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFfinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finincamt)", "")) ? 0.00 : dt.Compute("sum(finincamt)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.LoadGrid();

        }

        protected void gvAnnIncre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvAnnIncre.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string cutdate = this.GetStdDate(this.txtdate.Text);
            try
            {
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)Session["tblAnnInc"];
                string incno = this.lblCurIncrNo.Text.ToString().Trim().Substring(0, 3) + cutdate.Substring(7, 4) + this.lblCurIncrNo.Text.ToString().Trim().Substring(3, 2) + this.txtCurIncrNo.Text.ToString().Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string grossal = dt.Rows[i]["grossal"].ToString();
                    string incpercnt = dt.Rows[i]["inpercnt"].ToString();
                    string incamt = dt.Rows[i]["incamt"].ToString();
                    double pinincamt = Convert.ToDouble(dt.Rows[i]["pinincamt"].ToString());
                    double finincamt = Convert.ToDouble(dt.Rows[i]["finincamt"].ToString());
                    string remarks = dt.Rows[i]["remarks"].ToString();
                    string suballow = dt.Rows[i]["subamt"].ToString();
                    string carallow = dt.Rows[i]["carsubamt"].ToString();
                    string hrpromincamt = dt.Rows[i]["hrpromincamt"].ToString();
                    string signatory = dt.Rows[i]["signatory"].ToString();
                    string incamtprevyr = dt.Rows[i]["incamtprevyr"].ToString();
                    string grade = dt.Rows[i]["grade"].ToString();
                    string prodesigid = dt.Rows[i]["prodesigid"].ToString();
                    string hrprodesigid = dt.Rows[i]["hrprodesigid"].ToString();
                    string empType = dt.Rows[i]["seccode"].ToString();

                    if (finincamt != 0)
                    {
                        bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "INSUPDATE_INCREMENT", incno, empid,
                                                        cutdate, grossal, incpercnt, incamt, finincamt.ToString(), postDat, userid, trmid,
                                                        sessionid, remarks, carallow, suballow, signatory, incamtprevyr, grade, prodesigid, pinincamt.ToString(), hrpromincamt, hrprodesigid, "", "");
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        }

                        //Update Agreement
                        double fgrossal = Math.Round(Convert.ToDouble(dt.Rows[i]["grossal"]) + Convert.ToDouble(dt.Rows[i]["finincamt"]), 0);
                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "APPROVE_INC_SALARY", empid,
                                                        fgrossal.ToString(), empType, "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Increment Updated & Salary Reviewed Successfully. Please Check in Agreement for Confirm.');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);


            }
        }

        protected void imgbtnPreList_Click(object sender, EventArgs e)
        {
            this.GetPreviousList();
        }
        
        protected void lbtnPutSameValue_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            double incpercnt = Convert.ToDouble(dt.Rows[0]["inpercnt"]);
            for (int i = 1; i < dt.Rows.Count; i++)
            {

                double grossal = Convert.ToDouble(dt.Rows[i]["grossal"]);
                dt.Rows[i]["inpercnt"] = incpercnt;
                dt.Rows[i]["incamt"] = grossal * 0.01 * incpercnt;
                dt.Rows[i]["pinincamt"] = grossal * 0.01 * incpercnt;
            }
            Session["tblAnnInc"] = dt;
            this.LoadGrid();

        }

        protected void lbtnRound_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            int TblRowIndex;
            for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
            {

                double Finincamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("lgvfinamount")).Text.Trim());
                TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;
                dt.Rows[TblRowIndex]["pinincamt"] = Finincamt;
            }
            Session["tblAnnInc"] = dt;
            this.LoadGrid();


        }
       
        protected void gvAnnIncre_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtsign = (DataTable)Session["tblsignatory"];
            DataTable dtdesig = (DataTable)Session["tbldesignation"];


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string signatory = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "signatory"));

                DropDownList DdlSignatory = (DropDownList)e.Row.FindControl("DdlSignatory");
                DdlSignatory.DataTextField = "signame";
                DdlSignatory.DataValueField = "idcard";
                DdlSignatory.DataSource = dtsign;
                DdlSignatory.DataBind();
                DdlSignatory.SelectedValue = signatory;


                string prodesigid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prodesigid"));
                DropDownList ddlDesignation = (DropDownList)e.Row.FindControl("ddlDesignation");
                ddlDesignation.DataTextField = "hrgdesc";
                ddlDesignation.DataValueField = "hrgcod";
                ddlDesignation.DataSource = dtdesig;
                ddlDesignation.DataBind();
                ddlDesignation.SelectedValue = prodesigid;

                string hrprodesigid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrprodesigid"));
                DropDownList ddlDesignation1 = (DropDownList)e.Row.FindControl("ddlDesignation1");
                ddlDesignation1.DataTextField = "hrgdesc";
                ddlDesignation1.DataValueField = "hrgcod";
                ddlDesignation1.DataSource = dtdesig;
                ddlDesignation1.DataBind();
                ddlDesignation1.SelectedValue = hrprodesigid;

                string grade = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grade"));
                DropDownList ddlGrade = (DropDownList)e.Row.FindControl("ddlGrade");
                ddlGrade.SelectedValue = grade;
            }
        }

        protected void lnkBtnDelIncrmnt_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                string comcod = this.GetComeCode();
                int rowIndex = ((GridViewRow)((LinkButton)(sender)).NamingContainer).RowIndex;
                int index = (this.gvAnnIncre.PageSize * this.gvAnnIncre.PageIndex) + rowIndex;
                string empId = dt.Rows[index]["empid"].ToString();
                string incrmentNO = this.ddlPrevIncList.SelectedValue.ToString();
                //Checking Maximum Increment Existance 
                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GET_MAXIMUM_INCREMENT", incrmentNO, empId, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }
                else if (ds2.Tables[0].Rows.Count>0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You cannot delete this increment as another one exist after this!');", true);
                    return;
                }

                //Delete & Reversed Increment
                string grossal = dt.Rows[index]["grossal"].ToString();
                string empType = dt.Rows[index]["seccode"].ToString();
                bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "DELETE_REVERSED_INCREMENT", empId, grossal, empType, incrmentNO, "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                dt.Rows.RemoveAt(index);
                Session["tblAnnInc"] = dt;
                this.LoadGrid();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Increment Deleted & Salary Reversed Successfully. Please check in Agreement for Confirm.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }

        protected void lnkBtnDelIncrmntAll_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                string comcod = this.GetComeCode();
                string incrmentNO = this.ddlPrevIncList.SelectedValue.ToString();

                //Checking Maximum Increment Existance 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GET_MAXIMUM_INCREMENT", incrmentNO, empid, "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                        return;
                    }
                    else if (ds2.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You cannot delete this increment as another one exist after this!');", true);
                        return;
                    }
                }
                //Delete & Reversed All Increment
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string empType = dt.Rows[i]["seccode"].ToString();
                    string grossal = dt.Rows[i]["grossal"].ToString();
                    bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "DELETE_REVERSED_INCREMENT", empid, grossal, empType, incrmentNO, "ALL", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }                   

                }

                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Increment Deleted & Salary Reversed Successfully. Please check in Agreement for confirm.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
    }
}