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
using SPEENTITY;
using System.Drawing;

namespace SPEWEB.F_01_Mer
{
    public partial class RptMerChanInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        UserManagerSampling objUserMan = new UserManagerSampling();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Merchandising Smartface";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy"); //Convert.ToDateTime("01-Jan-2019").ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                switch (this.GetCompCode())
                {
                    case "5305":   // FB  
                    case "5306":   // Footbed 
                        this.RadioButtonList1.SelectedIndex = 3;
                        this.gvProCom.Columns[18].Visible = false;
                        break;
                    default:
                        this.RadioButtonList1.SelectedIndex = 0;
                        break;
                }
                //this.SaleRequRpt();
                this.PnlInt.Visible = true;
                this.ImportInterFace();
                this.PanelVisible();
                GetGenCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                HyperLink hyp1 = (HyperLink)this.HyperLink1 as HyperLink;
                HyperLink hyp2 = (HyperLink)this.HyperLink2 as HyperLink;
                hyp1.NavigateUrl = "~/F_01_Mer/SampleInquiry?Type=Entry&comcod=" + comcod + "&genno=";
                hyp2.NavigateUrl = "~/F_01_Mer/OrderDetails?Type=Reorder&actcode=";
             
            }

            if (FileUpLoad1.HasFile)
            {
                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string filename = System.IO.Path.GetFileName(FileUpLoad1.FileName);
                string Url = "";
                string posteddat = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string mlccod = this.mlccod.Text.ToString().Substring(0, 12);
                string styleid = this.mlccod.Text.Trim().ToString().Substring(12, 12);
                string colorid = this.mlccod.Text.ToString().Substring(24, 12);
                string dayid = this.mlccod.Text.ToString().Substring(36, 8);
                string extension = Path.GetExtension(FileUpLoad1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                FileUpLoad1.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);
                Url = "~/Upload/SAMPLE/" + random + extension;
                bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "CHANGE_COLORWISE_SAMPLE_THUMB", mlccod, styleid, colorid, Url, dayid);
                if (result)
                {

                    this.ImportInterFace();
                    ((Label)this.Master.FindControl("lblmsg")).Text = "File Uploaded: " + FileUpLoad1.FileName;
                }

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            //  ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;
            //  List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>)Session["lstgencode"];
            var lstseason = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "33");
            lstseason.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "All"));

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = lstseason;
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
        private void PanelVisible()
        {
            if (this.Request.QueryString["Type"].ToString() == "PD")
            {
                this.RadioButtonList1.Items[8].Attributes.CssStyle.Add("visibility", "hidden");// .AddAttributes("style=display:none");
                this.RadioButtonList1.Items[7].Attributes.CssStyle.Add("visibility", "hidden");
                this.RadioButtonList1.Items[6].Attributes.CssStyle.Add("visibility", "hidden");
                this.RadioButtonList1.Items[5].Attributes.CssStyle.Add("visibility", "hidden");
                this.RadioButtonList1.Items[4].Attributes.CssStyle.Add("visibility", "hidden");
                this.RadioButtonList1.Items[3].Attributes.CssStyle.Add("visibility", "hidden");
                this.RadioButtonList1.Items[2].Attributes.CssStyle.Add("visibility", "hidden");


            }
          

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int day = ASTUtility.Datediffday(Convert.ToDateTime(this.txtFDate.Text), Convert.ToDateTime(this.txtdate.Text));
            if (day != 0)
                return;
            txtdate_TextChanged(null, null);


        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        public string GetArticle()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst["comcod"].ToString() == "5301")
            {
                return "Edison Article";
            }
            else
            {
                return "Sys. Gen. Article";

            }
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.ImportInterFace();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ImportInterFace();
        }
        private void ImportInterFace()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string seasson = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "SHOW_MERCHAN_INTERFACE", Date1, Date2, seasson, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbllMarchanData"] = ds1.Tables[0];
            ViewState["tbllBomData"] = ds1.Tables[1];

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[2].Rows[0]["tinqlist"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Sample Inquiry</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[2].Rows[0]["tcons"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Consumption Sheet</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[2].Rows[0]["tpcost"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> CBD Sheet </div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[2].Rows[0]["tordacrej"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> Accept Or Reject</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[2].Rows[0]["toord"].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'> Order Sheet</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[2].Rows[0]["toordapp"].ToString() + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> Order Approval</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[2].Rows[0]["tbom"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> BOM Generate</div></div></div>";

            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[2].Rows[0]["tbomapp"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> BOM Approval</div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[2].Rows[0]["tcomp"].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Complete</div></div></div>";

            RadioButtonList1_SelectedIndexChanged(null, null);


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PanelVisible();
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tbllMarchanData"];
            DataTable bomdata = (DataTable)ViewState["tbllBomData"];

            DataTable Tempdt = new DataTable();
            DataView Tempdv = new DataView();
            switch (value)
            {
                case "0": ///Inquary
                    this.pnlallInqList.Visible = true;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = false;
                    this.Data_Bind("gvSmpleinqlist", dt);
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1": ///Consumtion
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = true;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("approved <>'' and conapp='' ");
                    this.Data_Bind("gvConSheet", Tempdv.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2": ///Pre-Costing
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = true;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("conapp <> '' and pcosapp=''");
                    this.Data_Bind("gvPreCost", Tempdv.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3": ///Accept or Reject
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = true;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("mlccod ='000000000000' and pcosapp<>''");
                    //Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved=''");
                    this.Data_Bind("gvOrdAcRej", Tempdv.ToTable());
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4": ///Order Input
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = true;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("mlccod <>'000000000000' and ordusrid=''");
                    //Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved=''");
                    this.Data_Bind("gvOrdDetails", Tempdv.ToTable());
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
                case "5": ///Order Approved
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = true;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("ordusrid <>'' and ordapp='' ");
                    this.Data_Bind("gvOrdDetailsApp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;

                case "6": /// BOM Generate
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = true;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = bomdata.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("ordapp ='Ok' and  (bomusrid='' Or frwdbyid='') ");
                    this.Data_Bind("gvBOMGen", Tempdv.ToTable());
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;
                case "7": ///BOM approval
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = true;
                    this.PnlProComp.Visible = false;
                    Tempdt = bomdata.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("bomusrid<>'' and frwdbyid<>'' and  bomapp=''");
                    this.Data_Bind("gvBOMApp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    break;
                case "8": ///Total
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.PanPreCost.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    this.PanOrdDet.Visible = false;
                    this.PanOrdDetApp.Visible = false;
                    this.pnlBOMGen.Visible = false;
                    this.PnlBomApp.Visible = false;
                    this.PnlProComp.Visible = true;
                    Tempdt = bomdata.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("bomapp<>''");
                    this.Data_Bind("gvProCom", Tempdv.ToTable());
                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    break;


            }
        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvSmpleinqlist":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvSmpleinqlist.DataSource = dt;
                    this.gvSmpleinqlist.DataBind();
                    break;
                case "gvConSheet":
                    this.gvConSheet.DataSource = (dt);
                    this.gvConSheet.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvPreCost":
                    this.gvPreCost.DataSource = (dt);
                    this.gvPreCost.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;  //
                case "gvOrdAcRej":
                    this.gvOrdAcRej.DataSource = (dt);
                    this.gvOrdAcRej.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvOrdDetails":
                    this.gvOrdDetails.DataSource = (dt);
                    this.gvOrdDetails.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvOrdDetailsApp":
                    this.gvOrdDetailsApp.DataSource = (dt);
                    this.gvOrdDetailsApp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvBOMGen":
                    this.gvBOMGen.DataSource = (dt);
                    this.gvBOMGen.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvBOMApp":
                    this.gvBOMApp.DataSource = (dt);
                    this.gvBOMApp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvProCom":
                    this.gvProCom.DataSource = (dt);
                    this.gvProCom.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
            }
        }
        protected void gvSmpleinqlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");
                HyperLink InprPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink HypCondir = (HyperLink)e.Row.FindControl("HypCondir");
                HyperLink HypConcom = (HyperLink)e.Row.FindControl("HypConcom");
                LinkButton SamDelInq = (LinkButton)e.Row.FindControl("btnSamDelInq");
                // HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("HyOrderPrint");
                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");
                //HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");
                //HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");

                if (this.Request.QueryString["Type"].ToString() == "PD")
                {
                    // HyOrderPrint.Visible = false;
                    HyPreCostPrint.Visible = false;
                    HyCommPreCostPrint.Visible = false;
                    //HyFOrderPrint.Visible = false;
                    //HyLOrderPrint.Visible = false;
                }

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string apstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approved"));
                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
                if (apstatus != "Ok")
                {
                    lnkEdit.NavigateUrl = "~/F_01_Mer/SampleInquiry?Type=Edit&genno=" + inqno;
                    lnkCheck.NavigateUrl = "~/F_01_Mer/SampleInquiry?Type=Approv&genno=" + inqno;
                    SamDelInq.Enabled = true;
                }
                else
                {
                    lnkEdit.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkEdit.CssClass = "btn btn-xs btn-none";
                    lnkEdit.ToolTip = "Approved";
                    SamDelInq.Enabled = false;
                    SamDelInq.OnClientClick = "Not delete";
                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Locked";
                }
                InprPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=INQPrint&inqno=" + inqno + "&printtype=" + printType;
                HypCondir.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=ConSheetPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HypConcom.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommConSheetPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                //HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=OrderPrint&mlccod=" + mlccod;
                //HyFOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=BOMPrint&mlccod=" + mlccod;         
                // HyLOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint.aspx?Type=BOMPrint&mlccod=" + mlccod;
            }
        }

        protected void gvConSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnCons = (HyperLink)e.Row.FindControl("lbtnCons");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");
                HyperLink HyConsPrint = (HyperLink)e.Row.FindControl("HyConsPrint");
                HyperLink HyCommConsPrint = (HyperLink)e.Row.FindControl("HyCommConsPrint");

                LinkButton btnDelInq = (LinkButton)e.Row.FindControl("btnDelInq");


                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string dconstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dconstatus"));
                string conusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conusrid"));

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();



                if (conusrid == "")
                {
                    lbtnCons.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=Entry&actcode=" + inqno + "&genno=" + styleid;

                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    lbtnCons.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=Entry&actcode=" + inqno + "&genno=" + styleid;
                    lnkCheck.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=ConApp&actcode=" + inqno + "&genno=" + styleid;

                    HyConsPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=ConSheetPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                    HyCommConsPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommConSheetPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;

                }

                //if (dconstatus == "Ok")
                //{

                //    btnDelInq.Enabled = false;
                //    btnDelInq.OnClientClick="Not delete";
                //}


                //Session["Report1"] = gvConSheet;
                //((HyperLink)this.gvConSheet.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvPreCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnCost = (HyperLink)e.Row.FindControl("lbtnCost");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");

                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                LinkButton btnDelCons = (LinkButton)e.Row.FindControl("btnDelCons");


                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string pcosusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pcosusrid"));
                string dprecostst = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dprecostst"));


                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();



                if (pcosusrid == "")
                {
                    lbtnCost.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=PreCosting&actcode=" + inqno + "&genno=" + styleid;

                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    lbtnCost.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=PreCosting&actcode=" + inqno + "&genno=" + styleid;
                    lnkCheck.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=PreCostingApp&actcode=" + inqno + "&genno=" + styleid;

                }
                // // if (dprecostst == "Ok")
                //   {

                //  btnDelCons.Enabled = false; // this button enable as per sumon vi req. when consumption completed and forwarded to CBD and CBD Complete  so that button should be disable
                // }

                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
            }
        }
        protected void gvOrdAcRej_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                HyperLink HyProdPlan = (HyperLink)e.Row.FindControl("HyProdPlan");

                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");
                LinkButton btnDelPreCost = (LinkButton)e.Row.FindControl("btnDelPreCost");
                if (comcod== "5305" || comcod == "5306")
                {
                    btnDelPreCost.Visible = false;
                }
                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                HyProdPlan.NavigateUrl = "~/F_15_Pro/ProdPlanTopSheet?Type=Datewise";
                HyProdPlan.ToolTip = "Production Plan";

                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
            }

        }
        protected void gvOrdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hypbtnorder = (HyperLink)e.Row.FindControl("hypbtnorder");

                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");


                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));

                //hyporder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_01_Mer/OrderDetails.aspx?Type=Entry&actcode=" + mlccod;

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();


                if (ordusrid == "")
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;




                }

                else
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;

                }
                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
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
                string date = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordrcvdat")).Trim().ToString();
                string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));
                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();


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
                HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&styleid=" + styleid + "&date=" + date + "&printtype=" + printType;
                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;

            }

        }

        protected void gvBOMGen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    HyperLink hypbtnMatReq = (HyperLink)e.Row.FindControl("hypbtnMatReq");
                    HyperLink hypbtnMatReq1 = (HyperLink)e.Row.FindControl("hypbtnMatReq1");
                    HyperLink HypOrderEdit = (HyperLink)e.Row.FindControl("HypOrderEdit");
                    HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("OrderPrint");
                    HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                    HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                    string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

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
                    DataTable Tempdt;
                    DataView Tempdv;
                    string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                    string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));
                    DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", mlccod, dayid, "", "", "", "", "", "", "");
                    Tempdt = ds2.Tables[0].Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("gcod ='010100101009' or gcod ='010100101010'");
                    Tempdt = Tempdv.ToTable();
                    if ((Tempdt.Rows[0]["gdesc1"].ToString()!= "") && (Tempdt.Rows[1]["gdesc1"].ToString()!=""))
                    {
                        
                        hypbtnMatReq.Enabled = true;
                        hypbtnMatReq.NavigateUrl = "~/F_03_CostABgd/MlcMatReq?Type=Entry&actcode=" + mlccod + "&genno=" + dayid + "&sircode=" + styleid + colorid;
                    }
                    else
                    {
                        hypbtnMatReq1.Enabled = true;
                        hypbtnMatReq.BackColor = Color.Yellow;
                        hypbtnMatReq.ToolTip = "Please Add Conversion Rate";
                        hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + dayid;
                    }
                    
                    HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&date=" + date + "&styleid=" + styleid + "&printtype=" + printType;
                    HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                    HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                    HypOrderEdit.NavigateUrl = "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;

                }
                catch (Exception ex)
                {

                }

            }

        }



        protected void gvBOMApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypbtnMatReqEntry = (HyperLink)e.Row.FindControl("hypbtnMatReqEntry");
                HyperLink hypbtnMatReq = (HyperLink)e.Row.FindControl("hypbtnMatReq");
                HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");
                HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("OrderPrint");
                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                //string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                //HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();
                string dayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dayid")).Trim().ToString();
                string colorid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "colorid")).Trim().ToString();
                string formattype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.ToString();
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

                HyFOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&date=" + date + "&styleid=" + styleid + "&printtype=" + printType;
                HyLOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=local&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
            }
        }

        protected void gvProCom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");
                HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("HyOrderPrint");
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string dayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dayid")).Trim().ToString();
                string colorid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "colorid")).Trim().ToString();
                string formattype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.ToString();
                string date = "";
                if (dayid == "00000000")
                {
                    date = "01-Jan-1900"; ;
                }
                else
                {
                    date = Convert.ToDateTime(dayid.Substring(4, 2) + "/" + ASTUtility.Right(dayid, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                }

                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                HyFOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                HyLOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=local&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                HyOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&date=" + date + "&styleid=" + styleid + "&printtype=" + printType;




            }
        }
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






        protected void btnDelInq_Click(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            string url = "ConsumptionSheet?Type=ConApp";
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
            string inqno = ((Label)this.gvConSheet.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "INQ", inqno);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Not Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";
            ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }

        protected void btnDelCons_Click(object sender, EventArgs e)
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
            string inqno = ((Label)this.gvPreCost.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string styleid = ((Label)this.gvPreCost.Rows[index].FindControl("lblstyleid")).Text.ToString();


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "CON", inqno, styleid);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Not Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }

        protected void btnDelPreCost_Click(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
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

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }

        protected void btnDelAc_Rej_Click(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            string url = "OrderDetails?Type=Entry";

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
            string inqno = ((Label)this.gvOrdDetails.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string styleid = ((Label)this.gvOrdDetails.Rows[index].FindControl("lblstyleid")).Text.ToString();


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "ACRREJ", inqno, styleid);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Not Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }//

        protected void btnDelOrde_App_Click(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            string url = "OrderDetails?Type=Approved";

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
            string mlccod = ((Label)this.gvBOMGen.Rows[index].FindControl("lblmlccod")).Text.ToString();
            string dayid = ((Label)this.gvBOMGen.Rows[index].FindControl("lbldayid")).Text.ToString();

            if (dayid != "00000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Re - Order Unable To Reverse');", true);
                return;
            }

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "ORD", mlccod);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Not Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }


        protected void lnkIndPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            int index = row.RowIndex;
            string inqnum = "";
            string CurDate1 = "";
            string buyer = "";
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            buyer = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("txtgvSupplier")).Text.ToString();
            inqnum = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            CurDate1 = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("txtgvItemdescc")).Text.ToString();
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqnum, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                // ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataTable dt = ds1.Tables[0];


            var lst = new List<SPEENTITY.C_01_Mer.EclassSampleInquiry>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.EclassSampleInquiry();

                obj1.styleid = dr1["styleid"].ToString();
                obj1.styledesc = dr1["styledesc"].ToString();
                obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);
                obj1.color = dr1["color"].ToString();
                //obj1.xmldata = dr1["xmldata"].ToString();   
                obj1.artno = dr1["artno"].ToString();
                obj1.catedesc = dr1["catedesc"].ToString();
                obj1.category = dr1["category"].ToString();
                obj1.comcod = dr1["comcod"].ToString();
                obj1.consize = dr1["consize"].ToString();
                obj1.samsize = dr1["samsize"].ToString();
                obj1.sirunit = dr1["sirunit"].ToString();

                obj1.attchmnt = dr1["attchmnt"].ToString();
                obj1.sizernge = dr1["sizernge"].ToString();
                //string att = obj1.attchmnt;
                obj1.attchmnt = (dr1["attchmnt"].ToString().Length == 0 ? "" : new Uri(Server.MapPath(dr1["attchmnt"].ToString())).AbsoluteUri);


                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;


                lst.Add(obj1);
            }


            LocalReport rpt1 = new LocalReport();



            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptSampleEntry", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("buyer", "CLIENT NAME: " + buyer));
            rpt1.SetParameters(new ReportParameter("date", "DATE: " + CurDate1));
            rpt1.SetParameters(new ReportParameter("inqnum", "INQUERY NO: " + inqnum));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Sample Inquery Entry "));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }

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
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
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

        protected void ReplaceThumbnail_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = ((Label)this.gvBOMGen.Rows[index].FindControl("lblmlccod")).Text.ToString();
            string stylid = ((Label)this.gvBOMGen.Rows[index].FindControl("lblstyleid")).Text.ToString();
            string colorid = ((Label)this.gvBOMGen.Rows[index].FindControl("lblcolorid")).Text.ToString();
            string dayid = ((Label)this.gvBOMGen.Rows[index].FindControl("lbldayid")).Text.ToString();
            this.ModalMultiView.ActiveViewIndex = 1;
            this.mlccod.Text = mlccod + stylid + colorid + dayid;
            this.lblmodalhead.Text = "Color Wise Thumb Replacement";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void ReplaceThumbnail_Click2(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = ((Label)this.gvProCom.Rows[index].FindControl("lblMlccod")).Text.ToString();
            string stylid = ((Label)this.gvProCom.Rows[index].FindControl("lblstyleid")).Text.ToString();
            string colorid = ((Label)this.gvProCom.Rows[index].FindControl("lblcolorid")).Text.ToString();
            string dayid = ((Label)this.gvProCom.Rows[index].FindControl("lbldayid")).Text.ToString();
            this.ModalMultiView.ActiveViewIndex = 1;
            this.mlccod.Text = mlccod + stylid + colorid + dayid;
            this.lblmodalhead.Text = "Color Wise Thumb Replacement";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();
            //string comcod = this.GetCompCode();
            //string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            //string Url = "";
            //string posteddat = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //string mlccod = this.mlccod.Text.ToString().Substring(0, 12);
            //string styleid = this.mlccod.Text.Trim().ToString().Substring(12, 12);
            //string colorid = this.mlccod.Text.ToString().Substring(24, 12);
            //string dayid = this.mlccod.Text.ToString().Substring(36, 8);
            //if (AsyncFileUpload1.HasFile)
            //{
            //    string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
            //    string random = ASTUtility.RandNumber(1, 99999).ToString();
            //    AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);
            //    Url = "~/Upload/SAMPLE/" + random + extension;

            //}

            //bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "CHANGE_COLORWISE_SAMPLE_THUMB", mlccod, styleid, colorid, Url, dayid);
            //if (result)
            //{
            //    AsyncFileUpload1.Dispose();
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //}


        }

        protected void btnSamDelInq_Click(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            string url = "ConsumptionSheet?Type=ConApp";
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
            string inqno = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "SAMINQ", inqno);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Not Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Sucessfully";
            ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
    }
}