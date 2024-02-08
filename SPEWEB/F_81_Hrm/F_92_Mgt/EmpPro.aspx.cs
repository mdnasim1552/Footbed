using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using System.IO;
using System.Drawing;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmpPro : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        static string prevPage = String.Empty;
        BL_ClassManPower getlist = new BL_ClassManPower();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetPromotionNo();

                GetWorkStation();
                GetAllOrganogramList();
                this.GetDesignation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE PROMOTION INFORMATION";

                this.CommonButton();
                GetGradeList();
                this.GetSingEmpName();
                this.GetJobLocation();
            }
            //Excel Upload (Designation Update)
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("ExcelData");
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {

                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    string query = "";
                    query = "SELECT * FROM [Sheet1$]";
                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    Session["ExcelData"] = ds.Tables[0];
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkupdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            //this.ddlSection.SelectedValue = "000000000000";

            ddlSection_SelectedIndexChanged(null, null);
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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPreProNm()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mProNo = "NEWPRO";
            if (this.ddlPrevProList.Items.Count > 0)
                mProNo = this.ddlPrevProList.SelectedValue.ToString();

            string mProDAT = this.txtCurDate.Text.Trim();

            //Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();
            if (mProNo == "NEWPRO")
            {
                DataSet ds2 = HRData.GetTransInfo(comcod, "DBO_HRM.SP_ENTRY_EMPLOYEE", "LASTOPROMOTIONNO", mProDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mProNo = ds2.Tables[0].Rows[0]["maxprono"].ToString();
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxprono1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxprono1"].ToString().Substring(6, 5);
                    this.ddlPrevProList.DataTextField = "maxprono1";
                    this.ddlPrevProList.DataValueField = "maxprono";
                    this.ddlPrevProList.DataSource = ds2.Tables[0];
                    this.ddlPrevProList.DataBind();
                }
            }
        }

        private void GetPromotionNo()
        {

            string comcod = this.GetComeCode();
            string date = this.txtCurDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "DBO_HRM.SP_ENTRY_EMPLOYEE", "LASTOPROMOTIONNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxprono1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxprono1"].ToString().Substring(6);
        }

        private void GetEmplist()
        {
            Session.Remove("tblempdsg");
            string comcod = this.GetComeCode();
            string company = (this.ddlWstation.SelectedValue.Substring(0, 4).ToString() == "0000") ? "%" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString() + "%";
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7).ToString() + "%";
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9).ToString() + "%";
            string sectioncode = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string txtEmpname = "%";
            string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLISTPROM", sectioncode, txtEmpname, company, div, deptid, jobLocation, "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();

            Session["tblempdsg"] = ds1.Tables[0];
            this.ddlEmpList_SelectedIndexChanged(null, null);

        }
        private void GetPreProlist()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GetPrevPromotion", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevProList.DataTextField = "prono1";
            this.ddlPrevProList.DataValueField = "prono";
            this.ddlPrevProList.DataSource = ds1.Tables[0];
            this.ddlPrevProList.DataBind();
        }
        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempdsg"];
            string empid = this.ddlEmpList.SelectedValue.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {
                return;

            }
            this.lblDesig.Text = dr[0]["desig"].ToString();
            this.ddlGrade.SelectedValue = dr[0]["hrgcod"].ToString() == "" ? "0399999" : dr[0]["hrgcod"].ToString();
            this.GetDesignation();
        }
        private void GetDesignation()
        {
            string comcod = this.GetComeCode();
            string txtsrchdesg = this.ddlGrade.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPDESIG", txtsrchdesg, "", "", "", "", "", "", "", "");
            this.ddlDesig.DataTextField = "desig";
            this.ddlDesig.DataValueField = "desigid";
            this.ddlDesig.DataSource = ds1.Tables[0];
            this.ddlDesig.DataBind();



        }
        private void GetSingEmpName()
        {

            string comcod = this.GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPSIGNAME", "80", "%", "%", "%", "", "", "", "", "");
            this.ddlsign.DataTextField = "signame";
            this.ddlsign.DataValueField = "idcard";
            this.ddlsign.DataSource = ds3.Tables[0];
            this.ddlsign.DataBind();
        }
        private void GetJobLocation()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string jobLocCode = "87";
            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            this.ddlJobLocation.DataTextField = "hrgdesc";
            this.ddlJobLocation.DataValueField = "hrgcod";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmplist();
        }

        protected void ibtnDesg_Click(object sender, EventArgs e)
        {
            this.GetDesignation();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)ViewState["tblpro"];
            //string date = Convert.ToDateTime(this.txtCurDate.Text).ToString("MMMM dd, yyyy");
            //ReportDocument rptemppro = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptEmpPromotion();
            //TextObject rptdate = rptemppro.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Date: " + Convert.ToDateTime(this.txtCurDate.Text).ToString("MMMM dd, yyyy");
            //TextObject rpttrnno = rptemppro.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
            //rpttrnno.Text = "Promotion No: " + this.lblCurNo1.Text.Trim() + "-" + this.lblCurNo2.Text.Trim();
            //TextObject rpttxtinformation = rptemppro.ReportDefinition.ReportObjects["txtinformation"] as TextObject;
            //rpttxtinformation.Text = "Management has decided to promotion you in the following Designation.";


            //TextObject txtRemarks = rptemppro.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            //txtRemarks.Text = this.txtRemarks.Text.Trim();
            //rptemppro.SetDataSource(dt);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptemppro.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptemppro;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lbtnPrevProList_Click(object sender, EventArgs e)
        {
            this.GetPreProlist();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlprj.Visible = true;
                this.PnlProRemarks.Visible = true;
                this.lbtnPrevProList.Visible = false;
                this.ddlPrevProList.Visible = false;
                this.pnlExcel.Visible=true;
                //this.txtCurDate.Enabled = true;
                this.ShowPromotion();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.txtRemarks.Text = "";
            this.pnlExcel.Visible = false;
            this.ddlPrevProList.Items.Clear();
            this.lbtnPrevProList.Visible = true;
            this.ddlPrevProList.Visible = true;
            this.pnlprj.Visible = false;
            this.PnlProRemarks.Visible = false;
            this.gvremppro.DataSource = null;
            this.txtCurDate.Enabled = true;
            this.gvremppro.DataBind();
        }

        private void ShowPromotion()
        {

            Session.Remove("tblpro");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mProNo = "NEWPRO";
            if (this.ddlPrevProList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mProNo = this.ddlPrevProList.SelectedValue.ToString();
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROINFO", mProNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblpro"] = ds1.Tables[0];


            if (mProNo == "NEWPRO")
            {
                ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTOPROMOTIONNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxprono1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxprono1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["prono1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["prono1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prodate"]).ToString("dd-MMM-yyyy");
            this.txtRemarks.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();

            this.Data_DataBind();



        }
        private void Data_DataBind()
        {

            this.gvremppro.DataSource = (DataTable)Session["tblpro"];
            this.gvremppro.DataBind();

        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblpro"];
            string empid = this.ddlEmpList.SelectedValue.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["sectionid"] = this.ddlDept.SelectedValue.ToString();
                dr1["section"] = this.ddlSection.SelectedItem.Text;
                dr1["empid"] = empid;
                dr1["empname"] = this.ddlEmpList.SelectedItem.Text.Trim();
                dr1["idcardno"] = (((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'"))[0]["idcardno"];
                dr1["pdesigid"] = (((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'"))[0]["desigid"];
                dr1["rdesigid"] = this.ddlDesig.SelectedValue.ToString();
                dr1["pdesig"] = this.lblDesig.Text.Trim();
                dr1["rdesig"] = this.ddlDesig.SelectedItem.Text.Trim();
                dr1["sempid"] = this.ddlsign.SelectedValue.ToString();
                dr1["sempname"] = this.ddlsign.SelectedItem.Text.Trim();
                dt.Rows.Add(dr1);
            }
            else
            {

                dr[0]["rdesigid"] = this.ddlDesig.SelectedValue.ToString();
                dr[0]["rdesig"] = this.ddlDesig.SelectedItem.Text.Trim();
                dr[0]["sempid"] = this.ddlsign.SelectedValue.ToString();
                dr[0]["sempname"] = this.ddlsign.SelectedItem.Text.Trim();
                Session["tblpro"] = dt;
            }
            this.Data_DataBind();


        }


        protected void lnkupdate_Click(object sender, EventArgs e)
        {
           
            try
            {

                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)Session["tblpro"];
                if (ddlPrevProList.Items.Count == 0)
                {
                    this.GetPreProNm();
                }
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string prono = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string Remarks = this.txtRemarks.Text.Trim();
                string userid = hst["usrid"].ToString();
                string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
                string sessionid = hst["session"].ToString();
                string trmid = hst["compname"].ToString();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string pdesigid = dt.Rows[i]["pdesigid"].ToString();
                    string rdesigid = dt.Rows[i]["rdesigid"].ToString();
                    string remarks = dt.Rows[i]["rmrks"].ToString();
                    string rdesig = dt.Rows[i]["rdesig"].ToString();
                    string sectionid = dt.Rows[i]["sectionid"].ToString();
                    string sempid = dt.Rows[i]["sempid"].ToString();

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSORUPDATEEMPPRO", prono, empid, curdate, pdesigid, rdesigid,
                        Remarks, rdesig, sectionid, postDat, trmid, sessionid, userid, sempid, "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

               

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message+"');", true);                

            }

        }




        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmplist();
        }

        private void GetGradeList()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();


            var lst = getlist.GetEmpGradelist(comcod);
            lst = lst.FindAll(x => x.hrgcod != "00000");


            this.ddlGrade.DataTextField = "hrgdesc";
            this.ddlGrade.DataValueField = "hrgcod";
            this.ddlGrade.DataSource = lst;
            this.ddlGrade.DataBind();
            this.ddlGrade_SelectedIndexChanged(null, null);


        }
        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDesignation();
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblpro"];
                string comcod = this.GetComeCode();
                int index = ((GridViewRow)((LinkButton)(sender)).NamingContainer).RowIndex;
                string empId = dt.Rows[index]["empid"].ToString();
                string prevDesigId = dt.Rows[index]["pdesigid"].ToString();
                string prevDesig = dt.Rows[index]["pdesig"].ToString();
                string promotionNo = this.ddlPrevProList.SelectedValue.ToString();
                bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETE_PROMOTION", promotionNo, empId, prevDesigId, prevDesig, "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                dt.Rows.RemoveAt(index);
                Session["tblpro"] = dt;
                Data_DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Promotion Deleted Successfully" + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }

        protected void btnExcelDataUpgrade_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtProm = (DataTable)Session["tblpro"];
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)Session["ExcelData"];
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = ("Card_no<>''");
                DataTable dt1 = dv1.ToTable();
                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dv1.ToTable());
                ds1.Tables[0].TableName = "tbl1";
                DataSet dsr = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GET_EMP_PROM_GRID_DATA", ds1, null, null, "", "", "", "");
                if (dsr == null)
                    return;

                dtProm = dsr.Tables[0].Copy();
                Session["tblpro"] = dtProm;
                this.Data_DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }           
        }

        protected void Label10_Click(object sender, EventArgs e)
        {
            GetGradeList();
        }
    }
}