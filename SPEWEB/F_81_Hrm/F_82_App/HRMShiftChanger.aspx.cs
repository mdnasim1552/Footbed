using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class HRMShiftChanger : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Shift Working Changer";

                this.txttodate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                this.txtShiftFrom.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");

                //  this.GetAllOrganogramList();
               
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetLine();             
                this.GetAllShiftData();
                //this.GettoShiftData();
                this.GetDesignation();
                this.GetGrade();
                this.GetJobLocation();
                this.GetEmployeeOff();
                this.CommonButton();
            

            }

        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Apply Shiftplan to Selected Employee";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            UpdateShiftTime();

        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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



            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetEmpType(comcod, userid);
            this.ddlWstation1.DataTextField = "actdesc";
            this.ddlWstation1.DataValueField = "actcode";
            this.ddlWstation1.DataSource = lst;
            this.ddlWstation1.DataBind();
            this.ddlWstation1.SelectedValue = "000000000000";



            //string comcod = GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();              
            //var lst = getlist.GetWstation(comcod, userid);
            //lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000" && x.actcode != "940100000000" && x.actcode != "940200000000" && x.actcode!="000000000000");
            //SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1 all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1("000000000000", "ALL", "", "", "", "");
            //lst.Add(all);

            //this.ddlWstation1.DataTextField = "actdesc";
            //this.ddlWstation1.DataValueField = "actcode";
            //this.ddlWstation1.DataSource = lst;
            //this.ddlWstation1.DataBind();
            //this.ddlWstation1.SelectedValue = "000000000000";

            //this.GetDivision();

        }

        private void GetDivision()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation1.SelectedValue.ToString()=="000000000000"?"94": this.ddlWstation1.SelectedValue.ToString().Substring(0,4))+"%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDivision(comcod, wstation);
            this.ddlDivision1.DataTextField = "actdesc";
            this.ddlDivision1.DataValueField = "actcode";
            this.ddlDivision1.DataSource = lst;
            this.ddlDivision1.DataBind();
            this.ddlDivision1.SelectedValue = "00000";



            //string wstation1 = this.ddlWstation1.SelectedValue.ToString();
            //string comcod = GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();

            //List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
            //var lst3 = lst.FindAll(x => x.actcode.Substring(7) == "00000" && x.actcode.Substring(5)!="0000000");
            ////var lst3 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation1.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation1);

            //SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all1 = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
            //lst3.Add(all1);

            //this.ddlDivision1.DataTextField = "actdesc";
            //this.ddlDivision1.DataValueField = "actcode";
            //this.ddlDivision1.DataSource = lst3;
            //this.ddlDivision1.DataBind();
            //this.ddlDivision1.SelectedValue = "000000000000";
            //this.GetDeptList();

        }

        private void GetDeptList()
        {


            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation1.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation1.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDept1.DataTextField = "actdesc";
            this.ddlDept1.DataValueField = "actcode";
            this.ddlDept1.DataSource = lst;
            this.ddlDept1.DataBind();
            this.ddlDept1.SelectedValue = "00000";



           

        }

        private void GetSectionList()
        {


            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation1.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation1.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetSection(comcod, wstation);
            this.ddlSection1.DataTextField = "actdesc";
            this.ddlSection1.DataValueField = "actcode";
            this.ddlSection1.DataSource = lst;
            this.ddlSection1.DataBind();
            this.ddlSection1.SelectedValue = "00000";

            //string comcod = GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();
            //List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
            //string wstation1 = this.ddlDept1.SelectedValue.ToString();
            //var lst12 = lst.FindAll(x => x.actcode.Substring(9)!="000");
            ////var lst12 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation1.Substring(0, 9) && x.actcode != wstation1);
            //SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all1 = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            //lst12.Add(all1);

            //this.ddlSection1.DataTextField = "actdesc";
            //this.ddlSection1.DataValueField = "actcode";
            //this.ddlSection1.DataSource = lst12;
            //this.ddlSection1.DataBind();
            //this.ddlSection1.SelectedValue = "000000000000";
            //this.GetLine();

        }

        private void GetLine()
        {
            string comcod = GetCompCode();
            string CompanyName = (this.ddlWstation1.SelectedValue.ToString()=="000000000000" ? "94" : this.ddlWstation1.SelectedValue.ToString().Substring(0, 4))+"%";

            string division ="%";
            string deptname =  "%";
            string section ="%";

            //string division = (this.ddlDivision1.SelectedValue.ToString() == "00000" ? "" : this.ddlDivision1.SelectedValue.ToString().Substring(0, 7)) + "%";
            //string deptname = (this.ddlDept1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept1.SelectedValue.ToString().Substring(0, 9)) + "%";
            //string section = (this.ddlSection1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection1.SelectedValue.ToString()) + "%";

            var lst = getlist.GETLine(comcod, CompanyName, division, deptname, section);
            Session["lstline"] = lst;

            this.ddlLine.DataTextField = "linedesc";
            this.ddlLine.DataValueField = "linecode";
            this.ddlLine.DataSource = lst;
            this.ddlLine.DataBind();

        }
        private void GetEmployeeOff()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string company = (this.ddlWstation1.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation1.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision1.SelectedValue.ToString() == "00000" ? "" : this.ddlDivision1.SelectedValue.ToString()) + "%";
            string dept = (this.ddlDept1.SelectedValue.ToString() == "00000" ? "" : this.ddlDept1.SelectedValue.ToString()) + "%";
            string secname = (this.ddlSection1.SelectedValue.ToString() == "00000" ? "" : this.ddlSection1.SelectedValue.ToString()) + "%";
            string grade = (this.ddlgrade.SelectedValue.ToString() == "0000000" ? "03" : this.ddlgrade.SelectedValue.ToString().Substring(0,4)) + "%";
            string desig = (this.ddldesig.SelectedValue.ToString() == "0000000" ? "03" : this.ddldesig.SelectedValue.ToString()) + "%";
            string floor = (this.ddlLine.SelectedValue.ToString()=="00000" ? "70" : this.ddlLine.SelectedValue.ToString())+"%";
            string shiftid = this.ddlExtShift.SelectedValue.ToString();
            string frmdate = this.txtShiftFrom.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string jobLocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAMEOT_SHIFTBYID",null,null,null, company, division, dept, secname, floor, shiftid, frmdate, todate, grade, desig, jobLocation, userid);
            if (ds3 == null)
                return;
            this.DropCheck1.DataTextField = "empname";
            this.DropCheck1.DataValueField = "empid";
            this.DropCheck1.DataSource = ds3.Tables[0];
            this.DropCheck1.DataBind();
            string toemp = ds3.Tables[0].Rows.Count == 0 ? "" : ds3.Tables[0].Rows.Count.ToString("#,##0;(#,##)0; ");
            this.lblshiftemp.Text = "Total Employee: <span style='color:maroon'>" + toemp+"</span>";
            ds3.Dispose();


        }
        private void GetAllShiftData()
        {
            Session.Remove("tblshiftinfo");
            string comcod = this.GetCompCode();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETSHIFTINFOR", "", "", "", "", "", "", "", "", "");


            if (ds4 == null || ds4.Tables[0].Rows.Count == 0)
            {
                this.ddlExtShift.Items.Clear();
                this.ddlExtShift.DataSource = null;
                this.ddlExtShift.DataBind();
                return;
            }
            this.ddlExtShift.DataTextField = "shifname";
            this.ddlExtShift.DataValueField = "shiftid";
            this.ddlExtShift.DataSource = ds4;
            this.ddlExtShift.DataBind();
            ViewState["tblshiftinfo"] = ds4.Tables[0];

            this.ddlWorkShfitTo.DataTextField = "shifname";
            this.ddlWorkShfitTo.DataValueField = "shiftid";
            this.ddlWorkShfitTo.DataSource = ds4.Tables[0];
            this.ddlWorkShfitTo.DataBind();
            ds4.Dispose();

        }

        private void GettoShiftData()
        {

            DataTable dt = ((DataTable)ViewState["tblshiftinfo"]).Copy();
            string fshift = this.ddlExtShift.SelectedValue.ToString();
            DataView dvr = new DataView();
            dvr = dt.DefaultView;
            dvr.RowFilter = ("shiftid<>" + fshift);
            this.ddlWorkShfitTo.DataTextField = "shifname";
            this.ddlWorkShfitTo.DataValueField = "shiftid";
            this.ddlWorkShfitTo.DataSource = dvr.ToTable();
            this.ddlWorkShfitTo.DataBind();


        }

        private void GetDesignation()
        {

            ViewState.Remove("ViewState");
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDesignation(comcod);
            ViewState["lstdesig"] = lst;
        }
        private void GetGrade()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];           
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetGrade(comcod);
            this.ddlgrade.DataTextField = "gradedesc";
            this.ddlgrade.DataValueField = "grade";
            this.ddlgrade.DataSource = lst;
            this.ddlgrade.DataBind();
            this.ddlgrade.SelectedValue = "0000000";
            this.DesigforSGrade();
        }

        private void GetJobLocation()
        {
            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }


        private void DesigforSGrade()
        {



            var lst =(List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation>)ViewState["lstdesig"];
            List < SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation> lst2 = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EClassDesignation>();
            string grade = this.ddlgrade.SelectedValue.ToString();
            this.ddldesig.Items.Clear();
            switch (grade)
            {

                case "0000000":
                    lst2 = lst;
                    break;


                default:
                    lst2 = lst.FindAll(l => l.desigid.Substring(0, 4) == grade.Substring(0, 4) || l.desigid=="0000000");
                    break;
            
            }
           
            this.ddldesig.DataTextField = "desig";
            this.ddldesig.DataValueField = "desigid";
            this.ddldesig.DataSource = lst2;
            this.ddldesig.DataBind();
            this.ddldesig.SelectedValue = "0000000";

        }

        protected void ddlWstation1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeOff();

            
        }

        protected void ddlDivision1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeOff();
        }

        protected void ddlDept1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeOff();
        }

        protected void ddlSection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeOff();

        }
        protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DesigforSGrade();
            this.GetEmployeeOff();

        }


        protected void ddldesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeOff();
        }

        protected void ddlExtShift_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            GetEmployeeOff();
        }

        private void UpdateShiftTime()
        {
            string frmdate, todate;

            DateTime offintime, today, offouttime, lanintime, lanouttime, systime;

            string dayid;
            frmdate = this.txtShiftFrom.Text;
            todate = this.txttodate.Text;
            string comcod = this.GetCompCode();

            //==============shift to information
            DataTable dt = (DataTable)ViewState["tblshiftinfo"];
            string shiftTOid = this.ddlWorkShfitTo.SelectedValue.ToString().Trim();
            DataView dvr = new DataView();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("shiftid=" + shiftTOid);
            dt = dvr.ToTable();


            string abstime = dt.Rows[0]["abstime"].ToString().Trim();
            string latemarg = dt.Rows[0]["latemarg"].ToString().Trim();
            string macstart = dt.Rows[0]["macstarttime"].ToString().Trim();
            string sintime = dt.Rows[0]["sintime"].ToString().Trim();
            string souttime = dt.Rows[0]["souttime"].ToString().Trim();
            string lintime = dt.Rows[0]["lintime"].ToString().Trim();
            string louttime = dt.Rows[0]["louttime"].ToString().Trim();
            int addday=Convert.ToInt32(dt.Rows[0]["addday"].ToString().Trim());

            string offintimepam = ASTUtility.Right(sintime, 2).ToUpper();
            string lintimepam= ASTUtility.Right(lintime, 2).ToUpper();

            int addlday = (offintimepam == "PM" && lintimepam == "AM") ? addday : 0;


            string empid = "";
            foreach (ListItem item in DropCheck1.Items)
            {
                if (item.Selected)
                {
                    empid += item.Value+",";
                }
            }

            string ws = (this.ddlWstation1.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation1.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision1.SelectedValue.ToString() == "00000" ? "" : this.ddlDivision1.SelectedValue.ToString()) + "%";
            string Dept1 = (this.ddlDept1.SelectedValue.ToString() == "00000" ? "" : this.ddlDept1.SelectedValue.ToString()) + "%";
            string sec = (this.ddlSection1.SelectedValue.ToString() == "00000" ? "" : this.ddlSection1.SelectedValue.ToString()) + "%";
          //  string grade = (this.ddlgrade.SelectedValue.ToString() == "0000000" ? "03" : this.ddlgrade.SelectedValue.ToString().Substring(0, 4)) + "%";
           // string desig = (this.ddldesig.SelectedValue.ToString() == "0000000" ? "03" : this.ddldesig.SelectedValue.ToString()) + "%";

            //while (frmdate <= todate)
            //{

                //dayid = frmdate.ToString("yyyyMMdd");
                //string ws = (this.ddlWstation1.SelectedValue.ToString().Substring(0, 4)) + "%";
                //string divison = ((this.ddlDivision1.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision1.SelectedValue.ToString().Substring(0, 7) + "%");
                //string Dept1 = (this.ddlDept1.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept1.SelectedValue.ToString().Substring(0, 9)) + "%";
                //string sec = (this.ddlSection1.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection1.SelectedValue.ToString()) + "%";


               



                //today =Convert.ToDateTime(frmdate);
                offintime = Convert.ToDateTime(frmdate + " " + sintime);
                //on day shift below code
                offouttime = Convert.ToDateTime(frmdate + " " + souttime);
                offouttime = offouttime.AddDays(addday);
                lanintime = Convert.ToDateTime(frmdate + " " + lintime);
                lanintime = lanintime.AddDays(addlday);
                lanouttime = Convert.ToDateTime(frmdate + " " + louttime);
                lanouttime = lanouttime.AddDays(addlday);
            // return;
                 bool result;
                empid = empid.Length > 0 ? empid.Substring(0, empid.Length - 1) : empid;

                //result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPSHIFTOFFTIME", dayid, "", "", "", empid, "",
                //    "", "", "", "", "", "", "", "", "", "", "", "", "");
                //if (result == false)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                //    return;
                //}



                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATESHIFTTIMECHANGER", frmdate, todate, "", "", empid, offintime.ToString(),
                    offouttime.ToString(), lanintime.ToString(), lanouttime.ToString(), division, shiftTOid, macstart, latemarg, abstime, "", "", "", "", "");
                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                    return;
                }
           // }

            this.GetEmployeeOff();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeOff();
        }

       
    }
}