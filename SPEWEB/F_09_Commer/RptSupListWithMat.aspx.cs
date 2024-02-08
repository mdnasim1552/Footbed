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

namespace SPEWEB.F_09_Commer
{
    public partial class RptSupListWithMat : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ShowSupLwMat();


            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void ShowSupLwMat()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTSUPLWMATERIAL", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSupLwMat.DataSource = null;
                this.gvSupLwMat.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tbldata"] = dt;
            this.Data_Bind();
            ds1.Dispose();

        }

        private void Data_Bind()
        {

            DataTable dt = ((DataTable)Session["tbldata"]);
            if (dt.Rows.Count == 0)
                return;
            this.gvSupLwMat.DataSource = dt;
            this.gvSupLwMat.DataBind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string sircode = dt1.Rows[0]["sircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["sircode"].ToString() == sircode)
                {
                    sircode = dt1.Rows[j]["sircode"].ToString();
                    dt1.Rows[j]["supdesc"] = "";
                    dt1.Rows[j]["addr"] = "";
                }

                else
                {
                    sircode = dt1.Rows[j]["sircode"].ToString();
                }

            }

            return dt1;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintShowSupLwMat();
        }

        private void PrintShowSupLwMat()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new RMGiRPT.R_09_Commer.rptSupLwMat();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Supplier List with Materials Status";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptstk.SetDataSource((DataTable)Session["tbldata"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

    }
}