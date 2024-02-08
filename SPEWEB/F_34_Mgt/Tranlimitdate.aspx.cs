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
    public partial class Tranlimitdate : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "BACK DATED TRANSACTION LIMIT";
                
            }



        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETBACKDAY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvcomlimit.DataSource = null;
                this.gvcomlimit.DataBind();
                return;

            }

            this.gvcomlimit.DataSource = ds1.Tables[0];
            this.gvcomlimit.DataBind();
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                return;
            }

            string comcod = this.GetCompCode();


            for (int i = 0; i < this.gvcomlimit.Rows.Count; i++)
            {

                string bday = Convert.ToDouble("0" + ((TextBox)this.gvcomlimit.Rows[i].FindControl("txtgvlimit")).Text.Trim()).ToString();

                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "UPDATEBDATTRNSINF", bday, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Fail');", true);

                    return;



                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);




        }




    }

}