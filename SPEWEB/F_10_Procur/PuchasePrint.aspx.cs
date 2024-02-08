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
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SPEWEB.F_10_Procur
{
    public partial class PuchasePrint : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.GetCompanyName();
            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {
                case "ReqPrint":
                    this.Req_Print_Rdlc();
                    break;

                case "SCPrepnation":
                    this.CSPrint();
                    break;

                case "OrdProcess":
                    this.Ord_Process_Print();
                    break;

                case "OrderPrint":

                    //  this.Order_Print();
                    this.OrderPrint("");

                    break;

                case "MRRPrint":
                    this.PurMRR_RDLC();
                    //this.PurMRR_Print();
                    break;

                case "BillPrint":
                    this.PurBill_Print();
                    break;

                case "IssueChallan":
                    break;

                case "ImportApp":
                    switch (comcod)
                    {
                        case "5305":
                        case "5306":
                            this.ImportAppFbPrint("");
                            break;
                        default:
                            this.ImportAppPrint("");
                            break;
                    }
                    break;

                case "LCRecPrint":
                    this.LCRecPrint();
                    break;

                case "LCQcPrint":
                    this.LCQcPrint();
                    break;

                case "OrderSavePdf":
                    this.SendMail();
                    break;

            }


        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }


        }
        private void GetCompanyName()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string usrid = ASTUtility.Right(hst["usrid"].ToString(), 3);
            //string comcod = hst["mcomcod"].ToString();
            //DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_03", "GETCOMPNAME", usrid, "", "", "", "", "", "", "", "");
            //var complist = ds.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.EClassPur.SisterCompList>();
            //ViewState["tblSistercomp"] = complist;

        }
        private void PurBill_Print()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.Request.QueryString["date"].ToString();
            string mBILLNo = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[1].Rows.Count == 0)
            {
                return;
            }
            DataTable dt = ds1.Tables[0];
            DataTable dt1 = ds1.Tables[1];

            mBILLNo = Convert.ToString(dt1.Rows[0]["billno1"]);


            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var lst = dt.DataTableToList<SPEENTITY.C_09_Commer.EClassBBLMatInfo>();
            string mrrdat = Convert.ToDateTime(dt1.Rows[0]["billdat"]).ToString("dd-MMM-yyyy");
            string orderno = Convert.ToString(dt.Rows[0]["orderno1"]);
            string mrfno = Convert.ToString(dt.Rows[0]["mrfno"]);
            string challno = Convert.ToString(dt.Rows[0]["chlnno"]);
            string acname = Convert.ToString(dt.Rows[0]["pactdesc"]);

            LocalReport Rpt1 = new LocalReport();
            
             Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_09_Commer.RptMatBBLInfo", lst, null, null);
            Rpt1.EnableExternalImages = true;
            
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Software Generated Bill"));
            Rpt1.SetParameters(new ReportParameter("mrrno", lst[0].mrrno1));
            Rpt1.SetParameters(new ReportParameter("mBILLNo", mBILLNo));
            Rpt1.SetParameters(new ReportParameter("mrrdat", mrrdat));
            Rpt1.SetParameters(new ReportParameter("orderno", orderno));
            Rpt1.SetParameters(new ReportParameter("mrfno", mrfno));
            Rpt1.SetParameters(new ReportParameter("challno", challno));
            Rpt1.SetParameters(new ReportParameter("acname", acname));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Software Generated Bill"));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
     

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
        private void Req_Print_Rdlc()
        {
            string reqno = this.Request.QueryString["reqno"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string unitname = "";

            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETPURREQINFO", reqno, "", "", "", "", "", "", "", "");


            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            string reqno1 = ds1.Tables[1].Rows[0]["reqno"].ToString();
            string reqdat1 = ds1.Tables[1].Rows[0]["reqdat1"].ToString();
            string pactdesc = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string deptdesc = ds1.Tables[1].Rows[0]["deptdesc"].ToString();
            string deptdesc2 = ds1.Tables[1].Rows[0]["deptdesc2"].ToString();
            string address = ds1.Tables[1].Rows[0]["add1"].ToString() + " " + ds1.Tables[1].Rows[0]["add2"].ToString();
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string mlccod = ds1.Tables[1].Rows[0]["mlccod"].ToString();
            string OrderNo = ds1.Tables[1].Rows[0]["mlcdesc"].ToString();
            string BuyerName = ds1.Tables[1].Rows[0]["buyername"].ToString();
            string OrdDate = ds1.Tables[1].Rows[0]["orddate"].ToString();
            string ordVal = Convert.ToDouble(ds1.Tables[1].Rows[0]["ordval"]).ToString("#,##0.00;(#,##0.00);");
            string OrdQty = Convert.ToDouble(ds1.Tables[1].Rows[0]["ordqty"]).ToString("#,##0.00;(#,##0.00);");
            string curdesc = ds1.Tables[0].Rows[0]["curdesc"].ToString();
            string purtypedesc = ds1.Tables[1].Rows[0]["purtypedesc"].ToString();


            string areqamt = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("Sum(areqamt)", "")) ? 0.00 : ds1.Tables[0].Compute("Sum(areqamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            string toamt02 = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("Sum(areqamt)", "")) ? 0.00 : ds1.Tables[0].Compute("Sum(areqamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            string reqnar = ds1.Tables[1].Rows[0]["reqnar"].ToString();

            string preparedby = ds1.Tables[1].Rows[0]["usrname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["posteddat"];
            string checkedby = ds1.Tables[1].Rows[0]["reqchkname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["reqchkdat"];
            string varifiedby = ds1.Tables[1].Rows[0]["revname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["reviewdat"];
            string approvuser = ds1.Tables[1].Rows[0]["appusrname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["apprdat"];
            string EntryDate = ds1.Tables[1].Rows[0]["posteddat"].ToString();
            string exdeldat = ds1.Tables[1].Rows[0]["eddat"].ToString();
            string season = ds1.Tables[1].Rows[0]["seasondesc"].ToString();
            //string paymodedesc = ds1.Tables[1].Rows[0]["paymodedesc"].ToString();

            string ReqType = this.Request.QueryString["ReqType"].ToString();
            string AppType = this.Request.QueryString["AppType"].ToString();


            LocalReport rpt1 = new LocalReport();

            var list1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.RptRequisition>();

            if (ReqType == "Local")
            {
                if (comcod == "5305" || comcod == "5306")
                {
                    //rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurcReqLocalFB", list1, null, null);
                    rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurcReqFB2", list1, null, null);
                    rpt1.EnableExternalImages = true;/// , , 
                    rpt1.SetParameters(new ReportParameter("mrfno", mrfno));
                    rpt1.SetParameters(new ReportParameter("season", season));

                }
                else
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurcReqLocal", list1, null, null);
                }

                string foot = ASTUtility.Concat("", username, printdate);
                rpt1.EnableExternalImages = true;/// , , 


                rpt1.SetParameters(new ReportParameter("RptTitle", (AppType == "NO") ? "Requisition Approval Form" : "Requisition Form"));


                rpt1.SetParameters(new ReportParameter("deptdesc", deptdesc));
                rpt1.SetParameters(new ReportParameter("pactdesc", pactdesc));
                rpt1.SetParameters(new ReportParameter("cCenter", deptdesc2));
                rpt1.SetParameters(new ReportParameter("EntryDate", EntryDate));
                rpt1.SetParameters(new ReportParameter("Note", (approvuser.Length == 0) ? "" : "This is system generated copy. No manual signature is required"));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("purtypedesc", "Purchase Type: " + purtypedesc));
                rpt1.SetParameters(new ReportParameter("footer", foot));
            }

            else
            {

                if (comcod == "5305" || comcod == "5306")
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurcReqFB2", list1, null, null);

                    rpt1.SetParameters(new ReportParameter("season", season));

                }
                else
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurcReq", list1, null, null);

                }
                //rpt1.EnableExternalImages = true;/// , , 

                rpt1.SetParameters(new ReportParameter("RptTitle", "Requisition Approval"));
                rpt1.SetParameters(new ReportParameter("purtypedesc", ""));
                //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo)); ReqNarr

            }


            rpt1.SetParameters(new ReportParameter("comnam", comnam + unitname));
            rpt1.SetParameters(new ReportParameter("reqno1", reqno1));
            rpt1.SetParameters(new ReportParameter("reqdat1", reqdat1));
            rpt1.SetParameters(new ReportParameter("OrderNo", OrderNo));
            rpt1.SetParameters(new ReportParameter("BuyerName", BuyerName));
            rpt1.SetParameters(new ReportParameter("BOMRef", ""));
            rpt1.SetParameters(new ReportParameter("OrdDate", OrdDate));
            rpt1.SetParameters(new ReportParameter("ordVal", ordVal));
            rpt1.SetParameters(new ReportParameter("OrdQty", OrdQty));
            rpt1.SetParameters(new ReportParameter("mrfno", mrfno));

            rpt1.SetParameters(new ReportParameter("mlccod", mlccod));
            rpt1.SetParameters(new ReportParameter("reqnar", "Narration:  " + reqnar));
            rpt1.SetParameters(new ReportParameter("preparedby", preparedby));
            rpt1.SetParameters(new ReportParameter("checkedby", checkedby));
            rpt1.SetParameters(new ReportParameter("varifiedby", varifiedby));
            rpt1.SetParameters(new ReportParameter("approvedby1", varifiedby));
            rpt1.SetParameters(new ReportParameter("approvedby", approvuser));
            rpt1.SetParameters(new ReportParameter("exdeldat", exdeldat));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            rpt1.SetParameters(new ReportParameter("unitdesc", "Unit Price (" + curdesc + ")"));
            rpt1.SetParameters(new ReportParameter("tpridcsc", "Total Price (" + curdesc + ")"));


            Session["Report1"] = rpt1;

            if (Request.QueryString.AllKeys.Contains("pType"))
            {

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                this.Request.QueryString["pType"].ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }



            //}
            //catch (Exception ex)
            //{
            //    //this.lblmsg1.Text = "Error:" + ex.Message;
            //}
        }
        private void CSPrint()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();

            string reqno = this.Request.QueryString["reqno"] ?? "";
            if (reqno == "")
            {
                reqno = this.Request.QueryString["appno"].ToString();
            }



            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "RPTMARKETSURVEY02", reqno, "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[2];

            string CurDate1 = Convert.ToDateTime(dt.Rows[0]["msrdat"]).ToString("dd-MMM-yyyy");
            string narration = dt.Rows[0]["remarks"].ToString();

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.MkrServay02>();
            var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.MkrServay03>().Take(4).ToList();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurMktSurvey", lst, lst1, null);
            int i = 1;
            foreach (SPEENTITY.C_10_Procur.EClassProcur.MkrServay03 lsts in lst1)
            {
                Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                i++;
            }
            string preparedby = ds1.Tables[2].Rows[0]["pusername"].ToString() + "\n" + ds1.Tables[2].Rows[0]["posteddat"];
            string checkedby = ds1.Tables[2].Rows[0]["chkusername"].ToString() + "\n" + ds1.Tables[2].Rows[0]["reqchkdat"];
            string approvedby = ds1.Tables[2].Rows[0]["apusername"].ToString() + "\n" + ds1.Tables[2].Rows[0]["aprvdat"];

            string appid = ds1.Tables[2].Rows[0]["aprvbyid"].ToString();

            //DataView dv;

            //dv = (ds1.Tables[2]).Copy().DefaultView;
            //dv.RowFilter = ("aprvbyid='5301007'");
            //DataTable dt1 = dv.ToTable();

            //dv = (ds1.Tables[2]).Copy().DefaultView;
            //dv.RowFilter = ("aprvbyid='5301008'");
            //DataTable dt2 = dv.ToTable();

            //string approvedby = (appid == "5301007") ? ((dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["apusername"].ToString() + "\n" + ds1.Tables[2].Rows[0]["aprvdat"]) : "";

            //string approvedby1 = (dt2.Rows.Count == 0) ? "" : dt2.Rows[0]["apusername"].ToString() + "\n" + ds1.Tables[2].Rows[0]["aprvdat"];



            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
            Rpt1.SetParameters(new ReportParameter("mMSRNo", reqno));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
            Rpt1.SetParameters(new ReportParameter("narration", "Comments : " + narration));


            Rpt1.SetParameters(new ReportParameter("preparedby", preparedby));
            Rpt1.SetParameters(new ReportParameter("checkedby", checkedby));
            Rpt1.SetParameters(new ReportParameter("approvedby1", ""));
            Rpt1.SetParameters(new ReportParameter("approvedby", approvedby));


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("f1head", ""));
            //Rpt1.SetParameters(new ReportParameter("f2head", ""));
            //Rpt1.SetParameters(new ReportParameter("f3head", ""));
            //Rpt1.SetParameters(new ReportParameter("f4head", ""));


            // }
            //else 
            //{
            //    Rpt1 = RMGiRDLC.RptSetupClass.GetLocalReport("R_10_Procur.RptPurMktSurvey03", lst, lst1, null);


            //    Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //    Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //    Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
            //    Rpt1.SetParameters(new ReportParameter("mMSRNo", reqno));
            //    //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
            //    Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
            //    Rpt1.SetParameters(new ReportParameter("narration", "Comments : " + narration));
            //    //Rpt1.SetParameters(new ReportParameter("MaterialsList", MaterialsList));
            //    // Rpt1.SetParameters(new ReportParameter("Specification", Specification));
            //    Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //    Rpt1.SetParameters(new ReportParameter("f1head", ""));
            //    Rpt1.SetParameters(new ReportParameter("f2head", ""));
            //    Rpt1.SetParameters(new ReportParameter("f3head", ""));
            //    //Rpt1.SetParameters(new ReportParameter("f4head", ""));
            //}


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            //string type = "PDF";
            //ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }

        private void Ord_Process_Print()
        {
        }

        private void OrderPrint(string type)
        {
            string orderno = Request.QueryString["orderno"].ToString();
            string printType = "";
            string rptLvl = "";
            string printOpt = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            if (Request.QueryString.AllKeys.Contains("PrintType"))
            {
                printType = Request.QueryString["PrintType"].ToString();
            }
            if (Request.QueryString.AllKeys.Contains("PrintOpt"))
            {
                printOpt = Request.QueryString["PrintOpt"].ToString();
            }
            if (Request.QueryString.AllKeys.Contains("rptLvl"))
            {
                rptLvl = Request.QueryString["rptLvl"].ToString();
            }
            //try
            //{
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string Calltype = this.PrintCallType();
            DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SHOWORKORDER01", orderno, printType, "", "", "", "", "", "", "");

            DataTable dt = _ReportDataSet.Tables[0].Copy();
            //nahid
            if (_ReportDataSet.Tables[0].Rows.Count == 0)
                return;
            //OrderGenerateBy
            string PurReqNo = _ReportDataSet.Tables[0].Rows[0]["reqno"].ToString();
            string lettdes = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
            string purord = _ReportDataSet.Tables[4].Rows[0]["orderno"].ToString(); // order no
            string ReqDate = Convert.ToDateTime(_ReportDataSet.Tables[4].Rows[0]["reqdat"]).ToString("dd-MMM-yyyy"); // order no
            string Purorderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy"); //PO DATE
            string eflcperson = _ReportDataSet.Tables[4].Rows[0]["usrname"].ToString();
            string eflcpcell = _ReportDataSet.Tables[4].Rows[0]["mob"].ToString() + ", " + _ReportDataSet.Tables[4].Rows[0]["phone"].ToString();
            string eflcpemail = _ReportDataSet.Tables[4].Rows[0]["email"].ToString();
            //supplier information
            string supName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
            string address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
            string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
            string phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString() + ", " + _ReportDataSet.Tables[1].Rows[0]["mobile"].ToString();
            string supEmail = _ReportDataSet.Tables[1].Rows[0]["email"].ToString();
            string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();
            string vatregno = _ReportDataSet.Tables[1].Rows[0]["vatregno"].ToString();
            string tinno = _ReportDataSet.Tables[1].Rows[0]["tinno"].ToString();
            string cursymbol = _ReportDataSet.Tables[4].Rows[0]["cursymbol"].ToString();
            string tin = _ReportDataSet.Tables[1].Rows[0]["tin"].ToString();
            string bin = _ReportDataSet.Tables[1].Rows[0]["bin"].ToString();

            string invadd = _ReportDataSet.Tables[4].Rows[0]["invadd"].ToString();
            string deladd = _ReportDataSet.Tables[4].Rows[0]["delvadd"].ToString();
            string padelivery = _ReportDataSet.Tables[4].Rows[0]["padelivery"].ToString();
            //string suploc = _ReportDataSet.Tables[4].Rows[0]["suploc"].ToString();
            //string cnperson = _ReportDataSet.Tables[4].Rows[0]["cnperson"].ToString(); 
            //string email = _ReportDataSet.Tables[4].Rows[0]["email"].ToString();
            //string mobile = _ReportDataSet.Tables[4].Rows[0]["mobile"].ToString();
            string deliverydt = _ReportDataSet.Tables[4].Rows[0]["deliverydt"].ToString();
            string papayment = _ReportDataSet.Tables[4].Rows[0]["paymentdesc"].ToString();
            string prodref = _ReportDataSet.Tables[4].Rows[0]["pordref"].ToString();
            string pordnar = _ReportDataSet.Tables[4].Rows[0]["pordnar"].ToString();
            string remarks = _ReportDataSet.Tables[4].Rows[0]["remarks"].ToString();
            string custompon = _ReportDataSet.Tables[4].Rows[0]["custompon"].ToString();
            string paymenttrm = _ReportDataSet.Tables[4].Rows[0]["paymentdesc"].ToString();
            string shipmodec = _ReportDataSet.Tables[4].Rows[0]["shipmodedesc"].ToString();
            string shiptrms = _ReportDataSet.Tables[4].Rows[0]["shiptrmsdesc"].ToString();
            string pact = _ReportDataSet.Tables[0].Rows[0]["pactcode"].ToString().Substring(0, 4);
            //endnahid deliverydt

            string subject = _ReportDataSet.Tables[4].Rows[0]["subject"].ToString();

            string storename = _ReportDataSet.Tables[0].Rows[0]["pactdesc"].ToString();
            string supplydetails = _ReportDataSet.Tables[0].Rows[0]["msrnar3"].ToString() + _ReportDataSet.Tables[4].Rows[0]["pordnar"].ToString();
            string refno = _ReportDataSet.Tables[4].Rows[0]["pordref"].ToString();

            string preparedby = _ReportDataSet.Tables[4].Rows[0]["csprename"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["cspredat"].ToString();
            string checkedby = _ReportDataSet.Tables[4].Rows[0]["cschkname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["cschkdat"].ToString();
            string varifiedby = _ReportDataSet.Tables[4].Rows[0]["varyname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["verydat"].ToString();
            string approvedby = _ReportDataSet.Tables[4].Rows[0]["appname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["aprvdat"].ToString();
            string reqcreatby = _ReportDataSet.Tables[4].Rows[0]["postname"].ToString() + "\n" + _ReportDataSet.Tables[4].Rows[0]["posteddat"].ToString();
            string justification = _ReportDataSet.Tables[0].Rows[0]["msrnar3"].ToString();
            double advamt = Convert.ToDouble((Convert.IsDBNull(_ReportDataSet.Tables[4].Compute("Sum(advamt)", "")) ? 0.00 : _ReportDataSet.Tables[4].Compute("Sum(advamt)", "")));

            LocalReport rpt1 = new LocalReport();

            var list1 = _ReportDataSet.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.EClassPurchaseOrdr>();
            var list2 = _ReportDataSet.Tables[2].DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseOrderTerms>();

            var AList = list1.FindAll(p => p.grp == "A");
            double totalamt = (AList.Sum(item => item.ordramt) == 0) ? 0.00 : AList.Sum(item => item.ordramt);
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            switch (comcod)
            {
                case "5305":
                case "5306":
                    invadd = "ULOSHARA, KALIAKOIR, GAZIPUR-1750, BANGLADESH";
                    string invphn = "+880255069911";

                    if(rptLvl == "1")
                    {

                        //rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPOLocalFBJobWork", list1, list2.FindAll(p => p.termsdesc.Length > 0).ToList(), null);
                        rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptLocalApproval2Outsol", list1.FindAll(x=> x.grp == "A").ToList(), list2.FindAll(p => p.termsdesc.Length > 0).ToList(), null);

                        rpt1.SetParameters(new ReportParameter("comnam", comnam.ToUpper().ToString()));
                        rpt1.SetParameters(new ReportParameter("comcod", comcod.Substring(0, 2)));
                        rpt1.SetParameters(new ReportParameter("comadd", comadd));
                        rpt1.SetParameters(new ReportParameter("Purorderdate", Purorderdate));
                        rpt1.SetParameters(new ReportParameter("expdatedel", deliverydt));
                        rpt1.SetParameters(new ReportParameter("supName", supName));
                        rpt1.SetParameters(new ReportParameter("portload", ""));
                        rpt1.SetParameters(new ReportParameter("vendor", supName));
                        rpt1.SetParameters(new ReportParameter("venadd", "Address: " + address));
                        rpt1.SetParameters(new ReportParameter("vencnperson", "Contact Person: " + Cperson));
                        rpt1.SetParameters(new ReportParameter("venemail", "Email: " + supEmail));
                        rpt1.SetParameters(new ReportParameter("venphn", " Mobile: " + phone));
                        rpt1.SetParameters(new ReportParameter("purord", custompon));
                        rpt1.SetParameters(new ReportParameter("poref", prodref));
                        rpt1.SetParameters(new ReportParameter("shipMode", shipmodec));
                        rpt1.SetParameters(new ReportParameter("incoterms", shiptrms));
                        rpt1.SetParameters(new ReportParameter("spnote", pordnar));
                        rpt1.SetParameters(new ReportParameter("remarks", ""));
                        rpt1.SetParameters(new ReportParameter("invadd", invadd));
                        rpt1.SetParameters(new ReportParameter("invphn", invphn));
                        rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(totalamt, 2)));
                        rpt1.SetParameters(new ReportParameter("cursymbol", cursymbol));
                        rpt1.SetParameters(new ReportParameter("email", "Email: " + ""));
                        rpt1.SetParameters(new ReportParameter("mobile", " Mobile: " + ""));
                        rpt1.SetParameters(new ReportParameter("cnperson", "Contact Person: " + Cperson));
                        //rpt1.SetParameters(new ReportParameter("supbin", lst1[0].supbin));
                        rpt1.SetParameters(new ReportParameter("suploc", "Address: " + address));
                        rpt1.SetParameters(new ReportParameter("paymodedesc", paymenttrm));
                        rpt1.SetParameters(new ReportParameter("tin", tin));
                        rpt1.SetParameters(new ReportParameter("bin", bin));
                        rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    }
                    else
                    {
                        rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPOLocalFBNew", list1, list2.FindAll(p => p.termsdesc.Length > 0).ToList(), null);
                    }
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comcod", comcod.Substring(0, 2)));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    //rpt1.SetParameters(new ReportParameter("ReqDate", ReqDate));
                    rpt1.SetParameters(new ReportParameter("Purorderdate", Purorderdate));
                    rpt1.SetParameters(new ReportParameter("supName", supName));
                    rpt1.SetParameters(new ReportParameter("suploc", "Address: " + address));
                    //rpt1.SetParameters(new ReportParameter("vatregno", vatregno));
                    //rpt1.SetParameters(new ReportParameter("tinno", tinno));
                    rpt1.SetParameters(new ReportParameter("mobile", " Mobile: " + phone));
                    rpt1.SetParameters(new ReportParameter("purord", custompon));
                    rpt1.SetParameters(new ReportParameter("poref", prodref));
                    rpt1.SetParameters(new ReportParameter("spnote", pordnar)); 
                    rpt1.SetParameters(new ReportParameter("remarks", remarks));
                    //rpt1.SetParameters(new ReportParameter("PurReqNo", PurReqNo));
                    //rpt1.SetParameters(new ReportParameter("eflcperson", eflcperson));
                    //rpt1.SetParameters(new ReportParameter("eflcpcell", eflcpcell));
                    //rpt1.SetParameters(new ReportParameter("eflcpemail", eflcpemail));
                    rpt1.SetParameters(new ReportParameter("cnperson", "Contact Person: " + Cperson));
                    rpt1.SetParameters(new ReportParameter("email", "Email: " + supEmail));
                    rpt1.SetParameters(new ReportParameter("invadd", invadd));
                    //rpt1.SetParameters(new ReportParameter("deladd", deladd));
                    //rpt1.SetParameters(new ReportParameter("padelivery", padelivery));
                    rpt1.SetParameters(new ReportParameter("expdatedel", deliverydt));
                    //rpt1.SetParameters(new ReportParameter("papayment", papayment));
                    rpt1.SetParameters(new ReportParameter("RptTitle", (pact == "1561") ? "SAMPLE ORDER": (pact == "1562") ? "JOB WORK ORDER" : "PURCHASE ORDER" ));
                    rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(totalamt, 2)));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                    rpt1.SetParameters(new ReportParameter("cursymbol", cursymbol));

                    rpt1.SetParameters(new ReportParameter("invphn", invphn));
                    rpt1.SetParameters(new ReportParameter("paymodedesc", paymenttrm));
                    rpt1.SetParameters(new ReportParameter("shipMode", shipmodec));
                    rpt1.SetParameters(new ReportParameter("incoterms", shiptrms));
                    rpt1.SetParameters(new ReportParameter("tin", tin));
                    rpt1.SetParameters(new ReportParameter("bin", bin));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPOLocal", list1, list2.FindAll(p => p.termsdesc.Length > 0).ToList(), null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comcod", comcod.Substring(0, 2)));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("ReqDate", ReqDate));
                    rpt1.SetParameters(new ReportParameter("Purorderdate", Purorderdate));
                    rpt1.SetParameters(new ReportParameter("supName", supName));
                    rpt1.SetParameters(new ReportParameter("address", "Address: " + address));
                    rpt1.SetParameters(new ReportParameter("vatregno", vatregno));
                    rpt1.SetParameters(new ReportParameter("tinno", tinno));
                    rpt1.SetParameters(new ReportParameter("phone", " Mobile: " + phone));
                    rpt1.SetParameters(new ReportParameter("purord", purord));
                    rpt1.SetParameters(new ReportParameter("PurReqNo", PurReqNo));
                    rpt1.SetParameters(new ReportParameter("eflcperson", eflcperson));
                    rpt1.SetParameters(new ReportParameter("eflcpcell", eflcpcell));
                    rpt1.SetParameters(new ReportParameter("eflcpemail", eflcpemail));
                    rpt1.SetParameters(new ReportParameter("Cperson", "Contact Person: " + Cperson));
                    rpt1.SetParameters(new ReportParameter("supEmail", "Email: " + supEmail));
                    rpt1.SetParameters(new ReportParameter("invadd", invadd));
                    rpt1.SetParameters(new ReportParameter("deladd", deladd));
                    rpt1.SetParameters(new ReportParameter("padelivery", padelivery));
                    rpt1.SetParameters(new ReportParameter("deliverydt", deliverydt));
                    rpt1.SetParameters(new ReportParameter("papayment", papayment));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "PURCHASE ORDER"));
                    rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(totalamt, 2)));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

                    break;
            }

            Session["Report1"] = rpt1;

            if (type == "auto")
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                Array.ForEach(Directory.GetFiles(Server.MapPath("~/EmailDoc/")), File.Delete);

                byte[] Bytes = rpt1.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                using (FileStream fileStream = new FileStream(Server.MapPath("~/EmailDoc/" + orderno + ".pdf"), FileMode.Create))
                {
                    fileStream.Write(Bytes, 0, Bytes.Length);
                    fileStream.Close();
                }
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    printOpt + "', target='_self');</script>";
            }

        }

        private void PurMRR_RDLC()
        {

            string mMRRNo = this.Request.QueryString["mrrno"].ToString();
            string comcod = this.Request.QueryString["comcod"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string unitname = "";
            //var complist = (List<MFGOBJ.C_11_Pro.EClassPur.SisterCompList>)ViewState["tblSistercomp"];
            //if (complist.Count != 0)
            //{
            //    var filtrcom = complist.FindAll(p => p.comcod == comcod);
            //    comnam = filtrcom[0].company.ToString();
            //    comadd = filtrcom[0].comadd.ToString();
            //    unitname = " - " + filtrcom[0].comname.ToString();
            //}
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, "", "", "", "", "", "", "", "");


            if (ds1 == null || (ds1.Tables[0].Rows.Count == 0 && ds1.Tables[1].Rows.Count == 0))
            {
                return;
            }

            string rptTitle = comcod == "5301" ? "GOODS RECEIVING NOTE" : (comcod == "5305" || comcod == "5306") ? "INCOMING MATERIAL RECEIVING REPORT" : "";
            string mrrno1 = ds1.Tables[1].Rows[0]["mrrno1"].ToString();
            string mrrdat = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd-MMM-yyyy");
            string MrrRef = comcod == "5301" ? ds1.Tables[1].Rows[0]["orderno1"].ToString()
                            : (comcod == "5305" || comcod == "5306") ? ds1.Tables[1].Rows[0]["mrrref"].ToString() : "";
            string custompon = ds1.Tables[1].Rows[0]["custompon"].ToString();
            string chlnno = ds1.Tables[1].Rows[0]["chlnno"].ToString();
            string pactdesc = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string ssirdesc = ds1.Tables[1].Rows[0]["bblcdesc"].ToString();

            string preparedby = ds1.Tables[1].Rows[0]["username"].ToString();

            LocalReport rpt1 = new LocalReport();

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseMRR>();


            switch (comcod)
            {
                case "5301"://edison
                    rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurMRR",lst, null, null);
                    break;

                case "5305": // FB
                case "5306": //Footbed
                    var list1 = lst.GroupBy(y => new { y.rsircode, y.spcfcod }).Select(x => new SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseMRR
                    {
                        rsircode = x.Key.rsircode,
                        spcfcod = x.Key.spcfcod,
                        rsirdesc1 = lst.Find(z => z.rsircode == x.Key.rsircode).rsirdesc1,
                        spcfdesc = lst.Find(z => z.rsircode == x.Key.rsircode && z.spcfcod== x.Key.spcfcod).spcfdesc,
                        rsirunit = lst.Find(z => z.rsircode == x.Key.rsircode).rsirunit,
                        bomid = "",
                        mrrrate = lst.Find(z => z.rsircode == x.Key.rsircode && z.spcfcod == x.Key.spcfcod).mrrrate,
                        mrramt = lst.Find(z => z.rsircode == x.Key.rsircode && z.spcfcod == x.Key.spcfcod).mrramt,
                        mrrnote = lst.Find(z => z.rsircode == x.Key.rsircode && z.spcfcod == x.Key.spcfcod).mrrnote,
                        reqno1 = lst.Find(z => z.rsircode == x.Key.rsircode && z.spcfcod == x.Key.spcfcod).reqno1,
                        size = lst.Find(z => z.rsircode == x.Key.rsircode && z.spcfcod == x.Key.spcfcod).size,
                        color = lst.Find(z => z.rsircode == x.Key.rsircode && z.spcfcod == x.Key.spcfcod).color,
                        mrrqty = x.Sum(y => y.mrrqty),
                        chlnqty = x.Sum(y => y.chlnqty),
                        orderqty = x.Sum(y => y.orderqty)
                    })
           .ToList();
                    rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurMRRFB", list1, null, null);
                    break;

             
            }

            rpt1.EnableExternalImages = true;///
            rpt1.SetParameters(new ReportParameter("comnam", comnam + unitname));
            rpt1.SetParameters(new ReportParameter("RptTitle", rptTitle));
            rpt1.SetParameters(new ReportParameter("mrrno1", ": " + mrrno1));
            rpt1.SetParameters(new ReportParameter("mrrdat", ": " + mrrdat));
            rpt1.SetParameters(new ReportParameter("MrrRef", ": " + MrrRef));
            rpt1.SetParameters(new ReportParameter("custompon", ": " + custompon));
            rpt1.SetParameters(new ReportParameter("chlnno", ": " + chlnno));
            rpt1.SetParameters(new ReportParameter("pactdesc", ": " + pactdesc));
            rpt1.SetParameters(new ReportParameter("ssirdesc", ": " + ssirdesc));
            rpt1.SetParameters(new ReportParameter("preparedby", preparedby));
            rpt1.SetParameters(new ReportParameter("approvedby", ""));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }

        private void ImportAppPrint(string type)
        {
            //string comcod = this.GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");

            //string supplier = this.Request.QueryString["supcode"].ToString();
            //string msrno = this.Request.QueryString["msrno"].ToString();
            //string reqno = this.Request.QueryString["reqno"].ToString();
            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_LC_INTERFACE", "SUPPLIER_WISE_SURVEY_INFORMATION", msrno, supplier, reqno, "", "", "", "", "", "");
            string comcod = this.GetCompCode();
            string supplier = this.Request.QueryString["supcode"].ToString();
            string msrno = this.Request.QueryString["msrno"].ToString();
            string reqno = this.Request.QueryString["reqno"].ToString();
            string syspon = this.Request.QueryString["dayid"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            string summary = "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_LC_INTERFACE", "SUPPLIER_WISE_SURVEY_INFORMATION", msrno, supplier, reqno, syspon, summary, "", "", "", "");

            //var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            //var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.VendorInfo>();        
            //var lst2 = ds1.Tables[3].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();


            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.VendorInfo>();
            var lst2 = ds1.Tables[3].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            var lst3 = ds1.Tables[4].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo>();
            List<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo> newlist = new List<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo>();
            foreach (var item in lst3)
            {
                if (item.termsdetails != "" && item.termstitle != "")
                {
                    newlist.Add(item);
                }

            }
            double NetAmt = Convert.ToDouble(lst.Select(p => p.reqamt).Sum());
            double tChAmt = Convert.ToDouble(lst2.Select(p => p.amount).Sum());
            var prodstdate = "";
            var expdatedel = "";
            var expdatarri = "";

            var paytrm = "No";

            var sptrm = "No";

            if (lst1[0].paymoddesc != "")
            {
                paytrm = "Yes";
            }
            if (lst1[0].deltrmdesc != "")
            {
                sptrm = "Yes";
            }
            if (lst1[0].prodstdate != "")
            {
                prodstdate = Convert.ToDateTime(lst1[0].prodstdate).ToString("dd-MMM-yyyy");
            }
            if (lst1[0].expdatedel != "")
            {
                expdatedel = Convert.ToDateTime(lst1[0].expdatedel).ToString("dd-MMM-yyyy");
            }
            if (lst1[0].expdatarri != "")
            {
                expdatarri = Convert.ToDateTime(lst1[0].expdatarri).ToString("dd-MMM-yyyy");
            }

            LocalReport rpt1 = new LocalReport();


            //rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval", lst, lst1, lst2);
            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2", lst, newlist, lst2);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("nameCust", nameCust));
            rpt1.SetParameters(new ReportParameter("nameCust", lst1[0].namecust));
            rpt1.SetParameters(new ReportParameter("paytrm", paytrm));
            rpt1.SetParameters(new ReportParameter("sptrm", sptrm));
            rpt1.SetParameters(new ReportParameter("DateOfDelivery", expdatarri));
            rpt1.SetParameters(new ReportParameter("comadd1", comadd));
            rpt1.SetParameters(new ReportParameter("username", username));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Import Approval"));
            rpt1.SetParameters(new ReportParameter("date", printdate));
            rpt1.SetParameters(new ReportParameter("reqno", reqno));
            rpt1.SetParameters(new ReportParameter("pactcode", lst1[0].pactcode));
            rpt1.SetParameters(new ReportParameter("pono", lst1[0].pono));
            rpt1.SetParameters(new ReportParameter("msrno", lst1[0].msrno));
            rpt1.SetParameters(new ReportParameter("address", lst1[0].supaddress));
            rpt1.SetParameters(new ReportParameter("vendorName", lst1[0].sirdesc));
            rpt1.SetParameters(new ReportParameter("portload", lst1[0].portloaddesc));
            rpt1.SetParameters(new ReportParameter("portdisc", lst1[0].portdisdesc));
            rpt1.SetParameters(new ReportParameter("yincoterms", lst1[0].yincoterms));
            rpt1.SetParameters(new ReportParameter("delleadt", lst1[0].delleadt));
            rpt1.SetParameters(new ReportParameter("cdate", lst1[0].cdate.ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("prodstdate", prodstdate));
            rpt1.SetParameters(new ReportParameter("expdatedel", expdatedel));
            rpt1.SetParameters(new ReportParameter("expdatarri", expdatarri));
            rpt1.SetParameters(new ReportParameter("lcopbnk", lst1[0].bankname));
            rpt1.SetParameters(new ReportParameter("swiftcod", lst1[0].swiftdesc));
            rpt1.SetParameters(new ReportParameter("vendtype", lst1[0].vendtype));
            rpt1.SetParameters(new ReportParameter("deltrmdesc", lst1[0].deltrmdesc));
            rpt1.SetParameters(new ReportParameter("delmoddesc", lst1[0].delmoddesc));
            rpt1.SetParameters(new ReportParameter("paytypedesc", lst1[0].paytypedesc));
            rpt1.SetParameters(new ReportParameter("email", lst1[0].email));
            rpt1.SetParameters(new ReportParameter("mobile", lst1[0].mobile));
            rpt1.SetParameters(new ReportParameter("cnperson", lst1[0].cnperson));
            rpt1.SetParameters(new ReportParameter("supbin", lst1[0].supbin));
            rpt1.SetParameters(new ReportParameter("suploc", lst1[0].suploc));
            rpt1.SetParameters(new ReportParameter("supcurrency", lst1[0].supcurrency));
            rpt1.SetParameters(new ReportParameter("supcurdesc", lst1[0].supcurdesc));
            rpt1.SetParameters(new ReportParameter("custadd", lst1[0].custadd));
            rpt1.SetParameters(new ReportParameter("custmob", lst1[0].custmob));
            rpt1.SetParameters(new ReportParameter("custemail", lst1[0].custemail));

            rpt1.SetParameters(new ReportParameter("tAmount", (NetAmt + tChAmt).ToString("#,##0.00;(#,##0.00); ")));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;

            if (type == "auto")
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                Array.ForEach(Directory.GetFiles(Server.MapPath("~/EmailDoc/")), File.Delete);

                byte[] Bytes = rpt1.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                string orderno = Request.QueryString["orderno"].ToString();

                using (FileStream fileStream = new FileStream(Server.MapPath("~/EmailDoc/" + orderno + ".pdf"), FileMode.Create))
                {
                    fileStream.Write(Bytes, 0, Bytes.Length);
                    fileStream.Close();
                }
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }


        }

        private void ImportAppFbPrint(string type)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string supplier = this.Request.QueryString["supcode"].ToString();
            string msrno = this.Request.QueryString["msrno"].ToString();
            string reqno = this.Request.QueryString["reqno"].ToString();
            string syspon = this.Request.QueryString["dayid"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_LC_INTERFACE", "SUPPLIER_WISE_SURVEY_INFORMATION", msrno, supplier, reqno, syspon, "", "", "", "", "");

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.VendorInfo>();
            var lst2 = ds1.Tables[3].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            List<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo> lst3 = ds1.Tables[4].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo>();

            lst3 = lst3.FindAll(c => c.termsdetails != "" && c.termsdetails != "").ToList();



            int indexcnt = 0;
            if (ds1.Tables[1].Rows[0]["printtype"].ToString() == "4")
            {
                try
                {
                    foreach (var item in lst)
                    {

                        string mstctn = ds1.Tables[0].Rows[indexcnt]["spcfdescmstrctn"].ToString();
                        var charmstctn = mstctn.Split('X');
                        //var charmstctn2 = charmstctn[2].Split(' ');
                        if (charmstctn.Length > 1)
                        {
                            Regex digits = new Regex(@"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*");
                            Match mx = digits.Match(charmstctn[2]);

                            decimal strValue1 = mx.Success ? Convert.ToDecimal(mx.Groups[1].Value) : 0;

                            item.spcfdesch = Convert.ToDouble(strValue1);
                            item.spcfdescl = Convert.ToDouble(charmstctn[0]);
                            item.spcfdescw = Convert.ToDouble(charmstctn[1]);
                            item.spcfdescsqm = Convert.ToDouble((item.spcfdescl + item.spcfdescw + 6) * (item.spcfdescw + item.spcfdesch + 4) * 2 / 10000);

                        }
                        indexcnt++;
                    }
                }
                catch (Exception)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Correct Print Type');", true);
                    return;
                }

            }

            double NetAmt = Convert.ToDouble(lst.Select(p => p.reqamt).Sum());
            double tChAmt = Convert.ToDouble(lst2.Select(p => p.amount).Sum());
            var prodstdate = "";
            var expdatedel = "";
            var expdatarri = "";

            var paytrm = "No";

            var sptrm = "No";

            if (lst1[0].paymoddesc != "")
            {
                paytrm = "Yes";
            }
            if (lst1[0].deltrmdesc != "")
            {
                sptrm = "Yes";
            }
            if (lst1[0].prodstdate != "")
            {
                prodstdate = Convert.ToDateTime(lst1[0].prodstdate).ToString("dd-MMM-yyyy");
            }
            if (lst1[0].expdatedel != "")
            {
                expdatedel = Convert.ToDateTime(lst1[0].expdatedel).ToString("dd-MMM-yyyy");
            }
            if (lst1[0].expdatarri != "")
            {
                expdatarri = Convert.ToDateTime(lst1[0].expdatarri).ToString("dd-MMM-yyyy");
            }
            string invadd = "ULOSHARA, KALIAKOIR, GAZIPUR-1750, BANGLADESH";
            string invphn = "+880255069911";
            LocalReport rpt1 = new LocalReport();

            switch (ds1.Tables[1].Rows[0]["printtype"])
            {
                case "1":
                    rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2FB", lst, lst3, lst2);
                    break;
                case "2":
                    rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2Accessories", lst, lst3, lst2);
                    break;
                case "3":
                    rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2Outsol", lst, lst3, lst2);
                    break;
                case "4":
                    rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2MasterCtn", lst, lst3, lst2);
                    break;
                case "5":
                    rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2DoubleUnit", lst, lst3, lst2);
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2FB", lst, lst3, lst2);
                    break;
            }
            //rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval", lst, lst1, lst2);

            string pact = ds1.Tables[0].Rows[0]["pactcode"].ToString().Substring(0, 4);
            string tin = ds1.Tables[1].Rows[0]["tin"].ToString();
            string bin = ds1.Tables[1].Rows[0]["bin"].ToString();


            //----------------------------------------------------------------------

            rpt1.SetParameters(new ReportParameter("comnam", comnam.ToUpper().ToString()));
            rpt1.SetParameters(new ReportParameter("comcod", comcod.Substring(0, 2)));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("Purorderdate", prodstdate));
            rpt1.SetParameters(new ReportParameter("expdatedel", expdatedel));
            rpt1.SetParameters(new ReportParameter("supName", lst1[0].sirdesc));
            rpt1.SetParameters(new ReportParameter("portload", lst1[0].portloaddesc));
            rpt1.SetParameters(new ReportParameter("vendor", lst1[0].vendor));
            rpt1.SetParameters(new ReportParameter("venadd", "Address: " + lst1[0].venadd));
            rpt1.SetParameters(new ReportParameter("vencnperson", "Contact Person: " + lst1[0].venconperson));
            rpt1.SetParameters(new ReportParameter("venemail", "Email: " + lst1[0].venemail));
            rpt1.SetParameters(new ReportParameter("venphn", " Mobile: " + lst1[0].venphn));
            rpt1.SetParameters(new ReportParameter("purord", lst1[0].pono));
            rpt1.SetParameters(new ReportParameter("poref", lst1[0].poref));
            rpt1.SetParameters(new ReportParameter("shipMode", lst1[0].shipmodedesc));
            rpt1.SetParameters(new ReportParameter("incoterms", lst1[0].incotermsdesc));
            rpt1.SetParameters(new ReportParameter("spnote", lst1[0].spnote));
            rpt1.SetParameters(new ReportParameter("remarks", lst1[0].remarks));
            rpt1.SetParameters(new ReportParameter("invadd", invadd));
            rpt1.SetParameters(new ReportParameter("invphn", invphn));
            rpt1.SetParameters(new ReportParameter("inwords", ((ASTUtility.Trans(NetAmt, 2)).Replace("Taka", lst1[0].supcurdesc)).Replace("Paisa", lst1[0].supcursubdesc).ToString()));
            rpt1.SetParameters(new ReportParameter("email", "Email: " + lst1[0].email));
            rpt1.SetParameters(new ReportParameter("mobile", " Mobile: " + lst1[0].mobile));
            rpt1.SetParameters(new ReportParameter("cnperson", "Contact Person: " + lst1[0].cnperson));
            //rpt1.SetParameters(new ReportParameter("supbin", lst1[0].supbin));
            rpt1.SetParameters(new ReportParameter("suploc", "Address: " + lst1[0].supaddress));
            rpt1.SetParameters(new ReportParameter("RptTitle", ((pact == "1561") ? "SAMPLE ORDER" : "PURCHASE ORDER")));
            rpt1.SetParameters(new ReportParameter("paymodedesc", lst1[0].paymodedesc));
            rpt1.SetParameters(new ReportParameter("cursymbol", lst1[0].cursymbol));
            rpt1.SetParameters(new ReportParameter("tin", tin));
            rpt1.SetParameters(new ReportParameter("bin", bin));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;

            if (type == "auto")
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string orderno = Request.QueryString["orderno"].ToString();

                Array.ForEach(Directory.GetFiles(Server.MapPath("~/EmailDoc/")), File.Delete);

                byte[] Bytes = rpt1.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                using (FileStream fileStream = new FileStream(Server.MapPath("~/EmailDoc/" + orderno + ".pdf"), FileMode.Create))
                {
                    fileStream.Write(Bytes, 0, Bytes.Length);
                    fileStream.Close();
                }
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }

        }


        private void LCRecPrint()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");


            string preno = this.Request.QueryString["genno"].ToString();
            string lcno2 = this.Request.QueryString["actcode"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_PRE_RCVINFO", lcno2, preno, "", "", "", "", "", "", "");

            if (ds1 == null || (ds1.Tables[0].Rows.Count == 0))
            {
                return;
            }

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.BO_AllLCInfo.LCQCPrint>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptLCRecInfo", lst, null, null);
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd1", comadd));
            rpt1.SetParameters(new ReportParameter("username", username));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Material Received Report"));
            rpt1.SetParameters(new ReportParameter("date", "Date: " + printdate));
            rpt1.SetParameters(new ReportParameter("reqno", preno));
            rpt1.SetParameters(new ReportParameter("chalandate", lst[0].chalandate.ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("chalanno", lst[0].chalanno));
            rpt1.SetParameters(new ReportParameter("rcvdate", lst[0].rcvdate.ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("stordesc", lst[0].stordesc));
            rpt1.SetParameters(new ReportParameter("rcvno", lst[0].actdesc));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }
        private void LCQcPrint()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");


            string preno = this.Request.QueryString["genno"].ToString();
            string lcno2 = this.Request.QueryString["centrid"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_LCQC_INFO", lcno2, preno, "", "", "", "", "", "", "");
            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.BO_AllLCInfo.LCCostingPrint>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptLCQc", lst, null, null);
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd1", comadd));
            rpt1.SetParameters(new ReportParameter("username", username));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Material QC Report"));
            rpt1.SetParameters(new ReportParameter("date", "Date: " + printdate));
            rpt1.SetParameters(new ReportParameter("reqno", preno));
            rpt1.SetParameters(new ReportParameter("chalanno", lst[0].chalanno));
            rpt1.SetParameters(new ReportParameter("chalandate", "" + lst[0].chalandate));
            rpt1.SetParameters(new ReportParameter("chalanno", lst[0].chalanno));
            rpt1.SetParameters(new ReportParameter("rcvdate", lst[0].rcvdate.ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("rcvno", lst[0].actdesc));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }


        private void SendMail()
        {
            this.AutoSavePDF();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString().Trim();
            string trmid = hst["compname"].ToString().Trim();
            string usrSession = hst["session"].ToString().Trim();

            string comcod = this.GetCompCode();
            string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SMSEMAILSETUP", usrid, "", "", "", "", "", "", "", "");

            string mORDERNO = Request.QueryString["orderno"];
            DataSet ds1 = this.purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, usrid, "", "", "", "", "", "", "");

            string subject = "Work Order Copy, Order No:"+ mORDERNO;

            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            SmtpClient client = new SmtpClient(hostname, portnumber);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["enblessl"]); ;
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            client.Timeout = 600000;
            ///////////////////////


            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(frmemail);

               msg.To.Add(new MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
              //
              //
            //  msg.To.Add("safi@pintechltd.com");
                msg.Subject = subject;
                msg.IsBodyHtml = true;

                System.Net.Mail.Attachment attachment;

                string apppath = Server.MapPath("~") + "\\EmailDoc" + "\\" + mORDERNO + ".pdf";

                attachment = new System.Net.Mail.Attachment(apppath);
                msg.Attachments.Add(attachment);

                msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear sir," + "<br/>" + "please find the attached file." + "</pre></body></html>");
                client.Send(msg);

                Response.Write("<script type=\"text/javascript\">window.close();</script>");

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Your Mail has been successfully sent');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error occured while sending your message." + ex.Message + "');", true);
            }
        }

        private void AutoSavePDF()
        {
            string orderno = Request.QueryString["orderno"].ToString();

            if (orderno.Substring(0, 3) == "POR")
            {
                OrderPrint("auto");
            }
            else
            {
                switch (this.GetCompCode())
                {
                    case "5301":
                        this.ImportAppPrint("auto");
                        break;
                    default:
                        this.ImportAppFbPrint("auto");
                        break;
                }
            }
        }
    }
}