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
    public partial class MatAvailability : System.Web.UI.Page
    {
        ProcessAccess Budget = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProduct();
                this.TableCreate();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "FG") ? "MATERAILS AVAILABILITY INFORMATION-FG" : "MATERAILS AVAILABILITY INFORMATION-Semi FG"; 
            }
            this.CommonButton();

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Budget";
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lbtnGenerate_Click);

        }

        
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Generate";

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private void TableCreate()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("prodcode", Type.GetType("System.String"));
            tblt01.Columns.Add("proddesc", Type.GetType("System.String"));
            tblt01.Columns.Add("produnit", Type.GetType("System.String"));
            tblt01.Columns.Add("qty", Type.GetType("System.Double"));
            Session["tblbbudget"] = tblt01;


        }

        private void GetProduct()
        {

            string comcod = this.GetCompCode();
            string filter = this.txtsearchProduct.Text.Trim() + "%";
            string pCode = (this.Request.QueryString["Type"] == "FG") ? "41%" : "04%";
            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_02", "GETMAINPROCODE", filter, pCode, "", "", "", "", "", "", "");
            this.ddlProduct.DataSource = ds3.Tables[0];
            this.ddlProduct.DataTextField = "proddesc";
            this.ddlProduct.DataValueField = "prodcode";
            this.ddlProduct.DataBind();
            Session["tblProduct"] = ds3.Tables[0];
            ds3.Dispose();

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


        private void ShowProduct()
        {
            Session.Remove("tblbbudget");
            string comcod = this.GetCompCode();

            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_02", "GETMAINPROCODE", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvBudget.DataSource = null;
                this.gvBudget.DataBind();
                return;
            }
            Session["tblbbudget"] = ds3.Tables[0];


            ds3.Dispose();


            this.Data_Bind();
        }

        private void ShowBgdQty()
        {
            Session.Remove("tbAllData");
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            string pCode = (this.Request.QueryString["Type"] == "FG") ? "41%" : "04%";
            DataSet ds3 = Budget.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_02", "SWOWBUDGETEDAVAIL", date, pCode, "", "", "", "", "", "", "");
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
            this.gvBudgetQty.DataSource = dv.Table;
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
            this.GetProduct();
        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string ProdCode = this.ddlProduct.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblbbudget"];
            DataRow[] dr = dt.Select(" prodcode='" + ProdCode + "'");
            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["prodcode"] = ProdCode;
                dr1["proddesc"] = this.ddlProduct.SelectedItem.Text.Trim();
                dr1["produnit"] = (((DataTable)Session["tblProduct"]).Select("prodcode='" + ProdCode + "'"))[0]["produnit"];
                dr1["qty"] = 0.00;
                dt.Rows.Add(dr1);

            }
            Session["tblbbudget"] = dt;
            this.Data_Bind();
        }


        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblbbudget"];
            int TblRowIndex, i;


            for (i = 0; i < this.gvBudget.Rows.Count; i++)
            {
                double bgdqty = Convert.ToDouble("0" + ((TextBox)this.gvBudget.Rows[i].FindControl("txtgvbgdQty")).Text.Trim());
                TblRowIndex = (gvBudget.PageIndex) * gvBudget.PageSize + i;
                dt.Rows[TblRowIndex]["qty"] = bgdqty;
            }

            Session["tblbbudget"] = dt;
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

            DataTable dt = (DataTable)Session["tblbbudget"];

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

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

        //protected void gvBudgetRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    this.gvBudgetQty.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}

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
            string ProdCode = this.ddlProduct.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tblbbudget"];
            DataTable tbl2 = (DataTable)Session["tblProduct"];
            DataRow[] dr = tbl1.Select("prodcode='" + ProdCode + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["prodcode"] = tbl2.Rows[i]["prodcode"].ToString();
                    dr1["proddesc"] = tbl2.Rows[i]["proddesc"].ToString();
                    dr1["produnit"] = tbl2.Rows[i]["produnit"].ToString();
                    dr1["qty"] = 0.00;
                    tbl1.Rows.Add(dr1);
                }
                Session["tblbbudget"] = tbl1;
            }
            this.Data_Bind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 0;
            this.PnlOth.Visible = true;
        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable tbl2 = (DataTable)Session["tblbbudget"];
            bool result = false;

            result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_02", "DELETEAVA", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            { return; }

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {

                string Prodcode = tbl2.Rows[i]["prodcode"].ToString();
                double qty = Convert.ToDouble(tbl2.Rows[i]["qty"].ToString().Trim());
                if (qty > 0)
                {
                    result = Budget.UpdateTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_02", "INORUPDATEBUDGETAVAIL", Prodcode, qty.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "");
                }
                if (result == false)
                { return; }

            }
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
            this.lblmsg.Visible = true;
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
                        this.lblmsg.Text = Budget.ErrorObject["Msg"].ToString();
                        this.lblmsg.BackColor = System.Drawing.Color.Red;
                        this.lblmsg.ForeColor = System.Drawing.Color.White;

                        return;
                    }
                }
            }
            this.lblmsg.Text = "Updated Successfully";
            this.lblmsg.BackColor = System.Drawing.Color.Green;
            this.lblmsg.ForeColor = System.Drawing.Color.White;

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
    }
}