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
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_10_Procur
{
    public partial class RptPurchasetracking : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                ((Label)this.Master.FindControl("lblTitle")).Text = "Day Wise Purchase Report";
                this.ShowView();
                this.ShowValue();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());



        }




        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {


                case "Purchasetrk":


                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


            }



        }











        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "Purchasetrk":
                    this.RptPurchaseTrack();
                    break;




            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }


        private void RptPurchaseTrack()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reqno = this.Request.QueryString["reqno"].ToString();

            var list = dt.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.DayWisePurchase>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptDayWisePurchase", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptitle", "Day Wise Purchase Report"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //DataTable dt = (DataTable)Session["tblpurchase"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string reqno = this.Request.QueryString["reqno"].ToString();

            //ReportDocument rptpur = new RMGiRPT.R_09_Commer.RptPurchaseTra();
            //TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtProjectName = rptpur.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString();
            ////TextObject txtreqno = rptpur.ReportDefinition.ReportObjects["txtreqno"] as TextObject;
            ////txtreqno.Text = "Req. No: " + ASTUtility.Left(this.ddlReqNo01.SelectedItem.Text.Trim(), 11);

            //TextObject rpttxtMRFno = rptpur.ReportDefinition.ReportObjects["txtMRFno"] as TextObject;
            //rpttxtMRFno.Text = "MRF No: " + (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["mrfno"].ToString();

            ////TextObject txtFDate1 = rptpur.ReportDefinition.ReportObjects["txtreqdate"] as TextObject;
            ////txtFDate1.Text = "Req. Date: " + this.ddlReqNo01.SelectedItem.Text.Substring(13, 11);

            //TextObject txtNarration = rptpur.ReportDefinition.ReportObjects["txtNarration"] as TextObject;
            //txtNarration.Text = this.txtReqNarr.Text.Trim();

            //TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptpur.SetDataSource(dt);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptpur.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptpur;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }


        private void ShowValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {



                case "Purchasetrk":
                    //this.ShowPurChaseTrk();
                    this.pnlnarration.Visible = true;
                    this.ShowPurChaseTrk01();
                    break;



            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Show Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }






        private void ShowPurChaseTrk01()
        {



            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string reqno = this.Request.QueryString["reqno"].ToString();


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            //this.txtReqNarr.Text = ds1.Tables[1].Rows.Count==0 ? "" :ds1.Tables[1].Rows[0]["reqnar"].ToString();
            ///this.lblshipsupdate.Text = ds1.Tables[0].Rows[0]["shipsupdat"].ToString();
            this.LoadGrid();


        }



        private string CompBudgetBalance()
        {
            string comcod = this.GetComeCode();
            string reqorapproved = "";
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                    reqorapproved = "req";
                    break;

                default:
                    break;



            }
            return reqorapproved;

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();

            string reqno = "", matcode = "", spcfcod = "";
            switch (rpt)
            {

                case "Purchasetrk":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

                    }



                    //reqno = dt1.Rows[0]["reqno"].ToString();
                    //matcode = dt1.Rows[0]["rsircode"].ToString();
                    // spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //    {

                    //        dt1.Rows[j]["reqno1"] = "";
                    //        dt1.Rows[j]["mrfno"] = "";
                    //        dt1.Rows[j]["reqdat"] = "";
                    //        dt1.Rows[j]["shipsupdat"] = "";
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";

                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    //        {
                    //            dt1.Rows[j]["reqno1"] = "";
                    //            dt1.Rows[j]["mrfno"] = "";
                    //            dt1.Rows[j]["reqdat"] = "";
                    //            dt1.Rows[j]["shipsupdat"] = "";
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //        }
                    //         if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //             dt1.Rows[j]["rsirdesc"] = "";
                    //         if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //             dt1.Rows[j]["spcfdesc"] = "";





                    //    }


                    //    reqno = dt1.Rows[j]["reqno"].ToString();
                    //    matcode = dt1.Rows[j]["rsircode"].ToString();
                    //    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //}

                    break;




                case "PurBilltk":
                    //reqno = dt1.Rows[0]["reqno"].ToString();
                    //matcode = dt1.Rows[0]["rsircode"].ToString();
                    //spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //    {

                    //        dt1.Rows[j]["reqno1"] = "";
                    //        dt1.Rows[j]["mrfno"] = "";
                    //        dt1.Rows[j]["reqdat"] = "";
                    //        dt1.Rows[j]["shipsupdat"] = "";
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";

                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    //        {
                    //            dt1.Rows[j]["reqno1"] = "";
                    //            dt1.Rows[j]["mrfno"] = "";
                    //            dt1.Rows[j]["reqdat"] = "";
                    //            dt1.Rows[j]["shipsupdat"] = "";
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //        }
                    //        if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //            dt1.Rows[j]["rsirdesc"] = "";
                    //        if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //            dt1.Rows[j]["spcfdesc"] = "";





                    //    }


                    //    reqno = dt1.Rows[j]["reqno"].ToString();
                    //    matcode = dt1.Rows[j]["rsircode"].ToString();
                    //    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //}

                    break;


                case "Purchasetrk02":
                    //string ppactcode = dt1.Rows[0]["pactcode"].ToString();
                    //string matcode = dt1.Rows[0]["rsircode"].ToString();
                    //string spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["pactcode"].ToString() == ppactcode && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() ==spcfcod)
                    //    {
                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";
                    //        dt1.Rows[j]["areqty"] = 0.0000000;
                    //    }

                    //    else
                    //    {
                    //         if (dt1.Rows[j]["pactcode"].ToString() == ppactcode)
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //         if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //             dt1.Rows[j]["rsirdesc"] = "";
                    //         if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //             dt1.Rows[j]["spcfdesc"] = "";




                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //    }
                    //}

                    break;



                case "MatRateVar":

                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                        }

                        rsircode = dt1.Rows[j]["rsircode"].ToString();

                    }

                    break;


            }


            return dt1;

        }


        private void LoadGrid()
        {

            try
            {
                DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();

                if ((dt.Rows.Count == 0)) //Problem
                    return;

                string rpt = this.Request.QueryString["Type"].ToString().Trim();
                switch (rpt)
                {


                    case "PenBill":



                        break;

                    case "Purchasetrk":
                        this.gvPurstk01.DataSource = dt;
                        this.gvPurstk01.DataBind();

                        break;


                }

            }
            catch (Exception ex)
            {


            }




        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvPurSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.LoadGrid();
        }










        protected void gvPurstk01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string reqno = this.Request.QueryString["reqno"].ToString();
                string comcod = this.GetComeCode();
                HyperLink printlink = (HyperLink)e.Row.FindControl("HypLinkPrint");
                string grp = ((Label)e.Row.FindControl("lblGrp")).Text.ToString();
                string genno = ((Label)e.Row.FindControl("lblGenno")).Text.ToString();
                string actcode = ((Label)e.Row.FindControl("lblactcode")).Text.ToString();

                string reqtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();
                reqtype = (reqtype != "LC") ? "Local" : "Import";

                switch (grp)
                {
                    case "A":
                    case "C":
                        printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=" + reqtype + "&AppType=YES";
                        break;
                    case "D":
                        printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + genno;
                        break;
                    case "E":
                        printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=OrderPrint&comcod=" + comcod + "&orderno=" + genno + "&ReqType=Local&AppType=YES";
                        break;
                    case "F":
                        if (reqtype == "Local")
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=MRRPrint&comcod=" + comcod + "&mrrno=" + genno + "&ReqType=Local&AppType=YES";

                        }
                        else
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=LCRecPrint&comcod=" + comcod + "&genno=" + genno + "&centrid=&actcode=" + actcode;

                        }
                        break;
                    case "G":
                        if (reqtype == "Local")
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=BillPrint&comcod=" + comcod + "&billno=" + genno + "&ReqType=Local&AppType=YES";
                        }
                        else
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=LCQcPrint&comcod=" + comcod + "&genno=" + genno + "&centrid=" + actcode + "&actcode=";

                        }
                        break;
                }



            }
        }
    }
}