using System;
using System.Collections;
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
using SPERDLC;
using SPEENTITY;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_15_Pro
{
    public partial class ProQCEntry : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();
        static string prevPage = String.Empty;
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtQcDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProdNo();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "Entry") ? "FG QC Entry" : "Semi-FG QC Entry";
                this.CommonButton();
                this.GETPRDHOur();
                this.VisiableData();
                //if (this.Request.QueryString["genno"] != "")
                //{
                //    ImgbtnFindGrrList_Click(null, null);
                if (this.Request.QueryString["genno"].ToString() != "")
                {
                    //this.ddlPrevGRRList.SelectedValue = this.Request.QueryString["genno"].ToString();
                    lbtnOk_Click(null, null);
                    //}
                }
            }

        }

        private void VisiableData()
        {
            string comcod = this.GetCompCode();
            if (this.Request.QueryString["Type"] == "Entry")
            {
                switch (comcod)
                {
                    case "8701":
                    case "8702":
                    case "8703":
                    case "8704":

                        this.PanelTime.Visible = true;
                        break;
                }

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;//(this.Request.QueryString["Type"] == "Edit") ? true : false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Calculation";
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "";


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProdNo()
        {
            string comcod = this.GetCompCode();
            string filter3 = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%%" : this.Request.QueryString["genno"].ToString() + "%";
            string Type = (this.Request.QueryString["Type"] == "Entry") ? "17" : "15";
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETPRODUCTIONNO", filter3, Type, "", "", "", "", "", "", "");
            this.ddlProNO.DataTextField = "prodno1";
            this.ddlProNO.DataValueField = "prodno";
            this.ddlProNO.DataSource = ds1.Tables[0];
            this.ddlProNO.DataBind();
            ds1.Dispose();
        }
        private void GETPRDHOur()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODHOUR", "", "", "", "", "", "", "", "", "");
            //this.ddlhour.DataTextField = "gdesc";
            //this.ddlhour.DataValueField = "gcod";
            //this.ddlhour.DataSource = ds1.Tables[2];
            //this.ddlhour.DataBind();
            //if (ASTUtility.Left(comcod, 2) != "87" || this.Request.QueryString["type"] == "EntrySemi")
            //{
            //    this.ddlhour.SelectedValue = "00000";
            //}



        }
        protected void ImgbtnFindpbpno_Click(object sender, EventArgs e)
        {

        }
        protected void ImgbtnReqse_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //if (this.ddlProNO.Items.Count > 0)
                //    this.lblddlProNo.Text = this.ddlProNO.SelectedItem.Text;
                this.ddlProNO.Enabled = false;
                this.PanelOther.Visible = true;
                //this.lblPreviousGrr.Visible = false;
                //this.txtSrchGRR.Visible = false;
                this.ImgbtnFindGrrList.Visible = false;
                this.ddlPrevGRRList.Visible = false;
                this.VisiableQCData();
                this.ShowGrrInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.ddlPrevGRRList.Items.Clear();
            this.ddlProNO.Enabled = true;
            this.ImgbtnFindGrrList.Visible = true;
            this.ddlPrevGRRList.Visible = true;
            this.txtCurDate.Enabled = true;
            this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            this.gvGRR.DataSource = null;
            this.txtMRFNo.Text = "";
            this.gvGRR.DataBind();
            this.GetProdNo();
            this.PanelOther.Visible = false;
            this.gvHourlyQc.DataSource = null;
            this.gvHourlyQc.DataBind();
            this.pnlHQC.Visible = false;
        }
        private void VisiableQCData()
        {
            string comcod = this.GetCompCode();
            if (this.Request.QueryString["Type"] == "Entry")
            {
                switch (comcod)
                {
                    case "8701":
                    case "8702":
                    case "8703":
                    case "8704":
                        this.pnlHQC.Visible = true;
                        this.GetHourlyQCInf();
                        break;
                }

            }

        }
        private void GetGRRNO()
        {

            string comcod = this.GetCompCode();
            string mGRRNo = "NEWPQC";
            if (this.ddlPrevGRRList.Items.Count > 0)
                mGRRNo = this.ddlPrevGRRList.SelectedValue.ToString();

            string CurDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            if (mGRRNo == "NEWPQC")
            {
                DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETLASTQCNO", CurDate,
                       "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxpqcno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxpqcno1"].ToString().Substring(6, 5);
                    this.ddlPrevGRRList.DataTextField = "maxpqcno1";
                    this.ddlPrevGRRList.DataValueField = "maxpqcno";
                    this.ddlPrevGRRList.DataSource = ds1.Tables[0];
                    this.ddlPrevGRRList.DataBind();
                }
            }

        }

        private void ShowGrrInfo()
        {

            ViewState.Remove("tblGRR");
            string comcod = this.GetCompCode();
            string CurDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtPRDno = ASTUtility.Left(this.ddlProNO.SelectedValue.Trim().ToString(), 14);

            string mGRRNo = "NEWPQC";
            if (this.ddlPrevGRRList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mGRRNo = this.ddlPrevGRRList.SelectedValue.ToString();
            }
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GRRINFO", mGRRNo, txtPRDno, "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            ViewState["tblGRR"] = HiddenSameData(ds1.Tables[0]);

            if (mGRRNo == "NEWPQC")
            {
                ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETLASTQCNO", CurDate, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxpqcno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxpqcno1"].ToString().Substring(6, 5);
                }
                //for New PBPNO 
                //string mPBPNo01 = this.lblCurPBPNo1.Text.Trim().Substring(0, 3) + this.txtDateto.Text.Trim().Substring(7, 4) + this.lblCurPBPNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();// this.txtReqNo.Text.Trim();
                //ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURBGDPREPARATION", "GETPBPINFO", StoreCode, date1, date2, mPBPNo01, "", "", "", "", "");
                //Session["tblPBP"] = ds1.Tables[0];
                this.Data_Bind();

                return;

            }

            //Load Production Number;

            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.ddlProNO.DataTextField = "proddesc";
                this.ddlProNO.DataValueField = "prodno";
                this.ddlProNO.DataSource = ds1.Tables[1];
                this.ddlProNO.DataBind();

            }
            this.ddlProNO.SelectedValue = ds1.Tables[1].Rows[0]["prodno"].ToString();
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["qcref"].ToString();

            if (this.ddlPrevGRRList.Items.Count > 0)
            {
                this.lblCurNo1.Text = this.ddlPrevGRRList.SelectedItem.Text.Substring(0, 6);//ds1.Tables[1].Rows[0]["pbpno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = this.ddlPrevGRRList.SelectedItem.Text.Substring(6, 5);//ds1.Tables[1].Rows[0]["pbpno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(this.ddlPrevGRRList.SelectedItem.Text.ToString().Substring(12, 11)).ToString("dd-MMM-yyyy");
                //this.lblddlProNo.Text = this.ddlProNO.SelectedItem.Text.Trim();
                //this.lblddlProNo.Visible = true;
            }
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string batchcode = dt1.Rows[0]["batchcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["batchcode"].ToString() == batchcode)
                {
                    dt1.Rows[j]["batchdesc"] = "";
                }



                batchcode = dt1.Rows[j]["batchcode"].ToString();

            }
            return dt1;

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblGRR"];
            if (dt.Rows.Count == 0)
                return;
            this.gvGRR.DataSource = dt;
            this.gvGRR.DataBind();
            this.FooterCal();
            if (this.Request.QueryString["Type"] == "Edit")
            {
                this.gvGRR.FooterRow.FindControl("lbtnUpdatePurPrepar").Visible = false;
            }
        }

        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tblGRR"];
            int TblRowIndex2;
            
            for (int j = 0; j < this.gvGRR.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvGRR.PageSize) * (this.gvGRR.PageIndex) + j;

                double lblgvQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvGRR.Rows[j].FindControl("lblQcBal")).Text.Trim()));
                double lblgvRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvGRR.Rows[j].FindControl("lblgvRate")).Text.Trim()));
                double txtgvActQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvGRR.Rows[j].FindControl("txtgvActQty")).Text.Trim()));
                double txtgvrejqc = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvGRR.Rows[j].FindControl("txtgvrejqc")).Text.Trim()));

                double QctotalQty = txtgvActQty + txtgvrejqc;

                if (lblgvQty < QctotalQty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Production Qty Empty');", true);

                  
                    break;

                }


                double dgvActAmt = txtgvActQty * lblgvRate;
                double dgvQcAmt = txtgvrejqc * lblgvRate;

                ((TextBox)this.gvGRR.Rows[j].FindControl("txtgvActQty")).Text = txtgvActQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvGRR.Rows[j].FindControl("lblgvActAmt")).Text = dgvActAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvGRR.Rows[j].FindControl("txtgvrejqc")).Text = txtgvrejqc.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvGRR.Rows[j].FindControl("lblgvrejamt")).Text = dgvQcAmt.ToString("#,##0.00;(#,##0.00); ");



                tbl1.Rows[TblRowIndex2]["qcqty"] = txtgvActQty;
                tbl1.Rows[TblRowIndex2]["qcamt"] = dgvActAmt;
                tbl1.Rows[TblRowIndex2]["rejqc"] = txtgvrejqc;
                tbl1.Rows[TblRowIndex2]["rejamt"] = dgvQcAmt;
            }
            ViewState["tblGRR"] = tbl1;
        }


        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument rptstk = new MFGRPT.R_19_FGInv.RptProReceive();
        //    TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    txtCompany.Text = comnam;
        //    //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
        //    //rpttxtnaration.Text = "Narration: " + this.txtReqNarr.Text.Trim();
        //    TextObject txtProductionNumber = rptstk.ReportDefinition.ReportObjects["txtProduction"] as TextObject;
        //    txtProductionNumber.Text = this.lblddlProNo.Text.Trim().ToString(); //this.ddlPBName.SelectedItem.Text.Trim();
        //    TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtcrdate.Text = "Date: " + this.txtCurDate.Text.ToString().Trim();
        //    TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["txtGRR"] as TextObject;
        //    txtcrno.Text = "GRR No:  " + this.lblCurNo1.Text + this.lblCurNo2.Text.ToString().Trim();
        //    TextObject txtref = rptstk.ReportDefinition.ReportObjects["txtREFNo"] as TextObject;
        //    txtref.Text = "REF No:  " + this.txtMRFNo.Text.Trim().ToString();
        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptstk.SetDataSource((DataTable)ViewState["tblGRR"]);
        //    Session["Report1"] = rptstk;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}
        protected void ImgbtnFindpbpno_Click1(object sender, EventArgs e)
        {
            this.GetProdNo();
        }
        protected void ImgbtnFindGrrList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string txtsrcgrr = "%";
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETPREGRRLIST", txtsrcgrr, "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            this.ddlPrevGRRList.DataTextField = "pqcdesc";
            this.ddlPrevGRRList.DataValueField = "pqcno";
            this.ddlPrevGRRList.DataSource = ds2.Tables[0];
            this.ddlPrevGRRList.DataBind();

        }
        protected void gvGRR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.lbtnUpdatePurPrepar_Click(null, null);
        }
        protected void lbtnUpdatePurPrepar_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            // return;

           
            DataTable tbl1 = (DataTable)ViewState["tblGRR"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            //DataTable dtuser = (DataTable)Session["tblUserReq"];
            //string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPostedDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            //string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string PostedByid = (this.Request.QueryString["Type"] == "Entry") ? userid :  (this.Request.QueryString["Type"] == "ReqEdit") ? userid 
            //    : (tblPostedByid == "") ? userid : tblPostedByid;

            //string Posttrmid = (this.Request.QueryString["Type"] == "Entry") ? Terminal :  (this.Request.QueryString["Type"] == "ReqEdit") ? Terminal 
            //    : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;

            //string PostSession = (this.Request.QueryString["Type"] == "Entry") ? Sessionid :  (this.Request.QueryString["Type"] == "ReqEdit") ? Sessionid 
            //    : (tblPostedSession == "") ? Sessionid : tblPostedSession;

            //string PostedDat = (this.Request.QueryString["Type"] == "Entry") ? Date :  (this.Request.QueryString["Type"] == "ReqEdit") ? Date 
            //    : (tblPostedSession == "") ? Date : tblPostedDat;

            string QCdate = this.txtQcDate.Text;
            string txtDPRno = ASTUtility.Left(this.ddlProNO.SelectedValue.ToString(), 14);
            string BatchNo = ASTUtility.Right(this.ddlProNO.SelectedValue.ToString(), 12);
            string GRRno = "";
            if (this.ddlPrevGRRList.Items.Count == 0)
                this.GetGRRNO();


            GRRno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string refno = this.txtMRFNo.Text.Trim();
            string Remarks = this.txtOrderNarr.Text.Trim();
            string qcTime = this.ddlhour.SelectedValue.ToString();
            bool result = false;
            result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "INORUPDATEPROQCB", GRRno, txtDPRno, date, refno, Remarks, userid, Terminal, Sessionid, Postdat, BatchNo, "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ProData.ErrorObject["Msg"] + "');", true);

                return;
            }

            DataTable tbl2 = (DataTable)ViewState["tblGRR"];

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string ProdCode = tbl2.Rows[i]["prodcode"].ToString();
                //string Batchcode = tbl2.Rows[i]["batchcode"].ToString();
                string spcfcod = tbl2.Rows[i]["spcfcod"].ToString();
                string qcqty = Convert.ToDouble(tbl2.Rows[i]["qcqty"].ToString().Trim()).ToString();
                string qcamt = Convert.ToDouble(tbl2.Rows[i]["qcamt"].ToString().Trim()).ToString();

                string rejqc = Convert.ToDouble(tbl2.Rows[i]["rejqc"].ToString().Trim()).ToString();
                string rejamt = Convert.ToDouble(tbl2.Rows[i]["rejamt"].ToString().Trim()).ToString();
                result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "INORUPDATEPROQCA", GRRno, ProdCode, qcqty, qcamt, "", rejqc, rejamt, qcTime, spcfcod, "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ProData.ErrorObject["Msg"] + "');", true);


                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully !');", true);
            }
            //result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "UPDATEPROFRMQC", GRRno, txtDPRno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            //if (result)
            //{
            //    for (int i = 0; i < tbl2.Rows.Count; i++)
            //    {
            //        string ProdCode = tbl2.Rows[i]["prodcode"].ToString();

            //        result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "UPDATEPRODINF", ProdCode, txtDPRno, GRRno, "", "", "", "", "", "", "", "", "", "", "", "");
            //    }
            //}

            //result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "UPDATEQCJV", txtDPRno, QCdate, userid, Terminal, Sessionid, Postdat, "", "", "", "", "", "", "", "");
            //if (!result)
            //{
            //    this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
            //    this.lblmsg.BackColor = System.Drawing.Color.Red;
            //    this.lblmsg.ForeColor = System.Drawing.Color.White;
            //    return;
            //}
            //this.lblmsg.Text = "Update Successfully !";

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        //protected void lnkbtnSave_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = this.GetCompCode();
        //    string userid = hst["usrid"].ToString();
        //    string Terminal = hst["trmid"].ToString();
        //    string Sessionid = hst["session"].ToString();
        //    string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        //    string QCdate = this.txtQcDate.Text;
        //    string txtDPRno = this.ddlProNO.SelectedValue.ToString();

        //       bool result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "UPDATEQCJV", txtDPRno, QCdate, userid, Terminal, Sessionid, Postdat, "", "", "", "", "", "", "", "");
        //    if (!result)
        //    {
        //        this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
        //        this.lblmsg.BackColor = System.Drawing.Color.Red;
        //        this.lblmsg.ForeColor = System.Drawing.Color.White;
        //        return;
        //    }
        //    this.lblmsg.Visible = true;
        //    this.lblmsg.Text = "Update Successfully !";
        //}
        protected void gvGRR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblGRR"];
            string mQCNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string rescode = ((Label)this.gvGRR.Rows[e.RowIndex].FindControl("lblprodcode")).Text.Trim();
            bool result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "DELETEQCITM",
                        mQCNO, rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvGRR.PageSize) * (this.gvGRR.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("prodcode<>''");
                ViewState["tblGRR"] = dv.ToTable();
                this.Data_Bind();
            }
        }

        private void GetHourlyQCInf()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string batch =ASTUtility.Right(this.ddlProNO.SelectedValue.ToString(),12);
            //DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETHOURLYQCINF", batch, "", "", "", "", "", "", "", "");


            //if (ds1 == null)
            //{
            //    return;
            //}
            //double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12, amt13, amt14, amt15, amt16, amt17, amt18, amt19, amt20, amt21, amt22, amt23,
            //              amt24;
            //var lst = ds1.Tables[0].DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.GetHourlyQCinf>();
            //if (lst.Count == 0)
            //    return;

            //amt1 = (lst.Select(p => p.t1).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t1).Sum();
            //amt2 = (lst.Select(p => p.t2).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t2).Sum();
            //amt3 = (lst.Select(p => p.t3).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t3).Sum();
            //amt4 = (lst.Select(p => p.t4).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t4).Sum();
            //amt5 = (lst.Select(p => p.t5).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t5).Sum();
            //amt6 = (lst.Select(p => p.t6).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t6).Sum();
            //amt7 = (lst.Select(p => p.t7).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t7).Sum();
            //amt8 = (lst.Select(p => p.t8).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t8).Sum();
            //amt9 = (lst.Select(p => p.t9).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t9).Sum();
            //amt10 = (lst.Select(p => p.t10).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t10).Sum();
            //amt11 = (lst.Select(p => p.t11).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t11).Sum();
            //amt12 = (lst.Select(p => p.t12).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t12).Sum();
            //amt13 = (lst.Select(p => p.t13).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t13).Sum();
            //amt14 = (lst.Select(p => p.t14).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t14).Sum();
            //amt15 = (lst.Select(p => p.t15).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t15).Sum();
            //amt16 = (lst.Select(p => p.t16).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t16).Sum();
            //amt17 = (lst.Select(p => p.t17).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t17).Sum();
            //amt18 = (lst.Select(p => p.t18).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t18).Sum();
            //amt19 = (lst.Select(p => p.t19).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t19).Sum();
            //amt20 = (lst.Select(p => p.t20).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t20).Sum();
            //amt21 = (lst.Select(p => p.t21).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t21).Sum();
            //amt22 = (lst.Select(p => p.t22).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t22).Sum();
            //amt23 = (lst.Select(p => p.t23).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t23).Sum();
            //amt24 = (lst.Select(p => p.t24).Sum() == 0.00) ? 0.00 : lst.Select(p => p.t24).Sum();



            //this.gvHourlyQc.Columns[5].Visible = (amt1 != 0);
            //this.gvHourlyQc.Columns[6].Visible = (amt2 != 0);
            //this.gvHourlyQc.Columns[7].Visible = (amt3 != 0);
            //this.gvHourlyQc.Columns[8].Visible = (amt4 != 0);
            //this.gvHourlyQc.Columns[9].Visible = (amt5 != 0);
            //this.gvHourlyQc.Columns[10].Visible = (amt6 != 0);
            //this.gvHourlyQc.Columns[11].Visible = (amt7 != 0);
            //this.gvHourlyQc.Columns[12].Visible = (amt8 != 0);
            //this.gvHourlyQc.Columns[13].Visible = (amt9 != 0);
            //this.gvHourlyQc.Columns[14].Visible = (amt10 != 0);
            //this.gvHourlyQc.Columns[15].Visible = (amt11 != 0);
            //this.gvHourlyQc.Columns[16].Visible = (amt12 != 0);
            //this.gvHourlyQc.Columns[17].Visible = (amt13 != 0);
            //this.gvHourlyQc.Columns[18].Visible = (amt14 != 0);
            //this.gvHourlyQc.Columns[19].Visible = (amt15 != 0);
            //this.gvHourlyQc.Columns[20].Visible = (amt16 != 0);
            //this.gvHourlyQc.Columns[21].Visible = (amt17 != 0);
            //this.gvHourlyQc.Columns[22].Visible = (amt18 != 0);
            //this.gvHourlyQc.Columns[23].Visible = (amt19 != 0);
            //this.gvHourlyQc.Columns[24].Visible = (amt20 != 0);
            //this.gvHourlyQc.Columns[25].Visible = (amt21 != 0);
            //this.gvHourlyQc.Columns[26].Visible = (amt22 != 0);
            //this.gvHourlyQc.Columns[27].Visible = (amt23 != 0);
            //this.gvHourlyQc.Columns[28].Visible = (amt24 != 0);
            //ViewState["hourlyqcinf"] = lst;
            //DataTable dt = ds1.Tables[1];
            //int j = 1;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (i > 23)
            //        break;
            //    this.gvHourlyQc.Columns[5 + i].HeaderText = dt.Rows[i]["gdesc"].ToString();
            //    //  i++;
            //}




            //this.gvHourlyQc.DataSource = (lst);
            //this.gvHourlyQc.DataBind();
            ////this.FooterCal();
            //Session["Report1"] = gvHourlyQc;
            //((HyperLink)this.gvHourlyQc.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        //private List<MFGOBJ.C_13_ProdMon.BO_Production.GetHourlyQCinf> HiddenSameData(List<MFGOBJ.C_13_ProdMon.BO_Production.GetHourlyQCinf> lst3)
        //{
        //    if (lst3.Count == 0)
        //        return lst3;

        //    int i = 0;
        //    string prodcode = "";
        //    foreach (MFGOBJ.C_13_ProdMon.BO_Production.GetHourlyQCinf c1 in lst3)
        //    {
        //        if (i == 0)
        //        {

        //            prodcode = c1.prodcode;
        //            i++;
        //            continue;

        //        }
        //        else if (c1.prodcode == prodcode)
        //        {
        //            c1.prodesc = "";
        //        }
        //        prodcode = c1.prodcode;

        //    }

        //    return lst3;

        //}
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tblGRR"];

            ((Label)this.gvGRR.FooterRow.FindControl("lblFRecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(grrqty)", "")) ? 0.00 : dt.Compute("Sum(grrqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFQcDoneqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(compqty)", "")) ? 0.00 : dt.Compute("Sum(compqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFRecAmtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(grramt)", "")) ? 0.00 : dt.Compute("Sum(grramt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFQcqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qcqty)", "")) ? 0.00 : dt.Compute("Sum(qcqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFQcAmtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qcamt)", "")) ? 0.00 : dt.Compute("Sum(qcamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFrejqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rejqc)", "")) ? 0.00 : dt.Compute("Sum(rejqc)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFrejamtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rejamt)", "")) ? 0.00 : dt.Compute("Sum(rejamt)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string txtCurDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtQcDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////  string DateFT= "Date: (From " + txtfromdate + " To " + todate + ")";
            //string  GRRno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            //string refno = this.txtMRFNo.Text.Trim();
            //string qcTime = this.ddlhour.SelectedItem.Text.Trim().ToString();

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //DataTable dt = (DataTable)ViewState["tblGRR"];

            //var lst = dt.DataTableToList<MFGOBJ.C_19_FGInv.Pro_Qc_Entry>();

            //LocalReport rpt1 = new LocalReport();
            //rpt1 =RptSetupClass1.GetLocalReport("RD_19_FGInv.RptProQc", lst, null, null);
            //rpt1.EnableExternalImages = true;
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("txtCurDate", "Date: "+ txtCurDate));
            //rpt1.SetParameters(new ReportParameter("todate", "QcDate: "+ todate));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "FG QC ENTRY"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //rpt1.SetParameters(new ReportParameter("GRRno","No:"+ GRRno));
            //rpt1.SetParameters(new ReportParameter("qcTime","Hour: "+ qcTime));
            //rpt1.SetParameters(new ReportParameter("refno","Qc No: "+ refno));
            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}