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
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_10_Procur
{
    public partial class PurMRQCEntry : System.Web.UI.Page
    {
        static string prevPage = String.Empty;

        ProcessAccess purData = new ProcessAccess();
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
                this.txtCurMRRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Entry") ? "Materials QC Information" :
                    (Request.QueryString["Type"].ToString() == "Approve") ? "Store Receive"
                    : "Materials QC ";
                //For Sanmar

                this.Load_Location();
                Hashtable hst = (Hashtable)Session["tblLogin"];

                if (hst["comcod"].ToString() == "3301")
                {
                    this.chkdupMRR.Enabled = false;
                    this.chkdupMRR.Checked = true;
                }
                this.GetMrList();
                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    ImgbtnPreMRR_Click(null, null);
                    this.lbtnOk_Click(null, null);
                }

                if (this.Request.QueryString["type"].ToString() == "Approve")
                {
                    this.FieldPrintType.Visible = false;
                    this.ddlResList.Enabled = false;
                    //this.gvPQCInfo.Columns[18].Visible = true;
                }
                this.CommonButton();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateMRR_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnResFooterTotal_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        protected void Load_Location()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMATLOCATION", "", "%", "", "", "", "", "", "", "");

            ViewState["tblLocation"] = ds1.Tables[0];
            if (ds1 == null)
                return;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = this.txtCurMRRDate.Text;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

          //  string lcrcvno = this.ddlOrderList.SelectedValue;
            string mQCno = this.lblCurMRRNo1.Text.Trim().Substring(0, 2) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(2, 2) + this.txtCurMRRNo2.Text.Trim();

            string ChalanNo = this.txtChalanNo.Text.Trim();
            //DataTable dt = (DataTable)ViewState["tblMRR"];
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "IQCDETAILSINFO", mQCno, ChalanNo, "", "", "", "", "", "", "");

            if (ds1 == null) return;
            if (ds1.Tables[0].Rows.Count == 0) return;
            
            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.BO_AllLCInfo.RptMatInspctReport>();

            LocalReport rpt1 = new LocalReport();

            string type = this.Request.QueryString["Type"].ToString();
            string prntType = "";

            switch (type)
            {

                case "Approve":
                    prntType = "0";
                    break;
            
                case "Entry":
                    prntType = this.ddlReportLevel.SelectedValue;
                    break;
            
                default:
                    prntType = "0";
                    break;
            
            }

            string chalanno = ds1.Tables[0].Rows[0]["chalanno"].ToString();
            string rcvdate = ds1.Tables[0].Rows[0]["rcvdate"].ToString();
            string suppliername = ds1.Tables[0].Rows[0]["ssirdesc"].ToString();

            switch (prntType)
            {

                case "0":
                    rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIncomingMatInspctFrmt0", list, null, null);
                    rpt1.EnableExternalImages = true;
                    
                    rpt1.SetParameters(new ReportParameter("rptTitle", "Materials Receive Report"));
                    break;
                    
                case "1":
                    rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIncomingMatInspctFrmt1", list, null, null);
                    rpt1.EnableExternalImages = true;

                    rpt1.SetParameters(new ReportParameter("rptTitle", "Incoming Materials Inspection Report"));
                    rpt1.SetParameters(new ReportParameter("rptType", "Leather/Non-leather"));
                    rpt1.SetParameters(new ReportParameter("notabene", "NB: QA Team is responsible for ensure quality only for new incoming leather/non-leather materials. Not for stock. if any stock Material issue with mixing new material; then store responsible personnel should ensure the similar material are issuing in one order."));
                    break;

                case "2":
                    rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIncomingMatInspctFrmt2", list, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("rptTitle", "Incoming Materials Inspection Report"));
                    rpt1.SetParameters(new ReportParameter("rptType", "Label & Hang Tag"));
                    break;

                case "3":
                    rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIncomingMatInspctFrmt3", list, null, null);
                    rpt1.EnableExternalImages = true;

                    rpt1.SetParameters(new ReportParameter("rptTitle", "Outsole Inspection Report"));
                    rpt1.SetParameters(new ReportParameter("notabene", "Note: Total Problem Qnty :-"));
                    rpt1.SetParameters(new ReportParameter("chalanno", chalanno));
                    rpt1.SetParameters(new ReportParameter("rcvdate", rcvdate));
                    rpt1.SetParameters(new ReportParameter("supplier", suppliername));
                    //rpt1.SetParameters(new ReportParameter("lc", lcrcvno));
                    break;
            }


            
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("date", date));

            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)ViewState["tblMRR"];
            //ReportDocument rptstk = new RMGiRPT.R_15_Pro.rptPurMrrEntry();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "Order Name: " + this.Request.QueryString["actcode"].ToString();
            //TextObject rpttxtsupplier = rptstk.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //rpttxtsupplier.Text = "Supplier Name: " + "";// this.txtSupplierName.Text.Trim(); 
            //TextObject rpttxtorder = rptstk.ReportDefinition.ReportObjects["txtorder"] as TextObject;
            //rpttxtorder.Text = "Order No: " + this.ddlOrderList.SelectedValue.ToString();
            //TextObject rpttxtBBLCName = rptstk.ReportDefinition.ReportObjects["txtBBLCName"] as TextObject;
            //rpttxtBBLCName.Text = "BBLC No:- " + "";//this.ddlBBLCList.SelectedItem.Text.Trim();


            //TextObject rpttxtchlnno = rptstk.ReportDefinition.ReportObjects["txtchalanno"] as TextObject;
            //rpttxtchlnno.Text = "Chalan No: " + this.txtChalanNo.Text.Trim();
            //TextObject rpttxtMrrno = rptstk.ReportDefinition.ReportObjects["Mrrno"] as TextObject;
            //rpttxtMrrno.Text = "MRR No: " + this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //TextObject rpttxtMrrRef = rptstk.ReportDefinition.ReportObjects["MrrRef"] as TextObject;
            //rpttxtMrrRef.Text = "MRR Ref: " + this.txtMRRRef.Text.Trim();
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + this.txtCurMRRDate.Text.Trim();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string comcod = this.GetCompCode();
            //    string eventtype = "Materials Receive Information";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ImgbtnPreMRR_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string SearchMrr = (this.Request.QueryString["genno"].ToString() == "") ? "%%" : this.Request.QueryString["genno"].ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPREVQCLIST", CurDate1, SearchMrr, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevMRRList.Items.Clear();
            this.ddlPrevMRRList.DataTextField = "purqcno1";
            this.ddlPrevMRRList.DataValueField = "purqcno";
            this.ddlPrevMRRList.DataSource = ds1.Tables[0];
            this.ddlPrevMRRList.DataBind();


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lblPreMRR.Visible = true;
                this.txtSrchPreMRR.Visible = true;
                this.ImgbtnPreMRR.Visible = true;
                this.ddlPrevMRRList.Visible = true;
                this.ddlPrevMRRList.Items.Clear();
                //NAHID
                //if (this.ddlPrevMRRList.Items.Count > 0)
                //{

                //    this.txtCurMRRDate.Enabled = false;
                //    this.lblCurMRRNo1.Text = ASTUtility.Left(this.ddlPrevMRRList.SelectedValue.ToString(),);
                //}


                this.lblCurMRRNo1.Text = "MRR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMRRDate.Enabled = true;
                this.txtMRRRef.Text = "";

                this.ddlOrderList.Enabled = true;
                this.txtResSearch.Text = "";
                this.ddlResList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtMRRNarr.Text = "";
                this.gvPQCInfo.DataSource = null;
                this.gvPQCInfo.DataBind();
                this.Panel1.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.txtChalanNo.Text = "";

                return;
            }

            this.lblPreMRR.Visible = false;
            this.txtSrchPreMRR.Visible = false;
            this.ImgbtnPreMRR.Visible = false;
            this.ddlPrevMRRList.Visible = false;



            this.txtCurMRRNo2.ReadOnly = true;
            this.ddlOrderList.Enabled = false;
            this.Panel1.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Receive_Info();

        }

        protected void Session_tblQC_Update()
        {

            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvPQCInfo.Rows.Count; j++)
            {
                double dgvOrderQty = 0.00;//Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvPQCInfo.Rows[j].FindControl("lblgvOrderQty")).Text.Trim()));
                double dgvMRRQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvPQCInfo.Rows[j].FindControl("txtgvMRRQty")).Text.Trim()));
                double dgvMRRRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvPQCInfo.Rows[j].FindControl("lblgvMRRRate")).Text.Trim()));
                double dgvChlnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvPQCInfo.Rows[j].FindControl("txtgvChlnqty")).Text.Trim()));
                string dgvMRRNote = ((TextBox)this.gvPQCInfo.Rows[j].FindControl("txtgvMRRNote")).Text.Trim();
                double dgvMRRAmt = dgvMRRQty * dgvMRRRate;
                double Balqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvPQCInfo.Rows[j].FindControl("lblgvOrderBal")).Text.Trim()));

                string gvRack = ((TextBox)this.gvPQCInfo.Rows[j].FindControl("txtgvRack")).Text.Trim();
                //string gvLoc = ((TextBox)this.gvPQCInfo.Rows[j].FindControl("txtgvLoc")).Text.Trim();
                string loction = ((DropDownList)this.gvPQCInfo.Rows[j].FindControl("ddlval")).SelectedValue.ToString();

                // double dgvOrderBal = dgvOrderQty - dgvMRRQty;
                if (Balqty >= dgvMRRQty)
                {
                    TblRowIndex2 = (this.gvPQCInfo.PageIndex) * this.gvPQCInfo.PageSize + j;
                    tbl1.Rows[TblRowIndex2]["purqcqty"] = dgvMRRQty;
                    tbl1.Rows[TblRowIndex2]["mrrrate"] = dgvMRRRate;
                    tbl1.Rows[TblRowIndex2]["purqcamt"] = dgvMRRAmt;
                    tbl1.Rows[TblRowIndex2]["purqcnote"] = dgvMRRNote;
                    tbl1.Rows[TblRowIndex2]["chlnqty"] = dgvChlnQty;
                    tbl1.Rows[TblRowIndex2]["rackno"] = gvRack;
                    tbl1.Rows[TblRowIndex2]["location"] = loction;

                }

                else
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "QC Qty  must be less then equal Balance Qty";
                    return;

                }

            }

            ViewState["tblMRR"] = tbl1;
        }

        protected void gvPQCInfo_DataBind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            if (this.Request.QueryString["Type"].ToString() == "Approve")
            {
                this.gvPQCInfo.Columns[19].Visible = true;
                this.gvPQCInfo.Columns[12].HeaderText = "Rcv. Qty.";
            }
            this.gvPQCInfo.DataSource = tbl1;
            this.gvPQCInfo.DataBind();
            this.Rcv_Footcal();

            var rcvdatasum = (List<SumLocalReceiveItems>)Session["TblLocalReceiveSum"];

            this.gvPQCItem.DataSource = rcvdatasum;
            this.gvPQCItem.DataBind();

            if (rcvdatasum.Count > 0)
            {
                ((Label)this.gvPQCItem.FooterRow.FindControl("lgvRSumRecqty")).Text = (rcvdatasum.Select(p => p.passqty).Sum() == 0.00) ? "0" : rcvdatasum.Select(p => p.passqty).Sum().ToString("#,##0.00;(#,##0.00); ");

            }

            //var rcvdatasum = (List<SumReceiveItems>)Session["TblReceivesum"];

            //this.gvPQCItem.DataSource = rcvdatasum;
            //this.gvPQCItem.DataBind();


            if (this.Request.QueryString["Type"].ToString() == "Entry")
            {

                //((LinkButton)this.gvPQCInfo.FooterRow.FindControl("lbtnDelMRR")).Visible = false;
                if (this.ddlPrevMRRList.Items.Count > 0)
                {

                    //((LinkButton)this.gvPQCInfo.FooterRow.FindControl("lbtnUpdateMRR")).Visible = false;
                    //   ((LinkButton)this.gvPQCInfo.FooterRow.FindControl("lbtnResFooterTotal")).Visible = false;
                }

            }
            else
            {

                this.gvPQCInfo.AutoGenerateDeleteButton = false;

            }

            if (tbl1.Rows.Count == 0)
                return;

            ((Label)this.gvPQCInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(purqcamt)", "")) ?
                   0.00 : tbl1.Compute("Sum(purqcamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((DropDownList)this.gvPQCInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvPQCInfo.PageSize);
            ((DropDownList)this.gvPQCInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvPQCInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvPQCInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
            ((DropDownList)this.gvPQCInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvPQCInfo.PageIndex;

        }


        protected void GetReceiveNo()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = "NEWMRR";
            if (this.ddlPrevMRRList.Items.Count > 0)
                mMRRNo = this.ddlPrevMRRList.SelectedValue.ToString();

            if (mMRRNo == "NEWMRR")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRQCLASTNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMRRNo1.Text = ds1.Tables[0].Rows[0]["maxqcno1"].ToString().Substring(0, 5);
                    this.txtCurMRRNo2.Text = ds1.Tables[0].Rows[0]["maxqcno1"].ToString().Substring(5, 6);
                    this.ddlPrevMRRList.DataTextField = "maxqcno1";
                    this.ddlPrevMRRList.DataValueField = "maxqcno";
                    this.ddlPrevMRRList.DataSource = ds1.Tables[0];
                    this.ddlPrevMRRList.DataBind();
                }
            }
        }



        protected void Get_Receive_Info()
        {
            Session.Remove("tblMRR");
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = "NEWQC";

            if (this.ddlPrevMRRList.Items.Count > 0)
            {
                this.txtCurMRRDate.Enabled = false;
                mMRRNo = this.ddlPrevMRRList.SelectedValue.ToString();
            }
            else if (this.Request.QueryString["mrrno"].ToString() != "")
            {
                mMRRNo = this.Request.QueryString["mrrno"].ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRQCINFO", mMRRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblMRR"] = ds1.Tables[0];
            Session["UserLog"] = ds1.Tables[1];
            Session["TblLocalReceiveSum"] = ds1.Tables[2].DataTableToList<SumLocalReceiveItems>();

            List<SumLocalReceiveItems> result = (List<SumLocalReceiveItems>)Session["TblLocalReceiveSum"];
            if (result.Count == 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    result = ds1.Tables[0]
                    .AsEnumerable()
                    .GroupBy(row => new { rescod = row.Field<string>("rsircode"), spcfcod = row.Field<string>("spcfcod") })
                    .Select(grp => new SumLocalReceiveItems
                    {
                        rescod = grp.Key.rescod,
                        resdesc = grp.First()["rsirdesc1"].ToString(),
                        spcfcod = grp.Key.spcfcod,
                        spcfdesc = grp.First()["spcfdesc"].ToString(),
                        color = grp.First()["color"].ToString(),
                        size = "",
                        unit = grp.First()["rsirunit"].ToString(),
                        passqty = grp.Sum(row => double.Parse(row["purqcqty"].ToString())),
                        rcvqty = grp.Sum(row => double.Parse(row["mrrqty"].ToString())),
                        qcqty = grp.Sum(row => double.Parse(row["purqcqty"].ToString())),
                        qcstatus = "0",
                        chckdetails = "",
                        chckmethod = "AQL",
                        finding = "",
                        remarks = "",
                    }).ToList();
                }
                Session["TblLocalReceiveSum"] = result;
            }

            if (mMRRNo == "NEWQC")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRQCLASTNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMRRNo1.Text = ds1.Tables[0].Rows[0]["maxqcno1"].ToString().Substring(0, 5);
                    this.txtCurMRRNo2.Text = ds1.Tables[0].Rows[0]["maxqcno1"].ToString().Substring(5, 6);
                }
                return;
            }

            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.ddlOrderList.DataTextField = "mrrno1";
                this.ddlOrderList.DataValueField = "mrrno";
                this.ddlOrderList.DataSource = ds1.Tables[1];
                this.ddlOrderList.DataBind();

                this.ddlResList.DataTextField = "rsirdesc1";
                this.ddlResList.DataValueField = "rsircode1";
                this.ddlResList.DataSource = ds1.Tables[1];
                this.ddlResList.DataBind();
            }

            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["mrrno"].ToString();

            this.txtMRRRef.Text = ds1.Tables[1].Rows[0]["mrrref"].ToString();
            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["mrrno"].ToString();
            this.lblCurMRRNo1.Text = ds1.Tables[1].Rows[0]["mrrno"].ToString().Substring(0, 2) + ds1.Tables[1].Rows[0]["mrrno"].ToString().Substring(6, 2) + "-";
            this.txtCurMRRNo2.Text = ds1.Tables[1].Rows[0]["mrrno"].ToString().Substring(8, 6);
            this.txtCurMRRDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd.MM.yyyy");

            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["aprvtrmid"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtMRRNarr.Text = ds1.Tables[1].Rows[0]["mrrnar"].ToString();
            this.txtChalanNo.Text = ds1.Tables[1].Rows[0]["chlnno"].ToString();

            this.gvPQCInfo_DataBind();
        }

        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {

            this.Session_tblQC_Update();
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            string reqno = this.ddlResList.SelectedValue.ToString().Substring(0, 14);
            string mResCode = this.ddlResList.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCode = this.ddlResList.SelectedValue.ToString().Substring(26, 12);
            string bomid = this.ddlResList.SelectedValue.ToString().Substring(66, 10);
            string mQCno = this.lblCurMRRNo1.Text.Trim().Substring(0, 2) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(2, 2) + this.txtCurMRRNo2.Text.Trim();

            DataRow[] dr2 = tbl1.Select("reqno='" + reqno + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "' and bomid='" + bomid + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["reqno"] = reqno;
                dr1["rsircode"] = mResCode;
                dr1["spcfcod"] = mSpcfCode;
                dr1["bomid"] = bomid;
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select(" reqno='" + reqno + "'and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "' and bomid='" + bomid + "'");
                dr1["reqno1"] = dr3[0]["reqno1"];
                dr1["orderno"] = dr3[0]["orderno"];
                dr1["orderno1"] = dr3[0]["orderno1"];
                dr1["purqcno"] = mQCno;
                dr1["rsirdesc1"] = dr3[0]["rsirdesc1"];
                dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["mrrqty"] = dr3[0]["mrrqty"];
                dr1["purqcqty"] = dr3[0]["purqcqty"];

                dr1["recup"] = dr3[0]["recup"];
                dr1["balqty"] = dr3[0]["balqty"];

                dr1["mrrrate"] = dr3[0]["mrrrate"];
                dr1["purqcamt"] = Convert.ToDouble(dr3[0]["balqty"]) * Convert.ToDouble(dr3[0]["mrrrate"]);
                dr1["purqcnote"] = "";
                dr1["chlnqty"] = 0;
                dr1["rackno"] = "";
                dr1["location"] = dr3[0]["location"];
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblMRR"] = tbl1;
            this.gvPQCInfo_DataBind();

        }

        protected void lbtnSelectResAll_Click(object sender, EventArgs e)
        {
            this.Session_tblQC_Update();
            string mReqno = this.ddlResList.SelectedValue.ToString().Substring(0, 14);
            string mResCode = this.ddlResList.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCode = this.ddlResList.SelectedValue.ToString().Substring(26, 12);
            string bomid = this.ddlResList.SelectedValue.ToString().Substring(66, 10);
            string mQCno = this.lblCurMRRNo1.Text.Trim().Substring(0, 2) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(2, 2) + this.txtCurMRRNo2.Text.Trim();


            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataTable tbl2 = (DataTable)ViewState["tblMat"];
            DataRow[] dr = tbl1.Select("reqno='" + mReqno + "' and  rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "' and bomid='" + bomid + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {

                    DataRow dr1 = tbl1.NewRow();
                    dr1["purqcno"] = mQCno;

                    dr1["reqno"] = tbl2.Rows[i]["reqno"].ToString();
                    dr1["reqno1"] = tbl2.Rows[i]["reqno1"].ToString();
                    dr1["orderno"] = tbl2.Rows[i]["orderno"].ToString();
                    dr1["orderno1"] = tbl2.Rows[i]["orderno1"].ToString();
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["bomid"] = tbl2.Rows[i]["bomid"].ToString();
                    dr1["rsirdesc1"] = tbl2.Rows[i]["rsirdesc1"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["purqcqty"] = tbl2.Rows[i]["purqcqty"].ToString();
                    dr1["recup"] = Convert.ToDouble(tbl2.Rows[i]["recup"]).ToString();
                    dr1["balqty"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    dr1["mrrqty"] = Convert.ToDouble(tbl2.Rows[i]["mrrqty"]);
                    dr1["mrrrate"] = Convert.ToDouble(tbl2.Rows[i]["mrrrate"]).ToString();
                    dr1["purqcamt"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]) * Convert.ToDouble(tbl2.Rows[i]["mrrrate"]);
                    dr1["purqcnote"] = "";
                    dr1["chlnqty"] = 0;
                    dr1["rackno"] = "";
                    dr1["location"] = tbl2.Rows[i]["location"].ToString();
                    tbl1.Rows.Add(dr1);
                    ////////////////////////////////////////



                }
                ViewState["tblMRR"] = tbl1;
            }
            this.gvPQCInfo_DataBind();

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblQC_Update();
            this.gvPQCInfo.PageIndex = ((DropDownList)this.gvPQCInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvPQCInfo_DataBind();
        }

        protected void lbtnUpdateMRR_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblbblcandsup"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataTable dtuser = (DataTable)Session["UserLog"];

            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
            string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : (tblEditByid == "") ? userid : tblEditByid;
            string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : (tblEditDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblEditDat;

            //////New Added
            this.SaveReciveItem();

            this.Session_tblQC_Update();
            if (this.ddlPrevMRRList.Items.Count == 0)
                this.GetReceiveNo();
            string mQCno = this.lblCurMRRNo1.Text.Trim().Substring(0, 2) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(2, 2) + this.txtCurMRRNo2.Text.Trim();
            string mMRRREF = this.txtMRRRef.Text.Trim();
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataRow[] dr = tbl1.Select("purqcqty>0");
            if (dr.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input QC Qty');", true);
                return;
            }

            if (this.chkdupMRR.Checked)
            {

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "CHECKEDDUPQCNO", mMRRREF, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                    ;


                else
                {

                    DataView dv1 = ds2.Tables[0].DefaultView;
                    dv1.RowFilter = ("purqcno <>'" + mQCno + "'");
                    DataTable dt = dv1.ToTable();
                    if (dt.Rows.Count == 0)
                        ;
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Found Duplicate QC No');", true);
                        this.ddlPrevMRRList.Items.Clear();
                        return;
                    }
                }
            }

            string type = this.Request.QueryString["Type"].ToString();
            string mMRRDAT = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mPACTCODE = this.Request.QueryString["actcode"].ToString();
            string mBBLCCode = dt1.Rows[0]["bblccode"].ToString();
            string mMRRUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mMRRBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mORDERNO = this.ddlResList.SelectedValue.ToString().Substring(52, 14);
            string mMRRNAR = this.txtMRRNarr.Text.Trim();
            string mMRRChlnNo = this.txtChalanNo.Text.Trim();
            string mMRRNO = this.Request.QueryString["mrrno"].ToString();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURQCINFO", "PURQCB",
                             mQCno, mMRRDAT, mPACTCODE, mBBLCCode, mORDERNO, mMRRUSRID, mAPPRUSRID, mAPPRDAT, mMRRBYDES, mAPPBYDES, mMRRREF, mMRRNAR, mMRRChlnNo, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, type);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mreqno = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string bomid = tbl1.Rows[i]["bomid"].ToString();
                double rcvqty = Convert.ToDouble(tbl1.Rows[i]["mrrqty"].ToString());
                double mMRRQTY = Convert.ToDouble(tbl1.Rows[i]["purqcqty"].ToString());
                string mMRRAMT = tbl1.Rows[i]["purqcamt"].ToString();
                string mMRRNOTE = tbl1.Rows[i]["purqcnote"].ToString();
                string mMRRchlnqty = tbl1.Rows[i]["chlnqty"].ToString();
                string mRackno = tbl1.Rows[i]["rackno"].ToString();
                string mLocation = tbl1.Rows[i]["location"].ToString();
                if (rcvqty >= mMRRQTY)
                {
                    if (mMRRQTY > 0)
                        result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURQCINFO", "PURQCA",
                                 mQCno, mRSIRCODE, mSPCFCOD, mMRRQTY.ToString(), mMRRAMT, mMRRNOTE, mMRRchlnqty, mreqno, mRackno, mLocation, mMRRNO, bomid, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('QC Qty  must be less then equal receive Qty');", true);  
                    return;
                }
            }
            this.txtCurMRRDate.Enabled = false;

            DataSet ds1 = new DataSet("ds1");
            var listsum = (List<SumLocalReceiveItems>)Session["TblLocalReceiveSum"];
            DataTable tbl5 = ASITUtility03.ListToDataTable(listsum); //((DataTable)ViewState["TblReceive"]).Copy();           

            ds1.Tables.Add(tbl5);
            ds1.Tables[0].TableName = "tbl1";

            result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURQCINFO", ds1, null,null, "PURQCC",mQCno, "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
            }  

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials QC Information";
                string eventdesc = "Update QC Qty";
                string eventdesc2 = mQCno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblQC_Update();
            gvPQCInfo_DataBind();
        }

        private void GetMrList()
        {
            string comcod = this.GetCompCode();
            string FindSupplier = "%";
            string mProjCode = "";//this.ddlProject.SelectedValue.ToString();
            string mrrno = this.Request.QueryString["mrrno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRBBLCQCLIST", FindSupplier, mrrno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblbblcandsup"] = ds1.Tables[0];


            this.ddlOrderList.DataTextField = "mrrno1";
            this.ddlOrderList.DataValueField = "mrrno";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();

            ds1.Dispose();

            this.ImgbtnFindRes_Click(null, null);
        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)ViewState["tblbblcandsup"];

            string reqno = this.Request.QueryString["genno"].ToString();

            string mrrno = this.Request.QueryString["mrrno"].ToString();

            string mProject = this.Request.QueryString["actcode"].ToString();
            string mBBLCCode = dt.Rows[0]["bblccode"].ToString();
            string mOrderNo = this.ddlOrderList.SelectedValue.ToString();
            string mSrchTxt = "%" + this.txtResSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRQCRESLIST", mSrchTxt, mProject, mBBLCCode, mrrno, reqno, "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblMat"] = ds1.Tables[0];

            this.ddlResList.DataTextField = "rsirdesc1";
            this.ddlResList.DataValueField = "rsircode1";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();
        }

        protected void gvPQCInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblMRR"];
            string MRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            string reqno = ((Label)this.gvPQCInfo.Rows[e.RowIndex].FindControl("lblgvReqnomain")).Text.Trim();
            string rescode = ((Label)this.gvPQCInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMRRMAT", MRRNO, reqno, rescode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvPQCInfo.PageSize) * (this.gvPQCInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState["tblMRR"] = dv.ToTable();
            this.gvPQCInfo_DataBind();
        }
        protected void lbtnDelMRR_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblMRR"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string date = System.DateTime.Today.ToString();
            string mMRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMRR", mMRRNO, userid, Terminal, Sessionid, date, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Deleted fail');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Receive Information";
                string eventdesc = "Delete Materials Received Qty";
                string eventdesc2 = mMRRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void gvPQCInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlval");
                string reqno = ((Label)e.Row.FindControl("lblgvReqnomain")).Text.Trim();
                string rescod = ((Label)e.Row.FindControl("lblgvResCod")).Text.Trim();
                string spcfcod = ((Label)e.Row.FindControl("lblgvSpcfCod")).Text.Trim();
                TextBox txtmrrqty = (TextBox)e.Row.FindControl("txtgvMRRQty");




                DataTable tbl1 = (DataTable)ViewState["tblMRR"];
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr = tbl1.Select("reqno='" + reqno + "' and  rsircode = '" + rescod + "' and spcfcod = '" + spcfcod + "'");

                DataTable dt = (DataTable)ViewState["tblLocation"];

                ddllocation.DataTextField = "gdesc";
                ddllocation.DataValueField = "gcod";
                ddllocation.DataSource = dt;
                ddllocation.DataBind();
                if (dr.Count() != 0)
                {
                    ddllocation.SelectedValue = dr[0]["location"].ToString();
                }
                //   ddllocation.Enabled = false;


                if (this.Request.QueryString["Type"] == "Approve")
                {
                    txtmrrqty.ReadOnly = true;
                }
            }
        }

        protected void LbtnQcUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string reqno = ((Label)this.gvPQCInfo.Rows[index].FindControl("lblgvReqnomain")).Text.ToString();
            string rsircode = ((Label)this.gvPQCInfo.Rows[index].FindControl("lblgvResCod")).Text.ToString();
            string spcfcod = ((Label)this.gvPQCInfo.Rows[index].FindControl("lblgvSpcfCod")).Text.ToString();
            string mrrno = this.Request.QueryString["mrrno"].ToString();
            string bomid = ((Label)this.gvPQCInfo.Rows[index].FindControl("lblgvBomNo")).Text.ToString();


            DataSet result = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "MATQCDETAILSINFO", reqno, mrrno, rsircode, spcfcod, "","", bomid);
            if (result == null)
            {
                return;
            }
            ViewState["tblqcdata"] = result.Tables[0];
            this.gvqcDeails.DataSource = result.Tables[0];
            this.gvqcDeails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenQCModal();", true);
        }

        protected void LbtnUpdateQcDetails_Click(object sender, EventArgs e)
        {
            this.Save_Value_QC();
            DataTable tbl1 = (DataTable)ViewState["tblqcdata"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(tbl1);
            ds.Tables[0].TableName = "tblqcdata";
            string comcod = this.GetCompCode();
            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "UPDATE_QC_DETAILS_INFO", ds, null, null);
            if (result)
            {
                DataTable tbl3 = (DataTable)ViewState["tblMRR"];
                //DataView dv1 = tbl3;
                //dv1.AllowDelete = ("bomid <>'" + tbl1.Rows[0]["bomid"] + "'");

                foreach (DataRow dr in tbl1.Rows)
                {
                    if (dr["bomid"].ToString() == tbl1.Rows[0]["bomid"].ToString())
                        dr.Delete();
                }

                this.gvPQCInfo_DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
                return;
            }
        }

        public void Save_Value_QC()
        {
            DataTable tbl1 = (DataTable)ViewState["tblqcdata"];
            for (int j = 0; j < this.gvqcDeails.Rows.Count; j++)
            {
                double qcqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvCckqty")).Text.Trim()));
                double passqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvpassqty")).Text.Trim()));

                string method = ((DropDownList)this.gvqcDeails.Rows[j].FindControl("ddlcheckmethod")).SelectedItem.ToString();
                string unit = ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvUom")).Text.Trim();
                string check_details = ((TextBox)this.gvqcDeails.Rows[j].FindControl("txtgvCheckDetails")).Text.Trim();
                string findings = ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvFindings")).Text.Trim();
                string remarks = ((TextBox)this.gvqcDeails.Rows[j].FindControl("txtgvRemarks")).Text.Trim().ToString();
                //string status = (((CheckBox)this.gvqcDeails.Rows[j].FindControl("ChckStatus")).Checked) ? "True" : "False";
                string status = ((DropDownList)this.gvqcDeails.Rows[j].FindControl("ddlPassFail")).SelectedValue;

                tbl1.Rows[j]["chckmethod"] = method;
                tbl1.Rows[j]["unit"] = unit;
                tbl1.Rows[j]["qcqty"] = qcqty;
                tbl1.Rows[j]["chckdetails"] = check_details;
                tbl1.Rows[j]["finding"] = findings;
                tbl1.Rows[j]["remarks"] = remarks;
                tbl1.Rows[j]["qcstatus"] = status;
                tbl1.Rows[j]["passqty"] = passqty;
            }

            ViewState["tblqcdata"] = tbl1;
        }

        protected void gvqcDeails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList method = (DropDownList)e.Row.FindControl("ddlcheckmethod");
                method.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "chckmethod"));
                //CheckBox qcstatus = (CheckBox)e.Row.FindControl("ChckStatus");
                //qcstatus.Checked = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qcstatus")).ToString() == "True") ? true : false;

                DropDownList ddlstatus = ((DropDownList)e.Row.FindControl("ddlPassFail"));
                ddlstatus.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qcstatus"));
            }
        }

        protected void LbtnToClear_Click(object sender, EventArgs e)
        {
            //DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            //foreach (DataRow row in tbl1.Rows)
            //{
            //    row["balqty"] = 0;
            //    row["purqcqty"] = 0;
            //}

            //ViewState["tblMRR"] = tbl1;
            this.Clear();

        }

        private void Clear()
        {
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            foreach (DataRow row in tbl1.Rows)
            {
                //row["balqty"] = 0;
                row["purqcqty"] = 0;
            }

            ViewState["tblMRR"] = tbl1;
            this.gvPQCInfo_DataBind();
        }

        private void Rcv_Footcal()
        {
            try
            {
                DataTable tbl1 = (DataTable)ViewState["tblMRR"];

                if (tbl1.Rows.Count == 0)
                    return;

                double sum1 = tbl1.AsEnumerable().Sum(row => Convert.ToDouble(row["mrrqty"]));
                double sum2 = tbl1.AsEnumerable().Sum(row => Convert.ToDouble(row["balqty"]));
                double sum3 = tbl1.AsEnumerable().Sum(row => Convert.ToDouble(row["purqcqty"]));

                ((Label)this.gvPQCInfo.FooterRow.FindControl("lblgvRecF")).Text = (sum1 == 0.00) ? "0" : sum1.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvPQCInfo.FooterRow.FindControl("lblgvBalFQty")).Text = (sum2 == 0.00) ? "0" : sum2.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvPQCInfo.FooterRow.FindControl("lblgvQCFQty")).Text = (sum3 == 0.00) ? "0" : sum3.ToString("#,##0.00;(#,##0.00); ");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Expand")
            {
                this.gvPQCItem.Visible = true;
                this.LbtnReqItemShow.Text = "Collapse";
            }
            else
            {
                this.gvPQCItem.Visible = false;
                this.LbtnReqItemShow.Text = "Expand";
            }
        }

        protected void gvPQCItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList archivebtn = (DropDownList)e.Row.FindControl("ddlQccheckmethod");
                archivebtn.SelectedValue = (DataBinder.Eval(e.Row.DataItem, "chckmethod")).ToString();

                DropDownList ddlqcsatus = (DropDownList)e.Row.FindControl("ddlQcStatus");
                ddlqcsatus.SelectedValue = (DataBinder.Eval(e.Row.DataItem, "qcstatus")).ToString();
            }
        }

        protected void LbtnRecItemCalculate_Click(object sender, EventArgs e)
        {

            this.SaveReciveItem();
            this.gvPQCInfo_DataBind();
        }

        private void SaveReciveItem()
        {
            var listsum = (List<SumLocalReceiveItems>)Session["TblLocalReceiveSum"];

            for (int i = 0; i < this.gvPQCItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvPQCItem.Rows[i].FindControl("lgvISRecqty")).Text.Trim()));
                double qcQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvPQCItem.Rows[i].FindControl("lgvSRecBalqty")).Text.Trim()));
                string unit = ((TextBox)this.gvPQCItem.Rows[i].FindControl("lblgvQcUnit")).Text.Trim().ToString();
                string chckdetails = ((TextBox)this.gvPQCItem.Rows[i].FindControl("lblgvchckdetails")).Text.Trim().ToString();
                string finding = ((TextBox)this.gvPQCItem.Rows[i].FindControl("lblgvFindings")).Text.Trim().ToString();
                string remarks = ((TextBox)this.gvPQCItem.Rows[i].FindControl("lblgvQcRemarks")).Text.Trim().ToString();
                string qcstatus = ((DropDownList)this.gvPQCItem.Rows[i].FindControl("ddlQcStatus")).SelectedValue.Trim().ToString();
                string chekmethod = ((DropDownList)this.gvPQCItem.Rows[i].FindControl("ddlQccheckmethod")).SelectedValue.Trim().ToString();

                listsum[i].passqty = Qty;
                listsum[i].qcqty = qcQty;
                listsum[i].unit = unit;
                listsum[i].chckdetails = chckdetails;
                listsum[i].chckmethod = chekmethod;
                listsum[i].finding = finding;
                listsum[i].remarks = remarks;
                listsum[i].qcstatus = qcstatus;

                //if (Qty == 0)
                //    continue;

                DataTable tbl1 = (DataTable)ViewState["tblMRR"];

                foreach (DataRow row in tbl1.AsEnumerable().Where(r => string.Concat(r.Field<string>("rsircode"), r.Field<string>("spcfcod")) == string.Concat(listsum[i].rescod, listsum[i].spcfcod)))
                {
                    double mqty;

                   // row["balqty"] = qcQty;

                    if (double.TryParse(row["mrrqty"].ToString(), out mqty))
                    {
                        if (mqty < Qty)
                        {
                            
                            row["purqcqty"] = mqty;
                            Qty -= mqty;
                        }
                        else
                        {
                            row["purqcqty"] = Qty;
                            row["balqty"] = mqty - Qty;
                            Qty = 0;
                            break;
                        }
                    }
                    else
                    {
                        mqty = 0;
                    }
                }

                ViewState["tblMRR"] = tbl1;
            }

            Session["TblLocalReceiveSum"] = listsum;
        }


        class SumLocalReceiveItems
        {
            public string rescod { get; set; }
            public string resdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public string unit { get; set; }
            public double rcvqty { get; set; }
            public double passqty { get; set; }
            public double qcqty { get; set; }
            public string chckdetails { get; set; }
            public string finding { get; set; }
            public string remarks { get; set; }
            public string chckmethod { get; set; }
            public string qcstatus { get; set; }
        }
    }
}