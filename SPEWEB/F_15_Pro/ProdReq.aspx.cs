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
using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;

namespace SPEWEB.F_15_Pro
{
    public partial class ProdReq : System.Web.UI.Page
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
                // this.GetBudgetedDate();

                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "Entry") ? "PRODUCTION REQUISITION INFORMATION" : "SEMI PRODUCTION REQUISITION INFORMATION";

                this.GetLine();
                this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                
                this.LoadDataLC_Order();
                this.CommonButton();
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
            // this.SaveValueP();
            this.Data_Bind();
        }


        private void CommonButton()
        {
           

            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";


        }
        private void LoadDataLC_Order()
        {

            string comcod = GetCompCode();
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "DTLLCLIST", "", "%%", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlBatch.DataTextField = "ACTDESC1";
            this.ddlBatch.DataValueField = "ACTCODE";
            this.ddlBatch.DataSource = ds1.Tables[0];
            this.ddlBatch.DataBind();

            if (this.Request.QueryString["actcode"].ToString().Length > 0)
            {
                this.ddlBatch.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlBatch_SelectedIndexChanged(null, null);
                this.lbtnOk_Click(null, null);
            }


        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlBatch.SelectedValue.ToString();
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GET_ORDERWISE_PLANNO", mlccod, "", "", "", "", "", "", "", "");
            this.ddlplan.DataTextField = "plnno1";
            this.ddlplan.DataValueField = "plnno";
            this.ddlplan.DataSource = ds1.Tables[0];
            this.ddlplan.DataBind();
            if (this.Request.QueryString["genno"].ToString().Length > 0)
            {
                var plnno = this.Request.QueryString["genno"].ToString();
                this.ddlplan.SelectedValue = plnno.Substring(0, 11);
            }
        }

        private void GetLine()
        {
            string comcod = GetCompCode();
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETLINE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlLine.DataTextField = "linedesc";
            this.ddlLine.DataValueField = "linecode";
            this.ddlLine.DataSource = ds1.Tables[0];
            this.ddlLine.DataBind();
            ds1.Dispose();



        }

        protected void imgbtnBatsearch_Click(object sender, EventArgs e)
        {

            ///this.GetAccCode();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string pbmno = this.ddlBatch.SelectedItem.ToString();

            string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            //string pbmno1 = pbmno.Substring(0, 3) + pbmno.Substring(7, 2) + "-" + pbmno.Substring(9, 5);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.txtbgddate.Text;


            string session = hst["session"].ToString();
            string footer = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

           
            
            DataTable dt1 = (DataTable)ViewState["tblbbudgetcost"];
            DataTable dt2 = (DataTable)ViewState["tblbbudget"];

            var rptlist = dt1.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProdReqPrint>();
            var rptlist2 = dt2.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdProcess.RptProditmPrint>();
            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_15_Pro.RptProdReq", rptlist, rptlist2, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));

            Rpt1a.SetParameters(new ReportParameter("date", "Date :" + date));

            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1a.SetParameters(new ReportParameter("pbmno", pbmno.Substring(15)));
            Rpt1a.SetParameters(new ReportParameter("DPRno", DPRno));

            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }








        private void PrintReport()
        {

            string comcod = this.GetCompCode();
            string pbmno = this.ddlBatch.SelectedValue.ToString();
            string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            DataSet ds4 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RPTBATCHBUDGETRPT", pbmno, DPRno, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvBudgetRpt.DataSource = null;
                this.gvBudgetRpt.DataBind();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);

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

               
                this.ddlBatch.Enabled = false;
                this.ddlLine.Enabled = false;
                this.ddlplan.Enabled = false;
                this.lblCurReqNo1.Text = "DPR" + DateTime.Today.ToString("MM") + "-";
                this.getDPRNO();
                this.ShowBudgetedIncome();
                //this.ddlDPR.Items.Clear();
                //this.lblProcess5.Visible = false;

                this.ibtnFindPrv.Visible=false;
                this.ddlDPR.Visible = false;
                return;
            }

            this.lbtnOk.Text = "Ok";


            this.ddlBatch.Enabled = true;
            this.ddlDPR.Items.Clear();

            this.MultiView1.ActiveViewIndex = -1;
          
            this.ddlLine.Enabled = true;
            this.txtbgddate.Enabled = true;
            //this.lblProcess5.Visible = true;
            
            this.ibtnFindPrv.Visible = true;
            this.ddlDPR.Visible = true;
            

        }


        private void ShowBudgetedIncome()
        {
            this.MultiView1.ActiveViewIndex = 0;
            ViewState.Remove("tblbbudget");
            ViewState.Remove("tblbbudgetcost");
            string comcod = this.GetCompCode();
            //string PBMNO = this.DDLMasterLC.SelectedValue.ToString();
            //string procode = this.ddlAllProduct.SelectedValue.ToString();
            string batchcode = ASTUtility.Right(this.ddlBatch.SelectedValue.ToString(), 12);
            string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string plnno = this.ddlplan.SelectedValue.ToString();
            string Date = this.txtbgddate.Text;
            string tardate = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
            string slnum = this.Request.QueryString["genno"].ToString().Substring(11, 3);
            string dayid = this.Request.QueryString["dayid"].ToString();

            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RPTBATCHBUDGET_01", batchcode, DPRno, "", Date, plnno.Trim(), tardate, slnum, dayid, "");
            if (ds3 == null)
            {
                this.gvBudgetRpt.DataSource = null;
                this.gvBudgetRpt.DataBind();
                return;
            }
            ViewState["tblbbudget"] = ds3.Tables[0];// HiddenSameData(ds3.Tables[0]);
            ViewState["tblbbudgetcost"] = HiddenSameData(ds3.Tables[1]);
            ds3.Dispose();
            this.Data_Bind();

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
                    dt1.Rows[j]["procname"] = "";
                }
                procode = dt1.Rows[j]["procode"].ToString();

            }


            return dt1;

        }

        private void Data_Bind()

        {
            DataTable dt = (DataTable)ViewState["tblbbudget"];
            DataTable dt1 = (DataTable)ViewState["tblbbudgetcost"];
            this.gvBudgetRpt.DataSource = dt;
            this.gvBudgetRpt.DataBind();
            this.FooterCalculation();
            this.gvCost.DataSource = dt1;
            this.gvCost.DataBind();


            if (dt.Rows.Count == 0)
                return;
            //((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRProdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmqty)", "")) ?
            // 0 : dt.Compute("sum(itmqty)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void getDPRNO()
        {
            string comcod = this.GetCompCode();
            string date = this.txtbgddate.Text;
            if (this.ddlDPR.Items.Count == 0)
            {
                DataSet ds2 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETDPRNO", date, "", "", "", "", "", "", "", "");
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

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label groupdesc = (Label)e.Row.FindControl("lblgvResDescRpt");
            //    Label amt = (Label)e.Row.FindControl("lblgvBgdamt");
            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "itmno")).ToString();

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
        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Label groupdesc = (Label)e.Row.FindControl("lblgvResDescRpt");
                double amt = Convert.ToDouble("0" + ((TextBox)e.Row.FindControl("txtgvcRProdqty")).Text);
                bool mark = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "mark"));

                if (mark == true)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }



            }
        }
        protected void lbtnSelect1_Click(object sender, EventArgs e)
        {
            this.ShowBudgetedIncome();
        }

        //private void SaveValueP()
        //{
        //    DataTable dt = (DataTable)ViewState["tblbbudget"];
        //    int TblRowIndex, i;

        //            for (i = 0; i < this.gvBudgetRpt.Rows.Count; i++)
        //            {
        //                double cbgdqty = Convert.ToDouble("0" + ((TextBox)this.gvBudgetRpt.Rows[i].FindControl("lblgvRProdqty")).Text.Trim());
        //                TblRowIndex = (gvBudgetRpt.PageIndex) * gvBudgetRpt.PageSize + i;
        //                dt.Rows[TblRowIndex]["cresqty"] = cbgdqty;
        //            }

        //            ViewState["tblbbudget"] = dt;
        // }
        protected void lbtnFinalProUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string date = Convert.ToDateTime(this.txtbgddate.Text.Trim()).ToString("dd-MMM-yyyy");
            string Batchcode = ASTUtility.Right(this.ddlBatch.SelectedValue.ToString(), 12);
            string Lineno = this.ddlLine.SelectedValue.ToString();
            string dayid = this.Request.QueryString["dayid"].ToString();
            string subcontact = this.ddlSupplierName.SelectedValue.ToString(); 
            string plnno = this.ddlplan.SelectedValue.ToString();
            string slnum = this.Request.QueryString["genno"].ToString().Substring(11, 3);
            string tardate = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");

            bool resultCheck = Budget.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GET_TARREQ_QTY", Batchcode, tardate, plnno, slnum, dayid, "", "", "",  "", "", "");

            if (!resultCheck)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have already saved once');", true);

                return;
            }

            // this.SaveValueP();
            if (this.ddlDPR.Items.Count == 0)
                this.getDPRNO();
            string DPRno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtbgddate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
           
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable tblcost = (DataTable)ViewState["tblbbudgetcost"];
            
            DataView dv = tblcost.DefaultView;
            dv.RowFilter = ("itmqty = 0 ");
            DataTable tblcostchek = dv.ToTable();
            if (tblcostchek.Rows.Count > 0 && comcod != "5301") // qty mismatch ignored for Edison as per their requirement
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please check materials qty');", true);

                return;
            }
            
            bool result = false;
            result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "INORUPDATEPBREQ", "PBREQB", DPRno, "", date, Batchcode, "000000000000", plnno, slnum, tardate, userid, Terminal, Sessionid, Posteddat, dayid, subcontact, "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid " + Budget.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }

            DataTable tbl2 = (DataTable)ViewState["tblbbudget"];
           

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string ProdCode = "000000000000";
                string Rescode = tbl2.Rows[i]["rsircode"].ToString();
                string colorid = tbl2.Rows[i]["colorid"].ToString();
                string sizeid = tbl2.Rows[i]["sizeid"].ToString();
                string Bgdqty = Convert.ToDouble(tbl2.Rows[i]["cresqty"].ToString().Trim()).ToString();
                result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "INORUPDATEPBREQ", "PBREQA", DPRno, ProdCode, Rescode, Bgdqty, colorid, sizeid, "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid " + Budget.ErrorObject["Msg"].ToString() + "');", true);

                  
                    return;
                }

            }
            for (int i = 0; i < tblcost.Rows.Count; i++)
            {
                string ProCode = tblcost.Rows[i]["procode"].ToString();
                string Rescode = tblcost.Rows[i]["itmno"].ToString();
                string colorid = "000000000000";
                string sizeid = "000000000000";
                string spcfcode = tblcost.Rows[i]["spcfcod"].ToString();
                string cbqty = Convert.ToDouble(tblcost.Rows[i]["itmqty"].ToString().Trim()).ToString();
                result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "INORUPDATEPBREQB", DPRno, ProCode, Rescode, cbqty, colorid, sizeid, spcfcode, date, "NORMAL", "", "");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            ///update production process


            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPREVIOUSPPNO", Batchcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            string ppno = ds1.Tables[0].Rows[0]["NewProdNo"].ToString();
            result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "INSERT_PRO_PROCESS", DPRno, Batchcode, userid, Terminal, Sessionid, Posteddat, ppno, "", "");

        }

        protected void imgbtnPreDPR_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblbgddat");
            string comcod = this.GetCompCode();
            string PBMno = "";//this.DDLMasterLC.SelectedValue.ToString();
            string date = this.txtbgddate.Text.Trim().ToString();
            DataSet ds6 = Budget.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETDPRNOLIST", date, PBMno, "", "", "", "", "", "", "");
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


        protected void ddlDPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblbgddat"];

            string dprno = this.ddlDPR.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("PREQNO='" + dprno + "'");


            this.ddlBatch.DataTextField = "actdesc";
            this.ddlBatch.DataValueField = "actcode";
            this.ddlBatch.DataSource = dv.ToTable();
            this.ddlBatch.DataBind();
            this.txtbgddate.Text = Convert.ToDateTime(dv.ToTable().Rows[0]["pbdate"]).ToString("dd-MMM-yyyy");
            //this.ddlplan.DataTextField = "plnno1";
            //this.ddlplan.DataValueField = "plnno";
            //this.ddlplan.DataSource = dv.ToTable();
            //this.ddlplan.DataBind();
            //if (this.Request.QueryString["genno"].Length > 0)
            //{
            //    this.ddlplan.SelectedValue = this.Request.QueryString["genno"].ToString();
            //}
        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblbbudget"];
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

        ((Label)this.gvBudgetRpt.FooterRow.FindControl("lblgvFRProdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cresqty)", "")) ?
           0 : dt.Compute("sum(cresqty)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void chkSubContract_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSubContract.Checked == true)
            {
                ddlSupplierName.Visible = true;

                this.FindSupplier_Click(null, null);
            }
            else
            {
                ddlSupplierName.Visible = false;

                this.ddlSupplierName.SelectedValue = "000000000000";
            }
        }


        protected void FindSupplier_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%";
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                return;
            }
            Session["tblSup"] = ds1.Tables[0];
            this.ddlSupplierName.DataTextField = "ssirdesc1";
            this.ddlSupplierName.DataValueField = "ssircode";
            this.ddlSupplierName.DataSource = ds1.Tables[0];
            this.ddlSupplierName.SelectedValue = "000000000000";
            this.ddlSupplierName.DataBind();
        }

        protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}