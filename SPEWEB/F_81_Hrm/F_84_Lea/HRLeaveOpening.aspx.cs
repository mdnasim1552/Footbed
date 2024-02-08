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
using System.Data.OleDb;
using System.IO;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class HRLeaveOpening : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Earn Leave Opening";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.LeaveOpen();
            }

            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("XcelData");
                    //  this.lmsg.Visible = true;
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            //  this.lmsg.Text = "Uploading Fail";
                            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' file Uploading failed');", true);
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                            //   this.lmsg.Text = "Uploading Successfully";
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    //string path = apppath + "ExcelFile\\" + StrFileName;
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

                    //string query = "SELECT [Product],[Category],[Qty(Pcs)],[Value],[Unit Price],[ERP CODE] FROM [Sheet1$]";
                    string query = "";
                    
                        query = "SELECT [id_card_no],[opening],[earn_leave] FROM [Sheet1$]";
                    
                    
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
                    //this.GetExelData();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["XcelData"];
            DataTable emp = (DataTable)Session["LeavOpen"];
            if (emp.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < emp.Rows.Count; i++)
            {
                DataRow[] rows = dt.Select("id_card_no ='" + emp.Rows[i]["idcardno"] + "'");

                if (rows.Length > 0)
                {

                    emp.Rows[i]["opening"] = Convert.ToDouble(rows[0]["opening"]);
                    emp.Rows[i]["ernleave"] = Convert.ToDouble(rows[0]["earn_leave"]);

                }

            }
            Session["LeavOpen"] = emp;
            this.LoadGrid();


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
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
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
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
        protected void imgbtnCompany_Click(object sender, ImageClickEventArgs e)
        {
            this.GetWorkStation();
        }
        protected void imgbtnProSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.GetSectionList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.txtdate.ReadOnly = true;
                this.pnlxcel.Visible = true;
                this.ShowValue();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.txtdate.ReadOnly = false;
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();
            }
        }

        protected void imgbtnEmpSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowValue();
        }

        private void LeaveOpen()
        {
            string comcod = this.GetComeCode();
            string company = this.ddlWstation.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "OPENDATETEST", company, "", "", "", "", "", "", "", "");
            DataTable dt = ds4.Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.txtdate.Text = Convert.ToDateTime(dt.Rows[0]["dayid"]).ToString("dd-MMM-yyyy");
                this.txtdate.Enabled = false;
            }
                  
        }
        private void ShowValue()
        {
            Session.Remove("LeavOpen");
            string comcod = this.GetComeCode();
            string company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string yearid = this.txtdate.Text;
            string pactcode = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empcode = this.txtSrcEmpCode.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLEAVE01", yearid, pactcode, company, empcode, "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds4.Tables[0]);
            Session["LeavOpen"] = dt;
            this.LoadGrid();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["secname"] = "";
                }
                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["LeavOpen"];
            this.gvLeaveRule.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLeaveRule.DataSource = dt;
            this.gvLeaveRule.DataBind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            
            
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.SaveValue();
            this.LoadGrid();

        }
        protected void gvLeaveRule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SaveValue();
            this.gvLeaveRule.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void lnkbtnFUpLeave_Click(object sender, EventArgs e)
        {
            try

            {

                this.SaveValue();
                DataTable dt = (DataTable)Session["LeavOpen"];
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString(); //comcod
                string yearid = this.txtdate.Text; //year
                foreach (DataRow dr1 in dt.Rows)
                {
                    string empid = dr1["empid"].ToString(); //empid
                    string ernid = dr1["ernid"].ToString();  //gcod
                    string leaopen = dr1["opening"].ToString();
                    string entitlement = dr1["ernleave"].ToString(); // entitlement

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTLEAVEOPEN", empid, ernid, yearid, leaopen, entitlement, "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;

                    }

                }


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ex.Message+"');", true);

            }
         
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["LeavOpen"];
            int TblRowIndex;
            for (int i = 0; i < this.gvLeaveRule.Rows.Count; i++)
            {

                string entitlement = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvel")).Text.Trim()).ToString();
                string leaveopen = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvelOpen")).Text.Trim()).ToString();
                TblRowIndex = (gvLeaveRule.PageIndex) * gvLeaveRule.PageSize + i;
                dt.Rows[TblRowIndex]["opening"] = leaveopen;
                dt.Rows[TblRowIndex]["ernleave"] = entitlement;

            }
            Session["LeavOpen"] = dt;

        }

        protected void imgbtnCompany_Click1(object sender, EventArgs e)
        {
            this.GetWorkStation();
        }

        protected void imgbtnProSrch_Click1(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        protected void imgbtnEmpSrch_Click1(object sender, EventArgs e)
        {
            this.ShowValue();
        }
    }
}