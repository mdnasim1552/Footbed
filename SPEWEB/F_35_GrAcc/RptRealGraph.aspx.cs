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
    public partial class RptRealGraph : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ////this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();



                this.SelectView();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "RealGrahp") ? "Order, Production & Export & Realization"
                    : "Month Wise Payment-Summary";





            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "RealGrahp":

                    this.ShowGraph();
                    break;
            }
        }


        private void ShowGraph()
        {

            //ViewState.Remove("tbRealFlow");
            string comcod = this.Request.QueryString["comcod"].ToString().Trim();


            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "GRAPHTEST", "", "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];
            DataTable dt1 = ds1.Tables[1];
            string category = "";



            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                category = category + "," + dt1.Rows[i]["Name"].ToString();
            }
            decimal[] values = new decimal[dt.Rows.Count];
            decimal[] values2 = new decimal[dt.Rows.Count];
            decimal[] values3 = new decimal[dt.Rows.Count];
            decimal[] values4 = new decimal[dt.Rows.Count];

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    values[i] = Convert.ToDecimal(dt.Rows[i]["orderamt"]);
            //    values2[i] = Convert.ToDecimal(dt.Rows[i]["proamt"]);
            //    values3[i] = Convert.ToDecimal(dt.Rows[i]["shipamt"]);

            //}


            values[0] = Convert.ToDecimal(this.Request.QueryString["orderamt"].ToString().Trim());
            values2[0] = Convert.ToDecimal(this.Request.QueryString["proamt"].ToString().Trim());
            values3[0] = Convert.ToDecimal(this.Request.QueryString["shipamt"].ToString().Trim());
            values4[0] = Convert.ToDecimal(this.Request.QueryString["rlzamt"].ToString().Trim());


            BarChart1.CategoriesAxis = category.Remove(0, 1);
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Order" });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values2, BarColor = "#2fd5g9", Name = "Production" });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values3, BarColor = "#2fd5g9", Name = "Shipment" });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values4, BarColor = "#2fd5g9", Name = "Realization" });



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }





    }

}