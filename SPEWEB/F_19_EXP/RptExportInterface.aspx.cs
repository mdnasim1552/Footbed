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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPELIB;

namespace SPEWEB.F_19_EXP
{
    public partial class RptExportInterface : System.Web.UI.Page
    {
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Export Smartface";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtFDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtRecDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.SaleRequRpt();
                this.PnlInt.Visible = true;

                this.ImportInterFace();
                this.PanelVisible();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                switch (comcod)
                {
                    case "5305":
                    case "5306":
                      
                        this.RadioButtonList1.SelectedIndex = 0;

                        break;
                    default:
                       
                        this.RadioButtonList1.Items[0].Attributes.CssStyle.Add("visibility", "hidden");
                        this.RadioButtonList1.SelectedIndex = 1;

                        break;
                }

                HyperLink hyp1 = (HyperLink)this.HyperLink1 as HyperLink;
                HyperLink hyp2 = (HyperLink)this.HyperLink2 as HyperLink;
                HyperLink hyp3 = (HyperLink)this.HyperLink3 as HyperLink;

                hyp1.NavigateUrl = "~/F_19_EXP/ExportMgt?Type=Entry&actcode=&genno=";
                hyp2.NavigateUrl = "~/F_19_EXP/MoneyReceipt2?Type=Entry&genno=&centrid=&sircode=";
                hyp3.NavigateUrl = "~/F_19_EXP/CreatePackList?Type=Entry&actcode=&genno=&date=";

                if(comcod == "5305" || comcod == "5306")
                {
                  
                    this.HyperLink3.Visible = true;
                }
                else
                {
                    
                    this.HyperLink3.Visible = false;
                }

            }
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
        //        this.gvAchList.Columns[4].Visible = true;
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


        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //int day = ASTUtility.Datediffday(Convert.ToDateTime(this.txtFDate.Text), Convert.ToDateTime(this.txtdate.Text));
            //if (day != 0)
            //    return;
            //txtdate_TextChanged(null, null);


        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            string Date2 = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_INTERFACE", "EXPORTDASHBOARD", Date1, Date2, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.gvFGChln.DataSource = null;
            this.gvFGChln.DataBind();

