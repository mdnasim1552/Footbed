using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using SPEENTITY;
using SPERDLC;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_09_Commer
{
    public partial class RptLCPosition : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ////this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = date;
                this.SelectView();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "LCPosition") ? "L/C Status"
                    : "Month Wise Collection-Summary";

                this.GetLcType();




            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetLcType()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds2 = AccData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "GETLCTYPE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                this.ddllctypelist.DataSource = ds2.Tables[0];
                this.ddllctypelist.DataTextField = "actdesc";
                this.ddllctypelist.DataValueField = "actcode";
                this.ddllctypelist.SelectedValue = "000000000000";
                this.ddllctypelist.DataBind();

                ds2.Dispose();

            }
        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LCPosition":
                    this.txtfromdate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-6).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "LCPosition":
                    string rptType = this.rdbtnRptType.SelectedValue;

                    if (rptType == "Details")
                    {
                        this.MultiView1.ActiveViewIndex = 0;
                        this.ShowMonCollvsPay();
                    }
                    else
                    {
                        this.MultiView1.ActiveViewIndex = 1;
                        this.ShowSummary();
                    }
                    break;



            }
        }


        private void ShowMonCollvsPay()
        {
            UserManagerLCM objUser = new UserManagerLCM();
            Session.Remove("tbLCPosition");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txtfromdate.Text.Trim()), Convert.ToDateTime(this.txttodate.Text.Trim()));
            if (mon > 6)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Six');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string lctype = (this.ddllctypelist.SelectedValue.ToString() == "000000000000") ? "%" : this.ddllctypelist.SelectedValue.ToString().Substring(0, 8) + "%";


            List<SPEENTITY.C_09_Commer.EClassLCStatus> lst3 = new List<SPEENTITY.C_09_Commer.EClassLCStatus>();


            lst3 = objUser.ShowLCStatus("SP_REPORTS_LC_INFO", "SHOWLCPOSITION", lctype, txtdatefrm, txtdateto);


            Session["tbLCPosition"] = HiddenSameData(lst3);

            this.Data_Bind();
        }

        private void ShowSummary()
        {
            string comcod = this.GetCompCode();
            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string lctype = (this.ddllctypelist.SelectedValue.ToString() == "000000000000") ? "%" : this.ddllctypelist.SelectedValue.ToString().Substring(0, 8) + "%";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "SHOWLCPOSITION", lctype, txtdatefrm, txtdateto, "summary", "", "");

            if (ds1 == null)
            {
                ViewState["tblLcSummary"] = null;

                this.grvLCSummary.DataSource = null;
                this.grvLCSummary.DataBind();
                this.FooterCalculation();
                return;
            }

            ViewState["tblLcSummary"] = ds1.Tables[0];

            this.grvLCSummary.DataSource = ds1.Tables[0];
            this.grvLCSummary.DataBind();
            this.FooterCalculation();


            //DataTable dt = (DataTable)ViewState["tblLcSummary"];
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            //((Label)this.grvLCSummary.FooterRow.FindControl("lblgvFlcsLcValUSD")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(lcamt)", "")) ?
            //       0 : ds1.Tables[0].Compute("sum(lcamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.grvLCSummary.FooterRow.FindControl("lblgvFlcsLcValEuro")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(euroval)", "")) ?
            //       0 : ds1.Tables[0].Compute("sum(euroval)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }


        private List<SPEENTITY.C_09_Commer.EClassLCStatus> HiddenSameData(List<SPEENTITY.C_09_Commer.EClassLCStatus> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string actcode = "";
            //string code1 = "";
            foreach (SPEENTITY.C_09_Commer.EClassLCStatus c1 in lst3)
            {
                if (i == 0)
                {
                    actcode = c1.actcode;
                    // code1 = c1.code1;
                    i++;
                    continue;

                }
                else if (c1.actcode == actcode)
                {
                    c1.actdesc = "";
                    c1.lcdate = "";
                    c1.shipdate = "";
                    c1.shipardate = "";
                    c1.expdate = "";
                    c1.deldate = "";
                    c1.rcvdate = "";

                    c1.fileno = "";
                    c1.lpaytrm = "";
                    c1.befnam = "";
                    c1.blno = "";
                    c1.bldat = "";
                    c1.etadat = "";
                    c1.beno = "";
                    c1.bedat = "";
                    c1.docpos = "";
                    c1.cnf = "";
                    c1.paydat = "";
                    c1.deltrm = "";
                    c1.delmod = "";
                    c1.remarks = "";
                    c1.lcval = 0.00;
                }
                actcode = c1.actcode;

            }

            return lst3;

        }


        private void Data_Bind()
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();

                switch (type)
                {

                    case "LCPosition":

                        List<SPEENTITY.C_09_Commer.EClassLCStatus> lst3 = (List<SPEENTITY.C_09_Commer.EClassLCStatus>)Session["tbLCPosition"];

                        this.grvLC.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue);
                        this.grvLC.DataSource = lst3;
                        this.grvLC.DataBind();
                        //this.FooterCalculation();
                        break;

                }

            }
            catch (Exception ex)
            {


            }

        }



        protected void FooterCalculation()
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();

                switch (type)
                {
                    case "LCPosition":

                        DataTable dt = (DataTable)ViewState["tblLcSummary"];

                        if(dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                double lcUSD = Convert.ToDouble(dt.Compute("SUM(lcamt)", string.Empty));
                                double lcEuro = Convert.ToDouble(dt.Compute("SUM(euroval)", string.Empty));

                                ((Label)(this.grvLCSummary.FooterRow.FindControl("flTtlLcUSD"))).Text = lcUSD.ToString("#,##0.00;(#,##0.00); ");
                                ((Label)(this.grvLCSummary.FooterRow.FindControl("flTtlLcEuro"))).Text = lcEuro.ToString("#,##0.00;(#,##0.00); ");
                            }
                        }
                        

                        DataTable dt2 = (DataTable)ViewState["LcPayHistoy"];

                        if (dt2 != null)
                        {
                            if (dt2.Rows.Count > 0 && dt2 != null)
                            {
                                double lcpayamt = Convert.ToDouble(dt2.Compute("SUM(trnam)", string.Empty));
                                double lcpayfcamt = Convert.ToDouble(dt2.Compute("SUM(fcamt)", string.Empty));

                                ((Label)(this.grvLcPay.FooterRow.FindControl("fLblAmt"))).Text = lcpayamt.ToString("#,##0.00;(#,##0.00); ");
                                ((Label)(this.grvLcPay.FooterRow.FindControl("fLblFcAmt"))).Text = lcpayfcamt.ToString("#,##0.00;(#,##0.00); ");
                            }
                        }

                        break;

                }
            }
            catch (Exception ex)
            {

            }

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "LCPosition":
                    string rptType = this.rdbtnRptType.SelectedValue;

                    if (rptType == "Details")
                    {
                        this.PrintLCPostion();
                    }
                    else
                    {
                        this.PrintLCPostionSummary();
                    }

                    break;

            }


        }

        private void PrintLCPostionSummary()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string fDate = this.txtfromdate.Text;
            string tDate = this.txttodate.Text;

            var dt = (DataTable)ViewState["tblLcSummary"];
            var list = dt.DataTableToList<SPEENTITY.C_09_Commer.EClassLCStatus>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptLCPositionSummary", list, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("Date", "Date: " + fDate + " To " + tDate));
            rpt1.SetParameters(new ReportParameter("RptTitle", "LC STATEMENT SUMMARY")); // 
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintLCPostion()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string fDate = this.txtfromdate.Text;
            string tDate = this.txttodate.Text;

            var list = (List<SPEENTITY.C_09_Commer.EClassLCStatus>)Session["tbLCPosition"];

            LocalReport rpt1 = new LocalReport();


            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptLCPosition", list, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("Date", "Date: " + fDate + " To " + tDate));
            rpt1.SetParameters(new ReportParameter("RptTitle", "MATERIAL STATEMENT")); // 
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }



        protected void grvLC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label mdesc = (Label)e.Row.FindControl("lgvActdesc");
            //    Label OpDate = (Label)e.Row.FindControl("lgvOpDate");
            //    Label ProDesc = (Label)e.Row.FindControl("lgvProDesc");
            //    Label Qty = (Label)e.Row.FindControl("lgvQty");
            //    Label PAmt = (Label)e.Row.FindControl("lgvAmt");
            //    Label Lcstatus = (Label)e.Row.FindControl("lgvLcstatus");

            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescod")).ToString();
            //    string lcstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lcstatus")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Right(code, 4) == "0000")
            //    {
            //        mdesc.Font.Bold = true;
            //        ProDesc.Font.Bold = true;
            //        OpDate.Font.Bold = true;
            //        Qty.Font.Bold = true;
            //        PAmt.Font.Bold = true;
            //        Lcstatus.Font.Bold = true;


            //        mdesc.Style.Add("color", "blue");
            //        OpDate.Style.Add("color", "blue");
            //        Lcstatus.Style.Add("color", "blue");
            //    }

            //    if (lcstatus == "Pipeline")
            //    {
            //        mdesc.Font.Bold = true;
            //        ProDesc.Font.Bold = true;
            //        OpDate.Font.Bold = true;
            //        Qty.Font.Bold = true;
            //        PAmt.Font.Bold = true;
            //        Lcstatus.Font.Bold = true;


            //        mdesc.Style.Add("color", "red");
            //        OpDate.Style.Add("color", "red");
            //        ProDesc.Style.Add("color", "red");
            //        PAmt.Style.Add("color", "red");
            //        Qty.Style.Add("color", "red");
            //        Lcstatus.Style.Add("color", "red");
            //    }


            //}
        }

        protected void lblgvlcsContract_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = GetCompCode();

                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string actcode = ((Label)this.grvLCSummary.Rows[index].FindControl("lblgvlcsActCode")).Text.ToString();

                string LcCode = actcode;

                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "LC_PAYMENT_HISTORY", LcCode);

                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    this.grvLcPay.DataSource = ds1.Tables[0];
                    this.grvLcPay.DataBind();
                    ViewState["LcPayHistoy"] = ds1.Tables[0];
                    this.FooterCalculation();

                    string lcNo = ((Label)this.grvLCSummary.Rows[index].FindControl("lblgvlcsLcNo")).Text.ToString();
                    string supplyName = ((Label)this.grvLCSummary.Rows[index].FindControl("lblgvlcsNamOf")).Text.ToString();

                    this.lblLcNo.Text = " " + lcNo;
                    this.lblSuppplyName.Text = " " + supplyName;

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);

                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}