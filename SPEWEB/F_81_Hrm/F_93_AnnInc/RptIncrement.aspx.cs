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
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_93_AnnInc
{
    public partial class RptIncrement : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.txtfrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetJobLocation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE INCREMENT INFORMATION";
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

        private void GetWorkStation()
        {
            Session.Remove("lstwrkstation");
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";
        }
        private void GetDivision()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDivision(comcod, wstation);
            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "00000";
        }

        private void GetDeptList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "00000";

        }

        private void GetSectionList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetSection(comcod, wstation);
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "00000";

        }

        private void GetJobLocation()
        {

            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDivision();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "New")
            {
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                this.lnkbtnShow.Text = "Ok";
                return;
            }

            this.lnkbtnShow.Text = "New";
            this.ShowInc();
        }

        private void GetIncrListDataSet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string DeptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string SecCode = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string empStatus = this.ddlEmpType.SelectedValue.ToString().Trim();
            string onlyIncr = this.chkOnlyIncr.Checked ? "onlyincr" : "";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ANNUAL_INCREMENT", "GETPREINCREMENTSTATUS", frmdate, todate, Company, division, DeptCode, 
                SecCode, joblocation, userid, empStatus, onlyIncr);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }

            Session["tblAnnInc"] = ds2.Tables[0];
        }
        private void ShowInc()
        {

            this.GetIncrListDataSet();
            DataTable dt = (DataTable)Session["tblAnnInc"];
            Session["tblAnnInc"] = HiddenSameData(dt);
            this.LoadGrid();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string seccode = dt1.Rows[0]["seccode"].ToString();
            string incrno = dt1.Rows[0]["incrno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["seccode"].ToString() == seccode && dt1.Rows[j]["incrno"].ToString() == incrno)
                {

                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";
                    dt1.Rows[j]["incrno1"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                        dt1.Rows[j]["deptname"] = "";
                    if (dt1.Rows[j]["seccode"].ToString() == seccode)
                        dt1.Rows[j]["section"] = "";
                    if (dt1.Rows[j]["incrno"].ToString() == incrno)
                    {
                        dt1.Rows[j]["incrno1"] = "";

                    }


                }

                deptcode = dt1.Rows[j]["deptcode"].ToString();
                seccode = dt1.Rows[j]["seccode"].ToString();
                incrno = dt1.Rows[j]["incrno"].ToString();

            }
            return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~/Image/LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string FromDate = this.txtfrmDate.Text;
            string ToDate = this.txttoDate.Text;
            //For Without Hidden Same Value
            this.GetIncrListDataSet();
            DataTable dt1 = (DataTable)Session["tblAnnInc"];

            var lst1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptIncrement>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptIncrementStatus", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + FromDate+" To: "+ToDate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Increment/Promotion"));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                this.gvAnnIncre.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvAnnIncre.DataSource = dt;
                this.gvAnnIncre.DataBind();

                if (dt.Rows.Count == 0)
                    return;
                Session["Report1"] = gvAnnIncre;
                ((HyperLink)this.gvAnnIncre.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                this.FooterCalCulation();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() +"')", true);
            }
        }
        protected void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFPrsntAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFLastIncrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lincamt)", "")) ? 0.00 : dt.Compute("sum(lincamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFIncramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incamt)", "")) ? 0.00 : dt.Compute("sum(incamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFfinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finincamt)", "")) ? 0.00 : dt.Compute("sum(finincamt)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void gvAnnIncre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAnnIncre.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void LbtnPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string empid = ((Label)this.gvAnnIncre.Rows[index].FindControl("lblempid")).Text.ToString();
            string incrno = ((Label)this.gvAnnIncre.Rows[index].FindControl("lblincrno")).Text.ToString();
            string comcod = this.GetComeCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ANNUAL_INCREMENT", "GET_SINGLE_EMPLOYEE_INCREMENT_INFO", incrno, empid, "", "", "");
            if (ds2 == null)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            double bsalry = Convert.ToDouble(ds2.Tables[0].Rows[0]["bsalry"]);
            double hrent = Convert.ToDouble(ds2.Tables[0].Rows[0]["hrent"]);
            double medical = Convert.ToDouble(ds2.Tables[0].Rows[0]["medical"]);
            double foodalw = Convert.ToDouble(ds2.Tables[0].Rows[0]["foodalw"]);
            double conv = Convert.ToDouble(ds2.Tables[0].Rows[0]["conv"]);
            double oldbsalry = Convert.ToDouble(ds2.Tables[0].Rows[0]["oldbsal"]);
            double oldhrent = Convert.ToDouble(ds2.Tables[0].Rows[0]["oldhrent"]);
            double oldmedical = Convert.ToDouble(ds2.Tables[0].Rows[0]["oldmallow"]);
            double oldconv = Convert.ToDouble(ds2.Tables[0].Rows[0]["oldcven"]);
            double oldfoodalow = Convert.ToDouble(ds2.Tables[0].Rows[0]["oldfoodalow"]);
            string type = "";
            double totalsal = (bsalry + hrent + conv + medical + foodalw);
            double oldtotalsal = (oldbsalry + oldhrent + oldconv + oldmedical + oldfoodalow);
            double totalsalSub =  totalsal - oldtotalsal;
            string Inword = ASTUtility.Trans(Convert.ToDouble(totalsal), 2);
            string Inwordo = ASTUtility.Trans(Convert.ToDouble(oldtotalsal), 2);
            string InwordSub = ASTUtility.Trans(Convert.ToDouble(totalsalSub), 2);
            
            LocalReport rpt1 = new LocalReport();

            switch (comcod)
            {
                case "5305":
                case "5306":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_93_AnnInc.RptEmpIncrFb", null, null, null);

                    rpt1.EnableExternalImages = true;

                    rpt1.SetParameters(new ReportParameter("empname", ds2.Tables[0].Rows[0]["empname"].ToString().Trim()));
                    rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds2.Tables[0].Rows[0]["incrdate"]).ToString("dd-MM-yyyy")));
                    rpt1.SetParameters(new ReportParameter("designation", ds2.Tables[0].Rows[0]["designation"].ToString()));
                    rpt1.SetParameters(new ReportParameter("deptname", ds2.Tables[0].Rows[0]["deptname"].ToString()));
                    rpt1.SetParameters(new ReportParameter("totalsal", totalsal.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("oldtotalsal", oldtotalsal.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("totalsalSub", totalsalSub.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("Inword", Inword));
                    rpt1.SetParameters(new ReportParameter("Inwordo", Inwordo));
                    rpt1.SetParameters(new ReportParameter("InwordSub", InwordSub));

                    Session["Report1"] = rpt1;
                     type = "PDF";
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_93_AnnInc.RptAnnInc", null, null, null);

                    rpt1.EnableExternalImages = true;

                    rpt1.SetParameters(new ReportParameter("comnam", "এডিসন ফুটওয়্যার লিমিটেড"));
                    rpt1.SetParameters(new ReportParameter("comadd", "তালতলী , মির্জাপুর ,হোতাপাড়া, গাজীপুর।"));
                    rpt1.SetParameters(new ReportParameter("empname", ds2.Tables[0].Rows[0]["empnameban"].ToString().Trim()));
                    rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds2.Tables[0].Rows[0]["incrdate"]).ToString("dd-MM-yyyy")));
                    rpt1.SetParameters(new ReportParameter("year", Convert.ToDateTime(ds2.Tables[0].Rows[0]["incrdate"]).ToString("yyyy")));
                    rpt1.SetParameters(new ReportParameter("designation", ds2.Tables[0].Rows[0]["desigban"].ToString()));
                    rpt1.SetParameters(new ReportParameter("cardno", ds2.Tables[0].Rows[0]["cardno"].ToString()));
                    rpt1.SetParameters(new ReportParameter("deptname", ds2.Tables[0].Rows[0]["refdescban"].ToString()));
                    rpt1.SetParameters(new ReportParameter("grade", ds2.Tables[0].Rows[0]["grade"].ToString()));
                    rpt1.SetParameters(new ReportParameter("bsalry", bsalry.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("hrent", hrent.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("medical", medical.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("conv", conv.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("oldbsalry", oldbsalry.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("oldhrent", oldhrent.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("oldmedical", oldmedical.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("oldconv", oldconv.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("totalsal", totalsal.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("oldtotalsal", oldtotalsal.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("foodalw", foodalw.ToString("#,##0.00;(#,##0.00); ")));
                    rpt1.SetParameters(new ReportParameter("foodalwold", oldfoodalow.ToString("#,##0.00;(#,##0.00); ")));

                    Session["Report1"] = rpt1;
                     type = "PDF";
                    break;
            }
            
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
    }
}