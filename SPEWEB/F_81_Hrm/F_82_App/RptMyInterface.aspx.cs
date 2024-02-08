using System;
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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class RptMyInterface : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        public static string empid = "";
        public static string frmdate = "";
        public static string todate = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "MY SERVICES INFORMATION";
                // this.SelectView();
                //this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                string empid = this.Request.QueryString["Type"] == "" ? "" : this.Request.QueryString["empid"].ToString();
               
                this.MultiView1.ActiveViewIndex = 0;
                this.lbtnOk_Click(null, null);

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            
        }
     
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblgrph");
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            this.EmpUserImg.Visible = true;
            this.AttHistoryGraph.Visible = true;
            this.hyplPreviewCv.Visible = true;


            this.EmpUserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
            //ViewState.Remove("tblservices");
            string comcod = this.GetComeCode();
            //string empid = this.Request.QueryString["empid"].ToString().Length > 0 ? this.Request.QueryString["empid"].ToString() : this.ddlEmpName.SelectedValue.ToString(); //this.ddlEmpName.SelectedValue.ToString();
            string empid =  this.Request.QueryString["empid"].ToString() ; //this.ddlEmpName.SelectedValue.ToString();

            string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTMYSERVICES", empid, Date, "", "", "", "", "", "", "");
            //this.lbldesg.Visible = true;

            if (ds1 == null)
            {
                this.gvempservices.DataSource = null;
                this.gvempservices.DataBind();
                return;
            }
            ViewState["tblservices"] = ds1.Tables[0];
            ViewState["tblAttHist"] = ds1.Tables[1];
            ViewState["tblLeaveStatus"] = ds1.Tables[2];
            Session["tblEmpimg"] = ds1.Tables[3];
            ViewState["tblJobRespon"] = ds1.Tables[4];
            ViewState["tblAttHistGraph"] = ds1.Tables[5];



            if (ViewState["tblgrph"] == null)
            {
                DataTable tblt01 = new DataTable();
                tblt01.Columns.Add("yearmon", Type.GetType("System.String"));
                tblt01.Columns.Add("absnt", Type.GetType("System.Double"));
                tblt01.Columns.Add("acintime", Type.GetType("System.Double"));
                tblt01.Columns.Add("aclate", Type.GetType("System.Double"));
                tblt01.Columns.Add("leave", Type.GetType("System.Double"));
                ViewState["tblgrph"] = tblt01;

                this.ShowJobRespon();
            }

            DataTable dt3 = (DataTable)ViewState["tblAttHist"];


            DataTable dt1 = (DataTable)ViewState["tblgrph"];

            DataRow dr1 = dt1.NewRow();
            dr1["yearmon"] = "";
            dr1["absnt"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(absnt)", "")) ? 0.00 : dt3.Compute("Sum(absnt)", ""))).ToString("#,##0;(#,##0)"); ;
            dr1["acintime"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(acintime)", "")) ? 0.00 : dt3.Compute("Sum(acintime)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dr1["aclate"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(aclate)", "")) ? 0.00 : dt3.Compute("Sum(aclate)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dr1["leave"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dt1.Rows.Add(dr1);
            ViewState["tblgrph"] = dt1;
            this.EmpUserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgEmp";
            this.Data_Bind();


        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblservices"];

                this.gvempservices.DataSource = dt;
                this.gvempservices.DataBind();




                DataTable dt2 = (DataTable)ViewState["tblAttHist"];
                this.ShowAttHistoryGraph();
                this.RptAttHistroy.DataSource = dt2;
                this.RptAttHistroy.DataBind();


                DataTable dt3 = (DataTable)ViewState["tblLeaveStatus"];
                this.gvLeaveStatus.DataSource = dt3;
                this.gvLeaveStatus.DataBind();




                //HyperLink lnknextbtn = (HyperLink)this.gvLeaveStatus.HeaderRow.FindControl("hlnkbtnNext"); system.DateTime.Today
                string comcod = this.GetComeCode();
                //string ymonid = this.txtDate.Text.ToString();  this.txtDate.Text.Substring(7)
                string frmdate = Convert.ToDateTime("01-Jan-" + System.DateTime.Today.ToString().Substring(7)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string empid = this.Request.QueryString["empid"].ToString();






                ////lnknextbtn.NavigateUrl = "~/F_82_App/LinkMyHRLeave.aspx?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;

                ((HyperLink)this.gvLeaveStatus.HeaderRow.FindControl("hlnkbtnNext")).NavigateUrl = "../F_82_App/LinkMyHRLeave.aspx?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;





                DataTable dt4 = (DataTable)ViewState["tblEmpimg"];
                DataTable dt5 = (DataTable)ViewState["tblJobRespon"];



            }

            catch (Exception ex)
            { }

        }

      
        protected void RptAttHistroy_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {


            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkyearmon = (HyperLink)e.Item.FindControl("hlnkbtnadd");
                string comcod = this.GetComeCode();
                string ymonid = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ymonid")).ToString();
                string frmdate = Convert.ToDateTime(ymonid.Substring(4, 2) + "/01/" + ymonid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                string empid =  this.Request.QueryString["empid"].ToString() ; //this.ddlEmpName.SelectedValue.ToString();

                lnkyearmon.NavigateUrl = "~/F_81_Hrm/F_82_App/RptMyAttendenceSheet.aspx?Type=&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;
            }



            if (e.Item.ItemType == ListItemType.Footer)
            {

                DataTable dt3 = (DataTable)ViewState["tblAttHist"];
                if (dt3.Rows.Count == 0)
                {

                    ((Label)e.Item.FindControl("lblacintime")).Text = "";
                    ((Label)e.Item.FindControl("lbltotalabs")).Text = "";
                    ((Label)e.Item.FindControl("lbltotallate")).Text = "";
                    ((Label)e.Item.FindControl("lbltotalleave")).Text = "";


                    return;
                }


                ((Label)e.Item.FindControl("lblacintime")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(acintime)", "")) ? 0.00 : dt3.Compute("Sum(acintime)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotalabs")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(absnt)", "")) ? 0.00 : dt3.Compute("Sum(absnt)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotallate")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(aclate)", "")) ? 0.00 : dt3.Compute("Sum(aclate)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotalleave")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0;(#,##0)");


            }

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string comname = dt1.Rows[0]["comname"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comname"].ToString() == comname)
                {
                    comname = dt1.Rows[j]["comname"].ToString();
                    dt1.Rows[j]["comdesc"] = "";
                }

                else
                    comname = dt1.Rows[j]["comname"].ToString();
            }

            return dt1;

        }




        private void ShowAttHistoryGraph()
        {
            DataTable dt4 = (DataTable)ViewState["tblAttHistGraph"];

            // AttHistoryGraph.Series["Actual in Time"].XValueMember = "workday";
            AttHistoryGraph.Series["Actual in Time"].YValueMembers = "perpontow";

            //AttHistoryGraph.Series["Late"].XValueMember = "workday";
            AttHistoryGraph.Series["Late"].YValueMembers = "perlate";

            // AttHistoryGraph.Series["Absent"].XValueMember = "workday";
            AttHistoryGraph.Series["Absent"].YValueMembers = "perab";

            // AttHistoryGraph.Series["Leave"].XValueMember = "workday";
            AttHistoryGraph.Series["Leave"].YValueMembers = "perleave";



            //AttHistoryGraph.Series["Series1"].LegendText = "Month";
            //AttHistoryGraph.Series["Series2"].LegendText = "Status";

            AttHistoryGraph.DataSource = dt4;
            AttHistoryGraph.DataBind();


        }


        protected void hyplPreviewCv_Click1(object sender, EventArgs e)
        {

        }


        private void ShowJobRespon()
        {
            string comcod = this.GetComeCode();
            string empid = this.Request.QueryString["empid"].ToString();
            DataTable dt = (DataTable)ViewState["tblJobRespon"]; ;
            if (dt == null)
            {

                
                this.grvJobRespo.DataSource = null;
                this.grvJobRespo.DataBind();

                return;

            }
            this.grvJobRespo.DataSource = dt;
            this.grvJobRespo.DataBind();

        }



    }
}