using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.Web.UI;
using System.Collections.Generic;

namespace SPEWEB.F_15_Pro
{
    public partial class PurMRREntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = (Convert.ToBoolean(dr1[0]["entry"]));
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                this.txtCurMRRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtChlDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Entry") ? "Materials Receive Information Input/Edit Screen"
                    : "Materials Receive Information Input/Edit Screen";
                this.Load_Location();
                // this.ImgbtnFindProject_Click(null, null);

                if (this.Request.QueryString["Type"].ToString() == "Mgt")
                {
                    this.ImgbtnPreMRR_Click(null, null);

                }
                else
                {
                    this.FindOrderList();

                }

                this.CommonButton();



            }
        }
        private void CommonButton()
        {

            //  ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Delete Selected Item";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).OnClientClick = "return confirm('Do you want to remove selected item?')";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).CssClass = "btn btn-danger btn-sm";


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateMRR_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnResFooterTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lbtnDelMRR_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(deleteSelectedItem_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void deleteSelectedItem_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string MRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            for (int i = 0; i < this.gvMRRInfo.Rows.Count; i++)
            {
                if (((CheckBox)this.gvMRRInfo.Rows[i].FindControl("chkCol")).Checked)
                {
                    DataTable dt = (DataTable)ViewState["tblMRR"];
                    DataView dv = dt.DefaultView;
                    string reqno = ((Label)this.gvMRRInfo.Rows[i].FindControl("lblgvReqnomain")).Text.Trim();
                    string rescode = ((Label)this.gvMRRInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
                    string spcfcod = ((Label)this.gvMRRInfo.Rows[i].FindControl("lblgvSpcfCod")).Text.Trim();
                    string bomid = ((Label)this.gvMRRInfo.Rows[i].FindControl("lblgvBomNo")).Text.Trim();
                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMRRMAT", MRRNO, reqno, rescode, spcfcod, bomid, "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {

                        dv.RowFilter = ("reqno+rsircode+spcfcod+bomid <>'" + reqno + rescode + spcfcod + bomid + "'");
                        ViewState["tblMRR"] = dv.ToTable();
                    }

                }
            }

            this.gvMRRInfo_DataBind();

        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
            return (hst["comcod"].ToString());
        }


        protected void Load_Location()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMATLOCATION", "", "%", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);


                return;
            }
            ViewState["tblLocation"] = ds1.Tables[0];

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
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string unitname = "";
            string mMRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNO, "",
                     "", "", "", "", "", "", "");
            string mrrno1 = ds1.Tables[1].Rows[0]["mrrno1"].ToString();
            string mrrdat = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd-MMM-yyyy");
            string MrrRef = ds1.Tables[1].Rows[0]["orderno1"].ToString();
            string chlnno = ds1.Tables[0].Rows[0]["reqno1"].ToString();
            string pactdesc = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string ssirdesc = ds1.Tables[1].Rows[0]["ssirdesc1"].ToString();

            string preparedby = ds1.Tables[1].Rows[0]["username"].ToString();

            LocalReport rpt1 = new LocalReport();

            var list1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.PurchaseMRR>();

            rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPurMRR", list1, null, null);


            rpt1.EnableExternalImages = true;///
            rpt1.SetParameters(new ReportParameter("comnam", comnam + unitname));
            rpt1.SetParameters(new ReportParameter("RptTitle", "GOODS RECEIVING NOTE"));
            rpt1.SetParameters(new ReportParameter("mrrno1", ": " + mrrno1));
            rpt1.SetParameters(new ReportParameter("mrrdat", ": " + mrrdat));
            rpt1.SetParameters(new ReportParameter("MrrRef", ": " + MrrRef));
            rpt1.SetParameters(new ReportParameter("chlnno", ": " + chlnno));
            rpt1.SetParameters(new ReportParameter("pactdesc", ": " + pactdesc));
            rpt1.SetParameters(new ReportParameter("ssirdesc", ": " + ssirdesc));
            rpt1.SetParameters(new ReportParameter("preparedby", preparedby));
            rpt1.SetParameters(new ReportParameter("approvedby", ""));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ImgbtnPreMRR_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string SearchMrr = this.txtSrchPreMRR.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPREVMRRLIST", CurDate1, SearchMrr, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);


                return;
            }

            this.ddlPrevMRRList.Items.Clear();
            this.ddlPrevMRRList.DataTextField = "mrrno1";
            this.ddlPrevMRRList.DataValueField = "mrrno";
            this.ddlPrevMRRList.DataSource = ds1.Tables[0];
            this.ddlPrevMRRList.DataBind();
            if (this.Request.QueryString["Type"].ToString() == "Mgt")
            {
                this.ddlPrevMRRList.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.ddlPrevMRRList.Enabled = false;
                this.lbtnOk_Click(null, null);
            }



        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lblPreMRR.Visible = true;
                //this.txtSrchPreMRR.Visible = true;
                //this.ImgbtnPreMRR.Visible = true;
                this.ddlPrevMRRList.Visible = true;
                this.ddlPrevMRRList.Items.Clear();
                //this.ddlProject.Visible = true;
                //this.lblddlProject.Visible = false;

                //this.ddlBBLCList.Enabled = true;
                //this.ImgbtnFindBBLC.Enabled = true;
                this.lblCurMRRNo1.Text = "MRR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMRRDate.Enabled = true;
                this.txtMRRRef.Text = "";

                this.ddlOrderList.Enabled = true;

                this.ddlResList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtMRRNarr.Text = "";
                this.gvMRRInfo.DataSource = null;
                this.gvMRRInfo.DataBind();
                this.Panel1.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.txtChalanNo.Text = "";
                ImgbtnFindBBLC_Click(null, null);
                this.LbtnReqItemShow_Click(null, null);
                return;
            }

            this.lblPreMRR.Visible = false;
            this.txtSrchPreMRR.Visible = false;
            this.ImgbtnPreMRR.Visible = false;
            this.ddlPrevMRRList.Visible = false;

            //  this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            // this.ddlProject.Visible = false;
            // this.lblddlProject.Visible = true;

            //this.ddlBBLCList.Enabled = false;
            //this.ImgbtnFindBBLC.Enabled = false;
            this.txtCurMRRNo2.ReadOnly = true;
            this.ddlOrderList.Enabled = false;
            this.Panel1.Visible = true;
            this.lbtnOk.Text = "New";
            this.ImgbtnFindRes_Click(null, null);

            this.Get_Receive_Info();

        }



        protected void Session_tblMRR_Update()
        {

            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvMRRInfo.Rows.Count; j++)
            {
                double dgvOrderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvOrderQty")).Text.Trim()));
                double dgvMRRQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvMRRQty")).Text.Trim()));
                double dgvMRRRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvMRRRate")).Text.Trim()));
                double dgvChlnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvChlnqty")).Text.Trim()));
                string dgvMRRNote = ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvMRRNote")).Text.Trim();
                double dgvMRRAmt = dgvMRRQty * dgvMRRRate;
                double Balqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvOrderBal")).Text.Trim()));

                string gvRack = ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvRack")).Text.Trim();
                // string gvLoc = ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvLoc")).Text.Trim();

                string loction = ((DropDownList)this.gvMRRInfo.Rows[j].FindControl("ddlval")).SelectedValue.ToString();

                // double dgvOrderBal = dgvOrderQty - dgvMRRQty;
                if (Balqty >= dgvMRRQty)
                {
                    TblRowIndex2 = (this.gvMRRInfo.PageIndex) * this.gvMRRInfo.PageSize + j;
                    tbl1.Rows[TblRowIndex2]["mrrqty"] = dgvMRRQty;
                    tbl1.Rows[TblRowIndex2]["mrrrate"] = dgvMRRRate;
                    tbl1.Rows[TblRowIndex2]["mrramt"] = dgvMRRAmt;
                    tbl1.Rows[TblRowIndex2]["mrrnote"] = dgvMRRNote;
                    tbl1.Rows[TblRowIndex2]["chlnqty"] = dgvChlnQty;
                    tbl1.Rows[TblRowIndex2]["rackno"] = gvRack;
                    tbl1.Rows[TblRowIndex2]["location"] = loction;

                }

                else
                {
                    TblRowIndex2 = (this.gvMRRInfo.PageIndex) * this.gvMRRInfo.PageSize + j;
                    tbl1.Rows[TblRowIndex2]["mrrqty"] = dgvMRRQty;
                    tbl1.Rows[TblRowIndex2]["mrrrate"] = dgvMRRRate;
                    //tbl1.Rows[TblRowIndex2]["mrramt"] = dgvMRRAmt;
                    tbl1.Rows[TblRowIndex2]["mrrnote"] = dgvMRRNote;
                    tbl1.Rows[TblRowIndex2]["chlnqty"] = dgvChlnQty;
                    tbl1.Rows[TblRowIndex2]["rackno"] = gvRack;
                    tbl1.Rows[TblRowIndex2]["location"] = loction;
                    //   ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('MRR Qty  greater or equal Balance Qty');", true);

                    // return;

                }

            }

            ViewState["tblMRR"] = tbl1;
        }

        protected void gvMRRInfo_DataBind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            this.gvMRRInfo.DataSource = tbl1;
            this.gvMRRInfo.DataBind();
            if (this.Request.QueryString["Type"].ToString() == "Entry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;


                if (this.ddlPrevMRRList.Items.Count > 0)
                {
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    // ((LinkButton)this.gvMRRInfo.FooterRow.FindControl("lbtnUpdateMRR")).Visible = false;
                    //   ((LinkButton)this.gvMRRInfo.FooterRow.FindControl("lbtnResFooterTotal")).Visible = false;
                }

            }
            else
            {

                this.gvMRRInfo.AutoGenerateDeleteButton = false;

            }

            if (tbl1.Rows.Count == 0)
                return;

            ((Label)this.gvMRRInfo.FooterRow.FindControl("lblFtrTtlArvlQty")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(mrrqty)", "")) ?
                   0.00 : tbl1.Compute("Sum(mrrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMRRInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(mrramt)", "")) ?
                   0.00 : tbl1.Compute("Sum(mrramt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvMRRInfo.PageSize);
            //((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            //for (int i = 1; i <= TotalPage; i++)
            //    ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            //if (TotalPage > 1)
            //    ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
            //((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvMRRInfo.PageIndex;

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
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTMRRINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                    return;
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMRRNo1.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(0, 6);
                    this.txtCurMRRNo2.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(6, 5);
                    this.ddlPrevMRRList.DataTextField = "maxmrrno1";
                    this.ddlPrevMRRList.DataValueField = "maxmrrno";
                    this.ddlPrevMRRList.DataSource = ds1.Tables[0];
                    this.ddlPrevMRRList.DataBind();
                }

            }






        }



        protected void Get_Receive_Info()
        {
            Session.Remove("tblorderno");
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = "NEWMRR";
            if (this.ddlPrevMRRList.Items.Count > 0)
            {

                this.txtCurMRRDate.Enabled = false;
                mMRRNo = this.ddlPrevMRRList.SelectedValue.ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, CurDate1,
                          "", "", "", "", "", "", "");


            ViewState["tblMRR"] = ds1.Tables[0];

            ViewState["UserLog"] = ds1.Tables[1];

            if (mMRRNo == "NEWMRR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTMRRINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMRRNo1.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(0, 6);
                    this.txtCurMRRNo2.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(6, 5);
                }


                return;
            }

            //this.Load_Project_Combo();
            if (ds1.Tables[1].Rows.Count > 0)
            {


                //Order
                this.ddlOrderList.DataTextField = "orderno1";
                this.ddlOrderList.DataValueField = "orderno";
                this.ddlOrderList.DataSource = ds1.Tables[1];
                this.ddlOrderList.DataBind();
            }

            //   this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            // this.ImgbtnFindSup_Click(null, null);

            //  this.ddlBBLCList.SelectedValue = ds1.Tables[1].Rows[0]["bblccode"].ToString();
            // this.FindOrderList();                
            if (ds1.Tables[1].Rows.Count <= 0)
                return;
            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["orderno"].ToString();

            this.txtMRRRef.Text = ds1.Tables[1].Rows[0]["mrrref"].ToString();
            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["orderno"].ToString();
            this.lblCurMRRNo1.Text = ds1.Tables[1].Rows[0]["mrrno1"].ToString().Substring(0, 6);
            this.txtCurMRRNo2.Text = ds1.Tables[1].Rows[0]["mrrno1"].ToString().Substring(6, 5);
            this.txtCurMRRDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd.MM.yyyy");
            // this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["mrrbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtMRRNarr.Text = ds1.Tables[1].Rows[0]["mrrnar"].ToString();
            this.txtChalanNo.Text = ds1.Tables[1].Rows[0]["chlnno"].ToString();
            this.gvMRRInfo_DataBind();

            //var List = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.LocalPORCV>();

        }

        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            string comcod = this.GetCompCode();
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            string mReqno = this.ddlResList.SelectedValue.ToString().Substring(0, 14);
            string mResCode = this.ddlResList.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCode = this.ddlResList.SelectedValue.ToString().Substring(26, 12);
            string bomid = this.ddlResList.SelectedValue.ToString().Substring(38, 10);
            DataRow[] dr2 = tbl1.Select("reqno='" + mReqno + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "' and bomid='" + bomid + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["reqno"] = mReqno;
                dr1["rsircode"] = mResCode;
                dr1["spcfcod"] = mSpcfCode;
                dr1["bomid"] = bomid;
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select(" reqno='" + mReqno + "'and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "' and bomid='" + bomid + "'");
                dr1["reqno1"] = dr3[0]["reqno1"];
                dr1["rsirdesc1"] = dr3[0]["rsirdesc2"];
                dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["orderqty"] = dr3[0]["orderqty"];
                dr1["recup"] = dr3[0]["recup"];
                dr1["orderbal"] = dr3[0]["balqty"];
                dr1["mrrqty"] = comcod == "5305" || comcod == "5306" ? 0.00 : Convert.ToDouble(dr3[0]["balqty"]);
                dr1["mrrrate"] = dr3[0]["mrrrate"];
                dr1["mrramt"] = Convert.ToDouble(dr3[0]["balqty"]) * Convert.ToDouble(dr3[0]["mrrrate"]);
                dr1["mrrnote"] = "";
                dr1["chlnqty"] = 0;
                dr1["rackno"] = "";
                dr1["location"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblMRR"] = tbl1;
            this.gvMRRInfo_DataBind();
            this.adjustment();

        }


        protected void lbtnSelectResAll_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            string comcod = this.GetCompCode();
            string mReqno = this.ddlResList.SelectedValue.ToString().Substring(0, 14);
            string mResCode = this.ddlResList.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCode = this.ddlResList.SelectedValue.ToString().Substring(26, 12);
            string bomid = this.ddlResList.SelectedValue.ToString().Substring(36, 10);
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataTable tbl2 = (DataTable)ViewState["tblMat"];


            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                DataRow[] dr = tbl1.Select("reqno='" + tbl2.Rows[i]["reqno"].ToString() + "' and  rsircode = '" + tbl2.Rows[i]["rsircode"].ToString() + "' and spcfcod = '" + tbl2.Rows[i]["spcfcod"].ToString() + "' and bomid='" + tbl2.Rows[i]["bomid"].ToString() + "'");
                if (dr.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Added');", true);

                    continue;
                }
                DataRow dr1 = tbl1.NewRow();
                dr1["reqno"] = tbl2.Rows[i]["reqno"].ToString();
                dr1["reqno1"] = tbl2.Rows[i]["reqno1"].ToString();
                dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                dr1["bomid"] = tbl2.Rows[i]["bomid"].ToString();
                dr1["rsirdesc1"] = tbl2.Rows[i]["rsirdesc2"].ToString();
                dr1["size"] = tbl2.Rows[i]["size"].ToString();
                dr1["color"] = tbl2.Rows[i]["color"].ToString();
                dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                dr1["orderqty"] = tbl2.Rows[i]["orderqty"].ToString();
                dr1["recup"] = Convert.ToDouble(tbl2.Rows[i]["recup"]).ToString();
                dr1["orderbal"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                dr1["mrrqty"] = comcod == "5305" || comcod == "5306" ? 0.00 : Convert.ToDouble(tbl2.Rows[i]["balqty"]);
                dr1["mrrrate"] = Convert.ToDouble("0" + tbl2.Rows[i]["mrrrate"]).ToString();
                dr1["mrramt"] = Convert.ToDouble("0" + tbl2.Rows[i]["balqty"]) * Convert.ToDouble("0" + tbl2.Rows[i]["mrrrate"]);
                dr1["mrrnote"] = "";
                dr1["chlnqty"] = 0;
                dr1["rackno"] = "";
                dr1["location"] = "";
                tbl1.Rows.Add(dr1);

                ViewState["tblMRR"] = tbl1;
            }
            this.gvMRRInfo_DataBind();
            this.adjustment();




        }
        private void adjustment()
        {
            DataTable dt = (DataTable)ViewState["tblMRR"];
            var List = dt.DataTableToList<SPEENTITY.C_09_Commer.LocalPORCV>();

            List<SumClass> result = new List<SumClass>();
            if (List.Count > 0)
            {

                result = List
         .GroupBy(l => new { l.rsircode, l.spcfcod })
         .Select(cl => new SumClass
         {
             rsircode = cl.First().rsircode,
             rsirdesc1 = cl.First().rsirdesc1,
             spcfcod = cl.First().spcfcod,
             spcfdesc = cl.First().spcfdesc,
             color = cl.First().color,
             size = cl.First().size,
             rsirunit = cl.First().rsirunit,
             orderbal = cl.Sum(c => c.orderbal),
             recup = cl.Sum(c => c.recup),
             ordrqty = cl.Sum(c => c.orderqty),
             mrrqty = cl.Sum(c => c.mrrqty),
         }).ToList();
            }
            Session["TblReceivesum"] = result;
            this.gvRecItem_DataBind();
        }

        private void gvRecItem_DataBind()
        {
            var listsum = (List<SumClass>)Session["TblReceiveSum"];
            this.gvRecItem.DataSource = listsum;
            this.gvRecItem.DataBind();

            ((Label)this.gvRecItem.FooterRow.FindControl("lgvRSumSMRRQty")).Text = (listsum.Select(p => p.mrrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.mrrqty).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRecItem.FooterRow.FindControl("lblTotalBalQty")).Text = (listsum.Select(p => p.orderbal).Sum() == 0.00) ? "0" : listsum.Select(p => p.orderbal).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRecItem.FooterRow.FindControl("lblTotalReceived")).Text = (listsum.Select(p => p.recup).Sum() == 0.00) ? "0" : listsum.Select(p => p.recup).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRecItem.FooterRow.FindControl("lblTotalOrdQty")).Text = (listsum.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.ordrqty).Sum().ToString("#,##0.00;(#,##0.00); ");

        }

        class SumClass
        {
            public string rsircode { get; set; }
            public string rsirdesc1 { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string bomid { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public string rsirunit { get; set; }
            public double recup { get; set; }
            public double ordrqty { get; set; }
            public double adjbalqty { get; set; }
            public double orderbal { get; set; }
            public double mrrqty { get; set; }

        }


        //protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Session_tblMRR_Update();
        //    this.gvMRRInfo.PageIndex = ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
        //    this.gvMRRInfo_DataBind();
        //}
        protected void lbtnUpdateMRR_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataTable dtuser = (DataTable)ViewState["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
            string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
            //string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            //string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : (tblEditByid == "") ? userid : tblEditByid;
            string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : (tblEditDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblEditDat;

            this.Session_tblMRR_Update();
            if (this.ddlPrevMRRList.Items.Count == 0)
                this.GetReceiveNo();
            string mMRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            string mMRRREF = this.txtMRRRef.Text.Trim();
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataRow[] dr = tbl1.Select("mrrqty>0");
            if (dr.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input Receive Qty');", true);


                return;
            }




            if (this.chkdupMRR.Checked)
            {

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "CHECKEDDUPMRRNO", mMRRREF, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                    ;


                else
                {

                    DataView dv1 = ds2.Tables[0].DefaultView;
                    dv1.RowFilter = ("mrrno <>'" + mMRRNO + "'");
                    DataTable dt = dv1.ToTable();
                    if (dt.Rows.Count == 0)
                        ;
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Found Duplicate MRR No');", true);


                        this.ddlPrevMRRList.Items.Clear();
                        return;
                    }
                }
            }


            string mMRRDAT = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mPACTCODE = this.Request.QueryString["actcode"].ToString();//this.ddlProject.SelectedValue.ToString().Trim();
            string mBBLCCode = this.Request.QueryString["sircode"].ToString();// this.ddlBBLCList.SelectedValue.ToString().Trim();
            string mMRRUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mMRRBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mORDERNO = this.ddlOrderList.SelectedValue.ToString().Trim();
            string mMRRNAR = this.txtMRRNarr.Text.Trim();
            string mMRRChlnNo = this.txtChalanNo.Text.Trim();
            string mChlDate = this.txtChlDate.Text.Trim();
            if (mMRRChlnNo.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Empty Challan No Not Allow');", true);


                return;
            }
            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURMRRINFO", "PURMRRB",
                             mMRRNO, mMRRDAT, mPACTCODE, mBBLCCode, mORDERNO, mMRRUSRID, mAPPRUSRID, mAPPRDAT, mMRRBYDES, mAPPBYDES, mMRRREF, mMRRNAR, mMRRChlnNo, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, mChlDate);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);


                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mreqno = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string bomid = tbl1.Rows[i]["bomid"].ToString();
                double orbal = Convert.ToDouble(tbl1.Rows[i]["orderbal"].ToString());
                double mMRRQTY = Convert.ToDouble(tbl1.Rows[i]["mrrqty"].ToString());
                string mMRRAMT = tbl1.Rows[i]["mrramt"].ToString();
                string mMRRNOTE = tbl1.Rows[i]["mrrnote"].ToString();
                string mMRRchlnqty = tbl1.Rows[i]["chlnqty"].ToString();
                string mRackno = tbl1.Rows[i]["rackno"].ToString();
                string mLocation = tbl1.Rows[i]["location"].ToString();
                //if (orbal >= mMRRQTY)
                //{
                if (mMRRQTY > 0)
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURMRRINFO", "PURMRRA",
                             mMRRNO, mRSIRCODE, mSPCFCOD, mMRRQTY.ToString(), mMRRAMT, mMRRNOTE, mMRRchlnqty, mreqno, mRackno, mLocation, bomid, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);


                    return;
                }

                //}

                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('MRR Qty  must be less then equal Balance Qty');", true);


                //    return;
                //}
            }
            this.txtCurMRRDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);



            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Receive Information";
                string eventdesc = "Update MRR Qty";
                string eventdesc2 = mMRRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            gvMRRInfo_DataBind();
            //DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            //((Label)this.gvMRRInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text =
            //    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(mrramt)", "")) ?
            //        0.00 : tbl1.Compute("Sum(mrramt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void ImgbtnFindBBLC_Click(object sender, EventArgs e)
        {
            //Session.Remove("tblbblcandsup");
            //string comcod = this.GetCompCode();
            //string FindSupplier = this.txtBBLCSearch.Text.Trim() + "%";
            //string mProjCode = this.ddlProject.SelectedValue.ToString();
            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRBBLCLIST", FindSupplier, mProjCode, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //this.ddlBBLCList.DataTextField = "bblcdesc";
            //this.ddlBBLCList.DataValueField = "bblccode";
            //this.ddlBBLCList.DataSource = ds1.Tables[0];
            //this.ddlBBLCList.DataBind();
            //Session["tblbblcandsup"]=ds1.Tables[0];
            //this.ShowSupplier();
            //this.FindOrderList();
            //ds1.Dispose();
        }



        private void FindOrderList()
        {

            string comcod = this.GetCompCode();
            string mPONO = this.Request.QueryString["genno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRORDERLIST02", "%", mPONO, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);


                return;
            }
            this.ddlOrderList.DataTextField = "orderno1";
            this.ddlOrderList.DataValueField = "orderno";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();



        }


        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mProject = "";//this.ddlProject.SelectedValue.ToString();
            string mBBLCCode = "";// this.ddlBBLCList.SelectedValue.ToString();
            string mOrderNo = this.ddlOrderList.SelectedValue.ToString();
            string mSrchTxt = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRRESLIST", mSrchTxt, mProject, mBBLCCode, mOrderNo, "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);


                return;
            }
            ViewState["tblMat"] = ds1.Tables[0];

            this.ddlResList.DataTextField = "rsirdesc1";
            this.ddlResList.DataValueField = "rsircode1";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();
        }



        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            //this.ddlBBLCList.Items.Clear();
            // this.ddlOrderList.Items.Clear();
            // this.ddlBBLCList_SelectedIndexChanged(null,null);
        }
        protected void ddlBBLCList_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            // this.Load_Project_Combo();
        }


        protected void gvMRRInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblMRR"];
            string MRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            string reqno = ((Label)this.gvMRRInfo.Rows[e.RowIndex].FindControl("lblgvReqnomain")).Text.Trim();
            string rescode = ((Label)this.gvMRRInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            string spcfcod = ((Label)this.gvMRRInfo.Rows[e.RowIndex].FindControl("lblgvSpcfCod")).Text.Trim();
            string bomid = ((Label)this.gvMRRInfo.Rows[e.RowIndex].FindControl("lblgvBomNo")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMRRMAT", MRRNO, reqno, rescode, spcfcod, bomid, "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvMRRInfo.PageSize) * (this.gvMRRInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState["tblMRR"] = dv.ToTable();
            this.gvMRRInfo_DataBind();
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
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Deleted successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Receive Information";
                string eventdesc = "Delete Materials Received Qty";
                string eventdesc2 = mMRRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void gvMRRInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlval");
                string reqno = ((Label)e.Row.FindControl("lblgvReqnomain")).Text.Trim();
                string rescod = ((Label)e.Row.FindControl("lblgvResCod")).Text.Trim();
                string spcfcod = ((Label)e.Row.FindControl("lblgvSpcfCod")).Text.Trim();
                string bomid = ((Label)e.Row.FindControl("lblgvBomNo")).Text.Trim();

                DataTable tbl1 = (DataTable)ViewState["tblMRR"];
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr = tbl1.Select("reqno='" + reqno + "' and  rsircode = '" + rescod + "' and spcfcod = '" + spcfcod + "' and bomid='" + bomid + "'");

                DataTable dt = (DataTable)ViewState["tblLocation"];

                ddllocation.DataTextField = "gdesc";
                ddllocation.DataValueField = "gcod";
                ddllocation.DataSource = dt;
                ddllocation.DataBind();
                if (dr.Count() != 0)
                {
                    ddllocation.SelectedValue = dr[0]["location"].ToString();
                }
            }
        }

        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Item Expand")
            {
                this.gvRecItem.Visible = true;
                this.LbtnReqItemShow.Text = "Item Collapse";
            }
            else
            {
                this.gvRecItem.Visible = false;
                this.LbtnReqItemShow.Text = "Item Expand";
            }
        }

        protected void LbtnRecItemCalculate_Click(object sender, EventArgs e)
        {
            var listsum = (List<SumClass>)Session["TblReceiveSum"];
            var sum = 0.00;

            for (int i = 0; i < this.gvRecItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRecItem.Rows[i].FindControl("txtgvSMRRQty")).Text.Trim()));

                sum += Qty;
                listsum[i].mrrqty = Qty;
                listsum[i].adjbalqty = listsum[i].orderbal - Qty;

                if (Qty == 0)
                    continue;
                DataTable dt = (DataTable)ViewState["tblMRR"];
                var list = dt.DataTableToList<SPEENTITY.C_09_Commer.LocalPORCV>();
                //var list = (List<SPEENTITY.C_09_Commer.LocalPORCV>)ViewState["tblMRR"];

                foreach (var item in list.FindAll(r => r.rsircode + r.spcfcod == listsum[i].rsircode + listsum[i].spcfcod).ToList())
                {

                    if (item.orderbal < Qty)
                    {
                        item.mrrqty = item.orderbal;
                        Qty = Qty - item.orderbal;
                    }
                    else
                    {
                        item.mrrqty = Qty;
                        Qty = 0;
                        break;
                    }
                }
                ViewState["tblMRR"] = ASITUtility03.ListToDataTable(list);

            }

            Session["TblReceivesum"] = listsum;

            this.gvMRRInfo_DataBind();
            this.gvRecItem_DataBind();
        }


        protected void lgvISBalqtyCalculate_Click(object sender, EventArgs e)
        {

            var listsum = (List<SumClass>)Session["TblReceiveSum"];
            for (int i = 0; i < this.gvRecItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(((TextBox)this.gvRecItem.Rows[i].FindControl("lgvISBalqty")).Text.Trim());

                if (Qty > 0)
                    continue;

                DataTable dt = (DataTable)ViewState["tblMRR"];
                var list = dt.DataTableToList<SPEENTITY.C_09_Commer.LocalPORCV>();


                var rdata = list.FindAll(r => r.rsircode + r.spcfcod == listsum[i].rsircode + listsum[i].spcfcod);
                var fdata = rdata.OrderByDescending(c => c.mrrqty).First();

                foreach(var item in rdata)
                {

                    if (item.rsircode == fdata.rsircode && item.spcfcod == fdata.spcfcod && item.bomid == fdata.bomid)
                    {
                        item.mrrqty = item.orderbal + (-Qty);
                        Qty = Qty - item.orderbal;
                    }

                }


                ViewState["tblMRR"] = ASITUtility03.ListToDataTable(list);

            }

            Session["TblReceiveSum"] = listsum;

            this.gvMRRInfo_DataBind();
            this.gvRecItem_DataBind();
        }




    }
}