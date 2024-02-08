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
    public partial class LinkProjectCollBrkDown : System.Web.UI.Page
    {
        ProcessAccess MisData = new ProcessAccess();
        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString() == "ClLedger") ? "Client Ledger Report" :
                    (this.Request.QueryString["Type"].ToString() == "SpLedger") ? "Subsidiary Ledger Report" : "PROJECT WISE COLLECTION BREAK DOWN REPORT";
                this.SelectView();
            }
        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowReport();
                    break;

                case "ClientLedger":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.PrintCleintLedger();
                    break;
                case "IndPrjStDet":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowIndPrjDet();
                    break;
                case "SpLedger":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowSPLedger();
                    break;
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    this.PrjWiseCollBrekDown();
                    break;

                case "ClientLedger":
                    this.PrintCleintLedger();
                    break;
                case "IndPrjStDet":
                    this.RptIndPrjStDet();
                    break;
                case "SpLedger":
                    this.RptSPLedger();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.lblHeadtitle.Text;
                string eventdesc = "Print Report";
                string eventdesc2 = this.Request.QueryString["Type"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void PrjWiseCollBrekDown()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string prjname = this.lblActDesc.Text.Trim();
            //ReportDocument rpcp = new ReportDocument();
            //if (ASTUtility.Left(Request.QueryString["pactcode"].ToString().Trim(), 2) == "41")
            //{
            //    rpcp = new RealERPRPT.R_32_Mis.RptPrjWiseBrkDown();
            //}
            //else
            //{
            //    rpcp = new RealERPRPT.R_32_Mis.RptPrjWiseBrkDown1();
            //}
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "Project Name: " + prjname;
            //TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "As on Date: " + Request.QueryString["Date1"].ToString().Trim(); ;
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////if (ConstantInfo.LogStatus == true)
            ////{
            ////    string eventtype = this.lblHeadtitle.Text;
            ////    string eventdesc = "Print Report";
            ////    //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
            ////    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, "");
            ////}

            //rpcp.SetDataSource((DataTable)Session["tblPrjData"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintCleintLedger()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            //string custid = Request.QueryString["usircode"].ToString().Trim();
            //string Date = Request.QueryString["Date1"].ToString().Trim();
            //DataSet ds5 = MisData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");
            //if (ds5.Tables[0].Rows.Count == 0)
            //    return;
            //DataTable tblins = this.HiddenSameData(ds5.Tables[0]);

            //this.LblPrjDesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
            //this.lblCustName.Text = ds5.Tables[1].Rows[0]["name"].ToString(); 
            //this.lblDate1.Text =Request.QueryString["Date1"].ToString().Trim();


            //ReportDocument rptStatus = new RealERPRPT.R_23_CR.RptClientLedger();
            //TextObject rpttxtCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rpttxtCompanyName.Text = comnam;
            //TextObject rptcompadd = rptStatus.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //rptcompadd.Text = comadd;

            //TextObject txtDate = rptStatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "Print Date: " + Request.QueryString["Date1"].ToString().Trim();
            //TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["custname"] as TextObject;
            //rptcustname.Text = ds5.Tables[1].Rows[0]["name"].ToString();
            //TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
            //rptCustAdd.Text = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            //TextObject rptCustPhone = rptStatus.ReportDefinition.ReportObjects["CustPhone"] as TextObject;
            //rptCustPhone.Text = ds5.Tables[1].Rows[0]["telephone"].ToString();
            //TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
            //rptpactdesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
            //TextObject rptUnitDesc = rptStatus.ReportDefinition.ReportObjects["UnitDesc"] as TextObject;
            //rptUnitDesc.Text = ds5.Tables[1].Rows[0]["aptname"].ToString();
            //TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
            //rptUsize.Text = ds5.Tables[1].Rows[0]["aptsize"].ToString();

            //TextObject rptSalesteam = rptStatus.ReportDefinition.ReportObjects["Salesteam"] as TextObject;
            //rptSalesteam.Text = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            //TextObject rptsalesdate = rptStatus.ReportDefinition.ReportObjects["salesdate"] as TextObject;
            //rptsalesdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            //TextObject rptagreementdate = rptStatus.ReportDefinition.ReportObjects["agreementdate"] as TextObject;
            //rptagreementdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            //TextObject rptHandoverdate = rptStatus.ReportDefinition.ReportObjects["Handoverdate"] as TextObject;
            //rptHandoverdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            //TextObject rptapartmentprice = rptStatus.ReportDefinition.ReportObjects["apartmentprice"] as TextObject;
            //rptapartmentprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            //TextObject rptcarparking = rptStatus.ReportDefinition.ReportObjects["carparking"] as TextObject;
            //rptcarparking.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            //TextObject rptUtility = rptStatus.ReportDefinition.ReportObjects["Utility"] as TextObject;
            //rptUtility.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
            //TextObject rptregistration = rptStatus.ReportDefinition.ReportObjects["registration"] as TextObject;
            //rptregistration.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0);");
            //TextObject rptdevelopmentcost = rptStatus.ReportDefinition.ReportObjects["developmentcost"] as TextObject;
            //rptdevelopmentcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");
            //TextObject rptwelfarefund = rptStatus.ReportDefinition.ReportObjects["welfarefund"] as TextObject;
            //rptwelfarefund.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            //TextObject rptOthers = rptStatus.ReportDefinition.ReportObjects["Others"] as TextObject;
            //rptOthers.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");

            //TextObject rpttoprice = rptStatus.ReportDefinition.ReportObjects["toprice"] as TextObject;
            //rpttoprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");

            //TextObject rptdiscount = rptStatus.ReportDefinition.ReportObjects["discount"] as TextObject;
            //rptdiscount.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");
            ////TextObject rptaccost = rptStatus.ReportDefinition.ReportObjects["accost"] as TextObject;
            ////rptaccost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            ////---------
            //TextObject rptbudgcost = rptStatus.ReportDefinition.ReportObjects["bgdcost"] as TextObject;
            //rptbudgcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["bgcost"]).ToString("#,##0;(#,##0); ");
            //TextObject rptcooperative = rptStatus.ReportDefinition.ReportObjects["coopcost"] as TextObject;
            //rptcooperative.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptStatus.SetDataSource(tblins);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptStatus.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptStatus;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptIndPrjStDet()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string prjname = this.lblIndPrjDesc.Text.Trim();
            //ReportDocument rpcp = new RealERPRPT.R_32_Mis.RptIndProjDet();

            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "Project Name: " + prjname;
            //TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "As on Date: " + Request.QueryString["Date1"].ToString().Trim(); ;
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource((DataTable)Session["tblPrjData"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptSPLedger()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //RealERPRPT.R_17_Acc.RptAccSLedger rptstk = new RealERPRPT.R_17_Acc.RptAccSLedger();
            //string Resdesc = "SUBSIDIARY HEAD: " + this.lblLGResDesc.Text;
            //DataTable dt = (DataTable)Session["tblPrjData"];
            //if (dt == null)
            //    return;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = "Date: " + this.lblLGDate.Text;
            //TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //rpttxtAccDesc.Text = "ACCOUNT HEAD: " + this.lblLGPrjDesc.Text;
            //TextObject txtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
            //txtResDesc.Text = Resdesc;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblPrjData"]);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void ShowReport()
        {
            Session.Remove("tblPrjData");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            this.lblDate.Text = Request.QueryString["Date1"].ToString().Trim();
            string date = Request.QueryString["Date1"].ToString().Trim();
            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTCLIENTSTAT", pactcode, date, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvPrjWiseCollBrkDown.DataSource = null;
                this.gvPrjWiseCollBrkDown.DataBind();
                return;
            }
            this.lblActDesc.Text = ds2.Tables[1].Rows[0]["acttdesc"].ToString();
            Session["tblPrjData"] = ds2.Tables[0];
            this.Data_Bind();
        }
        private void ShowIndPrjDet()
        {
            Session.Remove("tblPrjData");
            string comcod = Request.QueryString["comcod"].ToString().Trim();
            string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            this.lblIndDate.Text = Request.QueryString["Date1"].ToString().Trim();
            string date = Request.QueryString["Date1"].ToString().Trim();
            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTPRJSTATUSDETAILS", pactcode, date, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvIndPrjDet.DataSource = null;
                this.gvIndPrjDet.DataBind();
                return;
            }
            this.lblIndPrjDesc.Text = ds2.Tables[1].Rows[0]["actdesc"].ToString();
            Session["tblPrjData"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private string GetDate()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "GETOPDATE", "", "", "", "", "", "", "", "", "");
            string date1 = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            return date1;

        }
        private void ShowSPLedger()
        {
            Session.Remove("tblPrjData");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            this.lblLGDate.Text = Request.QueryString["Date1"].ToString().Trim();
            string date1 = this.GetDate();
            string date2 = Request.QueryString["Date1"].ToString().Trim();
            string rescode = Request.QueryString["rescode"].ToString().Trim();
            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", pactcode, date1, date2, rescode, "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvSPLedger.DataSource = null;
                this.gvSPLedger.DataBind();
                return;
            }
            this.lblLGPrjDesc.Text = ds2.Tables[1].Rows[0]["actdesc"].ToString();
            this.lblLGResDesc.Text = ds2.Tables[1].Rows[0]["resdesc"].ToString();
            this.BalCalculation(ds2.Tables[0]);
            Session["tblPrjData"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    this.gvPrjWiseCollBrkDown.DataSource = (DataTable)Session["tblPrjData"];
                    this.gvPrjWiseCollBrkDown.DataBind();
                    if (ASTUtility.Left(Request.QueryString["pactcode"].ToString().Trim(), 2) == "47")
                    {
                        this.gvPrjWiseCollBrkDown.Columns[2].Visible = false;
                        this.gvPrjWiseCollBrkDown.Columns[1].HeaderText = "Customer Name";
                    }
                    this.FooterCalculation();
                    break;

                case "ClientLedger":

                    break;
                case "IndPrjStDet":
                    this.gvIndPrjDet.DataSource = (DataTable)Session["tblPrjData"];
                    this.gvIndPrjDet.DataBind();
                    break;
                case "SpLedger":
                    this.gvSPLedger.DataSource = (DataTable)Session["tblPrjData"];
                    this.gvSPLedger.DataBind();
                    break;
            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    ////string gcod = dt1.Rows[0]["gcod"].ToString();

                    ////    for (int j = 1; j < dt1.Rows.Count; j++)
                    ////    {
                    ////        if (dt1.Rows[j]["gcod"].ToString() == gcod)
                    ////        {
                    ////            gcod = dt1.Rows[j]["gcod"].ToString();
                    ////            dt1.Rows[j]["gcod"] = "";
                    ////            dt1.Rows[j]["gdesc"] = "";
                    ////            dt1.Rows[j]["pactcode"] = "";
                    ////            dt1.Rows[j]["usircode"] = "";
                    ////            dt1.Rows[j]["schamt"] = 0;
                    ////            dt1.Rows[j]["schdate"] = "";
                    ////        }

                    ////        else
                    ////        {
                    ////            gcod = dt1.Rows[j]["gcod"].ToString();
                    ////        }

                    ////    }
                    break;

                case "ClientLedger":

                    break;
                case "IndPrjStDet":
                    grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }
                        grp = dt1.Rows[j]["grp"].ToString();


                    }
                    break;
                case "SpLedger":
                    grp = dt1.Rows[0]["grp"].ToString();
                    string Date1 = dt1.Rows[0]["voudat1"].ToString();
                    string vounum = dt1.Rows[0]["vounum1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                        {

                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                            //dt1.Rows[j]["refnum"] = "";
                        }

                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";

                        }
                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                        }

                        grp = dt1.Rows[j]["grp"].ToString();
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                    }


                    break;

            }



            return dt1;


        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {


                if ((dt.Rows[i]["vounum"]).ToString() == "TOTAL")
                    break;

                if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                {
                    dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                    cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                    balamt = balamt + (dramt - cramt);
                    dt.Rows[i]["balamt"] = balamt;
                }
            }
            return dt;


        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblPrjData"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFSaVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsalamt)", "")) ? 0.00
                         : dt.Compute("Sum(tsalamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgFClrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tclramt)", "")) ? 0.00
                        : dt.Compute("Sum(tclramt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFtretamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(retcheque)", "")) ?
                                            0.00 : dt.Compute("sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFtframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fcheque)", "")) ?
                                            0.00 : dt.Compute("sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFtpdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pcheque)", "")) ?
                                           0.00 : dt.Compute("sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFAmtrep")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trecev)", "")) ? 0.00
                        : dt.Compute("Sum(trecev)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFNOI")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(noiamt)", "")) ? 0.00
                        : dt.Compute("Sum(noiamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFStdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stdamt)", "")) ? 0.00
                        : dt.Compute("Sum(stdamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFcuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cuamt)", "")) ? 0.00
                        : dt.Compute("Sum(cuamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFTamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tamt)", "")) ? 0.00
                        : dt.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0);  ");
                    break;

                case "ClientLedger":

                    break;
                case "IndPrjStDet":

                    break;
                case "SpLedger":
                    break;
            }

        }

        protected void gvPrjWiseCollBrkDown_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            HyperLink hlink2 = (HyperLink)e.Row.FindControl("HLgvDesc1");
            //Label actcode = (Label)e.Row.FindControl("lblgvActcode");
            //Label usircode = (Label)e.Row.FindControl("lblgvUsircod");
            string Date1 = this.lblDate.Text;
            string actcode = ((Label)e.Row.FindControl("lblgvActcode")).Text;
            string usircode = ((Label)e.Row.FindControl("lblgvUsircod")).Text;
            if (ASTUtility.Left(rescode1, 1) == "6")
            {
                hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=ClientLedger&pactcode=" + actcode + "&usircode=" + usircode + "&Date1=" + Date1;
            }
            if (ASTUtility.Left(rescode1, 1) == "5")
            {
                hlink2.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=ClientLedger&pactcode=" + actcode + "&usircode=" + usircode + "&Date1=" + Date1;
            }
        }
        protected void gvIndPrjDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actcode = (Label)e.Row.FindControl("lblgvCode");
                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label Amount = (Label)e.Row.FindControl("lgvAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left((code), 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                    Amount.Style.Add("text-align", "Right");
                }
                if (ASTUtility.Right((code), 4) == "AAAA")
                {
                    actcode.Visible = false;
                    Amount.Font.Bold = true;
                }


            }
        }
        protected void gvSPLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;


            HyperLink hlinkbill = (HyperLink)e.Row.FindControl("HLgvRemarks");
            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;
            string billno = hlinkbill.Text;

            if (mVOUNUM.Trim().Length == 14 && ASTUtility.Left(mVOUNUM.Trim(), 2) != "PV")
            {
                //hlink1.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + mVOUNUM;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);



            }

            if (billno == "") ;
            else if (billno.Length > 3 && billno.Substring(0, 3) == "PBL")
            {
                hlinkbill.NavigateUrl = "LinkMis.aspx?Type=BillConfirmation&billno=" + billno;
            }

        }
    }
}