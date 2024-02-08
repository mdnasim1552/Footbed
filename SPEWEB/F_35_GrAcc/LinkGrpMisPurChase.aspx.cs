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


namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkGrpMisPurChase : System.Web.UI.Page
    {
        ProcessAccess GrpData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.lblHeader.Text = (type == "ReqStatus") ? "Requisition Status Information" : (type == "ReqAppStatus") ? "Requisition Approved Information" : (type == "WorkOrder") ? "Work Order Details"
                    : (type == "Purchase") ? "Purchase Status Information"
                    : "Bill Status Information";

                this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                this.SelectView();


            }
        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "ReqStatus":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowReqStatus();
                    break;


                case "ReqAppStatus":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowReqApproved();
                    break;

                case "WorkOrder":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowDetWorkOrder();
                    break;

                case "Purchase":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowPurchaseSt();
                    break;

                case "ConBill":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowBillSt();
                    break;










            }
        }

        private void ShowReqStatus()
        {

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string pactcode = "000000000000";
            string Reqno = "%";
            string Rsircode = "%";
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "REQSATIONSTATUS", frmdate, todate, pactcode, Reqno, Rsircode, "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatusAp.DataSource = null;
                this.gvReqStatusAp.DataBind();
                return;
            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.Data_Bind();




        }

        private void ShowReqApproved()
        {

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string pactcode = "000000000000";
            string Reqno = "%";
            string Rsircode = "%";
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQAPPSTATUS", frmdate, todate, pactcode, Reqno, Rsircode, "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatusAp.DataSource = null;
                this.gvReqStatusAp.DataBind();
                return;
            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.Data_Bind();

        }


        private void ShowDetWorkOrder()
        {
            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string pactcode = "000000000000";
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTWORKORDERSTATUS", frmdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvDeWorkOrdSt.DataSource = null;
                this.gvDeWorkOrdSt.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.Data_Bind();
            ds1.Dispose();


        }

        private void ShowPurchaseSt()
        {
            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string pactcode = "000000000000";
            string mrfno = "%";
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONMRRSTATUS", frmdate, todate, pactcode, mrfno, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt;
            this.Data_Bind();
            ds1.Dispose();
        }

        private void ShowBillSt()
        {
            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "BILLSTATUS", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBillStatus.DataSource = null;
                this.gvBillStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt;
            this.Data_Bind();
            ds1.Dispose();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode;
            switch (type)
            {
                case "ReqStatus":
                case "ReqAppStatus":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string reqno = dt1.Rows[0]["reqno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["reqno"].ToString() == reqno)
                        {
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat1"] = "";
                            dt1.Rows[j]["eusrname"] = "";
                            dt1.Rows[j]["ausrname"] = "";
                            dt1.Rows[j]["aprvdat"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                                dt1.Rows[j]["pactdesc"] = "";
                            if (dt1.Rows[j]["reqno"].ToString() == reqno)
                                dt1.Rows[j]["reqno1"] = "";
                        }


                        pactcode = dt1.Rows[j]["pactcode"].ToString();
                        reqno = dt1.Rows[j]["reqno"].ToString();

                    }
                    break;




                case "WorkOrder":

                    if (dt1.Rows.Count == 0)
                        return dt1;
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string orderno = dt1.Rows[0]["orderno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["orderno"].ToString() == orderno)
                        {

                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["orderno2"] = "";
                        }
                        pactcode = dt1.Rows[j]["pactcode"].ToString();
                        orderno = dt1.Rows[j]["orderno"].ToString();


                    }
                    break;

                case "ConBill":

                    if (dt1.Rows.Count == 0)
                        return dt1;
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string billno = dt1.Rows[0]["billno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["billno"].ToString() == billno)
                        {

                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["billno1"] = "";
                        }


                        pactcode = dt1.Rows[j]["pactcode"].ToString();
                        billno = dt1.Rows[j]["billno"].ToString();


                    }
                    break;






            }


            return dt1;

        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ReqStatus":
                case "ReqAppStatus":
                    this.gvReqStatusAp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatusAp.DataSource = (DataTable)Session["tblstatus"];
                    this.gvReqStatusAp.DataBind();
                    this.FooterCalculation();
                    break;

                case "WorkOrder":
                    this.gvDeWorkOrdSt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvDeWorkOrdSt.DataSource = (DataTable)Session["tblstatus"];
                    this.gvDeWorkOrdSt.DataBind();
                    this.FooterCalculation();
                    break;

                case "Purchase":
                    this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPurStatus.DataSource = (DataTable)Session["tblstatus"];
                    this.gvPurStatus.DataBind();
                    this.FooterCalculation();
                    break;


                case "ConBill":
                    this.gvBillStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBillStatus.DataSource = (DataTable)Session["tblstatus"];
                    this.gvBillStatus.DataBind();
                    this.FooterCalculation();
                    break;



            }

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ReqStatus":
                case "ReqAppStatus":
                    ((Label)this.gvReqStatusAp.FooterRow.FindControl("lgvreqapAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reqamt)", "")) ?
                               0 : dt.Compute("sum(reqamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "WorkOrder":
                    ((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFUsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ?
                                    0 : dt.Compute("sum(amount)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "ConBill":
                    ((Label)this.gvBillStatus.FooterRow.FindControl("lgvFbillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ?
                                        0 : dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "Purchase":
                    ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                        0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvReqStatusAp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatusAp.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvDeWorkOrdSt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDeWorkOrdSt.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvPurStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBillStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBillStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

    }
}