using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using SPEENTITY;
using System.Collections;
using System;

namespace SPEWEB
{
    public partial class StepofOperationNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if((DataSet)Session["tblusrlog"]==null)
                {
                    Response.Redirect("~/login");
                }
                ((Label)this.Master.FindControl("lblTitle")).Text = "STEPS OF OPERATIONS";
                string ModuleID = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
                string ModuleID2 = (this.Request.QueryString["Module"].ToString().Length > 0) ? this.Request.QueryString["Module"] : ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();
                string comcod = ASTUtility.Left(((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["comcod"].ToString().Trim(), 1);

                this.GetCompanyName();
                DataTable dt = (DataTable)ViewState["tblmoduleName"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "usrper = '" + "true" + "'";
                DataTable dt1 = dv.ToTable();
                string TopModule = (this.Request.QueryString["Module"].ToString().Length > 0) ? this.Request.QueryString["Module"] : dt1.Rows[0]["moduleid"].ToString();

                if (ModuleID2 == "AA")
                    this.ddlModuleName.SelectedValue = TopModule;

                else if (ModuleID2 != "AA")
                    this.ddlModuleName.SelectedValue = ModuleID2;


                this.ddlModuleName_SelectedIndexChanged(null, null);




                // string SModuleID = dt1.Rows[0]["moduleid"].ToString();

            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //   ((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            // ((Panel)this.Master.FindControl("pnlPrint")).Visible = false;


            //  ((Panel)this.Master.FindControl("pnlTitle")).

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
            this.GetModulename();
            this.ModuleVisible();
            //this.MasComNameAndAdd();


        }
        private void GetModulename()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            ProcessAccess ulogin = new ProcessAccess();
            string usrid = hst["usrid"].ToString();
            // string usrperm = "1"; 

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



            //DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", usrid, "", "", "", "", "", "", "", "");

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("moduleid<>'AA'");

            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = dv.ToTable();
            this.ddlModuleName.DataBind();


            ViewState["tblmoduleName"] = ds1.Tables[0];

        }
        private void MasComNameAndAdd()
        {
            string comcod = this.ddlCompanyName.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");

            // ((Label)this.Master.FindControl("LblGrpCompany")).Text = this.ddlCompanyName.SelectedItem.Text.Trim();// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            //   ((Label)this.Master.FindControl("lbladd")).Text = (dr[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? dr[0]["comadd"].ToString().Substring(6) : dr[0]["comadd"].ToString();
            //  ((Image)this.Master.FindControl("Image1")).ImageUrl = "~/Image/" + "LOGO" + comcod + ".PNG";

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
            string modulid = this.ddlModuleName.SelectedValue.ToString();
            string modulename = this.ddlModuleName.SelectedItem.Text.Trim();
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
            hst["modulenam"] = this.ddlModuleName.SelectedValue.ToString();
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

            //string Url1 = "MyDashboard.aspx?Type=";

            //string UComcode = ASTUtility.Left(Comcode, 1);
            //if (UComcode == "3")
            //{
            //    Url1 += "8000";
            //}
            //else if (UComcode == "9")
            //{
            //    Url1 = "";
            //    Url1 = "DeafultMenu.aspx?Type=9000";
            //}
            //else
            //{
            //    Url1 += "5000";

            //}

            //Response.Redirect(Url1);



        }
        private void ModuleVisible()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string CompName = this.ddlCompanyName.SelectedValue.ToString();
            string CompType = ASTUtility.Left(CompName, 1);
            string commod = hst["commod"].ToString();
            DataTable dt = (DataTable)ViewState["tblmoduleName"];

            this.ddlModuleName.Items[0].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[0]["usrper"])) && (Convert.ToBoolean(dt.Rows[0]["flag"])));

            this.ddlModuleName.Items[1].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[1]["usrper"])) && (Convert.ToBoolean(dt.Rows[1]["flag"])));

            this.ddlModuleName.Items[2].Enabled = ((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[2]["usrper"])) && (Convert.ToBoolean(dt.Rows[2]["flag"]));
            this.ddlModuleName.Items[3].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[3]["usrper"])) && (Convert.ToBoolean(dt.Rows[3]["flag"])));
            this.ddlModuleName.Items[4].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[4]["usrper"])) && (Convert.ToBoolean(dt.Rows[4]["flag"])));
            this.ddlModuleName.Items[5].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[5]["usrper"]))) && (Convert.ToBoolean(dt.Rows[5]["flag"]));

            this.ddlModuleName.Items[6].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[6]["usrper"])) && (Convert.ToBoolean(dt.Rows[6]["flag"])));

            this.ddlModuleName.Items[7].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[7]["usrper"])) && (Convert.ToBoolean(dt.Rows[7]["flag"])));
            this.ddlModuleName.Items[8].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[8]["usrper"])) && (Convert.ToBoolean(dt.Rows[8]["flag"])));

            this.ddlModuleName.Items[9].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[9]["usrper"])) && (Convert.ToBoolean(dt.Rows[9]["flag"])));


            this.ddlModuleName.Items[10].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[10]["usrper"])) && (Convert.ToBoolean(dt.Rows[10]["flag"])));
            this.ddlModuleName.Items[11].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[11]["usrper"])) && (Convert.ToBoolean(dt.Rows[11]["flag"])));
            this.ddlModuleName.Items[12].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[12]["usrper"])) && (Convert.ToBoolean(dt.Rows[12]["flag"])));
            this.ddlModuleName.Items[13].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[13]["usrper"])) && (Convert.ToBoolean(dt.Rows[13]["flag"])));

            this.ddlModuleName.Items[14].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[14]["usrper"])) && (Convert.ToBoolean(dt.Rows[14]["flag"])));
            this.ddlModuleName.Items[15].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[15]["usrper"])) && (Convert.ToBoolean(dt.Rows[15]["flag"])));
            this.ddlModuleName.Items[16].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[16]["usrper"])) && (Convert.ToBoolean(dt.Rows[16]["flag"])));

            ////
            this.ddlModuleName.Items[17].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[17]["usrper"])) && (Convert.ToBoolean(dt.Rows[17]["flag"])));
            this.ddlModuleName.Items[18].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[18]["usrper"])) && (Convert.ToBoolean(dt.Rows[18]["flag"])));
            this.ddlModuleName.Items[19].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[19]["usrper"])) && (Convert.ToBoolean(dt.Rows[19]["flag"])));
            this.ddlModuleName.Items[20].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[20]["usrper"])) && (Convert.ToBoolean(dt.Rows[20]["flag"])));
            this.ddlModuleName.Items[21].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[21]["usrper"])) && (Convert.ToBoolean(dt.Rows[21]["flag"])));
            this.ddlModuleName.Items[22].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[22]["usrper"])) && (Convert.ToBoolean(dt.Rows[22]["flag"])));
            this.ddlModuleName.Items[23].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[23]["usrper"])) && (Convert.ToBoolean(dt.Rows[23]["flag"])));
            this.ddlModuleName.Items[24].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[24]["usrper"])) && (Convert.ToBoolean(dt.Rows[24]["flag"])));
            this.ddlModuleName.Items[25].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[25]["usrper"])) && (Convert.ToBoolean(dt.Rows[25]["flag"])));
            this.ddlModuleName.Items[26].Enabled = true;


        }

        protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ModuleID = this.ddlModuleName.SelectedValue.ToString().Trim();
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            this.txtmodid.Text = this.ddlModuleName.SelectedValue.ToString();
            this.modulenam.Text = this.ddlModuleName.SelectedItem.Text;
            ds1.Tables[0].Rows[0]["ModuleID2"] = ModuleID;
            ds1.Tables[0].Rows[0]["modulename"] = this.ddlModuleName.SelectedItem.Text;
            Session["tblusrlog"] = ds1;

        }
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session.Remove("tblMainCenter");
            System.Web.HttpContext.Current.Session.Remove("tblMainTax");
            System.Web.HttpContext.Current.Session.Remove("tblMainTeam");
            System.Web.HttpContext.Current.Session.Remove("tblMainCustomer");

            this.GetUserPermission();
            this.GetModulename();
            this.ModuleVisible();

            DataTable dt = (DataTable)ViewState["tblmoduleName"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "usrper = '" + "true" + "'";
            DataTable dt1 = dv.ToTable();
            string TopModule = dt1.Rows[0]["moduleid"].ToString();
            this.ddlModuleName.SelectedValue = TopModule;
            //this.MasComNameAndAdd();
            this.ddlModuleName_SelectedIndexChanged(null, null);
            DataTable logdt = (DataTable)Session["tbllog1"];
            ((Image)this.Master.FindControl("ComLogo")).ImageUrl = "~/Image/" + "LOGO" + logdt.Rows[0]["comcod"].ToString() + ".PNG";
        }

    }
}