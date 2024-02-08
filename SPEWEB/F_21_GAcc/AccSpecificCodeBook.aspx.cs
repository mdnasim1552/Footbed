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

namespace SPEWEB.F_21_GAcc
{
    public partial class AccSpecificCodeBook : System.Web.UI.Page
    {
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            this.ConfirmMessage.Text = "";
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {


                string comcod = this.GetComeCode();
                string sircode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();

                string sircode = "";
                bool updateallow = true;


                if (sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
                }
                else
                {
                    this.ConfirmMessage.Text = "Invalid code!";
                    updateallow = false;
                }


                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string psircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();

                DataTable tbl1 = (DataTable)Session["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();


                if (tempddl2 == "4" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) != psircode1.Substring(2, 3))
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "7" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "9" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {

                    if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3) || sircode1.Substring(3, 3) == "000")
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }

                }
                else if (tempddl2 == "12" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) == "000" && sircode1.Substring(7, 2) != "00" || sircode1.Substring(7, 2) == "00" && sircode1.Substring(10, 3) != "000")
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }



                if (updateallow)
                {
                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;

                    this.grvacc.EditIndex = -1;

                    string s1 = sircode2.Substring(0, 2);
                    string s2 = sircode;
                    string s3 = Desc;

                    bool result = false;
                    //'01','010200000000','aasssss'
                    result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, "", "", "", "", "", "", "", "", "", "", "", "");
                    this.ShowInformation();
                    //here update information is needed for update information
                    //tempddl1 = "01";
                    //tempddl2 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                    //DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTINFO", tempddl1,
                    //       tempddl2, "", "", "", "", "", "", "");
                    //Session["storedata"] = ds1.Tables[0];
                    //this.grvacc_DataBind();

                    if (result)
                    {
                        this.ConfirmMessage.Text = "Update Successfully";

                        if (ConstantInfo.LogStatus == true)
                        {
                            string eventtype = "Specification CodeBook";
                            string eventdesc = "Update CodeBook";
                            string eventdesc2 = sircode;
                            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                        }
                    }
                    else
                    {
                        this.ConfirmMessage.Text = "Parent Code Not Found!!!";
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblprintstk.Text = "Error:" + ex.Message;
            }
        }

        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
            ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
            ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = true;
            ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;
        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc.PageIndex = ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }

        protected void lnkcancel_Click(object sender, EventArgs e)
        {



        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    this.ddlCodeBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.ShowInformation();
                }


                else
                {

                    this.lnkok.Text = "Ok";

                    this.lbalterofddl.Visible = false;
                    this.ddlCodeBookSegment.Visible = true;
                    this.ibtnSrch.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Information not found!!!!";
            }
        }

        private void ShowInformation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.lbalterofddl.Text = "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
            string dd1value = "01";
            string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
            string txtSearch = this.txtsrch.Text.Trim() + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTINFO", dd1value,
                            dd2value, txtSearch, "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTSPECIFICATIONCODE", "", "");
        //    ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptSpecification();
        //    TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
        //    txtCompany.Text = comnam;
        //    //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
        //    //txtadress.Text =comadd;
        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Specification CodeBook";
        //        string eventdesc = "Print CodeBook";
        //        string eventdesc2 = "";
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }

        //    rptstk.SetDataSource(ds1.Tables[0]);
        //    Session["Report1"] = rptstk;
        //    this.lblprintstk.Text = @"<script>window.open('RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ibtnSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowInformation();
        }
    }
}