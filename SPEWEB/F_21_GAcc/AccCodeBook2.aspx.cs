﻿using System;
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
    public partial class AccCodeBook2 : System.Web.UI.Page
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
                    this.Load_CodeBooList();


                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
                ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
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
            this.ConfirmMessage.Text = "";
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTCODE", "",
                                "", "", "", "", "", "", "", "");
                Session["LoadDataForDropDownList"] = dsone.Tables[0];
                DataTable dt1 = (DataTable)Session["LoadDataForDropDownList"];
                this.ddlCodeBook.DataSource = dt1;
                this.ddlCodeBook.DataTextField = "actcode";
                this.ddlCodeBook.DataValueField = "actcode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Error:" + ex.Message;
            }
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
            string actcode2 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim();


            string actcode = actcode1.Substring(0, 2) + actcode2.Substring(0, 2) + actcode2.Substring(3, 4) + actcode2.Substring(7);
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;

            //string mstno = ((DataTable)Session["storedata"]).Rows[rowindex]["mstno"].ToString();


            ///DropDownList ddl2 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlProName");
            //Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Panel2");
            //if (ASTUtility.Left(this.ddlCodeBook.SelectedValue.ToString(), 2) == "16" && ASTUtility.Right(actcode, 3) != "000")
            //{
            //    ViewState["gindex"] = e.NewEditIndex; ;
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string SearchProject = "%" + ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
            //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMSTNO", SearchProject, "", "", "", "", "", "", "", "");
            //    ddl2.DataTextField = "mstno1";
            //    ddl2.DataValueField = "mstno";
            //    ddl2.DataSource = ds1;
            //    ddl2.DataBind();
            //    ddl2.SelectedValue = mstno; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
            //    pnl02.Visible = true;
            //    //ddl2.Visible = true;
            //}
            //else
            //{
            //    pnl02.Visible = false;
            //    ddl2.Items.Clear();
            //}
        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ConfirmMessage.Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.ConfirmMessage.Text = "You have no permission";
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

                string actcode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
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
                    this.ConfirmMessage.Text = "Invalid code!";
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
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (dd2value == "8" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (dd2value == "12" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) == "0000" && actcode1.Substring(8, 4) != "0000")
                    {
                        this.ConfirmMessage.Text = "Update Not Allowed";
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

                    //string tempddl3 = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
                    //tempddl3 = (tempddl3 == "00" ? "" : tempddl3);
                    //string tempddl4 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();


                    //DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", tempddl3,
                    //        tempddl4, "", "", "", "", "", "", "");
                    //Session["storedata"] = ds1.Tables[0];
                    //this.grvacc_DataBind();
                    this.ShowInformation();
                    if (result)
                    {
                        this.ConfirmMessage.Text = "Update Successfully";
                    }
                    else
                    {
                        this.ConfirmMessage.Text = "Update Failed";
                    }

                    Common.LogStatus("Acounts Code", "Code Create/Update", "code No: ", actcode + " - " + Desc);
                }
            }



            //catch (Exception ex)
            //{
            //   this.ConfirmMessage.Text = "Error:" + ex.Message;
            //}
        }

        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
            //if (ASTUtility.Left(this.ddlCodeBook.SelectedValue.ToString(), 2) == "16")
            //{
            //    this.grvacc.Columns[8].Visible = true;
            //}
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
                    this.ConfirmMessage.Visible = false;
                    this.lnkok.Text = "New";
                    this.ddlCodeBook.Visible = false;
                    this.ddlCodeBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.LblBookName1.Text = "Code Book:";
                    this.lbalterofddl.Text = this.ddlCodeBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.ShowInformation();

                }
                else
                {

                    this.lnkok.Text = "Ok";

                    this.ConfirmMessage.Visible = false;
                    this.ddlCodeBook.Visible = true;
                    this.ddlCodeBookSegment.Visible = true;
                    this.lbalterofddl.Visible = false;
                    this.lbalterofddl0.Visible = false;
                    this.ibtnSrch.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }
            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Information not found!!!!";
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
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", dd1value, dd2value, srchoption, "", "", "", "", "", "");
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
            this.LblBookName1.Text = "Select Code Book:";
            this.lbalterofddl.Visible = false;
            this.lbalterofddl0.Visible = false;
            this.ddlCodeBook.Visible = true;
            this.ddlCodeBookSegment.Visible = true;
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
                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 4) == "0000" && ASTUtility.Right(Code, 8) != "00000000")
                {
                    e.Row.Attributes["style"] = "background:#d3d3d3;";
                }
                else if (ASTUtility.Right(Code, 10) == "0000000000")
                {
                    e.Row.Attributes["style"] = "background:#14BCF5;";
                }
                else if (ASTUtility.Right(Code, 8) == "00000000" && ASTUtility.Right(Code, 10) != "0000000000")
                {
                    e.Row.Attributes["style"] = "background:#85ffff;";
                }
            }
        }
    }
}