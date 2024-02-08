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
using System.IO;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing;
using System.Drawing.Imaging;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class ImgUpload : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");


                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Image & Signature Upload";

                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetEmployeeName();
                this.getAllData();
                this.GetLineDDL();
            }          

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void getAllData()
        {
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvimg.DataSource = null;
                this.gvimg.DataBind();
                return;
            }

            this.gvimg.DataSource = ds.Tables[0];
            this.gvimg.DataBind();
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
            this.GetSectionList();
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

            this.GetEmployeeName();
        }
        private void GetLineDDL()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            
        }
        protected void lnkbtnUpdateEMPImage_Click(object sender, EventArgs e)
        {
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            DataTable dt = (DataTable)Session["tblempdet"];
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string idcard = dt.Select("empid='" + empid + "'")[0]["idcard"].ToString();
            string filePath = "";
            string filePath2 = "";
            string msg = "";
            string fileExtention = "";
            int fileLenght = 0;


            //check image selected or not
            if ((imgFileUpload.PostedFile != null) && (imgFileUpload.PostedFile.ContentLength > 0) || (imgSigFileUpload.PostedFile != null) && (imgSigFileUpload.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName).ToString() ?? "";
                string fn2 = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName).ToString() ?? "";
                //check image
                if ((imgFileUpload.PostedFile != null) && (imgFileUpload.PostedFile.ContentLength > 0) && (fn != null || fn != ""))
                {
                    Guid uid = Guid.NewGuid();
                    fileExtention = "jpg";
                    //fileExtention = imgFileUpload.PostedFile.ContentType;
                    fileLenght = imgFileUpload.PostedFile.ContentLength;
                    fn = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName).ToString() ?? "";
                    filePath = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                    //filePath = "~/Upload/HRM/EmpImg/" + idcard + "." + fileExtention.ToString().Remove(0, 6);
                    if (fileExtention == "jpg")
                    {
                        if (fileLenght <= 5048576)
                        {
                            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");

                            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                            {
                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage = ScaleImage(bmpPostedImage);

                                objImage.Save(Server.MapPath(filePath), ImageFormat.Jpeg);



                            }
                            else
                            {
                                DataTable dt2 = ds2.Tables[0];
                                string file1 = dt2.Rows[0]["imgurl"].ToString();
                                if (fn2 == null || fn2 == "")
                                {
                                    filePath2 = dt2.Rows[0]["signurl"].ToString();
                                }

                                FileInfo getFile = new FileInfo(Server.MapPath(file1));

                                if (getFile.Exists)
                                {
                                    getFile.Delete();
                                }

                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage = ScaleImage(bmpPostedImage);
                                // Saving image in jpeg format
                                objImage.Save(Server.MapPath(filePath), ImageFormat.Jpeg);

                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Image must be less than 5MB!');", true);
                        }
                    }

                }

                //check signature
                if ((imgSigFileUpload.PostedFile != null) && (imgSigFileUpload.PostedFile.ContentLength > 0) && (fn2 != null || fn2 != ""))
                {
                    Guid uid = Guid.NewGuid();
                    fileExtention = "jpg";
                    //fileExtention = imgSigFileUpload.PostedFile.ContentType;
                    filePath2 = "~/Upload/HRM/EmpSign/" + idcard + ".jpg";
                    //filePath2 = "~/Upload/HRM/EmpSign/" + idcard + "." + fileExtention.ToString().Remove(0, 6);
                    fileLenght = imgSigFileUpload.PostedFile.ContentLength;
                    if (fileExtention == "jpg")
                    {
                        if (fileLenght <= 1048576)
                        {
                            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");

                            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                            {
                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgSigFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage1 = ScaleImage2(bmpPostedImage);
                                // Saving sign in jpeg format
                                objImage1.Save(Server.MapPath(filePath2), ImageFormat.Jpeg);


                            }
                            else
                            {
                                DataTable dt2 = ds2.Tables[0];
                                string file1 = dt2.Rows[0]["signurl"].ToString();
                                if (fn == null || fn == "")
                                {
                                    filePath = dt2.Rows[0]["imgurl"].ToString();
                                }

                                FileInfo getFile = new FileInfo(Server.MapPath(file1));
                                if (getFile.Exists)
                                {
                                    getFile.Delete();
                                }

                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgSigFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage1 = ScaleImage2(bmpPostedImage);
                                // Saving sign in jpeg format
                                objImage1.Save(Server.MapPath(filePath2), ImageFormat.Jpeg);

                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Signature must be less than 1MB!');", true);
                        }
                    }

                }

                bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGENEW", empid, "", "", filePath, filePath2, "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                this.getAllData();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Image Uploaded Successfully');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No File Selected. Please Select file!');", true);
            }

        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image)
        {
            //var ratio = (double)maxHeight / image.Height;
            var newWidth = 300;
            var newHeight = 300;
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        public static System.Drawing.Image ScaleImage2(System.Drawing.Image image)
        {

            var newWidth = 300;
            var newHeight = 80;
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        protected void btn_remove_Click1(object sender, EventArgs e)
        {
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            string comcod = this.GetCompCode();
            string msg = "";
            string filepath = "";
            string filepath2 = "";



            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            filepath = ((Label)this.gvimg.Rows[index].FindControl("lblimg")).Text.ToString();
            filepath2 = ((Label)this.gvimg.Rows[index].FindControl("lblsign")).Text.ToString();
            FileInfo file = new FileInfo(Server.MapPath(filepath));
            if (file.Exists)
            {
                file.Delete();
            }

            FileInfo file2 = new FileInfo(Server.MapPath(filepath2));
            if (file2.Exists)
            {
                file2.Delete();
            }

            string empid = ((Label)this.gvimg.Rows[index].FindControl("lblid")).Text.ToString();

            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "REMOVEDATA", empid, "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            this.getAllData();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Image & Sign Deleted Successfully');", true);

        }

        private void GetEmployeeName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();
            string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string secid = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string lineid = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPNAMEIMG", Company, division, deptid, secid, lineid, userid, "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();

            Session["tblempdet"] = ds3.Tables[0];
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getAllData();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
    }
}