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
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_05_ProShip
{
    public partial class OrderSheetPlan : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.Load_Project_Combo();

                this.CommonButton();
                //For Sanmar
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Export Plan Input/Edit Screen";
                
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFiUpdate_Click);

            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Session_tbExPlan_Update();
            this.Data_Bind();
        }

        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true; ;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Load_Project_Combo()
        {


            this.ddlOrderList.Items.Clear();
            string comcod = this.GetCompCode();
            string FindProject =  "1601%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", FindProject, "%", "%", "", "", "", "", "", "");
            //   DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "mlcdesc";
            this.ddlOrderList.DataValueField = "mlccod";
            this.ddlOrderList.DataSource = ds1.Tables[1];
            this.ddlOrderList.DataBind();
            ViewState["tblordstyle"] = ds1.Tables[0];
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlOrderList.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }
            this.ddlOrderList_SelectedIndexChanged(null, null);

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rsircode;


            rsircode = dt1.Rows[0]["procname"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["procname"].ToString() == rsircode)
                {
                    dt1.Rows[j]["procname"] = "";

                }
                else
                {
                    rsircode = dt1.Rows[j]["procname"].ToString();
                }


            }

            return dt1;
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            DataTable dt0 = (DataTable)ViewState["tblplndetailinf0"];
            DataTable dt1 = (DataTable)ViewState["tblplndetailinf1"];
            DataTable dt2 = (DataTable)ViewState["tblplndetailinf2"];
            DataTable dt3 = (DataTable)ViewState["tblplndetailinf3"];
            dt3 = this.HiddenSameData(dt3);
            var rptlist = dt0.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan>();
            var rptlist1 = dt1.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan1>();
            var rptlist2 = dt2.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan2>();
            var rptlist3 = dt3.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptExportPlan3>();
            string imgurl = new Uri(Server.MapPath(dt0.Rows[0]["images"].ToString())).AbsoluteUri;
            var ordrqty = dt0.Rows[0]["ordrqty"];

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptExportPlanInEdit", rptlist1, rptlist2, rptlist3);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Order Sheet"));
            rpt1.SetParameters(new ReportParameter("buyername", dt0.Rows[0]["buyername"].ToString()));
            rpt1.SetParameters(new ReportParameter("colordesc", dt0.Rows[0]["colordesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("sizerange", dt0.Rows[0]["sizerange"].ToString()));
            rpt1.SetParameters(new ReportParameter("colororderqty", Convert.ToDouble(dt0.Rows[0]["colororderqty"]).ToString("#,##0;(#,##0);")));
            rpt1.SetParameters(new ReportParameter("brand", dt0.Rows[0]["brand"].ToString()));
            rpt1.SetParameters(new ReportParameter("article", dt0.Rows[0]["article"].ToString()));
            rpt1.SetParameters(new ReportParameter("ordrqty", Convert.ToDouble(ordrqty).ToString("#,##0;(#,##0);  ")));
            rpt1.SetParameters(new ReportParameter("images", imgurl));
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), dt2.Rows[i]["sizedesc"].ToString()));

            }

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.ddlOrderList.Enabled = true;
                this.ddlStyle.Enabled = true;
              
                this.ddlOrderList.Enabled = true;
                this.gvsizes.DataSource = null;
                this.gvsizes.DataBind();

                this.lbtnOk.Text = "Ok";
                return;
            }


           
            this.ddlOrderList.Enabled = false;
            this.ddlOrderList.Enabled = false;
            this.lbtnOk.Text = "New";
            this.ddlStyle.Enabled = false;
            this.Get_Receive_Info();
        }

        protected void Session_tbExPlan_Update()
        {

            DataTable tbl1 = (DataTable)ViewState["tblcost"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvCost.Rows.Count; j++)
            {
                double prwestpc = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[j].FindControl("txtgvwestpc")).Text.Trim()));

                double prwestqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[j].FindControl("txtgvwestqty")).Text.Trim()));
                TblRowIndex2 = (this.gvCost.PageIndex) * this.gvCost.PageSize + j;

                if (prwestpc > 0 && prwestqty > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Use WASTAGE Qty Or Percent (Any One)');", true);
                    return;
                }
                double conpair = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["consppair"]);
                double ordrqty = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["ordrqty"]);
                tbl1.Rows[TblRowIndex2]["prwestqty"] = prwestqty;
                tbl1.Rows[TblRowIndex2]["prwestpc"] = prwestpc;
                tbl1.Rows[TblRowIndex2]["ttlreqrd"] = (prwestpc == 0) ? ((conpair + prwestqty) * ordrqty) : ((conpair + ((conpair * prwestpc) / 100)) * ordrqty);


            }
           
            ViewState["tblcost"] = tbl1;
            
        }

        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblcost"];
            this.gvCost.DataSource = tbl1;
            this.gvCost.DataBind();
            ((HyperLink)this.gvCost.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            if (tbl1.Rows.Count == 0)
                return;
            ////((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            ////double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvShiMentInfo.PageSize);
            ////((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            ////for (int i = 1; i <= TotalPage; i++)
            ////    ((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            ////if (TotalPage > 1)
            ////    ((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
            ////((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvShiMentInfo.PageIndex;
            //this.lbtnResFooterTotal_Click(null, null);
        }
        protected void Get_Receive_Info()
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string toddate = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
            string slnum = this.Request.QueryString["genno"].ToString();

            DataSet result = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSHEET_PLANDETAILS_INFO", mlccod, styleid, colorid, dayid, toddate, slnum, "", "", "");

            ViewState["tblplndetailinf0"] = result.Tables[0];
            ViewState["tblplndetailinf1"] = result.Tables[1];
            ViewState["tblplndetailinf2"] = result.Tables[2];
            ViewState["tblplndetailinf3"] = result.Tables[3];

            if (result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Not Found');", true);
                return;
            }
            for (int i = 0; i < result.Tables[2].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(result.Tables[2].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 2].Visible = true;
                this.gvsizes.Columns[columid + 2].HeaderText = result.Tables[2].Rows[i]["sizedesc"].ToString().Trim();
            }
            this.gvsizes.EditIndex = -1;

            this.gvsizes.DataSource = result.Tables[1];
            this.gvsizes.DataBind();
            this.TotalOrder.Text = Convert.ToDouble(result.Tables[0].Rows[0]["colororderqty"]).ToString("#,##0.00;(#,##0.00); ");
            this.BuyerName.Text = result.Tables[0].Rows[0]["buyername"].ToString();
            this.lblbrand.Text = result.Tables[0].Rows[0]["brand"].ToString();
            this.lblcolor.Text = result.Tables[0].Rows[0]["colordesc"].ToString();

            this.lblarticle.Text = result.Tables[0].Rows[0]["article"].ToString();
            this.lblsizernge.Text = result.Tables[0].Rows[0]["sizerange"].ToString();
            this.SmpleIMg.ImageUrl = result.Tables[0].Rows[0]["images"].ToString();
            this.ordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
            ViewState["tblcost"] = result.Tables[3];
            this.Data_Bind();
        }





        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tbExPlan_Update();
            //  this.gvShiMentInfo.PageIndex = ((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.Data_Bind();
        }

        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            //this.Session_tbExPlan_Update();
            //DataTable tbl1 = (DataTable)Session["tbExPlan"];
            //((Label)this.gvShiMentInfo.FooterRow.FindControl("lgvFSQty")).Text =
            //    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(shimentqty)", "")) ?
            //        0.00 : tbl1.Compute("Sum(shimentqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvShiMentInfo.FooterRow.FindControl("lgvFProQty")).Text =
            //    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(proplanqty)", "")) ?
            //        0.00 : tbl1.Compute("Sum(proplanqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }




        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlOrderList.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc1", "stylecode1", "styledesc2");
            this.ddlStyle.DataTextField = "styledesc1";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();

          
            if (this.Request.QueryString["sircode"].Length > 0)
            {
                this.ddlStyle.SelectedValue = this.Request.QueryString["sircode"].ToString();
                this.lbtnOk_Click(null, null);

            }
            DataView dv2;
            dv2 = dt1.DefaultView;
            string styleid= this.ddlStyle.SelectedValue.ToString();
            dv2.RowFilter = (" stylecode1<>'"+ styleid + "'");
            this.ddlImportStyle.DataTextField = "styledesc2";
            this.ddlImportStyle.DataValueField = "stylecode1";
            this.ddlImportStyle.DataSource = dv2.ToTable();
            this.ddlImportStyle.DataBind();
        }
        protected void ImgbtnFindOrder_Click(object sender, EventArgs e)
        {
            Load_Project_Combo();
        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.Session_tbExPlan_Update();
            DataTable tbl1 = (DataTable)ViewState["tblcost"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(tbl1);
            ds.Tables[0].TableName = "tblcost";
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.Substring(0, 12).ToString();
            string colorid = this.ddlStyle.SelectedValue.Substring(12, 12).ToString();
            string dayid = this.ddlStyle.SelectedValue.Substring(24, 8).ToString();
            var temp = ds.GetXml();
            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_ORDER_SHEET_WASTAGES", ds, null, null, mlccod, styleid, colorid, dayid);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + purData.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }



            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }




        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string mlccod = ((Label)e.Row.FindControl("lblgvMlcode")).Text.ToString();

                if (mlccod == "000000000000")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }
        }

        protected void LbtnImport_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            //destination information
            string styleid = this.ddlStyle.SelectedValue.Substring(0, 12).ToString();
            string colorid = this.ddlStyle.SelectedValue.Substring(12, 12).ToString();
            string dayid = this.ddlStyle.SelectedValue.Substring(24, 8).ToString();

          // import from 
            string styleid2 = this.ddlImportStyle.SelectedValue.Substring(0, 12).ToString();
            string colorid2 = this.ddlImportStyle.SelectedValue.Substring(12, 12).ToString();
            string dayid2 = this.ddlImportStyle.SelectedValue.Substring(24, 8).ToString();
          
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "IMPORT_ORDER_SHEET_WASTAGE", mlccod, styleid, colorid, dayid, styleid2, colorid2, dayid2, "", "");
            if (result)
            {
                this.Get_Receive_Info();
            }
        }
    }
}