            ViewState["tbldata1"] = ds1;

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[4].Rows[0]["packing"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Master Packing</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow  counter'>" + ds1.Tables[4].Rows[0]["fgdeliv"].ToString() + "</div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> Export App</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[4].Rows[0]["fgdelivn"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Create Challan</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[4].Rows[0]["fgchln"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> Challan App </div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[4].Rows[0]["upexp"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> Update Export</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[4].Rows[0]["coll"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> Update Collection</div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[4].Rows[0]["complete"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> Complete</div></div></div>";

            RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PanelVisible();
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataSet ds = (DataSet)ViewState["tbldata1"];
            DataTable dt = ds.Tables[0];

            DataTable Tempdt = new DataTable();
            DataView Tempdv = new DataView();


            switch (value)
            {

                case "0": ///Goods Delivery
                    this.pnlMpaking.Visible = true;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    this.PnlColl.Visible = false;
                    DataTable dts = ds.Tables[3].Copy();
                    DataView dv = dts.DefaultView;
                    this.Data_Bind("gvShipPlanDetails", dv.ToTable());

                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    
                    break;
                case "1": ///Goods Delivery
                    this.gvFGDeliv.DataSource = null;
                    this.gvFGDeliv.DataBind();
                    this.pnlMpaking.Visible = false;
                    this.pnlFGDeliv.Visible = true;
                    this.PnlFGChln.Visible = false;
                    this.PnlColl.Visible = false;
                    DataTable dtsm1 = ds.Tables[0].Copy();
                    DataView dvm1 = dtsm1.DefaultView;
                    dvm1.RowFilter = "approved=''";
                    this.Data_Bind("gvFGDeliv", dvm1.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";

                    this.gvFGDeliv.Columns[12].Visible = true;
                    this.gvFGDeliv.Columns[13].Visible = true;

                    this.gvFGDeliv.Columns[14].Visible = false;
                    this.gvFGDeliv.Columns[15].Visible = false;
                    break;

                case "2": ///Goods Delivery
                    this.gvFGDeliv.DataSource = null;
                    this.gvFGDeliv.DataBind();
                    this.gvCollectionAll.Visible = false;
                    this.pnlMpaking.Visible = false;
                    this.pnlFGDeliv.Visible = true;
                    this.PnlFGChln.Visible = false;
                    this.PnlColl.Visible = false;
                    DataTable dts1 = ds.Tables[0].Copy();
                    DataView dv1 = dts1.DefaultView;
                    dv1.RowFilter = "approved='Ok' and bqty<>0 ";
                    this.Data_Bind("gvFGDeliv", dv1.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";

                    this.gvFGDeliv.Columns[14].Visible = true;
                    this.gvFGDeliv.Columns[13].Visible = true;

                    this.gvFGDeliv.Columns[12].Visible = false;
                    this.gvFGDeliv.Columns[15].Visible = false;
                    break;

                case "3": /// Delivery Challan
                    this.gvCollectionAll.Visible = false;
                    this.pnlMpaking.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = true;
                    this.PnlColl.Visible = false;
                    DataView dv2 = ds.Tables[1].DefaultView;
                    dv2.RowFilter = "approved=''";
                    this.Data_Bind("gvFGChln", dv2.ToTable());
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;

                case "4": ///Goods Delivery
                    this.gvFGDeliv.DataSource = null;
                    this.gvFGDeliv.DataBind();
                    this.gvCollectionAll.Visible = false;
                    this.pnlMpaking.Visible = false;
                    this.pnlFGDeliv.Visible = true;
                    this.PnlFGChln.Visible = false;
                    this.PnlColl.Visible = false;
                    DataTable dts3 = ds.Tables[0].Copy();
                    DataView dv3 = dts3.DefaultView;
                    dv3.RowFilter = "approved<>'' and vounum='00000000000000' and expqty<>0.00 ";
                    this.Data_Bind("gvFGDeliv", dv3.ToTable());
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";

                    this.gvFGDeliv.Columns[15].Visible = true;

                    this.gvFGDeliv.Columns[12].Visible = false;
                    this.gvFGDeliv.Columns[13].Visible = false;
                    this.gvFGDeliv.Columns[14].Visible = false;

                    break;
                case "5": /// Update Collection
                    this.pnlMpaking.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    this.gvCollectionAll.Visible = true;
                    this.PnlColl.Visible = true;
                    this.Data_Bind("gvCollectionAll", ds.Tables[2]);
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;
                case "6": /// Complete
                    this.pnlMpaking.Visible = false;
                    this.gvCollectionAll.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = true;
                    this.PnlColl.Visible = false;
                    DataView dv4 = ds.Tables[1].DefaultView;
                    dv4.RowFilter = "approved<>''";
                    this.Data_Bind("gvFGChln", dv4.ToTable());
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
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
        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {

                case "gvFGDeliv":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvFGDeliv.DataSource = (dt);
                    this.gvFGDeliv.DataBind();
                    break;
                case "gvFGChln":

                    if (dt.Rows.Count == 0)
                    {
                        this.gvFGChln.DataSource =null;
                        this.gvFGChln.DataBind();
                    }
                      
                    this.gvFGChln.DataSource = (dt);
                    this.gvFGChln.DataBind();
                    break;
                case "gvCollectionAll":


                    this.gvCollectionAll.DataSource = dt;
                    this.gvCollectionAll.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    this.FooterCalculation();

                    break;
               case "gvShipPlanDetails":


                    this.gvShipPlanDetails.DataSource = dt;
                    this.gvShipPlanDetails.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                   

                    break;


            }
        }
        private void FooterCalculation()
        {
            //var lst = (List<RMGiEntity.C_19_Exp.BO_Collection>)ViewState["tblColl"];

            //if (lst.Count == 0)
            //    return;

            //((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFfcamt")).Text = ((lst.Select(p => p.fcamt).Sum() == 0.00) ? 0.00 : lst.Select(p => p.fcamt).Sum()).ToString("#,##0;(#,##0); ");
            //((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFtrnamount")).Text = ((lst.Select(p => p.amount).Sum() == 0.00) ? 0.00 : lst.Select(p => p.amount).Sum()).ToString("#,##0;(#,##0); ");
            //((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFfcbnkcharge")).Text = ((lst.Select(p => p.fcbnkcharge).Sum() == 0.00) ? 0.00 : lst.Select(p => p.fcbnkcharge).Sum()).ToString("#,##0;(#,##0); ");
            //((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFvatamt")).Text = ((lst.Select(p => p.vatamt).Sum() == 0.00) ? 0.00 : lst.Select(p => p.vatamt).Sum()).ToString("#,##0;(#,##0); ");
            //((Label)this.gvCollectionAll.FooterRow.FindControl("lgvFcglamt")).Text = ((lst.Select(p => p.cglamt).Sum() == 0.00) ? 0.00 : lst.Select(p => p.cglamt).Sum()).ToString("#,##0;(#,##0); ");


        }
        protected void gvCollectionAll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lbtnEdit");
                HyperLink pLink = (HyperLink)e.Row.FindControl("LbtnPrint");
                LinkButton lnkCheck = (LinkButton)e.Row.FindControl("lnkCheck");

                string grpmemo = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "memono")).ToString();
                string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                string custid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "custid")).ToString();
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                string comcod = this.GetCompCode();
                //string ptype = this.Request.QueryString["Type"].ToString();
                hlink3.NavigateUrl = "~/F_19_EXP/MoneyReceipt2?Type=Edit&genno=" + grpmemo + "&centrid=" + "&sircode=" + custid;
                hlink3.Target = "blank";
                pLink.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=MoneyReceipt&mrno=" + grpmemo;
                pLink.Target = "blank";


                if (vounum != "00000000000000")
                {
                    hlink3.Visible = false;
                    lnkCheck.Visible = false;
                }

            }
        }
        protected void lnkCheck_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string AppDate = this.txtRecDate.Text;



            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string centrid = ((Label)gvCollectionAll.Rows[index].FindControl("lblgvcentrid")).Text.ToString();
            string Memono = ((Label)gvCollectionAll.Rows[index].FindControl("lblgvMemono")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "APPCOLLECTION", "", Memono, AppDate, userid, Terminal, Sessionid, Posteddat);

            if (result == true)
            {
                //this.ImportInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Successfully');", true);
            }
        }
        protected void gvFGDeliv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink lnkbtnApp = (HyperLink)e.Row.FindControl("lnkbtnApp");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HypDelivery");
                HyperLink hlExprotUp = (HyperLink)e.Row.FindControl("HypExprotUp");
                HyperLink StockCheck = (HyperLink)e.Row.FindControl("HypStockCheck");

                
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string custid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "custid")).ToString();
                string invno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "invno")).ToString();
                string delvtrm = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "delvtrm")).ToString();
                string delvdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "delvdate")).ToString("dd-MMM-yyyy");
                string printformat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "printformat"));

                //if(comcod == "5305" || comcod == "5306") ---------added by Pritom
                //{
                //    hlink2.NavigateUrl = "~/F_19_EXP/CreatePackList?Type=Entry&actcode=" + actcode + "&genno=" + invno;
                //}

                hlink2.NavigateUrl = "~/F_19_EXP/ExportMgt?Type=Entry&actcode=" + actcode + "&genno=" + invno;

                //HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint.aspx?Type=DelChallan&actcode=" + actcode + "&genno=" + invno;
                HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=ExpInvoice&genno=" + invno;
                hlink3.NavigateUrl = "~/F_19_EXP/DelvChallan?Type=Entry&actcode=" + custid + "&genno=" + invno + "&sircode=";
                lnkbtnApp.NavigateUrl = "~/F_19_EXP/ExportMgt?Type=Approve&actcode=" + actcode + "&genno=" + invno;
                hlExprotUp.NavigateUrl = "~/F_21_GAcc/AccIncomeOfOrd?Type=Entry&actcode=" + invno + "&delvtrm=" + delvtrm + "&date=" + delvdate;
                StockCheck.NavigateUrl= "~/F_19_EXP/InvWiseStockChecker?genno=" + invno;
            }
        }
        protected void gvFGChln_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink btnEdit = (HyperLink)e.Row.FindControl("lnkbtnEdit");

                HyperLink HPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string invno = "";// Convert.ToString(DataBinder.Eval(e.Row.DataItem, "invno")).ToString();
                string dchno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dchno")).ToString();
                
                
                if(comcod == "5301")
                {
                    HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=DelChallan&actcode=" + actcode + "&genno=" + dchno + "&PrintFormat=2";
                }
                else
                {
                    HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=DelChallan&actcode=" + actcode + "&genno=" + dchno;
                }

                btnEdit.NavigateUrl = "~/F_19_EXP/DelvChallan?Type=Edit&actcode=" + actcode + "&genno=" + invno + "&sircode=" + dchno;

                string value = this.RadioButtonList1.SelectedValue.ToString();
                if (value != "5")
                {
                    
                    hlink2.NavigateUrl = "~/F_19_EXP/DelvChallan?Type=Approve&actcode=" + actcode + "&genno=" + invno + "&sircode=" + dchno;
                }
                else
                {
                    hlink2.Visible = false;
                }
               
                //hlink2.NavigateUrl = "~/F_19_EXP/DelvChallan.aspx?Type=Approve&actcode=" + actcode + "&genno=" + dchno;
                //HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint.aspx?Type=DelChallan&actcode=" + actcode + "&genno=" + dchno;

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
    }
}