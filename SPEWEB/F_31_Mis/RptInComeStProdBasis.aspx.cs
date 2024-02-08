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


namespace SPEWEB.F_31_Mis
{
    public partial class RptInComeStProdBasis : System.Web.UI.Page
    {
        ProcessAccess rptdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblincomest");

            string comcod = GetCompCode();

            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = rptdata.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTINCOMESTPROBASIS", frmdate, todate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvIncomeSt.DataSource = null;
                this.gvIncomeSt.DataBind();
                return;
            }
            Session["tblincomest"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {

                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }

                else
                {

                    grp = dt1.Rows[j]["grp"].ToString();

                }

            }
            return dt1;

        }

        private void Data_Bind()
        {

            this.gvIncomeSt.DataSource = (DataTable)Session["tblincomest"];
            this.gvIncomeSt.DataBind();
            //this.FooterCal();


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string LCname = this.ddlMLc.SelectedItem.Text.Trim().Substring(14);
            //ReportDocument rpcp = new RMGiRPT.R_31_Mis.RptInStProBasis();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            ////TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            ////txtPrjName.Text = "LC Name: " + LCname;
            //TextObject rpttxtdate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtdate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy") + ")";
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource((DataTable)Session["tblincomest"]);
            //Session["Report1"] = rpcp;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvIncomeSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lblgvItemDesc");
                Label proqty = (Label)e.Row.FindControl("lblgvproqty");
                Label amtfc = (Label)e.Row.FindControl("lblgvamtfc");
                Label amttk = (Label)e.Row.FindControl("lblgvamtounttk");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    proqty.Font.Bold = true;
                    amtfc.Font.Bold = true;
                    amttk.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");
                }

            }

        }
    }
}