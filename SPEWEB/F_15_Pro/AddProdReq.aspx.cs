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

namespace SPEWEB.F_15_Pro
{
    public partial class AddProdReq : System.Web.UI.Page
    {
        ProcessAccess Budget = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (prevPage.Length == 0)
                //{
                //    prevPage = Request.UrlReferrer.ToString();
                //}
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "addreq") ? "Production Additional Requisition" :
                    (Type == "ReCutMatReq") ? "Recutting Material Requisition" : "Common Material Requisition";
               

                this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.GetMatGroup();
                this.LoadDataLC_Order();
                this.CommonButton();
                this.GetSeason();
                //this.GetMatList();
                //this.SelectMatList();
                this.GetRequisition();


            }
        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            
            //ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            //ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");
            
            if (ds1 == null)
                return;

            ddlSeason.DataTextField = "gdesc";
            ddlSeason.DataValueField = "gcod";
            ddlSeason.DataSource = ds1.Tables[0];
            ddlSeason.DataBind();
            ddlSeason.Items.Add(new ListItem { Value = "00000", Text = "---All---"});
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.ddlSeason.SelectedValue = season;
            }
            else
            {
                this.ddlSeason.SelectedValue = "00000";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFinalProUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value_Cost();
            this.DataBind();
        }

        private void CommonButton()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";

        }

        private void LoadDataLC_Order()
        {
            string comcod = GetCompCode();

            string findseason = (this.ddlSeason.SelectedValue.ToString() == "00000") ? "%" : this.ddlSeason.SelectedValue.ToString() + "%";
            string srch = "1601%";
            if (this.Request.QueryString["actcode"].ToString().Length > 0)
            {
                srch = this.Request.QueryString["actcode"].ToString() + "%";
            }
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", srch, "%", findseason, "", "", "", "", "", "");
            
            if (ds1 == null)
                return;

            DataView dv = ds1.Tables[0].DefaultView;
            if (Request.QueryString.AllKeys.Contains("dayid"))
            {
                if (this.Request.QueryString["dayid"].ToString().Length > 0)
                {
                    dv.RowFilter = "dayid='" + this.Request.QueryString["dayid"].ToString() + "'";
                }
            }
            this.ddlBatch.DataTextField = "styledesc2";
            this.ddlBatch.DataValueField = "stylecode2";
            this.ddlBatch.DataSource = dv.ToTable();
            this.ddlBatch.DataBind();

            //ViewState["tblordstyle"] = ds1.Tables[0];


            //DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETORDERNO", "%%", "", "", "", "", "", "", "", "");

            //if (ds1 == null)
            //    return;


            //this.ddlBatch.DataTextField = "mlcdesc";
            //this.ddlBatch.DataValueField = "mlccod";
            //this.ddlBatch.DataSource = ds1.Tables[1];
            //this.ddlBatch.DataBind();

            if (this.Request.QueryString["actcode"].ToString().Length > 0)
            {
                this.ddlBatch.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlBatch_SelectedIndexChanged(null, null);
                this.lbtnOk_Click(null, null);
            }

            this.ddlBatch_SelectedIndexChanged(null, null);

        }

        private void GetRequisition()
        {
            string comcod = GetCompCode();
            string mlccod = this.ddlBatch.SelectedValue.ToString().Substring(0,12);
            string dayid = this.ddlBatch.SelectedValue.ToString().Substring(36,8);
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GET_ORDER_WISE_REQUISION_LIST", mlccod, dayid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataView dv = ds1.Tables[0].DefaultView;
            this.ddlPreqno.DataTextField = "preqno1";
            this.ddlPreqno.DataValueField = "preqno";
            this.ddlPreqno.DataSource = dv.ToTable(true, "preqno1", "preqno");
            this.ddlPreqno.DataBind();
            ds1.Dispose();
            ViewState["tblreqinfo"] = ds1.Tables[0];
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPreqno.SelectedValue = this.Request.QueryString["genno"].ToString();
            }

        }

        protected void imgbtnBatsearch_Click(object sender, EventArgs e)
        {

            ///this.GetAccCode();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintReport();
        }

        private void PrintReport()
        {

            string comcod = this.GetCompCode();
            string pbmno = this.ddlBatch.SelectedValue.ToString();
            string DPRno = "";
            DataSet ds4 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RPTBATCHBUDGETRPT", pbmno, DPRno, "", "", "", "", "", "", "");
            if (ds4 == null)
            {


                return;
            }

            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                //DataTable dt = (DataTable)ds4.Tables[0];

                //ReportDocument rptinfo = new RMGiRPT.R_11_Pro.RptProdReq();
                //TextObject comName = rptinfo.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                //comName.Text = comnam;
                //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtPBM"] as TextObject;
                //rpttxtVoutype.Text = this.DDLMasterLC.SelectedItem.Text.ToString();

                //TextObject txtDPR = rptinfo.ReportDefinition.ReportObjects["txtDPR"] as TextObject;
                //txtDPR.Text = DPRno.ToString();
                //TextObject txtDate = rptinfo.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                //txtDate.Text ="Date : "+ this.txtbgddate.Text.ToString();

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rptinfo.SetDataSource(dt);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {

            }

        }

        protected void imgbtnsearch_Click(object sender, EventArgs e)
        {
            // this.GetProNo();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                //this.lblBatch.Text = this.ddlBatch.SelectedItem.Text.Trim();
                //this.lblBatch.Visible = true;
                //this.txtbgddate.Enabled = false;

                if (this.Request.QueryString["Type"].ToString() == "commonreq")
                {
                    this.GetMatGroup();

                    this.ddlBatch.Enabled = false;
                    this.ddlPreqno.Enabled = false;
                    this.CommonMaterialPanel.Visible = true;
                    HideColumns();
                }

                this.ShowBudgetedIncome();

                return;
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.CommonMaterialPanel.Visible = false;
                this.ddlBatch.Enabled = true;
                this.ddlPreqno.Enabled = true;
                this.MultiView1.ActiveViewIndex = -1;

                //this.ddlBatch.Visible = true;
                //this.ddlPreqno.Enabled = true;
                //this.txtbgddate.Enabled = true;
                //this.lblBatch.Visible = false;
                // this.imgbtnPreDPR.Visible = true;
            }

        }

        private void HideColumns()
        {
            if (gvCost.Columns.Count >= 7)
            {
                for (int i = gvCost.Columns.Count - 7; i < gvCost.Columns.Count -1; i++)
                {
                    gvCost.Columns[i].Visible = false;
                }
            }
        }

        private void ShowBudgetedIncome()
        {
            this.MultiView1.ActiveViewIndex = 0;
            ViewState.Remove("tblbbudgetcost");
            string comcod = this.GetCompCode();
            string batchcode = this.ddlBatch.SelectedValue.ToString().Substring(0,12);
            string dayid = this.ddlBatch.SelectedValue.ToString().Substring(36,8);
            string DPRno = this.ddlPreqno.SelectedValue.ToString();
            string reqdat = Convert.ToDateTime(this.txtbgddate.Text).ToString("dd-MMM-yyyy");
            string rqtype = "";
            switch (this.Request.QueryString["Type"].ToString())
            {
                case "commonreq":
                    rqtype = "COMMON";
                    break;
                case "addreq":
                    rqtype = "ADDITIONAL";
                    break;
                case "ReCutMatReq":
                    rqtype = "RECUTTING";
                    break;


            }
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GET_ORDER_WISE_REQ_INFO", batchcode, DPRno, rqtype, reqdat, "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblbbudgetcost"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
       
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "sircode = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();

            //if (dt.Rows.Count > 1)
            //{
            //    dt.Rows[0].Delete();
            //}

            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnSpecification_Click(null, null);

        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string procode = dt1.Rows[0]["procode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["procode"].ToString() == procode)
                {

                    procode = dt1.Rows[j]["procode"].ToString();
                    dt1.Rows[j]["prodesc"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["procode"].ToString() == procode)
                    {
                        dt1.Rows[j]["prodesc"] = "";
                    }


                    procode = dt1.Rows[j]["procode"].ToString();

                }

            }
            return dt1;

        }

        private void Data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["tblbbudgetcost"];
            this.gvCost.DataSource = dt1;

            this.gvCost.DataBind();

            //if (dt.Rows.Count == 0)
            //    return;
            //((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRBgdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdqty)", "")) ?
            //     0 : dt.Compute("sum(bgdqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRIssqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(issqty)", "")) ?
            //     0 : dt.Compute("sum(issqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRBbqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bbqty)", "")) ?
            //     0 : dt.Compute("sum(bbqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRProdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cresqty)", "")) ?
            //     0 : dt.Compute("sum(cresqty)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void gvBudgetRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label groupdesc = (Label)e.Row.FindControl("lblgvResDescRpt");
            //    Label amt = (Label)e.Row.FindControl("lblgvBgdamt");
            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Right(code, 4) == "AAAA")
            //    {

            //        groupdesc.Font.Bold = true;
            //        amt.Font.Bold = true;
            //        groupdesc.Style.Add("text-align", "right");
            //    }

            //}

        }


        protected void lbtnFinalProUpdate_Click(object sender, EventArgs e)
        {

            this.Save_Value_Cost();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtbgddate.Text.Trim()).ToString("dd-MMM-yyyy");
            string Batchcode = this.ddlBatch.SelectedValue.ToString();
            string DPRno = this.ddlPreqno.SelectedValue.ToString();
            bool result = false;
            DataTable tbl2 = (DataTable)ViewState["tblbbudgetcost"];
            string rqtype = "";
            switch (this.Request.QueryString["Type"].ToString())
            {
                case "commonreq":
                    rqtype = "COMMON";
                    break;
                case "addreq":
                    rqtype = "ADDITIONAL";
                    break;
                case "ReCutMatReq":
                    rqtype = "RECUTTING";
                    break;


            }

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string ProCode = tbl2.Rows[i]["procode"].ToString();
                string Rescode = tbl2.Rows[i]["rsircode"].ToString();
                string colorid = "000000000000";
                string sizeid = "000000000000";
                string spcfcode = tbl2.Rows[i]["spcfcod"].ToString();
                string cbqty = Convert.ToDouble(tbl2.Rows[i]["cresqty"].ToString().Trim()).ToString();

                if (Convert.ToDouble("0" + cbqty) > 0)
                    result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "INORUPDATEPBREQB", DPRno, ProCode, Rescode, cbqty, colorid, sizeid, spcfcode, date, rqtype, "", "", "", "", "", "");

            }
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
            }

            if (result == true && ConstantInfo.LogStatus == true)
            {

                string eventtype = rqtype + " Material Requisition";
                string eventdesc = "Production Material Requisition, Batch:" + Batchcode;
                string eventdesc2 = "DPR- " + DPRno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }
        }

        //protected void Save_Value_Cost()
        //{
        //    DataTable dt1 = (DataTable)ViewState["tblbbudgetcost"];
        //    int TblRowIndex, i;

        //    for (i = 0; i < this.gvCost.Rows.Count; i++)
        //    {
        //        double cbgdqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvcRProdqty")).Text.Trim());
        //        double percent = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvcPercent")).Text.Trim());
        //        TblRowIndex = (gvCost.PageIndex) * gvCost.PageSize + i;
        //        dt1.Rows[TblRowIndex]["cresqty"] = cbgdqty;
        //        dt1.Rows[TblRowIndex]["percnt"] = percent;
        //    }

        //    ViewState["tblbbudgetcost"] = dt1;
        //    this.Data_Bind();
        //}

        protected void Save_Value_Cost()
        {
            DataTable dt1 = (DataTable)ViewState["tblbbudgetcost"];
            int TblRowIndex, i;

            for (i = 0; i < this.gvCost.Rows.Count; i++)
            {
                TblRowIndex = (gvCost.PageIndex) * gvCost.PageSize + i;

                double ttlreq = Convert.ToDouble("0" + ((Label)this.gvCost.Rows[i].FindControl("lblgvTotalReq")).Text.Trim());
                double cbgdqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvcRProdqty")).Text.Trim());

                double percent = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvcPercent")).Text.Trim());

                if (percent != 0)
                {
                    
                    double totalRequisition = Convert.ToDouble("0" + ((Label)this.gvCost.Rows[i].FindControl("lblgvTotalReq")).Text.Trim());
                    double calculatedReqQty = (percent / 100) * totalRequisition;
                    if (ttlreq>0 && calculatedReqQty > ttlreq)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Required Qty overflow that Total Requisition Qty');", true);
                        return;
                    }

                    dt1.Rows[TblRowIndex]["cresqty"] = calculatedReqQty;
                    dt1.Rows[TblRowIndex]["percnt"] = percent;
                    
                }
                else
                {
                    if (ttlreq > 0 && cbgdqty > ttlreq)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Required Qty overflow that Total Requisition Qty');", true);
                        return;
                    }
                    TblRowIndex = (gvCost.PageIndex) * gvCost.PageSize + i;
                    dt1.Rows[TblRowIndex]["cresqty"] = cbgdqty;

                }

            }

            ViewState["tblbbudgetcost"] = dt1;
            this.Data_Bind();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBatch.SelectedValue.Length > 0)
            {
                this.GetRequisition();
            }

        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {



            //this.Panel2.Visible = true;
            this.Save_Value_Cost();
            DataTable tbl1 = (DataTable)ViewState["tblbbudgetcost"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();

                dr1["comcod"] = this.GetCompCode(); ;
                dr1["preqno"] = this.ddlPreqno.SelectedValue.ToString();
                dr1["preqno1"] = this.ddlPreqno.SelectedValue.ToString();
                dr1["procode"] = "499900101001";
                dr1["prodesc"] = "COMMON DEPARTMENT";
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
                dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select("sircode = '" + mResCode + "'");
                dr1["rsirunit"] = dr3[0]["sirunit"];
                dr1["currqqty"] = 0.0000;
                dr1["ttlreqqty"] = 0.0000;
                dr1["ttlrecutqty"] = 0.0000;
                dr1["ttladition"] = 0.0000;
                dr1["grandttl"] = 0.0000;
                dr1["percnt"] = 0.0000;
                dr1["cresqty"] = 0.0000;
                tbl1.Rows.Add(dr1);

            }

            ViewState["tblbbudgetcost"] = HiddenSameData(tbl1);
            this.Data_Bind();

        }

        protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadDataLC_Order();
        }


        private void GetMatGroup()
        {
            Session.Remove("tblresleb2");

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.Budget.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_RESCODE_LEVEL2_ISSUE", "04%", "", userid, "", "", "", "", "", "");
            Session["tblresleb2"] = ds1.Tables[0];
            ds1.Dispose();

            DataTable dt = ((DataTable)Session["tblresleb2"]).Copy();

            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "sircode";
            this.ddlcatagory.DataSource = dt;
            this.ddlcatagory.DataBind();
            this.ddlcatagory_SelectedIndexChanged(null, null);
        }


        private void GetMatList()
        {
            string comcod = this.GetCompCode();
            string CompGroup = this.ddlcatagory.SelectedValue.ToString() == "0000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            string matcode = CompGroup + "%";

            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GET_RESOURCE_WITH_SPECIFICATIONS", matcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlResList.Items.Clear();
                // this.ddlResSpcf.Items.Clear();
                return;
            }
            ViewState["tblMat"] = ds1.Tables[0];

            ViewState["tblSpcf"] = ds1.Tables[1];
            this.ddlResList.DataTextField = "sirdesc";
            this.ddlResList.DataValueField = "sircode";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();
            this.ddlResList_SelectedIndexChanged(null, null);

        }

        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMatList();
        }


        //protected void lbtnSelectAll_Click(object sender, EventArgs e)
        //{

        //    this.Save_Value_Cost();
        //    DataTable tbl1 = (DataTable)ViewState["tblIssue"];
        //    string mResCode = this.ddlResList.SelectedValue.ToString();
        //    string Specification = this.ddlResSpcf.SelectedValue.ToString();
        //    DataTable tbl2 = (DataTable)ViewState["tblMat"];

        //    for (int i = 0; i < tbl2.Rows.Count; i++)
        //    {
        //        DataRow[] dr3 = tbl1.Select("rsircode = '" + tbl2.Rows[i]["rsircode"].ToString() + "'");
        //        if (dr3.Length == 0)
        //        {
        //            DataRow dr1 = tbl1.NewRow();
        //            dr1["comcod"] = this.GetCompCode(); ;
        //            dr1["rsircode"] = tbl2.Rows[i]["rsircode"];
        //            dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
        //            dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
        //            dr1["rsirdesc"] = tbl2.Rows[i]["rsirdesc"];
        //            dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
        //            dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
        //            dr1["empid"] = this.ddlEmpList.SelectedValue.ToString();
        //            dr1["conqty"] = 0;
        //            dr1["conunt"] = this.ddlunit.SelectedValue.ToString();
        //            dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
        //            dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"];
        //            dr1["untcod"] = tbl2.Rows[i]["untcod"];
        //            dr1["stkqty"] = tbl2.Rows[i]["stkqty"];
        //            dr1["stkrate"] = tbl2.Rows[i]["stkrate"];
        //            dr1["issueqty"] = 0;
        //            dr1["issueamt"] = 0;
        //            dr1["remarks"] = "";

        //            tbl1.Rows.Add(dr1);
        //        }


        //    }

        //    ViewState["tblIssue"] = tbl1;
        //    this.Data_Bind();

        //}
    }


}