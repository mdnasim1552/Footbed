using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class HRGeneralDayAdjustment : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "General Day Adjustment";
                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                this.txtTDate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                this.txtGenDate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                this.txtOTAdjDate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                this.txtfrmdate.Text = System.DateTime.Today.ToString("MM/dd/yyyy hh:mm tt");
                this.txttodate.Text = System.DateTime.Today.ToString("MM/dd/yyyy hh:mm tt");
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetJobLocation();
                this.GetEmpLine();
                this.CommonButton();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lFinalTotal_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void CommonButton()
        {

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.lblhdncdate.Value = "";
            this.ShowData();
        }

        protected void lFinalTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        private void ShowData()
        {
            Session.Remove("tblEmpDesc");
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string emptype = (this.ddlWstation.SelectedValue.Substring(0, 4).ToString() == "0000") ? "%" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString() + "%";
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7).ToString() + "%";
            string department = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9).ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string txtEmpname = "%";
            string InactiveEmp = (this.ChckResign.Checked == true) ? "0" : "";
            string empline = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string frmDate = this.txtFDate.Text.Trim();
            string toDate = this.txtTDate.Text.Trim();
            string genDate = this.txtGenDate.Text.Trim();
            string empStatus = this.ddlPrsntStatus.SelectedValue == "1" ? "P" : this.ddlPrsntStatus.SelectedValue == "2" ? "A" : "";
            string joinDateWise = this.chkJoinDate.Checked ? "joindwise" : "";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPGENDAYADJ", null, null, null, txtEmpname, department, section, joinDateWise, emptype, div,
                InactiveEmp, joblocation, userid, empline, frmDate, toDate, genDate, empStatus);
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvMonthlyAttn.DataSource = null;
                this.gvMonthlyAttn.DataBind();
                return;
            }

            //DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "MONTHLYTENDENCE", Empid, monthId, fullmonth, "", "", "", "", "", "");
         
            Session["tblEmpDesc"] = ds3.Tables[0];
            Session["tblAttnSum"] = ds3.Tables[1];
            this.LoadGrid();
            if (ds3.Tables[1].Rows.Count > 0)
            {
                this.gvMonAttnsum.DataSource = ds3.Tables[1];
                this.gvMonAttnsum.DataBind();
            }
        }
        private void LoadGrid()
        {
            try
            {
                this.gvMonthlyAttn.DataSource = (DataTable)Session["tblEmpDesc"];
                this.gvMonthlyAttn.DataBind();


                DataTable dt = (DataTable)Session["tblEmpDesc"];
                if (dt.Rows.Count > 0)

                {

                    string emptype = dt.Rows[0]["company"].ToString().Substring(0, 4);

                    Int32 toMinute; int txtHrsFrac; double txtMinFrac; double totalHrs;

                    toMinute = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(totalmin)", "")) ?
                              0 : dt.Compute("sum(totalmin)", "")));
                    txtHrsFrac = Convert.ToInt32(toMinute / 60);
                    txtMinFrac = toMinute % 60;
                    totalHrs = txtHrsFrac + txtMinFrac * 0.01;
                    //Only OT Based
                    if (emptype == "9403" || emptype == "9414" || emptype == "9408" || emptype == "9416")
                    {

                        ((Label)this.gvMonthlyAttn.FooterRow.FindControl("lgvFtohour")).Text = totalHrs.ToString("#,##0.00;(#,##0.00);");
                    }

                    toMinute = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(ttwohourmin)", "")) ?
                              0 : dt.Compute("sum(ttwohourmin)", "")));
                    txtHrsFrac = Convert.ToInt32(toMinute / 60);
                    txtMinFrac = toMinute % 60;
                    totalHrs = txtHrsFrac + txtMinFrac * 0.01;
                    if (emptype == "9403" || emptype == "9414" || emptype == "9408" || emptype == "9416")
                    {

                        ((Label)this.gvMonthlyAttn.FooterRow.FindControl("lgvFtwohour")).Text = totalHrs.ToString("#,##0.00;(#,##0.00);");
                    }

                    toMinute = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(tacmin)", "")) ?
                              0 : dt.Compute("sum(tacmin)", "")));
                    txtHrsFrac = Convert.ToInt32(toMinute / 60);
                    txtMinFrac = toMinute % 60;
                    totalHrs = txtHrsFrac + txtMinFrac * 0.01;
                    ((Label)this.gvMonthlyAttn.FooterRow.FindControl("lgvFtoworkhour")).Text = totalHrs.ToString("#,##0.00;(#,##0.00);");


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            int TblRowIndex;
            for (int i = 0; i < this.gvMonthlyAttn.Rows.Count; i++)
            {
                string offintime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvoffIntime")).Text.Trim();
                string offouttime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvoffouttime")).Text.Trim();
                string intime = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                string poutDate = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtOutDategv")).Text.Trim();

                string outime = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();
                string lnintime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvlnintime")).Text.Trim();
                string lnouttime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvlnouttime")).Text.Trim();
                string leave = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvLeave")).Text.Trim();
                string absent = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvAbsent")).Text.Trim();
                string hday = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvholiday")).Text.Trim();
                string remarks = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                //string dedout = Convert.ToDouble("0" + ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvDed")).Text.Trim()).ToString();
                //string Addhour = Convert.ToDouble("0" + ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvAddHour")).Text.Trim()).ToString();
                TblRowIndex = (gvMonthlyAttn.PageIndex) * gvMonthlyAttn.PageSize + i;
                //string  Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + intime

                DateTime empintime = Convert.ToDateTime(((LinkButton)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim() + " " + intime);
                DateTime empouttime = Convert.ToDateTime(poutDate + " " + outime);

                //  txtOutDategv

                DateTime empintime1 = Convert.ToDateTime(Convert.ToDateTime(((LinkButton)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dt.Rows[i]["intime1"]).ToString("HH:mm:ss "));
                DateTime empouttime1 = Convert.ToDateTime(poutDate + " " + Convert.ToDateTime(dt.Rows[i]["outtime1"]).ToString("HH:mm:ss "));

                string dtattnstatus = dt.Rows[i]["attnstatus"].ToString();
                string attnstatus = (empintime != empintime1 || empouttime != empouttime1) ? "Manual" : dtattnstatus;
                dt.Rows[TblRowIndex]["offintime"] = Convert.ToDateTime(((LinkButton)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + offintime;
                dt.Rows[TblRowIndex]["offouttime"] = Convert.ToDateTime(((LinkButton)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + offouttime;
                dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(((LinkButton)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + intime;
                dt.Rows[TblRowIndex]["outtime"] = poutDate + " " + outime;
                //dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + outime;
                dt.Rows[TblRowIndex]["lnchintime"] = Convert.ToDateTime(((LinkButton)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + lnintime;
                dt.Rows[TblRowIndex]["lnchouttime"] = Convert.ToDateTime(((LinkButton)this.gvMonthlyAttn.Rows[i].FindControl("lnkgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + lnouttime;
                dt.Rows[TblRowIndex]["leav"] = leave;
                dt.Rows[TblRowIndex]["absnt"] = absent;
                dt.Rows[TblRowIndex]["hday"] = hday;
                dt.Rows[TblRowIndex]["attnstatus"] = attnstatus;
                dt.Rows[TblRowIndex]["remarks"] = remarks;


                //dt.Rows[TblRowIndex]["dedout"] = dedout;
                //dt.Rows[TblRowIndex]["addhour"] = Addhour;

            }
            Session["tblEmpDesc"] = dt;


        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            DataTable dt = (DataTable)Session["tblEmpDesc"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string dayid = Convert.ToDateTime(dt.Rows[i]["cdate"].ToString()).ToString("yyyyMMdd");
                string empid = dt.Rows[i]["empid"].ToString();
                string absent = dt.Rows[i]["absnt"].ToString().Trim();
                string leave = dt.Rows[i]["leav"].ToString().Trim();
                string hday = dt.Rows[i]["hday"].ToString().Trim();
                string attnstatus = dt.Rows[i]["attnstatus"].ToString().Trim();

                switch (leave)
                {
                    case "EL":
                    case "CL":
                    case "SL":
                    case "ML":
                    case "WPL":
                    case "LFT":
                    case "PL":
                    case "HL":
                    case "AL":
                    case "L":
                        leave = "L";
                        break;
                    default:
                        break;




                }

                //'EL' when gcod = '51002' then 'CL' when gcod = '51003' then 'SL' when gcod = '51004' then 'ML'

                //when gcod = '51005' then 'WPL' when gcod = '51006' then 'LFT' when gcod = '51007' then 'PL' when gcod = '51008' then 'HL' when gcod = '51050' then 'AL' else 'L'


                if ((absent != "A") && (leave != "L") && (hday != "FH") && (hday != "SPH") && (hday != "W"))
                {
                    string machid = "01";
                    string idcardno = dt.Rows[i]["idcardno"].ToString();
                    string intime = dt.Rows[i]["intime"].ToString();
                    string outtime = dt.Rows[i]["outtime"].ToString();
                    string dedout = "0";
                    string addhour = "0";
                    string addoffhour = "0";
                    string offintime = dt.Rows[i]["offintime"].ToString();
                    string offoutime = dt.Rows[i]["offouttime"].ToString();
                    string lnintime = dt.Rows[i]["lnchintime"].ToString();
                    string lnoutime = dt.Rows[i]["lnchouttime"].ToString();
                    string remarks = dt.Rows[i]["remarks"].ToString();
                    string absaprstatus = "";
                    string happlicable = dt.Rows[i]["happlicable"].ToString();
                    string adjust = dt.Rows[i]["adjust"].ToString();
                    if (Convert.ToBoolean(dt.Rows[i]["otnotallow"].ToString()) == true)
                        continue;

                    bool result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIME", dayid, empid, machid, idcardno, intime, outtime,
                          leave, absent, dedout, addhour, addoffhour, offintime, offoutime, lnintime, lnoutime, "", remarks, absaprstatus, userid, postDat, trmid, sessionid, attnstatus, happlicable, adjust);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;

                    }
                }

                if (absent == "A")
                {
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    string month = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("ddMMyyyy").Substring(2, 2);
                    string year = ASTUtility.Right(Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy"), 4);
                    string monyr = month + year;

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, dayid, userid, postDat, trmid, sessionid, "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;

                    }
                }

                if (hday == "FH" || hday == "SPH" || hday == "W")
                {
                    string hdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTOUPHOLIDAY", hdate, empid, dayid, userid, postDat, trmid, sessionid, "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;

                    }
                }


                if (leave == "L")
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEEMPOFFTIME", dayid, empid, userid, postDat, trmid, sessionid, "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;

                    }
                }

            }



            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Monthly Attendance Updated Successfully');", true);



            if (ConstantInfo.LogStatus == true)
            {
                string empcount = dt.Rows.Count.ToString();
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string eventtype = "DAILY ATTENDANCE INFORMATION";
                string eventdesc = "Month ID: " + todate + ", Employee Type: " + this.ddlWstation.SelectedValue.ToString(); ;
                string eventdesc2 = ((Label)this.Master.FindControl("lblmsg")).Text + "Employe Status- Active Employee, Total Day" + empcount;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            this.LoadGrid();
        }


        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetEmpLine()
        {
            string comcod = GetCompCode();
            string CompanyName = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlSection.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            var lst = getlist.GETLine(comcod, CompanyName, division, deptname, section);
            this.ddlEmpLine.DataTextField = "linedesc";
            this.ddlEmpLine.DataValueField = "linecode";
            this.ddlEmpLine.DataSource = lst;
            this.ddlEmpLine.DataBind();

        }
        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJob.DataTextField = "location";
            this.ddlJob.DataValueField = "loccode";
            this.ddlJob.DataSource = lst;
            this.ddlJob.DataBind();

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
            Session["hrcompnameadd"] = lst;
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

        }


        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblEmpDesc"];
            DateTime cdate;

            if (((CheckBox)this.gvMonthlyAttn.HeaderRow.FindControl("chkAll")).Checked == true)
            {

                foreach (DataRow dr1 in dt.Rows)
                {
                    string estatus = dr1["estatus"].ToString();
                    if (estatus == "AB")
                    {
                        cdate = Convert.ToDateTime(dr1["cdate"]);
                        dr1["intime"] = cdate.ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dr1["offintime"]).ToString("hh:mm tt");
                        dr1["outtime"] = cdate.ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dr1["offouttime"]).ToString("hh:mm tt");
                        dr1["absnt"] = "";
                        dr1["hday"] = "";
                        dr1["estatus"] = "P";
                        // bool chkdate = 0;
                        dr1["chkdate"] = 1;
                        //dr1["attnstatus"] = 1;

                    }
                }
            }

            else
            {
                foreach (DataRow dr1 in dt.Rows)
                {
                    dr1["chkdate"] = 0;
                }
            }

            Session["tblEmpDesc"] = dt;
            this.LoadGrid();
        }

        protected void checkdate_CheckedChanged(object sender, EventArgs e)
        {
            //Call SaveValue for update previous change data 01-June-2023
            this.SaveValue();
            int RowIndex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;

            DataTable dt = (DataTable)Session["tblEmpDesc"];
            bool chkdate = ((CheckBox)this.gvMonthlyAttn.Rows[RowIndex].FindControl("checkdate")).Checked;
            string cdate = Convert.ToDateTime(dt.Rows[RowIndex]["cdate"]).ToString("dd-MMM-yyy");
            string estatus = dt.Rows[RowIndex]["estatus"].ToString();
            if (chkdate == true)
            {


                if (estatus != "P")
                {


                    cdate = Convert.ToDateTime(dt.Rows[RowIndex]["offintime"]).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? cdate : Convert.ToDateTime(dt.Rows[RowIndex]["offintime"]).ToString("dd-MMM-yyyy");

                    string coutime = Convert.ToDateTime(dt.Rows[RowIndex]["offouttime"]).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? cdate : Convert.ToDateTime(dt.Rows[RowIndex]["offouttime"]).ToString("dd-MMM-yyyy");


                    string punchstatus = dt.Rows[RowIndex]["punchstatus"].ToString().Trim();
                    bool oadjust = Convert.ToBoolean(dt.Rows[RowIndex]["oadjust"].ToString());
                    bool adjust = punchstatus.Length > 0 ? true : oadjust;

                    dt.Rows[RowIndex]["offintime"] = cdate + " " + Convert.ToDateTime(dt.Rows[RowIndex]["offintime"]).ToString("hh:mm tt");
                    dt.Rows[RowIndex]["offouttime"] = coutime + " " + Convert.ToDateTime(dt.Rows[RowIndex]["offouttime"]).ToString("hh:mm tt");
                    dt.Rows[RowIndex]["intime"] = cdate + " " + Convert.ToDateTime(dt.Rows[RowIndex]["offintime"]).ToString("hh:mm tt");
                    dt.Rows[RowIndex]["outtime"] = coutime + " " + Convert.ToDateTime(dt.Rows[RowIndex]["offouttime"]).ToString("hh:mm tt");
                    dt.Rows[RowIndex]["absnt"] = "";
                    dt.Rows[RowIndex]["hday"] = "";
                    dt.Rows[RowIndex]["estatus"] = "P";
                    dt.Rows[RowIndex]["adjust"] = adjust;

                    this.AddOtHourAndComments(RowIndex);
                }



            }
            else
            {

                string oestatus = dt.Rows[RowIndex]["oestatus"].ToString();
                if (oestatus == "W")
                {
                    dt.Rows[RowIndex]["absnt"] = "";
                    dt.Rows[RowIndex]["hday"] = "W";
                    dt.Rows[RowIndex]["estatus"] = "W";

                }
                if (oestatus == "FH")
                {
                    dt.Rows[RowIndex]["absnt"] = "";
                    dt.Rows[RowIndex]["hday"] = "FH";
                    dt.Rows[RowIndex]["estatus"] = "FH";

                }
                if (oestatus == "SPH")
                {
                    dt.Rows[RowIndex]["absnt"] = "";
                    dt.Rows[RowIndex]["hday"] = "SPH";
                    dt.Rows[RowIndex]["estatus"] = "SPH";

                }
                else if (oestatus == "AB")
                {
                    dt.Rows[RowIndex]["absnt"] = "A";
                    dt.Rows[RowIndex]["hday"] = "";
                    dt.Rows[RowIndex]["estatus"] = "AB";
                    bool adjust = Convert.ToBoolean(dt.Rows[RowIndex]["oadjust"].ToString());
                    dt.Rows[RowIndex]["tohour"] = 0;
                    dt.Rows[RowIndex]["twohour"] = 0;
                    dt.Rows[RowIndex]["achour"] = 0;
                    dt.Rows[RowIndex]["totalmin"] = 0;
                    dt.Rows[RowIndex]["ttwohourmin"] = 0;
                    dt.Rows[RowIndex]["achour"] = 0;
                    dt.Rows[RowIndex]["adjust"] = adjust;

                }

                else if (oestatus == "P")
                {
                    dt.Rows[RowIndex]["absnt"] = "A";
                    dt.Rows[RowIndex]["hday"] = "";
                    dt.Rows[RowIndex]["estatus"] = "AB";

                    dt.Rows[RowIndex]["tohour"] = 0;
                    dt.Rows[RowIndex]["twohour"] = 0;
                    dt.Rows[RowIndex]["achour"] = 0;
                    dt.Rows[RowIndex]["totalmin"] = 0;
                    dt.Rows[RowIndex]["ttwohourmin"] = 0;
                    dt.Rows[RowIndex]["achour"] = 0;


                }

            }

            dt.Rows[RowIndex]["chkdate"] = chkdate;


            Session["tblEmpDesc"] = dt;
            this.LoadGrid();
        }

        protected void gvMonthlyAttn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string estatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "estatus")).ToString();
                string oestatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "oestatus")).ToString();

                Label lblgvSlNo = ((Label)e.Row.FindControl("lblgvSlNo"));
                CheckBox checkdate = ((CheckBox)e.Row.FindControl("checkdate"));
                LinkButton lnkgvDate = ((LinkButton)e.Row.FindControl("lnkgvDate"));
                //Coloring
                string cdate = Convert.ToDateTime(((LinkButton)e.Row.FindControl("lnkgvDate")).Text).ToString("dd-MMM-yyyy");
                string scdate = this.lblhdncdate.Value;
                if (cdate == scdate)
                {
                    e.Row.Attributes["style"] = "background:blue; color:white!important";

                    TextBox txtgvIntime = (TextBox)e.Row.FindControl("txtgvIntime");
                    TextBox txtgvOuttime = (TextBox)e.Row.FindControl("txtgvOuttime");
                    TextBox txtOutDategv = (TextBox)e.Row.FindControl("txtOutDategv");
                    TextBox txtgvAbsent = (TextBox)e.Row.FindControl("txtgvAbsent");
                    TextBox txtgvholiday = (TextBox)e.Row.FindControl("txtgvholiday");
                    TextBox txtgvRemarks = (TextBox)e.Row.FindControl("txtgvRemarks");
                    lnkgvDate.Attributes["style"] = "color:white;";
                    txtgvIntime.Attributes["style"] = "color:white; text-align:center;";
                    txtgvOuttime.Attributes["style"] = "color:white;text-align:center;";
                    txtOutDategv.Attributes["style"] = "color:white;";
                    txtgvAbsent.Attributes["style"] = "color:white;";
                    txtgvholiday.Attributes["style"] = "color:white;";
                    txtgvRemarks.Attributes["style"] = "color:white;";



                }







                //DateTime offimein = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimein"));
                //DateTime offouttim = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimeout"));

                // Enable Link Button
                if (estatus == "El" || estatus == "CL" || estatus == "SL" || estatus == "ML" || estatus == "WPL" || estatus == "LFT" || estatus == "PL" || estatus == "HL" || estatus == "AL" || estatus == "L" || estatus == "AB" || estatus == "W" || estatus == "FH" || estatus == "SPH")
                {
                    lnkgvDate.Enabled = false;



                }



                if (estatus == "AB")

                {
                    lblgvSlNo.Attributes["style"] = "background-color:red; color:white;";

                }
                else if (estatus == "W" || estatus == "FH" || estatus == "SPH" || oestatus == "W" || oestatus == "FH" || oestatus == "SPH")
                {
                    lblgvSlNo.Attributes["style"] = "background-color:yellow;";
                }


                else if (estatus == "EL" || estatus == "CL" || estatus == "SL" || estatus == "ML" || estatus == "WPL" || estatus == "LFT" || estatus == "PL" || estatus == "HL" || estatus == "AL" || estatus == "L")
                {
                    lblgvSlNo.Attributes["style"] = "background-color:blue; color:white;";
                    checkdate.Enabled = false;


                }



                //if (estatus == "AB" || estatus == "FH" || estatus == "L")
                //{

                //    ((Label)e.Row.FindControl("lnkgvDate")).Attributes["style"] = "font-weight:bold;";

                //}

            }




        }

        private void AddOtHourAndComments(int rowindex)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblEmpDesc"];
                double hour = ASTUtility.StrPosOrNagative(this.txtothour.Text.Trim());
                string comments = this.txtComments.Text.Trim();
                int hour1 = (int)Math.Floor(hour);
                double min = (hour - (double)hour1) * 100;

                string empid = dt.Rows[rowindex]["empid"].ToString();
                DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                string offinmin = Convert.ToDateTime(dt.Rows[rowindex]["offintime"]).ToString("HH:mm");
                string offoutmin = Convert.ToDateTime(dt.Rows[rowindex]["offouttime"]).ToString("HH:mm");
                string lunchinmin = Convert.ToDateTime(dt.Rows[rowindex]["lnchintime"]).ToString("HH:mm");
                string lunchoutmin = Convert.ToDateTime(dt.Rows[rowindex]["lnchouttime"]).ToString("HH:mm");

                DateTime indate = Convert.ToDateTime(this.txtOTAdjDate.Text + " " + offinmin);
                DateTime todate = Convert.ToDateTime(this.txtOTAdjDate.Text + " " + offoutmin);
                string cdate = Convert.ToDateTime(this.txtOTAdjDate.Text).ToString("dd-MMM-yyyy");

                int itotalMinute = Convert.ToInt32(dt.Rows[rowindex]["totalmin"]);
                todate = todate.AddHours(hour1);
                todate = todate.AddMinutes(min);


                DateTime acintime = Convert.ToDateTime(indate);
                DateTime acouttime = Convert.ToDateTime(todate);
                double tohour, twohour, achour, ttwohourmin, totalmin; int tacmin;

                totalmin = hour1 * 60 + min + itotalMinute;
                tohour = (double)Math.Floor(totalmin / 60) + ((totalmin % 60) * 0.01);
                ttwohourmin = totalmin > 120 ? 120 : totalmin;
                twohour = (double)Math.Floor(ttwohourmin / 60) + ((ttwohourmin % 60) * 0.01);
                TimeSpan ts = acouttime - acintime;
                tacmin = (int)(ts.TotalMinutes);
                achour = (int)(tacmin / 60) + ((tacmin % 60) * 0.01);

                if (dr1.Length > 0)
                {
                    dr1[0]["cdate"] = cdate;
                    dr1[0]["intime"] = cdate + " " + offinmin;
                    dr1[0]["offintime"] = cdate + " " + offinmin;
                    dr1[0]["offouttime"] = cdate + " " + offoutmin;
                    dr1[0]["lnchintime"] = cdate + " " + lunchinmin;
                    dr1[0]["lnchouttime"] = cdate + " " + lunchoutmin;
                    //Prev
                    dr1[0]["outtime"] = todate;
                    dr1[0]["tohour"] = tohour;
                    dr1[0]["twohour"] = twohour;
                    dr1[0]["achour"] = achour;
                    dr1[0]["totalmin"] = totalmin;
                    dr1[0]["ttwohourmin"] = ttwohourmin;
                    dr1[0]["achour"] = achour;
                    dr1[0]["remarks"] = comments;

                }

                Session["tblEmpDesc"] = dt;
                this.LoadGrid();
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }

        }

        protected void lnkgvDate_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;

                DataTable dt = (DataTable)Session["tblEmpDesc"];
                this.txtfrmdate.Text = Convert.ToDateTime(dt.Rows[index]["intime"]).ToString("MM/dd/yyyy HH:mm");
                this.txttodate.Text = Convert.ToDateTime(dt.Rows[index]["outtime"]) > Convert.ToDateTime(dt.Rows[index]["offouttime"]) ? Convert.ToDateTime(dt.Rows[index]["outtime"]).ToString("MM/dd/yyyy HH:mm") : Convert.ToDateTime(dt.Rows[index]["offouttime"]).ToString("MM/dd/yyyy HH:mm");
                this.txtothour.Text = Convert.ToDouble(dt.Rows[index]["tohour"]).ToString("#,##0.00;(#,##0.00);");
                this.lblhdncdate.Value = Convert.ToDateTime(dt.Rows[index]["cdate"]).ToString("dd-MMM-yyyy");
                this.LoadGrid();
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }

        }

        protected void checkFullMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }      

        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAddEmp.Checked)
            {
                this.divAddEmp.Visible = true;
                DataTable dt1 = (DataTable)Session["tblEmpDesc"];
                Session.Remove("tblemp");
                this.CreateDataTable();
                DataTable dt = (DataTable)Session["tblemp"];
                this.ddlEmployee.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)

                {

                    string empid = dr1["empid"].ToString();
                    if (dt.Select("empid='" + empid + "'").Length == 0)
                    {

                        DataRow dra = dt.NewRow();
                        dra["empid"] = dr1["empid"].ToString();
                        dra["idcard"] = dr1["idcardno"].ToString();
                        dra["empname"] = dr1["idcardno"].ToString() + "-" + dr1["empname"].ToString();
                        dt.Rows.Add(dra);
                    }

                }
                Session.Remove("tblEmpDesc");
                Session.Remove("tbladdempdet");
                DataTable dt2 = dt1.Copy();
                Session["tbladdempdet"] = dt2;
                DataTable dt3 = dt1.Clone();
                Session["tblEmpDesc"] = dt3;

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = dt;
                this.DataBind();
                this.LoadGrid();




            }
            else
            {
                this.divAddEmp.Visible = false;
            }
        }
        private void CreateDataTable()
        {
            if (Session["tblemp"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("empid", Type.GetType("System.String"));
                dt.Columns.Add("empname", Type.GetType("System.String"));
                dt.Columns.Add("idcard", Type.GetType("System.String"));
                Session["tblemp"] = dt;

            }
        }
        protected void lnkbtnAddEmp_Click(object sender, EventArgs e)
        {

            try
            {

                DataTable dt = (DataTable)Session["tblEmpDesc"];
                DataTable dtadd = (DataTable)Session["tbladdempdet"];
                string empid = this.ddlEmployee.SelectedValue.ToString();
                DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                if (dr1.Length == 0)
                {
                    DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                    dt.ImportRow(dra[0]);
                }
                else
                {

                    string existempdet = "Employee : " + dr1[0]["idcardno"].ToString() + " - " + dr1[0]["empname"].ToString() + " already existed!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);
                }
                DataView dv = dt.DefaultView;
                dv.Sort = ("section,idcardno");
                Session["tblEmpDesc"] = dv.ToTable();
                this.LoadGrid();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        protected void txtOTAdjDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblEmpDesc"];
                double hour = ASTUtility.StrPosOrNagative(this.txtothour.Text.Trim());
                string comments = this.txtComments.Text.Trim();
                int hour1 = (int)Math.Floor(hour);
                double min = (hour - (double)hour1) * 100;
                             
                string empid = dt.Rows[0]["empid"].ToString();
                DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                string offinmin = Convert.ToDateTime(dr1[0]["offintime"]).ToString("HH:mm");
                string offoutmin = Convert.ToDateTime(dr1[0]["offouttime"]).ToString("HH:mm");
                string lunchinmin = Convert.ToDateTime(dr1[0]["lnchintime"]).ToString("HH:mm");
                string lunchoutmin = Convert.ToDateTime(dr1[0]["lnchouttime"]).ToString("HH:mm");


                DateTime indate = Convert.ToDateTime(this.txtOTAdjDate.Text + " " + offinmin);
                DateTime todate = Convert.ToDateTime(this.txtOTAdjDate.Text + " " + offoutmin);
                string cdate = Convert.ToDateTime(this.txtOTAdjDate.Text).ToString("dd-MMM-yyyy");

                int itotalMinute = Convert.ToInt32(dr1[0]["totalmin"]);
                todate = todate.AddHours(hour1);
                todate = todate.AddMinutes(min);


                DateTime acintime = Convert.ToDateTime(indate);
                DateTime acouttime = Convert.ToDateTime(todate);
                double tohour, twohour, achour, ttwohourmin, totalmin; int tacmin;

                totalmin = hour1 * 60 + min + itotalMinute;
                tohour = (double)Math.Floor(totalmin / 60) + ((totalmin % 60) * 0.01);
                ttwohourmin = totalmin > 120 ? 120 : totalmin;
                twohour = (double)Math.Floor(ttwohourmin / 60) + ((ttwohourmin % 60) * 0.01);
                TimeSpan ts = acouttime - acintime;
                tacmin = (int)(ts.TotalMinutes);
                achour = (int)(tacmin / 60) + ((tacmin % 60) * 0.01);

                if (dr1.Length > 0)
                {
                    dr1[0]["cdate"] = cdate;
                    dr1[0]["intime"] = cdate + " " + offinmin;
                    dr1[0]["offintime"] = cdate + " " + offinmin;
                    dr1[0]["offouttime"] = cdate + " " + offoutmin;
                    dr1[0]["lnchintime"] = cdate + " " + lunchinmin;
                    dr1[0]["lnchouttime"] = cdate + " " + lunchoutmin;
                    //Prev
                    dr1[0]["outtime"] = todate;
                    dr1[0]["tohour"] = tohour;
                    dr1[0]["twohour"] = twohour;
                    dr1[0]["achour"] = achour;
                    dr1[0]["totalmin"] = totalmin;
                    dr1[0]["ttwohourmin"] = ttwohourmin;
                    dr1[0]["achour"] = achour;
                    dr1[0]["remarks"] = comments;

                }

                Session["tblEmpDesc"] = dt;
                this.LoadGrid();
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }
        }
    }
}