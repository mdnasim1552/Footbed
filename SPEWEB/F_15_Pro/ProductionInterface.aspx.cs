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
using System.Web.Services;
using System.Web.Script.Services;

namespace SPEWEB.F_15_Pro
{
    public partial class ProductionInterface : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION Smartface";//
                if (this.ddlLCName.Items.Count == 0)
                {
                    this.GetSesson();
                }
                this.txtFDate.Text = System.DateTime.Today.AddDays(-15).ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();
                HyperLink HyPPlan = (HyperLink)this.HyPPlan as HyperLink;
                HyPPlan.NavigateUrl = "~/F_01_Mer/RptOrdAppSheet?Type=OrdPlan";
                this.ShowPlanCount();
                string Type = this.Request.QueryString["Type"].ToString();
                this.PnlInt.Visible = true;
                if (Type == "FG")
                {
                    //this.HyperLink3.NavigateUrl = "~/F_15_Pro/EntryProTarget.aspx";
                    this.HyperLink1.NavigateUrl = "~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode=&sircode=";
                }
                else
                {
                    //this.HyperLink3.NavigateUrl= "~/F_15_Pro/EntryProTarget.aspx";
                    this.HyperLink1.NavigateUrl = "~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode=&sircode=";
                    // this.HyperLink4.Visible = true;

                }
                txtdate_TextChanged(null, null);
                string comcod = GetCompCode();
                if (comcod == "8305")
                {
                    this.ColoumVisiable();
                }
                //this.RadioButtonList1_SelectedIndexChanged(null, null);
                //  
                
            }
            
        }
        private void ShowPlanCount()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "RPTPENBOMMAT", Date2, "", "", "", "", "", "", "", "");

            this.HyPPlan.Text = "10"; //ds1.Tables[0].Rows.Count.ToString();

        }
        private void ColoumVisiable()
        {
            this.gvProdInfo.Columns[4].Visible = true;
            this.grvProReq.Columns[4].Visible = true;
            //this.grvProIssue.Columns[4].Visible = true;
            this.grvReqAprvl.Columns[4].Visible = true;
            this.grvQCEntry.Columns[4].Visible = true;
            //this.gvstorec.Columns[4].Visible = true;
            this.grvComp.Columns[4].Visible = true;
            //this.gvProdInfo.Columns[4].Visible = true;
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {

            txtdate_TextChanged(null, null);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.ProdRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ProdRequRpt();
        }
        private void ProdRequRpt()
        {
            try
            {
                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                    string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
                string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string LCcode = (this.ddlLCName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlLCName.SelectedValue.ToString();
                string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "RPT_PRODUCTION_INTERFACE", Date1, Date2, usrid, LCcode, season, "", "", "");
                if (ds1 == null)
                    return;

                this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[7].Rows[0]["totalplan"] + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Production Plan</div></div></div>";

                this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[7].Rows[0]["reqty"] + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Requision</div></div></div>";
                this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[7].Rows[0]["reqaprv"] + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Requision Approve </div></div></div>";
                this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[7].Rows[0]["isueqty"] + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Material Issue</div></div></div>";

                this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[7].Rows[0]["proqty"] + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Production Process</div></div></div>";

                this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[7].Rows[0]["flissue"] + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> Floor Issue </div></div></div>";

                this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[7].Rows[0]["qcqty"] + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'>QC</div></div></div>";

                this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[7].Rows[0]["strrcvqty"] + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> Store Receive</div></div></div>";
                this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[7].Rows[0]["comqty"] + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> Complete</div></div></div>";



                DataTable dt = new DataTable();

                DataView dv = new DataView();
                dt = ((DataTable)ds1.Tables[0]).Copy();
                this.Data_Bind("gvProdInfo", dt);

                //Requsition
                dt = ((DataTable)ds1.Tables[0]).Copy();
                dv = dt.DefaultView;
                dv.RowFilter = ("cstatus='Requisition' ");
                this.Data_Bind("grvProReq", dv.ToTable());



                dt = ((DataTable)ds1.Tables[3]).Copy();
                if (dt.Rows.Count > 0)
                {

                    ViewState["tblReqAppList"] = dt;

                    var newDt = ds1.Tables[3].AsEnumerable()
                                  .GroupBy(r => r.Field<string>("preqno"))
                                  .Select(g =>
                                  {
                                      var row = ds1.Tables[3].NewRow();

                                      row["preqno"] = g.Key;
                                      row["procode"] = g.Key;
                                      row["procdesc"] = "Click For Details Breakdown";
                                      row["pbdate"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["pbdate"];
                                      row["plnno"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["plnno"];
                                      row["comcod"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["comcod"];
                                      row["batchcode"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["batchcode"];
                                      row["batchdesc"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["batchdesc"];
                                      row["article"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["article"];
                                      row["orderno"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["orderno"];
                                      row["odayid"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["odayid"];
                                      row["itmcount"] = g.Sum(r => r.Field<int>("itmcount"));
                                      row["reqtype"] = "";
                                      row["flag"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["flag"];
                                      row["rsqty"] = g.Sum(r => r.Field<decimal>("rsqty"));
                                      row["demanddate"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["demanddate"];
                                      return row;
                                  }).CopyToDataTable();

                    ViewState["tblreqappviewlist"] = newDt;
                    this.Data_Bind("grvReqAprvl", newDt);
                }




                //issue
                dt = ((DataTable)ds1.Tables[1]).Copy();
                this.Data_Bind("grvProIssue", dt);



                //Production process
                dt = ((DataTable)ds1.Tables[2]).Copy();
                if (dt.Rows.Count > 0)
                {

                    dv = dt.DefaultView;
                    dv.RowFilter = ("qcstatus='True' and balqty<>0");

                    ViewState["tblallprocess"] = dv.ToTable();
                    var dtprocess = new DataTable();
                    if (dv.ToTable().Rows.Count > 0)
                    {

                         dtprocess = dv.ToTable().AsEnumerable()
                                      .GroupBy(r => r.Field<string>("preqno"))
                                      .Select(g =>
                                      {
                                          var row = dv.ToTable().NewRow();

                                          row["preqno"] = g.Key;
                                          row["tprostep"] = g.Key;
                                          row["tprostepdesc"] = "Click For Details Breakdown";
                                          row["odayid"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["odayid"];
                                          row["preqno1"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["preqno1"];
                                          row["comcod"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["comcod"];
                                          row["rdate"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["rdate"];
                                          row["mlccod"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["mlccod"];
                                          row["mlcdesc"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["mlcdesc"];
                                          row["styleid"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["styleid"];
                                          row["colorid"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["colorid"];
                                          row["styledesc"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["styledesc"];
                                          row["orderno"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["orderno"];
                                          row["article"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["article"];
                                          row["trialorder"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["trialorder"];
                                          row["qcstatus"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["qcstatus"];
                                          row["flag"] = g.Where(r => r["preqno"] == g.Key.ToString()).First()["flag"];
                                          row["qty"] = g.Where(r => r["preqno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("qty"));
                                          row["outqty"] = g.Where(r => r["preqno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("outqty"));
                                          row["balqty"] = g.Where(r => r["preqno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("balqty"));
                                          row["rejectionqty"] = g.Where(r => r["preqno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("rejectionqty"));
                                          row["repairqty"] = g.Where(r => r["preqno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("repairqty"));
                                          row["manpower"] = g.Where(r => r["preqno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("manpower"));
                                          row["rcvqty"] = g.Where(r => r["preqno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("rcvqty"));
                                          row["permission"] = "false";
                                          return row;
                                      }).CopyToDataTable();
                    }

                    ViewState["tblheadprocess"] = dtprocess;

                    this.Data_Bind("gvprocess", dtprocess);
                }
                //iqc check
                dt = ((DataTable)ds1.Tables[2]).Copy();
                dv = dt.DefaultView;
                dv.RowFilter = ("qcstatus='false' ");

                this.Data_Bind("grvQCEntry", dv.ToTable());

                //Ready for store receive
                dt = ((DataTable)ds1.Tables[4]).Copy();
                dv = dt.DefaultView;
                dv.RowFilter = ("qty<>rcvqty ");
                this.Data_Bind("gvstorec", dt);

                ////Ready for Qc
                //dt = ((DataTable)ds1.Tables[4]).Copy();
                //this.Data_Bind("gvstorec", dt);

                dt = ((DataTable)ds1.Tables[5]).Copy();
                this.Data_Bind("grvComp", dt);

                dt = ((DataTable)ds1.Tables[6]).Copy();
                this.Data_Bind("grvFlissue", dt);
            }
            catch (Exception ex)
            {

            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "0":
                    this.pnlallProd.Visible = true;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlFlorIssue.Visible = false;
                    this.PnlReqApp.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = true;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlFlorIssue.Visible = false;
                    this.PnlReqApp.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PnlReqApp.Visible = true;
                    this.PanelIssue.Visible = false;
                    this.PnlFlorIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PnlReqApp.Visible = false;
                    this.PanelIssue.Visible = true;
                    this.pnlProProcs.Visible = false;
                    this.PnlFlorIssue.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;

                case "4":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = true;
                    this.PnlFlorIssue.Visible = false;
                    this.PnlReqApp.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
                case "5":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlFlorIssue.Visible = true;
                    this.PnlReqApp.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;

                case "6":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlFlorIssue.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlReqApp.Visible = false;
                    this.PnlQC.Visible = true;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;
                case "7":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlFlorIssue.Visible = false;
                    this.PnlReqApp.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = true;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    break;
                case "8":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlFlorIssue.Visible = false;
                    this.PnlReqApp.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = true;
                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    break;



            }
        }

        protected void grvProReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                // string pbmno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pbmno")).ToString();
                //

                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string slnum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slnum")).ToString();
                string plnno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prono")).ToString();
                string tardaet = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "tardate")).ToString("dd-MMM-yyyy");
                string odayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "odayid")).ToString();

                hlink1.NavigateUrl = "~/F_15_Pro/ProdReq?Type=Entry&actcode=" + mlccod + "&genno=" + plnno.Trim() + slnum + "&date=" + tardaet+"&dayid="+ odayid;

            }
        }
        protected void grvProIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlinkissue = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
           
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string reqtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();
                string Type = this.Request.QueryString["Type"].ToString();
                //string tardate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "tardate")).ToString("dd-MMM-yyyy");
                //string slnum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slnum")).ToString();
                // string plnno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prono")).ToString();
                string pbdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "pbdate")).ToString("dd-MMM-yyyy");

                hlink2.NavigateUrl = "~/F_15_Pro/Print?Type=proreq&dprno=" + preqno + "&pbmno=" + actcode + "&tardate=" + "" + "&slnum=" + "" + "&plnno=" + "" + "&pbdate=" + pbdate;
                if (this.GetCompCode() == "5305")
                {
                    hlink1.NavigateUrl = "~/F_11_RawInv/MaterialIssueSuplierWise?Type=Entry&genno=" + preqno + "&actcode=" + actcode + "&reptype=" + reqtype;

                }
                else
                {
                    hlink1.NavigateUrl = "~/F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=" + preqno + "&actcode=" + actcode + "&reptype=" + reqtype;

                }
                hlinkissue.NavigateUrl= "~/F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=" + preqno + "&actcode=" + actcode + "&reptype=" + reqtype;
                if (reqtype == "ADDITIONAL")
                {
                    e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                }
                else if (reqtype == "COMMON")
                {
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;

                }
                else if (reqtype == "RECUTTING")
                {
                    e.Row.BackColor = System.Drawing.Color.LightSeaGreen;

                }


            }
        }
        protected void grvReqAprvl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hlink1 = (LinkButton)e.Row.FindControl("LnkBtnApprove");
                HyperLink hyprint = (HyperLink)e.Row.FindControl("HyInprPrint");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string batchcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchcode")).ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string procode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "procode")).ToString();
                string reqdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "pbdate")).ToString("dd-MMM-yyyy");
                string reqtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();

                if (procode.Substring(0, 3).ToString() == "DPR")
                {
                    hlink1.Enabled = false;
                    hlink1.Visible = false;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
                //string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                hyprint.NavigateUrl = "~/F_15_Pro/Print?Type=reqapp&actcode=" + batchcode + "&genno=" + preqno + "&process=" + procode + "&reqdate=" + reqdate + "&reqtype=" + reqtype;

            }
        }
        protected void grvQCEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "tprostep")).ToString();
                string odayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "odayid")).ToString();

                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "rdate")).ToString("dd-MMM-yyyy");
                string Type = this.Request.QueryString["Type"].ToString();
                hlink1.NavigateUrl = "~/F_15_Pro/ProductionProcess?Type=ProQc&actcode=" + mlccod + "&genno=" + preqno + "&sircode=" + sircode + "&date=" + date+"&dayid="+ odayid;
            }
        }
        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvProdInfo":
                    this.gvProdInfo.DataSource = dt;
                    this.gvProdInfo.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "grvProReq":
                    this.grvProReq.DataSource = (dt);
                    this.grvProReq.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "grvProIssue":
                    this.grvProIssue.DataSource = (dt);
                    this.grvProIssue.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvprocess":
                    this.gvprocess.DataSource = (dt);
                    this.gvprocess.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "grvQCEntry":
                    this.grvQCEntry.DataSource = (dt);
                    this.grvQCEntry.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvstorec":
                    this.gvstorec.DataSource = (dt);
                    this.gvstorec.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "grvComp":
                    this.grvComp.DataSource = (dt);
                    this.grvComp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "grvReqAprvl":
                    this.grvReqAprvl.DataSource = (dt);
                    this.grvReqAprvl.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "grvFlissue":
                    if (dt.Rows.Count == 0)
                        return;
                    this.grvFlissue.DataSource = (dt);
                    this.grvFlissue.DataBind();

                    break;

            }
        }

        protected void LbtnRep_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = false;
            this.PnlRep.Visible = true;
            this.PnlSet.Visible = false;
            string Type = this.Request.QueryString["Type"].ToString();

            if (Type == "FG")
            {
                this.PnlRepFg.Visible = true;
            }
            else
            {
                this.PnlRepSemiFg.Visible = true;
            }
        }

        protected void LbtnInt_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = true;
            this.PnlRep.Visible = false;
            this.PnlSet.Visible = false;
            string Type = this.Request.QueryString["Type"].ToString();


        }

        protected void LbtnSetting_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = false;
            this.PnlRep.Visible = false;
            this.PnlSet.Visible = true;
            string Type = this.Request.QueryString["Type"].ToString();

            if (Type == "FG")
            {
                this.PnlSetFG.Visible = true;
            }
            else
            {
                this.PnlSetSemiFg.Visible = true;
            }
        }


        protected void gvProdInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyOrderPrint");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                //string prono = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prono")).ToString();
                //string Type = this.Request.QueryString["Type"].ToString();

                //hlink1.NavigateUrl = "~/F_13_ProdMon/Print.aspx?Type=" + Type + "&pbmno=" + prono + "&option=probudget";

            }
        }
        protected void gvstorec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                // string Type = this.Request.QueryString["Type"].ToString();

                hlink2.NavigateUrl = "~/F_15_Pro/ProdEntry?Type=Entry&actcode=" + actcode + "&genno=" + preqno+"&sircode=";

            }
        }



        protected void gvprocess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hypaddreq = (HyperLink)e.Row.FindControl("HypLinkAdditionReq");
                HyperLink hypcommatreq = (HyperLink)e.Row.FindControl("HypCommonMatreq");
                HyperLink hyprecutMatReq = (HyperLink)e.Row.FindControl("HypRecutMatReq");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string odayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "odayid")).ToString();

                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "tprostep")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "rdate")).ToString("dd-MMM-yyyy");
                bool permission = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "permission"));

                //   string Type = this.Request.QueryString["Type"].ToString();            
                if (sircode.Substring(0, 3).ToString() == "DPR")
                {
                    hlink2.Enabled = false;
                    hlink2.Visible = false;
                    hypaddreq.Visible = true;
                    hypcommatreq.Visible = true;
                    hyprecutMatReq.Visible = true;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
                hlink2.NavigateUrl = "~/F_15_Pro/ProductionProcess?Type=ProTransfer&actcode=" + mlccod + "&genno=" + preqno + "&sircode=" + sircode + "&date=" + date+"&dayid="+ odayid;
                hypaddreq.NavigateUrl = "~/F_15_Pro/AddProdReq?Type=addreq&actcode=" + mlccod + "&genno=" + preqno+"&dayid="+ odayid;
                hypcommatreq.NavigateUrl = "~/F_15_Pro/AddProdReq?Type=commonreq&actcode=" + mlccod + "&genno=" + preqno + "&dayid=" + odayid;
                hyprecutMatReq.NavigateUrl = "~/F_15_Pro/AddProdReq?Type=ReCutMatReq&actcode=" + mlccod + "&genno=" + preqno + "&dayid=" + odayid;

                if (permission == false)
                {
                hlink2.NavigateUrl = "#";
                hlink2.Attributes.Add("onclick", "showContentFail('Not Eligible Production due to Permission')");
                hlink2.Target = "_self";
                }

            }
        }

        protected void LnkBtnApprove_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = ((Label)this.grvReqAprvl.Rows[index].FindControl("lgvmlccod")).Text.ToString();
            string reqno = ((Label)this.grvReqAprvl.Rows[index].FindControl("lgvpreqno")).Text.ToString();
            string process = ((Label)this.grvReqAprvl.Rows[index].FindControl("lblProc")).Text.ToString();
            string reqtype = ((Label)this.grvReqAprvl.Rows[index].FindControl("lgrvReqtype")).Text.ToString();
            string demandate = ((Label)this.grvReqAprvl.Rows[index].FindControl("lgrvdmndDate")).Text.ToString();
            DataTable mainlist = (DataTable)ViewState["tblReqAppList"];
            DataRow[] deptrow = mainlist.Select("preqno = '" + reqno + "' and procode ='" + process + "'");

            if (Convert.ToBoolean(deptrow[0]["permission"]) == false && process!="000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + deptrow[0]["procdesc"] + " Permission missing');", true);

                return;
            }



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "GET_DEPARTMENT_WISE_REQUISITION", mlccod, reqno, process, reqtype, demandate, "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblReqitm"] = ds1.Tables[0];
            this.gvReqitem.DataSource = ViewState["tblReqitm"];
            this.gvReqitem.DataBind();
            this.lblPreqno.Text = reqno;
            this.lblmlccod.Text = mlccod;
            this.lbldept.Text = process;
            this.lblreqdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["reqdate"]).ToString("dd-MMM-yyyy");
            this.lblreqtype.Text = ((Label)this.grvReqAprvl.Rows[index].FindControl("lgrvReqtype")).Text.ToString();
            //---------basic information-----
            if (ds1.Tables[1].Rows.Count == 0)
                return;
            this.TotalOrder.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["colororderqty"]).ToString("#,##0.00;(#,##0.00); ");
            this.BuyerName.Text = ds1.Tables[1].Rows[0]["buyername"].ToString();
            this.lblbrand.Text = ds1.Tables[1].Rows[0]["brand"].ToString();
            this.lblcolor.Text = ds1.Tables[1].Rows[0]["colordesc"].ToString();

            if(comcod=="5305" || comcod == "5306")
            {
                this.lblOrdrNo.Text = "Order No.";
                this.lblTrialOrderNo.Text = ds1.Tables[1].Rows[0]["orderno"].ToString();
                this.lblordrqty.Text = "Order Qty.";
            }
            else
            {
                this.lblOrdrNo.Text = "Trial Order No.";
                this.lblTrialOrderNo.Text = ds1.Tables[1].Rows[0]["trialordr"].ToString();
                this.lblordrqty.Text = "Trial Order";
            }

            this.lblarticle.Text = ds1.Tables[1].Rows[0]["article"].ToString();
            this.lblsizernge.Text = ds1.Tables[1].Rows[0]["sizerange"].ToString();
            this.SmpleIMg.ImageUrl = ds1.Tables[1].Rows[0]["images"].ToString();
            this.ordqty.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");

            if (ds1.Tables[2].Rows.Count == 0)
                return;
            for (int i = 0; i < ds1.Tables[3].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[3].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 1].Visible = true;
                this.gvsizes.Columns[columid + 1].HeaderText = ds1.Tables[3].Rows[i]["sizedesc"].ToString().Trim();
            }
            this.gvsizes.EditIndex = -1;

            this.gvsizes.DataSource = ds1.Tables[2];
            this.gvsizes.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void lblbtnSave_Click(object sender, EventArgs e)
        {

            string reqno = this.lblPreqno.Text.Trim().ToString();
            string mlccod = this.lblmlccod.Text.ToString();
            string process = this.lbldept.Text.ToString();
            string reqtype = this.lblreqtype.Text.ToString();
            string reqdate = this.lblreqdate.Text.ToString();
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string allaporove = (this.ChkAllPendingDprApprove.Checked == true) ? "true" : "false";
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "APPROVE_PRODUCTION_REQUISITION", mlccod, reqno, process,
                userid, Terminal, Sessionid, Posteddat, reqtype, reqdate, allaporove);

            if (result == true)
            {
                this.ChkAllPendingDprApprove.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Approved Successfully');", true);


            }
        }

        protected void grvComp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string prdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prdno")).ToString();
                //   string Type = this.Request.QueryString["Type"].ToString();

                hlink2.NavigateUrl = "~/F_15_Pro/Print?Type=strcv&actcode=" + mlccod + "&genno=" + preqno + "&prodno=" + prdno;

            }
        }

        protected void lgrvDept_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string preqno = ((Label)this.grvReqAprvl.Rows[index].FindControl("lgvpreqno")).Text.ToString();

            DataTable mainlist = (DataTable)ViewState["tblReqAppList"];
            DataTable viewlist = (DataTable)ViewState["tblreqappviewlist"];

            string flag = ((Label)grvReqAprvl.Rows[index].FindControl("lblFlag")).Text.ToString();
           

            DataView dv1 = new DataView();
            dv1 = viewlist.DefaultView;
            dv1.RowFilter = ("procode like 'DPR%'");
            viewlist = dv1.ToTable();


            DataView dv = new DataView();
            dv = mainlist.DefaultView;
            dv.RowFilter = ("preqno ='" + preqno + "'");
            mainlist = dv.ToTable();
            if (flag == "")
                return;
            if (Convert.ToBoolean(flag) == false)
            {



                foreach (DataRow dr2 in viewlist.Rows)
                {
                    if (dr2["preqno"].ToString() != preqno)
                    {
                        dr2["flag"] = false;

                    }
                    else
                    {
                        dr2["flag"] = true;
                    }
                }


                foreach (DataRow dr in mainlist.Rows)
                {
                    DataRow drs = viewlist.NewRow();
                    drs["comcod"] = dr["comcod"].ToString();
                    drs["preqno"] = preqno;
                    drs["plnno"] = dr["plnno"].ToString();
                    drs["pbdate"] = dr["pbdate"].ToString();
                    drs["batchcode"] = dr["batchcode"].ToString();
                    drs["batchdesc"] = dr["batchdesc"].ToString();
                    drs["procode"] = dr["procode"].ToString();
                    drs["procdesc"] = dr["procdesc"].ToString();
                    drs["itmcount"] = dr["itmcount"];
                    drs["rsqty"] = dr["rsqty"];
                    drs["reqtype"] = dr["reqtype"];
                    drs["demanddate"] = dr["demanddate"].ToString();
                    viewlist.Rows.Add(drs);

                }
                DataTable ma = (DataTable)ViewState["tblReqAppList"];
                DataTable v = (DataTable)ViewState["tblreqappviewlist"];
            }
            else
            {
                foreach (DataRow dr2 in viewlist.Rows)
                {
                    if (dr2["preqno"].ToString() == preqno)
                    {
                        dr2["flag"] = false;

                    }

                }
            }



            DataTable dt = viewlist.AsEnumerable()
                     .OrderBy(r => r.Field<string>("preqno"))
                     .CopyToDataTable();
            this.Data_Bind("grvReqAprvl", dt);

        }

        protected void lblgvProcess_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;           
            string preqno = ((Label)gvprocess.Rows[index].FindControl("lblpreq")).Text.ToString();
            string dayid = ((Label)gvprocess.Rows[index].FindControl("LblOdayid")).Text.ToString();

            DataTable mainlist = (DataTable)ViewState["tblallprocess"];
            DataTable viewlist = (DataTable)ViewState["tblheadprocess"];

            string flag = ((Label)gvprocess.Rows[index].FindControl("lblFlags")).Text.ToString();


            DataView dv1 = new DataView();
            dv1 = viewlist.DefaultView;
            dv1.RowFilter = ("tprostep like 'DPR%'");
            viewlist = dv1.ToTable();


            DataView dv = new DataView();
            dv = mainlist.DefaultView;
            dv.RowFilter = ("preqno ='" + preqno + "' and odayid='" + dayid + "'");
            mainlist = dv.ToTable();
            if (flag == "")
                return;
            if (Convert.ToBoolean(flag) == false)
            {



                foreach (DataRow dr2 in viewlist.Rows)
                {
                    if (dr2["preqno"].ToString() != preqno)
                    {
                        dr2["flag"] = false;

                    }
                    else
                    {
                        dr2["flag"] = true;
                    }
                }


                foreach (DataRow dr in mainlist.Rows)
                {
                    DataRow drs = viewlist.NewRow();
                    drs["comcod"] = dr["comcod"].ToString();
                    drs["preqno"] = preqno;
                    drs["tprostep"] = dr["tprostep"].ToString();
                    drs["tprostepdesc"] = dr["tprostepdesc"].ToString();
                    drs["odayid"] = dr["odayid"].ToString();
                    drs["preqno1"] = dr["preqno1"].ToString();
                    drs["rdate"] = dr["rdate"].ToString();
                    drs["mlccod"] = dr["mlccod"].ToString();
                    drs["mlcdesc"] = dr["mlcdesc"].ToString();
                    drs["styleid"] = dr["styleid"].ToString();
                    drs["styledesc"] = dr["styledesc"].ToString();
                    drs["orderno"] = dr["orderno"].ToString();
                    drs["article"] = dr["article"].ToString();
                    // drs ["prdate"] = dr ["prdate"].ToString();
                    drs["qcstatus"] = dr["qcstatus"].ToString();
                    drs["qty"] = dr["qty"];
                    drs["outqty"] = dr["outqty"];
                    drs["balqty"] = dr["balqty"];
                    drs["rejectionqty"] = dr["rejectionqty"];
                    drs["repairqty"] = dr["repairqty"];
                    drs["manpower"] = dr["manpower"];
                    drs["rcvqty"] = dr["rcvqty"];
                    drs["permission"] = dr["permission"];
                    viewlist.Rows.Add(drs);






                }
            }
            else
            {
                foreach (DataRow dr2 in viewlist.Rows)
                {
                    if (dr2["preqno"].ToString() == preqno)
                    {
                        dr2["flag"] = false;

                    }

                }
            }



            DataTable dt = viewlist.AsEnumerable()
                     .OrderBy(r => r.Field<string>("preqno"))
                     .CopyToDataTable();
            this.Data_Bind("gvprocess", dt);
        }

        protected void grvFlissue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();

                hlink2.NavigateUrl = "~/F_15_Pro/FloorMatIssue?Type=Entry&genno=" + preqno + "&actcode=" + mlccod;

            }
        }

        private void GetSesson()
        {
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
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
        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GeLCName();
        }

        private void GeLCName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            //string txtSProject = "%1601%";
            //string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", txtSProject, findseason, "", "", "", "", "", "", "");
            //DataRow dr = ds1.Tables[0].NewRow();
            //dr["actdesc1"] = "----All----";
            //dr["actcode"] = "000000000000";
            //ds1.Tables[0].Rows.Add(dr);
            //this.ddlLCName.DataTextField = "actdesc1";
            //this.ddlLCName.DataValueField = "actcode";
            //this.ddlLCName.DataSource = ds1.Tables[0];
            //this.ddlLCName.DataBind();
            //this.ddlLCName.SelectedValue = "000000000000";

            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string customer = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETBOMLIST", findseason, customer, "", "", "", "", "", "", "");
            // BOM List appear as per WH Request by Safi-05-Nov-23
            if (ds1 == null)
                return;

            DataRow dr = ds1.Tables[0].NewRow();
            dr["bomdesc"] = "All";
            dr["mlccod"] = "000000000000";
            ds1.Tables[0].Rows.Add(dr);

            this.ddlLCName.DataTextField = "bomdesc";
            this.ddlLCName.DataValueField = "mlccod";
            this.ddlLCName.DataSource = ds1.Tables[0];
            this.ddlLCName.SelectedValue = "000000000000";
            this.ddlLCName.DataBind();
            
        }

        protected void ibtnFindLC_Click(object sender, EventArgs e)
        {
            this.GeLCName();
        }


        protected void lnkbtnPrintCombined_Click(object sender, EventArgs e)
        {

            string order = "";
            string reqno = "";
            string reqdat = "";

            for (int i = 0; i < this.grvProIssue.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.grvProIssue.Rows[i].FindControl("chkPrintCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.grvProIssue.Rows[i].FindControl("lblgvcentrid")).Text;
                    string dayid = ((Label)this.grvProIssue.Rows[i].FindControl("lgrvDayId")).Text;
                    string order2 = mlccod + dayid;
                    reqdat = ((Label)this.grvProIssue.Rows[i].FindControl("lgpreqdate")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.grvProIssue.Rows[i].FindControl("lblgvpreqno")).Text;

                }
            }

            string printFormat = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;
            string url = ResolveClientUrl("~//F_15_Pro/Print.aspx?Type=PrintReqMulti&mlccod=" + order + "&reqno=" + reqno + "&pbdate=" + reqdat + "&printfrmt=" + printFormat);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);

        }

        

        protected void graLnkbtnPrintCombined_Click(object sender, EventArgs e)
        {

            string order = "";
            string reqno = "";
            string reqdat = "";

            for (int i = 0; i < this.grvReqAprvl.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.grvReqAprvl.Rows[i].FindControl("graChkPrntCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.grvReqAprvl.Rows[i].FindControl("lgvmlccod")).Text;
                    string dayid = ((Label)this.grvReqAprvl.Rows[i].FindControl("gralblDayId")).Text;
                    string order2 = mlccod + dayid;
                    reqdat = ((Label)this.grvReqAprvl.Rows[i].FindControl("lgvReqDate1")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.grvReqAprvl.Rows[i].FindControl("lgvpreqno")).Text;

                }
            }

            string printFormat = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;
            string url = ResolveClientUrl("~//F_15_Pro/Print.aspx?Type=PrintReqMulti&mlccod=" + order + "&reqno=" + reqno + "&pbdate=" + reqdat + "&printfrmt=" + printFormat);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);

        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool DeleteRequisition(string preqno, string itmno, string spcfno, string procode, string reqdate, string reqtype)
        {

            if(reqtype == "NORMAL")
            {
                return false;
            }
            else
            {
                Common com = new Common();
                string comcod = com.GetCompCode();
                ProcessAccess accData = new ProcessAccess();

                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "DELETE_ITEM_FROM_PRODUCTION_REQ", preqno, itmno, spcfno, procode, reqdate, reqtype);

                if (result)
                {
                    if (result == true && ConstantInfo.LogStatus == true)
                    {

                        string eventtype = reqtype + " Material Requisition Item Removed";
                        string eventdesc = "Production Material Requisition,Type "+ reqtype + " Preqno No:" + preqno;
                        string eventdesc2 = "Item No- " + itmno+ spcfno;
                        com.LogStatus( eventtype, eventdesc, eventdesc2,"From Interfaces");
                      

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool UpdateRequisition(string preqno, string itmno, string spcfno, string procode, string reqdate, string reqtype, string reqty)
        {

            Common com = new Common();
            string comcod = com.GetCompCode();
            ProcessAccess accData = new ProcessAccess();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "UPDATE_ITEM_FROM_PRODUCTION_REQ", preqno, itmno, spcfno, procode, reqdate, reqtype, reqty);

            if (result)
            {
                if (result == true && ConstantInfo.LogStatus == true)
                {

                    string eventtype = reqtype + " Material Requisition Item Updated";
                    string eventdesc = "Production Material Requisition, Preqno No:" + preqno;
                    string eventdesc2 = "Item No- " + itmno + spcfno;
                    com.LogStatus(eventtype, eventdesc, eventdesc2, "From Interfaces");
                }
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}