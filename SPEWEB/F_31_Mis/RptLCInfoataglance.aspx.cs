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

namespace SPEWEB.F_31_Mis
{
    public partial class RptLCInfoataglance : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetSeason();
                //this.GetProjectName();
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Lc Information - At a glance";

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");
            if (ds1 == null)
                return;

            ddlSeason.DataTextField = "gdesc";
            ddlSeason.DataValueField = "gcod";
            ddlSeason.DataSource = ds1.Tables[0];
            ddlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.ddlSeason.SelectedValue = season;
            }
            else
            {
                this.ddlSeason.SelectedValue = "00000";
            }

            this.ddlSeason_SelectedIndexChanged(null, null);
        }


        protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string filter = this.txtSearchpIndp.Text.Trim() + "%";
            string buyer = "";
            string season = this.ddlSeason.SelectedValue == "00000" ? "" : this.ddlSeason.SelectedValue;
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_LC_STATUS", "GETLCNAME", filter, "", "", "", "", "", "", "", "");
            //DataTable dt1 = ds1.Tables[0];
            //this.ddlProjectInd.DataSource = dt1;
            //this.ddlProjectInd.DataTextField = "actdesc";
            //this.ddlProjectInd.DataValueField = "actcode";
            //this.ddlProjectInd.DataBind();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", filter, buyer, season, "", "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tbllcorder"] = lst;
            this.ddlProjectInd.DataSource = lst.Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            this.ddlProjectInd.DataTextField = "mlcdesc";
            this.ddlProjectInd.DataValueField = "mlccod";
            this.ddlProjectInd.DataBind();
            this.ddlProjectInd_SelectedIndexChanged(null,null);
        }
        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblOrderDetails.Visible = true;
            this.lblBBlCStatus.Visible = true;
            this.lblProStatus.Visible = true;
            this.lblOrdproVsShip.Visible = true;
            this.lbldocumentation.Visible = true;
            this.lblBBlCduestatus.Visible = true;
            this.lblIncomeSt.Visible = true;
            string comcod = this.GetCompCode();
            string actcode = this.ddlProjectInd.SelectedValue.ToString();
            string date1 = this.txtDate.Text.Substring(0, 11);
            string dayid = this.dllorderType.SelectedValue.ToString()+"%";
            Session.Remove("tbllcstatus");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_LC_STATUS", "RPTLCSTATUS", actcode, date1, dayid, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvOrDer.DataSource = null;
                this.gvOrDer.DataBind();
                this.gvbblcst.DataSource = null;
                this.gvbblcst.DataBind();
                this.gvproStatus.DataSource = null;
                this.gvproStatus.DataBind();
                this.gvOrdProVsShip.DataSource = null;
                this.gvOrdProVsShip.DataBind();
                this.gvExport.DataSource = null;
                this.gvExport.DataBind();
                this.gvduebblc.DataSource = null;
                this.gvduebblc.DataBind();
                this.gvinstment.DataSource = null;
                this.gvinstment.DataBind();
                return;
            }
            Session["tbllcstatus"] = ds2;
            this.Data_Bind();




        }
      
        private void Data_Bind()
        {
            DataSet ds1 = ((DataSet)Session["tbllcstatus"]).Copy();


            // Order
            this.gvOrDer.DataSource = ds1.Tables[0];
            this.gvOrDer.DataBind();
            this.FooterCalculation("gv_OrDer", ds1.Tables[0]);
            // BBLC Staut
            this.gvbblcst.DataSource = ds1.Tables[1];
            this.gvbblcst.DataBind();
            // Production Status
            this.gvproStatus.DataSource = this.HiddenSameData(ds1.Tables[2]);
            this.gvproStatus.DataBind();

            // Order, Pro Vs. Shipment
            this.gvOrdProVsShip.DataSource = ds1.Tables[3];
            this.gvOrdProVsShip.DataBind();
            //  Doocumentation
            this.gvExport.DataSource = ds1.Tables[4];
            this.gvExport.DataBind();
            // BBLC Due Status
            this.gvduebblc.DataSource = ds1.Tables[5];
            this.gvduebblc.DataBind();
            this.FooterCalculation("gv_duebblc", ds1.Tables[5]);
            // Income Statement
            this.gvinstment.DataSource = this.HiddenSameData(ds1.Tables[6]);
            this.gvinstment.DataBind();






        }





        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string mgrp = dt1.Rows[0]["mgrp"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mgrp"].ToString() == mgrp)
                    dt1.Rows[j]["mgrpdesc"] = "";

                mgrp = dt1.Rows[j]["mgrp"].ToString();

            }

            return dt1;
        }

        private void FooterCalculation(string GrView, DataTable dt)
        {


            if (dt.Rows.Count == 0)
                return;

            switch (GrView)
            {
                case "gv_OrDer":
                    ((Label)this.gvOrDer.FooterRow.FindControl("lgvFordramtord")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordramt)", "")) ?
                        0.00 : dt.Compute("Sum(ordramt)", ""))).ToString("#,##0;(#,##0);  ");

                    break;


                case "gv_duebblc":
                    ((Label)this.gvduebblc.FooterRow.FindControl("lgvFbillamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(billamt)", "")) ?
                        0.00 : dt.Compute("Sum(billamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvduebblc.FooterRow.FindControl("lgvFdueamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam)", "")) ?
                       0.00 : dt.Compute("Sum(dueam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvduebblc.FooterRow.FindControl("lgvFnydueamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(nydueamt)", "")) ?
                       0.00 : dt.Compute("Sum(nydueamt)", ""))).ToString("#,##0;(#,##0);  ");

                    break;




            }


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataSet ds1 = ((DataSet)Session["tbllcstatus"]).Copy();


            //ReportDocument rptstk = new RMGiRPT.R_31_Mis.RptLcStatataglance();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            //txtprojectname.Text = "LC Name: "+this.ddlProjectInd.SelectedItem.Text;

            //TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void gvIncomeSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label parcent = (Label)e.Row.FindControl("lgvParcent");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    parcent.Font.Bold = true;
                    DAmount.Style.Add("text-align", "Left");
                }
                if (ASTUtility.Right((code), 5) == "99998" || ASTUtility.Right((code), 5) == "99999" || ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    parcent.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                }

            }
        }
        protected void gvinstment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lblgvItemDesc");
                Label BudgetedAmtFc = (Label)e.Row.FindControl("lblgvBudgetedFC");
                Label BudgetedAmt = (Label)e.Row.FindControl("lblgvBudgetedCost");
                //Label Preamt = (Label)e.Row.FindControl("lblgvPreamt");
                //Label Curamt = (Label)e.Row.FindControl("lblgvCuramt");
                Label Toamt = (Label)e.Row.FindControl("lblgvtoamt");
                Label BalAmt = (Label)e.Row.FindControl("lblgvBalAmt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    BudgetedAmtFc.Font.Bold = true;
                    BudgetedAmt.Font.Bold = true;
                    //Preamt.Font.Bold = true;
                    //Curamt.Font.Bold = true;
                    Toamt.Font.Bold = true;
                    BalAmt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");
                }

            }
        }
        protected void gvbblcst_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ressdesc = (Label)e.Row.FindControl("lblgvressdescbblc");
                Label orderamt = (Label)e.Row.FindControl("lgvorderamtbblc");
                Label rcvamt = (Label)e.Row.FindControl("lgvrcvamtbblc");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    ressdesc.Font.Bold = true;
                    orderamt.Font.Bold = true;
                    rcvamt.Font.Bold = true;
                    ressdesc.Style.Add("text-align", "right");
                }

            }
        }

        protected void ddlProjectInd_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tbllcorder"];
            string mlccode = this.ddlProjectInd.SelectedValue.ToString();
            this.dllorderType.DataSource = tbl1.FindAll(x => x.mlccod == mlccode);
            this.dllorderType.DataTextField = "rdaydesc";
            this.dllorderType.DataValueField = "rdayid";
            this.dllorderType.DataBind();

        }

        protected void gvExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink ressdesc = (HyperLink)e.Row.FindControl("hlnkgvressdescexp");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }

                ressdesc.Style.Add("color", "blue");
                ressdesc.NavigateUrl = "~/F_31_Mis/LinkExportDocs.aspx?comcod=" + code;








            }


        }
        protected void gvproStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lblgvgrpdesc");
                Label proqty = (Label)e.Row.FindControl("lgvproqty");
                Label percentage = (Label)e.Row.FindControl("lgvpercentage");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "stepcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    proqty.Font.Bold = true;
                    percentage.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");
                }

            }

        }
    }
}