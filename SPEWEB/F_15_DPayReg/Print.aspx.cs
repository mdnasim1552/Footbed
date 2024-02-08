using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_15_DPayReg
{

    public partial class Print : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            string comcod = this.Request.QueryString["comcod"].ToString();
            string chqno = this.Request.QueryString["chqno"].ToString();
            string vouchar = this.Request.QueryString["genno"].ToString();
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "CheckPrint":
                    this.PrintCheque(comcod, vouchar);
                    break;
                case "CheckPrintPost":
                    this.RptPostDatChq(comcod, vouchar, chqno);
                    break;

            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void PrintCheque(string comcod, string vounum)
        {

            //string mcomcod = hst["mcomcod"].ToString();
            //string comcod = (mcomcod == "0000") ? this.GetCompCode() : this.ddlCompany.SelectedValue.ToString();
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = this.GetCompCode();
                //string vounum = lblVoun.Text;
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("ddMMyyyy");
                string voudat2 = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MM-yyyy");
                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;

                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split(' ', ' ');
                // string[] amtdivide = amtWrd1[1].Split(' ');

                string value = "";//this.ChboxPayee.Checked ? "A/C Payee" : " ";


                for (int i = 0; i <= amtWrd1.Length - 1; i++)
                {
                    if (i == amtWrd1.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtWrd1[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtWrd1[i].ToString();
                    }
                }

                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["Payee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["date2"] = voudat2;
                hshtbl["amtWord"] = wam2.ToUpper();
                hshtbl["amtWord1"] = wam1.ToUpper();
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";

                LocalReport rpt1 = new LocalReport();


                rpt1 = RptSetupClass.GetLocalReport("RD_15_Acc.RptCheque", hshtbl, null, null);


                Session["Report1"] = rpt1;

                Response.Redirect("../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "");

            }
            catch (Exception ex)
            {
                //  this.lblmsg.Text = "Error:" + ex.Message;
            }



        }



        private void RptPostDatChq(string comcod, string vounum, string chqno)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = this.GetCompCode();
                //string vounum = lblVoun.Text;
                //string chqno = this.txtRefNum.Text;//this.ddlPostDatedCheque.SelectedValue.Substring(14);
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];

                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                string voudat2 = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");

                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string value = "";//this.ChboxPayee.Checked ? "A/C Payee" : " ";

                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split(' ', ' ');

                for (int i = 0; i <= amtWrd1.Length - 1; i++)
                {
                    if (i == amtWrd1.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtWrd1[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtWrd1[i].ToString();
                    }
                }

                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["Payee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["date2"] = voudat2;
                hshtbl["amtWord"] = wam2.ToUpper();
                hshtbl["amtWord1"] = wam1.ToUpper();
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";

                LocalReport rpt1 = new LocalReport();


                rpt1 = RptSetupClass.GetLocalReport("RD_15_Acc.RptCheque", hshtbl, null, null);


                Session["Report1"] = rpt1;
                Response.Redirect("../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "");

            }
            catch (Exception ex)
            {
                // this.lblmsg.Text = "Error:" + ex.Message;
            }
        }
    }

}