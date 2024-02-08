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

namespace SPEWEB.F_05_ProShip
{
    public partial class RptCriticalorder : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Critical Path Of Order";
                this.GetSeason();
                this.GetBuyer();
            }
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetSeason()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();

            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";
            }
        }

        private void GetBuyer()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            //DataView dv1 = ds2.Tables[0].DefaultView;
            //DataRowView newRow = dv1.AddNew();

            DataView dv1 = new DataView(ds2.Tables[0]);
            dv1.RowFilter = ("sircode not like '000000000000'");
            DataTable dt2 = dv1.ToTable();

            dt2.Rows.Add("000000000000", "All");
            dt2.DefaultView.Sort = "sircode ASC";

            this.DdlBuyer.DataTextField = "sirdesc";
            this.DdlBuyer.DataValueField = "sircode";
            this.DdlBuyer.DataSource = dt2;
            this.DdlBuyer.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetCriticalOrderInf();
        }

        private void GetCriticalOrderInf()
        {
            string comcod = this.GetComCode();
            string season = this.DdlSeason.SelectedValue.ToString();
            string buyer = this.DdlBuyer.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_EXPORT_PLAN", "GETCRITICALPATHORDER", season, buyer, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            ViewState["tblCriticalData"] = ds1.Tables[0];
            ViewState["tblCriticalHeader"] = ds1.Tables[1];

            this.Data_Bind();

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblCriticalData"];
            DataTable dt1 = (DataTable)ViewState["tblCriticalHeader"];

            this.gvCrticalOrder.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCrticalOrder.DataSource = dt;
            this.gvCrticalOrder.DataBind();

            for (int k = 1; k <= dt.Columns.Count; k++)
            {

                this.gvCrticalOrder.HeaderRow.Cells[k].Text = dt1.Rows[0]["col" + k.ToString()].ToString().Trim();

            }

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

    }
}