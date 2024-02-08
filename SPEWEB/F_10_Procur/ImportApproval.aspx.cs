using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.Net.Mail;
using System.Text.RegularExpressions;
using SPEENTITY.C_22_Sal;

namespace SPEWEB.F_10_Procur
{
    public partial class ImportApproval : System.Web.UI.Page
    {

        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text =(GetCompCode()=="5301")? "Import Approval":"PO Approval";
                this.CommonButton();
                this.GetLCGeneralInfo();
                this.Get_Survey_Wise_Supplier();
                if(GetCompCode() == "5301")
                {
                    this.checkAmt.Visible = true;
                }
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.proStartDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.deliveryLeadTime.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.expectedDateOfDelivery.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.expectedDateOfArrival.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //if (this.Request.QueryString["sircode"].Length > 0)
                //{
                //    this.ddlsupplier.SelectedValue = this.Request.QueryString["sircode"].ToString();
                //    this.lbtnOk_Click(null,null);
                //}
                if (this.Request.QueryString["dayid"].Length > 0)
                {
                    this.lblSyspo.Text = this.Request.QueryString["dayid"].ToString();
                }
                this.GetUnitName();
            }
        }
        public void CommonButton()
        {
            // ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            if (this.Request.QueryString["Type"] == "Approved")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approval";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do You want to Approve?')";
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;

            }
             ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            SaveValue();
            this.Data_bind();
        }
        protected void GetSYSPONO()
        {
            string comcod = this.GetCompCode();

            string season = "00000";
            DataTable surveyinfo = (DataTable)ViewState["surveyinfo"];
            if (surveyinfo.Rows.Count > 0)
            {
                season = surveyinfo.Rows[0]["season"].ToString();
            }

            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_SEASON_WISE_LAST_PO", season,
                   "LC", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.lblSyspo.Text = ds2.Tables[0].Rows[0]["maxsyspo"].ToString();
                this.txtPoNo.Text = ds2.Tables[0].Rows[0]["syspondesc"].ToString();


            }


        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.SaveValue();

            DataTable surveyinfo = (DataTable)ViewState["surveyinfo"];
            DataTable dt = (DataTable)ViewState["tblcharging"];
            DataTable tblterms = (DataTable)ViewState["tblterms"];
            DataSet ds1 = new DataSet("ds1");
            DataSet ds2 = new DataSet("ds2");
            if (this.lblSyspo.Text.Length == 0 || this.lblSyspo.Text.ToString()=="0000000000")
                this.GetSYSPONO();           
            string syspono = this.lblSyspo.Text.Trim().ToString();


            ds1.Tables.Add(dt);
            ds2.Tables.Add(tblterms);
            ds2.Tables.Add(surveyinfo);
            ds1.Tables[0].TableName = "tbl1";
            ds2.Tables[0].TableName = "tblterms";
            ds2.Tables[1].TableName = "surveyinfo";
            string comcod = this.GetCompCode();
            string supplier = this.ddlsupplier.SelectedValue.ToString().Substring(0, 12);
            string msrno = this.ddlsupplier.SelectedValue.ToString().Substring(12, 14);
            string reqno = this.Request.QueryString["genno"].ToString();
            string date = this.txtDatefrom.Text.ToString();
            string proStartDate = this.proStartDate.Text.ToString();
            string portLoad = this.portOfLoading.Text.ToString();
            string portDischarge = this.portOfDischarge.Text.ToString();
            string yincoterms = this.yearOfIncotemrs.Text.ToString();
            string deliveryLeadTime = this.deliveryLeadTime.Text.ToString();
            string expDelDate = this.expectedDateOfDelivery.Text.ToString();
            string expArrDate = this.expectedDateOfArrival.Text.ToString();
            string lcOpnBank = this.lcOpeningBank.Text.ToString();
            string swiftCode = this.swiftCode.Text.ToString();
            string status = (this.Request.QueryString["Type"].ToString() == "Approved" ? "Ok" : "");
            string incoterms = this.DDLIncoTerms.SelectedValue.ToString();
            string ReportLevel = this.ddlReportLevel.SelectedValue.ToString();
            string modeofpaymnt = this.DdlModeOfPayment.SelectedValue.ToString();
            string pono = this.txtPoNo.Text.ToString();
            string poRef = this.txtPoRef.Text.ToString();
            string shipMode = this.ddlShipMode.SelectedValue.ToString();
            string spNote = this.txtSpNote.Text.ToString();
            string Remarks = this.txtNar.Text.ToString();
            string VenCode = this.ddlVenCode.SelectedValue.ToString();
            string currency = this.ddlCurrency.SelectedValue.ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();          
          
            string Posteddate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            if (this.ddlReportLevel.SelectedValue.ToString() == "4")
            {
                DataTable tblsurveyinfo = ds2.Tables[1];
                DataView dv = tblsurveyinfo.DefaultView;
                dv.RowFilter = "sqmrate = '0.00'";
                if (dv.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Enter SQM Rate');", true);

                    return;
                }
            } 
            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_LC_INFO", "SAVE_INFO_FROM_IMPORT_APPROVAL", ds1, ds2, null, msrno, supplier, 
                proStartDate, portLoad, portDischarge, yincoterms, deliveryLeadTime, expDelDate, expArrDate, 
                lcOpnBank, swiftCode, status, incoterms, modeofpaymnt, syspono, date, Terminal, userid, Sessionid, Posteddate, pono, poRef, shipMode, spNote, Remarks, VenCode, ReportLevel, currency);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                Response.Redirect("~/F_10_Procur/ImportApproval?Type=Entry&comcod='" + comcod + "'&genno='" + reqno + "'&sircode='" + supplier + "'&dayid='" + pono + "'");
            }
        }

        private void SaveValue()
        {
            int index;
            DataTable dt = (DataTable)ViewState["tblcharging"];
            for (int i = 0; i < this.gvcharging.Rows.Count; i++)
            {
                index = (this.gvcharging.PageSize) * (this.gvcharging.PageIndex) + i;

                double chargingamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvcharging.Rows[i].FindControl("txtChrAmount")).Text.Trim()));

                dt.Rows[i]["amount"] = chargingamt;
            }
            ViewState["tblcharging"] = dt;


            DataTable tblterms = (DataTable)ViewState["tblterms"];
            for (int i = 0; i < this.gvOrderTerms.Rows.Count; i++)
            {

                string termstitle = ((TextBox)this.gvOrderTerms.Rows[i].FindControl("txtgvSubject")).Text.Trim();
                string termsdetails = ((TextBox)this.gvOrderTerms.Rows[i].FindControl("txtgvDesc")).Text.Trim();


                tblterms.Rows[i]["termstitle"] = termstitle;
                tblterms.Rows[i]["termsdetails"] = termsdetails;
            }
            ViewState["tblterms"] = tblterms;



            // resource talbe ----------------------
            DataTable surveyinfo = (DataTable)ViewState["surveyinfo"];

            var lst = surveyinfo.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            for (int i = 0; i < this.gvsurveyinfo.Rows.Count; i++)
            {


                string mstctn = surveyinfo.Rows[i]["spcfdescmstrctn"].ToString();
                var charmstctn = mstctn.Split('X');
                string RptLevel = this.ddlReportLevel.SelectedValue.ToString();

                double price = Convert.ToDouble(((TextBox)this.gvsurveyinfo.Rows[i].FindControl("txtgvPrice")).Text.Trim() == "" ? "0.00" :
                    ((TextBox)this.gvsurveyinfo.Rows[i].FindControl("txtgvPrice")).Text.Trim());
                double sqmrate = Convert.ToDouble(((TextBox)this.gvsurveyinfo.Rows[i].FindControl("txtgvSQM")).Text.Trim() == "" ? "0.00" :
                    ((TextBox)this.gvsurveyinfo.Rows[i].FindControl("txtgvSQM")).Text.Trim());
                if (RptLevel == "4")
                {
                    //var charmstctn2 = charmstctn[2].Split(' ');
                    if (charmstctn.Length > 1)
                    {
                        Regex digits = new Regex(@"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*");
                        Match mx = digits.Match(charmstctn[2]);

                        decimal strValue1 = mx.Success ? Convert.ToDecimal(mx.Groups[1].Value) : 0;

                        lst[i].spcfdesch = Convert.ToDouble(strValue1);
                        lst[i].spcfdescl = Convert.ToDouble(charmstctn[0]);
                        lst[i].spcfdescw = Convert.ToDouble(charmstctn[1]);
                        lst[i].spcfdescsqm = Convert.ToDouble((lst[i].spcfdescl + lst[i].spcfdescw + 6) * (lst[i].spcfdescw + lst[i].spcfdesch + 4) * 2 / 10000);

                    }



                    //index = (this.gvsurveyinfo.PageSize) * (this.gvsurveyinfo.PageIndex) + i;
                    price = Convert.ToDouble(lst[i].spcfdescsqm * sqmrate);
                }
                DropDownList ddlConUnit = (DropDownList)this.gvsurveyinfo.Rows[i].FindControl("ddlConUnit");
                
                string selection =((TextBox)this.gvsurveyinfo.Rows[i].FindControl("TxtSelection")).Text.Trim();
                string mattype = ((DropDownList)this.gvsurveyinfo.Rows[i].FindControl("DdlCstFinished")).SelectedValue.Trim();
                string unitcode = ((Label)this.gvsurveyinfo.Rows[i].FindControl("lblgvunitcode")).Text.Trim();
                string conreqqty = ((TextBox)this.gvsurveyinfo.Rows[i].FindControl("txtgvconreqqty")).Text.Trim();
                string conUnit = ddlConUnit.SelectedValue.ToString();


                DataTable stdunit = (DataTable)ViewState["UnitsRate"];
                DataView dv = stdunit.DefaultView;
                dv.RowFilter = "ccod = '" + conUnit.ToString() + "' and bcod = '"+ unitcode + "' ";
                
                double conrat = 1;

                DataTable newtabl = dv.ToTable();
                if (newtabl.Rows.Count != 0)
                {
                    conrat =Convert.ToDouble(newtabl.Rows[0]["conrat"]);
                }
                

                surveyinfo.Rows[i]["mattype"] = mattype;
                surveyinfo.Rows[i]["selection"] = selection;
                surveyinfo.Rows[i]["resrate"] = price;
                surveyinfo.Rows[i]["sqmrate"] = sqmrate;
                surveyinfo.Rows[i]["reqqty"] = Convert.ToDouble("0" + conreqqty) * conrat;
                surveyinfo.Rows[i]["reqamt"] = Convert.ToDouble(surveyinfo.Rows[i]["reqqty"])*price;
                surveyinfo.Rows[i]["conunitqty"] = Convert.ToDouble("0" + conreqqty);
                surveyinfo.Rows[i]["conunit"] = conUnit;
                

            }
            ViewState["surveyinfo"] = surveyinfo;
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string supplier = this.ddlsupplier.SelectedValue.ToString().Substring(0, 12);
            string msrno = this.ddlsupplier.SelectedValue.ToString().Substring(12, 14);
            string reqno = this.Request.QueryString["genno"].ToString();
            string syspon = this.lblSyspo.Text.Trim().ToString();
            string ReportLevel = this.ddlReportLevel.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            string summary = (this.CheckBox_Summary.Checked) ? "summary" : "";
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "SUPPLIER_WISE_SURVEY_INFORMATION", msrno, supplier, reqno, syspon, summary, "", "", "", "");

            var lstf = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.VendorInfo>();
            var lst2 = ds1.Tables[3].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            var lst3 = ds1.Tables[4].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo>();
            List<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo> newlist = new List<SPEENTITY.C_10_Procur.EClassProcur.TermsInfo>();
            var lst = lstf.ToList();

            int indexcnt = 0;
            if (ReportLevel== "4")
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
                            item.spcfdescsqm = Convert.ToDouble((item.spcfdescl + item.spcfdescw + 6) * (item.spcfdescw + item.spcfdesch + 4)*2 / 10000);

                        }
                        indexcnt++;
                    }
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Correct Print Type');", true);
                    return;
                }
               
            }
            
            foreach (var item in lst3)
            {
                if (item.termsdetails != "")
                {
                    newlist.Add(item);
                }
            }
             lst = lstf.Concat(lst2.Where(c => c.amount != 0.00)).ToList();

            double NetAmt = Convert.ToDouble(lst.Select(p => p.reqamt).Sum());
            double tChAmt = Convert.ToDouble(lst2.Select(p => p.amount).Sum());
            var prodstdate = "";
            var expdatedel = "";
            var expdatarri = "";

            var paytrm = "No";

            var sptrm = "No";

            if (this.payterm.Checked)
            {
                paytrm = "Yes";
            }
            if (this.delmod.Checked)
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
            var nameCust = this.nameCust.Text;
            var address = this.address.Text;
            var DateOfDelivery = this.expectedDateOfDelivery.Text;
            string invadd = "ULOSHARA, KALIAKOIR, GAZIPUR-1750, BANGLADESH";
            string invphn = "+880255069911";

            //rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval", lst, lst1, lst2);
            switch (comcod)
            {
                case "5305":
                case "5306":
                    switch (ReportLevel)
                    {
                        case "1":
                            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2FB", lst, newlist, lst2);
                            break;
                        case "2":
                            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2Accessories", lst, newlist, lst2);
                            break;
                        case "3":
                            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2Outsol", lst, newlist, lst2);
                            break;
                        case "4":
                            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2MasterCtn", lst, newlist, lst2);
                            break;
                        case "5":
                            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2DoubleUnit", lst, newlist, lst2);
                            break;
                        default:
                            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2FB", lst, newlist, lst2);
                            break;
                    }
                    string pact = ds1.Tables[0].Rows[0]["pactcode"].ToString().Substring(0, 4);
                    string tin = ds1.Tables[1].Rows[0]["tin"].ToString();
                    string bin = ds1.Tables[1].Rows[0]["bin"].ToString();


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
                    rpt1.SetParameters(new ReportParameter("cursymbol", lst1[0].cursymbol.ToString()));
                    rpt1.SetParameters(new ReportParameter("email", "Email: " + lst1[0].email));
                    rpt1.SetParameters(new ReportParameter("mobile", " Mobile: " + lst1[0].mobile));
                    rpt1.SetParameters(new ReportParameter("cnperson", "Contact Person: " + lst1[0].cnperson));
                    //rpt1.SetParameters(new ReportParameter("supbin", lst1[0].supbin));
                    rpt1.SetParameters(new ReportParameter("suploc", "Address: " + lst1[0].supaddress));
                    rpt1.SetParameters(new ReportParameter("RptTitle", ((pact == "1561") ? "SAMPLE ORDER" : "PURCHASE ORDER") ));
                    rpt1.SetParameters(new ReportParameter("paymodedesc", lst1[0].paymodedesc));
                    rpt1.SetParameters(new ReportParameter("tin", tin));
                    rpt1.SetParameters(new ReportParameter("bin", bin));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


                    break;
                default:

                    if(checkAmt.Checked == true)
                    {
                        rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval2", lst, newlist, lst2);
                    }
                    else
                    {
                        rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval23", lst, newlist, lst2);
                    }

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("nameCust", nameCust));
                    rpt1.SetParameters(new ReportParameter("paytrm", paytrm));
                    rpt1.SetParameters(new ReportParameter("sptrm", sptrm));
                    rpt1.SetParameters(new ReportParameter("selleraddress", address));
                    rpt1.SetParameters(new ReportParameter("DateOfDelivery", DateOfDelivery));
                    rpt1.SetParameters(new ReportParameter("comadd1", comadd));
                    rpt1.SetParameters(new ReportParameter("username", username));
                    rpt1.SetParameters(new ReportParameter("RptTitle",  "Import Approval"));
                    rpt1.SetParameters(new ReportParameter("date", printdate));
                    rpt1.SetParameters(new ReportParameter("reqno", reqno));
                    rpt1.SetParameters(new ReportParameter("pono", lst1[0].pono));
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
                    //rpt1.SetParameters(new ReportParameter("namecust", lst1[0].namecust));
                    rpt1.SetParameters(new ReportParameter("tAmount", (NetAmt + tChAmt).ToString("#,##0.00;(#,##0.00); ")));
                    //rpt1.SetParameters(new ReportParameter("preparedby", ""));
                    //rpt1.SetParameters(new ReportParameter("checkedby", ""));
                    //rpt1.SetParameters(new ReportParameter("approvedby", ""));
                    //rpt1.SetParameters(new ReportParameter("reviewby", ""));

                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    break;
            }


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            #region old


            //string comcod = this.GetCompCode();
            //string supplier = this.ddlsupplier.SelectedValue.ToString().Substring(0, 12);
            //string msrno = this.ddlsupplier.SelectedValue.ToString().Substring(12, 14);
            //string reqno = this.Request.QueryString["genno"].ToString();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint.aspx?Type=ImportApp&comcod=" + comcod + "&reqno=" + reqno + "&msrno=" + msrno  + "&supcode=" + supplier + "', target='_blank');</script>";

            //string comcod = this.GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)ViewState["surveyinfo"];
            //DataTable dt1 = (DataTable)ViewState["vendorinfo"];
            //DataTable dt2 = (DataTable)ViewState["reqapproval"];

            //var lst = dt.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.SurveyInfo>();
            //var lst1 = dt1.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.VendorInfo>();
            //var lst2 = dt2.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.ReqApproval>();

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptImportApproval", lst, lst1, lst2);

            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            ////rpt1.SetParameters(new ReportParameter("comadd1", comadd));
            ////rpt1.SetParameters(new ReportParameter("username", username));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "Import Approval"));
            //rpt1.SetParameters(new ReportParameter("date", printdate));
            //rpt1.SetParameters(new ReportParameter("reqno", ""));
            //rpt1.SetParameters(new ReportParameter("pactcode", lst1[0].pactcode ));
            //rpt1.SetParameters(new ReportParameter("address", lst1[0].supaddress));
            //rpt1.SetParameters(new ReportParameter("vendorName", lst1[0].sirdesc));
            //rpt1.SetParameters(new ReportParameter("portload", lst1[0].portloaddesc));
            //rpt1.SetParameters(new ReportParameter("portdisc", lst1[0].portdisdesc));
            //rpt1.SetParameters(new ReportParameter("yincoterms", lst1[0].yincoterms));
            //rpt1.SetParameters(new ReportParameter("delleadt", lst1[0].delleadt));
            //rpt1.SetParameters(new ReportParameter("cdate", lst1[0].cdate.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("prodstdate", lst1[0].prodstdate.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("expdatedel", lst1[0].expdatedel.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("expdatarri", lst1[0].expdatarri.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("lcopbnk", lst1[0].bankname));
            //rpt1.SetParameters(new ReportParameter("swiftcod", lst1[0].swiftdesc));
            //rpt1.SetParameters(new ReportParameter("vendtype", lst1[0].vendtype));
            //rpt1.SetParameters(new ReportParameter("deltrmdesc", lst1[0].deltrmdesc));
            //rpt1.SetParameters(new ReportParameter("delmoddesc", lst1[0].delmoddesc));
            //rpt1.SetParameters(new ReportParameter("paytypedesc", lst1[0].paytypedesc));
            //rpt1.SetParameters(new ReportParameter("namecust", lst1[0].namecust));
            //rpt1.SetParameters(new ReportParameter("sigheadcom", ""));
            //rpt1.SetParameters(new ReportParameter("sigfincon", ""));
            //rpt1.SetParameters(new ReportParameter("sigheadbuz", ""));
            //rpt1.SetParameters(new ReportParameter("sigextmgt", ""));

            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            #endregion

        }

        public void GetLCGeneralInfo()
        {
            string comcod = this.GetCompCode();
            string reqno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_LC_GENERAL_INFO", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = "sircode like '010100411%'";
            this.portOfLoading.DataTextField = "sirdesc";
            this.portOfLoading.DataValueField = "sircode";
            this.portOfLoading.DataSource = dv.ToTable();
            this.portOfLoading.DataBind();

            DataView dvd = ds1.Tables[0].DefaultView;
            dvd.RowFilter = "sircode like '010100412%'";
            this.portOfDischarge.DataTextField = "sirdesc";
            this.portOfDischarge.DataValueField = "sircode";
            this.portOfDischarge.DataSource = dvd.ToTable();
            this.portOfDischarge.DataBind();

            DataView dv1 = ds1.Tables[0].DefaultView;
            dv1.RowFilter = "sircode like '010100403%'";
            this.swiftCode.DataTextField = "sirdesc";
            this.swiftCode.DataValueField = "sircode";
            this.swiftCode.DataSource = dv1.ToTable();
            this.swiftCode.DataBind();

            DataView dv2 = ds1.Tables[0].DefaultView;
            dv2.RowFilter = "sircode like '010100401%'";
            this.lcOpeningBank.DataTextField = "sirdesc";
            this.lcOpeningBank.DataValueField = "sircode";
            this.lcOpeningBank.DataSource = dv2.ToTable();
            this.lcOpeningBank.DataBind();


            //DataView dv2 = ds1.Tables[0].DefaultView;
            //dv2.RowFilter = "sircode like '010100401%'";
            //this.lcOpeningBank.DataTextField = "sirdesc";
            //this.lcOpeningBank.DataValueField = "sircode";
            //this.lcOpeningBank.DataSource = dv2.ToTable();
            //this.lcOpeningBank.DataBind();


        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void Get_Survey_Wise_Supplier()
        {
            string comcod = this.GetCompCode();
            string reqno = this.Request.QueryString["genno"].ToString();
            string sircode = (this.Request.QueryString["sircode"].Length == 0 || this.Request.QueryString["sircode"].ToString() == "000000000000") ? "%" : this.Request.QueryString["sircode"].ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_SUPPLIER_FROM_MARKET_SURVEY", reqno, sircode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlsupplier.DataTextField = "ssirdesc";
            this.ddlsupplier.DataValueField = "ssircode";
            this.ddlsupplier.DataSource = ds1.Tables[0];
            this.ddlsupplier.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.panel1.Visible = true;
                this.panel2.Visible = true;
                this.ddlsupplier.Enabled = false;
                this.lbtnOk.Text = "New";
                this.lblcharging.Visible = true;
                this.TermsConditions.Visible = true;
                this.LblImpRequistion.Visible = true;
                this.DdlImportRequistion.Visible = true;
                this.btnMerge.Visible = true;
                this.GET_Incotrms();
                this.GET_ModeoFPayments();
                this.GET_ShipingMode();
                this.GET_Vendor();
                this.GET_Supplier_wise_Info();
                this.GetCurrencyInf();
                this.GET_Supplier_Pending_Requistion();
                this.ddlReportLevel_SelectedIndexChanged(null, null);

                return;
            }
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.gvsurveyinfo.DataSource = null;
            this.gvsurveyinfo.DataBind();
            this.gvcharging.DataSource = null;
            this.gvcharging.DataBind();
            this.ddlsupplier.Enabled = true;
            this.LblImpRequistion.Visible = false;
            this.DdlImportRequistion.Visible = false;
            this.btnMerge.Visible = false;
            this.lbtnOk.Text = "Ok";
            this.lblcharging.Visible = false;
            this.TermsConditions.Visible = false;
        }
        private void GET_Incotrms()
        {
             DataRow dr1;

            string comcod = this.GetCompCode();            
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO",  "17%", "", "", "", "", "");
            dr1 = ds1.Tables[0].NewRow();
            dr1["gcod"] = "00000";
            dr1["gdesc"] = "NONE";
            dr1["comcod"] = comcod;
            ds1.Tables[0].Rows.Add(dr1);
            this.DDLIncoTerms.DataValueField = "gcod";
            this.DDLIncoTerms.DataTextField= "gdesc";
            this.DDLIncoTerms.DataSource = ds1.Tables[0];
            this.DDLIncoTerms.SelectedValue = "00000";
            this.DDLIncoTerms.DataBind();
            
        }
        private void GET_ModeoFPayments()
        {
            DataRow dr1;

            string comcod = this.GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "16%", "", "", "", "", "");
            dr1 = ds1.Tables[0].NewRow();
            dr1["gcod"] = "00000";
            dr1["gdesc"] = "NONE";
            dr1["comcod"] = comcod;
            ds1.Tables[0].Rows.Add(dr1);
            this.DdlModeOfPayment.DataValueField = "gcod";
            this.DdlModeOfPayment.DataTextField = "gdesc";
            this.DdlModeOfPayment.DataSource = ds1.Tables[0];
            this.DdlModeOfPayment.SelectedValue = "00000";
            this.DdlModeOfPayment.DataBind();

        }
        private void GET_Vendor()
        {
            DataRow dr1;

            string comcod = this.GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETVENDOR", "99%", "", "", "", "", "");
            dr1 = ds1.Tables[0].NewRow();
            dr1["sircode"] = "000000000000";
            dr1["sirdesc"] = "NONE";
            dr1["comcod"] = comcod;
            ds1.Tables[0].Rows.Add(dr1);
            this.ddlVenCode.DataValueField = "sircode";
            this.ddlVenCode.DataTextField = "sirdesc";
            this.ddlVenCode.DataSource = ds1.Tables[0];
            this.ddlVenCode.SelectedValue = "000000000000";
            this.ddlVenCode.DataBind();

        }
        private void GET_ShipingMode()
        {
            DataRow dr1;

            string comcod = this.GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "14%", "", "", "", "", "");
            dr1 = ds1.Tables[0].NewRow();
            dr1["gcod"] = "00000";
            dr1["gdesc"] = "NONE";
            dr1["comcod"] = comcod;
            ds1.Tables[0].Rows.Add(dr1);
            this.ddlShipMode.DataValueField = "gcod";
            this.ddlShipMode.DataTextField = "gdesc";
            this.ddlShipMode.DataSource = ds1.Tables[0];
            this.ddlShipMode.SelectedValue = "00000";
            this.ddlShipMode.DataBind();

        }

        private void GetCurrencyInf()
        {
            SalesInvoice_BL _salesInvBl = new SalesInvoice_BL();

            DataSet ds = _salesInvBl.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;

            DataTable dtCur = (DataTable) ViewState["vendorinfo"];

            this.ddlCurrency.DataValueField = "curcode";
            this.ddlCurrency.DataTextField = "curdesc";
            this.ddlCurrency.DataSource = lstCurryDesc;
            this.ddlCurrency.DataBind();

            if(dtCur.Rows[0]["supcurrency"].ToString().Trim().Length > 0)
            {
                this.ddlCurrency.SelectedValue = dtCur.Rows[0]["supcurrency"].ToString().Trim();
            }

        }

        private void GET_Supplier_wise_Info()
        {
            string comcod = this.GetCompCode();
            string supplier = this.ddlsupplier.SelectedValue.ToString().Substring(0, 12);
            string msrno = this.ddlsupplier.SelectedValue.ToString().Substring(12, 14);
            string reqno = this.Request.QueryString["genno"].ToString();
            string syspono = this.lblSyspo.Text.Trim().ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "SUPPLIER_WISE_SURVEY_INFORMATION", msrno, supplier, reqno, syspono, "", "", "", "", "");
            this.address.Text = ds1.Tables[1].Rows[0]["supaddress"].ToString();
            this.checkboxven.Text = ds1.Tables[1].Rows[0]["vendtype"].ToString();
            this.payterm.Text = ds1.Tables[1].Rows[0]["paytypedesc"].ToString();
            this.delterm.Text = ds1.Tables[1].Rows[0]["deltrmdesc"].ToString();
            this.delmod.Text = ds1.Tables[1].Rows[0]["delmoddesc"].ToString();
            this.txtDatefrom.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["orddat"]).ToString("dd-MMM-yyyy");

            this.txtPoNo.Text = ds1.Tables[1].Rows[0]["pono"].ToString();
            this.txtPoRef.Text = ds1.Tables[1].Rows[0]["poref"].ToString();
            this.txtSpNote.Text = ds1.Tables[1].Rows[0]["spnote"].ToString();
            this.txtNar.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();
            if (ds1.Tables[1].Rows[0]["syspon"].ToString() != "0000000000")
            { 
                this.lblSyspo.Text = ds1.Tables[1].Rows[0]["syspon"].ToString();
            }
            //this.delmod.Text = ds1.Tables[1].Rows[0]["vencode"].ToString();

            if (ds1.Tables[1].Rows[0]["vencode"].ToString() != "")
            {
                this.ddlVenCode.SelectedValue = ds1.Tables[1].Rows[0]["vencode"].ToString();


            }
            if (ds1.Tables[1].Rows[0]["shipmode"].ToString() != "")
            {
                this.ddlShipMode.SelectedValue = ds1.Tables[1].Rows[0]["shipmode"].ToString();


            }

            if (ds1.Tables[1].Rows[0]["portload"].ToString() != "")
            {
                this.portOfLoading.SelectedValue = ds1.Tables[1].Rows[0]["portload"].ToString();
            }
            if (ds1.Tables[1].Rows[0]["portdisc"].ToString() != "")
            {
                this.portOfDischarge.SelectedValue = ds1.Tables[1].Rows[0]["portdisc"].ToString();
            }


            this.yearOfIncotemrs.Text = ds1.Tables[1].Rows[0]["yincoterms"].ToString();
            this.DDLIncoTerms.SelectedValue = ds1.Tables[1].Rows[0]["incoterms"].ToString();
            this.DdlModeOfPayment.SelectedValue = ds1.Tables[1].Rows[0]["paymode"].ToString();
            this.ddlReportLevel.SelectedValue = ds1.Tables[1].Rows[0]["printtype"].ToString();
            //this.deliveryLeadTime.Text = ds1.Tables[1].Rows[0]["delleadt"].ToString();
            this.expectedDateOfDelivery.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["expdatedel"]).ToString("dd-MMM-yyyy");
            this.expectedDateOfArrival.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["expdatarri"]).ToString("dd-MMM-yyyy");
            if (ds1.Tables[1].Rows[0]["lcopbnk"].ToString() != "")
            {
                this.lcOpeningBank.SelectedValue = ds1.Tables[1].Rows[0]["lcopbnk"].ToString();
            }


            if (ds1.Tables[1].Rows[0]["swiftcod"].ToString() != "")
            {
                this.swiftCode.SelectedValue = ds1.Tables[1].Rows[0]["swiftcod"].ToString();
            }
            this.nameCust.Text = ds1.Tables[1].Rows[0]["namecust"].ToString();
            ViewState["surveyinfo"] = ds1.Tables[0];
            ViewState["vendorinfo"] = ds1.Tables[1];
            ViewState["reqapproval"] = ds1.Tables[2];
            ViewState["tblcharging"] = ds1.Tables[3];
            ViewState["tblterms"] = ds1.Tables[4];
            this.Data_bind();
        }

        private void GET_Supplier_Pending_Requistion()
        {
            string comcod = this.GetCompCode();
            string supplier = this.ddlsupplier.SelectedValue.ToString().Substring(0, 12); 
            string reqno = this.Request.QueryString["genno"].ToString();

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "SUPPLIER_WISE_PENDING_REQUISITION", supplier, reqno, "", "", "", "", ""); ;
            ViewState["tblreqlist"] = ds1.Tables[0];

            this.DdlImportRequistion.DataValueField = "reqno";
            this.DdlImportRequistion.DataTextField = "reqno1";
            this.DdlImportRequistion.DataSource = ds1.Tables[0].DefaultView.ToTable(true,"reqno","reqno1");          
            this.DdlImportRequistion.DataBind();

        }
        private void Data_bind()
        {
            DataTable dt = (DataTable)ViewState["surveyinfo"];
            this.gvsurveyinfo.DataSource = dt;
            this.gvsurveyinfo.DataBind();
            DataTable tblcharging = (DataTable)ViewState["tblcharging"];
            this.gvcharging.DataSource = tblcharging;
            this.gvcharging.DataBind();

            this.gvOrderTerms.DataSource = (DataTable)ViewState["tblterms"];
            this.gvOrderTerms.DataBind();
            if (dt.Rows.Count == 0)
                return;

            double itmvalue = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reqamt)", "")) ?
                   0.00 : dt.Compute("Sum(reqamt)", "")));

            ((Label)this.gvsurveyinfo.FooterRow.FindControl("lblgvFConfirmPrice")).Text = itmvalue.ToString("#,##0.00;(#,##0.00); ");

            if (tblcharging.Rows.Count == 0)
                return;

            ((Label)this.gvcharging.FooterRow.FindControl("lblFchamt")).Text =
                (Convert.ToDouble((Convert.IsDBNull(tblcharging.Compute("Sum(amount)", "")) ?
                    0.00 : tblcharging.Compute("Sum(amount)", ""))) + itmvalue).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void AddMore_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblterms"];
            int lastnum = dt.Rows.Count + 1;
            DataRow row2 = dt.NewRow();
            row2["slno"] = lastnum;
            row2["termstitle"] = "";
            row2["termsdetails"] = "";
            dt.Rows.Add(row2);

            ViewState["tblterms"] = dt;
            this.Data_bind();

        }
        protected void lblgvSpfDesc10_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            this.ModalHead.Text = "Material Specification Change";
            string sircode = ((Label)this.gvsurveyinfo.Rows[rowindex].FindControl("lblgvItmCodc")).Text.Trim();
            string spcfcod = ((Label)this.gvsurveyinfo.Rows[rowindex].FindControl("lblSpcfcod")).Text.Trim();
            this.lblHelper.Text = sircode + spcfcod;
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_WISE_SPECIFICATION", sircode);
            DataSet colorinfo = this.Merdata.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_COLOR_CODE", "", "", "", "", "", "", "");
            ViewState["tblcolor"] = colorinfo.Tables[0];
            if (result.Tables[0].Rows.Count == 0 || result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Found any Specification');", true);

            
                return;
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = result.Tables[0];
            this.ddlSpecification.DataBind();
            this.TxtThikness.Text = "";
            this.txtlength.Text = "";
            this.txtbrand.Text = "";
            this.txtRemarks.Text = "";
            this.ddlModalColor.DataTextField = "gdesc";
            this.ddlModalColor.DataValueField = "gcod";
            this.ddlModalColor.DataSource = colorinfo;
            this.ddlModalColor.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }
        protected void LbtnChngSpcf_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblcolor"];
            DataTable dt1 = (DataTable)ViewState["surveyinfo"];
            DataView dv = dt.DefaultView;
            string material = this.lblHelper.Text.ToString().Substring(0, 12);
            string spcfcode = this.lblHelper.Text.ToString().Substring(12, 12);
            string reqno = this.Request.QueryString["genno"].ToString();
            string actcode = "000000000000";
            string bomid = dt1.Rows[0]["bomid"].ToString();
            string tospcfcod = this.ddlSpecification.SelectedValue.ToString();
            if (this.txtlength.Text.ToString() != "" || this.TxtThikness.Text.ToString() != "")
            {
                string thikness = this.TxtThikness.Text.ToString();
                string len = this.txtlength.Text.ToString();
                string brand = this.txtbrand.Text.ToString();
                string remarks = this.txtRemarks.Text.ToString();
                string newcolor = this.ddlModalColor.SelectedValue.ToString();
                string newcolorname = this.ddlModalColor.SelectedItem.ToString();
                dv.RowFilter = "gcod = '" + newcolor + "'";
                string colcode = dv.ToTable().Rows[0]["colcode"].ToString();

                DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_NEW_SPECIFICATIONS", material, thikness, len, newcolorname, brand, remarks, colcode);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Error!!!');", true);

                    return;
                }

                tospcfcod = ds.Tables[0].Rows[0]["spcfcod"].ToString();
            }
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "CHANGE_SPECIFICATIONS_OF_LCOPENING", actcode, reqno, material, spcfcode, tospcfcod, bomid);
            if (result == true)
            {
                this.GET_Supplier_wise_Info();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Specification Change Successfully');", true);

             
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist This Specifications');", true);

                
            }
        }
        protected void GetUnitName()
        {
            string comcod = this.GetCompCode();
            DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_CONVRT_INF", "", "", "", "", "", "", "");
            ViewState["UnitsRate"] = ds.Tables[0];
        }
        protected void gvsurveyinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt1 = (DataTable)ViewState["surveyinfo"];
                string mattype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mattype")).ToString().Trim();
                string unitcoder = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "unitcode")).Trim();
                string conunitcode = Convert.ToString(dt1.Rows[e.Row.RowIndex]["conunit"]).ToString().Trim();
                string Unitdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Unit")).ToString().Trim();
                DropDownList mattypeddl = (DropDownList)e.Row.FindControl("DdlCstFinished");
                DropDownList ddlConUnit = (DropDownList)e.Row.FindControl("ddlConUnit");
                mattypeddl.SelectedValue = mattype;

                DataTable stdunit = (DataTable)ViewState["UnitsRate"];
                DataView dv = stdunit.DefaultView;
                dv.RowFilter = "bcod = '" + unitcoder.ToString() + "' ";
                DataTable newtabl = dv.ToTable();
                if (newtabl.Rows.Count == 0)
                {
                    DataRow dr1 = newtabl.NewRow();

                    dr1["comcod"] = this.GetCompCode(); ;
                    dr1["bcod"] = unitcoder.ToString();
                    dr1["bcodesc"] = Unitdesc.ToString();
                    dr1["ccod"] = unitcoder.ToString();
                    dr1["ccodesc"] = Unitdesc.ToString();
                    dr1["conrat"] = 1.00;

                    newtabl.Rows.Add(dr1);
                }
                else
                {
                    dv = newtabl.Copy().DefaultView;
                    dv.RowFilter = "ccod = '" + unitcoder.ToString() + "' ";
                    if (dv.ToTable().Rows.Count == 0)
                    {
                        DataRow dr1 = newtabl.NewRow();
                        dr1["comcod"] = this.GetCompCode(); ;
                        dr1["bcod"] = unitcoder.ToString();
                        dr1["bcodesc"] = Unitdesc.ToString().ToString();
                        dr1["ccod"] = unitcoder.ToString();
                        dr1["ccodesc"] = Unitdesc.ToString().ToString();
                        dr1["conrat"] = 1.00;

                        newtabl.Rows.Add(dr1);
                    }
                }
                ddlConUnit.DataTextField = "ccodesc";
                ddlConUnit.DataValueField = "ccod";
                ddlConUnit.DataSource = newtabl;
                ddlConUnit.DataBind();
                ddlConUnit.SelectedValue = conunitcode.ToString();
                //this.ddlConUnit_SelectedIndexChanged(null, null);
            }
        }

        protected void btnMerge_Click(object sender, EventArgs e)
        {
            DataTable tblreq = (DataTable)ViewState["tblreqlist"];
            DataTable tbl2 = (DataTable)ViewState["surveyinfo"];
            string reqno = this.DdlImportRequistion.SelectedValue.ToString();
            string season = "00000";
            if (tbl2.Rows.Count > 0)
            {
                season = tbl2.Rows[0]["season"].ToString();
            }
            DataView dv = tblreq.DefaultView;
            dv.RowFilter = "reqno='"+ reqno + "' and season='"+ season + "'";
            if (dv.ToTable().Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry! You Select Multi Season Requisiton');", true);

                return;
            }
            DataTable tbl1 = dv.ToTable();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                DataRow[] dr = tbl2.Select("rsircode='" + tbl1.Rows[i]["rsircode"] + "' and  spcfcod='" + tbl1.Rows[i]["spcfcod"] + "' and bomid='" +tbl1.Rows[i]["bomid"] + "' and reqno='" + tbl1.Rows[i]["reqno"] + "'");

                if (dr.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + tbl1.Rows[i]["rsirdesc"].ToString() + " Already Added');", true);


                    continue;
                }
                else
                {

                    DataRow dr1 = tbl2.NewRow();
                    dr1["comcod"] = tbl1.Rows[i]["comcod"];
                    dr1["msrno"] = tbl1.Rows[i]["msrno"];
                    dr1["ssircode"] = tbl1.Rows[i]["ssircode"]; 
                    dr1["bomid"] = tbl1.Rows[i]["bomid"];
                    dr1["mlccod"] = tbl1.Rows[i]["mlccod"];
                    dr1["article"] = tbl1.Rows[i]["mlccod"];
                    dr1["rsircode"] = tbl1.Rows[i]["rsircode"];
                    dr1["matcode"] = tbl1.Rows[i]["matcode"];
                    dr1["rsirdesc"] = tbl1.Rows[i]["rsirdesc"];
                    dr1["unit"] = tbl1.Rows[i]["rsirunit"];
                    dr1["spcfcod"] = tbl1.Rows[i]["spcfcod"];
                    dr1["spcfdesc"] = tbl1.Rows[i]["spcfdesc"];
                    dr1["spcfsizedesc"] = tbl1.Rows[i]["spcfdesc"]; 
                    dr1["spcfdesc1"] = tbl1.Rows[i]["spcfdesc"];
                    dr1["spcfdesc2"] = tbl1.Rows[i]["spcfdesc"];
                    dr1["reqqty"] = tbl1.Rows[i]["reqqty"];
                    dr1["conunitqty"] = tbl1.Rows[i]["reqqty"];
                    dr1["sqmrate"] = 0;
                    dr1["resrate"] = tbl1.Rows[i]["resrate"];
                    // (((DataTable)ViewState["tblresRes"]).Select("sircode='" + ds4.Tables[0].Rows[i]["rescode"] + "' "))[0]["rate"].ToString();
                    dr1["reqamt"] = tbl1.Rows[i]["reqamt"];
                    dr1["msrrmrk"] = tbl1.Rows[i]["msrrmrk"];
                    dr1["curdesc"] ="";
                    dr1["selection"] ="";
                    dr1["mattype"] ="";
                    dr1["season"] = tbl1.Rows[i]["season"];
                    dr1["reqno"] = tbl1.Rows[i]["reqno"];
                    dr1["conunit"] = tbl1.Rows[i]["rsirunit"];
                    tbl2.Rows.Add(dr1);
                }

               
            }
            ViewState["surveyinfo"] = tbl2;
            this.Data_bind();
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mORDERNO = this.lblSyspo.Text.Trim().ToString();
            string supplier = this.ddlsupplier.SelectedValue.ToString().Substring(0, 12);
            string msrno = this.ddlsupplier.SelectedValue.ToString().Substring(12, 14);
            string reqno = this.Request.QueryString["genno"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "LoadRdlcVIewer('" + comcod + "', '" + mORDERNO + "','" + supplier + "','" + msrno + "','" + reqno + "');", true);
        }

        protected void ddlReportLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ReportLevel = this.ddlReportLevel.SelectedValue.ToString();

            if (ReportLevel == "4")
            {
                gvsurveyinfo.Columns[9].Visible = true;
            }
            else
            {
                gvsurveyinfo.Columns[9].Visible = false;

            }
            this.Data_bind();
        }

        //protected void ddlConUnit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlLabTest = (DropDownList)sender;
        //    DataGridItem row = (DataGridItem)ddlLabTest.NamingContainer;
        //    DropDownList ddlAddLabTestShortName = (DropDownList)row.FindControl("ddlConUnit");
        //    DataTable stdunit = (DataTable)ViewState["UnitsRate"];
        //    DataView dv = stdunit.DefaultView;

        //    GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        //    Label txtgvconreqqty = (Label)clickedRow.FindControl("txtgvconreqqty");

        //    dv.RowFilter = "bcod = '" + ddlAddLabTestShortName.SelectedIndex.ToString() + "' ";
        //    DataTable tblconreqqty = dv.ToTable();
        //    txtgvconreqqty.Text = tblconreqqty.Rows[0]["conrat"].ToString();

        //}
    }
}