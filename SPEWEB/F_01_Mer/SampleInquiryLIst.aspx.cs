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
using System.IO;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;

namespace SPEWEB.F_01_Mer
{
    public partial class SampleInquiryLIst : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        UserManagerSampling objUserMan = new UserManagerSampling();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text =(type== "Summary") ? "Costing Summary Report" : "Sample Inquiry List";
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "SummaryForPD") ? "All Article Wise Report" : ((Label)this.Master.FindControl("lblTitle")).Text;
                this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtdateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                CommonButton();
                this.GetMasterLc();
                this.GetGenCode();
                this.Get_BuyerName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("href", ResolveUrl("~/F_01_Mer/SampleInquiry?Type=Entry&genno="));
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("target", "Blank");
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            if(this.Request.QueryString["Type"].ToString()== "Summary")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            }
        }

        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;

            var lstcat = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "11");
            lstcat.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstagent = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "32");
            lstagent.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstseason = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "33");
            lstseason.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstsamptype = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "34");
            lstsamptype.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            DdlCategory.DataTextField = "gdesc";
            DdlCategory.DataValueField = "gcod";
            DdlCategory.DataSource = lstcat;
            DdlCategory.DataBind();
            DdlCategory.SelectedValue = "00000";

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

            DdlAgent.DataTextField = "gdesc";
            DdlAgent.DataValueField = "gcod";
            DdlAgent.DataSource = lstagent;
            DdlAgent.DataBind();
            DdlAgent.SelectedValue = "00000";

            DdlSampType.DataTextField = "gdesc";
            DdlSampType.DataValueField = "gcod";
            DdlSampType.DataSource = lstsamptype;
            DdlSampType.DataBind();
            DdlSampType.SelectedValue = "00000";
        }
        private void Get_BuyerName()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            DdlCustomer.DataTextField = "sirdesc";
            DdlCustomer.DataValueField = "sircode";
            DdlCustomer.DataSource = ds2.Tables[0];
            DdlCustomer.DataBind();
            DdlCustomer.SelectedValue = "000000000000";
        }
        protected void gvMarchQutation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

               
                string userrole = hst["userrole"].ToString();
                //Label lblactcode = (Label)e.Row.FindControl("lblactcode");
                //HyperLink gvLink = (HyperLink)e.Row.FindControl("gvLink");

                //gvLink.NavigateUrl = "~/F_21_GAcc/LinkInflowAndOutflow.aspx?Type=MonReceipt&Date1=" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataTable dt = (DataTable)ViewState["tblsmpleinqlist"];
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                HyperLink lbtnCons = (HyperLink)e.Row.FindControl("lbtnCons");
                HyperLink lbtnCost = (HyperLink)e.Row.FindControl("lbtnCost");
                HyperLink lbtnOrder = (HyperLink)e.Row.FindControl("lbtnOrder");


                //HyperLink lnkELink = (HyperLink)e.Row.FindControl("lnkELink");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();
                string sdino = dt.Rows[e.Row.RowIndex]["sdino"].ToString();
                if (dt.Rows[e.Row.RowIndex]["isbom"].ToString()!="0" && userrole!= "97")
                {
                    lbtnCons.ToolTip = "BOM Already Done!";
                    lbtnCons.Enabled = false;
                    lbtnCons.ForeColor = System.Drawing.Color.Red ;
                    lbtnCost.ToolTip = "BOM Already Done!";
                    lbtnCons.Enabled = false;
                    lbtnCost.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbtnCons.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=Entry&actcode=" + inqno + "&genno=" + styleid;
                    lbtnCost.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=PreCosting&actcode=" + inqno + "&genno=" + styleid + "&sdino="+sdino;
                }
                lnkEdit.NavigateUrl = "~/F_01_Mer/SampleInquiry?Type=Edit&genno=" + inqno;

                if (this.GetCompCode() == "5305" || this.GetCompCode() == "5306")
                {
                    lnkEdit.Visible = false;
                    lbtnCons.Visible = false;
                    lbtnCost.NavigateUrl= "~/F_04_Sampling/DepartmentAlocation?Type=DeptAloc&actcode=" + inqno + "&genno=" + styleid + "&sdino=" + sdino;
                }
                
                lbtnOrder.NavigateUrl = "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Sample":
                    this.Multiview.ActiveViewIndex = 0;
                    this.GetSampleList();
                    break;
                case "Summary":
                    this.Multiview.ActiveViewIndex = 1;

                    this.GetCostingSummary();
                    break;
                case "SummaryForPD":
                    this.Multiview.ActiveViewIndex = 1;

                    this.GetCostingSummary();
                    break;

            }

            this.Data_Bind();

        }
        private void GetSampleList()
        {
            string comcod = this.GetCompCode();
            //string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtdateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string season = this.DdlSeason.SelectedValue == "00000" ? "%%" : this.DdlSeason.SelectedValue + "%";
            string agent = this.DdlAgent.SelectedValue == "00000" ? "%%" : this.DdlAgent.SelectedValue + "%";
            string customer = this.DdlCustomer.SelectedValue == "000000000000" ? "%%" : this.DdlCustomer.SelectedValue + "%";
            string category = this.DdlCategory.SelectedValue == "00000" ? "%%" : this.DdlCategory.SelectedValue + "%";
            string samptype = this.DdlSampType.SelectedValue == "00000" ? "%%" : this.DdlSampType.SelectedValue + "%";


            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ_LIST", season, agent, customer, category, samptype);
            int a= ds1.Tables[0].Rows.Count;
            ViewState["tblsmpleinqlist"] = ds1.Tables[0];
        }
        private void GetCostingSummary()
        {
            string comcod = this.GetCompCode();
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string season = (this.DdlSeason.Text.Trim()) == "00000" ? "%" : (this.DdlSeason.Text.Trim()+ "%");
            string agent = (this.DdlAgent.Text.Trim()) == "00000" ? "%" : (this.DdlAgent.Text.Trim()+ "%");
            string Customer = (this.DdlCustomer.Text.Trim()) == "000000000000" ? "%" : (this.DdlCustomer.Text.Trim()+ "%");
            string category = (this.DdlCategory.Text.Trim()) == "00000" ? "%" : (this.DdlCategory.Text.Trim()+ "%");
            string sampType = (this.DdlSampType.Text.Trim()) == "00000" ? "%" : (this.DdlSampType.Text.Trim()+ "%");
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SAMPLE_COSTING_SUMMARY", curdate, todate, season, agent, Customer, category, sampType);
            
            ViewState["tblsmpleinqlist"] = ds1.Tables[0];
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblsmpleinqlist"];
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
             case "Sample":
             this.gvSmpleinqlist.DataSource = dt;
             this.gvSmpleinqlist.DataBind();
                    break;
             case "Summary":
             this.gvCostingSummary.DataSource = dt;
             this.gvCostingSummary.DataBind();
                    break;
             case "SummaryForPD":
                this.gvCostingSummary.Columns[6].Visible = false;

                for(int i =11; i<29; i++)
                {
                    this.gvCostingSummary.Columns[i].Visible = false;
                }

                this.gvCostingSummary.DataSource = dt;
                this.gvCostingSummary.DataBind();
                break;
            }
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblsmpleinqlist"];
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Sample":
                    PrntSmpleinqlist();
                    break;
                case "Summary":
                    PrntCostingSummary();
                    break;
                case "SummaryForPD":
                    PrntCostingSummary();
                    break;
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
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqnum, CurDate1,
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
            rpt1.SetParameters(new ReportParameter("buyer", "Buyer: " + buyer));
            rpt1.SetParameters(new ReportParameter("date", "Date: " + CurDate1));
            rpt1.SetParameters(new ReportParameter("inqnum", "Inquery No: " + inqnum));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Sample Inquery Entry "));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        private void PrntSmpleinqlist()
        {

            DataTable dt = (DataTable)ViewState["tblsmpleinqlist"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text).ToString("dd-MMM-yyyy");
            string Datft = "Date: (From " + frmdate + " " + "To " + todate + ")";
            string month = System.DateTime.Today.ToString("MMMM-yyyy");
            var lst = new List<SPEENTITY.C_01_Mer.EClassSampleInq>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.EClassSampleInq();

                obj1.buyerid = dr1["buyerid"].ToString();
                obj1.inqno = dr1["inqno"].ToString();
                obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);
                obj1.userid = dr1["userid"].ToString();
                obj1.username = dr1["username"].ToString();
                obj1.buyerdesc = dr1["buyerdesc"].ToString();
                obj1.artno = dr1["artno"].ToString();
                obj1.catedesc = dr1["catedesc"].ToString();
                obj1.category = dr1["category"].ToString();
                obj1.comcod = dr1["comcod"].ToString();
                obj1.consize = dr1["consize"].ToString();
                obj1.samsize = dr1["samsize"].ToString();
                obj1.inqno = dr1["inqno"].ToString();
                obj1.inqno1 = dr1["inqno1"].ToString();
                obj1.inqno2 = dr1["inqno2"].ToString();
                obj1.itmcount = dr1["itmcount"].ToString();
                obj1.attchmnt = dr1["attchmnt"].ToString();
                obj1.sizernge = dr1["sizernge"].ToString();
                //string att = obj1.attchmnt;
                obj1.attchmnt = (dr1["attchmnt"].ToString().Length == 0 ? "" : new Uri(Server.MapPath(dr1["attchmnt"].ToString())).AbsoluteUri);


                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
                obj1.inqdat = Convert.ToDateTime(dr1["inqdat"]);

                lst.Add(obj1);
            }


            LocalReport rpt1 = new LocalReport();



            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptSampleInqTopSheet", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("date", Datft));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Sample Inquery List "));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrntCostingSummary()
        {

            DataTable dt = (DataTable)ViewState["tblsmpleinqlist"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text).ToString("dd-MMM-yyyy");
            string Datft = "Date: (From " + frmdate + " " + "To " + todate + ")";
            
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.EClassCostingSummary>();
            var lst = new List<SPEENTITY.C_01_Mer.EClassCostingSummary>();


            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.EClassCostingSummary();

                obj1.comcod = dr1["comcod"].ToString();
                obj1.inqno = dr1["inqno"].ToString();
                obj1.styleid = dr1["styleid"].ToString();
                obj1.curcod = dr1["curcod"].ToString();
                obj1.cnfrmprice = Convert.ToDouble(dr1["cnfrmprice"]);
                obj1.offprice = Convert.ToDouble(dr1["offprice"]);
                obj1.notes = dr1["notes"].ToString();
                obj1.uppercom = dr1["uppercom"].ToString();
                obj1.lining = dr1["lining"].ToString();
                obj1.socks = dr1["socks"].ToString();
                obj1.outsole = dr1["outsole"].ToString();
                obj1.stdamt = Convert.ToDouble(dr1["stdamtc"]);
                obj1.outsdning = Convert.ToDouble(dr1["outsdning"]);
                obj1.matcost = Convert.ToDouble(dr1["stdamte"]);
                obj1.article = dr1["article"].ToString();
                obj1.imgurl = dr1["imgurl"].ToString();
                obj1.agntcomm = Convert.ToDouble(dr1["agntcomm"]);
                obj1.dsigncomm = Convert.ToDouble(dr1["dsigncomm"]);
                obj1.moccasin = Convert.ToDouble(dr1["moccasin"]);
                obj1.noclaim = Convert.ToDouble(dr1["noclaim"]);
                obj1.testfee = Convert.ToDouble(dr1["testfee"]);
                obj1.locost = Convert.ToDouble(dr1["locost"]);
                obj1.devcost = Convert.ToDouble(dr1["devcost"]);
                obj1.moldcost = Convert.ToDouble(dr1["moldcost"]);
                obj1.netcost = Convert.ToDouble(dr1["netcost"]);
                obj1.ttlamt = Convert.ToDouble(dr1["ttlamt"]);
                obj1.ttlamteuro = Convert.ToDouble(dr1["ttlamteuro"]);
                obj1.profitloss = Convert.ToDouble(dr1["profitloss"]);
                obj1.prftlssprcnt = Convert.ToDouble(dr1["prftlssprcnt"]);
                obj1.othercomm = Convert.ToDouble(dr1["othercomm"]);
                obj1.llast = dr1["llast"].ToString();
                obj1.knife = dr1["knife"].ToString();

                string deliveryDate = Convert.ToDateTime(dr1["deliverydate"]).ToString("dd-MMM-yyyy");
                string sampleconfirm = Convert.ToDateTime(dr1["sampleconfirm"]).ToString("dd-MMM-yyyy");

                obj1.deliverydate = deliveryDate == "01-Jan-1900" ? "": deliveryDate;
                obj1.sampleconfirm = sampleconfirm == "01-Jan-1900" ? "" : sampleconfirm;

                obj1.imgurl = (dr1["imgurl"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["imgurl"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;

                lst.Add(obj1);
            }


            string type = this.Request.QueryString["Type"].ToString();

            LocalReport rpt1 = new LocalReport();

            if(type == "SummaryForPD")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptAllArtclSum", lst, null, null);
            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptCostingSummary", lst, null, null);
            }

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("date", Datft));
            rpt1.SetParameters(new ReportParameter("rpttitle", "All Article Wise Report"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void GetMasterLc()
        {

            string comcod = GetCompCode();
            string txtsrch = "%";
            //string CallType = (this.Request.QueryString["Type"].Trim() == "0") ? "LCList" : "DTLLCLIST";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "DTLLCLIST", "", txtsrch, "", "", "", "", "", "", ""); ;
            if (ds1 == null)
                return;
            DataRow dr = ds1.Tables[0].NewRow();
            dr["actdesc"] = "None";
            dr["actcode"] = "000000000000";
            ds1.Tables[0].Rows.Add(dr);
            ViewState["tblmasterlc"] = ds1.Tables[0];


        }

    }
}