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
    public partial class MyDashboard : System.Web.UI.Page
    {
        //UserManager userManager = new UserManager();
        UserService objuserser = new UserService();
        ProcessAccess GrpData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ((Panel)this.Master.FindControl("pnlHead")).Visible = false;
                //this.pnlHead.Visible = false;
                if (Session["tblusrlog"] != null)
                {
                    DataSet ds2 = ((DataSet)Session["tblusrlog"]);

                    this.UserImg.ImageUrl = "~/GetImage?ImgID=ImgUser";
                    //this.UserImg2.ImageUrl = "~/GetImage?ImgID=ImgUser";



                }

                if (Session["tbllog1"] != null)
                {


                    this.LblGrpCompany.Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
                    this.lbladd.Text = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();
                    this.Image1.ImageUrl = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
                    this.lblLoginInfo.Text = "User: " + ((DataTable)Session["tbllog1"]).Rows[0]["usrsname"].ToString() + ", Session:" + ((DataTable)Session["tbllog1"]).Rows[0]["session"].ToString(); //+ ", Login Time: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
                                                                                                                                                                                                       //((Image)this.Master.FindControl("Image1"))
                }

                //this.pnlAcc.Visible = (this.Request.QueryString["Type"] == "17");
                //this.pnlBudget.Visible = (this.Request.QueryString["Type"] == "04");
                //this.pnlInv.Visible = (this.Request.QueryString["Type"] == "13");
                //this.pnlPur.Visible = (this.Request.QueryString["Type"] == "14");
                //this.PnlSales.Visible = (this.Request.QueryString["Type"] == "22");

                //this.pnlMrkt.Visible = (this.Request.QueryString["Type"] == "21");
                //this.pnlProd.Visible = (this.Request.QueryString["Type"] == "100");
                //this.pnlHR.Visible = (this.Request.QueryString["Type"] == "4101");
                //this.pnlGeneral.Visible = (this.Request.QueryString["Type"] == "4102");
                //this.PnlFinReports.Visible = (this.Request.QueryString["Type"] == "4103");
                //this.PnlMISReports.Visible = (this.Request.QueryString["Type"] == "4104");
                //this.PnlMFg.Visible = (this.Request.QueryString["Type"] == "4105");
                //this.pnlSetting4106.Visible = (this.Request.QueryString["Type"] == "4106");

                //this.Panel8.Visible = (this.Request.QueryString["Type"] == "5000");

                //this.PnFGoods.Visible = (this.Request.QueryString["Type"] == "5001");
                //this.Panel7.Visible = (this.Request.QueryString["Type"] == "4110");
                //this.pnlAdminPermission.Visible = (this.Request.QueryString["Type"] == "4112");
                //this.PanelHR.Visible = (this.Request.QueryString["Type"] == "7000");

                this.PanTrading.Visible = (this.Request.QueryString["Type"] == "8000");

                //this.PnlGrp.Visible = (this.Request.QueryString["Type"] == "9000");
                //this.mfgAcc.Visible = (this.Request.QueryString["Type"] == "5010");
                //this.PnlGrpDetails.Visible = (this.Request.QueryString["Type"] == "37");
                //this.pnlImport.Visible = (this.Request.QueryString["Type"] == "5005");
                //this.pnlExport.Visible = (this.Request.QueryString["Type"] == "5006");
                //this.pnlkMIS.Visible = (this.Request.QueryString["Type"] == "5004");


                //this.pnlflowchart.Visible = (this.Request.QueryString["Type"] == "8010");
                //this.pnlhrn.Visible = (this.Request.QueryString["Type"] == "8011");


                if (this.Request.QueryString["Type"] == "9000")
                {
                    this.CallCompanyList();

                }

            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void CallCompanyList()
        {
            //string comcod = this.GetCompCode();
            //string consolidate = "";
            //DataSet ds1 = this.GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            //this.rbtnList1.DataTextField = "comsnam";
            //this.rbtnList1.DataValueField = "comcod";
            //this.rbtnList1.DataSource = ds1.Tables[0];
            //this.rbtnList1.DataBind();
        }
        //protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string comcod = "";
        //    string comsnam = "";
        //    //string Url1 = "F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=";
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
        //    switch (rbtnList1.SelectedIndex)
        //    {
        //        case 0:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();

        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;
        //        case 1:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1); 
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;

        //        case 2:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1); 
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;
        //        case 3:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1); 
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;
        //        case 4:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1);
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;
        //        case 5:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1); 
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;


        //        case 6:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1); 
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;
        //        case 7:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1); 
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;
        //        case 8:
        //            this.PnlGrpDetails.Visible = true;
        //            this.PnlGrp.Visible = false;
        //            //comcod = rbtnList1.SelectedValue.ToString();
        //            //comsnam = rbtnList1.SelectedItem.Text.ToString();
        //            ////Url1 += comcod;
        //            ////Response.Redirect(Url1);
        //            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //            break;
        //    }
        //}
        //public string GetCompCodeS()
        //{
        //    return rbtnList1.SelectedValue.ToString();
        //}
        //public string GetCompCodeS1()
        //{
        //    return rbtnList1.SelectedItem.Text.ToString();
        //}

        protected void btnSales_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetCompCodeS();
            //string comsnam = this.GetCompCodeS1();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnPur_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetCompCodeS();
            //string comsnam = this.GetCompCodeS1();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpPurInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnPro_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetCompCodeS();
            //string comsnam = this.GetCompCodeS1();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpProductionInfo.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnAcc_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetCompCodeS();
            //string comsnam = this.GetCompCodeS1();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpAccInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnOver_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetCompCodeS();
            //string comsnam = this.GetCompCodeS1();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpDashBoardAll.aspx?comcod=" + comcod + "', target='_blank');</script>";

        }


        protected void lnkbtnGeneral_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //hst["commod"] = "1";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('DeafultMenu?Type=5000', target='_self');</script>";


        }

        //protected void lnkbtnKPI_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //   // hst["commod"] = "3";
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('MyDashboard.aspx?Type=5004', target='_self');</script>";
        //}
        protected void lnkbtnGrp_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //hst["commod"] = "1";
            this.loginBtn_Click(null, null);
            //((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('DeafultMenu.aspx?Type=9000', target='_self');</script>";
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            //UserLogin ulog = new UserLogin();
            //DataSet ds1 = ulog.GetHitCounter();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string txtuserpass = hst["password"].ToString();
            string pass = ASTUtility.EncodePassword(username + txtuserpass);
            DataTable dt5 = ((DataTable)Session["tbllog"]).Copy();
            DataView dv = dt5.DefaultView;
            dv.RowFilter = "comcod like '9%'";
            dt5 = dv.ToTable();
            string comcod = dt5.Rows[0]["comcod"].ToString();

            string modulid = "35";


            DataSet ds5 = GrpData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, "", "", "", "", "", "");

            Session["tblusrlog"] = ds5;
            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            if (dr.Length > 0)
            {
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];

                dt2.Rows[0]["comnam"] = dr[0]["comnam"];
                dt2.Rows[0]["comsnam"] = dr[0]["comsnam"];
                dt2.Rows[0]["comadd1"] = dr[0]["comadd1"];
                dt2.Rows[0]["comadd"] = dr[0]["comadd"];
            }

            hst["comcod"] = comcod;
            //  hst["comnam"] = ComName;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["modulenam"] = "";// this.ddlModuleName.SelectedValue.ToString();
            hst["trmid"] = "";
            Session["tblLogin"] = hst;
            Session["tbllog1"] = dt2;




            //Session["tblusrlog"] = ds5;
            //if (Request.QueryString["index"] != null)
            //    ind = Request.QueryString["index"].ToString();

            //if (Request.QueryString["index"] == null)
            //    ind = this.ListModulename.SelectedIndex.ToString();


            //string Comcode = this.listComName.SelectedValue.ToString();
            //string ComName = this.listComName.SelectedItem.Text;

            //string Mname = "35";


            //DataTable dt1 = (DataTable)Session["tbllog"];
            //DataTable dt2 = new DataTable();
            //if ((DataTable)Session["tbllog1"] == null)
            //{
            //    dt2.Columns.Add("comcod", Type.GetType("System.String"));
            //    dt2.Columns.Add("comnam", Type.GetType("System.String"));
            //    dt2.Columns.Add("comsnam", Type.GetType("System.String"));
            //    dt2.Columns.Add("comadd1", Type.GetType("System.String"));
            //    dt2.Columns.Add("comadd", Type.GetType("System.String"));
            //    dt2.Columns.Add("usrsname", Type.GetType("System.String"));
            //    dt2.Columns.Add("session", Type.GetType("System.String"));
            //    Session["tbllog1"] = dt2;
            //}

            //DataTable dtr = (DataTable)Session["tbllog1"];
            //DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            //Hashtable hst = new Hashtable();
            //if (dr.Length > 0)
            //{

            //    hst["comnam"] = dr[0]["comnam"];
            //    hst["comnam"] = dr[0]["comnam"];
            //    hst["comsnam"] = dr[0]["comsnam"];
            //    hst["comadd1"] = dr[0]["comadd1"];
            //    hst["comweb"] = dr[0]["comadd3"];
            //    hst["combranch"] = dr[0]["combranch"];
            //    hst["comadd"] = dr[0]["comadd"];
            //    DataRow dr2 = dt2.NewRow();
            //    dr2["comcod"] = comcod;
            //    dr2["comnam"] = dr[0]["comnam"];
            //    dr2["comsnam"] = dr[0]["comsnam"];
            //    dr2["comadd1"] = dr[0]["comadd1"];
            //    dr2["comadd"] = dr[0]["comadd"];
            //    dt2.Rows.Add(dr2);

            //}
            //string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
            //hst["comcod"] = comcod;
            ////hst["hcomcod"] = ds5.Tables[3].Rows[0]["comcod"];
            //// hst["comnam"] = ComName;
            //hst["deptcode"] = "";
            //hst["ldeptcode"] = "";
            //hst["deptname"] = "";
            //hst["modulenam"] = modulid;
            //hst["username"] = ds5.Tables[0].Rows[0]["usrsname"];
            ////hst["compname"] = HostAddress;
            //hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            ////hst["password"] = this.txtuserpass.Text;
            //hst["session"] = sessionid;
            //hst["trmid"] = "";
            //hst["commod"] = "1";

            //Session["tblLogin"] = hst;
            //dt2.Rows[0]["usrsname"] = ds5.Tables[0].Rows[0]["usrsname"];
            //dt2.Rows[0]["session"] = sessionid;
            //Session["tbllog1"] = dt2;



            string Url1 = "DeafultMenu.aspx?Type=9000";

            Response.Redirect(Url1);

        }
        protected void lnkbtnAbp_Click(object sender, EventArgs e)
        {


            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "02";
            ds1.Tables[0].Rows[0]["moduleid2"] = "02";



            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnProc_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "10";
            ds1.Tables[0].Rows[0]["moduleid2"] = "10";


            //string ModuleID = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
            //string ModuleID2 = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();

            //hst["commod"] = "1";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }


        protected void lnkbtnProd_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "13";
            ds1.Tables[0].Rows[0]["moduleid2"] = "13";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }


        protected void lnkbtnExport_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "19";
            ds1.Tables[0].Rows[0]["moduleid2"] = "19";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnGInventory_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "17";
            ds1.Tables[0].Rows[0]["moduleid2"] = "17";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnAccounts_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "21";
            ds1.Tables[0].Rows[0]["moduleid2"] = "21";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnProShiPlan_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "05";
            ds1.Tables[0].Rows[0]["moduleid2"] = "05";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void linkbtnprodMin_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "15";
            ds1.Tables[0].Rows[0]["moduleid2"] = "15";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnCostBud_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "03";
            ds1.Tables[0].Rows[0]["moduleid2"] = "03";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnRowMatInventory_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "11";
            ds1.Tables[0].Rows[0]["moduleid2"] = "11";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnCommer_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "09";
            ds1.Tables[0].Rows[0]["moduleid2"] = "09";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnMis_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "97";
            ds1.Tables[0].Rows[0]["moduleid2"] = "97";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnFixedAss_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "27";
            ds1.Tables[0].Rows[0]["moduleid2"] = "27";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void linkbtnMerch_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "01";
            ds1.Tables[0].Rows[0]["moduleid2"] = "01";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnStepOpra_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "32";
            ds1.Tables[0].Rows[0]["moduleid2"] = "32";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnProcur_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "10";
            ds1.Tables[0].Rows[0]["moduleid2"] = "10";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnAudit_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "24";
            ds1.Tables[0].Rows[0]["moduleid2"] = "24";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnMarketing_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "25";
            ds1.Tables[0].Rows[0]["moduleid2"] = "25";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnMM_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "34";
            ds1.Tables[0].Rows[0]["moduleid2"] = "34";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnHR_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "80";
            ds1.Tables[0].Rows[0]["moduleid2"] = "80";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnExp_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "19";
            ds1.Tables[0].Rows[0]["moduleid2"] = "19";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnKPI_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "79";
            ds1.Tables[0].Rows[0]["moduleid2"] = "79";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnDocu_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "33";
            ds1.Tables[0].Rows[0]["moduleid2"] = "33";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation?Module=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
    }
}