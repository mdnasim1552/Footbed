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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;


namespace SPEWEB.F_99_Allinterface
{
    public partial class HRDashboard : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Services") ? "EMPLOYEE  SERVICES INFORMATION" : "EMPLOYEE INFORMATION";

                ////GetProfile
                TabName.Value = Request.Form[TabName.UniqueID];

                // GetProfile();
                ShowImage();
            }
            if (imgFileUpload.HasFile)
            {

                Upload = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                imgFileUpload.PostedFile.SaveAs(savelocation);
                EmpImg.ImageUrl = "~/Image1/" + Upload;
                // Session["x"] = "~/Image1/" + Upload;
                image_file = imgFileUpload.PostedFile.InputStream;
                size = imgFileUpload.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

            }

            if (imgSigFileUpload.HasFile)
            {


                Upload = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                imgSigFileUpload.PostedFile.SaveAs(savelocation);
                EmpSig.ImageUrl = "~/Image1/" + Upload;
                // Session["x1"] = "~/Image1/" + Upload;
                image_file = imgSigFileUpload.PostedFile.InputStream;
                size = imgSigFileUpload.PostedFile.ContentLength;
                Session["i1"] = image_file;
                Session["s1"] = size;
                // image_file.Close();
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ShowImage()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string empid = hst["empid"].ToString();
            ProcessAccess HRData = new ProcessAccess();


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "SHOWIMG", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            Session["tblEmpimg"] = ds1.Tables[0];
            this.EmpImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgEmp";
            this.EmpSig.ImageUrl = "~/GetImage.aspx?ImgID=HREmpSign";


        }
        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();


            string comcod = this.GetComeCode();
            string savelocation = Server.MapPath("~") + "\\Image1";
            string[] filePaths = Directory.GetFiles(savelocation);
            foreach (string filePath in filePaths)
                File.Delete(filePath);


            byte[] photo = new byte[0];
            byte[] signature = new byte[0];


            image_file = (Stream)Session["i"];
            size = Convert.ToInt32(Session["s"]);
            //Stream fstream = new FileStream(image_file);
            // photo=
            BinaryReader br = new BinaryReader(image_file);
            photo = br.ReadBytes(size);

            //Signature
            if (Session["i1"] != null)
            {
                image_file = (Stream)Session["i1"];
                size = Convert.ToInt32(Session["s1"]);
                BinaryReader br1 = new BinaryReader(image_file);
                signature = br1.ReadBytes(size);
            }

            ProcessAccess HRData = new ProcessAccess();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "EMPID", empid, "", "", "", "", "", "", "", "");
            bool updatPhoto;
            if (ds3.Tables[0].Rows.Count == 0)
            {
                updatPhoto = HRData.InsertClientPhoto(comcod, empid, photo, signature);
                //bool updatPhoto = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGE", empid, photo.ToString(), signature.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");
                if (updatPhoto)
                {
                    // this.lblmsg.Text = "Updated Successfully";

                }
                else
                {
                    // this.lblmsg.Text = "Updated failed";
                }

            }

            else
            {
                updatPhoto = HRData.UpdateClientPhoto(comcod, empid, photo, signature);

                if (updatPhoto)
                {
                    //  this.lblmsg.Text = "Updated Successfully";

                }
                else
                {
                    // this.lblmsg.Text = "Updated failed";
                }
            }
            ShowImage();

        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            ProcessAccess HRData = new ProcessAccess();

            string comcod = this.GetComeCode();
            string empid = hst["empid"].ToString();


            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "DELETEUSEIMG", empid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Fail";
            else
                ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Deleted";


        }

    }
}