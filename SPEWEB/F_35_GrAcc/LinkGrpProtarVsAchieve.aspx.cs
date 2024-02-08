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


namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkGrpProtarVsAchieve : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "WORKING BUDGET Vs. ACHIEVEMENT VIEW/EDIT";
                this.ViewSection();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void ViewSection()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Protvach":
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.MultiView1.ActiveViewIndex = 0;

                    break;

                case "IndProduc":
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
                    this.chkGraph.Visible = true;
                    this.GetLCName();
                    this.MultiView1.ActiveViewIndex = 1;
                    // this.ShowIndProduc();
                    break;

                case "RptAllPro":
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.MultiView1.ActiveViewIndex = 2;
                    this.chkGraph.Visible = true;
                    break;

            }
        }


        private string GetCompCode()
        {

            return (this.Request.QueryString["comcod"].ToString());

        }





        private void ShowProTarVsAch()
        {

            Session.Remove("tblprotvac");
            string comcod = this.GetCompCode();
            string fromdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "RPTPROTARGET", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtvsach.DataSource = null;
                this.gvtvsach.DataBind();
                return;
            }
            Session["tblprotvac"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowIndProduc()
        {

            Session.Remove("tblprotvac");

            string comcod = this.GetCompCode();

            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string date = this.Request.QueryString["Date2"].ToString();

            string mlccod = this.ddlMLc.SelectedValue.ToString();


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "RPTPROTRINF", "", date, mlccod, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.RptIndPro.DataSource = null;
                this.RptIndPro.DataBind();
                return;
            }

            Session["tblprotvac"] = ds1.Tables[0];
            this.Data_Bind();
            this.showChart();
            ds1.Dispose();

        }
        private void ShowAllProduc()
        {

            Session.Remove("tblprotvac");
            string comcod = this.GetCompCode();
            string fromdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "RPTPRODAll", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.RptAllPro.DataSource = null;
                this.RptAllPro.DataBind();
                return;
            }
            Session["tblprotvac"] = ds1.Tables[0];
            this.Data_Bind();
            this.showChartAll();
            ds1.Dispose();
        }

        private void GetLCName()
        {
            string comcod = this.GetCompCode();
            string txtsrch = "%" + this.txtpmlcsrch.Text.Trim() + "%";





            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "GETMASTERLC", txtsrch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlMLc.DataTextField = "actdesc";
            this.ddlMLc.DataValueField = "actcode";
            this.ddlMLc.DataSource = ds1.Tables[0];
            this.ddlMLc.DataBind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string flrcode, linecode;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "Protvach":
                    flrcode = dt1.Rows[0]["flrcode"].ToString();
                    linecode = dt1.Rows[0]["linecode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["flrcode"].ToString() == flrcode && dt1.Rows[j]["linecode"].ToString() == linecode)
                        {


                            dt1.Rows[j]["flrdesc"] = "";
                            dt1.Rows[j]["linedesc"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["flrcode"].ToString() == flrcode)
                                dt1.Rows[j]["flrdesc"] = "";

                            if (dt1.Rows[j]["linecode"].ToString() == linecode)
                                dt1.Rows[j]["linedesc"] = "";

                        }

                        flrcode = dt1.Rows[j]["flrcode"].ToString();
                        linecode = dt1.Rows[j]["linecode"].ToString();


                    }


                    break;



            }


            return dt1;

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblprotvac"];

            switch (type)
            {
                case "Protvach":
                    this.gvtvsach.DataSource = dt;
                    this.gvtvsach.DataBind();
                    break;

                case "IndProduc":
                    this.RptIndPro.DataSource = dt;
                    this.RptIndPro.DataBind();
                    this.FooterCal();
                    break;

                case "RptAllPro":
                    this.RptAllPro.DataSource = dt;
                    this.RptAllPro.DataBind();
                    this.FooterCal();
                    break;

            }


        }
        protected void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblprotvac"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {


                case "Protvach":

                    break;
                case "IndProduc":



                    ((Label)this.RptIndPro.FooterRow.FindControl("lblgvFortarget")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ppqty)", "")) ?
                                    0 : dt.Compute("sum(ppqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.RptIndPro.FooterRow.FindControl("lblgvFProqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proqty)", "")) ?
                                    0 : dt.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.RptIndPro.FooterRow.FindControl("lblgvFProqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tqty)", "")) ?
                                    0 : dt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "RptAllPro":
                    ((Label)this.RptAllPro.FooterRow.FindControl("lblgvCapacityAll")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(capacity)", "")) ?
                                    0 : dt.Compute("sum(capacity)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.RptAllPro.FooterRow.FindControl("lblgvFoProqtyAll")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proqty)", "")) ?
                                    0 : dt.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.RptAllPro.FooterRow.FindControl("lgvFoTargetQunAll")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tqty)", "")) ?
                                    0 : dt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "Protvach":
                    this.PrintTarVsAch();
                    break;

                case "IndProduc":
                    this.PrintIndProduc();
                    break;

                case "RptAllPro":
                    this.PrintAllProduc();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Report Budget";
                string eventdesc = "Print Report: " + type; ;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void PrintTarVsAch()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_15_Pro.RptProtarVsAchieve();

            //DataTable dt = (DataTable)Session["tblprotvac"];

            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource((DataTable)Session["tblprotvac"]);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            // ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintIndProduc()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new ReportDocument();

            //if (this.chkGraph.Checked)
            //{

            //    rptstk = new RMGiRPT.R_15_Pro.RptIndvProduGraph();

            //}

            //else
            //{

            //   rptstk = new RMGiRPT.R_15_Pro.RptIndvProduction();

            //}
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtorderno = rptstk.ReportDefinition.ReportObjects["txtorderno"] as TextObject;
            //txtorderno.Text = "Order No.:-"+this.ddlMLc.SelectedItem.Text.Substring(14);
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text =  "Date:  " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") ;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource((DataTable)Session["tblprotvac"]);
            //Session["Report1"] = rptstk;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            // ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintAllProduc()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new ReportDocument();

            //if (this.chkGraph.Checked)
            //{

            //    rptstk = new RMGiRPT.R_15_Pro.RptAllProduGraph();

            //}


            //else
            //{

            //    rptstk = new RMGiRPT.R_15_Pro.RptAllProduction();
            //}

            //DataTable dt = (DataTable)Session["tblprotvac"];

            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblprotvac"]);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            // ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void showChart()
        {


            Chart1.Series["Cum Orginal Target"].XValueMember = "prodate1";
            Chart1.Series["Cum Orginal Target"].YValueMembers = "comppqty";
            Chart1.Series["Cum Working Target"].XValueMember = "prodate1";
            Chart1.Series["Cum Working Target"].YValueMembers = "comtqty";
            Chart1.Series["Cum Achievement"].XValueMember = "prodate1";
            Chart1.Series["Cum Achievement"].YValueMembers = "comproqty";


            Chart1.DataSource = (DataTable)Session["tblprotvac"];
            Chart1.DataBind();
        }

        private void showChartAll()
        {

            Chart2.Series["Cum Capacity"].XValueMember = "prodate1";
            Chart2.Series["Cum Capacity"].YValueMembers = "comcap";

            Chart2.Series["Cum Quantity"].XValueMember = "prodate1";
            Chart2.Series["Cum Quantity"].YValueMembers = "comtqty";

            Chart2.Series["Cum Production Quantity"].XValueMember = "prodate1";
            Chart2.Series["Cum Production Quantity"].YValueMembers = "comproqty";

            Chart2.DataSource = (DataTable)Session["tblprotvac"];
            Chart2.DataBind();
        }


        protected void gvtvsach_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label unitdesc = (Label)e.Row.FindControl("lblgvDesc");
                Label target = (Label)e.Row.FindControl("lgvtarget");
                Label production = (Label)e.Row.FindControl("lgvproduction");
                Label sorexcess = (Label)e.Row.FindControl("lgvsorexcess");
                Label macno = (Label)e.Row.FindControl("lgvmacno");
                Label capacity = (Label)e.Row.FindControl("lgvcapacity");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "linecode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    unitdesc.Font.Bold = true;
                    target.Font.Bold = true;
                    production.Font.Bold = true;
                    sorexcess.Font.Bold = true;
                    macno.Font.Bold = true;
                    capacity.Font.Bold = true;
                    unitdesc.Style.Add("text-align", "right");


                }

            }

        }
        protected void gvRptIndPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlProdInv_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowIndProduc();
        }
        protected void imgbtnFindPMlc_Click(object sender, EventArgs e)
        {
            this.GetLCName();
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Protvach":
                    this.ShowProTarVsAch();
                    break;

                case "IndProduc":

                    this.ShowIndProduc();
                    break;

                case "RptAllPro":
                    this.ShowAllProduc();
                    break;
            }
        }
    }
}