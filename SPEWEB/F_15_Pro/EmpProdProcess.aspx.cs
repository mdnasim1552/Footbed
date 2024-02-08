using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY.C_15_Pro;

namespace SPEWEB.F_15_Pro
{
    public partial class EmpProdProcess : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        BL_Emp_Setup objUserService = new BL_Emp_Setup();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)

            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION PROCESS";
                this.ShowProcessInfo();

            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        private void ShowProcessInfo()
        {
            string comcod = this.GetCompCode();
            List<SPEENTITY.C_15_Pro.BO_Emp_Setup.EClassEmpProc> lst1 = objUserService.ShowProcessList(comcod);
            ViewState["tbEmpProc"] = lst1;

            this.gvEmpSetup.DataSource = lst1;
            this.gvEmpSetup.DataBind();

            //((Label)this.gvEmpSetup.FooterRow.FindControl("lblFgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ?
            //               0 : dt.Compute("sum(trndram)", ""))).ToString("#,##0;(#,##0); ");


        }



        protected void gvEmpSetup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblgvSetup");
            //string monid = this.ddlyearmon.SelectedValue.ToString();
            string sircode = ((Label)e.Row.FindControl("lblsircode")).Text;
            //string Deptcode = this.ddldeptlist.SelectedValue.ToString();
            //string Month = this.ddlyearmon.SelectedItem.Text;


            string sirdesc = ((Label)e.Row.FindControl("lblsirdesc")).Text;
            //Label Desg = (Label)e.Row.FindControl("lblgvDesg");
            //Label Joindat = (Label)e.Row.FindControl("lblgvJoindat");
            //string estatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "estatus")).ToString();
            //if (estatus != "active")
            //{
            //Empname.Style.Add("color", "red");
            //Desg.Style.Add("color", "red");
            //Joindat.Style.Add("color", "red");
            // }


            hlink1.NavigateUrl = "OvEmpSetup.aspx?Type=SetupInfo&comcod=" + mCOMCOD + "&sircode=" + sircode + "&sirdesc=" + sirdesc;
        }
    }
}