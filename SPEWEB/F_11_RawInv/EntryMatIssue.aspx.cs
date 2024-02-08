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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_11_RawInv
{
    public partial class EntryMatIssue : System.Web.UI.Page
    {
        ProcessAccess MatIssue = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetOrderNo();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS ISSUE INFORMATION";

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetOrderNo()
        {
            string comcod = GetComCode();
            string txtsrch = this.txtOrdsrch.Text.Trim() + "%";
            DataSet ds1 = MatIssue.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETORDNO", txtsrch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrder.DataTextField = "actdesc";
            this.ddlOrder.DataValueField = "actcode";
            this.ddlOrder.DataSource = ds1.Tables[0];
            this.ddlOrder.DataBind();
        }
        private void GetResList()
        {
            Session.Remove("ResList");
            string comcod = GetComCode();
            string orderno = this.ddlOrder.SelectedValue.ToString();
            string txtsrch = this.txtRessrch.Text.Trim() + "%";
            DataSet ds2 = MatIssue.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETRESLIST", orderno, txtsrch, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.ddlResList.DataTextField = "itmdesc";
            this.ddlResList.DataValueField = "itmcode";
            Session["ResList"] = ds2.Tables[0];
            this.ddlResList.DataSource = ds2.Tables[0];
            this.ddlResList.DataBind();

        }

        protected void lbtnPreIsue_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = MatIssue.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "PREISSUENO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;

            this.ddlPreIssueNo.DataTextField = "isueno1";
            this.ddlPreIssueNo.DataValueField = "isueno";
            this.ddlPreIssueNo.DataSource = ds3.Tables[0];
            this.ddlPreIssueNo.DataBind();
        }
        protected void imgbtnFindOrd_Click(object sender, EventArgs e)
        {
            this.GetOrderNo();
        }
        protected void imgbtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetResList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblOrder.Text = this.ddlOrder.SelectedItem.Text.Substring(14);
                this.PnlReslist.Visible = true;
                this.ddlPreIssueNo.Visible = false;
                this.lbtnPreIsue.Visible = false;
                this.ddlOrder.Visible = false;
                this.lblOrder.Visible = true;
                this.ShowMatIssue();


            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.lblmsg.Text = "";
                this.lblOrder.Text = "";
                this.ddlPreIssueNo.Items.Clear();
                this.ddlResList.Items.Clear();
                this.ddlPreIssueNo.Visible = true;
                this.lbtnPreIsue.Visible = true;
                this.ddlOrder.Visible = true;
                this.lblOrder.Visible = false;
                this.PnlReslist.Visible = false;
                this.gvMat.DataSource = null;
                this.gvMat.DataBind();


            }

        }
        private void ShowMatIssue()
        {
            Session.Remove("MatIssue");
            string comcod = GetComCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string issueno = "NEWISSUENO";
            if (this.ddlPreIssueNo.Items.Count > 0)
                issueno = this.ddlPreIssueNo.SelectedValue.ToString();
            DataSet ds1 = MatIssue.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "SHOWISSUEINFO", issueno, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            Session["MatIssue"] = ds1.Tables[0];


            if (issueno == "NEWISSUENO")
            {
                ds1 = MatIssue.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETISSUENO", date, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.txtIssueno.Text = ds1.Tables[0].Rows[0]["maxisuno1"].ToString();

                }
                return;
            }


            this.ddlOrder.SelectedValue = ds1.Tables[0].Rows[0]["ordrno"].ToString();
            this.lblOrder.Text = this.ddlOrder.SelectedItem.Text.Substring(14);
            this.txtIssueno.Text = this.ddlPreIssueNo.SelectedValue.ToString().Substring(0, 3) + this.ddlPreIssueNo.SelectedValue.ToString().Substring(7, 2) + '-' + ASTUtility.Right(this.ddlPreIssueNo.SelectedValue.ToString(), 5);
            this.txtDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["isuedate"]).ToString("dd-MMM-yyyy");
            this.LoadGrid();

        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["MatIssue"];
            this.gvMat.DataSource = dt;
            this.gvMat.DataBind();
            this.FooterCalculation();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["MatIssue"];
            ((Label)this.gvMat.FooterRow.FindControl("lgvfqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                0 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }



        protected void gvMat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_Update();
            this.gvMat.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            this.Session_Update();
            string itmcode = this.ddlResList.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["MatIssue"];
            DataRow[] dr1 = dt.Select("itmcode='" + itmcode + "'");
            if (dr1.Length == 0)
            {
                DataRow dr2 = dt.NewRow();
                dr2["itmcode"] = this.ddlResList.SelectedValue.ToString();
                dr2["itmdesc"] = this.ddlResList.SelectedItem.ToString();
                dr2["unit"] = ((DataTable)Session["ResList"]).Select("itmcode='" + itmcode + "'")[0]["unit"];
                dr2["balqty"] = ((DataTable)Session["ResList"]).Select("itmcode='" + itmcode + "'")[0]["balqty"];
                dr2["qty"] = 0;
                dt.Rows.Add(dr2);
            }

            Session["MatIssue"] = dt;
            this.LoadGrid();

        }

        private void Session_Update()
        {
            DataTable dt = (DataTable)Session["MatIssue"];
            int rowindex;
            for (int i = 0; i < this.gvMat.Rows.Count; i++)
            {

                string unit = ((Label)this.gvMat.Rows[i].FindControl("lblgvUnit")).Text;
                string qty = Convert.ToDouble("0" + ((TextBox)this.gvMat.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();

                rowindex = this.gvMat.PageIndex * this.gvMat.PageSize + i;
                dt.Rows[rowindex]["unit"] = unit;
                dt.Rows[rowindex]["qty"] = qty;
            }
            Session["MatIssue"] = dt;
        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            this.Session_Update();
            string comcod = GetComCode();
            string Orderno = this.ddlOrder.SelectedValue.ToString();
            string Issueno = this.txtIssueno.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtDate.Text.Trim()), 4) + this.txtIssueno.Text.Trim().Substring(3, 2) + ASTUtility.Right(this.txtIssueno.Text.Trim(), 5);
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["MatIssue"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Itmcode = dt.Rows[i]["itmcode"].ToString();
                double balqty = Convert.ToDouble(dt.Rows[i]["balqty"]);
                double qty = Convert.ToDouble(dt.Rows[i]["Qty"]);
                if (qty > 0 & qty <= balqty)
                {

                    bool result = MatIssue.UpdateTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "INORUPDATEISSUE", Issueno, Orderno, Itmcode, date, qty.ToString(), "",
                    "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        this.lblmsg.Text = "Updated Successfully";
                    }
                    else
                    {
                        this.lblmsg.Text = "Updated Not Successfully";
                    }
                }
                else
                {
                    this.lblmsg.Text = "Balance Qty not Available";
                }


            }

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_Update();
            this.LoadGrid();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["MatIssue"];
            ReportDocument rptMat = new RMGiRPT.R_15_Pro.RptMaterialIsue();
            TextObject rpttxtcname = rptMat.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            rpttxtcname.Text = comnam;
            TextObject rpttxtOrderDesc = rptMat.ReportDefinition.ReportObjects["txtOrderDesc"] as TextObject;
            rpttxtOrderDesc.Text = "Order No: " + this.ddlOrder.SelectedItem.Text.Substring(14);
            TextObject rpttxtisueno = rptMat.ReportDefinition.ReportObjects["txtisueno"] as TextObject;
            rpttxtisueno.Text = "Issue No: " + this.txtIssueno.Text;
            TextObject rpttxtDate = rptMat.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtDate.Text = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptMat.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptMat.SetDataSource(dt);
            Session["Report1"] = rptMat;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}