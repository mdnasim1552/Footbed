using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_03_CostABgd
{
    public partial class SalesContact : System.Web.UI.Page
    {

        ProcessAccess proc1 = new ProcessAccess();
        public static double ToCost, OrdrVal, toqty, ToCostPer, ToCostPerM, totalcmPer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //this.CreateList();
                ((Label)this.Master.FindControl("lblTitle")).Text = Request.QueryString["Type"].ToString() == "Entry" ? "Create Proforma Invoice"
                                                                    : Request.QueryString["Type"].ToString() == "Edit" ? "Edit Proforma Invoice" : "";

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.CommonButton();
                this.LoadData();
                string qgenno = this.Request.QueryString["genno"] ?? "";
                if (qgenno.Length > 0)
                {
                    this.lnkbtnPrevPfi_Click(null, null);
                    //this.ddlPreList.SelectedValue = this.Request.QueryString["genno"].ToString();
                    this.OkBtn_Click(null, null);
                }
            }
        }

        private void LoadData()
        {

            this.GetSesson();
            this.GetBuyer();
            this.GetLCCode();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkbtnHisprice_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDel_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
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
            //DdlSeason_SelectedIndexChanged(null, null);
        }

        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            string agent = "%";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_AGENTWISE_BUYER_LIST", agent, "", "", "", "", "", "", "", "");

            ViewState["tblBuyer"] = ds1.Tables[0];

            //DataView dv = ds1.Tables[0].DefaultView;
            //DataRowView newRow = dv.AddNew();
            //DataView dv2 = new DataView(ds1.Tables[0]);
            //dv2.RowFilter = ("sircode not like '000000000000'");

            //newRow = dv2.AddNew();
            //newRow["sircode"] = "000000000000";
            //newRow["sirdesc"] = "----All----";
            //dv2.ToTable().Rows.Add(newRow);

            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds1.Tables[0];
            this.ddlBuyer.DataBind();
            this.ddlBuyer.Items.Add(new ListItem { Value = "000000000000", Text = "----All----" });

            if (string.IsNullOrWhiteSpace(Request.QueryString["sircode"].ToString()))
            {
                this.ddlBuyer.SelectedValue = "000000000000";
            }
            else
            {
                this.ddlBuyer.SelectedValue = Request.QueryString["sircode"].ToString();
            }
        }


        private void CreateList()
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = new List<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tblsalecontact"] = lst;
        }

        private void CommonButton()
        {

            // ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Lc Details Information";
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";

            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Text = "Delete Selected Item";
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).OnClientClick = "return confirm('Do you want to Remove Selected Item?')";
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).CssClass = "btn btn-info btn-sm";


        }

        private void lnkbtnDel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvOrderTerms.Rows.Count; i++)
            {
                DataTable dt1 = (DataTable)ViewState["tblterms"];
                DataView dv = dt1.DefaultView;
                string termsid = ((Label)this.gvOrderTerms.Rows[i].FindControl("lblgvTermsID")).Text.ToString();
                if (((CheckBox)this.gvOrderTerms.Rows[i].FindControl("chkCol")).Checked)
                {
                    dv.RowFilter = ("termsid <>'" + termsid  + "'");
                    ViewState["tblterms"] = dv.ToTable();
                }

            }
            this.Data_Bind();
        }

        protected void lnkbtnHisprice_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "RidirectToLcinfo('MLCInfoEntry?Type=Entry&actcode=" + this.ddlmlccode.SelectedValue.ToString() + "&dayid=" + this.ddlArticle.SelectedValue.ToString() + "');", true);


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetLCCode()
        {
            string comcod = this.GetCompCode();
            string filter = "1601%";
            string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string buyer = this.ddlBuyer.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";

            //DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");

            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", filter, buyer, season, "", "", "", "", "");

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["orders"] = lst;
            this.ddlmlccode.DataSource = lst.Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            this.ddlmlccode.DataTextField = "mlcdesc";
            this.ddlmlccode.DataValueField = "mlccod";
            this.ddlmlccode.DataBind();

            //this.ddlmlccode.DataTextField = "mlcdesc";
            //this.ddlmlccode.DataValueField = "mlccod";
            //this.ddlmlccode.DataSource = ds1.Tables[1];
            //this.ddlmlccode.DataBind();

            if (Request.QueryString["actcode"].ToString() != "")
            {
                this.ddlmlccode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlmlccode.Enabled = false;
            }
            ds1.Dispose();
            this.ddlmlccode_SelectedIndexChanged(null, null);

        }

        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ((List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["orders"]).FindAll(p => p.mlccod == mlccod).ToList();
            this.ddlArticle.DataSource = lst.Select(m => new { m.rdayid, m.rdaydesc }).Distinct().ToList();
            this.ddlArticle.DataTextField = "rdaydesc";
            this.ddlArticle.DataValueField = "rdayid";
            this.ddlArticle.DataBind();
            if (Request.QueryString["dayid"].ToString() != "")
            {
                this.ddlArticle.SelectedValue = this.Request.QueryString["dayid"].ToString();
                this.ddlArticle.Enabled = false;
            }
            this.ddlArticle_SelectedIndexChanged(null, null);

        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd.MM.yyyy");
            string Invoiceno = this.txtInvoiceno.Text;

            DataTable dt = (DataTable)ViewState["tblterms"];
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            var lst2 = dt.DataTableToList<SPEENTITY.C_03_CostABgd.ProformaInvTrms>();
            var lst1 = lst
                        .GroupBy(c => new { c.custordno, c.mlccod })
                        .Select(c =>
                             new SPEENTITY.C_03_CostABgd.EclassSalesContact
                             {
                                 mlccod = c.First().mlccod,
                                 mlcdesc = c.First().mlcdesc,
                                 artno = c.First().artno,
                                 custrefno = c.First().custrefno,
                                 styleid = c.First().styleid,
                                 styledesc = c.First().styledesc,
                                 colorid = c.First().colorid,
                                 colordesc = c.First().colordesc,
                                 sizeid = c.First().sizeid,
                                 sizedesc = c.First().sizedesc,
                                 ordrqty = c.Sum(x1 => x1.ordrqty),
                                 delvdat = c.First().delvdat,
                                 rescode = c.First().rescode,
                                 resdesc = c.First().resdesc,
                                 ordrqty1 = c.Sum(x1 => x1.ordrqty1),
                                 rate = c.Average(x1 => x1.rate),
                                 ordrno = c.First().ordrno,
                                 custordno = c.First().custordno,
                                 hscode = c.First().hscode,
                                 style1 = c.First().style1,
                                 styledesc1 = c.First().styledesc1,
                                 balqty = c.Sum(x1 => x1.balqty),
                                 rdayid = c.First().rdayid,
                                 rdaydesc = c.First().rdaydesc,
                                 custname = c.First().custname,
                                 custdetails1 = c.First().custdetails1,
                                 custdetails2 = c.First().custdetails2,
                                 sdino = c.First().sdino,
                                 lastformadesc = c.First().lastformadesc,
                                 curdesc = c.First().curdesc,
                                 subcurdesc = c.First().subcurdesc,
                                 cursymbol = c.First().cursymbol,
                                 curword = c.First().curword,
                                 codedesc = c.First().codedesc
                             }
                        ).OrderBy(c => c.custordno).ToList();
            string totalqty = lst1.Sum(p => p.ordrqty1).ToString("#,##0;(#,##0); ");
            double discount = this.txtDiscount.Text.Trim().Length == 0 ? 0 : Convert.ToDouble(this.txtDiscount.Text.Trim());
            double prepayment = this.txtPrep.Text.Trim().Length == 0 ? 0 : Convert.ToDouble(this.txtPrep.Text.Trim());
            double totalvalue = Convert.ToDouble((lst1.Sum(p => p.ordrqty1 * p.rate)));
            double netPayblAmt = totalvalue - (totalvalue * discount / 100);
            //double amount = lst1.
            string inwords = ASTUtility.Trans(netPayblAmt, 2).Replace("Taka", lst1[0].curword).Replace("Paisa", lst1[0].subcurdesc).ToString();

            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptProformaInvFb", lst1, lst2, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("netPayable", netPayblAmt.ToString()));
                    Rpt1.SetParameters(new ReportParameter("discount", discount.ToString()));   
                    Rpt1.SetParameters(new ReportParameter("discountclaim", "Less " + discount.ToString("#,##0.00;(#,##0.00);") + "%" + " Claim Discount"));
                    Rpt1.SetParameters(new ReportParameter("goodsdesc", "Completed Leather Mens/Ladies Footwear"));

                    break;

                default:
                    Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptProformaInv", lst1, lst2, null);
                    break;
            }

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("amtword", inwords));
            Rpt1.SetParameters(new ReportParameter("pino", Invoiceno));
            Rpt1.SetParameters(new ReportParameter("pidate", date));
            Rpt1.SetParameters(new ReportParameter("totalqty", totalqty));
            Rpt1.SetParameters(new ReportParameter("cursymbol", lst[0].cursymbol));
            Rpt1.SetParameters(new ReportParameter("curword", lst[0].curword));
            Rpt1.SetParameters(new ReportParameter("curdesc", lst[0].curdesc));
            Rpt1.SetParameters(new ReportParameter("codedesc", lst[0].codedesc));
            Rpt1.SetParameters(new ReportParameter("rptitle", "PROFORMA INVOICE"));
            Rpt1.SetParameters(new ReportParameter("origin", ""));
            Rpt1.SetParameters(new ReportParameter("custname", lst[0].custname));
            Rpt1.SetParameters(new ReportParameter("custdetails1", lst[0].custdetails1));
            Rpt1.SetParameters(new ReportParameter("custdetails2", lst[0].custdetails2));
            Rpt1.SetParameters(new ReportParameter("payment", ""));
            Rpt1.SetParameters(new ReportParameter("devterms", ""));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        private void Data_Bind()
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            if (lst.Count == 0)
            {
                this.gvSalCon.DataSource = null;
                this.gvSalCon.DataBind();
                return;
            }

            this.gvSalCon.DataSource = HiddenSameData(lst);
            this.gvSalCon.DataBind();

            this.gvOrderTerms.DataSource = (DataTable)ViewState["tblterms"];
            this.gvOrderTerms.DataBind();

            FooterCal();
        }


        protected void ddlprocode_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];
            string lccode = this.ddlmlccode.SelectedValue.ToString().Trim();
            string custordno = this.ddlprocode.SelectedValue.ToString().Trim();
            var lst2 = lst.FindAll(p => p.mlccod == lccode && p.custordno == custordno);
            this.ddlDetail.DataSource = lst2;
            this.ddlDetail.DataTextField = "resdesc";
            this.ddlDetail.DataValueField = "rescode";
            this.ddlDetail.DataBind();

            //this.Data_Bind();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];

            DataTable dtBuyers = (DataTable)ViewState["tblBuyer"];
            DataView dv = dtBuyers.AsDataView();
            dv.RowFilter = "sircode='" + this.ddlBuyer.SelectedValue + "'";
            DataTable dtSelectedBuyer = dv.ToTable();

            string rescode = this.ddlDetail.SelectedValue.ToString();
            string custord = this.ddlprocode.SelectedValue.ToString();

            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            var dr = tbl1.FindAll(x => x.rescode == rescode && x.custordno == custord);
            string mlccode = dr[0].mlccod.ToString();
            string mlcdesc = dr[0].mlcdesc.ToString();
            string styleid = dr[0].styleid.ToString();
            string styledesc = dr[0].styledesc.ToString();
            string colorid = dr[0].colorid.ToString();
            string colordesc = dr[0].colordesc.ToString();
            string sizeid = dr[0].sizeid.ToString();
            string sizedesc = dr[0].sizedesc.ToString();
            double ordrqty = Convert.ToDouble(dr[0].sizewisetotal);
            double rate = Convert.ToDouble(dr[0].rate);
            string artno = dr[0].artno.ToString();
            string hscode = dr[0].hscode.ToString();
            string rdayid = dr[0].rdayid.ToString();
            string ordrno = dr[0].ordrno.ToString();
            string custname = ddlBuyer.SelectedItem.Text;
            string custaddr = dtSelectedBuyer.Rows[0]["custaddr"].ToString();
            string contactname = dtSelectedBuyer.Rows[0]["contactname"].ToString();
            string custordno = dr[0].custordno.ToString();
            string custrefno = dr[0].custrefno.ToString();
            double ordrqty1 = Convert.ToDouble(dr[0].ordrqty);
            string cursymbol = tbl1[0].cursymbol;
            string lastformadesc = dr[0].lastformadesc.ToString();
            var checklist = lst.FindAll(p => p.mlccod == mlccode && p.rdayid == rdayid && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.custordno == custordno);
            
            if (checklist.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);

                return;
            }

            lst.Add(new SPEENTITY.C_03_CostABgd.EclassSalesContact(mlccode, mlcdesc, artno, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, deldate, ordrqty1, rate, ordrno, hscode, rdayid, "", custordno, lastformadesc, custrefno));
            lst[lst.Count - 1].custname = custname;
            lst[lst.Count - 1].custdetails1 = custaddr;
            lst[lst.Count - 1].custdetails2 = contactname;
            lst[lst.Count - 1].cursymbol = cursymbol;

            ViewState["tblsalecontact"] = lst;
            this.Data_Bind();
        }

        protected void AddAll_Click(object sender, EventArgs e)
        {
            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            if (lst == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please click ok before add');", true);
                return;
            }
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];


            DataTable dtBuyers = (DataTable)ViewState["tblBuyer"];
            DataView dv = dtBuyers.AsDataView();
            dv.RowFilter = "sircode='" + this.ddlBuyer.SelectedValue + "'";
            DataTable dtSelectedBuyer = dv.ToTable();


            string custordno = this.ddlprocode.SelectedValue.ToString();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            var list3 = tbl1.FindAll(x => x.mlccod == mlccod && x.custordno == custordno);
            foreach (SPEENTITY.C_03_CostABgd.EclassSalesContact c1 in list3)
            {
                string mlccode = c1.mlccod.ToString();
                string mlcdesc = c1.mlcdesc.ToString();
                string styleid = c1.styleid.ToString();
                string styledesc = c1.styledesc.ToString();
                string colorid = c1.colorid.ToString();
                string colordesc = c1.colordesc.ToString();
                string sizeid = c1.sizeid.ToString();
                string sizedesc = c1.sizedesc.ToString();
                double sizewisetotal = Convert.ToDouble(c1.sizewisetotal);
                double rate = Convert.ToDouble(c1.rate);
                string artno = c1.artno.ToString();
                string hscode = c1.hscode.ToString();
                string rdayid = c1.rdayid.ToString();
                string ordrno = c1.ordrno.ToString();
                string cusordrno = c1.custordno.ToString();
                string custrefno = c1.custrefno.ToString();
                string custname = ddlBuyer.SelectedItem.Text;
                string custaddr = dtSelectedBuyer.Rows[0]["custaddr"].ToString();
                string contactname = dtSelectedBuyer.Rows[0]["contactname"].ToString();
                double ordrqty = Convert.ToDouble(c1.ordrqty);

                string lastformadesc = c1.lastformadesc.ToString();
                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.rdayid == rdayid && p.custordno == cusordrno && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid);
                if (checklist.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);
                    return;
                }

                lst.Add(new SPEENTITY.C_03_CostABgd.EclassSalesContact(mlccode, mlcdesc, artno, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, sizewisetotal, deldate, ordrqty, rate, ordrno, hscode, rdayid, "", cusordrno, lastformadesc, custrefno));
                lst[lst.Count - 1].custname = custname;
                lst[lst.Count - 1].custdetails1 = custaddr;
                lst[lst.Count - 1].custdetails2 = contactname;
                lst[lst.Count - 1].cursymbol = list3[0].cursymbol;

            }

            ViewState["tblsalecontact"] = lst;
            this.Data_Bind();
        }

        protected void AddAllOrder_Click(object sender, EventArgs e)
        {

            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];

            if (lst == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please click ok before add');", true);
                return;
            }
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];


            DataTable dtBuyers = (DataTable)ViewState["tblBuyer"];
            DataView dv = dtBuyers.AsDataView();
            dv.RowFilter = "sircode='" + this.ddlBuyer.SelectedValue + "'";
            DataTable dtSelectedBuyer = dv.ToTable();


            string custordno = this.ddlprocode.SelectedValue.ToString();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            //var list3 = tbl1.FindAll(x => x.mlccod == mlccod && x.custordno == custordno);
            foreach (SPEENTITY.C_03_CostABgd.EclassSalesContact c1 in tbl1)
            {
                string mlccode = c1.mlccod.ToString();
                string mlcdesc = c1.mlcdesc.ToString();
                string styleid = c1.styleid.ToString();
                string styledesc = c1.styledesc.ToString();
                string colorid = c1.colorid.ToString();
                string colordesc = c1.colordesc.ToString();
                string sizeid = c1.sizeid.ToString();
                string sizedesc = c1.sizedesc.ToString();
                double sizewisetotal = Convert.ToDouble(c1.sizewisetotal);
                double rate = Convert.ToDouble(c1.rate);
                string artno = c1.artno.ToString();
                string hscode = c1.hscode.ToString();
                string rdayid = c1.rdayid.ToString();
                string ordrno = c1.ordrno.ToString();
                string custname = ddlBuyer.SelectedItem.Text;
                string custaddr = dtSelectedBuyer.Rows[0]["custaddr"].ToString();
                string contactname = dtSelectedBuyer.Rows[0]["contactname"].ToString();
                string cusordrno = c1.custordno.ToString();
                string custrefno = c1.custrefno.ToString();
                double ordrqty = Convert.ToDouble(c1.ordrqty);

                string lastformadesc = c1.lastformadesc.ToString();
                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.rdayid == rdayid && p.custordno == cusordrno && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid);
                if (checklist.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);
                    return;
                }

                lst.Add(new SPEENTITY.C_03_CostABgd.EclassSalesContact(mlccode, mlcdesc, artno, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, sizewisetotal, deldate, ordrqty, rate, ordrno, hscode, rdayid, "", cusordrno, lastformadesc, custrefno));
                lst[lst.Count - 1].custname = custname;
                lst[lst.Count - 1].custdetails1 = custaddr;
                lst[lst.Count - 1].custdetails2 = contactname;
                lst[lst.Count - 1].cursymbol = tbl1[0].cursymbol;
            }

            ViewState["tblsalecontact"] = lst;
            this.Data_Bind();
        }


        protected void gvSalCon_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            int rowindex = (this.gvSalCon.PageSize) * (this.gvSalCon.PageIndex) + e.RowIndex;
            lst.RemoveAt(rowindex);
            ViewState["tblsalecontact"] = lst;
            this.Data_Bind();
        }
        protected void AddMore_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string mlccod = lst[RowIndex].mlccod.ToString();
            string mlcdesc = lst[RowIndex].mlcdesc.ToString();
            string styleid = lst[RowIndex].styleid.ToString();
            string styledesc = lst[RowIndex].styledesc.ToString();
            string colorid = lst[RowIndex].colorid.ToString();
            string colordesc = lst[RowIndex].colordesc.ToString();
            string sizeid = lst[RowIndex].sizeid.ToString();
            string sizedesc = lst[RowIndex].sizedesc.ToString();
            double ordrqty = Convert.ToDouble(lst[RowIndex].ordrqty);
            DateTime deldate = lst[RowIndex].delvdat;
            double rate = Convert.ToDouble(lst[RowIndex].rate);
            string ordrno = lst[RowIndex].ordrno.ToString();
            string hscode = lst[RowIndex].hscode.ToString();
            string dayid = lst[RowIndex].rdayid.ToString();
            string cusordrno = lst[RowIndex].custordno.ToString();
            string custrefno = lst[RowIndex].custrefno.ToString();

            string lastformadesc = lst[RowIndex].lastformadesc.ToString();
            lst.Add(new SPEENTITY.C_03_CostABgd.EclassSalesContact(mlccod, mlcdesc, "", styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, deldate, 0.00, rate, ordrno, hscode, dayid, "", cusordrno, lastformadesc, custrefno));
            ViewState["tblsalecontact"] = lst;
            this.Data_Bind();

        }
        private void SaveValue()
        {

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];

            DataTable dt = (DataTable)ViewState["tblterms"];

            for (int i = 0; i < this.gvSalCon.Rows.Count; i++)
            {
                double ordrqty = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvordrqty1")).Text.Trim());
                string custrefno = ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvCustname")).Text.Trim().ToString();
                string custordno = ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvCustorderno")).Text.Trim().ToString();
                string deldate = ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvDelvdate")).Text.Trim().ToString();
                lst[i].ordrqty1 = ordrqty;
                lst[i].custrefno = custrefno;
                lst[i].custordno = custordno;
                lst[i].delvdat = Convert.ToDateTime(deldate);
            }

            for (int i = 0; i < this.gvOrderTerms.Rows.Count; i++)
            {
                dt.Rows[i]["termssubj"] = ((TextBox)this.gvOrderTerms.Rows[i].FindControl("txtgvSubject")).Text;
                dt.Rows[i]["termsdesc"] = ((TextBox)this.gvOrderTerms.Rows[i].FindControl("txtgvDesc")).Text;
            }

            ViewState["tblsalecontact"] = lst;
            ViewState["tblterms"] = dt;
        }

        protected void ddlArticle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            DataSet ds2 = new DataSet();

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = new List<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst2 = new List<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            string mlccode = this.ddlmlccode.SelectedValue.ToString();
            string rdayid = this.ddlArticle.SelectedValue.ToString();
            ds2 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERDETAILS", mlccode, rdayid, "", "", "", "", "", "");
            lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();

            this.ddlprocode.DataSource = lst.Select(m => new { m.custordno }).Distinct().ToList();
            this.ddlprocode.DataTextField = "custordno";
            this.ddlprocode.DataValueField = "custordno";
            this.ddlprocode.DataBind();
            ViewState["tblmlcorder"] = lst;
            ViewState["tblterms"] = ds2.Tables[1];
            this.ddlprocode_SelectedIndexChanged(null, null);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.txtDiscount.Text = ds2.Tables[0].Rows[0]["discount"].ToString();
                this.txtPrep.Text = ds2.Tables[0].Rows[0]["prepayment"].ToString();
            }


        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            SaveValue();
            this.Data_Bind();
        }

        protected void RefBtn_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblsalecontact");
            this.gvSalCon.DataSource = null;
            this.gvSalCon.DataBind();
            this.CreateList();
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLCCode();
        }

        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLCCode();
        }

        protected void lnkbtnPrevPfi_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string curdate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string srchsam = (qgenno.Length > 0 ? qgenno : "") + "%";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GET_ALL_PROFORMA_INV_NO", curdate, srchsam);

            this.ddlPrevPfi.DataValueField = "pfino";
            this.ddlPrevPfi.DataTextField = "pfino2";
            this.ddlPrevPfi.DataSource = ds1.Tables[0];
            this.ddlPrevPfi.DataBind();
        }

        protected void OkBtn_Click(object sender, EventArgs e)
        {
            if (this.OkBtn.Text == "New")
            {
                this.OkBtn.Text = "Ok";

                this.lnkbtnPrevPfi.Visible = true;
                this.ddlPrevPfi.Visible = true;
                this.DdlSeason.Enabled = true;
                this.ddlBuyer.Enabled = true;

            }
            else
            {
                this.OkBtn.Text = "New";
                this.lnkbtnPrevPfi.Visible = false;
                this.ddlPrevPfi.Visible = false;
                this.ddlBuyer.Enabled = false;
                this.DdlSeason.Enabled = false;

                string comcod = this.GetCompCode();
                ViewState.Remove("tblsalecontact");
                string CurDate1 = this.txtdate.Text.Trim();
                string mInqNo = "NEWFPI";
                if (this.ddlPrevPfi.Items.Count > 0)
                {
                    this.txtdate.Enabled = false;
                    mInqNo = this.ddlPrevPfi.SelectedValue.ToString();
                }

                DataSet ds1 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GET_PFI_WISE_ORDR_DETAILS", mInqNo, CurDate1, "", "", "", "", "", "", "");

                if (ds1 == null)
                {
                    return;
                }

                ViewState["tblsalecontact"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();

                if( mInqNo!= "NEWFPI" && ds1.Tables[1].Rows.Count > 0)
                {
                    ViewState["tblterms"] = ds1.Tables[1];
                }

                if (mInqNo == "NEWFPI")
                {

                    DataSet ds3 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GET_NEW_PROFORMA_INV_NO", CurDate1, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    this.txtPfiNo1.Text = ds3.Tables[0].Rows[0]["maxpfino1"].ToString().Substring(0, 6);
                    this.txtPfiNo2.Text = ds3.Tables[0].Rows[0]["maxpfino1"].ToString().Substring(6);

                    this.Data_Bind();
                    return;
                }


                this.txtPfiNo1.Text = ds1.Tables[2].Rows[0]["pfino1"].ToString().Substring(0, 6);
                this.txtPfiNo2.Text = ds1.Tables[2].Rows[0]["pfino1"].ToString().Substring(6, 5);
                this.txtdate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");
                this.txtInvoiceno.Text = ds1.Tables[2].Rows[0]["invno"].ToString();
                this.txtDiscount.Text = ds1.Tables[2].Rows[0]["discount"].ToString();
                this.txtPrep.Text = ds1.Tables[2].Rows[0]["prepayment"].ToString();
                this.Data_Bind();
            }
        }

        protected void gvOrderTerms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblterms"];
            int index = (this.gvOrderTerms.PageIndex) * this.gvOrderTerms.PageSize + e.RowIndex;
            dt.Rows.RemoveAt(index);
            ViewState["tblterms"] = dt;
            this.Data_Bind();
        }

        private List<SPEENTITY.C_03_CostABgd.EclassSalesContact> HiddenSameData(List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst)
        {

            //string slnum = dt.Rows[0]["slnum"].ToString();
            string styleid = "";
            string mlccod = "";
            string sizeid = "";
            var list22 = lst.OrderBy(m => m.styleid).ThenBy(m => m.colordesc).ThenBy(m => m.sizeid).ToList();
            foreach (SPEENTITY.C_03_CostABgd.EclassSalesContact c1 in list22)
            {
                if (styleid == c1.styleid.ToString())
                {
                    c1.styledesc = "";
                }
                if (styleid == c1.styleid.ToString() && mlccod == c1.mlccod.ToString())
                {
                    c1.colordesc = "";
                }
                if (styleid == c1.styleid.ToString() && mlccod == c1.mlccod.ToString() && sizeid == c1.sizeid.ToString())
                {
                    c1.sizedesc = "";
                    c1.ordrqty = 0.00;
                }
                styleid = c1.styleid.ToString();
                mlccod = c1.mlccod.ToString();
                sizeid = c1.sizeid.ToString();

            }
            ViewState["tblsalecontact"] = list22;
            return list22;

        }

        protected void gvSalCon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string custordno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "custordno")).ToString();

                string styledesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styledesc")).ToString();
                string sizedesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sizedesc")).ToString();
                var lst2 = lst.FindAll(p => p.custordno == custordno);


                if (lst2.Count == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                    e.Row.ToolTip = "Newly Added PO thats not listed in Order Sheet";
                }
                if (sizedesc != "")
                {
                    e.Row.FindControl("lbAddMore").Visible = true;
                }
                else
                {
                    e.Row.FindControl("lbAddMore").Visible = false;
                }

            }
        }
        private void FooterCal()
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> list = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];

            if (list == null || list.Count == 0)
            {
                return;
            }

             ((Label)this.gvSalCon.FooterRow.FindControl("txtFNetTotal1")).Text = ((list.Sum(p => p.ordrqty1) == 0) ? 0 : list.Sum(p => p.ordrqty1)).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvSalCon.FooterRow.FindControl("FLblgvColTotal")).Text = ((list.Sum(p => p.colqty) == 0) ? 0 : list.Sum(p => p.colqty)).ToString("#,##0;(#,##0); ");


        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string invno = this.txtInvoiceno.Text.ToString();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string dayid = this.ddlArticle.SelectedValue.ToString();
            string buyerid = this.ddlBuyer.SelectedValue.ToString();
            string discount = this.txtDiscount.Text.Trim().Length == 0 ? "0" : this.txtDiscount.Text.Trim();
            string prepayment = this.txtPrep.Text.Trim().Length == 0 ? "0" : this.txtPrep.Text.Trim();
            this.SaveValue();

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            var checklist = lst.FindAll(p => p.custordno == "" || p.custrefno == "");
            if (checklist.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Order Number & Customer Name can not be blank');", true);
                return;
            }

            var data = lst.GroupBy(d => new { d.styleid, d.colorid, d.sizeid })
            .Select(
             g => new
             {
                 Key = g.Key,
                 ordrqty = g.Sum(s => s.ordrqty),
                 ordrqty1 = g.Sum(s => s.ordrqty1)
             }
             ).ToList();

            //if (data.FindAll(p => p.ordrqty != p.ordrqty1).Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Check order Qty');", true);
            //    return;
            //}

            //string year = DateTime.Now.ToString("yyyy");
            //string newpfino = this.txtPfiNo1.Text.Trim().Substring(0, 3) + year + this.txtPfiNo1.Text.Trim().Substring(3, 2) + this.txtPfiNo2.Text.Trim();

            if (this.ddlPrevPfi.Items.Count == 0)
                this.GetNewPFINo();
            string newpfino = this.txtPfiNo1.Text.ToString().Trim().Substring(0, 3) + date.Substring(7, 4) + this.txtPfiNo1.Text.ToString().Trim().Substring(3, 2) + this.txtPfiNo2.Text.ToString().Trim();



            DataTable dt1 = ASITUtility03.ListToDataTable(lst);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string PostDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable dtterms = (DataTable)ViewState["tblterms"];
            DataSet ds2 = new DataSet("ds1");
            ds2.Tables.Add(dtterms);
            ds2.Tables[0].TableName = "tbl1";
            //DataSet result = proc1.GetTransInfoNew(comcod, "SP_INV_STDANA", "UPDATE_SALES_CONTACT", ds1, ds2, null, mlccod, dayid, invno, date, userid, trmid, sessionid, PostDat, newpfino, buyerid, discount, prepayment);
            bool result = proc1.UpdateXmlTransInfo(comcod, "SP_INV_STDANA", "UPDATE_SALES_CONTACT", ds1, ds2, null, mlccod, dayid, invno, date, userid, trmid, sessionid, PostDat, newpfino, buyerid, discount, prepayment);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
                return;
            }
        }

        private void GetNewPFINo()
        {

            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GET_NEW_PROFORMA_INV_NO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.txtPfiNo1.Text = ds3.Tables[0].Rows[0]["maxpfino1"].ToString().Substring(0, 6);
            this.txtPfiNo2.Text = ds3.Tables[0].Rows[0]["maxpfino1"].ToString().Substring(6);

            this.ddlPrevPfi.DataTextField = "maxpfino1";
            this.ddlPrevPfi.DataValueField = "maxpfino";
            this.ddlPrevPfi.DataSource = ds3.Tables[0];
            this.ddlPrevPfi.DataBind();

        }
    }
}

