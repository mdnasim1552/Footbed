using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SPEWEB.F_27_Fxt
{
    public partial class FxtAssetRegister : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                // Session.Remove("Unit");
                //string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Fixed Asset Information";

                //this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetDeparment();
                this.Show();
            }

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetDeparment()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETASSETCAT", "", "", "", "", "", "", "", "", "");

            ds1.Tables[0].Rows.Add(comcod, "000000000000", "All Type");

            this.ddlProjectName.DataTextField = "sirdesc";
            this.ddlProjectName.DataValueField = "sircode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = "000000000000";

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            this.Show();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            //this.GetProjectName();
        }

        private void Show()
        {
            Session.Remove("tblcost");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchmat = "%" + this.txtSrchMat.Text.Trim() + "%";
            string ddlprojact = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "21%" : this.ddlProjectName.SelectedValue.ToString() ;
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETSHOWASSET", srchmat, ddlprojact, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvFixAsset.DataSource = null;
                this.gvFixAsset.DataBind();
                return;
            }
            this.gvFixAsset.DataSource = ds1.Tables[0];
            this.gvFixAsset.DataBind();
        }
        protected void gvFixAsset_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkqty");
            string mCOMCOD = comcod;
            ///string mRsircode = ((Label)e.Row.FindControl("lblcode")).Text;
            string qty = ((HyperLink)e.Row.FindControl("hlnkqty")).Text;
            string mdesc = ((Label)e.Row.FindControl("lbldesc")).Text;
            string mspdesc = ((Label)e.Row.FindControl("lblspecification")).Text;

            //string pactcode = this.ddlProjectName.SelectedValue.ToString();


            // //string mTRNDAT1 = this.txtDatefrom.Text;
            // //string mTRNDAT2 = this.txtDateto.Text;
            // //------------------------------//////
            ////string qty = (Label)e.Row.FindControl("lblgvcode");
            // //HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");

            string mRsircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString().Trim();
            string spcfcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString().Trim();

            //hlink1.Font.Bold = true;
            hlink1.Font.Bold = true;
            hlink1.Style.Add("text-align", "right");

            hlink1.NavigateUrl = "LinkFxtAssetDetails.aspx?rsircode=" + mRsircode + "&Comcod=" + comcod + "&spcfcod=" + spcfcod + "&Qty=" + qty + "&rsirdesc=" + mdesc + " [ " + mspdesc + " ]";

            //if (ASTUtility.Right(code, 4) == "0000")
            //{
            //    actcode.Font.Bold = true;
            //    actdesc.Font.Bold = true;
            //    //actdesc.Style.Add("text-align", "right");

            //}
            /////---------------------------------//// 

            //if (ASTUtility.Left(mACTCODE, 1) == "4")
            //{
            //    hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            //}
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);
        }
    }
}