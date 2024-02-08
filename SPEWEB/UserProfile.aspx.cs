using SPELIB;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections;

namespace SPEWEB
{
    public partial class UserProfile : System.Web.UI.Page
    {
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        ProcessAccess UserData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetProfile();
                this.GetSesson();
            }

            if (fileuploaddropzone.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];



                Upload = System.IO.Path.GetFileName(fileuploaddropzone.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                fileuploaddropzone.PostedFile.SaveAs(savelocation);
                //this.UserImg.ImageUrl = "~/Image1/" + Upload;
                image_file = fileuploaddropzone.PostedFile.InputStream;
                size = fileuploaddropzone.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;
                string comcod = this.GetCompCode();
                string savelocation1 = Server.MapPath("~") + "\\Image1";
                string[] filePaths = Directory.GetFiles(savelocation1);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
                string UserId = hst["usrid"].ToString();
                byte[] photo = new byte[0];

                image_file = (Stream)Session["i"];
                size = Convert.ToInt32(Session["s"]);
                BinaryReader br = new BinaryReader(image_file);
                photo = br.ReadBytes(size);

                DataSet ds3 = UserData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERID", UserId, "", "", "", "", "", "", "", "");
                bool updatPhoto;
                ProcessAccess UserData01 = new ProcessAccess("ASTREALERPMSG");
                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                if (ds3.Tables[0].Rows.Count == 0)
                {
                    updatPhoto = UserData01.InsertUserPhoto(comcod, UserId, photo);
                    if (updatPhoto)

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Your Porofile Picture Updated Successfully";

                    else
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Profile Picture Updated failed";
                }

                else
                {
                    updatPhoto = UserData01.UpdateUserPhoto(comcod, UserId, photo);

                    if (updatPhoto)
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Your Porofile Picture Updated Successfully";
                    else
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Profile Picture Updated failed";

                }
            }
        }
        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = UserData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
                      

            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if(season!=null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
        }

        public void GetProfile()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
            {
                Response.Redirect("~/Error404.aspx");
            }
            this.UDesignation.InnerHtml = hst["usrdesig"].ToString();
            UserName.InnerHtml = "Hi, " + hst["usrname"].ToString();
            UserName1.InnerHtml = "Hey <b>" + hst["usrname"].ToString() + "!!</b>  do you want to enable Notifications Panel in your Main Dashboard? (Note: ON for Enable and OFF for Disable)";

            if (hst["events"].ToString() == "True")
            {
                EventSTatus.InnerHtml = "<input type='checkbox'  class='switcher-input'> " +
                                "<span class='switcher-indicator'></span> <span class='switcher-label-on'>ON</span> <span class='switcher-label-off'>OFF</span>";

            }
            else
            {
                EventSTatus.InnerHtml = "<input type='checkbox' class='switcher-input' checked='checked'> " +
                                         "<span class='switcher-indicator'></span> <span class='switcher-label-on'>ON</span> <span class='switcher-label-off'>OFF</span>";

            }

        }

        [WebMethod]
        public static void ChangeEventsStatus()
        {
            Common comn = new Common();
            string usrid = comn.GetUserCode();
            string comcod = comn.GetCompCode();
            ProcessAccess accData = new ProcessAccess();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "EVENTSTATUS_UPDATE", usrid, "", "", "", "", "", "");
            if (result == true)
            {

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void LbtnSaveDefultSeason_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string season = this.DdlSeason.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string UserId = hst["usrid"].ToString();
            bool result = UserData.UpdateTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "SET_USER_DEFAULT_SEASON", season, UserId, "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Your Default Season Set Successfully');", true);

            }
        }

        protected void btnSubmitCngMail_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string mail = lblMail.Text;
            string pass = lblPassword.Text;

            bool result = UserData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT_NEW", "UPDATE_USER_MAIL", userid, username, mail, pass);

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Your Default Message Updated Successfully');", true);

            }
        }
    }
}