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

namespace SPEWEB.F_04_Sampling
{
    public partial class SamplingInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        UserManagerSampling objUserMan = new UserManagerSampling();

        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sample Development Smartface";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy"); //Convert.ToDateTime("01-Jan-2019").ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                this.PnlInt.Visible = true;
                GetGenCode();
                Get_BuyerName();
                this.Get_SampleType();
                this.SamplingInterFace();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                HyperLink hyp1 = (HyperLink)this.HyperLink1 as HyperLink;
                hyp1.NavigateUrl = "~/F_04_Sampling/SamSampleInquiry?Type=Entry&comcod=" + comcod + "&genno=";


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

            var lstsm = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "34");
            DdlSamType.DataTextField = "gdesc";
            DdlSamType.DataValueField = "gcod";
            DdlSamType.DataSource = lstsm;
            DdlSamType.DataBind();

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


            var lstpatterdesigner = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "36");
            lstpatterdesigner.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            DdlPaterDesigner.DataTextField = "gdesc";
            DdlPaterDesigner.DataValueField = "gcod";
            DdlPaterDesigner.DataSource = lstpatterdesigner;
            DdlPaterDesigner.DataBind();
            DdlPaterDesigner.SelectedValue = "00000";

            var lstagent = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "32");
            lstagent.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "All"));


            DdlAgent.DataTextField = "gdesc";
            DdlAgent.DataValueField = "gcod";
            DdlAgent.DataSource = lstagent;
            DdlAgent.DataBind();
            DdlAgent.SelectedValue = "00000";
        }
        private void Get_SampleType()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "Get_Sample_Type", "", "", "", "", "", "", "", "", "");

            ddlsampletype.DataTextField = "gdesc";
            ddlsampletype.DataValueField = "gcod";
            ddlsampletype.DataSource = ds2.Tables[0];
            ddlsampletype.SelectedValue = "00000";
            ddlsampletype.DataBind();

        }

        private void Get_BuyerName()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            DdlCustomer.DataTextField = "sirdesc";
            DdlCustomer.DataValueField = "sircode";
            DdlCustomer.DataSource = ds2.Tables[0];
            DdlCustomer.DataBind();

            DataView dv = ds2.Tables[0].DefaultView;
            dv.RowFilter = "sircode not like '000000000000%'";

            DataTable dt1 = dv.ToTable();
            DataRow dr1 = dt1.NewRow();
            dr1["sircode"] = "000000000000";
            dr1["sirdesc"] = "--All--";
            dt1.Rows.Add(dr1);

            ddlbuyer.DataTextField = "sirdesc";
            ddlbuyer.DataValueField = "sircode";
            ddlbuyer.DataSource = dt1;
            ddlbuyer.SelectedValue = "000000000000";
            ddlbuyer.DataBind();

        }

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
            this.SamplingInterFace();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.SamplingInterFace();
        }
        private void SamplingInterFace()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            var difdate = (Convert.ToDateTime(Date2) - Convert.ToDateTime(Date1)).TotalDays;
            if ((Convert.ToDateTime(Date2) - Convert.ToDateTime(Date1)).TotalDays >= 31)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Maximum 31 days of data can show at a time.');", true);
                return;
            }
            string seasson = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string buyer = (this.ddlbuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlbuyer.SelectedValue.ToString() + "%";
            string agent = (this.DdlAgent.SelectedValue.ToString() == "00000") ? "%" : this.DdlAgent.SelectedValue.ToString() + "%";
            string sampletype = (this.ddlsampletype.SelectedValue.ToString() == "00000") ? "%" : this.ddlsampletype.SelectedValue.ToString() + "%";
            
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "SHOWPROTOSAMPLINGINTERFACE", Date1, Date2, seasson, buyer, agent, sampletype, "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbllMarchanData"] = ds1.Tables[0];
            ViewState["tbllBomData"] = ds1.Tables[1];

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[2].Rows[0]["collsam"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Sample</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[2].Rows[0]["const"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Basic Information</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[2].Rows[0]["pdguide"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> PD GUIDE </div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[2].Rows[0]["issue"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> Issue/Production</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[2].Rows[0]["pdguideapp"].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Modified PD Guide</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[2].Rows[0]["cbd"].ToString() + "</i></div></a><div class='circle-tile-content purple '><div class='circle-tile-description text-faded'>CBD Sheet Draft</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[2].Rows[0]["complete"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Complete</div></div></div>";

            //this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[2].Rows[0]["export"].ToString() + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> Order Approval</div></div></div>";

            //this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[2].Rows[0]["export"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> BOM Generate</div></div></div>";

            //this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[2].Rows[0]["export"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> BOM Approval</div></div></div>";
            //this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[2].Rows[0]["export"].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Complete</div></div></div>";

            RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.PanelVisible();
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
                    this.Panelpdguide.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.PnlFinpdguideapp.Visible = false;
                    this.PnlConCost.Visible = false;
                    this.PnlProComp.Visible = false;
                    this.Data_Bind("gvSmpleinqlist", dt);
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1": ///Basic Information
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = true;
                    this.Panelpdguide.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.PnlFinpdguideapp.Visible = false;
                    this.PnlConCost.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("approved <>'' and conapp='' ");
                    this.Data_Bind("gvConSheet", Tempdv.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;

                case "2": ///PD Guide
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.Panelpdguide.Visible = true;
                    this.PnlIssued.Visible = false;
                    this.PnlFinpdguideapp.Visible = false;
                    this.PnlConCost.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("conapp <> '' and pgusrid=''");
                    this.Data_Bind("gvpdguide", Tempdv.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;

                case "3": ///Issued
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.Panelpdguide.Visible = false;
                    this.PnlIssued.Visible = true;
                    this.PnlFinpdguideapp.Visible = false;
                    this.PnlConCost.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("pgaitemusrid <> '' and pgausrid=''");
                    this.Data_Bind("gvissued", Tempdv.ToTable());
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;


                case "4": ///Approval(PD Guide)
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.Panelpdguide.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.PnlFinpdguideapp.Visible = true;
                    this.PnlConCost.Visible = false;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("pgaitemusrid <> '' and pgausrid=''");
                    this.Data_Bind("gvpdguidapp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;

                case "5": ///CBD Sheet
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.Panelpdguide.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.PnlFinpdguideapp.Visible = false;
                    this.PnlConCost.Visible = true;
                    this.PnlProComp.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("pgausrid <> '' and pcbdapusrid=''");
                    this.Data_Bind("gvPreCost", Tempdv.ToTable());
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;

                case "6": ///Complete
                    this.pnlallInqList.Visible = false;
                    this.pnlConShet.Visible = false;
                    this.Panelpdguide.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.PnlFinpdguideapp.Visible = false;
                    this.PnlConCost.Visible = false;
                    this.PnlProComp.Visible = true;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("pcbdapusrid<>''");
                    this.Data_Bind("gvProCom", Tempdv.ToTable());
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;


            }
        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvSmpleinqlist":
                    this.gvSmpleinqlist.DataSource = dt;
                    this.gvSmpleinqlist.DataBind();
                    break;
                case "gvConSheet":
                    this.gvConSheet.DataSource = (dt);
                    this.gvConSheet.DataBind();
                    break;

                case "gvpdguide":
                    this.gvpdguide.DataSource = (dt);
                    this.gvpdguide.DataBind();
                    break;

                case "gvissued":
                    this.gvissued.DataSource = (dt);
                    this.gvissued.DataBind();
                    break;



                case "gvpdguidapp":
                    this.gvpdguidapp.DataSource = (dt);
                    this.gvpdguidapp.DataBind();
                    break;

                case "gvPreCost":
                    this.gvPreCost.DataSource = (dt);
                    this.gvPreCost.DataBind();

                    break;  //

                case "gvProCom":
                    this.gvProCom.DataSource = (dt);
                    this.gvProCom.DataBind();

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
                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");

                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

                if (this.Request.QueryString["Type"].ToString() == "PD")
                {

                    HyPreCostPrint.Visible = false;
                    HyCommPreCostPrint.Visible = false;

                }

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                int sampleid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "sampleid"));
                //string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string apstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approved"));
                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                if (apstatus != "Ok")
                {
                    lnkEdit.NavigateUrl = "~/F_04_Sampling/SamSampleInquiry?Type=Edit&genno=" + inqno + "&inqno=" + sampleid;
                    lnkCheck.NavigateUrl = "~/F_04_Sampling/SamSampleInquiry?Type=Approv&genno=" + inqno + "&inqno=" + sampleid;
                }
                else
                {
                    lnkEdit.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkEdit.CssClass = "btn btn-xs btn-none";
                    lnkEdit.ToolTip = "Approved";

                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Locked";
                }

                InprPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=INQPrint&inqno=" + sampleid + "&printtype=" + printType;

            }
        }

        protected void gvConSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnCons = (HyperLink)e.Row.FindControl("lbtnCons");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");
                HyperLink HyConsPrint = (HyperLink)e.Row.FindControl("HyConsPrint");
                LinkButton btnDelInq = (LinkButton)e.Row.FindControl("btnDelInq");

                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();


                string dconstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string conusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conusrid"));

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();



                if (conusrid == "")
                {
                    lbtnCons.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=Entry&genno=" + inqno;

                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    lbtnCons.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=Entry&genno=" + inqno;
                    lnkCheck.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=ConApp&genno=" + inqno;

                    HyConsPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PGEntry&genno=" + inqno + "&printtype=" + printType;

                }

                if (dconstatus == "Ok")
                {

                    btnDelInq.Enabled = false;

                }



            }

        }

        protected void gvpdguide_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnconpg = (HyperLink)e.Row.FindControl("lbtnconpg");
                HyperLink HypgPrint = (HyperLink)e.Row.FindControl("HypgPrint");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();

                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

                string pgusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pgusrid"));

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchcode")).ToString();
                lbtnconpg.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=PGEntry&genno=" + inqno + "&actcode=" + actcode;

                HypgPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PGEntry&genno=" + inqno + "&printtype=" + printType;

            }
        }
        protected void gvPreCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnCost = (HyperLink)e.Row.FindControl("lbtnCost");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");
                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");

                HyperLink HycbdpdappPrint = (HyperLink)e.Row.FindControl("HycbdpdappPrint");
                LinkButton btnDelCons = (LinkButton)e.Row.FindControl("btnDelCons");
                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());



                string pcbdusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pcbdusrid"));



                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();



                if (pcbdusrid == "")
                {
                    lbtnCost.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=PreCosting&genno=" + inqno;

                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    lbtnCost.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=PreCosting&genno=" + inqno;
                    lnkCheck.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=PreCostingApp&genno=" + inqno;

                }

                HycbdpdappPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PGEntry&genno=" + inqno + "&printtype=" + printType;
                HyPreCostPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PreCostPrint&genno=" + inqno + "&printtype=" + printType;
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
                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                HyProdPlan.NavigateUrl = "~/F_15_Pro/ProdPlanTopSheet?Type=Datewise";
                HyProdPlan.ToolTip = "Production Plan";

                HyPreCostPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PreCostPrint&genno=" + inqno + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=CommPreCostPrint&genno=" + inqno + "&printtype=" + printType;
            }
        }
        protected void gvOrdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hypbtnorder = (HyperLink)e.Row.FindControl("hypbtnorder");

                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");
                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());


                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));

                //hyporder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_04_Sampling/OrderDetails.aspx?Type=Entry&actcode=" + mlccod;

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();


                if (ordusrid == "")
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_04_Sampling/OrderDetails?Type=Entry&actcode=" + mlccod;




                }

                else
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_04_Sampling/OrderDetails?Type=Entry&actcode=" + mlccod;

                }
                HyPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
            }

        }

        protected void gvOrdDetailsApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

                HyperLink hypbtnorder = (HyperLink)e.Row.FindControl("hypbtnorder");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");
                HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("HyOrderPrint");
                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));
                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();


                if (ordusrid == "")
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_04_Sampling/OrderDetails?Type=Entry&actcode=" + mlccod;




                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    hypbtnorder.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_04_Sampling/OrderDetails?Type=Entry&actcode=" + mlccod;
                    lnkCheck.NavigateUrl = (mlccod == "000000000000") ? "" : "~/F_04_Sampling/OrderDetails?Type=Approved&actcode=" + mlccod;

                }
                HyOrderPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&styleid=" + styleid + "&printtype=" + printType;
                HyPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;

            }

        }

        protected void gvBOMGen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

                    HyperLink hypbtnMatReq = (HyperLink)e.Row.FindControl("hypbtnMatReq");
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

                    string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                    string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ordusrid"));


                    hypbtnMatReq.NavigateUrl = "~/F_03_CostABgd/MlcMatReq?Type=Entry&actcode=" + mlccod + "&genno=" + dayid + "&sircode=" + styleid + colorid;
                    HyOrderPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&date=" + date + "&styleid=" + styleid + "&printtype=" + printType;
                    HyPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                    HyCommPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                    HypOrderEdit.NavigateUrl = "~/F_04_Sampling/OrderDetails?Type=Entry&actcode=" + mlccod;

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
                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

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

                HyFOrderPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                HyOrderPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=OrderPrint&mlccod=" + mlccod + "&date=" + date + "&styleid=" + styleid + "&printtype=" + printType;
                HyLOrderPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=local&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                HyPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_04_Sampling/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
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

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string samptype = ((Label)e.Row.FindControl("gvpcLblSampType")).Text.Trim();

                string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "iscomplite"));
                string transstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "transstatus"));
                //
                if (ordusrid == "Completed")
                {
                    LinkButton LbtnForward = (LinkButton)e.Row.FindControl("LbtnForward");
                    LbtnForward.Enabled = false;
                    LbtnForward.ForeColor = System.Drawing.Color.Red;
                }
                if (transstatus == "Transferred")
                {
                    LinkButton LbtnFinal = (LinkButton)e.Row.FindControl("LbtnFinal");
                    LbtnFinal.Enabled = false;
                    LbtnFinal.ForeColor = System.Drawing.Color.Red;

                }
                HyOrderPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PreCostPrint&genno=" + inqno + "&printtype=" + printType;

                if (samptype != "34005")
                {
                    ((LinkButton)e.Row.FindControl("LbtnFinal")).Visible = false;
                }
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

            string url = "SamConsumptionSheet?Type=Entry";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);


                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string inqno = ((Label)this.gvConSheet.Rows[index].FindControl("lblgvinqnocst")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "REV_ALL_SAMPLING_PROCESS", "INQ", inqno);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Reverse Not Sucessfully');", true);


                return;
            }
            this.SamplingInterFace();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Reverse Sucessfully');", true);


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
            string usrid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string sdino = this.ModalSdino.Text.Trim();
            string makenew = (this.MakeNew.Checked == true) ? "True" : "False";
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "SAMPLE_FINAL_SUBMIT", sdino, usrid, Posttrmid, PostSession, makenew, "", "");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sample Final Submit Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went wrong!!');", true);

            }


        }





        protected void btnDelConspg_Click(object sender, EventArgs e)
        {
            string url = "SamConsumptionSheet?Type=PGEntry";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);


                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sampleid = ((Label)this.gvpdguide.Rows[index].FindControl("LblSampleId")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "REV_ALL_SAMPLING_PROCESS", "BASIC", sampleid);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Reverse Not Sucessfully');", true);


                return;
            }
            this.SamplingInterFace();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Reverse To Basic Information Sucessfully');", true);



        }

        protected void btnDelCons_Click(object sender, EventArgs e)
        {
            string url = "SamConsumptionSheet?Type=PreCosting";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);


                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sampleid = ((Label)this.gvPreCost.Rows[index].FindControl("LblSampleId")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "REV_ALL_SAMPLING_PROCESS", "MPDG", sampleid);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Reverse Not Sucessfully');", true);


                return;
            }
            this.SamplingInterFace();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Reverse To Basic Information Sucessfully');", true);


        }

        protected void gvissued_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnIssued = (HyperLink)e.Row.FindControl("lbtnIssued");
                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchcode"));
                string reptype = "SAMPLING";
                lbtnIssued.NavigateUrl = "~/F_11_RawInv/SamplingMatIssue?Type=Entry&genno=" + inqno + "&actcode=" + actcode + "&reptype=" + reptype;

                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());



            }
        }

        protected void btnDelConsiss_Click(object sender, EventArgs e)
        {

        }

        protected void gvpdguidapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkCheckpdapp = (HyperLink)e.Row.FindControl("lnkCheckpdapp");
                HyperLink HypdappPrint = (HyperLink)e.Row.FindControl("HypdappPrint");

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                lnkCheckpdapp.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=PGApp&genno=" + inqno;
                HypdappPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PGEntry&genno=" + inqno + "&printtype=" + printType;

                e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString());

            }

        }

        protected void LbtnReRunUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();

            string comcod = this.GetCompCode();
            string sdino = this.TxtSdino.Text.ToString();
            string buyerid = this.DdlCustomer.SelectedValue.ToString();
            string samptype = this.DdlSamType.SelectedValue.ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAMPLE_DEV_RERUN", sdino, buyerid, samptype, usrid, Posttrmid, PostSession, "", "", "");

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sample Inquery Re-run Successfully');", true);
            }
        }

        protected void LbtnFinal_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrname = hst["usrname"].ToString();
            this.ModalSdino.Text = String.Empty;
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string article = ((Label)this.gvProCom.Rows[index].FindControl("lblgvarticlenocom")).Text.ToString();
            ModalSdino.Text = ((Label)this.gvProCom.Rows[index].FindControl("lblSdino")).Text.ToString();
            string sdino = ((Label)this.gvProCom.Rows[index].FindControl("lblSdino")).Text.ToString();
            DataSet result = accData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_SAME_ARTICLE_INQUIRY_STATUS", article, sdino, "", "");
            if (result == null)
            {
                this.MakeNew.Visible = false;
                this.Divider.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong!!');", true);
            }
            else
            {
                this.LblAlrtMsg.Text = "Hey, <a href='#'>" + usrname + "</a><br>" + result.Tables[0].Rows[0]["msg"].ToString();
                if (Convert.ToBoolean(result.Tables[0].Rows[0]["instatus"]) == true && Convert.ToBoolean(result.Tables[0].Rows[0]["sdidup"]) == false)
                {
                    this.MakeNew.Checked = false;
                    this.MakeNew.Visible = true;
                    this.Divider.Visible = true;
                    this.lblbtnSave.Visible = true;
                }
                else if (Convert.ToBoolean(result.Tables[0].Rows[0]["sdidup"]) == false)
                {
                    this.MakeNew.Visible = false;
                    this.Divider.Visible = false;
                    this.lblbtnSave.Visible = true;

                }
                else
                {
                    this.MakeNew.Visible = false;
                    this.Divider.Visible = false;
                    this.lblbtnSave.Visible = false;
                    this.lblbtnSave.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "openModal();", true);

            }
        }

        protected void LbtnDuplication_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sdino = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("LblSdino")).Text.ToString();
            string buyerid = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("LblbUyer")).Text.ToString();

            string samptype = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("LblSampType")).Text.ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();

            string comcod = this.GetCompCode();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAMPLE_DEV_RERUN", sdino, buyerid, samptype, usrid, Posttrmid, PostSession, "", "", "");

            if (result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sample Duplicate Successfully');", true);
                this.SamplingInterFace();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went wrong!!');", true);

            }
        }


        protected void LbtnRetrace_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sdino = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("LblSdino")).Text.ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SET_SDI_RETRACING", sdino, "", "", "", "");

            if (result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sample Retrace Successfully');", true);
                this.SamplingInterFace();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went wrong!!');", true);

            }
        }



        private void GetProcess(string process)
        {
            Session.Remove("tblprocess");
            string comcod = GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("procode <>'" + process + "'");
            dt = dv.ToTable();

            this.ddlToProcess.DataTextField = "prodesc";
            this.ddlToProcess.DataValueField = "procode";
            this.ddlToProcess.DataSource = dt;
            this.ddlToProcess.DataBind();
            Session["tblprocess"] = ds1.Tables[0];

        }
        protected void LbtnStaus_Click(object sender, EventArgs e)
        {
            this.gvProductionlog.DataSource = null;
            this.gvProductionlog.DataBind();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sdino = ((Label)this.gvissued.Rows[index].FindControl("lblgvinqnoiss")).Text.ToString();
            string comcod = this.GetCompCode();
            this.LblSdino.Text = sdino;
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_SAMPLE_PRODUCTION_LOG", sdino, "", "", "");
            if (ds == null)
            {
                this.gvProductionlog.DataSource = null;
                this.gvProductionlog.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went wrong!!');", true);

            }
            ViewState["samprodlog"] = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.GetProcess(ds.Tables[0].Rows[0]["tprocode"].ToString());
                this.TxtFromProces.Text = ds.Tables[0].Rows[0]["tprodesc"].ToString();
                this.gvProductionlog.DataSource = ds.Tables[0];
                this.gvProductionlog.DataBind();
            }
            else
            {
                this.GetProcess("800100101099");
                this.TxtFromProces.Text = "Not Start yet";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "openProdModal();", true);

        }

        protected void LbtnUpdateProduction_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["samprodlog"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sdino = this.LblSdino.Text.Trim();
            string toprocess = this.ddlToProcess.SelectedValue.ToString();
            string fromprocess = (dt.Rows.Count == 0) ? "000000000000" : dt.Rows[0]["tprocode"].ToString();
            string notes = this.TxtNotes.Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "UPDATE_SAMPLE_PRODUCTION", sdino, fromprocess, toprocess, notes, usrid, "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went wrong!!');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }

        }


        protected void LbtnNewCodeUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string codetype = this.txtCodeType.Text.ToString() + "%";
            string codedesc = this.TxtNewGenCode.Text.Trim().ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAVE_NEW_GENCODE", codetype, codedesc);
            if (result)
            {
                this.TxtNewGenCode.Text = String.Empty;
                this.GetGenCode();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal();", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('New Code Saved Successfully');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

            }
        }


        protected void LbtnPdBookUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sdino = this.LblSdinoPdBook.Text.Trim();
            string designer = this.DdlPaterDesigner.SelectedValue.ToString();
            string pattloc = this.TxtPatLoc.Text.Trim();
            string pattgrad = this.DdlPatGrading.SelectedValue.Trim();
            string uppknif = this.DdlUppKnif.SelectedValue.Trim();
            string linknif = this.DdlLinKnif.SelectedValue.Trim();
            string bomknif = this.DdlBotmKniff.SelectedValue.Trim();
            string outsole = this.DdlOutsole.SelectedValue.Trim();
            string remarks = this.TxtRemarksPdBook.Text.Trim();
            string pptdate = this.TxtPPTDate.Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "UPDATE_PD_BOOK_INFO", sdino, designer, pattloc, pattgrad, uppknif, linknif, bomknif, outsole, remarks, pptdate);
            if (result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal();", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('PD Book Information Saved Successfully');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

            }
        }

        protected void btnDelPreCost_Click(object sender, EventArgs e)
        {
            string url = "SamConsumptionSheet?Type=Entry";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string inqno = ((Label)this.gvPreCost.Rows[index].FindControl("lblgvinqnocst")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "REV_ALL_SAMPLING_PROCESS", "PDG", inqno);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Reverse Not Sucessfully');", true);


                return;
            }
            this.SamplingInterFace();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Reverse Sucessfully');", true);


        }


        protected void LbtnForward_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string userrole = hst["userrole"].ToString();
                List<string> allowuserrole = new List<string> { "10", "12", "16", "97"};
                if(!allowuserrole.Contains(userrole))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You are not eligible.');", true);
                    return;
                }

                string usrid = hst["usrid"].ToString();
                string sessionid = hst["session"].ToString();
                string terminal = hst["compname"].ToString();
                string comcod = hst["comcod"].ToString();

                string destcomcod = (comcod == "5305") ? "5306" : "5305";
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string sdino = ((Label)this.gvProCom.Rows[index].FindControl("lblSdino")).Text.ToString();

                //SP_ENTRY_PROTO_SAMPLING '5305', 'SAMPLE_FORWARD_COMPANY', null, null, null, 'SDI20211100003','5305001','::1','985108', '5306'
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAMPLE_FORWARD_COMPANY", sdino, usrid, terminal, sessionid, destcomcod);
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Forward successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Forwared! Not eligible to multi time forward');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ex.Message+"');", true);

            }

        }

        protected void LbtnPDBook_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sdino = ((Label)this.gvpdguide.Rows[index].FindControl("lblgvinqnopg")).Text.ToString();
            this.LblSdinoPdBook.Text = sdino;
            string comcod = this.GetCompCode();
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_PD_BOOK_INFO", sdino, "", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                this.DdlPaterDesigner.SelectedValue = ds.Tables[0].Rows[0]["designer"].ToString();
                this.TxtPatLoc.Text = ds.Tables[0].Rows[0]["pattloc"].ToString();
                this.DdlUppKnif.SelectedValue = (ds.Tables[0].Rows[0]["uppknif"].ToString() == "True") ? "1" : "0";
                this.DdlLinKnif.SelectedValue = (ds.Tables[0].Rows[0]["linknif"].ToString() == "True") ? "1" : "0";
                this.DdlBotmKniff.SelectedValue = (ds.Tables[0].Rows[0]["botknif"].ToString() == "True") ? "1" : "0";
                this.DdlOutsole.SelectedValue = ds.Tables[0].Rows[0]["outsole"].ToString();
                this.DdlPatGrading.SelectedValue = (ds.Tables[0].Rows[0]["pattgrad"].ToString() == "True") ? "1" : "0";
                this.TxtRemarksPdBook.Text = ds.Tables[0].Rows[0]["pdboknotes"].ToString();
                this.TxtPPTDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["pptdate"]).Year.ToString() == "1900" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["pptdate"]).ToString("dd-MMM-yyyy");
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "PDBookModal();", true);
        }


        protected void modPdGuideBtnPDBook_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sdino = ((Label)this.gvpdguidapp.Rows[index].FindControl("lblgvinqnopdapp")).Text.ToString();
            this.LblSdinoPdBook.Text = sdino;
            string comcod = this.GetCompCode();
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_PD_BOOK_INFO", sdino, "", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                this.DdlPaterDesigner.SelectedValue = ds.Tables[0].Rows[0]["designer"].ToString();
                this.TxtPatLoc.Text = ds.Tables[0].Rows[0]["pattloc"].ToString();
                this.DdlUppKnif.SelectedValue = (ds.Tables[0].Rows[0]["uppknif"].ToString() == "True") ? "1" : "0";
                this.DdlLinKnif.SelectedValue = (ds.Tables[0].Rows[0]["linknif"].ToString() == "True") ? "1" : "0";
                this.DdlBotmKniff.SelectedValue = (ds.Tables[0].Rows[0]["botknif"].ToString() == "True") ? "1" : "0";
                this.DdlOutsole.SelectedValue = ds.Tables[0].Rows[0]["outsole"].ToString();
                this.DdlPatGrading.SelectedValue = (ds.Tables[0].Rows[0]["pattgrad"].ToString() == "True") ? "1" : "0";
                this.TxtRemarksPdBook.Text = ds.Tables[0].Rows[0]["pdboknotes"].ToString();
                this.TxtPPTDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["pptdate"]).Year.ToString() == "1900" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["pptdate"]).ToString("dd-MMM-yyyy");
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "PDBookModal();", true);
        }


        protected void gvPreCostPDBook_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtnSdino = (LinkButton)sender;
            string sdino = lnkbtnSdino.CommandArgument.ToString();
            this.LblSdinoPdBook.Text = sdino;
            string comcod = this.GetCompCode();
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_PD_BOOK_INFO", sdino, "", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                this.DdlPaterDesigner.SelectedValue = ds.Tables[0].Rows[0]["designer"].ToString();
                this.TxtPatLoc.Text = ds.Tables[0].Rows[0]["pattloc"].ToString();
                this.DdlUppKnif.SelectedValue = (ds.Tables[0].Rows[0]["uppknif"].ToString() == "True") ? "1" : "0";
                this.DdlLinKnif.SelectedValue = (ds.Tables[0].Rows[0]["linknif"].ToString() == "True") ? "1" : "0";
                this.DdlBotmKniff.SelectedValue = (ds.Tables[0].Rows[0]["botknif"].ToString() == "True") ? "1" : "0";
                this.DdlOutsole.SelectedValue = ds.Tables[0].Rows[0]["outsole"].ToString();
                this.DdlPatGrading.SelectedValue = (ds.Tables[0].Rows[0]["pattgrad"].ToString() == "True") ? "1" : "0";
                this.TxtRemarksPdBook.Text = ds.Tables[0].Rows[0]["pdboknotes"].ToString();
                this.TxtPPTDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["pptdate"]).Year.ToString() == "1900" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["pptdate"]).ToString("dd-MMM-yyyy");
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "PDBookModal();", true);
        }
    }
}