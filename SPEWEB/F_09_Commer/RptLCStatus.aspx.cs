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
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_09_Commer
{
    public partial class RptLCStatus : System.Web.UI.Page
    {
        ProcessAccess LcData = new ProcessAccess();
        Common _common = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "LCCosting") ? "LC COSTING REPORT"
                    : (type == "LCRecvCon") ? "L/C Received Consignment Wise"  : (type == "LCCostingPreset") ? "Lc Proposed Costing":"L/C Received Variance Reports";
                this.SelectView();

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(LbtnUpdateProposedCosting_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LCCosting":
                    this.GetLCCode();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "LCVari":
                    this.GetLCCode();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "LCRecvCon":
                    this.GetLCCode();
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "LCCostingPreset":
                    this.GetLCCode();
                    if (this.Request.QueryString["actcode"].ToString().Length > 0)
                    {
                        this.ddlLCNumber.SelectedValue = this.Request.QueryString["actcode"].ToString();
                        this.lbtnOk_Click(null,null);
                    }
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

            }
        }

        private void GetLCCode()
        {
            UserManagerLCM objUser = new UserManagerLCM();
            string comcod = this.GetCompCode();
            string LCSch = "%%";

            List<SPEENTITY.C_09_Commer.EClassLCCode> lst3 = new List<SPEENTITY.C_09_Commer.EClassLCCode>();
            lst3 = objUser.GetLCCode("SP_REPORTS_LC_INFO", "GETLCOCDE", LCSch);

            this.ddlLCNumber.DataTextField = "actdesc";

            this.ddlLCNumber.DataValueField = "actcode";
            this.ddlLCNumber.DataSource = lst3;
            this.ddlLCNumber.DataBind();
        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "LCCosting":
                    this.lblProduct.Visible = true;
                    this.lblCost.Visible = true;
                    this.lblPrice.Visible = true;
                    this.lblAcPrice.Visible = true;
                    this.lblLcInfo.Visible = true;
                    this.lblDate.Visible = true;

                    this.ShowLCCosting();
                    break;
                case "LCVari":
                    this.lblStat1.Visible = true;
                    this.lblStat2.Visible = true;
                    this.lblRec.Visible = true;
                    this.lblconinfo.Visible = true;
                    this.lblStatConsig1.Visible = false;
                    this.lblStat2consig.Visible = false;
                    this.ShowLCVari();
                    break;
                case "LCRecvCon":
                    this.lblStat1.Visible = true;
                    this.lblStat2.Visible = true;
                    this.lblRec.Visible = true;
                    this.lblconinfo.Visible = true;
                    this.lblStatConsig1.Visible = true;
                    this.lblStat2consig.Visible = true;
                    this.ShowLCVari();
                    break;
                case "LCCostingPreset":                  
                    this.ShowLCProposedCosting();
                    break;
            }
        }

        private void ShowLCProposedCosting()
        {
            UserManagerLCM objUser = new UserManagerLCM();
            Session.Remove("tbLCCosting");
            string comcod = this.GetCompCode();

            string LCnumber = this.ddlLCNumber.SelectedValue.ToString();
            string Label = this.ddlRptGroup.SelectedValue.ToString();

            DataSet ds1 = LcData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_LC_COST_HEAD_FOR_PROPOSED_COSTING", LCnumber, "%", "%", "", "", "", "", "", "");
            List<SPEENTITY.C_09_Commer.EClassLCCosting> lst3 = ASITUtility03.DataTableToList<SPEENTITY.C_09_Commer.EClassLCCosting>(ds1.Tables[0]);
         
            Session["tbLCCosting"] = (lst3.FindAll(x=>x.grp=="B").ToList());

            this.Data_Bind();
        }

        private void ShowLCCosting()
        {
            UserManagerLCM objUser = new UserManagerLCM();
            Session.Remove("tbLCCosting");
            string comcod = this.GetCompCode();

            string LCnumber = this.ddlLCNumber.SelectedValue.ToString();
            string Label = this.ddlRptGroup.SelectedValue.ToString();

            List<SPEENTITY.C_09_Commer.EClassLCCosting> lst3 = new List<SPEENTITY.C_09_Commer.EClassLCCosting>();


            lst3 = objUser.ShowLCCosting("SP_REPORTS_LC_INFO", "SHOWLCCOSTING", LCnumber, Label);
            Session["tbLCCosting"] = (lst3);

            this.Data_Bind();
        }

        private void ShowLCVari()
        {
            UserManagerLCM objUser = new UserManagerLCM();
            ViewState.Remove("tbLCVari");
            string comcod = this.GetCompCode();

            string LCnumber = this.ddlLCNumber.SelectedValue.ToString();
            string Label = this.ddlRptGroup.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            DataSet ds1 = LcData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "SHOWLCVARIENCE", LCnumber, Label, type);
            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.EClassLCVari>();
            var list1 = ds1.Tables[1].DataTableToList<SPEENTITY.C_09_Commer.EClassLCRcvCons>();

            ViewState["tbLCVari"] = (list);
            ViewState["tbLCRcvCon"] = (list1);
            ViewState["tbLCInfo"] = ds1.Tables[2];
            if (type == "LCRecvCon")
            {
                ViewState["tbLconsignment"] = ds1.Tables[3];
                ViewState["tbLconsignmenthead"] = ds1.Tables[4];
            }
            this.Data_Bind();
        }

        private List<SPEENTITY.C_09_Commer.EClassLCCosting> HiddenSameData(List<SPEENTITY.C_09_Commer.EClassLCCosting> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string rescode = "";
            foreach (SPEENTITY.C_09_Commer.EClassLCCosting c1 in lst3)
            {
                if (i == 0)
                {
                    rescode = c1.rescode;
                    i++;
                    continue;

                }
                else if (c1.rescode == rescode)
                {
                    c1.resdesc = "";
                }
                rescode = c1.rescode;

            }

            return lst3;

        }
        private List<SPEENTITY.C_09_Commer.EClassLCRcvCons> HiddenSameData1(List<SPEENTITY.C_09_Commer.EClassLCRcvCons> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string rstatus = "";
            foreach (SPEENTITY.C_09_Commer.EClassLCRcvCons c1 in lst3)
            {
                if (i == 0)
                {
                    rstatus = c1.rstatus;
                    i++;
                    continue;

                }
                else if (c1.rstatus == rstatus)
                {
                    c1.rstatus1 = "";
                    c1.stordesc = "";
                    c1.grrno1 = "";
                    c1.rcvdate = "";
                    c1.vounum = "";

                }
                rstatus = c1.rstatus;

            }

            return lst3;

        }


        private void Data_Bind()
        {
            try
            {


                string type = this.Request.QueryString["Type"].ToString().Trim();

                switch (type)
                {


                    case "LCCosting":

                        List<SPEENTITY.C_09_Commer.EClassLCCosting> lst3 = (List<SPEENTITY.C_09_Commer.EClassLCCosting>)Session["tbLCCosting"];
                        this.grvProduct.DataSource = lst3.FindAll(p => p.grp == "A");
                        this.grvProduct.DataBind();

                        this.grvLCCost.DataSource = lst3.FindAll(p => p.grp == "B");
                        this.grvLCCost.DataBind();

                        this.gvProPrice.DataSource = lst3.FindAll(p => p.grp == "C");
                        this.gvProPrice.DataBind();

                        this.gvActPrice.DataSource = lst3.FindAll(p => p.grp == "E");
                        this.gvActPrice.DataBind();

                        List<SPEENTITY.C_09_Commer.EClassLCCosting> lst4 = lst3.FindAll(p => p.grp == "D");


                        this.lblLcInfo.Text = "Currency: <span class='text-success'>" + lst4[0].currency.ToString() + "</span> , Rate: <span class='text-success'>" + lst4[0].conrate.ToString() + "</span> ";

                        this.lblDate.Text = "Bank: <span class='text-success'>" + lst4[0].bankname.ToString() + "</span>, LC Opening Date: <span class='text-success'>" + lst4[0].lcdate.ToString()+"</span>" +
                            ", LC Expired Date: <span class='text-success'>" + lst4[0].expdate.ToString()+ "</span>, Supplier Name: <span class='text-success'> " + lst4[0].supname.ToString()+"</span>"; 




                        break;

                    case "LCVari":
                        List<SPEENTITY.C_09_Commer.EClassLCVari> lst1 = (List<SPEENTITY.C_09_Commer.EClassLCVari>)ViewState["tbLCVari"];
                        this.gvLcVari.DataSource = lst1;
                        this.gvLcVari.DataBind();


                        List<SPEENTITY.C_09_Commer.EClassLCRcvCons> lst2 = (List<SPEENTITY.C_09_Commer.EClassLCRcvCons>)ViewState["tbLCRcvCon"];
                        this.gvRcvCons.DataSource = HiddenSameData1(lst2);
                        this.gvRcvCons.DataBind();


                        DataTable dt = (DataTable)ViewState["tbLCInfo"];
                        this.lblStat1.Text = "Currency: <span class='text-info'>" + dt.Rows[0]["currency"].ToString() + "</span> , Rate: <span class='text-info'>" + dt.Rows[0]["cnvrsion"].ToString() + "</span> ";

                        this.lblStat2.Text = "Bank: <span class='text-info'>" + dt.Rows[0]["bankname"].ToString() + "</span>, LC Opening Date: <span class='text-info'>" +
                                Convert.ToDateTime(dt.Rows[0]["lcdate"]).ToString("dd-MMM-yyyy")+"</span>"+
                                 ", LC Expired Date:<span class='text-info'>" + Convert.ToDateTime(dt.Rows[0]["expdate"]).ToString("dd-MMM-yyyy")+
                                 "</span>, Supplier Name:<span class='text-info'>"+ dt.Rows[0]["ssirdesc"].ToString();
                        break;
                    case "LCRecvCon":
                        List<SPEENTITY.C_09_Commer.EClassLCVari> lstrecv = (List<SPEENTITY.C_09_Commer.EClassLCVari>)ViewState["tbLCVari"];

                        DataTable dtconsign = (DataTable)ViewState["tbLconsignment"];
                        DataTable dtconsignhead = (DataTable)ViewState["tbLconsignmenthead"];

                        for (int i = 0; i < dtconsignhead.Rows.Count; i++)
                        {

                            string ColHeadDesc = Convert.ToString(i + 1) + "<br>Consignment <br>" +
                                Convert.ToDateTime(dtconsignhead.Rows[i]["heading"]).ToString("dd-MMM-yyyy") + "<br>" +
                                dtconsignhead.Rows[i]["stordesc"];
                            this.gvConsignment.Columns[i + 4].HeaderText = ColHeadDesc;
                            this.gvConsignment.Columns[i + 4].HeaderStyle.Font.Size = 8;
                            this.gvConsignment.Columns[i + 4].Visible = true;



                        }
                        this.gvLcRecieveSum.DataSource = lstrecv;
                        this.gvLcRecieveSum.DataBind();

                        this.gvConsignment.DataSource = dtconsign;
                        this.gvConsignment.DataBind();

                        ((Label)this.gvConsignment.FooterRow.FindControl("gvconFRcvqty")).Text = Convert.ToDouble((Convert.IsDBNull(dtconsign.Compute("sum(rcvqty)", "")) ?
                                   0 : dtconsign.Compute("sum(rcvqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvConsignment.FooterRow.FindControl("gvconFCon1")).Text = Convert.ToDouble((Convert.IsDBNull(dtconsign.Compute("sum(cons1)", "")) ?
                                   0 : dtconsign.Compute("sum(cons1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvConsignment.FooterRow.FindControl("gvconFCon2")).Text = Convert.ToDouble((Convert.IsDBNull(dtconsign.Compute("sum(cons2)", "")) ?
                                   0 : dtconsign.Compute("sum(cons2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvConsignment.FooterRow.FindControl("gvconFCon3")).Text = Convert.ToDouble((Convert.IsDBNull(dtconsign.Compute("sum(cons3)", "")) ?
                                   0 : dtconsign.Compute("sum(cons3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvConsignment.FooterRow.FindControl("gvconFCon4")).Text = Convert.ToDouble((Convert.IsDBNull(dtconsign.Compute("sum(cons4)", "")) ?
                                   0 : dtconsign.Compute("sum(cons4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvConsignment.FooterRow.FindControl("gvconFCon5")).Text = Convert.ToDouble((Convert.IsDBNull(dtconsign.Compute("sum(cons5)", "")) ?
                                   0 : dtconsign.Compute("sum(cons5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvConsignment.FooterRow.FindControl("gvconFCon6")).Text = Convert.ToDouble((Convert.IsDBNull(dtconsign.Compute("sum(cons6)", "")) ?
                                   0 : dtconsign.Compute("sum(cons6)", ""))).ToString("#,##0;(#,##0); ");

                        DataTable dtlcinfo =(DataTable) ViewState["tbLCInfo"];
                        this.lblStatConsig1.Text = "Currency: <span class='text-info'>" + dtlcinfo.Rows[0]["currency"].ToString() + "</span> , Rate: <span class='text-info'>" + dtlcinfo.Rows[0]["cnvrsion"].ToString() + "</span> ";

                        this.lblStat2consig.Text = "Bank: <span class='text-info'>" + dtlcinfo.Rows[0]["bankname"].ToString() + "</span>, LC Opening Date: <span class='text-info'>" +
                                Convert.ToDateTime(dtlcinfo.Rows[0]["lcdate"]).ToString("dd-MMM-yyyy") + "</span>" +
                                 ", LC Expired Date:<span class='text-info'>" + Convert.ToDateTime(dtlcinfo.Rows[0]["expdate"]).ToString("dd-MMM-yyyy") +
                                 "</span>, Supplier Name:<span class='text-info'>" + dtlcinfo.Rows[0]["ssirdesc"].ToString();

                        break;

                    case "LCCostingPreset":

                        List<SPEENTITY.C_09_Commer.EClassLCCosting> lstprecosting = (List<SPEENTITY.C_09_Commer.EClassLCCosting>)Session["tbLCCosting"];
                      
                        this.gvlcProCosting.DataSource = lstprecosting.FindAll(p => p.grp == "B");
                        this.gvlcProCosting.DataBind();

                        break;
                }







            }
            catch (Exception ex)
            {


            }


        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            switch (type)
            {
                case "LCCosting":
                    switch (comcod)
                    {
                        case "5301":
                            this.rptlccosting();
                            break;

                        default:
                            this.PrintLCStatus();
                            break;

                    }

                    break;
                case "LCVari":
                    this.PrintLCSVarianc();
                    break;
            }
        }


        private void rptlccosting()

        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string LCnumber = this.ddlLCNumber.SelectedItem.ToString().Substring(13);

            List<SPEENTITY.C_09_Commer.EClassLCCosting> lst3 = (List<SPEENTITY.C_09_Commer.EClassLCCosting>)Session["tbLCCosting"];

            List<SPEENTITY.C_09_Commer.EClassLCCosting> lst6 = lst3.FindAll(p => p.grp == "D");


            string LcDate = lst6[0].lcdate;
            string currency = lst6[0].currency;
            string rate = Convert.ToDouble(lst6[0].conrate).ToString();
            string bank = lst6[0].bankname;
            string supname = lst6[0].supname;


            var filterlist = lst3.FindAll(p => p.grp != "D" & p.tparcent != 0);

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptLCCost", filterlist, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("LcDate", "Date:" + LcDate));
            rpt1.SetParameters(new ReportParameter("currency", "Currency:" + currency));
            rpt1.SetParameters(new ReportParameter("rate", "Rate:" + rate));
            rpt1.SetParameters(new ReportParameter("bank", "Bank:" + bank));
            rpt1.SetParameters(new ReportParameter("RptTitel", "Landed Cost Sheet"));
            rpt1.SetParameters(new ReportParameter("LCnumber", "LC Number: " + LCnumber));
            rpt1.SetParameters(new ReportParameter("supName", "Supplier: " + supname));
            rpt1.SetParameters(new ReportParameter("prepared", "Prepared By"));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintLCStatus()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //List<SPEENTITY.C_09_Commer.EClassLCCosting> lst3 = (List<SPEENTITY.C_09_Commer.EClassLCCosting>)Session["tbLCCosting"];

            //List<SPEENTITY.C_09_Commer.EClassLCCosting> lst4 = lst3.FindAll(p => p.bdamt != 0.00);

            //if (lst.Count == 0)
            //    return;

            //ReportDocument rptstk = new RMGiRPT.R_09_Commer.rptBBLCPayStatus();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = "LC Costing Report";

            //TextObject txtDet1 = rptstk.ReportDefinition.ReportObjects["txtDet1"] as TextObject;
            //txtDet1.Text = this.lblLcInfo.Text;

            //TextObject txtDet2 = rptstk.ReportDefinition.ReportObjects["txtDet2"] as TextObject;
            //txtDet2.Text = this.lblDate.Text;

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtAccHead"] as TextObject;
            //txtdate.Text = "LC Name " + this.ddlLCNumber.SelectedItem.ToString().Substring(14);

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(lst4);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = _common.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string LCnumber = this.ddlLCNumber.SelectedItem.ToString().Substring(13);


            List<SPEENTITY.C_09_Commer.EClassLCCosting> lst = (List<SPEENTITY.C_09_Commer.EClassLCCosting>)Session["tbLCCosting"];
            
            var lstProduct = lst.FindAll(p => p.grp == "A" && !string.IsNullOrWhiteSpace(p.unit));
            var lst4 = lst.FindAll(p => p.grp == "D");

            string lcinfo = "Currency: " + lst4[0].currency.ToString() + " , Rate: " + lst4[0].conrate.ToString() + " " + "Bank: " + lst4[0].bankname.ToString() + ", LC Opening Date: " + lst4[0].lcdate.ToString() + ", LC Expired Date:" + lst4[0].expdate.ToString();

            LocalReport Rpt1 = new LocalReport();
            //Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_09_Commer.RptLcCosting", lstProduct, lstLcCost, lstProPrice);
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_09_Commer.RptLcCosting", lstProduct, lst, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comlogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "LC Costing Report"));
            Rpt1.SetParameters(new ReportParameter("LCnumber", "LC Number: " + LCnumber));
            Rpt1.SetParameters(new ReportParameter("lcinfo", lcinfo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintLCSVarianc()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();  //company name
            //string comadd = hst["comadd1"].ToString();  //address
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)ViewState["tbLCInfo"];

            //string lblStat1 = this.lblStat1.Text = "Currency: " + dt.Rows[0]["currency"].ToString() + " , Rate: " + dt.Rows[0]["cnvrsion"].ToString() + " ";
            //string lblStat2 = this.lblStat2.Text = "Bank: " + dt.Rows[0]["bankname"].ToString() + ", LC Opening Date: " + dt.Rows[0]["lcdate"].ToString()
            //        + ", LC Expired Date:" + dt.Rows[0]["expdate"].ToString();




            //List<SPEENTITY.C_09_Commer.EClassLCVari> lst1 = (List<SPEENTITY.C_09_Commer.EClassLCVari>)ViewState["tbLCVari"];
            //List<SPEENTITY.C_09_Commer.EClassLCRcvCons> lst2 = (List<SPEENTITY.C_09_Commer.EClassLCRcvCons>)ViewState["tbLCRcvCon"];

            //LocalReport rpt1 = new LocalReport();

            //rpt1 = RMGiRDLC.RptSetupClass1.GetLocalReport("RD_09_LCM.RptLcVarianc", lst1, lst2, null);



            //rpt1.SetParameters(new ReportParameter("compnam", comnam));
            //rpt1.SetParameters(new ReportParameter("rtpTitle", "L/C Received Variance Reports"));
            //rpt1.SetParameters(new ReportParameter("lblStat1", lblStat1));
            //rpt1.SetParameters(new ReportParameter("lblStat2", lblStat2));
            //rpt1.SetParameters(new ReportParameter("Received", "Received Info:"));
            //rpt1.SetParameters(new ReportParameter("Consignment", "Consignment Info:"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //Session["Report1"] = rpt1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + 
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }






        protected void grvLCCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label mdesc = (Label)e.Row.FindControl("lgvLDesc");
                Label LCbdamt = (Label)e.Row.FindControl("lgvbdamt");
                Label LCPar = (Label)e.Row.FindControl("lgvtPar");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {
                    mdesc.Font.Bold = true;
                    LCbdamt.Font.Bold = true;
                    LCPar.Font.Bold = true;

                    mdesc.Style.Add("text-align", "right");
                    mdesc.Style.Add("color", "red");
                    LCbdamt.Style.Add("color", "red");
                    LCPar.Style.Add("color", "red");
                }
                if (ASTUtility.Right(code, 3) == "000")
                {
                    mdesc.Font.Bold = true;
                    LCbdamt.Font.Bold = true;
                    LCPar.Font.Bold = true;

                    LCbdamt.Style.Add("text-align", "left");
                    LCPar.Style.Add("text-align", "left");
                    mdesc.Style.Add("color", "blue");
                    LCbdamt.Style.Add("color", "blue");
                    LCPar.Style.Add("color", "blue");
                }


            }
        }
        //protected void ImgbtnFindpLC_Click(object sender, EventArgs e)
        //{
        //    //this.GetLCCode();
        //}
        protected void grvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label mdesc = (Label)e.Row.FindControl("lgvPRes");
                Label Ptqty = (Label)e.Row.FindControl("lgvPtqty");
                Label PRate = (Label)e.Row.FindControl("lgvPRate");
                Label PAmt = (Label)e.Row.FindControl("lgvPAmt");
                Label PbdAmt = (Label)e.Row.FindControl("lgvPbdamt");
                Label PPer = (Label)e.Row.FindControl("lgvPPer");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    mdesc.Font.Bold = true;
                    Ptqty.Font.Bold = true;
                    PRate.Font.Bold = true;
                    PAmt.Font.Bold = true;
                    PbdAmt.Font.Bold = true;
                    PPer.Font.Bold = true;

                    mdesc.Style.Add("text-align", "right");
                    mdesc.Style.Add("color", "red");
                    Ptqty.Style.Add("color", "red");
                    PAmt.Style.Add("color", "red");
                    PRate.Style.Add("color", "red");
                    PbdAmt.Style.Add("color", "red");
                    PPer.Style.Add("color", "red");
                }


            }
        }
        protected void gvProPrice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "COST PER UNIT";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 6;



                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvProPrice.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvRcvCons_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lgvvounum = (HyperLink)e.Row.FindControl("lgvvounum");

                string comcod = this.GetCompCode();
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                if (vounum == "")
                {
                    return;
                }
                if (vounum.Length == 14)
                {
                    //mdesc.Font.Bold = true;
                    //Ptqty.Font.Bold = true;
                    //PRate.Font.Bold = true;
                    //PAmt.Font.Bold = true;
                    //PbdAmt.Font.Bold = true;
                    //PPer.Font.Bold = true;

                    //mdesc.Style.Add("text-align", "right");
                    //mdesc.Style.Add("color", "red");
                    //Ptqty.Style.Add("color", "red");
                    //PAmt.Style.Add("color", "red");
                    //PRate.Style.Add("color", "red");
                    //PbdAmt.Style.Add("color", "red");
                    //PPer.Style.Add("color", "red");

                    lgvvounum.NavigateUrl = "~/F_21_GAcc/Print?Type=accVou&vounum=" + vounum + "&comcod=" + comcod;
                }


            }
        }

        protected void gvlcProCosting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label mdesc = (Label)e.Row.FindControl("lgvLDesc");
                TextBox LCbdamt = (TextBox)e.Row.FindControl("lgvbdamt");
                Label LCPar = (Label)e.Row.FindControl("lgvtPar");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {
                    mdesc.Font.Bold = true;
                    LCbdamt.Font.Bold = true;
                    LCPar.Font.Bold = true;
                    LCbdamt.Enabled = false;
                    LCbdamt.CssClass = "bg-gray";
                    mdesc.Style.Add("text-align", "right");
                    mdesc.Style.Add("color", "red");
                    LCbdamt.Style.Add("color", "red");
                    LCPar.Style.Add("color", "red");
                }
                if (ASTUtility.Right(code, 3) == "000")
                {
                    mdesc.Font.Bold = true;
                    LCbdamt.Font.Bold = true;
                    LCPar.Font.Bold = true;
                    LCbdamt.Enabled = false;
                    LCbdamt.CssClass = "bg-gray";

                    LCbdamt.Style.Add("text-align", "left");
                    LCPar.Style.Add("text-align", "left");
                    mdesc.Style.Add("color", "blue");
                    LCbdamt.Style.Add("color", "blue");
                    LCPar.Style.Add("color", "blue");
                }


            }
        }
        private void Save_Value()
        {
            List<SPEENTITY.C_09_Commer.EClassLCCosting> lstprecosting = (List<SPEENTITY.C_09_Commer.EClassLCCosting>)Session["tbLCCosting"];


            int RowIndex = 0;

            for (int i = 0; i < this.gvlcProCosting.Rows.Count; i++)
            {
                double amount = Convert.ToDouble("0" + ((TextBox)this.gvlcProCosting.Rows[i].FindControl("lgvbdamt")).Text.Trim());
                RowIndex = this.gvlcProCosting.PageIndex * this.gvlcProCosting.PageSize + i;
                lstprecosting[RowIndex].bdamt = amount;
            }

            Session["tbLCCosting"] = lstprecosting;
        }

        protected void LbtnUpdateProposedCosting_Click(object sender, EventArgs e)
        {
            Save_Value();
            string comcod = this.GetCompCode();
            string actcode = this.ddlLCNumber.SelectedValue.ToString();

            List<SPEENTITY.C_09_Commer.EClassLCCosting> lstprecosting = (List<SPEENTITY.C_09_Commer.EClassLCCosting>)Session["tbLCCosting"];
            DataTable dt = ASITUtility03.ListToDataTable(lstprecosting);
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tbl1";
            var temp = ds.GetXml();
            bool result = LcData.UpdateXmlTransInfo(comcod, "SP_ENTRY_LC_INFO", "UPDATE_LC_PROPOSED_COSTING", ds, null, null, actcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }


        }
    }
}