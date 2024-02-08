using System;
using System.Collections;
using System.Collections.Generic;
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

namespace SPEWEB.F_31_Mis
{
    public partial class RptLCProaLossAcount : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime("01" + date.Substring(2)).AddMinutes(1).AddDays(-1).ToString("dd-MMM-yyyy");


            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            Session.Remove("tbllcstatus");
            string frmdate = this.txtfrmDate.Text;
            string todate = this.txttodate.Text;


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_LC_LOSSORPROFIT", "RPTLCLOSSPROFIT", frmdate, todate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvinstment.DataSource = null;
                this.gvinstment.DataBind();
                return;
            }
            Session["tbllcstatus"] = ds2.Tables[0];
            this.Data_Bind();




        }

        private void Data_Bind()
        {

            this.gvinstment.DataSource = (DataTable)Session["tbllcstatus"];
            this.gvinstment.DataBind();






        }



        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string mgrp = dt1.Rows[0]["mgrp"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mgrp"].ToString() == mgrp)
                    dt1.Rows[j]["mgrpdesc"] = "";

                mgrp = dt1.Rows[j]["mgrp"].ToString();

            }

            return dt1;
        }

        private void FooterCalculation(string GrView, DataTable dt)
        {


            //if (dt.Rows.Count == 0)
            //    return;

            //switch (GrView)
            //{
            //    case "gv_OrDer":
            //        ((Label)this.gvOrDer.FooterRow.FindControl("lgvFordramtord")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordramt)", "")) ?
            //            0.00 : dt.Compute("Sum(ordramt)", ""))).ToString("#,##0;(#,##0);  ");

            //        break;


            //    case "gv_duebblc":
            //        ((Label)this.gvduebblc.FooterRow.FindControl("lgvFbillamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(billamt)", "")) ?
            //            0.00 : dt.Compute("Sum(billamt)", ""))).ToString("#,##0;(#,##0);  ");
            //        ((Label)this.gvduebblc.FooterRow.FindControl("lgvFdueamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam)", "")) ?
            //           0.00 : dt.Compute("Sum(dueam)", ""))).ToString("#,##0;(#,##0);  ");
            //        ((Label)this.gvduebblc.FooterRow.FindControl("lgvFnydueamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(nydueamt)", "")) ?
            //           0.00 : dt.Compute("Sum(nydueamt)", ""))).ToString("#,##0;(#,##0);  ");

            //        break;




            //}


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataSet ds1 = ((DataSet)Session["tbllcstatus"]).Copy();


            //ReportDocument rptstk = new RMGiRPT.R_31_Mis.RptLcStatataglance();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            //txtprojectname.Text = "LC Name: "+this.ddlProjectInd.SelectedItem.Text;

            //TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void gvinstment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ItemDesc = (Label)e.Row.FindControl("lblgvItemDesc");
                Label fcamt = (Label)e.Row.FindControl("lblgvfcamt");
                Label amount = (Label)e.Row.FindControl("lblgvamount");
                Label percntage = (Label)e.Row.FindControl("lblgvpercntage");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    ItemDesc.Font.Bold = true;
                    fcamt.Font.Bold = true;
                    amount.Font.Bold = true;
                    percntage.Font.Bold = true;
                    ItemDesc.Style.Add("text-align", "right");
                }

            }
        }

    }
}