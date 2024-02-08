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

namespace SPEWEB.F_21_GAcc
{
    public partial class RptAccSpLedgerDet : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double dramt, cramt, opnamt, clsamt, balamt;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowDetailLedger();

            this.LblSchReportPeriod.Text = "(From " + Convert.ToDateTime(Request.QueryString["Date1"].ToString().Trim()).ToString("dd-MMM-yyyy") +
                " to " + Convert.ToDateTime(Request.QueryString["Date2"].ToString().Trim()).ToString("dd-MMM-yyyy") + ")";
            this.LblAcchead.Text = Request.QueryString["head"].ToString().Trim();
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.PrintDetailLedger();

        }
        private void PrintDetailLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptsl = new RMGiRPT.R_21_GAcc.RPTSpecialLedger();
            //DataTable dt = (DataTable)Session["tblspledger"];

            ////DataView dv = dt.DefaultView;
            ////dv.RowFilter = "head1='03CT'";
            ////dt = dv.ToTable();

            ////string opam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
            ////        0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            ////string dram = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
            ////        0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ////string cram = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
            ////        0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ////string clsam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
            ////        0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");


            //TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            ////TextObject txtOpam = rptsl.ReportDefinition.ReportObjects["opening"] as TextObject;
            ////txtOpam.Text = opam;
            ////TextObject txtDr = rptsl.ReportDefinition.ReportObjects["dr"] as TextObject;
            ////txtDr.Text = dram;
            ////TextObject txtCr = rptsl.ReportDefinition.ReportObjects["cr"] as TextObject;
            ////txtCr.Text = cram;
            ////TextObject txtCloseing = rptsl.ReportDefinition.ReportObjects["closeing"] as TextObject;
            ////txtCloseing.Text = clsam;
            //TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "(From " + Convert.ToDateTime(Request.QueryString["Date1"].ToString().Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(Request.QueryString["Date2"].ToString().Trim()).ToString("dd-MMM-yyyy") + ")";
            //TextObject rpttxtAccDesc = rptsl.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //rpttxtAccDesc.Text = "Account Description: " + dt.Rows[0]["resdesc"].ToString();

            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            //Session["Report1"] = rptsl;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void ShowDetailLedger()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Request.QueryString["comcod"].ToString().Trim();
            string frmdate = Request.QueryString["Date1"].ToString().Trim();
            string todate = Request.QueryString["Date2"].ToString().Trim();
            string resource = Request.QueryString["actcode"].ToString().Trim();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvSpledger.DataSource = null;
                this.gvSpledger.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblspledger"] = dt;
            this.gvSpledger.DataSource = dt;
            this.gvSpledger.DataBind();
            //this.FooterCal();


        }




        //private void SaveValue()
        //{
        //    DataTable dt = (DataTable)Session["tblspledger"];

        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFOpnbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnbill)", "")) ?
        //                  0 : dt.Compute("sum(opnbill)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFOpnAdv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnadv)", "")) ?
        //          0 : dt.Compute("sum(opnadv)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFCrAmtas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
        //           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFDrAmtas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
        //          0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFclsbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsbill)", "")) ?
        //           0 : dt.Compute("sum(clsbill)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFclsadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsadv)", "")) ?
        //          0 : dt.Compute("sum(clsadv)", ""))).ToString("#,##0;(#,##0); ");



        //}


        private DataTable HiddenSameData(DataTable dt1)
        {

            string vounum = dt1.Rows[0]["vounum1"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum1"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum1"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {

                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                    {

                        dt1.Rows[j]["vounum1"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();
                }

            }
            return dt1;

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblspledger"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "head1='03CT'";
            dt = dv.ToTable();
            //string type = Request.QueryString["Type"].ToString().Trim();
            //switch (type)
            //{

            // case "DetailLedger":
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFOpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
                    0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                  0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                   0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFClsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                  0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");
            //    break;

            //case "SPayment":
            //    dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?0 : dt.Compute("sum(dram)", "")));
            //    ((Label)this.gvSPayment.FooterRow.FindControl("lgvFDrAmts")).Text = dramt.ToString("#,##0;(#,##0); ");
            //   cramt=Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?0 : dt.Compute("sum(cram)", "")))  ;
            //   ((Label)this.gvSPayment.FooterRow.FindControl("lgvFCrAmts")).Text = cramt.ToString("#,##0;(#,##0); ");
            //   balamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ? 0 : dt.Compute("sum(balamt)", "")));
            //    ((Label)this.gvSPayment.FooterRow.FindControl("lgvFBalAmts")).Text = balamt.ToString("#,##0;(#,##0); ");

            //   break;

            //}

        }
        protected void gvSpledger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvVounum1");
                Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmount");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmount");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmount");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AB")
                {
                    hlink.Font.Bold = true;
                    OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;
                    ClAmt.Font.Bold = true;
                    hlink.Style.Add("text-align", "right");
                }
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14)
            {
                hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + mVOUNUM;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }


        //protected void gvAllSupPay_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    if (e.Row.RowType != DataControlRowType.DataRow)
        //        return;

        //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvSupDesc");
        //    string mCOMCOD = comcod;
        //    string sircod = ((Label)e.Row.FindControl("lblSupCode")).Text;
        //    string mTRNDAT1 = this.txtDateFrom.Text;
        //    string mTRNDAT2 = this.txtDateto.Text;

        //    //if (ASTUtility.Right(mACTCODE, 4) == "0000")
        //    //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
        //    //         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        //    //else
        //    hlink1.NavigateUrl = "RptAccSpLedgerDet.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + sircod +
        //             "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        //}

    }
}