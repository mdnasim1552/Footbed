using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_01_Mer
{
    public partial class OrderExSheet : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Order Execution Sheet";
                this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtdateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbtnOk_Click(null, null);
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetCostingSummary();
            this.Data_Bind();

        }
        private void GetCostingSummary()
        {
            string comcod = this.GetCompCode();
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdateto.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_MERCHAN_01", "ORDER_EX_SHEET", curdate, todate);
            ViewState["tblOrderExSheet"] = ds1.Tables[0];
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblOrderExSheet"];
            if (dt != null)
            {
                this.gvOrderExSheet.DataSource = dt;
                this.gvOrderExSheet.DataBind();
                
                Session["Report1"] = gvOrderExSheet;
                if (dt.Rows.Count > 0)
                {
                    ((HyperLink)this.gvOrderExSheet.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RDLCViewerWin.aspx?PrintOpt=GRIDTOEXCEL";
                }

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

    }
}