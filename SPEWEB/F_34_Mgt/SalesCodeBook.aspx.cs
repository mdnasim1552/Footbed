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

namespace SPEWEB.F_34_Mgt
{
    public partial class SalesCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "All") ? "GENERAL CODE BOOK INFORMATION VIEW/EDIT" : (this.Request.QueryString["Type"].ToString().Trim() == "Procurement") ? "SUPPLIER INFORMATION FIELD VIEW/EDIT" : "CUSTOMER INFORMATION FIELD VIEW/EDIT";

            }
            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Load_CodeBooList()
        {

            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                string comcod = this.GetCompCode();
                string Type = this.Request.QueryString["Type"].ToString().Trim();

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_SALES_CODE", Type,
                                "", "", "", "", "", "", "", "");
                if (dsone == null)
                    return;

                this.ddlOthersBook.DataTextField = "gdesc";
                this.ddlOthersBook.DataValueField = "gcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

               
            }



        }


        //private void LoadGrid()
        //{
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = hst["comcod"].ToString();
        //    string comcod = this.GetCompCode();
        //    string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
        //    string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

        //    DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1,
        //                    tempddl2, "", "", "", "", "", "", "");
        //    if (ds1.Tables[0].Rows.Count == 0)
        //    {
        //        this.lnknewentry.Visible = true;

        //    }
        //    Session["storedata"] = ds1.Tables[0];
        //    this.grvacc_DataBind();

        //}


        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {


            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                this.grvacc.EditIndex = e.NewEditIndex;
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string symbol = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvsymbol")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string callType = (this.Request.QueryString["Type"].ToString() == "Sales") ? "INSERTUPCUSTINF"
                : (this.Request.QueryString["Type"].ToString() == "Procurement") ? "INSERTUPSUPGINF" : "INSERTUPL_SALE_CODE_INF";

            string color = "";


            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);

            if (tempddl1 == "30")
            {
                color = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlPProces")).SelectedValue;
            }
            else
            {
                color = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvColor")).Text.Trim();
            }

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", callType, tgcod,
                           gdesc, Gtype, symbol, color, "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Updated faild');", true);
            }
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales Code Book";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                string Code = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
                if (Code == "34")
                {
                    this.grvacc.Columns[8].Visible = true;
                }
                DataTable tbl1 = (DataTable)ViewState["storedata"];
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

                string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);

                if(tempddl1 == "30")
                {
                    grvacc.Columns[9].Visible = true;
                }
                else
                {
                    grvacc.Columns[9].Visible = false;
                }



            }
            catch (Exception ex)
            {
            }

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTOTHERACCOUNTCODEBook", "", tempddl2);
            //ReportDocument rptAccCode = new MFGRPT.R_15_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtadress = rptAccCode.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = "OTHER ACCOUNTS  CODE BOOK  REPORT";
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptAccCode.SetDataSource(ds1.Tables[0]);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sales Code Book";
            //    string eventdesc = "Print CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //Session["Report1"] = rptAccCode;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    ViewState.Remove("storedata");
                    this.lnkok.Text = "New";
                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    //this.ddlpagesize.Enabled = true;
                    string Code = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);

                    this.ShowInformation();
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Information Not Found!!');", true);

                    
                }
            }
            else
            {
                this.lnkok.Text = "Ok";
                this.ddlOthersBook.Enabled = true;
                this.ddlOthersBookSegment.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            string qtype = this.Request.QueryString["Type"].ToString();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_SALE_CODE_DETAILS", tempddl1,
                            tempddl2, qtype, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];

            if (tempddl1 == "30")
            {
                GetProcess();

                if (dt.Rows.Count > 0)
                {

                    dt.Columns.Add("newColor", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string process = dt.Rows[i]["color"].ToString();

                        DataTable dt1 = (DataTable)ViewState["ddlProcess"];

                        DataView dv1 = dt1.DefaultView;

                        dv1.RowFilter = "procode='" + process + "'";

                        DataTable dt2 = dv1.ToTable();

                        if(dt2.Rows.Count > 0)
                        {
                            dt.Rows[i]["newColor"] = dt.Rows[i]["color"];
                            dt.Rows[i]["color"] = dt2.Rows[0]["prodesc"].ToString();
                        }
                        
                    }

                }
            }

            ViewState["storedata"] = dt;
        }



        //protected void lnknewentry_Click(object sender, EventArgs e)
        //{
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = hst["comcod"].ToString();
        //    string comcod = this.GetCompCode();
        //    string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
        //    string sircode = tempddl1 + "0100000000";
        //    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", tempddl1, sircode, "", "", "", "", "0.000000", "", "", "",
        //                "", "", "", "", "");
        //    DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1, "12",
        //                    "", "", "", "", "", "", "");
        //    Session["storedata"] = ds1.Tables[0];


        //    this.grvacc.DataSource = (DataTable)Session["storedata"];
        //    this.grvacc.DataBind();
        //    //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
        //    this.lnknewentry.Visible = false;

        //}




        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            grvacc_DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["storedata"];
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
        }

        public void GetProcess()
        {
            string comcod = GetCompCode();

            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0].DefaultView.ToTable(true, "prodesc", "procode");

            if (ds1 == null)
                return;

            ViewState["ddlProcess"] = dt;
        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);

                if (tempddl1 == "30")
                {

                    DataTable dt = (DataTable)ViewState["ddlProcess"];

                    DataView dv = dt.DefaultView;

                    dv.RowFilter = "procode<>'800100101099'";

                    string gcode1 = ((Label)e.Row.FindControl("lblgrcode")).Text.Trim();

                    string gcode2 = ((TextBox)e.Row.FindControl("txtgrcode")).Text.Trim();

                    if (gcode2.Length == 5 && gcode2.Substring(2, 3) == "000" && gcode2.Substring(0, 5) != "00000" && gcode1.Substring(0, 2) == "30")
                    {
                        string comcod = GetCompCode();

                        DropDownList DdlPProcess = ((DropDownList)e.Row.FindControl("ddlPProces"));

                        DdlPProcess.DataTextField = "prodesc";
                        DdlPProcess.DataValueField = "procode";
                        DdlPProcess.DataSource = dv;
                        DdlPProcess.DataBind();

                        DdlPProcess.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "newColor")).ToString();

                        DdlPProcess.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}