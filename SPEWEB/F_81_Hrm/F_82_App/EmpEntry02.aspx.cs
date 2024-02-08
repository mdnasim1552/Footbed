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

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntry02 : System.Web.UI.Page
    {
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

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE PERSONAL INFORMATION";
                this.GetEmployeeName();
                this.GetInformation();

                this.lblmsg.Visible = false;
                this.getLastCardNo();
                this.lblLastCardNo.Visible = false;
            }




        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);


        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void getLastCardNo()
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLASTCARDNO", "", "", "", "", "", "", "", "", "");
            this.lblLastCardNo.Text = "Last Card Number :- " + ds5.Tables[0].Rows[0]["lastCard"].ToString().Trim();


        }
        private void GetEmployeeName()
        {

            Session.Remove("tblempname");
            string comcod = this.GetComeCode();
            string txtSProject = (this.Request.QueryString["empid"] != "") ? "%" + this.Request.QueryString["empid"].ToString() + "%" : "%" + this.EmployeeList.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME2", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblempname"] = ds3.Tables[0];
            ds3.Dispose();

            this.ddlEmpName.SelectedValue = (this.Request.QueryString["empid"] == "") ? this.ddlEmpName.Items[0].Value : this.Request.QueryString["empid"].ToString(); ;

        }

        private void GetInformation()
        {

            string comcod = this.GetComeCode();
            string txtinformation = this.txtInformation.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETINFORMATION", txtinformation, "", "", "", "", "", "", "", "");
            this.ddlInformation.DataTextField = "infodesc";
            this.ddlInformation.DataValueField = "infoid";
            this.ddlInformation.DataSource = ds3.Tables[0];
            this.ddlInformation.DataBind();

            // this.lblInformation.Text = ds3.Tables[0].Rows[0]["infodesc"].ToString();
        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetEmployeeName();
        }
        protected void ibtnInformation_Click(object sender, EventArgs e)
        {
            this.GetInformation();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lblInformation.Visible = true;
                this.ddlEmpName.Visible = false;
                this.lblEmpName.Visible = true;
                this.ddlInformation.Visible = false;
                this.lbtnOk.Text = "New";
                this.lblEmpName.Text = this.ddlEmpName.SelectedItem.Text;
                this.lblInformation.Text = this.ddlInformation.SelectedItem.Text;
                this.SelectView();
                return;
            }

            this.ddlEmpName.Visible = true;
            this.lblEmpName.Visible = false;
            this.ddlInformation.Visible = true;
            this.lbtnOk.Text = "Ok";
            this.MultiView1.ActiveViewIndex = -1;
            this.lblEmpName.Text = "";
            this.lblInformation.Visible = false;
            this.lblmsg.Text = "";



        }

        private void SelectView()
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();
            this.lblLastCardNo.Visible = false;

            switch (infoid)
            {
                case "01":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetBldMeReFes();
                    this.GetSupervisorName();
                    this.ShowPersonalInformation();
                    this.showDptInfor();
                    this.showDesignation();
                    this.lblLastCardNo.Visible = true;
                    break;
                case "99":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetBldMeReFes();
                    this.ShowLocation();

                    break;


            }

        }

        private void GetBldMeReFes()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            Session.Remove("tblbmrf");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBLDMEREFES", userid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblbmrf"] = ds2.Tables[0];



        }
        private void GetSupervisorName()
        {

            Session.Remove("tblsppname");
            string comcod = this.GetComeCode();
            string txtSProject = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", txtSProject, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataRow dr1 = ds3.Tables[0].NewRow();
            dr1["empid"] = "000000000000";
            dr1["empname"] = "None";
            ds3.Tables[0].Rows.Add(dr1);
            DataView dv = ds3.Tables[0].DefaultView;
            dv.Sort = "empid";
            Session["tblsppname"] = dv.ToTable();
            ds3.Dispose();



        }


        private void ShowPersonalInformation()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPERSONALINFO", empid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[2].Rows.Count > 0)
            {
                this.ddldpt.SelectedValue = ds2.Tables[2].Rows[0]["refno"].ToString().Trim();
                this.ddlDesignation.SelectedValue = ds2.Tables[2].Rows[0]["desgid"].ToString().Trim();
            }

            DataTable dt = ds2.Tables[0];
            Session["UserLog"] = ds2.Tables[1];
            DataRow[] dr = dt.Select("gcod='01002'");
            dr[0]["gdesc1"] = (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["empname1"];
            DataTable dt1 = (DataTable)Session["tblbmrf"];
            DataView dv1;
            this.gvPersonalInfo.DataSource = ds2.Tables[0];
            this.gvPersonalInfo.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {
                    case "01006": //Confirmation Date
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '85%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "01009": //Blood Group
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '90%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01010": //Relationship Status 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '92%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01011":// Religion
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '95%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01012": // Festival
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '97%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01013": // Nationality
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '98%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01023": // Sex
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '99%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01025": // Sex
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '96%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    //------------------------///
                    //case "01041": // Circle
                    //    dv1 = dt1.DefaultView;
                    //    dv1.RowFilter = ("gcod like '41%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    break;
                    //case "01043": // Region
                    //    dv1 = dt1.DefaultView;
                    //    dv1.RowFilter = ("gcod like '43%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    break;
                    //case "01045": // Area
                    //    dv1 = dt1.DefaultView;
                    //    dv1.RowFilter = ("gcod like '45%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    break;
                    //case "01047": // Territory
                    //    dv1 = dt1.DefaultView;
                    //    dv1.RowFilter = ("gcod like '46%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    break;
                    //case "01048": // Territory 2
                    //    dv1 = dt1.DefaultView;
                    //    dv1.RowFilter = ("gcod like '46%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    break;
                    //------------------------------------------//


                    case "01995": // Service Location
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '28%' or gcod like '29%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01996": // Supper Location


                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "empname";
                        ddlgval.DataValueField = "empid";
                        ddlgval.DataSource = (DataTable)Session["tblsppname"];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01997": // Grade
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '86%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01003": // Datetime
                    case "01007":
                    case "01008":
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        break;


                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }





        }

        private void ShowLocation()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOCATION", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvLocation.DataSource = null;
                this.gvLocation.DataBind();
                return;
            }

            Session["tblempLoc"] = ds3.Tables[0];
            this.Bind_Location();
            // DataTable dt = ds3.Tables[0];

            // DataSet ds1 = (DataSet)Session["tblacadeg"];

        }

        private void Bind_Location()
        {
            DataTable dt = (DataTable)Session["tblempLoc"];
            this.gvLocation.DataSource = dt;
            this.gvLocation.DataBind();
            DataTable dt1 = (DataTable)Session["tblbmrf"];
            DataView dv1;
            DropDownList ddlcircle, ddlregion, ddlarea, ddlterritory;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Circle
                string circle = dt.Rows[i]["circle"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '41%'");
                ddlcircle = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlcircle"));
                ddlcircle.DataTextField = "gdesc";
                ddlcircle.DataValueField = "gcod";
                ddlcircle.DataSource = dv1.ToTable();
                ddlcircle.DataBind();
                ddlcircle.SelectedValue = circle;

                //region
                string region = dt.Rows[i]["region"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '43%'");
                ddlregion = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlregion"));
                ddlregion.DataTextField = "gdesc";
                ddlregion.DataValueField = "gcod";
                ddlregion.DataSource = dv1.ToTable();
                ddlregion.DataBind();
                ddlregion.SelectedValue = region;
                //ddlarea
                string area = dt.Rows[i]["area"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '45%'");
                ddlarea = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlarea"));
                ddlarea.DataTextField = "gdesc";
                ddlarea.DataValueField = "gcod";
                ddlarea.DataSource = dv1.ToTable();
                ddlarea.DataBind();
                ddlarea.SelectedValue = area;

                //ddlterritory
                string territory = dt.Rows[i]["territory"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '46%'");
                ddlterritory = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlterritory"));
                ddlterritory.DataTextField = "gdesc";
                ddlterritory.DataValueField = "gcod";
                ddlterritory.DataSource = dv1.ToTable();
                ddlterritory.DataBind();
                ddlterritory.SelectedValue = territory;
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string slno1 = "";
            DataTable dt = (DataTable)Session["tblempLoc"];
            if (dt.Rows.Count == 0)
            {
                slno1 = "001";
            }
            else
            {
                int slno = Convert.ToInt16(dt.Rows[dt.Rows.Count - 1]["seq"]) + 1;//
                slno1 = slno.ToString();// ASTUtility.Right(("000" + slno), 3).ToString();
            }

            DataRow row = dt.NewRow();
            row["comcod"] = comcod;
            row["gcod"] = "32001";
            row["seq"] = slno1;
            row["gval"] = "T";
            row["circle"] = "";
            row["region"] = "";
            row["area"] = "";
            row["territory"] = "";
            dt.Rows.Add(row);
            Session["tblempLoc"] = dt;
            this.Bind_Location();
        }
        protected void lUpdateLocation_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvLocation.Rows.Count; i++)
            {
                string Circle = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlcircle")).SelectedValue.ToString();
                string gtype = ((Label)this.gvLocation.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Region = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlregion")).SelectedValue.ToString().Trim();
                string Area = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlarea")).SelectedValue.ToString();
                string Territory = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlterritory")).SelectedValue.ToString();
                string Seq = ((Label)this.gvLocation.Rows[i].FindControl("lgvseq")).Text.Trim();

                // if (Gcode != "99999" && Group != "999999999")
                HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "32001", gtype, Circle, "", Region, Area, Territory,
                    "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", Seq, "", "", "", "0");

            }
            this.lblmsg.Text = "Updated Successfully";


        }
        protected void gvLocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempLoc"];
            string comcod = this.GetComeCode();

            string empid = this.ddlEmpName.SelectedValue.ToString();
            string seq = ((Label)this.gvLocation.Rows[e.RowIndex].FindControl("lgvseq")).Text.ToString();



            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOCATIONDELETE", empid, "32001", seq, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                int rowindex = (this.gvLocation.PageSize) * (this.gvLocation.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();

            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblempLoc");
            Session["tblempLoc"] = dv.ToTable();

            this.Bind_Location();

        }
        private void showDptInfor()
        {
            string comcod = this.GetComeCode();

            string Company = "%%";
            string txtSProject = "%%";
            //string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement") ? "GETPROJECTNAME" : "GETPROJECTNAMEFOT";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME02", Company, txtSProject, "", "", "", "", "", "", "");
            if (ds4 == null)
                return;
            this.ddldpt.DataTextField = "actdesc";
            this.ddldpt.DataValueField = "actcode";
            this.ddldpt.DataSource = ds4.Tables[0];
            this.ddldpt.DataBind();

        }
        private void showDesignation()
        {
            string comcod = this.GetComeCode();
            // string dptcode = this.ddldpt.SelectedValue.ToString();
            // string empid = (this.ddlEmpName.Items.Count > 0) ? this.ddlEmpName.SelectedValue.ToString() : this.ddlEmpName.SelectedValue.ToString();
            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPDESGINFO", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            this.ddlDesignation.DataTextField = "hrgdesc";
            this.ddlDesignation.DataValueField = "hrgcod";
            this.ddlDesignation.DataSource = ds6.Tables[0];
            this.ddlDesignation.DataBind();
        }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;
            this.lblmsg.Text = "";


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            //string Gvalue="";
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string empname = ((TextBox)this.gvPersonalInfo.Rows[1].FindControl("txtgvVal")).Text.Trim();
            //Log Entry

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
            //string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
            //string tblEdittrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editrmid"].ToString();

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (dtuser.Rows.Count == 0) ? "" : userid;
            string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Editrmid = (dtuser.Rows.Count == 0) ? "" : Terminal;

            string dptcode = this.ddldpt.SelectedValue.ToString().Trim();
            string desigid = this.ddlDesignation.SelectedValue.ToString().Trim();
            string designame = this.ddlDesignation.SelectedItem.ToString().Trim();

            bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPLINF", empid, empname, PostedByid, PostSession, Posttrmid, Posteddat,
                    EditByid, Editdat, Editrmid, "", "", "", "", "", "", "", "", "", "", "", "");



            result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPDESGCODE", empid, "1", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                this.lblmsg.Text = "Data Is Not Updated";
                return;
            }

            result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, desigid, "T", designame, dptcode, "", "", "", "", "0", "",
                "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0");
            if (result == false)
            {
                this.lblmsg.Text = "Data Is Not Updated";
                return;
            }



            //---------------Validation Check---------------------//
            int value = 0;
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                if (Gcode == "01001" || Gcode == "01003")
                {
                    string Gvalue = (Gcode == "01001") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                    if (Gvalue.Length == 0)
                    {
                        int x = 1;
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                        value = value + x;
                    }
                    else
                    {
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000");
                    }

                }



            }

            if (result == false)
                return;

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                if (Gcode == "01001")
                {
                    Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    if (comcod != "4306")
                    {
                        if (Gvalue.Length != 6)
                        {
                            this.lblmsg.Text = "Please Put 6 Digit ID CARD Number";
                            return;
                        }
                    }
                    ///////////////----------------------------------------
                    DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETIDCARDNO", Gvalue, "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;
                    else
                    {
                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("empid <>'" + empid + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            this.lblmsg.Text = "Found Duplicate ID CARD No";
                            return;
                        }
                    }

                }

                if (Gcode == "01003" || Gcode == "01007" || Gcode == "01008")
                {

                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "",
                    "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0");


                if (!result)
                {
                    this.lblmsg.Text = "Updated Fail";
                }

            }
            this.getLastCardNo();

            this.lblmsg.Text = "Updated Successfully";
        }


        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01002")
                {

                    txtgvname.ReadOnly = true;

                }

            }


        }

        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {

        }

        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Joindate = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                switch (Gcode)
                {
                    case "01003": //Join Date

                        Joindate = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        //Joindate = ASTUtility.DateFormat(Joindate) ;
                        // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = Joindate;

                        break;


                    case "01006": //Confirmation Date
                        string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                        int monyear = (value.Contains("Month")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
                        string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd-MMM-yyyy");
                        ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvdVal")).Text = ConDate;
                        break;
                }


            }


        }

        protected void ibtngrdEmpList_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                switch (Gcode)
                {
                    case "01996": //Supper Visor


                        string comcod = this.GetComeCode();
                        DropDownList ddl2 = (DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval");
                        string Searchemp = "%" + ((TextBox)gvPersonalInfo.Rows[i].FindControl("txtgrdEmpSrc")).Text.Trim() + "%";
                        DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", Searchemp, "", "", "", "", "", "", "", "");
                        ddl2.DataTextField = "empname";
                        ddl2.DataValueField = "empid";
                        ddl2.DataSource = ds3.Tables[0];
                        ddl2.DataBind();
                        ds3.Dispose();
                        break;



                }


            }
        }


        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('HREmpEntry.aspx?Type=Aggrement', target='_self');</script>";
        }





    }
}