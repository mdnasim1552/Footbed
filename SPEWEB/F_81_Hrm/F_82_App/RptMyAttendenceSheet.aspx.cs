﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.IO;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class RptMyAttendenceSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "  Employee Status";
                this.getMyAttData();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void getMyAttData()
        {
            string comcod = this.GetComeCode();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string empid = this.Request.QueryString["empid"].ToString();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            //this.lblDateOn.Text = " From " + this.Request.QueryString["frmdate"].ToString() + " To " + this.Request.QueryString["todate"].ToString();
            this.lblcompname.Text = ds1.Tables[2].Rows[0]["companyname"].ToString();
            this.lblname.Text = ds1.Tables[0].Rows[0]["empnam"].ToString();
            this.lbldpt.Text = ds1.Tables[0].Rows[0]["empdept"].ToString();
            this.lbldesg.Text = ds1.Tables[0].Rows[0]["empdsg"].ToString();
            this.lblcard.Text = ds1.Tables[0].Rows[0]["idcardno"].ToString();
            this.lblIntime.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimein"]).ToString("hh:mm tt");
            this.lblout.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimeout"]).ToString("hh:mm tt");
            this.lblwork.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["twrkday"]).ToString("#, ##0;(#, ##0);");
            this.lblLate.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tlday"]).ToString("#, ##0;(#, ##0);");
            this.lblLeave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tlvday"]).ToString("#, ##0;(#, ##0);");
            this.lblAbsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tabsday"]).ToString("#, ##0;(#, ##0);");
            this.lblHoliday.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["thday"]).ToString("#, ##0;(#, ##0);");
            this.lblOvtime.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["ttot"]).ToString("#, ##0;(#, ##0);");


            Session["tblempdatewise"] = ds1.Tables[0];




            this.RptMyAttenView.DataSource = ds1;
            this.RptMyAttenView.DataBind();




        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintEmpAttnIdWise();
        }

        private void PrintEmpAttnIdWise()
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comnam = hst["comnam"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string comcod = this.GetComeCode();
            // //string Company = "";
            //// string PCompany = "";
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string empid = this.Request.QueryString["empid"].ToString();
            // string frmdate = this.Request.QueryString["frmdate"].ToString();
            // string todate = this.Request.QueryString["todate"].ToString();
            // DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");


            // DataTable dtdailyiemp = ds4.Tables[0];
            // int sum = 0;
            // string hour, minute;
            // for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            // {
            //     sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            // }
            // hour = Convert.ToInt32(sum / 60).ToString();
            // minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            // string TotalHour = hour + ":" + minute;
            // ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptDailyAttnEmp();
            // rptTest.SetDataSource(ds4.Tables[0]);
            // TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            // txtRptComName.Text = ds4.Tables[2].Rows[0]["companyname"].ToString();

            // TextObject txttowrkday = rptTest.ReportDefinition.ReportObjects["txttowrkday"] as TextObject;
            // txttowrkday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            // TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            // txttolateday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            // TextObject txttoleaveday = rptTest.ReportDefinition.ReportObjects["txttoleaveday"] as TextObject;
            // txttoleaveday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            // TextObject txtoabsday = rptTest.ReportDefinition.ReportObjects["txtoabsday"] as TextObject;
            // txtoabsday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            // TextObject txtohday = rptTest.ReportDefinition.ReportObjects["txtohday"] as TextObject;
            // txtohday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");


            // TextObject txtrptTotalHour = rptTest.ReportDefinition.ReportObjects["txtTHour"] as TextObject;
            // txtrptTotalHour.Text = TotalHour;
            // TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            // Session["Report1"] = rptTest;

            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void RptMyAttenView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {


                string ahleave = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "leav")).ToString();

                DateTime offimein = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimein"));
                DateTime offouttim = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimeout"));


                DateTime actualin = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualin"));
                DateTime actualout = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualout"));



                if (ahleave == "A" || ahleave == "H" || ahleave == "Lv")
                {
                    ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                    ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                    ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";

                }
                else if (offimein < actualin || offouttim > actualout)
                {
                    ((Label)e.Item.FindControl("lblactualout")).Attributes["style"] = "font-weight:bold; color:red;";
                    ((Label)e.Item.FindControl("lblactualin")).Attributes["style"] = "font-weight:bold; color:red;";
                    ((Label)e.Item.FindControl("lbldtimehour")).Attributes["style"] = "font-weight:bold; color:red;";


                }

            }



            if (e.Item.ItemType == ListItemType.Footer)
            {
                double AcTime = 0.00;
                DataTable dt3 = (DataTable)Session["tblempdatewise"];
                foreach (DataRow dr in dt3.Rows)
                {
                    double time = Convert.ToDouble("0" + dr["actTimehour"]);
                    AcTime = AcTime + time;
                }
                ((Label)e.Item.FindControl("lblTotalHour")).Text = AcTime.ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");

                //Double actTimehour =Convert.ToDouble(dt3.Rows[0]["actTimehour"]);
                //((Label)e.Item.FindControl("lblTotalHour")).Text = Convert.ToDouble((Convert.IsDBNull(Convert.ToDouble(dt3.Compute("Sum(actTimehour)", ""))))).ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");

            }
        }
    }
}