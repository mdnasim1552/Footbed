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
using AjaxControlToolkit;


namespace SPEWEB.F_03_CostABgd
{
    public partial class MLCInfoEntry : System.Web.UI.Page
    {
        Common Common = new Common();
        ProcessAccess MasterLc = new ProcessAccess();
        string Upload = "";
        int size = 0;
        public static string Url = "";
        System.IO.Stream image_file = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                GetBuyer();
                // this.LoadMasterLCList();
                this.CommonButton();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Order Information";
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


        protected void LoadMasterLCList()
        {
            string comcod = this.GetComeCode();
            string filter = "%";
            string Buyer = this.ddlBuyer.SelectedValue.ToString();
            if (Request.QueryString["actcode"].ToString() != "")
            {
                Buyer = "%";
            }
                DataSet ds2 = MasterLc.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", filter, Buyer, "", "", "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tbllcorder"] = lst;

            this.ddlMLC.DataSource = lst.Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            this.ddlMLC.DataTextField = "mlcdesc";
            this.ddlMLC.DataValueField = "mlccod";
            this.ddlMLC.DataBind();

           

            ds2.Dispose();


            if (Request.QueryString["actcode"].ToString() != "")
            {
                if(lst.FindAll(p=>p.mlccod== this.Request.QueryString["actcode"].ToString()).ToList().Count() == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Required Master Lc Archived Please active');", true);

                    return;
                }

                this.ddlMLC.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlmlccode_SelectedIndexChanged(null, null);
                this.lbtnOk_Click(null, null);
                this.ddlMLC.Enabled = false;
            }
            else
            {
                this.ddlmlccode_SelectedIndexChanged(null, null);
            }
        }
        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tbllcorder"];
            string mlccode = this.ddlMLC.SelectedValue.ToString();
           
            this.dllorderType.DataSource = tbl1.FindAll(x => x.mlccod == mlccode);
            this.dllorderType.DataTextField = "rdaydesc";
            this.dllorderType.DataValueField = "rdayid";
            this.dllorderType.DataBind();

          

            if (Request.QueryString["dayid"].ToString() != "")
            {
                this.dllorderType.SelectedValue= Request.QueryString["dayid"].ToString();
            }

            this.ddlMLC2.DataSource = tbl1.FindAll(x => x.mlccod != mlccode).Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            this.ddlMLC2.DataTextField = "mlcdesc";
            this.ddlMLC2.DataValueField = "mlccod";
            this.ddlMLC2.DataBind();
            ddlMLC2_SelectedIndexChanged(null,null);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lUpdatPerInfo_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetBuyer()
        {
            string comcod = this.GetComeCode();
            DataSet ds2 = MasterLc.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();
            this.ddlBuyer_SelectedIndexChanged(null, null);
        }
        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadMasterLCList();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {

                this.ddlMLC.Enabled = false;
                this.ddlBuyer.Enabled = false;
                this.dllorderType.Enabled = false;
                this.lbtnOk.Text = "New";
                this.importFrom.Visible = true;
                this.SelectView();
                return;
            }

            this.ddlMLC.Enabled = true;
            this.ddlBuyer.Enabled = true;
            this.dllorderType.Enabled = true;
            this.lbtnOk.Text = "Ok";
            this.importFrom.Visible = false;
            this.MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }


        private void SelectView()
        {

            string dsdsd = "01";
            switch (dsdsd)
            {
                case "01":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.Othernformation();
                    this.ShowLcInformation();
                    break;
            }

        }



        private void Othernformation()
        {

            string comcod = GetComeCode();

            DataSet ds2 = MasterLc.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "CMBBBSRNINFORMATION", "", "", "", "", "", "", "", "", "");
            ViewState["tbllcgeninfo"] = ds2;



            DataSet ds1 = MasterLc.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETCURRENCY", "", "", "", "", "", "", "", "", ""); ;

