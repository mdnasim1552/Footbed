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

namespace SPEWEB.F_34_Mgt
{
    public partial class AccConversion : System.Web.UI.Page
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

                this.GetCurruency();
                this.ShowInformation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Currency Conversion";
                this.CommonButton();

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

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnQuUpdate_Click);
        }

        public List<SPEENTITY.C_34_Mgt.EntityClassCurrency> ShowCurrency()
        {
            List<SPEENTITY.C_34_Mgt.EntityClassCurrency> lst = new List<SPEENTITY.C_34_Mgt.EntityClassCurrency>();
            string comcod = this.GetCompCode();  // ObjCommon.GetCompCode();

            SqlDataReader dr = PurData.GetSqlReader(comcod, "SP_ENTRY_CODEBOOK", "GETCURRENCY", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {

                SPEENTITY.C_34_Mgt.EntityClassCurrency details = new SPEENTITY.C_34_Mgt.EntityClassCurrency(dr["code"].ToString(), dr["codedesc"].ToString(), dr["curdesc"].ToString(), dr["cursymbol"].ToString(), dr["curword"].ToString(), dr["curstatus"].ToString());
                lst.Add(details);
            }
            return lst;
        }

        public List<SPEENTITY.C_34_Mgt.EntityClassConversion> ShowConversion()
        {
            List<SPEENTITY.C_34_Mgt.EntityClassConversion> lst = new List<SPEENTITY.C_34_Mgt.EntityClassConversion>();
            string comcod = this.GetCompCode();
            SqlDataReader dr = PurData.GetSqlReader(comcod, "SP_ENTRY_MGT", "SHOWCONVERSION", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_34_Mgt.EntityClassConversion details = new SPEENTITY.C_34_Mgt.EntityClassConversion(dr["fcode"].ToString(), dr["tcode"].ToString(),
                    dr["fcodedesc"].ToString(), dr["tcodedesc"].ToString(), Convert.ToDouble(dr["conrate"].ToString()), Convert.ToDouble(dr["conrate1"].ToString()));
                lst.Add(details);
            }
            return lst;
        }




        protected void Data_Bind()
        {
            try
            {
                this.gvConversion.Columns[3].HeaderText = "1 " + this.hfBaseCurrency.Value + " = FC";    // HeaderText="1 SGD = FC"
                this.gvConversion.Columns[4].HeaderText = "1 FC = " + this.hfBaseCurrency.Value;    // HeaderText="1 FC = SGD"          
                var lst = (List<SPEENTITY.C_34_Mgt.EntityClassConversion>)ViewState["tblconversion"];
                this.gvConversion.DataSource = lst;
                this.gvConversion.DataBind();
            }
            catch (Exception ex)
            {


            }

        }




        private void GetCurruency()

        {
            List<SPEENTITY.C_34_Mgt.EntityClassCurrency> lst = new List<SPEENTITY.C_34_Mgt.EntityClassCurrency>();
            lst = ShowCurrency();//    //objUser.ShowCurrency();

            this.hfBaseCurrency.Value = "";
            foreach (var item in lst)
            {
                if (item.curstatus == "True")
                {
                    this.hfBaseCurrency.Value = item.codedesc.Trim();
                    break;
                }

            }

            this.ddlfromcurrency.DataTextField = "codedesc";
            this.ddlfromcurrency.DataValueField = "code";
            this.ddlfromcurrency.DataSource = lst;
            this.ddlfromcurrency.DataBind();

            this.ddltocurrency.DataTextField = "codedesc";
            this.ddltocurrency.DataValueField = "code";
            this.ddltocurrency.DataSource = lst;
            this.ddltocurrency.DataBind();


        }

        private void ShowInformation()
        {

            ViewState.Remove("tblconversion");
            List<SPEENTITY.C_34_Mgt.EntityClassConversion> lst = new List<SPEENTITY.C_34_Mgt.EntityClassConversion>();
            lst = ShowConversion();
            ViewState["tblconversion"] = lst;
            this.Data_Bind();


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        private void SaveValue()
        {
            var lst = (List<SPEENTITY.C_34_Mgt.EntityClassConversion>)ViewState["tblconversion"];
            int index;
            for (int j = 0; j < this.gvConversion.Rows.Count; j++)
            {
                double conrate1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvConversion.Rows[j].FindControl("txtgvconrate1")).Text.Trim()));
                double conrate = conrate1 > 0 ? (1 / conrate1) : 0.00;
                index = (this.gvConversion.PageIndex) * this.gvConversion.PageSize + j;
                lst[index].conrate = conrate;
                lst[index].conrate1 = conrate1;
            }
            ViewState["tblconversion"] = lst;

        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            var lst = (List<SPEENTITY.C_34_Mgt.EntityClassConversion>)ViewState["tblconversion"];
            string fcode = this.ddlfromcurrency.SelectedValue.ToString();
            string fcodedesc = this.ddlfromcurrency.SelectedItem.Text;
            string tcode = this.ddltocurrency.SelectedValue.ToString();
            string tcodedesc = this.ddltocurrency.SelectedItem.Text;
            double conrate = Convert.ToDouble("0" + this.txtConversion.Text.Trim());
            double conrate1 = (conrate > 0.00 ? (1 / conrate) : 0.00);
            List<SPEENTITY.C_34_Mgt.EntityClassConversion> lst1 = lst.FindAll((p => (p.fcode == fcode) && (p.tcode == tcode)));

            if (lst1.Count == 0)
            {
                SPEENTITY.C_34_Mgt.EntityClassConversion objconadd = new SPEENTITY.C_34_Mgt.EntityClassConversion(fcode, tcode, fcodedesc, tcodedesc, conrate, conrate1);
                lst.Add(objconadd);
            }
            ViewState["tblconversion"] = lst;
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnQuUpdate_Click(object sender, EventArgs e)
        {
                       

            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), BDAccSession.Current.tblusrlog);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg1.Text = "You have no permission";
            //    return;
            //}

            bool result=false;
            string comcod = this.GetCompCode();// this.GetCompCode();

            this.SaveValue();
            var lst = (List<SPEENTITY.C_34_Mgt.EntityClassConversion>)ViewState["tblconversion"];

            foreach (SPEENTITY.C_34_Mgt.EntityClassConversion c1 in lst)
            {
                string fcode = c1.fcode;
                string tcode = c1.tcode;
                string conrate = Convert.ToDouble(c1.conrate).ToString();


                result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTUPDATECON", fcode,
                        tcode, conrate, "", "", "", "", "", "", "", "", "", "", "", "");


                
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + PurData.ErrorObject["Msg"] + "');", true);

                    return;
                }
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
            }
            if (result == true && ConstantInfo.LogStatus == true)
            {
                string eventtype = "Currency Conversion";
                string eventdesc = "Currency Conversion";
                string eventdesc2 = "Currency Conversion Update/Add";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }

    }

}