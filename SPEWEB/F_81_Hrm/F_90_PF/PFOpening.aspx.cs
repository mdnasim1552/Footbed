using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;

namespace SPEWEB.F_81_Hrm.F_90_PF
{
    public partial class PFOpening : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "OPENING PROVIDENT FUND SCREEN VIEW";
                
                this.GetWorkStation();
                this.GetJobLocation();
                //this.GetPFOpeningDate();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnFUpLeave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //string type = this.Request.QueryString["Type"].ToString().Trim();
            //switch (type)
            //{
            //    case "LeaveRule":
            //        break;

            //}
            
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowValue();
            //if (this.lbtnOk.Text == "Ok")
            //{
            //    this.lbtnOk.Text = "New";               
            //    this.ShowValue();
            //}
            //else
            //{
            //    this.lbtnOk.Text = "Ok";
            //    this.gvPFOpening.DataSource = null;
            //    this.gvPFOpening.DataBind();
            //    this.txtEmpSearch.Text = "";
            //    return;
            //}
        }
        private void GetJobLocation()
        {

            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }

        private void ShowValue()
        {
            Session.Remove("PFOpen");
            Session.Remove("PFOpen2");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string emptype = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEPFOPENINGENTRY", emptype, joblocation, userid, "", "");
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                this.gvPFOpening.DataSource = null;
                this.gvPFOpening.DataBind();
                return;
            }
            DataTable dt = ds.Tables[0];
            Session["PFOpen"] = dt;
            Session["PFOpen2"] = dt;
            this.LoadGrid();

        }


        private void GetWorkStation()
        {

            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();


        }
        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["PFOpen"];
            string searchcard = this.txtEmpSearch.Text.Trim();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "idcardno like '%" + searchcard + "%'";
            dt = dv.ToTable();
            this.gvPFOpening.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPFOpening.DataSource = dt;
            this.gvPFOpening.DataBind();
            this.FooterCalculation(dt);
            Session["PFOpen"] = dt;
        }


        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
           
            ((Label)this.gvPFOpening.FooterRow.FindControl("lgvOpeningAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opening)", "")) ?
                                        0 : dt.Compute("sum(opening)", ""))).ToString("#,##0.00;(#,##0.00); ");
            Session["Report1"] = gvPFOpening;
            ((HyperLink)this.gvPFOpening.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }



        protected void lnkbtnFUpLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["PFOpen"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString(); 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString(); //empid               
                string idcardno = dt.Rows[i]["idcardno"].ToString();
                string principal = dt.Rows[i]["opening"].ToString();
                
                //string interest = dt.Rows[i]["interest"].ToString(); 

                bool result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEEMPLOYEEPFOPEN", empid, idcardno, principal, "", "", "", "", "", "", "", "", "", "", "");
                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ accData.ErrorObject["Msg"].ToString()+"');", true);
                    return;
                }
                else
                {
                  
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee PF Opening Updated Successfully');", true);

        }





        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["PFOpen"];
            int TblRowIndex;
            for (int i = 0; i < this.gvPFOpening.Rows.Count; i++)
            {
                string spfopen = ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvelOpen")).Text.Trim();
                spfopen = spfopen == "" ? "0" : spfopen;
                double pfopen = Convert.ToDouble(spfopen);
                TblRowIndex = (gvPFOpening.PageIndex) * gvPFOpening.PageSize + i;

                dt.Rows[TblRowIndex]["opening"] = pfopen;

            }
            Session["PFOpen"] = dt;

        }
        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        //private void GetPFOpeningDate()
        //{
        //    string comcod = this.GetComeCode();
        //    DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPFOPENDATE", "", "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;

        //    this.txtdate.Text = (ds1.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["opdate"]).ToString("dd-MMM-yyyy");
        //    //  this.txtdate.ReadOnly = ds1.Tables[0].Rows.Count == 0 ? false : false;       
        //    this.txtdate.Enabled = ds1.Tables[0].Rows.Count == 0 ? true : false;

        //}

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        protected void gvPFOpening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvPFOpening.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void txtEmpSearch_TextChanged(object sender, EventArgs e)
       {
            //string searchcard = this.txtEmpSearch.Text.Trim();
            DataTable dt = (DataTable)Session["PFOpen2"];
            Session["PFOpen"] = dt;
            if (dt == null) return;
            this.LoadGrid();
            //if (searchcard == "")
            //{
            //    this.ShowValue();
            //}
            //else
            //{
                
            //    Session["PFOpen"] = dt;
            //    this.LoadGrid();
            //}
            
            
        }
    }
}