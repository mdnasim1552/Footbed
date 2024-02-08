using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SPEWEB
{
    public partial class SPE : System.Web.UI.MasterPage
    {
        ProcessAccess ulogin = new ProcessAccess();
    
        CommonHelperClass chcls = new CommonHelperClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tblusrlog"] == null)
            {
                Response.Redirect("~/ErrorHandling.aspx?Type=SDestroy");

            }
            else if (Session["tbllog1"] == null)
            {
                Response.Redirect("~/ErrorHandling.aspx?Type=SDestroy");

            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst.Count == 0)
                {
                    Response.Redirect("~/ErrorHandling.aspx?Type=SDestroy");

                }
            }
            if (!IsPostBack)
            {
                ((Image)this.FindControl("ComLogo")).ImageUrl = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
                this.ParentDir.Text = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;


                GetCompanyName();
                GetModulename();
                this.GetShortCut();

            }
            
        }
        public string GetUserId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];           
            return (hst["usrid"].ToString());
                      
        }
        private void GetModulename()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;

            string usrid = hst["usrid"].ToString();
            // string usrperm = "1";
            this.LogoBar.Attributes.Add("href", path + "/Dashboard.aspx");

            DataSet ds1;
            DataSet ds2 = CommonHelperClass.Current.tblUserMod;
            if (ds2 == null)
            {
                ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", usrid, "", "", "", "", "", "", "", "");

                CommonHelperClass.Current.tblUserMod = ds1;
            }
            else
            {
                ds1 = CommonHelperClass.Current.tblUserMod;
            }





            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("moduleid<>'AA' AND usrper='True'");
            string imageHTML = "";
            foreach (DataRow dr in dv.ToTable().Rows)
            {
                imageHTML += @"<li class='menu-item'>
                        <a href ='" + path + "/StepofOperationNew?Module=" + dr["moduleid"] + "' class='menu-link'>" + dr["modulename"] + "</a></li>";
            }

            this.Module.InnerHtml = imageHTML;
            // for interfce//
            DataSet dtint = (DataSet)Session["tblusrlog"];
            DataView dvint = dtint.Tables[1].DefaultView;
            dvint.RowFilter = ("interface='I'");
            DataTable dtnew = dvint.ToTable();

            //if (this.GetCompCode() == "5305")
            //{
            //    dtnew.Rows[2][3] = "0000000";

            //}
            DataView dvnew = dtnew.DefaultView;
            dvnew.Sort = ("slno, frmid");

            string interfacehtml = "";

            foreach (DataRow dr in dvnew.ToTable().Rows)
            {
                interfacehtml += @"<li class='menu-item'><a href ='" + path + "/" + dr["floc"] + "/" + dr["urlinf"] + "' class='menu-link' target='_self'>" + dr["dscrption"] + "</a></li>";
            }

            this.Interface.InnerHtml = interfacehtml;

            // for All in One//

            DataView dvAll = dtint.Tables[1].Copy().DefaultView;
            dvAll.RowFilter = ("interface='M'");
            if (dvAll.ToTable().Rows.Count != 0)
            {
                this.dbAllinOne.Visible = true;
                //this.dbAllinOne.HRef = path + "/" + dvAll.ToTable().Rows[0]["floc"] + "/" + dvAll.ToTable().Rows[0]["urlinf"];
                this.dbAllinOne.HRef = path+"/" +  dvAll.ToTable().Rows[0]["urlinf"];
            }


            // for Dashboard//

            DataView dvDas = dtint.Tables[1].Copy().DefaultView;
            dvDas.RowFilter = ("interface='D'");
            string dashboardhtml = "";

            foreach (DataRow dr in dvDas.ToTable().Rows)
            {
                dashboardhtml += @"<li class='menu-item'><a href ='" + path + "/" + dr["floc"] + "/" + dr["urlinf"] + "' class='menu-link' target='_self'>" + dr["dscrption"] + "</a></li>";
            }
            
            this.dbDashboard.InnerHtml = dashboardhtml;
            if (dashboardhtml.Length == 0)
            {
                this.DashboardArea.Visible = false;
            }
            // for Analysis//

            DataView dvAna = dtint.Tables[1].Copy().DefaultView;
            dvAna.RowFilter = ("interface='A'");
            string Analysishtml = "";

            foreach (DataRow dr in dvAna.ToTable().Rows)
            {
                Analysishtml += @"<li class='menu-item'><a href ='" + path + "/" + dr["floc"] + "/" + dr["urlinf"] + "' class='menu-link' target='_self'>" + dr["dscrption"] + "</a></li>";
            }

            this.dbGraph.InnerHtml = Analysishtml;
            if (Analysishtml.Length == 0)
            {
                this.AnalysisArea.Visible = false;
            }
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "manipulate();", true);
            // Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "manipulate();", true);
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "manipulate();", true);

            //((Label)this.FindControl("lblprintstk1")).Text = @"<script>manipulate();</script>";

            Designation.InnerHtml = hst["usrdesig"].ToString();
            //UserName.InnerHtml = "Hi, " + hst ["usrname"].ToString();
            UserName1.InnerHtml = hst["username"].ToString();
            UserName2.InnerHtml = hst["username"].ToString();
            UserName3.InnerHtml = hst["username"].ToString();



        }
        private void GetCompanyName()
        {

            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            this.ddlCompanyName.DataTextField = "comsnam";
            this.ddlCompanyName.DataValueField = "comcod";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.ddlCompanyName.SelectedValue = this.GetCompCode();
           


        }

        private void GetShortCut()
        {
            string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            DataSet ds = ((DataSet)Session["tblusrlog"]);
            DataTable dt1 = ds.Tables[1];
            DataTable shrtcuttble = ds.Tables[1].Clone();
            DataTable dt3 = ds.Tables[3];
            if (dt3.Rows.Count == 0 || dt3 == null)
                return;
            foreach (DataRow dr in dt3.Rows)
            {
                DataRow[] srows = dt1.Select("frmid = '" + dr["formid"].ToString().Trim() + "'");
                if( srows.Length > 0)
                {
                    shrtcuttble.ImportRow(srows[0]);

                }
            }
            DataTable newdt = shrtcuttble;

            string MyShortCut = "";

            foreach (DataRow dr in shrtcuttble.Rows)
            {
                MyShortCut += @"<li class='menu-item'><a href ='" + path + "/" + dr["floc"] + "/" + dr["urlinf"] + dr["qrytype1"] + "' class='menu-link' target='_self'>" + dr["dscrption"] + "</a></li>";
            }

            this.ShorCut.InnerHtml = MyShortCut;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lnkbtnLedger_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnHisprice_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnTranList_Click(object sender, EventArgs e)
        {

        }

        protected void chkBoxN_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnAdd_Click1(object sender, EventArgs e)
        {

        }
        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {


        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {


            System.Web.HttpContext.Current.Session.Remove("tblMainCenter");
            System.Web.HttpContext.Current.Session.Remove("tblMainTax");
            System.Web.HttpContext.Current.Session.Remove("tblMainTeam");
            System.Web.HttpContext.Current.Session.Remove("tblMainCustomer");

            this.GetUserPermission();
            this.GetModulename();
         

            
        }
        private void GetUserPermission()
        {




            ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompanyName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
            string comcod = this.ddlCompanyName.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string txtuserpass = hst["password"].ToString();
            string pass = ASTUtility.EncodePassword(txtuserpass);

            //        string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = "";// this.ddlModuleName.SelectedValue.ToString();
            string modulename = "";// this.ddlModuleName.SelectedItem.Text.Trim();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            Session["tblusrlog"] = ds5;
            string Comcode = this.ddlCompanyName.SelectedValue.ToString();
            string ComName = this.ddlCompanyName.SelectedItem.ToString();

            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + Comcode + "'");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            if (dr.Length > 0)
            {
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];

                dt2.Rows[0]["comcod"] = dr[0]["comcod"];
                dt2.Rows[0]["comnam"] = dr[0]["comnam"];
                dt2.Rows[0]["comsnam"] = dr[0]["comsnam"];
                dt2.Rows[0]["comadd1"] = dr[0]["comadd1"];
                dt2.Rows[0]["comadd"] = dr[0]["comadd"];
            }

            hst["comcod"] = Comcode;
            //  hst["comnam"] = ComName;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["modulenam"] = "";// this.ddlModuleName.SelectedValue.ToString();
            hst["trmid"] = "";
            string HostAddress = Request.UserHostAddress.ToString();
            string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
            hst["comcod"] = Comcode;
            // hst["comnam"] = ComName;
            hst["modulenam"] = "";
            hst["username"] = ds5.Tables[0].Rows[0]["usrsname"];
            hst["usrname"] = ds5.Tables[0].Rows[0]["usrname"];
            hst["compname"] = HostAddress;
            hst["comnambn"] = ds5.Tables[0].Rows[0]["comnambn"];
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["password"] = txtuserpass;
            hst["session"] = sessionid;
            hst["trmid"] = "";
            hst["commod"] = "1";
            hst["compsms"] = ds5.Tables[0].Rows[0]["compsms"];
            hst["empid"] = ds5.Tables[0].Rows[0]["empid"];
            hst["usrdesig"] = ds5.Tables[0].Rows[0]["usrdesig"];
            hst["events"] = ds5.Tables[0].Rows[0]["eventspanel"];
            hst["userrole"] = ds5.Tables[0].Rows[0]["userrole"];
            Session["tblLogin"] = hst;
            Session["tbllog1"] = dt2;

            Response.Redirect(Request.Url.AbsoluteUri);

        }
    }
}