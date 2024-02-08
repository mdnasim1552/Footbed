using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPEENTITY;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_04_Sampling
{
    public partial class SamTagPrint : System.Web.UI.Page
    {
        UserManagerSampling objUserMan = new UserManagerSampling();
        ProcessAccess samData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "TagPrint" ? "Sample Tag Print" : "Sample Packing List");
                this.GetGenCode();
                this.GetSampType();
                this.Getagent();
                this.GetBuyer();
                
            }
        }


        protected void gvPackList_DataBind()
        {
            string comcod = this.GetCompCode();
            string Season = this.DdlSeason.SelectedValue.Trim().ToString() != "00000" ? this.DdlSeason.SelectedValue.Trim().ToString() + "%" : "%";
            string SDI = ""; // this.ddlAccProject.SelectedValue.ToString();
            foreach (ListItem item in ddlSDI.Items)
            {
                if (item.Selected)
                {
                    SDI += item.Value;
                }
            }

            DataSet ds1 = samData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_SESSION_WISE_SDI", Season, SDI, "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];

            ViewState["dtPackList"] = ds1.Tables[0];

            this.gvPackList.DataSource = dt;
            this.gvPackList.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "TagPrint":
                    this.PrintTagPrintReport();
                    break;

                case "PackingList":
                    this.PrintPackListReport();
                    break;
            }


        }

        private void PrintTagPrintReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            string Season = this.DdlSeason.SelectedValue.Trim().ToString() != "00000" ? this.DdlSeason.SelectedValue.Trim().ToString() + "%" : "%";


            string SDI = ""; // this.ddlAccProject.SelectedValue.ToString();
            foreach (ListItem item in ddlSDI.Items)
            {
                if (item.Selected)
                {
                    SDI += item.Value;
                }
            }

            DataSet ds3 = samData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_SESSION_WISE_SDI", Season, SDI, "", "", "", "", "", "", "");

            DataTable dt = ds3.Tables[0];
            var list = dt.DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.SamTagPrint>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptSamTagPrint", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            //rpt1.SetParameters(new ReportParameter("date", date));
            //rpt1.SetParameters(new ReportParameter("rptTitle", "Material Master Report"));
            //rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            //rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintPackListReport()
        {
            DataTable dtPackList = (DataTable)ViewState["dtPackList"];
            var lstPackList = dtPackList.DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PackList>();
            string season = DdlSeason.SelectedItem.Text;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();

            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptPackListPrint", lstPackList, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", compadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comlogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Packing List"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            rpt1.SetParameters(new ReportParameter("season", season));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "TagPrint":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.lbtnOk.Text = "New";

                    break;

                case "PackingList":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lbtnOk.Text = "New";
                    this.gvPackList_DataBind();
                    break;
            }
        }



        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);


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
            DdlSeason_SelectedIndexChanged(null, null);

        }

        private void GetSampType()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;

            var lstsm = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "34");

            ddlSmpltype.DataTextField = "gdesc";
            ddlSmpltype.DataValueField = "gcod";
            ddlSmpltype.DataSource = lstsm;
            ddlSmpltype.DataBind();
            ddlSmpltype.Items.Add(new ListItem { Selected = true, Value = "00000", Text = "--All--" });
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

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetArticle();
        }

        private void GetArticle()
        {
            string comcod = this.GetCompCode();
            string Season = this.DdlSeason.SelectedValue.Trim().ToString() != "00000" ? this.DdlSeason.SelectedValue.Trim().ToString() + "%" : "%";
            string samptype = this.ddlSmpltype.SelectedValue.Trim().ToString() != "00000" ? this.ddlSmpltype.SelectedValue.Trim().ToString() + "%" : "%";
            string agent = this.DdlAgent.SelectedValue.Trim().ToString() != "00000" ? this.DdlAgent.SelectedValue.Trim().ToString() + "%" : "%";
            string buyer = this.ddlBuyer.SelectedValue.Trim().ToString() != "000000000000" ? this.ddlBuyer.SelectedValue.Trim().ToString() + "%" : "%";

            DataSet ds3 = samData.GetTransInfo(comcod, "SP_REPORT_PROTOSAMPLING_INTERFACE", "GET_SESSION_WISE_SDI", Season, "", samptype, agent, buyer, "", "", "", "");


            this.ddlSDI.DataTextField = "INQNO1";
            this.ddlSDI.DataValueField = "INQNO";
            this.ddlSDI.DataSource = ds3.Tables[0];
            this.ddlSDI.DataBind();
            Session["tblSDINo"] = ds3.Tables[0];
        }

        protected void ddlSmpltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetArticle();
        }
        protected void DdlAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBuyer();
        }

        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            string agent = (this.DdlAgent.SelectedValue.ToString() == "00000") ? "%" : this.DdlAgent.SelectedValue.ToString() + "%";
            DataSet ds2 = samData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_AGENTWISE_BUYER_LIST", agent, "", "", "", "", "", "", "", "");


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

        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetArticle();
        }

    }
}