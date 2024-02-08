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
//using BDACCRPT;
using SPEENTITY;
namespace SPEWEB.F_34_Mgt
{
    public partial class AccCurCodeBook : System.Web.UI.Page
    {
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "CURRENCY CODE BOOK INFORMATION";
               
                this.ShowInformation();


            }
            
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        public List<SPEENTITY.C_34_Mgt.EntityClassCurrency> ShowCurrency()
        {
            List<SPEENTITY.C_34_Mgt.EntityClassCurrency> lst = new List<SPEENTITY.C_34_Mgt.EntityClassCurrency>();
            string comcod = this.GetCompCode();// ObjCommon.GetCompCode();

            SqlDataReader dr = da.GetSqlReader(comcod, "SP_ENTRY_CODEBOOK", "GETCURRENCY", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_34_Mgt.EntityClassCurrency details = new SPEENTITY.C_34_Mgt.EntityClassCurrency(dr["code"].ToString(), dr["codedesc"].ToString(), dr["curdesc"].ToString(), dr["cursymbol"].ToString(), dr["curword"].ToString(), dr["curstatus"].ToString());
                lst.Add(details);
            }

            return lst;
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
                string comcod = this.GetCompCode(); // this.GetComeCode();
                string code = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

                if (code.Length != 3)
                {
                    return;
                }
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string curdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvCurDesc")).Text.Trim();
                string cursymbol = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvSym")).Text.Trim();
                string curword = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvWord")).Text.Trim();
                string curstatus = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvnoneActive")).Text.Trim();

                this.grvacc.EditIndex = -1;
                bool result = false;
                result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INORUPCURRENCY", code, Desc, curdesc, cursymbol, curword, curstatus, "", "", "", "", "", "", "", "", "");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Parent Code Not Found!!!');", true);
                }
                this.ShowInformation();
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                var lst = (List<SPEENTITY.C_34_Mgt.EntityClassCurrency>)ViewState["storedata"];
                this.grvacc.DataSource = lst;
                this.grvacc.DataBind();
            }
            catch (Exception ex)
            {


            }

        }


        protected void lnkcancel_Click(object sender, EventArgs e)
        {

           

        }


        private void ShowInformation()
        {

            string comcod = this.GetCompCode();// this.GetComeCode();
            ViewState.Remove("storedata");
            var lst = new List<SPEENTITY.C_34_Mgt.EntityClassCurrency>();

            lst = ShowCurrency();
            ViewState["storedata"] = lst;
            this.grvacc_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTSPECIFICATIONCODE", "", "");
            /*Block By Mehedi*/
            /*
            // rptstk = new BDACCRPT.R_17_Acc.RptSpecification() ;
            // txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as //;
            txtCompany.Text = comnam;
            //// txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as //;
            //txtadress.Text =comadd;
            // txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as //;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, session, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Specification CodeBook";
                string eventdesc = "Print CodeBook";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, (BDAccSession.Current.tblLogin), eventtype, eventdesc, eventdesc2);
            }

            rptstk.SetDataSource(ds1.Tables[0]);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
             */

        }
        protected void ibtnSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowInformation();
        }




    }
}