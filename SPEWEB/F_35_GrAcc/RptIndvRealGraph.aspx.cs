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
namespace SPEWEB.F_35_GrAcc
{
    public partial class RptIndvRealGraph : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ////this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.AddDays(-1).AddMonths(1).ToString("dd-MMM-yyyy");


                //this.ShowGraph();

                ((Label)this.Master.FindControl("lblTitle")).Text = "Order, Production & Export & Realization";






            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.pnlbarchart.Visible = true;
            this.ShowGraph();

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void ShowGraph()

        {


            string comcod = this.GetCompCode();
            string sDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string eDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_MIS02", "RPTMGTINDGRAPH", sDate, eDate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            DataTable dt = ds1.Tables[0];


            double ordramt = Convert.ToDouble((Convert.IsDBNull(dt.Rows[0]["ordramt"]) ? 0.00 : dt.Rows[0]["ordramt"]));
            double proamt = Convert.ToDouble((Convert.IsDBNull(dt.Rows[0]["proamt"]) ? 0.00 : dt.Rows[0]["proamt"]));
            double shipamt = Convert.ToDouble((Convert.IsDBNull(dt.Rows[0]["shipamt"]) ? 0.00 : dt.Rows[0]["shipamt"]));
            double rlzamt = Convert.ToDouble((Convert.IsDBNull(dt.Rows[0]["rlzamt"]) ? 0.00 : dt.Rows[0]["rlzamt"]));

            this.txOrderamt.Text = Convert.ToDouble(ordramt).ToString();
            this.txProduction.Text = Convert.ToDouble(proamt).ToString();
            this.txShipment.Text = Convert.ToDouble(shipamt).ToString();
            this.txtRealization.Text = Convert.ToDouble(rlzamt).ToString();

            this.gvOrderrec.DataSource = dt;
            this.gvOrderrec.DataBind();

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }

    }
}