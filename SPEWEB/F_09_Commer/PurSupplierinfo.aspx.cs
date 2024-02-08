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

namespace SPEWEB.F_09_Commer
{
    public partial class PurSupplierinfo : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //Response.Redirect("~/AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.CommonButton();
                ((Label)this.Master.FindControl("lblTitle")).Text = "SUPPLIERS INFORMATION VIEW/EDIT";
                string sircode = "000000000000";
                
                if (this.ddlSName.Items.Count == 0)
                {
                    this.GetSupplierName();
                }
                this.CurrencyInf();
                if (Request.QueryString.AllKeys.Contains("sircode"))
                {
                    if (this.Request.QueryString["sircode"] != "")
                    {

                        sircode = this.Request.QueryString["sircode"].ToString();
                        this.ddlSName.SelectedValue = sircode;
                        this.lbtnOk_Click(null, null);
                    }
                }
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
        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;

        }
        private void GetSupplierName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "GETPSNAME", txtSProject, "", "", "", "", "", "", "", "");
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
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
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

        private void LoadGrid()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds11 = MktData.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_REGION_INFORMATION", "", "", "", "", "", "", ""); ;
            ViewState["tblorigin"] = ds11.Tables[0];
           
            
            string SupplierCode = this.ddlSName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "SUPPERSONALINFO", SupplierCode, "", "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                string Gcode = ds1.Tables[0].Rows[i]["gcod"].ToString();
                switch (Gcode)
                {

                    //bangla lang disable for few codes  
                    //case "71005":

                    //    var lstloac = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>)ViewState["tblLoca"]; 
                    //     ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;                  
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "hrgdesc";
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "hrgcod";
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = lstloac;
                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();

                    //    break;

                    case "71052":
                        var lstCurryDesc = (List<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>)ViewState["tblcurdesc"];

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataTextField = "curdesc";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataValueField = "curcode";
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataSource = lstCurryDesc;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = (ds1.Tables[0].Rows[i]["gdesc1"].ToString().Trim() == "" || ds1.Tables[0].Rows[i]["gdesc1"].ToString() == null) ? "019" : ds1.Tables[0].Rows[i]["gdesc1"].ToString();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).DataBind();
                        //((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue = ds1.Tables[0].Rows[i]["gdatat"].ToString();

                        break;
                    case "71080":
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        DataTable dtregion = (DataTable)ViewState["tblorigin"];
                        DropDownList ddlorigin = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlorigin.DataTextField = "gdesc";
                        ddlorigin.DataValueField = "gcod";
                        ddlorigin.DataSource = dtregion;
                        ddlorigin.SelectedValue = (ds1.Tables[0].Rows[i]["gdesc1"].ToString().Trim() == "" || ds1.Tables[0].Rows[i]["gdesc1"].ToString() == null) ? "38001" : ds1.Tables[0].Rows[i]["gdesc1"].ToString();
                        ddlorigin.DataBind();
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
                if (Gcode == "71052" || Gcode == "71080")
                {
                    Gvalue = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.Trim();
                }

                MktData.UpdateTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "INSERTORUPDATESUPLINF", ssirCode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "SUPPLIERS INFORMATION";
                string eventdesc = "Update Sup Info";
                string eventdesc2 = this.ddlSName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


    }
}