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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Drawing;
using SPEENTITY;
using SPELIB;


namespace SPEWEB.F_15_Pro
{

public partial class ProductionInterfaceSemi : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION Smartface- Semi";

                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();

                this.PnlInt.Visible = true;

                txtdate_TextChanged(null, null);
                string comcod = GetCompCode();

                //this.RadioButtonList1_SelectedIndexChanged(null, null);
                //  
            }
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
            string comcod = this.GetCompCode();

            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string Type = (this.Request.QueryString["Type"].ToString() == "FG") ? "41%" : "04%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "RPTPRODUCTIONDASHBOARD", Date1, Date2, Type, "", "", "", "", "", "");
            if (ds1 == null)
                return;


            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[5].Rows[0]["reqqty"] + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Request</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[5].Rows[0]["proreqqty"] + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Requsition</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[5].Rows[0]["issueqty"] + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Material Issue</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[5].Rows[0]["procsqty"] + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Process</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[5].Rows[0]["prodqty"] + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Pro. Entry</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[5].Rows[0]["qcqty"] + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> QC </div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[5].Rows[0]["strecqty"] + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'>Store Receive</div></div></div>";

            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[5].Rows[0]["compqty"] + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> Completed</div></div></div>";
            




            //this.RadioButtonList1.Items[0].Text = "<table><tr><th rowspan='2' style='width:35px; border: medium none;height:35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["reqqty"].ToString() + "</span></th>" + "<tr>" + "<td  style='width: 100px;background:#4BCF9E none repeat scroll 0 0; color:#000; border: medium none;text-align: center; line-height: 33px; font-size: 13px;'>" + "Request" + "</td></tr></table>";

            //this.RadioButtonList1.Items[1].Text = "<table><tr><th rowspan='2' style='width:35px; border: medium none;height:35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["proreqqty"].ToString() + "</span></th>" + "<tr>" + "<td  style='width: 100px;background:#92D14F none repeat scroll 0 0; color:#000; border: medium none; text-align: center; line-height: 33px; font-size: 13px;'>" + "Requsition" + "</td></tr></table>";


            //this.RadioButtonList1.Items[2].Text = "<table><tr><th rowspan='2' style='width:35px; medium none;height: 35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["issueqty"].ToString() + "</span></th>" + "<tr>" + "<td  style='width: 100px;background:#5EB75B none repeat scroll 0 0; color:#000; border: medium none;text-align: center;line-height: 33px; font-size: 13px;''>" + "Material Issue" + "</td></tr></table>";

            //this.RadioButtonList1.Items[3].Text = "<table><tr><th rowspan='2' style='width:50px; medium none;height: 35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["procsqty"].ToString() + "</span></th>" + "<tr>" + "<td  style='width: 100px;background:#92D14F none repeat scroll 0 0; color:#000; border: medium none;text-align: center; line-height: 33px; font-size: 13px;''>" + "Process" + "</td></tr></table>";

            //this.RadioButtonList1.Items[4].Text = "<table><tr><th rowspan='2' style='width:40px; medium none;height: 35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["prodqty"].ToString() + "</span></th>" + "<tr>" + "<td  style='width: 100px;background:#00AF50 none repeat scroll 0 0; color:#000; border: medium none;text-align: center; line-height: 33px; font-size: 13px;''>" + "Pro. Entry" + "</td></tr></table>";

            //this.RadioButtonList1.Items[5].Text = "<table><tr><th rowspan='2' style='width:40px; medium none;height: 35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["qcqty"].ToString() + "</span></th>" + "<tr>" + "<td  style='width: 100px;background:#68A5AD none repeat scroll 0 0; color:#000; border: medium none;text-align: center; line-height: 33px; font-size: 13px;''>" + "QC" + "</td></tr></table>";

            //this.RadioButtonList1.Items[6].Text = "<table><tr><th rowspan='2' style='width:40px; medium none;height: 35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["strecqty"].ToString() + "</span></th>" + "</tr>" + "<tr>" + "<td  style='width: 100px;background:#D39657 none repeat scroll 0 0; color:#000; border: medium none;text-align: center; line-height: 33px; font-size: 13px;''>" + "Store Receive" + "</td></tr></table>";
            //this.RadioButtonList1.Items[7].Text = "<table><tr><th rowspan='2' style='width:40px; medium none;height: 35px;'><span class='fan counter'>" + ds1.Tables[5].Rows[0]["compqty"].ToString() + "</span></th>" + "</tr>" + "<tr>" + "<td  style='width: 100px;background:#769BF4 none repeat scroll 0 0; color:#000; border: medium none;text-align: center; line-height: 33px; font-size: 13px;''>" + "Completed" + "</td></tr></table>";
            // All Production

            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            this.Data_Bind("gvProdInfo", dt);

            //Requsition
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("pbmststus='Requsition' ");

            DataTable dt1req = dv.ToTable();
            dt1req = dt1req.DefaultView.ToTable(true, "pbmno", "pbmno1", "bgddat", "prodesc", "itemcount", "bgdwqty", "bgdamt", "bgdbal");

            this.Data_Bind("grvProReq", dt1req);


            //issue
            dt = ((DataTable)ds1.Tables[1]).Copy();
            this.Data_Bind("grvProIssue", dt);

            //Production Entry
            dt = ((DataTable)ds1.Tables[2]).Copy();
            this.Data_Bind("grvProdtion", dt);

            //Ready for Qc
            dt = ((DataTable)ds1.Tables[3]).Copy();
            this.Data_Bind("grvQCEntry", dt);

            //Ready for Qc
            dt = ((DataTable)ds1.Tables[4]).Copy();
            this.Data_Bind("gvstorec", dt);

            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("pbmststus='Complete' ");
            this.Data_Bind("grvComp", dv.ToTable());


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "0":
                    this.pnlallProd.Visible = true;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlProduction.Visible = false;
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
                    this.PnlProduction.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = true;
                    this.pnlProProcs.Visible = false;
                    this.PnlProduction.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;

                case "3":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = true;
                    this.PnlProduction.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlProduction.Visible = true;
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
                    this.PnlProduction.Visible = false;
                    this.PnlQC.Visible = true;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;
                case "6":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlProduction.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = true;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;
                case "7":
                    this.pnlallProd.Visible = false;
                    this.pnlReq.Visible = false;
                    this.PanelIssue.Visible = false;
                    this.pnlProProcs.Visible = false;
                    this.PnlProduction.Visible = false;
                    this.PnlQC.Visible = false;
                    this.PnlStrec.Visible = false;
                    this.PnlComp.Visible = true;

                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
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
                string pbmno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pbmno")).ToString();
                //string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string Type = this.Request.QueryString["Type"].ToString();
                if (Type == "FG")
                {
                    hlink1.NavigateUrl = "~/F_15_Pro/ProdReqSemi?Type=Entry&genno=" + pbmno;
                }
                else
                {
                    hlink1.NavigateUrl = "~/F_15_Pro/ProdReqSemi?Type=EntrySemi&genno=" + pbmno;
                }
            }
        }
        protected void grvProIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string pbmno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pbmno")).ToString();
                string pbdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "pbdate")).ToString("dd-MMM-yyyy");

                //double titem = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "trescount"));
                //double issitem = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rcount"));



                string Type = this.Request.QueryString["Type"].ToString();
                hlink2.NavigateUrl = "~/F_13_ProdMon/Print?dprno=" + preqno + "&pbmno=" + pbmno + "&issuedate=" + pbdate + "&option=proreq";
                if (Type == "FG")
                {
                    hlink1.NavigateUrl = "~/F_07_Inv/PBMatIssueSingle?Type=Entry&actcode=" + preqno + "&genno=" + pbdate;
                }
                else
                {
                    hlink1.NavigateUrl = "~/F_15_Pro/PBMatIssueSemi?Type=EntrySemi&actcode=" + preqno + "&genno=" + pbdate;

                }

            }
        }
        protected void grvProdtion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string batchcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchcode")).ToString();
                //string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string Type = this.Request.QueryString["Type"].ToString();
                if (Type == "FG")
                {
                    hlink1.NavigateUrl = "~/F_13_ProdMon/ProductionPlan?Type=Entry&actcode=" + batchcode;
                }
                else
                {
                    hlink1.NavigateUrl = "~/F_15_Pro/ProductionPlan?Type=EntrySemi&actcode=" + batchcode;
                }
            }
        }
        protected void grvQCEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string prodid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prodid")).ToString();
                //string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string Type = this.Request.QueryString["Type"].ToString();
                if (Type == "FG")
                {
                    hlink1.NavigateUrl = "~/F_15_Pro/ProQCEntry?Type=Entry&genno=" + prodid;
                }
                else
                {
                    hlink1.NavigateUrl = "~/F_15_Pro/ProQCEntry?Type=EntrySemi&genno=" + prodid;
                }
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
                    for (int i = 0; i < grvProIssue.Rows.Count; i++)
                    {
                        double titem = Convert.ToDouble(((Label)grvProIssue.Rows[i].FindControl("lblgvtitemcount")).Text.Trim());
                        //double isubalitm = Convert.ToDouble(((Label)grvProIssue.Rows[i].FindControl("lblgvitemcount")).Text.Trim());


                        //if (titem != isubalitm)
                        //{
                        //    this.grvProIssue.Rows[i].BackColor = Color.SkyBlue;
                        //    this.grvProIssue.Rows[i].ForeColor = Color.Black;
                        //}
                    }

                    break;
                case "grvProdtion":
                    this.grvProdtion.DataSource = (dt);
                    this.grvProdtion.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    //for (int i = 0; i < grvProdtion.Rows.Count; i++)
                    //{
                    //    //ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" +
                    //    double acqty = Convert.ToDouble(((Label)grvProdtion.Rows[i].FindControl("lblgvacqty")).Text.Trim());
                    //    double balqty = ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" + Convert.ToDouble(((Label)grvProdtion.Rows[i].FindControl("lblgvbalqty")).Text.Trim())));


                    //    if (acqty != balqty)
                    //    {
                    //        this.grvProdtion.Rows[i].BackColor = Color.SkyBlue;
                    //        this.grvProdtion.Rows[i].ForeColor = Color.Black;
                    //    }
                    //}
                    break;
                case "grvQCEntry":
                    this.grvQCEntry.DataSource = (dt);
                    this.grvQCEntry.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    for (int i = 0; i < grvQCEntry.Rows.Count; i++)
                    {
                        string Status = ((Label)grvQCEntry.Rows[i].FindControl("lblgvpStatus")).Text.Trim();

                        if (Status == "Unfinished")
                        {
                            this.grvQCEntry.Rows[i].BackColor = Color.SkyBlue;
                            this.grvQCEntry.Rows[i].ForeColor = Color.Black;
                        }
                    }

                    break;
                case "gvstorec":
                    this.gvstorec.DataSource = (dt);
                    this.gvstorec.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    for (int i = 0; i < gvstorec.Rows.Count; i++)
                    {
                        string rcvtype = ((Label)gvstorec.Rows[i].FindControl("lblgvrcvtype")).Text.Trim();

                        if (rcvtype == "Reject")
                        {
                            this.gvstorec.Rows[i].BackColor = Color.SkyBlue;
                            this.gvstorec.Rows[i].ForeColor = Color.Black;
                        }
                    }
                    break;

                case "grvComp":
                    this.grvComp.DataSource = (dt);
                    this.grvComp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
            }
        }



        //protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        //{


        //    int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
        //    this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

        //    string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
        //    string centrid = ASTUtility.Left(code, 12);
        //    string orderno = ASTUtility.Right(code, 14);

        //    try
        //    {

        //        string comcod = this.GetCompCode();
        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comnam = hst["comnam"].ToString();
        //        string compname = hst["compname"].ToString();
        //        string comadd = hst["comadd1"].ToString();
        //        string username = hst["username"].ToString();
        //        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");




        //        DataSet ds2 = accData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTCUSTINFORMATION", orderno, centrid, "", "", "", "", "", "", "");
        //        if (ds2 == null)
        //            return;
        //        ReportDocument rptSOrder = new ReportDocument();
        //        //string qType = this.Request.QueryString["Type"].ToString();
        //        //if (qType == "dNote")
        //        //{
        //        //    rptSOrder = new MFGRPT.R_23_SaM.RptSalDelNoteZelta();
        //        //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
        //        //    txtHeader.Text = "DELIVERY NOTE";
        //        //}
        //        //else
        //        //{
        //        rptSOrder = new MFGRPT.R_23_SaM.RptSalOrdrZelta();
        //        TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
        //        txtHeader.Text = "SALES ORDER";
        //        // }


        //        TextObject txtrptcomp = rptSOrder.ReportDefinition.ReportObjects["Company"] as TextObject;
        //        txtrptcomp.Text = comnam;



        //        TextObject txtCompAdd = rptSOrder.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
        //        txtCompAdd.Text = comadd;

        //        TextObject txtsaledate = rptSOrder.ReportDefinition.ReportObjects["txtsaledate"] as TextObject;
        //        txtsaledate.Text = ds2.Tables[2].Rows[0]["orderdat"].ToString().Trim();

        //        TextObject txtCust = rptSOrder.ReportDefinition.ReportObjects["txtCust"] as TextObject;
        //        txtCust.Text = ds2.Tables[2].Rows[0]["name"].ToString().Trim();

        //        TextObject txtAdd = rptSOrder.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
        //        txtAdd.Text = ds2.Tables[2].Rows[0]["addr"].ToString().Trim();

        //        TextObject txtPhone = rptSOrder.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
        //        txtPhone.Text = ds2.Tables[2].Rows[0]["phone"].ToString().Trim();

        //        TextObject txtTrans = rptSOrder.ReportDefinition.ReportObjects["txtTrans"] as TextObject;
        //        txtTrans.Text = ds2.Tables[0].Rows[0]["courie"].ToString().Trim();

        //        TextObject txtStore = rptSOrder.ReportDefinition.ReportObjects["txtStore"] as TextObject;
        //        txtStore.Text = ds2.Tables[2].Rows[0]["storename"].ToString().Trim();


        //        TextObject txtCode = rptSOrder.ReportDefinition.ReportObjects["txtCode"] as TextObject;
        //        txtCode.Text = ds2.Tables[2].Rows[0]["sirtdes"].ToString().Trim();

        //        TextObject txtOrdTime = rptSOrder.ReportDefinition.ReportObjects["txtOrdTime"] as TextObject;
        //        txtOrdTime.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["posteddat"]).ToString("hh:mm:ss tt").Trim();

        //        TextObject txtRemarks = rptSOrder.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
        //        txtRemarks.Text = (ds2.Tables[2].Rows[0]["narration"]).ToString().Trim();

        //        TextObject txtChannel = rptSOrder.ReportDefinition.ReportObjects["txtChannel"] as TextObject;
        //        txtChannel.Text = (ds2.Tables[2].Rows[0]["chnl"]).ToString().Trim();

        //        TextObject txtsaleNo = rptSOrder.ReportDefinition.ReportObjects["txtsaleNo"] as TextObject;
        //        txtsaleNo.Text = orderno;
        //        //BALANCE 

        //        DataTable dt = ds2.Tables[0];
        //        DataTable dt2 = ds2.Tables[1];
        //        DataTable dt3 = ds2.Tables[2];

        //        double oStdAmt, Dipsamt, ordAmt, balAmt;
        //        oStdAmt = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(dues)", "")) ? 0.00 : dt3.Compute("sum(dues)", "")));
        //        ordAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ? 0.00 : dt.Compute("sum(tamount)", "")));
        //        Dipsamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ? 0.00 : dt2.Compute("sum(paidamt)", "")));

        //        balAmt = (oStdAmt + ordAmt) - Dipsamt;
        //        //if (qType == "All")
        //        //{
        //        TextObject txtOutStdBal = rptSOrder.ReportDefinition.ReportObjects["txtOutStdBal"] as TextObject;
        //        txtOutStdBal.Text = oStdAmt.ToString("#,##0.00;(#,##0.00);");

        //        TextObject txtDipositeAmt = rptSOrder.ReportDefinition.ReportObjects["txtDipositeAmt"] as TextObject;
        //        txtDipositeAmt.Text = Dipsamt.ToString("#,##0.00;(#,##0.00);");

        //        TextObject txtOrderAmt = rptSOrder.ReportDefinition.ReportObjects["txtOrderAmt"] as TextObject;
        //        txtOrderAmt.Text = ordAmt.ToString("#,##0.00;(#,##0.00);");

        //        TextObject txtBalanceAmt = rptSOrder.ReportDefinition.ReportObjects["txtBalanceAmt"] as TextObject;
        //        txtBalanceAmt.Text = balAmt.ToString("#,##0.00;(#,##0.00);");
        //        //}

        //        TextObject txtAppby = rptSOrder.ReportDefinition.ReportObjects["txtAppby"] as TextObject;
        //        txtAppby.Text = ds2.Tables[2].Rows[0]["appby"].ToString().Trim();

        //        TextObject txtPreBy = rptSOrder.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
        //        txtPreBy.Text = ds2.Tables[0].Rows[0]["usrname"].ToString().Trim();

        //        TextObject txtuserinfo = rptSOrder.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //        rptSOrder.SetDataSource(ds2.Tables[0]);

        //        // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

        //        // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource(ds2.Tables[1]);


        //        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //        rptSOrder.SetParameterValue("ComLogo", ComLogo);

        //        Session["Report1"] = rptSOrder;

        //        this.lblprintstkl.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     "PDF" + "', target='_blank');</script>";


        //        Common.LogStatus("Order", "Order Print", "Order No: ", orderno + " - " + centrid);

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        //protected void lnkbtnView_Click(object sender, EventArgs e)
        //{
        //    int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
        //    this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";
        //    string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
        //    string centrid = ASTUtility.Left(code, 12);
        //    string Delorderno = ASTUtility.Right(code, 14);

        //    if (Delorderno.Length == 0)
        //        return;
        //    try
        //    {
        //        string comcod = this.GetCompCode();
        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comnam = hst["comnam"].ToString();
        //        string compname = hst["compname"].ToString();
        //        string comadd = hst["comadd1"].ToString();
        //        string username = hst["username"].ToString();
        //        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //        DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_CHALLAN", "PRINTDELIVERYCHALLAN", Delorderno, centrid, "", "", "", "", "", "", "");

        //        double Qty = Convert.ToDouble(ds.Tables[0].Rows[0]["delqty"]);
        //        //double Vat = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("sum(vat)", "")) ? 0.00 : ds.Tables[0].Compute("sum(vat)", "")));

        //        ReportDocument rptChallan = new MFGRPT.R_23_SaM.RptDelChallanZelta();

        //        TextObject txtCompAdd = rptChallan.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
        //        txtCompAdd.Text = comnam + "\n" + "Corporate Office" + "\n" + comadd;
        //        TextObject txtrptHeader = rptChallan.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
        //        txtrptHeader.Text = "DELIVERY CHALLAN";

        //        TextObject txtDelNo = rptChallan.ReportDefinition.ReportObjects["txtDelNo"] as TextObject;
        //        txtDelNo.Text = Delorderno;// "DO" + sdelno.Substring(3);
        //        TextObject txtChallan = rptChallan.ReportDefinition.ReportObjects["txtChallan"] as TextObject;
        //        txtChallan.Text = ds.Tables[1].Rows[0]["orderno"].ToString();
        //        TextObject txtDate = rptChallan.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //        txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["memodat"]).ToString("dd-MMM-yyyy");
        //        //TextObject txtOrder = rptChallan.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
        //        //txtOrder.Text = (ds.Tables[2].Rows[0]["orderno"].ToString() == "00000000000000") ? "CURRENT SALES" :
        //        //    ASTUtility.Left(ds.Tables[2].Rows[0]["orderno"].ToString(), 2) + ds.Tables[2].Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(ds.Tables[2].Rows[0]["orderno"].ToString(), 5); ;

        //        TextObject txtCust = rptChallan.ReportDefinition.ReportObjects["txtCust"] as TextObject;
        //        txtCust.Text = ds.Tables[1].Rows[0]["custname"].ToString();
        //        TextObject txtCustadd = rptChallan.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
        //        txtCustadd.Text = ds.Tables[1].Rows[0]["custadd"].ToString();
        //        TextObject txtPhone = rptChallan.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
        //        txtPhone.Text = ds.Tables[1].Rows[0]["custphone"].ToString();

        //        TextObject txtBag = rptChallan.ReportDefinition.ReportObjects["txtBag"] as TextObject;
        //        txtBag.Text = Convert.ToDouble(ds.Tables[1].Rows[0]["bagqty"]).ToString("#,##0;(#,##0);");

        //        TextObject txtSsirdesc = rptChallan.ReportDefinition.ReportObjects["txtSsirdesc"] as TextObject;
        //        txtSsirdesc.Text = ds.Tables[1].Rows[0]["ssirdesc"].ToString();

        //        TextObject txtRemarks = rptChallan.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
        //        txtRemarks.Text = ds.Tables[1].Rows[0]["narr"].ToString();

        //        TextObject txtOrdTime = rptChallan.ReportDefinition.ReportObjects["txtDelTime"] as TextObject;
        //        txtOrdTime.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["posteddat"].ToString()).ToString("hh:mm:ss tt");
        //        TextObject txtuserinfo = rptChallan.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //        rptChallan.SetDataSource(ds.Tables[0]);
        //        TextObject txtPreBy = rptChallan.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
        //        txtPreBy.Text = ds.Tables[1].Rows[0]["username"].ToString();

        //        TextObject txtDesBy = rptChallan.ReportDefinition.ReportObjects["txtDesBy"] as TextObject;
        //        txtDesBy.Text = ds.Tables[1].Rows[0]["apusername"].ToString();

        //        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //        rptChallan.SetParameterValue("ComLogo", ComLogo);

        //        Session["Report1"] = rptChallan;
        //        this.lblprintstkl.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     "PDF" + "', target='_blank');</script>";
        //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //        //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //        ///
        //        if (ConstantInfo.LogStatus == true)
        //        {
        //            string eventtype = "Delivery ORDER";
        //            string eventdesc = "Print Report";
        //            string eventdesc2 = "Del No : " + Delorderno;
        //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        protected void btnDelOrder_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(code, 12);
            string orderno = ASTUtility.Right(code, 14);
            if (orderno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "DELORDERSHOW", centrid, orderno);

            //DataSet ds = lst.GetDataSetForXml(ds1, centrid, orderno);

            // bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, orderno);

            // if (!resulta)
            // {

            //     return;
            // }

            bool result = accData.UpdateTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "ORDERDELETE", centrid, orderno, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Delete Successfully');", true);
            }
            Common.LogStatus("Sales Interface", "Order Delete", "Order No: ", orderno + " - " + centrid);
        }

        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(code, 12);
            string orderno = ASTUtility.Right(code, 14);

            //if (RDsostatus != "Approved")
            //    return;

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERLASTAPPDELETE", centrid, orderno, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            }
            Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }
        protected void btnDelRev_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();

            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(code, 12);
            string orderno = ASTUtility.Right(code, 14);
            if (orderno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
                return;
            }

            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "CHKFINALAPP", usrid, centrid, "", "", "");
            if (ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You Have no Permission');", true);
                return;
            }

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERAPPDELETE", centrid, orderno, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            }
            Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }
        protected void btnDelDO_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();

            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(code, 12);
            string Delorderno = ASTUtility.Right(code, 14);
            if (Delorderno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
                return;
            }


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "SHOWDELIVERYORDER", centrid, Delorderno);

            //DataSet ds = lst.GetDataSetForXmlDo(ds1, centrid, Delorderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, Delorderno);

            //if (!resulta)
            //{

            //    return;
            //}
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "DELETEDOLIST", centrid, Delorderno, "", "", "");
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('DO Delete Successfully');", true);
            }
            Common.LogStatus("Sales Interface", "DO Delete", "DO No: ", Delorderno + " - " + centrid);
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
                string pbmno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pbmno")).ToString();
                string Type = this.Request.QueryString["Type"].ToString();

                hlink1.NavigateUrl = "~/F_13_ProdMon/Print?Type=" + Type + "&pbmno=" + pbmno + "&option=probudget";

            }
        }
        protected void gvstorec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string prodid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prodid")).ToString();
                string production = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "production")).ToString();
                string grrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grrno")).ToString();
                string rcvtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rcvtype")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "prodate")).ToString("dd-MMM-yyyy");


                string Type = this.Request.QueryString["Type"].ToString();
                if (Type == "SemiFG")
                {
                    if (rcvtype == "Fresh")
                    {
                        hlink2.NavigateUrl = "~/F_15_Pro/ProStoreReceive?Type=EntrySemi&genno=" + prodid + "&date=" + date + "&reptype=" + rcvtype;
                    }
                    else

                    {
                        hlink2.NavigateUrl = "~/F_15_Pro/ProStoreReceive?Type=EntrySemiRej&genno=" + prodid + "&date=" + date + "&reptype=" + rcvtype;
                    }


                }




            }
        }



        protected void lnkbtnRemove_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            var prodid = ((Label)row.FindControl("lgprodid")).Text.ToString();
            var pbmno = ((Label)row.FindControl("lblgvcentrid")).Text.ToString();
            string comcod = this.GetCompCode();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "DELETEPRODUCTFROMQCSTATE", prodid, "", "", "", "");

            if (result == true)
            {
                this.RadioButtonList1.SelectedIndex = 5;
                txtdate_TextChanged(null, null);
            }
            Common.LogStatus("Production Interface", "DO Delete", "DO No: ", prodid + " - " + pbmno);
        }

        protected void lnkbtnRemoveStr_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            var prodid = ((Label)row.FindControl("lblgvstorproid")).Text.ToString();
            var grrno = ((Label)row.FindControl("lblgvstorgrrno")).Text.ToString();
            string comcod = this.GetCompCode();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "DELETEPRODFROMSTREC", prodid, grrno, "", "", "");

            if (result == true)
            {
                this.RadioButtonList1.SelectedIndex = 6;
                txtdate_TextChanged(null, null);
            }
            Common.LogStatus("Production Interface", "DO Delete", "DO No: ", prodid + " - " + grrno);
        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.grvProdtion.HeaderRow.FindControl("chkall")).Checked)
            {
                for (i = 0; i < this.grvProdtion.Rows.Count; i++)
                {

                    ((CheckBox)this.grvProdtion.Rows[i].FindControl("chkack")).Checked = true;
                }
            }
            else
            {
                for (i = 0; i < this.grvProdtion.Rows.Count; i++)
                {

                    if (((CheckBox)this.grvProdtion.Rows[i].FindControl("chkack")).Enabled == true)
                    {
                        ((CheckBox)this.grvProdtion.Rows[i].FindControl("chkack")).Checked = false;
                    }
                }
            }
        }

        protected void lbtnProdCOmplete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            bool result = false;
            for (int i = 0; i < grvProdtion.Rows.Count; i++)
            {

                if (((CheckBox)this.grvProdtion.Rows[i].FindControl("chkack")).Checked == true)
                {
                    string batchcode = ((Label)this.grvProdtion.Rows[i].FindControl("lblgvbatchcode")).Text.ToString();

                    result = accData.UpdateTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "UPDATECOMPSTATUS", batchcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



                }
            }

            if (result == true)
            {

                this.RadioButtonList1.SelectedIndex = 4;
                txtdate_TextChanged(null, null);
            }
        }

        protected void LbtnRem_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            string pbmno = ((Label)row.FindControl("lblgvcentrid")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "DELETEBUDGET", pbmno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.RadioButtonList1.SelectedIndex = 0;
                txtdate_TextChanged(null, null);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('unable to delete. because requisition already done');", true);
            }


        }

    }
}
