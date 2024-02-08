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
    public partial class HRDailyAttenUpload : System.Web.UI.Page
    {
        //Attendance _AppLib = new Attendance();
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                Session.Remove("DayAtten");
                this.txtMrrDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "DAILY ATTENDANCE INFORMATION";

                GetWorkStation();
                GetAllOrganogramList();
            }

        }
        protected void UploadFile(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "4101":
                    this.UploadData();
                    break;

                case "4315": // Assure Group
                    this.UploadData();
                    break;

                case "4306": // Greenland
                    this.UploadDataGreenLand();
                    break;

                default:
                    this.UploadDataGreenLand();
                    break;



            }

        }

        private void UploadData()
        {

            this.lblmsg.Visible = true;
            try
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

                if (txtMrrDate.Text.Trim() == "")
                {
                    this.lblmsg.Text = " Date can not be a blank";
                    return;

                }


                string filename1 = Server.MapPath("~") + ("\\Upload\\" + StrFileName); //IIS Path

                //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
                //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

                //string savelocation = Server.MapPath("~") + "\\Image1";

                System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
                System.IO.StreamReader r = new System.IO.StreamReader(fs);
                Label3.Text = r.ReadToEnd();
                Label4.Text = filename1;
                //UpdatePanel1.Controls.Add(Label1);
                r.Close();


                // Update  Data

                string comcod = this.GetCompCode();
                DataTable t4 = new DataTable();
                t4.Columns.Add("adate", typeof(String));
                t4.Columns.Add("atime", typeof(String));
                t4.Columns.Add("IDCARDNO", typeof(String));
                t4.Columns.Add("machid", typeof(String));


                string ROWID = string.Empty;
                string MACHID = string.Empty;
                string IDCARDNO = string.Empty;
                string LastNo = string.Empty;
                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                DateTime ADAT;
                DateTime ATIME;



                string retFilePath = Label4.Text.Trim();

                StreamReader objReader = new StreamReader(retFilePath);
                ///////
                string[] X1 = new string[30000];
                string sLine = "";
                int i = 0;
                DataTable t1 = new DataTable();
                t1.Columns.Add("empattn", typeof(String));
                while (sLine != null)
                {
                    DataRow dr = t1.NewRow();
                    sLine = objReader.ReadLine();
                    X1[i] = sLine;
                    dr["empattn"] = X1[i];
                    t1.Rows.Add(dr);
                    i = i + 1;
                }
                objReader.Close();

                string allstr;
                string IDCARDNO1;
                string adt;
                string atm;
                string h1;
                string ampm;

                for (int j = 0; j < t1.Rows.Count - 1; j++)
                {

                    allstr = t1.Rows[j]["empattn"].ToString();
                    adt = allstr.Substring(3, 6).ToString();
                    IDCARDNO1 = "0" + allstr.Substring(19, 5).ToString(); //allstr.Substring(10, 6).ToString();
                    MACHID = ASTUtility.Left(allstr, 3);//MACHID = ASTUtility.Right(allstr, 2);//allstr.Substring(26,2).ToString();

                    string a1 = adt.Substring(0, 4).ToString();
                    string ADAT1 = "20" + adt.Substring(4, 2).ToString() + "/" + adt.Substring(2, 2).ToString() + "/" + adt.Substring(0, 2).ToString();
                    ADAT = Convert.ToDateTime(ADAT1);

                    atm = allstr.Substring(9, 4).ToString();
                    if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                    {
                        h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                        h1 = h1.PadLeft(2, '0');
                        ampm = " PM";
                    }
                    else
                    {
                        h1 = (atm.Substring(0, 2)).ToString();
                        ampm = " AM"; //TEST
                    }
                    string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                            "00" + ampm.ToString();

                    ATIME = Convert.ToDateTime(dttime.ToString());

                    DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                    seldate = (seldt.ToString("dd-MMM-yyyy"));
                    bool result = HRData.UpdateTransInfo(comcod, "SP_ATTN_UPDATE", "ATTN_UPDATE_TEMP", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                            Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        this.lblmsg.Text = "Updated Fail";
                    }

                }

                this.lblmsg.Text = "Updated Successfully";





                //Delete File
                string savelocation = Server.MapPath("~") + "\\Upload";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);





            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }




            //this.lblmsg.Visible = true;
            //try
            //{
            //    string StrFileName = string.Empty;

            //    if (File1.PostedFile != null)
            //    {
            //        StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
            //        string StrFileType = File1.PostedFile.ContentType;
            //        int IntFileSize = File1.PostedFile.ContentLength;
            //        if (IntFileSize <= 0)

            //            this.lblmsg.Text = "Uploading of file failed";

            //        else
            //        {
            //            File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\" + StrFileName));
            //            this.lblmsg.Text = "Updated Successfully";
            //        }
            //    }
            //    if (StrFileName == "")
            //    {
            //        this.lblmsg.Text = "Please fill a file";
            //        return;

            //    }

            //    if (txtMrrDate.Text.Trim() == "")
            //    {
            //        this.lblmsg.Text = " Date can not be a blank";
            //        return;

            //    }

            //    string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
            //    //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

            //    //string savelocation = Server.MapPath("~") + "\\Image1";

            //    System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
            //    System.IO.StreamReader r = new System.IO.StreamReader(fs);
            //    Label3.Text = r.ReadToEnd();
            //    Label4.Text = filename1;
            //    //UpdatePanel1.Controls.Add(Label1);
            //    r.Close();

            //    //******************************************
            //    DateTime adtc = Convert.ToDateTime(this.txtMrrDate.Text);
            //    string seldate = (adtc.ToString("dd-MMM-yyyy"));
            //    string dayidc = (adtc.ToString("yyyyMMdd"));
            //    string fileDateStr = StrFileName.Substring(0, 4) + "/" + StrFileName.Substring(4, 2) + "/" + StrFileName.Substring(6, 2);
            //    DateTime fileDate = Convert.ToDateTime(fileDateStr);
            //    string dayidfile = (fileDate.ToString("yyyyMMdd"));
            //    if (dayidc == dayidfile)
            //    {
            //        SelectedDates("");
            //        GetData_Button();
            //        this.ShowData();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    this.lblmsg.Text = "Error:" + ex.Message;
            //}


        }
        private void UploadDataGreenLand()
        {
            this.lblmsg.Visible = true;
            try
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

                if (txtMrrDate.Text.Trim() == "")
                {
                    this.lblmsg.Text = " Date can not be a blank";
                    return;

                }


                string filename1 = Server.MapPath("~") + ("\\Upload\\" + StrFileName); //IIS Path

                //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
                //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

                //string savelocation = Server.MapPath("~") + "\\Image1";

                System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
                System.IO.StreamReader r = new System.IO.StreamReader(fs);
                Label3.Text = r.ReadToEnd();
                Label4.Text = filename1;
                //UpdatePanel1.Controls.Add(Label1);
                r.Close();


                // Update  Data

                string comcod = this.GetCompCode();
                DataTable t4 = new DataTable();
                t4.Columns.Add("adate", typeof(String));
                t4.Columns.Add("atime", typeof(String));
                t4.Columns.Add("IDCARDNO", typeof(String));
                t4.Columns.Add("machid", typeof(String));


                string ROWID = string.Empty;
                string MACHID = string.Empty;
                string IDCARDNO = string.Empty;
                string LastNo = string.Empty;
                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                DateTime ADAT;
                DateTime ATIME;



                string retFilePath = Label4.Text.Trim();

                StreamReader objReader = new StreamReader(retFilePath);
                ///////
                string[] X1 = new string[5000000];
                string sLine = "";
                int i = 0;
                DataTable t1 = new DataTable();
                t1.Columns.Add("empattn", typeof(String));
                while (sLine != null)
                {
                    DataRow dr = t1.NewRow();
                    sLine = objReader.ReadLine();
                    X1[i] = sLine;
                    dr["empattn"] = X1[i];
                    t1.Rows.Add(dr);
                    i = i + 1;
                }
                objReader.Close();

                string allstr;
                string IDCARDNO1;
                string adt;
                string atm;
                string h1;
                string ampm;

                for (int j = 0; j < t1.Rows.Count - 1; j++)
                {

                    allstr = t1.Rows[j]["empattn"].ToString();
                    adt = allstr.Substring(16, 8).ToString();
                    IDCARDNO1 = allstr.Substring(5, 10).ToString(); //allstr.Substring(10, 6).ToString();
                    MACHID = allstr.Substring(1, 3).ToString();

                    string ADAT1 = adt.Substring(0, 4).ToString() + "/" + adt.Substring(4, 2).ToString() + "/" + adt.Substring(6, 2).ToString();//(yyyy/MM/dd)
                    ADAT = Convert.ToDateTime(ADAT1);

                    atm = allstr.Substring(25, 6).ToString();
                    if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                    {
                        h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                        h1 = h1.PadLeft(2, '0');
                        ampm = " PM";
                    }
                    else
                    {
                        h1 = (atm.Substring(0, 2)).ToString();
                        ampm = " AM"; //TEST
                    }
                    string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                             atm.Substring(4, 2).ToString();

                    ATIME = Convert.ToDateTime(dttime.ToString());
                    DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                    bool result = HRData.UpdateTransInfo(comcod, "SP_ATTN_UPDATE", "ATTENDANCEUPDATE", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                            Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");

                }


                //Delete File
                string savelocation = Server.MapPath("~") + "\\Upload";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);





            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }



        }

        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void SelectedDates(string stat1)
        {
            string mTRNDAT = this.txtMrrDate.Text.Trim(); // joinning Date
            this.CalExt1.SelectedDate = Convert.ToDateTime(mTRNDAT);
        }


        protected void cusbuttondupload_Click(object sender, EventArgs e)
        {
            DateTime adtc = Convert.ToDateTime(this.txtMrrDate.Text);
            string seldate = (adtc.ToString("dd-MMM-yyyy"));
            string dayidc = (adtc.ToString("yyyyMMdd"));
            string fileDateStr = this.Label4.Text.Substring(0, 4) + "/" + this.Label4.Text.Substring(4, 2) + "/" + this.Label4.Text.Substring(6, 2);
            DateTime fileDate = Convert.ToDateTime(fileDateStr);
            string dayidfile = (fileDate.ToString("yyyyMMdd"));
            if (dayidc == dayidfile)
            {
                SelectedDates("");
                GetData_Button();
            }
        }

        protected void GetData_Button()
        {
            this.lblmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataTable t4 = new DataTable();
            t4.Columns.Add("adate", typeof(String));
            t4.Columns.Add("atime", typeof(String));
            t4.Columns.Add("IDCARDNO", typeof(String));
            t4.Columns.Add("machid", typeof(String));


            string ROWID = string.Empty;
            string MACHID = string.Empty;
            string IDCARDNO = string.Empty;
            string LastNo = string.Empty;
            string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                                                                                              //string ATIME = string.Empty;
                                                                                              //D_D.DataSource = null;
                                                                                              //D_D.DataBind();
            DateTime ADAT;
            DateTime ATIME;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string aday = Convert.ToDateTime(this.txtMrrDate.Text).ToString("yyyyMMdd");
            /////////////////////////
            SelectedDates("");
            //string mach = cmbmach.Text;
            string retFilePath = Label4.Text.Trim();

            StreamReader objReader = new StreamReader(retFilePath);
            ///////
            string[] X1 = new string[30000];
            string sLine = "";
            int i = 0;
            DataTable t1 = new DataTable();
            t1.Columns.Add("empattn", typeof(String));
            while (sLine != null)
            {
                DataRow dr = t1.NewRow();
                sLine = objReader.ReadLine();
                X1[i] = sLine;
                dr["empattn"] = X1[i];
                t1.Rows.Add(dr);
                i = i + 1;
            }
            objReader.Close();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ATTN_UPDATE", "ATTN_LAST_NO", seldate, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            if (ds3.Tables.Count == 0)
                return;
            if (ds3.Tables[0].Rows.Count == 0)
            {
                this.lblmsg.Text = "File uploaded Successfully";
                return;
            }
            LastNo = (ds3.Tables[0].Rows[0]["MaxNo"]).ToString().Trim();
            for (int j = 0; j < t1.Rows.Count - 1; j++)
            {
                string allstr;
                string IDCARDNO1;
                string adt;
                string atm;
                string h1;
                string ampm;
                allstr = t1.Rows[j]["empattn"].ToString();
                adt = allstr.Substring(3, 6).ToString();
                IDCARDNO1 = "0" + allstr.Substring(19, 5).ToString(); //allstr.Substring(10, 6).ToString();
                MACHID = ASTUtility.Left(allstr, 3);//MACHID = ASTUtility.Right(allstr, 2);//allstr.Substring(26,2).ToString();

                string a1 = adt.Substring(0, 4).ToString();
                string ADAT1 = "20" + adt.Substring(4, 2).ToString() + "/" + adt.Substring(2, 2).ToString() + "/" + adt.Substring(0, 2).ToString();
                ADAT = Convert.ToDateTime(ADAT1);

                atm = allstr.Substring(9, 4).ToString();
                if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                {
                    h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                    h1 = h1.PadLeft(2, '0');
                    ampm = " PM";
                }
                else
                {
                    h1 = (atm.Substring(0, 2)).ToString();
                    ampm = " AM"; //TEST
                }
                string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                        "00" + ampm.ToString();

                ATIME = Convert.ToDateTime(dttime.ToString());



                DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                seldate = (seldt.ToString("dd-MMM-yyyy"));
                SelectedDates("");
                string rid = seldt.ToString("yyMMdd");
                LastNo = Convert.ToString((Convert.ToInt64(LastNo) + 1));
                ROWID = (rid + LastNo.PadLeft(5, '0'));

                bool result = HRData.UpdateTransInfo(comcod, "SP_ATTN_UPDATE", "ATTN_UPDATE_TEMP", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                        Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");

            }



        }

        protected void lbtnTranfered_Click(object sender, EventArgs e)
        {
            bool uptest = true;
            this.lblmsg.Visible = true;
            if (this.txtMrrDate.Text.Trim() != "")
            {

                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");

                DataSet ds3 = HRData.GetTransInfo("SP_ATTN_UPDATE", "UPDATETREANSFEREDINFO", seldate, "", "", "", "", "", "", "", "", "");

                if (ds3 != null)
                {
                    this.lblmsg.Text = "Updated Successfully";
                }
                else
                {
                    this.lblmsg.Text = "Updated  failed!";

                }
            }
            else
            {
                this.lblmsg.Text = "Please Enter  Date";
            }
        }
        protected void D_M_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void lnkbtaUpLocalpc_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;
            bool uptest = true;
            System.IO.StreamReader sr = new System.IO.StreamReader(@"E:\tas\output\inout.txt");
            string line;
            while (sr.Peek() != -1)
            {
                line = sr.ReadLine();
                string[] s = line.Split(',');
                string d = s[2].Substring(0, 2);
                string yyyy = s[2].Substring(6, 4);
                string mmm = s[2].Substring(3, 2);
                string mon = (mmm == "01" ? "Jan" : mmm == "02" ? "Feb" : mmm == "03" ? "Mar" : mmm == "04" ? "Apr" : mmm == "05" ? "May" : mmm == "06" ? "Jun"
                : mmm == "07" ? "Jul" : mmm == "08" ? "Aug" : mmm == "09" ? "Sep" : mmm == "10" ? "Oct" : mmm == "11" ? "Nov" : "Dec");
                string adate = d + "-" + mon + "-" + yyyy;
                string atime = adate + " " + s[3].ToString().Trim();


                uptest = HRData.UpdateTransInfo("SP_ATTN_UPDATE", "NEW_ATTN_UPDATE", s[0].ToString().Trim(), s[1].Substring(6).Trim(), adate, atime, adate, "", "", "", "", "", "", "", "", "", "", "");
                if (uptest)
                {
                    this.lblmsg.Text = "Updated Successfully";
                }
                else
                {
                    this.lblmsg.Text = "Updated  failed!";
                }
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        private string cutdate(string ss)
        {
            string month = "";
            if (ss == "01")
                month = "Jan";
            if (ss == "02")
                month = "Feb";
            if (ss == "03")
                month = "Mar";
            if (ss == "04")
                month = "Apr";
            if (ss == "05")
                month = "May";
            if (ss == "06")
                month = "Jun";
            if (ss == "07")
                month = "Jul";
            if (ss == "08")
                month = "Aug";
            if (ss == "09")
                month = "Sep";
            if (ss == "10")
                month = "Oct";
            if (ss == "11")
                month = "Nov";
            if (ss == "12")
                month = "Dec";
            return month;
        }



        //

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ShowData()
        {
            this.lblmsg.Text = "";
            Session.Remove("ShowAtten");

            string comcod = this.GetCompCode();

            string frmdate = this.txtMrrDate.Text;
            //string todate = this.txttodate.Text;

            string empType = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";





            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "SHOWEMPATTEN", "", "", frmdate, Dept, section, empType, div, "", "");
            if (ds4 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }

            Session["ShowAtten"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

            //this.lblmsg.Text = "";
            //Session.Remove("ShowAtten");

            //string comcod = this.GetCompCode();

            //string date = this.txtMrrDate.Text;
            //DataSet ds4 = HRData.GetTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "SHOWEMPATTEN", "", "", date, "", "", "", "", "", "");
            //if (ds4 == null)
            //{
            //    this.gvDailyAttn.DataSource = null;
            //    this.gvDailyAttn.DataBind();
            //    return;
            //}

            //Session["ShowAtten"] = HiddenSameData(ds4.Tables[0]);
            //this.LoadGrid();
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
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;
            bool result;
            this.SaveValue();
            DataTable dt = (DataTable)Session["ShowAtten"];
            string comcod = this.GetCompCode();
            string date = this.txtMrrDate.Text;
            string dayid = Convert.ToDateTime(this.txtMrrDate.Text).ToString("yyyyMMdd");

            //this.lFinalUpdate.Enabled = (Convert.ToBoolean(dt[0]["entry"]));

            //result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "DELETEOFFTIME", dayid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string absent = dt.Rows[i]["absnt"].ToString().Trim();
                //string leave = dt.Rows[i]["leave"].ToString().Trim();
                //if ((absent != "A") && (leave != "L"))
                //{

                string empid = dt.Rows[i]["empid"].ToString();
                string machid = "01";
                string idcardno = dt.Rows[i]["idcardno"].ToString();
                string intime = dt.Rows[i]["intime"].ToString();
                string outtime = dt.Rows[i]["outtime"].ToString();
                string offintime = dt.Rows[i]["offintime"].ToString();
                string offoutime = dt.Rows[i]["offouttime"].ToString();
                string lnintime = dt.Rows[i]["lnchintime"].ToString();
                string lnoutime = dt.Rows[i]["lnchouttime"].ToString();
                result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIMEAUTO", dayid, empid, machid, idcardno, intime, outtime, offintime, offoutime, lnintime, lnoutime, "", "", "", "", "");
                // }
                //if (absent == "A")
                //{
                //    string empid = dt.Rows[i]["empid"].ToString();
                //    string frmdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                //    string absfl = "1";
                //    string month = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("ddMMyyyy").Substring(2, 2);
                //    //tring month1 = month.PadLeft(2, '0');
                //    string year = ASTUtility.Right(Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy"), 4);
                //    string monyr = month + year;

                //    result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ATTENDENCE", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");

                //}

            }

            this.lblmsg.Text = "Updated Successfully";



        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["ShowAtten"];
            int TblRowIndex;
            for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
            {

                string intime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                string outime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();
                TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;

                dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy") + " " + intime;
                dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy") + " " + outime;

            }
            Session["ShowAtten"] = dt;


        }

        private void LoadGrid()
        {

            this.gvDailyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDailyAttn.DataSource = (DataTable)Session["ShowAtten"]; ;
            this.gvDailyAttn.DataBind();
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
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

    }
}