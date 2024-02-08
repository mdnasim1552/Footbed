using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.IO;
using AjaxControlToolkit;
using SPEENTITY;

namespace SPEWEB.F_01_Mer
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        Common Commonobj = new Common();
       
        UserManagerSampling objUserMan = new UserManagerSampling();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

           
                this.GetGenCode();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Order Details Input";
                this.CommonButton();
                this.GetMasterLc();
                this.GetOrderType();
                //if (this.GetCompCode() == "5301")
                //{
                //    this.HyplinkProforma.Visible = false;
                //}
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                if (this.Request.QueryString["actcode"].ToString().Length > 0)
                {
                    this.ddlmlccod.SelectedValue = this.Request.QueryString["actcode"].ToString();
                    this.lbtnOk_Click(null, null);
                }
                this.GetComponentList();
                this.SelectView();

                string type = Request.QueryString["Type"].ToString();

                if (type == "Reorder")
                {
                    this.GetSeason();
                }
            }
        }

        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;
        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();

            this.DdlSeason.SelectedValue = "00000";
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
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(Order_Approved);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString();
            this.OrdrSumpan.Visible = false;
            if (type == "Reorder")
            {
                this.Label1.Text = "R.O.Date";
                this.OrdrSumpan.Visible = true;
                this.lblpayterms.Visible = false;
                this.txtpayterms.Visible = false;
                this.Companel.Visible = false;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetMasterLc()
        {

            string comcod = GetCompCode();
            string txtsrch = "1601%";
            //string CallType = (this.Request.QueryString["Type"].Trim() == "0") ? "LCList" : "DTLLCLIST";
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "DTLLCLIST", "", txtsrch, "", "", "", "", "", "", ""); ;
            if (ds1 == null)
                return;
            this.ddlmlccod.DataTextField = "actdesc1";
            this.ddlmlccod.DataValueField = "actcode";
            this.ddlmlccod.DataSource = ds1.Tables[0];
            this.ddlmlccod.DataBind();

        }

        private void GetOrderType()
        {
            string type = this.Request.QueryString["Type"].ToString();
            string comcod = GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_SALGINF_CODEBOOKINFO", "85%", "", "", "", "", "", "", "");
            ;
            if (ds1 == null)
                return;
            this.DdlOrderType.DataTextField = "gdesc";
            this.DdlOrderType.DataValueField = "gcod";
            this.DdlOrderType.DataSource = ds1.Tables[0];
            this.DdlOrderType.DataBind();
            if (type == "Entry")
            {
                this.DdlOrderType.SelectedValue = "00000";
                this.DdlOrderType.Enabled = false;
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.ddlmlccod.Enabled = false;
                this.txtDatefrom.Enabled = false;
                this.lbtnOk.Text = "New";
                this.lblsum.Visible = true;
                this.Get_Order_info();
                //this.OrderDetailsInformation();
                string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");
                this.HyplinkProforma.NavigateUrl = "~/F_03_CostABgd/SalesContact?Type=Entry&genno=&actcode=" + this.ddlmlccod.SelectedValue.ToString() + "&dayid=" + dayid + "&sircode=";

                return;
            }
            this.gvorderinfo.DataSource = null;
            this.gvorderinfo.DataBind();
            this.gvordersumm.DataSource = null;
            this.gvordersumm.DataBind();
            this.gv1.DataSource = null;
            this.gv1.DataBind();
            this.lblsum.Visible = false;
            this.ddlmlccod.Enabled = true;
            this.txtDatefrom.Enabled = true;
            this.pnlSeason.Visible = false;
            this.DdlSeason.SelectedValue = "00000";
            this.lbtnOk.Text = "Ok";

        }

        private void Get_Order_info()
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            string date = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "MCLC_WISE_ORDERINFO", mlccod, type, date, "", "", "", "", ""); ;
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COLOR_SIZE_ORDER", ds1.Tables[0].Rows[0]["inqno"].ToString(), ds1.Tables[0].Rows[0]["styleid"].ToString(), mlccod, type, date, "", "", "", "");

            DataView dv1 = ds2.Tables[1].Copy().DefaultView;
            dv1.RowFilter = "sizeselect = 'y'";
            DataTable dt = dv1.ToTable();
            if (dt.Rows.Count > 0)
            {
                ds1.Tables[0].Rows[0]["sizernge"] = dt.Rows[0]["sizedesc"].ToString() + "-" + dt.Rows[dt.Rows.Count - 1]["sizedesc"].ToString();

            }
            DataView dv2 = ds2.Tables[0].Copy().DefaultView;
            dv2.RowFilter = "colorselect = 'y' and ordqty > 0";
            DataTable dt2 = dv2.ToTable();
            if (dt2.Rows.Count > 0)
            {
                ds1.Tables[0].Rows[0]["color"] = "";
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    ds1.Tables[0].Rows[0]["color"] += i == 0 ? dt2.Rows[i]["colordesc"].ToString() : "," + dt2.Rows[i]["colordesc"].ToString();
                }

            }
            ViewState["orderinfo"] = ds1.Tables[0];
            ViewState["colorwiseorder"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MASTER_LC_ORDER_SUMMARY", mlccod, "", "", "", "", ""); ;
            ViewState["ordersummary"] = ds.Tables[0];

            this.Data_bind();
        }

        private void Data_bind()
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["orderinfo"];
            if (dt == null)
                return;
            this.DdlOrderType.SelectedValue = dt.Rows[0]["ordtype"].ToString();
            if (this.Request.QueryString["Type"].ToString() == "Reorder")
            {

                this.gvorderinfo.Columns[14].Visible = false;
                this.gvorderinfo.Columns[15].Visible = false;
                //this.gvorderinfo.Columns[14].Visible = false;
                //this.gvorderinfo.Columns[15].Visible = false;
                this.pnlSeason.Visible = true;

                string season = dt.Rows[0]["season"].ToString();
                this.DdlSeason.SelectedValue = season;
            }
            if (this.Request.QueryString["Type"].ToString() == "Approved")
            {

                this.gvorderinfo.Columns[21].Visible = true; //approval

            }
            if (comcod == "5305" || comcod == "5306")
            {
                this.gvorderinfo.Columns[17].Visible = false;
            }
            this.gvorderinfo.DataSource = dt;
            this.gvorderinfo.DataBind();

            DataTable dt1 = (DataTable)ViewState["ordersummary"];
            if (dt1 == null || dt1.Rows.Count == 0)
                return;
            this.gvordersumm.DataSource = dt1;
            this.gvordersumm.DataBind();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            ((HyperLink)this.gvordersumm.FooterRow.FindControl("HypMainOrder")).NavigateUrl = "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;
            ((HyperLink)this.gvordersumm.FooterRow.FindControl("HypMainOrder")).Target = "blank";
        }

        protected void lblgvColor_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string styleid = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string inqno = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvinqno")).Text.ToString();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            string date = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COLOR_SIZE_ORDER", inqno, styleid, mlccod, type, date, "", "", "", "");
            ViewState["tblcolor"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            ViewState["tblsizerange"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            this.lblStylecode.Text = styleid;
            this.MOdal_Color_Bind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        private void MOdal_Color_Bind()
        {
            this.sizepanel.Visible = false;
            this.UploadPanel.Visible = false;
            this.Brandpanel.Visible = false;
            this.Colorpanel.Visible = true;
            this.lblbtnSave.Visible = true;
            this.lbtnUpdateSize.Visible = false;
            this.lbtnUpdateBrand.Visible = false;
            this.lbtnRefresh.Visible = false;
            var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
            this.gvColor.DataSource = color;
            this.gvColor.DataBind();
        }


        protected void lblbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Color_Save_Value();
                string comcod = this.GetCompCode();
                string mlccod = this.ddlmlccod.SelectedValue.ToString();
                string styleid = lblStylecode.Text.Trim().ToString();
                string date = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
                string type = this.Request.QueryString["Type"].ToString();
                var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(ASITUtility03.ListToDataTable(color));
                ds.Tables[0].TableName = "tblcolor";
                bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_COLOR_WISE_ORDER_QUANTITY", ds, null, null, mlccod, styleid, type, date);
                if (result)
                    this.Get_Order_info();
                // this.OrderDetailsInformation();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }

        private void Color_Save_Value()
        {
            var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
            for (int i = 0; i < this.gvColor.Rows.Count; i++)
            {
                string colordesc = ((TextBox)gvColor.Rows[i].FindControl("txtColorDesc")).Text.ToString();
                string colorselect = (((CheckBox)gvColor.Rows[i].FindControl("gvChkColor1")).Checked == true) ? "Y" : "n";
                double ordqty = Convert.ToDouble("0" + ((TextBox)gvColor.Rows[i].FindControl("txtColorqty")).Text.Trim());

                color[i].colordesc = colordesc;
                color[i].colorselect = colorselect;
                color[i].ordqty = ordqty;
            }
            ViewState["tblcolor"] = color;
        }

        private void Save_Value()
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["orderinfo"];
            string ordtype = this.DdlOrderType.SelectedValue.ToString();
            for (int i = 0; i < this.gvorderinfo.Rows.Count; i++)
            {
                string shipmntdat = "01-01-2010";
                string ordrno = ((TextBox)gvorderinfo.Rows[i].FindControl("txtgvordno")).Text.ToString();
                string price = Convert.ToDouble("0" + ((TextBox)gvorderinfo.Rows[i].FindControl("txtgvPrice")).Text.Trim()).ToString();
                string ordrcvdat = Convert.ToDateTime(((TextBox)gvorderinfo.Rows[i].FindControl("txtgvordrcvdat")).Text).ToString("dd-MMM-yyyy");
                if (comcod == "5301")
                {
                    shipmntdat = Convert.ToDateTime(((TextBox)gvorderinfo.Rows[i].FindControl("txtgvshipdat")).Text).ToString("dd-MMM-yyyy");

                }
                string remarks = Convert.ToString(((TextBox)gvorderinfo.Rows[i].FindControl("txtgvRemarks")).Text);
                string exrate = Convert.ToDouble("0" + ((TextBox)gvorderinfo.Rows[i].FindControl("txtgvExrate")).Text.Trim()).ToString();

                dt.Rows[i]["ordrno"] = ordrno;
                dt.Rows[i]["ordrcvdat"] = ordrcvdat;
                dt.Rows[i]["shipmntdat"] = shipmntdat;
                dt.Rows[i]["price"] = price;
                dt.Rows[i]["remarks"] = remarks;
                dt.Rows[i]["exrate"] = exrate;
                dt.Rows[i]["ordtype"] = ordtype;
            }
            ViewState["orderinfo"] = dt;
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            string comcod = this.GetCompCode();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            string date = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)ViewState["orderinfo"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tblcolor";
            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_ORDER_INF", ds, null, null, mlccod, type, date);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to save data');", true);
                return;
            }
            this.Update_Order_Allocation();

            this.Update_Pack_Allocation();
            this.Update_additional_Allocation();
            if (result == true && ConstantInfo.LogStatus == true)
            {
                string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");

                string eventtype = "Order Details Update";
                string eventdesc = "Update Order Information, Details Code" + mlccod + dayid;
                string eventdesc2 = "Mlccod- " + mlccod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                string msgbody = "Article Name: " + this.ddlmlccod.SelectedItem.ToString() + " Information Changed.";

                DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_REPORT_NOTICE", "TYPE_WISE_USERLIST", "05%");
                if (ds2 != null)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds2.Tables[0].Rows)
                        {
                            bool result2 = Commonobj.SendNotification("Order Details Update", msgbody, item["usrid"].ToString());

                        }
                    }
                }


                string esubject = "Order Details Update/Modifications";
                string url = "";
                string bodyContent = "Dear Sir, </br>"+ msgbody+"</br> Buyer Name:"+ dt.Rows[0]["buyername"].ToString()+
                    "<br>Order Number:"+ dt.Rows[0]["ordrno"].ToString()+"<br>Order Qty:"+ dt.Rows[0]["ordqty"].ToString();

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                string frmid = dr1[0].ItemArray[3].ToString();

                if (Commonobj.ConfimMail(frmid, esubject, url, bodyContent) == true)
                {

                }

            }
        }

        protected void lblgvSizernge_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");

            string styleid = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string inqno = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvinqno")).Text.ToString();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            string date = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COLOR_SIZE_ORDER", inqno, styleid, mlccod, type, date, "", "", "", "");
            ViewState["tblsizerange"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            this.lblStylecode.Text = styleid;
            this.MOdal_Size_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }

        private void MOdal_Size_Bind()
        {
            this.sizepanel.Visible = true;
            this.UploadPanel.Visible = false;
            this.Colorpanel.Visible = false;
            this.lblbtnSave.Visible = false;
            this.lbtnRefresh.Visible = false;
            this.lbtnUpdateSize.Visible = true;
            this.lbtnUpdateBrand.Visible = false;
            var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsizerange"];
            this.gvSize.DataSource = color;
            this.gvSize.DataBind();

        }


        protected void lbtnUpdateSize_Click(object sender, EventArgs e)
        {
            try
            {
                this.Size_Save_Value();
                string comcod = this.GetCompCode();
                string mlccod = this.ddlmlccod.SelectedValue.ToString();
                string styleid = lblStylecode.Text.Trim().ToString();
                string type = this.Request.QueryString["Type"].ToString();
                string date = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
                var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsizerange"];
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(ASITUtility03.ListToDataTable(color));
                ds.Tables[0].TableName = "tblsize";
                bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_STYLE_WISE_SIZE", ds, null, null, mlccod, styleid, type, date);
                if (result)
                {
                    this.Get_Order_info();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }


        protected void lblgvBrand_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

           // int index = row.RowIndex;

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BRAND_NAME", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;


            //DropDownList ddlbrand = (DropDownList)gvBrand.FindControl("ddlbrand");

            ddlbrand.DataTextField = "gdesc";
            ddlbrand.DataValueField = "gcod";
            ddlbrand.DataSource = ds1.Tables[0];
            ddlbrand.DataBind();


            this.MOdal_Brand_Bind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }


        private void MOdal_Brand_Bind()
        {
            this.sizepanel.Visible = false;
            this.UploadPanel.Visible = false;
            this.Colorpanel.Visible = false;
            this.Brandpanel.Visible = true;
            this.lblbtnSave.Visible = false;
            this.lbtnRefresh.Visible = false;
            this.lbtnUpdateSize.Visible = false;
            this.lbtnUpdateBrand.Visible = true;

        }


        protected void lbtnUpdateBrand_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetCompCode();

                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string inqno = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvinqno")).Text.ToString();

                string gcod = this.ddlbrand.SelectedValue.ToString();

                bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_BRAND_NAME", null, null, null, inqno, gcod);

                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                }


                if (ConstantInfo.LogStatus == true)
                {

                    string eventtype = "Order Update";
                    string eventdesc = "Brand Change";
                    string eventdesc2 = "actcode- " + inqno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }




        private void Size_Save_Value()
        {
            var size = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsizerange"];
            for (int i = 0; i < this.gvSize.Rows.Count; i++)
            {
                string sizedesc = ((TextBox)gvSize.Rows[i].FindControl("txtgvSizeDesc")).Text.ToString();
                string sizeselect = (((CheckBox)gvSize.Rows[i].FindControl("gvChkSize1")).Checked == true) ? "Y" : "n";

                size[i].sizedesc = sizedesc;
                size[i].sizeselect = sizeselect;

            }
            ViewState["tblsizerange"] = size;
        }


        private void Bind_Order_Allocation()
        {
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            this.gv1.DataSource = HiddenSameData(list);
            this.gv1.DataBind();
            this.FooterCal();
            this.OrderINput_Selection();
        }

        protected void Update_Order_Allocation()
        {
            try
            {
                this.Save_Value_order_allocation();

                List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
                if (lst2 == null)
                    return;
                List<SPEENTITY.C_01_Mer.GetOrderWithCat> orderlist = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["colorwiseorder"];
                if (orderlist == null || orderlist.Count == 0 || lst2.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input Actual Order Qty');", true);


                    return;
                }

                foreach (SPEENTITY.C_01_Mer.GetOrderWithCat list5 in orderlist)
                {
                    var vartuallist = lst2.FindAll(p => p.styleid == list5.styleid && p.colorid == list5.colorid).Sum(p => p.totalqty);
                    if (vartuallist != 0)
                    {
                        if (list5.totalqty < vartuallist || list5.totalqty > vartuallist)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid Allocation Qty');", true);



                            return;
                        }

                    }

                }





                DataTable tblstyle = (DataTable)ViewState["orderinfo"];
                DataSet ds = new DataSet("ds");
                ds.Merge(tblstyle);
                ds.Tables[0].TableName = "tblstyle";
                string comcod = GetCompCode();
                string mLCCode = this.ddlmlccod.SelectedValue.ToString();
                string type = this.Request.QueryString["Type"].ToString();
                string mOrderID = "020100101001"; // Order ID
                string orddate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
                string terms = this.txtpayterms.Text.ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                bool result = false;
                string season = this.DdlSeason.SelectedValue;
                List<SPEENTITY.C_01_Mer.EclassPartInOrder> lst = (List<SPEENTITY.C_01_Mer.EclassPartInOrder>)ViewState["Partname"];
                DataSet ds1 = new DataSet("ds1");
                ds1.Tables.Add(ASITUtility03.ListToDataTable(lst));
                ds1.Tables[0].TableName = "tblcomponent";
                // string type = this.Request.QueryString["Type"].ToString();
                //if (this.Request.QueryString["Type"].ToString() != "Reorder")
                //{
                result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_ORDER_DETAILS_INFORMATION", ds1, ds, null, mLCCode, orddate, terms, userid, Terminal, Sessionid, Posteddat, type, season);

                //  }
                DataTable dtsizes = (DataTable)ViewState["tblsizedesc"];
                String[] SizeID = new String[dtsizes.Rows.Count];
                int sizeindex = 0;
                foreach (DataRow item in dtsizes.Rows)
                {
                    SizeID[sizeindex] = item["sizeid"].ToString();
                    sizeindex++;
                }

                for (int i = 0; i < gv1.Rows.Count; i++)
                {
                    string mStyleID = ((Label)gv1.Rows[i].FindControl("lblgvStyleID")).Text.Trim();
                    string mColorID = ((Label)gv1.Rows[i].FindControl("lblgvColorID")).Text.Trim();

                    //String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                    //           "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                    //           "720100101013", "720100101014", "720100101015", "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                    //              "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                    //              "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                    //              "720100101038", "720100101039", "720100101040"};
                    String[] OrderQty = {
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF1")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF2")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF3")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF4")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF5")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF6")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF7")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF8")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF9")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF10")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF11")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF12")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF13")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF14")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF15")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF16")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF17")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF18")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF19")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF20")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF21")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF22")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF23")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF24")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF25")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF26")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF27")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF28")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF29")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF30")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF31")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF32")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF33")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF34")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF35")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF36")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF37")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF38")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF39")).Text.Trim(),
            "0"+((TextBox)gv1.Rows[i].FindControl("txtgvF40")).Text.Trim(),



            };


                    for (int j = 0; j < SizeID.Length; j++)
                    {

                        result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MASTERLC", "SaveOrderDetails", mLCCode, mOrderID, mStyleID, mColorID,
                                       SizeID[j], OrderQty[j], type, orddate, "", "", "", "", "", "", "");

                    }
                }

                if (result)
                {
                    //   this.OrderDetailsInformation();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    if (result == true && ConstantInfo.LogStatus == true)
                    {
                        string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");

                        string eventtype = "Order Size Wise Qty Update";
                        string eventdesc = "Update  Order Size Wise Qty Information, Details Code" + mLCCode + dayid;
                        string eventdesc2 = "Mlccod- " + mLCCode;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                    }
                }
                //this.lbtnBOrderDetails_Click(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);

            }
        }

        protected void Update_Pack_Allocation()
        {
            try
            {
                this.Save_Value_Packing_allocation();
                List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqtyNew"];
                if (lst2 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to save pack allocation. Check if any Customer Order No is blank.');", true);
                    return;
                }
                string comcod = GetCompCode();
                string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");
                string mLCCode = this.ddlmlccod.SelectedValue.ToString();

                bool result = false;

                DataTable dtsizes = (DataTable)ViewState["tblsizedesc"];
                String[] SizeID = new String[dtsizes.Rows.Count];
                int sizeindex = 0;
                foreach (DataRow item in dtsizes.Rows)
                {
                    SizeID[sizeindex] = item["sizeid"].ToString();
                    sizeindex++;
                }


                for (int i = 0; i < lst2.Count; i++)
                {
                    string slnum = lst2[i].slnum;

                    string pStyleID = lst2[i].styleid;
                    string pColorID = lst2[i].colorid;
                    string packid = lst2[i].packid;
                    string custordno = lst2[i].custordno;
                    string custrefno = lst2[i].custrefno;
                    string cartoon = lst2[i].cartoon.ToString();
                    string exfactdate = lst2[i].exfactorydate.ToString();

                    //String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                    //           "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                    //           "720100101013", "720100101014", "720100101015", "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                    //              "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                    //              "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                    //              "720100101038", "720100101039", "720100101040"};
                    String[] OrderQty = {lst2[i].s1.ToString(),
          lst2[i].s2.ToString(),lst2[i].s3.ToString(),
        lst2[i].s4.ToString(),lst2[i].s5.ToString(),  lst2[i].s6.ToString(),lst2[i].s7.ToString(),
            lst2[i].s8.ToString(),lst2[i].s9.ToString(),
           lst2[i].s10.ToString(), lst2[i].s11.ToString(),
           lst2[i].s12.ToString(),lst2[i].s13.ToString(),
           lst2[i].s14.ToString(),            lst2[i].s15.ToString(),
            lst2[i].s16.ToString(),           lst2[i].s17.ToString(),
           lst2[i].s18.ToString(),            lst2[i].s19.ToString(),
            lst2[i].s20.ToString(),            lst2[i].s21.ToString(),
           lst2[i].s22.ToString(),            lst2[i].s23.ToString(),
           lst2[i].s24.ToString(),            lst2[i].s25.ToString(),
            lst2[i].s26.ToString(),            lst2[i].s27.ToString(),
            lst2[i].s28.ToString(),          lst2[i].s29.ToString(),
            lst2[i].s30.ToString(),            lst2[i].s31.ToString(),
           lst2[i].s32.ToString(),           lst2[i].s33.ToString(),
            lst2[i].s34.ToString(),            lst2[i].s35.ToString(),
            lst2[i].s36.ToString(),            lst2[i].s37.ToString(),
          lst2[i].s38.ToString(),            lst2[i].s39.ToString(),
          lst2[i].s40.ToString(),

            };


                    for (int j = 0; j < SizeID.Length; j++)
                    {
                        //if (Convert.ToDouble(OrderQty[j]) > 0)
                        //{

                        result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MASTERLC", "UPDATE_ORDER_PACK_DETAILS", mLCCode, dayid, pStyleID, pColorID, packid, cartoon,
                                       SizeID[j], OrderQty[j], custordno, custrefno, slnum, exfactdate, "", "", "");

                        //  }
                    }


                }

                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    ViewState["tblPackqty"] = null;
                    ViewState["tblPackqty"] = ViewState["tblPackqtyNew"];
                    this.Bind_Pack_Allocation();
                    //   this.OrderDetailsInformation();
                }

                //this.lbtnBOrderDetails_Click(null, null);
            }


            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);


            }
        }

        private void GetComponentList()
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COSTING_WISE_COMPONENT", mlccod, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.ddlPartName.DataTextField = "compname";
            this.ddlPartName.DataValueField = "compcode";
            this.ddlPartName.DataSource = ds2.Tables[0];
            this.ddlPartName.DataBind();
            ddlPartName_SelectedIndexChanged(null, null);
        }

        protected void ddlPartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string component = this.ddlPartName.SelectedValue.ToString();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_PART_WISE_MATERIAL", mlccod, component, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["partresource"] = ds2.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassPartInOrder>();
            this.ddlResourcesCost.DataTextField = "rsirdesc1";
            this.ddlResourcesCost.DataValueField = "rsircode1";
            this.ddlResourcesCost.DataSource = ds2.Tables[0];
            this.ddlResourcesCost.DataBind();
        }

        protected void LbtnAdd_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_01_Mer.EclassPartInOrder> lst = (List<SPEENTITY.C_01_Mer.EclassPartInOrder>)ViewState["Partname"];
            List<SPEENTITY.C_01_Mer.EclassPartInOrder> list2 = (List<SPEENTITY.C_01_Mer.EclassPartInOrder>)ViewState["partresource"];
            string partcode = this.ddlPartName.SelectedValue.ToString();
            string partname = this.ddlPartName.SelectedItem.ToString();
            string rsircode = this.ddlResourcesCost.SelectedValue.ToString();
            string rsirdesc = this.ddlResourcesCost.SelectedItem.ToString();
            var checklist = lst.FindAll(p => p.partcode == partcode);
            if (checklist.Count > 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);

                return;
            }

            var lst2 = list2.FindAll(p => p.rsircode1 == rsircode);
            lst.Add(new SPEENTITY.C_01_Mer.EclassPartInOrder(partcode, partname, lst2[0].rsircode, lst2[0].rsirdesc, lst2[0].spcfcod, lst2[0].spcfdesc, lst2[0].rsirdesc1));

            ViewState["Partname"] = lst;
            this.PartName_Bind();
        }

        private void PartName_Bind()
        {
            List<SPEENTITY.C_01_Mer.EclassPartInOrder> lst = (List<SPEENTITY.C_01_Mer.EclassPartInOrder>)ViewState["Partname"];

            if (lst == null || lst.Count == 0)
                return;
            this.gvPart.DataSource = lst;
            this.gvPart.DataBind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["orderinfo"];
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");


            string month = System.DateTime.Today.ToString("MMMM-yyyy");
            var lst = new List<SPEENTITY.C_01_Mer.OrderDetails>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.OrderDetails();

                obj1.buyerid = dr1["buyerid"].ToString();
                obj1.buyername = dr1["buyername"].ToString();
                obj1.inqno = dr1["inqno"].ToString();
                obj1.ordrno = dr1["ordrno"].ToString();
                //obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);

                obj1.artno = dr1["artno"].ToString();
                obj1.catedesc = dr1["catedesc"].ToString();
                obj1.category = dr1["category"].ToString();
                obj1.comcod = dr1["comcod"].ToString();
                obj1.ordrcvdat = Convert.ToDateTime(dr1["ordrcvdat"]);
                obj1.shipmntdat = Convert.ToDateTime(dr1["shipmntdat"]);
                obj1.color = dr1["color"].ToString();
                obj1.currency = dr1["currency"].ToString();
                obj1.exrate = Convert.ToDouble(dr1["exrate"]);
                obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);
                obj1.price = Convert.ToDouble(dr1["price"]);
                obj1.sizernge = dr1["sizernge"].ToString();
                //string att = obj1.attchmnt;

                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;

                lst.Add(obj1);
            }
            string payterm = this.txtpayterms.Text.Trim().ToString();
            LocalReport rpt1 = new LocalReport();
            var lst1 = (List<SPEENTITY.C_01_Mer.EclassPartInOrder>)ViewState["Partname"];
            DataTable dt2 = (DataTable)ViewState["tblsizedesc"];

            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptOrderSheet", lst, lst1, lst2);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("payterm", payterm));
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), dt2.Rows[i]["SizeDesc"].ToString()));

            }
            rpt1.SetParameters(new ReportParameter("rpttitle", "Order Input Sheet "));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void OrderINput_Selection()
        {

            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            if (lst2 == null || lst2.Count == 0)
                return;

            DataTable dt = ASITUtility03.ListToDataTable(lst2);

            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    double value = Convert.ToDouble(dt.Rows[i]["p" + j + ""]);
                    //   double value=Convert.ToDouble("0" + ((Label)this.gv1.Rows[i].FindControl("lblP"+j+"")).Text.Trim());
                    if (value == 0)
                    {
                        ((TextBox)gv1.Rows[i].FindControl("txtgvF" + j + "")).Enabled = false;
                        ((TextBox)gv1.Rows[i].FindControl("txtgvF" + j + "")).BackColor = System.Drawing.Color.LightGray;
                        ((TextBox)gv1.Rows[i].FindControl("txtgvF" + j + "")).ToolTip = "Can't Production";
                    }
                }
            }
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value_order_allocation();
            this.Bind_Order_Allocation();
            this.Bind_Pack_Allocation();
        }

        private void FooterCal()
        {
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            if (list == null || list.Count == 0)
            {
                return;
            }

          ((Label)this.gv1.FooterRow.FindControl("FLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1.FooterRow.FindControl("FLblgvColTotal")).Text = ((list.Sum(p => p.colqty) == 0) ? 0 : list.Sum(p => p.colqty)).ToString("#,##0;(#,##0); ");


        }

        private void Save_Value_order_allocation()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            if (lst2 == null)
                return;
            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                double s1 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF1")).Text.Trim());
                double s2 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF2")).Text.Trim());
                double s3 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF3")).Text.Trim());
                double s4 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF4")).Text.Trim());
                double s5 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF5")).Text.Trim());
                double s6 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF6")).Text.Trim());
                double s7 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7")).Text.Trim());
                double s8 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF8")).Text.Trim());
                double s9 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF9")).Text.Trim());
                double s10 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF10")).Text.Trim());
                double s11 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF11")).Text.Trim());
                double s12 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF12")).Text.Trim());
                double s13 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF13")).Text.Trim());
                double s14 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF14")).Text.Trim());
                double s15 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF15")).Text.Trim());
                double s16 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF16")).Text.Trim());
                double s17 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF17")).Text.Trim());
                double s18 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF18")).Text.Trim());
                double s19 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF19")).Text.Trim());
                double s20 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF20")).Text.Trim());
                double s21 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF21")).Text.Trim());
                double s22 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF22")).Text.Trim());
                double s23 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF23")).Text.Trim());
                double s24 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF24")).Text.Trim());
                double s25 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF25")).Text.Trim());
                double s26 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF26")).Text.Trim());
                double s27 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF27")).Text.Trim());
                double s28 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF28")).Text.Trim());
                double s29 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF29")).Text.Trim());
                double s30 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF30")).Text.Trim());
                double s31 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF31")).Text.Trim());
                double s32 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF32")).Text.Trim());
                double s33 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF33")).Text.Trim());
                double s34 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF34")).Text.Trim());
                double s35 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF35")).Text.Trim());
                double s36 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF36")).Text.Trim());
                double s37 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF37")).Text.Trim());
                double s38 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF38")).Text.Trim());
                double s39 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF39")).Text.Trim());
                double s40 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF40")).Text.Trim());
                lst2[i].s1 = s1;
                lst2[i].s2 = s2;
                lst2[i].s3 = s3;
                lst2[i].s4 = s4;
                lst2[i].s5 = s5;
                lst2[i].s6 = s6;
                lst2[i].s7 = s7;
                lst2[i].s8 = s8;
                lst2[i].s9 = s9;
                lst2[i].s10 = s10;
                lst2[i].s11 = s11;
                lst2[i].s12 = s12;
                lst2[i].s13 = s13;
                lst2[i].s14 = s14;
                lst2[i].s15 = s15;
                lst2[i].s16 = s16;
                lst2[i].s17 = s17;
                lst2[i].s18 = s18;
                lst2[i].s19 = s19;
                lst2[i].s20 = s20;
                lst2[i].s21 = s21;
                lst2[i].s22 = s22;
                lst2[i].s23 = s23;
                lst2[i].s24 = s24;
                lst2[i].s25 = s25;
                lst2[i].s26 = s26;
                lst2[i].s27 = s27;
                lst2[i].s28 = s28;
                lst2[i].s29 = s29;
                lst2[i].s30 = s30;
                lst2[i].s31 = s31;
                lst2[i].s32 = s32;
                lst2[i].s33 = s33;
                lst2[i].s34 = s34;
                lst2[i].s35 = s35;
                lst2[i].s36 = s36;
                lst2[i].s37 = s37;
                lst2[i].s38 = s38;
                lst2[i].s39 = s39;
                lst2[i].s40 = s10;
                lst2[i].totalqty = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                    s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);
            }
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> orderlist = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["colorwiseorder"];
            if (orderlist == null || orderlist.Count == 0 || lst2.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input Actual Order Qty');", true);




                return;
            }

            foreach (SPEENTITY.C_01_Mer.GetOrderWithCat list5 in orderlist)
            {
                var vartuallist = lst2.FindAll(p => p.styleid == list5.styleid && p.colorid == list5.colorid).Sum(p => p.totalqty);
                if (vartuallist == 0)
                {

                    return;
                }
                if (list5.totalqty < vartuallist || list5.totalqty > vartuallist)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid Allocation Qty');", true);



                    return;
                }
                else
                {

                }

            }

            ViewState["tblOrderQty"] = lst2;



        }

        protected void Order_Approved(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Enabled = false;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string styleid = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string actcode = this.Request.QueryString["actcode"].ToString();

            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "APPROVED_ORDER", null, null, null, actcode, userid, AppDat, trmid, sessionid, styleid);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Order Not Approved');", true);


                return;

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Order Approved');", true);




            //Common.LogStatus("Diagnosis Complite", "QC Qualified", "Recived No: ", centrid + " - " + wrRecvno);
        }

        protected void lblgvItmCodc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string styleid = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            this.lblStylecode.Text = styleid;
            string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_ORDERWISE_ATTACHMENT", mlccod, styleid, dayid);
            if (ds2 == null || ds2.Tables[3].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Before Upload Please Save your Order');", true);

                return;
            }
            Session["tblattchpln"] = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EcLassAttachMent>();
            Session["tblattchcom"] = ds2.Tables[1].DataTableToList<SPEENTITY.C_03_CostABgd.EcLassAttachMent>();
            Session["tblattchsmp"] = ds2.Tables[2].DataTableToList<SPEENTITY.C_03_CostABgd.EcLassAttachMent>();
            //this.Uploadedimg.ImageUrl = inqinfo1[0].images.ToString();
            this.Upload_Modal_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        private void Upload_Modal_Bind()
        {
            this.sizepanel.Visible = false;
            this.UploadPanel.Visible = true;
            this.Colorpanel.Visible = false;
            this.Brandpanel.Visible = false;
            this.Brandpanel.Visible = false;
            this.lblbtnSave.Visible = false;
            this.lbtnUpdateSize.Visible = false;
            this.lbtnRefresh.Visible = true;
        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EcLassAttachMent> tblattchpln = (List<SPEENTITY.C_03_CostABgd.EcLassAttachMent>)Session["tblattchpln"];
            List<SPEENTITY.C_03_CostABgd.EcLassAttachMent> tblattchcom = (List<SPEENTITY.C_03_CostABgd.EcLassAttachMent>)Session["tblattchcom"];
            List<SPEENTITY.C_03_CostABgd.EcLassAttachMent> tblattchsmp = (List<SPEENTITY.C_03_CostABgd.EcLassAttachMent>)Session["tblattchsmp"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string Url = "";
            string posteddat = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string styleid = this.lblStylecode.Text.Trim().ToString();
            string uploadtype = this.ddluploadtype.SelectedValue.ToString();
            if (AsyncFileUpload1.HasFile)
            {
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/") + mlccod + styleid + random + extension);

                Url = "~/Upload/" + mlccod + styleid + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                //dt.Rows.Add(comcod, Url);

            }

            //var index = inqinfo.FindIndex(s => s.styleid == styleid);
            if (uploadtype == "pln")
            {
                tblattchpln.Add(new SPEENTITY.C_03_CostABgd.EcLassAttachMent(mlccod, styleid, tblattchpln.Count + 1, Url, Convert.ToDateTime(posteddat), userid));
            }
            else if (uploadtype == "com")
            {
                tblattchcom.Add(new SPEENTITY.C_03_CostABgd.EcLassAttachMent(mlccod, styleid, tblattchcom.Count + 1, Url, Convert.ToDateTime(posteddat), userid));

            }
            else
            {
                tblattchsmp.Add(new SPEENTITY.C_03_CostABgd.EcLassAttachMent(mlccod, styleid, tblattchsmp.Count + 1, Url, Convert.ToDateTime(posteddat), userid));

            }

            Session["tblattchpln"] = tblattchpln;
            Session["tblattchcom"] = tblattchcom;
            Session["tblattchsmp"] = tblattchsmp;
            DataSet ds1 = new DataSet("ds1");
            DataSet ds2 = new DataSet("ds2");
            ds1.Tables.Add(ASITUtility03.ListToDataTable(tblattchpln));
            ds2.Tables.Add(ASITUtility03.ListToDataTable(tblattchcom));
            ds2.Tables.Add(ASITUtility03.ListToDataTable(tblattchsmp));
            ds1.Tables[0].TableName = "tblattchpln";
            ds2.Tables[0].TableName = "tblattchcom";
            ds2.Tables[1].TableName = "tblattchsmp";
            string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");
            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "UPDATE_STYLEWISE_ATTACHMENT", ds1, ds2, null, mlccod, styleid, dayid);
            if (result)
            {
                AsyncFileUpload1.Dispose();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


            }


        }

        protected void gvorderinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                string mlccod = this.ddlmlccod.SelectedValue.ToString();
                string styleid = ((Label)e.Row.FindControl("lblgvItmCodc")).Text.Trim().ToString();
                DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_ORDERWISE_ATTACHMENT", mlccod, styleid);
                string approve = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approve")).ToString();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string role = hst["userrole"].ToString();
                if (approve == "Ok" && role != "12")
                {
                    ((TextBox)e.Row.FindControl("txtgvordno")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtgvQtyc")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtgvPrice")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtgvordrcvdat")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtgvshipdat")).Enabled = false;
                    ((LinkButton)e.Row.FindControl("lblgvColor")).Enabled = false;
                    ((LinkButton)e.Row.FindControl("lblgvBrand")).Enabled = false;
                }
                ((ListView)e.Row.FindControl("AttchUrlListPln")).DataSource = ds2.Tables[0];
                ((ListView)e.Row.FindControl("AttchUrlListPln")).DataBind();
                ((ListView)e.Row.FindControl("AttchUrlListcom")).DataSource = ds2.Tables[1];
                ((ListView)e.Row.FindControl("AttchUrlListcom")).DataBind();

            }
        }

        private List<SPEENTITY.C_01_Mer.GetOrderWithCat> HiddenSameData(List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst)
        {

            string styleid = "";
            string colorid = "";
            if (lst == null)
                return new List<SPEENTITY.C_01_Mer.GetOrderWithCat>();

            foreach (SPEENTITY.C_01_Mer.GetOrderWithCat c1 in lst)
            {
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString())
                {
                    c1.description = "";
                }

                styleid = c1.styleid.ToString();
                colorid = c1.colorid.ToString();

            }

            return lst;

        }

        private List<SPEENTITY.C_01_Mer.GetOrderWithCat> HiddenSameDataPack(List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst)
        {

            string styleid = "";
            string colorid = "";
            string packid = "";
            if (lst == null)
                return new List<SPEENTITY.C_01_Mer.GetOrderWithCat>();

            foreach (SPEENTITY.C_01_Mer.GetOrderWithCat c1 in lst)
            {
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString() && packid == c1.packid.Substring(0, 3).ToString())
                {
                    c1.description = "";
                }

                styleid = c1.styleid.ToString();
                colorid = c1.colorid.ToString();
                packid = c1.packid.Substring(0, 3).ToString();
            }

            return lst;

        }

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            this.Get_Order_info();
        }

        protected void lblgArtno_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            ViewState.Remove("tblOrderQty");
            string mMLCCOD = this.ddlmlccod.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string styleid = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string date = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mMLCCOD, type, date, styleid, "", "", "", ""); ;

            if (ds1 == null)
                return;
            ViewState["tblsizedesc"] = ds1.Tables[1];
            string mStyleID = "xxxxxxx";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID)
                    ds1.Tables[0].Rows[i]["StyleDesc"] = " >> ";
                mStyleID = ds1.Tables[0].Rows[i]["styleid"].ToString();
            }
            ViewState["tblOrderQty"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            List<SPEENTITY.C_01_Mer.EclassPartInOrder> lst = ds1.Tables[3].DataTableToList<SPEENTITY.C_01_Mer.EclassPartInOrder>();
            ViewState["Partname"] = lst;
            if (ds1.Tables[2].Rows.Count > 0)
            {
                this.txtDatefrom.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["orddat"]).ToString("dd-MMM-yyyy");
                this.txtpayterms.Text = ds1.Tables[2].Rows[0]["trmscond"].ToString();
            }
            for (int i = 5; i < 45; i++)
                this.gv1.Columns[i].Visible = false;

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gv1.Columns[columid + 4].Visible = true;
                this.gv1.Columns[columid + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            this.gv1.EditIndex = -1;

            this.Bind_Order_Allocation();
            this.PartName_Bind();
        }

        protected void txtgvStyle_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            ViewState.Remove("tblPackqty");
            string mMLCCOD = this.ddlmlccod.SelectedValue.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string styleid = ((Label)this.gvorderinfo.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "GET_ORDER_PACKING_DETAILS", mMLCCOD, dayid, styleid, "", "", "", ""); ;

            if (ds1 == null)
                return;
            ViewState["tblsizedesc"] = ds1.Tables[1];
            string mStyleID = "xxxxxxx";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID)
                    ds1.Tables[0].Rows[i]["StyleDesc"] = " >> ";
                mStyleID = ds1.Tables[0].Rows[i]["styleid"].ToString();
            }

            ViewState["tblPackqty"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>().OrderBy(p => p.slnum).ToList();


            for (int i = 11; i < 47; i++)
                this.gv1pack.Columns[i].Visible = false;

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gv1pack.Columns[columid + 10].Visible = true;
                this.gv1pack.Columns[columid + 10].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            this.gv1pack.EditIndex = -1;
            this.Bind_Pack_Allocation();

        }

        private void Bind_Pack_Allocation()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = HiddenSameDataPack(list);
            this.gv1pack.DataSource = lst;
            this.gv1pack.DataBind();
            this.FooterCalPackList();
            //this.OrderINput_Selection();
        }

        private void FooterCalPackList()
        {
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            if (list == null || list.Count == 0)
            {
                return;
            }
         ((Label)this.gv1pack.FooterRow.FindControl("flblgvF1")).Text = ((list.Sum(p => p.p1) == 0) ? 0 : list.Sum(p => p.p1)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF2")).Text = ((list.Sum(p => p.p2) == 0) ? 0 : list.Sum(p => p.p2)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF3")).Text = ((list.Sum(p => p.p3) == 0) ? 0 : list.Sum(p => p.p3)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF4")).Text = ((list.Sum(p => p.p4) == 0) ? 0 : list.Sum(p => p.p4)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF5")).Text = ((list.Sum(p => p.p5) == 0) ? 0 : list.Sum(p => p.p5)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF6")).Text = ((list.Sum(p => p.p6) == 0) ? 0 : list.Sum(p => p.p6)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF7")).Text = ((list.Sum(p => p.p7) == 0) ? 0 : list.Sum(p => p.p7)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF8")).Text = ((list.Sum(p => p.p8) == 0) ? 0 : list.Sum(p => p.p8)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF9")).Text = ((list.Sum(p => p.p9) == 0) ? 0 : list.Sum(p => p.p9)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF10")).Text = ((list.Sum(p => p.p10) == 0) ? 0 : list.Sum(p => p.p10)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF11")).Text = ((list.Sum(p => p.p11) == 0) ? 0 : list.Sum(p => p.p11)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF12")).Text = ((list.Sum(p => p.p12) == 0) ? 0 : list.Sum(p => p.p12)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF13")).Text = ((list.Sum(p => p.p13) == 0) ? 0 : list.Sum(p => p.p13)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF14")).Text = ((list.Sum(p => p.p14) == 0) ? 0 : list.Sum(p => p.p14)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF15")).Text = ((list.Sum(p => p.p15) == 0) ? 0 : list.Sum(p => p.p15)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF16")).Text = ((list.Sum(p => p.p16) == 0) ? 0 : list.Sum(p => p.p16)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF17")).Text = ((list.Sum(p => p.p17) == 0) ? 0 : list.Sum(p => p.p17)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF18")).Text = ((list.Sum(p => p.p18) == 0) ? 0 : list.Sum(p => p.p18)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF19")).Text = ((list.Sum(p => p.p19) == 0) ? 0 : list.Sum(p => p.p19)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF20")).Text = ((list.Sum(p => p.p20) == 0) ? 0 : list.Sum(p => p.p20)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF21")).Text = ((list.Sum(p => p.p21) == 0) ? 0 : list.Sum(p => p.p21)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF22")).Text = ((list.Sum(p => p.p22) == 0) ? 0 : list.Sum(p => p.p22)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF23")).Text = ((list.Sum(p => p.p23) == 0) ? 0 : list.Sum(p => p.p23)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF24")).Text = ((list.Sum(p => p.p24) == 0) ? 0 : list.Sum(p => p.p24)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF25")).Text = ((list.Sum(p => p.p25) == 0) ? 0 : list.Sum(p => p.p25)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF26")).Text = ((list.Sum(p => p.p26) == 0) ? 0 : list.Sum(p => p.p26)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF27")).Text = ((list.Sum(p => p.p27) == 0) ? 0 : list.Sum(p => p.p27)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF28")).Text = ((list.Sum(p => p.p28) == 0) ? 0 : list.Sum(p => p.p28)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF29")).Text = ((list.Sum(p => p.p29) == 0) ? 0 : list.Sum(p => p.p29)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF30")).Text = ((list.Sum(p => p.p30) == 0) ? 0 : list.Sum(p => p.p30)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF31")).Text = ((list.Sum(p => p.p31) == 0) ? 0 : list.Sum(p => p.p31)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF32")).Text = ((list.Sum(p => p.p32) == 0) ? 0 : list.Sum(p => p.p32)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF33")).Text = ((list.Sum(p => p.p33) == 0) ? 0 : list.Sum(p => p.p33)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF34")).Text = ((list.Sum(p => p.p34) == 0) ? 0 : list.Sum(p => p.p34)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF35")).Text = ((list.Sum(p => p.p35) == 0) ? 0 : list.Sum(p => p.p35)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF36")).Text = ((list.Sum(p => p.p36) == 0) ? 0 : list.Sum(p => p.p36)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF37")).Text = ((list.Sum(p => p.p37) == 0) ? 0 : list.Sum(p => p.p37)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF38")).Text = ((list.Sum(p => p.p38) == 0) ? 0 : list.Sum(p => p.p38)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF39")).Text = ((list.Sum(p => p.p39) == 0) ? 0 : list.Sum(p => p.p39)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF40")).Text = ((list.Sum(p => p.p40) == 0) ? 0 : list.Sum(p => p.p30)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("PFLblgvTotal")).Text = ((list.Sum(p => p.cartoon) == 0) ? 0 : list.Sum(p => p.cartoon)).ToString("#,##0;(#,##0); ") + " CTN";
            ((Label)this.gv1pack.FooterRow.FindControl("PFLblgvTotalPair")).Text = ((list.Sum(p => p.psum) == 0) ? 0 : list.Sum(p => p.psum)).ToString("#,##0;(#,##0); ") + " PRS";

        }

        protected void pLbtnCLose_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblPackqty");
            this.gv1pack.DataSource = null;
            this.gv1pack.DataBind();
        }

        protected void LbtnCLose_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblOrderQty");
            this.gv1.DataSource = null;
            this.gv1.DataBind();

        }
        protected void AddLbtnCLose_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblAdditionQty");
            this.Addgv1.DataSource = null;
            this.Addgv1.DataBind();

        }

        private void Save_Value_Packing_allocation()
        {
            //////////// packing /////////////////
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lstpack = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lstpacknew = new List<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            if (lstpack == null)
                return;
            for (int i = 0; i < this.gv1pack.Rows.Count; i++)
            {
                string custordno = ((TextBox)gv1pack.Rows[i].FindControl("TxtCustOrder")).Text.Trim().ToString();

                if (string.IsNullOrEmpty(custordno))
                {
                    ((TextBox)gv1pack.Rows[i].FindControl("TxtCustOrder")).Style.Add("border", "3px solid red");
                    //((TextBox)gv1pack.Rows[i].FindControl("TxtCustOrder")).BorderWidth = 2;
                    //((TextBox)gv1pack.Rows[i].FindControl("TxtCustOrder")).BorderStyle = BorderStyle.Solid;
                    //((TextBox)gv1pack.Rows[i].FindControl("TxtCustOrder")).BorderColor = System.Drawing.Color.Red;
                    lstpack = null;
                    ViewState["tblPackqtyNew"] = lstpack;
                    return;
                }

                string custrefno = ((TextBox)gv1pack.Rows[i].FindControl("TxtRefno")).Text.ToString();

                string packid = ((DropDownList)gv1pack.Rows[i].FindControl("DdlPacklist")).SelectedValue.ToString();
                DateTime exfactdate = Convert.ToDateTime(((TextBox)gv1pack.Rows[i].FindControl("PlblgvExfactDate1")).Text);

                double carton = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("Ptxtcarton")).Text.Trim()));
                double s1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF1")).Text.Trim()));
                double s2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF2")).Text.Trim()));
                double s3 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF3")).Text.Trim()));
                double s4 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF4")).Text.Trim()));
                double s5 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF5")).Text.Trim()));
                double s6 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF6")).Text.Trim()));
                double s7 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF7")).Text.Trim()));
                double s8 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF8")).Text.Trim()));
                double s9 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF9")).Text.Trim()));
                double s10 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF10")).Text.Trim()));
                double s11 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF11")).Text.Trim()));
                double s12 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF12")).Text.Trim()));
                double s13 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF13")).Text.Trim()));
                double s14 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF14")).Text.Trim()));
                double s15 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF15")).Text.Trim()));
                double s16 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF16")).Text.Trim()));
                double s17 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF17")).Text.Trim()));
                double s18 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF18")).Text.Trim()));
                double s19 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF19")).Text.Trim()));
                double s20 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF20")).Text.Trim()));
                double s21 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF21")).Text.Trim()));
                double s22 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF22")).Text.Trim()));
                double s23 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF23")).Text.Trim()));
                double s24 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF24")).Text.Trim()));
                double s25 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF25")).Text.Trim()));
                double s26 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF26")).Text.Trim()));
                double s27 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF27")).Text.Trim()));
                double s28 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF28")).Text.Trim()));
                double s29 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF29")).Text.Trim()));
                double s30 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF30")).Text.Trim()));
                double s31 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF31")).Text.Trim()));
                double s32 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF32")).Text.Trim()));
                double s33 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF33")).Text.Trim()));
                double s34 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF34")).Text.Trim()));
                double s35 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF35")).Text.Trim()));
                double s36 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF36")).Text.Trim()));
                double s37 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF37")).Text.Trim()));
                double s38 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF38")).Text.Trim()));
                double s39 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF39")).Text.Trim()));
                double s40 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF40")).Text.Trim()));

                lstpack[i].exfactorydate = exfactdate;
                lstpack[i].custordno = custordno.Replace(" ", String.Empty);
                lstpack[i].custrefno = custrefno;
                lstpack[i].packid = packid;
                lstpack[i].cartoon = carton;
                lstpack[i].s1 = s1;
                lstpack[i].s2 = s2;
                lstpack[i].s3 = s3;
                lstpack[i].s4 = s4;
                lstpack[i].s5 = s5;
                lstpack[i].s6 = s6;
                lstpack[i].s7 = s7;
                lstpack[i].s8 = s8;
                lstpack[i].s9 = s9;
                lstpack[i].s10 = s10;
                lstpack[i].s11 = s11;
                lstpack[i].s12 = s12;
                lstpack[i].s13 = s13;
                lstpack[i].s14 = s14;
                lstpack[i].s15 = s15;
                lstpack[i].s16 = s16;
                lstpack[i].s17 = s17;
                lstpack[i].s18 = s18;
                lstpack[i].s19 = s19;
                lstpack[i].s20 = s20;
                lstpack[i].s21 = s21;
                lstpack[i].s22 = s22;
                lstpack[i].s23 = s23;
                lstpack[i].s24 = s24;
                lstpack[i].s25 = s25;
                lstpack[i].s26 = s26;
                lstpack[i].s27 = s27;
                lstpack[i].s28 = s28;
                lstpack[i].s29 = s29;
                lstpack[i].s30 = s30;
                lstpack[i].s31 = s31;
                lstpack[i].s32 = s32;
                lstpack[i].s33 = s33;
                lstpack[i].s34 = s34;
                lstpack[i].s35 = s35;
                lstpack[i].s36 = s36;
                lstpack[i].s37 = s37;
                lstpack[i].s38 = s38;
                lstpack[i].s39 = s39;
                lstpack[i].s40 = s10;
                lstpack[i].totalqty = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                    s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);

                lstpack[i].p1 = s1 * carton;
                lstpack[i].p2 = s2 * carton;
                lstpack[i].p3 = s3 * carton;
                lstpack[i].p4 = s4 * carton;
                lstpack[i].p5 = s5 * carton;
                lstpack[i].p6 = s6 * carton;
                lstpack[i].p7 = s7 * carton;
                lstpack[i].p8 = s8 * carton;
                lstpack[i].p9 = s9 * carton;
                lstpack[i].p10 = s10 * carton;
                lstpack[i].p11 = s11 * carton;
                lstpack[i].p12 = s12 * carton;
                lstpack[i].p13 = s13 * carton;
                lstpack[i].p14 = s14 * carton;
                lstpack[i].p15 = s15 * carton;
                lstpack[i].p16 = s16 * carton;
                lstpack[i].p17 = s17 * carton;
                lstpack[i].p18 = s18 * carton;
                lstpack[i].p19 = s19 * carton;
                lstpack[i].p20 = s20 * carton;
                lstpack[i].p21 = s21 * carton;
                lstpack[i].p22 = s22 * carton;
                lstpack[i].p23 = s23 * carton;
                lstpack[i].p24 = s24 * carton;
                lstpack[i].p25 = s25 * carton;
                lstpack[i].p26 = s26 * carton;
                lstpack[i].p27 = s27 * carton;
                lstpack[i].p28 = s28 * carton;
                lstpack[i].p29 = s29 * carton;
                lstpack[i].p30 = s30 * carton;
                lstpack[i].p31 = s31 * carton;
                lstpack[i].p32 = s32 * carton;
                lstpack[i].p33 = s33 * carton;
                lstpack[i].p34 = s34 * carton;
                lstpack[i].p35 = s35 * carton;
                lstpack[i].p36 = s36 * carton;
                lstpack[i].p37 = s37 * carton;
                lstpack[i].p38 = s38 * carton;
                lstpack[i].p39 = s39 * carton;
                lstpack[i].p40 = s10 * carton;

                lstpack[i].psum = lstpack[i].p1 + lstpack[i].p2 + lstpack[i].p3 + lstpack[i].p4 + lstpack[i].p5 + lstpack[i].p6 + lstpack[i].p7 + lstpack[i].p8 + lstpack[i].p9 + lstpack[i].p10 + lstpack[i].p11 + lstpack[i].p12;
                lstpacknew.Insert(lstpacknew.Count, lstpack[i]);
            }
            ViewState["tblPackqtyNew"] = lstpack;
        }

        protected void LbtnCalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value_Packing_allocation();
            this.Bind_Pack_Allocation();
        }

        protected void gv1pack_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>)Session["lstgencode"];
                var lstpack = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "27");
                lstpack.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

                DropDownList DdlPacklist = (DropDownList)e.Row.FindControl("DdlPacklist");
                DdlPacklist.DataTextField = "gdesc";
                DdlPacklist.DataValueField = "gcod";
                DdlPacklist.DataSource = lstpack;
                DdlPacklist.DataBind();
                DdlPacklist.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "packid"));

                TextBox Exfactdate = (TextBox)e.Row.FindControl("PlblgvExfactDate1");
                if(Exfactdate.Text=="01-Jan-1900" || Exfactdate.Text == "01-Jan-0001")
                {
                    Exfactdate.BackColor = System.Drawing.Color.Red;
                    Exfactdate.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        protected void AddMore_Click(object sender, EventArgs e)
        {
            this.Save_Value_Packing_allocation();
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            string styleid = lst[RowIndex].styleid.ToString();
            string colorid = lst[RowIndex].colorid.ToString();
            string packid = lst[RowIndex].packid.ToString();
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> coplist = lst.FindAll(p => p.styleid == styleid && p.colorid == colorid && p.packid == packid);
            SPEENTITY.C_01_Mer.GetOrderWithCat coplist1 = lst.FirstOrDefault(p => p.styleid == styleid && p.colorid == colorid && p.packid == packid);
            string slnum = "000";
            //List<SPEENTITY.C_01_Mer.GetOrderWithCat> finallst =
            if (lst.Count > 0)
            {
                slnum = (Convert.ToInt32(lst.Max(p => p.slnum)) == 0) ? "000" : Convert.ToInt32(lst.Max(p => p.slnum)).ToString();

            }
            string slnum1 = ASTUtility.Right("000" + (Convert.ToInt16(slnum) + 1), 3).ToString();
            var slnumlist = lst.FindAll(p => p.slnum == slnum1);
            if (slnumlist.Count == 0)
            {
                lst.Add(new SPEENTITY.C_01_Mer.GetOrderWithCat(coplist1.comcod, coplist1.mlccod, coplist1.dayid, coplist1.styleid, coplist1.colorid,
                       coplist1.styledesc, coplist1.colordesc, coplist1.styleunit, coplist1.description,
                        coplist1.custordno, coplist1.custrefno, coplist1.packid, 0.00, 0.00, 0.00, 0.00,
                        0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00,
                        0.00, 0.00, 0.00, 0.00, 0.00, 0.00, coplist1.s17, coplist1.s18, coplist1.s19,
                        coplist1.s20, coplist1.s21, coplist1.s22, coplist1.s23, coplist1.s24, coplist1.s25, coplist1.s26, coplist1.s27, coplist1.s28, coplist1.s29, coplist1.s30, coplist1.s31, coplist1.s32,
                        coplist1.s33, coplist1.s34, coplist1.s35, coplist1.s36, coplist1.s37, coplist1.s38,
                        coplist1.s39, coplist1.s40, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00,
                        0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, coplist1.p22,
                        coplist1.p23, coplist1.p24, coplist1.p25, coplist1.p26, coplist1.p27, coplist1.p28, coplist1.p29, coplist1.p30, coplist1.p31, coplist1.p32, coplist1.p33, coplist1.p34, coplist1.p35, coplist1.p36, coplist1.p37,
                        coplist1.p38, coplist1.p39, coplist1.p40, coplist1.totalqty, coplist1.colqty, coplist1.psum, slnum1));
            }

            ViewState["tblPackqty"] = lst.OrderBy(p => p.styleid).ThenBy(p => p.colorid).ToList();
            this.Bind_Pack_Allocation();

        }



        protected void lbtnPush_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            if (lst == null || lst.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sorry,Invalid Destination, Please click on Article');", true);
                return;
            }
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> packlst = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];

            foreach (var item in lst)
            {
                var packlist1 = packlst.FindAll(p => p.styleid == item.styleid && p.colorid == item.colorid).ToList();
                item.s1 = packlist1.Sum(p => p.p1);
                item.s2 = packlist1.Sum(p => p.p2);
                item.s3 = packlist1.Sum(p => p.p3);
                item.s4 = packlist1.Sum(p => p.p4);
                item.s5 = packlist1.Sum(p => p.p5);
                item.s6 = packlist1.Sum(p => p.p6);
                item.s7 = packlist1.Sum(p => p.p7);
                item.s8 = packlist1.Sum(p => p.p8);
                item.s9 = packlist1.Sum(p => p.p9);
                item.s10 = packlist1.Sum(p => p.p10);
                item.s11 = packlist1.Sum(p => p.p11);
                item.s12 = packlist1.Sum(p => p.p12);
                item.s13 = packlist1.Sum(p => p.p13);
                item.s14 = packlist1.Sum(p => p.p14);
                item.s15 = packlist1.Sum(p => p.p15);
                item.s16 = packlist1.Sum(p => p.p16);
                item.s17 = packlist1.Sum(p => p.p17);
                item.s18 = packlist1.Sum(p => p.p18);
                item.s19 = packlist1.Sum(p => p.p19);
                item.s20 = packlist1.Sum(p => p.p20);
                item.s21 = packlist1.Sum(p => p.p21);
                item.s22 = packlist1.Sum(p => p.p22);
                item.s23 = packlist1.Sum(p => p.p23);
                item.s24 = packlist1.Sum(p => p.p24);
                item.s25 = packlist1.Sum(p => p.p25);
                item.s26 = packlist1.Sum(p => p.p26);
                item.s27 = packlist1.Sum(p => p.p27);
                item.s28 = packlist1.Sum(p => p.p28);
                item.s29 = packlist1.Sum(p => p.p29);
                item.s30 = packlist1.Sum(p => p.p30);
                item.s31 = packlist1.Sum(p => p.p31);
                item.s32 = packlist1.Sum(p => p.p32);
                item.s33 = packlist1.Sum(p => p.p33);
                item.s34 = packlist1.Sum(p => p.p34);
                item.s35 = packlist1.Sum(p => p.p35);
                item.s36 = packlist1.Sum(p => p.p36);
                item.s37 = packlist1.Sum(p => p.p37);
                item.s38 = packlist1.Sum(p => p.p38);
                item.s39 = packlist1.Sum(p => p.p39);
                item.s40 = packlist1.Sum(p => p.p40);
            }

            ViewState["tblOrderQty"] = lst;
            this.Bind_Order_Allocation();
        }

        protected void gv1pack_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> packlst = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            string comcod = this.GetCompCode();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");

            int rowindex = (this.gv1pack.PageSize) * (this.gv1pack.PageIndex) + e.RowIndex;

            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MASTERLC", "DELETE_PACK_LIST_ITEM", mlccod, dayid, packlst[rowindex].styleid,
                packlst[rowindex].colorid, packlst[rowindex].slnum, "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            packlst.RemoveAt(rowindex);
            ViewState["tblPackqty"] = packlst;
            this.Bind_Pack_Allocation();
        }


        protected void LbtnAddition_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            ViewState.Remove("tblAdditionQty");
            string mMLCCOD = this.ddlmlccod.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string styleid = ((Label)this.gv1.Rows[index].FindControl("lblgvStyleID")).Text.ToString();
            string colorid = ((Label)this.gv1.Rows[index].FindControl("lblgvColorID")).Text.ToString();
            string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_ADDITIONAL_ORDER", mMLCCOD, dayid, styleid, colorid, "", "", "", ""); ;

            if (ds1 == null)
                return;
            DataTable dtsizes = (DataTable)ViewState["tblsizedesc"];

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            ViewState["tblAdditionQty"] = list;
            for (int i = 5; i < 45; i++)
                this.Addgv1.Columns[i].Visible = false;

            for (int i = 0; i < dtsizes.Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(dtsizes.Rows[i]["sizeid"].ToString(), 2));

                this.Addgv1.Columns[columid + 4].Visible = true;
                this.Addgv1.Columns[columid + 4].HeaderText = dtsizes.Rows[i]["SizeDesc"].ToString().Trim();
            }
            this.Addgv1.EditIndex = -1;


            this.Addgv1.DataSource = HiddenSameData(list);
            this.Addgv1.DataBind();
            //this.FooterCal();


        }
        private void Save_Value_Additional_order_allocation()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblAdditionQty"];
            if (lst2 == null)
                return;
            for (int i = 0; i < this.Addgv1.Rows.Count; i++)
            {
                double s1 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF1")).Text.Trim());
                double s2 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF2")).Text.Trim());
                double s3 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF3")).Text.Trim());
                double s4 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF4")).Text.Trim());
                double s5 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF5")).Text.Trim());
                double s6 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF6")).Text.Trim());
                double s7 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF7")).Text.Trim());
                double s8 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF8")).Text.Trim());
                double s9 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF9")).Text.Trim());
                double s10 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF10")).Text.Trim());
                double s11 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF11")).Text.Trim());
                double s12 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF12")).Text.Trim());
                double s13 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF13")).Text.Trim());
                double s14 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF14")).Text.Trim());
                double s15 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF15")).Text.Trim());
                double s16 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF16")).Text.Trim());
                double s17 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF17")).Text.Trim());
                double s18 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF18")).Text.Trim());
                double s19 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF19")).Text.Trim());
                double s20 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF20")).Text.Trim());
                double s21 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF21")).Text.Trim());
                double s22 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF22")).Text.Trim());
                double s23 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF23")).Text.Trim());
                double s24 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF24")).Text.Trim());
                double s25 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF25")).Text.Trim());
                double s26 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF26")).Text.Trim());
                double s27 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF27")).Text.Trim());
                double s28 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF28")).Text.Trim());
                double s29 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF29")).Text.Trim());
                double s30 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF30")).Text.Trim());
                double s31 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF31")).Text.Trim());
                double s32 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF32")).Text.Trim());
                double s33 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF33")).Text.Trim());
                double s34 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF34")).Text.Trim());
                double s35 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF35")).Text.Trim());
                double s36 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF36")).Text.Trim());
                double s37 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF37")).Text.Trim());
                double s38 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF38")).Text.Trim());
                double s39 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF39")).Text.Trim());
                double s40 = ASTUtility.StrPosOrNagative(((TextBox)Addgv1.Rows[i].FindControl("AddtxtgvF40")).Text.Trim());
                lst2[i].s1 = s1;
                lst2[i].s2 = s2;
                lst2[i].s3 = s3;
                lst2[i].s4 = s4;
                lst2[i].s5 = s5;
                lst2[i].s6 = s6;
                lst2[i].s7 = s7;
                lst2[i].s8 = s8;
                lst2[i].s9 = s9;
                lst2[i].s10 = s10;
                lst2[i].s11 = s11;
                lst2[i].s12 = s12;
                lst2[i].s13 = s13;
                lst2[i].s14 = s14;
                lst2[i].s15 = s15;
                lst2[i].s16 = s16;
                lst2[i].s17 = s17;
                lst2[i].s18 = s18;
                lst2[i].s19 = s19;
                lst2[i].s20 = s20;
                lst2[i].s21 = s21;
                lst2[i].s22 = s22;
                lst2[i].s23 = s23;
                lst2[i].s24 = s24;
                lst2[i].s25 = s25;
                lst2[i].s26 = s26;
                lst2[i].s27 = s27;
                lst2[i].s28 = s28;
                lst2[i].s29 = s29;
                lst2[i].s30 = s30;
                lst2[i].s31 = s31;
                lst2[i].s32 = s32;
                lst2[i].s33 = s33;
                lst2[i].s34 = s34;
                lst2[i].s35 = s35;
                lst2[i].s36 = s36;
                lst2[i].s37 = s37;
                lst2[i].s38 = s38;
                lst2[i].s39 = s39;
                lst2[i].s40 = s10;
                lst2[i].totalqty = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                    s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);
            }


            ViewState["tblAdditionQty"] = lst2;



        }
        protected void Update_additional_Allocation()
        {
            try
            {
                this.Save_Value_Additional_order_allocation();
                List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblAdditionQty"];
                if (lst2 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to save Additional allocation.');", true);
                    return;
                }
                string comcod = GetCompCode();
                string dayid = (this.Request.QueryString["Type"].ToString() == "Entry") ? "00000000" : Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("yyyyMMdd");
                string mLCCode = this.ddlmlccod.SelectedValue.ToString();

                bool result = false;

                DataTable dtsizes = (DataTable)ViewState["tblsizedesc"];
                String[] SizeID = new String[dtsizes.Rows.Count];
                int sizeindex = 0;
                foreach (DataRow item in dtsizes.Rows)
                {
                    SizeID[sizeindex] = item["sizeid"].ToString();
                    sizeindex++;
                }

                for (int i = 0; i < lst2.Count; i++)
                {
                    string pStyleID = lst2[i].styleid;
                    string pColorID = lst2[i].colorid;

                    //String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                    //           "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                    //           "720100101013", "720100101014", "720100101015", "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                    //              "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                    //              "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                    //              "720100101038", "720100101039", "720100101040"};
                    String[] OrderQty = {lst2[i].s1.ToString(),
          lst2[i].s2.ToString(),lst2[i].s3.ToString(),
        lst2[i].s4.ToString(),lst2[i].s5.ToString(),  lst2[i].s6.ToString(),lst2[i].s7.ToString(),
            lst2[i].s8.ToString(),lst2[i].s9.ToString(),
           lst2[i].s10.ToString(), lst2[i].s11.ToString(),
           lst2[i].s12.ToString(),lst2[i].s13.ToString(),
           lst2[i].s14.ToString(),            lst2[i].s15.ToString(),
            lst2[i].s16.ToString(),           lst2[i].s17.ToString(),
           lst2[i].s18.ToString(),            lst2[i].s19.ToString(),
            lst2[i].s20.ToString(),            lst2[i].s21.ToString(),
           lst2[i].s22.ToString(),            lst2[i].s23.ToString(),
           lst2[i].s24.ToString(),            lst2[i].s25.ToString(),
            lst2[i].s26.ToString(),            lst2[i].s27.ToString(),
            lst2[i].s28.ToString(),          lst2[i].s29.ToString(),
            lst2[i].s30.ToString(),            lst2[i].s31.ToString(),
           lst2[i].s32.ToString(),           lst2[i].s33.ToString(),
            lst2[i].s34.ToString(),            lst2[i].s35.ToString(),
            lst2[i].s36.ToString(),            lst2[i].s37.ToString(),
          lst2[i].s38.ToString(),            lst2[i].s39.ToString(),
          lst2[i].s40.ToString(),

            };


                    for (int j = 0; j < SizeID.Length; j++)
                    {
                        //if (Convert.ToDouble(OrderQty[j]) > 0)
                        //{

                        result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "UPDATE_ORDER_ADDITIONAL_QTY", mLCCode, dayid, pStyleID, pColorID,
                                       SizeID[j], OrderQty[j], "", "", "");

                        //  }
                    }


                }

                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }

            }


            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);


            }
        }


    }
}