            if (ds1 == null)
                return;
            var lstConv = ds1.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds1.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;


        }


        protected void btnImport_Click(object sender, EventArgs e)
        {
            string comcod = GetComeCode();
            string toLC = this.ddlMLC.SelectedValue.ToString();
            string toDayID = this.dllorderType.SelectedValue.ToString();
            string fromLC = this.ddlMLC2.SelectedValue.ToString();
            string fromDayID = this.dllorderType2.SelectedValue.ToString();

            bool result = MasterLc.UpdateTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "TRNSFRLCDETINFO", fromLC, fromDayID, toLC, toDayID, "", "", "", "", "");


            if (result == true && ConstantInfo.LogStatus == true)
            {
                string actcode = Request.QueryString["actcode"].ToString();
                string dayid = Request.QueryString["dayid"].ToString();

                string eventtype = "Import from LC-Article (" + actcode + ")";
                string eventdesc = "Imported LC from " + fromLC + "to " + toLC;
                string eventdesc2 = "Article from: " + fromDayID + ", To DayID: " + toDayID;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }

            if (result)
            {
                this.ShowLcInformation();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Information Imported Successfully!');", true);

                return;
            } 
        }

        private void ShowLcInformation()
        {

            string comcod = this.GetComeCode();
            string Mlccode = this.ddlMLC.SelectedValue.ToString();
            string ordertype = this.dllorderType.SelectedValue.ToString();
            DataSet ds2 = MasterLc.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", Mlccode, ordertype, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[0];
            ViewState["tblinfo"] = ds2.Tables[0];
            ViewState["UserLog"] = ds2.Tables[1];

            DataSet ds1 = (DataSet)ViewState["tbllcgeninfo"];

            var lst12 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];
            var lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>)ViewState["tblcurdesc"];



            DataView dv1;
            this.gvPersonalInfo.DataSource = ds2.Tables[0];
            this.gvPersonalInfo.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {
                    case "010100101001": //Contact Type
                        dv1 = ds1.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '26%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101043": //Payment Terms
                        dv1 = ds1.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '16%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101046": //Delivery Terms
                        dv1 = ds1.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '14%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101047": //Freight Terms
                        dv1 = ds1.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '13%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101060": //Partial shipment
                        dv1 = ds1.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '23%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101064": //Trans shipment
                        dv1 = ds1.Tables[7].DefaultView;
                        dv1.RowFilter = ("gcod like '24%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "010100101013": //BUYER LIST
                        dv1 = ds1.Tables[0].DefaultView;
                        dv1.RowFilter = ("sircode not like '%000'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "010100101014": //BUYING HOUSE
                        dv1 = ds1.Tables[1].DefaultView;
                        dv1.RowFilter = ("sircode not like '%000'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "010100101018": //SENDER BANK
                        dv1 = ds1.Tables[2].DefaultView;
                        dv1.RowFilter = ("sircode not like '%000'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101020":// RECEIVE BANK ----29

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = ds1.Tables[3];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "010100101022": // LEAN BANK---29

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = ds1.Tables[4];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "010100101024": // NOTIFY PARTY 1
                        dv1 = ds1.Tables[5].DefaultView;
                        dv1.RowFilter = ("sircode not like '%000'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101025": // NOTIFY PARTY 2
                        dv1 = ds1.Tables[10].DefaultView;
                        dv1.RowFilter = ("sircode like '010100129%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101026": // NOTIFY PARTY 3
                        dv1 = ds1.Tables[10].DefaultView;
                        dv1.RowFilter = ("sircode like '010100130%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101009": // CURRENCY

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "curdesc";
                        ddlgval.DataValueField = "curcode";
                        ddlgval.DataSource = lst1;//ds1.Tables[6];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "010100101054": // iNSURANCE
                        dv1 = ds1.Tables[8].DefaultView;
                        //dv1.RowFilter = ("sircode not like '%000'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101052": // Country
                        dv1 = ds1.Tables[9].DefaultView;
                        //dv1.RowFilter = ("sircode not like '%000'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "name";
                        ddlgval.DataValueField = "id";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101057": // Port of Loading
                        dv1 = ds1.Tables[10].DefaultView;
                        dv1.RowFilter = ("sircode like '010100421%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101058": // Port of Discharge
                        dv1 = ds1.Tables[10].DefaultView;
                        dv1.RowFilter = ("sircode like '010100422%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "010100101056": // Shiping Mark
                        dv1 = ds1.Tables[10].DefaultView;
                        dv1.RowFilter = ("sircode like '010100128%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    //case "010100101001": // Datetime
                    case "010100101006":
                    case "010100101048":
                    case "010100101050":
                        //case "01008":
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
            Bind_docfile();


        }
        private void Bind_docfile()
        {
            DataTable dt = (DataTable)ViewState["UserLog"];
            if (dt.Rows.Count == 0)
                return;

            Image imgname = (Image)this.LoadImg;

            string extension = Path.GetExtension(dt.Rows[0]["lcdoc"].ToString());
            if (extension == "")
            {
                return;
            }

            imgname.Visible = true;
            switch (extension)
            {
                case ".PNG":
                case ".png":
                case ".JPEG":
                case ".JPG":
                case ".jpg":
                case ".jpeg":
                case ".GIF":
                case ".gif":
                    imgname.ImageUrl = "~/Upload/orderdoc/" + dt.Rows[0]["lcdoc"].ToString();
                    break;
                case ".PDF":
                case ".pdf":
                    imgname.ImageUrl = "~/images/pdf.png";
                    break;
                case ".xls":
                case ".xlsx":
                    imgname.ImageUrl = "~/images/excel.svg";
                    break;
                case ".doc":
                case ".docx":
                    imgname.ImageUrl = "~/images/word.png";
                    break;


            }
            btnLink.HRef = "~/Upload/orderdoc/" + dt.Rows[0]["lcdoc"].ToString();
        }


        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblinfo"];
            string Gvalue = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvgval")).Text.Trim();

                if (Gcode == "010100101006" || Gcode == "010100101048" || Gcode == "010100101050")
                {

                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }

                else
                {
                    Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                }
                dt.Rows[i]["gdesc1"] = Gvalue;
            }

            ViewState["tblinfo"] = dt;

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();

            string mlccode = this.ddlMLC.SelectedValue.ToString();
            string dayid = this.dllorderType.SelectedValue.ToString();


            DataTable dt = (DataTable)ViewState["tblinfo"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";

            //DataSet ds2 = MasterLc.GetTransInfoNew(comcod, "SP_ENTRY_MASTERLC_02", "UPDATEMLCDATA", ds1, null, null, mlccode, "", "", "", "", "", "", "");



            bool result = MasterLc.UpdateXmlTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "UPDATEMLCDATA", ds1, null, null, mlccode, dayid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated fail');", true);

             //   ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";

                return;
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

             //   ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            }



        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (code == "01002") //if (code == "01002" || code == "01999")
            //    {

            //        txtgvname.ReadOnly = true;

            //    }

            //}


        }



        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string Joindate = "";
            //for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            //{
            //    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

            //    switch (Gcode)
            //    {
            //        case "01003": //Join Date

            //            Joindate = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
            //                : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();


            //            break;


            //        case "01006": //Confirmation Date
            //            string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
            //            int monyear = (value.Contains("Month")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
            //            string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd-MMM-yyyy");
            //            ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvdVal")).Text = ConDate;
            //            break;
            //    }


            //}


        }



        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComeCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string lccod = "";

            if (AsyncFileUpload1.HasFile)
            {
                lccod = this.ddlMLC.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/orderdoc/") + comcod + lccod + extension);

                Url = comcod + lccod + extension;

            }

            bool result = MasterLc.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATE_ORDER_DOC", lccod, Url, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {


                //this.lblMesg.Text=" Successfully Updated ";
            }

        }

        

        protected void ddlMLC2_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tbllcorder"];
            string mlccode2 = this.ddlMLC2.SelectedValue.ToString();

            this.dllorderType2.DataSource = tbl1.FindAll(x => x.mlccod == mlccode2);
            this.dllorderType2.DataTextField = "rdaydesc";
            this.dllorderType2.DataValueField = "rdayid";
            this.dllorderType2.DataBind();
        }
    }
}