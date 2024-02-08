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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using SPEENTITY;
using SPELIB;

namespace SPEWEB.F_34_Mgt
{
    public partial class UserfrmGroup : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                //----------------udate-20150120---------
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "GROUP USER MANAGEMENT";

               
                this.ShowUserInfo();

                this.getListModulename();
              //  this.ModuleVisible();
                this.GetCompPermission();
                this.CommonButton();
            }
        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;


        }
        private void GetCompPermission()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string usrid = hst["usrid"].ToString();


            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORMASIT", usrid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }


            Session["tblcompper"] = ds2.Tables[0];

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //  ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void ibtnFindName_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowUserInfo();

        }
        private void getListModulename()
        {

            string comcod = this.GetComeCode();
            ProcessAccess ulogin = new ProcessAccess();
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", "", "", "", "", "", "", "", "", "");

            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = ds1.Tables[0];
            this.ddlModuleName.DataBind();
            //ViewState["tblmoduleName"] = ds51.Tables[0];
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void ModuleVisible()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string CompName = this.GetComeCode();
            string CompType = ASTUtility.Left(CompName, 1);
            string commod = hst["commod"].ToString();
            DataTable dt = (DataTable)ViewState["tblmoduleName"];


            this.ddlModuleName.Items[0].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[0]["usrper"])) && (Convert.ToBoolean(dt.Rows[0]["flag"]));
                                                                                                                                     //this.ddlModuleName.Items[1].Enabled = ((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.Items[1].Enabled = (((CompType == "8") && (commod == "1")));//;// && (Convert.ToBoolean(dt.Rows[1]["usrper"]));// && (Convert.ToBoolean(dt.Rows[1]["flag"]));
                                                                                           //this.ddlModuleName.Items[3].Enabled = ((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.Items[2].Enabled = ((CompType == "8") && (commod == "1"));// && (Convert.ToBoolean(dt.Rows[2]["usrper"])) ;//&& (Convert.ToBoolean(dt.Rows[2]["flag"]));
            this.ddlModuleName.Items[3].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[3]["usrper"])) && (Convert.ToBoolean(dt.Rows[3]["flag"]));
            this.ddlModuleName.Items[4].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//;// && (Convert.ToBoolean(dt.Rows[4]["usrper"])) && (Convert.ToBoolean(dt.Rows[4]["flag"]));
            this.ddlModuleName.Items[5].Enabled = (((CompType == "8") && (commod == "1"))); // && (Convert.ToBoolean(dt.Rows[0]["usrper"]))) && (Convert.ToBoolean(dt.Rows[5]["usrper"])) && (Convert.ToBoolean(dt.Rows[5]["flag"]));
                                                                                            //this.ddlModuleName.Items[8].Enabled = ((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.Items[6].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[6]["usrper"])) && (Convert.ToBoolean(dt.Rows[6]["flag"]));
                                                                                                                                     //this.ddlModuleName.Items[10].Enabled = ((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.Items[7].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[7]["usrper"])) && (Convert.ToBoolean(dt.Rows[7]["flag"]));
            this.ddlModuleName.Items[8].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[8]["usrper"])) && (Convert.ToBoolean(dt.Rows[8]["flag"]));

            this.ddlModuleName.Items[9].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[9]["usrper"])) && (Convert.ToBoolean(dt.Rows[9]["flag"]));
                                                                                                                                     //this.ddlModuleName.Items[14].Enabled = ((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.Items[10].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[10]["usrper"])) && (Convert.ToBoolean(dt.Rows[10]["flag"]));
            this.ddlModuleName.Items[11].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[11]["usrper"])) && (Convert.ToBoolean(dt.Rows[11]["flag"]));
            this.ddlModuleName.Items[12].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[12]["usrper"])) && (Convert.ToBoolean(dt.Rows[12]["flag"]));
            this.ddlModuleName.Items[13].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[13]["usrper"])) && (Convert.ToBoolean(dt.Rows[13]["flag"]));

            this.ddlModuleName.Items[14].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[14]["usrper"])) && (Convert.ToBoolean(dt.Rows[14]["flag"]));
                                                                                                                                      //this.ddlModuleName.Items[20].Enabled = ((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.Items[15].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[15]["usrper"])) && (Convert.ToBoolean(dt.Rows[15]["flag"]));

            this.ddlModuleName.Items[16].Enabled = (((CompType == "8") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[16]["usrper"])) && (Convert.ToBoolean(dt.Rows[16]["flag"]));
            this.ddlModuleName.Items[17].Enabled = (((CompType == "7") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[17]["usrper"])) && (Convert.ToBoolean(dt.Rows[17]["flag"])); ///Trading Step
            this.ddlModuleName.Items[18].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[18]["usrper"])) && (Convert.ToBoolean(dt.Rows[18]["flag"]));
            this.ddlModuleName.Items[19].Enabled = (((CompType == "9") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[19]["usrper"])) && (Convert.ToBoolean(dt.Rows[19]["flag"]));
            this.ddlModuleName.Items[20].Enabled = (((CompType == "9") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[20]["usrper"])) && (Convert.ToBoolean(dt.Rows[20]["flag"]));


            this.ddlModuleName.Items[21].Enabled = (((CompType == "7") && (commod == "3")) || ((CompType == "8") && (commod == "3")));//&& (Convert.ToBoolean(dt.Rows[21]["usrper"])) && (Convert.ToBoolean(dt.Rows[21]["flag"]));
            this.ddlModuleName.Items[22].Enabled = (((CompType == "7") && (commod == "3")) || ((CompType == "8") && (commod == "3")));//&& (Convert.ToBoolean(dt.Rows[22]["usrper"])) && (Convert.ToBoolean(dt.Rows[22]["flag"]));
            this.ddlModuleName.Items[23].Enabled = (((CompType == "7") && (commod == "3")) || ((CompType == "8") && (commod == "3")));//&& (Convert.ToBoolean(dt.Rows[23]["usrper"])) && (Convert.ToBoolean(dt.Rows[23]["flag"]));
            this.ddlModuleName.Items[24].Enabled = (((CompType == "7") && (commod == "3")) || ((CompType == "8") && (commod == "3")));//&& (Convert.ToBoolean(dt.Rows[24]["usrper"])) && (Convert.ToBoolean(dt.Rows[24]["flag"]));
            this.ddlModuleName.Items[25].Enabled = (((CompType == "7") && (commod == "3")) || ((CompType == "8") && (commod == "3")));//&& (Convert.ToBoolean(dt.Rows[25]["usrper"])) && (Convert.ToBoolean(dt.Rows[25]["flag"]));
            this.ddlModuleName.Items[26].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));// && (Convert.ToBoolean(dt.Rows[26]["usrper"])) && (Convert.ToBoolean(dt.Rows[26]["flag"]));/
            this.ddlModuleName.Items[27].Enabled = (((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1")));//&& (Convert.ToBoolean(dt.Rows[27]["usrper"])) && (Convert.ToBoolean(dt.Rows[27]["flag"]));



            this.ddlModuleName.Items[28].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[28]["flag"]));
            this.ddlModuleName.Items[29].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4"))); // && (Convert.ToBoolean(dt.Rows[29]["usrper"])) && (Convert.ToBoolean(dt.Rows[29]["flag"]));
            this.ddlModuleName.Items[30].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));// && (Convert.ToBoolean(dt.Rows[30]["usrper"])) && (Convert.ToBoolean(dt.Rows[30]["flag"]));
            this.ddlModuleName.Items[31].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[31]["usrper"])) && (Convert.ToBoolean(dt.Rows[31]["flag"]));
            this.ddlModuleName.Items[32].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[32]["usrper"])) && (Convert.ToBoolean(dt.Rows[32]["flag"]));
            this.ddlModuleName.Items[33].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));// && (Convert.ToBoolean(dt.Rows[33]["usrper"])) && (Convert.ToBoolean(dt.Rows[33]["flag"]));
            this.ddlModuleName.Items[34].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));// && (Convert.ToBoolean(dt.Rows[34]["usrper"])) && (Convert.ToBoolean(dt.Rows[34]["flag"]));
            this.ddlModuleName.Items[35].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[35]["usrper"])) && (Convert.ToBoolean(dt.Rows[35]["flag"]));
            this.ddlModuleName.Items[36].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));// && (Convert.ToBoolean(dt.Rows[36]["usrper"])) && (Convert.ToBoolean(dt.Rows[36]["flag"]));
            this.ddlModuleName.Items[37].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[37]["usrper"])) && (Convert.ToBoolean(dt.Rows[37]["flag"]));
            this.ddlModuleName.Items[38].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));// && (Convert.ToBoolean(dt.Rows[38]["usrper"])) && (Convert.ToBoolean(dt.Rows[38]["flag"]));
            this.ddlModuleName.Items[39].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));// && (Convert.ToBoolean(dt.Rows[39]["usrper"])) && (Convert.ToBoolean(dt.Rows[39]["flag"]));
            this.ddlModuleName.Items[40].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[40]["usrper"])) && (Convert.ToBoolean(dt.Rows[40]["flag"]));
            this.ddlModuleName.Items[41].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[41]["usrper"])) && (Convert.ToBoolean(dt.Rows[41]["flag"]));
            this.ddlModuleName.Items[42].Enabled = (((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")));//&& (Convert.ToBoolean(dt.Rows[42]["usrper"])) && (Convert.ToBoolean(dt.Rows[42]["flag"]));


            this.ddlModuleName.Items[43].Enabled = ((CompType == "7") && (commod == "4")) || ((CompType == "8") && (commod == "4")) || ((CompType == "9") && (commod == "1"))
               || ((CompType == "7") && (commod == "1")) || ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.SelectedIndex = 43;

            

        }


        private void ShowUserInfo()
        {
            ViewState.Remove("tblUsrinfo");
            string comcod = GetComeCode();
            string SearcUser = "%" + this.txtSrcName.Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "GETGRPWISEUSER", SearcUser, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvUseForm.DataSource = null;
                this.gvUseForm.DataBind();
                return;
            }
            ViewState["tblUsrinfo"] = ds1.Tables[0];
            this.LoadGrid();


        }



        protected void ibtnFindName_Click(object sender, EventArgs e)
        {
            this.ShowUserInfo();

        }
        private void LoadGrid()
        {

            this.gvUseForm.DataSource = (DataTable)ViewState["tblUsrinfo"];
            this.gvUseForm.DataBind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string usrid = this.lblusrid.Text;

            //DataSet ds1 = User.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "RPTUSRPRFORM", usrid, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;


            //ReportDocument rptcb1 = new MFGRPT.R_34_Mgt.RptUsrPerFrm();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptUname = rptcb1.ReportDefinition.ReportObjects["txtUserName"] as TextObject;
            //rptUname.Text = "User Name: " + this.txtuserid.Text;

            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Date " + System.DateTime.Now.ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void gvUseForm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUseForm.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void ibtnSrchCentr_Click(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl3 = (DropDownList)this.gvUseForm.Rows[rowindex].FindControl("ddlempid");
            string SearchProject = "%" + ((TextBox)gvUseForm.Rows[rowindex].FindControl("txtSrCentrid")).Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo("", "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddl3.DataTextField = "empname";
            ddl3.DataValueField = "empid";
            ddl3.DataSource = ds1;
            ddl3.DataBind();
        }
        protected void lbtnUserId_Click(object sender, EventArgs e)
        {
            this.ddlModuleName.SelectedValue = "AA";
            Session.Remove("tblusrper");
           

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvUseForm.Visible = false;
            this.userDetPan.Visible = false;
            this.MultiView1.ActiveViewIndex = 0;
            string comcod = this.GetComeCode();
            string usrid = Convert.ToString(((LinkButton)sender).Text.Trim());
            this.lblusrid.Text = usrid;
            ///-------------------------///////////
            this.lblId.Visible = true;
            this.txtuserid.Visible = true;
            DataTable tbl01 = (DataTable)ViewState["tblUsrinfo"];
            DataRow[] dr1 = tbl01.Select("grpusrid='" + usrid + "'");
            this.txtuserid.Text = dr1[0]["usrname"].ToString();
            ///-------------------------///////////
            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "SHOWGRPUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.lblusrid.Text = usrid;
                Session["tblusrper"] = this.HiddenSameData(ds2.Tables[0]);
            }
            else
            {



                DataTable dt = (DataTable)Session["tblcompper"];
                Session["tblusrper"] = this.HiddenSameData(dt);

            }
            this.ShowPer();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            //if (dt1.Rows.Count == 0)
            //    return dt1;

            //        string modulename = dt1.Rows[0]["modulename"].ToString();
            //        for (int j = 1; j < dt1.Rows.Count; j++)
            //        {
            //            if (dt1.Rows[j]["modulename"].ToString() == modulename)
            //            {
            //                modulename = dt1.Rows[j]["modulename"].ToString();
            //                dt1.Rows[j]["modulename"] = "";


            //            }

            //            else
            //            {
            //                modulename = dt1.Rows[j]["modulename"].ToString();
            //            }
            //        }



            //return dt1;
            if (dt1.Rows.Count == 0)
                return dt1;

            string modulename = dt1.Rows[0]["modulename"].ToString();
            string frmid1 = dt1.Rows[0]["frmid1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["frmid1"].ToString() == frmid1)
                {
                    dt1.Rows[j]["frmdesc"] = "";

                }


                if (dt1.Rows[j]["modulename"].ToString() == modulename)
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                    dt1.Rows[j]["modulename"] = "";
                }
                else
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                }



                frmid1 = dt1.Rows[j]["frmid1"].ToString();


            }



            return dt1;

        }
        private void ShowPer()
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            this.gvPermission.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPermission.DataSource = (DataTable)Session["tblusrper"];
            this.gvPermission.DataBind();

        }

        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {
            this.chkShowall.Checked = false;
            this.MultiView1.ActiveViewIndex = -1;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvUseForm.Visible = true;
            this.lblId.Visible = false;
            this.txtuserid.Visible = false;

            this.LoadGrid();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPer();
        }
        private void Session_update()
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int index;
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                string chkper = (((CheckBox)gvPermission.Rows[i].FindControl("chkPermit")).Checked) ? "True" : "False";
                string chkEntry = (((CheckBox)gvPermission.Rows[i].FindControl("chkEntry")).Checked) ? "True" : "False";
                string chkPrint = (((CheckBox)gvPermission.Rows[i].FindControl("chkPrint")).Checked) ? "True" : "False";
                string deleteCk = (((CheckBox)gvPermission.Rows[i].FindControl("deleteCk")).Checked) ? "True" : "False";

                index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                dt.Rows[index]["chkper"] = chkper;
                dt.Rows[index]["entry"] = chkEntry;
                dt.Rows[index]["printable"] = chkPrint;
                dt.Rows[index]["deleteCk"] = deleteCk;

            }
            Session["tblusrper"] = dt;
        }

        protected void gvPermission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_update();
            this.gvPermission.PageIndex = e.NewPageIndex;
            this.ShowPer();
        }
        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
        
            this.Session_update();
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataTable dt1 = (DataTable)Session["tblusrper"];
            DataView dv = dt1.DefaultView;
            dv.RowFilter = "chkper='true'";
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dv.ToTable());
            ds1.Tables[0].TableName = "tbl1";

            bool result = false;
            result = User.UpdateTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "DELETEUSER", usrid, modname,
                        "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + User.ErrorObject["Msg"].ToString() + "');", true);

             
                return;
            }

            string getsml = ds1.GetXml();

            result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "INSERTUSRPER", ds1, null, null, usrid, "", "",
                         "", "");

           
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



        }
        protected void chkShowall_CheckedChanged(object sender, EventArgs e)
        {
            //this.ddlModName.SelectedValue = "00";

            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
                //this.usrSpcPer();
            }
            else
            {
                this.usrSpcPer();
            }
        }
        private void ShowAllPer()
        {
            DataTable dt = (DataTable)Session["tblcompper"];
            DataTable dt1 = (DataTable)Session["tblusrper"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
                string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
                string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
                string entry = dt1.Rows[i]["entry"].ToString().Trim();
                string deleteCk = dt1.Rows[i]["deleteCk"].ToString().Trim();

                string printable = dt1.Rows[i]["printable"].ToString().Trim();
                string confrmqry = frmname + qrytype;
                DataRow[] dr1 = dt.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;
                    dr1[0]["deleteCk"] = deleteCk;


                }

            }

            Session["tblusrper"] = this.HiddenSameData(dt);
            this.ShowPer();

        }
        private void usrSpcPer()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "SHOWGRPUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);
            this.ShowPer();
        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("deleteCk")).Checked = true;

                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";
                    dt.Rows[index]["entry"] = "True";
                    dt.Rows[index]["printable"] = "True";
                    dt.Rows[index]["deleteCk"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;

                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "False";
                    dt.Rows[index]["entry"] = "False";
                    dt.Rows[index]["printable"] = "False";
                    dt.Rows[index]["deleteCk"] = "False";

                }

            }

            Session["tblusrper"] = dt;
            // this.ShowPer();

        }


        private void ShowAllData()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "SHOWGRPUSERPERFORM", usrid, modname, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);


            DataTable dt = (DataTable)Session["tblcompper"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = "frmid like '" + modname + "'";
            DataTable dt2 = dv.ToTable();






            DataTable dt1 = (DataTable)Session["tblusrper"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
                string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
                string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
                string entry = dt1.Rows[i]["entry"].ToString().Trim();
                string printable = dt1.Rows[i]["printable"].ToString().Trim();
                string deleteCk = dt1.Rows[i]["deleteCk"].ToString().Trim();

                string confrmqry = frmname + qrytype;
                DataRow[] dr1 = dt2.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;
                    dr1[0]["deleteCk"] = deleteCk;

                }

            }




            Session["tblusrper"] = this.HiddenSameData(dt2);
            this.ShowPer();






        }
        private void ShowData()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "SHOWGRPUSERPERFORM", usrid, modname, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);
            this.ShowPer();
        }
        protected void gvPermission_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;

            string frmid = ((Label)this.gvPermission.Rows[e.RowIndex].FindControl("lgvufrmid")).Text.Trim();

            bool result1 = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETEUSER", usrid, frmid,
                            "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                
                return;

            }
            this.ShowData();
        }
        protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
            }
            else
            {
                this.ShowData();
            }
        }



        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                if (((CheckBox)this.gvPermission.Rows[i].FindControl("chkAll")).Checked)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("deleteCk")).Checked = true;

                }
                //else
                //{
                //    //((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                //}
            }

        }
        protected void chkallView_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallView")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "False";


                }

            }

            Session["tblusrper"] = dt;
        }
        protected void chkallEntry_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallEntry")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["entry"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["entry"] = "False";


                }

            }

            Session["tblusrper"] = dt;

        }
        protected void chkallPrint_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallPrint")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["printable"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["printable"] = "False";

                }

            }

            Session["tblusrper"] = dt;

        }
        protected void chkAllDel_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkAllDel")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("deleteCk")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["deleteCk"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("deleteCk")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["deleteCk"] = "False";


                }

            }

            Session["tblusrper"] = dt;

        }

        protected void lgvusrShorName_Click(object sender, EventArgs e)
        {
            this.userDetPan.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string grpusrid = ((LinkButton)this.gvUseForm.Rows[index].FindControl("lbtnUserId")).Text.ToString();
            DataSet ds1 = User.GetTransInfo("", "SP_UTILITY_USER_MANAGEMENT", "GETGRUSERCOMPANY", grpusrid, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            this.lbluserheading.Text = dt.Rows[0]["usrname"].ToString();
            this.lblGrpUsrId.Text= "Group ID: " + dt.Rows[0]["grpusrid"].ToString();
            string url = String.Empty;
            if (dt.Rows[0]["usrimg"] != null && dt.Rows[0]["usrimg"].ToString() != "")
            {

                byte[] ifff = (byte[])dt.Rows[0]["usrimg"];
                url = "data:image;base64," + Convert.ToBase64String(ifff);
            }
            else
            {
                url = "~/Content/Theme/images/avatars/human_avatar.png";
            }
            this.UsrImg.ImageUrl = url;
            this.indUsrinf.DataSource = dt;
            this.indUsrinf.DataBind();
        }
        protected void lbtnNewUser_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblapend");
            this.LblUpFlag.Text = "NEW";
            gvcompany.DataSource = null;
            gvcompany.DataBind();
            this.gvUseForm.Visible = false;
            this.userDetPan.Visible = false;
            this.MultiView1.ActiveViewIndex = 1;
            this.txtUsr.Text = "";
            this.TxtFullName.Text = "";
            this.TxtDesg.Text = "";
            this.TxtRemark.Text = "";
            this.GetAllComp();
            this.GetEmployeeName();
            this.CreateList();
            this.GetLastGrpUserId();




        }
        private void CreateList()
        {
            List<SPEENTITY.C_34_Mgt.GetCompany> list = new List<SPEENTITY.C_34_Mgt.GetCompany>();
            ViewState["tblapend"] = list;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var list2 = (List<SPEENTITY.C_34_Mgt.GetCompany>)ViewState["tblapend"];


            string comcod = this.ddlComp.SelectedValue.ToString();
            string comname = this.ddlComp.SelectedItem.ToString();
            string usrsname = this.txtUsr.Text.Trim().ToString();
            string usrname = this.TxtFullName.Text.Trim().ToString();
            string usrdesig = this.TxtDesg.Text.Trim().ToString();
            // string usrrmrk = this.TxtRemark.Text.Trim().ToString();
            string empid = this.ddlHrEmp.SelectedValue.ToString();
            string uRole = this.ddlUserRole.SelectedValue.ToString();
            string usractive = (this.actChkbox.Checked == true) ? "True" : "False";
            var checklist = list2.FindAll(p => p.comcod == comcod);
            if (checklist.Count == 0)
            {
                SPEENTITY.C_34_Mgt.GetCompany newlist = new SPEENTITY.C_34_Mgt.GetCompany(comcod, comname, "", usrsname, usrname, usrdesig, "", "", empid, uRole, usractive);
                list2.Add(newlist);
            }
            ViewState["tblapend"] = list2;
            gvcompany.DataSource = list2;
            gvcompany.DataBind();
        }

        private void GetAllComp()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //  string comcod = hst["comcod"].ToString();
            DataSet ds = User.GetTransInfo("", "SP_UTILITY_USER_MANAGEMENT", "GETCOMPANY", "", "", "", "", "", "", "", "", "");
            var List = ds.Tables[0].DataTableToList<SPEENTITY.C_34_Mgt.GetCompany>();
            ViewState["compdata"] = List;
            this.ddlComp.DataTextField = "comsnam";
            this.ddlComp.DataValueField = "comcod";
            this.ddlComp.DataSource = ds.Tables[0];
            this.ddlComp.DataBind();

        }
        private void GetEmployeeName()
        {
            DataTable dt1 = new DataTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_USER_MANAGEMENT", "GETCOMPANYNAME", "", "", "", "", "", "", "", "", "");

            dt1 = ds1.Tables[0].Copy();

            dt1 = dt1.DefaultView.ToTable(true, "sircode", "sirdesc");
            dt1.Rows.Add("", "None");
            //var List = ds.Tables[0].DataTableToList<MFGOBJ.C_34_Mgt.GetCompanyname>();
            //ViewState["compname"] = List;
            this.ddlHrEmp.DataTextField = "sirdesc";
            this.ddlHrEmp.DataValueField = "sircode";
            this.ddlHrEmp.DataSource = dt1;
            this.ddlHrEmp.DataBind();
            this.ddlHrEmp.SelectedValue = "";



            DataSet ds2 = User.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETUROLECODE", "", "", "", "", "", "", "", "", "");

            ddlUserRole.DataTextField = "roledesc";
            ddlUserRole.DataValueField = "roleid";
            ddlUserRole.DataSource = ds2;
            ddlUserRole.DataBind();
            //ddlUserRole.SelectedValue = "";

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.lnkbtnBack_Click(null, null);
        }
        private void GetLastGrpUserId()
        {
            DataSet ds3 = User.GetTransInfo("", "SP_UTILITY_USER_MANAGEMENT", "GETLASTGRPUSRID", "", "", "", "", "", "", "", "", "");
            DataTable dt = ds3.Tables[0];
            string mastrcomlastid = ds3.Tables[0].Rows[0]["maxgrusr"].ToString();
            this.Grpusr.Text = mastrcomlastid;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            this.Save_Value();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //  string actvstatus = (this.actChkbox.Checked == true) ? "True" : "False";


            var list2 = (List<SPEENTITY.C_34_Mgt.GetCompany>)ViewState["tblapend"];
            string pass = this.TxtPass.Text.Trim().ToString();
            string mastrcomlastid = this.Grpusr.Text.ToString();
            string usrsname = this.txtUsr.Text.Trim().ToString();

            string flag = this.LblUpFlag.Text.ToString();
            if (flag == "NEW")
            {
                DataSet ds3 = User.GetTransInfo("", "SP_UTILITY_USER_MANAGEMENT", "CHECKGRPUSERFORDUPLICATE", usrsname, "", "", "", "", "", "", "", "");
                DataTable dt = ds3.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    this.lblMsg.Visible = true;
                    this.lblMsg.Text = "This User Already Exist";
                    return;
                }

            }


            string usrpass = (pass.Length == 0) ? "" : ASTUtility.EncodePassword( pass);
            // string usrpass = ASTUtility.EncodePassword(usrsname + pass);
            string usrname = this.TxtFullName.Text.Trim().ToString();
            string remark = this.TxtRemark.Text.ToString();
            string desig = this.TxtDesg.Text.Trim().ToString();
            string employee = this.ddlHrEmp.SelectedValue.ToString();
            string uRole = this.ddlUserRole.SelectedValue.ToString();
            bool result = false;
            foreach (SPEENTITY.C_34_Mgt.GetCompany lis in list2)
            {
                string userid = lis.comcod + ASTUtility.Right(mastrcomlastid,3);
                string actvstatus = lis.usractive.ToString();
                result = User.UpdateTransInfo(lis.comcod, "SP_UTILITY_USER_MANAGEMENT", "INSORUPDATEGRPUSR", userid, usrsname, usrname, desig, usrpass, remark, actvstatus, employee, mastrcomlastid, uRole, "", "", "", "", "", "", "", "", "", "", "");

            }

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

               
                this.ShowUserInfo();
                //  ViewState.Remove("tblapend");
                lnkbtnBack_Click(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);

              
                return;
            }
        }
        protected void gvcompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var mrlist = (List<SPEENTITY.C_34_Mgt.GetCompany>)ViewState["tblapend"];
            mrlist.RemoveAt(e.RowIndex);
            ViewState["tblapend"] = mrlist;
            gvcompany.DataSource = mrlist;
            gvcompany.DataBind();

        }



        protected void EditBtn_Click(object sender, EventArgs e)
        {
            this.userDetPan.Visible = true;
            this.LblUpFlag.Text = "OLD";
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string grpuser = ((LinkButton)gvUseForm.Rows[index].FindControl("lbtnUserId")).Text.Trim().ToString();

            DataSet ds3 = User.GetTransInfo("", "SP_UTILITY_USER_MANAGEMENT", "GETGRUSERCOMPANY", grpuser, "", "", "", "", "", "", "", "");
            var list = ds3.Tables[0].DataTableToList<SPEENTITY.C_34_Mgt.GetCompany>();

            ViewState["tblapend"] = list;
            gvcompany.DataSource = list;
            gvcompany.DataBind();
            this.gvUseForm.Visible = false;
            this.userDetPan.Visible = false;
            this.MultiView1.ActiveViewIndex = 1;
            this.txtUsr.Text = list[0].usrsname;
            this.TxtFullName.Text = list[0].usrname;
            this.TxtDesg.Text = list[0].usrdesig;
            this.TxtRemark.Text = list[0].usrrmrk;
            this.Grpusr.Text = grpuser;
            this.GetAllComp();
            this.GetEmployeeName();
            this.ddlHrEmp.SelectedValue = list[0].empid;
            this.ddlUserRole.SelectedValue = list[0].urole;

        }
        private void Save_Value()
        {
            var list2 = (List<SPEENTITY.C_34_Mgt.GetCompany>)ViewState["tblapend"];

            for (int i = 0; i < this.gvcompany.Rows.Count; i++)
            {
                //double tAmt = Convert.ToDouble("0" + ((Label)this.RepInv.Items[i].FindControl("rlbltAmt")).Text.Trim());

                string usractive = (((CheckBox)this.gvcompany.Rows[i].FindControl("chkActiveSt")).Checked == true) ? "True" : "False";
                list2[i].usractive = usractive;
            }
            ViewState["tblapend"] = list2;
        }

    }
}