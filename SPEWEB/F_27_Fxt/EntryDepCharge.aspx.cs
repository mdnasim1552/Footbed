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
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_27_Fxt
{
    public partial class EntryDepCharge : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "DEPRECIATION CHARGE CALCULATION";



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetOpeningDate();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }



        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());



        }

        private void GetOpeningDate()
        {

            string date = "";
            string comcod = this.GetComcod();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                date = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                this.txtFromdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy"); ;
                this.txtTodate.Text = Convert.ToDateTime(this.txtFromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                return;
            }

            date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["depodate"]).AddDays(1).ToString("dd-MMM-yyyy");
            this.txtFromdate.Text = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"); ;
            this.txtTodate.Text = Convert.ToDateTime(this.txtFromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            this.txtFromdate.ReadOnly = true;


            ds1.Dispose();


        }



        private DataTable HiddenSameData(DataTable dt1)
        {

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else
                {



                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";
                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                    {
                        dt1.Rows[j]["rsirdesc"] = "";

                    }
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();

                }

            }
            return dt1;


        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblDepcost"];

            //ReportDocument rptsale = new RMGiRPT.R_27_Fxt.RptDeprectionCost();//.rptFxtAsstValue();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptDate.Text = "Period: " + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            //int dateDife = ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));
            //int dateDife1 = dateDife + 1;
            //TextObject rpttxtDays = rptsale.ReportDefinition.ReportObjects["txtDays"] as TextObject;
            //rpttxtDays.Text = "Days : " + dateDife1.ToString();

            //TextObject txtBalance = rptsale.ReportDefinition.ReportObjects["txtBalance"] as TextObject;
            //txtBalance.Text = "Balance as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            //TextObject txtTotal = rptsale.ReportDefinition.ReportObjects["txtTotal"] as TextObject;
            //txtTotal.Text = "Total as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            //TextObject txtDepr = rptsale.ReportDefinition.ReportObjects["txtDepr"] as TextObject;
            //txtDepr.Text = "Depreciation as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            //TextObject txtWD = rptsale.ReportDefinition.ReportObjects["txtWD"] as TextObject;
            //txtWD.Text = "W.D Values as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");

            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }






        protected void grDep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grDep.PageIndex = e.NewPageIndex;
            this.grDep_DataBind();
        }

        private void grDep_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblDepcost"];
            this.grDep.Columns[3].HeaderText = "Balance as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[7].HeaderText = "Total as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            this.grDep.Columns[9].HeaderText = "Depreciation as at " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[14].HeaderText = "W.D Values as at " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");

            this.grDep.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.grDep.DataSource = tbl1;
            this.grDep.DataBind();

            this.FooterRowCal();
            if (tbl1.Rows.Count == 0)
                return;
            Session["Report1"] = grDep;
            ((HyperLink)this.grDep.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblDepcost");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frdate = Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");

            string straight = (this.chkStraight.Checked) ? "straight" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTDEPRECIATION", frdate, todate, straight, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grDep.DataSource = null;
                this.grDep.DataBind();
                return;
            }


            //int dateDife = TimeSpan(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text)); ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));

            this.txtDays.Visible = true;
            this.txtDays.Text = "Days: " + Convert.ToDouble(ds1.Tables[1].Rows[0]["cday"]).ToString("#,##0;(#,##0);");
            Session["tblDepcost"] = (DataTable)ds1.Tables[0];
            this.grDep_DataBind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grDep_DataBind();
        }
        private void FooterRowCal()
        {
            DataTable dt = (DataTable)Session["tblDepcost"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grDep.FooterRow.FindControl("lgvFTOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                   0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTAddition")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ?
                                   0 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFsalesdec")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saleam)", "")) ?
                                 0 : dt.Compute("sum(saleam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.grDep.FooterRow.FindControl("lgvFTDisposal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disam)", "")) ?
                                  0 : dt.Compute("sum(disam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ?
                                   0 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepOpen")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opndep)", "")) ?
                                   0 : dt.Compute("sum(opndep)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFadjment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adjam)", "")) ?
                                   0 : dt.Compute("sum(adjam)", ""))).ToString("#,##0;(#,##0); ");



            //((Label)this.grDep.FooterRow.FindControl("lgvFTDepCur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curdep)", "")) ?
            //                       0 : dt.Compute("sum(curdep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTcurDepop")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curdepop)", "")) ?
                                   0 : dt.Compute("sum(curdepop)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.grDep.FooterRow.FindControl("lgvFTcurDepcur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curpdepcur)", "")) ?
                                  0 : dt.Compute("sum(curpdepcur)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(todep)", "")) ?
                                   0 : dt.Compute("sum(todep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTCBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                                   0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");

            string straight = (this.chkStraight.Checked) ? "straight" : "";
            ((HyperLink)this.grDep.FooterRow.FindControl("hlnkgvFdep")).NavigateUrl = "~/F_21_GAcc/AccDepJournal.aspx?&Date1=" + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy") + "&Method=" + straight;
        }


        protected void lgvcurDepcut_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComcod();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;

            string frmdate = Convert.ToDateTime(this.txtFromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string actcode = ((Label)this.grDep.Rows[index].FindControl("lblActcode")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTCURDEPRECIATION", frmdate, todate, actcode);
            if (ds2 == null)
            {
                this.mgvbreakdown.DataSource = null;
                this.mgvbreakdown.DataBind();
                return;
            }
            this.lbmodalheading.Text = "Depreciation calculation on addition :" + ds2.Tables[0].Rows[0]["actdesc"].ToString();
            ViewState["tblCurDep"] = ds2.Tables[0];
            this.mgvbreakdown.DataSource = ds2.Tables[0];
            this.mgvbreakdown.DataBind();
            this.mgvCalculation();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        private void mgvCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblCurDep"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.mgvbreakdown.FooterRow.FindControl("LblFDepval")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curpdep)", "")) ?
                                   0 : dt.Compute("sum(curpdep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.mgvbreakdown.FooterRow.FindControl("LblFPayVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ?
                                   0 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void lbtnmisuprint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetComcod();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)ViewState["tblCurDep"];

            //var lst2 = dt.DataTableToList<RMGiEntity.C_27_Fxt.ACCWISECURDEP>();

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass1.GetLocalReport("RD_27_Fxt.RptAccWiseAddDep", lst2, null, null);

            //rpt1.SetParameters(new ReportParameter("ComName", comnam));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "Plant & Machinary"));
            ////rpt1.SetParameters(new ReportParameter("Date1", chlndat));
            ////rpt1.SetParameters(new ReportParameter("userinf", userinf));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //Session["Report1"] = rpt1;
            //string type = this.ddldowntype.SelectedValue.ToString();

            //ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTargetForRdlc('" + type + "');", true);





        }
    }
}