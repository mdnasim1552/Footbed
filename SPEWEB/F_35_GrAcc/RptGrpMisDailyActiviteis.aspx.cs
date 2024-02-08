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
    public partial class RptGrpMisDailyActiviteis : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Management Interface";
                this.CallCompanyList();

                //this.chkConsolidate.Checked = true;

                //this.chkConsolidate_CheckedChanged(null, null);

                this.chkall.Checked = true;
                this.chkall_CheckedChanged(null, null);

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void CallCompanyList()
        {
            string comcod = this.GetCompCode();
            string consolidate = this.chkConsolidate.Checked ? "Consolidate" : "";
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.cblCompany.DataTextField = "comsnam";
            this.cblCompany.DataValueField = "comcod";
            this.cblCompany.DataSource = ds1.Tables[0];
            this.cblCompany.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblData");
            this.lblnote.Visible = true;
            this.lblSales.Visible = true;
            this.lblordtvsach.Visible = true;
            this.lblprotvach.Visible = true;
            this.lblexptvach.Visible = true;
            this.lblrlztvach.Visible = true;

            this.lblBalasoftoday.Visible = true;
            this.lblBBLCStatus.Visible = true;
            this.lblRecAPayEncash.Visible = true;
            this.lblProcurement.Visible = true;

            this.lbllcstatusatagalance.Visible = true;
            this.lblBankPosition.Visible = true;
            this.lblProdProc.Visible = true;

            this.lblRawMat.Visible = true;
            this.lblFinGoods.Visible = true;
            this.lblProjectStatus.Visible = true;
            this.lblForecasted.Visible = true;
            this.lblHrMgt.Visible = true;
            string comp1 = "";
            for (int i = 0; i < this.cblCompany.Items.Count; i++)
                comp1 += (this.cblCompany.Items[i].Selected ? this.cblCompany.Items[i].Value.ToString() : "");


            string frdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = MktData.GetTransInfo(comp1, "SP_REPORT_GROUP_MIS02", "RPTMGTACTIVITEIS", frdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvOrderrec.DataSource = null;
                this.gvOrderrec.DataBind();
                return;
            }

            ViewState["tblData"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }

        private void Data_Bind()
        {

            DataTable dt = ((DataTable)ViewState["tblData"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();





            //A. Sales
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'A'");
            dt1 = dvr.ToTable();
            this.gvOrderrec.DataSource = dt1;
            this.gvOrderrec.DataBind();
            // this.FooterCalculation(dt1, "gvDayWSale");   

            //B. Collection Summary
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'B'");
            dt1 = dvr.ToTable();
            this.gvorrtvach.DataSource = dt1;
            this.gvorrtvach.DataBind();

            //B. Collection Summary
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'C'");
            dt1 = dvr.ToTable();
            this.gvprotvach.DataSource = dt1;
            this.gvprotvach.DataBind();
            //B. Collection Summary
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'D'");
            dt1 = dvr.ToTable();
            this.gvexptvach.DataSource = dt1;
            this.gvexptvach.DataBind();
            //B. Collection Summary
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'E'");
            dt1 = dvr.ToTable();
            this.gvrlztvach.DataSource = dt1;
            this.gvrlztvach.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'F'");
            dt1 = dvr.ToTable();
            this.gvoexarlzbal.DataSource = dt1;
            this.gvoexarlzbal.DataBind();



            //C. Cheque In Hand

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'G'");
            dt1 = dvr.ToTable();
            this.gvLcStatus01.DataSource = dt1;
            this.gvLcStatus01.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'H'");
            dt1 = dvr.ToTable();
            this.gvProdProce.DataSource = dt1;
            this.gvProdProce.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'I'");
            dt1 = dvr.ToTable();
            this.gvlcstagalance.DataSource = dt1;
            this.gvlcstagalance.DataBind();



            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'J'");
            dt1 = dvr.ToTable();
            this.gvarecandpay.DataSource = dt1;
            this.gvarecandpay.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'K'");
            dt1 = dvr.ToTable();
            this.gvBankPosition.DataSource = dt1;
            this.gvBankPosition.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'L'");
            dt1 = dvr.ToTable();
            this.gvForInSt.DataSource = dt1;
            this.gvForInSt.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'M'");
            dt1 = dvr.ToTable();
            this.gvbblcstatus.DataSource = dt1;
            this.gvbblcstatus.DataBind();




            //dvr = dt.DefaultView;
            //dvr.RowFilter = ("grp = 'K'");
            //dt1 = dvr.ToTable();
            //this.gvpdc.DataSource = dt1;
            //this.gvpdc.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'N'");
            dt1 = dvr.ToTable();
            this.gvprocure.DataSource = dt1;
            this.gvprocure.DataBind();





            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'O'");
            dt1 = dvr.ToTable();
            this.gvRawMat.DataSource = dt1;
            this.gvRawMat.DataBind();




            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'P'");
            dt1 = dvr.ToTable();
            this.gvFinGoods.DataSource = dt1;
            this.gvFinGoods.DataBind();




            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'R'");
            dt1 = dvr.ToTable();
            this.gvHremp.DataSource = dt1;
            this.gvHremp.DataBind();
        }



        private DataTable HiddenSameData01(DataTable dt1)

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

        private void LinkBound()
        {

            //for (int i = 0; i < gvprocure.Rows.Count; i++)
            //{
            //    string comcod = ((Label)gvprocure.Rows[i].FindControl("lblgvcomcodepro")).Text.Trim();

            //    LinkButton lbtn1 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvreqpro");
            //    LinkButton lbtn2 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvreqapppro");
            //    LinkButton lbtn3 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvorderpro");
            //    LinkButton lbtn4 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvourchasepro");
            //    LinkButton lbtn5 = (LinkButton)gvprocure.Rows[i].FindControl("hlnkgvbillpro");
            //    if (lbtn1 != null)
            //    {
            //        if (lbtn1.Text.Trim().Length > 0)
            //            lbtn1.CommandArgument = comcod;
            //    }
            //    if (lbtn2 != null)
            //    {
            //        if (lbtn2.Text.Trim().Length > 0)
            //            lbtn2.CommandArgument = comcod;
            //    }

            //    if (lbtn3 != null)
            //    {
            //        if (lbtn3.Text.Trim().Length > 0)
            //            lbtn3.CommandArgument = comcod;
            //    }
            //    if (lbtn4 != null)
            //    {
            //        if (lbtn4.Text.Trim().Length > 0)
            //            lbtn4.CommandArgument = comcod;
            //    }

            //    if (lbtn5 != null)
            //    {
            //        if (lbtn5.Text.Trim().Length > 0)
            //            lbtn5.CommandArgument = comcod;
            //    }


            //}

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = dt1.Rows[0]["comcod"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            string pgrp = dt1.Rows[0]["pgrp"].ToString();
            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["comcod"].ToString() == comcod) && (dt1.Rows[j]["grp"].ToString() == grp))
                    dt1.Rows[j]["compname"] = "";
                if (dt1.Rows[j]["pgrp"].ToString() == pgrp)
                    dt1.Rows[j]["pgrpdesc"] = "";

                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                    dt1.Rows[j]["deptname"] = "";

                comcod = dt1.Rows[j]["comcod"].ToString();
                grp = dt1.Rows[j]["grp"].ToString();
                pgrp = dt1.Rows[j]["pgrp"].ToString();
                deptcode = dt1.Rows[j]["deptcode"].ToString();
            }


            return dt1;

        }

        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            DataTable dt2 = new DataTable();



            //DataTable dt = (DataTable)Session["tblData"];




            switch (GvName)
            {
                case "gvDayWSale":
                    dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
                    dt2 = dv.ToTable();
                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tuamt)", "")) ?
                            0 : dt2.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(suamt)", "")) ?
                                    0 : dt2.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;




                    break;






            }


        }


        protected void gvOrderrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamesale");
                Label orderamt = (Label)e.Row.FindControl("lgvorderamtrec");
                Label proamt = (Label)e.Row.FindControl("lgvproamt");
                Label shipamt = (Label)e.Row.FindControl("lgvshipamt");
                Label realizedamt = (Label)e.Row.FindControl("lgvrealizedamt");


                Label orderqty = (Label)e.Row.FindControl("lgvorderrecqty");
                Label proqty = (Label)e.Row.FindControl("lgvproqty");
                Label shipqty = (Label)e.Row.FindControl("lgvshipqty");


                HyperLink gvGraph = (HyperLink)e.Row.FindControl("hlnkgvGraph");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                double amt1 = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ordramt")) / 1000000);
                double amt2 = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "proamt")) / 1000000);
                double amt3 = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "shipamt")) / 1000000);
                double amt4 = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rlzamt")) / 1000000);




                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;

                    orderamt.Font.Bold = true;
                    proamt.Font.Bold = true;
                    shipamt.Font.Bold = true;
                    orderamt.Font.Bold = true;
                    realizedamt.Font.Bold = true;
                    orderqty.Font.Bold = true;
                    proqty.Font.Bold = true;
                    shipqty.Font.Bold = true;

                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    comname.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptOrdProVsShip&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    gvGraph.Style.Add("color", "blue");
                    gvGraph.NavigateUrl = "~/F_35_GrAcc/RptRealGraph.aspx?Type=RealGrahp&comcod=" + code + "&orderamt=" + amt1.ToString("#,##0.00;(#,##0.00);") + "&proamt=" + amt2.ToString("#,##0.00;(#,##0.00);") + "&shipamt=" + amt3.ToString("#,##0.00;(#,##0.00);") + "&rlzamt=" + amt4.ToString("#,##0.00;(#,##0.00);");


                }

            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblData"];
            //ReportDocument rrs1 = new RMGiRPT.R_35_GrAcc.RptGrpMisMgtActivities();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "(From " + this.txtDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs1.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rrs1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvorrtvach_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink company = (HyperLink)e.Row.FindControl("hlnkgvcompanyotvach");
                Label capacity = (Label)e.Row.FindControl("lgvcapacityotvach");
                Label masbgd = (Label)e.Row.FindControl("lgvmasbgdotvach");
                Label bep = (Label)e.Row.FindControl("lgvbepotvach");
                Label monbgd = (Label)e.Row.FindControl("lgvmonbgdotvach");
                Label actual = (Label)e.Row.FindControl("lgvactualotvach");
                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphotvach");



                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    company.Font.Bold = true;
                    capacity.Font.Bold = true;
                    masbgd.Font.Bold = true;
                    bep.Font.Bold = true;
                    monbgd.Font.Bold = true;
                    actual.Font.Bold = true;
                    company.Style.Add("text-align", "right");



                }

                else
                {
                    string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Monbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "monbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Actual = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "actual")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    graph.Style.Add("color", "blue");
                    graph.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisGraph.aspx?comcod=" + comcod + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&monbgd=" + Monbgd + "&actual=" + Actual;


                }

            }

        }

        protected void gvprotvach_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink company = (HyperLink)e.Row.FindControl("hlnkgvcompanyptvach");
                Label capacity = (Label)e.Row.FindControl("lgvcapacityptvach");
                Label masbgd = (Label)e.Row.FindControl("lgvmasbgdptvach");
                Label bep = (Label)e.Row.FindControl("lgvbepptvach");
                Label monbgd = (Label)e.Row.FindControl("lgvmonbgdptvach");
                Label actual = (Label)e.Row.FindControl("lgvactualptvach");


                HyperLink tarvsach = (HyperLink)e.Row.FindControl("hlnkgvactdesctvach");
                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphptvach");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    company.Font.Bold = true;
                    capacity.Font.Bold = true;
                    masbgd.Font.Bold = true;
                    bep.Font.Bold = true;
                    monbgd.Font.Bold = true;
                    actual.Font.Bold = true;
                    company.Style.Add("text-align", "right");



                }

                else
                {
                    string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Monbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "monbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Actual = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "actual")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    graph.Style.Add("color", "blue");
                    tarvsach.Style.Add("color", "blue");
                    graph.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisGraph.aspx?comcod=" + comcod + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&monbgd=" + Monbgd + "&actual=" + Actual;
                    tarvsach.NavigateUrl = "~/F_35_GrAcc/LinkGrpExPlanAchiv.aspx?comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }

            }
        }

        protected void gvexptvach_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink company = (HyperLink)e.Row.FindControl("hlnkgvcompanyetvach");
                Label capacity = (Label)e.Row.FindControl("lgvcapacityetvach");
                Label masbgd = (Label)e.Row.FindControl("lgvmasbgdetvach");
                Label bep = (Label)e.Row.FindControl("lgvbepetvach");
                Label monbgd = (Label)e.Row.FindControl("lgvmonbgdetvach");
                Label actual = (Label)e.Row.FindControl("lgvactualetvach");
                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphetvach");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    company.Font.Bold = true;
                    capacity.Font.Bold = true;
                    masbgd.Font.Bold = true;
                    bep.Font.Bold = true;
                    monbgd.Font.Bold = true;
                    actual.Font.Bold = true;
                    company.Style.Add("text-align", "right");



                }

                else
                {
                    string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Monbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "monbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Actual = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "actual")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    graph.Style.Add("color", "blue");
                    graph.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisGraph.aspx?comcod=" + comcod + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&monbgd=" + Monbgd + "&actual=" + Actual;

                }

            }
        }
        protected void gvrlztvach_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink company = (HyperLink)e.Row.FindControl("hlnkgvcompanyrtvach");
                Label capacity = (Label)e.Row.FindControl("lgvcapacityrtvach");
                Label masbgd = (Label)e.Row.FindControl("lgvmasbgdrtvach");
                Label bep = (Label)e.Row.FindControl("lgvbeprtvach");
                Label monbgd = (Label)e.Row.FindControl("lgvmonbgdrtvach");
                Label actual = (Label)e.Row.FindControl("lgvactualrtvach");
                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphrtvach");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    company.Font.Bold = true;
                    capacity.Font.Bold = true;
                    masbgd.Font.Bold = true;
                    bep.Font.Bold = true;
                    monbgd.Font.Bold = true;
                    actual.Font.Bold = true;
                    company.Style.Add("text-align", "right");



                }

                else
                {
                    string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Monbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "monbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Actual = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "actual")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    graph.Style.Add("color", "blue");
                    graph.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisGraph.aspx?comcod=" + comcod + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&monbgd=" + Monbgd + "&actual=" + Actual;


                }

            }
        }


        protected void gvoexarlzbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink company = (HyperLink)e.Row.FindControl("hlnkgvcompanycbal");
                Label ordramt = (Label)e.Row.FindControl("lgvordramtoexarbal");
                Label proamt = (Label)e.Row.FindControl("lgvproamtoexarbal");
                Label probal = (Label)e.Row.FindControl("lgvprobaltoexarbal");
                Label examt = (Label)e.Row.FindControl("lgvexamtoexarbal");
                Label exbal = (Label)e.Row.FindControl("lgvexbaltoexarbal");
                Label realizedamt = (Label)e.Row.FindControl("lgvrealizedamtoexarbal");
                Label realbal = (Label)e.Row.FindControl("lgvrealbaltoexarbal");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    company.Font.Bold = true;
                    ordramt.Font.Bold = true;
                    proamt.Font.Bold = true;
                    probal.Font.Bold = true;
                    examt.Font.Bold = true;
                    exbal.Font.Bold = true;
                    realizedamt.Font.Bold = true;
                    realbal.Font.Bold = true;
                    company.Style.Add("text-align", "right");



                }

                else
                {
                    company.Style.Add("color", "blue");
                    company.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptOrdExparlz&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }

            }
        }
        protected void gvbblcstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label Company = (Label)e.Row.FindControl("lblgvCompanybblc");
                Label lcamtbblc = (Label)e.Row.FindControl("lgvlcamtbblc");
                HyperLink orderworder = (HyperLink)e.Row.FindControl("hlnkgvorderworder");
                HyperLink orderwsupp = (HyperLink)e.Row.FindControl("hlnkgvorderwsupp");
                Label peronbblc = (Label)e.Row.FindControl("lgvperonbblc");
                Label paidamt = (Label)e.Row.FindControl("lgvpaidamt");
                Label dueam = (Label)e.Row.FindControl("lgvdueam");
                Label nydueamt = (Label)e.Row.FindControl("lgvnydueamt");



                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();



                if (comcod != "AAAA")
                {
                    orderworder.Style.Add("color", "blue");
                    orderwsupp.Style.Add("color", "blue");

                    orderworder.NavigateUrl = "~/F_35_GrAcc/LinkBBLCSummary.aspx?Type=OrderWise&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    orderwsupp.NavigateUrl = "~/F_35_GrAcc/LinkBBLCStatus.aspx?Type=SupWise&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    //  HLgvDescpaysum.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptBBLPaySt&comcod=" + comcod + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy"); ;

                }
                else

                {
                    Company.Font.Bold = true;
                    lcamtbblc.Font.Bold = true;
                    orderworder.Font.Bold = true;
                    orderwsupp.Font.Bold = true;
                    peronbblc.Font.Bold = true;
                    paidamt.Font.Bold = true;
                    dueam.Font.Bold = true;
                    nydueamt.Font.Bold = true;

                }








            }


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label Company = (Label)e.Row.FindControl("lblgvCompanycih");
            //    HyperLink amount = (HyperLink)e.Row.FindControl("hlnkgvamtcin");
            //    Label inhrchqcih = (Label)e.Row.FindControl("lgvinhrchqcih");
            //    Label inhfchqcih = (Label)e.Row.FindControl("lgvinhfchqcih");
            //    Label inhpchqcih = (Label)e.Row.FindControl("lgvinhpchqcih");


            //    string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

            //    if (comcod == "")
            //    {
            //        return;
            //    }
            //    if (comcod == "AAAA")
            //    {

            //        Company.Font.Bold = true;
            //        amount.Font.Bold = true;
            //        inhrchqcih.Font.Bold = true;
            //        inhfchqcih.Font.Bold = true;
            //        inhpchqcih.Font.Bold = true;
            //        Company.Style.Add("text-align", "right");
            //    }

            //    else
            //    {

            //        DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            //        if (ds2.Tables[0].Rows.Count == 0)
            //        {
            //            return;

            //        }
            //        amount.Style.Add("color", "blue");
            //        DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);
            //        amount.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=ChequeInHand&comcod="+comcod+"&Date1=" + Convert.ToDateTime(txtopndate).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            //    }

            //}
        }
        protected void gvarecandpay_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label Company = (Label)e.Row.FindControl("lblgvCompanyrecapay");
                Label recam = (Label)e.Row.FindControl("lgvrecam");
                Label PayAmt = (Label)e.Row.FindControl("lgvpayam");
                HyperLink Balamt = (HyperLink)e.Row.FindControl("hlnkgvbalpam");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                {
                    return;
                }

                if (comcod == "AAAA")
                {

                    Company.Font.Bold = true;
                    recam.Font.Bold = true;
                    PayAmt.Font.Bold = true;
                    Balamt.Font.Bold = true;
                    Company.Style.Add("text-align", "right");
                }
                else
                {
                    Balamt.Style.Add("color", "blue");
                    Balamt.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RecPay&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }



            }




        }
        protected void gvarecandpayis_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label RecpDesc = (Label)e.Row.FindControl("lgvRecDescis");
                HyperLink lgvRecAmt = (HyperLink)e.Row.FindControl("hlnkgvrecpamis");

                Label PayDesc = (Label)e.Row.FindControl("lblgvPayDescis");
                Label lgvPayAmt = (Label)e.Row.FindControl("lgvpayamis");


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 4) == "AAAA")
                {

                    RecpDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 4) == "AAAA")
                {
                    PayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

                if (code1 == "99BBBBAAAAAA")
                {
                    lgvRecAmt.Style.Add("color", "blue");
                    string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                    lgvRecAmt.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=IssuedVsCollect&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }

            }

        }


        protected void gvHremp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("lnkgvcomname");
                Label toemp = (Label)e.Row.FindControl("lgvtoemp");
                Label netsalary = (Label)e.Row.FindControl("lgvnetsalary");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrcomcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    toemp.Font.Bold = true;
                    netsalary.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                    comname.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkRptMgtInterface.aspx?comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }





            }


        }
        protected void gvBankPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink bankposition = (HyperLink)e.Row.FindControl("hlnkgvbankposition");
                Label BankBal = (Label)e.Row.FindControl("lgvbankbalbp");
                Label banklia = (Label)e.Row.FindControl("lblgvbankliabp");
                Label netcbolia = (Label)e.Row.FindControl("lblgvnetcbolia");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                    return;

                if (comcod == "AAAA")
                {
                    bankposition.Font.Bold = true;
                    BankBal.Font.Bold = true;
                    banklia.Font.Bold = true;
                    netcbolia.Font.Bold = true;
                    bankposition.Style.Add("text-align", "right");
                }

                else
                {
                    bankposition.Style.Add("color", "blue");
                    bankposition.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=BankPosition&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }



            }






        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall.Checked)
            {
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                {
                    cblCompany.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                {
                    cblCompany.Items[i].Selected = false;

                }

            }
        }
        protected void chkConsolidate_CheckedChanged(object sender, EventArgs e)
        {
            this.CallCompanyList();
            this.chkall.Checked = false;
            this.chkall_CheckedChanged(null, null);
        }

        protected void gvExRel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label comname = (Label)e.Row.FindControl("lblgvcomnameexrel");
                HyperLink exraincen = (HyperLink)e.Row.FindControl("hlnkgvexraincen");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {
                    comname.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
                else
                {
                    exraincen.Style.Add("color", "blue");

                    exraincen.NavigateUrl = "~/F_35_GrAcc/LinkGrpExportRLZ.aspx?comcod=" + code;

                }






            }

        }

        protected void gvlcstagalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink lcsgalance = (HyperLink)e.Row.FindControl("hlnkgvlcsgalance");
                HyperLink lcposition = (HyperLink)e.Row.FindControl("hlnkgvlcposition");
                HyperLink exraincen = (HyperLink)e.Row.FindControl("hlnkgvexraincen");

                //hlnkgvlcposition
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }

                lcsgalance.Style.Add("color", "blue");
                lcposition.Style.Add("color", "blue");
                exraincen.Style.Add("color", "blue");

                lcsgalance.NavigateUrl = "~/F_35_GrAcc/LinkGrpLCInfoataglance.aspx?comcod=" + code + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                lcposition.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=LcSummary&comcod=" + code + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                exraincen.NavigateUrl = "~/F_35_GrAcc/LinkGrpExportRLZ.aspx?comcod=" + code;
                //lcposition.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=LcDetails&comcod=" + code + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");













            }


        }

        protected void gvLcStatus01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnameps1");


                Label ordrvalue = (Label)e.Row.FindControl("lgvordrvaluels");
                Label lgvbudgetcost = (Label)e.Row.FindControl("lgvbudgetcost");
                HyperLink budgetnp = (HyperLink)e.Row.FindControl("hlnkgvbudgetnp");
                Label bgdnpinper = (Label)e.Row.FindControl("lgvbgdnpinper");

                Label exportvalue = (Label)e.Row.FindControl("lgvexportvaluels");
                Label tocost = (Label)e.Row.FindControl("lgvtocostls");
                HyperLink netposition = (HyperLink)e.Row.FindControl("hlnkgvnetpositionls");
                Label peronpro = (Label)e.Row.FindControl("lgvperonpro");








                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {

                    Compname.Font.Bold = true;
                    ordrvalue.Font.Bold = true;
                    lgvbudgetcost.Font.Bold = true;
                    exportvalue.Font.Bold = true;
                    tocost.Font.Bold = true;
                    netposition.Font.Bold = true;
                    peronpro.Font.Bold = true;
                    budgetnp.Font.Bold = true;
                    bgdnpinper.Font.Bold = true;
                    Compname.Style.Add("text-align", "right");
                }

                else
                {


                    budgetnp.Style.Add("color", "blue");
                    budgetnp.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=BgdLCStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();


                    netposition.Style.Add("color", "blue");
                    netposition.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=LCStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();



                }

            }
        }

        protected void gvProjectStatus02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnameps2");
                Label ExpAmt = (Label)e.Row.FindControl("lgvExpAmt");
                Label PAdvAmt = (Label)e.Row.FindControl("lgvPAdvAmt");
                Label LCNFAmt = (Label)e.Row.FindControl("lgvLCNFAmt");
                Label Ovmt = (Label)e.Row.FindControl("lgvOvmt");
                Label IAmt = (Label)e.Row.FindControl("lgvIAmt");
                Label texpamt = (Label)e.Row.FindControl("lgvtexamt");
                Label liaamt = (Label)e.Row.FindControl("lgvliaamt");
                Label netexpenses = (Label)e.Row.FindControl("lgvnetexpenses02");





                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {

                    Compname.Font.Bold = true;
                    ExpAmt.Font.Bold = true;
                    PAdvAmt.Font.Bold = true;
                    LCNFAmt.Font.Bold = true;
                    Ovmt.Font.Bold = true;
                    IAmt.Font.Bold = true;
                    texpamt.Font.Bold = true;
                    liaamt.Font.Bold = true;
                    netexpenses.Font.Bold = true;
                    Compname.Style.Add("text-align", "right");
                }



            }

        }



        protected void gvSalevsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamesvscoll");
                Label tsales = (Label)e.Row.FindControl("lgvtsales");
                Label tosales = (Label)e.Row.FindControl("lgvtosales");
                Label acsales = (Label)e.Row.FindControl("lgvacsales");
                Label peronsal = (Label)e.Row.FindControl("lgvperonsal");
                Label tcoll = (Label)e.Row.FindControl("lgvtcoll");
                Label tocoll = (Label)e.Row.FindControl("lgvtocoll");
                Label accoll = (Label)e.Row.FindControl("lgvaccoll");
                Label peroncoll = (Label)e.Row.FindControl("lgvperoncoll");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    tsales.Font.Bold = true;
                    tosales.Font.Bold = true;
                    acsales.Font.Bold = true;

                    peronsal.Font.Bold = true;
                    tcoll.Font.Bold = true;
                    tocoll.Font.Bold = true;
                    accoll.Font.Bold = true;
                    peroncoll.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    comname.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }





            }

        }

        protected void gvProdProce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink AllOrd = (HyperLink)e.Row.FindControl("hlnkgvAllOrd");
                HyperLink IndOrd = (HyperLink)e.Row.FindControl("hlnkgvIndOrd");
                HyperLink probsinst = (HyperLink)e.Row.FindControl("hlnkgvprobsinst");
                HyperLink protarpro = (HyperLink)e.Row.FindControl("hlnkgvprotarpro");
                HyperLink linepro = (HyperLink)e.Row.FindControl("hlnkgvlinepro");
                HyperLink daywisepro = (HyperLink)e.Row.FindControl("hlnkgvdaywisepro");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {
                }
                else
                {

                    probsinst.Style.Add("color", "blue");
                    probsinst.NavigateUrl = "~/F_35_GrAcc/LinkGrpIncomestprobasis.aspx?comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                    AllOrd.Style.Add("color", "blue");
                    AllOrd.NavigateUrl = "~/F_35_GrAcc/LinkGrpProtarVsAchieve.aspx?Type=RptAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    IndOrd.Style.Add("color", "blue");
                    IndOrd.NavigateUrl = "~/F_35_GrAcc/LinkGrpProtarVsAchieve.aspx?Type=IndProduc&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                    linepro.Style.Add("color", "blue");
                    linepro.NavigateUrl = "~/F_35_GrAcc/LinkGrpProtarVsAchieve.aspx?Type=Protvach&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    daywisepro.Style.Add("color", "blue");
                    daywisepro.NavigateUrl = "~/F_35_GrAcc/LinkGrpDWiseProduction.aspx?Type=ProStatus&comcod=" + code;



                    //IndOrd.Style.Add("color", "blue");
                    //IndOrd.NavigateUrl = "~/F_35_GrAcc/LinkGrpProtarVsAchieve.aspx?Type=ProProcess&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    //IndOrd.Style.Add("color", "blue");
                    //IndOrd.NavigateUrl = "~/F_35_GrAcc/LinkGrpProtarVsAchieve.aspx?Type=ProProcess&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    //protarpro.Style.Add("color", "blue");
                    //protarpro.NavigateUrl = "~/F_35_GrAcc/LinkGrpExPlanAchivAll.aspx?comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }


            }

        }


        protected void gvFinGoods_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink FInGen = (HyperLink)e.Row.FindControl("hlnkgvFGen");
                HyperLink FInQty = (HyperLink)e.Row.FindControl("hlnkgvFQty");
                HyperLink FInAmt = (HyperLink)e.Row.FindControl("hlnkgvFAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {
                }
                else
                {

                    FInGen.Style.Add("color", "blue");
                    FInGen.NavigateUrl = "~/F_35_GrAcc/LinkFGInvReport.aspx?InputType=General&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    FInQty.Style.Add("color", "blue");
                    FInQty.NavigateUrl = "~/F_35_GrAcc/LinkFGInvReport.aspx?InputType=QuantityB&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    FInAmt.Style.Add("color", "blue");
                    FInAmt.NavigateUrl = "~/F_35_GrAcc/LinkFGInvReport.aspx?InputType=AmountB&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }


            }

        }
        protected void gvRawMat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink InGen = (HyperLink)e.Row.FindControl("hlnkgvInGen");
                HyperLink InQty = (HyperLink)e.Row.FindControl("hlnkgvInQty");
                HyperLink InAmt = (HyperLink)e.Row.FindControl("hlnkgvInAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    //InGen.Font.Bold = true;
                    //InQty.Font.Bold = true;
                    //InAmt.Font.Bold = true;

                }
                else
                {

                    InGen.Style.Add("color", "blue");
                    InGen.NavigateUrl = "~/F_35_GrAcc/LinkInvReport.aspx?Type=General&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    InQty.Style.Add("color", "blue");
                    InQty.NavigateUrl = "~/F_35_GrAcc/LinkInvReport.aspx?Type=QuantityB&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    InAmt.Style.Add("color", "blue");
                    InAmt.NavigateUrl = "~/F_35_GrAcc/LinkInvReport.aspx?Type=AmountB&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }





            }

        }
        protected void gvForInSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    Label comname = (Label)e.Row.FindControl("lgvcomnamefinst");
                    HyperLink forinst = (HyperLink)e.Row.FindControl("hlnkgvfinst");
                    HyperLink bvsacinst = (HyperLink)e.Row.FindControl("hlnkgvbvsacinst");

                    HyperLink actualinst = (HyperLink)e.Row.FindControl("hlnkgvactualinst");
                    HyperLink balsheet = (HyperLink)e.Row.FindControl("hlnkgvbalsheet");
                    HyperLink CFlow = (HyperLink)e.Row.FindControl("hlnkgvCFlow");

                    HyperLink Flow = (HyperLink)e.Row.FindControl("hlnkgvFlow");


                    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                    if (code == "")
                    {
                        return;
                    }
                    if (code == "AAAA")
                    {

                        comname.Font.Bold = true;
                        comname.Style.Add("text-align", "right");

                    }
                    else
                    {
                        forinst.Style.Add("color", "blue");
                        bvsacinst.Style.Add("color", "blue");

                        actualinst.Style.Add("color", "blue");
                        balsheet.Style.Add("color", "blue");
                        CFlow.Style.Add("color", "blue");
                        Flow.Style.Add("color", "blue");
                        forinst.NavigateUrl = "~/F_35_GrAcc/LinkRptGrpIncomeSt.aspx?comcod=" + code;
                        bvsacinst.NavigateUrl = "~/F_35_GrAcc/LinkGrpIncomestbgdvsac.aspx?comcod=" + code + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy"); ;

                        actualinst.NavigateUrl = "~/F_35_GrAcc/LinkFinncialReports.aspx?Type=IS&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                        balsheet.NavigateUrl = "~/F_35_GrAcc/LinkFinncialReports.aspx?Type=BS&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                        CFlow.NavigateUrl = "~/F_35_GrAcc/GrpLinkAccount.aspx?Type=CashFlow&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                        Flow.NavigateUrl = "~/F_35_GrAcc/LinkRealInOutFlow.aspx?Type=RealFlow&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");



                    }






                }





            }



        }
        protected void gvprocure_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkgvreqpro = (HyperLink)e.Row.FindControl("hlnkgvreqpro");
                HyperLink hlnkgvreqapppro = (HyperLink)e.Row.FindControl("hlnkgvreqapppro");
                HyperLink hlnkgvorderpro = (HyperLink)e.Row.FindControl("hlnkgvorderpro");
                HyperLink hlnkgvourchasepro = (HyperLink)e.Row.FindControl("hlnkgvourchasepro");
                HyperLink hlnkgvbillpro = (HyperLink)e.Row.FindControl("hlnkgvbillpro");
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();




                if (comcod != "AAAA")
                {
                    hlnkgvreqpro.Style.Add("color", "blue");
                    hlnkgvreqapppro.Style.Add("color", "blue");
                    hlnkgvorderpro.Style.Add("color", "blue");
                    hlnkgvourchasepro.Style.Add("color", "blue");
                    hlnkgvbillpro.Style.Add("color", "blue");

                    hlnkgvreqpro.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisPurChase.aspx?Type=ReqStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    hlnkgvreqapppro.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisPurChase.aspx?Type=ReqAppStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    hlnkgvorderpro.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisPurChase.aspx?Type=WorkOrder&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    hlnkgvourchasepro.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisPurChase.aspx?Type=Purchase&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    hlnkgvbillpro.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisPurChase.aspx?Type=ConBill&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }








            }
        }
        protected void gvarecandpay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lnkgvreqpro_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string recpcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //DataTable dt = (DataTable)Session["recandpay"];
            //DataView dv1 = dt.DefaultView;
            //dv1.RowFilter = "recpcode like('" + recpcode + "')";
            //dt = dv1.ToTable();

            //string mCOMCOD = comcod;
            //string mTRNDAT1 = this.txtfromdate.Text;
            //string mTRNDAT2 = this.txttodate.Text;
            //string mACTCODE = dt.Rows[0]["recpcode"].ToString();
            //string mACTDESC = dt.Rows[0]["recpdesc"].ToString();
            //string lebel2 = dt.Rows[0]["rleb2"].ToString();
            //if (mACTCODE == "")
            //{
            //    return;
            //}

            /////---------------------------------//// 
            //if (ASTUtility.Left(mACTCODE, 1) == "4")
            //{
            //    lbljavascript.Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
            //                    mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            //}
            //else if (lebel2 == "")
            //{
            //    lbljavascript.Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
            //                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            //}
            //else
            //{
            //    lbljavascript.Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
            //                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            //}

        }
        protected void lnkgvreqapppro_Click(object sender, EventArgs e)
        {
            string comcod = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('~/F_35_GrAcc/LinkGrpMisPurChase.aspx?Type=ReqAppStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + "', target='_blank');</script>";


        }
        protected void lnkgvorderpro_Click(object sender, EventArgs e)
        {

        }
        protected void lnkgvourchasepro_Click(object sender, EventArgs e)
        {

        }
        protected void hlnkgvbillpro_Click(object sender, EventArgs e)
        {

        }






        protected void gvmonprost_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnamemps");
                Label Cost = (Label)e.Row.FindControl("lgvcostmps");
                Label collamt = (Label)e.Row.FindControl("lgvcollamtmps");
                Label netposition = (Label)e.Row.FindControl("lgvnetpositionmps");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Compname.Font.Bold = true;
                    Cost.Font.Bold = true;
                    collamt.Font.Bold = true;
                    netposition.Font.Bold = true;

                    Compname.Style.Add("text-align", "right");
                }

                else
                {

                    Compname.Style.Add("color", "blue");
                    Compname.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MProStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");



                }

            }
        }






    }

}