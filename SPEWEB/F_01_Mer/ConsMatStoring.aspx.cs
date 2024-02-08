using SPEENTITY.C_22_Sal;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_01_Mer
{
    public partial class ConsMatStoring : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
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
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text =(type=="Matgrp")?"Material Group wise Pre-analysis":
                    (type == "Brand") ? "Brand wise Material Analysis" :
                    "Department wise Material Analysis";
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                CommonButton();
                this.Get_BuyerName();
                this.GetComponentList();
                
                if (type == "Matgrp")
                {
                    this.LblMatGroup.Visible = true;
                    this.DdlMatGrp.Visible = true;

                    this.LblBuyer.Visible = false;
                    this.ddlbuyer.Visible = false;
                    MaterialGroup();
                }
                else
                {
                    this.LblBuyer.Visible = true;
                    this.ddlbuyer.Visible = true;
                    this.LblMatGroup.Visible = false;
                    this.DdlMatGrp.Visible = false;

                }
                if (this.GetCompCode() == "5301")
                {
                    this.PanelRes.Visible = true;
                    this.Panelresspe.Visible = false;
                }
                else
                {
                    this.PanelRes.Visible = false;
                    this.Panelresspe.Visible = true;
                }


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);


        }



        public void CommonButton()
        {
            
           
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
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
        private void Get_BuyerName()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "All")
            {
                
                DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
                ddlbuyer.DataTextField = "sirdesc";
                ddlbuyer.DataValueField = "sircode";
                ddlbuyer.DataSource = ds2.Tables[0];
                ddlbuyer.DataBind();

                DdlFromBuyer.DataTextField = "sirdesc";
                DdlFromBuyer.DataValueField = "sircode";
                DdlFromBuyer.DataSource = ds2.Tables[0];
                DdlFromBuyer.DataBind();

            }
            else
            {
                this.LblBuyer.Text = "Brand Name";
                this.lblFrombuyer.Text = "From Brand Name";
                DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BRAND_NAME", "", "", "", "", "", "", "", "", "");

                ddlbuyer.DataTextField = "gdesc";
                ddlbuyer.DataValueField = "gcod";
                ddlbuyer.DataSource = ds2.Tables[0];
                ddlbuyer.DataBind();

                DdlFromBuyer.DataTextField = "gdesc";
                DdlFromBuyer.DataValueField = "gcod";
                DdlFromBuyer.DataSource = ds2.Tables[0];
                DdlFromBuyer.DataBind();

            }
            
           
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString();
            if (type == "PreCosting")
            {
                //  this.PrintPreCostingSheet();
            }
            else
            {
                //  this.PrintConsumptionSheet();
            }

        }





        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Validation Error!!!');", true);


                return;
            }
            this.UpdateCost();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlbuyer.Enabled = false;
                this.DdlMatGrp.Enabled = false;
                this.analysispanel.Visible = true;
                string type = this.Request.QueryString["Type"].ToString();
                if (type != "Matgrp")
                {
                    this.ImportChckbox.Visible = true;
                    this.LblGrpSpcf.Visible = false;
                    this.DdlSpcgrp.Visible = false;
                }
                else
                {
                    this.lblProcess.Visible = false;
                    this.ddlProcess.Visible = false;
                }
                this.GetProcess();
                return;
            }
            this.ImportChckbox.Visible = false;
            this.lbtnOk.Text = "Ok";
            this.ddlbuyer.Enabled = true;
            this.DdlMatGrp.Enabled = true;
            this.analysispanel.Visible = false;
            this.gvCost.DataSource = null;
            this.gvCost.DataBind();

        }

        protected void GetProcess()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString();
            string filter = "%";
            
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GETPROCESSCODE", filter, "", "", "", "", "", "");
            this.ddlProcess.DataSource = ds3.Tables[0];
            this.ddlProcess.DataTextField = "resdesc";
            this.ddlProcess.DataValueField = "rescode";
            this.ddlProcess.DataBind();


            ViewState["tblcodeType"] = ds3.Tables[0];
            ds3.Dispose();
            this.ddlProcess_SelectedIndexChanged(null, null);
            //this.imgbtnResourceCost_Click(null, null);
        }

        protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnResourceCost_Click(null, null);
            //this.Image2.Visible = false;
            //this.Image3.Visible = false;

            this.ShowConsump();

        }
        protected void MaterialGroup()
        {
            string comcod = this.GetCompCode();
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION", "04%", "", "", "", "", "", "", "", "");
            ViewState["tblresRes"] = ds3.Tables[0];
            DataView dv = ds3.Tables[0].DefaultView;         

            this.DdlMatGrp.DataSource = dv.ToTable(true, "sircode", "sirdesc2");
            this.DdlMatGrp.DataTextField = "sirdesc2";
            this.DdlMatGrp.DataValueField = "sircode";
            this.DdlMatGrp.DataBind();
            DdlMatGrp_SelectedIndexChanged(null,null);
        }
        protected void imgbtnResourceCost_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblres");
            string comcod = this.GetCompCode();
            
            string srchinf = "04%";

            DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ONLY_MATERIAL_LIST", srchinf, "", "", "", "", "", "", "", "");

            //    DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_INV_STDANA", "GETRESCODE_02", SearchInfo, "", "", "", "", "", "", "", "");
           
            this.DdlMatecode.DataSource = ds.Tables[0];
            this.DdlMatecode.DataTextField = "sirdesc";
            this.DdlMatecode.DataValueField = "sircode";
            this.DdlMatecode.DataBind();          

             this.DdlMatecode_SelectedIndexChanged(null, null);

        }

        private void GetComponentList()
        {
            string comcod = this.GetCompCode();
            string rawMaterials = this.ddlResourcesCost.SelectedValue.ToString();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_INV_STDANA", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            ddlComponent.DataTextField = "resdesc";
            ddlComponent.DataValueField = "rescode";
            ddlComponent.DataSource = ds2.Tables[0];
            ddlComponent.DataBind();
            //   string curencyName = this.ddlCurList.SelectedItem.ToString();
            // this.gvCost.Columns[10].HeaderText = "Std. Rate (" + curencyName + ")";
        }
        private void ShowConsump()
        {
            ViewState.Remove("tblstdcost");
            string type = this.Request.QueryString["Type"].ToString();
            string comcod = this.GetCompCode();

            string processcode = (type == "Matgrp")? this.DdlSpcgrp.SelectedValue.ToString() +"%": this.ddlProcess.SelectedValue.ToString() + "%";
            
            string buyerid=(type=="Matgrp")?this.DdlMatGrp.SelectedValue.ToString(): this.ddlbuyer.SelectedValue.ToString();

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_ANALYSIS", processcode, buyerid, "", "", "");
            if (ds4 == null)
            {
                this.gvCost.DataSource = null;
                this.gvCost.DataBind();
                return;
            }
            ViewState["tblstdcost"] = ds4.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {

            string type = this.Request.QueryString["Type"].ToString();
            if (type == "Matgrp")
            {
                this.gvCost.Columns[2].Visible = false;
            }
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            this.gvCost.DataSource = HiddenSameValue(dt);
            this.gvCost.DataBind();
            if (dt.Rows.Count == 0)
                return;
            Session["Report1"] = gvCost;
            ((HyperLink)this.gvCost.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            this.FooterCalculation(dt);




        }
        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvCost.FooterRow.FindControl("lblgvBdamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(convrate)", "")) ? 0.00 : dt.Compute("sum(convrate)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvCost.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvCost.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.0000;(#,##0.0000); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
            //((Label)this.gvCost.FooterRow.FindControl("lblgvttlqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }


        private void othFooterCalculation(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataTable dt2 = (DataTable)ViewState["tblstdcost"];
            double conAmt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(amt)", "")) ? 0.00 : dt2.Compute("sum(amt)", "")));

            double otAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));


        }
        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            DataTable tbl2 = (DataTable)ViewState["tblstdcost"];
            string type = this.Request.QueryString["Type"].ToString();
            string processcode = (type == "Matgrp") ? this.DdlSpcgrp.SelectedValue.ToString() : this.ddlProcess.SelectedValue.ToString();
            string processdesc = (type == "Matgrp") ? this.DdlSpcgrp.SelectedItem.ToString(): this.ddlProcess.SelectedItem.ToString();
            string rescod = this.ddlResourcesCost.SelectedValue.ToString().Substring(0, 12);
            string Specification = this.ddlResourcesCost.SelectedValue.ToString().Substring(12, 12); ;

            if (this.GetCompCode() != "5301")
            {
                rescod = this.DdlMatecode.SelectedValue.ToString();
                Specification = this.DdlSpcfcod.SelectedValue.ToString();

            }
            string compcode = this.ddlComponent.SelectedValue.ToString();
            string compname = this.ddlComponent.SelectedItem.ToString();



            DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and spcfcode='" + Specification + "' and compcode='" + compcode + "'");
            if (dr.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:Selected Component Already Added');", true);

           
                return;
            }
            else
            {
                DataRow dr1 = tbl2.NewRow();
                dr1["procode"] = processcode;
                dr1["prodesc"] = processdesc;
                dr1["rescode"] = rescod;
                dr1["resdesc"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirtdes"].ToString();
                dr1["resunit"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirunit"].ToString();
                dr1["conqty"] = 0;
                dr1["westpc"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["allowance"].ToString();
                dr1["qty"] = 0;
                dr1["rate"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["rate"].ToString();
                dr1["amt"] = 0;
                dr1["convrate"] = 0;
                dr1["percnt"] = 0;
                dr1["spcfcode"] = Specification;
                dr1["spcfdesc"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirdesc"].ToString();
                dr1["compname"] = compname;
                dr1["compcode"] = compcode;
                tbl2.Rows.Add(dr1);
            }
            //    }
            //}




            ViewState["tblstdcost"] = tbl2;
            this.Data_Bind();
        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            double qty, rate, amt, convRate;




            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                string resdesc = ((Label)this.gvCost.Rows[i].FindControl("lblgvDesc")).Text.Trim();
                string resunit = ((Label)this.gvCost.Rows[i].FindControl("txtgvunit0")).Text.Trim();

                double conqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                double wper = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvwestpc")).Text.Trim());

                double netQty = conqty + (conqty * (wper / 100));

                ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text = netQty.ToString("#,##0.000000;(#,##0.000000); ");

                qty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text.Trim());
                rate = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqrateCost")).Text.Trim());
                double amtgrid = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvamtCost")).Text.Trim());
                amt = qty * rate;
                convRate = qty;
                dt.Rows[i]["resdesc"] = resdesc;
                dt.Rows[i]["resunit"] = resunit;
                dt.Rows[i]["conqty"] = conqty;
                dt.Rows[i]["westpc"] = wper;
                dt.Rows[i]["qty"] = qty;
                dt.Rows[i]["rate"] = rate;
                dt.Rows[i]["convrate"] = convRate;


                dt.Rows[i]["amt"] = (qty * rate > 0) ? amt : amtgrid;
            }

            ViewState["tblstdcost"] = dt;


        }
        private void UpdateCost()
        {

            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string PostDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string pcsessionid = hst["session"].ToString();
            string pctrmid = hst["compname"].ToString();
            string pcuserid = hst["usrid"].ToString();
            string pcDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblstdcost"];

            string proscode = (type == "Matgrp") ? "000000000000" : this.ddlProcess.SelectedValue.ToString();
            string buyerid = (type == "Matgrp") ? this.DdlMatGrp.SelectedValue.ToString(): this.ddlbuyer.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            { //
                string procode = dt.Rows[i]["procode"].ToString();
                string rescod = dt.Rows[i]["rescode"].ToString();
                string resdesc = dt.Rows[i]["resdesc"].ToString();
                string runit = dt.Rows[i]["resunit"].ToString();
                string resqty = dt.Rows[i]["qty"].ToString();
                string rate = dt.Rows[i]["rate"].ToString();
                string resamt = dt.Rows[i]["amt"].ToString();
                string percnt = dt.Rows[i]["percnt"].ToString();
                string conqty = dt.Rows[i]["conqty"].ToString();
                string westpc = dt.Rows[i]["westpc"].ToString();
                string comptCode = dt.Rows[i]["compcode"].ToString();
                string spcfcode = dt.Rows[i]["spcfcode"].ToString();

                if (percnt == "")
                    percnt = "0.0";
                if (Convert.ToDouble("0" + resqty) > 0)
                {
                    bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_CONSUMPTION_ANALYSIS", procode, rescod, resqty, resamt, percnt, resdesc, runit, conqty, westpc, spcfcode, comptCode, buyerid, rate);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + Merdata.ErrorObject["Msg"].ToString()+"');", true);

                      
                        return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully.');", true);


            }


        }
        protected void gvCost_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string type = this.Request.QueryString["Type"].ToString();
            string proscode = (type == "Matgrp") ? this.DdlSpcgrp.SelectedValue.ToString() : this.ddlProcess.SelectedValue.ToString();
            string buyerid = (type == "Matgrp") ? this.DdlMatGrp.SelectedValue.ToString() : this.ddlbuyer.SelectedValue.ToString();
            string rescod = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcodeCost")).Text.Trim();
            string spcfcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();
            string compcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcompcode")).Text.Trim();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "DELETE_CONSUMPTION_ANALYSIS", proscode, rescod, compcode, spcfcode, buyerid, "", "", "", "", "", "", "");
            if (!result)
                return;
            int index = (this.gvCost.PageIndex) * this.gvCost.PageSize + e.RowIndex;
            dt.Rows[index].Delete();
            DataView dv = dt.DefaultView;
            ViewState["tblstdcost"] = dv.ToTable();
            this.Data_Bind();
        }


        private DataTable HiddenSameValue(DataTable dt)
        {

            string procode = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (procode == dt.Rows[i]["procode"].ToString())
                {
                    dt.Rows[i]["prodesc"] = "";
                }
                procode = dt.Rows[i]["procode"].ToString();
            }
            ViewState["tblstdcost"] = dt;
            return dt;
        }

        protected void LbtnComponent_Click(object sender, EventArgs e)
        {
            this.GetComponentList();
        }

        protected void txtgvqrateCost_TextChanged(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        protected void DdlMatecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sircod = this.DdlMatecode.SelectedValue.ToString() + "%";


            DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION", sircod, "", "", "", "", "", "", "", "");


            this.ddlResourcesCost.DataSource = ds.Tables[0];
            this.ddlResourcesCost.DataTextField = "sirdesc1";
            this.ddlResourcesCost.DataValueField = "sircode1";
            this.ddlResourcesCost.DataBind();

            this.DdlSpcfcod.DataTextField = "spcfdesc";
            this.DdlSpcfcod.DataValueField = "spcfcod";
            this.DdlSpcfcod.DataSource = ds.Tables[0];
            this.DdlSpcfcod.DataBind();
            ViewState["tblresRes"] = ds.Tables[0];
        }

        protected void LbtnImportSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string frombuyer = this.DdlFromBuyer.SelectedValue.ToString();
            string fromdept = this.ddlProcess.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_ANALYSIS", fromdept, frombuyer, "", "", "");
            if (ds4 == null)
            {               
                return;
            }

            foreach (DataRow dr in ds4.Tables[0].Rows)
            {

                DataRow[] dr1 = dt.Select("compcode='" + dr["compcode"] + "' and  rescode='" + dr["rescode"] + "' and spcfcode='" + dr["spcfcode"] + "'");

                if (dr1.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + dr["resdesc"].ToString() + " Already Added');", true);


                    continue;
                }
                else
                {

                    dt.ImportRow(dr);
                }

            }

            ViewState["tblstdcost"] = dt;


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Loaded Please Save for Import');", true);

            this.Data_Bind();
           
        }

        protected void DdlMatGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblresRes"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode='" + this.DdlMatGrp.SelectedValue.ToString() + "'";

            this.DdlSpcgrp.DataTextField = "spcfdesc";
            this.DdlSpcgrp.DataValueField = "spcfcod";
            this.DdlSpcgrp.DataSource = dv.ToTable();
            this.DdlSpcgrp.DataBind();
        }
    }
}