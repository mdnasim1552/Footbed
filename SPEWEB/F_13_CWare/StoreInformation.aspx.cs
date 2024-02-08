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
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_13_CWare
{
    public partial class StoreInformation : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Store Information";
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                this.CommonButton();
                
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
                this.GetJobLocation();
                
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

        private void GetJobLocation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string jobLocCode = "87";

            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            ViewState["tblLoca"] = lst;


        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {
            string comcod = this.GetComCode();
            string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETEXPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlPrjName.Enabled = false;
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
            this.ddlPrjName.Enabled = true;
            this.gvPrjInfo.DataSource = null;
            this.gvPrjInfo.DataBind();
        }

        private void LoadGrid()
        {


            string comcod = this.GetComCode();
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MGT", "PROJECTINFO", ProjectCode, "", "", "", "", "", "", "", "");
            this.gvPrjInfo.DataSource = ds1.Tables[0];
            this.gvPrjInfo.DataBind();
            DropDownList ddlgval;
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                string Gcode = ds1.Tables[0].Rows[i]["gcod"].ToString();
                switch (Gcode)
                {
                    case "04005":

                        var lstloac = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>)ViewState["tblLoca"];
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlval")).Visible = true;


                        ddlgval = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "hrgdesc";
                        ddlgval.DataValueField = "hrgcod";
                        ddlgval.DataSource = lstloac;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";

                        break;

                    default:
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = true;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;
                }
            }

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPrjInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string Gunit = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtResunit")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;

                if (Gcode == "04005")
                {
                    Gvalue = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlval")).SelectedValue.Trim();
                }


                MktData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTORUPDATEPRJINF", pactcode, Gcode, gtype, Gvalue, Gunit, "", "", "", "", "", "", "", "", "", "");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Store Information";
                string eventdesc = "Update Store Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



    }
}