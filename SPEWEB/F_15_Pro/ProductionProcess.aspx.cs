using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using SPERDLC;
using Microsoft.Reporting.WinForms;


namespace SPEWEB.F_15_Pro
{
    public partial class ProductionProcess : System.Web.UI.Page
    {
        ProcessAccess rptdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.GetOrder();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION PROCESS";

                this.CommonButton();
                if (this.Request.QueryString["Type"].ToString() == "ProProcess" || this.Request.QueryString["Type"].ToString() == "ProTransfer" || this.Request.QueryString["Type"].ToString() == "ProQc")
                {
                    this.ddlReqno.Visible = true;
                    //  ddlOrderNo_SelectedIndexChanged(null, null);
                }

                //UPDATED BY AREFIN 11/15/2023
                if(this.Request.QueryString["Type"].ToString() == "ProQc")
                {
                    this.TextBoxRejQty.Enabled = false;
                    this.TxtPassQty.Enabled = false;
                    this.TextBoxRepQty.Enabled = false;
                } 
            }
        }

        private void CommonButton()
        {

            if (this.Request.QueryString["Type"].ToString() == "ProTransfer")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            }

            if (this.Request.QueryString["Type"].ToString() == "ProQc")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Bulk QC Update";
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnProTrnsUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkbtnHisprice_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private void GetOrder()
        {
            string comcod = GetCompCode();
            string txtsrch = "%";
            if (this.Request.QueryString["actcode"].ToString().Length > 0)
            {
                txtsrch = this.Request.QueryString["actcode"].ToString() + "%";
            }
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETORDERNO", txtsrch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlOrderNo.DataTextField = "actdesc";
            this.ddlOrderNo.DataValueField = "actcode";
            this.ddlOrderNo.DataSource = ds1.Tables[0];
            this.ddlOrderNo.DataBind();
            if (this.Request.QueryString["actcode"].ToString().Length > 0)
            {
                this.ddlOrderNo.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlOrderNo_SelectedIndexChanged(null, null);
                this.lbtnOk_Click(null, null);
            }
            ds1.Dispose();

        }


        protected void imgbtnFindOrder_Click(object sender, EventArgs e)
        {
            this.GetOrder();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                CreateProcessList();
                this.lbtnOk.Text = "New";

                this.ddlOrderNo.Enabled = false;
                this.ShowProcess();
                string comcod = GetCompCode();


                return;
            }


            this.lbtnOk.Text = "Ok";
            ViewState.Remove("tblstpprocess");
            this.MultiView1.ActiveViewIndex = -1;
            this.gvStartPro.DataSource = null;
            this.gvStartPro.DataBind();
            this.gvTransPro.DataSource = null;
            this.gvTransPro.DataBind();
            this.ddlOrderNo.Enabled = true;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }


        private void ShowProcess()
        {
            string comcod = this.GetCompCode();
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ProStart":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowStProProcess();
                    break;

                case "ProTransfer":
                    this.MultiView1.ActiveViewIndex = 1;
                    string mlccod = this.ddlOrderNo.SelectedValue.ToString();

                    DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPREVIOUSPPNO", mlccod, "", "", "", "", "", "", "", "");
                    if (ds1 == null)
                        return;
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["NewProdNo"].ToString();

                    this.txttrnsDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.GetProcess();
                    this.GetFloorSetupInfo();
                    this.Get_StyleColor();
                    break;
                case "ProProcess":
                    this.MultiView1.ActiveViewIndex = 2;

                    this.ProProcess();
                    break;
                case "ProQc":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ProProcessQc();
                    break;
            }
        }

        private void GetFloorSetupInfo()
        {
            //
            string comcod = GetCompCode();
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPRODSETUPINFO", "", "", "", "", "", "", "", "", "");


            this.ddHour.DataTextField = "gdesc";
            this.ddHour.DataValueField = "gcod";
            this.ddHour.DataSource = ds1.Tables[0];
            this.ddHour.DataBind();

            string frmProcess = this.ddlFromProcess.SelectedValue;

            DataTable dt = new DataTable();
            DataView dv = ds1.Tables[1].AsDataView();

            dv.RowFilter = "prodprocess = " + frmProcess + "";
            dt = dv.ToTable();

            this.ddlLine.DataTextField = "sirdesc";
            this.ddlLine.DataValueField = "sircode";
            this.ddlLine.DataSource = dt;
            this.ddlLine.DataBind();
            this.ddlLine.Items.Add(new ListItem { Value = "000000000000", Text = "None", Selected = true });


            if (comcod == "5305")
                divMachine.Visible = false;

            this.ddlMachine.DataTextField = "sirdesc";
            this.ddlMachine.DataValueField = "sircode";
            this.ddlMachine.DataSource = ds1.Tables[2];
            this.ddlMachine.DataBind();

        }

        private void ShowStProProcess()
        {
            ViewState.Remove("tblstpprocess");
            string comcod = GetCompCode();
            string Orderno = this.ddlOrderNo.SelectedValue.ToString();
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "SHOWSTPROPROCESS", Orderno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvStartPro.DataSource = null;
                this.gvStartPro.DataBind();
                return;
            }
            //string Date = Convert.ToDateTime(ds1.Tables[1].Rows[0]["rdate"]).ToString("dd-MMM-yyyy");
            //this.txtDate.Text = (Date == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Date;
            ViewState["tblstpprocess"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void GetProcess()
        {
            Session.Remove("tblprocess");
            string comcod = GetCompCode();
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("procode <>'800100101099'");
            dt = dv.ToTable();

            this.ddlFromProcess.DataTextField = "prodesc";
            this.ddlFromProcess.DataValueField = "procode";
            this.ddlFromProcess.DataSource = dt;
            this.ddlFromProcess.DataBind();
            Session["tblprocess"] = ds1.Tables[0];
            ds1.Dispose();
            if (this.Request.QueryString["sircode"].Length > 0)
            {
                this.ddlFromProcess.SelectedValue = this.Request.QueryString["sircode"].ToString();
                this.ddlFromProcess.Enabled = false;
            }
            this.ddlFromProcess_SelectedIndexChanged(null, null);

        }
        private void ProProcess()
        {
            ViewState.Remove("tblstpprocess");
            string comcod = GetCompCode();
            string Orderno = this.ddlOrderNo.SelectedValue.ToString();
            string reqno = this.ddlReqno.SelectedValue.ToString();
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "RPTPROPROCESS", Orderno, reqno, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvproprocess.DataSource = null;
                this.gvproprocess.DataBind();
                return;

            }

            ViewState["tblstpprocess"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>(); //HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void ProProcessQc()
        {
            ViewState.Remove("tblstpprocess");
            string comcod = GetCompCode();
            string mlccod = this.ddlOrderNo.SelectedValue.ToString();
            string preqno = this.Request.QueryString["genno"].ToString();
            string sircode = this.Request.QueryString["sircode"].ToString();
            string date = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GET_PROCESSWISE_INFO_FORQC", mlccod, preqno, sircode, date, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvqc.DataSource = null;
                this.gvqc.DataBind();
                return;

            }

            ViewState["tblstpprocess"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>(); //HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        protected void ddlFromProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblprocess"];
            string Process = this.ddlFromProcess.SelectedValue.ToString().Trim();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "procode not in ('" + Process + "') and prodesc <> ''";
            this.ddlToProcess.DataTextField = "prodesc";
            this.ddlToProcess.DataValueField = "procode";
            this.ddlToProcess.DataSource = dv1.ToTable();
            this.ddlToProcess.DataBind();
            ddlToProcess_SelectedIndexChanged(null, null);
        }

        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            var list = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];
            switch (Type)
            {
                case "ProStart":
                    this.gvStartPro.DataSource = list;
                    this.gvStartPro.DataBind();
                    break;

                case "ProTransfer":
                    this.gvTransPro.DataSource = list;
                    this.gvTransPro.DataBind();
                    this.CalculateTotalFooter();
                    break;
                case "ProProcess":
                    this.gvproprocess.DataSource = list;
                    this.gvproprocess.DataBind();
                    break;
                case "ProQc":
                    this.gvqc.DataSource = list;
                    this.gvqc.DataBind();

                    if (list.Count != 0)
                    {
                        ((Label)this.gvqc.FooterRow.FindControl("lblgvTotalTransQty")).Text = list.Sum(x => x.trnsqty).ToString("#,##0.00;(#,##0.00); ");
                    }



                    break;
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            string STYLEID = dt1.Rows[0]["STYLEID"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }
                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                }
                //if (dt1.Rows[j]["STYLEID"].ToString() == STYLEID)
                //{
                //    STYLEID = dt1.Rows[j]["STYLEID"].ToString();
                //    dt1.Rows[j]["StyleDes"] = "";
                //}
                //else
                //{
                //    STYLEID = dt1.Rows[j]["STYLEID"].ToString();
                //}
            }
            return dt1;
        }
        private void SaveValue()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            int rowindex, i;
            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> list = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];

            switch (Type)
            {
                case "ProStart":
                    for (i = 0; i < this.gvStartPro.Rows.Count; i++)
                    {
                        double ProQty = Convert.ToDouble('0' + ((TextBox)this.gvStartPro.Rows[i].FindControl("txtgvproQty")).Text.Trim());
                        rowindex = (this.gvStartPro.PageSize * this.gvStartPro.PageIndex) + i;
                        list[rowindex].proqty = ProQty;
                    }
                    break;

                case "ProTransfer":
                    for (i = 0; i < this.gvTransPro.Rows.Count; i++)
                    {
                        double balqty = Convert.ToDouble(list[i].balqty);
                        double trnqty = Convert.ToDouble(list[i].trnsqty);
                        double TrnsQty = Convert.ToDouble('0' + ((TextBox)this.gvTransPro.Rows[i].FindControl("txtgvtrnsQty")).Text.Trim());

                        if (TrnsQty > balqty)
                        {
                            list[0].trnsqty = 0;
                            ViewState["tblstpprocess"] = list;
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry! You Not Eligible to Transfer to This Process. Pass Qty Must Equal or Less Then Balance Qty ');", true);
                            return;
                        }
                        //double rejectionqty = Convert.ToDouble('0' + ((TextBox)this.gvTransPro.Rows[i].FindControl("txtgvrejectionqty")).Text.Trim());
                        //double repairqty = Convert.ToDouble('0' + ((TextBox)this.gvTransPro.Rows[i].FindControl("txtgvtrepairqty")).Text.Trim());
                        rowindex = (this.gvTransPro.PageSize * this.gvTransPro.PageIndex) + i;
                        list[rowindex].trnsqty = TrnsQty;
                        //list[rowindex].rejectionqty = rejectionqty;
                        //list[rowindex].repairqty = repairqty;
                    }
                    break;
            }
            ViewState["tblstpprocess"] = list;
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ProStart":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;

                case "ProTransfer":
                    this.MultiView1.ActiveViewIndex = 1;

                    break;
                case "ProProcess":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.RptProProcess();
                    break;
            }
        }
        private void RptProProcess()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string OrderNo = this.ddlOrderNo.SelectedItem.Text.Substring(14).ToString();

            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];


            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptProdProcess", lst, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("OrderNo", OrderNo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Production Process"));

            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void OldRptProProcess()
        {

        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();

            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> list = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string Orderno = this.ddlOrderNo.SelectedValue.ToString();
            string hourData = this.ddHour.SelectedValue.ToString().Trim();
            string lineData = this.ddlLine.SelectedValue.ToString().Trim();
            string MachineData = this.ddlMachine.SelectedValue.ToString().Trim();
            string ManPowerData = "0" + this.txtManpower.Text.ToString().Trim();



            string fProcessStep = "000000000000";
            string tProcessStep = "800100101001";
            for (int i = 0; i < list.Count(); i++)
            {

                string styleid = list[i].styleid.ToString();
                double ordrqty = Convert.ToDouble(list[i].ordrqty);
                double Proqty = Convert.ToDouble(list[i].proqty);

                if (Proqty > 0)
                {
                    rptdata.UpdateTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "INORUPPROPROCESS", date, Orderno, fProcessStep, tProcessStep, styleid, Proqty.ToString(), "0.00", "0.00", hourData, lineData, MachineData, ManPowerData, "", "", "", "", "");
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }
        private void Get_StyleColor()
        {
            string comcod = GetCompCode();
            string Orderno = this.ddlOrderNo.SelectedValue.ToString();
            string preqno = this.ddlReqno.SelectedValue.ToString();
            string curprocess = this.Request.QueryString["sircode"].ToString() + "%";
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GET_STYLEWISE_COLOR_SIZE", Orderno, preqno, curprocess, "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt2 = ds1.Tables[0];
            ViewState["tblcolorsize"] = dt2;
            DataView dv = dt2.DefaultView;
            dt2 = dv.ToTable(true, "styledesc", "styleid");
            this.ddlStyle.DataTextField = "styledesc";
            this.ddlStyle.DataValueField = "styleid";
            this.ddlStyle.DataSource = dt2; // ds1.Tables[1];
            this.ddlStyle.DataBind();
            ddlStyle_SelectedIndexChanged(null, null);
        }
        private void CreateProcessList()
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> list = new List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>();
            ViewState["tblstpprocess"] = list;
        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            var List = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];
            string comcod = GetCompCode();
            string Orderno = this.ddlOrderNo.SelectedValue.ToString();
            string Process = this.ddlFromProcess.SelectedValue.ToString();
            string Processdesc = this.ddlFromProcess.SelectedItem.ToString();
            string preqno = this.ddlReqno.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString();
            string styledesc = this.ddlStyle.SelectedItem.ToString();
            string colorid = this.ddlcolor.SelectedValue.ToString();
            string colordesc = this.ddlcolor.SelectedItem.ToString();
            string sizeid = "";// this.ddlsize.SelectedValue.ToString();
            string sizedesc = "";// this.ddlsize.SelectedItem.ToString();
            double manpower = Convert.ToDouble("0" + this.txtManpower.Text.Trim());
            string floor = this.ddlLine.SelectedValue.ToString();
            string floordesc = this.ddlLine.SelectedItem.ToString() == "000000000000" ? "%%" : this.ddlLine.SelectedItem.ToString() + "%";
            string machine = this.ddlMachine.SelectedValue.ToString();
            string machdesc = this.ddlMachine.SelectedItem.ToString();
            string prodtime = this.ddHour.SelectedValue.ToString();
            string proddesc = this.ddHour.SelectedItem.ToString();
            string trdate = Convert.ToDateTime(this.txttrnsDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string toprocess = this.ddlToProcess.SelectedValue.ToString();
            string toprocessdesc = this.ddlToProcess.SelectedItem.ToString();
            string rdate = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
            foreach (ListItem item in ddlsize.Items)
            {
                if (item.Selected)
                {

                    sizeid = item.Value;
                    sizedesc = item.Text;
                    DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "SHOWTRANSPROCESS", Orderno, Process, preqno, styleid, colorid, sizeid, rdate, "", "");
                    if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid " + sizedesc + "'Or Process'" + toprocessdesc + "');", true);


                        continue;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    }
                    double recvqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["recvqty"]);
                    double trnsqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["trnqty"]);
                    double balqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["balqty"]);
                    double rejectionqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["rejectionqty"]);
                    double repairqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["repairqty"]);

                    string styleunit = ds1.Tables[0].Rows[0]["styleunit"].ToString();
                    var checklist = List.FindAll(p => p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.preqno == preqno); // && p.prodtime == prodtime
                    if (checklist.Count() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Added!!');", true);
                        continue;
                    }

                    List.Add(new SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess(comcod, "", "", "", "", "", styleid, styledesc, colorid, colordesc,
                        sizeid, sizedesc, "", recvqty, trdate, trnsqty, balqty, rejectionqty, manpower, repairqty, floor, machine, prodtime, styleunit,
                        floordesc, machdesc, proddesc, toprocess, toprocessdesc, Process, Processdesc, preqno));

                }
            }
            ViewState["tblstpprocess"] = List;
            this.Data_Bind();
            adjustment();
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            Data_Bind();
        }

        protected void lbtnProTrnsUpdate_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> list = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txttrnsDate.Text).ToString("dd-MMM-yyyy");
            string Orderno = this.ddlOrderNo.SelectedValue.ToString();

            string dprno = this.Request.QueryString["genno"].ToString();
            string process = this.Request.QueryString["sircode"].ToString();

            DataTable dt = new DataTable();
            dt.Columns.Add("drpno", Type.GetType("System.String"));
            var listdprno = dprno.Split('D');
            foreach (var item in listdprno)
            {
                dt.Rows.Add(item);

            }
            DataSet ds = new DataSet("ds1");
            ds.Merge(dt);
            ds.Tables[0].TableName = "tbldrpno";
            string xml = ds.GetXml();
            bool tdata = rptdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "CHECK_PRODPROCESS_BAL", ds, null, null, process, Orderno);
            if (!tdata)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Fail');", true);
                return;
            }
            string ppno = this.lblCurNo1.Text.ToString();
            for (int i = 0; i < list.Count; i++)
            {
                string preqno = list[i].preqno.ToString();
                string fProcessStep = list[i].fprostep.ToString();
                string tProcessStep = list[i].tprostep.ToString();
                string styleid = list[i].styleid.ToString();
                string colorid = list[i].colorid.ToString();
                string sizeid = list[i].sizeid.ToString();
                double balqty = Convert.ToDouble(list[i].balqty);
                double trnqty = Convert.ToDouble(list[i].trnsqty);
                double rejectionqty = Convert.ToDouble(list[i].rejectionqty);
                double repairqty = Convert.ToDouble(list[i].repairqty);
                string hourData = list[i].prodtime.ToString();
                string lineData = list[i].floorline.ToString();
                string MachineData = list[i].machine.ToString();
                string ManPowerData = Convert.ToDouble(list[i].manpower).ToString();

                if (trnqty > 0 && trnqty <= balqty)
                {
                    rptdata.UpdateTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "INORUPPROPROCESS", date, Orderno, fProcessStep, tProcessStep, styleid, trnqty.ToString(), rejectionqty.ToString(), repairqty.ToString(), hourData, lineData, MachineData, ManPowerData,
                        preqno, colorid, sizeid, ppno);

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Not ');", true);
                    continue;
                }

                //else
                //{
                //    ((Label)this.Master.FindControl("lblmsg")).Text = "Transfer Qty More then Balance Qty";
                //}

            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string macqty = Convert.ToDouble("0" + this.TxtMacqty.Text).ToString();
            rptdata.UpdateTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "UPDATE_PRODUCTION_PROCESS_INFO", Orderno, "00000000000000", ppno, userid, Terminal, Sessionid, Posteddat, macqty);

        }

        protected void gvproprocess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label reqty = (Label)e.Row.FindControl("lblgvrecQty");
                Label trnsqty = (Label)e.Row.FindControl("lblgvtrnsQty");
                Label balqty = (Label)e.Row.FindControl("lblgvBalQty");
                Label recqty = (Label)e.Row.FindControl("Label1");
                Label repare = (Label)e.Row.FindControl("lblgvrepairqty");
                Label reject = (Label)e.Row.FindControl("lblgvrejectionqty");

                Label descr = (Label)e.Row.FindControl("lblgvStyleDesr");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "AAAAAAAAAAAA")
                {

                    //reqty.Font.Bold = true;
                    trnsqty.Font.Bold = true;
                    balqty.Font.Bold = true;
                    descr.Font.Bold = true;
                    descr.Style.Add("text-align", "Right");

                    trnsqty.Style.Add("color", "blue");
                    balqty.Style.Add("color", "blue");
                    descr.Style.Add("color", "blue");
                    descr.Style.Add("color", "blue");
                    recqty.Style.Add("color", "blue");
                    repare.Style.Add("color", "blue");
                    reject.Style.Add("color", "blue");

                }

            }
        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string style = this.ddlStyle.SelectedValue.ToString();
            DataTable dt2 = (DataTable)ViewState["tblcolorsize"];
            DataView dv = dt2.DefaultView;
            dv.RowFilter = ("styleid='" + style + "'");
            dt2 = dv.ToTable(true, "colordesc", "colorid");
            this.ddlcolor.DataTextField = "colordesc";
            this.ddlcolor.DataValueField = "colorid";
            this.ddlcolor.DataSource = dt2; // ds1.Tables[1];
            this.ddlcolor.DataBind();
            ddlcolor_SelectedIndexChanged(null, null);
        }

        protected void ddlcolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string style = this.ddlStyle.SelectedValue.ToString();
            string color = this.ddlcolor.SelectedValue.ToString();
            DataTable dt2 = (DataTable)ViewState["tblcolorsize"];
            DataView dv = dt2.DefaultView;
            dv.RowFilter = ("styleid='" + style + "' and colorid='" + color + "'");
            dt2 = dv.ToTable(true, "sizedesc", "sizeid");
            this.ddlsize.DataTextField = "sizedesc";
            this.ddlsize.DataValueField = "sizeid";
            this.ddlsize.DataSource = dt2; // ds1.Tables[1];
            this.ddlsize.DataBind();
        }
        protected void LbtnQcUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.Request.QueryString["Type"].ToString() == "ProQc")
                {
                    this.txtComponent.Text = "";
                    this.txtReasonCode.Text = "";
                    this.txtRejReasonQty.Text = "";

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "RejReasonNew();", true);
                }

                string comcod = this.GetCompCode();
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string colorid = ((Label)this.gvqc.Rows[index].FindControl("lblColorid")).Text.ToString();
                string styleid = ((Label)this.gvqc.Rows[index].FindControl("lblgvStyleid")).Text.ToString();
                string sizeid = ((Label)this.gvqc.Rows[index].FindControl("lblgvSizeid")).Text.ToString();
                string tprocess = ((Label)this.gvqc.Rows[index].FindControl("lblgvToProcessid")).Text.ToString();
                string fprocess = ((Label)this.gvqc.Rows[index].FindControl("lblgvfromProcessid")).Text.ToString();

                
                //string fprocess = ((Label)this.gvqc.Rows[index].FindControl("lblgvSizeid")).Text.ToString();
                string ppnno = ((Label)this.gvqc.Rows[index].FindControl("lblPPnno")).Text.ToString();
                this.GetFaildReason(fprocess);

                this.GetComponentList();
                this.GetLineInfo();

                txtFDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                string mlccod = this.Request.QueryString["actcode"].ToString();
                string reqno = this.Request.QueryString["genno"].ToString();
                string sircode = this.Request.QueryString["sircode"].ToString();
                string date = this.Request.QueryString["date"].ToString();
                DataSet result = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GET_PRODUCTION_PROCESS_QCDETAILS", mlccod, reqno, styleid, colorid, sizeid, sircode, date, ppnno);
                if (result == null)
                {
                    return;
                }
                this.TextBoxRecQty.Text = Convert.ToDouble("0" + result.Tables[0].Rows[0]["qty"]).ToString();

                this.TxtPassQty.Text = Convert.ToDouble("0" + result.Tables[0].Rows[0]["qty"]).ToString();

                this.TextBoxRejQty.Text = Convert.ToDouble("0" + result.Tables[0].Rows[0]["rejectionqty"]).ToString();
                this.TextBoxRepQty.Text = Convert.ToDouble("0" + result.Tables[0].Rows[0]["repairqty"]).ToString();

                if (this.Request.QueryString["Type"].ToString() == "ProQc")
                {
                    this.TextBoxRejQty.Text = "0";
                    this.TextBoxRepQty.Text = "0";
                    ddlReason.SelectedValue = null;
                    ddlReason22.SelectedValue = null;
                }

                this.ChckStatus.Checked = (Convert.ToBoolean(result.Tables[0].Rows[0]["qcstatus"]) == true) ? true : false;
                this.txtgRemarks.Text = result.Tables[0].Rows[0]["remarks"].ToString();
                string reason = result.Tables[0].Rows[0]["reasons"].ToString();

                for (int i = 0; i < reason.Length; i = i + 5)
                {
                    var temp1 = reason.Substring(i, 5);
                    if (temp1 != "00000")
                    {

                        foreach (ListItem item in ddlReason.Items)
                        {
                            if (temp1 == item.Value.ToString())
                            {
                                item.Selected = true;
                            }

                        }
                    }
                }
                ViewState["tblqcdata"] = result.Tables[0];
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenQCModal();", true);
            }
            catch (Exception ex)
            {

            }
        }
        protected void LbtnUpdateQcDetails_Click(object sender, EventArgs e)
        {
            string Recqty = Convert.ToDouble("0" + this.TextBoxRecQty.Text.Trim()).ToString();
            string passqty = Convert.ToDouble("0" + this.TxtPassQty.Text.Trim()).ToString();
            string rejqty = Convert.ToDouble("0" + this.TextBoxRejQty.Text.Trim()).ToString();
            string repqty = Convert.ToDouble("0" + this.TextBoxRepQty.Text.Trim()).ToString();

            DataTable dt = (DataTable)ViewState["tblqcdata"];

            string reasondata = "";
            foreach (ListItem item in ddlReason.Items)
            {
                DataRow dr = dt.NewRow();
                if (item.Selected)
                {
                    reasondata += item.Value;
                }
            }
            dt.Rows[0]["reasons"] = reasondata;

            string reasondata22 = "";
            foreach (ListItem item in ddlReason22.Items)
            {
                DataRow dr = dt.NewRow();
                if (item.Selected)
                {
                    reasondata22 += item.Value;
                }
            }

            DataSet ds = new DataSet("ds1");
            ds.Merge(dt);
            ds.Tables[0].TableName = "tblreason";
            string comcod = this.GetCompCode();
            string qcstatus = (this.ChckStatus.Checked == true) ? "True" : "False";
            string Remarks = this.txtgRemarks.Text.ToString();
            string mPower = this.txtmpower.Text.ToString();
            string data = ds.GetXml();


            //////////////////////////////////////////////////////////////////
            DataSet tblreason = new DataSet("tblreason");

            string Components = this.txtComponent.Text;
            string Reasons = this.txtReasonCode.Text;
            string ReasonQty = this.txtRejReasonQty.Text;

            if (ReasonQty != "")
            {
                DataTable dt1 = new DataTable();
                dt1.Clear();
                dt1.Columns.Add("component");
                dt1.Columns.Add("reason");
                dt1.Columns.Add("rqty");

                string[] comp = Components.Split(',');
                string[] res = Reasons.Split(',');
                string[] rqty = ReasonQty.Split(',');

                for (int i = 0; i < rqty.Length; i++)
                {
                    DataRow dr1 = dt1.NewRow();
                    dr1["reason"] = res[i];
                    dr1["rqty"] = rqty[i];
                    dr1["component"] = comp[i];
                    dt1.Rows.Add(dr1);
                }
                tblreason.Merge(dt1);
                tblreason.Tables[0].TableName = "qcreason";
                string data2 = tblreason.GetXml();
            }

            ///////////////////////////////////////////////////////////////////////////
            string ReasonQty22 = this.txtRejReasonQty22.Text;
            string Components22 = this.txtComponent22.Text;
            string Reasons22 = this.txtReasonCode22.Text;

            if (ReasonQty22 != "")
            {
                DataTable dt12 = new DataTable();
                dt12.Clear();
                dt12.Columns.Add("rpairComp");
                dt12.Columns.Add("repair");
                dt12.Columns.Add("rpairqty");

                string[] rqty22 = ReasonQty22.Split(',');
                string[] comp22 = Components22.Split(',');
                string[] res22 = Reasons22.Split(',');

                for (int i = 0; i < rqty22.Length; i++)
                {
                    DataRow dr12 = dt12.NewRow();
                    dr12["rpairqty"] = rqty22[i];
                    dr12["repair"] = res22[i];
                    dr12["rpairComp"] = comp22[i];
                    dt12.Rows.Add(dr12);
                }
                tblreason.Merge(dt12);
                tblreason.Tables[1].TableName = "qcrepair";
            }
            var temp2 = tblreason.GetXml();

            string appliedDate = Convert.ToDateTime(this.txtFDate.Text).ToString();

            bool result = rptdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "UPDATE_PRODQC_DETAILS_INFO", ds, tblreason, null, qcstatus, Remarks,
                Recqty, passqty, rejqty, repqty, mPower, appliedDate);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                this.ProProcessQc();
                return;

            }

        }


        protected void gvqc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string qcstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qcstatus"));
                if (qcstatus == "True")
                {
                    e.Row.BackColor = System.Drawing.Color.LimeGreen;
                }

            }
        }

        public void GetFaildReason(string process)
        {
            string comcod = this.GetCompCode();
            //string process = this.Request.QueryString["sircode"].ToString();
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GET_FAILD_REASON", process, "");
            ddlReason.DataTextField = "gdesc";
            ddlReason.DataValueField = "gcod";
            ddlReason.DataSource = ds1.Tables[0];
            ddlReason.DataBind();

            ddlReason22.DataTextField = "gdesc";
            ddlReason22.DataValueField = "gcod";
            ddlReason22.DataSource = ds1.Tables[0];
            ddlReason22.DataBind();
        }


        //UPDATED BYE AREFIN 12/3/2023

        private void GetComponentList()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            ddlComponent.DataTextField = "resdesc";
            ddlComponent.DataValueField = "rescode";
            ddlComponent.DataSource = ds2;
            ddlComponent.DataBind();
        }


        private void GetLineInfo()
        {
            //
            string comcod = GetCompCode();
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPRODSETUPINFO", "", "", "", "", "", "", "", "", "");

            this.ddlLineModal.DataTextField = "sirdesc";
            this.ddlLineModal.DataValueField = "sircode";
            this.ddlLineModal.DataSource = ds1.Tables[1];
            this.ddlLineModal.DataBind();
        }

        protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderNo.SelectedValue.ToString();
            string preqno = "";
            if (Request.QueryString.AllKeys.Contains("genno"))
            {

                preqno = Request.QueryString["genno"].ToString();

            }
            string dayid = "%";
            if (Request.QueryString.AllKeys.Contains("dayid"))
            {
                if (this.Request.QueryString["dayid"].ToString().Length > 0)
                {
                    dayid = Request.QueryString["dayid"].ToString();
                }
            }
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_REPORT_PRODPROCESS", "GET_MASTER_LC_WISE_REQNO", mlccod, dayid, preqno);
            ddlReqno.DataTextField = "preqno1";
            ddlReqno.DataValueField = "preqno";
            ddlReqno.DataSource = ds1.Tables[0];
            ddlReqno.DataBind();
        }

        protected void ddlToProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblprocess"];
            string Process = this.ddlFromProcess.SelectedValue.ToString().Trim();
            string toProcess = this.ddlToProcess.SelectedValue.ToString().Trim();

            DataRow[] dr1 = dt.Select("procode = '" + Process + "' ");
            DataRow[] dr2 = dt.Select("procode = '" + toProcess + "' ");


            if (Convert.ToInt32(dr1[0]["sirval"]) != 0)
            {
                if (Convert.ToInt32(dr1[0]["sirval"]) > Convert.ToInt32(dr2[0]["sirval"]))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry! You Not eligible to Transfer to this process');", true);
                    this.lbtnShow.Enabled = false;
                    this.LbtnAddAllReq.Enabled = false;
                    return;
                }
                else
                {
                    this.lbtnShow.Enabled = true;
                    this.LbtnAddAllReq.Enabled = true;

                }
            }
        }

        protected void LbtnAddAllReq_Click(object sender, EventArgs e)
        {
            var List = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];
            string comcod = GetCompCode();
            string Orderno = this.ddlOrderNo.SelectedValue.ToString();
            string Process = this.ddlFromProcess.SelectedValue.ToString();
            string Processdesc = this.ddlFromProcess.SelectedItem.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString();
            string styledesc = this.ddlStyle.SelectedItem.ToString();
            string colorid = this.ddlcolor.SelectedValue.ToString();
            string colordesc = this.ddlcolor.SelectedItem.ToString();
            string sizeid = "";// this.ddlsize.SelectedValue.ToString();
            string sizedesc = "";// this.ddlsize.SelectedItem.ToString();
            double manpower = Convert.ToDouble("0" + this.txtManpower.Text.Trim());
            string floor = this.ddlLine.SelectedValue.ToString();
            string floordesc = this.ddlLine.SelectedItem.ToString() == "000000000000" ? "%%" : this.ddlLine.SelectedItem.ToString() + "%";
            string machine = this.ddlMachine.SelectedValue.ToString();
            string machdesc = this.ddlMachine.SelectedItem.ToString();
            string prodtime = this.ddHour.SelectedValue.ToString();
            string proddesc = this.ddHour.SelectedItem.ToString();
            string trdate = Convert.ToDateTime(this.txttrnsDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string toprocess = this.ddlToProcess.SelectedValue.ToString();
            string toprocessdesc = this.ddlToProcess.SelectedItem.ToString();

            foreach (ListItem item in ddlsize.Items)
            {
                if (item.Selected)
                {
                    foreach (ListItem item1 in ddlReqno.Items)
                    {
                        string preqno = item1.Value;
                        string rdate = item1.Text.Substring(15);// Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
                        sizeid = item.Value;
                        sizedesc = item.Text;
                        DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "SHOWTRANSPROCESS", Orderno, Process, preqno, styleid, colorid, sizeid, rdate, "", "");
                        if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid " + sizedesc + "'Or Process'" + toprocessdesc + "');", true);


                            continue;
                        }
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "";
                        }
                        double recvqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["recvqty"]);
                        double trnsqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["trnqty"]);
                        double balqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["balqty"]);
                        double rejectionqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["rejectionqty"]);
                        double repairqty = Convert.ToDouble(ds1.Tables[0].Rows[0]["repairqty"]);

                        string styleunit = ds1.Tables[0].Rows[0]["styleunit"].ToString();
                        var checklist = List.FindAll(p => p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.preqno == preqno); // && p.prodtime == prodtime
                        if (checklist.Count() > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Added!!');", true);
                            continue;
                        }

                        List.Add(new SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess(comcod, "", "", "", "", "", styleid, styledesc, colorid, colordesc,
                            sizeid, sizedesc, "", recvqty, trdate, trnsqty, balqty, rejectionqty, manpower, repairqty, floor, machine, prodtime, styleunit,
                            floordesc, machdesc, proddesc, toprocess, toprocessdesc, Process, Processdesc, preqno));

                    }
                }
            }
            ViewState["tblstpprocess"] = List;
            this.Data_Bind();
            adjustment();
        }

        private void adjustment()
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> List = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];
            DataTable dt = ASITUtility03.ListToDataTable(List);
            if (dt.Rows.Count > 0)
            {
                var newDt = dt.AsEnumerable()
                                      .GroupBy(r => new
                                      {
                                          styleid = r.Field<string>("styleid"),
                                          colorid = r.Field<string>("colorid"),
                                          sizeid = r.Field<string>("sizeid"),
                                      })
                                      .Select(g =>
                                      {
                                          var row = dt.NewRow();
                                          row["styleid"] = g.Key.styleid;
                                          row["colorid"] = g.Key.colorid;
                                          row["sizeid"] = g.Key.sizeid;
                                          row["styledesc"] = g.First()["styledesc"];
                                          row["colordesc"] = g.First()["colordesc"];
                                          row["sizedesc"] = g.First()["sizedesc"];
                                          row["balqty"] = g.Sum(r => r.Field<double>("balqty"));
                                          row["recvqty"] = g.Sum(r => r.Field<double>("recvqty"));
                                          row["trnsqty"] = g.Sum(r => r.Field<double>("trnsqty"));


                                          return row;
                                      }).CopyToDataTable();

                Session["tblProductionHelper"] = newDt;

                this.gvtblIsueHelper_DataBind();
            }
        }


        private void gvtblIsueHelper_DataBind()
        {
            DataTable dtisue = (DataTable)Session["tblProductionHelper"];

            this.gvProItem.DataSource = dtisue;
            this.gvProItem.DataBind();
            this.CalculateSumFooter();

            //((Label)this.gvIsuItem.FooterRow.FindControl("lgvRSumSMRRQty")).Text = (listsum.Select(p => p.mrrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.mrrqty).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalBalQty")).Text = (listsum.Select(p => p.orderbal).Sum() == 0.00) ? "0" : listsum.Select(p => p.orderbal).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalReceived")).Text = (listsum.Select(p => p.recup).Sum() == 0.00) ? "0" : listsum.Select(p => p.recup).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalOrdQty")).Text = (listsum.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.ordrqty).Sum().ToString("#,##0.00;(#,##0.00); ");

        }
        protected void LbtnRecItemCalculate_Click(object sender, EventArgs e)
        {
            DataTable dtRec = (DataTable)Session["tblProductionHelper"];
            //    var listsum = dtRec.DataTableToList<SumClass>();
            var sum = 0.00;

            for (int i = 0; i < this.gvProItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProItem.Rows[i].FindControl("txtgvSMRRQty")).Text.Trim()));
                sum += Qty;
                dtRec.Rows[i]["trnsqty"] = Qty;

                if (Qty == 0)
                    continue;
                List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> List = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];

                DataTable dt = ASITUtility03.ListToDataTable(List);

                DataRow[] dr3 = dt.Select("styleid='" + dtRec.Rows[i]["styleid"] + "' and colorid='" + dtRec.Rows[i]["colorid"] + "' and sizeid='" + dtRec.Rows[i]["sizeid"] + "'");

                foreach (DataRow item in dr3)
                {


                    if (Convert.ToDouble(item["balqty"]) < Qty)
                    {
                        item["trnsqty"] = item["balqty"];
                        Qty = Qty - Convert.ToDouble(item["balqty"]);
                    }
                    else
                    {
                        item["trnsqty"] = Qty;
                        Qty = 0;
                        break;
                    }
                }
                List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> List1 = ASITUtility03.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>(dt);

                ViewState["tblstpprocess"] = List1;

            }
            Session["tblProductionHelper"] = dtRec;
            this.Data_Bind();
            this.gvtblIsueHelper_DataBind();
        }

        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Expand")
            {
                this.gvProItem.Visible = true;
                this.LbtnReqItemShow.Text = "Collapse";
            }
            else
            {
                this.gvProItem.Visible = false;
                this.LbtnReqItemShow.Text = "Expand";
            }
        }

        protected void LbtnToClear_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> List = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];

            foreach (var item in List)
            {
                item.trnsqty = 0;
            }
            Session["tblstpprocess"] = List;
            this.Data_Bind();
        }

        protected void LbtnToClear1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblProductionHelper"];

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row["trnsqty"] = 0;
                }
                Session["tblProductionHelper"] = dt;
                this.gvtblIsueHelper_DataBind();
            }


        }


        private void CalculateTotalFooter()
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> listsum1 = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["tblstpprocess"];

            if (listsum1 != null && listsum1.Count > 0)
            {
                double ttlTransfer = listsum1.Sum(x => x.recvqty);
                double ttlProcess = listsum1.Sum(x => x.recvqty - x.balqty);
                double ttlBalance = listsum1.Sum(x => x.balqty);
                double ttlPass = listsum1.Sum(x => x.trnsqty);

                ((Label)(this.gvTransPro.FooterRow.FindControl("gvTargetQty"))).Text = ttlTransfer.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvTransPro.FooterRow.FindControl("gvProcQty"))).Text = ttlProcess.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvTransPro.FooterRow.FindControl("gvBalQty"))).Text = ttlBalance.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvTransPro.FooterRow.FindControl("gvPass"))).Text = ttlPass.ToString("#,##0.00;(#,##0.00); ");
            }

        }

        private void CalculateSumFooter()
        {
            DataTable dtsum2 = (DataTable)Session["tblProductionHelper"];

            if (dtsum2 != null && dtsum2.Rows.Count > 0)
            {

                double ttlReceive = Convert.ToDouble(dtsum2.Compute("SUM(recvqty)", string.Empty));
                double ttlProcess = (Convert.ToDouble(dtsum2.Compute("SUM(recvqty)", string.Empty)) - Convert.ToDouble(dtsum2.Compute("SUM(balqty)", string.Empty)));
                double ttlBalance = Convert.ToDouble(dtsum2.Compute("SUM(balqty)", string.Empty));
                double ttlPassQ = Convert.ToDouble(dtsum2.Compute("SUM(trnsqty)", string.Empty));

                ((Label)(this.gvProItem.FooterRow.FindControl("lblTotalReceived"))).Text = ttlReceive.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvProItem.FooterRow.FindControl("lblTotalOrdQty"))).Text = ttlProcess.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvProItem.FooterRow.FindControl("lblTotalBalQty"))).Text = ttlBalance.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvProItem.FooterRow.FindControl("lgvRSumSMRRQty"))).Text = ttlPassQ.ToString("#,##0.00;(#,##0.00); ");
            }
        }

        //protected void chkPass_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
                
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
        //    }
        //}

        private void lnkbtnHisprice_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();

                Hashtable hst = (Hashtable)Session["tblLogin"];

                List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> checkedRowsData = new List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>();

                for (int i = 0; i < this.gvqc.Rows.Count; i++)
                {

                    if (((CheckBox)this.gvqc.Rows[i].FindControl("chkPass")).Checked)
                    {
                        SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess rowData = new SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess();

                        rowData.colorid = ((Label)this.gvqc.Rows[i].FindControl("lblColorid")).Text;
                        rowData.colordesc = ((Label)this.gvqc.Rows[i].FindControl("lblgvColor")).Text;
                        rowData.styleid = ((Label)this.gvqc.Rows[i].FindControl("lblgvStyleid")).Text;
                        rowData.styledesc = ((Label)this.gvqc.Rows[i].FindControl("lblgvStyleDestrns")).Text;
                        rowData.sizeid = ((Label)this.gvqc.Rows[i].FindControl("lblgvSizeid")).Text;
                        rowData.sizedesc = ((Label)this.gvqc.Rows[i].FindControl("lblgvSize")).Text;
                        rowData.tprostep = ((Label)this.gvqc.Rows[i].FindControl("lblgvToProcessid")).Text;
                        rowData.fprostep = ((Label)this.gvqc.Rows[i].FindControl("lblgvfromProcessid")).Text;
                        rowData.ppnno = ((Label)this.gvqc.Rows[i].FindControl("lblPPnno")).Text;
                        rowData.flordesc = ((Label)this.gvqc.Rows[i].FindControl("lblgvFloor")).Text;
                        rowData.machdesc = ((Label)this.gvqc.Rows[i].FindControl("lblgvMach")).Text;
                        rowData.pid = Convert.ToInt32(((Label)this.gvqc.Rows[i].FindControl("lblgvPID")).Text);
                        rowData.orderno = ((Label)this.gvqc.Rows[i].FindControl("lblgvOrderNo")).Text;
                        rowData.preqno = this.Request.QueryString["genno"].ToString();

                        checkedRowsData.Add(rowData);
                    }

                }

                ViewState["CheckedRowsData"] = checkedRowsData;

                this.TextBoxAppDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenBulkQCModal();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }


        }

        protected void upButton_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();

                Hashtable hst = (Hashtable)Session["tblLogin"];

                List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess> checkedRowsData = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess>)ViewState["CheckedRowsData"];
                bool result = false;
                string qcstatus = (this.CheckBoxPassFail.Checked == true) ? "True" : "False";
                string appliedDate = Convert.ToDateTime(this.TextBoxAppDate.Text).ToString("dd-MMM-yyyy");
                string Remarks = this.TextBoxRemarks.Text.ToString();
                foreach (var item in checkedRowsData)
                {
                    string pid = item.pid.ToString();
                    string ppnno = item.ppnno.ToString();
                    string orderno = item.orderno.ToString();
                    string fprostep = item.fprostep.ToString();
                    string tprostep = item.tprostep.ToString();
                    string styleid = item.styleid.ToString();
                    string colorid = item.colorid.ToString();
                    string sizeid = item.sizeid.ToString();
                    string preqno = item.preqno.ToString();

                     result = rptdata.UpdateTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "UPDATE_BULKQC", qcstatus, appliedDate, Remarks, pid, ppnno, orderno, fprostep, tprostep, styleid, colorid, sizeid, preqno);
                    
                }
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    this.ProProcessQc();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }
    }
}