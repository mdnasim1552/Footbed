using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class DeviceIPSetup : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.GetIpSetup();
                ((Label)this.Master.FindControl("lblTitle")).Text = "IP Setup";
            }
        }



        private void GetIpSetup()
        {
            string comcod = GetComCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_COMWISE_MACH_IP2");

            ViewState["IpSetup"] = ds1.Tables[0];


            this.Data_Bind();
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void Data_Bind()
        {
            var IpSetupInf = ViewState["IpSetup"];
            this.grvIpSetup.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvIpSetup.DataSource = IpSetupInf;
            this.grvIpSetup.DataBind();
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dl = (DataTable)ViewState["IpSetup"];

                int index = dl.Rows.Count - 1;

                string machno;

                if (index < 0)
                {
                    machno = "100";
                    this.txtMachineNo.Text = Convert.ToString(Convert.ToInt32(machno) + 1);
                }
                else
                {
                    machno = dl.Rows[index]["machno"].ToString();
                    this.txtMachineNo.Text = Convert.ToString(Convert.ToInt32(machno) + 1);
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }
            catch (Exception ex)
            {

            }
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvIpSetup.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string machno = this.txtMachineNo.Text;
            string ipaddress = this.txtIpAddress.Text;
            string alias = this.txtAlias.Text;
            string port = this.txtPort.Text;

            if (ipaddress == "")
            {
                string msg = "IP Address Field Is Empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            DataTable dt=(DataTable)ViewState["IpSetup"];
            DataRow[] dr = dt.Select("machno = '" + machno + "' and ipaddress= '"+ ipaddress+"'");
            //DataSet duplicacycheck = this.MktData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "CHECKADDIP", machno, ipaddress);

            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["machno"] = machno;
                dr1["ipaddress"] = ipaddress;
                dr1["machinealias"] = alias;
                dr1["port"] = port;
                dr1["comcod"] = comcod;
                dt.Rows.Add(dr1);
                ViewState["IpSetup"] = dt;
                this.Data_Bind();
            }
            else
            {
                string msg = "Duplicate value is not allowed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpPer_Click);
        }
        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["IpSetup"];
            int rowindex;
            foreach (DataRow dr in dt.Rows)
            {
                string comcod = dr["comcod"].ToString();
                string machno = dr["machno"].ToString();
                string ipaddress = dr["ipaddress"].ToString();
                string machinealias = dr["machinealias"].ToString();
                string port = dr["port"].ToString();
                string corcernname = dr["corcernname"].ToString();
                bool result = MktData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "ADD_COMWISE_MACH_IP", machno, ipaddress, machinealias, port, corcernname);

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);
                    return;
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);

        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["IpSetup"];
            int rowindex;
            foreach (GridViewRow gv1 in grvIpSetup.Rows)
            {
                string machno = ((Label)gv1.FindControl("txtMachNo")).Text.ToString();
                string ipaddress = ((Label)gv1.FindControl("txtIpAddress")).Text.ToString();
                string machinealias = ((TextBox)gv1.FindControl("txtAlias")).Text.ToString();
                string port = ((TextBox)gv1.FindControl("txtPort")).Text.ToString();
                string corcernname= ((TextBox)gv1.FindControl("txtCorcernname")).Text.ToString();
                string comcod = ((Label)gv1.FindControl("lvlComcod")).Text.ToString();
                rowindex = this.grvIpSetup.PageIndex * this.grvIpSetup.PageSize + gv1.RowIndex;
                dt.Rows[rowindex]["machno"] = machno;
                dt.Rows[rowindex]["ipaddress"] = ipaddress;
                dt.Rows[rowindex]["machinealias"] = machinealias;
                dt.Rows[rowindex]["port"] = port;
                dt.Rows[rowindex]["comcod"] = comcod;
                dt.Rows[rowindex]["corcernname"] = corcernname;
            }
            ViewState["IpSetup"] = dt;
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int index = this.grvIpSetup.PageSize * this.grvIpSetup.PageIndex + RowIndex;

            string comcod = ((Label)this.grvIpSetup.Rows[RowIndex].FindControl("lvlComcod")).Text.ToString();
            string machno = ((Label)this.grvIpSetup.Rows[RowIndex].FindControl("txtMachNo")).Text.ToString();
            string ipaddress = ((Label)this.grvIpSetup.Rows[RowIndex].FindControl("txtIpAddress")).Text.ToString();
            bool deleted = MktData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETE_IP_ADDRESS", machno, ipaddress);

            DataTable dt = (DataTable)ViewState["IpSetup"];
            dt.Rows[index].Delete();
            dt.AcceptChanges();
            ViewState["IpSetup"] = dt;
            this.Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Deleted successfully');", true);
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}