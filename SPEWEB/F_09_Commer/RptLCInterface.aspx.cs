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
using SPEENTITY;

namespace SPEWEB.F_09_Commer
{
    public partial class RptLCInterface : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //this.txtFDate.Text = System.DateTime.Today.ToString("01-MMM-yyyy");
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                //this.txtFDate.Text = DateTime.Today.AddDays(-day).AddMonths(-2).ToString("dd-MMM-yyyy");


                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();
                this.PnlInt.Visible = true;
                GetStore();



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = (comcod == "5305") ? "Procurement (LC)" : " Foreign Import Smartface";
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");//(comcod == "5301") ? "01-Jan-2019" : DateTime.Today.AddDays(-day).AddMonths(-2).ToString("dd-MMM-yyyy");



                HyperLink hyp1 = (HyperLink)this.HyperLink1 as HyperLink;
                HyperLink HyPBom = (HyperLink)this.HyPBom as HyperLink;


                hyp1.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=LCEntry&comcod=" + comcod + "&actcode=&genno=";
                HyPBom.NavigateUrl = "~/F_01_Mer/RptOrdAppSheet?Type=PendBom";

                this.GetSeason();
                this.ShowBOMCount();
                this.ImportInterFace();
            }
        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");
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
        }

        protected void GetStore()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string fxtast = "FxtAst";
            string Aproval = "";
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string ReFindProject = "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.DDlStore.DataTextField = "actdesc1";
            this.DDlStore.DataValueField = "actcode";
            this.DDlStore.DataSource = ds2.Tables[0];
            this.DDlStore.DataBind();
            ViewState["tblStoreType"] = ds2.Tables[0];


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        //private void ColoumVisiable()
        //    {
        //        this.gvprobrec.Columns[4].Visible = true;
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
            //int day = ASTUtility.Datediffday(Convert.ToDateTime(this.txtFDate.Text), Convert.ToDateTime(this.txtdate.Text));
            //if (day != 0)
            //    return;
            //txtdate_TextChanged(null, null);


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void ShowBOMCount()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "BOM_COUNTER", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.HyPBom.Text = "0";

            }
            else
            {
                this.HyPBom.Text = ds1.Tables[0].Rows[0]["bomcount"].ToString();

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
            ViewState.Remove("tbllcreqest");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string season = DdlSeason.SelectedValue == "00000" ? "%" : DdlSeason.SelectedValue ;

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_LC_INTERFACE", Date1, Date2, season, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tbllcreqest"] = ds1.Tables[0];
            ViewState["tbllcopen"] = ds1.Tables[1];
            ViewState["tbllcrecv"] = ds1.Tables[2];
            ViewState["tbllcQc"] = ds1.Tables[3];
            ViewState["tblImport"] = ds1.Tables[4];
            ViewState["tbllc"] = ds1.Tables[5];

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[6].Rows[0]["reqqty"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Request</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[6].Rows[0]["chkqty"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> Req Checked</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading mix counter'>" + ds1.Tables[6].Rows[0]["reqauth"].ToString() + "</i></div></a><div class='circle-tile-content mix'><div class='circle-tile-description text-faded'> Req Review</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[6].Rows[0]["appqty"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Req Approval</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[6].Rows[0]["cscrtqty"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> CS Create</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[6].Rows[0]["cschkqty"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> CS Check</div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[6].Rows[0]["csaprvqty"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> CS Approval</div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-yal counter'>" + ds1.Tables[6].Rows[0]["poin"].ToString() + "</i></div></a><div class='circle-tile-content dark-yal'><div class='circle-tile-description text-faded'>PO</div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-yal counter'>" + ds1.Tables[6].Rows[0]["imapp"].ToString() + "</i></div></a><div class='circle-tile-content dark-yal'><div class='circle-tile-description text-faded'>PO Approval</div></div></div>";
            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[6].Rows[0]["opnqty"].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'> LC Opening</div></div></div>";
            this.RadioButtonList1.Items[10].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[6].Rows[0]["recqty"].ToString() + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> Received</div></div></div>";
            this.RadioButtonList1.Items[11].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[6].Rows[0]["qcqty"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> Qc Check</div></div></div>";
            this.RadioButtonList1.Items[12].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[6].Rows[0]["costqty"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>LC Costing</div></div></div>";

            RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            
            DataTable dt2 = (DataTable)ViewState["tbllcopen"];
            DataTable dt4 = (DataTable)ViewState["tbllcQc"];
            DataTable dt6 = (DataTable)ViewState["tbllc"];


            DataTable Tempdt = new DataTable();
            DataView Tempdv = new DataView();
            switch (value)
            {
                case "0":
                    DataTable dts1 = (DataTable)ViewState["tbllcreqest"];
                    this.pnlallRec.Visible = true;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlPOin.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.Data_Bind("gvprobrec", dts1);
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;
                case "1":
                    DataTable dts2 = (DataTable)ViewState["tbllcreqest"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = true;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    //this.Data_Bind("gvReqCheck", dt);
                    Tempdt = dts2.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("checked =''");
                    this.Data_Bind("gvReqCheck", Tempdv.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    DataTable dts3 = (DataTable)ViewState["tbllcreqest"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = true;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    //this.Data_Bind("gvReqCheck", dt);
                    Tempdt = dts3.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("checked <>'' and  authed='' ");
                    this.Data_Bind("gvReqAuth", Tempdv.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;


                case "3":
                    DataTable dts4 = (DataTable)ViewState["tbllcreqest"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = true;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dts4.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("authed <>'' and approved =''");
                    this.Data_Bind("gvprobapp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4":
                    DataTable dts5 = (DataTable)ViewState["tbllcreqest"];
                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = true;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dts5.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("approved ='Ok' and csstus=''");
                    this.Data_Bind("gvcscrte", Tempdv.ToTable());
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
                case "5":
                    DataTable dts6 = (DataTable)ViewState["tbllcreqest"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = true;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dts6.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("csstus <>'' and cschk=''");
                    this.Data_Bind("gvCsCheck", Tempdv.ToTable());
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;
                case "6":
                    DataTable dts7 = (DataTable)ViewState["tbllcreqest"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = true;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dts7.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("cschk<>'' and reqaprv=''");
                    //Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved=''");
                    this.Data_Bind("gvcsaprv", Tempdv.ToTable());

                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;
                case "7"://po in
                    DataTable dts8 = (DataTable)ViewState["tblImport"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = true;
                    Tempdt = dts8;
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("imapp='' and approved='Ok' and supwiseitm='0' ");
                    this.Data_Bind("gvPoIn", Tempdv.ToTable());
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    break;
                case "8":
                    DataTable dts9 = (DataTable)ViewState["tblImport"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.pnlImportAppr.Visible = true;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dts9;
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("imapp='' and approved='Ok' and supwiseitm<>'0' ");
                    this.Data_Bind("gvImportApp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    break;
                case "9":

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = true;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dt6.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved='Ok'  and  reqaprv<>'' and imapp='Ok' and lcopn='' "); //OR itmcount<>passitm
                    this.Data_Bind("gvprobass", Tempdv.ToTable());
                    this.RadioButtonList1.Items[9].Attributes["class"] = "lblactive blink_me";
                    break;

                case "10":
                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = true;
                    this.Pnlbill.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dt2.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("reqaprv ='Ok'");
                    this.Data_Bind("gvprobassapp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[10].Attributes["class"] = "lblactive blink_me";
                    break;
                case "11":
                    DataTable dts12 = (DataTable)ViewState["tbllcrecv"];

                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = true;
                    this.PnlCosting.Visible = false;
                    this.pnlPOin.Visible = false;
                    Tempdt = dts12.Copy();
                    Tempdv = Tempdt.DefaultView;
                    //  Tempdv.RowFilter = ("asrstatus ='Ok'");
                    this.Data_Bind("gvprobbill", Tempdv.ToTable());
                    this.RadioButtonList1.Items[11].Attributes["class"] = "lblactive blink_me";
                    break;
                case "12":
                    this.pnlallRec.Visible = false;
                    this.PnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.pnlImportAppr.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = true;
                    this.pnlPOin.Visible = false;
                    this.Data_Bind("gvCosting", dt4);
                    this.RadioButtonList1.Items[12].Attributes["class"] = "lblactive blink_me";
                    break;


            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
            }
            
            return dt1;
        }
        private DataTable HiddenSameData1(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string syspon = dt1.Rows[0]["syspon"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode & dt1.Rows[j]["syspon"].ToString() == syspon)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    syspon = dt1.Rows[j]["syspon"].ToString();

                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["pono"] = "";
                    dt1.Rows[j]["supname"] = "";


                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    syspon = dt1.Rows[j]["syspon"].ToString();

                }


            }

            return dt1;
        }
        private DataTable HiddenSameData2(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string syspon = dt1.Rows[0]["syspon"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["syspon"].ToString() == syspon)
                {
                    syspon = dt1.Rows[j]["syspon"].ToString();
                    dt1.Rows[j]["pono"] = "";
                    dt1.Rows[j]["supname"] = "";

                }

                else
                    syspon = dt1.Rows[j]["syspon"].ToString();
            }

            return dt1;
        }

        private void Data_Bind(string gv, DataTable dt1)
        {
            DataTable dt = dt1.Copy();

            switch (gv)
            {
                case "gvprobrec":
                    if (dt.Rows.Count == 0)
                        return;
                    this.gvprobrec.DataSource = HiddenSameData(dt);
                    this.gvprobrec.DataBind();
                    break;
                case "gvReqCheck":
                    if (dt.Rows.Count == 0)
                        return;
                    this.gvReqCheck.DataSource = HiddenSameData(dt);
                    this.gvReqCheck.DataBind();
                    for (int i = 0; i < gvReqCheck.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvReqCheck.Rows[i].FindControl("lblgvprobno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvReqCheck.Rows[i].FindControl("gvreqcheckDelete");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }
                    break;
                case "gvReqAuth":
                    if (dt.Rows.Count == 0)
                        return;
                    this.gvReqAuth.DataSource = HiddenSameData(dt);
                    this.gvReqAuth.DataBind();

                    for (int i = 0; i < gvReqAuth.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvReqAuth.Rows[i].FindControl("lblgvprobno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvReqAuth.Rows[i].FindControl("gvaprbtnDelReqChk");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }

                    break;
                case "gvprobapp":
                    this.gvprobapp.DataSource = HiddenSameData(dt);
                    this.gvprobapp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvprobapp.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvprobapp.Rows[i].FindControl("lblgvprobno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvprobapp.Rows[i].FindControl("btnDelReqRev");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }

                    break;
                case "gvcscrte":
                    this.gvcscrte.DataSource = HiddenSameData(dt);
                    this.gvcscrte.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvcscrte.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvcscrte.Rows[i].FindControl("lblgvcentrid")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvcscrte.Rows[i].FindControl("btnDelReqApp");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }
                    break;
                case "gvCsCheck":
                    this.gvCsCheck.DataSource = HiddenSameData(dt);
                    this.gvCsCheck.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvCsCheck.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvCsCheck.Rows[i].FindControl("lblgvcentrid")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvCsCheck.Rows[i].FindControl("btnCSRev");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + "Next";
                    }
                    break;
                case "gvcsaprv":
                    this.gvcsaprv.DataSource = HiddenSameData(dt);
                    this.gvcsaprv.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvcsaprv.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvcsaprv.Rows[i].FindControl("lblgvcentrid")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvcsaprv.Rows[i].FindControl("btnDelCSNext");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + "Check";
                    }



                    break;
                case "gvprobass":
                    this.gvprobass.DataSource = HiddenSameData2(dt);
                    this.gvprobass.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    break;
                case "gvprobassapp":
                    this.gvprobassapp.DataSource = dt;
                    this.gvprobassapp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    break;
                case "gvprobbill":
                    this.gvprobbill.DataSource = HiddenSameData(dt);
                    this.gvprobbill.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvCosting":
                    this.gvCosting.DataSource = HiddenSameData(dt);
                    this.gvCosting.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvPoIn":
                    this.gvPoIn.DataSource = HiddenSameData(dt);
                    this.gvPoIn.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    for (int i = 0; i < gvPoIn.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvPoIn.Rows[i].FindControl("lblgvcentridim")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvPoIn.Rows[i].FindControl("btnDelPoIn");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }
                    break;
                case "gvImportApp":
                    this.gvImportApp.DataSource = HiddenSameData1(dt);
                    this.gvImportApp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvImportApp.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvImportApp.Rows[i].FindControl("lblsyspondel")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvImportApp.Rows[i].FindControl("btnDelPoApp");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }
                    break;

                    //case "grvComp":
                    //    this.grvComp.DataSource = (dt);
                    //    this.grvComp.DataBind();
                    //    if (dt.Rows.Count == 0)
                    //        return;
                    //    break;
            }
        }
        protected void gvprobrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyImportPrint");

                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";

                hlnkgvgvmrfno.NavigateUrl = "~/F_10_Procur/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno;


                //Session["Report1"] = gvprobrec;
                //((HyperLink)this.gvprobrec.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void gvReqCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnUpdate");

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import";
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";
                hlink2.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=FxtAstAuth&comcod=" + comcod + "&actcode=" + pactcode + "&genno=" + reqno;
                hlink3.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=ReqEdit&comcod=" + comcod + "&actcode=" + pactcode + "&genno=" + reqno;

                Session["Report1"] = gvprobapp;
                ((HyperLink)this.gvReqCheck.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvReqAuth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEdit");

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import";
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";


                hlink2.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=ReqReview&comcod=" + comcod + "&actcode=" + pactcode + "&genno=" + reqno;

                hlink3.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=ReqEdit&comcod=" + comcod + "&actcode=" + pactcode + "&genno=" + reqno;

                //Session["Report1"] = gvprobapp;
                //((HyperLink)this.gvReqCheck.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvprobapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import";
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";


                hlink2.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=FxtAstApproval&comcod=" + comcod + "&actcode=" + pactcode + "&genno=" + reqno;

                //Session["Report1"] = gvprobapp;
                //((HyperLink)this.gvprobapp.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvprobass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();

                HyperLink hlinkImApp = (HyperLink)e.Row.FindControl("lnkbtnImApp");

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                HyperLink hlinkReqP = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlinkCsP = (HyperLink)e.Row.FindControl("HyPrintCS");
                HyperLink HyImportApp = (HyperLink)e.Row.FindControl("HyImportApp");

                //CheckBox chkack = (CheckBox)e.Row.FindControl("gvCheckbx");


                string supcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "supcode")).ToString();
                string msrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string syspon = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "syspon")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                //string syspon1 = "";
                //if (chkack.Checked == true)
                //{
                //    syspon1 += Convert.ToString(DataBinder.Eval(e.Row.DataItem, "syspon")).ToString();


                //}

                hlinkImApp.NavigateUrl = "~/F_10_Procur/ImportApproval?Type=Approved&comcod=" + comcod + "&genno=" + reqno + "&sircode=" + supcode + "&dayid=" + syspon;

                hlinkImApp.ToolTip = "PO Eidt";

                HyImportApp.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ImportApp&comcod=" + comcod + "&reqno=" + reqno + "&supcode=" + supcode + "&msrno=" + msrno + "&dayid=" + syspon;
                hlinkReqP.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";
                hlinkCsP.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;
                hlink1.NavigateUrl = "~/F_09_Commer/LCOpening?Type=Open&genno=" + reqno + "&actcode=" + actcode + "&ssircode=" + supcode + "&dayid=" + syspon;

                HyImportApp.ToolTip = "L/C Opening";
                //Session["Report1"] = gvprobass;
                //((HyperLink)this.gvprobass.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";











            }

        }
        protected void gvcscrte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint");
                hlink1.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Create&comcod=" + comcod + "&genno=" + reqno + "&ReqType=Import";
                hlink2.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";
                //Session["Report1"] = gvcscrte;
                //((HyperLink)this.gvcscrte.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }



        }
        protected void gvCsCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint");
                hlink1.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Check&comcod=" + comcod + "&genno=" + reqno + "&ReqType=Import";
                hlink2.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";
                //Session["Report1"] = gvcscrte;
                //((HyperLink)this.gvcscrte.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }



        }
        protected void gvcsaprv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlinkCS = (HyperLink)e.Row.FindControl("HyPrintCS");
                //HyperLink HypNote = (HyperLink)e.Row.FindControl("HypNote");

                string comcod = this.GetCompCode();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                //string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnimport");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkImporAppr");
                HyperLink hlink4 = (HyperLink)e.Row.FindControl("HyInprPrint");

                hlink1.Visible = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqaprv")).ToString() == "" ? true : false;
                hlink2.Visible = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lcopn")).ToString() == "" && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqaprv")).ToString() != "" && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "imapp")).ToString() == "" ? true : false;
                hlink3.Visible = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lcopn")).ToString() == "" && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqaprv")).ToString() != "" ? true : false;


                //checked ='Ok' and csstus='Ok' and approved='Ok' and lcopn='' and  reqaprv<>'' 

                hlink1.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Approved&comcod=" + comcod + "&genno=" + reqno + "&ReqType=Import";
                hlink2.NavigateUrl = "~/F_10_Procur/ImportApproval?Type=Entry&comcod=" + comcod + "&genno=" + reqno + "&sircode=";
                hlink3.NavigateUrl = "~/F_10_Procur/ImportApproval?Type=Approved&comcod=" + comcod + "&genno=" + reqno + "&sircode=";
                hlink4.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";
                Session["Report1"] = gvcsaprv;
                ((HyperLink)this.gvcsaprv.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                hlinkCS.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;
                //HypNote.NavigateUrl = "~/NOAFormat.aspx?Type=Entry&comcod=" + comcod + "&reqno=" + reqno;


            }

        }
        protected void gvprobassapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                HyperLink hlinkedit = (HyperLink)e.Row.FindControl("HypEditLc");
                string storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "storid")).ToString();
               
                hlinkedit.NavigateUrl = "~/F_09_Commer/LCOpening?Type=Open&genno=" + reqno + "&actcode=" + actcode + "&ssircode=&dayid=";

                hlink1.NavigateUrl = "~/F_09_Commer/LcReceive?Type=Entry&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=";
                hlink1.ToolTip = "Lc Received";


                //Session["Report1"] = gvprobassapp;
                //((HyperLink)this.gvprobassapp.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }



        protected void gvprobbill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string rcvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rcvno")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "storid")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                HyperLink LinkPrint = (HyperLink)e.Row.FindControl("LinkPrint");
                HyperLink editbtn = (HyperLink)e.Row.FindControl("HypPreEdit");
                hlink1.NavigateUrl = "~/F_09_Commer/LcQcRecv?Type=Entry&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + rcvno;
                editbtn.NavigateUrl = "~/F_09_Commer/LcReceive?Type=Edit&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + rcvno;
                LinkPrint.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=LCRecPrint&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + rcvno;

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




        protected void gvCosting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HypbtnEdit");
                HyperLink hlinkprint = (HyperLink)e.Row.FindControl("HypbtnPrint");
                HyperLink btnforward = (HyperLink)e.Row.FindControl("lnkbtnForward");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string grrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grrno")).ToString();
                string storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "storid")).ToString();

                hlink1.NavigateUrl = "~/F_09_Commer/LcQcRecv?Type=Edit&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + grrno;
                btnforward.NavigateUrl = "~/F_09_Commer/LCCostingDetails?Type=Entry&comcod=" + comcod + "&actcode=" + actcode;
                hlinkprint.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=LCQcPrint&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + grrno;
                Session["Report1"] = gvCosting;
                ((HyperLink)this.gvCosting.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void btnDelRec_Click(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string actcode = ((Label)this.gvCosting.Rows[index].FindControl("lblgvlccode")).Text.ToString();
            string grrno = ((Label)this.gvCosting.Rows[index].FindControl("lgvgrrno")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_LC_INTERFACE", "LC_INTERFACE_REVARSE", "COST", actcode, grrno);
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "GRR Reverse Successfull";
            }
        }



        protected void btnDelQC_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string actcode = ((Label)this.gvprobbill.Rows[index].FindControl("lblgvlccode")).Text.ToString();
            string rcvno = ((Label)this.gvprobbill.Rows[index].FindControl("lgvrcvNo")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_LC_INTERFACE", "LC_INTERFACE_REVARSE", "QC", actcode, rcvno);
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "This QC Reverse Sucessfully";

            }
        }

        protected void btnDelRcv_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string actcode = ((Label)this.gvprobassapp.Rows[index].FindControl("lblgvcentrid")).Text.ToString();
            string reqno = ((Label)this.gvprobassapp.Rows[index].FindControl("lblgReq")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_LC_INTERFACE", "LC_INTERFACE_REVARSE", "RCV", actcode, reqno);
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "This Received Reverse Sucessfully";
            }
        }

        //protected void btnDelReq_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();


        //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    int index = row.RowIndex;
        //    string reqChecked = ((Label)this.gvcscrte.Rows[index].FindControl("lblReqCkapp")).Text.ToString();
        //    string reqno = ((Label)this.gvcscrte.Rows[index].FindControl("lblgvcentrid")).Text.ToString();


        //    bool result = accData.UpdateTransInfo(comcod, "SP_LC_INTERFACE", "LC_INTERFACE_REVARSE", "DelReqApp", reqno);
        //    if (result == true)
        //    {
        //        this.lblmsg.Text = "This Received Reverse Sucessfully";
        //    }
        //}

        protected void btnDelReqApp_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
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



            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEREQAPPROVAl", Reqno, userid, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Approval Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Approval Delete", "Order No: ", Reqno);
        }

        protected void btnDelPoIn_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Reqno = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string userid = hst["usrid"].ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEPO", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Review Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Approval Delete", "Order No: ", Reqno);
        }

        protected void btnDelPoApp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string pon = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            if (pon.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string userid = hst["usrid"].ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEPOAUTH", pon, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Review Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Approval Delete", "Order No: ", pon);
        }

        //protected void lnkbtnPrintCS_Click(object sender, EventArgs e)
        //{

        //}
        //protected void lnkbtnPrintNapp_Click(object sender, EventArgs e)
        //{

        //}
        protected void gvPoIn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string comcod = this.GetCompCode();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string supcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "supcode")).ToString();
                string syspono = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "syspon")).ToString();

                HyperLink hlinkReqP = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlinkCsP = (HyperLink)e.Row.FindControl("HyPrintCS");

                HyperLink hlink4 = (HyperLink)e.Row.FindControl("lnkbtnimportim");
                //HyperLink hlink5 = (HyperLink)e.Row.FindControl("lnkImporApprim");
                hlink4.Visible = Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "imapp")).ToString()) == "" && Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approved")).ToString()) == "Ok" ? true : false;
                //hlink5.Visible = Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "imapp")).ToString()) == "Ok" && Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approved")).ToString()) == "Ok" ? false : true;

                hlink4.NavigateUrl = "~/F_10_Procur/ImportApproval?Type=Entry&comcod=" + comcod + "&genno=" + reqno + "&sircode=" + supcode + "&dayid=" + syspono;
                //hlink5.NavigateUrl = "~/F_10_Procur/ImportApproval?Type=Approved&comcod=" + comcod + "&genno=" + reqno + "&sircode=" + supcode + "&dayid=";


                hlinkReqP.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";
                hlinkCsP.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;



                Session["Report1"] = gvcsaprv;
                //((HyperLink)this.gvcsaprv.HeaderRow.FindControl("hlbtntbCdataExelim")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
        }
        protected void gvImportApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string comcod = this.GetCompCode();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string supcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "supcode")).ToString();
                string msrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString();
                string syspon = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "syspon")).ToString();

                HyperLink hlinkPO = (HyperLink)e.Row.FindControl("HyInprPrintPO");
                HyperLink hlinkReqP = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlinkCsP = (HyperLink)e.Row.FindControl("HyPrintCS");

                HyperLink hlink4 = (HyperLink)e.Row.FindControl("lnkbtnimportim");
                HyperLink hlink5 = (HyperLink)e.Row.FindControl("lnkImporApprim");
                hlink4.Visible = Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "imapp")).ToString()) == "" && Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approved")).ToString()) == "Ok" ? true : false;
                hlink5.Visible = Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "imapp")).ToString()) == "Ok" && Convert.ToString(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approved")).ToString()) == "Ok" ? false : true;

                hlink4.NavigateUrl = "~/F_10_Procur/ImportApproval?Type=Entry&comcod=" + comcod + "&genno=" + reqno + "&sircode=" + supcode + "&dayid=" + syspon;
                hlink5.NavigateUrl = "~/F_10_Procur/ImportApproval?Type=Approved&comcod=" + comcod + "&genno=" + reqno + "&sircode=" + supcode + "&dayid=" + syspon;

                hlinkPO.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ImportApp&comcod=" + comcod + "&reqno=" + reqno + "&supcode=" + supcode + "&msrno=" + msrno + "&dayid=" + syspon;

                hlinkReqP.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Import&AppType=YES";
                hlinkCsP.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;



                Session["Report1"] = gvcsaprv;
                //((HyperLink)this.gvcsaprv.HeaderRow.FindControl("hlbtntbCdataExelim")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
        }

        protected void gvaprbtnDelReqChk_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
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


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEREQCHK", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Checked Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Checked Delete", "Order No: ", Reqno);
        }

        protected void btnDelReqRev_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
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

        protected void btnCSRev_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Type = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVCSPART", Reqno, Type, userid, Date, "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CS Check Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Approval Delete", "Order No: ", Reqno);
        }

        protected void btnDelCSNext_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
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
        protected void gvreqcheckDelete_Click(object sender, EventArgs e)
        {
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


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETE_REQUISITION", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Delete", "Order No: ", Reqno);
        }

        protected void LbtnForwardtoLocal_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvPoIn.Rows[rowindex].FindControl("lblgvcentridim")).Text.Trim();
            DataSet ds = accData.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_REQ_WISE_MATERIAL_LIST", reqno, "", "", "", "");

            Session["tblMatList"] = ds;

            GV_MatList.DataSource = ds.Tables[0];
            GV_MatList.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showMvToLclModal();", true);
        }

        protected void btnMvToLocal_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Session["tblMatList"];

            foreach (GridViewRow gvrow in GV_MatList.Rows)
            {
                CheckBox check = (CheckBox)gvrow.FindControl("chkMat");

                if (check != null && check.Checked)
                {
                    ds.Tables[0].Rows[gvrow.RowIndex]["checked"] = 1;
                }
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string postedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string storeid = this.DDlStore.SelectedValue.ToString();
            DataView dataView = ds.Tables[0].DefaultView;
            dataView.RowFilter = ("checked=1");

            DataSet ds2 = new DataSet("dsMatList");
            ds2.Tables.Add(dataView.ToTable());
            ds2.Tables[0].TableName = "tbl0";
            string comcod = this.GetCompCode();
            var temp = ds2.GetXml();
            var isSuccessful = accData.UpdateXmlTransInfo(comcod, "SP_LC_INTERFACE", "MOVE_MATERIALS_TO_LOCAL", ds2, null, null, userid, Terminal, Sessionid, postedDate, storeid);

            if (!isSuccessful)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to move materials to local');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeMvToLclModal();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully moved materials to local');", true);
        }
        protected void LbtnLcOpening_Click(object sender, EventArgs e)
        {
            string syspon = "";
            string reqno = "";
            string supcode = "";

            foreach (GridViewRow gvrow in gvprobass.Rows)
            {
                CheckBox check = (CheckBox)gvrow.FindControl("gvCheckbx");
                //Label pono = (Label)gvrow.FindControl("lgpsyspon");
                string pono = ((Label)gvrow.FindControl("lblsyspon")).Text.Trim();

                //string actcode = ((Label)gvrow.FindControl("lblsyspon")).Text.Trim();


                if (check != null && check.Checked)
                {
                    syspon += pono;
                    reqno += ((Label)gvrow.FindControl("lblgvreqno")).Text.Trim();
                    supcode = ((Label)gvrow.FindControl("lblvencode")).Text.Trim();
                }
            }
            //string URL = "~/F_09_Commer/LCOpening?Type=Open&genno=" + reqno + "&actcode=&ssircode=" + supcode + "&dayid=" + syspon;
            //Response.Write("<script>window.open ('"+ URL + "','_blank');</script>");
            //hlink1.NavigateUrl = "~/F_09_Commer/LCOpening?Type=Open&genno=" + reqno + "&actcode=" + actcode + "&ssircode=" + supcode + "&dayid=" + syspon;
            Response.Redirect("~/F_09_Commer/LCOpening?Type=Open&genno=" + reqno + "&actcode=000000000000&ssircode=" + supcode + "&dayid=" + syspon + "");
        }
        protected void lnkSendMail_Click(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)ViewState["tblImport"];

            //DataView Tempdv = new DataView();
            //Tempdv = dt.DefaultView;
            //Tempdv.RowFilter = ("imapp='' and approved='Ok' and supwiseitm<>'0' ");

         //   DataTable dt2 = Tempdv.ToTable();

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            //string comcod = this.GetCompCode();
            //string mORDERNO = dt2.Rows[index]["syspon"].ToString();
            //string supplier = dt2.Rows[index]["supcode"].ToString();
            //string msrno = dt2.Rows[index]["msrno"].ToString();
            //string reqno = dt2.Rows[index]["reqno"].ToString();

            string comcod = this.GetCompCode();
            string mORDERNO = ((Label)gvImportApp.Rows[index].FindControl("lblsyspondel")).Text;
            string supplier = ((Label)gvImportApp.Rows[index].FindControl("lblgvImportAppSupCode")).Text;
            string msrno = ((Label)gvImportApp.Rows[index].FindControl("lblgvImportAppMsrno")).Text;
            string reqno = ((Label)gvImportApp.Rows[index].FindControl("lblgvcentridim")).Text;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "LoadRdlcVIewer('" + comcod + "', '" + mORDERNO + "','" + supplier + "','" + msrno + "','" + reqno + "');", true);
        }
    }
}