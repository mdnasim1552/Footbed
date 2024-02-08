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
using SPEENTITY.C_22_Sal;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_01_Mer
{
    public partial class PurCustInfo : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "CUSTOMERS INFORMATION VIEW/EDIT";
                this.CommonButton();
                if (this.ddlSName.Items.Count == 0)
                {
                    this.GetSupplierName();
                }
                string sircode = "000000000000";

                if (Request.QueryString.AllKeys.Contains("sircode"))
                {
                    if (this.Request.QueryString["sircode"] != "")
                    {

                        sircode = this.Request.QueryString["sircode"].ToString();
                        this.ddlSName.SelectedValue = sircode;
                        this.lbtnOk_Click(null, null);
                    }
                }
                this.ShowOtherInfo();
                this.ShowAgent();
            }

        }


        public void CommonButton()
        {
            //   ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lUpdatPerInfo_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void ShowOtherInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds6 = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETCOUNTRYPORT", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            ViewState["tblcountry"] = ds6.Tables[0];


        }


        private void ShowAgent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds6 = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETAGENT", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            ViewState["tblAgent"] = ds6.Tables[0];
        }


        private void GetSupplierName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCUSTOMER", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlSName.DataTextField = "sirdesc";
            this.ddlSName.DataValueField = "sircode";
            this.ddlSName.DataSource = ds1.Tables[0];
            this.ddlSName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetSupplierName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblProjectdesc.Text = this.ddlSName.SelectedItem.Text;
                //this.lblProjectmDesc.Text = this.ddlSName.SelectedItem.Text.Substring(13);
                this.ddlSName.Enabled = false;
                //this.lblProjectmDesc.Visible = true;
                //this.lblProjectdesc.Visible = true;
                this.PopulateDataSetForDdl();
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }

        private void PopulateDataSetForDdl()
        {
            Common common = new Common();
            string comcod = common.GetCompCode();
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "CMBBBSRNINFORMATION", "", "", "", "", "", "", "", "", "");
            ViewState["tbllcgeninfo"] = ds2;
        }

        private void ClearScreen()
        {
            this.ddlSName.Enabled = true;
            //this.lblProjectmDesc.Text = "";
            //this.lblProjectmDesc.Visible = false;
            //this.lblProjectdesc.Text = "";
           
            //this.lblProjectdesc.Visible = false;
            this.gvPersonalInfo.DataSource = null;
            this.gvPersonalInfo.DataBind();
        }

        private void GET_Incotrms()
        {
            Common common = new Common();
            string comcod = common.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "17%", "", "", "", "", "");
            ViewState["tblSaleTerms"] = ds1;
        }

        private void LoadGrid()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SupplierCode = this.ddlSName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "CUST_PERSONAL_INFO", SupplierCode, "", "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();

            DataSet ds2 = (DataSet) ViewState["tbllcgeninfo"];
            DataView dv1 = new DataView();

            this.GET_Incotrms();

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                string Gcode = ds1.Tables[0].Rows[i]["gcod"].ToString();

                
                switch (Gcode)
                {
                    case "72001":
                        DataTable dt = (DataTable)ViewState["tblcountry"];

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "name";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "id";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dt;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                        break;

                    case "72015":
                        DataTable dta = (DataTable)ViewState["tblAgent"];

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "gdesc";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "gcod";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dta;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                        break;

                    case "72502":
                        dv1 = ds2.Tables[10].DefaultView;
                        dv1.RowFilter = ("sircode like '010100421%'");

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "sirdesc";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "sircode";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dv1.ToTable();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                        break;
                        
                    case "72503":
                        dv1 = ds2.Tables[10].DefaultView;
                        dv1.RowFilter = ("sircode like '010100422%'");

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "sirdesc";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "sircode";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dv1.ToTable();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                        break;

                    case "72504":
                        dv1 = ds2.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '23%'");

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "gdesc";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "gcod";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dv1.ToTable();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                        break;

                    //case "72505":
                    //    dv1 = ds2.Tables[4].DefaultView;

                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "sirdesc";
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "sircode";
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dv1.ToTable();
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                    //    break;

                    //case "72508":
                    //    dv1 = ds2.Tables[3].DefaultView;

                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "sirdesc";
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "sircode";
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dv1.ToTable();
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                    //    break;
                        
                    case "72509":
                        dv1 = ((DataSet)ViewState["tblSaleTerms"]).Tables[0].AsDataView() ;

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "gdesc";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "gcod";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dv1.ToTable();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                        break;
                        
                    case "72510":
                        dv1 = ds2.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '14%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "gdesc";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "gcod";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = dv1.ToTable();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdesc1"].ToString();

                        break;

                    case "72500":
                    case "72600":
                        this.gvPersonalInfo.Rows[i].BackColor = System.Drawing.Color.LightSkyBlue;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Enabled = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;

                        break;
                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;


                        break;
                }
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ssirCode = this.ddlSName.SelectedValue.ToString();

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                
                if (Gcode == "72001" || Gcode == "72015" || Gcode == "72502" || Gcode == "72503" || Gcode == "72504"  || Gcode == "72509" || Gcode == "72510")
                //|| Gcode == "72505" || Gcode == "72508"
                {
                    Gvalue = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.Trim();
                }


                MktData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTORUPDATECUSTLINF", ssirCode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "CUSTOMER INFORMATION";
                string eventdesc = "Update Sup Info";
                string eventdesc2 = this.ddlSName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


    }
}