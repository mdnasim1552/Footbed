using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Services;
using SPELIB;

using SPEENTITY.C_81_Hrm.C_81_Rec;
namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class HRDailyAtten : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                Session.Remove("DayAtten");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ComVisibility();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetJobLocation();

                ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Attendance - System";
            }


        }

        private void ComVisibility()
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "5305":
                case "5306":

                    this.chkFootBed.Visible = true;
                    this.lblfootbed.Visible = true;
                    break;
                default:
                    this.chkFootBed.Visible = false;
                    this.lblfootbed.Visible = false;

                    break;

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        public void GetDataSet()
        {


            bool result;

            OleDbConnection conn;
            string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
            string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
            string con = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =\\Saiful\rims\Database\RAS.mdb; Jet OLEDB:Database Password=ras258";
            conn = new OleDbConnection(con);
            conn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter("Select din, clock from ras_AttRecord where Clock between " + date1 + " and " + date2, conn);
            adapter.Fill(ds);

            Session["DayAtten"] = ds.Tables[0];
            conn.Close();

            DataTable dt = (DataTable)Session["DayAtten"];
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string idcardno1 = dt.Rows[i]["din"].ToString();
                string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Empolyee Added Successfully');", true);
            this.ShowData();


        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string comid = dt1.Rows[0]["comid"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comid"].ToString() == comid)
                {
                    comid = dt1.Rows[j]["comid"].ToString();
                    dt1.Rows[j]["comname"] = "";

                }
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {

                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";


                }

                else
                {
                    comid = dt1.Rows[j]["comid"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }



            return dt1;

        }



        private void LoadGrid()
        {
            try
            {
                this.gvDailyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvDailyAttn.DataSource = (DataTable)Session["ShowAtten"];
                this.gvDailyAttn.DataBind();

                Session["Report1"] = gvDailyAttn;
                ((HyperLink)this.gvDailyAttn.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["ShowAtten"];
            int TblRowIndex;
            for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
            {

                string intime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvIntime")).Text.Trim();
                string outime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvOuttime")).Text.Trim();
                TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;

                dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + intime;
                dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + outime;
            }
            Session["ShowAtten"] = dt;


        }


        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {


            bool result;
            // this.SaveValue();
            DataTable dt = (DataTable)Session["ShowAtten"];
            Hashtable hst = (Hashtable)Session["tblLogin"];            
            string usrid = hst["usrid"].ToString();
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            DataTable dt1 = new DataTable();

            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DayAttn>();

            dt1 = ASITUtility03.ListToDataTable((List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DayAttn>)list);


            // Date Varification
            string offdayid = Convert.ToDateTime(dt1.Rows[0]["offintime"]).ToString("yyyyMMdd");
            if (dayid != offdayid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Date varification Fail!');", true);
                return;

            }
            //////// same as atten auto service by sabid

            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt1);
            ds1.Tables[0].TableName = "tbl1";

            string CompanyName = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";  
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            //  string deptcode = this.ddlDept.SelectedValue.ToString().Substring(0, 7) + "%";
            //string xml = ds1.GetXml();
            //return;
            DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIMEAUTO1", ds1, null, null, dayid, CompanyName, "", division, dpt, section, joblocation, usrid, postDat, trmid, sessionid, "", "", "", "", "", "", "", "");

            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('System Attendance Update Filed!');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('System Attendance Updated Successfully');", true);
                return;
            }
           



        }
        private void ShowData()
        {

            Session.Remove("ShowAtten");
            Hashtable hst = (Hashtable)Session["tblLogin"];            
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            string empType =((this.ddlWstation.SelectedValue.ToString()=="000000000000")?"94": this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";           
            string usrid = hst["usrid"].ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "SHOWEMPATTEN", empType, div, date, Dept, section, joblocation, usrid, "", "");
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }

            Session["ShowAtten"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }

        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
            this.LoadGrid();


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "5305":
                case "5306":
                    if (this.chkFootBed.Checked == true)
                    {
                        this.GetDailyAttenDanceZKT();

                    }
                    else
                    {
                        this.InsertDailyAttnFB();

                    }
                
                   
                    break;



                case "5301":
                case "5104":
                    this.InsertDailyAttnEdisonFoot();
                    break;



            }




            //this.GetDataSet();
            //Web Referecne






            //SystemProcessAccess prodata = new SystemProcessAccess();
            //try
            //{

            //    Session.Remove("DayAtten");
            //    bool result;
            //    string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
            //    string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
            //    string cmd = "Select din, clock from ras_AttRecord where Clock between " + date1 + " and " + date2;
            //    //string cmd= "select comcod, comnam, comsnam,  comadd1+'<br />'+comadd2+' '+comadd3 as comadd, comadd1,comadd2, comadd3, comadd4  from compinf order by comcod asc";
            //    DataSet ds = prodata.GetDailyAttenDance(cmd);
            //    if (ds == null)
            //    {
            //        this.lmsg.Text = prodata.ErrorObject["Msg"].ToString();
            //        return;

            //    }

            //      Session["DayAtten"] = ds.Tables[0];
            //    DataTable dt = (DataTable)Session["DayAtten"];
            //    string comcod = this.GetCompCode();
            //    string date = this.txtdate.Text;

            //    result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string idcardno1 = dt.Rows[i]["din"].ToString();
            //        string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
            //        string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            //        result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");

            //    }

            //    this.lmsg.Text = "Updated Successfully";
            //    this.ShowData();
            //}



        }
        private void InsertDailyAttnRup()
        {
            //try
            //{


            //    Session.Remove("DayAtten");
            //    bool result;
            //    string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
            //    string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";

            //    HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
            //    DataSet ds = DailyAttendance.GetDailyAttenDance(date1, date2);
            //    Session["DayAtten"] = ds.Tables[0];
            //    DataTable dt = (DataTable)Session["DayAtten"];
            //    string comcod = this.GetCompCode();
            //    string date = this.txtdate.Text;

            //    result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string idcardno1 = dt.Rows[i]["din"].ToString();
            //        string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
            //        string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            //        result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");

            //    }

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);
            //    // this.lmsg.Text = "Updated Successfully";
            //    this.ShowData();
            //}

            //catch (Exception ex)
            //{


            //}


        }

        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        private void PreviousDayAutoUpdate()
        {

            //try
            //{

            //    //this.lmsg.Visible = true;
            //    Session.Remove("DayAtten");
            //    bool result;
            //    string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");
            //    string date1 = pdate + " 12:00:00 AM";
            //    string date2 = pdate + " 11:59:00 PM";

            //    HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
            //    DataSet ds = DailyAttendance.GetDailyAttenDance02(date1, date2);
            //    Session["DayAtten"] = ds.Tables[0];
            //    DataTable dt = (DataTable)Session["DayAtten"];
            //    string comcod = this.GetCompCode();
            //    string date = this.txtdate.Text;
            //    result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //    if (!result)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);

            //        return;
            //    }
            //    // Get EmpIdCard
            //    DataSet ds4 = HRData.GetTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "GETEMPIDCARD", "", "", "", "", "", "", "", "", "");
            //    if (ds4 == null) return;
            //    DataTable dtcrdno = ds4.Tables[0];


            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string idcardno1 = dt.Rows[i]["din"].ToString();

            //        DataRow[] dr = dtcrdno.Select("scardno='" + idcardno1 + "'");

            //        if (dr.Length > 0)
            //            idcardno1 = dr[0]["idcardno"].ToString();
            //        else
            //            continue;
            //        string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
            //        string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            //        result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
            //        if (!result)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
            //            return;
            //        }

            //    }


            //    // All Data Update
            //    result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "INSORUPPREEMPOFFTIME", "", "", pdate, "", "", "", "", "", "", "", "", "", "", "", "");
            //    if (!result)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
            //        return;
            //    }

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);

            //    //this.ShowData();
            //}

            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            //}
        }


        private void InsertDailyAttnFB()
        {

            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                string date1 = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
                string date2 = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");

                //string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                //string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";


                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();               
                DataSet ds = DailyAttendance.GetDailyAttenDanceFB(date1, date2);
                //  string count = DailyAttendance.Country();
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;
                //  result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                //DateTime  cintime =Convert.ToDateTime(Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + " " + ASTUtility.Left(dt.Rows[0]["t_card"].ToString(), 2) + ":" + dt.Rows[0]["t_card"].ToString().Substring(2, 2) + ":" + ASTUtility.Right(dt.Rows[0]["t_card"].ToString(), 2));

                //return;

                dt.Columns.Remove("d_card");
                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dt);
                ds1.Tables[0].TableName = "dt1";
                //string xml = ds1.GetXml();
                //return;

                result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTENFB",ds1, null, null, date, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    string msg = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Attendance Uploaded Successfully');", true);


            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;

            }


        }


        private void GetDailyAttenDanceZKT()
        {
            try
            {
                string comcod = this.GetCompCode();
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                //string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";

                //PtlComClass obj = new PtlComClass(comcod);
                //string mrno = obj.mrfno;
                // List<PtlComClass> lst = new List<PtlComClass>(comcod);
                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                DataSet ds = DailyAttendance.GetDailyAttenDanceZKT(date1, date2);
                //DataSet ds = DailyAttendance.GetDailyAttenDanceAssure(date1, date2);
                //string count = DailyAttendance.Country();

                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];

                string date = this.txtdate.Text;




                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dt);
                ds1.Tables[0].TableName = "dt1";
                //string xml = ds1.GetXml();


                result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTENZKT", ds1, null, null, date, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    string msg = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Attendance Uploaded Successfully');", true);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;

            }
        }
        private void InsertDailyAttnEdisonFoot()
        {
            try
            {



                //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;

                string empType = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
                //string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                //string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";



                DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETDATAFORUPLOAD", date, empType, "", "", "", "", "", "", "");
                if (ds.Tables[0].Rows.Count == 0 || ds == null)
                {

                    return;
                }


                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];



                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno = dt.Rows[i]["din"].ToString();
                    //string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                    if (result == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Successfully');", true);

                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);

                this.ShowData();
            }

            catch (Exception ex)
            {


            }


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
            this.ddlDept.SelectedValue = "000000000000";

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

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
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
            this.GetSectionList();
        }

        protected void lbtnChangeShift_Click(object sender, EventArgs e)
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;
                string empType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";

                string usrid = hst["usrid"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATESHIFTCHANGER", empType, div, date, Dept, section, joblocation, usrid, "", "","","","","","","","","","","","","");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+HRData.ErrorObject["Msg"].ToString()+"');", true);
                   
                    return;
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ex.Message+"');", true);


            }

        }
    }
}