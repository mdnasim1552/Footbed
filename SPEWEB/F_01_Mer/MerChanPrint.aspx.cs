using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY;
using SPEENTITY.C_22_Sal;
using SPERDLC;
using System.Collections;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_01_Mer
{
    public partial class MerChanPrint : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "INQPrint":
                    this.Ind_Inq_Print();
                    break;
                case "ConSheetPrint":
                case "CommConSheetPrint":
                case "PreCostPrint":
                case "CommPreCostPrint":
                    this.PrintConsumptionSheet();
                    break;

                case "OrderPrint":
                    this.Order_Print();

                    break;
                case "BOMPrint":
                    this.BOM_Print();
                    break;
                case "SAMPINQLST":
                    this.lnkIndPrint();
                    break;

                case "PFIWISEORDR":
                    this.PrintPFIWiseOrder();
                    break;
                case "PFIWISEORDRv2":
                    this.PrintPFIWiseOrderV2();
                    break;
            }


        }

        private void PrintPFIWiseOrder()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string pfino = Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_INV_STDANA", "GET_PFI_WISE_ORDR_DETAILS", pfino, "", "", "", "", "", "", "");
            ViewState["tblsalecontact"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tblterms"] = ds1.Tables[1];

            string date = Convert.ToDateTime(ds1.Tables[2].Rows[0]["invdate"]).ToString("dd.MM.yyyy");

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            var lst2 = ds1.Tables[1].DataTableToList<SPEENTITY.C_03_CostABgd.ProformaInvTrms>();
            var lst1 = lst.GroupBy(c => new { c.custordno, c.mlccod }).Select(x =>
                 new SPEENTITY.C_03_CostABgd.EclassSalesContact
                 {
                     mlccod = x.First().mlccod,
                     mlcdesc = x.First().mlcdesc,
                     artno = x.First().artno,
                     custrefno = x.First().custrefno,
                     styleid = x.First().styleid,
                     styledesc = x.First().styledesc,
                     colorid = x.First().colorid,
                     colordesc = x.First().colordesc,
                     sizeid = x.First().sizeid,
                     sizedesc = x.First().sizedesc,
                     ordrqty = x.Sum(x1 => x1.ordrqty),
                     delvdat = x.First().delvdat,
                     rescode = x.First().rescode,
                     resdesc = x.First().resdesc,
                     ordrqty1 = x.Sum(x1 => x1.ordrqty1),
                     rate = x.Average(x1 => x1.rate),
                     ordrno = x.First().ordrno,
                     custordno = x.First().custordno,
                     hscode = x.First().hscode,
                     style1 = x.First().style1,
                     styledesc1 = x.First().styledesc1,
                     balqty = x.Sum(x1 => x1.balqty),
                     rdayid = x.First().rdayid,
                     rdaydesc = x.First().rdaydesc,
                     custname = x.First().custname,
                     custdetails1 = x.First().custdetails1,
                     custdetails2 = x.First().custdetails2,
                     sdino = x.First().sdino,
                     lastformadesc = x.First().lastformadesc,
                     curdesc = x.First().curdesc,
                     subcurdesc = x.First().subcurdesc,
                     cursymbol = x.First().cursymbol,
                     curword = x.First().curword,
                     codedesc = x.First().codedesc
                 }
                ).OrderBy(c =>c.ordrno).ThenBy(n=>n.custordno).ThenBy(m=>m.artno).ToList();
            string totalqty = lst1.Sum(p => p.ordrqty1).ToString("#,##0;(#,##0); ");
            //double totalvalue = Convert.ToDouble((lst1.Sum(p => p.ordrqty1)) * (lst1.Sum(q => q.rate)));

            double discount = Convert.ToDouble( ds1.Tables[2].Rows[0]["discount"].ToString().Trim() ) == 0 ? 
                                0.00 :
                                Convert.ToDouble(ds1.Tables[2].Rows[0]["discount"].ToString().Trim());
            double totalvalue = Convert.ToDouble((lst1.Sum(p => p.ordrqty1 * p.rate)));
            double netPayblAmt = totalvalue - (totalvalue * discount / 100);
            string inwords = ASTUtility.Trans(netPayblAmt, 2).Replace("Taka", lst1[0].curword).Replace("Paisa", lst1[0].subcurdesc).ToString();


            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptProformaInvFb", lst1, lst2, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("netPayable", netPayblAmt.ToString()));
                    Rpt1.SetParameters(new ReportParameter("discount", discount.ToString()));
                    Rpt1.SetParameters(new ReportParameter("discountclaim", "Less " + discount.ToString("#,##0.00;(#,##0.00);") + "%" + " Claim Discount"));
                    Rpt1.SetParameters(new ReportParameter("goodsdesc", "Completed Leather Mens/Ladies Footwear"));
                    break;

                default:
                    Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptProformaInv", lst1, lst2, null);
                    break;
            }

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("amtword", inwords));
            Rpt1.SetParameters(new ReportParameter("pino", ds1.Tables[2].Rows[0]["invno"].ToString() ));
            Rpt1.SetParameters(new ReportParameter("pidate", date));
            Rpt1.SetParameters(new ReportParameter("totalqty", totalqty));
            Rpt1.SetParameters(new ReportParameter("cursymbol", lst[0].cursymbol));
            Rpt1.SetParameters(new ReportParameter("curword", lst[0].curword));
            Rpt1.SetParameters(new ReportParameter("curdesc", lst[0].curdesc));
            Rpt1.SetParameters(new ReportParameter("codedesc", lst[0].codedesc));
            Rpt1.SetParameters(new ReportParameter("rptitle", "PROFORMA INVOICE"));
            Rpt1.SetParameters(new ReportParameter("origin", ""));
            Rpt1.SetParameters(new ReportParameter("custname", lst[0].custname));
            Rpt1.SetParameters(new ReportParameter("custdetails1", lst[0].custdetails1));
            Rpt1.SetParameters(new ReportParameter("custdetails2", lst[0].custdetails2));
            Rpt1.SetParameters(new ReportParameter("payment", ""));
            Rpt1.SetParameters(new ReportParameter("devterms", ""));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }

        private void PrintPFIWiseOrderV2()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string pfino = Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_INV_STDANA", "GET_PFI_WISE_ORDR_DETAILS", pfino, "", "", "", "", "", "", "");
            ViewState["tblsalecontact"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tblterms"] = ds1.Tables[1];

            string date = Convert.ToDateTime(ds1.Tables[2].Rows[0]["invdate"]).ToString("dd.MM.yyyy");
            string tinno = Convert.ToString(ds1.Tables[2].Rows[0]["tinno"]).ToString();

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblsalecontact"];
            var lst2 = ds1.Tables[1].DataTableToList<SPEENTITY.C_03_CostABgd.ProformaInvTrms>();

            var lst1 = lst.GroupBy(c => new { c.custordno, c.mlccod }).Select(x =>
                 new SPEENTITY.C_03_CostABgd.EclassSalesContact
                 {
                     mlccod = x.First().mlccod,
                     mlcdesc = x.First().mlcdesc,
                     artno = x.First().artno,
                     custrefno = x.First().custrefno,
                     styleid = x.First().styleid,
                     styledesc = x.First().styledesc,
                     colorid = x.First().colorid,
                     colordesc = x.First().colordesc,
                     sizeid = x.First().sizeid,
                     sizedesc = x.First().sizedesc,
                     ordrqty = x.Sum(x1 => x1.ordrqty),
                     delvdat = x.First().delvdat,
                     rescode = x.First().rescode,
                     resdesc = x.First().resdesc,
                     ordrqty1 = x.Sum(x1 => x1.ordrqty1),
                     rate = x.Average(x1 => x1.rate),
                     ordrno = x.First().ordrno,
                     custordno = x.First().custordno,
                     hscode = x.First().hscode,
                     style1 = x.First().style1,
                     styledesc1 = x.First().styledesc1,
                     balqty = x.Sum(x1 => x1.balqty),
                     rdayid = x.First().rdayid,
                     rdaydesc = x.First().rdaydesc,
                     custname = x.First().custname,
                     custdetails1 = x.First().custdetails1,
                     custdetails2 = x.First().custdetails2,
                     sdino = x.First().sdino,
                     lastformadesc = x.First().lastformadesc,
                     curdesc = x.First().curdesc,
                     subcurdesc = x.First().subcurdesc,
                     cursymbol = x.First().cursymbol,
                     curword = x.First().curword,
                     codedesc = x.First().codedesc
                 }
                ).OrderBy(c => c.ordrno).ThenBy(n => n.custordno).ThenBy(m => m.artno).ToList();
            string totalqty = lst1.Sum(p => p.ordrqty1).ToString("#,##0;(#,##0); ");
            //double totalvalue = Convert.ToDouble((lst1.Sum(p => p.ordrqty1)) * (lst1.Sum(q => q.rate)));

            double discount = Convert.ToDouble(ds1.Tables[2].Rows[0]["discount"].ToString().Trim()) == 0 ? 0.00 : Convert.ToDouble(ds1.Tables[2].Rows[0]["discount"].ToString().Trim());
            double prepayment = Convert.ToDouble(ds1.Tables[2].Rows[0]["prepayment"].ToString().Trim()) == 0 ? 0.00 : Convert.ToDouble(ds1.Tables[2].Rows[0]["prepayment"].ToString().Trim());
            double totalvalue = Convert.ToDouble(lst1.Sum(p => p.ordrqty1 * p.rate));
            double netPayblAmt = (totalvalue * prepayment / 100) + 1;
            string inwords = ASTUtility.Trans(netPayblAmt, 2).Replace("Taka", lst1[0].curword).Replace("Paisa", lst1[0].subcurdesc).ToString();


            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptProformaInvCCC", lst1, lst2, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("netPayable", netPayblAmt.ToString()));
                    Rpt1.SetParameters(new ReportParameter("discount", discount.ToString()));
                    Rpt1.SetParameters(new ReportParameter("prepayment", prepayment.ToString()));
                    Rpt1.SetParameters(new ReportParameter("paymentpaid", prepayment.ToString("#,##0.0;(#,##0.0);") + "%"));
                    Rpt1.SetParameters(new ReportParameter("discountclaim", "Less " + discount.ToString("#,##0.00;(#,##0.00);") + "%" + " Claim Discount"));
                    Rpt1.SetParameters(new ReportParameter("goodsdesc", "Completed Leather Mens/Ladies Footwear"));
                    Rpt1.SetParameters(new ReportParameter("tinno", tinno));
                    break;

                default:
                    Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptProformaInv", lst1, lst2, null);
                    break;
            }

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("amtword", inwords));
            Rpt1.SetParameters(new ReportParameter("pino", ds1.Tables[2].Rows[0]["invno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("pidate", date));
            Rpt1.SetParameters(new ReportParameter("totalqty", totalqty));
            Rpt1.SetParameters(new ReportParameter("cursymbol", lst[0].cursymbol));
            Rpt1.SetParameters(new ReportParameter("curword", lst[0].curword));
            Rpt1.SetParameters(new ReportParameter("curdesc", lst[0].curdesc));
            Rpt1.SetParameters(new ReportParameter("codedesc", lst[0].codedesc));
            Rpt1.SetParameters(new ReportParameter("consigne", lst[0].consigne));
            Rpt1.SetParameters(new ReportParameter("rptitle", "PREPAYMENT INVOICE"));
            Rpt1.SetParameters(new ReportParameter("origin", ""));
            Rpt1.SetParameters(new ReportParameter("custname", lst[0].custname));
            Rpt1.SetParameters(new ReportParameter("custdetails1", lst[0].custdetails1));
            Rpt1.SetParameters(new ReportParameter("custdetails2", lst[0].custdetails2));
            Rpt1.SetParameters(new ReportParameter("payment", ""));
            Rpt1.SetParameters(new ReportParameter("devterms", ""));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }

        protected void lnkIndPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            //string inqnum = "";
            string CurDate1 = "";
            //string buyer = "";
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string buyer = "";

            buyer = this.Request.QueryString["inqno"].ToString();
            string inqnum = this.Request.QueryString["inqno"].ToString();
            CurDate1 = this.Request.QueryString["inqno"].ToString();

            //buyer = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("txtgvSupplier")).Text.ToString();
            //inqnum = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            //CurDate1 = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("txtgvItemdescc")).Text.ToString();
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqnum, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                // ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataTable dt = ds1.Tables[0];


            var lst = new List<SPEENTITY.C_01_Mer.EclassSampleInquiry>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.EclassSampleInquiry();

                obj1.styleid = dr1["styleid"].ToString();
                obj1.styledesc = dr1["styledesc"].ToString();
                obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);
                obj1.color = dr1["color"].ToString();
                //obj1.xmldata = dr1["xmldata"].ToString();   
                obj1.artno = dr1["artno"].ToString();
                obj1.catedesc = dr1["catedesc"].ToString();
                obj1.category = dr1["category"].ToString();
                obj1.comcod = dr1["comcod"].ToString();
                obj1.consize = dr1["consize"].ToString();
                obj1.samsize = dr1["samsize"].ToString();
                obj1.sirunit = dr1["sirunit"].ToString();

                obj1.attchmnt = dr1["attchmnt"].ToString();
                obj1.sizernge = dr1["sizernge"].ToString();
                //string att = obj1.attchmnt;
                obj1.attchmnt = (dr1["attchmnt"].ToString().Length == 0 ? "" : new Uri(Server.MapPath(dr1["attchmnt"].ToString())).AbsoluteUri);


                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;


                lst.Add(obj1);
            }


            LocalReport rpt1 = new LocalReport();



            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptSampleEntry", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("buyer", "Buyer: " + buyer));
            rpt1.SetParameters(new ReportParameter("date", "Date: " + CurDate1));
            rpt1.SetParameters(new ReportParameter("inqnum", "Inquery No: " + inqnum));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Sample Inquery Entry "));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            //string type = "PDF";
            //ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void Ind_Inq_Print()
        {

            string inqnum = this.Request.QueryString["inqno"].ToString();
            string printType = this.Request.QueryString["printtype"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];

            //string inqnum = "";
            string CurDate1 = "";
            //string buyer = "";
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            //buyer = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("txtgvSupplier")).Text.ToString();
            //inqnum = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            //CurDate1 = ((Label)this.gvSmpleinqlist.Rows[index].FindControl("txtgvItemdescc")).Text.ToString();
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqnum, printdate,
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                // ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataTable dt = ds1.Tables[0];


            var lst = new List<SPEENTITY.C_01_Mer.EclassSampleInquiry>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.EclassSampleInquiry();

                obj1.styleid = dr1["styleid"].ToString();
                obj1.styledesc = dr1["styledesc"].ToString();
                obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);
                obj1.color = dr1["color"].ToString();
                //obj1.xmldata = dr1["xmldata"].ToString();   
                obj1.artno = dr1["artno"].ToString();
                obj1.catedesc = dr1["catedesc"].ToString();
                obj1.category = dr1["category"].ToString();
                obj1.comcod = dr1["comcod"].ToString();
                obj1.consize = dr1["consize"].ToString();
                obj1.samsize = dr1["samsize"].ToString();
                obj1.sirunit = dr1["sirunit"].ToString();
                obj1.seasondesc = dr1["seasondesc"].ToString();
                obj1.brandesc = dr1["brandesc"].ToString();
                obj1.attchmnt = dr1["attchmnt"].ToString();
                obj1.sizernge = dr1["sizernge"].ToString();
                //string att = obj1.attchmnt;
                obj1.attchmnt = (dr1["attchmnt"].ToString().Length == 0 ? "" : new Uri(Server.MapPath(dr1["attchmnt"].ToString())).AbsoluteUri);


                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;


                lst.Add(obj1);
            }


            LocalReport rpt1 = new LocalReport();



            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptSampleEntry", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("buyer", ds1.Tables[1].Rows[0]["buyername"].ToString()));
            rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds1.Tables[1].Rows[0]["inqdat"]).ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("inqnum", "INQUERY NO: " + ds1.Tables[1].Rows[0]["inqno1"]));
            rpt1.SetParameters(new ReportParameter("rpttitle", "SAMPLE INQUIRY ENTRY"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     printType + "', target='_self');</script>";
        }

        private DataTable HiddenSameValue(DataTable dt)
        {

            string procode = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (procode == dt.Rows[i]["procode"].ToString())
                {
                    dt.Rows[i]["prodesc"] = "";
                }
                procode = dt.Rows[i]["procode"].ToString();
            }
            ViewState["tblstdcost"] = dt;
            return dt;
        }
        private DataTable HiddenSameValue1(DataTable dt)
        {

            string procode = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (procode == dt.Rows[i]["procode"].ToString())
                {
                    dt.Rows[i]["prodesc"] = "";
                }
                procode = dt.Rows[i]["procode"].ToString();
            }
            ViewState["tblcomItm"] = dt;
            return dt;
        }

        private void PrintConsumptionSheet()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string inqno = this.Request.QueryString["inqno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqno, "", "", "", "", "", "", "");
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassSampleInquiry>();
            ViewState["tblinquery"] = list1;
            string styleid = this.Request.QueryString["styleid"].ToString();  //HiddenSameValue
            string printype = this.Request.QueryString["printtype"].ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list11 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list11.FindAll(s => s.styleid == styleid);
            string url = list2[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }



            string artno = list2[0].artno.ToString();
            string color = list2[0].color.ToString();
            string range = list2[0].sizernge.ToString();
            string size = list2[0].consize.ToString();
            string qty = Convert.ToDouble(list2[0].ordqty).ToString("#,##0.00;(#,##0.00); ");
            string date = Convert.ToDateTime(ds1.Tables[1].Rows[0]["inqdat"]).ToString("dd-MMM-yyyy");
            string buyer = ds1.Tables[1].Rows[0]["buyername"].ToString();
            string mrsendizer = ds1.Tables[1].Rows[0]["postedbyname"].ToString();
            string catgory = list2[0].catedesc.ToString();

            string smploptionno = list2[0].samsize.ToString();
            string season = list2[0].seasondesc.ToString();
            DataSet dsComm = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COMM_MATERIALS", inqno, styleid, "%", "", "", "", "");
            var lstComm = HiddenSameValue1(dsComm.Tables[0]).DataTableToList<SPEENTITY.C_01_Mer.CommonMterailsCal>();
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_INFO_FOR_CONSUMPTION", inqno, styleid, "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            var lst1 = ds3.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCost>();
            DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_INFO", styleid, "%", inqno, "", "", "", "");
            ViewState["tblstdcost"] = HiddenSameValue(ds4.Tables[0]);
            Session["tblstdspmsize"] = ds4.Tables[1];
            //DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_INFO", prodcode, processcode, lccode, "", "", "", "");
            string lstref = ds3.Tables[0].Rows[0]["lstrefno"].ToString();
            string construction = ds3.Tables[0].Rows[0]["constrction"].ToString();
            string brndname = ds3.Tables[0].Rows[0]["brndnme"].ToString();
            string estdate = ds3.Tables[0].Rows[0]["estprddat"].ToString();
            string exrate = Convert.ToDouble(ds3.Tables[0].Rows[0]["exrate"]).ToString("#,##0.00;(#,##0.00); ");
            string tarrate = Convert.ToDouble(ds3.Tables[0].Rows[0]["tarprice"]).ToString("#,##0.00;(#,##0.00); ");
            string offer = Convert.ToDouble(ds3.Tables[0].Rows[0]["offprice"]).ToString("#,##0.00;(#,##0.00); ");
            string confirm = Convert.ToDouble(ds3.Tables[0].Rows[0]["cnfrmprice"]).ToString("#,##0.00;(#,##0.00); ");
            string currency = "";// ds3.Tables[0].Rows[0]["currency"].ToString();
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            if (dt == null)
            {
                return;
            }
            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumption>();
            var lst2 = ds4.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();
            double tAmt = lst.Select(p => p.amt).Sum();
            double tAmtc = lst1.Select(p => p.amt).Sum();
            string artamount = Convert.ToDouble(tAmt + tAmtc).ToString("#,##0.00;(#,##0.00); ");

            LocalReport rpt1 = new LocalReport();
            string Type = this.Request.QueryString["Type"].ToString();
            if (Type == "ConSheetPrint")
            {
                switch (comcod)
                {
                    case "5305":
                        rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptConsumptionSheetFB", lst, lst2, null);
                        break;
                    default:
                        rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptConsumptionSheet", lst, null, null);
                        break;
                }

                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));

                rpt1.SetParameters(new ReportParameter("artno", artno));
                rpt1.SetParameters(new ReportParameter("color", color));
                rpt1.SetParameters(new ReportParameter("range", range));
                rpt1.SetParameters(new ReportParameter("size", size));
                rpt1.SetParameters(new ReportParameter("qty", qty));
                rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
                rpt1.SetParameters(new ReportParameter("buyer", buyer));
                rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
                rpt1.SetParameters(new ReportParameter("catgory", catgory));
                rpt1.SetParameters(new ReportParameter("lstref", lstref));
                rpt1.SetParameters(new ReportParameter("construction", construction));
                rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
                rpt1.SetParameters(new ReportParameter("season", season));
                rpt1.SetParameters(new ReportParameter("brndname", brndname));
                rpt1.SetParameters(new ReportParameter("image", image));
                rpt1.SetParameters(new ReportParameter("estdate", estdate));

                //rpt1.SetParameters(new ReportParameter("costrang", costrang));
                //rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
                //rpt1.SetParameters(new ReportParameter("technician", technician));
                //rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
                //rpt1.SetParameters(new ReportParameter("style", style));

                rpt1.SetParameters(new ReportParameter("RptTitle", "Consumption Sheet"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

                //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
                //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            }
            else if (Type == "ConSheetPrint")
            {


                rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptCommonConsSheet", lstComm, null, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));

                rpt1.SetParameters(new ReportParameter("artno", artno));
                rpt1.SetParameters(new ReportParameter("color", color));
                rpt1.SetParameters(new ReportParameter("range", range));
                rpt1.SetParameters(new ReportParameter("size", size));
                rpt1.SetParameters(new ReportParameter("qty", qty));
                rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
                rpt1.SetParameters(new ReportParameter("buyer", buyer));
                rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
                rpt1.SetParameters(new ReportParameter("catgory", catgory));
                rpt1.SetParameters(new ReportParameter("lstref", lstref));
                rpt1.SetParameters(new ReportParameter("construction", construction));
                rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
                rpt1.SetParameters(new ReportParameter("season", season));
                rpt1.SetParameters(new ReportParameter("brndname", brndname));
                rpt1.SetParameters(new ReportParameter("estdate", estdate));

                rpt1.SetParameters(new ReportParameter("RptTitle", "Common Material  Consumption"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            }

            else if (Type == "PreCostPrint")
            {
                switch (comcod)
                {
                    case "5305":
                        rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptPreCostingSheetFB", lst, lst2, null);
                        break;
                    default:
                        rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptPreCostingSheet", lst, lst1, null);
                        break;
                }

                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));

                rpt1.SetParameters(new ReportParameter("artno", artno));
                rpt1.SetParameters(new ReportParameter("color", color));
                rpt1.SetParameters(new ReportParameter("range", range));
                rpt1.SetParameters(new ReportParameter("size", size));
                rpt1.SetParameters(new ReportParameter("qty", qty));
                rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
                rpt1.SetParameters(new ReportParameter("buyer", buyer));
                rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
                rpt1.SetParameters(new ReportParameter("catgory", catgory));
                rpt1.SetParameters(new ReportParameter("lstref", lstref));
                rpt1.SetParameters(new ReportParameter("construction", construction));
                rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
                rpt1.SetParameters(new ReportParameter("season", season));
                rpt1.SetParameters(new ReportParameter("brndname", brndname));
                rpt1.SetParameters(new ReportParameter("image", image));
                rpt1.SetParameters(new ReportParameter("estdate", estdate));
                rpt1.SetParameters(new ReportParameter("exrate", exrate));
                rpt1.SetParameters(new ReportParameter("tarrate", tarrate));
                rpt1.SetParameters(new ReportParameter("offer", offer));
                rpt1.SetParameters(new ReportParameter("confirm", confirm));
                rpt1.SetParameters(new ReportParameter("currency", currency));
                rpt1.SetParameters(new ReportParameter("artamount", artamount));
                rpt1.SetParameters(new ReportParameter("costrang", size));
                rpt1.SetParameters(new ReportParameter("ordrqty", qty));
                rpt1.SetParameters(new ReportParameter("technician", smploptionno));
                rpt1.SetParameters(new ReportParameter("lastmold", lstref));
                rpt1.SetParameters(new ReportParameter("style", catgory));
                rpt1.SetParameters(new ReportParameter("RptTitle", "CBD Sheet"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptCommonCostingSheet", lstComm, null, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));

                rpt1.SetParameters(new ReportParameter("artno", artno));
                rpt1.SetParameters(new ReportParameter("color", color));
                rpt1.SetParameters(new ReportParameter("range", range));
                rpt1.SetParameters(new ReportParameter("size", size));
                rpt1.SetParameters(new ReportParameter("qty", qty));
                rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
                rpt1.SetParameters(new ReportParameter("buyer", buyer));
                rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
                rpt1.SetParameters(new ReportParameter("catgory", catgory));
                rpt1.SetParameters(new ReportParameter("lstref", lstref));
                rpt1.SetParameters(new ReportParameter("construction", construction));
                rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
                rpt1.SetParameters(new ReportParameter("season", season));
                rpt1.SetParameters(new ReportParameter("brndname", brndname));
                rpt1.SetParameters(new ReportParameter("estdate", estdate));

                rpt1.SetParameters(new ReportParameter("RptTitle", "Common Material Pre-Costing"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            }
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
               printype + "', target='_self');</script>";
        }
        //private void PrintConsumptionSheetfb()
        //{
        //    string comcod = this.GetCompCode();
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

        //    string styleid = this.ddlStyle.SelectedValue.ToString();
        //    List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
        //    List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list1.FindAll(s => s.styleid == styleid);
        //    string url = list2[0].images.ToString();
        //    string image = "";

        //    if (url.Length > 0)
        //    {
        //        image = new Uri(Server.MapPath(url)).AbsoluteUri;
        //    }
        //    else
        //    {
        //        image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
        //    }



        //    string artno = this.txtArtno.Text.Trim().ToString();
        //    string color = this.txtcolor.Text.Trim().ToString();
        //    string range = this.txtsizernge.Text.Trim().ToString();
        //    string size = this.txtconsize.Text.Trim().ToString();
        //    string qty = this.txtordqty.Text.Trim().ToString();
        //    string date = this.txtDatefrom.Text.Trim().ToString();
        //    string buyer = this.txtbuyer.Text.Trim().ToString();
        //    string mrsendizer = this.txtmerchand.Text.Trim().ToString();
        //    string catgory = this.txtCategory.Text.Trim().ToString();
        //    string lstref = this.txtlastrefno.Text.Trim().ToString();
        //    string construction = this.txtconstruction.Text.Trim().ToString();
        //    string smploptionno = this.txtsampleno.Text.Trim().ToString();
        //    string season = this.txtseason.Text.Trim().ToString();
        //    string brndname = this.txtbrand.Text.Trim().ToString();
        //    string estdate = this.txtestproduction.Text.Trim().ToString();
        //    string costrang = size;
        //    string ordrqty = this.txtordqty.Text.Trim().ToString(); ;
        //    string technician = this.txtsampleno.Text.Trim().ToString();
        //    string lastmold = this.txtlastrefno.Text.Trim().ToString();
        //    string style = this.txtCategory.Text.Trim().ToString();



        //    DataTable dt = (DataTable)ViewState["tblstdcost"];
        //    DataTable tbl3 = (DataTable)ViewState["tblSmpleSizes"];
        //    if (dt == null)
        //    {
        //        return;
        //    }

        //    var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumption>();
        //    var lst2 = tbl3.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();


        //    LocalReport rpt1 = new LocalReport();
        //    switch (comcod)
        //    {
        //        case "5305":
        //            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptConsumptionSheetFB", lst, lst2, null);
        //            break;
        //        default:
        //            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptConsumptionSheet", lst, null, null);
        //            break;
        //    }

        //    rpt1.EnableExternalImages = true;

        //    rpt1.SetParameters(new ReportParameter("comnam", comnam));
        //    rpt1.SetParameters(new ReportParameter("comadd", comadd));

        //    rpt1.SetParameters(new ReportParameter("artno", artno));
        //    rpt1.SetParameters(new ReportParameter("color", color));
        //    rpt1.SetParameters(new ReportParameter("range", range));
        //    rpt1.SetParameters(new ReportParameter("size", size));
        //    rpt1.SetParameters(new ReportParameter("qty", qty));
        //    rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
        //    rpt1.SetParameters(new ReportParameter("buyer", buyer));
        //    rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
        //    rpt1.SetParameters(new ReportParameter("catgory", catgory));
        //    rpt1.SetParameters(new ReportParameter("lstref", lstref));
        //    rpt1.SetParameters(new ReportParameter("construction", construction));
        //    rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
        //    rpt1.SetParameters(new ReportParameter("season", season));
        //    rpt1.SetParameters(new ReportParameter("brndname", brndname));
        //    rpt1.SetParameters(new ReportParameter("image", image));
        //    rpt1.SetParameters(new ReportParameter("estdate", estdate));

        //    rpt1.SetParameters(new ReportParameter("costrang", costrang));
        //    rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
        //    rpt1.SetParameters(new ReportParameter("technician", technician));
        //    rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
        //    rpt1.SetParameters(new ReportParameter("style", style));

        //    rpt1.SetParameters(new ReportParameter("RptTitle", "Consumption Sheet"));
        //    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

        //    //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
        //    //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

        //    Session["Report1"] = rpt1;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
        //        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}
        private void Order_Print()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string printype = this.Request.QueryString["printtype"].ToString();

            //string date = Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yyyy");
            //string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");


            string comcod = this.GetCompCode();
            string mlccod = this.Request.QueryString["mlccod"].ToString();
            string date = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
            string dayid = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("yyyyMMdd");
            string styleid = Convert.ToString(this.Request.QueryString["styleid"]);
            string type = (dayid != "19000101") ? "Reorder" : "";
            DataSet ds1Ord = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "MCLC_WISE_ORDERINFO", mlccod, type, date, "", "", "", "", ""); ;
            DataView dv = ds1Ord.Tables[0].DefaultView;
            dv.RowFilter = "styleid='" + styleid + "'";
            ViewState["orderinfo"] = dv.ToTable();
            DataTable dt = (DataTable)ViewState["orderinfo"];
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, type, date, styleid + "%", "", "", "", ""); ;

            if (ds1 == null)
                return;

            string mStyleID = "xxxxxxx";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID)
                    ds1.Tables[0].Rows[i]["StyleDesc"] = " >> ";
                mStyleID = ds1.Tables[0].Rows[i]["styleid"].ToString();
            }


            ViewState["tblOrderQty"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            List<SPEENTITY.C_01_Mer.EclassPartInOrder> lstPart = ds1.Tables[3].DataTableToList<SPEENTITY.C_01_Mer.EclassPartInOrder>();
            ViewState["Partname"] = lstPart;



            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];



            string month = System.DateTime.Today.ToString("MMMM-yyyy");
            var lst = new List<SPEENTITY.C_01_Mer.OrderDetails>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.OrderDetails();

                obj1.buyerid = dr1["buyerid"].ToString();
                obj1.buyername = dr1["buyername"].ToString();
                obj1.inqno = dr1["inqno"].ToString();
                obj1.ordrno = dr1["ordrno"].ToString();
                //obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);

                obj1.artno = dr1["artno"].ToString();
                obj1.catedesc = dr1["catedesc"].ToString();
                obj1.category = dr1["category"].ToString();
                obj1.comcod = dr1["comcod"].ToString();
                obj1.ordrcvdat = Convert.ToDateTime(dr1["ordrcvdat"]);
                obj1.shipmntdat = Convert.ToDateTime(dr1["shipmntdat"]);
                obj1.color = dr1["color"].ToString();
                obj1.currency = dr1["currency"].ToString();
                obj1.exrate = Convert.ToDouble(dr1["exrate"]);
                obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);
                obj1.price = Convert.ToDouble(dr1["price"]);
                obj1.cnfrmprice = Convert.ToDouble(dr1["cnfrmprice"]);
                obj1.sizernge = dr1["sizernge"].ToString();
                obj1.autoartcle = dr1["autoartcle"].ToString();
                //string att = obj1.attchmnt;

                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;

                lst.Add(obj1);
            }



            string payterm = "";
            //string size1 = "";
            //string size2 = "";
            //string size3 = "";
            //string size4 = "";
            //string size5 = "";
            //string size6 = "";
            //string size7 = "";
            //string size8 = "";
            //string size9 = "";
            //string size10 = "";
            //string size11 = "";
            //string size12 = "";
            //string size13= "";
            //string size14= "";
            //string size15= "";


            LocalReport rpt1 = new LocalReport();

            var lst1 = (List<SPEENTITY.C_01_Mer.EclassPartInOrder>)ViewState["Partname"];


            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptOrderSheet", lst, lst1, lst2);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("payterm", payterm));

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), ds1.Tables[1].Rows[i]["SizeDesc"].ToString()));

            }

            rpt1.SetParameters(new ReportParameter("rpttitle", "Order Input Sheet "));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     printype + "', target='_self');</script>";
        }

        private void BOM_Print()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string mlccod = this.Request.QueryString["mlccod"].ToString();
            string styleid = this.Request.QueryString["sircode"].ToString().Substring(0, 12);
            string colorid = this.Request.QueryString["sircode"].ToString().Substring(12, 12);

            var dayid = this.Request.QueryString["dayid"].ToString().Trim();
            string date = (dayid == "00000000") ? "01-Jan-1900" : Convert.ToDateTime(dayid.Substring(4, 2) + "/" + dayid.Substring(6, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");

            string Dept = "";
            if (Request.QueryString.AllKeys.Contains("Dept"))
            {
                Dept = Request.QueryString["Dept"].ToString();
            }
            string format = (this.Request.QueryString["format"].ToString().Length > 0) ? this.Request.QueryString["format"].ToString() : "PDF";

            string rtype = dayid != "00000000" ? "Reorder" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", (comcod == "5301") ? "GET_BOM_MATERIALINFO_FOR_PRINT" : "GET_BOM_MATERIALINFO_FOR_PRINT_FB", mlccod, dayid, styleid, colorid, Dept, "", "", "");

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, rtype, date, styleid, Dept, colorid, "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblimport"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            ViewState["tbllocal"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            //DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_INFO", styleid, "%", ds1.Tables[4].Rows[0]["inqno"].ToString() , "", "", "", "");
            
            List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder> lst = ds1.Tables[3].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            var tbladdlocal = lst.FindAll(p => p.reqtype == "ADDLOCAL");
            var tbladdimport = lst.FindAll(p => p.reqtype == "ADDIMPORT");
            var list = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)ViewState["tblimport"];
            var loclist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)ViewState["tbllocal"];
            var lst2 = ds2.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();

            LocalReport rpt1 = new LocalReport();
            string type = "";
            string heading = "";
            string buyername = "";
            string frwdbyuser = "";
            string approvedby = "";
            string styledesc = "";
            string colordesc = "";
            string artno = "";
            string ordrqty = "";
            string catedesc = "";
            string orderno = "";
            string orddate = "";
            string aprvdat = "";
            string frwddat = "";
            string estprodate = "";
            string lforma = "";
            string notes = "";
            string samsize = "";
            string sizernge = "";
            string brand = "";
            string bomid = "";
            string season = "";
            string inspdate = "";
            string exdate = "";

            if (ds1.Tables[2].Rows.Count != 0)
            {
                type = this.Request.QueryString["ptype"].ToString();
                heading = "";
                buyername = ds1.Tables[2].Rows[0]["buyername"].ToString();
                frwdbyuser = ds1.Tables[2].Rows[0]["frwdbyuser"].ToString();
                approvedby = ds1.Tables[2].Rows[0]["approvedby"].ToString();
                styledesc = ds1.Tables[2].Rows[0]["styledesc"].ToString();
                colordesc = ds1.Tables[2].Rows[0]["colordesc"].ToString();
                artno = ds1.Tables[2].Rows[0]["artno"].ToString();
                ordrqty = Convert.ToDouble(ds1.Tables[2].Rows[0]["ordrqty"]).ToString("#,##0;(#,##0); ");
                catedesc = ds1.Tables[2].Rows[0]["catedesc"].ToString();
                orderno = ds1.Tables[2].Rows[0]["orderno"].ToString();
                orddate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["orddate"]).ToString("dd-MMM-yyyy");
                estprodate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["estprddat"]).ToString("dd-MMM-yyyy");
                aprvdat = ds1.Tables[2].Rows[0]["aprvdat"].ToString();
                lforma = ds1.Tables[2].Rows[0]["lforma"].ToString();
                lforma = ds1.Tables[2].Rows[0]["lforma"].ToString();
                samsize = ds1.Tables[2].Rows[0]["samsize"].ToString();
                sizernge = ds1.Tables[2].Rows[0]["sizernge"].ToString();
                brand = ds1.Tables[2].Rows[0]["brand"].ToString();
                bomid = ds1.Tables[2].Rows[0]["bomid"].ToString();
                season = ds1.Tables[2].Rows[0]["season"].ToString();
                inspdate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["inspdate"]).ToString("dd-MMM-yyyy");
                exdate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["shipmntdat"]).ToString("dd-MMM-yyyy");
                notes = ds1.Tables[2].Rows[0]["notes"].ToString();
            }

            if (type == "import")
            {
                heading = "BILL OF MATERIALS";
                switch (comcod)
                {
                    case "5305":
                    case "5306":

                        DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "GET_ORDER_PACKING_DETAILS", mlccod, dayid, styleid, "", "", "", ""); ;
                        List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst3 = ds3.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
                        List<SPEENTITY.C_01_Mer.EclassConsumptionSamSize> lst4 = ds1.Tables[5].DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();

                        lst2.AddRange(lst3);
                        int count = ds3.Tables[1].Rows.Count - 1;
                        sizernge = ds3.Tables[1].Rows[0]["sizedesc"].ToString() + "-" + ds3.Tables[1].Rows[count]["sizedesc"].ToString();
                        if (Request.QueryString.AllKeys.Contains("info"))
                        {
                            if (Request.QueryString["info"].ToString() == "packinginfo")
                            {
                                heading = "Packing Details";
                                rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptMatReqImportFBPakingInfo", list.ToList(), lst4, lst2);
                            }
                        }
                        else
                        {
                            rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptMatReqImportFB", list.ToList(), lst4, lst2);
                        }
                        //var lst2 = ds4.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();
                        rpt1.EnableExternalImages = true;
                        for (int i = 0; i < ds2.Tables[1].Rows.Count; i++)
                        {
                            rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), ds2.Tables[1].Rows[i]["SizeDesc"].ToString()));

                        }
                        string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                        string image = new Uri(Server.MapPath(ds1.Tables[2].Rows[0]["imgurl"].ToString())).AbsoluteUri;
                        rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                        rpt1.SetParameters(new ReportParameter("comadd", comadd));
                        rpt1.SetParameters(new ReportParameter("ImgUrl", image));
                        rpt1.SetParameters(new ReportParameter("lforma", lforma));
                        rpt1.SetParameters(new ReportParameter("notes", notes));
                        rpt1.SetParameters(new ReportParameter("samsize", samsize));
                        rpt1.SetParameters(new ReportParameter("sizernge", sizernge));
                        rpt1.SetParameters(new ReportParameter("brand", brand));
                        rpt1.SetParameters(new ReportParameter("bomid", bomid));
                        rpt1.SetParameters(new ReportParameter("season", season));
                        rpt1.SetParameters(new ReportParameter("inspdate", inspdate));
                        rpt1.SetParameters(new ReportParameter("exdate", exdate));
                        rpt1.SetParameters(new ReportParameter("aprvdat", aprvdat));

                        break;
                    default:
                        rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptMatReqImport", list.Concat(tbladdimport).ToList(), null, null);
                        break;
                }
            }
            else
            {
                heading = "MATERIAL REQUIREMENT AGAINST ORDER- LOCAL PURCHASE";
                rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptMatReqLocal", HiddenSameData(loclist).Concat(tbladdlocal).ToList(), null, null);
            }

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("RptCustname", buyername));
            rpt1.SetParameters(new ReportParameter("RptOrdName", orderno));
            rpt1.SetParameters(new ReportParameter("OrdQty", ordrqty));
            rpt1.SetParameters(new ReportParameter("ProDate", estprodate));
            rpt1.SetParameters(new ReportParameter("style", styledesc));
            rpt1.SetParameters(new ReportParameter("color", colordesc));
            rpt1.SetParameters(new ReportParameter("article", artno));
            rpt1.SetParameters(new ReportParameter("category", catedesc));
            rpt1.SetParameters(new ReportParameter("RptTitle", heading)); // 
            rpt1.SetParameters(new ReportParameter("OrdDate", orddate));
            rpt1.SetParameters(new ReportParameter("approvedby", approvedby + "\n" + aprvdat));
            rpt1.SetParameters(new ReportParameter("frwdbyuser", frwdbyuser + "\n" + frwddat)); // 
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + format + "', target='_self');</script>";

        }


        private List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder> HiddenSameData(List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder> lst)
        {

            string procode = "";
            var list22 = lst.OrderBy(m => m.procode).ThenBy(m => m.rsircode).ThenBy(m => m.spcfcode).ToList();
            foreach (SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder c1 in list22)
            {
                if (procode == c1.procode.ToString())
                {
                    c1.procname = "";
                }
                procode = c1.procode.ToString();

            }
            ViewState["tbllocal"] = list22;
            return list22;

        }

    }
}