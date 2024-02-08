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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.Collections;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SPEWEB.F_04_Sampling
{
    public partial class Print : System.Web.UI.Page
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
                case "CBD":
                case "PreCostPrint":
                case "PGEntry":
                case "CommPreCostPrint":
                    switch (comcod)
                    {
                        case "5101":
                        case "5306":
                        case "5305":
                            PrintConsumptionSheetFB();
                            break;
                        default:
                            PrintConsumptionSheet();
                            break;
                    }
                    break;
                case "SAMPINQLST":
                    this.lnkIndPrint();
                    break;

            }


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
            rpt1.SetParameters(new ReportParameter("inqnum", inqnum));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Sample Development Inquery "));
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

            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQINFO", inqnum,
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                // ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataTable dt = ds1.Tables[0];


            var lst = new List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling();

                obj1.samptype = dr1["samptype"].ToString();
                obj1.samtypedesc = dr1["samtypedesc"].ToString();
                obj1.samqty = Convert.ToDouble(dr1["samqty"]);
                obj1.shoetype = dr1["shoetype"].ToString();
                obj1.agent = dr1["agent"].ToString();
                obj1.agentcod = dr1["agentcod"].ToString();
                obj1.construction = dr1["construction"].ToString();   
                obj1.article = dr1["article"].ToString();
                obj1.catagory = dr1["catagory"].ToString();
                obj1.catagorydesc = dr1["catagorydesc"].ToString();
                obj1.comsize = dr1["comsize"].ToString();
                obj1.samsize = dr1["samsize"].ToString();
                obj1.sizerange = dr1["sizerange"].ToString();
                obj1.brand = dr1["brand"].ToString();
                obj1.seasondesc = dr1["seasondesc"].ToString();
                obj1.branddesc = dr1["branddesc"].ToString();
                obj1.season = dr1["season"].ToString();
                obj1.remarks = dr1["remarks"].ToString();
                obj1.colordesc = dr1["colordesc"].ToString();
                obj1.batchdesc = dr1["batchdesc"].ToString();
             

                obj1.lformadesc = dr1["lformadesc"].ToString();

                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;


                lst.Add(obj1);
            }


            LocalReport rpt1 = new LocalReport();



            rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptSampleInterfaceInquery", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds1.Tables[0].Rows[0]["deldate"]).ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("sdino", ds1.Tables[0].Rows[0]["inqno"].ToString()));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Sample Development Inquery"));
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

            string inqno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqno, "", "", "", "", "", "", "");
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassSampleInquiry>();
            ViewState["tblinquery"] = list1;
            //string styleid = this.Request.QueryString["styleid"].ToString();  //HiddenSameValue
            string printype = this.Request.QueryString["printtype"].ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list11 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            //List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list11.FindAll(s => s.styleid == styleid);
            string url = list1[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }



            string artno = list11[0].artno.ToString();
            string color = list11[0].color.ToString();
            string range = list11[0].sizernge.ToString();
            string size = list11[0].consize.ToString();
            string qty = Convert.ToDouble(list11[0].ordqty).ToString("#,##0.00;(#,##0.00); ");
            string date = Convert.ToDateTime(ds1.Tables[1].Rows[0]["inqdat"]).ToString("dd-MMM-yyyy");
            string buyer = ds1.Tables[1].Rows[0]["buyername"].ToString();
            string mrsendizer = ds1.Tables[1].Rows[0]["postedbyname"].ToString();
            string catgory = list11[0].catedesc.ToString();

            string smploptionno = list11[0].samsize.ToString();
            string season = list11[0].seasondesc.ToString();
            DataSet dsComm = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COMM_MATERIALS", inqno, "", "%", "", "", "", "");
            var lstComm = HiddenSameValue1(dsComm.Tables[0]).DataTableToList<SPEENTITY.C_01_Mer.CommonMterailsCal>();
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_INFO_FOR_CONSUMPTION", inqno, "", "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            var lst1 = ds3.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCost>();
            DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_INFO", "", "%", inqno, "", "", "", "");
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
        private void GetInqnumber()
        {
          
        }
        private void PrintConsumptionSheetFB()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string qinqno = this.Request.QueryString["genno"].ToString();
            string srchinqno = (qinqno.Length > 0 ? qinqno : "") + "%";
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQNO", todate, srchinqno);

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQINFO", ds.Tables[0].Rows[0]["sampleid"].ToString(), "",
                          "", "", "", "", "", "", "");

            var lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>();
            string url = lst1[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }

            string artno = lst1[0].article.Trim();
            string color = lst1[0].colordesc;
            string range = lst1[0].sizerange;
            string size = lst1[0].comsize.ToString();
            string qty = "";
            
            string buyer = lst1[0].buyerdesc;
            string mrsendizer = "";
            string catgory = lst1[0].catagorydesc; 
            string lstref = "";
            string construction = lst1[0].construction;
            string smploptionno = "";
            string season = lst1[0].seasondesc;
            string brndname = "";
            string estdate = "";
            string costrang = lst1[0].comsize;
            string ordrqty = "";
            string technician = "";
            string lastmold = lst1[0].lformadesc;
            string samptype = lst1[0].samtypedesc;
            string forma = lst1[0].lformadesc;
            string brand = lst1[0].branddesc;
            string samqty = lst1[0].samqty.ToString();
            
            
            string style = lst1[0].catagorydesc;
            string Type = this.Request.QueryString["Type"].ToString();

            DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCONSUMPTIONINFO", ds.Tables[0].Rows[0]["sampleid"].ToString(), "", "", "", "", "", "");
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_SAM_CONSUMPTION_INFO", ds.Tables[0].Rows[0]["sampleid"].ToString(), "", "", "", "");
            //DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_INFO", "", "%", ds.Tables[0].Rows[0]["sampleid"].ToString(), "", "", "", "");
            if (ds4 == null)
            {
                return;
            }
            
            string date = (ds4.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds4.Tables[0].Rows[0]["csumpdat"]).ToString("dd-MMM-yyyy");
            var lst = ds4.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionFB>();
            var lstCost = ds4.Tables[2].DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCostSam>();
            var curinfo = ds4.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassCurinfo>();
            var samsize = ds5.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();
            DataView dv = ds4.Tables[2].Copy().DefaultView;
            dv.RowFilter = "sircode not like '%999'";

            double totlcst = Convert.ToDouble((Convert.IsDBNull(ds4.Tables[0].Compute("sum(amt)", "")) ? 0.00 : ds4.Tables[0].Compute("sum(amt)", "")));
            double tltcommncost = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(stdamt)", "")) ? 0.00 : dv.ToTable().Compute("sum(stdamt)", "")));
            string totalamt = Convert.ToDouble(totlcst + tltcommncost).ToString("#,##0.0000;(#,##0.0000);");
            string exchnga = curinfo.Select(x => x.exratea).SingleOrDefault().ToString();
            string exchngb = curinfo.Select(a => a.exrateb).SingleOrDefault().ToString();
            string conrate = curinfo.Select(a => a.conrate).SingleOrDefault().ToString();
            string curdesc = curinfo.Select(a => a.codedesc).SingleOrDefault().ToString();
            string offerPrice = curinfo.Select(a => a.codedesc).SingleOrDefault().ToString();
            //string curdesc = curinfo.Select(a => a.codedesc).SingleOrDefault().ToString();
            //string curdesc = curinfo.Select(a => a.codedesc).SingleOrDefault().ToString();
            //var lstother = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);
            //string samptype = lstother[0].samtypedesc;
            //string forma = lstother[0].lformadesc;
            //season = lstother[0].season;
            LocalReport rpt1 = new LocalReport();
            if (Type == "PGEntry")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptConsumptionSheetFB", lst, samsize, null);

                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("artno", artno));
                rpt1.SetParameters(new ReportParameter("color", color));
                rpt1.SetParameters(new ReportParameter("range", range));
                rpt1.SetParameters(new ReportParameter("size", size));
                rpt1.SetParameters(new ReportParameter("qty", qty));
                rpt1.SetParameters(new ReportParameter("date", date));
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
                rpt1.SetParameters(new ReportParameter("costrang", costrang));
                rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
                rpt1.SetParameters(new ReportParameter("technician", technician));
                rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
                rpt1.SetParameters(new ReportParameter("style", style));
                rpt1.SetParameters(new ReportParameter("brand", brand));
                rpt1.SetParameters(new ReportParameter("samqty", samqty));
                


                rpt1.SetParameters(new ReportParameter("RptTitle", "PD Guide"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                rpt1.SetParameters(new ReportParameter("notes", ds4.Tables[1].Rows[0]["notes"].ToString()));

                rpt1.SetParameters(new ReportParameter("createdby", ds4.Tables[1].Rows[0]["createdby"].ToString()));
                rpt1.SetParameters(new ReportParameter("createdbydesig", ds4.Tables[1].Rows[0]["createdbydesig"].ToString()));
                rpt1.SetParameters(new ReportParameter("createdbydept", ds4.Tables[1].Rows[0]["createdbydept"].ToString()));

                rpt1.SetParameters(new ReportParameter("pdappby", ds4.Tables[1].Rows[0]["pdappby"].ToString()));
                rpt1.SetParameters(new ReportParameter("pdappbydesig", ds4.Tables[1].Rows[0]["pdappbydesig"].ToString()));
                rpt1.SetParameters(new ReportParameter("pdappbydept", ds4.Tables[1].Rows[0]["pdappbydept"].ToString()));

                rpt1.SetParameters(new ReportParameter("pdmdappby", ds4.Tables[1].Rows[0]["pdmdappby"].ToString()));
                rpt1.SetParameters(new ReportParameter("pdmdappbydesig", ds4.Tables[1].Rows[0]["pdmdappbydesig"].ToString()));
                rpt1.SetParameters(new ReportParameter("pdmdappbydept", ds4.Tables[1].Rows[0]["pdmdappbydept"].ToString()));

                rpt1.SetParameters(new ReportParameter("samptype", ds4.Tables[1].Rows[0]["samptypedesc"].ToString()));
                rpt1.SetParameters(new ReportParameter("pddate", Convert.ToDateTime( ds4.Tables[1].Rows[0]["posteddat"]).ToString("dd-MMM-yyyy")));
            }
            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));
            else if (Type == "PreCostPrint")
            {
                switch (comcod)
                {
                    case "5101":
                    case "5306":
                    case "5305":
                        rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptPreSampCostingSheetFB", lst, lstCost, curinfo);
                        rpt1.EnableExternalImages = true;
                        rpt1.SetParameters(new ReportParameter("PlAnalysis", (Convert.ToDouble(lst[0].offprice) - Convert.ToDouble(Convert.ToDouble(totalamt) * Convert.ToDouble(exchnga))).ToString("#,##0.00;(#,##0.00); ")));
                        rpt1.SetParameters(new ReportParameter("con2rate", (Convert.ToDouble(exchngb)).ToString("#,##0.0000;(#,##0.0000); ")));
                        rpt1.SetParameters(new ReportParameter("totalamt", (Convert.ToDouble(totalamt)).ToString("#,##0.0000;(#,##0.0000); ")));
                        rpt1.SetParameters(new ReportParameter("conrate", conrate));
                        rpt1.SetParameters(new ReportParameter("curdesc", curdesc));
                        rpt1.SetParameters(new ReportParameter("con1rate", (Convert.ToDouble(exchnga)).ToString("#,##0.0000;(#,##0.0000); ")));

                        rpt1.SetParameters(new ReportParameter("offerPrice", " "));
                        rpt1.SetParameters(new ReportParameter("pLAnalysis1", " "));
                        rpt1.SetParameters(new ReportParameter("cusTarget", " "));

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
                rpt1.SetParameters(new ReportParameter("date", date));
                rpt1.SetParameters(new ReportParameter("buyer", buyer));
                rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
                rpt1.SetParameters(new ReportParameter("catgory", catgory));
                rpt1.SetParameters(new ReportParameter("lstref", lstref));
                rpt1.SetParameters(new ReportParameter("construction", construction));
                rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
                rpt1.SetParameters(new ReportParameter("season", season));
                rpt1.SetParameters(new ReportParameter("brndname", brndname));
                rpt1.SetParameters(new ReportParameter("image", image));
                //rpt1.SetParameters(new ReportParameter("estdate", estdate));
                //rpt1.SetParameters(new ReportParameter("exrate", exrate));
                //rpt1.SetParameters(new ReportParameter("tarrate", tarrate));
                //rpt1.SetParameters(new ReportParameter("offer", offer));
                //rpt1.SetParameters(new ReportParameter("confirm", confirm));
                //rpt1.SetParameters(new ReportParameter("currency", currency));
                //rpt1.SetParameters(new ReportParameter("artamount", artamount));
                rpt1.SetParameters(new ReportParameter("season", season));

                rpt1.SetParameters(new ReportParameter("costrang", costrang));
                rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
                rpt1.SetParameters(new ReportParameter("technician", technician));
                rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
                rpt1.SetParameters(new ReportParameter("style", style));

                rpt1.SetParameters(new ReportParameter("offprice", lst[0].offprice.ToString("#,##0.00; (#,##0.00); ")));
                rpt1.SetParameters(new ReportParameter("tarprice", lst[0].tarprice.ToString("#,##0.00; (#,##0.00); ")));
                rpt1.SetParameters(new ReportParameter("cnfrmprice", Convert.ToDouble(lstCost.Where(x => x.sircode == "049800102999").Select(c => c.stdamt).FirstOrDefault()).ToString("#,##0.00; (#,##0.00); ")));
                rpt1.SetParameters(new ReportParameter("con1", (Convert.ToDouble(totalamt) * Convert.ToDouble(exchnga)).ToString("#,##0.0000;(#,##0.0000); ")));
                rpt1.SetParameters(new ReportParameter("con2", (Convert.ToDouble(totalamt) * Convert.ToDouble(exchngb)).ToString("#,##0.0000;(#,##0.0000); ")));
                rpt1.SetParameters(new ReportParameter("samptype", samptype));

                rpt1.SetParameters(new ReportParameter("forma", forma));
                rpt1.SetParameters(new ReportParameter("RptTitle", "CBD Sheet"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            }
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
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