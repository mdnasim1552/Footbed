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
    public partial class ReProductionEntry : System.Web.UI.Page
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

                this.GetRECINFO();
                //this.GetGRRNO();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "Entry") ? "Re-Production Request- FG"
                  : "Re-Production Request- Semi FG";
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
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Calculation";



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
            string Type = (this.Request.QueryString["Type"] == "Entry" || this.Request.QueryString["Type"] == "EntryRej") ? "17%" : "15%";
            string RcvType = (this.Request.QueryString["Type"] == "Entry" || this.Request.QueryString["Type"] == "EntrySemi") ? "Fresh" : "Reject";
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETREPRODLIST", filter3, Type, RcvType, "", "", "", "", "", "");

            DataTable dt1 = ds1.Tables[0].Copy();
            dt1 = dt1.DefaultView.ToTable(true, "batchcode", "batchdesc");

            this.ddlProdinfo.DataTextField = "batchdesc";
            this.ddlProdinfo.DataValueField = "batchcode";
            this.ddlProdinfo.DataSource = dt1;
            this.ddlProdinfo.DataBind();
            ViewState["tblRpRodData"] = ds1.Tables[0];
            ds1.Dispose();

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

                this.PanelOther.Visible = true;
                //this.lblPreviousGrr.Visible = false;
                //this.txtSrchGRR.Visible = false;
                this.ImgbtnFindGrrList.Visible = false;
                this.ddlPrevRPRList.Visible = false;
                this.ShowGrrInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.ddlPrevRPRList.Items.Clear();
            this.ddlProdinfo.Enabled = true;

            //this.lblPreviousGrr.Visible = true;
            //this.txtSrchGRR.Visible = true;
            this.ImgbtnFindGrrList.Visible = true;
            this.ddlPrevRPRList.Visible = true;
            this.txtCurDate.Enabled = true;
            this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            this.gvReProd.DataSource = null;
            this.lblmsg.Text = "";
            this.txtMRFNo.Text = "";
            this.gvReProd.DataBind();
            this.GetRECINFO();
            this.lblmsg.Visible = false;
            this.PanelOther.Visible = false;
            this.grvMatList.DataSource = null;
            this.grvMatList.DataBind();
        }

        private void GetGRRNO()
        {

            string comcod = this.GetCompCode();
            string mGRRNo = "NEWRPR";
            if (this.ddlPrevRPRList.Items.Count > 0)
                mGRRNo = this.ddlPrevRPRList.SelectedValue.ToString();

            string CurDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            if (mGRRNo == "NEWRPR")
            {
                DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETLASTRPREQNO", CurDate,
                       "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxrpno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxrpno1"].ToString().Substring(6, 5);
                    this.ddlPrevRPRList.DataTextField = "maxrpno1";
                    this.ddlPrevRPRList.DataValueField = "maxrpno";
                    this.ddlPrevRPRList.DataSource = ds1.Tables[0];
                    this.ddlPrevRPRList.DataBind();
                }
            }

        }

        private void ShowGrrInfo()
        {

            ViewState.Remove("tblRePro");
            string comcod = this.GetCompCode();
            string CurDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");

            //string txtgrrno = this.ddlPrevRPRList.SelectedValue.Trim().ToString();
            string mGRRNo = "NEWRPR";
            if (this.ddlPrevRPRList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mGRRNo = this.ddlPrevRPRList.SelectedValue.ToString();
            }
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "SHOWREPRODLIST", mGRRNo, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            ViewState["tblRePro"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tbAllData"] = (ds1.Tables[1]);

            if (mGRRNo == "NEWRPR")
            {
                ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETLASTRPREQNO", CurDate, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxrpno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxrpno1"].ToString().Substring(6, 5);
                }

                this.Data_Bind();

                this.lbtnSelectQc_Click(null, null);

                return;

            }

            this.ddlProdinfo.DataTextField = "batchdesc";
            this.ddlProdinfo.DataValueField = "batchcode";
            this.ddlProdinfo.DataSource = ds1.Tables[2]; ;
            this.ddlProdinfo.DataBind();


            //this.txtMRFNo.Text = ds1.Tables[2].Rows[0]["batchcode"].ToString();


            this.txtOrderNarr.Text = ds1.Tables[2].Rows[0]["remark"].ToString();

            if (this.ddlPrevRPRList.Items.Count > 0)
            {
                this.lblCurNo1.Text = this.ddlPrevRPRList.SelectedItem.Text.Substring(0, 6);//ds1.Tables[1].Rows[0]["pbpno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = this.ddlPrevRPRList.SelectedItem.Text.Substring(6, 5);//ds1.Tables[1].Rows[0]["pbpno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["rpbdate"]).ToString("dd-MMM-yyyy");
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
            DataTable dt = (DataTable)ViewState["tblRePro"];
            if (dt.Rows.Count == 0)
                return;
            this.gvReProd.DataSource = dt;
            this.gvReProd.DataBind();

            DataTable dt1 = (DataTable)ViewState["tbAllData"];
            if (dt1.Rows.Count == 0)
                return;
            this.grvMatList.DataSource = HiddenSameData1(dt1);
            this.grvMatList.DataBind();

        }

        private void Save_Value_mat()
        {
            DataTable dt = (DataTable)ViewState["tbAllData"];
            int TblRowIndex, i;


            for (i = 0; i < this.grvMatList.Rows.Count; i++)
            {
                double bgdqty = Convert.ToDouble("0" + ((TextBox)this.grvMatList.Rows[i].FindControl("lblgvnewqty")).Text.Trim());
                TblRowIndex = (grvMatList.PageIndex) * grvMatList.PageSize + i;
                dt.Rows[TblRowIndex]["newqty"] = bgdqty;
            }

            ViewState["tbAllData"] = dt;
        }
        private void SaveValue_Pro()
        {

            DataTable dt = (DataTable)ViewState["tblRePro"];
            int TblRowIndex, i;


            for (i = 0; i < this.gvReProd.Rows.Count; i++)
            {
                double Rate = Convert.ToDouble("0" + ((Label)this.gvReProd.Rows[i].FindControl("lblrate")).Text.Trim());
                double bgdqty = Convert.ToDouble("0" + ((TextBox)this.gvReProd.Rows[i].FindControl("lblgvreqqty")).Text.Trim());
                TblRowIndex = (gvReProd.PageIndex) * gvReProd.PageSize + i;
                dt.Rows[TblRowIndex]["reqqty"] = bgdqty;
                dt.Rows[TblRowIndex]["rsamt"] = bgdqty * Rate;



            }

            ViewState["tblRePro"] = dt;
        }
        protected void lbtnSelectQc_Click(object sender, EventArgs e)
        {

            //this.Panel2.Visible = true;
            //this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblRePro"];
            string batchcode = this.ddlProdinfo.SelectedValue.ToString();

            DataRow[] dr2 = tbl1.Select("batchcode = '" + batchcode + "'");

            if (dr2.Length == 0)
            {
                DataTable tbl2 = (DataTable)ViewState["tblRpRodData"];
                //string batchcode1 = this.ddlProdinfo.SelectedValue.ToString();

                DataView dv = tbl2.DefaultView;
                dv.RowFilter = ("batchcode='" + batchcode + "'");
                tbl2 = dv.ToTable();

                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["grrno"] = tbl2.Rows[i]["grrno"].ToString();
                    dr1["batchcode"] = tbl2.Rows[i]["batchcode"].ToString();
                    dr1["batchdesc"] = tbl2.Rows[i]["batchdesc"].ToString();
                    dr1["rsircode"] = tbl2.Rows[i]["prodcode"].ToString();
                    dr1["rsirdesc"] = tbl2.Rows[i]["proddesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["rsqty"] = Convert.ToDouble(tbl2.Rows[i]["grrqty"]).ToString();
                    dr1["rate"] = Convert.ToDouble(tbl2.Rows[i]["rate"]).ToString();
                    dr1["reqqty"] = 0.00;
                    dr1["rsamt"] = 0.00;


                    tbl1.Rows.Add(dr1);
                }


            }

            ViewState["tblRePro"] = this.HiddenSameData(tbl1);
            this.Data_Bind();

        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            this.SaveValue_Pro();
            string comcod = this.GetCompCode();
            DataTable tbl2 = (DataTable)ViewState["tblRePro"];


            try
            {
                string pCode = (this.Request.QueryString["Type"] == "Entry") ? "41%" : "04%";
                DataSet ds1 = new DataSet("ds1");
                ds1.Tables.Add(tbl2);
                ds1.Tables[0].TableName = "tbl1";
                DataSet ds = ProData.GetTransInfoNew(comcod, "SP_ENTRY_PRO_QC_INFO", "SHOWREPRODRES", ds1, null, null, pCode, "", "", "", "", "", "", "", "");

                if (ds == null)
                {
                    this.grvMatList.DataSource = null;
                    this.grvMatList.DataBind();
                    return;
                }

                ds.Dispose();
                ViewState["tbAllData"] = ds.Tables[0];
                this.grvMatList.DataSource = HiddenSameData1(ds.Tables[0]);
                this.grvMatList.DataBind();


            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }


            //this.lblCost.Visible = true;
            //this.lblProSt.Visible = true;
            //this.ShowBgdQty();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //   Hashtable hst = (Hashtable)Session["tblLogin"];
            //   string comcod = this.GetCompCode();
            //   string comnam = hst["comnam"].ToString();
            //   string compname = hst["compname"].ToString();
            //   string username = hst["username"].ToString();
            //   string txtfromdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //   string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //   string rptDt = "Date " + txtfromdate;

            // //  string wpname = this.ddlProdinfo.SelectedItem.ToString();
            // //  string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //   string rpttitle = "Re Production List";
            //   DataTable matlist = (DataTable)ViewState["tbAllData"];
            //   DataTable prolist = (DataTable)ViewState["tblRePro"];

            //   var list = prolist.DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.ReProductionList>();
            //   var list2 = matlist.DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.ReProductionList>();

            //   LocalReport rpt1 = new LocalReport();
            //   //rpt1 = MFGRPTRDLC.RptSetupClass1.GetLocalReport("RD_23_SaM.RptSalordChlnDelv", null, null, null);
            //   rpt1 = MFGRPTRDLC.RptSetupClass1.GetLocalReport("RD_13_ProdMon.RptReProductionList", list, list2, null);      

            // //  rpt1.EnableExternalImages = true;
            //   rpt1.SetParameters(new ReportParameter("comnam", comnam));
            // //  rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            // //  rpt1.SetParameters(new ReportParameter("wipname", "WIP Name: " + wpname));

            //   rpt1.SetParameters(new ReportParameter("RptTitle", rpttitle));
            //   rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            ////   rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //   Session["Report1"] = rpt1;
            //   ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ImgbtnFindGrrList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string txtsrcgrr = "%";
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "GETPRERPRODLIST", txtsrcgrr, "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            this.ddlPrevRPRList.DataTextField = "rpreqno1";
            this.ddlPrevRPRList.DataValueField = "rpreqno";
            this.ddlPrevRPRList.DataSource = ds2.Tables[0];
            this.ddlPrevRPRList.DataBind();

        }
        protected void gvReProd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void btnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue_Pro();
            this.Save_Value_mat();
            this.Data_Bind();
        }
        protected void lbtnUpdatePurPrepar_Click(object sender, EventArgs e)
        {
            this.SaveValue_Pro();

            this.lblmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string user = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();


            this.GetGRRNO();
            string DPRno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string Batchcode = this.ddlProdinfo.SelectedValue.ToString();
            string remarks = this.txtOrderNarr.Text.ToString();
            bool result = false;
            result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "INORUPDATEREPBREQ", "PBREQB", DPRno, Batchcode, date, remarks, user, trmid, sessionid, postDat, "", "", "", "", "");

            if (!result)
            {
                this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
                this.lblmsg.BackColor = System.Drawing.Color.Red;
                this.lblmsg.ForeColor = System.Drawing.Color.White;
                return;
            }

            DataTable tbl2 = (DataTable)ViewState["tblRePro"];

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string grrno = tbl2.Rows[i]["grrno"].ToString();
                string Rescode = tbl2.Rows[i]["rsircode"].ToString();
                string Bgdqty = Convert.ToDouble(tbl2.Rows[i]["reqqty"].ToString().Trim()).ToString();
                string rsamt = Convert.ToDouble(tbl2.Rows[i]["rsamt"].ToString().Trim()).ToString();
                string procode = "000000000000";

                if (Convert.ToDouble(Bgdqty) > 0)
                {
                    result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "INORUPDATEREPBREQ", "PBREQA", DPRno, grrno, procode, Rescode, Bgdqty, rsamt, "", "", "", "", "", "", "", "", "");

                    if (!result)
                    {
                        this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
                        this.lblmsg.BackColor = System.Drawing.Color.Red;
                        this.lblmsg.ForeColor = System.Drawing.Color.White;
                        return;
                    }
                }


            }
            this.lblmsg.Text = "Updated Successfully";
            this.lblmsg.BackColor = System.Drawing.Color.Green;
            this.lblmsg.ForeColor = System.Drawing.Color.White;
            //this.ShowBudgetedIncome();

            this.Save_Value_mat();
            this.lbtnFinalMatUpdate_Click(null, null);

        }

        private void lbtnFinalMatUpdate_Click(object p1, object p2)
        {
            this.lblmsg.Visible = true;
            string DPRno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string comcod = this.GetCompCode();
            bool result = false;

            DataTable tbl2 = (DataTable)ViewState["tbAllData"];

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string grrno = "00000000000000";
                string Rescode = tbl2.Rows[i]["rsircode"].ToString();
                string Bgdqty = Convert.ToDouble(tbl2.Rows[i]["newqty"].ToString().Trim()).ToString();
                string procode = tbl2.Rows[i]["procode1"].ToString();
                result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "INORUPDATEREPBREQ", "PBREQA", DPRno, grrno, procode, Rescode, Bgdqty, "0", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    this.lblmsg.Text = ProData.ErrorObject["Msg"].ToString();
                    this.lblmsg.BackColor = System.Drawing.Color.Red;
                    this.lblmsg.ForeColor = System.Drawing.Color.White;
                    return;
                }


            }
            this.lblmsg.Text = "Updated Successfully";
            this.lblmsg.BackColor = System.Drawing.Color.Green;
            this.lblmsg.ForeColor = System.Drawing.Color.White;
            //this.ShowBudgetedIncome();


        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.lbtnUpdatePurPrepar_Click(null, null);
        }
        protected void gvReProd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblRePro"];
            string mGRNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string pqcno = ((Label)this.gvReProd.Rows[e.RowIndex].FindControl("lblpqcno")).Text.Trim();
            string rescode = ((Label)this.gvReProd.Rows[e.RowIndex].FindControl("lblprodcode")).Text.Trim();
            bool result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "DELETERECEIVEITM",
                        mGRNO, pqcno, rescode, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvReProd.PageSize) * (this.gvReProd.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("pqcno<>'' and prodcode<>''");
                ViewState["tblRePro"] = dv.ToTable();
                this.Data_Bind();
            }
        }
        private DataTable HiddenSameData1(DataTable dt1)
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
    }
}