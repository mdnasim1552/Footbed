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
    public partial class ProjTrialBalanc : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.ImgbtnFindProjind_Click(null, null);
                this.SelectView();
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.LblTitle.Text = (type == "PrjTrailBal") ? "Project Trail Balance" : (type == "LandPrj") ? "Project Trail Balance" : "Trail Balance 2";
            }

        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjTrailBal":
                case "LandPrj":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "TrailBal2":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblPrjName.Visible = false;
                    this.txtSearchpIndp.Visible = false;
                    this.ImgbtnFindProjind.Visible = false;
                    this.ddlProjectInd.Visible = false;
                    this.lblGrp.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    break;
            }
        }

        protected void ImgbtnFindProjind_Click(object sender, ImageClickEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = "%" + this.txtSearchpIndp.Text.Trim() + "%";
            string pactcode = (this.Request.QueryString["Type"].ToString() == "LandPrj") ? "16%" : "4[1-9]%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "GETPROJECTNAME", pactcode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjTrailBal":
                case "LandPrj":
                    this.ShowPrjTriBal();
                    break;

                case "TrailBal2":
                    this.ShowTrailsBal2();
                    break;
            }
        }
        private void ShowPrjTriBal()
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string actcode = (((ASTUtility.Right(this.ddlProjectInd.SelectedValue, 10) == "0000000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 2)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 8) == "00000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 4)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 4) == "0000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 8) : this.ddlProjectInd.SelectedValue.ToString()) + "%");

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
            string CallType = (ASTUtility.Left(actcode, 2) == "41") ? "RPT_PROJ_TRIALBAL" : (ASTUtility.Left(actcode, 2) == "16") ? "RPT_PROJ_TRIALBALWIP" : "RPTPROJTRIALBALHF";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", CallType, "", date1, actcode, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvPrjtrbal.DataSource = null;
                this.gvPrjtrbal.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;
            this.gvPrjtrbal.DataSource = dt;
            this.gvPrjtrbal.DataBind();

            Session["tblFooter"] = ds2.Tables[1];
            Session["tblPrjname"] = ds2.Tables[2];

            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvPrjtrbal.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["Report1"] = gvPrjtrbal;
            ((HyperLink)this.gvPrjtrbal.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            //((Label)this.gvPrjtrbal.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(dramt)", "")) ?
            //          0.00 : ds2.Tables[1].Compute("Sum(dramt)", ""))).ToString("#,##0;(#,##0); - ");

            //((Label)this.gvPrjtrbal.FooterRow.FindControl("lgvFTCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(cramt)", "")) ?
            //         0.00 : ds2.Tables[1].Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); - ");

        }
        private void ShowTrailsBal2()
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTTRIALBALANCE2", date1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.grvTrBal2.DataSource = null;
                this.grvTrBal2.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;

            this.grvTrBal2.DataSource = dt;
            this.grvTrBal2.DataBind();

            Session["tblFooter"] = ds2.Tables[1];


            ((Label)this.grvTrBal2.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(dram)", "")) ?
                      0.00 : ds2.Tables[1].Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); - ");

            ((Label)this.grvTrBal2.FooterRow.FindControl("lgvFTCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(cram)", "")) ?
                     0.00 : ds2.Tables[1].Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); - ");

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string grpcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjTrailBal":
                    grpcode = dt1.Rows[0]["grp"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }
                        else
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                        }

                    }
                    break;

                case "TrailBal2":
                    string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                        {
                            actcode1 = dt1.Rows[j]["actcode1"].ToString();
                            dt1.Rows[j]["actdesc1"] = "";
                        }
                        else
                        {
                            actcode1 = dt1.Rows[j]["actcode1"].ToString();
                        }

                    }
                    break;
            }



            return dt1;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjTrailBal":
                case "LandPrj":
                    this.RtpPrjTrBal();
                    break;

                case "TrailBal2":
                    this.RtpTrBal2();
                    break;
            }
        }
        private void RtpPrjTrBal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;
            //ReportDocument rptstk = new RMGiRPT.R_31_Mis.RptProjTrialBalance();
            ////TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            ////txtCompany.Text = comnam;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            //txtprojectname.Text = "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString(); // prjsdesc   this.ddlProjectInd.SelectedItem.ToString().Trim().Substring(13);

            ////TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            ////txtdramt.Text = Convert.ToDouble(dt2.Rows[0]["dramt"]).ToString("#, #,#0; (#, #,#0); "); ;
            ////TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            ////txtcramt.Text = Convert.ToDouble(dt2.Rows[0]["cramt"]).ToString("#, #,#0; (#, #,#0); "); ;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RtpTrBal2()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];

            if (dt1.Rows.Count == 0)
                return;
            //ReportDocument rptstk = new RMGiRPT.R_31_Mis.RptTrialBalance2();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");

            ////TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            ////txtdramt.Text = Convert.ToDouble(dt2.Rows[0]["dram"]).ToString("#, #,#0; (#, #,#0); "); ;
            ////TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            ////txtcramt.Text = Convert.ToDouble(dt2.Rows[0]["cram"]).ToString("#, #,#0; (#, #,#0); "); ;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvPrjtrbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000" || code == "000000000001")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;

                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");
                }
                if (code == "999999999999" || code == "000000000002" || code == "000000000003" || code == "000000000004")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                    DAmount.Style.Add("text-align", "Right");
                    CAmount.Style.Add("text-align", "Right");
                }
                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }


                //if (e.Row.RowType != DataControlRowType.DataRow)
                //    return;
                string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode1")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                string Actcode = this.ddlProjectInd.SelectedValue.ToString();
                string Date1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyy");
                string rescode = ((Label)e.Row.FindControl("lblgvCode")).Text;
                if (ASTUtility.Left(rescode1, 4) == "AAAA")
                {
                    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + Actcode + "&Date1=" + Date1;
                }

                if (ASTUtility.Right((code), 10) != "0000000000" && code != "000000000001" && code != "999999999999" && code != "000000000002")
                {
                    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=SpLedger&pactcode=" + Actcode + "&Date1=" + Date1 + "&rescode=" + rescode;
                }


            }


        }
        protected void grvTrBal2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "DIFFERENCE")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                }


            }
        }
    }
}