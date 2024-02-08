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
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_15_Pro
{
    public partial class ProStoreReceive : System.Web.UI.Page
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
                //this.txtQcDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.imgbtnStorid_Click(null, null);
                this.GetRECINFO();
                //this.GetProdNo();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "Entry") ? "Store Received Entry- FG" :
                    (this.Request.QueryString["Type"] == "EntrySemi") ? "Store Received Entry- Semi FG"
                    : (this.Request.QueryString["Type"] == "EntryRej") ? "Store Received Entry(Rejection)- FG" : "Store Received Entry(Rejection)- Semi FG";
                this.CommonButton();

                if (this.Request.QueryString["genno"].ToString() != "")
                {
                    lbtnOk_Click(null, null);
                }

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
        }


        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Calculation";


            if ((this.Request.QueryString["Type"] == "EntrySemiRej"))
            {
                this.gvGRR.Columns[8].HeaderText = "Reject Qty";
                this.gvGRR.Columns[10].HeaderText = "Reject Amount";
            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetRECINFO()
        {
            string comcod = this.GetCompCode();
            string filter3 = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%%" : this.Request.QueryString["genno"].ToString() + "%";
            string Type = (this.Request.QueryString["Type"] == "Entry" || this.Request.QueryString["Type"] == "EntryRej") ? "17" : "15";
            string RcvType = (this.Request.QueryString["Type"] == "Entry" || this.Request.QueryString["Type"] == "EntrySemi") ? "Fresh" : "Reject";
            string Date = this.Request.QueryString["date"].ToString();
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETQCLIST", filter3, Type, RcvType, Date, "", "", "", "", "");

            DataTable dt1 = ds1.Tables[0].Copy();
            dt1 = dt1.DefaultView.ToTable(true, "podvaluefield", "podtextfield");

            this.ddlProdinfo.DataTextField = "podtextfield";
            this.ddlProdinfo.DataValueField = "podvaluefield";
            this.ddlProdinfo.DataSource = dt1;
            this.ddlProdinfo.DataBind();
            ViewState["tblRecData"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlProdinfo_SelectedIndexChanged(null, null);
        }
        protected void ddlProdinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProdNo();
        }
        private void GetProdNo()
        {
            DataTable dt = (DataTable)ViewState["tblRecData"];
            string Prodno = this.ddlProdinfo.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("podvaluefield='" + Prodno + "'");

            DataTable dt1 = dv.ToTable().Copy();
            dt1 = dt1.DefaultView.ToTable(true, "qcvaluefield", "qctextfield");


            this.ddlQcNO.DataTextField = "qctextfield";
            this.ddlQcNO.DataValueField = "qcvaluefield";
            this.ddlQcNO.DataSource = dt1;
            this.ddlQcNO.DataBind();
            //ds1.Dispose();
        }
        protected void imgbtnStorid_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter2 = "%" + "%";
            DataTable dt;
            string sType = (this.Request.QueryString["reptype"].Length == 0) ? "%" : this.Request.QueryString["reptype"].ToString();
            //Store Change by Chairman for Date: 15-May-2015
            string EntryType = (this.Request.QueryString["Type"] == "Entry") ? "17%" : "1[5]%";
            DataSet ds5 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "RETRIVEFGSTOREALL", filter2, EntryType, "", "", "", "", "", "", "");

            DataView dv = (ds5.Tables[0]).Copy().DefaultView;
            dv.RowFilter = "stype like '" + sType + "'";
            dt = dv.ToTable();

            if (dt.Rows.Count == 0)
            {
                dt = ds5.Tables[0];
            }

            dt.Rows.Add(comcod, "000000000000", "", "-------------------- Select Store --------------------", "");

            this.ddlStorid.DataTextField = "actdesc1";
            this.ddlStorid.DataValueField = "actcode";
            this.ddlStorid.DataSource = dt;
            this.ddlStorid.DataBind();
            this.ddlStorid.SelectedValue = "000000000000";

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
                //if (this.ddlQcNO.Items.Count > 0)
                //    this.lblddlQcNO.Text = this.ddlQcNO.SelectedItem.Text;
                this.ddlProdinfo.Enabled = false;
                this.plnQc.Visible = true;
                this.PanelOther.Visible = true;
                //this.lblPreviousGrr.Visible = false;
                //this.txtSrchGRR.Visible = false;
                this.ImgbtnFindGrrList.Visible = false;
                this.ddlPrevGRRList.Visible = false;
                this.ShowGrrInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.ddlPrevGRRList.Items.Clear();
            this.ddlProdinfo.Enabled = true;
            this.plnQc.Visible = false;
            //this.lblPreviousGrr.Visible = true;
            //this.txtSrchGRR.Visible = true;
            this.ImgbtnFindGrrList.Visible = true;
            this.ddlPrevGRRList.Visible = true;
            this.txtCurDate.Enabled = true;
            this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            this.gvGRR.DataSource = null;
            this.lblmsg.Text = "";
            this.txtMRFNo.Text = "";
            this.gvGRR.DataBind();
            this.GetRECINFO();
            this.lblmsg.Visible = false;
            this.PanelOther.Visible = false;
        }

        private void GetGRRNO()
        {

            string comcod = this.GetCompCode();
            string mGRRNo = "NEWGRR";
            if (this.ddlPrevGRRList.Items.Count > 0)
                mGRRNo = this.ddlPrevGRRList.SelectedValue.ToString();

            string CurDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            if (mGRRNo == "NEWGRR")
            {
                DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_RECEIVE", "GETLASTGRRNO", CurDate,
                       "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxmgrrno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxmgrrno1"].ToString().Substring(6, 5);
                    this.ddlPrevGRRList.DataTextField = "maxmgrrno1";
                    this.ddlPrevGRRList.DataValueField = "maxmgrrno";
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
            string txtPRDno = this.ddlQcNO.SelectedValue.Trim().ToString();
            string txtgrrno = this.ddlPrevGRRList.SelectedValue.Trim().ToString();
            string mGRRNo = "NEWGRR";
            if (this.ddlPrevGRRList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mGRRNo = this.ddlPrevGRRList.SelectedValue.ToString();
            }
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "SHOWRECEVIEDLIST", mGRRNo, txtPRDno, txtgrrno, "", "", "", "", "", "");

            if (ds1 == null)
                return;

            ViewState["tblGRR"] = HiddenSameData(ds1.Tables[0]);

            if (mGRRNo == "NEWGRR")
            {
                ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_RECEIVE", "GETLASTGRRNO", CurDate, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxmgrrno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxmgrrno1"].ToString().Substring(6, 5);
                }
                //for New PBPNO 
                //string mPBPNo01 = this.lblCurPBPNo1.Text.Trim().Substring(0, 3) + this.txtDateto.Text.Trim().Substring(7, 4) + this.lblCurPBPNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();// this.txtReqNo.Text.Trim();
                //ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURBGDPREPARATION", "GETPBPINFO", StoreCode, date1, date2, mPBPNo01, "", "", "", "", "");
                //Session["tblPBP"] = ds1.Tables[0];
                this.Data_Bind();

                return;

            }

            //Load Production Number;

            //if (ds1.Tables[1].Rows.Count > 0)
            //{
            //    this.ddlQcNO.DataTextField = "proddesc";
            //    this.ddlQcNO.DataValueField = "prodno";
            //    this.ddlQcNO.DataSource = ds1.Tables[1];
            //    this.ddlQcNO.DataBind();

            //}
            //this.ddlQcNO.SelectedValue = ds1.Tables[0].Rows[0]["prodno"].ToString();
            this.ddlStorid.SelectedValue = ds1.Tables[0].Rows[0]["storid"].ToString();
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["grrref"].ToString();
            this.txtOrderNarr.Text = ds1.Tables[1].Rows[0]["grremarks"].ToString();

            if (this.ddlPrevGRRList.Items.Count > 0)
            {
                this.lblCurNo1.Text = this.ddlPrevGRRList.SelectedItem.Text.Substring(0, 6);//ds1.Tables[1].Rows[0]["pbpno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = this.ddlPrevGRRList.SelectedItem.Text.Substring(6, 5);//ds1.Tables[1].Rows[0]["pbpno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(this.ddlPrevGRRList.SelectedItem.Text.ToString().Substring(12, 11)).ToString("dd-MMM-yyyy");
                //this.lblddlQcNO.Text = this.ddlQcNO.SelectedItem.Text.Trim();
                //this.lblddlQcNO.Visible = true;
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

        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tblGRR"];

            ((Label)this.gvGRR.FooterRow.FindControl("lblFRecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(grrqty)", "")) ? 0.00 : dt.Compute("Sum(grrqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFRecAmtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(grramt)", "")) ? 0.00 : dt.Compute("Sum(grramt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFQcqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qcqty)", "")) ? 0.00 : dt.Compute("Sum(qcqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvGRR.FooterRow.FindControl("lblFQcAmtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qcamt)", "")) ? 0.00 : dt.Compute("Sum(qcamt)", ""))).ToString("#,##0;(#,##0); ");

        }

        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tblGRR"];
            int TblRowIndex2;
            this.lblmsg.Text = "";
            for (int j = 0; j < this.gvGRR.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvGRR.PageSize) * (this.gvGRR.PageIndex) + j;


                double lblgvQcQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvGRR.Rows[j].FindControl("lblgvQcQty")).Text.Trim()));
                double lblgvRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvGRR.Rows[j].FindControl("lblgvRate")).Text.Trim()));

                double lblgvQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvGRR.Rows[j].FindControl("lblgvQty")).Text.Trim()));

                if (lblgvQcQty < lblgvQty)
                {
                    this.lblmsg.Visible = true;
                    this.lblmsg.Text = "Receive Qty Not More then QC Qty";
                    break;

                }


                double dgvRcmt = lblgvQty * lblgvRate;

                ((TextBox)this.gvGRR.Rows[j].FindControl("lblgvQty")).Text = lblgvQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvGRR.Rows[j].FindControl("lblgvAmt")).Text = dgvRcmt.ToString("#,##0.00;(#,##0.00); ");



                tbl1.Rows[TblRowIndex2]["grrqty"] = lblgvQty;
                tbl1.Rows[TblRowIndex2]["grramt"] = dgvRcmt;
            }
            ViewState["tblGRR"] = tbl1;
        }
        protected void lbtnSelectQc_Click(object sender, EventArgs e)
        {

            //this.Panel2.Visible = true;
            this.Save_Value();
            DataTable tbl1 = (DataTable)ViewState["tblGRR"];
            string Prodno = ASTUtility.Left(this.ddlProdinfo.SelectedValue.ToString(), 14);
            string qcno = this.ddlQcNO.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("prodno = '" + Prodno + "' and pqcno='" + qcno + "'");

            if (dr2.Length == 0)
            {


                string ProdnoBatch = this.ddlProdinfo.SelectedValue.ToString();
                DataTable tbl2 = (DataTable)ViewState["tblRecData"];
                DataView dv = tbl2.DefaultView;
                dv.RowFilter = ("podvaluefield='" + ProdnoBatch + "' and qcvaluefield='" + qcno + "'");
                tbl2 = dv.ToTable();

                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();

                    dr1["prodno"] = tbl2.Rows[i]["prodno"].ToString();
                    dr1["pqcno"] = tbl2.Rows[i]["pqcno"].ToString();
                    dr1["batchcode"] = tbl2.Rows[i]["batchcode"].ToString();
                    dr1["batchdesc"] = tbl2.Rows[i]["batchdesc"].ToString();
                    dr1["prodcode"] = tbl2.Rows[i]["prodcode"].ToString();
                    dr1["rsirdesc"] = tbl2.Rows[i]["rsirdesc"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["qcqty"] = Convert.ToDouble(tbl2.Rows[i]["balrecqty"]).ToString();
                    //dr1["tsrecqty"] = Convert.ToDouble(tbl2.Rows[i]["tsrecqty"]).ToString();
                    dr1["qcamt"] = Convert.ToDouble(tbl2.Rows[i]["balrecamt"]).ToString();

                    dr1["grrrate"] = Convert.ToDouble(tbl2.Rows[i]["balrate"]).ToString();
                    //dr1["balrecqty"] = Convert.ToDouble(tbl2.Rows[i]["balrecqty"]).ToString();
                    dr1["grrqty"] = 0.00;
                    dr1["grramt"] = 0.00;




                    tbl1.Rows.Add(dr1);
                }




            }

            ViewState["tblGRR"] = this.HiddenSameData(tbl1);
            this.Data_Bind();

        }



        protected void ImgbtnFindpbpno_Click1(object sender, EventArgs e)
        {
            this.GetProdNo();
        }
        protected void ImgbtnFindGrrList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string txtsrcgrr = "%";
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_RECEIVE", "GETPREGRRLIST", txtsrcgrr, "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            this.ddlPrevGRRList.DataTextField = "grrdesc";
            this.ddlPrevGRRList.DataValueField = "grrno";
            this.ddlPrevGRRList.DataSource = ds2.Tables[0];
            this.ddlPrevGRRList.DataBind();

        }
        protected void gvGRR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void btnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
        protected void lbtnUpdatePurPrepar_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            // return;

            this.lblmsg.Visible = true;
            DataTable tbl1 = (DataTable)ViewState["tblGRR"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            string txtDPRno = ASTUtility.Left(this.ddlProdinfo.SelectedValue.ToString(), 14);
            string GRRno = "";
            if (this.ddlPrevGRRList.Items.Count == 0)
                this.GetGRRNO();


            GRRno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string refno = this.txtMRFNo.Text.Trim();
            string storid = this.ddlStorid.SelectedValue.ToString();
            if (storid == "000000000000")
            {
                this.lblmsg.Text = "Please Select Store";
                return;
            }

            string Remarks = this.txtOrderNarr.Text;

            DataTable tbl2 = (DataTable)ViewState["tblGRR"];


            double recAmt = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(grrqty)", "")) ? 0.00 : tbl2.Compute("sum(grrqty)", ""))); //Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(grrqty)", ""))));
            if (recAmt == 0)
            {
                this.lblmsg.Text = "Please Input Receied Qty";
                return;
            }
            string RcvType = "";
            if ((this.Request.QueryString["Type"] == "EntrySemiRej"))
            {
                RcvType = "Reject";
            }
            else
            {
                RcvType = "Fresh";
            }




            bool result = false;
            result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "INORUPDATEPROGRRB", GRRno, txtDPRno, date, refno, storid, userid, Terminal, Sessionid, Postdat, Remarks, RcvType, "", "", "", "");

            if (!result)
            {
                this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
                this.lblmsg.BackColor = System.Drawing.Color.Red;
                this.lblmsg.ForeColor = System.Drawing.Color.White;
                return;
            }



            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string pqcno = tbl2.Rows[i]["pqcno"].ToString();
                string ProdCode = tbl2.Rows[i]["prodcode"].ToString();
                string spcfcod = tbl2.Rows[i]["spcfcod"].ToString();
                string Batchcode = tbl2.Rows[i]["batchcode"].ToString();
                string grrqty = Convert.ToDouble(tbl2.Rows[i]["grrqty"].ToString().Trim()).ToString();
                string grramt = Convert.ToDouble(tbl2.Rows[i]["grramt"].ToString().Trim()).ToString();
                string qcqty = Convert.ToDouble(tbl2.Rows[i]["qcqty"].ToString().Trim()).ToString();
                string qcamt = Convert.ToDouble(tbl2.Rows[i]["qcamt"].ToString().Trim()).ToString();

                result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "INORUPDATEPROGRRA", GRRno, ProdCode, grrqty, grramt, Batchcode, qcqty, qcamt, pqcno, spcfcod, "", "", "", "", "", "");

                if (!result)
                {
                    this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
                    this.lblmsg.BackColor = System.Drawing.Color.Red;
                    this.lblmsg.ForeColor = System.Drawing.Color.White;

                    return;
                }

                this.lblmsg.Text = "Update Successfully !";
            }

        }
        protected void lnkbtnUpdate_Prod_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string txtDPRno = this.ddlProNO.SelectedValue.ToString();
            string txtDPRno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();


            bool result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "UPDATEQCJV", txtDPRno, date, userid, Terminal, Sessionid, Postdat, "", "", "", "", "", "", "", "");
            if (!result)
            {
                this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
                this.lblmsg.BackColor = System.Drawing.Color.Red;
                this.lblmsg.ForeColor = System.Drawing.Color.White;
                return;
            }
            this.lblmsg.Visible = true;
            this.lblmsg.Text = "Update Successfully !";
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.lbtnUpdatePurPrepar_Click(null, null);
            string Type = this.Request.QueryString["Type"].ToString();
            if (Type == "EntrySemi")
            {
                //  this.lnkbtnUpdate_Prod_Click(null, null);
            }

        }


        protected void gvGRR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblGRR"];
            string mGRNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string pqcno = ((Label)this.gvGRR.Rows[e.RowIndex].FindControl("lblpqcno")).Text.Trim();
            string rescode = ((Label)this.gvGRR.Rows[e.RowIndex].FindControl("lblprodcode")).Text.Trim();
            bool result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "DELETERECEIVEITM",
                        mGRNO, pqcno, rescode, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvGRR.PageSize) * (this.gvGRR.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("pqcno<>'' and prodcode<>''");
                ViewState["tblGRR"] = dv.ToTable();
                this.Data_Bind();
            }
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string CurDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Prodno = ASTUtility.Left(this.ddlProdinfo.SelectedValue.ToString(), 14);
            //string storid = this.ddlStorid.SelectedItem.Text.Trim().ToString();
            //string GRRno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            //string refno = this.txtMRFNo.Text.Trim();
            //string qcno = this.ddlQcNO.SelectedValue.ToString();


            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //DataTable dt = (DataTable)ViewState["tblGRR"];

            //var lst = dt.DataTableToList<MFGOBJ.C_19_FGInv.Store_Recv_Entry>();

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass1.GetLocalReport("RD_19_FGInv.RptStoreRecv", lst, null, null);

            //rpt1.EnableExternalImages = true;
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("CurDate", "Date: " + CurDate));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "STORE RECEIVED ENTRY- FG"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //rpt1.SetParameters(new ReportParameter("GRRno", "No:" + GRRno));

            //rpt1.SetParameters(new ReportParameter("refno", "Ref NO: " + refno));
            //rpt1.SetParameters(new ReportParameter("Prodno", "Production: " + Prodno));
            //rpt1.SetParameters(new ReportParameter("storid", "Store List: " + storid));
            //rpt1.SetParameters(new ReportParameter("qcno", "Qc List: " + qcno));

            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




    }
}