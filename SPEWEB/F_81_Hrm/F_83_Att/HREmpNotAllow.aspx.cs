using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Services;
using SPELIB;

using SPEENTITY.C_81_Hrm.C_81_Rec;
namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class HREmpNotAllow : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                Session.Remove("DayAtten");
                this.txtFrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ComVisibility();
                this.ComVisibility();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetJobLocation();

                ((Label)this.Master.FindControl("lblTitle")).Text = "Overtime Not Allow & OT Deduciton - System";
                this.GetLineddl();
            }
        }

        private void ComVisibility()
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "5305":
                case "5306":

                    this.chkFootBed.Visible = true;
                    this.lblfootbed.Visible = true;
                    break;
                default:
                    this.chkFootBed.Visible = false;
                    this.lblfootbed.Visible = false;

                    break;

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }       

        private void GetLineddl()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";

            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        private void ShowData()
        {

            Session.Remove("ShowAtten");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string frmDate = this.txtFrmDate.Text.Trim();
            string toDate = this.txtToDate.Text.Trim();
            string usrid = hst["usrid"].ToString();
            string empType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string idCard = (this.txtIdcard.Text.ToString() == "") ? "%" : this.txtIdcard.Text.ToString() + "%";  
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "SHOWEMPOTALLOW", empType, div, frmDate, Dept, section, joblocation, usrid, idCard, line, toDate);
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Data Found!');", true);
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }

            Session["ShowAtten"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            

            string comid = dt1.Rows[0]["comid"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comid"].ToString() == comid)
                {
                    dt1.Rows[j]["comname"] = "";
                }
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    dt1.Rows[j]["section"] = "";


                }

                comid = dt1.Rows[j]["comid"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }

            return dt1;
        }

        private void LoadGrid()
        {
            try
            {
                this.gvDailyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvDailyAttn.DataSource = (DataTable)Session["ShowAtten"];
                this.gvDailyAttn.DataBind();

                Session["Report1"] = gvDailyAttn;
                ((HyperLink)this.gvDailyAttn.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)Session["ShowAtten"];
                int TblRowIndex; double dedout;
                for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
                {
                    string otNotAllow = (((CheckBox)gvDailyAttn.Rows[i].FindControl("CheckOTAllow")).Checked) ? "True" : "False";
                    dedout = Convert.ToDouble("0" + ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvDed")).Text.Trim());
                    DateTime outtime = Convert.ToDateTime(((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvAcOuttime")).Text.Trim());
                    //Deduction OT
                    DateTime dedouttime = outtime.AddHours(-dedout);
                    TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;

                    dt.Rows[TblRowIndex]["OTNOTALLOW"] = otNotAllow;
                    dt.Rows[TblRowIndex]["dedout"] = dedout;
                    dt.Rows[TblRowIndex]["outtime"] = dedouttime;

                }

                Session["ShowAtten"] = dt;
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }

        }
        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            bool result;
            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["ShowAtten"];

            foreach (DataRow dr1 in dt.Rows)
            {

                string otNotAllow = dr1["otnotallow"].ToString();
                string dayid = dr1["dayid"].ToString();
                string empid = dr1["empid"].ToString();
                string outtime = dr1["outtime"].ToString();
                string dedout = dr1["dedout"].ToString();
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOTNOTALLOW", empid, dayid, otNotAllow, dedout, outtime, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('OT Not Allow & Deduction Updated Successfully');", true);

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }     

       
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];


            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
            lst1.Add(all);

            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";

            this.ddlDivision_SelectedIndexChanged(null, null);

        }

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";

            this.ddlDept_SelectedIndexChanged(null, null);

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
        }


        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJob.DataTextField = "location";
            this.ddlJob.DataValueField = "loccode";
            this.ddlJob.DataSource = lst;
            this.ddlJob.DataBind();

        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {

          
                DataTable dt = (DataTable)Session["ShowAtten"];
                int i, index;
                if (((CheckBox)this.gvDailyAttn.FooterRow.FindControl("chkall")).Checked)
                {

                    for (i = 0; i < this.gvDailyAttn.Rows.Count; i++)
                    {

                        ((CheckBox)this.gvDailyAttn.Rows[i].FindControl("CheckOTAllow")).Checked = true;
                        index = (this.gvDailyAttn.PageSize) * (this.gvDailyAttn.PageIndex) + i;
                        dt.Rows[index]["otnotallow"] = "True";

                    }


                }

                else
                {
                    for (i = 0; i < this.gvDailyAttn.Rows.Count; i++)
                    {

                        ((CheckBox)this.gvDailyAttn.Rows[i].FindControl("CheckOTAllow")).Checked = false;
                        index = (this.gvDailyAttn.PageSize) * (this.gvDailyAttn.PageIndex) + i;
                        dt.Rows[index]["otnotallow"] = "False";

                    }

                }

                Session["otnotallow"] = dt;

          


        }

        protected void lnkbtnSameValue_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["ShowAtten"];
            double dedout =Convert.ToDouble("0"+((TextBox)this.gvDailyAttn.Rows[0].FindControl("txtgvDed")).Text.Trim()) ;
            int i = 0, rowindex;
            foreach (GridViewRow gv1 in gvDailyAttn.Rows)
            {
                rowindex = (this.gvDailyAttn.PageSize) * (this.gvDailyAttn.PageIndex) + i;
                i++;
                dt.Rows[rowindex]["dedout"] = dedout;

            }

            Session["ShowAtten"] = dt;
            this.LoadGrid();


        }

        protected void gvDailyAttn_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            DataTable dt = (DataTable)Session["ShowAtten"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvDailyAttn.DataSource = sortedView;
            gvDailyAttn.DataBind();

            Session["ShowAtten"] = sortedView.ToTable();
        }
        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }
    }
}