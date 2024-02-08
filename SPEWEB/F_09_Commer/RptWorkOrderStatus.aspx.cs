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

namespace SPEWEB.F_09_Commer
{
    public partial class RptWorkOrderStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.gvVisibility();
                this.rbtnList1.Visible = false;
                this.ChkBalance.Visible = false;
                this.ChkBalance.Checked = false;
                this.rbtnList1.SelectedIndex = 0;
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetOrderName();
            }
        }

        private void gvVisibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DetailsWorkIOrdStatus":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }
        }



        private void GetOrderName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlOrderName.DataTextField = "pactdesc";
            this.ddlOrderName.DataValueField = "pactcode";
            this.ddlOrderName.DataSource = ds1.Tables[0];
            this.ddlOrderName.DataBind();



        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "WorkIOrdStatus":
                    this.rbtnList1.Visible = true;
                    this.ChkBalance.Visible = true;
                    this.workorderStatus();
                    break;
                case "DetailsWorkIOrdStatus":
                    this.PanelDeWorkOrder.Visible = true;
                    this.DetworkorderStatus();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Show Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void workorderStatus()
        {

            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk.Text = "New";
                this.lblOrderdesc.Text = this.ddlOrderName.SelectedItem.Text.Substring(13);
                this.ddlOrderName.Visible = false;
                this.lblOrderdesc.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.LoadData();
            }
            else
            {
                this.lnkbtnOk.Text = "Ok";
                this.ddlOrderName.Visible = true;
                this.lblOrderdesc.Visible = false;
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
            }
        }
        private void DetworkorderStatus()
        {

            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk.Text = "New";
                this.lblOrderdesc.Text = this.ddlOrderName.SelectedItem.Text.Substring(13);
                this.ddlOrderName.Visible = false;
                this.lblOrderdesc.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.LoadDetailsData();
            }
            else
            {
                this.lnkbtnOk.Text = "Ok";
                this.ddlOrderName.Visible = true;
                this.lblOrderdesc.Visible = false;
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.gvDeWorkOrdSt.DataSource = null;
                this.gvDeWorkOrdSt.DataBind();
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }

        private void LoadData()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Ordercode = this.ddlOrderName.SelectedValue.ToString();
            string balance = (this.ChkBalance.Checked) ? "woz" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONWORKORDERSTATUS", fromdate, todate, Ordercode, balance, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.LoadGrid();

        }
        private void LoadDetailsData()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Ordercode = this.ddlOrderName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTWORKORDERSTATUS", fromdate, todate, Ordercode, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvDeWorkOrdSt.DataSource = null;
                this.gvDeWorkOrdSt.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.LoadGrid();
            this.FooterCalculation();

        }
        private void LoadGrid()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblstatus"];
            switch (type)
            {
                case "WorkIOrdStatus":

                    this.gvReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();
                    break;
                case "DetailsWorkIOrdStatus":
                    this.gvDeWorkOrdSt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvDeWorkOrdSt.DataSource = dt;
                    this.gvDeWorkOrdSt.DataBind();
                    break;
            }



        }
        private DataTable HiddenSameDate(DataTable dt1)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":

                    if (rbtnList1.SelectedIndex == 1)
                    {
                        return dt1;
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        return dt1;
                    }

                    string Ordercode = dt1.Rows[0]["pactcode"].ToString();
                    string reqno = dt1.Rows[0]["reqno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == Ordercode && dt1.Rows[j]["reqno"].ToString() == reqno)
                        {
                            Ordercode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["reqno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat1"] = "";
                        }

                        else
                        {



                            if (dt1.Rows[j]["pactcode"].ToString() == Ordercode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            if (dt1.Rows[j]["reqno"].ToString() == reqno)
                            {
                                dt1.Rows[j]["reqno1"] = "";

                            }
                            Ordercode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["reqno"].ToString();

                        }

                    }
                    break;

                case "DetailsWorkIOrdStatus":

                    if (dt1.Rows.Count == 0)
                        return dt1;
                    string DOrdercode = dt1.Rows[0]["pactcode"].ToString();
                    string orderno = dt1.Rows[0]["orderno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == DOrdercode && dt1.Rows[j]["orderno"].ToString() == orderno)
                        {
                            DOrdercode = dt1.Rows[j]["pactcode"].ToString();
                            orderno = dt1.Rows[j]["orderno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["orderno2"] = "";
                        }

                        else
                        {
                            DOrdercode = dt1.Rows[j]["pactcode"].ToString();
                            orderno = dt1.Rows[j]["orderno"].ToString();
                        }

                    }
                    break;
            }
            return dt1;
        }

        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblstatus"];
            if (dt.Rows.Count == 0)
                return;
            switch (type)
            {
                case "WorkIOrdStatus":

                    break;
                case "DetailsWorkIOrdStatus":
                    //((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFSqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                    //                   0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFUsqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aprovqty)", "")) ?
                    //                0 : dt.Compute("sum(aprovqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFUsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ?
                                    0 : dt.Compute("sum(amount)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":

                    if (rbtnList1.SelectedIndex == 0)
                    {
                        RequisitionBasisStatus();
                    }
                    else if (rbtnList1.SelectedIndex == 1)
                    {
                        ProjectBasisStatus();
                    }
                    break;

                case "DetailsWorkIOrdStatus":
                    this.rptDetWorOrdStatus();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Print Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void ProjectBasisStatus()
        {
            //if (this.lnkbtnOk.Text == "Ok")
            //{
            //    this.lnkbtnOk_Click(null, null);
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string basis = this.rbtnList1.SelectedItem.Text;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs1 = new RMGiRPT.R_09_Commer.RptWorkOrderStatus2();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Work Order Status( " + basis + " )";
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);
            //Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //this.ChkBalance.Checked = false;
        }

        private void RequisitionBasisStatus()
        {
            //if (this.lnkbtnOk.Text == "Ok")
            //{
            //    this.lnkbtnOk_Click(null, null);
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //string basis = this.rbtnList1.SelectedItem.Text;
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs1 = new RMGiRPT.R_09_Commer.RptWorkOrderStatus1();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Work Order Status( " + basis + " )";
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rrs1.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //this.ChkBalance.Checked = false;
        }

        private void rptDetWorOrdStatus()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs1 = new RMGiRPT.R_09_Commer.RptWorkOrderStatus3();
            ////TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            ////rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            ////TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            ////txtTitle.Text = "Work Order Status( " + basis + " )";
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs1.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":
                    this.LoadGrid();
                    break;
                case "DetailsWorkIOrdStatus":
                    this.LoadGrid();
                    break;
            }

        }


        protected void gvDeWorkOrdSt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDeWorkOrdSt.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void imgbtnFindOrder_Click(object sender, ImageClickEventArgs e)
        {
            this.GetOrderName();
        }
    }
}