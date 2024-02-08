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
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_01_Mer
{
    public partial class MerchandProcessEdit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = " Merchandising Process Edit ";
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.CommonButton();
                this.GetComponentList();
                this.GetSesson();
                Load_Project_Combo();

                //test
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFiUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }


        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");


            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";

            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";

            }


            //this.DdlSeason_SelectedIndexChanged(null, null);

        }


        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_Combo();
        }


        private void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            this.SaveValue();

            DataTable dt = (DataTable)ViewState["tblmerchand"];
            string type = this.ddlType.SelectedItem.ToString();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            string detailscod = this.ddlStyle.SelectedValue.ToString();
            bool result = false;
            foreach (DataRow dr2 in dt.Rows)
            {

                string refno = dr2["refno"].ToString();
                string rsircode = dr2["rsircode"].ToString();
                string styleid = dr2["styleid"].ToString();
                string spcfcod = dr2["spcfcode"].ToString();
                string compcode = dr2["compcode"].ToString();
                string mlccod = dr2["mlccod"].ToString();
                string osircode = dr2["osircode"].ToString();
                string spcfcodo = dr2["ospcfcod"].ToString();
                string ocompcode = dr2["ocompcode"].ToString();
                string procode = dr2["procode"].ToString();
                string mattype = dr2["mattype"].ToString();
                string colorid = dr2["colorid"].ToString();
                string fgsize = dr2["fgsize"].ToString();
                string conqty = Convert.ToDouble(dr2["conqty"]).ToString("#,##0.000000;(#,##0.000000); ");
                string westpc = dr2["westpc"].ToString();
                string rstdqty = Convert.ToDouble(dr2["rstdqty"]).ToString("###0.000000;(#,##0.000000); ");
                string oconqty = Convert.ToDouble(dr2["oconqty"]).ToString("#,##0.000000;(#,##0.000000); ");
                string owestpc = dr2["owestpc"].ToString();
                string orstdqty = Convert.ToDouble(dr2["orstdqty"]).ToString("###0.000000;(#,##0.000000); ");
                if (rsircode == osircode && spcfcodo == spcfcod && ocompcode == compcode && conqty == oconqty && orstdqty == rstdqty)
                    continue;
                result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_MER_PROANALYSIS", "FORCE_UPDATE_MERCHANDISING_MATERIAL", refno, mattype, rsircode, spcfcod, compcode, styleid, osircode, spcfcodo, ocompcode, procode, mlccod, colorid, fgsize, conqty, westpc, rstdqty);

                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);

                    return;
                }


            }
            if (result == true && ConstantInfo.LogStatus == true)
            {

                string eventtype = type + " Force Edit";
                string eventdesc = "Update " + type + " From Force Edit, Details Code" + detailscod;
                string eventdesc2 = "Order No- " + orderno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }
            // this.GetRateQtyChangeMsg();

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void Load_Project_Combo()
        {

            this.ddlOrderList.Items.Clear();
            string comcod = this.GetCompCode();
            string FindProject =  "1601%";
            string season = this.DdlSeason.SelectedValue == "00000" ? "%" : this.DdlSeason.SelectedValue + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", FindProject, "%", season, "", "", "", "", "");
            //   DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "mlcdesc";
            this.ddlOrderList.DataValueField = "mlccod";
            this.ddlOrderList.DataSource = ds1.Tables[1];
            this.ddlOrderList.DataBind();
            ViewState["tblordstyle"] = ds1.Tables[0];

            this.ddlOrderList_SelectedIndexChanged(null, null);

        }
        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlOrderList.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode1");
            this.ddlStyle.DataTextField = "styledesc2";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();


            this.ddlStyle_SelectedIndexChanged(null, null);
        }
        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataSet result = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_WISE_INFO", mlccod, styleid, colorid, dayid, "", "", "", "", "");

            if (result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Not Found');", true);
                return;
            }
            this.BuyerName.Text = result.Tables[0].Rows[0]["buyername"].ToString();
            this.ordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");

        }
        protected void ImgbtnFindOrder_Click(object sender, EventArgs e)
        {
            Load_Project_Combo();
        }
        private void CommonButton()
        {
            
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblmerchand"];
            var lst2 = dt.DataTableToList<SPEENTITY.C_01_Mer.EclassMerForceEdit>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptMerForceEdit", lst2, "", "");
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Order Input Sheet "));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblmerchand");
            string comcod = this.GetCompCode();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string type = this.ddlType.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_ORDER_COST_MAT_INFO", orderno, styleid, colorid, dayid, type, "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.gvdetails.DataSource = null;
                this.gvdetails.DataBind();
                return;
            }
            ViewState["tblmerchand"] = ds1.Tables[0];
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblmerchand"];

            if (dt.Rows.Count == 0)
            {
                return;
            }
            string type = this.ddlType.SelectedValue.ToString();
            if (type == "BOM")
            {
                // this.gvdetails.Columns[10].Visible = true;
                this.gvdetails.Columns[11].Visible = false;
                this.gvdetails.Columns[12].Visible = false;
                this.gvdetails.Columns[3].Visible = false;
            }
            else
            {
                this.gvdetails.Columns[3].Visible = false;
                //  this.gvdetails.Columns[10].Visible = true;
                this.gvdetails.Columns[11].Visible = true;
                this.gvdetails.Columns[12].Visible = true;
            }

            this.gvdetails.DataSource = dt;
            this.gvdetails.DataBind();
            Session["Report1"] = gvdetails;
            ((HyperLink)this.gvdetails.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl =
                "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            //((LinkButton)this.gvpurorder.FooterRow.FindControl("lbtnUpdate")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000");
            //this.gvpurorder.Columns[9].Visible= (this.lblvalvounum.Text.Trim() == "00000000000000");



        }

        private void SaveValue()
        {
            string type = this.ddlType.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)ViewState["tblmerchand"];
            for (int i = 0; i < gvdetails.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvdetails.Rows[i].FindControl("txtgvreqty01")).Text.Trim());
                double conqty = Convert.ToDouble("0" + ((TextBox)this.gvdetails.Rows[i].FindControl("txtgvConqty")).Text.Trim());
                double wstge = Convert.ToDouble("0" + ((TextBox)this.gvdetails.Rows[i].FindControl("txtgvWst")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((Label)this.gvdetails.Rows[i].FindControl("lgvRate")).Text.Trim());

                if (type == "BOM")
                {
                    tbl1.Rows[i]["rstdqty"] = qty;
                    tbl1.Rows[i]["stdamt"] = rate * qty;
                }
                else if (type == "CON")
                {
                    tbl1.Rows[i]["conqty"] = conqty;
                    tbl1.Rows[i]["westpc"] = wstge;
                    tbl1.Rows[i]["rstdqty"] = qty;
                }
                else
                {
                    tbl1.Rows[i]["conqty"] = conqty;
                    tbl1.Rows[i]["westpc"] = wstge;
                    tbl1.Rows[i]["rstdqty"] = conqty + ((conqty * wstge) / 100);
                    tbl1.Rows[i]["stdamt"] = (conqty + ((conqty * wstge) / 100)) * rate;
                }

            }

            ViewState["tblmerchand"] = tbl1;
        }



        protected void gvpurorder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvdetails.EditIndex = -1;
            this.Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SetPosition()", true);
        }
        protected void gvpurorder_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvdetails.EditIndex = e.NewEditIndex;
            this.Data_Bind();

            string comcod = this.GetCompCode();
            string mSrchTxt = "%%";
            string mResCode = ((Label)this.gvdetails.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            string mCompCode = ((Label)this.gvdetails.Rows[e.NewEditIndex].FindControl("lblgvCompCod")).Text.Trim();
            string mSpcfCod = ((Label)this.gvdetails.Rows[e.NewEditIndex].FindControl("lblgvSpcfCod")).Text.Trim();
           

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;

            //DropDownList ddl1 = (DropDownList)this.gvdetails.Rows[e.NewEditIndex].FindControl("ddlSupname");
            //ddl1.DataTextField = "ssirdesc1";
            //ddl1.DataValueField = "ssircode";
            //ddl1.DataSource = ds1.Tables[0];
            //ddl1.DataBind();
            //ddl1.SelectedValue = mSupCode;

            // Specification

            DropDownList ddlspeci = (DropDownList)this.gvdetails.Rows[e.NewEditIndex].FindControl("ddlspecification");
            ddlspeci.DataTextField = "spcfdesc2";
            ddlspeci.DataValueField = "spcfcod";
            ddlspeci.DataSource = ds1.Tables[1];
            ddlspeci.DataBind();
            ddlspeci.SelectedValue = mSpcfCod;


            //Materials
            DropDownList ddlmatn = (DropDownList)this.gvdetails.Rows[e.NewEditIndex].FindControl("ddlmaterialname");
            ddlmatn.DataTextField = "rsirdesc3";
            ddlmatn.DataValueField = "rsircode";
            ddlmatn.DataSource = ds1.Tables[2];
            ddlmatn.DataBind();
            ddlmatn.SelectedValue = mResCode;

            DropDownList ddlComponent = (DropDownList)this.gvdetails.Rows[e.NewEditIndex].FindControl("ddlComponent");
            ddlComponent.DataTextField = "resdesc";
            ddlComponent.DataValueField = "rescode";
            ddlComponent.DataSource = (DataTable)ViewState["Component"];
            ddlComponent.DataBind();
            ddlComponent.SelectedValue = mCompCode;


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SetPosition()", true);

        }

        private void GetComponentList()
        {
            string comcod = this.GetCompCode();

            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_INV_STDANA", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;
            ViewState["Component"] = ds2.Tables[0];
            //   string curencyName = this.ddlCurList.SelectedItem.ToString();
            // this.gvCost.Columns[10].HeaderText = "Std. Rate (" + curencyName + ")";
        }
        protected void gvpurorder_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataTable tbl1 = (DataTable)ViewState["tblmerchand"];

            string spcfcod = ((DropDownList)this.gvdetails.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedValue.ToString();
            string spcfdesc = ((DropDownList)this.gvdetails.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedItem.Text.Trim();

            string compcode = ((DropDownList)this.gvdetails.Rows[e.RowIndex].FindControl("ddlComponent")).SelectedValue.ToString();
            string compdesc = ((DropDownList)this.gvdetails.Rows[e.RowIndex].FindControl("ddlComponent")).SelectedItem.Text.Trim();

            string msircode = ((DropDownList)this.gvdetails.Rows[e.RowIndex].FindControl("ddlmaterialname")).SelectedValue.ToString();
            string msirdesc = ((DropDownList)this.gvdetails.Rows[e.RowIndex].FindControl("ddlmaterialname")).SelectedItem.Text.Trim();
            string mAPROVQTY = Convert.ToDouble("0" + ((TextBox)this.gvdetails.Rows[e.RowIndex].FindControl("txtgvConqty")).Text.Trim()).ToString();

            string westpc = Convert.ToDouble("0" + ((TextBox)this.gvdetails.Rows[e.RowIndex].FindControl("txtgvWst")).Text.Trim()).ToString();
            string rstdqty = Convert.ToDouble("0" + ((TextBox)this.gvdetails.Rows[e.RowIndex].FindControl("txtgvreqty01")).Text.Trim()).ToString();

            int index = (this.gvdetails.PageIndex) * this.gvdetails.PageSize + e.RowIndex;

            string type = this.ddlType.SelectedValue.ToString();

            tbl1.Rows[index]["spcfcode"] = spcfcod;
            tbl1.Rows[index]["spcfdesc"] = spcfdesc;
            //if (type == "CON")
            //{
            tbl1.Rows[index]["compcode"] = compcode;
            tbl1.Rows[index]["compdesc"] = compdesc;
            //   }

            tbl1.Rows[index]["rsircode"] = msircode;
            tbl1.Rows[index]["rsirdesc"] = msirdesc;

            tbl1.Rows[index]["rstdqty"] = rstdqty;
            tbl1.Rows[index]["westpc"] = westpc;
            tbl1.Rows[index]["conqty"] = mAPROVQTY;



            ViewState["tblmerchand"] = tbl1;
            this.gvdetails.EditIndex = -1;
            this.Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SetPosition()", true);
        }



        protected void btnDelMat_Click(object sender, EventArgs e)
        {

            //string comcod = this.GetCompCode();
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";


            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            //if (!Convert.ToBoolean(dr1[0]["delete"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}

            //int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string reqno = ((DataTable)ViewState["tblpurchase"]).Rows[RowIndex]["reqno"].ToString();

            //string rsircode = ((DataTable)ViewState["tblpurchase"]).Rows[RowIndex]["rsircode"].ToString();
            //string spcfcod = ((DataTable)ViewState["tblpurchase"]).Rows[RowIndex]["spcfcod"].ToString();




            //bool result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMATPURCHASE", reqno, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            //if (!result)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
            //    return;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            //    lbtnOk_Click(null, null);
            //}


        }
        protected void ddlmaterialname_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddlgval;
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            int index = row.RowIndex;
            string comcod = this.GetCompCode();
            try
            {
                string rsircode = ((DropDownList)this.gvdetails.Rows[index].FindControl("ddlmaterialname")).SelectedValue.ToString();




                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_WISE_SPECIFICATION", rsircode);
                if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                {
                    return;
                }

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    //((DropDownList)this.gvdetails.Rows[i].FindControl("ddlSupname")).Items.Clear();
                    //((DropDownList)this.gvdetails.Rows[i].FindControl("ddlspecification")).Items.Clear();
                    //((DropDownList)this.gvdetails.Rows[i].FindControl("ddlSupname")).DataBind();
                    //((DropDownList)this.gvdetails.Rows[i].FindControl("ddlspecification")).DataBind();

                    return;
                }
                else
                {

                    ddlgval = ((DropDownList)this.gvdetails.Rows[index].FindControl("ddlspecification"));
                    ddlgval.DataTextField = "spcfdesc2";
                    ddlgval.DataValueField = "spcfcod";
                    ddlgval.DataSource = ds1.Tables[0];
                    ddlgval.DataBind();

                }



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Please Servey First!');", true);
            }

        }

        protected void gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Label groupdesc = (Label)e.Row.FindControl("lblgvResDescRpt");
                double amt = Convert.ToDouble("0" + ((TextBox)e.Row.FindControl("txtgvConqty")).Text);

                if (amt == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }



            }
        }
    }
}