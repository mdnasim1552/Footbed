using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using SPELIB;
using AjaxControlToolkit;
using SPERDLC;
using Microsoft.Reporting.WinForms;
using SPEENTITY.C_22_Sal;

namespace SPEWEB.F_10_Procur
{
    public partial class PurMktSurvey02 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        Common CommonClass = new Common();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        public static int i, j;
        public static string Url = "";
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Market Survey Information Input/Edit Screen";
                this.GetOther();
                this.CommonButton();
                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                //: (Request.QueryString["InputType"].ToString() == "ReqReview") ? "Materials Requisition Check"
                if (this.Request.QueryString["genno"].Length != 0)
                {
                    this.lnkReqList_Click(null, null);
                    this.lbtnMSROk_Click(null, null);
                }

                this.viewseciton();
            }

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(btnForward_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnResUpdate1_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(btnReqDelete_Click);
        }



        private void CommonButton()
        {


            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;


            if (this.Request.QueryString["Type"] == "Create")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Next";
                this.txtMSRNarr2.ReadOnly = true;
                this.txtMSRNarr3.ReadOnly = true;
            }
            else if (this.Request.QueryString["Type"] == "Check")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Checked";
                this.txtMSRNarr2.ReadOnly = true;
                this.txtMSRNarr3.ReadOnly = true;
            }
            else if (this.Request.QueryString["Type"] == "Audit")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Audit Checked";
                this.txtMSRNarr.ReadOnly = true;
                this.txtMSRNarr3.ReadOnly = true;

            }

            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Requisition Delete";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
                this.txtMSRNarr2.ReadOnly = true;
                this.txtMSRNarr.ReadOnly = true;

                this.gvBestSelect.Columns[10].Visible = true;
            }

        }

        private void viewseciton()
        {
            this.Multiview1.ActiveViewIndex = 0;
            string qtype = this.Request.QueryString["Type"].ToString();
            if (qtype == "Create")
            {
                this.AsyncFileUpload1.Visible = true;
                this.imgLoader.Visible = true;
                this.ddlBestSupplier.Visible = true;
                this.Label2.Visible = true;
                if (this.txtCurMSRNo2.Text == "00000")
                {
                    this.pnlQutatt.Visible = false;
                }
                else
                {
                    this.pnlQutatt.Visible = true;
                    this.btnShowimg_Click(null, null);
                }
            }
            else
            {
                this.AsyncFileUpload1.Visible = false;
                this.imgLoader.Visible = false;
                this.ddlBestSupplier.Visible = false;
                this.Label2.Visible = false;
                this.pnlQutatt.Visible = true;
                this.btnShowimg.Visible = false;
                this.btnShowimg_Click(null, null);
            }
        }


        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {
            if (this.lbtnMSROk.Text == "New")
            {

                this.ImgbtnFindPreMR.Visible = true;
                this.ddlPrevMSRList.Visible = true;
                this.lblPreMrList.Visible = true;
                this.txtPreMSRSearch.Visible = true;
                this.ddlPrevMSRList.Items.Clear();
                this.lblCurMSRNo1.Text = "MSR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMSRDate.Enabled = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.lbltitel1.Visible = false;
                this.lbltitel2.Visible = false;
                // this.lblJus.Visible = false;

                this.ddlReqList.Items.Clear();
                this.ddlReqList.Enabled = true;

                this.gvResInfo.DataSource = null;
                this.gvResInfo.DataBind();

                this.gvBestSelect.DataSource = null;
                this.gvBestSelect.DataBind();
                this.fotpanel.Visible = false;
                this.lbtnMSROk.Text = "Ok";
                return;
            }
            this.ddlReqList.Enabled = false;

            this.lbltitel1.Visible = true;
            this.lbltitel2.Visible = true;
            //  this.lblJus.Visible = true;
            this.txtMSRNarr.Visible = true;
            this.ImgbtnFindPreMR.Visible = false;
            this.ddlPrevMSRList.Visible = false;
            this.lblPreMrList.Visible = false;
            this.txtPreMSRSearch.Visible = false;
            this.fotpanel.Visible = true;
            this.lbtnMSROk.Text = "New";
            this.Get_Survey_Info();


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.CSPrint();

        }

        private void CSPrint()
        {
            string comcod = this.GetCompCode();
            string reqno = this.ddlReqList.SelectedValue.ToString();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint.aspx?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno + "', target='_blank');</script>";
            //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;
        }
        private void Get_Survey_Info()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string txtsearch = "%" + this.txtReqSearch.Text.ToString() + "%";
            string reqno = this.ddlReqList.SelectedValue.ToString();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMATREQWISE", reqno,
                          txtsearch, CurDate1, "", "", "", "", "", "");

            if (ds2 == null)
                return;
            this.lblConRate.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["conrate"]).ToString("#,##0.00;(#,##0.00); ");

            if (ds2.Tables[2].Rows.Count > 0)
            {

                //this.txtCurMSRDate.Enabled = false;


                this.lblCurMSRNo1.Text = ds2.Tables[2].Rows[0]["msrno1"].ToString().Substring(0, 6);
                this.txtCurMSRNo2.Text = ds2.Tables[2].Rows[0]["msrno1"].ToString().Substring(6, 5);
                this.txtCurMSRDate.Text = Convert.ToDateTime(ds2.Tables[2].Rows[0]["msrdat"]).ToString("dd.MM.yyyy");
                this.txtCurMSRDate.Enabled = false;



                this.ddlPrevMSRList.DataTextField = "msrno1";
                this.ddlPrevMSRList.DataValueField = "msrno";
                this.ddlPrevMSRList.DataSource = ds2.Tables[2];
                this.ddlPrevMSRList.DataBind();

                this.txtMSRNarr.Text = ds2.Tables[2].Rows[0]["msrnar"].ToString();
                this.txtMSRNarr2.Text = ds2.Tables[2].Rows[0]["msrnar2"].ToString();
                this.txtMSRNarr3.Text = ds2.Tables[2].Rows[0]["msrnar3"].ToString();

                //this.ddlPayType.SelectedValue = ds2.Tables[2].Rows[0]["paytype"].ToString();
                //this.txtadvAmt.Text = Convert.ToDouble(ds2.Tables[2].Rows[0]["advamt"]).ToString("#,##0;(#,##0); ");

            }






            ViewState["tblBestSelect"] = ds2.Tables[1];
            ViewState["tblsup"] = ds2.Tables[0];
            ViewState["tblStdcharging"] = ds2.Tables[3];
            ViewState["tblSupCharging"] = ds2.Tables[4];
            this.gvResInfo_DataBind();
            this.Charging_DataBind();
            //this.txtMSRNarr.Text = ds2.Tables[2].Rows[0]["msrnar"].ToString();

            //this.txtMSRNarr.Text = "";
            this.ddlBestSupplierinfo();
        }
        protected void Charging_DataBind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblSupCharging"];

            DataTable tblcharging = (DataTable)ViewState["tblStdcharging"];
            if (this.Request.QueryString["ReqType"].ToString() == "Import")
            {
                for (int i = 0; i < tblcharging.Rows.Count; i++)
                {
                    if (i == 5)
                        break;
                    this.gvcharging.Columns[6 + i].Visible = true;
                    this.gvcharging.Columns[6 + i].HeaderText = tblcharging.Rows[i]["sirdesc"].ToString();


                }
                this.gvcharging.Columns[14].Visible = true;
            }
            this.gvcharging.DataSource = tbl1;
            this.gvcharging.DataBind();



        }

        protected void gvResInfo_DataBind()
        {
            string reqtype = ASTUtility.Right(this.ddlReqList.SelectedItem.Text.Trim(), 2);
            string ReqType = this.Request.QueryString["ReqType"].ToString();
            if (reqtype != "LC")
            {
                this.gvBestSelect.Columns[11].Visible = false;
                this.gvBestSelect.Columns[12].Visible = false;
                this.gvBestSelect.Columns[15].Visible = false;
                this.gvBestSelect.Columns[16].Visible = false;

                this.gvBestSelect.Columns[13].HeaderText = "BDT Rate";
                this.gvBestSelect.Columns[14].HeaderText = "BDT Amount";

                ////////////////////////

                this.gvResInfo.Columns[9].Visible = false;
                this.gvResInfo.Columns[10].Visible = false;
                this.gvResInfo.Columns[13].Visible = false;
                this.gvResInfo.Columns[14].Visible = false;

                this.gvResInfo.Columns[11].HeaderText = "BDT Rate";
                this.gvResInfo.Columns[12].HeaderText = "BDT Amount";



            }
            if (ReqType == "Local")
            {
                //Best Selection
                //this.gvBestSelect.Columns[11].Visible = false;

                this.gvBestSelect.Columns[20].Visible = false;
                this.gvBestSelect.Columns[21].Visible = false;
                this.gvBestSelect.Columns[22].Visible = false;
                this.gvBestSelect.Columns[23].Visible = false;
                this.gvBestSelect.Columns[24].Visible = false;
                this.gvBestSelect.Columns[25].Visible = false;
                //Selection

                //this.gvResInfo.Columns[10].Visible = false;

                this.gvResInfo.Columns[19].Visible = false;
                this.gvResInfo.Columns[20].Visible = false;
                this.gvResInfo.Columns[21].Visible = false;
                this.gvResInfo.Columns[22].Visible = false;
                this.gvResInfo.Columns[23].Visible = false;
                this.gvResInfo.Columns[24].Visible = false;
                this.gvResInfo.Columns[25].Visible = false;
            }
            else
            {

            }

            DataTable tbl1 = (DataTable)ViewState["tblsup"];
            DataTable tbl2 = (DataTable)ViewState["tblBestSelect"];


            string comcod = this.GetCompCode();

            if (tbl1.Rows.Count == 0)
                return;
            if (tbl2.Rows.Count == 0)
                return;
            tbl1 = this.HiddenSameData(tbl1);



            this.gvResInfo.DataSource = tbl1;
            this.gvResInfo.DataBind();


            for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            {

                string rsircode = tbl1.Rows[i]["rsircode"].ToString();
                string spcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                DataView dv = tbl1.DefaultView;


                dv.RowFilter = ("rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "'");
                dv.Sort = "rsircode, rate asc";
                DataTable dt1 = dv.ToTable();
                double lsrate = Convert.ToDouble(dt1.Rows[0]["rate"]);
                //string chek = dt1.Rows[0]["approved"].ToString();

                //DataRow[] dr = dt1.Select("rsircode='" + rsircode + "' and spcfcod='" + spcfcod +"'");
                //double la = Convert.ToDouble(dr[0]["rate"]);
                //rate*conrate=bdtrate
                //csreqqty*(rate*conrate)
                double csreqqty = Convert.ToDouble(dt1.Rows[0]["csreqqty"]);
                double conrate = Convert.ToDouble(dt1.Rows[0]["conrate"]);
                double rate = Convert.ToDouble(dt1.Rows[0]["rate"]);
                double gvlstrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double bdtrate = gvlstrate * conrate;

                double bdtamt = csreqqty * bdtrate;

                ((Label)this.gvResInfo.Rows[i].FindControl("gvldbtRate")).Text = bdtrate.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvResInfo.Rows[i].FindControl("gvlblbdtAmount")).Text = bdtamt.ToString("#,##0.00;(#,##0.00); ");


                if (lsrate == gvlstrate)
                {

                    TextBox txtgvRate = (TextBox)gvResInfo.Rows[i].FindControl("txtgvRate");
                    txtgvRate.Style.Add("color", "blue");


                }
                string supcode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvSuplCod1")).Text.Trim();
                if (supcode == "000000000000")
                {
                    Label txtgvsupcode = (Label)gvResInfo.Rows[i].FindControl("lblgrsirdescs1");
                    if(txtgvsupcode !=null)
                    txtgvsupcode.Style.Add("color", "red");
                }


                //if (chek == "True")
                //{

                //    ((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked = true;

                //}



            }


            this.gvBestSelect.DataSource = HiddenSameData(tbl2);
            this.gvBestSelect.DataBind();
            for (int i = 0; i < this.gvBestSelect.Rows.Count; i++)
            {

                //string rsircode = tbl2.Rows[i]["rsircode"].ToString();
                //DataView dv = tbl2.DefaultView;
                //dv.RowFilter = ("rsircode='" + rsircode + "'");
                //dv.Sort = "rsircode, rate asc";
                //DataTable dt1 = dv.ToTable();
                //double lsrate = Convert.ToDouble(tbl2.Rows[0]["rate"]);


                //double gvlstrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBestSelect.Rows[i].FindControl("txtgvRateBSel")).Text.Trim()));

                //if (lsrate == gvlstrate)
                //{
                Label txtgvRate = (Label)gvBestSelect.Rows[i].FindControl("lblgvRateBSel");
                txtgvRate.Style.Add("color", "blue");


                string supcode1 = ((Label)this.gvBestSelect.Rows[i].FindControl("lblgvSuplBSel")).Text.Trim();
                if (supcode1 == "000000000000")
                {
                    LinkButton txtgvBsup = (LinkButton)gvBestSelect.Rows[i].FindControl("lblgrmet1BSel");
                    txtgvBsup.Style.Add("color", "red");
                }




                //  }
            }


            this.FooterAmount();

        }

        protected void lnkReqList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string txtsearch = "%" + this.txtReqSearch.Text.ToString() + "%";
            string Type = (this.Request.QueryString["Type"] == "Check") ? "Check" : (this.Request.QueryString["Type"] == "Approved") ? "Approved"
                : (this.Request.QueryString["Type"] == "Audit") ? "Audit" : "Next";

            string Gennno = (this.Request.QueryString["genno"].Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETRPUREQNO", CurDate1,
                          txtsearch, Gennno, Type, "", "", "", "", "");

            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.ddlReqList.DataTextField = "REQNO1";
                this.ddlReqList.DataValueField = "REQNO";
                this.ddlReqList.DataSource = ds2.Tables[0];
                this.ddlReqList.DataBind();
            }
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rsircode, spcfcod, bomid;


            rsircode = dt1.Rows[0]["rsircode"].ToString();
            spcfcod = dt1.Rows[0]["spcfcod"].ToString();
            bomid = dt1.Rows[0]["bomid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod && dt1.Rows[j]["bomid"].ToString() == bomid)
                {
                    dt1.Rows[j]["rsirdesc"] = "";
                    dt1.Rows[j]["propqty"] = 0.00;
                    dt1.Rows[j]["stkqty"] = 0.00;
                    dt1.Rows[j]["reqnote"] = "";
                }

                rsircode = dt1.Rows[j]["rsircode"].ToString();
                spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                bomid = dt1.Rows[j]["bomid"].ToString();
            }

            return dt1;
        }

        private void FooterAmount()
        {
            //  DataTable dt = (DataTable)ViewState["tblsup"];
            DataTable tbl2 = (DataTable)ViewState["tblBestSelect"];

            //if (dt.Rows.Count == 0)
            //    return;
            if (tbl2.Rows.Count == 0)
                return;

            //((Label)this.gvResInfo.FooterRow.FindControl("lblAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ?
            //                     0 : dt.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvBestSelect.FooterRow.FindControl("lblFAmountbsBDT")).Text = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(bdtamt)", "")) ?
                                 0 : tbl2.Compute("sum(bdtamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvBestSelect.FooterRow.FindControl("lblFAmountbs")).Text = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(amount)", "")) ?
                                0 : tbl2.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvBestSelect.FooterRow.FindControl("lblFQtybs")).Text = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(propqty)", "")) ?
            //                     0 : tbl2.Compute("sum(propqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }



        private void BS_SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblBestSelect"];
            for (int i = 0; i < this.gvBestSelect.Rows.Count; i++)
            {
                dt.Rows[i]["areqty"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBestSelect.Rows[i].FindControl("txtgvareqty")).Text.Trim()));
                dt.Rows[i]["rate"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvBestSelect.Rows[i].FindControl("lblgvRateBSel")).Text.Trim()));

                dt.Rows[i]["paytype"] = ((DropDownList)this.gvBestSelect.Rows[i].FindControl("ddlpayterms")).SelectedValue.Trim().ToString();
                dt.Rows[i]["paymod"] = ((DropDownList)this.gvBestSelect.Rows[i].FindControl("ddlpaymode")).SelectedValue.Trim().ToString();
                dt.Rows[i]["deltrm"] = ((DropDownList)this.gvBestSelect.Rows[i].FindControl("ddldeltrm")).SelectedValue.Trim().ToString();
                dt.Rows[i]["delmod"] = ((DropDownList)this.gvBestSelect.Rows[i].FindControl("ddldelmod")).SelectedValue.Trim().ToString();

                dt.Rows[i]["advamt"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBestSelect.Rows[i].FindControl("txtgvadvamt")).Text.Trim()));

            }
            ViewState["tblBestSelect"] = dt;

        }


        private void SaveValue()
        {
            double Conrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.lblConRate.Text));
            Conrate = (Conrate == 0) ? 1 : Conrate;
            int index;
            DataTable dt = (DataTable)ViewState["tblsup"];
            for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            {
                index = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + i;

                string approved = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False"; //dt.Rows[index]["approved"].ToString();

                double Reqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvResInfo.Rows[i].FindControl("lblgvpropqty_01")).Text.Trim()));
                double Rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                //double ConRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvResInfo.Rows[i].FindControl("txtgvConrate")).Text.Trim()));
                double csreqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));
                double advamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvadvamtC")).Text.Trim()));
                string paytype = ((DropDownList)this.gvResInfo.Rows[i].FindControl("ddlpaytermsc")).SelectedValue.Trim().ToString();
                string paymode = ((DropDownList)this.gvResInfo.Rows[i].FindControl("ddlpaymodec")).SelectedValue.Trim().ToString();
                string deltrm = ((DropDownList)this.gvResInfo.Rows[i].FindControl("ddldeltrmc")).SelectedValue.Trim().ToString();
                string delmod = ((DropDownList)this.gvResInfo.Rows[i].FindControl("ddldelmodc")).SelectedValue.Trim().ToString();
                string remakrs = ((TextBox)this.gvResInfo.Rows[i].FindControl("TextRemarks")).Text.Trim();
                string payment = ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvaPeriod")).Text.Trim();



                dt.Rows[i]["approved"] = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False";
                dt.Rows[i]["conrate"] = Conrate;
                dt.Rows[i]["advamt"] = advamt;
                dt.Rows[i]["paytype"] = paytype;
                dt.Rows[i]["paymod"] = paymode;
                dt.Rows[i]["delmod"] = delmod;
                dt.Rows[i]["deltrm"] = deltrm;
                dt.Rows[i]["msrrmrk"] = remakrs;
                dt.Rows[i]["msrrmrk"] = remakrs;
                dt.Rows[i]["payment1"] = payment;


                dt.Rows[i]["rate"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                dt.Rows[i]["csreqqty"] = (approved == "False") ? 0.00 : (csreqqty == 0) ? Reqqty : Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));



                ((Label)this.gvResInfo.Rows[j].FindControl("txtgvConrate")).Text = Conrate.ToString();
                ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvcsreqqty")).Text = (approved == "False") ? "" : (csreqqty == 0) ? Reqqty.ToString() : csreqqty.ToString();


                dt.Rows[i]["amount"] = (approved == "False") ? 0.00 : (Rate * ((csreqqty == 0) ? Reqqty : csreqqty));
                //dt.Rows[i]["csreqqty"] = 0.00;

            }
            ViewState["tblsup"] = dt;

        }

        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.BS_SaveValue();
            this.gvResInfo_DataBind();
            this.Chargin_SaveValue();
            this.Charging_DataBind();
        }

        protected void GetMSRNo()
        {
            string comcod = this.GetCompCode();
            string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
                mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();

            string mREQDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            if (mMSRNO == "NEWMSR")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mMSRNO = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();

                    this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);

                    this.ddlPrevMSRList.DataTextField = "maxmsrno";
                    this.ddlPrevMSRList.DataValueField = "maxmsrno1";
                    this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                    this.ddlPrevMSRList.DataBind();
                }
            }

        }
        protected void lbtnResUpdate1_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                Response.Redirect("~/AcceessError.aspx");

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            this.lnkbtnRecalculate_Click(null, null);
            //this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblsup"];





            //string mMSRNO = "NEWMSR";
            //if (this.ddlPrevMSRList.Items.Count > 0)
            //{
            //    mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();
            //}
            if (this.ddlPrevMSRList.Items.Count == 0)
                this.GetMSRNo();
            string mMSRDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string reqno = this.ddlReqList.SelectedValue.ToString();
            string mMSRNO = this.lblCurMSRNo1.Text.Trim().Substring(0, 3) + this.txtCurMSRDate.Text.Trim().Substring(6, 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
          
            ///Duplicate Req Chk

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHKDUPREQNO", reqno, "", "", "", "", "", "", "", "");

            if (ds2.Tables[0].Rows.Count != 0 && mMSRNO != ds2.Tables[0].Rows[0]["msrno"].ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Found Duplicate Req. No');", true);
                return;
            }


            int index;
            string Rsircode = "000000000000", spcfcod = "000000000000", bomidno = "0000000000";
            double chkqty = 0.00, treqqty = 0.00;
            for (int j = 0; j < this.gvResInfo.Rows.Count; j++)
            {

                index = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + j;

                string Resocde = tbl1.Rows[index]["rsircode"].ToString();
                string spcfcode = tbl1.Rows[index]["spcfcod"].ToString();
                string approved = tbl1.Rows[index]["approved"].ToString();
                string bomid = tbl1.Rows[index]["bomid"].ToString();

                double gvpropqty = Convert.ToDouble(tbl1.Rows[index]["propqty"]);



                double gvcsreqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvcsreqqty")).Text.Trim()));



                if (Rsircode == Resocde && spcfcod == spcfcode && bomidno == bomid)
                {
                    chkqty = chkqty - gvcsreqqty;
                    if (chkqty < 0)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Not Within the Requisition');", true); 
                        return;
                    }
                }
                else
                {
                    chkqty = gvpropqty - gvcsreqqty;
                }

                Rsircode = tbl1.Rows[index]["rsircode"].ToString();
                spcfcod = tbl1.Rows[index]["spcfcod"].ToString();
                bomidno = tbl1.Rows[index]["bomid"].ToString();

            }
            //}




            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                double mRESRATE = Convert.ToDouble(tbl1.Rows[i]["rate"].ToString());

                if (mRESRATE == 0.00)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Check Supplier Rate";
                    return;
                }



            }


            string PostedByid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");



            string mMSRNAR = this.txtMSRNarr.Text.Trim();
            string mMSRNAR2 = this.txtMSRNarr2.Text.Trim();
            string mMSRNAR3 = this.txtMSRNarr3.Text.Trim();
            //string pType = this.ddlPayType.SelectedValue.ToString();
            //string AdvAmt = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString();


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO", "PURMSRB",
                             mMSRNO, mMSRDAT, PostedByid, Postedtrmid, PostedSession, PostedDat, mMSRNAR, reqno, mMSRNAR2, mMSRNAR3, "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }

            //DataTable tbl1 = (DataTable)Session["tblsup"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string bomid = tbl1.Rows[i]["bomid"].ToString();
                string mRESRATE = tbl1.Rows[i]["rate"].ToString();

                string mMSRRMRK = tbl1.Rows[i]["msrrmrk"].ToString();
                string mMSRRQty = tbl1.Rows[i]["csreqqty"].ToString();
                string mMSRRBrand = "";//tbl1.Rows[i]["brand"].ToString();
                string mMSRRDelivery = "0.00";// tbl1.Rows[i]["delivery"].ToString();

                string mMSRRPay = tbl1.Rows[i]["payment1"].ToString();

                string mMaxrate = "0.00";//tbl1.Rows[i]["maxrate"].ToString();
                string mPaylimit = "0.00";//tbl1.Rows[i]["payment"].ToString();
                string mCurcode = tbl1.Rows[i]["curcode"].ToString();
                string mConrate = tbl1.Rows[i]["conrate"].ToString();
                string mAppr = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False";
                string paytype = tbl1.Rows[i]["paytype"].ToString();
                string paymod = tbl1.Rows[i]["paymod"].ToString();
                string delmod = tbl1.Rows[i]["delmod"].ToString();
                string deltrm = tbl1.Rows[i]["deltrm"].ToString();
                string advamt = tbl1.Rows[i]["advamt"].ToString();
                mConrate = (mConrate == "0") ? "1" : mConrate;

                result = purData.UpdateTransInfo1(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO", "PURMSRA",
                         mMSRNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mRESRATE, mMSRRMRK, mMSRRQty, mMSRRBrand, mMSRRDelivery, mMSRRPay, mMaxrate, mPaylimit, mCurcode, mConrate, mAppr, paytype, advamt, paymod, delmod, deltrm, reqno, bomid);



                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }

                //if(result==true)
                //{
                this.pnlQutatt.Visible = true;
                //}



            }



            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string rate = tbl1.Rows[i]["rate"].ToString();
                // string approved = tbl1.Rows[i]["approved"].ToString();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRESINFO", mSSIRCODE, mRSIRCODE, mSPCFCOD, rate, "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            this.Get_Survey_Info();
            this.Chargin_SaveValue();
            // add new charging valuee----- by Safi
            DataTable newCharging = (DataTable)ViewState["tblnewCharging"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(newCharging);
            ds1.Tables[0].TableName = "tbl1";
            result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLCHARGING", ds1, null, null, mMSRNO, "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Successfully Successfully');", true);
            Response.Redirect(Request.Url.AbsoluteUri);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.Label1.Text;
                string eventdesc = "Update Supplier";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string Type = (this.Request.QueryString["Type"] == "Check") ? "Check" : (this.Request.QueryString["Type"] == "Approved") ? "Approved" : "Next";

            //if (Type == "Approved")
            //{
            //    this.AppReq();
            //}



            // this.Get_Survey_Info();




        }
        protected void gvResInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dtOther = (DataTable)ViewState["tblOther"];
            string comcod = this.GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtpayTr = dtOther.Copy();
                DataView dv = dtpayTr.DefaultView;
                dv.RowFilter = ("gcod like '12%'");
                DropDownList ddlcate = (DropDownList)e.Row.FindControl("ddlpaytermsc");
                ddlcate.DataTextField = "gdesc1";
                ddlcate.DataValueField = "gcod1";
                ddlcate.DataSource = dv.ToTable();
                ddlcate.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paytype")) != "")
                {
                    ddlcate.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paytype"));
                }


                DataView dv1 = dtpayTr.DefaultView;
                dv1.RowFilter = ("gcod like '16%'");
                DropDownList ddlpaymodec = (DropDownList)e.Row.FindControl("ddlpaymodec");
                ddlpaymodec.DataTextField = "gdesc1";
                ddlpaymodec.DataValueField = "gcod1";
                ddlpaymodec.DataSource = dv1.ToTable();
                ddlpaymodec.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymod")) != "")
                {
                    ddlpaymodec.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymod"));
                }
                DataView dv2 = dtpayTr.DefaultView;
                dv2.RowFilter = ("gcod like '14%'");
                DropDownList ddldelmodc = (DropDownList)e.Row.FindControl("ddldelmodc");
                ddldelmodc.DataTextField = "gdesc1";
                ddldelmodc.DataValueField = "gcod1";
                ddldelmodc.DataSource = dv2.ToTable();
                ddldelmodc.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "delmod")) != "")
                {
                    ddldelmodc.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "delmod"));
                }
                DataView dv3 = dtpayTr.DefaultView;
                dv3.RowFilter = ("gcod like '13%'");
                DropDownList ddldeltrmc = (DropDownList)e.Row.FindControl("ddldeltrmc");
                ddldeltrmc.DataTextField = "gdesc1";
                ddldeltrmc.DataValueField = "gcod1";
                ddldeltrmc.DataSource = dv3.ToTable();
                ddldeltrmc.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deltrm")) != "")
                {
                    ddldeltrmc.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deltrm"));
                }
                //  ddlpaymodec.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paytype"));
            }

            // string rsircode = ((Label)e.Row.FindControl("lblrsircode")).Text;

            //DataTable tbl1 = (DataTable)ViewState["tblsup"];
            //if (tbl1.Rows.Count == 0)
            //    return;

            //DataView dv = tbl1.DefaultView;

            //dv.RowFilter = ("rsircode='" + rsircode + "'");
            //DataTable dt1 = dv.ToTable();
            //double rate = Convert.ToDouble(dt1.Rows[2]["rate"]);



            //for (int j = 1; j < tbl1.Rows.Count; j++)
            //{
            //    dt1.Rows[j]["rate"] = rate;
            //    if (rate==19.10)
            //    {
            //        TextBox txtgvRate = (TextBox)e.Row.FindControl("txtgvRate");
            //        txtgvRate.Style.Add("color", "green");
            //    }

            //}


        }




        protected void lbtTotal1_Click(object sender, EventArgs e)
        {
            this.FooterAmount();
            this.gvResInfo_DataBind();
        }
        protected void btnForward_Click(object sender, EventArgs e)
        {

            try
            {
                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string reqno = this.ddlReqList.SelectedValue.ToString();
                string surveynpo = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
                string Sessionid = hst["session"].ToString();
                string Terminal = hst["compname"].ToString();

                string Type = (this.Request.QueryString["Type"] == "Check") ? "Check" : (this.Request.QueryString["Type"] == "Approved") ? "Approved"
                    : (this.Request.QueryString["Type"] == "Audit") ? "Audit" : "Next";
                if (Type == "Approved")
                {
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHQREQAPP", reqno, "", "", "", "", "", "", "", "");

                    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETCASHPURCHASE", surveynpo, userid, Terminal, Sessionid, Date, "", "", "", "");

                    if (Convert.ToDouble(ds.Tables[0].Rows[0]["areqty"]) == 0)
                    {
                        this.AppReq();
                    }
                }



                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEMSERVAYNUM", reqno, surveynpo, userid, Date, Type, Sessionid,
                       Terminal, "", "", "", "", "", "", "", "");
                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated  Fail');", true);
                    return;
                }

                string qtype = this.Request.QueryString["Type"].ToString();
                if (qtype == "Check")
                {

                    string esubject = "CS Checked Complete! Request to CS Approval";
                    string url = "http://202.0.94.49/F_34_Mgt/RptAdminInterface.aspx";
                    string bodyContent = "Dear Sir, </br>A New CS Generate, Please click  <b> <a href='" + url +
                                    "' target='_blank'>" + reqno + " </a> </b> on the link to CS Approval";

                    if (CommonClass.ConfimMail("1002005", esubject, url, bodyContent) == true)
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Successfully";
                    }

                }

                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);


            }
            catch (Exception ex)
            {

            }
        }
        private void AppReq()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            this.BS_SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblBestSelect"];
            // string mMSRNO = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
            string Reqno = this.ddlReqList.SelectedValue.ToString();
            string mMSRNO = this.lblCurMSRNo1.Text.Trim().Substring(0, 3) + this.txtCurMSRDate.Text.Trim().Substring(6, 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(tbl1);
            ds1.Tables[0].TableName = "tbl1";

            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATECSAPPREQ", ds1, null, null, Reqno, mMSRNO, "", "", "", "", "", "", "",
              "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }
            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
            //    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
            //    string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
            //    string mRESRATE = tbl1.Rows[i]["rate"].ToString();
            //    string mAPPqty = tbl1.Rows[i]["areqty"].ToString();

            //    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATECSAPPREQ", Reqno, mRSIRCODE, mSPCFCOD, mSSIRCODE, mRESRATE, mAPPqty);

            //    if (!result)
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
            //        return;
            //    }
            //}
        }



        // ======================== file upload================


        private void ddlBestSupplierinfo()
        {


            //ViewState["tblBestSelect"] = ds2.Tables[1];
            //ViewState["tblsup"] = ds2.Tables[0];


            // DataTable dt = (DataTable)ViewState["tblBestSelect"];

            DataTable dt = (DataTable)ViewState["tblsup"];
            DataView dv = dt.DefaultView;



            dv.RowFilter = ("ssircode <> '000000000000'");
            DataTable tbSupList = dv.ToTable(true, "ssircode", "supdesc");
            this.ddlBestSupplier.DataTextField = "supdesc";
            this.ddlBestSupplier.DataValueField = "ssircode";
            this.ddlBestSupplier.DataSource = tbSupList;
            this.ddlBestSupplier.DataBind();
        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            string comcod1 = comcod;//this.Request.QueryString["comcod"].ToString();
            string msrno = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
            //lblCurMSRNo1
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            // i = i + 1;
            string ssircode = "";

            if (AsyncFileUpload1.HasFile)
            {
                ssircode = this.ddlBestSupplier.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                //AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/Survey/") + comcod1 + msrno + ssircode + extension);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/Survey/") + comcod1 + msrno + ssircode + extension);

                Url = comcod1 + msrno + ssircode + extension;

            }
            //Url = Url.Substring(0,(Url.Length-1));

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "QUTATIONIMAGEUPLOAD", msrno, ssircode, Url, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                btnShowimg_Click(null, null);
                //this.lblMesg.Text=" Successfully Updated ";
            }

        }
        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblimgPath");
            DataTable tbfilePath = new DataTable();
            tbfilePath.Columns.Add("filePath1", Type.GetType("System.String"));
            tbfilePath.Columns.Add("supinfo", Type.GetType("System.String"));
            tbfilePath.Columns.Add("msrno", Type.GetType("System.String"));
            tbfilePath.Columns.Add("ssircode", Type.GetType("System.String"));
            ViewState["tblimgPath"] = tbfilePath;
            DataTable tbl2 = (DataTable)ViewState["tblimgPath"];
            string comcod = this.GetCompCode();
            string ssircode = this.ddlBestSupplier.SelectedValue.ToString();
            string msrno = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "QUTATIONIMAGESHOW", msrno, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];

            string Url = "";
            string supinfo = "";
            for (int j = 0; j < tbl1.Rows.Count; j++)
            {

                Url = "../Upload/Survey/" + tbl1.Rows[j]["attacheddoc"].ToString().Trim();
                supinfo = tbl1.Rows[j]["SIRDESC"].ToString().Trim();
                DataRow dr1 = tbl2.NewRow();
                dr1["filePath1"] = Url;
                dr1["supinfo"] = supinfo;
                dr1["msrno"] = tbl1.Rows[j]["msrno"].ToString().Trim(); ;
                dr1["ssircode"] = tbl1.Rows[j]["ssircode"].ToString().Trim(); ;
                tbl2.Rows.Add(dr1);

            }




            ListViewEmpAll.DataSource = tbl2;
            ListViewEmpAll.DataBind();
            ViewState["tblimgPath"] = tbl2;


        }

        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;


                }

            }

        }





        protected void UploadFile(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Upload/Survey/") + Path.GetFileName(FileUpload1.FileName);
            if (File.Exists(path))
            {
                Session["PostedFile"] = FileUpload1.PostedFile;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "confirm", "Confirm();", true);
            }
            else
            {
                FileUpload1.SaveAs(path);
            }
        }
        protected void ConfirmReplace(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                HttpPostedFile postedFile = (Session["PostedFile"] as HttpPostedFile);
                string path = Server.MapPath("~/Upload/Survey/") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(path);
            }
            Session["PostedFile"] = null;
        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string mrsno = ((Label)this.ListViewEmpAll.Items[j].FindControl("msrno")).Text.ToString();
                string ssircode = ((Label)this.ListViewEmpAll.Items[j].FindControl("ssircode")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "REMOVESURVEYIMG", mrsno, ssircode, "", "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/MFGWEB/");
                        System.IO.File.Delete(filePath + filesname);

                        this.lblMesg.Text = " Files Removed ";
                    }


                }




            }
            if (this.Request.QueryString["genno"].Length != 0)
            {
                this.lnkReqList_Click(null, null);
                this.lbtnMSROk.Text = "Ok";
                this.lbtnMSROk_Click(null, null);

            }
            this.viewseciton();

        }
        protected void btnReqDelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string comcod = this.Request.QueryString["comcod"].ToString();
            if (this.Request.QueryString["genno"].Length == 0)
            {
                return;
            }
            string reqno = this.Request.QueryString["genno"].ToString();
            DataTable dt = (DataTable)ViewState["tblBestSelect"];
            //ViewState["tblBestSelect"] = ds2.Tables[1];
            //ViewState["tblsup"]
            this.XmlDataInsert(reqno, reqno, dt);
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELTEPPURREQAFTERCSAPPRV", reqno, "", "", "", "", "");
            if (!result)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
            }


        }
        private string XmlDataInsert(string Reqno, string Apprno, DataTable dt)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            dt.Columns.Add("delbyid", typeof(string));
            dt.Columns.Add("delseson", typeof(string));
            dt.Columns.Add("deldate", typeof(DateTime));

            dt.Rows[0]["delbyid"] = usrid;
            dt.Rows[0]["delseson"] = session;
            dt.Rows[0]["deldate"] = Date;


            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            //ds1.Tables[1].TableName = "tbl2";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno, Apprno);

            if (!resulta)
            {

                //return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
        private void Chargin_SaveValue()
        {
            //    int index;
            ViewState.Remove("tblnewCharging");
            this.NewChargingTable();


            DataTable dt = (DataTable)ViewState["tblStdcharging"];
            DataTable dt2 = (DataTable)ViewState["tblSupCharging"];
            DataTable newchrg = (DataTable)ViewState["tblnewCharging"];
            string supl = "";

            for (int i = 0; i < this.gvcharging.Rows.Count; i++)
            {
                //  index = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + i;
                supl = ((Label)gvcharging.Rows[i].FindControl("lblgvSuplCod1")).Text.Trim();
                // double suplamt = Convert.ToDouble("0" + ((Label)gvcharging.Rows[i].FindControl("lblgvSupamt")).Text.Trim());
                string purtype = ((DropDownList)gvcharging.Rows[i].FindControl("ddlPurType")).SelectedValue.ToString();

                double total1 = 0.00;
                double total2 = 0.00;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (j == 7)
                        break;
                    double amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)gvcharging.Rows[i].FindControl("gvText" + j)).Text.Trim()));
                    dt2.Rows[i]["c" + j] = amount;
                    //if (dt.Rows[j]["sircode"].ToString().Substring(0, 9) != "019999902")
                    //{
                    //    total1 += amount;
                    //}
                    //else
                    //{
                    //    total2 += amount;
                    //}
                    //  total1 += amount;
                    DataRow dr = newchrg.NewRow();
                    dr["supcode"] = supl;
                    dr["rsircode"] = dt.Rows[j]["sircode"].ToString();
                    dr["amount"] = amount;
                    dr["purtype"] = purtype;
                    newchrg.Rows.Add(dr);
                }
                dt2.Rows[i]["tamt"] = total1 - total2;
                dt2.Rows[i]["tax"] = 0;
                dt2.Rows[i]["vat"] = 0;
                dt2.Rows[i]["purtype"] = purtype;
                //dt2.Rows[i]["tax"] = tax.ToString();
                //dt2.Rows[i]["vat"] = vat.ToString();

            }
            ViewState["tblnewCharging"] = newchrg;
            ViewState["tblSupCharging"] = dt2;
        }

        private void NewChargingTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("supcode", typeof(System.String));
            dt.Columns.Add("rsircode", typeof(System.String));
            dt.Columns.Add("amount", typeof(System.Double));
            dt.Columns.Add("purtype", typeof(System.String));
            ViewState["tblnewCharging"] = dt;
        }

        protected void lblgrmet1BSel_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string rsircode = ((Label)this.gvBestSelect.Rows[index].FindControl("lblgvResCodBSel1")).Text.ToString();
            string spcfcod = ((Label)this.gvBestSelect.Rows[index].FindControl("lblgvSpcCodBSel1")).Text.ToString();
            string reqno = this.Request.QueryString["genno"].ToString();

            DataSet mathistory = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETMATWISEPURINF", rsircode, spcfcod, reqno, "", "");

            if (mathistory.Tables[0].Rows.Count == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger();", true);
                return;
            }
            this.lblstore.Text = mathistory.Tables[0].Rows[0]["actdesc"].ToString();
            this.lblmat.Text = mathistory.Tables[0].Rows[0]["subdesc"].ToString();
            this.lblspc.Text = mathistory.Tables[0].Rows[0]["spcfdesc"].ToString();
            this.lblUnit.Text = mathistory.Tables[0].Rows[0]["unit"].ToString();

            var matlist = mathistory.Tables[0].DataTableToList<SPEENTITY.C_07_Fin.MaterialWiseStock>();
            ViewState["MatWiseStock"] = matlist;
            this.gvMatHis.DataSource = matlist;
            this.gvMatHis.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);


        }
        protected void lblgrsirdescs1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string rsircode = ((Label)this.gvResInfo.Rows[index].FindControl("lblrsircode")).Text.ToString();
            string spcfcod = ((Label)this.gvResInfo.Rows[index].FindControl("lblspcfcod")).Text.ToString();
            Session["rsircode"] = rsircode;
            Session["spcfcod"] = spcfcod;
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlSupl2.DataTextField = "ssirdesc1";
            this.ddlSupl2.DataValueField = "ssircode";
            this.ddlSupl2.DataSource = ds1.Tables[0];
            this.ddlSupl2.DataBind();
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            Session["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            Session["tblcurdesc"] = lstCurryDesc;
            this.ddlCurrency.DataValueField = "curcode";
            this.ddlCurrency.DataTextField = "curdesc";
            this.ddlCurrency.DataSource = lstCurryDesc;
            this.ddlCurrency.DataBind();

            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)Session["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)Session["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            this.lblConRate.Text = Convert.ToDouble(method).ToString("#,##0.000000;-#,##0.000000; ");
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openServyModal();", true);


        }
        protected void UpdateData_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string conRate = this.lblConRate.Text.ToString();
            string SSIRCODE = this.ddlSupl2.SelectedValue.ToString();
            string Currency = this.ddlCurrency.SelectedValue.ToString();
            string rsircode = Session["rsircode"].ToString();
            string spcfcod = Session["spcfcod"].ToString();
            string TextRate = this.TextRate.Text.ToString();

            purData.GetTransInfoNew(comcod, "SP_ENTRY_PURCHASE_04", "UPDATESRSUPLIST", null, null, null, SSIRCODE, rsircode, spcfcod, TextRate, Currency, conRate, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            Response.Redirect(Request.RawUrl, true);

        }
        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)Session["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)Session["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            this.lblConRate.Text = Convert.ToDouble(method).ToString("#,##0.000000;-#,##0.000000; ");

        }
        protected void gvMatHis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string reqno = this.Request.QueryString["genno"].ToString();
                string comcod = this.GetCompCode();
                HyperLink printlink = (HyperLink)e.Row.FindControl("LbtnPrint");
                string grp = ((Label)e.Row.FindControl("lblGp")).Text.ToString();
                string genno = ((Label)e.Row.FindControl("lblgenno")).Text.ToString();

                switch (grp)
                {
                    case "B":
                        printlink.Visible = true;
                        printlink.NavigateUrl = "~/F_11_Pro/PuchasePrint.aspx?Type=OrderPrint&comcod=" + comcod + "&orderno=" + genno;
                        printlink.CssClass = "btn btn-xs btn-info";
                        break;

                }




            }
        }
        protected void LbtnModalPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();  //address
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            // string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = (List<SPEENTITY.C_10_Procur.EClassProcur.MaterialWiseStock>)ViewState["MatWiseStock"];
            string storename = list[0].actdesc;
            string material = list[0].subdesc;
            string specification = list[0].subdesc;

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_07_Inv.RptMWIssueandPS", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("storename", storename));

            rpt1.SetParameters(new ReportParameter("material", material));
            rpt1.SetParameters(new ReportParameter("specification", specification));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Material Wise Issue and Purchase stock"));
            //rpt1.SetParameters(new ReportParameter("FromToDate", ""));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt1;
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRDLC();", true);
        }

        protected void gvcharging_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                string purtypecode = ((Label)e.Row.FindControl("lblpurtype")).Text;
                string genno = this.Request.QueryString["genno"].ToString().Trim();
                string mMSRNO = this.lblCurMSRNo1.Text.Trim().Substring(0, 3) + this.txtCurMSRDate.Text.Trim().Substring(6, 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

                HyperLink HypNote = (HyperLink)e.Row.FindControl("HypNote");
                Label supcode = (Label)e.Row.FindControl("lblgvSuplCod1");
                Label lblgvSupamt = (Label)e.Row.FindControl("lblgvSupamt");

                DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURTYPE", genno);
                if (ds == null)
                    return;

                DropDownList ddlPurType = ((DropDownList)e.Row.FindControl("ddlPurType"));
                ddlPurType.DataSource = ds.Tables[0];
                ddlPurType.DataTextField = "gdesc";
                ddlPurType.DataValueField = "gcod";
                ddlPurType.DataBind();

                if (purtypecode.Length > 0)
                {
                    ddlPurType.SelectedValue = purtypecode;
                }
                HypNote.NavigateUrl = "~/NOAFormat.aspx?Type=Entry&comcod=" + comcod + "&reqno=" + genno + "&msrno=" + mMSRNO + "&supcode=" + supcode.Text + "&amt=" + lblgvSupamt.Text; ;


            }

        }
        private void GetOther()
        {
            string comcod = this.GetCompCode();
            //ViewState.Remove("tblcur");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETCURRENCYAGST", "", "", "", "", "", "", "", "", "");
            ViewState["tblSup"] = ds1.Tables[0];
            ViewState["tblBank"] = ds1.Tables[1];
            ViewState["tblOther"] = ds1.Tables[2];
            ds1.Dispose();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.Multiview1.ActiveViewIndex = 0;
                    break;
                case "1":
                    this.Multiview1.ActiveViewIndex = 1;
                    break;
                case "2":
                    this.BindTotal();
                    this.Multiview1.ActiveViewIndex = 2;
                    break;
            }
        }
        private void BindTotal()
        {
            DataTable dt = (DataTable)ViewState["tblBestSelect"];
            var newDt = dt.AsEnumerable()
                                  .GroupBy(r => r.Field<string>("ssircode"))
                                  .Select(g =>
                                  {
                                      var row = dt.NewRow();
                                      row["ssircode"] = g.Key;
                                      row["supdesc"] = g.Where(r => r["ssircode"] == g.Key.ToString()).First()["supdesc"];                                      
                                      row["areqty"] = g.Count();

                                      return row;
                                  }).CopyToDataTable();

            string [] color  = { "bg-primary", "bg-yellow", "bg-purple", "bg-red", "bg-amazon", "bg-ebay", "bg-facebook", "bg-google", "bg-twitter", "bg-github", "bg-flickr", "bg-orange", "bg-pink" };
            string innHTML = "";
            int i = 0;
            foreach (DataRow dr in newDt.Rows)
            {
                string supname = (dr["supdesc"].ToString().Length> 0) ? dr["supdesc"].ToString() : "Not Allocated";
                string [] splitname = supname.Split(' ');
                string shortname = String.Empty;
                if (splitname.Count() > 1)
                {
                    shortname = splitname[0].Substring(0, 1) + splitname[1].Substring(0, 1);
                }
                else
                {
                    shortname = splitname[0].Substring(0, 2) ;
                }
                innHTML += "<a href=\"\" class=\"list-group-item list-group-item-action\">" +
                          "<div class=\"list-group-item-figure\">" +
                            "<div class=\"tile tile-circle " + color[i] + "\"> " + shortname.ToUpper() + "</div>"+
                          "</div>"+
                          "<div class=\"list-group-item-body\">"+ supname + "</div>"+
                            "<div class=\"list-group-item-figure\">"+
                            "<button class=\"btn btn-sm btn-danger\">" +
                              dr["areqty"].ToString() +
                            "</button>"+
                          "</div>"+
                        "</a>";
                i++;

            }
            this.SupTotal.InnerHtml = Convert.ToString(innHTML);
           

        }

        protected void gvBestSelect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtOther = (DataTable)ViewState["tblOther"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtpayTr = dtOther.Copy();
                DataView dv = dtpayTr.DefaultView;
                dv.RowFilter = ("gcod like '12%'");

                DropDownList ddlcate = (DropDownList)e.Row.FindControl("ddlpayterms");
                ddlcate.DataTextField = "gdesc1";
                ddlcate.DataValueField = "gcod1";
                ddlcate.DataSource = dv.ToTable();
                ddlcate.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paytype")) != "")
                {
                    ddlcate.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paytype"));
                }


                DataView dv1 = dtpayTr.DefaultView;
                dv1.RowFilter = ("gcod like '16%'");
                DropDownList ddlpaymodec = (DropDownList)e.Row.FindControl("ddlpaymode");
                ddlpaymodec.DataTextField = "gdesc1";
                ddlpaymodec.DataValueField = "gcod1";
                ddlpaymodec.DataSource = dv1.ToTable();
                ddlpaymodec.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymod")) != "")
                {
                    ddlpaymodec.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymod"));
                }
                DataView dv2 = dtpayTr.DefaultView;
                dv2.RowFilter = ("gcod like '14%'");
                DropDownList ddldelmodc = (DropDownList)e.Row.FindControl("ddldelmod");
                ddldelmodc.DataTextField = "gdesc1";
                ddldelmodc.DataValueField = "gcod1";
                ddldelmodc.DataSource = dv2.ToTable();
                ddldelmodc.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "delmod")) != "")
                {
                    ddldelmodc.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "delmod"));
                }
                DataView dv3 = dtpayTr.DefaultView;
                dv3.RowFilter = ("gcod like '13%'");
                DropDownList ddldeltrmc = (DropDownList)e.Row.FindControl("ddldeltrm");
                ddldeltrmc.DataTextField = "gdesc1";
                ddldeltrmc.DataValueField = "gcod1";
                ddldeltrmc.DataSource = dv3.ToTable();
                ddldeltrmc.DataBind();
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deltrm")) != "")
                {
                    ddldeltrmc.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deltrm"));
                }

            }
        }
    }
}