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


namespace SPEWEB.F_34_Mgt
{
    public partial class RptGroupTaskold : System.Web.UI.Page
    {
        ProcessAccess gtsdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Group Task Details";
                this.rbtgtc_SelectedIndexChanged(null, null);

                this.GetGtsDataShow();
            }
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lbtnrrecalculate_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lFinalUpdate_Click);
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //string comcod = this.GetComCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fdate = this.txtDatefrom.Text.ToString();
            //string tdate = this.txtdateto.Text.ToString();
            //string ToFrDate = "(From :" + fdate + " To " + tdate + ")";
            //DataTable dt = (DataTable)ViewState["tblgts"];

            //var lst = dt.DataTableToList<RMGiEntity.C_34_Mgt.Grouptaskchat>();

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptOrderStatus", lst, null, null);
            //rpt1.EnableExternalImages = true;

            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("ToFrDate", ToFrDate));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "Order Status"));
            //rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            ////rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            ////rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void GetGtsDataShow()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            //string userid = hst["usrid"].ToString();
            // string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet dsgts = gtsdata.GetTransInfo(comcod, "SP_REPORT_CHAT_MGT", "GETCHATDATA", "", "", "", "", "", "", "", "", "");
            if (dsgts == null)
                return;

            //gvGrouptask.DataSource = dsgts.Tables[0];
            //gvGrouptask.DataBind();
            ViewState["tblgts"] = dsgts.Tables[0];
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            string inx = this.rbtgtc.SelectedValue.ToString();

            DataTable dt = (DataTable)ViewState["tblgts"];


            string gtccod = this.ddlShowAlldata.SelectedValue.ToString().Trim();
            //string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            DataTable dt1 = new DataTable();
            DataView view = new DataView();

            view.Table = dt;

            if (inx == "0")

            //dv.RowFilter = "Num = 7097" ////This is correct
            {
                view.RowFilter = ("asinuser ='" + gtccod + "'");

            }
            if (inx == "1")
            {
                view.RowFilter = ("userid =" + gtccod);

            }
            else if (inx == "2")
            {
                view.RowFilter = ("actcode =" + gtccod);

            }
            else if (inx == "3")
            {
                view.RowFilter = ("taskcod =" + gtccod);

            }
            //else if (inx == "4")
            //{
            //    view.RowFilter = ("taskcod =" + gtccod);

            //}

            ////view.Sort = "createdate Desc ";




            dt1 = view.ToTable();






            gvGrouptask.DataSource = dt1;
            gvGrouptask.DataBind();
        }

        private void GetUsername()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = gtsdata.GetTransInfo(comcod, "SP_REPORT_CHAT_MGT", "GETUSERWISEDATA", "", "", "", "", "", "", "", "", "");
            this.ddlShowAlldata.DataTextField = "usrsname";
            this.ddlShowAlldata.DataValueField = "usrid";
            this.ddlShowAlldata.DataSource = ds1.Tables[0];
            this.ddlShowAlldata.DataBind();
        }
        private void GetProjectName()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = gtsdata.GetTransInfo(comcod, "SP_REPORT_CHAT_MGT", "GETPROJWISEDATA", "", "", "", "", "", "", "", "", "");
            this.ddlShowAlldata.DataTextField = "actdesc";
            this.ddlShowAlldata.DataValueField = "actcode";
            this.ddlShowAlldata.DataSource = ds1.Tables[0];
            this.ddlShowAlldata.DataBind();
        }

        private void GetTaskName()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = gtsdata.GetTransInfo(comcod, "SP_REPORT_CHAT_MGT", "GETTASKWISEDATA", "", "", "", "", "", "", "", "", "");
            this.ddlShowAlldata.DataTextField = "gdesc";
            this.ddlShowAlldata.DataValueField = "gcod";
            this.ddlShowAlldata.DataSource = ds1.Tables[0];
            this.ddlShowAlldata.DataBind();
        }

        private void GetTaskActive()
        {

        }

        private void GetAsindege()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = gtsdata.GetTransInfo(comcod, "SP_REPORT_CHAT_MGT", "GETCHATASIN", "", "", "", "", "", "", "", "", "");
            this.ddlShowAlldata.DataTextField = "hrgdesc";
            this.ddlShowAlldata.DataValueField = "hrgcod";
            this.ddlShowAlldata.DataSource = ds1.Tables[0];
            this.ddlShowAlldata.DataBind();

        }

        protected void rbtgtc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string inx = this.rbtgtc.SelectedValue.ToString();
            //DataTable dt = (DataTable)ViewState["tblgts"];
            //string userid = dt.Rows[0]["userid"].ToString();
            //string actcode = dt.Rows[0]["actcode"].ToString();
            ////DataTable dt = ASITUtility03.ListToDataTable((List<EComRpEntity.Both.EclassBoth_BO.ProdList>)ViewState["catproduct"]);
            //dt.TableName = "Table1";
            //DataTable dt1 = new DataTable();
            //DataView view = new DataView();

            if (inx == "0")
            {
                this.GetAsindege();
                this.lblgtcwise.InnerText = "Assign Wise";

            }

            if (inx == "1")
            {
                this.GetUsername();
                this.lblgtcwise.InnerText = "User Wise";


            }

            else if (inx == "2")
            {
                this.GetProjectName();

                //view.Table = dt;
                //view.RowFilter = "actcode =" + actcode;
                ////view.Sort = "createdate Desc ";
                //dt1 = view.ToTable();
                ////  ViewState["catprodfinal"] = this.HiddenSameData(dt1);
                // this.titleproduct.InnerText = "Distributor Product List";
                //this.Data_Bind();

                this.lblgtcwise.InnerText = "Project Wise";
            }
            else if (inx == "3")
            {
                this.GetTaskName();
                this.lblgtcwise.InnerText = "Task Wise";
            }
            else if (inx == "4")
            {
                this.GetTaskActive();
                //this.lblgtcwise.InnerText = "Target Date Wise";
            }

            //else if (inx == "5")
            //{
            //    this.GetProjectName();
            // this.lblgtcwise.InnerText = "User Wise";
            //}

            //else if (inx == "6")
            //{
            //    this.GetProjectName();
            // this.lblgtcwise.InnerText = "User Wise";
            //}

            //else
            //{
            //    view.Table = dt;
            //    view.Sort = "createdate Desc ";
            //    dt1 = view.ToTable();
            //    ViewState["catprodfinal"] = this.HiddenSameData(dt1);
            //    this.titleproduct.InnerText = "All Upload Product List";
            //    this.data_bind();
            //}

        }

        protected void lbtOk_Click(object sender, EventArgs e)
        {
            Data_Bind();
        }


    }

}