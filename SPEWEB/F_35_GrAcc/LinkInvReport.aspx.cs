using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;

namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkInvReport : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Final Accounts Reports View/Print Screen
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();
                this.Visable();
                //double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = this.Request.QueryString["Date1"].ToString();
                this.txtDateto.Text = this.Request.QueryString["Date2"].ToString();

            }
            if (this.Request.QueryString["Type"].ToString() == "QuantityB")
            { ((Label)this.Master.FindControl("lblTitle")).Text = "Inventory Report-Quantity Basis"; }
            if (this.Request.QueryString["Type"].ToString() == "AmountB")
            { ((Label)this.Master.FindControl("lblTitle")).Text = "Inventory Report-Amount Basis"; }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void Visable()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "General":

                    break;
                case "QuantityB":
                    break;
                case "AmountB":
                    break;
                case "ProProcess":
                    this.lblDatefrom.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txtDatefrom.Visible = false;
                    this.txtDateto.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    break;
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "General":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetDataForProjectReport();
                    break;
                case "QuantityB":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetInvQB();
                    break;
                case "AmountB":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.GetAmtInvB();
                    break;
                case "ProProcess":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ProProcess();
                    break;
            }

        }

        private void GetDataForProjectReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccProject.SelectedValue.ToString();

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            //GENARALINVRPT
            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetInvQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccProject.SelectedValue.ToString();

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetAmtInvB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccProject.SelectedValue.ToString();

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void ProProcess()
        {
            Session.Remove("tblVeiw");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string Orderno = this.ddlAccProject.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "RPTPROPROCESS", Orderno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvproprocess.DataSource = null;
                this.gvproprocess.DataBind();
                return;

            }
            Session["tblVeiw"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            string STYLEID = dt1.Rows[0]["STYLEID"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }
                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                }
                //if (dt1.Rows[j]["STYLEID"].ToString() == STYLEID)
                //{
                //    STYLEID = dt1.Rows[j]["STYLEID"].ToString();
                //    dt1.Rows[j]["StyleDes"] = "";
                //}
                //else
                //{
                //    STYLEID = dt1.Rows[j]["STYLEID"].ToString();
                //}
            }
            return dt1;
        }
        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "General":
                    this.gvCenStore.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCenStore.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvCenStore.DataBind();
                    break;
                case "QuantityB":
                    this.gvQBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvQBasis.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvQBasis.DataBind();
                    break;
                case "AmountB":
                    this.gvAmtBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAmtBasis.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvAmtBasis.DataBind();
                    break;
                case "ProProcess":
                    this.gvproprocess.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvproprocess.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvproprocess.DataBind();
                    break;
            }

            this.FooterCalculation((DataTable)Session["tblVeiw"]);
        }

        //private DataTable HiddenSameDate(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    string pactcode = dt1.Rows[0]["actcode"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["actcode"].ToString() == pactcode)
        //        {
        //            pactcode = dt1.Rows[j]["actcode"].ToString();
        //            dt1.Rows[j]["actdesc"] = "";
        //        }

        //        else
        //        {
        //            pactcode = dt1.Rows[j]["actcode"].ToString();
        //        }

        //    }
        //    return dt1;

        //}


        protected void ddlAccProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetMaterial();
        }



        protected void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string HeaderCode = "41%";
            string filter = this.txtSearch.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlAccProject.DataSource = dt1;
            this.ddlAccProject.DataTextField = "actdesc1";
            this.ddlAccProject.DataValueField = "actcode";
            this.ddlAccProject.DataBind();

        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "General":
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFOpnAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ?
                        0.00 : dt.Compute("Sum(recam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFtrnsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                        0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFIssAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisamt)", "")) ?
                        0.00 : dt.Compute("Sum(matisamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFStkAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stcamt)", "")) ?
                        0.00 : dt.Compute("Sum(stcamt)", ""))).ToString("#,##0;(#,##0);  ");
                    break;
                case "QuantityB":

                    break;
                case "AmountB":
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFOpnAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ?
                        0.00 : dt.Compute("Sum(recam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFtrnsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                        0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFIssAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisamt)", "")) ?
                        0.00 : dt.Compute("Sum(matisamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFStkAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stcamt)", "")) ?
                        0.00 : dt.Compute("Sum(stcamt)", ""))).ToString("#,##0;(#,##0);  ");
                    //this.gvAmtBasis.DataBind();
                    break;
            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "General":
                    this.rptCentralStock();
                    break;
                case "QuantityB":
                    this.rptCentralStockQB();
                    break;
                case "AmountB":
                    this.rptCentralStockAB();
                    break;
                case "ProProcess":
                    this.RptProProcess();
                    break;
            }

        }

        protected void rptCentralStock()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_11_RawInv.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void rptCentralStockQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvQtyBasis();//.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void rptCentralStockAB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvAmtBasis();//.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptProProcess()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblstpprocess"];
            //ReportDocument RptES = new RMGiRPT.R_15_Pro.RptProProcess();
            //TextObject rptheader = RptES.ReportDefinition.ReportObjects["order"] as TextObject;
            //rptheader.Text = this.ddlAccProject.SelectedItem.Text.Substring(14).ToString();
            //TextObject txtuserinfo = RptES.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //RptES.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //RptES.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = RptES;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ImgbtnFindProj_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void gvCenStore_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCenStore.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvQBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvQBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvAmtBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAmtBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvproprocess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label reqty = (Label)e.Row.FindControl("lblgvrecQty");
                Label trnsqty = (Label)e.Row.FindControl("lblgvtrnsQty");
                Label balqty = (Label)e.Row.FindControl("lblgvBalQty");
                Label descr = (Label)e.Row.FindControl("lblgvStyleDesr");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "AAAAAAAAAAAA")
                {

                    reqty.Font.Bold = true;
                    trnsqty.Font.Bold = true;
                    balqty.Font.Bold = true;
                    descr.Font.Bold = true;
                    descr.Style.Add("text-align", "Right");
                }

            }
        }
    }
}