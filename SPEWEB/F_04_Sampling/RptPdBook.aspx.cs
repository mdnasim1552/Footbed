using Microsoft.Reporting.WinForms;
using SPEENTITY;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPEENTITY.C_22_Sal;
using System.Web.Services;

namespace SPEWEB.F_04_Sampling
{
    public partial class RptPdBook : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        UserManagerSampling objUserMan = new UserManagerSampling();
        ProcessAccess purData = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
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
                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "PdBook" ? "PD Book Report" : type == "PdBookEntry" ? "PD Book Information Entry" : type == "KnChkList" ? "Knife Check List" : "Date Wise Sample Report");
                this.txtDatefrom.Text = DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtdateto.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetGenCode();
                this.CommonButton();

                if (type == "SamReport")
                {
                    this.datewise.Visible = true;
                    this.Getagent();
                    this.Get_BuyerName();
                    this.GetSampType();

                }
                if (type == "PdBook")
                {
                    this.divDdlSmpltype.Visible = false;
                    this.divSrcArt.Visible = false;
                }
                if (type == "PdBookEntry")
                {
                    this.divDdlSmpltype.Visible = false;
                    this.divDdlCategory.Visible = false;
                    this.divDdlShoetype.Visible = false;
                    this.divSrcArt.Visible = false;
                }
                if (type == "KnChkList")
                {
                    this.GetComponentList();
                    this.GetMaterialList();
                    this.GetSupplierName();
                    this.GetStore();
                    this.GetDeparment();
                    this.CurrencyInf();
                    this.GetPurType();

                    this.divDdlSmpltype.Visible = false;
                    this.divDdlCategory.Visible = false;
                    this.divDdlShoetype.Visible = false;
                    this.divSrcArt.Visible = false;
                }
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        public void CommonButton()
        {
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "PdBookEntry")
            {
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            }

            if (type == "KnChkList")
            {
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void Get_BuyerName()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            DdlCustomer.DataTextField = "sirdesc";
            DdlCustomer.DataValueField = "sircode";
            DdlCustomer.DataSource = ds2.Tables[0];
            DdlCustomer.DataBind();

            //DataView dv = ds2.Tables[0].DefaultView;
            //dv.RowFilter = "sircode not like '000000000000%'";

            //DataTable dt1 = dv.ToTable();
            //DataRow dr1 = dt1.NewRow();
            //dr1["sircode"] = "000000000000";
            //dr1["sirdesc"] = "--All--";
            //dt1.Rows.Add(dr1);

            //ddlbuyer.DataTextField = "sirdesc";
            //ddlbuyer.DataValueField = "sircode";
            //ddlbuyer.DataSource = dt1;
            //ddlbuyer.SelectedValue = "000000000000";
            //ddlbuyer.DataBind();

        }
        private void GetSampType()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;
            //  List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>)Session["lstgencode"];

            var lstsm = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "34");

            ddlSmpltype.DataTextField = "gdesc";
            ddlSmpltype.DataValueField = "gcod";
            ddlSmpltype.DataSource = lstsm;
            ddlSmpltype.DataBind();
            ddlSmpltype.Items.Add(new ListItem { Selected = true, Value = "00000", Text = "--All--" });

            DdlSamType.DataTextField = "gdesc";
            DdlSamType.DataValueField = "gcod";
            DdlSamType.DataSource = lstsm;
            DdlSamType.DataBind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "SamReport":
                    this.PrintSamReport();
                    break;
                case "KnChkList":
                    this.PrintKnifEntryReport();
                    break;
                default:
                    this.PrintPdBook();
                    break;
            }

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "SamReport":
                    this.SamReport();
                    break;
                case "KnChkList":
                    this.KnChkListInfoEntry();
                    break;
                case "PdBookEntry":
                    this.PdBookInfoEntry();
                    break;
                default:
                    this.PdBook();
                    break;
            }

        }



        //private void PdBookInfoEntry()
        //{
        //    string comcod = this.GetCompCode();
        //    string fdate = this.txtDatefrom.Text.Trim();
        //    string tdate = this.txtdateto.Text.Trim();

        //    //SP_REPORT_PROTOSAMPLING_INTERFACE '5305', 'GET_UNIQUE_ARTICLE_FOR_PDBOOKINF',null, null, null,'33008%','11001%','43001%'
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_UNIQUE_ARTICLE_FOR_PDBOOKINF", "%", "%", "%", fdate, tdate, "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {
        //        gvPdBookInfoEntry.DataSource = ds1.Tables[0];
        //        gvPdBookInfoEntry.DataBind();
        //    }
        //}


        private void PdBookInfoEntry()
        {
            string comcod = this.GetCompCode();
            string fdate = this.txtDatefrom.Text.Trim();
            string tdate = this.txtdateto.Text.Trim();
            string season = (this.DdlSeason.SelectedValue.Trim().ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.Trim().ToString() + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_UNIQUE_ARTICLE_FOR_PDBOOKINF", season, "%", "%", fdate, tdate, "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                gvPdBookInfoEntry.DataSource = ds1.Tables[0];
                gvPdBookInfoEntry.DataBind();
            }

        }


        //private void KnChkListInfoEntry()
        //{
        //    string comcod = this.GetCompCode();
        //    string fdate = this.txtDatefrom.Text.Trim();
        //    string tdate = this.txtdateto.Text.Trim();
        //    string season = (this.DdlSeason.SelectedValue.Trim().ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.Trim().ToString() + "%";


        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_UNIQUE_ARTICLE_FOR_PDBOOKINF", season, "%", "%", fdate, tdate, "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {
        //        gvKnChkListInfoEntry.DataSource = ds1.Tables[0];
        //        gvKnChkListInfoEntry.DataBind();
        //    }

        //}


        private void KnChkListInfoEntry()
        {
            string comcod = this.GetCompCode();
            string fdate = this.txtDatefrom.Text.Trim();
            string tdate = this.txtdateto.Text.Trim();
            string season = (this.DdlSeason.SelectedValue.Trim().ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.Trim().ToString() + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_UNIQUE_ARTICLE_FOR_PDBOOKINF", season, "%", "%", fdate, tdate, "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count > 0)
            {
                gvKnChkListInfoEntry.DataSource = ds1.Tables[0];
                gvKnChkListInfoEntry.DataBind();

                foreach (GridViewRow row in gvKnChkListInfoEntry.Rows)
                {
                    DataRow dr = ds1.Tables[0].Rows[row.RowIndex];
                    int knifitemsValue;

                    if (int.TryParse(dr["knifitems"].ToString(), out knifitemsValue))
                    {
                        LinkButton btnImport = (LinkButton)row.FindControl("btnImport");
                        if (knifitemsValue == 0)
                        {
                            btnImport.Visible = true;
                        }
                        else
                        {
                            btnImport.Visible = false;
                        }
                    }
                }
            }
        }



        public List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> GetGenCode(string comcod)
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> lst = new List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>();

            SqlDataReader dr = purData.GetSqlReader(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETGENERALCODE", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode objgencode = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode(dr["gcod"].ToString(), dr["gdesc"].ToString());
                lst.Add(objgencode);
            }

            return lst;
        }

        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;

            var lstcat = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "11");
            lstcat.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lststype = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "43");
            lststype.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstseason = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "33");
            lstseason.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "All"));



            DdlCategory.DataTextField = "gdesc";
            DdlCategory.DataValueField = "gcod";
            DdlCategory.DataSource = lstcat;
            DdlCategory.DataBind();
            DdlCategory.SelectedValue = "00000";


            DdlShoType.DataTextField = "gdesc";
            DdlShoType.DataValueField = "gcod";
            DdlShoType.DataSource = lststype;
            DdlShoType.DataBind();
            DdlShoType.SelectedValue = "00000";


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


        protected void PdBook()
        {
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string Season = (this.DdlSeason.SelectedValue.Trim().ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.Trim().ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_PD_BOOK_REPORT", fromdate, todate, Season, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.gvpdbook.DataSource = ds1.Tables[0];
                this.gvpdbook.DataBind();
                Session["tblPdBook"] = ds1.Tables[0];
            }
        }


        protected void SamReport()
        {
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string Season = (this.DdlSeason.SelectedValue.Trim().ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.Trim().ToString() + "%";
            string smplType = (this.ddlSmpltype.SelectedValue.Trim().ToString() == "00000") ? "%" : this.ddlSmpltype.SelectedValue.Trim().ToString() + "%";
            string Agent = (this.DdlAgent.SelectedValue.Trim().ToString() == "00000") ? "%" : this.DdlAgent.SelectedValue.Trim().ToString() + "%";
            string Buyer = (this.ddlBuyer.SelectedValue.Trim().ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.Trim().ToString() + "%";
            string srcArt = this.txtSrcArt.Text.Length > 0 ? "%" + this.txtSrcArt.Text.Trim() + "%" : "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_DATE_WISE_SAMPLE_INFO", fromdate, todate, Season, Agent, Buyer, srcArt, smplType, "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.pnlProReqNote.Visible = true;

                this.gvSamReport.DataSource = ds1.Tables[0];
                this.gvSamReport.DataBind();
                Session["tblPdBook"] = ds1.Tables[0];
            }
            else
            {
                this.gvSamReport.DataSource = null;
                this.gvSamReport.DataBind();
                Session["tblPdBook"] = null;
            }
            txtSrcArt.Text = "";
        }


        protected void DdlAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBuyer();
        }


        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            string agent = (this.DdlAgent.SelectedValue.ToString() == "00000") ? "%" : this.DdlAgent.SelectedValue.ToString() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_AGENTWISE_BUYER_LIST", agent, "", "", "", "", "",
                "", "", "");


            DataView dv21 = ds2.Tables[0].DefaultView;
            DataRowView newRow = dv21.AddNew();
            DataView dv22 = new DataView(ds2.Tables[0]);
            dv22.RowFilter = ("sircode not like '000000000000'");


            newRow = dv22.AddNew();
            newRow["sircode"] = "000000000000";
            newRow["sirdesc"] = "----All----";
            dv22.ToTable().Rows.Add(newRow);



            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = dv22;
            this.ddlBuyer.DataBind();
            this.ddlBuyer.SelectedValue = "000000000000";
        }


        private void Getagent()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;
            var lstagent = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "32");
            lstagent.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            DdlAgent.DataTextField = "gdesc";
            DdlAgent.DataValueField = "gcod";
            DdlAgent.DataSource = lstagent;
            DdlAgent.DataBind();
            this.DdlAgent.SelectedValue = "00000";
            DdlAgent_SelectedIndexChanged(null, null);
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void PrintPdBook()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string Season = this.DdlSeason.SelectedItem.ToString();

            DataTable dt = (DataTable)Session["tblPdBook"];
            var lst = dt.DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassPdBook>();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_04_Samp.RptPdBook", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "PD Book Report"));
            Rpt1.SetParameters(new ReportParameter("Season", Season));
            Rpt1.SetParameters(new ReportParameter("date", fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintKnifEntryReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblKnifEntry"];
            var lst = dt.DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassKnifEntry>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_04_Samp.RptKnifEntry", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Knife Check List"));
            Rpt1.SetParameters(new ReportParameter("DptTitle", "PD Department"));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintSamReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string Season = this.DdlSeason.SelectedItem.ToString();
            string Agent = this.DdlAgent.SelectedItem.ToString();
            string Buyer = (this.ddlBuyer.SelectedValue.Trim().ToString() == "000000000000") ? "ALL" : this.ddlBuyer.SelectedItem.ToString();

            DataTable dt = (DataTable)Session["tblPdBook"];
            var lst = dt.DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassSamReport>();

            foreach (var item in lst)
            {
                item.images = item.images.Length > 0 ? new Uri(Server.MapPath(item.images.ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_04_Samp.RptSamReport", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "PD Book Report"));
            Rpt1.SetParameters(new ReportParameter("Season", Season));
            Rpt1.SetParameters(new ReportParameter("Agent", Agent));
            Rpt1.SetParameters(new ReportParameter("Buyer", Buyer));
            Rpt1.SetParameters(new ReportParameter("date", fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvPdBookInfoEntry_RowEditing(object sender, GridViewEditEventArgs e)
        {


            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;

            var lstpatterdesigner = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "36");
            lstpatterdesigner.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            this.gvPdBookInfoEntry.EditIndex = e.NewEditIndex;
            PdBookInfoEntry();

            DropDownList ddlgvPDIEdesigner = (DropDownList)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("ddlgvPDIEdesigner");
            ddlgvPDIEdesigner.DataTextField = "gdesc";
            ddlgvPDIEdesigner.DataValueField = "gcod";
            ddlgvPDIEdesigner.DataSource = lstpatterdesigner;
            ddlgvPDIEdesigner.DataBind();
            ddlgvPDIEdesigner.SelectedValue = ((Label)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("lblforddlgvPDIEdesigner")).Text;
            //ddlgvPDIEdesigner.SelectedValue = ((Label)e.Row.FindControl("lblforddlgvPDIEdesigner")).Text;



            DropDownList ddlgvPDIEshowboard = (DropDownList)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("ddlgvPDIEshowboard");
            ddlgvPDIEshowboard.Items.Insert(0, new ListItem("True", "true"));
            ddlgvPDIEshowboard.Items.Insert(1, new ListItem("False", "false"));
            ddlgvPDIEshowboard.DataBind();
            string res = ((Label)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("forddlgvPDIEshowboard")).Text.ToLower();
            ddlgvPDIEshowboard.SelectedValue = ((Label)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("forddlgvPDIEshowboard")).Text.ToLower();

            DropDownList ddlgvPDIEmarpatern = (DropDownList)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("ddlgvPDIEmarpatern");
            ddlgvPDIEmarpatern.Items.Insert(0, new ListItem("True", "true"));
            ddlgvPDIEmarpatern.Items.Insert(1, new ListItem("False", "false"));
            ddlgvPDIEmarpatern.DataBind();
            string re = ((Label)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("forddlgvPDIEmarpatern")).Text.ToLower();
            ddlgvPDIEmarpatern.SelectedValue = ((Label)this.gvPdBookInfoEntry.Rows[e.NewEditIndex].FindControl("forddlgvPDIEmarpatern")).Text.ToLower();


        }

        protected void gvPdBookInfoEntry_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = this.GetCompCode();
            string article = ((Label)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("lbleditgvPDIEdesigner")).Text.Trim();
            string designer = ((DropDownList)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("ddlgvPDIEdesigner")).SelectedValue.Trim();
            string outsole = ((TextBox)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("txtgvPDIEoutsole")).Text.Trim();
            string pdboknotes = ((TextBox)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("txtgvPDIEpdboknotes")).Text.Trim();
            string knif = ((TextBox)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("txtgvPDIEknif")).Text.Trim();
            string pptdate = ((TextBox)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("txtgvPDIEpptdate")).Text.Trim();
            string pattgrad = ((TextBox)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("txtgvPDIEpattgrad")).Text.Trim();
            string showboard = ((DropDownList)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("ddlgvPDIEshowboard")).SelectedValue.Trim();
            string marpatern = ((DropDownList)this.gvPdBookInfoEntry.Rows[e.RowIndex].FindControl("ddlgvPDIEmarpatern")).SelectedValue.Trim();


            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "UPDATE_ARTICLE_FOR_PDBOOKINF", article, designer, outsole, pdboknotes, knif, pptdate, pattgrad, showboard, marpatern);

            if (result)
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

            this.gvPdBookInfoEntry.EditIndex = -1;
            this.PdBookInfoEntry();
        }

        protected void gvPdBookInfoEntry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvPdBookInfoEntry.EditIndex = -1;

        }

        protected void gvPdBookInfoEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //Session.Remove("lstgencode");
            //string comcod = this.GetCompCode();
            //var lst = objUserMan.GetGenCode(comcod);
            //Session["lstgencode"] = lst;

            //var lstpatterdesigner = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "36");
            //lstpatterdesigner.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            //    {
            //        DropDownList ddlgvPDIEdesigner = (DropDownList)e.Row.FindControl("ddlgvPDIEdesigner");
            //        ddlgvPDIEdesigner.DataTextField = "gdesc";
            //        ddlgvPDIEdesigner.DataValueField = "gcod";
            //        ddlgvPDIEdesigner.DataSource = lstpatterdesigner;
            //        ddlgvPDIEdesigner.DataBind();
            //        ddlgvPDIEdesigner.SelectedValue = ((Label)e.Row.FindControl("lblforddlgvPDIEdesigner")).Text;

            //        DropDownList ddlgvPDIEshowboard = (DropDownList)e.Row.FindControl("ddlgvPDIEshowboard");
            //        ddlgvPDIEshowboard.Items.Insert(0, new ListItem("True", "true"));
            //        ddlgvPDIEshowboard.Items.Insert(1, new ListItem("False", "false"));
            //        ddlgvPDIEshowboard.DataBind();
            //        ddlgvPDIEshowboard.SelectedValue = ((Label)e.Row.FindControl("forddlgvPDIEshowboard")).Text.ToLower();

            //        DropDownList ddlgvPDIEmarpatern = (DropDownList)e.Row.FindControl("ddlgvPDIEmarpatern");
            //        ddlgvPDIEmarpatern.Items.Insert(0, new ListItem("True", "true"));
            //        ddlgvPDIEmarpatern.Items.Insert(1, new ListItem("False", "false"));
            //        ddlgvPDIEmarpatern.DataBind();
            //        ddlgvPDIEmarpatern.SelectedValue = ((Label)e.Row.FindControl("forddlgvPDIEmarpatern")).Text.ToLower();

            //    }
            //}
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            this.SamReport();
        }

        protected void gvSamReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");
                //HyperLink HyOrderPrint = (HyperLink)e.Row.FindControl("HyOrderPrint");
                HyperLink HyPdPrint = (HyperLink)e.Row.FindControl("HypPdGuidPrint");
                HyperLink hlnkCnsumpSheet = (HyperLink)e.Row.FindControl("hlnkCnsumpSheet");
                string proreq = ((Label)e.Row.FindControl("lblgvsrProReq")).Text;

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string samptype = ((Label)e.Row.FindControl("lblgvsrSampType")).Text.Trim();

                string ordusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "iscomplite"));
                string transstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "transstatus"));

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

                //HyPdPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PreCostPrint&genno=" + inqno + "&printtype=" + printType;
                HyPdPrint.NavigateUrl = "~/F_04_Sampling/Print?Type=PGEntry&genno=" + inqno + "&printtype=" + printType;

                string approved = ((Label)e.Row.FindControl("lblgvsrApprv")).Text.Trim();
                if (approved == "Ok")
                {
                    hlnkCnsumpSheet.Visible = true;
                    hlnkCnsumpSheet.Visible = true;
                    hlnkCnsumpSheet.NavigateUrl = "~/F_04_Sampling/SamConsumptionSheet?Type=PreCosting&genno=" + inqno;
                }

                if (samptype != "34005")
                {
                    ((LinkButton)e.Row.FindControl("LbtnFinal")).Visible = false;
                }

                if (proreq == "True")
                {
                    e.Row.BackColor = System.Drawing.Color.MediumSpringGreen;
                }
            }
        }

        protected void CbReqOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (CbReqOnly.Checked)
            {
                DataTable dt = ((DataTable)Session["tblPdBook"]).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = "proreq='true'";
                this.gvSamReport.DataSource = dv.ToTable();
                this.gvSamReport.DataBind();

            }
            else
            {
                this.gvSamReport.DataSource = (DataTable)Session["tblPdBook"];
                this.gvSamReport.DataBind();
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
            string article = ((Label)this.gvSamReport.Rows[index].FindControl("lblgvSizeRange")).Text.ToString();
            ModalSdino.Text = ((Label)this.gvSamReport.Rows[index].FindControl("lblgvSeason")).Text.ToString();
            string sdino = ((Label)this.gvSamReport.Rows[index].FindControl("lblgvSeason")).Text.ToString();
            DataSet result = purData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_SAME_ARTICLE_INQUIRY_STATUS", article, sdino, "", "");
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

        protected void LbtnForward_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string userrole = hst["userrole"].ToString();
                List<string> allowuserrole = new List<string> { "10", "12", "16", "97" };
                if (!allowuserrole.Contains(userrole))
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
                string sdino = ((Label)this.gvSamReport.Rows[index].FindControl("lblgvSeason")).Text.ToString();

                //SP_ENTRY_PROTO_SAMPLING '5305', 'SAMPLE_FORWARD_COMPANY', null, null, null, 'SDI20211100003','5305001','::1','985108', '5306'
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAMPLE_FORWARD_COMPANY", sdino, usrid, terminal, sessionid, destcomcod);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

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
            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "SAMPLE_FINAL_SUBMIT", sdino, usrid, Posttrmid, PostSession, makenew, "", "");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sample Final Submit Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went wrong!!');", true);

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
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAMPLE_DEV_RERUN", sdino, buyerid, samptype, usrid, Posttrmid, PostSession, "", "", "");

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sample Inquery Re-run Successfully');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal( );", true);

            }
        }

        protected void lnkbtnWHReq_Click(object sender, EventArgs e)
        {
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string comcod = this.GetCompCode();
            string sdino = this.txtbxSdiNo.Text.Trim();
            string reqQty = this.txtProReqQty.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "MAKE_WH_REQUISITION", sdino, usrid, reqQty, "", "");

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal( );", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
            }

        }


        protected void lnkbtnPrintCombined_Click(object sender, EventArgs e)
        {

            string sdino = "";

            for (int i = 0; i < this.gvSamReport.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvSamReport.Rows[i].FindControl("chkPrintCombined");

                if (chkbox.Checked)
                {
                    string sdino2 = ((Label)this.gvSamReport.Rows[i].FindControl("lblgvinqno")).Text;

                    if (!sdino.Contains(sdino2))
                    {
                        sdino += sdino2;
                    }
                }
            }


            string printFormat = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;
            string url = ResolveClientUrl("~//F_15_Pro/Print.aspx?Type=SamProdReqMulti&genno=" + sdino + "&printfrmt=" + printFormat);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);

            //string comcod = this.GetCompCode();
            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "SAMPLING_MAT_REQUISITION_ITEMS", sdino, "", "");

            //if (ds1 == null) return;
            //if (ds1.Tables[0].Rows.Count == 0 || ds1.Tables[1].Rows.Count == 0) return;

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printfrmt = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;

            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string session = hst["session"].ToString();
            //string footer = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //var rptlist = ds1.Tables[0].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.RptSamProdReq1>();
            //var rptlist2 = ds1.Tables[1].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.RptSamProdReq2>();


            //string sdinoFormatted = "";
            //while (true)
            //{
            //    string reqnoFrmated = sdino.Substring(0, 14);
            //    reqnoFrmated = reqnoFrmated.Substring(0, 3) + reqnoFrmated.Substring(7, 2) + "-" + reqnoFrmated.Substring(9, 5);

            //    sdinoFormatted += reqnoFrmated + ", ";
            //    sdino = sdino.Substring(14);

            //    if (sdino.Length == 0) break;
            //}

            //sdinoFormatted = sdinoFormatted.Trim().Substring(0, sdinoFormatted.Length - 2);

            //LocalReport Rpt1a = new LocalReport();

            //Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_04_Samp.RptSampleProdRequisition", rptlist, rptlist2, null);
            //Rpt1a.EnableExternalImages = true;
            //Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1a.SetParameters(new ReportParameter("rptTitle", "Sample Production Requisition"));
            //Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1a.SetParameters(new ReportParameter("sdino", sdinoFormatted));
            //Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //Session["Report1"] = Rpt1a;

            //string url = "../RDLCViewerWin.aspx?PrintOpt=" + printfrmt;

            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);

        }

        protected void LbtnIssueMulti_Click(object sender, EventArgs e)
        {
            string order = "";
            string inqno = "";
            string inqdat = "";

            for (int i = 0; i < this.gvSamReport.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvSamReport.Rows[i].FindControl("chkPrintCombined");

                if (chkbox.Checked)
                {
                    string batchcode = ((Label)this.gvSamReport.Rows[i].FindControl("lblBatchNo")).Text;
                    string order2 = batchcode;

                    inqdat = ((Label)this.gvSamReport.Rows[i].FindControl("lblgvartno")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 12)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    inqno += ((Label)this.gvSamReport.Rows[i].FindControl("lblgvinqno")).Text;

                }
            }

            if (inqno.Length <= 1402)
            {
                string url = ResolveClientUrl("~/F_11_RawInv/SamplingMatIssue?Type=Entry&genno=" + inqno + "&actcode=" + order + "&reptype=SAMPLING");
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You can select maximum 100 INQs');", true);
                return;
            }
        }



        private void ModalDataBind()
        {
            var sizerange = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EclassSizeDetails>)ViewState["tbladdsize"];
            this.gvSize.DataSource = sizerange;
            this.gvSize.DataBind();
        }

        protected void btnSize_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            LinkButton btnSize = (LinkButton)sender;

            GridViewRow row = (GridViewRow)btnSize.NamingContainer;

            Label lblArticle = row.FindControl("lblgvPDIEarticle") as Label;

            if (lblArticle != null)
            {
                this.LblKnifeEntryTxt.Text = lblArticle.Text;

                ViewState["SelectedArticle"] = lblArticle.Text;
            }

            int index = row.RowIndex;
            string article = ((Label)gvKnChkListInfoEntry.Rows[index].FindControl("lblgvPDIEarticle")).Text.ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GET_SIZE_ADD_SIZE", article, "", "", "", "", "", "", "", "");
            ViewState["tbladdsize"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EclassSizeDetails>();

            this.ModalDataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal2();", true);
        }

        private void Modal_Save_Value()
        {
            var size = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EclassSizeDetails>)ViewState["tbladdsize"];
            for (int i = 0; i < this.gvSize.Rows.Count; i++)
            {
                string sizedesc = ((TextBox)gvSize.Rows[i].FindControl("txtgvSizeDesc")).Text.ToString();
                string sizeselect = (((CheckBox)gvSize.Rows[i].FindControl("gvChkSize1")).Checked == true) ? "Y" : "n";
                size[i].sizedesc = sizedesc;
                size[i].sizeselect = sizeselect;
            }
            ViewState["tbladdsize"] = size;
        }


        protected void btnKnifeEntry_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();

                LinkButton btnKnifeEntry = (LinkButton)sender;

                GridViewRow row = (GridViewRow)btnKnifeEntry.NamingContainer;

                Label lblArticle = row.FindControl("lblgvPDIEarticle") as Label;

                if (lblArticle != null)
                {
                    this.LblKnifeEntryTxt.Text = lblArticle.Text;
                    ViewState["SArticle"] = lblArticle.Text;
                }

                int index = row.RowIndex;
                string article = ((Label)gvKnChkListInfoEntry.Rows[index].FindControl("lblgvPDIEarticle")).Text.ToString();

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GET_KNIFE_ENTRY", article, "", "", "", "", "", "");

                if (ds1 == null)
                    return;

                ViewState["tblKnifEntry"] = ds1.Tables[0];

                this.Data_Bind();

                //LinkButton btnImport = (LinkButton)row.FindControl("btnImport");
                //btnImport.Visible = ds1.Tables[0].Rows.Count == 0;

                this.ReqBtn.Visible = true;

                this.PanelKnifeEntry.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblKnifEntry"];

            DataTable copyDt = dt.Copy();

            this.gvknifentry.DataSource = dt;
            this.gvknifentry.DataBind();
            if (dt.Rows.Count == 0)
                return;

            ViewState["tblKnifEntry"] = dt;

            ViewState["tblCopyKnifEntry"] = copyDt;
        }


        protected void AddSizeButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Modal_Save_Value();

                string comcod = this.GetCompCode();

                string selectedArticle = ViewState["SelectedArticle"] as string;


                string article = selectedArticle;

                var size = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EclassSizeDetails>)ViewState["tbladdsize"];
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(ASITUtility03.ListToDataTable(size.FindAll(x => x.sizeselect == "Y").ToList()));
                ds.Tables[0].TableName = "tbladdsize";

                bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "UPDATE_SIZEADD", ds, null, null, article, "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please save this article in PD Book Information first in-order to add size');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        private void GetComponentList()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_INV_STDANA", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            ddlComponent.DataTextField = "resdesc";
            ddlComponent.DataValueField = "rescode";
            ddlComponent.DataSource = ds2.Tables[0];
            ddlComponent.DataBind();
        }

        private void GetMaterialList()
        {
            string comcod = this.GetCompCode();
            string srchinf = "220100107%";

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ONLY_MATERIAL_LIST", srchinf, "", "", "", "", "", "", "", "");

            this.DdlMatecode.DataSource = ds.Tables[0];
            this.DdlMatecode.DataTextField = "sirdesc";
            this.DdlMatecode.DataValueField = "sircode";
            this.DdlMatecode.DataBind();

            this.DdlMatecode_SelectedIndexChanged(null, null);
        }

        protected void DdlMatecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sircod = this.DdlMatecode.SelectedValue.ToString() + "%";

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION", sircod, "", "", "", "", "", "", "", "");

            this.DdlSpcfcod.DataTextField = "spcfdesc";
            this.DdlSpcfcod.DataValueField = "spcfcod";
            this.DdlSpcfcod.DataSource = ds.Tables[0];
            this.DdlSpcfcod.DataBind();
        }

        protected void lnkAddKnifeEntry_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();

                this.Session_tblKnifEntry_Update();

                DataTable tbl = (DataTable)ViewState["tblKnifEntry"];

                string article = this.LblKnifeEntryTxt.Text;
                string compcode = this.ddlComponent.SelectedValue.ToString();
                string compname = this.ddlComponent.SelectedItem.ToString();
                string rsircode = this.DdlMatecode.SelectedValue.ToString() == "" ? "000000000000" : this.DdlMatecode.SelectedValue.ToString();
                string rsirdesc = (rsircode == "000000000000") ? "" : this.DdlMatecode.SelectedItem.ToString();
                string spcfcod = this.DdlSpcfcod.SelectedValue.ToString() == "" ? "000000000000" : this.DdlSpcfcod.SelectedValue.ToString();
                string spcfdesc = (spcfcod == "000000000000") ? "" : this.DdlSpcfcod.SelectedItem.ToString();

                DataRow[] dr = tbl.Select("compcode='" + compcode + "' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "'");

                if (dr.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Selected Component already added');", true);
                    return;
                }
                else
                {
                    DataRow dr1 = tbl.NewRow();
                    dr1["comcod"] = comcod;
                    dr1["article"] = article;
                    dr1["compcode"] = compcode;
                    dr1["compname"] = compname;
                    dr1["rsircode"] = rsircode;
                    dr1["rsirdesc"] = rsirdesc;
                    dr1["spcfcod"] = spcfcod;
                    dr1["spcfdesc"] = spcfdesc;
                    dr1["qty"] = 0;
                    dr1["conqty"] = 0;
                    dr1["inch"] = 0;
                    dr1["remarks"] = "";
                    tbl.Rows.Add(dr1);
                }

                ViewState["tblKnifEntry"] = tbl;
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        protected void LbtnComponent_Click(object sender, EventArgs e)
        {
            this.GetComponentList();
        }

        protected void LbtnMaterial_Click(object sender, EventArgs e)
        {
            this.GetMaterialList();
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            this.Session_tblKnifEntry_Update();

            DataTable tbl1 = (DataTable)ViewState["tblKnifEntry"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mArticle = tbl1.Rows[i]["article"].ToString();
                string mCompcode = tbl1.Rows[i]["compcode"].ToString();
                string mRsircode = tbl1.Rows[i]["rsircode"].ToString();
                string mSpcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                string mQty = tbl1.Rows[i]["qty"].ToString();
                string mConqty = tbl1.Rows[i]["conqty"].ToString();
                string mInch = tbl1.Rows[i]["inch"].ToString();
                string mRemarks = tbl1.Rows[i]["remarks"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "UPDATE_KNIFE_ENTRY",
                              mArticle, mCompcode, mRsircode, mSpcfcod, mQty, mConqty, mInch, mRemarks, "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update failed');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated successfully');", true);
                }


                if (result == true && ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Knife Check List";
                    string eventdesc = "Knife Entry";
                    string eventdesc2 = "Knife Entry for Article: " + mArticle + ", Component: " + mCompcode + " Material: " + mRsircode + ", Specification: " + mSpcfcod;

                    bool IsArticleSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
        }

        private void Session_tblKnifEntry_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblKnifEntry"];
            int Index;

            for (int j = 0; j < this.gvknifentry.Rows.Count; j++)
            {
                double qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvknifentry.Rows[j].FindControl("txtgvQty")).Text.Trim()));
                double conqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvknifentry.Rows[j].FindControl("txtgvConQty")).Text.Trim()));
                double inch;

                if (conqty != null && conqty != 0)
                {
                    inch = qty * conqty;
                }
                else
                {
                    inch = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvknifentry.Rows[j].FindControl("txtgvInch")).Text.Trim()));
                }

                Index = (this.gvknifentry.PageIndex) * this.gvknifentry.PageSize + j;

                tbl1.Rows[Index]["qty"] = qty;
                tbl1.Rows[Index]["conqty"] = conqty;
                tbl1.Rows[Index]["inch"] = inch;

                string remarks = ((TextBox)this.gvknifentry.Rows[j].FindControl("lblgvRmks")).Text.Trim();
                tbl1.Rows[Index]["remarks"] = remarks;
            }

            ViewState["tblKnifEntry"] = tbl1;
        }


        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Session_tblKnifEntry_Update();
            this.Data_Bind();
        }

        protected void gvknifentry_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();

            DataTable dt = (DataTable)ViewState["tblKnifEntry"];

            string selectedArticle = ViewState["SArticle"] as string;
            string article = selectedArticle;

            string compcode = ((Label)this.gvknifentry.Rows[e.RowIndex].FindControl("lblgvComponentCode")).Text.Trim();
            string rsircode = ((Label)this.gvknifentry.Rows[e.RowIndex].FindControl("lblgvMaterialCode")).Text.Trim();
            string spcfcod = ((Label)this.gvknifentry.Rows[e.RowIndex].FindControl("lblgvSpecfCode")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "DELETE_KNIFE_ENTRY", article, compcode, rsircode, spcfcod, "", "", "", "", "", "", "", "");

            if (!result)
                return;

            int index = (this.gvknifentry.PageIndex) * this.gvknifentry.PageSize + e.RowIndex;
            dt.Rows[index].Delete();

            DataView dv = dt.DefaultView;
            ViewState["tblKnifEntry"] = dv.ToTable();

            this.Data_Bind();
        }

        private void GetSupplierName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "GETPSNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "sirdesc";
            this.ddlSupplier.DataValueField = "sircode";
            this.ddlSupplier.DataSource = ds1.Tables[0];
            this.ddlSupplier.DataBind();

        }


        protected void GetStore()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string fxtast = "FxtAst";
            string Aproval = "";
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string ReFindProject = "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.DDlStore.DataTextField = "actdesc1";
            this.DDlStore.DataValueField = "actcode";
            this.DDlStore.DataSource = ds2.Tables[0];
            this.DDlStore.DataBind();
            ViewState["tblStoreType"] = ds2.Tables[0];


        }

        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].Rows.Add(comcod, "000000000000", "Department");
            ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");


            this.ddlDeptCode.DataTextField = "fxtgdesc";
            this.ddlDeptCode.DataValueField = "fxtgcod";
            this.ddlDeptCode.DataSource = ds1.Tables[0];
            this.ddlDeptCode.DataBind();
            this.ddlDeptCode.SelectedValue = "AAAAAAAAAAAA";


        }

        private void GetPurType()
        {
            string comcod = this.GetCompCode();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURTYPE", "");
            if (ds == null)
                return;



            ddlPurType.DataSource = ds.Tables[0];
            ddlPurType.DataTextField = "gdesc";
            ddlPurType.DataValueField = "gcod";
            ddlPurType.DataBind();

            this.ddlPurType.SelectedItem.Text = "----Select----";

            //if (comcod == "5305")
            //{
            //    this.ddlPurType.SelectedValue = "25001";
            //}

        }

        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;
            this.ddlCurrency.DataValueField = "curcode";
            this.ddlCurrency.DataTextField = "curdesc";
            this.ddlCurrency.DataSource = lstCurryDesc;
            this.ddlCurrency.DataBind();

            if (this.Request.QueryString["InputType"] == "FxtAstEntry")
            {
                this.ddlCurrency.SelectedValue = "001";
                this.ddlCurrency.Enabled = false;
            }



            this.ddlCurrency_SelectedIndexChanged(null, null);
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");

        }

        [WebMethod(EnableSession = false)]
        public static string GetCurRate(string curcode)
        {
            try
            {
                string fcode = "001";
                string tcode = curcode;
                SalesInvoice_BL lst1 = new SalesInvoice_BL();
                DataSet ds = lst1.Curreny();
                var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();


                double method = lstConv.FindAll(p => p.fcode == fcode && p.tcode == tcode).ToList()[0].conrate;


                return Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        protected void LbtnCreateReq_Click(object sender, EventArgs e)
        {


            ((CheckBox)this.gvknifentry.HeaderRow.FindControl("chkhead")).Checked = false;
            this.chkheadl_CheckedChanged(null, null);
        }

        protected void chkheadl_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvknifentry.Rows.Count; i++)
            {
                if (((CheckBox)this.gvknifentry.HeaderRow.FindControl("chkhead")).Checked)
                {
                    ((CheckBox)this.gvknifentry.Rows[i].FindControl("chkCol")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.gvknifentry.Rows[i].FindControl("chkCol")).Checked = false;
                }
            }

        }


        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();

                LinkButton btnImport = (LinkButton)sender;

                GridViewRow row = (GridViewRow)btnImport.NamingContainer;

                Label lblArticle = row.FindControl("lblgvPDIEarticle") as Label;

                if (lblArticle != null)
                {
                    this.LblKnifeEntryTxt.Text = lblArticle.Text;
                    ViewState["SArticle"] = lblArticle.Text;


                    DataTable copyDt = ViewState["tblCopyKnifEntry"] as DataTable;

                    if (copyDt != null)
                    {

                        foreach (DataRow cpyRow in copyDt.Rows)
                        {
                            cpyRow["article"] = ViewState["SArticle"];
                        }

                        DataTable selectedArticleDt = copyDt.Clone();
                        foreach (DataRow copyRow in copyDt.Rows)
                        {
                            selectedArticleDt.ImportRow(copyRow);
                        }

                        ViewState["tblKnifEntry"] = selectedArticleDt;

                        this.Data_Bind();

                        this.lnkbtnSave_Click(null, null);

                        this.lbtnOk_Click(null, null);

                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }



    }
}