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
    public partial class ProdReqSemi : System.Web.UI.Page
    {
        ProcessAccess Budget = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // this.GetBudgetedDate();
                this.GetProNo();
                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "Entry") ? "PRODUCTION REQUISITION INFORMATION" : "SEMI PRODUCTION REQUISITION INFORMATION";
                
                    this.Semiwip.Visible = true;

                this.GetAccCode();
                this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.lblmsg.Visible = false;
                this.CommonButton();
            }
        }
        private void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Send Mail";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFinalProUpdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetAccCode()
        {
            if (ASTUtility.Left(this.GetCompCode(), 2) == "61")
            {
                return;
            }
            if (this.lbtnOk.Text == "New")
                return;
            string comcod = this.GetCompCode();
            string filter =  "%";
            string Type = (this.Request.QueryString["Type"] == "Entry") ? "" : "Semi";

            DataSet ds2 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETBATCHCODEREQ", filter, Type, "", "", "", "", "", "", "");
            this.ddlBatch.DataTextField = "actdesc";
            this.ddlBatch.DataValueField = "actcode";
            this.ddlBatch.DataSource = ds2.Tables[0];
            this.ddlBatch.DataBind();

            ds2.Dispose();
        }
        protected void imgbtnBatsearch_Click(object sender, EventArgs e)
        {

            this.GetAccCode();
        }
        private void GetProNo()
        {
            Session.Remove("tblproinfo");
            string comcod = this.GetCompCode();
            string PBMno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString();
            string Type = (this.Request.QueryString["Type"] == "Entry") ? "03%" : "04%";
            DataSet ds2 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETPBMNUMBER", PBMno, Type, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                this.ddlProdNo.DataSource = ds2.Tables[0];
                this.ddlProdNo.DataTextField = "pbmno1";
                this.ddlProdNo.DataValueField = "pbmno";
                this.ddlProdNo.DataBind();
                Session["tblproinfo"] = ds2.Tables[0];
                ds2.Dispose();

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        //protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //     DataTable dt = (DataTable)Session["tblbgddat"];

        //     string dpr = this.ddlDPR.SelectedValue.ToString();
        //     DataRow[] dr = dt.Select("PREQNO='" + dpr + "'");
        //     if (dr.Length > 0)
        //     {
        //         this.txtbgddate.Text = Convert.ToDateTime(dr[0]["pbdate"]).ToString("dd-MMM-yyyy");
        //         this.txtbgddate.Enabled = false;
        //     }
        //     else
        //     {
        //         this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        //         this.txtbgddate.Enabled = true ;             
        //     }


        //}


        private void GetAllProduct()
        {
            this.MultiView1.ActiveViewIndex = 2;
            string comcod = this.GetCompCode();
            string filter = "%" + this.txtsearchAllProduct.Text.Trim() + "%";
            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETPROCODE", filter, "", "", "", "", "", "", "", "");
            this.ddlAllProduct.DataSource = ds3.Tables[0];
            this.ddlAllProduct.DataTextField = "proddesc";
            this.ddlAllProduct.DataValueField = "prodcode";
            this.ddlAllProduct.DataBind();
            Session["tblProduct"] = ds3.Tables[0];
            ds3.Dispose();
            //this.lbtnSelect_Click(null,null);
            //this.lnkbtnPreDPR_Click(null, null);

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintReport();
        }
        private void PrintReport()
        {

            //string comcod = this.GetCompCode();
            //string pbmno = this.ddlProdNo.SelectedValue.ToString();
            //string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            //DataSet ds4 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "RPTBATCHBUDGETRPT", pbmno, DPRno, "", "", "", "", "", "", "");
            //if (ds4 == null)
            //{
            //    this.gvBudgetRpt.DataSource = null;
            //    this.gvBudgetRpt.DataBind();
            //    return;
            //}

            //try
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    //string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //    DataTable dt = (DataTable)ds4.Tables[0];

            //    ReportDocument rptinfo = new RMGiRPT.R_11_Pro.RptProdReq();
            //    TextObject comName = rptinfo.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //    comName.Text = comnam;
            //    TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtPBM"] as TextObject;
            //    rpttxtVoutype.Text = this.ddlProdNo.SelectedItem.Text.ToString();
            //    //TextObject txtBatch = rptinfo.ReportDefinition.ReportObjects["txtBatch"] as TextObject;
            //    //txtBatch.Text = "Batch No:";

            //    TextObject txtDPR = rptinfo.ReportDefinition.ReportObjects["txtDPR"] as TextObject;
            //    txtDPR.Text = DPRno.ToString();
            //    TextObject txtDate = rptinfo.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //    txtDate.Text = "Date : " + this.txtbgddate.Text.ToString();

            //    TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptinfo.SetDataSource(dt);
            //    Session["Report1"] = rptinfo;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //}
            //catch (Exception ex)
            //{
            //    this.lblmsg.Text = "Error:" + ex.Message;
            //}

        }

        protected void imgbtnsearch_Click(object sender, EventArgs e)
        {
            this.GetProNo();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

               
                this.ddlBatch.Enabled = false;

                
                this.ddlProdNo.Enabled = false;
                this.lblCurReqNo1.Text = "DPR" + DateTime.Today.ToString("MM") + "-";
                this.getDPRNO();
                this.ShowBudgetedIncome();
                //this.ddlDPR.Items.Clear();
                //this.lblProcess5.Visible = false;
                //this.txtbatchno0.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.ddlDPR.Visible = false;
                return;
            }

            this.lbtnOk.Text = "Ok";


            this.ddlBatch.Enabled = true;
            this.ddlDPR.Items.Clear();
            this.ddlProdNo.Enabled = true;
            this.MultiView1.ActiveViewIndex = -1;

            this.txtbgddate.Enabled = true;
            //this.lblProcess5.Visible = true;
            //this.txtbatchno0.Visible = true;
            this.ibtnFindPrv.Visible = true;
            this.ddlDPR.Visible = true;
            

        }
        private void SaveValue()
        {
            //int index;
            //DataTable dt = (DataTable)ViewState["tblsup"];
            //for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            //{
            //    index = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + i;

            //    string approved = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False"; //dt.Rows[index]["approved"].ToString();

            //    double Reqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvResInfo.Rows[i].FindControl("lblgvpropqty_01")).Text.Trim()));

            //    double Rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
            //    double csreqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));
            //    double advamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvadvamtC")).Text.Trim()));
            //    string paytype = ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvpaytypeC")).Text.Trim();
            //    string remakrs = ((TextBox)this.gvResInfo.Rows[i].FindControl("TextRemarks")).Text.Trim();
            //    dt.Rows[i]["approved"] = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False";

            //    dt.Rows[i]["advamt"] = advamt;
            //    dt.Rows[i]["paytype"] = paytype;
            //    dt.Rows[i]["msrrmrk"] = remakrs;

            //    dt.Rows[i]["rate"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
            //    dt.Rows[i]["csreqqty"] = (approved == "False") ? 0.00 : (csreqqty == 0) ? Reqqty : Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));




            //    ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvcsreqqty")).Text = (approved == "False") ? "" : (csreqqty == 0) ? Reqqty.ToString() : csreqqty.ToString();


            //    dt.Rows[i]["amount"] = (approved == "False") ? 0.00 : (Rate * ((csreqqty == 0) ? Reqqty : csreqqty));


            //}
            //ViewState["tblsup"] = dt;

        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValueP();
            this.Data_Bind();

        }

        private void ShowBudgetedIncome()
        {
            this.MultiView1.ActiveViewIndex = 0;
            Session.Remove("tblbbudget");
            Session.Remove("tblbbudgetcost");
            string comcod = this.GetCompCode();
            string PBMNO = this.ddlProdNo.SelectedValue.ToString();
            string procode = this.ddlAllProduct.SelectedValue.ToString();
            string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string CallType = (this.GetCompCode() == "6101") ? "RPTBATCHBUDGET_02" : "RPTBATCHBUDGET_01";
            string Type = (this.Request.QueryString["Type"] == "Entry") ? "41%" : "04%";
            string Date = this.txtbgddate.Text;
            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", CallType, PBMNO, DPRno, Type, Date, "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvBudgetRpt.DataSource = null;
                this.gvBudgetRpt.DataBind();
                return;
            }
            Session["tblbbudget"] = HiddenSameData(ds3.Tables[0]);
            Session["tblbbudgetcost"] = HiddenSameData(ds3.Tables[1]);
            ds3.Dispose();
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string procode = dt1.Rows[0]["procode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["procode"].ToString() == procode)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["prodesc"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["procode"].ToString() == procode)
                    {
                        dt1.Rows[j]["prodesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    grp = dt1.Rows[j]["grp"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();

                }

            }
            return dt1;

        }




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblbbudget"];
            DataTable dt1 = (DataTable)Session["tblbbudgetcost"];
            this.gvBudgetRpt.DataSource = dt;
            this.gvBudgetRpt.DataBind();

            this.gvCost.DataSource = dt1;
            this.gvCost.DataBind();


            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRBgdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdqty)", "")) ?
                 0 : dt.Compute("sum(bgdqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRIssqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(issqty)", "")) ?
                 0 : dt.Compute("sum(issqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRBbqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bbqty)", "")) ?
                 0 : dt.Compute("sum(bbqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRProdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cresqty)", "")) ?
                 0 : dt.Compute("sum(cresqty)", ""))).ToString("#,##0;(#,##0); ");

        }



        protected void getDPRNO()
        {
            string comcod = this.GetCompCode();
            string date = this.txtbgddate.Text;
            if (this.ddlDPR.Items.Count == 0)
            {
                DataSet ds2 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETDPRNO", date, "", "", "", "", "", "", "", "");
                this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["dprno"].ToString().Substring(0, 3) + ds2.Tables[0].Rows[0]["dprno"].ToString().Substring(7, 2);  //DPR20120600001
                this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["dprno"].ToString().Substring(9, 5);
                this.txtbgddate.Enabled = true;

                this.ddlDPR.DataTextField = "dprno";
                this.ddlDPR.DataValueField = "dprno";
                this.ddlDPR.DataSource = ds2.Tables[0];
                this.ddlDPR.DataBind();
            }
            else
            {
                string dpr = this.ddlDPR.SelectedValue.ToString();
                this.lblCurReqNo1.Text = dpr.Substring(0, 3) + dpr.Substring(7, 2);
                this.txtCurReqNo2.Text = dpr.Substring(9, 5);
                //this.ddlProcess_SelectedIndexChanged(null,null);
            }
        }



        protected void gvBudgetRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lblgvResDescRpt");
                Label amt = (Label)e.Row.FindControl("lblgvBgdamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();


                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");
                }


            }

        }


        protected void imgbtnsrchAllProduct_Click(object sender, EventArgs e)
        {
            this.GetAllProduct();
        }
        protected void lbtnSelect1_Click(object sender, EventArgs e)
        {
            this.ShowBudgetedIncome();
        }

        private void SaveValueP()
        {
            DataTable dt = (DataTable)Session["tblbbudget"];
            int TblRowIndex, i;

            for (i = 0; i < this.gvBudgetRpt.Rows.Count; i++)
            {
                double cbgdqty = Convert.ToDouble("0" + ((TextBox)this.gvBudgetRpt.Rows[i].FindControl("lblgvRProdqty")).Text.Trim());
                TblRowIndex = (gvBudgetRpt.PageIndex) * gvBudgetRpt.PageSize + i;
                dt.Rows[TblRowIndex]["cresqty"] = cbgdqty;
            }

            Session["tblbbudget"] = dt;
        }
        protected void lbtnFinalProUpdate_Click(object sender, EventArgs e)
        {

            
            this.SaveValueP();
            if (this.ddlDPR.Items.Count == 0)
                this.getDPRNO();
            string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            
            string comcod = this.GetCompCode();
            string PBMno = this.ddlProdNo.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtbgddate.Text.Trim()).ToString("dd-MMM-yyyy");
            string Batchcode = this.ddlBatch.SelectedValue.ToString();
            bool result = false;
            result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEPBREQ", "PBREQB", DPRno, PBMno, date, Batchcode, "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Budget.ErrorObject["Msg"].ToString() + "');", true);


                return;
            }

            DataTable tbl2 = (DataTable)Session["tblbbudget"];

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string ProdCode = tbl2.Rows[i]["procode"].ToString();
                string Rescode = tbl2.Rows[i]["rsircode"].ToString();
                string spcfcod = tbl2.Rows[i]["spcfcod"].ToString();
                string colorid = "000000000000";
                string sizeid = "000000000000";
                string Bgdqty = Convert.ToDouble(tbl2.Rows[i]["cresqty"].ToString().Trim()).ToString();
                result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEPBREQ", "PBREQA", DPRno, ProdCode, Rescode, Bgdqty, spcfcod, colorid, sizeid, "Normal", date, "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Budget.ErrorObject["Msg"].ToString() + "');", true);


                    return;
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


            this.ShowBudgetedIncome();

            this.lbtnTotalMat_Click(null, null);
            this.lbtnFinalMatUpdate_Click(null, null);

        }
        protected void lbtnTotalMat_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblbbudgetcost"];
            int TblRowIndex, i;

            for (i = 0; i < this.gvCost.Rows.Count; i++)
            {
                double cbgdqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvcRProdqty")).Text.Trim());
                TblRowIndex = (gvCost.PageIndex) * gvCost.PageSize + i;
                dt1.Rows[TblRowIndex]["cresqty"] = cbgdqty;
            }

            Session["tblbbudgetcost"] = dt1;
            this.Data_Bind();
        }
        protected void lbtnFinalMatUpdate_Click(object sender, EventArgs e)
        {
            //this.SaveValue();
            
            string comcod = this.GetCompCode();
            DataTable tbl2 = (DataTable)Session["tblbbudgetcost"];
            string date = Convert.ToDateTime(this.txtbgddate.Text.Trim()).ToString("dd-MMM-yyyy");

            string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            bool result = false;
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {

                string ProCode = tbl2.Rows[i]["procode"].ToString();
                string Rescode = tbl2.Rows[i]["rsircode"].ToString();
                string spcfcod = tbl2.Rows[i]["spcfcod"].ToString();
                string cbqty = tbl2.Rows[i]["cresqty"].ToString();
                result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEPBREQB", DPRno, ProCode, Rescode, cbqty, spcfcod, "000000000000", "000000000000", "Normal", date, "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Budget.ErrorObject["Msg"].ToString() + "');", true);


                    return;
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


        }
        protected void imgbtnPreDPR_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblbgddat");
            string comcod = this.GetCompCode();
            string PBMno = this.ddlProdNo.SelectedValue.ToString();
            string date = this.txtbgddate.Text.Trim().ToString();
            DataSet ds6 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETDPRNOLIST", date, PBMno, "", "", "", "", "", "", "");
            ViewState["tblbgddat"] = ds6.Tables[0];
            if (ds6.Tables[0].Rows.Count > 0)
            {
                this.ddlDPR.DataTextField = "preqno1";
                this.ddlDPR.DataValueField = "preqno";
                this.ddlDPR.DataSource = ds6.Tables[0];
                this.ddlDPR.DataBind();

                this.ddlDPR_SelectedIndexChanged(null, null);


            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void ddlDPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblbgddat"];

            string dprno = this.ddlDPR.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("PREQNO='" + dprno + "'");

            this.ddlProdNo.DataTextField = "pbmno1";
            this.ddlProdNo.DataValueField = "pbmno";
            this.ddlProdNo.DataSource = dv.ToTable();
            this.ddlProdNo.DataBind();


            this.ddlBatch.DataTextField = "actdesc";
            this.ddlBatch.DataValueField = "actcode";
            this.ddlBatch.DataSource = dv.ToTable();
            this.ddlBatch.DataBind();
            this.txtbgddate.Text = Convert.ToDateTime(dv.ToTable().Rows[0]["pbdate"]).ToString("dd-MMM-yyyy");

        }
        //protected void wip_Click(object sender, EventArgs e)
        //{
        //    if (this.Request.QueryString["Type"] == "FgWIP")
        //    {
        //        ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('F_33_Mgt/AccWIPCode.aspx?Type=FgWIP', target='_blank');</script>";

        //    }
        //    else
        //    {
        //        this.lblprintstkl.Text = @"<script>window.open('~/F_33_Mgt/AccWIPCode.aspx?Type=FgSeWIP', target='_blank');</script>";

        //    }
        //}

        protected void lblgvchlnqty_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int index = row.RowIndex;
            for (int i = 0; i < this.gvBudgetRpt.Rows.Count; i++)
            {
                if (i == index)
                {
                    DataTable dt = (DataTable)Session["tblbbudget"];
                    dt.Rows.RemoveAt(index);
                    Session["tblbbudget"] = dt;
                    this.Data_Bind();


                }


            }
        }

        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvcstockqty = (Label)e.Row.FindControl("lblgvcstockqty");

                double stockqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "stockqty"));

                if (stockqty < 1)
                {
                    lblgvcstockqty.Style.Add("color", "red");


                }

            }
        }
    }

}