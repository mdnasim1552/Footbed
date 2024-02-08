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
using SPERDLC;

namespace SPEWEB
{
    public partial class DeafultMenu : System.Web.UI.Page
    {
        ProcessAccess GrpData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((Label)this.Master.FindControl("lblTitle")).Visible = false;
                this.pnlAcc.Visible = (this.Request.QueryString["Type"] == "17");
                this.pnlBudget.Visible = (this.Request.QueryString["Type"] == "04");
                this.pnlInv.Visible = (this.Request.QueryString["Type"] == "13");
                this.pnlPur.Visible = (this.Request.QueryString["Type"] == "14");
                this.PnlSales.Visible = (this.Request.QueryString["Type"] == "22");

                this.pnlMrkt.Visible = (this.Request.QueryString["Type"] == "21");
                this.pnlProd.Visible = (this.Request.QueryString["Type"] == "100");
                this.pnlHR.Visible = (this.Request.QueryString["Type"] == "4101");
                this.pnlGeneral.Visible = (this.Request.QueryString["Type"] == "4102");
                this.PnlFinReports.Visible = (this.Request.QueryString["Type"] == "4103");
                this.PnlMISReports.Visible = (this.Request.QueryString["Type"] == "4104");
                this.PnlMFg.Visible = (this.Request.QueryString["Type"] == "4105");
                this.pnlSetting4106.Visible = (this.Request.QueryString["Type"] == "4106");

                this.Panel8.Visible = (this.Request.QueryString["Type"] == "5000");

                this.PnFGoods.Visible = (this.Request.QueryString["Type"] == "5001");
                this.Panel7.Visible = (this.Request.QueryString["Type"] == "4110");
                this.pnlAdminPermission.Visible = (this.Request.QueryString["Type"] == "4112");
                this.PanelHR.Visible = (this.Request.QueryString["Type"] == "7000");
                this.PanTrading.Visible = (this.Request.QueryString["Type"] == "8000");
                this.PnlGrp.Visible = (this.Request.QueryString["Type"] == "9000");
                this.mfgAcc.Visible = (this.Request.QueryString["Type"] == "5010");
                this.PnlGrpDetails.Visible = (this.Request.QueryString["Type"] == "37");
                this.pnlImport.Visible = (this.Request.QueryString["Type"] == "5005");
                this.pnlExport.Visible = (this.Request.QueryString["Type"] == "5006");
                this.pnlkMIS.Visible = (this.Request.QueryString["Type"] == "5004");

                this.FlowchartShow();

                //  this.pnlflowchart.Visible = (this.Request.QueryString["Type"] == "8010");
                //this.pnlhrn.Visible = (this.Request.QueryString["Type"] == "8011");


                if (this.Request.QueryString["Type"] == "9000")
                {
                    this.CallCompanyList();

                }

            }
        }

        protected void FlowchartShow()
        {
            string comcod = ASTUtility.Left(this.GetCompCode(), 1);
            switch (comcod)
            {
                //case"7107":
                //this.pnlflowchart011.Visible = (this.Request.QueryString["Type"] == "8010");
                //break;
                case "8":
                case "5":
                    this.pnlflowchart01.Visible = (this.Request.QueryString["Type"] == "8010");
                    break;
                //case "8505":
                //this.pnlflowchart.Visible = (this.Request.QueryString["Type"] == "8010");
                //break;
                case "7":
                    this.pnlflowchart.Visible = (this.Request.QueryString["Type"] == "8010");
                    break;
                    //case "8702":
                    //this.pnlflowchart.Visible = (this.Request.QueryString["Type"] == "8010");
                    //break;
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void CallCompanyList()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = this.GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }


            this.rbtnList1.DataTextField = "comnam";
            this.rbtnList1.DataValueField = "comcod";
            this.rbtnList1.DataSource = ds1.Tables[0];
            this.rbtnList1.DataBind();
        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = "";
            string comsnam = "";
            //string Url1 = "F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=";
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();

                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;
                case 1:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1); 
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;

                case 2:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1); 
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;
                case 3:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1); 
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;
                case 4:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1);
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;
                case 5:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1); 
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;


                case 6:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1); 
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;
                case 7:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1); 
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    this.PnlGrpDetails.Visible = true;
                    this.PnlGrp.Visible = false;
                    //comcod = rbtnList1.SelectedValue.ToString();
                    //comsnam = rbtnList1.SelectedItem.Text.ToString();
                    ////Url1 += comcod;
                    ////Response.Redirect(Url1);
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
                    break;
            }
        }
        public string GetCompCodeS()
        {
            return rbtnList1.SelectedValue.ToString();
        }
        public string GetCompCodeS1()
        {
            return rbtnList1.SelectedItem.Text.ToString();
        }
        protected void btnSales_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnPur_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpPurInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnPro_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpProductionInfo.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnAcc_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpAccInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnOver_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpDashBoardAll.aspx?comcod=" + comcod + "', target='_blank');</script>";

        }

        protected void lnkbtnGeneral_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            // hst["commod"] = "1";
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('DeafultMenu.aspx?Type=8011', target='_self');</script>";


        }
        protected void lnkbtnHr_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //hst["commod"] = "4";
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('DeafultMenu.aspx?Type=7000', target='_self');</script>";
        }
        protected void lnkbtnKPI_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //hst["commod"] = "3";
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('DeafultMenu.aspx?Type=5004', target='_self');</script>";



        }
        protected void lnkbtnCompany_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //hst["commod"] = "1";
            this.loginBtn_Click(null, null);


        }
        protected void loginBtn_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string txtuserpass = hst["password"].ToString();
            string pass = ASTUtility.EncodePassword(username + txtuserpass);
            DataTable dt5 = ((DataTable)Session["tbllog"]).Copy();
            DataView dv = dt5.DefaultView;
            dv.RowFilter = "comcod like '8505%'";
            dt5 = dv.ToTable();
            string comcod = dt5.Rows[0]["comcod"].ToString();

            string modulid = "AA";


            DataSet ds5 = GrpData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, "", "", "", "", "", "");
            if (ds5.Tables[0].Rows.Count == 0 || ds5 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

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




            string Url1 = "MyDashboard.aspx?Type=5000";

            Response.Redirect(Url1);

        }

    }
}