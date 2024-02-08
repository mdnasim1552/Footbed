using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;

using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_27_Fxt
{
    public partial class RptFixAsset02 : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Fixed Asset Report";
                this.GetDeparment();
                this.GetAsset();
                this.GetUser();
                this.GetAssetDetails();
                DropDownList1_SelectedIndexChanged(null, null);
                this.txtDateFrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetAsset()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXASSTLIST", "%", "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }


            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["sircode"] = "%%";
            dr1["sirdesc1"] = "All";
            ds1.Tables[0].Rows.Add(dr1);

            this.ddlasset.DataSource = ds1.Tables[0];
            this.ddlasset.DataTextField = "sirdesc1";
            this.ddlasset.DataValueField = "sircode";
            this.ddlasset.DataBind();
            this.ddlasset.SelectedValue = "%%";

        }

        private void GetAssetDetails()
        {
            string comcod = this.GetComeCode();
            string sercCode;
            string valuess = this.ddlasset.SelectedValue;
            if (valuess == "%%")
            {
                sercCode = "%%";
            }
            else
            {
                sercCode = ASTUtility.Left(this.ddlasset.SelectedValue, 4).ToString() + "%";
            }


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXASSTLIST", sercCode, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }



            DataRow dr1 = ds1.Tables[1].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["sircode"] = "%%";
            dr1["sirdesc1"] = "All";
            ds1.Tables[1].Rows.Add(dr1);

            this.ddlAssetDetails.DataSource = ds1.Tables[1];
            this.ddlAssetDetails.DataTextField = "sirdesc1";
            this.ddlAssetDetails.DataValueField = "sircode";
            this.ddlAssetDetails.DataBind();


            this.ddlAssetDetails.SelectedValue = "%%";

        }

        private void GetUser()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETEMPTIDNAME", "%", "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["empid"] = "%%";
            dr1["empname"] = "All";
            ds1.Tables[0].Rows.Add(dr1);

            ddluser.DataTextField = "empname";
            ddluser.DataValueField = "empid";
            ddluser.DataSource = ds1.Tables[0];
            ddluser.DataBind();
            this.ddluser.SelectedValue = "%%";
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetDeparment()
        {
            string comcod = this.GetComeCode();
            // string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", "%", "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }


            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["fxtgcod"] = "%%";
            dr1["fxtgdesc"] = "All";
            ds1.Tables[0].Rows.Add(dr1);

            this.ddldeptName.DataTextField = "fxtgdesc";
            this.ddldeptName.DataValueField = "fxtgcod";
            this.ddldeptName.DataSource = ds1.Tables[0];
            this.ddldeptName.DataBind();
            this.ddldeptName.SelectedValue = "%%";
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();

            string val = DropDownList1.SelectedValue.ToString();
            switch (val)
            {
                case "location":
                    this.gvuser.DataSource = null;
                    this.gvuser.DataBind();
                    this.gvuser.Visible = false;
                    string loct = this.ddldeptName.SelectedValue.ToString();
                    DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTASSETDEPARTMENT", loct, "", "", "", "", "", "", "", "");
                    if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                        ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                        return;
                    }

                    Session["tblLocation"] = ds1.Tables[0];

                    this.gvFixAsset.DataSource = this.HiddenSameDataDept(ds1.Tables[0]);
                    this.gvFixAsset.DataBind();
                    this.gvFixAsset.Visible = true;


                    ((Label)this.gvFixAsset.FooterRow.FindControl("lgFPurValue")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(pvalu)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(pvalu)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvFixAsset.FooterRow.FindControl("lgFDepciation")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(depreamt)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(depreamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvFixAsset.FooterRow.FindControl("lgFBookVal")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(bookval)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(bookval)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "asset":
                    this.gvuser.DataSource = null;
                    this.gvuser.DataBind();
                    this.gvuser.Visible = false;
                    this.gvFixAsset.DataSource = null;
                    this.gvFixAsset.DataBind();
                    this.gvFixAsset.Visible = false;
                    string asst = this.ddlAssetDetails.SelectedValue.ToString();
                    string valuess = this.ddlasset.SelectedValue;

                    string asstparent = ((valuess == "%%") ? "%%" : ASTUtility.Left(valuess, 4).ToString() + "%");


                    DataSet ds2 = PurData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTASSETEQUIPWISE", asst, asstparent, "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                        ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                        return;
                    }

                    Session["tblAsset"] = ds2.Tables[0];
                    this.gvassetwise.DataSource = this.HiddenSameDataAsset(ds2.Tables[0]);
                    this.gvassetwise.DataBind();
                    this.gvassetwise.Visible = true;
                    ((Label)this.gvassetwise.FooterRow.FindControl("lgFPurValue1")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(pvalu)", "")) ? 0.00 : ds2.Tables[0].Compute("sum(pvalu)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvassetwise.FooterRow.FindControl("lgFDepciation1")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(depreamt)", "")) ? 0.00 : ds2.Tables[0].Compute("sum(depreamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvassetwise.FooterRow.FindControl("lgFBookVal1")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(bookval)", "")) ? 0.00 : ds2.Tables[0].Compute("sum(bookval)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "user":
                    this.gvFixAsset.DataSource = null;
                    this.gvFixAsset.DataBind();
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.DataSource = null;
                    this.gvassetwise.DataBind();
                    this.gvassetwise.Visible = false;

                    string user = this.ddluser.SelectedValue.ToString();
                    DataSet ds3 = PurData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTASSETUSERWISE", user, "", "", "", "", "", "", "", "");
                    if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                        ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                        return;
                    }
                    Session["tblUsers"] = ds3.Tables[0];
                    this.gvuser.DataSource = this.HiddenSameDataUser(ds3.Tables[0]);
                    this.gvuser.DataBind();
                    this.gvuser.Visible = true;
                    ((Label)this.gvuser.FooterRow.FindControl("lgFPurValue2")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[0].Compute("sum(pvalu)", "")) ? 0.00 : ds3.Tables[0].Compute("sum(pvalu)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvuser.FooterRow.FindControl("lgFDepreciation2")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[0].Compute("sum(depreamt)", "")) ? 0.00 : ds3.Tables[0].Compute("sum(depreamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvuser.FooterRow.FindControl("lgFBookVal2")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[0].Compute("sum(bookval)", "")) ? 0.00 : ds3.Tables[0].Compute("sum(bookval)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;
            }


        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = DropDownList1.SelectedValue.ToString();
            switch (val)
            {
                case "location":
                    this.gvuser.Visible = false;
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.Visible = false;
                    this.divddldeptName.Visible = true;
                    this.divddluser.Visible = false;
                    this.divddlasset.Visible = false;
                    this.divddlAssetDetails.Visible = false;
                    break;
                case "asset":
                    this.gvuser.Visible = false;
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.Visible = false;
                    this.divddldeptName.Visible = false;
                    this.divddluser.Visible = false;
                    this.divddlasset.Visible = true;
                    this.divddlAssetDetails.Visible = true;
                    break;
                case "user":
                    this.gvuser.Visible = false;
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.Visible = false;
                    this.divddldeptName.Visible = false;
                    this.divddluser.Visible = true;
                    this.divddlasset.Visible = false;
                    this.divddlAssetDetails.Visible = false;
                    break;
            }
        }

        private DataTable HiddenSameDataAsset(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string sircode = dt1.Rows[0]["rsircatcode"].ToString();
            string advno = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == advno && dt1.Rows[j]["rsircatcode"].ToString() == sircode)
                {
                    sircode = dt1.Rows[j]["rsircatcode"].ToString();
                    advno = dt1.Rows[j]["rsircode"].ToString();

                    dt1.Rows[j]["sirdesc1"] = "";
                    dt1.Rows[j]["assetnam"] = "";
                }
                else
                {

                    if (dt1.Rows[j]["rsircatcode"].ToString() == sircode)
                    {
                        dt1.Rows[j]["sirdesc1"] = "";
                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == advno)
                    {
                        dt1.Rows[j]["assetnam"] = "";

                    }


                    sircode = dt1.Rows[j]["rsircatcode"].ToString();
                    advno = dt1.Rows[j]["rsircode"].ToString();

                }
            }

            return dt1;
        }

        private DataTable HiddenSameDataDept(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string advno = dt1.Rows[0]["pactcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == advno)
                {
                    dt1.Rows[j]["dept"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == advno)
                    {
                        dt1.Rows[j]["dept"] = "";

                    }

                    advno = dt1.Rows[j]["pactcode"].ToString();

                }
            }

            return dt1;
        }

        private DataTable HiddenSameDataUser(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string rsircode = dt1.Rows[0]["empid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["empid"].ToString() == rsircode)
                {

                    dt1.Rows[j]["EMPNAME"] = "";

                }

                rsircode = dt1.Rows[j]["empid"].ToString();

            }
            return dt1;

        }

        protected void ddlasset_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetAssetDetails();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string RptType = DropDownList1.SelectedValue.ToString();
            switch (RptType)
            {
                case "location":
                    this.PrintDepartmentWise();
                    break;
                case "asset":
                    this.PrintEquipmentWise();
                    break;
                case "user":
                    this.PrintUserWise();
                    break;
            }
        }

        private void PrintDepartmentWise()
        {
            DataTable dt = (DataTable)Session["tblLocation"];
            var lst1 = dt.DataTableToList<SPEENTITY.C_27_Fxt.FixedAssetRpt>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt"); 
            string asonDate = "As On Date: " + Convert.ToDateTime(txtDateFrom.Text).ToString("dd-MMM-yyyy");
            
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_27_Fxt.RptFixAssetDepartment", lst1, "", "");
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Fixed Asset Report (Department Wise)"));
            rpt1.SetParameters(new ReportParameter("asondate", asonDate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintEquipmentWise()
        {
            DataTable dt = (DataTable)Session["tblAsset"];
            var lst2 = dt.DataTableToList<SPEENTITY.C_27_Fxt.FixedAssetRpt>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string asonDate = "As On Date: " + Convert.ToDateTime(txtDateFrom.Text).ToString("dd-MMM-yyyy");

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_27_Fxt.RptFixAssetEquipment", lst2, "", "");
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Fixed Asset Report (Equipment Wise)"));
            rpt1.SetParameters(new ReportParameter("asondate", asonDate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintUserWise()
        {
            DataTable dt = (DataTable)Session["tblUsers"];
            var lst3 = dt.DataTableToList<SPEENTITY.C_27_Fxt.FixedAssetRpt>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string asonDate = "As On Date: " + Convert.ToDateTime(txtDateFrom.Text).ToString("dd-MMM-yyyy");

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_27_Fxt.RptFixAssetUser", lst3, "", "");
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Fixed Asset Report (User Wise)"));
            rpt1.SetParameters(new ReportParameter("asondate", asonDate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}