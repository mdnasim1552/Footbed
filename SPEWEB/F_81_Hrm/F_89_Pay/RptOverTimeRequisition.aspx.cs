using Microsoft.Reporting.WinForms;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{

    public partial class RptOverTimeRequisition : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();


                DateTime date = System.DateTime.Today;
                this.txtDate.Text = date.AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetJobLocation();
                this.GetLineDDL();
                this.SelectView();
            }
        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SecWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                default:
                    break;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent eventPFL-000020660
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetWorkStation()
        {

            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            switch (comcod)
            {
                //FB & Footbed Footwear OT Emp. Only
                case "5305":
                    lst = lst.FindAll(x => (x.actcode.Substring(0, 4) == "0000") || (x.actcode.Substring(0, 4) == "9403") || (x.actcode.Substring(0, 4) == "9414"));
                    break;
                case "5306":
                    lst = lst.FindAll(x => x.actcode.Substring(0, 4) == "0000" || x.actcode.Substring(0, 4) == "9408" || x.actcode.Substring(0, 4) == "9416");
                    break;

                default:
                    lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
                    break;
            }

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation_SelectedIndexChanged(null, null);
        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetDivision()
        {
            try
            {
                string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
                string comcod = GetComCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

                if (lst == null)
                    return;
                var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
                lst1.Add(all);
                this.ddlDivision.DataTextField = "actdesc";
                this.ddlDivision.DataValueField = "actcode";
                this.ddlDivision.DataSource = lst1;
                this.ddlDivision.DataBind();
                this.ddlDivision.SelectedValue = "000000000000";

                this.ddlDivision_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";
            this.ddlDept_SelectedIndexChanged(null, null);
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";

        }
        private void GetJobLocation()
        {

            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        private void GetLineDDL()
        {
            string comcod = GetComCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
            ViewState["tbllineddl"] = ds3.Tables[0];
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SecWise":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.gvOTReqSecWise.DataSource = null;
                        this.gvOTReqSecWise.DataBind();
                        return;
                    }

                    this.lnkbtnShow.Text = "New";
                    this.ShowSecWiseOTReqSummary();
                    break;
                default:
                    break;
            }
          
        }

        private void ShowSecWiseOTReqSummary()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string txtDate = this.txtDate.Text.Trim();
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
                string deptCode = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
                string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
                string linecode = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
                string joblocation = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";

                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "RPT_OT_REQSUMM_SECWISE", null, null, null, txtDate, Company, division, deptCode, section, linecode, joblocation, userid);
                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                Session["tblotsummary"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblotsummary"];
                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "SecWise":
                        this.gvOTReqSecWise.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                        this.gvOTReqSecWise.DataSource = this.HiddenSameData(dt);
                        this.gvOTReqSecWise.DataBind();
                        this.FooterCalculation();
                        break;
                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "')", true);

            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string seccod = dt1.Rows[0]["sectionid"].ToString();
           
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["sectionid"].ToString() == seccod)
                {
                    dt1.Rows[j]["empsection"] = "";
                }
               
                seccod = dt1.Rows[j]["sectionid"].ToString();
            }
            return dt1;
        }
        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblotsummary"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SecWise":
                    ((Label)this.gvOTReqSecWise.FooterRow.FindControl("lblgvFAprvOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(actotcount)", "")) ? 0.00 : dt.Compute("sum(actotcount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOTReqSecWise.FooterRow.FindControl("lblgvFPrsntOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prsntotcount)", "")) ? 0.00 : dt.Compute("sum(prsntotcount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOTReqSecWise.FooterRow.FindControl("lgvFOtAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otamt)", "")) ? 0.00 : dt.Compute("sum(otamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOTReqSecWise.FooterRow.FindControl("lblgvFuauthot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uauthotcount)", "")) ? 0.00 : dt.Compute("sum(uauthotcount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOTReqSecWise.FooterRow.FindControl("lblgvFuauthtotamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uauthotamt)", "")) ? 0.00 : dt.Compute("sum(uauthotamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOTReqSecWise.FooterRow.FindControl("lblgvFTotalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tototamt)", "")) ? 0.00 : dt.Compute("sum(tototamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }
        }

        protected void gvOTReqSecWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Save_Value();
            this.gvOTReqSecWise.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void Save_Value()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblotsummary"];
                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "SecWise":
                        int TblRowIndex;
                        for (int i = 0; i < this.gvOTReqSecWise.Rows.Count; i++)
                        {
                            double aprvotcount = Convert.ToDouble("0" + ((TextBox)this.gvOTReqSecWise.Rows[i].FindControl("txtgvAprvOt")).Text.Trim());
                            double aprvothrs = Convert.ToDouble("0" + ((TextBox)this.gvOTReqSecWise.Rows[i].FindControl("txtgvAprvOtHrs")).Text.Trim());
                            double prsntotcount = Convert.ToDouble("0" + ((Label)this.gvOTReqSecWise.Rows[i].FindControl("lblgvPrsntOt")).Text.Trim());
                            double otamt = Convert.ToDouble("0" + ((Label)this.gvOTReqSecWise.Rows[i].FindControl("lgvOtAmt")).Text.Trim());
                            double avgothrs = Convert.ToDouble("0" + ((Label)this.gvOTReqSecWise.Rows[i].FindControl("lblgvavgothrs")).Text.Trim());
                            string remarks = ((TextBox)this.gvOTReqSecWise.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                            TblRowIndex = (gvOTReqSecWise.PageIndex) * gvOTReqSecWise.PageSize + i;

                            double uauthotcount = aprvotcount == 0 ? prsntotcount : prsntotcount < aprvotcount ? 0 : prsntotcount - aprvotcount;
                            double tototamt = (int)Math.Ceiling(prsntotcount * avgothrs) * 50;
                            double uauthotamt =(int)Math.Ceiling(uauthotcount * avgothrs) * 50;
                            double totalamt = aprvotcount > 0 ? tototamt + uauthotamt : uauthotamt;

                            dt.Rows[TblRowIndex]["actotcount"] = aprvotcount;
                            dt.Rows[TblRowIndex]["aprvothrs"] = aprvothrs;
                            dt.Rows[TblRowIndex]["actaprvothrs"] = aprvotcount * aprvothrs;
                            dt.Rows[TblRowIndex]["actothrs"] = prsntotcount * avgothrs;
                            dt.Rows[TblRowIndex]["uauthotcount"] = prsntotcount < aprvotcount ? 0 : uauthotcount;
                            dt.Rows[TblRowIndex]["uauthothrs"] = prsntotcount < aprvotcount ? 0 : aprvothrs;
                            dt.Rows[TblRowIndex]["otamt"] = aprvotcount > 0 ? tototamt : 0;
                            dt.Rows[TblRowIndex]["uauthotamt"] = prsntotcount < aprvotcount ? 0 : uauthotamt;
                            dt.Rows[TblRowIndex]["tototamt"] = totalamt;
                            dt.Rows[TblRowIndex]["remarks"] = remarks;
                        }
                        break;
                }

                Session["tblotsummary"] = dt;
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "')", true);

            }
        }
        protected void gvOTReqSecWise_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01 = new TableCell();
                cell01.Text = "SL#";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.Font.Bold = true;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);

                TableCell cell02 = new TableCell();
                cell02.Text = "Section";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.Font.Bold = true;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Line";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.Font.Bold = true;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);

                TableCell cell04 = new TableCell();
                cell04.Text = "Approv. <br>Head Count";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.Font.Bold = true;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);

                TableCell cell05 = new TableCell();
                cell05.Text = "Present OT </br>Head Count";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Font.Bold = true;
                cell05.RowSpan = 2;
                gvrow.Cells.Add(cell05);

                TableCell cell06 = new TableCell();
                cell06.Text = "Approv.<br> OT (Hr)";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.Font.Bold = true;
                cell06.RowSpan = 2;
                gvrow.Cells.Add(cell06);

                TableCell cell07 = new TableCell();
                cell07.Text = "Actual Approv.<br> OT (Hr)";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.Font.Bold = true;
                cell07.RowSpan = 2;
                gvrow.Cells.Add(cell07);

                TableCell cell08 = new TableCell();
                cell08.Text = "Actual Present<br> OT (Hr)";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.Font.Bold = true;
                cell08.RowSpan = 2;
                gvrow.Cells.Add(cell08);

                TableCell cell14 = new TableCell();
                cell14.Text = "Total <br>OT (Hr)";
                cell14.HorizontalAlign = HorizontalAlign.Center;
                cell14.Font.Bold = true;
                cell14.RowSpan = 2;
                gvrow.Cells.Add(cell14);

                TableCell cell09 = new TableCell();
                cell09.Text = "OT<br> Amount";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.Font.Bold = true;
                cell09.RowSpan = 2;
                gvrow.Cells.Add(cell09);

                TableCell cell10 = new TableCell();
                cell10.Text = "Unauthorized OT";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.Font.Bold = true;
                cell10.ColumnSpan = 3;
                gvrow.Cells.Add(cell10);

                TableCell cell12 = new TableCell();
                cell12.Text = "Total Amount";
                cell12.HorizontalAlign = HorizontalAlign.Center;
                cell12.Font.Bold = true;
                cell12.RowSpan = 2;
                gvrow.Cells.Add(cell12);

                TableCell cell13 = new TableCell();
                cell13.Text = "Remarks";
                cell13.HorizontalAlign = HorizontalAlign.Center;
                cell13.Font.Bold = true;
                cell13.RowSpan = 2;
                gvrow.Cells.Add(cell13);

                this.gvOTReqSecWise.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvOTReqSecWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;

            }
        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SecWise":
                    this.PrintSecWiseOTReqSummary();
                    break;
            }
        }
        private void PrintSecWiseOTReqSummary()
        {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblotsummary"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptOTReqSummary>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSecWiseOTReqSummary", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Overtime Requisition Summary"));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
       
    }
}