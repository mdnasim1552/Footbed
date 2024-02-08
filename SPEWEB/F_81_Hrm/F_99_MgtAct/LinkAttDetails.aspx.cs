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

namespace SPEWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class LinkAttDetails : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                string titel = "";
                string QType = this.Request.QueryString["Type"].ToString();

                if (QType == "lempname")
                {
                    titel = "Late Employe information";
                }

                else if (QType == "elempname")
                {
                    titel = "Early Late Employe information";
                }

                else if (QType == "olempname")
                {
                    titel = "On Leave Employe information";
                }

                else if (QType == "aempname")
                {
                    titel = "Absent Employe information";
                }


                ((Label)this.Master.FindControl("lblTitle")).Text = titel;
                this.ShowView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void ShowView()
        {
            string comcod = this.Request.QueryString["comcod"].ToString();

            string Date = Convert.ToDateTime(this.Request.QueryString["date"].ToString()).ToString("dd-MMM-yyyy");
            string code = ASTUtility.Left(this.Request.QueryString["code"], 7).ToString() + "%";
            string QType = this.Request.QueryString["Type"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE02", "RPTLATEEONANDABSENTDET", Date, code, QType, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rplellaabsemp.DataSource = null;
                this.rplellaabsemp.DataBind();
                return;
            }

            this.rplellaabsemp.DataSource = ds1.Tables[2];
            this.rplellaabsemp.DataBind();

        }





    }
}