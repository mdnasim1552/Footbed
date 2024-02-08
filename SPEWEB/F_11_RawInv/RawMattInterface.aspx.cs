using SPEENTITY;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_11_RawInv
{
    public partial class RawMattInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                ((Label)this.Master.FindControl("lblTitle")).Text = "Material Transfer Smartface";

                lbtnOk_Click(null, null);
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GoodsInfoRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "RPT_MATERIAL_TRANSFER_INTERFACE", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblReqStatus"] = ds1;
            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["reqqty"]).ToString("#,##0;(#,##0);") + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Requisition</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["reqaqty"]).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Req. Approval</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["gpqty"]).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Store Issue(G.Pass)</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["trnsqty"]).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Store Receive</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["trnsappqty"]).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>Matt Transfered</div></div></div>";

            // this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["reqqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Requisition" + "</span>";
           // this.RadioButtonList1.Items[1].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["reqaqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Req. Approval" + "</span>";
           // this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["gpqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Store Issue(G.Pass)" + "</span>";
           // this.RadioButtonList1.Items[3].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["trnsqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Store Receive" + "</span>";
            //this.RadioButtonList1.Items[4].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[2].Rows[0]["trnsappqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Matt Transfered" + "</span>";
        }
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            //this.ImportInterFace();
            //this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GoodsInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void gvReqInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void gvReqrfap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnPrint");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                LinkButton hlink4 = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                string mtrdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtrdat")).ToString();
                hlink1.NavigateUrl = "~/F_11_RawInv/StoreMtTrnsReqEntry?Type=approve&genno=" + preqno+"&date=" + mtrdat; ;
                hlink2.NavigateUrl = "~/F_11_RawInv/Inv_Print?Type=MatReqIntrfce&genno=" + preqno + "&date=" + mtrdat;
                hlink3.NavigateUrl = "~/F_11_RawInv/StoreMtTrnsReqEntry?Type=Edit&genno=" + preqno + "&date=" + mtrdat;


            }
        }
        protected void gvReqGatePass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                hlink1.NavigateUrl = "~/F_11_RawInv/StoreMtReqTrnsGatePass?Type=Entry&genno=" + preqno;
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnPrint");
                string mtrdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtrdat")).ToString();
                hlink2.NavigateUrl = "~/F_11_RawInv/Inv_Print?Type=MatReqIntrfce&genno=" + preqno + "&date=" + mtrdat;
            }
        }
        protected void gvReqGatePassrfap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlinkGatePass = (HyperLink)e.Row.FindControl("hlnkGatPsPrnt");
                string getpno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "getpno")).ToString();
                hlink2.NavigateUrl = "~/F_11_RawInv/MaterialsTransfer?Type=Material&genno=" + getpno;
                hlinkGatePass.NavigateUrl = "~/F_11_RawInv/Inv_Print?Type=GatePass&genno=" + getpno;

            }
        }
        protected void gvProTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                Hashtable hst = (Hashtable)Session["tblLogin"];
              
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "tfpactcode")).ToString();
                string genno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "trnno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "mtrdat")).ToString("dd-MMM-yyyy");
                hlink2.NavigateUrl = "~/F_21_GAcc/AccTransfer?Type=Entry&genno="+ genno + "&comcod=" + comcod;

            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)ViewState["tblReqStatus"];
            
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataView dv;
            switch (value)
            {
                case "0":
                    //Status Requisition Status

                    this.pnlReqInfo.Visible = true;
                    this.pnlreqreadyfap.Visible = false;
                    this.PnlReqGPass.Visible = false;
                    this.PnlReqGPassrfap.Visible = false;
                    this.pnlProTrnas.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["style"] = "background:#5A5C59; display:block";
                    this.Data_Bind("gvReqInfo", ds.Tables[0]);
                    break;


                case "1":
                    // Requisition Ready for Approval

                    this.pnlReqInfo.Visible = false;
                    this.pnlreqreadyfap.Visible = true;
                    this.PnlReqGPass.Visible = false;
                    this.PnlReqGPassrfap.Visible = false;
                    this.pnlProTrnas.Visible = false;
                    dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = ("approved <>'OK'");
                    Session["tblReqStatusPen"] = dv;

                    this.RadioButtonList1.Items[1].Attributes["style"] = "background:#5A5C59; display:block";
                    this.Data_Bind("gvReqrfap", dv.ToTable());


                    break;
                case "2":
                    //Gate Pass
                    this.pnlReqInfo.Visible = false;
                    this.pnlreqreadyfap.Visible = false;
                    this.PnlReqGPass.Visible = true;
                    this.PnlReqGPassrfap.Visible = false;
                    this.pnlProTrnas.Visible = false;

                    dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = ("gatpbal<>0 and approved='Ok'");
                    Session["tblReqGet"] = dv;
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background:#5A5C59; display:block";
                    this.Data_Bind("gvReqGatePass", dv.ToTable());

                    break;

                case "3":
                    //Gate Pass Ready For Approval

                    this.pnlReqInfo.Visible = false;
                    this.pnlreqreadyfap.Visible = false;
                    this.PnlReqGPass.Visible = false;
                    this.PnlReqGPassrfap.Visible = true;
                    this.pnlProTrnas.Visible = false;
                    dv = ds.Tables[1].DefaultView;
                    dv.RowFilter = ("gatpqty>0 and trnbal>0 ");
                    Session["tblReqGet"] = dv;

                    this.RadioButtonList1.Items[3].Attributes["style"] = "background:#5A5C59; display:block";
                    this.Data_Bind("gvReqGatePassrfap", dv.ToTable());

                    break;

                case "4":
                    //Gate Pass

                    this.pnlReqInfo.Visible = false;
                    this.pnlreqreadyfap.Visible = false;
                    this.PnlReqGPass.Visible = false;
                    this.PnlReqGPassrfap.Visible = false;
                    this.pnlProTrnas.Visible = true;
                    dv = ds.Tables[1].DefaultView;
                    dv.RowFilter = ("trnno <> '00000000000000' and vounum = '00000000000000'");
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background:#5A5C59; display:block";
                    this.Data_Bind("gvProTransfer", dv.ToTable());


                    break;




            }
        }
        private void Data_Bind(string gv, DataTable dt)
        {


            switch (gv)
            {
                case "gvReqInfo":
                    this.gvReqInfo.DataSource = dt;
                    this.gvReqInfo.DataBind();
                    break;
                case "gvReqrfap":
                    this.gvReqrfap.DataSource = dt;
                    this.gvReqrfap.DataBind();
                    for (int i = 0; i < gvReqrfap.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvReqrfap.Rows[i].FindControl("lblgvreqno")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvReqrfap.Rows[i].FindControl("lnkbtndelete");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }

                    break;
                case "gvReqGatePass":
                    this.gvReqGatePass.DataSource = dt;
                    this.gvReqGatePass.DataBind();
                    for (int i = 0; i < gvReqGatePass.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvReqGatePass.Rows[i].FindControl("lblgvreqno")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvReqGatePass.Rows[i].FindControl("lnkbtnstrdelete");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }
                    break;

                case "gvReqGatePassrfap":
                    this.gvReqGatePassrfap.DataSource = dt;
                    this.gvReqGatePassrfap.DataBind();
                    for (int i = 0; i < gvReqGatePassrfap.Rows.Count; i++)
                    {
                        string gpno = ((Label)gvReqGatePassrfap.Rows[i].FindControl("lblgvreqno12")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvReqGatePassrfap.Rows[i].FindControl("lnkbtngpdelete");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = gpno;
                    }
                    break;

                case "gvProTransfer":

                    this.gvProTransfer.DataSource = dt;
                    this.gvProTransfer.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    break;

            }


            //this.FooterCalculation(gv, dt);




        }
        protected void lnkbtndelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Type = "ReqDelete";
            if (Reqno.Length == 0)
            {
                string tmsg = "Please select your item for Delete";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.txtFDate.Text;
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", Reqno, CurDate1,
                          "", "", "", "", "", "", "");
            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, "", Reqno);
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "RPT_MATERIAL_TRANSFER_INTERFACE_DEL", Reqno, Type, "", "", "");
            if (result == true)
            {
                string tmsg = "Delete Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CS Audit Delete Successfully');", true);
            }
            Common.LogStatus("Material Transfer Interface", "Req Delete", "DO No: ", Reqno);
        }

        protected void lnkbtnstrdelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = ((DataView)Session["tblReqGet"]).ToTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Type = "StoreDelete";
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "RPT_MATERIAL_TRANSFER_INTERFACE_DEL", Reqno, Type, "", "", "");

            Common.LogStatus("Material Transfer Interface", "Req Delete form Store", "MTR No: ", Reqno);
        }

        protected void lnkbtngpdelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = ((DataView)Session["tblReqGet"]).ToTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            string gpno = ASTUtility.Left(Code, 14).ToString();
            string Type = "GetpDelete";
            if (gpno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "RPT_MATERIAL_TRANSFER_INTERFACE_DEL", gpno, Type, "", "", "");

            Common.LogStatus("Material Transfer Interface", "Req Delete form Get pass", "GPN No: ", gpno);
        }

        protected void btnSetup_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = false;
            this.PnlWarehouseSetting.Visible = true;



        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = true;
            this.PnlWarehouseSetting.Visible = false;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = false;
            this.PnlWarehouseSetting.Visible = false;
        }
    }
}