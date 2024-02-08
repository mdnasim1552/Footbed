using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using SPELIB;
using AjaxControlToolkit;
using SPERDLC;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_10_Procur
{
    public partial class ReqAdjstmntList : System.Web.UI.Page
    {
        private int PageSize = 5;
        ProcessAccess InvData = new ProcessAccess();
        //SalesInvoice_BL viewSales = new SalesInvoice_BL();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (prevPage.Length == 0)
                //{

                //    prevPage = Request.UrlReferrer.ToString();
                //}
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ReqAdjst") ? "Requisition Adjustment List" : (type == "ProAdjst") ? "Production Adjustment List" : "Production Manually List";
                this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
                //((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.SelectView();
            }
        }
        private void CommonButton()
        {


            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "View";
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Request";


            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "ProAdjst")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Revarse in Production";
            }
            else if (type == "prodman")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Complete";
            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("href", "../F_13_ProdMon/ProductionManually.aspx?Type=Entryprod&genno=");
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("target", "_blank");

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnAll_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click);

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString() == "prodman")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_13_ProdMon/ProductionManually.aspx?Type=Entry&genno=" + "', target='_blank');</script>";
            }

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "ReqAdjst")
            {
                this.MultiView.ActiveViewIndex = 0;
            }
            else if (type == "ProAdjst")
            {
                this.MultiView.ActiveViewIndex = 1;
            }
            else
            {
                this.MultiView.ActiveViewIndex = 2;
            }
        }



        protected void lnkprint_Click(object sender, EventArgs e)
        {
            //RptpendingOrdPrint();
        }
        private void RptpendingOrdPrint()
        {
            //Hashtable hst = (Hashtable)Session["tbllogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();  //company name
            //string comadd = hst["comadd1"].ToString();  //address
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();

            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtFromDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtToDate.Text).ToString("dd-MMM-yyyy");
            //string rptdt = "Date: From: " + this.txtFromDate.Text.ToString() + " To: " + this.txtToDate.Text.ToString();

            //var list = (List<MFGOBJ.C_12_Inv.MatReturnwip.DaywiseMatisselist>)ViewState["listmatissue"];
            ////string store = list[0].actdesc;
            //LocalReport rpt1 = new LocalReport();
            //rpt1 = MFGRDLC.RptSetupClass1.GetLocalReport("RD_07_Inv.Rptdaywisematissue", list, null, null);
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("reprtdate", rptdt));
            ////rpt1.SetParameters(new ReportParameter("store", store));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "DAY WISE MATERIALS ISSUE"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetRequisitionList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtFdate = this.txtFromDate.Text.ToString();
            string txttdate = this.txtToDate.Text.ToString();

            DataSet ds1 = InvData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETINCOMPLETEREQLIST", txtFdate, txttdate, "", " ", "", "", "");

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.reqadjstmntlist>();


            ViewState["reqlist"] = list;
            if (list == null)
            {
                return;
            }
            this.Data_Bind();
        }
        private void GetAdjustProduction()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string txtFdate = this.txtFromDate.Text.ToString();
            //string txttdate = this.txtToDate.Text.ToString();

            //DataSet ds1 = InvData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_PROCESS", "GETADJUSTEDPRODUCTION", txtFdate, txttdate, "", " ", "", "", "");

            //var list = ds1.Tables[0].DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.PendingProdutionList>();


            //ViewState["Proreqlist"] = list;
            //if (list == null)
            //{
            //    return;
            //}
            //this.Data_Bind();
        }
        private void GetProductionManually()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string txtFdate = this.txtFromDate.Text.ToString();
            //string txttdate = this.txtToDate.Text.ToString();

            //DataSet ds1 = InvData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODMANLIST", txtFdate, txttdate, "", " ", "", "", "");

            //var list = ds1.Tables[0].DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.EclassProdManList>();


            //ViewState["ProdManlist"] = list;
            //if (list == null)
            //{
            //    return;
            //}
            //this.Data_Bind();
        }
        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "ReqAdjst")
            {

                var list = (List<SPEENTITY.C_10_Procur.EClassPur.reqadjstmntlist>)ViewState["reqlist"];
                this.gvreqlist.DataSource = list;
                this.gvreqlist.DataBind();
                if (list.Count == 0)
                    return;
                Session["Report1"] = gvreqlist;
                ((HyperLink)this.gvreqlist.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            else if (type == "ProAdjst")
            {

                //var list1 = (List<MFGOBJ.C_13_ProdMon.BO_Production.PendingProdutionList>)ViewState["Proreqlist"];
                //this.gvproadjst.DataSource = list1;
                //this.gvproadjst.DataBind();
                //if (list1.Count == 0)
                //    return;
                //Session["Report1"] = gvproadjst;
                //((HyperLink)this.gvproadjst.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
            else
            {
                //var list2 = (List<MFGOBJ.C_13_ProdMon.BO_Production.EclassProdManList>)ViewState["ProdManlist"];
                //this.gvprodman.DataSource = list2;
                //this.gvprodman.DataBind();
                //if (list2.Count == 0)
                //    return;
                //Session["Report1"] = gvprodman;
                //((HyperLink)this.gvprodman.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "ReqAdjst")
            {
                this.GetRequisitionList();
            }
            else if (type == "ProAdjst")
            {
                this.GetAdjustProduction();
            }
            else
            {
                this.GetProductionManually();
            }


        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvreqlist.HeaderRow.FindControl("chkall")).Checked)
            {
                for (i = 0; i < this.gvreqlist.Rows.Count; i++)
                {

                    ((CheckBox)this.gvreqlist.Rows[i].FindControl("chkack")).Checked = true;
                }
            }
            else
            {
                for (i = 0; i < this.gvreqlist.Rows.Count; i++)
                {

                    if (((CheckBox)this.gvreqlist.Rows[i].FindControl("chkack")).Enabled == true)
                    {
                        ((CheckBox)this.gvreqlist.Rows[i].FindControl("chkack")).Checked = false;
                    }
                }
            }
        }
        protected void chkallpro_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvproadjst.HeaderRow.FindControl("chkallpro")).Checked)
            {
                for (i = 0; i < this.gvproadjst.Rows.Count; i++)
                {

                    ((CheckBox)this.gvproadjst.Rows[i].FindControl("chkack")).Checked = true;
                }
            }
            else
            {
                for (i = 0; i < this.gvproadjst.Rows.Count; i++)
                {

                    if (((CheckBox)this.gvproadjst.Rows[i].FindControl("chkack")).Enabled == true)
                    {
                        ((CheckBox)this.gvproadjst.Rows[i].FindControl("chkack")).Checked = false;
                    }
                }
            }
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString() == "ReqAdjst")
            {
                this.updateReqAdjst();
            }
            else if (this.Request.QueryString["Type"].ToString() == "ProAdjst")
            {
                this.updateProAdjst();
            }



        }
        private void updateReqAdjst()
        {
            string comcod = this.GetCompCode();
            for (int i = 0; i < this.gvreqlist.Rows.Count; i++)
            {
                string reqno = ((Label)this.gvreqlist.Rows[i].FindControl("lgreqno")).Text;
                string chkack = (((CheckBox)this.gvreqlist.Rows[i].FindControl("chkack")).Checked) ? "True" : "False";

                if (chkack == "True")
                {
                    bool result = InvData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "UPDATEREQADJUSTMENT", reqno,
                            "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (result == true)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                        ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                    }

                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    }
                }

            }
            this.GetRequisitionList();
        }

        private void updateProAdjst()
        {
            string comcod = this.GetCompCode();
            bool result = false;
            for (int i = 0; i < this.gvproadjst.Rows.Count; i++)
            {
                string pbno = ((Label)this.gvproadjst.Rows[i].FindControl("lgpbno")).Text;
                string batchcode = ((Label)this.gvproadjst.Rows[i].FindControl("lgcbatchcode")).Text;
                string chkack = (((CheckBox)this.gvproadjst.Rows[i].FindControl("chkack")).Checked) ? "True" : "False";

                if (chkack == "False")
                {
                    result = InvData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_PROCESS", "UPDATEPRODUCTIONAJDST", pbno, batchcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

                }

            }
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Revarse Successfully  ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
            }
            this.GetAdjustProduction();
        }









        protected void gvprodman_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink Lbtn = (HyperLink)e.Row.FindControl("LbtnApp");
                string status = ((Label)e.Row.FindControl("LblApstats")).Text.ToString();
                string mgrrno = ((Label)e.Row.FindControl("lvgrrno")).Text.ToString();
                Session["mgrrno"] = mgrrno;
                if (status == "False")
                {
                    Lbtn.NavigateUrl = "~/F_13_ProdMon/ProductionManually.aspx?Type=Approve&genno=" + mgrrno;
                    Lbtn.Target = "blank";
                }
                else
                {
                    Lbtn.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    Lbtn.CssClass = "btn btn-xs btn-danger";
                    Lbtn.ToolTip = "Approved";
                }




            }
        }

        //private List<MFGOBJ.C_13_ProdMon.BO_Production.EclassMttLst> HiddenSameData(List<MFGOBJ.C_13_ProdMon.BO_Production.EclassMttLst> lst)
        //{
        //    if (lst.Count == 0)
        //        return lst;

        //    int i = 0;
        //    string procode = lst[0].actdesc;

        //    //  List<MFGOBJ.C_22_Sal.EClassComProposal.EClassSalProInfo> lst2 = new List<MFGOBJ.C_22_Sal.EClassComProposal.EClassSalProInfo>();
        //    foreach (MFGOBJ.C_13_ProdMon.BO_Production.EclassMttLst lst1 in lst)
        //    {



        //        if (i == 0)
        //        {
        //            i++;
        //            continue;
        //        }
        //        else if (lst1.actdesc == procode)
        //        {
        //            lst[i].actdesc = "";


        //        }
        //        else
        //        {

        //            if (lst1.actdesc == procode)
        //                lst[i].actdesc = "";

        //            procode = lst1.actdesc;
        //        }




        //        i++;



        //    }
        //    return lst;

        //}


        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;
            //string comcod = this.GetCompCode();
            //Hashtable hst = (Hashtable)Session["tbllogin"];
            //string comnam = hst["comnam"].ToString();  //company name
            //string comadd = hst["comadd1"].ToString();  //address
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();

            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string mgrrno = ((Label)this.gvprodman.Rows[index].FindControl("lvgrrno")).Text.ToString();
            //DataSet mgrrdata = InvData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODMINFO", mgrrno, "", "", "", "");

            //var list2 = mgrrdata.Tables[2].DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.EclassMttLst>();
            //var list = mgrrdata.Tables[0].DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.EClassSalProInfo>();

            //string batch = list[0].batchdesc;

            //var list1 = mgrrdata.Tables[1].DataTableToList<MFGOBJ.C_13_ProdMon.BO_Production.EclassProdManList>();

            //string Proposal = list1[0].mgrrno;
            //string date = list1[0].mgrrdate.ToString("dd-MMM-yyyy");
            //string refno = list1[0].refno;
            //string supcenter = list1[0].stordesc;
            //string suplname = list1[0].suplname;
            //string remarks = list1[0].remarks;
            //var list2N = HiddenSameData(list2);
            //LocalReport rpt1 = new LocalReport();
            //rpt1 = MFGRDLC.RptSetupClass1.GetLocalReport("RD_11_Pro.RptManualProdPur", list, HiddenSameData(list2), null);


            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("batch", batch));
            //rpt1.SetParameters(new ReportParameter("date", date));
            //rpt1.SetParameters(new ReportParameter("refno", refno));
            //rpt1.SetParameters(new ReportParameter("remarks", "Remarks: " + remarks));
            //rpt1.SetParameters(new ReportParameter("supcenter", supcenter));
            //rpt1.SetParameters(new ReportParameter("suplname", suplname));
            //rpt1.SetParameters(new ReportParameter("Proposal", Proposal));


            //rpt1.SetParameters(new ReportParameter("RptTitle", "Manual Product Purchase Report"));

            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //Session["Report1"] = rpt1;

            //ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRdLc('PDF');", true);
        }

    }
}