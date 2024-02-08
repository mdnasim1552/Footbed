using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;

namespace SPEWEB.F_17_GFInv
{
    public partial class FGInvReport : System.Web.UI.Page
    {
        UserManagerSampling objUserMan = new UserManagerSampling();
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Final Accounts Reports View/Print Screen
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string type = this.Request.QueryString["InputType"].ToString();
                GetGenCode();
                this.GetBuyer();
                
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "General") ? "Inventory Report General" : (type == "Summary") ? "Receive And Shipment Summary Report" : "Inventory Report Location wise";
                if (type == "Location" || type== "Summary")
                {
                    this.LblLocation.Visible = true;
                    this.DDlLocation.Visible = true;
                    this.Label10.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    
                }
                else
                {
                    this.LblLocation.Visible = false;
                    this.DDlLocation.Visible = false;
                    this.Label10.Visible = true;
                    this.ddlRptGroup.Visible = true;
                    this.lockpro1.Visible = false;
                }
            }
        }
        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;
            //  List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>)Session["lstgencode"];

            var lstlocation = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "45");
            lstlocation.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            DDlLocation.DataTextField = "gdesc";
            DDlLocation.DataValueField = "gcod";
            DDlLocation.DataSource = lstlocation;
            DDlLocation.DataBind();
            DDlLocation.SelectedValue = "00000";
            
        }
        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");

            DataView dv21 = ds2.Tables[0].DefaultView;
            DataRowView newRow = dv21.AddNew();
            DataView dv22 = new DataView(ds2.Tables[0]);
            dv22.RowFilter = ("sircode not like '000000000000'");


            newRow = dv22.AddNew();
            newRow["sircode"] = "000000000000";
            newRow["sirdesc"] = "----All----";
            dv22.ToTable().Rows.Add(newRow);


            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = dv22;
            this.ddlBuyer.DataBind();
            this.ddlBuyer.SelectedValue = "000000000000";
            this.ddlBuyer_SelectedIndexChanged(null, null);
        }
        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLCCode();
        }
        private void GetLCCode()
        {

            string comcod = this.GetCompCode();
            string filter = "%";
            string Buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", filter, Buyer, "", "", "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tbllcorder"] = lst;
            



            DataView dv21 = ds2.Tables[0].DefaultView;
            DataRowView newRow = dv21.AddNew();
            DataView dv22 = new DataView(ds2.Tables[0]);
            dv22.RowFilter = ("mlccod not like '000000000000'");

            newRow = dv22.AddNew();
            newRow["mlccod"] = "000000000000";
            newRow["mlcdesc"] = "----All----";
            dv22.ToTable().Rows.Add(newRow);
            this.ddlmlccode.DataSource = dv22.ToTable(true, "mlccod", "mlcdesc"); ;
            this.ddlmlccode.DataTextField = "mlcdesc";
            this.ddlmlccode.DataValueField = "mlccod";
            this.ddlmlccode.DataBind();
            this.ddlmlccode.SelectedValue= "000000000000";
            ds2.Dispose();
            this.ddlmlccode_SelectedIndexChanged(null, null);
        }
        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tbllcorder"];
            string mlccode = this.ddlmlccode.SelectedValue.ToString();
            tbl1=(mlccode == "000000000000") ? tbl1 : tbl1.FindAll(x => x.mlccod == mlccode);
            tbl1.Add(new SPEENTITY.C_03_CostABgd.EclassSalesContact("", "", "", "", "", "", "", "", "", 0.00, Convert.ToDateTime("01-Jan-1900"), 0.00,0.00, "", "", "AAAAAAAA", "--All--","","",""));
            this.dllorderType.DataSource = tbl1;
            this.dllorderType.DataTextField = "rdaydesc";
            this.dllorderType.DataValueField = "rdayid";
            this.dllorderType.DataBind();
            this.dllorderType.SelectedValue = "AAAAAAAA";
            

        }
       
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetDataForProjectReport();
                    break;
                case "QuantityB":
                    //this.MultiView1.ActiveViewIndex = 1;
                    //this.GetInvQB();
                    break;
                case "AmountB":
                    //this.MultiView1.ActiveViewIndex = 2;
                    //this.GetAmtInvB();
                    break;
                case "Location":
                case "Summary":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetDataLocWise();
                    break;
            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetDataForProjectReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string Buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";


            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "Color" : (mRptGroup == "1" ? "Summary" :  "Details"));
            //GENARALINVRPT
            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            //ds2 = ProData.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "RPTORDPROVSSHIP", OrdrNo, date, "", "", "", "", "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "RPTFGSTOCK", Buyer, date1, date2, mRptGroup, "", "", "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            DataTable dt1 = ds2.Tables[0];
            this.HiddenSameDate(dt1);
            this.Data_Bind();

        }
        private void GetInvQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
                                      //string actcode = this.ddlAccProject.SelectedValue.ToString();

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, "", "", mRptGroup, "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetAmtInvB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
                                      // string actcode = this.ddlAccProject.SelectedValue.ToString();

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, "", "", mRptGroup, "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void GetDataLocWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.Request.QueryString["InputType"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string location = (this.DDlLocation.SelectedValue.ToString()=="00000")?"%":this.DDlLocation.SelectedValue.ToString()+"%";
            string Buyer = (this.ddlBuyer.SelectedValue.ToString()=="000000000000")?"%":this.ddlBuyer.SelectedValue.ToString()+"%";
            string mlccode = (this.ddlmlccode.SelectedValue.ToString()=="000000000000")?"%":this.ddlmlccode.SelectedValue.ToString()+"%";
            string orderType = (this.dllorderType.SelectedValue.ToString()== "AAAAAAAA") ?"%":this.dllorderType.SelectedValue.ToString()+"%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_FG_INV_02", "RPT_LOCATION_WISE_STOCK", location, date1, date2, Buyer, mlccode, orderType, Type, "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            DataTable dt1 = ds2.Tables[0];
            this.HiddenSameDate(dt1);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":
                    this.gvCenStore.Columns[18].Visible = false;

                    string rptindex = this.ddlRptGroup.SelectedIndex.ToString();
                    if (rptindex == "0")
                    {
                        this.gvCenStore.Columns[6].Visible = false;
                    }
                    else if (rptindex == "1")
                    {
                        this.gvCenStore.Columns[6].Visible = false;
                        this.gvCenStore.Columns[5].Visible = false;
                        this.gvCenStore.Columns[4].Visible = false;
                        this.gvCenStore.Columns[3].Visible = false;

                    }
                    else if (rptindex == "2")
                    {
                        this.gvCenStore.Columns[6].Visible = true;
                        this.gvCenStore.Columns[5].Visible = true;
                        this.gvCenStore.Columns[4].Visible = true;
                        this.gvCenStore.Columns[3].Visible = true;

                    }

                    this.gvCenStore.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCenStore.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvCenStore.DataBind();
                    break;
                case "Location":
                    this.gvCenStore.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCenStore.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvCenStore.DataBind();
                    break;
                case "Summary":
                    this.gvCenStore.Columns[5].Visible = false;
                    this.gvCenStore.Columns[4].Visible = false;
                    this.gvCenStore.Columns[6].Visible = false;
                    this.gvCenStore.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCenStore.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvCenStore.DataBind();
                    break;
                case "QuantityB":
                    this.gvQBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvQBasis.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvQBasis.DataBind();
                    break;
                case "AmountB":
                    this.gvAmtBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAmtBasis.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvAmtBasis.DataBind();
                    break;
            }

            this.FooterCalculation((DataTable)Session["tblVeiw"]);
        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["mlccod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mlccod"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["mlccod"].ToString();
                }

            }
            return dt1;

            Session["tblVeiw"] = dt1;
        }


        protected void ddlAccProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetMaterial();
        }



        protected void GetProjectName()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            ////string comcod = GetComCode();
            //string txtsrch = this.txtSearch.Text.Trim() + "%";
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "GETORDERNO", txtsrch, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //this.ddlAccProject.DataTextField = "actdesc";
            //this.ddlAccProject.DataValueField = "actcode";
            //this.ddlAccProject.DataSource = ds1.Tables[0];
            //this.ddlAccProject.DataBind();

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string HeaderCode = "41%";
            //string filter = this.txtSearch.Text.Trim() + "%";

            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            //DataTable dt1 = ds1.Tables[0];
            //this.ddlAccProject.DataSource = dt1;
            //this.ddlAccProject.DataTextField = "actdesc1";
            //this.ddlAccProject.DataValueField = "actcode";
            //this.ddlAccProject.DataBind();

        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":


                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFOpQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opproqty)", "")) ?
                        0.00 : dt.Compute("Sum(opproqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFProdeQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(proqty)", "")) ?
                        0.00 : dt.Compute("Sum(proqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFShipQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipqty)", "")) ?
                        0.00 : dt.Compute("Sum(shipqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFStocQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stockqty)", "")) ?
                        0.00 : dt.Compute("Sum(stockqty)", ""))).ToString("#,##0;(#,##0);  ");


                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvFOpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opproamt)", "")) ?
                        0.00 : dt.Compute("Sum(opproamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvFProdeAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(proamt)", "")) ?
                        0.00 : dt.Compute("Sum(proamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvFShipAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipamt)", "")) ?
                        0.00 : dt.Compute("Sum(shipamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvFStockAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stockamt)", "")) ?
                        0.00 : dt.Compute("Sum(stockamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");

                    break;
                case "QuantityB":

                    break;
                case "AmountB":
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFOpnAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ?
                        0.00 : dt.Compute("Sum(recam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFtrnsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                        0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFIssAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisamt)", "")) ?
                        0.00 : dt.Compute("Sum(matisamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFStkAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stcamt)", "")) ?
                        0.00 : dt.Compute("Sum(stcamt)", ""))).ToString("#,##0;(#,##0);  ");
                    //this.gvAmtBasis.DataBind();
                    break;
                case "Location":
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "grp='A'";
                    dt = dv.ToTable();
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFOpQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opproqty)", "")) ?
                       0.00 : dt.Compute("Sum(opproqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFProdeQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(proqty)", "")) ?
                        0.00 : dt.Compute("Sum(proqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFShipQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipqty)", "")) ?
                        0.00 : dt.Compute("Sum(shipqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvABFStocQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stockqty)", "")) ?
                        0.00 : dt.Compute("Sum(stockqty)", ""))).ToString("#,##0;(#,##0);  ");
                     ((Label)this.gvCenStore.FooterRow.FindControl("lblgvFStockAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stockamt)", "")) ?
                       0.00 : dt.Compute("Sum(stockamt)", ""))).ToString("#,##0;(#,##0);  ");
                    //this.gvAmtBasis.DataBind();
                    break;
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":
                    this.rptCentralStock();
                    break;
                case "QuantityB":
                    this.rptCentralStockQB();
                    break;
                case "AmountB":
                    this.rptCentralStockAB();
                    break;
                case "Location":
                case "Summary":
                    this.rptLocation();
                    break;
                    
            }

        }


        protected void rptCentralStock()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //   string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //   string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //  string ToFrDate = "(From :" + fromdate + " To " + todate + ")";

            DataTable dt = (DataTable)Session["tblVeiw"];

            var lst = dt.DataTableToList<SPEENTITY.C_17_GFInv.FGCenterStorec>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_17_GFInv.RptFGCentralStore", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("ToFrDate", "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )"));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "FG Central Store"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            //  rpt1.SetParameters(new ReportParameter("ProjectName", "Priject Name: " + this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13)));
            rpt1.SetParameters(new ReportParameter("Level", "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim()));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            // rpt1.SetParameters(new ReportParameter("todate", DateTime.Today.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        protected void rptCentralStockOld()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_17_FGInv.rptFGInvReport();//  new RMGiRPT.R_11_RawInv.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //txtlevel.Text = comnam;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void rptCentralStockQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvQtyBasis();//.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            ////TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            ////txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void rptCentralStockAB()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvAmtBasis();//.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            ////TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            ////txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void rptLocation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
           
            string Type = this.Request.QueryString["InputType"].ToString();
            string RptTitle = "Inventory Report Location wise";

            DataTable dt = (DataTable)Session["tblVeiw"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("grp = 'A'");
            DataTable dt1 = dv.ToTable();
            var lst = dt1.DataTableToList<SPEENTITY.C_17_GFInv.FGCenterStorec>();

            LocalReport rpt1 = new LocalReport();
            if (Type == "Summary")
            {             
                RptTitle = "Receive And Shipment Summary Report";
                rpt1 = RptSetupClass.GetLocalReport("R_17_GFInv.RptShipmentSummary", lst, null, null);
            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("R_17_GFInv.RptLocation", lst, null, null);
            }
            
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("ToFrDate", "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )"));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", RptTitle));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            //  rpt1.SetParameters(new ReportParameter("ProjectName", "Priject Name: " + this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13)));
            rpt1.SetParameters(new ReportParameter("Level", "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim()));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            // rpt1.SetParameters(new ReportParameter("todate", DateTime.Today.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ImgbtnFindProj_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }


        //protected void gvCenStore_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvCenStore.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvQBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvQBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvAmtBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAmtBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

       
    }
}