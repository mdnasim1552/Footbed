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
using SPEENTITY;

namespace SPEWEB.F_34_Mgt
{
    public partial class FormaAnalysis : System.Web.UI.Page
    {
        ProcessAccess MgtData = new ProcessAccess();
        UserManagerSampling objUserMan = new UserManagerSampling();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.GetGenCode();
               
                ((Label)this.Master.FindControl("lblTitle")).Text = "Forma Analysis";
                this.CommonButton();

                

            }
           
        }
        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;
            string code = this.Request.QueryString["sircode"].ToString();
            var lstlformat = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == code.Substring(0,2));         


            DdlForma.DataTextField = "gdesc";
            DdlForma.DataValueField = "gcod";
            DdlForma.DataSource = lstlformat;
            DdlForma.DataBind();
            if (this.Request.QueryString["sircode"].Length > 0)
            {
                DdlForma.SelectedValue= this.Request.QueryString["sircode"].ToString();               
                this.LbtnOk_Click(null, null);           
            }

           

        }
        private void CommonButton()
        {

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnQuUpdate_Click);
           ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);

        }

        protected void Data_Bind()
        {
            try
            {

                DataTable tbl = (DataTable)ViewState["tblFormadetails"];
                if (this.Request.QueryString["sircode"].ToString().Substring(0, 2) == "43")
                {
                    this.gvanalysis.Columns[2].HeaderText = "Percent";
                    this.gvanalysis.Columns[3].HeaderText = "Amount";
                    this.gvanalysis.Columns[4].Visible = false;
                    this.gvanalysis.Columns[5].Visible = false;
                    this.gvanalysis.Columns[6].Visible = false;
                    this.gvanalysis.Columns[7].Visible = false;
                    this.gvanalysis.Columns[8].Visible = false;
                    this.gvanalysis.Columns[9].Visible = false;
                    this.gvanalysis.Columns[10].Visible = false;
                    this.gvanalysis.Columns[11].Visible = false;
                    this.gvanalysis.Columns[12].Visible = false;
                    this.gvanalysis.Columns[13].Visible = false;
                    this.gvanalysis.Columns[14].Visible = false;
                    this.gvanalysis.Columns[15].Visible = false;
                    this.gvanalysis.Columns[16].Visible = false;

                }
                this.gvanalysis.DataSource = tbl;
                this.gvanalysis.DataBind();
            }
            catch (Exception ex)
            {


            }

        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string forma = this.DdlForma.SelectedValue.ToString();
            DataSet ds1 = MgtData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_FORMA_ANALYSIS", forma, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblFormadetails"] = ds1.Tables[0];
            this.Data_Bind();


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        private void SaveValue()
        {
            DataTable dt =(DataTable) ViewState["tblFormadetails"];
            int index;
            for (int j = 0; j < this.gvanalysis.Rows.Count; j++)
            {
                string s1 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS1")).Text.Trim().ToString();
                string s2 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS2")).Text.Trim().ToString();
                string s3 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS3")).Text.Trim().ToString();
                string s4 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS4")).Text.Trim().ToString();
                string s5 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS5")).Text.Trim().ToString();
                string s6 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS6")).Text.Trim().ToString();
                string s7 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS7")).Text.Trim().ToString();
                string s8 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS8")).Text.Trim().ToString();
                string s9 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS9")).Text.Trim().ToString();
                string s10 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS10")).Text.Trim().ToString();
                string s11 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS11")).Text.Trim().ToString();
                string s12 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS12")).Text.Trim().ToString();
                string s13 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS13")).Text.Trim().ToString();
                string s14 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS14")).Text.Trim().ToString();
                string s15 = ((TextBox)this.gvanalysis.Rows[j].FindControl("TxtgvS15")).Text.Trim().ToString();
                

                index = (this.gvanalysis.PageIndex) * this.gvanalysis.PageSize + j;
                dt.Rows[index]["s1"] = s1;
                dt.Rows[index]["s2"] = s2;
                dt.Rows[index]["s3"] = s3;
                dt.Rows[index]["s4"] = s4;
                dt.Rows[index]["s5"] = s5;
                dt.Rows[index]["s6"] = s6;
                dt.Rows[index]["s7"] = s7;
                dt.Rows[index]["s8"] = s8;
                dt.Rows[index]["s9"] = s9;
                dt.Rows[index]["s10"] = s10;
                dt.Rows[index]["s11"] = s11;
                dt.Rows[index]["s12"] = s12;
                dt.Rows[index]["s13"] = s13;
                dt.Rows[index]["s14"] = s14;
                dt.Rows[index]["s15"] = s15;
        

            }


        
             ViewState["tblFormadetails"] = dt;
            this.Data_Bind();
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnQuUpdate_Click(object sender, EventArgs e)
        {

            bool result;
            string comcod = this.GetCompCode();// this.GetCompCode();
            string formacode = this.DdlForma.SelectedValue.ToString();
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblFormadetails"];


            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt.Copy());
            ds.Tables[0].TableName = "tblanalysis";


                result = MgtData.UpdateXmlTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_FORMA_ANALYSIS", ds,
                        null, null, formacode, "", "", "", "", "", "", "", "", "", "", "");



                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + MgtData.ErrorObject["Msg"] + "');", true);

                    return;
                }
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
           
            


        }

        protected void LbtnOk_Click(object sender, EventArgs e)
        {
            this.DdlForma.Enabled = false;
            ShowInformation();
        }
    }

}