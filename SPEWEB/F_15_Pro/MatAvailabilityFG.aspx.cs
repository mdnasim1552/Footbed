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
    public partial class MatAvailabilityFG : System.Web.UI.Page
    {
        ProcessAccess Budget = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.TableCreate();
                this.GetOrderNumber();


                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "FG") ? "MATERAILS AVAILABILITY INFORMATION-FG" : "MATERAILS AVAILABILITY INFORMATION-Semi FG";



            }

            this.CommonButton();

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnGenerate_Click);

        }


        private void CommonButton()
        {

           
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Generate";

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetOrderNumber()
        {
            string comcod = this.GetCompCode();
            string Searchorder = "%%";
            DataSet ds1 = Budget.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", Searchorder, "%", "%", "", "", "", "", "", "");
            this.ddlOrder.DataTextField = "mlcdesc";
            this.ddlOrder.DataValueField = "mlccod";
            this.ddlOrder.DataSource = ds1.Tables[1];
            this.ddlOrder.DataBind();
            ViewState["tblProduct"] = ds1.Tables[0];
            if (this.Request.QueryString["actcode"].ToString().Length > 0)
            {
                this.lbtnOk_Click(null, null);
                this.ddlOrder.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }
            ddlOrder_SelectedIndexChanged(null, null);
        }
        private void TableCreate()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("prodcode", Type.GetType("System.String"));
            tblt01.Columns.Add("colorid", Type.GetType("System.String"));
            tblt01.Columns.Add("dayid", Type.GetType("System.String"));
            tblt01.Columns.Add("colordesc", Type.GetType("System.String"));
            tblt01.Columns.Add("proddesc", Type.GetType("System.String"));
            tblt01.Columns.Add("produnit", Type.GetType("System.String"));
            tblt01.Columns.Add("qty", Type.GetType("System.Double"));
            ViewState["tblbbudget"] = tblt01;


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintMatAvaReport();
        }

        protected void PrintMatAvaReport()
        {
            //try
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //    DataTable dt = (DataTable)Session["tbAllData"];

            //    //DataView dv = dt.DefaultView;
            //    //dv.RowFilter = ("avaqty< 0");
            //    //dt = dv.ToTable();
            //    ReportDocument rptinfo = new MFGRPT.R_03_StdCost.MatAvailablity();
            //    TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //    rpttxtVoutype.Text = comnam;
            //    TextObject rpttxtlblBpn = rptinfo.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //    rpttxtlblBpn.Text = "Date: " + this.txtdate.Text.Trim().ToString();
            //    TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptinfo.SetDataSource(dt);
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptinfo.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptinfo;


            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //}
            //catch (Exception ex)
            //{
            //    //this.lblmsg.Text = "Error:" + ex.Message;
            //}

        }







        private void ShowBgdQty()
        {
            Session.Remove("tbAllData");
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            string mlccod = this.ddlOrder.SelectedValue.ToString();
            string pCode = (this.Request.QueryString["Type"] == "FG") ? "03%" : "04%";
            DataTable dt = (DataTable)ViewState["tblbbudget"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tblitem";
            DataSet ds3 = Budget.GetTransInfoNew(comcod, "SP_ENTRY_BATCH_BUDGET_03", "GET_STYLE_ANALYSIS", ds, null, null, date, pCode, mlccod, "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvBudgetQty.DataSource = null;
                this.gvBudgetQty.DataBind();
                return;
            }
            ds3.Dispose();
            Session["tbAllData"] = ds3.Tables[0];

            DataView dv = ds3.Tables[0].DefaultView;
            dv.RowFilter = ("grp='B'");
            if (dv.ToTable() == null || dv.ToTable().Rows.Count==0)
                return;
            this.gvBudgetQty.DataSource = dv.ToTable();
            this.gvBudgetQty.DataBind();
            Session["Report1"] = gvBudgetQty;
            ((HyperLink)this.gvBudgetQty.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }




        //private DataTable HiddenSameData(DataTable dt1)
        //{

        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    string grp = dt1.Rows[0]["grp"].ToString();
        //    string procode = dt1.Rows[0]["procode"].ToString();

        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["procode"].ToString() == procode)
        //        {
        //            grp = dt1.Rows[j]["grp"].ToString();
        //            procode = dt1.Rows[j]["procode"].ToString();
        //            dt1.Rows[j]["grpdesc"] = "";
        //            dt1.Rows[j]["prodesc"] = "";

        //        }

        //        else
        //        {
        //            if (dt1.Rows[j]["procode"].ToString() == procode)
        //            {
        //                dt1.Rows[j]["prodesc"] = "";
        //            }

        //            if (dt1.Rows[j]["grp"].ToString() == grp)
        //            {
        //                dt1.Rows[j]["grpdesc"] = "";
        //            }

        //            grp = dt1.Rows[j]["grp"].ToString();
        //            procode = dt1.Rows[j]["procode"].ToString();

        //        }

        //    }
        //    return dt1;

        //}

        protected void imgbtnsrchProduct_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string ProdCode = this.ddlProduct.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlProduct.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlProduct.SelectedValue.ToString().Substring(24, 8);
            DataTable dt = (DataTable)ViewState["tblbbudget"];
            DataRow[] dr = dt.Select(" prodcode='" + ProdCode + "' and colorid='" + colorid + "'");
            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["prodcode"] = ProdCode;
                dr1["proddesc"] = this.ddlProduct.SelectedItem.Text.Trim();
                dr1["colorid"] = colorid;
                dr1["dayid"] = dayid;
                dr1["produnit"] = (((DataTable)ViewState["tblProduct"]).Select("stylecode='" + ProdCode + "'"))[0]["unit1"];
                dr1["qty"] = (this.Request.QueryString["genno"].Length > 0) ? Convert.ToDouble(this.Request.QueryString["genno"]) : 0.00;
                dt.Rows.Add(dr1);

            }
            ViewState["tblbbudget"] = dt;
            this.Data_Bind();
        }


        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblbbudget"];
            int TblRowIndex, i;


            for (i = 0; i < this.gvBudget.Rows.Count; i++)
            {
                double bgdqty = Convert.ToDouble("0" + ((TextBox)this.gvBudget.Rows[i].FindControl("txtgvbgdQty")).Text.Trim());
                TblRowIndex = (gvBudget.PageIndex) * gvBudget.PageSize + i;
                dt.Rows[TblRowIndex]["qty"] = bgdqty;
            }

            ViewState["tblbbudget"] = dt;
        }
        private void SaveValueForBudgqty()
        {

            DataTable dt = (DataTable)Session["tbAllData"];
            int TblRowIndex, i;


            for (i = 0; i < this.gvBudget.Rows.Count; i++)
            {
                double bgdqty = Convert.ToDouble("0" + ((TextBox)this.gvBudgetQty.Rows[i].FindControl("TxtgvQty")).Text.Trim());
                TblRowIndex = (gvBudgetQty.PageIndex) * gvBudgetQty.PageSize + i;
                dt.Rows[TblRowIndex]["qty"] = bgdqty;
            }

            Session["tbAllData"] = dt;
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblbbudget"];

            this.gvBudget.DataSource = dt;
            this.gvBudget.DataBind();

            //this.gvBudgetRate.DataSource = dt;
            //this.gvBudgetRate.DataBind();

        }
        protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBudget.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBudget_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblbbudget"];
            int rowindex = (this.gvBudget.PageSize) * (this.gvBudget.PageIndex) + e.RowIndex;
            tbl1.Rows[rowindex].Delete();
            ViewState["tblbbudget"] = tbl1;
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

        protected void lbtnTotalRate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

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


        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string mlccod = this.ddlOrder.SelectedValue.ToString();
            string ProdCode = this.ddlProduct.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlProduct.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlProduct.SelectedValue.ToString().Substring(24, 8);
            DataTable tbl1 = (DataTable)ViewState["tblbbudget"];
            DataTable tbl2 = (DataTable)ViewState["tblProduct"];
            DataView dv = tbl2.DefaultView;
            dv.RowFilter = "mlccod='" + mlccod + "'";
            tbl2 = dv.ToTable();
            DataRow[] dr = tbl1.Select("prodcode='" + ProdCode + "' and colorid='" + colorid + "' and dayid='" + dayid + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["colorid"] = tbl2.Rows[i]["colorid"].ToString();
                    dr1["dayid"] = tbl2.Rows[i]["dayid"].ToString();
                    dr1["prodcode"] = tbl2.Rows[i]["stylecode"].ToString();
                    dr1["proddesc"] = tbl2.Rows[i]["styledesc1"].ToString();
                    dr1["produnit"] = tbl2.Rows[i]["unit1"].ToString();
                    dr1["qty"] = 0.00;
                    tbl1.Rows.Add(dr1);
                }
                ViewState["tblbbudget"] = tbl1;
            }
            this.Data_Bind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 0;
            
        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            //string comcod = this.GetCompCode();
            //DataTable tbl2 = (DataTable)ViewState["tblbbudget"];
            //bool result = false;

            //result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_02", "DELETEAVA", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (result == false)
            //{ return; }

            //for (int i = 0; i < tbl2.Rows.Count; i++)
            //{

            //    string Prodcode = tbl2.Rows[i]["prodcode"].ToString();
            //    double qty = Convert.ToDouble(tbl2.Rows[i]["qty"].ToString().Trim());
            //    if (qty > 0)
            //    {
            //        result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_02", "INORUPDATEBUDGETAVAIL", Prodcode, qty.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "");
            //    }
            //    if (result == false)
            //    { return; }

            //}
            this.lblCost.Visible = true;
            this.lblProSt.Visible = true;
            this.ShowBgdQty();
        }
        protected void gvBudgetQty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label AvQty = (Label)e.Row.FindControl("lblgvAvQty");
                double Qty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "avaqty"));

                if (Qty == 0.00)
                {
                    return;
                }
                if (Qty < 0)
                {
                    AvQty.Style.Add("color", "red");
                }

            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetProdBudgetNo()
        {
            string comcod = this.GetCompCode();
            string Date = this.txtdate.Text.Trim();
            DataSet ds2 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "GETPROBUDGETNO", Date, "", "", "", "", "", "", "", "");
            string Bpno = ds2.Tables[0].Rows[0]["bpno"].ToString();
            return Bpno;


        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            
            this.SaveValueForBudgqty();
            string comcod = this.GetCompCode();
            //string BatchCode = this.ddlBatch.SelectedValue.ToString();
            string pbnno = this.GetProdBudgetNo();

            //------------
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string EditByid = "";
            string Editdat = "01-Jan-1900";
            string sDate = this.txtdate.Text;
            string tDate = this.txtdate.Text;

            //-------------
            string date = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            bool result = false;
            result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEBGDWRKLOG", pbnno, date, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, sDate, tDate, "", "", "", "", "");


            // bool result=false;
            for (int i = 0; i < gvBudgetQty.Rows.Count; i++)
            {

                string ProdCode = ((Label)this.gvBudgetQty.Rows[i].FindControl("LblSircode")).Text.ToString();
                double Bgdqty = Convert.ToDouble("0" + ((TextBox)this.gvBudgetQty.Rows[i].FindControl("TxtgvQty")).Text);
                if (Bgdqty > 0 && ((CheckBox)this.gvBudgetQty.Rows[i].FindControl("chkack")).Checked)
                {
                    result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET", "INORUPDATEBUDGET", "000000000000", pbnno, ProdCode, Bgdqty.ToString(), date, "", "", "", "", "", "", "", "", "", "");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Budget.ErrorObject["Msg"].ToString() + "');", true);


                        return;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvBudgetQty.HeaderRow.FindControl("chkall")).Checked)
            {
                for (i = 0; i < this.gvBudgetQty.Rows.Count; i++)
                {

                    ((CheckBox)this.gvBudgetQty.Rows[i].FindControl("chkack")).Checked = true;
                }
            }
            else
            {
                for (i = 0; i < this.gvBudgetQty.Rows.Count; i++)
                {

                    if (((CheckBox)this.gvBudgetQty.Rows[i].FindControl("chkack")).Enabled == true)
                    {
                        ((CheckBox)this.gvBudgetQty.Rows[i].FindControl("chkack")).Checked = false;
                    }
                }
            }
        }

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlOrder.SelectedValue.ToString();
            DataTable dt1 = (DataTable)ViewState["tblProduct"];
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "mlccod", "stylecode1", "styledesc1");
            ddlProduct.DataTextField = "styledesc1";
            ddlProduct.DataValueField = "stylecode1";
            ddlProduct.DataSource = dt1;
            ddlProduct.DataBind();
            if (this.Request.QueryString["sircode"].ToString().Length > 0)
            {
                this.ddlProduct.SelectedValue = this.Request.QueryString["sircode"].ToString();
                this.lbtnSelect_Click(null, null);
                this.lbtnGenerate_Click(null, null);
            }
        }
    }
}