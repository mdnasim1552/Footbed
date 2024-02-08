using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_13_CWare
{
    public partial class QCDashboard : System.Web.UI.Page
    {
        ProcessAccess getData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Quality Smartboard";

                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                HyperLink hyp1 = (HyperLink)this.HyperLink1 as HyperLink;
                HyperLink hyp2 = (HyperLink)this.HyperLink2 as HyperLink;
                HyperLink hyp3 = (HyperLink)this.HyperLink3 as HyperLink;
                HyperLink hyp4 = (HyperLink)this.HyperLink4 as HyperLink;

                hyp1.NavigateUrl = "~/F_15_Pro/RptProduction?Type=DefParChart";
                hyp2.NavigateUrl = "~/F_15_Pro/RptProduction?Type=OrderDefect";
                hyp3.NavigateUrl = "~/F_10_Procur/RptSupplierDueStatus?Type=IQCInspection";
                hyp4.NavigateUrl = "~/F_17_GFInv/FGInspectionEntry?Type=Entry";
            }
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.BestSupplier.Visible = true;
            this.WorstSupplier.Visible = true;
            this.PnIQCStatus.Visible = true;
            this.PieIQCStat.Visible = true;

            this.GetData(); // Fetch data from procedure.

            this.AllGraphs();
            this.GetIQCStatus();
        }

        private void GetData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fdate = this.txtDatefrom.Text;
            string tdate = this.txtDateto.Text;

            DataSet ds = getData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_QC", "QUALITY_DASHBOARD", fdate, tdate);
            if (ds == null)
                return;

            ViewState["tblHighlights"] = ds.Tables[0];
            ViewState["tblBestSupp"] = ds.Tables[1];
            ViewState["tblWorstSupp"] = ds.Tables[2];
        }

        private void GetIQCStatus()
        {
            DataTable dt = (DataTable)ViewState["tblHighlights"];
            this.lblTotalRcv.Text = Convert.ToDouble(dt.Rows[0]["ttlrcv"]).ToString("#,##0.0;(#,##0.0);");
            this.lblQpassed1.Text = Convert.ToDouble(dt.Rows[0]["qcpass1"]).ToString("#,##0.0;(#,##0.0);");
            this.lblQpassed2.Text = Convert.ToDouble(dt.Rows[0]["qcpass2"]).ToString("#,##0.0;(#,##0.0);");
            this.lblFailed.Text = Convert.ToDouble(dt.Rows[0]["qcfaild"]).ToString("#,##0.0;(#,##0.0);");
            this.lblPending.Text = Convert.ToDouble(dt.Rows[0]["pending"]).ToString("#,##0.0;(#,##0.0);");
            this.lblLdTime.Text = Convert.ToDouble(dt.Rows[0]["leadtime"]).ToString("#,##0.0;(#,##0.0);");
        }

        private void AllGraphs()
        {
            DataTable dt = (DataTable)ViewState["tblHighlights"];
            double QcP1 = Convert.ToDouble(dt.Rows[0]["qcpass1"]);
            double QcP2 = Convert.ToDouble(dt.Rows[0]["qcpass2"]);
            double QcFail = Convert.ToDouble(dt.Rows[0]["qcfaild"]);
            double Pending = Convert.ToDouble(dt.Rows[0]["pending"]);

            DataTable dt1 = (DataTable)ViewState["tblBestSupp"];
            var bestSupplier = dt1.DataTableToList<TopBestSupplier>();
            var jsonSerialiser1 = new JavaScriptSerializer();
            var bestSupplier_json = jsonSerialiser1.Serialize(bestSupplier);

            DataTable dt2 = (DataTable)ViewState["tblWorstSupp"];
            var worstSupplier = dt2.DataTableToList<TopWorstSupplier>();
            var jsonSerialiser2 = new JavaScriptSerializer();
            var worstSupplier_json = jsonSerialiser2.Serialize(worstSupplier);

            // IQC Status
            ScriptManager.RegisterStartupScript(this, GetType(), "PieChartIQC", "PieChartIQC('" + QcP1.ToString() + "','" +
                QcP2.ToString() + "','" + QcFail.ToString() + "','" +
                Pending.ToString() + "');", true);

            // BarBestSupplier
            ScriptManager.RegisterStartupScript(this, GetType(), "BarBestSupplier", "BarBestSupplier('" + bestSupplier_json + "');", true);

            // BarWorstSupplier
            ScriptManager.RegisterStartupScript(this, GetType(), "BarWorstSupplier", "BarWorstSupplier('" + worstSupplier_json + "');", true);
        }


        public class TopBestSupplier
        {
            public double rcvqty { get; set; }
            public double passqty { get; set; }
            public double qcqty { get; set; }
            public string ssirdesc { get; set; }
            public double passprcnt { get; set; }
        }

        public class TopWorstSupplier
        {
            public double rcvqty { get; set; }
            public double passqty { get; set; }
            public double qcqty { get; set; }
            public string ssirdesc { get; set; }
            public double passprcnt { get; set; }
        }
    }
}