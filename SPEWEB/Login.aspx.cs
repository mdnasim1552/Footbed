using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB
{
    public partial class Login : System.Web.UI.Page
    {
        string ind = "x";
        public static int day = 0;
        //#region Property

        //public DataTable DataSource
        //{
        //    get;
        //    set;
        //}


        //public TreeNode SelectedNode { get; set; }

        //#endregion
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                //this.Initilize();

                this.getComName();
                this.GetHitCounter();
                this.MasComNameaAdd();
                Session.Remove("tbllog1");
                //this.notice();
                //if ((Hashtable)Session["tblLogin"] == null)
                //    return;
                //this.txtuserid.Text = ((Hashtable)Session["tblLogin"])["username"].ToString();
                //this.txtuserpass.Text = ((Hashtable)Session["tblLogin"])["password"].ToString();
            }
            Session.Remove("tblLogin");
        }





        ////private void notice()
        ////{
        ////    string comcod = this.listComName.SelectedValue.ToString();
        ////    ProcessAccess ulogin=new ProcessAccess();
        ////    DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETNOTICE", "", "", "", "", "", "", "", "", "");
        ////    if (ds5.Tables[0].Rows.Count > 0)
        ////    {
        ////        this.mHlabel1.Text = ds5.Tables[0].Rows[0]["nhead"].ToString();
        ////        this.mDeslabel1.Text = ds5.Tables[0].Rows[0]["ndesc"].ToString();
        ////    }
        ////    if (ds5.Tables[1].Rows.Count > 0)
        ////    {
        ////        this.mHlabel2.Text = ds5.Tables[1].Rows[0]["nhead"].ToString();
        ////        this.mDeslabel2.Text = ds5.Tables[1].Rows[0]["ndesc"].ToString();
        ////    }
        ////    if (ds5.Tables[2].Rows.Count > 0)
        ////    {
        ////        this.mHlabel3.Text = ds5.Tables[2].Rows[0]["nhead"].ToString();
        ////        this.mDeslabel3.Text = ds5.Tables[2].Rows[0]["ndesc"].ToString();tblLogin
        ////    }
        ////    if (ds5.Tables[3].Rows.Count > 0)
        ////    {
        ////        this.mHlabel4.Text = ds5.Tables[3].Rows[0]["nhead"].ToString();
        ////        this.mDeslabel4.Text = ds5.Tables[3].Rows[0]["ndesc"].ToString();
        ////    }

        ////}
        private void GetHitCounter()
        {
            Session.Remove("tblhcntlmt");
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetHitCounter();
            DataSet ds3 = ulog.ExpDate();
            DataSet ds2 = ulog.GetHitCounterLimit();
            string comcod = this.listComName.SelectedValue.ToString();
            ProcessAccess ulogin = (ASTUtility.Left(this.listComName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess("ASITHRM") : new ProcessAccess();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGIN", "", "", "", "", "", "", "", "", "");
            Session["tblhcntlmt"] = ds2.Tables[0];

            double cnumber = Convert.ToDouble(ds1.Tables[0].Rows[0]["cnumber"]);
            double cntlim1, cntlim2, cntlim3, dcntlim1, dcntlim2, dcntlim3;
            cntlim1 = Convert.ToDouble(ds2.Tables[0].Rows[0]["cntval"]);
            cntlim2 = Convert.ToDouble(ds2.Tables[0].Rows[1]["cntval"]);
            cntlim3 = Convert.ToDouble(ds2.Tables[0].Rows[2]["cntval"]);

            dcntlim1 = Convert.ToDouble(ds5.Tables[0].Rows[0]["cntval"]);
            dcntlim2 = Convert.ToDouble(ds5.Tables[0].Rows[1]["cntval"]);
            dcntlim3 = Convert.ToDouble(ds5.Tables[0].Rows[2]["cntval"]);
            double dcnumber = Convert.ToDouble(ds5.Tables[1].Rows[0]["cnumber"]);

            DateTime dt1 = Convert.ToDateTime(ds3.Tables[0].Rows[0]["expdate"]);
            DateTime dt2 = System.DateTime.Today;
            day = ASTUtility.Datediffday(dt1, dt2);

            if (ds5.Tables[2].Rows.Count != 0)
            {
                string commsg = ds5.Tables[2].Rows[0]["commsg"].ToString();
                string msgCol = ds5.Tables[2].Rows[0]["commsgcol"].ToString(); //"text-danger";
                if (commsg.Length != 1)
                {
                    //this.pnlmsgbox.Visible = true;
                    //this.lblalrtmsg.InnerHtml = "<span class='glyphicon glyphicon-alert'></span> " + commsg;
                    //this.lblalrtmsg.Attributes.Add("class", msgCol + " text-justify notificationmsg");
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ commsg + "');", true);

                }
            }
            else
            {
                //this.pnlmsgbox.Visible = false;

            }



            if ((cnumber >= cntlim1 && cnumber < cntlim2) || (day < 10) || (dcnumber >= dcntlim1 && dcnumber < dcntlim2))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);


            }
            else if ((cnumber >= cntlim2 && cnumber < cntlim3) || (day <= 5) || (dcnumber >= dcntlim2 && cnumber < dcntlim3))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);

         

            }


            else if ((cnumber >= cntlim3) || (day <= 0) || (dcnumber >= dcntlim3))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);

              

            }




        }




        ////private void GetExpDate() 
        ////{

        ////    //Access Database
        ////    UserLogin ulog = new UserLogin();
        ////    DataSet ds1 = ulog.ExpDate();
        ////    DateTime dt1 = Convert.ToDateTime(ds1.Tables[0].Rows[0]["expdate"]);
        ////    DateTime dt2 = System.DateTime.Today;
        ////    DateTime dtm=System.DateTime.Today.AddDays(15);
        ////    DateTime dtw = System.DateTime.Today.AddDays(7);


        ////    if (dt1 < dtm)
        ////    {
        ////        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repaired Selected File.');", true);

        ////    }
        ////    else if (dt1 < dtw) 
        ////    {
        ////        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repaired Selected File.');", true);

        ////    }


        ////    else  if (dt1 < dt2)
        ////    {
        ////        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repaired Selected File.');", true);

        ////    }



        ////}
        ///
        
        protected void cahngeBtn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ChPaModalOpen();", true);
        }
        private void getComName()
        {
            //Access Database (List View)
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            this.listComName.DataTextField = "comnam";
            this.listComName.DataValueField = "comcod";
            this.listComName.DataSource = ds1.Tables[0];
            this.listComName.DataBind();

            //if (this.listComName.Items.Count > 0)
            //{
            //    // DataRow[] dr = ds1.Tables[0].Select("comcod like '3%'");
            //    DataView dv = ds1.Tables[0].DefaultView;
            //    dv.RowFilter = ("comcod like '5106%'");
            //    dv.Sort = "slnum";
            //    DataTable dt1 = dv.ToTable();

            //    if (dt1.Rows.Count == 0)
            //    {

            //        this.listComName.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        this.listComName.SelectedValue = dt1.Rows[0]["comcod"].ToString();

            //    }
            //}
            Session["tbllog"] = ds1.Tables[0];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;
            string comcod = hst["comcod"].ToString();
            string module = hst["modulenam"].ToString().Trim();
            if (comcod != "")
            {
                this.listComName.SelectedValue = comcod;
            }





            //    //Sql Server
            //    //DataSet ds1 = ulogin.GetTransInfo("", "SP_UTILITY_LOGIN_MGT", "COMPINFORMATION", "", "", "", "", "", "", "", "", "");
            //    //this.listComName.DataTextField = "comnam";
            //    //this.listComName.DataValueField = "comcod";
            //    //this.listComName.DataSource = ds1.Tables[0];
            //    //this.listComName.DataBind();
            //    //if (this.listComName.Items.Count > 0)
            //    //{
            //    //    this.listComName.SelectedIndex = 0;

            //    //}


            //    //Session["tbllog"] = ds1.Tables[0];
            //    //Hashtable hst = (Hashtable)Session["tblLogin"];
            //    //if (hst == null)
            //    //    return;
            //    //string comcod = hst["comcod"].ToString();
            //    //string module = hst["modulenam"].ToString().Trim();
            //    //if (comcod != "")
            //    //{
            //    //    this.listComName.SelectedValue = comcod;
            //    //}




            }


        //private void Initilize()
        //{

        //    UserLogin ulog = new UserLogin();
        //    DataSet ds1 = ulog.GoupCompany();
        //    if (ds1 == null)
        //        return;
        //    ((Label)this.Master.FindControl("LblGrpCompany")).Text = ds1.Tables[0].Rows[0]["comnam"].ToString(); //((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
        //    ((Label)this.Master.FindControl("lbladd")).Text = ds1.Tables[0].Rows[0]["comadd"].ToString();
        //    //((Label)this.Master.FindControl("lblsoftname")).Visible = false;
        //    //((Label)this.Master.FindControl("lblLoginInfo")).Visible = false;
        //    //((Menu)this.Master.FindControl("Menu1")).Visible = false;
        //    //((Image)this.Master.FindControl("UserImg")).Visible = false;

        //}


        //private void CheangePassword()
        //{

        //    //if (!result)
        //    //    this.lblmsg.Text = "Updated Failed";


        //    //else
        //    //{
        //    //    this.lblmsg.Text = "Successfully Updated";
        //    //    this.ChkChangePass.Checked = false;
        //    //    this.ChkChangePass_CheckedChanged(null, null);
        //    //}


        //}

        //protected void ChkChangePass_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.lbloldPass.Visible = this.ChkChangePass.Checked;
        //    this.lblNewPass.Visible = this.ChkChangePass.Checked;
        //    this.txtuserOldrpass.Visible = this.ChkChangePass.Checked;
        //    this.txtuserNewrpass.Visible = this.ChkChangePass.Checked;

        //    //if (this.ChkChangePass.Checked)
        //    //{
        //    //    this.lblPass.Text = "New Password";
        //    //}
        //    //else 
        //    //{
        //    //    this.lblPass.Text = "Password";
        //    //}
        //}

        //private void CheangePassword()
        //{

        //    string comcod = this.listComName.SelectedValue.ToString();
        //    string username = this.txtuserid.Text.Trim();
        //    string oldpass = ASTUtility.EncodePassword(this.txtuserOldrpass.Text.Trim());
        //    string newpass = this.txtuserNewrpass.Text.Trim();
        //    if (newpass.Length == 0)
        //    {
        //        this.lblmsg.Text = "New Password wiil not be empty";
        //        return;
        //    }
        //    else if (newpass.Length < 6)
        //    {
        //        this.lblmsg.Text = "Password length must be Equal or greater 6 digits ";
        //        return;
        //    }


        //    newpass = ASTUtility.EncodePassword(newpass);



        //    ProcessAccess ulogin = (ASTUtility.Left(this.listComName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
        //    DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSEROLDPASS", username, oldpass, "", "", "", "", "", "", "");
        //    if (ds5.Tables[0].Rows.Count == 0)
        //    {
        //        this.lblmsg.Text = "Please Enter Correct UserId & Password";
        //        return;

        //    }


        //    bool result = ulogin.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "CHANGEPASS", username,
        //                   newpass, "", "", "", "", "", "", "", "", "", "", "", "", "");

        //    if (!result)
        //        this.lblmsg.Text = "Updated Failed";


        //    else
        //    {
        //        this.lblmsg.Text = "Successfully Updated";
        //        this.ChkChangePass.Checked = false;
        //        this.ChkChangePass_CheckedChanged(null, null);
        //    }


        //}

        private void MasComNameaAdd()
        {

            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + this.listComName.SelectedValue.ToString() + "'");
          //  ((Label)this.Master.FindControl("CompAddress")).Text = this.listComName.SelectedItem.Text.Trim();// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
         //   ((Label)this.Master.FindControl("CompAddress")).Text = (dr[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? dr[0]["comadd"].ToString().Substring(6) : dr[0]["comadd"].ToString();
            ((Image)this.Master.FindControl("Image1")).ImageUrl = "~/Image/" + "LOGO" + this.listComName.SelectedValue.ToString() + ".png";
         //   ((Image)this.Image1).ImageUrl = "~/Image/" + "LOGO" + this.listComName.SelectedValue.ToString() + ".png";


            //((Label)this.Master.FindControl("LblGrpCompany")).Text = this.listComName.SelectedItem.Text.Trim();// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();

            //((Image)this.Master.FindControl("Image1")).ImageUrl = "~/Image/" + "LOGO" + this.listComName.SelectedValue.ToString() + ".PNG";
        }



        //protected void ChkChangePass_CheckedChanged(object sender, EventArgs e)
        //{

        //    this.lbloldPass.Visible = this.ChkChangePass.Checked;
        //    this.lblNewPass.Visible = this.ChkChangePass.Checked;
        //    this.txtuserOldrpass.Visible = this.ChkChangePass.Checked;
        //    this.txtuserNewrpass.Visible = this.ChkChangePass.Checked;

        //    //if (this.ChkChangePass.Checked)
        //    //{
        //    //    this.lblPass.Text = "New Password";

        //    //}
        //    //else 
        //    //{
        //    //    this.lblPass.Text = "Password";

        //    //}


        //}

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            CommonHelperClass.Current.tblUserMod = null;

            //chcls.DataStatus = "1";
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetHitCounter();
            string comcod = this.listComName.SelectedValue.ToString();
            ProcessAccess ulogin = (ASTUtility.Left(this.listComName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess("ASITHRM") : new ProcessAccess();
            DataSet ds51 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGIN", "", "", "", "", "", "", "", "", "");

            double cnumber = Convert.ToDouble(ds1.Tables[0].Rows[0]["cnumber"]);
            double dcnumber = Convert.ToDouble(ds51.Tables[1].Rows[0]["cnumber"]);
            double cntlim3, dcntlim3;
            cntlim3 = Convert.ToDouble(((DataTable)Session["tblhcntlmt"]).Rows[2]["cntval"]);
            dcntlim3 = Convert.ToDouble(ds51.Tables[0].Rows[2]["cntval"].ToString());
            if ((cnumber >= cntlim3) || (day <= 0) || (dcnumber >= dcntlim3))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);


                return;

            }


            Session.Remove("tblusrlog");
            Session.Remove("tbllog1");
            string username = this.txtuserid.Text.Trim();
            //string pass =this.txtuserpass.Text.Trim();
            string pass = ASTUtility.EncodePassword(this.txtuserpass.Text.Trim());
            string HostAddress = Request.UserHostAddress.ToString();
            //if (this.ChkChangePass.Checked)
            //{
            //    this.CheangePassword();
            //    return;
            //}

            string modulid = "AA";
            string modulename = "All Module";


            DataSet ds5Footbed = null;
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");

            if (ds5.Tables[0].Rows.Count == 1)
            {
                FormsAuthentication.RedirectFromLoginPage(username, false);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Enter Correct Name & Password');", true);
                return;
            }
            if (username == "Footbed")
            {
                ds5Footbed = ulogin.GetTransInfo("5306", "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            }



            //Hit counter Update
            //  bool result=ulog.UpdateHitCounter((cnumber + 1).ToString());
            //ulog.UpdateHitCounter((cnumber + 1).ToString());
            //if (!reuslt)
            //{
            //    this.lblmsg.Text = ulog.ErrorObject["Msg"].ToString();
            //    return;

            //}


            //

            bool result = ulogin.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "UPDATEHCNTLMT", (dcnumber + 1).ToString(),
                             "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                return;

            }

            Session["tblusrlog"] = ds5;
            if (Request.QueryString["index"] != null)
                ind = Request.QueryString["index"].ToString();

            //if (Request.QueryString["index"] == null)
            //    //ind = this.ListModulename.SelectedIndex.ToString();


            string Comcode = this.listComName.SelectedValue.ToString();
            string ComName = this.listComName.SelectedItem.Text;

            //string Mname = this.ListModulename.SelectedValue.ToString();
            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = new DataTable();
            if ((DataTable)Session["tbllog1"] == null)
            {
                dt2.Columns.Add("comcod", Type.GetType("System.String"));
                dt2.Columns.Add("comnam", Type.GetType("System.String"));
                dt2.Columns.Add("comsnam", Type.GetType("System.String"));
                dt2.Columns.Add("comadd1", Type.GetType("System.String"));
                dt2.Columns.Add("comadd", Type.GetType("System.String"));
                dt2.Columns.Add("comaddf", Type.GetType("System.String"));
                dt2.Columns.Add("usrsname", Type.GetType("System.String"));
                dt2.Columns.Add("session", Type.GetType("System.String"));
                //dt2.Columns.Add("usrdesig", Type.GetType("System.String"));
                Session["tbllog1"] = dt2;
            }

            DataTable dtr = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + Comcode + "'");
            Hashtable hst = new Hashtable();
            if (dr.Length > 0)
            {
                if (username == "Footbed")
                {
                    DataRow[] drF = dt1.Select("comcod='5306'");
                    hst["comnam"] = drF[0]["comnam"];
                    hst["comnam"] = drF[0]["comnam"];
                    hst["comsnam"] = drF[0]["comsnam"];
                    hst["comadd1"] = drF[0]["comadd1"];
                    hst["comweb"] = drF[0]["comadd3"];
                    hst["combranch"] = drF[0]["combranch"];
                    hst["comadd"] = drF[0]["comadd"];
                    hst["comaddf"] = drF[0]["comaddf"];
                    DataRow dr2 = dt2.NewRow();
                    dr2["comcod"] = Comcode;
                    dr2["comnam"] = drF[0]["comnam"];
                    dr2["comsnam"] = drF[0]["comsnam"];
                    dr2["comadd1"] = drF[0]["comadd1"];
                    dr2["comadd"] = drF[0]["comadd"];
                    dr2["comaddf"] = drF[0]["comaddf"];
                    dt2.Rows.Add(dr2);
                }
                else
                {
                    hst["comnam"] = dr[0]["comnam"];
                    hst["comnam"] = dr[0]["comnam"];
                    hst["comsnam"] = dr[0]["comsnam"];
                    hst["comadd1"] = dr[0]["comadd1"];
                    hst["comweb"] = dr[0]["comadd3"];
                    hst["combranch"] = dr[0]["combranch"];
                    hst["comadd"] = dr[0]["comadd"];
                    hst["comaddf"] = dr[0]["comaddf"];
                    DataRow dr2 = dt2.NewRow();
                    dr2["comcod"] = Comcode;
                    dr2["comnam"] = dr[0]["comnam"];
                    dr2["comsnam"] = dr[0]["comsnam"];
                    dr2["comadd1"] = dr[0]["comadd1"];
                    dr2["comadd"] = dr[0]["comadd"];
                    dr2["comaddf"] = dr[0]["comaddf"];
                    dt2.Rows.Add(dr2);
                }


            }
            if (username == "Footbed")
            {
                string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
                hst["comcod"] = Comcode;
                // hst["comnam"] = ComName;
                hst["modulenam"] = "";
                hst["username"] = ds5Footbed.Tables[0].Rows[0]["usrsname"];
                hst["usrname"] = ds5Footbed.Tables[0].Rows[0]["usrname"];
                hst["compname"] = HostAddress;
                hst["comnambn"] = ds5Footbed.Tables[0].Rows[0]["comnambn"];
                hst["comaddbn"] = ds5Footbed.Tables[0].Rows[0]["comaddbn"];
                hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
                hst["password"] = this.txtuserpass.Text;
                hst["session"] = sessionid;
                hst["trmid"] = "";
                hst["commod"] = "1";
                hst["compsms"] = ds5Footbed.Tables[0].Rows[0]["compsms"];
                hst["empid"] = ds5Footbed.Tables[0].Rows[0]["empid"];
                hst["usrdesig"] = ds5Footbed.Tables[0].Rows[0]["usrdesig"];
                hst["events"] = ds5Footbed.Tables[0].Rows[0]["eventspanel"];
                hst["userrole"] = ds5Footbed.Tables[0].Rows[0]["userrole"];
                hst["season"] = ds5Footbed.Tables[0].Rows[0]["season"];
                Session["tblLogin"] = hst;
                dt2.Rows[0]["usrsname"] = ds5Footbed.Tables[0].Rows[0]["usrsname"];
                dt2.Rows[0]["session"] = sessionid;
                Session["tbllog1"] = dt2;

                //Log Report

                string eventtype = "";
                string eventdesc = "Login into the system";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                string userrole = ds5Footbed.Tables[0].Rows[0]["userrole"].ToString();
                string Url1 = "Dashboard";
                Response.Redirect(Url1);
            }
            else
            {
                string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
                hst["comcod"] = Comcode;
                // hst["comnam"] = ComName;
                hst["modulenam"] = "";
                hst["username"] = ds5.Tables[0].Rows[0]["usrsname"];
                hst["usrname"] = ds5.Tables[0].Rows[0]["usrname"];
                hst["compname"] = HostAddress;
                hst["comnambn"] = ds5.Tables[0].Rows[0]["comnambn"];
                hst["comaddbn"] = ds5.Tables[0].Rows[0]["comaddbn"];
                hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
                hst["password"] = this.txtuserpass.Text;
                hst["session"] = sessionid;
                hst["trmid"] = "";
                hst["commod"] = "1";
                hst["compsms"] = ds5.Tables[0].Rows[0]["compsms"];
                hst["empid"] = ds5.Tables[0].Rows[0]["empid"];
                hst["usrdesig"] = ds5.Tables[0].Rows[0]["usrdesig"];
                hst["events"] = ds5.Tables[0].Rows[0]["eventspanel"];
                hst["userrole"] = ds5.Tables[0].Rows[0]["userrole"];
                hst["season"] = ds5.Tables[0].Rows[0]["season"];
                Session["tblLogin"] = hst;
                dt2.Rows[0]["usrsname"] = ds5.Tables[0].Rows[0]["usrsname"];
                dt2.Rows[0]["session"] = sessionid;
                Session["tbllog1"] = dt2;

                //Log Report

                string eventtype = "";
                string eventdesc = "Login into the system";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                string userrole = ds5.Tables[0].Rows[0]["userrole"].ToString();
                string Url1 = "Dashboard";
                Response.Redirect(Url1);
            }


            //if (userrole == "2")
            //{
            //    Url1 = "AllGraph.aspx";
            //}
            //else if (userrole == "4")
            //{
            //    Url1 = "HrWinMenu.aspx";
            //}
            //else
            //{
            //   Url1 = "MyDashboard.aspx?Type=";
            //    string UComcode = ASTUtility.Left(Comcode, 1);
            //    if (UComcode == "3")
            //    {
            //        Url1 += "5000";
            //    }
            //    else if (UComcode == "4")
            //    {
            //        Url1 += "7000";
            //    }
            //    else
            //    {
            //        Url1 += "8000";

            //    }
            //}


        }


        protected void listComName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MasComNameaAdd();

        }

        protected void ModalUpdateBtn_Click(object sender, EventArgs e)
        {

            string comcod = this.listComName.SelectedValue.ToString();
            string username = this.txtuserid.Text.Trim();
            string oldpass = ASTUtility.EncodePassword(this.txtuserOldrpass.Text.Trim());
            string newpass = this.txtuserNewrpass.Text.Trim();
            if (newpass.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('New Password wiil not be empty');", true);
                //this.lblmsg.Text = "";
                return;
            }
            else if (newpass.Length < 6)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Password length must be Equal or greater 6 digits');", true);

                //this.lblmsg.Text = " ";
                return;
            }

            //


            newpass = ASTUtility.EncodePassword(newpass);



            ProcessAccess ulogin = (ASTUtility.Left(this.listComName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess("ASITHRM") : new ProcessAccess();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSEROLDPASS", username, oldpass, "", "", "", "", "", "", "");
            if (ds5.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Enter Correct UserId & Password');", true);

                //this.lblmsg.Text = "Please Enter Correct UserId & Password";
                return;

            }


            bool result = ulogin.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "CHANGEPASS", username,
                           newpass, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Password Change Successfully');", true);

            }
        }


        //protected void ForgotSubmit_click(object sender, EventArgs e)
        //{
        //    ProcessAccess UserLoginCl = new ProcessAccess();
        //    string comcod = this.listComName.SelectedValue.ToString();
        //    string company = this.listComName.SelectedItem.ToString();
        //    string UsrName = this.TxtUserName.Text.ToString();
        //    string usrEmail = this.TxtEmail.Text.ToString();
        //    string UsrPhone = this.TxtPhone.Text.ToString();
        //    string url = HttpContext.Current.Request.Url.AbsoluteUri;
        //    DataSet ds = UserLoginCl.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "CHECKUSERFORRESET", UsrName, usrEmail, UsrPhone, "", "", "", "", "", "");

        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        string mobilenum = ds.Tables[0].Rows[0]["mobile"].ToString();

        //        Random rnd = new Random();
        //        int rndnumber = rnd.Next();

        //        string Pass = ASTUtility.EncodePassword(rndnumber.ToString());

        //        DataSet dssmtpandmail = UserLoginCl.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAILSYS", "", "", "", "", "", "", "", "", "");

        //        string comsmsstatus = dssmtpandmail.Tables[2].Rows[0]["compsms"].ToString();
        //        string userid = ds.Tables[0].Rows[0]["usrid"].ToString();

        //        //SMTP
        //        string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
        //        int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
        //        SmtpClient client = new SmtpClient(hostname, portnumber);
        //        //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        //client.EnableSsl = true;
        //        client.EnableSsl = false;
        //        string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
        //        string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
        //        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = credentials;
        //        ///////////////////////

        //        MailMessage msg = new MailMessage();
        //        msg.From = new MailAddress(frmemail);


        //        string body = string.Empty;


        //        msg.To.Add(new MailAddress(usrEmail));

        //        msg.Subject = "Reset Password";
        //        msg.IsBodyHtml = true;
        //        using (StreamReader reader = new StreamReader(Server.MapPath("~/forgetMsgtemp.html")))
        //        {

        //            body = reader.ReadToEnd();

        //        }
        //        body = body.Replace("{UserName}", UsrName); //replacing the required things  

        //        body = body.Replace("{pass}", rndnumber.ToString());
        //        body = body.Replace("{company}", company);
        //        body = body.Replace("{url}", url);

        //        //  body = body.Replace("{message}", message);
        //        msg.Body = body;

        //        // msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Hello," + UsrName + "<br/>" + "please Collect Your Password " + rndnumber + "</pre></body></html>");
        //        try
        //        {
        //            bool result = UserLoginCl.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "UPDATEUSERPASS", Pass, userid, "", "", "", "", "", "", "", "", "", "", "", "", "");
        //            if (result == true)
        //            {
        //                string rbtnindex = this.rbtnAtten.SelectedIndex.ToString();
        //                switch (rbtnindex)
        //                {

        //                    case "0":
        //                        client.Send(msg);
        //                        this.lblmsg.Visible = true;
        //                        this.lblmsg.CssClass = "alert alert-success col-sm-12";
        //                        this.lblmsg.Text = " <span class='glyphicon glyphicon-ok'></span>  Please check Your Email";

        //                        break;
        //                    case "1":
        //                        if (comsmsstatus == "True")
        //                        {
        //                            SendSmsProcess sms = new SendSmsProcess();
        //                            string SMSText = "Congratulations! Your New Password is: " + rndnumber.ToString() + "\n\n" + " Thanking By " + company;
        //                            bool resultsms = sms.SendSmmsPwd(comcod, SMSText, mobilenum);
        //                        }
        //                        this.lblmsg.Visible = true;
        //                        this.lblmsg.CssClass = "alert alert-success col-sm-12";
        //                        this.lblmsg.Text = " <span class='glyphicon glyphicon-ok'></span>  Please check Your Mobile";
        //                        break;
        //                }



        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            this.lblmsg.Visible = true;
        //            this.lblmsg.CssClass = "alert alert-danger col-sm-12";
        //            this.lblmsg.Text = " <span class='glyphicon glyphicon-warning-sign'></span>  Error occured while sending your message." + ex.Message;

        //        }


        //    }
        //    else
        //    {
        //        this.lblmsg.Visible = true;
        //        this.lblmsg.CssClass = "alert alert-danger col-sm-12";
        //        this.lblmsg.Text = "<span class='glyphicon glyphicon-warning-sign'></span>   Sorry Your Given Information Are Invalid";
        //    }
        //}
    }
}