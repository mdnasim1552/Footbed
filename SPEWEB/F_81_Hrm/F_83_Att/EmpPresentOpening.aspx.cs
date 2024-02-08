using System;
using System.Collections;
using System.Collections.Generic;
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


namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class EmpPresentOpening : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Present Opening";

                this.GetAllOrganogramList();
                this.GetWorkStation();
                this.GetJobLocation();
                this.GetLineddl();
                this.GetYearMonth();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
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
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ShowValue();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.txtSrcEmployee.Text = "";
                this.gvPFOpening.DataSource = null;
                this.gvPFOpening.DataBind();
                return;
            }
        }


        private void ShowValue()
        {
            Session.Remove("EarnedLeaveOpen");
            string comcod = this.GetComeCode();
            string empType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000" ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
            string year = this.ddlyearmon.SelectedValue.Substring(0,4);
            string fDate = "01-Jan-" + year;
            string toDate = "31-Dec-" + year;
            string cardno = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "PRE_EARNEDLEAVE_OPENING",empType, Dept, division, fDate, toDate, section, joblocation,line, cardno);
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvPFOpening.DataSource = null;
                this.gvPFOpening.DataBind();
                return;
            }
            DataTable dt = ds.Tables[0];
            Session["EarnedLeaveOpen"] = dt;
            this.LoadGrid();
        }


        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            Session["lstOrganoData"] = lst;
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
            this.ddlWstation_SelectedIndexChanged(null, null);
        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDivision();
        }

        private void GetDivision()
        {
            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
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

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);
            //lst1.Add()
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
        }


        private void LoadGrid()
        {
            try
            {
                string year = this.ddlyearmon.SelectedValue.Substring(0, 4);
                DateTime datefrm = Convert.ToDateTime("01-Jan-" + year);
                DateTime dateto = Convert.ToDateTime("31-Dec-" + year);

                for (int i = 6; i < 18; i++)
                {
                    if (datefrm > dateto)
                        break;

                    this.gvPFOpening.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                    datefrm = datefrm.AddMonths(1);

                }
                DataTable dt = (DataTable)Session["EarnedLeaveOpen"];
                this.gvPFOpening.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvPFOpening.DataSource = dt;
                this.gvPFOpening.DataBind();

                Session["Report1"] = gvPFOpening;
                if (dt.Rows.Count != 0)
                {
                    ((HyperLink)this.gvPFOpening.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message+ "');", true);

            }
        }

        protected void lnkbtnFUpLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["EarnedLeaveOpen"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString(); 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string monthid = "";
                bool result = true;
                string year = this.ddlyearmon.SelectedValue.Substring(0, 4);

                for (int j = 1; j < 13; j++)
                {
                    if (j < 10)
                    {
                        monthid = year + "0" + j;
                    }
                    else 
                    {
                        monthid = year + j;
                    }

                    string famt = dt.Rows[i]["amt"+j].ToString();
                    if(famt !="0")
                    {
                        result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATE_EARNED_LEAVE_OPEN", empid, monthid, famt, "", "", "", "", "", "", "", "", "", "", "");
                    }
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Present Opening Updated Successfully');", true);
        }





        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["EarnedLeaveOpen"];
            int TblRowIndex;
            for (int i = 0; i < this.gvPFOpening.Rows.Count; i++)
            {
                string txtgvernl1 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl1")).Text.Trim()).ToString();
                string txtgvernl2 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl2")).Text.Trim()).ToString();
                string txtgvernl3 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl3")).Text.Trim()).ToString();
                string txtgvernl4 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl4")).Text.Trim()).ToString();
                string txtgvernl5 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl5")).Text.Trim()).ToString();
                string txtgvernl6 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl6")).Text.Trim()).ToString();
                string txtgvernl7 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl7")).Text.Trim()).ToString();
                string txtgvernl8 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl8")).Text.Trim()).ToString();
                string txtgvernl9 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl9")).Text.Trim()).ToString();
                string txtgvernl10 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl10")).Text.Trim()).ToString();
                string txtgvernl11 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl11")).Text.Trim()).ToString();
                string txtgvernl12 = Convert.ToDouble("0" + ((TextBox)this.gvPFOpening.Rows[i].FindControl("txtgvernl12")).Text.Trim()).ToString();
                TblRowIndex = (gvPFOpening.PageIndex) * gvPFOpening.PageSize + i;

                dt.Rows[TblRowIndex]["amt1"] = txtgvernl1;
                dt.Rows[TblRowIndex]["amt2"] = txtgvernl2;
                dt.Rows[TblRowIndex]["amt3"] = txtgvernl3;
                dt.Rows[TblRowIndex]["amt4"] = txtgvernl4;
                dt.Rows[TblRowIndex]["amt5"] = txtgvernl5;
                dt.Rows[TblRowIndex]["amt6"] = txtgvernl6;
                dt.Rows[TblRowIndex]["amt7"] = txtgvernl7;
                dt.Rows[TblRowIndex]["amt8"] = txtgvernl8;
                dt.Rows[TblRowIndex]["amt9"] = txtgvernl9;
                dt.Rows[TblRowIndex]["amt10"] = txtgvernl10;
                dt.Rows[TblRowIndex]["amt11"] = txtgvernl11;
                dt.Rows[TblRowIndex]["amt12"] = txtgvernl12;
            }
            Session["EarnedLeaveOpen"] = dt;

        }

        private void ClearScreen()
        {
            //this.txtRefNum.Text = "";
            //this.txtSrinfo.Text = "";
            //this.txtRefNum.Text = "";
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetLineddl()
        {
            string comcod = GetComeCode();
            DataSet ds3 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
            ds3.Dispose();
            // ViewState["tbllineddl"] = ds3.Tables[0];
        }
        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "Y", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.AddYears(-1).ToString("yyyy") + "12";
            ds1.Dispose();
        }
        private void GetJobLocation()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }

      
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

        protected void lnkbtnPF_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }
    }
}