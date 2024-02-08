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

namespace SPEWEB.F_05_ProShip
{
    public partial class GeneralCodeBook : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "All") ? "GENERAL CODE BOOK INFORMATION VIEW/EDIT" : "SUPPLIER INFORMATION FIELD VIEW/EDIT";

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

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETLCPLANCODE", Type,
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


     

        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {


            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();

            DropDownList grvaccdll = ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces"));

            string comcod = this.GetCompCode();

            string rescode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcod1")).Text.Trim();

            if (rescode1.Substring(0, 2) == "15" &&  ASTUtility.Right(rescode1,5) != "00000" && ASTUtility.Right(rescode1, 3) == "000")
            { 
                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                DataTable dt = ds1.Tables[0].Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("procode <>'800100101099'");
                dt = dv.ToTable();

                grvaccdll.DataTextField = "prodesc";
                grvaccdll.DataValueField = "procode";
                grvaccdll.DataSource = dt;
                grvaccdll.DataBind();
                ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces")).Visible = true;

            }

            else if (rescode1.Substring(0, 2) == "15" && ASTUtility.Right(rescode1, 5) != "00000" && ASTUtility.Right(rescode1, 3) != "000")
            {
                DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETLCPLANDETAILS", "21",
                            "5", "All", "", "", "", "", "", "");

                DataTable dt = ds1.Tables[0].Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("gcod <>'21000'");
                dt = dv.ToTable();


                grvaccdll.DataTextField = "gdesc";
                grvaccdll.DataValueField = "gcod";
                grvaccdll.DataSource = dt;
                grvaccdll.DataBind();
                ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces")).Visible = true;
            }


        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();

            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();



            string code = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            if (code.Length == 6)
            {
                gcode2 = code.Substring(0, 2) + code.Substring(3, 3);
            }

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();

            string tgcod = gcode1.Substring(0, 2) + gcode2;

            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string val1 = gcode1 == "12-" ? ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvVal1")).Text.Trim() : "0.00";
            string val2 = gcode1 == "12-" ? ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvVal2")).Text.Trim() : "0.00";
            string val3 = gcode1 == "12-" ? ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvVal3")).Text.Trim() : "0.00";
            string LINKCODE = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlPProces")).Text.Trim();


            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPLCPLANINF", tgcod,
                           gdesc, Gtype, val1, val2, val3, LINKCODE, "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);
              
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
              
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

                DataTable tbl1 = (DataTable)Session["storedata"];

                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

                if(Code == "12")
                {
                    grvacc.Columns[7].Visible = true;
                    grvacc.Columns[8].Visible = true;
                    grvacc.Columns[9].Visible = true;
                    grvacc.Columns[10].Visible = false;
                }
                else if (Code == "15")
                {
                    grvacc.Columns[7].Visible = false;
                    grvacc.Columns[8].Visible = false;
                    grvacc.Columns[9].Visible = false;
                    grvacc.Columns[10].Visible = true;
                }
                else
                {
                    grvacc.Columns[7].Visible = false;
                    grvacc.Columns[8].Visible = false;
                    grvacc.Columns[9].Visible = false;
                    grvacc.Columns[10].Visible = false;
                }

            }
            catch (Exception ex)
            {
            }

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            


        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lnkok.Text = "New";
                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    string Code = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);

                    this.ShowInformation();
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Information not found!!!!');", true);

                  
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

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETLCPLANDETAILS", tempddl1,
                            tempddl2, qtype, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                //this.lnknewentry.Visible = true;

            }
            Session["storedata"] = ds1.Tables[0];
        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");
            //    string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lbgrcod1")).ToString();
                
            //    if (Code == "")
            //        return;

            //    if (ASTUtility.Right(Code, 3) == "000" || ASTUtility.Right(Code, 5) == "00000")
            //    {
            //        ((DropDownList)e.Row.FindControl("ddlPProces")).Enabled = false;
            //    }
            //    if (ASTUtility.Left(Code, 2) == "15")
            //    {
            //        ((DropDownList)e.Row.FindControl("ddlPProces")).Visible = true;

            //    }
            //    else
            //    {
            //        ((DropDownList)e.Row.FindControl("ddlPProces")).Visible = false;

            //    }

            //}

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



    }
}