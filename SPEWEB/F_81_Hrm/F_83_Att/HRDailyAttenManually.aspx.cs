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
using System.IO;
using System.Data.OleDb;
namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class HRDailyAttenManually : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                Session.Remove("DailyAttendence");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.GetCompany();
                this.GetAllOrganogramList();
                this.GetWorkStation();
                this.GetSectionType();
                this.GetJobLocation();
                this.GetLineddl();
                ((Label)this.Master.FindControl("lblTitle")).Text = "DAILY ATTENDANCE INFORMATION";
                //this.ddlCompany_SelectedIndexChanged(null, null);
            }
            ///////upload deduction sheet/////




            //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
            //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

            //string savelocation = Server.MapPath("~") + "\\Image1";



        }

        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            Session["lstOrganoData"] = lst;
        }

        private void GetLineddl()
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
            lst.Add(new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1("000000000000", "ALL","","","",""));
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";
            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.GetDivision();
        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];

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

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);


            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
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

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];

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

        private void GetSectionType()
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "DateWise":
                    this.divToDate.Visible = true;
                    this.divFinalUpdate.Visible = true;

                    string date = "01-" + System.DateTime.Today.ToString("MMM-yyyy");
                    this.txtdateto.Text = Convert.ToDateTime(date).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.lbtnOk.Visible = false;
                    //case "4305"://Rupayan
                    break;

                default:
                    break;
            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowData();
            this.pnlxcel.Visible = true; 
            this.pnlxcel2.Visible = true;
        }



        private void ShowData()
        {
            Session.Remove("DailyAttendence");
            string comcod = this.GetCompCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4))+"%";
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%");
            string department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%");

            string secid = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%");

            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            //string txtempcode = "%%";
            string type = this.rbtnAtten.SelectedValue.ToString();
            string idcardno = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000" ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DAILYATTENDENCE", department, dayid, date, Company, div, secid, type, idcardno, joblocation, line);
            if (ds4 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }

            Session["DailyAttendence"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                    {
                        dt1.Rows[j]["section"] = "";

                    }

                    secid = dt1.Rows[j]["secid"].ToString();
                }

            }
            return dt1;

        }



        private void LoadGrid()
        {
            DataTable dt = (DataTable)(DataTable)Session["DailyAttendence"];
            try
            {
                this.gvDailyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvDailyAttn.DataSource = dt;
                this.gvDailyAttn.DataBind();               

                if (dt.Rows.Count > 0)
                {
                    Session["Report1"] = gvDailyAttn;
                    ((HyperLink)this.gvDailyAttn.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                }

            }

            catch(Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunctionFail", "showContent('"+ex.Message+"');", true);

            }      


        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            DataTable dt = (DataTable)Session["DailyAttendence"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");


            // Date Varification
            string offdayid = Convert.ToDateTime(dt.Rows[0]["offintime"]).ToString("yyyyMMdd");
            if (dayid != offdayid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Date varification Fail!');", true);
                return;

            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string absent = dt.Rows[i]["absnt"].ToString().Trim();
                string leave = dt.Rows[i]["leave"].ToString().Trim();
                if ((absent != "A") && (leave != "L"))
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string machid = "01";
                    string idcardno = dt.Rows[i]["idcardno"].ToString();
                    string intime = dt.Rows[i]["intime"].ToString();
                    string outtime = dt.Rows[i]["outtime"].ToString();
                    string dedout = dt.Rows[i]["dedout"].ToString();
                    string addhour = dt.Rows[i]["addhour"].ToString();
                    string addoffhour = "0";
                    string absaprstatus = dt.Rows[i]["absaprstatus"].ToString();
                    string offintime = dt.Rows[i]["offintime"].ToString();
                    string offoutime = dt.Rows[i]["offouttime"].ToString();
                    string lnintime = dt.Rows[i]["lnchintime"].ToString();
                    string lnoutime = dt.Rows[i]["lnchouttime"].ToString();
                  
                    string remarks = dt.Rows[i]["remarks"].ToString();
                    string happlicable = dt.Rows[i]["happlicable"].ToString();
                    string attnstatus = "Manual";
                    string adjust = dt.Rows[i]["adjust"].ToString();

                    bool result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIME", dayid, empid, machid, idcardno, intime, outtime,
                          leave, absent, dedout, addhour, addoffhour, offintime, offoutime, lnintime, lnoutime, "", remarks, absaprstatus, userid, postDat, trmid, sessionid, attnstatus, happlicable, adjust);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ HRData.ErrorObject["Msg"].ToString()+"');", true);
                        return;
                    }
                   
                }
                if (absent == "A")
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    string month = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("ddMMyyyy").Substring(2, 2);
                    //tring month1 = month.PadLeft(2, '0');
                    string year = ASTUtility.Right(Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy"), 4);
                    string monyr = month + year;

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                    
                }

            }



            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully.');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string empcount = dt.Rows.Count.ToString();
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string eventtype = "DAILY ATTENDANCE INFORMATION";
                string eventdesc = "Month ID: " + todate + ", Employee Type: " + this.ddlWstation.SelectedValue.ToString(); ;
                string eventdesc2 = this.lblmsg.Text + "Employe Status- Active Employee, Total Employe " + empcount;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            this.LoadGrid();





        }


        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        private void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)Session["DailyAttendence"];
                int TblRowIndex;
                for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
                {
                    string offintime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvoffIntime")).Text.Trim();
                    string offouttime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvoffouttime")).Text.Trim();
                    string intime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                    string poutDate = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtOutDategv")).Text.Trim();

                    string outime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();
                    string lnintime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvlnintime")).Text.Trim();
                    string lnouttime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvlnouttime")).Text.Trim();
                    string leave = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvLeave")).Text.Trim();
                    string absent = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvAbsent")).Text.Trim();
                    string dedout = Convert.ToDouble("0" + ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvDed")).Text.Trim()).ToString();
                    string Addhour = Convert.ToDouble("0" + ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvAddHour")).Text.Trim()).ToString();
                    string Remarks = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvremarks")).Text.Trim();


                    TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;


                    string soffintimedat = Convert.ToDateTime(dt.Rows[TblRowIndex]["offintime"]).ToString("dd-MMM-yyyy");
                    string soffouttimedat = Convert.ToDateTime(dt.Rows[TblRowIndex]["offouttime"]).ToString("dd-MMM-yyyy");

                    DateTime empintime = (soffintimedat == "01-jan-1900") ? Convert.ToDateTime((Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + intime)) : Convert.ToDateTime((Convert.ToDateTime(soffintimedat).ToString("dd-MMM-yyyy") + " " + intime));
                    DateTime empouttime = (soffouttimedat == "01-jan-1900") ? Convert.ToDateTime((Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + offouttime)) : Convert.ToDateTime((Convert.ToDateTime(soffouttimedat).ToString("dd-MMM-yyyy") + " " + offouttime));

                    DateTime empintime1 = Convert.ToDateTime(dt.Rows[i]["intime1"]);
                    DateTime empouttime1 = Convert.ToDateTime(dt.Rows[i]["outtime1"]);
                    string dtattnstatus = dt.Rows[i]["attnstatus"].ToString();


                    string attnstatus = (empintime != empintime1 || empouttime != empouttime1) ? "Manual" : dtattnstatus;

                    dt.Rows[TblRowIndex]["offintime"] = (soffintimedat == "01-jan-1900") ? (Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + offintime) : (Convert.ToDateTime(soffintimedat).ToString("dd-MMM-yyyy") + " " + offintime);
                    dt.Rows[TblRowIndex]["offouttime"] = (soffouttimedat == "01-jan-1900") ? (Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + offouttime) : (Convert.ToDateTime(soffouttimedat).ToString("dd-MMM-yyyy") + " " + offouttime);
                    dt.Rows[TblRowIndex]["intime"] = (soffintimedat == "01-jan-1900") ? (Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + intime) : (Convert.ToDateTime(soffintimedat).ToString("dd-MMM-yyyy") + " " + intime); //Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + intime;
                    dt.Rows[TblRowIndex]["outtime"] = (soffouttimedat == "01-jan-1900") ? (Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + outime) : (Convert.ToDateTime(poutDate).ToString("dd-MMM-yyyy") + " " + outime); //Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + outime;
                    dt.Rows[TblRowIndex]["lnchintime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + lnintime;
                    dt.Rows[TblRowIndex]["lnchouttime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + lnouttime;
                    dt.Rows[TblRowIndex]["leave"] = leave;
                    dt.Rows[TblRowIndex]["absnt"] = absent;
                    dt.Rows[TblRowIndex]["dedout"] = dedout;
                    dt.Rows[TblRowIndex]["addhour"] = Addhour;
                    dt.Rows[TblRowIndex]["remarks"] = Remarks;
                    dt.Rows[TblRowIndex]["attnstatus"] = attnstatus;
                }
                Session["DailyAttendence"] = dt;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message+"');", true);
            }           
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
       
        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }


        protected void lFinalUpdatedwise_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string Company = "94%";
            string Deptid = this.ddlDept.SelectedValue.ToString().Substring(0, 7) + "%";
            string secid = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string txtempcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string dateto = Convert.ToDateTime(this.txtdateto.Text).ToString("dd-MMM-yyyy");
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DATEWISEATTENDENCE", Deptid, dayid, date, Company, txtempcode, secid, dateto, "", "", userid, postDat, trmid, sessionid);


            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed!');", true);
                return;

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


        }


        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {
            string StrFileName = string.Empty;

            if (File1.PostedFile != null)
            {
                StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
                string StrFileType = File1.PostedFile.ContentType;
                int IntFileSize = File1.PostedFile.ContentLength;
                if (IntFileSize <= 0)

                    this.lblmsg.Text = "Uploading of file failed";

                else
                {

                    File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\" + StrFileName));
                    this.lblmsg.Text = "Data Uploading Successfully";
                }
            }
            if (StrFileName == "")
            {
                this.lblmsg.Text = "Please fill a file";
                return;

            }

            string test = Path.GetExtension(File1.PostedFile.FileName);


            string filename1 = Server.MapPath("~") + ("\\Upload\\" + StrFileName); //IIS Path

            //string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);
            string connString = "";
            //Connection String to Excel Workbook
            if (test.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (test.Trim() == ".xlsx")
            {

                connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename1 + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
            }

            //string query = "SELECT [Product],[Category],[Qty(Pcs)],[Value],[Unit Price],[ERP CODE] FROM [Sheet1$]";
            string query = "SELECT [Card_no],[Ac_Intime],[Ac_Outtime],[Leave],[Absent],[Deduction],[Add_hour] FROM [Sheet1$]";
            OleDbConnection conn = new OleDbConnection(connString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            Session["XcelData"] = ds.Tables[0];
            // this.DataInsert();
            da.Dispose();
            conn.Close();
            conn.Dispose();

            DataTable dt = (DataTable)Session["XcelData"];
            if (dt == null)
            {
                return;
            }
            DataTable emp = (DataTable)Session["DailyAttendence"];
            if (emp.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < emp.Rows.Count; i++)
            {
                DataRow[] rows = dt.Select("Card_no ='" + emp.Rows[i]["idcardno"] + "'");

                if (rows.Length > 0)
                {
                    emp.Rows[i]["intime"] = Convert.ToDateTime(rows[0]["Ac_Intime"]).ToString("hh:mm tt");
                    emp.Rows[i]["outtime"] = Convert.ToDateTime(rows[0]["Ac_Outtime"]).ToString("hh:mm tt");
                    emp.Rows[i]["leave"] = Convert.ToString(rows[0]["Leave"]);
                    emp.Rows[i]["absnt"] = Convert.ToString(rows[0]["Absent"]);
                    emp.Rows[i]["dedout"] = Convert.ToDouble(rows[0]["Deduction"]);
                    emp.Rows[i]["addhour"] = Convert.ToDouble(rows[0]["Add_hour"]);
                }

            }
            Session["DailyAttendence"] = emp;
            this.LoadGrid();

        }

        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }

        protected void lnkbtnDeleteAttn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["DailyAttendence"];
                string comcod = this.GetCompCode();
                int rowIndex = ((GridViewRow)((LinkButton)(sender)).NamingContainer).RowIndex;
                int index = (this.gvDailyAttn.PageSize * this.gvDailyAttn.PageIndex) + rowIndex;
                string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
                string EmpID = ((Label)this.gvDailyAttn.Rows[index].FindControl("lblgvEmpId")).Text.Trim();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "DELETE_MANUALENTRY", EmpID, dayid, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result)
                {
                    dt.Rows[index].Delete();
                }

                DataView dv = dt.DefaultView;
                Session["DailyAttendence"] = dv.ToTable();
                this.LoadGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Attendance Deleted Successfully" + "');", true);
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }           
        }
    }
}