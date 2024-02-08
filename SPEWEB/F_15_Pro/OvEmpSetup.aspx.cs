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
using SPEENTITY.C_15_Pro;

namespace SPEWEB.F_15_Pro
{
    public partial class OvEmpSetup : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = true;
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Setup Information";

                this.LoadGrid();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }
        protected void lnkprint_Click(object sender, EventArgs e)
        {





        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (this.Request.QueryString["comcod"].ToString());


        }


        private void SaveValue()
        {

            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tblSetupDet"];
            for (int i = 0; i < this.gvSetupDet.Rows.Count; i++)
            {

                bool chkmr = ((CheckBox)this.gvSetupDet.Rows[i].FindControl("chkvmrno")).Checked;

                rowindex = (this.gvSetupDet.PageSize * this.gvSetupDet.PageIndex) + i;

                tblt02.Rows[rowindex]["estatus"] = chkmr;
            }
            ViewState["tblSetupDet"] = tblt02;


        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            this.lmsg.Visible = true;
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblSetupDet"];

            string comcod = this.GetCompCode();

            string sircode = this.Request.QueryString["sircode"].ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string active = dt.Rows[i]["estatus"].ToString();

                if (active == "True")
                {
                    MktData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "INSERTUPDATEPROC", sircode, empid, "", "", "", "");
                }

            }
            this.lmsg.Text = "Updated Successfully";
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Update Fixation";
                string eventdesc2 = "Project Name: "; //this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadGrid()
        {

            ViewState.Remove("tblSetupDet");

            string comcod = this.GetCompCode();

            string sircode = this.Request.QueryString["sircode"].ToString();

            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETEMPINFO", sircode, "", "", "", "", "", "", "", "");
            if (ds4 == null)
                return;

            ViewState["tblSetupDet"] = ds4.Tables[0].Copy();
            ViewState["tblSetupDet1"] = ds4.Tables[0].Copy();

            this.txtDesc.Text = "Operation Name - " + this.Request.QueryString["sirdesc"].ToString(); ;

            this.ShowValue();

        }
        private void ShowValue()
        {
            DataTable tblt05 = (DataTable)ViewState["tblSetupDet"];
            DataView dv = tblt05.DefaultView;
            dv.RowFilter = "estatus='True'";
            ViewState["tblSetupDet"] = HiddenSameData(dv.ToTable());
            if (dv.ToTable().Rows.Count == 0)
            {
                DataTable dt = (DataTable)ViewState["tblSetupDet1"];
                ViewState["tblSetupDet"] = HiddenSameData(dt);

            }

            this.Data_bind();
        }
        private void Data_bind()
        {
            DataTable tblt05 = (DataTable)ViewState["tblSetupDet"];
            this.gvSetupDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSetupDet.DataSource = tblt05;
            this.gvSetupDet.DataBind();
            for (int i = 0; i < this.gvSetupDet.Rows.Count; i++)
            {
                ((CheckBox)gvSetupDet.Rows[i].FindControl("chkvmrno")).Visible = (!((CheckBox)gvSetupDet.Rows[i].FindControl("chkvother")).Checked);
            }




            this.FooterCal();
        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tblSetupDet"];
            if (dt.Rows.Count > 0)
            {
                //((Label)this.gvSetupDet.FooterRow.FindControl("lblgvFmarks")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(marks)", "")) ?
                //                   0 : dt.Compute("sum(marks)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new MFGRPT.R_06_Sal.rptUnitFxInf();
            //DataTable dt1 = (DataTable)ViewState["tblSetupDet"];
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = "uamt>0";
            //rptstk.SetDataSource(dv1);

            //TextObject txtCompname = rptstk.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //txtCompname.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Unit Fixation";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //dv1.RowFilter = "";

        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            DataTable tblt05 = (DataTable)ViewState["tblSetupDet1"];
            string comcod = this.GetCompCode();
            if (this.chkAllSInf.Checked)
            {
                ViewState["tblSetupDet"] = HiddenSameData(tblt05);
            }

            else
            {
                DataView dv = tblt05.DefaultView;
                dv.RowFilter = "estatus='True'";
                ViewState["tblSetupDet"] = HiddenSameData(dv.ToTable());
            }
            this.Data_bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                    dt1.Rows[j]["deptname"] = "";
                deptcode = dt1.Rows[j]["deptcode"].ToString();
            }
            return dt1;

        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        protected void gvSetupDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvSetupDet.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }

        protected void lblgvFTotal_Click(object sender, EventArgs e)
        {
            //this.SaveValue();
            this.Data_bind();
        }
        protected void gvSetupDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblSetupDet"];
            string comcod = this.GetCompCode();
            string sircode = this.Request.QueryString["sircode"].ToString();

            string empid = ((Label)this.gvSetupDet.Rows[e.RowIndex].FindControl("lblgvActcode")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "DELETEPROCESS", sircode, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                //int rowindex = (this.gvSetupDet.PageSize) * (this.gvSetupDet.PageIndex) + e.RowIndex;
                //dt.Rows[rowindex].Delete();
                this.LoadGrid();

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Emp Setup";
                string eventdesc = "Delete Emp List";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
    }
}