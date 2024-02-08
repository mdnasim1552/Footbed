using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using AjaxControlToolkit;

namespace SPEWEB.F_34_Mgt
{
    public partial class RptAdminInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Management Working Smartface";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtFDate.Text = System.DateTime.Today.ToString("01-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();
                this.PnlInt.Visible = true;

                this.ManagementInterFace();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();




            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            //  ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        //private void ColoumVisiable()
        //    {
        //        this.gvSmpleinqlist.Columns[4].Visible = true;
        //        //this.grvProReq.Columns[4].Visible = true;
        //        ////this.grvProIssue.Columns[4].Visible = true;
        //        //this.grvProdtion.Columns[4].Visible = true;
        //        //this.grvQCEntry.Columns[4].Visible = true;
        //        ////this.gvstorec.Columns[4].Visible = true;
        //        //this.grvComp.Columns[4].Visible = true;
        //        //this.gvProdInfo.Columns[4].Visible = true;
        //    }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int day = ASTUtility.Datediffday(Convert.ToDateTime(this.txtFDate.Text), Convert.ToDateTime(this.txtdate.Text));
            if (day != 0)
                return;
            txtdate_TextChanged(null, null);


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.ManagementInterFace();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ManagementInterFace();
        }
        private void ManagementInterFace()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_MANAGEMENT_INTERFACE", "SHOW_MGT_INTERFACE", Date1, Date2, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblacptrecj"] = ds1.Tables[0];
            ViewState["tbllBomData"] = ds1.Tables[1];
            ViewState["tbllPurchase"] = ds1.Tables[2];
            ViewState["tbllGbillAproval"] = ds1.Tables[3];
            ViewState["tbllPayReg"] = ds1.Tables[4];

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[5].Rows[0]["ordrs"] + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Accept Or Reject</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[5].Rows[0]["ordrsapp"] + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Order Approval</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[5].Rows[0]["bomapp"] + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> BOM Approval </div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[5].Rows[0]["reqapp"] + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> Requisition Aproval</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[5].Rows[0]["csapp"] + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'> CS Approval</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[5].Rows[0]["gbillapp"] + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> G.Bill Aproval</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[5].Rows[0]["paybillapp"] + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> Payment Approval</div></div></div>";

            //this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + 00 + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> Test</div></div></div>";
            //this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + 00 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> TEst</div></div></div>";

            RadioButtonList1_SelectedIndexChanged(null, null);


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblacptrecj"];
            DataTable bomdata = (DataTable)ViewState["tbllBomData"];
            DataTable purdata = (DataTable)ViewState["tbllPurchase"];
            DataTable tblgbill = (DataTable)ViewState["tbllGbillAproval"];
            DataTable tblPaybil = (DataTable)ViewState["tbllPayReg"];

            DataTable Tempdt = new DataTable();
            DataView Tempdv = new DataView();
            switch (value)
            {
                case "0": ///Accept Reject
                    this.PanlOrdAcRej.Visible = true;
                    this.PanOrdDetApp.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCsAproval.Visible = false;
                    this.PanGbillAproval.Visible = false;
                    this.PanlPayBillAprv.Visible = false;
                    //this.pnlBOMGen.Visible = false;               
                    //this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("mlccod='000000000000' and approved <>'' ");
                    this.Data_Bind("gvOrdAcRej", Tempdv.ToTable());
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1": ///Consumtion
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDetApp.Visible = true;
                    this.PnlBomApp.Visible = false;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCsAproval.Visible = false;
                    this.PanGbillAproval.Visible = false;
                    this.PanlPayBillAprv.Visible = false;
                    //this.pnlBOMGen.Visible = false;             
                    //this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("mlccod <>'000000000000' and ordusrid<>'' and approved <>'' AND ordapprove <>'Ok'");
                    this.Data_Bind("gvOrdDetailsApp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2": ///Bom Approval
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.PnlBomApp.Visible = true;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCsAproval.Visible = false;
                    this.PanGbillAproval.Visible = false;
                    this.PanlPayBillAprv.Visible = false;
                    //this.pnlBOMGen.Visible = false;                
                    //this.PnlProComp.Visible = false;             
                    this.Data_Bind("gvBOMApp", bomdata);
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3": ///Requsition Approval
                    this.PnlReqApproval.Visible = true;
                    this.PnlCsAproval.Visible = false;
                    this.PanGbillAproval.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanlPayBillAprv.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    //this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    //this.PnlProComp.Visible = false;
                    Tempdt = purdata.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("approved<>'OK' ");
                    //Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved=''");
                    this.Data_Bind("gvReqApproval", Tempdv.ToTable());
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4": ///Cs APproval
                    this.PnlReqApproval.Visible = false;
                    this.PnlCsAproval.Visible = true;
                    this.PanGbillAproval.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanlPayBillAprv.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    //this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    //this.PnlProComp.Visible = false;
                    Tempdt = purdata.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("APPROVED='Ok' and csfwrd='Ok' and csaprv<>'Ok'");
                    //Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved=''");
                    this.Data_Bind("gvRatePro", Tempdv.ToTable());
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
                case "5": ///General Bill Approved
                    this.PnlReqApproval.Visible = false;
                    this.PnlCsAproval.Visible = false;
                    this.PanGbillAproval.Visible = true;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanlPayBillAprv.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    //this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    //this.PnlProComp.Visible = false;
                    //Tempdt = dt.Copy();
                    //Tempdv = Tempdt.DefaultView;
                    //Tempdv.RowFilter = ("ordusrid <>'' and ordapp='' ");
                    this.Data_Bind("gvPenApproval", tblgbill);
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;

                case "6": /// Payment Bill Aproval
                    this.PnlReqApproval.Visible = false;
                    this.PnlCsAproval.Visible = false;
                    this.PanGbillAproval.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanlPayBillAprv.Visible = true;
                    this.PanOrdDetApp.Visible = false;
                    //this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    //this.PnlProComp.Visible = false;
                    //Tempdt = bomdata.Copy();
                    //Tempdv = Tempdt.DefaultView;
                    //Tempdv.RowFilter = ("ordapp ='Ok' and  (bomusrid='' Or frwdbyid='') ");
                    this.Data_Bind("grvApproved", tblPaybil);
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;
                    //case "7": ///BOM approval
                    //    this.PnlReqApproval.Visible = false;
                    //    this.PnlCsAproval.Visible = false;
                    //    this.PanGbillAproval.Visible = false;
                    //    this.PanlOrdAcRej.Visible = false;
                    //    this.PanlPayBillAprv.Visible = false;
                    //    this.PanOrdDetApp.Visible = false;
                    //    this.pnlBOMGen.Visible = false;
                    //    this.PnlBomApp.Visible = true;
                    //    this.PnlProComp.Visible = false;
                    //    Tempdt = bomdata.Copy();
                    //    Tempdv = Tempdt.DefaultView;
                    //    Tempdv.RowFilter = ("bomusrid<>'' and frwdbyid<>'' and  bomapp=''");
                    //    this.Data_Bind("gvBOMApp", Tempdv.ToTable());
                    //    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    //    break;
                    //case "8": ///Total
                    //    this.PnlReqApproval.Visible = false;
                    //    this.PnlCsAproval.Visible = false;
                    //    this.PanGbillAproval.Visible = false;
                    //    this.PanlOrdAcRej.Visible = false;
                    //    this.PanlPayBillAprv.Visible = false;
                    //    this.PanOrdDetApp.Visible = false;
                    //    //this.pnlBOMGen.Visible = false;
                    //    this.PnlBomApp.Visible = false;
                    //    this.PnlProComp.Visible = true;
                    //    Tempdt = bomdata.Copy();
                    //    Tempdv = Tempdt.DefaultView;
                    //    Tempdv.RowFilter = ("bomapp<>''");
                    //    this.Data_Bind("gvProCom", Tempdv.ToTable());
                    //    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    //    break;


            }
        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvReqApproval":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvReqApproval.DataSource = dt;
                    this.gvReqApproval.DataBind();
                    break;
                case "gvRatePro":
                    this.gvRatePro.DataSource = (dt);
                    this.gvRatePro.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvPenApproval":
                    this.gvPenApproval.DataSource = (dt);
                    this.gvPenApproval.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;  //
                case "gvOrdAcRej":
                    this.gvOrdAcRej.DataSource = (dt);
                    this.gvOrdAcRej.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "grvApproved":
                    this.grvApproved.DataSource = (dt);
                    this.grvApproved.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvOrdDetailsApp":
                    this.gvOrdDetailsApp.DataSource = (dt);
                    this.gvOrdDetailsApp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                //case "gvBOMGen":
                //    this.gvBOMGen.DataSource = (dt);
                //    this.gvBOMGen.DataBind();
                //    if (dt.Rows.Count == 0)
                //        return;
                //    break;
                case "gvBOMApp":
                    this.gvBOMApp.DataSource = (dt);
                    this.gvBOMApp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                    //case "gvProCom":
                    //    this.gvProCom.DataSource = (dt);
                    //    this.gvProCom.DataBind();
                    //    if (dt.Rows.Count == 0)
                    //        return;
                    //    break;
            }
        }
        protected void gvReqApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("gvHyInprPrint");

                HyperLink hlnkgvapmrfno = (HyperLink)e.Row.FindControl("hlnkgvapmrfno");
                HyperLink HypApproval = (HyperLink)e.Row.FindControl("HypApproval");



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();



                string reqtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();
                string prjCode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();

                //TableCell cell = e.Row.Cells[9];
                //string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                //if (cstatus == "Bill Confirm")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4BCF9E");
                //}
                //if (cstatus == "Purchase Invoice")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00CBF3");
                //}
                //if (cstatus == "Rate Proposal")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5EB75B");
                //}
                //if (cstatus == "Order Process")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D95350");
                //}
                //if (cstatus == "Rate Approval")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EFAD4D");
                //}
                //if (cstatus == "Materials Receved")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                //}
                //if (cstatus == "QC Check")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                //}
                //if (cstatus == "Purchase Order")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#76C9B5");
                //}



                //string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
                //string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                //hlink2.NavigateUrl = "~/F_20_Service/RecProductEntry.aspx?Type=Entry";
                //hlink2.ToolTip = "Create New";
                if (reqtype == "Local Purchase")
                {
                    reqtype = "Local";

                }
                else
                {
                    reqtype = "Import";
                }

                hlnkgvapmrfno.NavigateUrl = "~/F_10_Procur/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno;
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=" + reqtype + "&AppType=YES";
                if (ASTUtility.Left(prjCode, 2) == "16")
                {
                    HypApproval.NavigateUrl = "~/F_15_Pro/PurReqEntry?InputType=Approval&actcode=" + prjCode + "&genno=" + reqno;
                }
                else
                {
                    HypApproval.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=FxtAstApproval&actcode=" + prjCode + "&genno=" + reqno;
                }






            }
        }

        protected void gvOrdAcRej_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HyProdPlan = (HyperLink)e.Row.FindControl("HyProdPlan");

                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();

                HyProdPlan.NavigateUrl = "~/F_15_Pro/ProdPlanTopSheet?Type=Datewise";
                HyProdPlan.ToolTip = "Production Plan";

                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid;
            }

        }

        protected void gvOrdDetailsApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hypbtnorder = (HyperLink)e.Row.FindControl("hypbtnorder");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");
                HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("HyOrderPrint");
                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                // string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));


                if (ordusrid == "")
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;




                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;
                    lnkCheck.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_01_Mer/OrderDetails?Type=Approved&actcode=" + mlccod;

                }
                HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod;
                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid;

            }

        }

        //protected void gvBOMGen_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        try { 

        //        HyperLink hypbtnMatReq = (HyperLink)e.Row.FindControl("hypbtnMatReq");
        //        HyperLink HypOrderEdit = (HyperLink)e.Row.FindControl("HypOrderEdit");                
        //        HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("OrderPrint");
        //        HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
        //        HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");
        //        string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
        //        string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
        //        string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();
        //            string dayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dayid")).Trim().ToString();
        //            string colorid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "colorid")).Trim().ToString();
        //        string date = "";
        //        if (dayid == "00000000")
        //        {
        //            date = "01-Jan-1900"; ;
        //        }
        //        else
        //        {
        //             date = Convert.ToDateTime(dayid.Substring(4, 2)+ "/" + ASTUtility.Right(dayid, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
        //        }

        //        string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
        //        string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));

        //        hypbtnMatReq.NavigateUrl = "~/F_03_CostABgd/MlcMatReq.aspx?Type=Entry&actcode=" + mlccod+"&genno="+ dayid + "&sircode="+styleid+colorid;
        //        HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=OrderPrint&mlccod=" + mlccod+"&date="+ date;
        //        HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid;
        //        HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid;
        //        HypOrderEdit.NavigateUrl = "~/F_01_Mer/OrderDetails.aspx?Type=Entry&actcode="+ mlccod;

        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //    }

        //}



        protected void gvBOMApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypbtnMatReqEntry = (HyperLink)e.Row.FindControl("hypbtnMatReqEntry");
                HyperLink hypbtnMatReq = (HyperLink)e.Row.FindControl("hypbtnMatReq");
                HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");

                HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");
                HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("OrderPrint");
                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                //HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();
                string dayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dayid")).Trim().ToString();
                string colorid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "colorid")).Trim().ToString();

                string date = "";
                if (dayid == "00000000")
                {
                    date = "01-Jan-1900"; ;
                }
                else
                {
                    date = Convert.ToDateTime(dayid.Substring(4, 2) + "/" + ASTUtility.Right(dayid, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                }
                hypbtnMatReqEntry.NavigateUrl = "~/F_03_CostABgd/MlcMatReq?Type=Entry&actcode=" + mlccod + "&genno=" + dayid + "&sircode=" + styleid + colorid;
                hypbtnMatReq.NavigateUrl = "~/F_03_CostABgd/MlcMatReq?Type=Approve&actcode=" + mlccod + "&genno=" + dayid + "&sircode=" + styleid + colorid;

                HyFOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&date=" + date + "&sircode=" + styleid + colorid + "&format=";
                HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&date=" + date;
                HyLOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=local&date=" + date + "&sircode=" + styleid + colorid + "&format=";
                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid;
            }
        }

        //protected void gvProCom_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");

        //        HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");
        //        HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("HyOrderPrint");
        //        string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
        //        string dayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dayid")).Trim().ToString();
        //        string colorid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "colorid")).Trim().ToString();
        //        string formattype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.ToString();
        //        string date = "";
        //        if (dayid == "00000000")
        //        {
        //            date = "01-Jan-1900"; ;
        //        }
        //        else
        //        {
        //            date = Convert.ToDateTime(dayid.Substring(4, 2) + "/" + ASTUtility.Right(dayid, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
        //        }

        //        string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

        //        HyFOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&date=" + date + "&sircode=" + styleid + colorid + "&format=" + formattype;
        //        HyLOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=local&date=" + date + "&sircode=" + styleid + colorid + "&format=" + formattype;
        //        HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=OrderPrint&mlccod=" + mlccod + "&date=" + date;




        //    }
        //}
        protected void btnSetup_Click(object sender, EventArgs e)
        {
            this.PnlSalesSetup.Visible = true;
            this.PnlInt.Visible = false;
            this.pnlReprots.Visible = true;
            this.plnMgf.Visible = false;
            //this.lblVal.Visible = false;

        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = true;
            this.pnlReprots.Visible = false;
            //this.plnMgf.Visible = false;
            //this.lblVal.Visible = true;
            this.PnlSalesSetup.Visible = false;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = false;
            this.pnlReprots.Visible = true;
            this.plnMgf.Visible = true;
            //this.lblVal.Visible = false;
            this.PnlSalesSetup.Visible = false;
        }



        protected void btnDelPreCost_Click(object sender, EventArgs e)
        {

            string url = "ConsumptionSheet?Type=PreCostingApp";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string inqno = ((Label)this.gvOrdAcRej.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string styleid = ((Label)this.gvOrdAcRej.Rows[index].FindControl("lblstyleid")).Text.ToString();


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "PRECOS", inqno, styleid);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Not Sucessfully";
                ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Red";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";
                ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }


        //protected void btnDelOrde_App_Click(object sender, EventArgs e)
        //{
        //    string url = "OrderDetails.aspx?Type=Approved";

        //    DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
        //    if (dr1.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
        //        return;
        //    }
        //    if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
        //        return;
        //    }
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    int index = row.RowIndex;
        //    string mlccod = ((Label)this.gvBOMGen.Rows[index].FindControl("lblmlccod")).Text.ToString();



        //    bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "ORD", mlccod);
        //    if (!result)
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Not Sucessfully";
        //        ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Red";
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        //        return;
        //    }
        //    else
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";
        //        ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green";
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        //    }

        //}

        protected void lbtnLink_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string inqno = ((Label)this.gvOrdAcRej.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();

            DataTable dt = ((DataTable)ViewState["tbllMarchanData"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "inqno='" + inqno + "' and mlccod ='000000000000' and pcosapp<>''";
            this.buyername.Text = dv.ToTable().Rows[0]["buyerdesc"].ToString();
            this.gvstylemodal.DataSource = dv.ToTable();
            this.gvstylemodal.DataBind();
            this.ModalMultiView.ActiveViewIndex = 0;
            this.lblmodalhead.Text = "Accept Or Reject  for Order";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void gvstylemodal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataTable masterlc = (DataTable)ViewState["tblmasterlc"];

            string comcod = GetCompCode();
            string txtsrch = "%";
            //string CallType = (this.Request.QueryString["Type"].Trim() == "0") ? "LCList" : "DTLLCLIST";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "MCLC_FOR_ORDER_ACCEPT", "", txtsrch, "", "", "", "", "", "", ""); ;
            if (ds1 == null)
                return;
            DataRow dr = ds1.Tables[0].NewRow();
            dr["actdesc"] = "None";
            dr["actcode"] = "000000000000";
            ds1.Tables[0].Rows.Add(dr);
            //ViewState["tblmasterlc"] = ds1.Tables[0];


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlmlccod = (DropDownList)e.Row.FindControl("ddlmlccod");
                ddlmlccod.DataTextField = "actdesc";
                ddlmlccod.DataValueField = "actcode";
                ddlmlccod.DataSource = ds1.Tables[0];
                ddlmlccod.DataBind();
                ddlmlccod.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod"));
            }
        }

        protected void lblbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            for (int i = 0; i < this.gvstylemodal.Rows.Count; i++)
            {
                string inqno = ((Label)gvstylemodal.Rows[i].FindControl("lblinqno")).Text.Trim().ToString();
                string styleid = ((Label)gvstylemodal.Rows[i].FindControl("lblstyleid")).Text.Trim().ToString();
                //string mlccod = ((DropDownList)gvstylemodal.Rows[i].FindControl("ddlmlccod")).SelectedValue.ToString();

                DataSet result = accData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_MLCCOD_WITH_SAMPLE", inqno, styleid);
                if (result == null)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid Data";
                    return;
                }
                string SubCode2 = result.Tables[0].Rows[0]["mlccod"].ToString();
                if (SubCode2 == "000000000000")
                {

                    string prevacc = result.Tables[0].Rows[0]["prevacc"].ToString();
                    string ProjectName = result.Tables[0].Rows[0]["mlcdesc"].ToString();
                    string ShortName = result.Tables[0].Rows[0]["styledesc"].ToString() + "" + result.Tables[0].Rows[0]["artno"].ToString();
                    bool output = accData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTPROJECT", "41" + prevacc.Substring(2, 6), ProjectName, ShortName, userid, "SAMPLE", inqno, styleid, "", "", "", "", "", "", "", "");

                }


            }

        }
        protected void btnDelReqRev_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = Code;//ASTUtility.Left(Code, 14).ToString();

            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string userid = hst["usrid"].ToString();



            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEREQAUTH", Reqno, userid, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Review Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Approval Delete", "Order No: ", Reqno);
        }
        //protected void ReplaceThumbnail_Click(object sender, EventArgs e)
        //{
        //    string comcod = this.GetCompCode();
        //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    int index = row.RowIndex;
        //    string mlccod = ((Label)this.gvBOMGen.Rows[index].FindControl("lblmlccod")).Text.ToString();
        //    string stylid= ((Label)this.gvBOMGen.Rows[index].FindControl("lblstyleid")).Text.ToString();
        //    string colorid = ((Label)this.gvBOMGen.Rows[index].FindControl("lblcolorid")).Text.ToString();
        //    string dayid = ((Label)this.gvBOMGen.Rows[index].FindControl("lbldayid")).Text.ToString();
        //    this.ModalMultiView.ActiveViewIndex = 1;
        //    this.mlccod.Text = mlccod + stylid + colorid+dayid;
        //    this.lblmodalhead.Text = "Color Wise Thumb Replacement";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        //}
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string Url = "";
            string posteddat = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string mlccod = this.mlccod.Text.ToString().Substring(0, 12);
            string styleid = this.mlccod.Text.Trim().ToString().Substring(12, 12);
            string colorid = this.mlccod.Text.ToString().Substring(24, 12);
            string dayid = this.mlccod.Text.ToString().Substring(36, 8);
            if (AsyncFileUpload1.HasFile)
            {
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);
                Url = "~/Upload/SAMPLE/" + random + extension;

            }

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "CHANGE_COLORWISE_SAMPLE_THUMB", mlccod, styleid, colorid, Url, dayid);
            if (result)
            {
                AsyncFileUpload1.Dispose();
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            }


        }
        protected void gvRatePro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string purtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();
                if (purtype == "Local Purchase")
                {
                    purtype = "Local";
                }
                else
                {
                    purtype = "Import";
                }
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;

                hlink2.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Approved&genno=" + reqno + "&ReqType=" + purtype;


            }
        }
        protected void btnDelCSNext_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Type = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();



            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVCSPART", Reqno, Type, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CS Check Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Next Delete", "Order No: ", Reqno);
        }
        protected void gvPenApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlkQutation");
                // HyperLink hlink3 = (HyperLink)e.Row.FindControl("HyInprPrint");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqApproved&actcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod;
                hlink2.NavigateUrl = "~/F_14_DPayReg/LinkQutaAttached?Type=QutAttached&reqno=" + reqno + "&app=1" + "&comcod=" + comcod;


            }

        }
        protected void btnDelOrderfapproved_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvPenApproval.Rows[rowindex].FindControl("lblgvreqnopapr")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEOTHERREQ", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.ManagementInterFace();

                //  dt.Rows[rowindex].Delete();
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);

            }




        }
        protected void grvApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                //  HyperLink hlink3 = (HyperLink)e.Row.FindControl("lbgvreqno");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string payid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slnum")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                hlink1.NavigateUrl = "~/F_11_Pro/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                //hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentRa.aspx?Type=ChequeApproval";
                hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentApp?Type=ChequePayment&comcod=" + comcod + "&genno=" + payid;
                //   hlink3.NavigateUrl = "~/F_11_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=Purchasetrk";


            }
        }
    }

}