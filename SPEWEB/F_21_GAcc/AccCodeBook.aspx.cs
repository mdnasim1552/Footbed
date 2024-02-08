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

namespace SPEWEB.F_21_GAcc
{
    public partial class AccCodeBook : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        CommonHelperClass helpcls = new CommonHelperClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Account Code";
                if (this.ddlCodeBook.Items.Count == 0)
                {
                    this.Load_CodeBooList();
                    this.GetResCodeleb2();
                    this.SelectResCodeLeb2();
                }
                    

              
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
                helpcls.GetSisterConcernInf();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

        }

        protected void Load_CodeBooList()
        {
            
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTCODE", userid,
                                "", "", "", "", "", "", "", "");
                Session["LoadDataForDropDownList"] = dsone.Tables[0];
                DataTable dt1 = (DataTable)Session["LoadDataForDropDownList"];
                if (dt1.Rows.Count == 0)
                {
                    dt1.Rows.Add("----Have No Code Permission Please Contact Sys Admin----", "XXXXXXXXXXXX");

                }
                this.ddlCodeBook.DataSource = dt1;
                this.ddlCodeBook.DataTextField = "actcode";
                this.ddlCodeBook.DataValueField = "actcode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        //private void LoadGrid()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string dd1value = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
        //    string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
        //    tempddl1 = dd1value;
        //    tempddl2 = dd2value;

        //    DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", "dd1value",
        //                    "dd2value", "", "", "", "", "", "", "");
        //    if (ds1.Tables[0].Rows.Count == 0)
        //    {
        //        this.lnknewentry.Visible = true; 
        //    }
        //    Session["storedata"] = ds1.Tables[0];
        //    this.grvacc.EditIndex = -1; 
        //    this.grvacc_DataBind();
        //}
        protected void ibtnSrchProject_Click(object sender, ImageClickEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[rowindex].FindControl("ddlProName");
            string SearchProject = "%" + ((TextBox)grvacc.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMSTNO", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "mstno1";
            ddl2.DataValueField = "mstno";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            //ddl2.SelectedValue = actcode;

        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
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

            string actcode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim();
            string actcode2 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim();


            string actcode = actcode1.Substring(0, 2) + actcode2.Substring(0, 2) + actcode2.Substring(3, 4) + actcode2.Substring(7);
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;

          
        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

             
                return;
            }
            //try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string userid = hst["usrid"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                string actcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string actcode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string actcode = "";
                bool updateallow = true;
                bool c = actcode1.Contains(" ");
                if (actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    actcode = actcode2.Substring(0, 2) + actcode1.Substring(0, 2) + actcode1.Substring(3, 4) + actcode1.Substring(8, 4);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid code!');", true);
                    updateallow = false;
                }
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtgvlevel = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridlevel")).Text.Trim();
                string typeCode = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvTypeCode")).Text.Trim();
                string TypeDesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvTypeDesc")).Text.Trim();
                string pactcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                //string wodesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvShortDesc")).Text.Trim();

                DataTable tbl1 = (DataTable)Session["storedata"];
                string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                if (dd2value == "4" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) != pactcode1.Substring(2, 4))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);
                        updateallow = false;
                    }
                    else if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                }
                else if (dd2value == "8" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                }
                else if (dd2value == "12" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) == "0000" && actcode1.Substring(8, 4) != "0000")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                }


                //string mSTCode = "";

                if (updateallow)
                {


                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                    tbl1.Rows[Index]["ACTCODE"] = actcode;
                    tbl1.Rows[Index]["ACTDESC"] = Desc;
                    tbl1.Rows[Index]["ACTELEV"] = txtgvlevel;
                    tbl1.Rows[Index]["ACTTYPE"] = typeCode;
                    tbl1.Rows[Index]["ACTTDESC"] = TypeDesc;
                    //tbl1.Rows[Index]["WODESC"] = wodesc;
                    this.grvacc.EditIndex = -1;

                    bool result = false;


                    string ddlcod = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 1);



                    result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTUPDATE", actcode2.Substring(0, 2), actcode, Desc, txtgvlevel, typeCode, TypeDesc, "", userid, "", "",
                                           "", "", "", "", "");

                    this.ShowInformation();
                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
                    }

                    Common.LogStatus("Acounts Code", "Code Create/Update", "code No: ", actcode + " - " + Desc);
                }
            }

        }

        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
            if (tbl1.Rows.Count == 0)
                return;
            Session["Report1"] = grvacc;
            ((HyperLink)this.grvacc.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc.PageIndex = ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Visible == true)
            {
                this.lnkok_Click(null, null);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CodeDesc = this.ddlCodeBook.SelectedItem.ToString().Trim().Substring(3)
                        + " " + "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";

            DataTable ddup = (DataTable)Session["storedata"];
            ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccountcode2();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtCodeBookDesc = rptstk.ReportDefinition.ReportObjects["CodeBookDesc"] as TextObject;
            txtCodeBookDesc.Text = CodeDesc;
            rptstk.SetDataSource(ddup);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            Common.LogStatus("Acounts Code", "Code Print", "code No: ", CodeDesc);
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.lnkok.Text == "Ok")
                {
                    
                    this.lnkok.Text = "New";
                    this.ddlCodeBook.Enabled = false;
                    this.ddlCodeBookSegment.Enabled = false;
                    this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowInformation();

                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.ddlCodeBook.Enabled = true;
                    this.ddlCodeBookSegment.Enabled = true;
                    this.ibtnSrch.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Information not found!!!!');", true);
            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
        private void ShowInformation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string dd1value = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
            dd1value = (dd1value == "00" ? "" : dd1value);
            string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
            string srchoption = this.txtsrch.Text.Trim() + "%";
            string catgrory = ((this.ddlcatagory.SelectedValue.ToString() == "0000") ? "" : this.ddlcatagory.SelectedValue.ToString()) + "%";

            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", dd1value, dd2value, srchoption, catgrory, "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();
        }
        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            this.lnkok.Visible = true;
            this.ddlCodeBook.Enabled = true;
            this.ddlCodeBookSegment.Enabled = true;
            this.grvacc.DataSource = null;
            this.grvacc.DataBind();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");
                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                int additem = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "additem"));

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 4) == "0000" && ASTUtility.Right(Code, 8) != "00000000")
                {
                    e.Row.Attributes["style"] = "color:#3399FF;";//d3d3d3
                }
                else if (ASTUtility.Right(Code, 10) == "0000000000")
                {
                    e.Row.Attributes["style"] = "color:#34A853;";
                }
                else if (ASTUtility.Right(Code, 8) == "00000000" && ASTUtility.Right(Code, 10) != "0000000000")
                {
                    e.Row.Attributes["style"] = "color:#EA4335;";
                }
                if (additem == 1)
                {

                    lbtnAdd.Visible = true;


                }
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string actcode = ((DataTable)Session["storedata"]).Rows[RowIndex]["actcode"].ToString();
            string pactcode = ((DataTable)Session["storedata"]).Rows[RowIndex]["actcode"].ToString();
            this.lblactcode.Text = actcode;
            this.txtacountcode.Text = actcode.Substring(0, 2) + "-" + actcode.Substring(2, 2) + "-" + actcode.Substring(4, 4) + "-" + ASTUtility.Right(actcode, 4);

            this.Chboxchild.Checked = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");
            this.chkbod.Visible = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");

            this.lblchild.Visible = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");


            this.txtaccounthead.Text = "";
            this.txtlevel.Text = "";
            this.txttype.Text = "";
            this.txtshort.Text = "";



            // this.GetDetailsInfo(rsircode);
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }
        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string iactcode = this.txtacountcode.Text.Trim();
                iactcode = iactcode.Replace("-", "");
                string actcode = (this.Chboxchild.Checked) ? ((ASTUtility.Right(iactcode, 8) == "00000000") ? (ASTUtility.Left(this.lblactcode.Text, 4) + "0001" + ASTUtility.Right(iactcode, 4))
                    : ASTUtility.Left(this.lblactcode.Text, 8) + "0001") : iactcode;
                //string actcode2 = actcode.Substring(0, 2);
                string Desc = this.txtaccounthead.Text.Trim();
                string txtgvlevel = this.txtlevel.Text.Trim();
                string typeCode = this.txttype.Text.Trim();
                string TypeDesc = this.txtshort.Text;
                 

                if (Desc.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Account Head is not empty');", true);

                    return;
                }
                else
                {
                    bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDACCOUNTCODE", actcode, Desc, txtgvlevel, typeCode, TypeDesc, userid, "", "", "",
                      "", "", "", "", "", "");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"].ToString() + "');", true);
                        return;

                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                    this.ShowInformation();
                    this.Chboxchild.Checked = false;

                }




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }

        }

        protected void ddlCodeBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectResCodeLeb2();
        }
        private void GetResCodeleb2()
        {
            ViewState.Remove("tblactleb2");
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETACTCODELEVEL2", "", "", userid, "", "", "", "", "", "");
            ViewState["tblactleb2"] = ds1.Tables[0];
            ds1.Dispose();

        }
        private void SelectResCodeLeb2()
        {
            DataTable dt = ((DataTable)ViewState["tblactleb2"]).Copy();
            if (this.ddlCodeBook.SelectedValue.ToString().Length == 0)
                return;

            string mrescode = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 2);
            EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
                                                     where (r.Field<string>("actcode").Substring(0, 2) == mrescode || r.Field<string>("actcode").Substring(0, 2) == "00")
                                                     select r);
            dt = item.AsDataView().ToTable();

            this.ddlcatagory.DataTextField = "actdesc";
            this.ddlcatagory.DataValueField = "actcode";
            this.ddlcatagory.DataSource = dt;
            this.ddlcatagory.DataBind();
        }
    }
}