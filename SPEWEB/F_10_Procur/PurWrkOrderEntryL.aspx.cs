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
using System.Net.Mail;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_10_Procur
{
    public partial class PurWrkOrderEntryL : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        static string prevPage = String.Empty;
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                Hashtable hst = (Hashtable)Session["tblLogin"];

                string comnam = hst["comnam"].ToString();
                string unitname = "";


                this.GET_Incotrms();
                this.GET_ShipingMode();
                this.GET_ModeoFPayments();

                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Order";
                this.txtCurOrderDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtLETDES.Text = comnam + unitname + " requests you to arrange supply of following materials from your factory.";
                if (this.Request.QueryString["genno"].Length != 0 && (this.Request.QueryString["InputType"].ToString() == "OrderEdit" || this.Request.QueryString["InputType"].ToString() == "OrderApprove"))
                {
                    this.lbtnPrevOrderList_Click(null, null);
                }
                if (this.Request.QueryString["genno"].Length != 0)
                {

                    this.lbtnOk_Click(null, null);

                }
                
                this.CommonButton();
                this.ColoumVisiable();
                GetJobLocation();
               
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdatePurOrder_Click);
        }

        private void ColoumVisiable()
        {
            string comcod = this.GetCompCode();
            if (comcod == "8305")
            {
                this.gvOrderTerms.Columns[2].Visible = false;
            }

        }
        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

            if (this.Request.QueryString["InputType"] == "OrderApprove")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approval";
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).OnClientClick = "return confirm('Do You want to Approve?')";
            }

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
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose"))
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Calculation";


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private string CompanyPrintWorkOrder()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string PrintWorkOrder = "";
            switch (comcod)
            {
                case "1301":
                case "3301":
                    PrintWorkOrder = "PrintWorkOrder02";
                    break;
                default:
                    PrintWorkOrder = "PrintWorkOrder";
                    break;
            }
            return PrintWorkOrder;
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string printOpt =((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            string comcod = this.GetCompCode();
            string rptLevel = this.ddlReportLevel.SelectedItem.Value;
            string printType = this.chkSummary.Checked ? "summary" : rptLevel;
            //string reqno = this.ddlReqList.SelectedValue.ToString();
            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint?Type=OrderPrint&comcod=" + comcod + "&orderno=" + mORDERNO + "&PrintOpt=" + printOpt + "&PrintType=" + printType + "&rptLvl="+rptLevel+"', target='_blank');</script>";
        }


        private void GET_Incotrms()
        {
            DataRow dr1;

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "17%", "", "", "", "", "");
            dr1 = ds1.Tables[0].NewRow();
            dr1["gcod"] = "00000";
            dr1["gdesc"] = "NONE";
            dr1["comcod"] = comcod;
            ds1.Tables[0].Rows.Add(dr1);
            this.DDLIncoTerms.DataValueField = "gcod";
            this.DDLIncoTerms.DataTextField = "gdesc";
            this.DDLIncoTerms.DataSource = ds1.Tables[0];
            this.DDLIncoTerms.SelectedValue = "00000";
            this.DDLIncoTerms.DataBind();

        }
        private void GET_ShipingMode()
        {
            DataRow dr1;

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "14%", "", "", "", "", "");
            dr1 = ds1.Tables[0].NewRow();
            dr1["gcod"] = "00000";
            dr1["gdesc"] = "NONE";
            dr1["comcod"] = comcod;
            ds1.Tables[0].Rows.Add(dr1);
            this.ddlShipMode.DataValueField = "gcod";
            this.ddlShipMode.DataTextField = "gdesc";
            this.ddlShipMode.DataSource = ds1.Tables[0];
            this.ddlShipMode.SelectedValue = "00000";
            this.ddlShipMode.DataBind();

        }
        private void GET_ModeoFPayments()
        {
            DataRow dr1;

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "16%", "", "", "", "", "");
            dr1 = ds1.Tables[0].NewRow();
            dr1["gcod"] = "00000";
            dr1["gdesc"] = "NONE";
            dr1["comcod"] = comcod;
            ds1.Tables[0].Rows.Add(dr1);
            this.DdlModeOfPayment.DataValueField = "gcod";
            this.DdlModeOfPayment.DataTextField = "gdesc";
            this.DdlModeOfPayment.DataSource = ds1.Tables[0];
            this.DdlModeOfPayment.SelectedValue = "00000";
            this.DdlModeOfPayment.DataBind();

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void lbtnPrevOrderList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string findReq = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + "%" : this.Request.QueryString["genno"].ToString() + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPREVORDERLIST", CurDate1,
                          findReq, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevOrderList.Items.Clear();
            this.ddlPrevOrderList.DataTextField = "orderno1";
            this.ddlPrevOrderList.DataValueField = "orderno";
            this.ddlPrevOrderList.DataSource = ds1.Tables[0];
            this.ddlPrevOrderList.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "New")
            {
                this.MultiView1.ActiveViewIndex = -1;

                //this.lblPrevious.Visible = true;
                //this.txtsearchpre.Visible = true;
                this.lbtnPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Items.Clear();
                this.lblCurOrderNo1.Text = "POR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurOrderDate.Enabled = true;
                this.txtOrderRefNo.Text = "";
                this.txtOrderRefNo.ReadOnly = false;
                this.lssircode.Text = "";
               
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtOrderNarr.Text = "";
                this.gvOrderInfo.DataSource = null;
                this.gvOrderInfo.DataBind();
                this.gvOrderTerms.DataSource = null;
                this.gvOrderTerms.DataBind();
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                this.lbtnOk.Text = "Ok";
                this.ddlSuplierList.Items.Clear();
                return;
            }


            //"Refer to your offer with specification dated on " +
            //this.lblPrevious.Visible = false;
            //this.txtsearchpre.Visible = false;
            this.lbtnPrevOrderList.Visible = false;
            this.ddlPrevOrderList.Visible = false;
            this.txtCurOrderNo2.ReadOnly = true;
            this.lbtnOk.Text = "New";
            if (this.ddlPrevOrderList.Items.Count <= 0)
            {
                this.MultiView1.ActiveViewIndex = 0;
                this.ResourceForOrder();
                return;

            }
            this.MultiView1.ActiveViewIndex = 1;
            this.Get_Pur_Order_Info();
        }


        protected void GetOrderNo()
        {

            string comcod = this.GetCompCode();
            string mOrderdate = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mOrderNo = "NEWORDER";
            if (this.ddlPrevOrderList.Items.Count > 0)
                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();

            if (mOrderNo == "NEWORDER")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTORDERINFO", mOrderdate, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(6, 5);
                    this.ddlPrevOrderList.DataTextField = "maxorderno1";
                    this.ddlPrevOrderList.DataValueField = "maxorderno";
                    this.ddlPrevOrderList.DataSource = ds1.Tables[0];
                    this.ddlPrevOrderList.DataBind();
                }

            }
        }

        private void ResourceForOrder()
        {

            ViewState.Remove("tblResP");

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string findSupplier = (this.Request.QueryString["actcode"].ToString()).Length == 0 ? "%" : this.Request.QueryString["actcode"].ToString() + "%";
            string findReq = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + "%" : this.Request.QueryString["genno"].ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "RESOURCEINFFORORDER", CurDate1,
                         findSupplier, findReq, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSuplierList.DataTextField = "ssirdesc1";
            this.ddlSuplierList.DataValueField = "ssircode";
            this.ddlSuplierList.DataSource = ds1.Tables[1];
            this.ddlSuplierList.DataBind();

            //this.lblSupMail.Text = "Suplier's Email: " + ds1.Tables[1].Rows[0]["email"].ToString();


            double advamt = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("Sum(advamt)", "")) ? 0.00 : ds1.Tables[0].Compute("Sum(advamt)", "")));
            this.txtadvAmt.Text = (ds1.Tables[0].Rows.Count == 0) ? "" : advamt.ToString("#,##0;(#,##0); ");

            ViewState["tblResP"] = ds1.Tables[0];
            this.ddlSuplierList_SelectedIndexChanged(null, null);
        }

        protected void imgSearchOrderno_Click(object sender, EventArgs e)
        {
            this.ResourceForOrder();
        }

        protected void ddlSuplierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblResP"];
            if (dt.Rows.Count == 0)
                return;
            string supcode = this.ddlSuplierList.SelectedValue.ToString();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "ssircode in ('" + supcode + "')";
            this.gvAprovInfo.DataSource = dv1.ToTable();
            this.gvAprovInfo.DataBind();

            this.lblStoreName.Text = dt.Rows[0]["projdesc1"].ToString();

        }
        protected void Get_Pur_Order_Info()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mOrderNo = "NEWORDER";
            if (comcod == "3301" || comcod == "2301" || comcod == "1301")
            {
                this.lblReqNarr.Text = "Special Notes:- ";
            }
            if (this.ddlPrevOrderList.Items.Count > 0)
            {

                this.txtCurOrderDate.Enabled = false;
                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", mOrderNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblOrder"] = this.HiddenSameData(ds1.Tables[0]);
            this.gvOrderTerms.DataSource = ds1.Tables[1];
            this.gvOrderTerms.DataBind();
            ViewState["tblpaysch"] = ds1.Tables[2];
            this.SchData_Bind();
            if (mOrderNo == "NEWORDER")
            {
                string reqno= this.Request.QueryString["genno"].ToString();
                string supcode = this.Request.QueryString["actcode"].ToString();
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTORDERINFO", CurDate1, "", "", "", "", "", "", "", "");
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETORDERREFNO", reqno, supcode, "", "", "", "", "", "", "");

                this.txtOrderRefNo.Text = ds2.Tables[0].Rows[0]["refno"].ToString();

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(6, 5);
                }


                return;
            }

            this.lblCurOrderNo1.Text = ds1.Tables[3].Rows[0]["orderno1"].ToString().Substring(0, 6);
            this.txtCurOrderNo2.Text = ds1.Tables[3].Rows[0]["orderno1"].ToString().Substring(6, 5);
            this.txtOrderRefNo.Text = ds1.Tables[3].Rows[0]["pordref"].ToString();
            this.txtLETDES.Text = ds1.Tables[3].Rows[0]["leterdes"].ToString();
            this.txtSubject.Text = ds1.Tables[3].Rows[0]["subject"].ToString();


            this.txtCurOrderDate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["orderdat"]).ToString("dd.MM.yyyy");
            this.txtPreparedBy.Text = ds1.Tables[3].Rows[0]["pordbydes"].ToString();
            this.lssircode.Text = ds1.Tables[3].Rows[0]["ssircode"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[3].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtOrderNarr.Text = ds1.Tables[3].Rows[0]["pordnar"].ToString();
            this.txtRemarks2.Text = ds1.Tables[3].Rows[0]["remarks"].ToString();
            this.txtadvAmt.Text = Convert.ToDouble(ds1.Tables[3].Rows[0]["advamt"]).ToString("#,##0;(#,##0); ");

            this.ddlJobLocation.SelectedValue = ds1.Tables[3].Rows[0]["deliveryloca"].ToString();
            this.parpayType.SelectedValue = ds1.Tables[3].Rows[0]["parpayment"].ToString();
            this.ddlpardelivery.SelectedValue = ds1.Tables[3].Rows[0]["pardelivery"].ToString();
            this.ddlShipMode.SelectedValue = ds1.Tables[3].Rows[0]["shipmode"].ToString();
            this.DDLIncoTerms.SelectedValue = ds1.Tables[3].Rows[0]["shiptrms"].ToString();
            this.DdlModeOfPayment.SelectedValue = ds1.Tables[3].Rows[0]["paymode"].ToString();
            this.TxtDateDelivery.Text = ds1.Tables[3].Rows[0]["deliverydate"].ToString();
            this.lblSyspocustom.Text = ds1.Tables[3].Rows[0]["custompon"].ToString();
            this.lblSyspo.Text = ds1.Tables[3].Rows[0]["syspon"].ToString();
            
            this.gvOrderInfo_DataBind();


            DataTable dt1 = ds1.Tables[0].DefaultView.ToTable(true, "ssircode", "ssirdesc1");
            this.ddlSuplierList.DataTextField = "ssirdesc1";
            this.ddlSuplierList.DataValueField = "ssircode";
            this.ddlSuplierList.DataSource = dt1;
            this.ddlSuplierList.DataBind();

            this.txtSupName.Text = dt1.Rows[0]["ssirdesc1"].ToString();
            this.lblStoreName.Text = ds1.Tables[0].Rows[0]["projdesc1"].ToString();

        }
        protected void gvOrderInfo_DataBind()
        {
            DataTable tbl1 = this.HiddenSameData((DataTable)ViewState["tblOrder"]);
            this.gvOrderInfo.DataSource = tbl1;
            this.gvOrderInfo.DataBind();

            if (this.ddlPrevOrderList.Items.Count > 0)
            {
                //((LinkButton)this.gvOrderInfo.FooterRow.FindControl("lbtnDelete")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "OrderEdit");
                //((LinkButton)this.gvOrderInfo.FooterRow.FindControl("lbtnUpdatePurOrder")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "OrderEdit");
            }

            if (tbl1.Rows.Count == 0)
                return;


            double amt1 = 0.00, amt2 = 0.00;
            DataTable td1 = tbl1.Copy();
            DataTable td2 = tbl1.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like '049999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '049999902%'");
            td1 = dv1.ToTable();
            amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(ordramt)", "")) ? 0.00 : td2.Compute("Sum(ordramt)", "")));
            amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(ordramt)", "")) ? 0.00 : td1.Compute("Sum(ordramt)", "")));
            ((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text = (amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");

        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "rsircode";
            dt1 = dv.ToTable();

            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                }

            }

            return dt1;
        }


        private DataTable HiddenSameDataPrint(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string reqno = dt1.Rows[0]["reqno"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    reqno = dt1.Rows[j]["reqno"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["reqno"] = "";
                    dt1.Rows[j]["mrfno"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    {
                        dt1.Rows[j]["reqno"] = "";
                        dt1.Rows[j]["mrfno"] = "";

                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        dt1.Rows[j]["rsirdesc"] = "";


                    reqno = dt1.Rows[j]["reqno"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                }

            }

            return dt1;
        }


        protected void Session_tblOrder_Update()
        {

            DataTable tbl1 = this.HiddenSameData((DataTable)ViewState["tblOrder"]);
            int TblRowIndex2;
            for (int j = 0; j < this.gvOrderInfo.Rows.Count; j++)
            {
                double dgvorderQty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderQty")).Text.Trim()));

                double ApprovRate = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvApprovRate")).Text.Trim()));

                double dgvAppAmt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderAmt")).Text.Trim()));
                //double dgvMRRRate = (dgvorderQty > 0) ? dgvAppAmt / dgvorderQty : 00;

                TblRowIndex2 = (this.gvOrderInfo.PageIndex) * this.gvOrderInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["ordrqty"] = dgvorderQty;
                tbl1.Rows[TblRowIndex2]["aprovrate"] = ApprovRate;
                tbl1.Rows[TblRowIndex2]["ordramt"] = (dgvorderQty == 0) ? dgvAppAmt : (dgvorderQty * ApprovRate);
                tbl1.Rows[TblRowIndex2]["selection"] = ((TextBox)this.gvOrderInfo.Rows[j].FindControl("TxtSelection")).Text.Trim();

                tbl1.Rows[TblRowIndex2]["mattype"] = ((DropDownList)this.gvOrderInfo.Rows[j].FindControl("DdlCstFinished")).SelectedValue.Trim();

            }
            ViewState["tblOrder"] = tbl1;

        }



        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo.PageIndex = ((DropDownList)this.gvOrderInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvOrderInfo_DataBind();
        }
        protected void lbtnUpdatePurOrder_Click(object sender, EventArgs e)
        {
          
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            if (this.ddlPrevOrderList.Items.Count == 0)
            {
                this.GetOrderNo();
                this.GetSYSPONO();
            }
              
            this.Session_tblOrder_Update();
            string Posteddate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            string mORDERDAT = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mPORDUSRID = "";
            string mAPPRUSRID = "";
            string mSSIRCODE = this.ddlSuplierList.Items.Count > 0 ? this.ddlSuplierList.SelectedValue.ToString() : this.lssircode.Text.Trim();
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            string mPORDBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mPORDREF = this.txtOrderRefNo.Text.Trim();
            string mLETERDES = this.txtLETDES.Text.Trim();
            string ShipMode = this.ddlShipMode.SelectedValue.Trim();
            string ModeOfPayment = this.DdlModeOfPayment.SelectedValue.Trim();
            string SippingTerms = this.DDLIncoTerms.SelectedValue.Trim();
            
            string mPORDNAR = this.txtOrderNarr.Text.Trim();
            string mPORMRKS = this.txtRemarks2.Text.Trim();
            string subject = this.txtSubject.Text.Trim();
            string AdvAmt = Convert.ToDouble("0" + this.txtadvAmt.Text.Trim()).ToString();
            //log report

            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();


            string deliveryloc = this.ddlJobLocation.SelectedValue.ToString();
            string parpayType = this.parpayType.SelectedValue.ToString();
            string ddlpardelivery = this.ddlpardelivery.SelectedValue.ToString();
            string datedelivery = this.TxtDateDelivery.Text.Trim().ToString();

            //PON for Specially FB
            string syspon = this.lblSyspo.Text.Trim();
            string sysponcustom = this.lblSyspocustom.Text.Trim();
            string status = (this.Request.QueryString["InputType"].ToString() == "OrderApprove" ? "Ok" : "");
            string rptLvl = this.ddlReportLevel.SelectedValue;

            //end log
            bool result = false;
            //Balance Approval
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            if ((this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry"))
            {

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                    string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                    string mSupCODE = tbl1.Rows[i]["ssircode"].ToString();

                    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "EMPTYORDERNO", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSupCODE, "", "", "", "");
                    if (ds1 == null)
                        return;
                    if (ds1.Tables[0].Rows.Count == 0)
                        continue;
                    if (ds1.Tables[0].Rows[0]["orderno"].ToString().Trim() != "")
                    {

                        DataView dv1 = ds1.Tables[0].DefaultView;
                        dv1.RowFilter = ("orderno <>'" + mORDERNO + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Materials  already Orderred another order');", true);


                            return;
                        }
                    }
                }

            }

            result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERB",
                             mORDERNO, mORDERDAT, mSSIRCODE, mPORDUSRID, mAPPRUSRID, mPORDREF, mLETERDES, mPORDNAR, subject, userid,
                             Sessionid, Terminal, AdvAmt, Posteddate, mAPPRDAT, deliveryloc, parpayType,
                             ddlpardelivery, datedelivery, syspon, sysponcustom, ShipMode, ModeOfPayment, SippingTerms, status, rptLvl, mPORMRKS);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() + "');", true);

              
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();

                string apprvdate = Convert.ToDateTime(tbl1.Rows[i]["aprovdat"]).ToString("dd-MMM-yyyy");
                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(apprvdate), Convert.ToDateTime(mORDERDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Date is equal or greater Approved Date');", true);
                    return;
                }



                string mREQNO = tbl1.Rows[i]["reqno"].ToString().Trim();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string bomid = tbl1.Rows[i]["bomid"].ToString();
                double mAprovqty = Convert.ToDouble(tbl1.Rows[i]["aprovqty"]);
                double mORDRQTY = Convert.ToDouble(tbl1.Rows[i]["ordrqty"]);
                string selection = tbl1.Rows[i]["selection"].ToString();
                string mattype = tbl1.Rows[i]["mattype"].ToString();


                // string mORDRQTY = tbl1.Rows[i]["ordrqty"].ToString();
                if (mAprovqty < mORDRQTY)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Order Qty Must be Less Or Equal with Approve Qty');", true);

                    
                    return;
                }

                //purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURPROPOSAL", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mORDERNO, mORDRQTY.ToString(), "", "", "", "", "", "", "", "");

                if (mREQNO != "")
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERA",
                             mORDERNO, mREQNO, mRSIRCODE, mSPCFCOD, mORDRQTY.ToString(), mAPROVNO, bomid, selection, mattype, "", "", "", "", "", "", "", "", "", "");

                else
                {
                    string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
                    string mOrderAmt = Convert.ToDouble(tbl1.Rows[i]["ordramt"]).ToString();

                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERE", mORDERNO, mPactcode, mRSIRCODE, "000000000000", mOrderAmt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }



                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() +"');", true);

                  
                    return;
                }
            }

            for (int j = 0; j < this.gvOrderTerms.Rows.Count; j++)
            {
                string mTERMSID = ((Label)this.gvOrderTerms.Rows[j].FindControl("lblgvTermsID")).Text.Trim();
                string mTERMSSUBJ = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvSubject")).Text.Trim();
                string mTERMSDESC = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvDesc")).Text.Trim();
                string mTERMSRMRK = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvRemarks")).Text.Trim();
                result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERC",
                        mORDERNO, mTERMSID, mTERMSSUBJ, mTERMSDESC, mTERMSRMRK, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                  
                    return;
                }

            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mORDRQTY = tbl1.Rows[i]["ordrqty"].ToString();
                string bomid = tbl1.Rows[i]["bomid"].ToString();
                string aprovrate = tbl1.Rows[i]["aprovrate"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPPROVA", mAPROVNO, mREQNO, mRSIRCODE, SSIRCODE, mORDERNO, mSPCFCOD, aprovrate, bomid, "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

               
                    return;
                }
            }

            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                string inscode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvschcode")).Text.Trim();
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim())).ToString();
                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                string Remarks02 = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks02")).Text.Trim();




                result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERD",
                        mORDERNO, inscode, desc, Date, Amt, Remarks, Remarks02, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                   
                    return;
                }

            }


            this.txtCurOrderDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

          
        }

        protected void lbtnSelectedOrdr_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 1;
            this.Get_Pur_Order_Info();
            this.txtSupName.Text = this.ddlSuplierList.SelectedItem.ToString();



            DataTable dt1 = (DataTable)ViewState["tblOrder"];
            DataTable dtResP = (DataTable)ViewState["tblResP"];
            int i;
            for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
            {
                bool chkitm = ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked;


                if (chkitm == true)
                {
                    string PactCode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPrjCod11")).Text.Trim();
                    string Appno = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPAPNo")).Text.Trim();
                    string Reqno = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvReqNo2")).Text.Trim();
                    string Rsircode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvResCod2")).Text.Trim();
                    string Spcfcod = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvspfcod02")).Text.Trim();
                    string Ssircode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvSupCod")).Text.Trim();
                    string bomid = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvBomNo")).Text.Trim();
                    DataRow[] dr2 = dtResP.Select("pactcode='" + PactCode + "'and aprovno='" + Appno + "'and reqno = '" + Reqno + "' and rsircode = '" + Rsircode +
                                    "' and spcfcod='" + Spcfcod + "' and ssircode = '" + Ssircode + "' and bomid='" + bomid + "'");
                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = "1";
                    }


                }

            }


            string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "1301":
                case "2301":
                case "3301":
                    string Narration = "";
                    string aprovno1 = "00000000000000";
                    for (i = 0; i < dtResP.Rows.Count; i++)
                    {


                        string chkitem = dtResP.Rows[i]["chk"].ToString();
                        if (chkitem == "1")
                        {
                            DataRow dr1 = dt1.NewRow();

                            string aprovno = dtResP.Rows[i]["aprovno"].ToString();
                            dr1["aprovno"] = dtResP.Rows[i]["aprovno"];
                            dr1["reqno"] = dtResP.Rows[i]["reqno"];
                            dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                            dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                            dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                            dr1["aprovno1"] = dtResP.Rows[i]["aprovno1"];
                            dr1["aprovdat"] = dtResP.Rows[i]["aprovdat"];
                            dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                            dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                            dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                            dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                            dr1["rsirdesc1"] = dtResP.Rows[i]["rsirdesc1"];
                            dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                            dr1["spcfdesc"] = dtResP.Rows[i]["spcfdesc"];
                            dr1["rsirunit"] = dtResP.Rows[i]["rsirunit"];
                            dr1["aprovqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["ordrqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["aprovrate"] = dtResP.Rows[i]["aprovrate"];

                            dr1["ordramt"] = Convert.ToDouble(dtResP.Rows[i]["aprovqty"]) * Convert.ToDouble(dtResP.Rows[i]["aprovrate"]);
                            dr1["paytype"] = dtResP.Rows[i]["paytype"];
                            dr1["bomid"] = dtResP.Rows[i]["bomid"];
                            dr1["selection"] = "";
                            dr1["mattype"] ="";
                            dr1["season"] = dtResP.Rows[i]["season"].ToString();
                            dt1.Rows.Add(dr1);
                            if (aprovno1 != aprovno)
                            {
                                Narration = Narration + dtResP.Rows[i]["aprovnar"] + ", ";

                            }
                            aprovno1 = aprovno;

                        }
                    }

                    this.txtOrderNarr.Text = Narration.Substring(0, (Narration.Length) - 2);
                    break;

                default:
                    for (i = 0; i < dtResP.Rows.Count; i++)
                    {

                        string aprovno = dtResP.Rows[i]["aprovno"].ToString();
                        string chkitem = dtResP.Rows[i]["chk"].ToString();
                        if (chkitem == "1")
                        {
                            DataRow dr1 = dt1.NewRow();
                            dr1["aprovno"] = dtResP.Rows[i]["aprovno"];
                            dr1["reqno"] = dtResP.Rows[i]["reqno"];
                            dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                            dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                            dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                            dr1["aprovno1"] = dtResP.Rows[i]["aprovno1"];
                            dr1["aprovdat"] = dtResP.Rows[i]["aprovdat"];
                            dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                            dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                            dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                            dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                            dr1["rsirdesc1"] = dtResP.Rows[i]["rsirdesc1"];
                            dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                            dr1["spcfdesc"] = dtResP.Rows[i]["spcfdesc"];
                            dr1["rsirunit"] = dtResP.Rows[i]["rsirunit"];
                            dr1["aprovqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["ordrqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["aprovrate"] = dtResP.Rows[i]["aprovrate"];
                            dr1["ordramt"] = (Convert.ToDouble(dtResP.Rows[i]["aprovqty"]) == 0.00) ? dtResP.Rows[i]["aprovamt"] : Convert.ToDouble(dtResP.Rows[i]["aprovqty"]) * Convert.ToDouble(dtResP.Rows[i]["aprovrate"]);
                            dr1["paytype"] = dtResP.Rows[i]["paytype"];
                            dr1["bomid"] = dtResP.Rows[i]["bomid"];
                            dr1["selection"] = "";
                            dr1["mattype"] = "";
                            dr1["season"] = dtResP.Rows[i]["season"].ToString();
                            dt1.Rows.Add(dr1);


                        }
                    }
                    break;



            }


            ViewState["tblOrder"] = this.HiddenSameData(dt1);
            this.gvOrderInfo_DataBind();
            this.GET_Supplier_Pending_Requistion();
        }

        protected void GetSYSPONO()
        {
            string comcod = this.GetCompCode();

            string season = "00000";
            DataTable order = (DataTable)ViewState["tblOrder"];
            if (order.Rows.Count > 0)
            {
                season = order.Rows[0]["season"].ToString();
            }

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_SEASON_WISE_LAST_PO", season,
                   "LOCAL", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.lblSyspo.Text = ds2.Tables[0].Rows[0]["maxsyspo"].ToString();
                this.lblSyspocustom.Text = ds2.Tables[0].Rows[0]["syspondesc"].ToString();


            }


        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvAprovInfo.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = true;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = false;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "False";

                }

            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];

            string comcod = this.GetCompCode();
            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            bool result;


            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNO",
                           mORDERNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Successfully');", true);

                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UNLINKAPPROVENO",
                         mAPROVNO, mREQNO, mRSIRCODE, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() + "');", true);

                    
                    return;
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);

          

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Order Info";
                string eventdesc = "Update Order";
                string eventdesc2 = mORDERNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {




            this.pnlschgenerate.Visible = false;
            DataTable dt = (DataTable)ViewState["tblpaysch"];
            int toins = Convert.ToInt32("0" + this.txtTInstall.Text.Trim());
            int incode = 0;
            for (int i = 0; i < toins; i++)
            {
                incode = incode + 1;
                string inscode = incode.ToString().PadLeft(3, '0');
                incode = Convert.ToInt32(inscode);
                DataRow dr = dt.NewRow();

                dr["inscode"] = inscode;
                dr["insdesc"] = "";
                dr["insdate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr["insamt"] = 0.00;
                dr["rmrks"] = "";
                dr["rmrks02"] = "";
                dt.Rows.Add(dr);


            }
            ViewState["tblpaysch"] = dt;

            this.chkVisible.Checked = false;
            this.SchData_Bind();


        }


        private void SchData_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblpaysch"];

            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();

            if (dt.Rows.Count > 0)
                ((Label)this.gvPayment.FooterRow.FindControl("lblgvfschAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(insamt)", "")) ? 0.00 : dt.Compute("sum(insamt)", ""))).ToString("#,##0;(#,##0); ");







        }
        private void SavePaymentSchdule()
        {

            DataTable dt = (DataTable)ViewState["tblpaysch"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                string Remarks02 = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks02")).Text.Trim();
                dt.Rows[i]["insdesc"] = desc;
                dt.Rows[i]["insdate"] = Date;
                dt.Rows[i]["insamt"] = Amt;
                dt.Rows[i]["rmrks"] = Remarks;
                dt.Rows[i]["rmrks02"] = Remarks02;
            }

            ViewState["tblpaysch"] = dt;

        }

        protected void lUpdatpayment_Click(object sender, EventArgs e)
        {
            this.SavePaymentSchdule();
            DataTable dt = (DataTable)ViewState["tblpaysch"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                dt.Rows[i]["insdesc"] = desc;
                dt.Rows[i]["insdate"] = Date;
                dt.Rows[i]["insamt"] = Amt;
                dt.Rows[i]["rmrks"] = Remarks;
            }

            ViewState["tblpaysch"] = dt;
        }
        protected void lTotalPayment_Click(object sender, EventArgs e)
        {
            this.SavePaymentSchdule();
            this.SchData_Bind();
        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlschgenerate.Visible = this.chkVisible.Checked;
        }
        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void chkCharging_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCharging.Visible = (chkCharging.Checked);
            imgSearchProject_Click(null, null);
            imgSearchCharge_Click(null, null);
        }
        protected void imgSearchCharge_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABOURCHARGE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlCharge.DataTextField = "sirdesc";
            this.ddlCharge.DataValueField = "sircode";
            this.ddlCharge.DataSource = ds1.Tables[0];
            this.ddlCharge.DataBind();
            ds1.Dispose();
        }
        private void TblProject()
        {
            if (Session["tblproject"] == null)
            {
                DataTable tblproject = new DataTable();
                tblproject.Columns.Add("pactcode", Type.GetType("System.String"));
                tblproject.Columns.Add("pactdesc", Type.GetType("System.String"));
                Session["tblproject"] = tblproject;
            }
        }
        protected void imgSearchProject_Click(object sender, EventArgs e)
        {
            this.TblProject();
            DataTable dt = (DataTable)(DataTable)ViewState["tblOrder"];
            DataTable dt1 = (DataTable)Session["tblproject"];
            if (dt.Rows.Count == 0)
            {
                this.ddlProjectName.Items.Clear();
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Pactcode = dt.Rows[i]["pactcode"].ToString();
                DataRow[] dr1 = dt1.Select("pactcode='" + Pactcode + "'");
                if (dr1.Length == 0)
                {
                    DataRow dr2 = dt1.NewRow();
                    dr2["pactcode"] = Pactcode;
                    dr2["pactdesc"] = dt.Rows[i]["projdesc1"].ToString();
                    dt1.Rows.Add(dr2);

                }

            }

            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt1;
            this.ddlProjectName.DataBind();
        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            string mProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string mResCode = this.ddlCharge.SelectedValue.ToString();
            string mSpcfCode = "000000000000";
            DataRow[] dr2 = tbl1.Select("pactcode ='" + mProjectCode + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();


                dr1["aprovno"] = "";
                dr1["reqno"] = "";
                dr1["rsircode"] = mResCode;
                dr1["ssircode"] = "";
                dr1["spcfcod"] = mSpcfCode;
                dr1["aprovno1"] = "";
                dr1["aprovdat"] = "01-Jan-1900";
                dr1["reqno1"] = "";
                dr1["mrfno"] = "";
                dr1["pactcode"] = mProjectCode;
                dr1["projdesc1"] = this.ddlProjectName.SelectedItem.ToString();
                dr1["rsirdesc1"] = this.ddlCharge.SelectedItem.ToString(); ;
                dr1["ssirdesc1"] = "";
                dr1["spcfdesc"] = "";
                dr1["rsirunit"] = "";
                dr1["aprovqty"] = 0.00;
                dr1["ordrqty"] = 0.00;
                dr1["aprovrate"] = 0.00;
                dr1["ordramt"] = 0.00;
                dr1["paytype"] = "";
                dr1["selection"] = "";
                dr1["mattype"] = "";
                tbl1.Rows.Add(dr1);
            }






            ViewState["tblOrder"] = this.HiddenSameData(tbl1);
            this.gvOrderInfo_DataBind();
            // this.gvBillInfo_DataBind();
        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo_DataBind();

        }

        private void AutoSavePDF()
        {
            try
            {
                string orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

                //try
                //{
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                //string Calltype = this.PrintCallType();
                DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SHOWORKORDER01", orderno, "", "", "", "", "", "", "", "");

                DataTable dt = _ReportDataSet.Tables[0].Copy();
                //nahid
                if (_ReportDataSet.Tables[0].Rows.Count == 0)
                    return;
                //OrderGenerateBy
                string PurReqNo = _ReportDataSet.Tables[0].Rows[0]["reqno"].ToString();
                string lettdes = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
                string purord = _ReportDataSet.Tables[4].Rows[0]["orderno"].ToString(); // order no
                string ReqDate = Convert.ToDateTime(_ReportDataSet.Tables[4].Rows[0]["reqdat"]).ToString("dd-MMM-yyyy"); // order no
                string Purorderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy"); //PO DATE
                string eflcperson = _ReportDataSet.Tables[4].Rows[0]["usrname"].ToString();
                string eflcpcell = _ReportDataSet.Tables[4].Rows[0]["mob"].ToString() + ", " + _ReportDataSet.Tables[4].Rows[0]["phone"].ToString();
                string eflcpemail = _ReportDataSet.Tables[4].Rows[0]["email"].ToString();
                //supplier information
                string supName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString() + ", " + _ReportDataSet.Tables[1].Rows[0]["mobile"].ToString();
                string supEmail = _ReportDataSet.Tables[1].Rows[0]["email"].ToString();
                string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();
                string vatregno = _ReportDataSet.Tables[1].Rows[0]["vatregno"].ToString();
                string tinno = _ReportDataSet.Tables[1].Rows[0]["tinno"].ToString();

                string invadd = _ReportDataSet.Tables[4].Rows[0]["invadd"].ToString();
                string deladd = _ReportDataSet.Tables[4].Rows[0]["delvadd"].ToString();
                string padelivery = _ReportDataSet.Tables[4].Rows[0]["padelivery"].ToString();
                //string suploc = _ReportDataSet.Tables[4].Rows[0]["suploc"].ToString();
                //string cnperson = _ReportDataSet.Tables[4].Rows[0]["cnperson"].ToString(); 
                //string email = _ReportDataSet.Tables[4].Rows[0]["email"].ToString();
                //string mobile = _ReportDataSet.Tables[4].Rows[0]["mobile"].ToString();
                string deliverydt = _ReportDataSet.Tables[4].Rows[0]["deliverydt"].ToString();
                string papayment = _ReportDataSet.Tables[4].Rows[0]["paymentdesc"].ToString();
                string prodref = _ReportDataSet.Tables[4].Rows[0]["pordref"].ToString();
                string pordnar = _ReportDataSet.Tables[4].Rows[0]["pordnar"].ToString();
                string custompon = _ReportDataSet.Tables[4].Rows[0]["custompon"].ToString();
                string paymenttrm = _ReportDataSet.Tables[4].Rows[0]["paymentdesc"].ToString();
                string shipmodec = _ReportDataSet.Tables[4].Rows[0]["shipmodedesc"].ToString();
                string shiptrms = _ReportDataSet.Tables[4].Rows[0]["shiptrmsdesc"].ToString();

                //endnahid deliverydt

                string subject = _ReportDataSet.Tables[4].Rows[0]["subject"].ToString();

                string storename = _ReportDataSet.Tables[0].Rows[0]["pactdesc"].ToString();
                string supplydetails = _ReportDataSet.Tables[0].Rows[0]["msrnar3"].ToString() + _ReportDataSet.Tables[4].Rows[0]["pordnar"].ToString();
                string refno = _ReportDataSet.Tables[4].Rows[0]["pordref"].ToString();

                string preparedby = _ReportDataSet.Tables[4].Rows[0]["csprename"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["cspredat"].ToString();
                string checkedby = _ReportDataSet.Tables[4].Rows[0]["cschkname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["cschkdat"].ToString();
                string varifiedby = _ReportDataSet.Tables[4].Rows[0]["varyname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["verydat"].ToString();
                string approvedby = _ReportDataSet.Tables[4].Rows[0]["appname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["aprvdat"].ToString();
                string reqcreatby = _ReportDataSet.Tables[4].Rows[0]["postname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["posteddat"].ToString();
                string justification = _ReportDataSet.Tables[0].Rows[0]["msrnar3"].ToString();
                double advamt = Convert.ToDouble((Convert.IsDBNull(_ReportDataSet.Tables[4].Compute("Sum(advamt)", "")) ? 0.00 : _ReportDataSet.Tables[4].Compute("Sum(advamt)", "")));

                LocalReport rpt1 = new LocalReport();

                var list1 = _ReportDataSet.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.EClassPurchaseOrdr>();
                var list2 = _ReportDataSet.Tables[2].DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseOrderTerms>();

                var AList = list1.FindAll(p => p.grp == "A");
                double totalamt = (AList.Sum(item => item.ordramt) == 0) ? 0.00 : AList.Sum(item => item.ordramt);
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                switch (comcod)
                {
                    case "5305":
                    case "5306":
                        invadd = "ULOSHARA, KALIAKOIR, GAZIPUR-1750, BANGLADESH";
                        string invphn = "+880255069911";
                        rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPOLocalFBNew", list1, list2.FindAll(p => p.termsdesc.Length > 0).ToList(), null);
                        rpt1.EnableExternalImages = true;
                        rpt1.SetParameters(new ReportParameter("comnam", comnam));
                        rpt1.SetParameters(new ReportParameter("comcod", comcod.Substring(0, 2)));
                        rpt1.SetParameters(new ReportParameter("comadd", comadd));
                        //rpt1.SetParameters(new ReportParameter("ReqDate", ReqDate));
                        rpt1.SetParameters(new ReportParameter("Purorderdate", Purorderdate));
                        rpt1.SetParameters(new ReportParameter("supName", supName));
                        rpt1.SetParameters(new ReportParameter("suploc", "Address: " + address));
                        //rpt1.SetParameters(new ReportParameter("vatregno", vatregno));
                        //rpt1.SetParameters(new ReportParameter("tinno", tinno));
                        rpt1.SetParameters(new ReportParameter("mobile", " Mobile: " + phone));
                        rpt1.SetParameters(new ReportParameter("purord", custompon));
                        rpt1.SetParameters(new ReportParameter("poref", prodref));
                        rpt1.SetParameters(new ReportParameter("spnote", pordnar));
                        //rpt1.SetParameters(new ReportParameter("PurReqNo", PurReqNo));
                        //rpt1.SetParameters(new ReportParameter("eflcperson", eflcperson));
                        //rpt1.SetParameters(new ReportParameter("eflcpcell", eflcpcell));
                        //rpt1.SetParameters(new ReportParameter("eflcpemail", eflcpemail));
                        rpt1.SetParameters(new ReportParameter("cnperson", "Contact Person: " + Cperson));
                        rpt1.SetParameters(new ReportParameter("email", "Email: " + supEmail));
                        rpt1.SetParameters(new ReportParameter("invadd", invadd));
                        //rpt1.SetParameters(new ReportParameter("deladd", deladd));
                        //rpt1.SetParameters(new ReportParameter("padelivery", padelivery));
                        rpt1.SetParameters(new ReportParameter("expdatedel", deliverydt));
                        //rpt1.SetParameters(new ReportParameter("papayment", papayment));
                        rpt1.SetParameters(new ReportParameter("RptTitle", "PURCHASE ORDER"));
                        rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(totalamt, 2)));
                        rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                        //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

                        rpt1.SetParameters(new ReportParameter("invphn", invphn));
                        rpt1.SetParameters(new ReportParameter("paymodedesc", paymenttrm));
                        rpt1.SetParameters(new ReportParameter("shipMode", shipmodec));
                        rpt1.SetParameters(new ReportParameter("incoterms", shiptrms));


                        break;
                    default:
                        rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPOLocal", list1, list2.FindAll(p => p.termsdesc.Length > 0).ToList(), null);
                        rpt1.EnableExternalImages = true;
                        rpt1.SetParameters(new ReportParameter("comnam", comnam));
                        rpt1.SetParameters(new ReportParameter("comcod", comcod.Substring(0, 2)));
                        rpt1.SetParameters(new ReportParameter("comadd", comadd));
                        rpt1.SetParameters(new ReportParameter("ReqDate", ReqDate));
                        rpt1.SetParameters(new ReportParameter("Purorderdate", Purorderdate));
                        rpt1.SetParameters(new ReportParameter("supName", supName));
                        rpt1.SetParameters(new ReportParameter("address", "Address: " + address));
                        rpt1.SetParameters(new ReportParameter("vatregno", vatregno));
                        rpt1.SetParameters(new ReportParameter("tinno", tinno));
                        rpt1.SetParameters(new ReportParameter("phone", " Mobile: " + phone));
                        rpt1.SetParameters(new ReportParameter("purord", purord));
                        rpt1.SetParameters(new ReportParameter("PurReqNo", PurReqNo));
                        rpt1.SetParameters(new ReportParameter("eflcperson", eflcperson));
                        rpt1.SetParameters(new ReportParameter("eflcpcell", eflcpcell));
                        rpt1.SetParameters(new ReportParameter("eflcpemail", eflcpemail));
                        rpt1.SetParameters(new ReportParameter("Cperson", "Contact Person: " + Cperson));
                        rpt1.SetParameters(new ReportParameter("supEmail", "Email: " + supEmail));
                        rpt1.SetParameters(new ReportParameter("invadd", invadd));
                        rpt1.SetParameters(new ReportParameter("deladd", deladd));
                        rpt1.SetParameters(new ReportParameter("padelivery", padelivery));
                        rpt1.SetParameters(new ReportParameter("deliverydt", deliverydt));
                        rpt1.SetParameters(new ReportParameter("papayment", papayment));
                        rpt1.SetParameters(new ReportParameter("RptTitle", "PURCHASE ORDER"));
                        rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(totalamt, 2)));
                        rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                        rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

                        break;
                }

                Session["Report1"] = rpt1;

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                //if (File.Exists(Server.MapPath("~/EmailDoc/")))
                //{
                //    File.Delete(Server.MapPath("~/EmailDoc/"));
                //}

                Array.ForEach(Directory.GetFiles(Server.MapPath("~/EmailDoc/")), File.Delete);

                byte[] Bytes = rpt1.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                //string apppath = Server.MapPath("~") + "\\EmailDoc" + "\\" + orderno + ".pdf";
                using (FileStream fileStream = new FileStream(Server.MapPath("~/EmailDoc/" + orderno + ".pdf"), FileMode.Create))
                {
                    fileStream.Write(Bytes, 0, Bytes.Length);
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error occured while sending your message." + ex.Message + "');", true);
            }
        }


        protected void btnSendmail_Click(object sender, EventArgs e)
        {
            this.AutoSavePDF();

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString().Trim();
            string trmid = hst["compname"].ToString().Trim();
            string usrSession = hst["session"].ToString().Trim();


            string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SMSEMAILSETUP", usrid, "", "", "", "", "", "", "", "");
            //DataSet ds = this.purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SMSEMAILSETUP", usrid, ConId, "", "", "", "", "", "", "");


            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, usrid, "", "", "", "", "", "", "");

            string subject = "Work Order";

            //this.txtDescription.Text = ds1.Tables[0].Rows[2]["letdesc"].ToString().Trim();


            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            SmtpClient client = new SmtpClient(hostname, portnumber);
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = false;
            string frmemail = ((this.txtSenderMail.Text).Length == 0) ? dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString() : this.txtSenderMail.Text;  //
            string psssword = ((this.txtPass.Text).Length == 0) ? dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString() : this.txtPass.Text;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            ///////////////////////

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(frmemail);

            msg.To.Add(new MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));

            msg.Subject = subject;
            msg.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment;

            string apppath = Server.MapPath("~") + "\\EmailDoc" + "\\" + mORDERNO + ".pdf";

            attachment = new System.Net.Mail.Attachment(apppath);
            msg.Attachments.Add(attachment);

            //////////////SMS----------------------
            //string Phone = ds1.Tables[0].Rows[0]["phone"].ToString().Trim();
            //string usrname = ds1.Tables[0].Rows[0]["usrname"].ToString().Trim();
            //string Note = "Dear Supplier, PO No: " + mORDERNO + " Send for Delivery.";

            //SmsApps.SendSms(comcod, Note, comnam + " , " + usrname, usrid + ", " + trmid + ", " + usrSession, Phone);

            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear sir," + "<br/>" + "please find the attached file." + "</pre></body></html>");
            try
            {
                client.Send(msg);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Your Mail has been successfully sent');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error occured while sending your message." + ex.Message+"');", true);
            }

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void gvOrderInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo.PageIndex = e.NewPageIndex;
            this.gvOrderInfo_DataBind();
        }
        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string jobLocCode = "87";

            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            this.ddlJobLocation.DataTextField = "hrgdesc";
            this.ddlJobLocation.DataValueField = "hrgcod";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();

        }

        protected void lblgvSpfDesc10_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            this.ModalHead.Text = "Material Specification Change";
            string sircode = ((Label)this.gvOrderInfo.Rows[rowindex].FindControl("lblgvResCod")).Text.Trim();
            string spcfcod = ((Label)this.gvOrderInfo.Rows[rowindex].FindControl("LblgvSpcfcod")).Text.Trim();
            this.lblHelper.Text = sircode + spcfcod;
            DataSet result = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_WISE_SPECIFICATION", sircode);
            DataSet colorinfo = this.purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_COLOR_CODE", "", "", "", "", "", "", "");
            ViewState["tblcolor"] = colorinfo.Tables[0];
            if (result.Tables[0].Rows.Count == 0 || result == null)
            {
                return;
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = result.Tables[0];
            this.ddlSpecification.DataBind();
            this.TxtThikness.Text = "";
            this.txtlength.Text = "";
            this.txtbrand.Text = "";
            this.txtRemarks.Text = "";
            this.ddlModalColor.DataTextField = "gdesc";
            this.ddlModalColor.DataValueField = "gcod";
            this.ddlModalColor.DataSource = colorinfo;
            this.ddlModalColor.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }
        protected void LbtnChngSpcf_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblcolor"];
            DataView dv = dt.DefaultView;
            DataTable dt2 = (DataTable)ViewState["tblOrder"];
            string material = this.lblHelper.Text.ToString().Substring(0, 12);
            string spcfcode = this.lblHelper.Text.ToString().Substring(12, 12);
            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            DataView dv2 = dt2.DefaultView;
            dv2.RowFilter = "rsircode='" + material + "' and spcfcod='" + spcfcode + "'";
            string reqno = dv2.ToTable().Rows[0]["reqno"].ToString();
            string msrno = dv2.ToTable().Rows[0]["aprovno"].ToString();
            //string lccode = this.ddlmlccode.SelectedValue.ToString();
            //string style = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            //string color = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            string supplier = this.ddlSuplierList.SelectedValue.ToString();
            string tospcfcod = this.ddlSpecification.SelectedValue.ToString();
            string dayid = this.Request.QueryString["genno"].ToString();
            if (this.txtlength.Text.ToString() != "" || this.TxtThikness.Text.ToString() != "")
            {
                string thikness = this.TxtThikness.Text.ToString();
                string len = this.txtlength.Text.ToString();
                string brand = this.txtbrand.Text.ToString();
                string remarks = this.txtRemarks.Text.ToString();
                string newcolor = this.ddlModalColor.SelectedValue.ToString();
                string newcolorname = this.ddlModalColor.SelectedItem.ToString();
                dv.RowFilter = "gcod = '" + newcolor + "'";
                string colcode = dv.ToTable().Rows[0]["colcode"].ToString();

                DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_NEW_SPECIFICATIONS", material, thikness, len, newcolorname, brand, remarks, colcode);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Error!!!";
                    return;
                }

                tospcfcod = ds.Tables[0].Rows[0]["spcfcod"].ToString();
            }
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "CHANGE_SPECIFICATIONS_OF_PURCHASEORDER", mORDERNO, reqno, msrno, supplier, material, spcfcode, tospcfcod);
            if (result == true)
            {
                this.Get_Pur_Order_Info();
                ((Label)this.Master.FindControl("lblmsg")).Text = "Specification Change Successfully";
            }
        }

        protected void gvOrderInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string mattype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mattype")).ToString().Trim();
                DropDownList mattypeddl = (DropDownList)e.Row.FindControl("DdlCstFinished");
                mattypeddl.SelectedValue = mattype;

            }
        }


        private void GET_Supplier_Pending_Requistion()
        {
            string comcod = this.GetCompCode();
            string supplier = this.ddlSuplierList.SelectedValue.ToString().Substring(0, 12);
            string reqno = this.Request.QueryString["genno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_LC_INTERFACE", "SUPPLIER_WISE_PENDING_REQUISITION", supplier, reqno, "", "", "", "", "");
            ViewState["tblreqlist"] = ds1.Tables[0];

            this.ddlSlctReq.DataValueField = "reqno";
            this.ddlSlctReq.DataTextField = "reqno1";
            this.ddlSlctReq.DataSource = ds1.Tables[0].DefaultView.ToTable(true, "reqno", "reqno1");
            this.ddlSlctReq.DataBind();

        }

        protected void btnMerge_Click(object sender, EventArgs e)
        {
            DataTable tblreq = (DataTable)ViewState["tblreqlist"];
            DataTable tbl2 = (DataTable)ViewState["tblOrder"];
            string reqno = this.ddlSlctReq.SelectedValue.ToString();
            string season = "00000";
            if (tbl2.Rows.Count > 0)
            {
                season = tbl2.Rows[0]["season"].ToString();
            }
            DataView dv = tblreq.DefaultView;
            dv.RowFilter = "reqno='" + reqno + "' and season='" + season + "'";
            if (dv.ToTable().Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry! You Select Multi Season Requisiton');", true);
                return;
            }
            DataTable tbl1 = dv.ToTable();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                DataRow[] dr = tbl2.Select("rsircode='" + tbl1.Rows[i]["rsircode"] + "' and  spcfcod='" + tbl1.Rows[i]["spcfcod"] + "' and bomid='" + tbl1.Rows[i]["bomid"] + "' and reqno='" + tbl1.Rows[i]["reqno"] + "'");

                if (dr.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + tbl1.Rows[i]["rsirdesc"].ToString() + " Already Added');", true);
                    continue;
                }
                else
                {
                    DataRow dr1 = tbl2.NewRow();

                    dr1["aprovno"] = tbl1.Rows[i]["msrno"];
                    dr1["reqno"] = tbl1.Rows[i]["reqno"];
                    dr1["rsircode"] = tbl1.Rows[i]["rsircode"];
                    dr1["ssircode"] = tbl1.Rows[i]["ssircode"];
                    dr1["spcfcod"] = tbl1.Rows[i]["spcfcod"];
                    dr1["aprovno1"] = "";
                    dr1["aprovdat"] = "01-01-1900";
                    dr1["reqno1"] = tbl1.Rows[i]["reqno1"];
                    dr1["mrfno"] = "";
                    dr1["pactcode"] = "";
                    dr1["projdesc1"] = "";
                    dr1["rsirdesc1"] = tbl1.Rows[i]["rsirdesc"];
                    dr1["ssirdesc1"] = "";
                    dr1["spcfdesc"] = tbl1.Rows[i]["spcfdesc"];
                    dr1["rsirunit"] = tbl1.Rows[i]["rsirunit"];
                    dr1["aprovqty"] = tbl1.Rows[i]["reqqty"];
                    dr1["ordrqty"] = tbl1.Rows[i]["reqqty"];
                    dr1["aprovrate"] = tbl1.Rows[i]["resrate"];
                    dr1["ordramt"] = tbl1.Rows[i]["reqamt"];
                    dr1["paytype"] = "";
                    dr1["bomid"] = tbl1.Rows[i]["bomid"];
                    dr1["selection"] = "";
                    dr1["mattype"] = "";
                    dr1["season"] = tbl1.Rows[i]["season"].ToString();

                    tbl2.Rows.Add(dr1);
                }
            }

            ViewState["tblOrder"] = tbl2;
            this.gvOrderInfo_DataBind();
        }


    }
}