using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace SPEWEB.F_81_Hrm.F_81_Rec
{
    public partial class EmpCustDocPrint : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        
        {


            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee All Document";
                this.GetWorkStation();
                this.GetAllOrganogramList();
            }
            UpdatePanel panel = ((UpdatePanel)Master.FindControl("UpdatePanel1"));
            panel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            panel.ChildrenAsTriggers = false;
            

        }

       
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            this.hrPrint();


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

            this.ddlDivision_SelectedIndexChanged(null, null);

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

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);
            //lst1.Add()

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();

            this.ddlSection.SelectedValue = "000000000000";
            ddlSection_SelectedIndexChanged(null, null);
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.getemp();
            // this.GetComASecSelected("000000000000");
        }
        protected void getemp()
        {

            string comcod = this.GetCompCode();
            string company = (this.ddlWstation.SelectedValue.Substring(0, 4).ToString() == "0000") ? "%" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString() + "%";
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7).ToString() + "%";
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9).ToString() + "%";
            string sectioncode = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string txtEmpname = "%";
            // string txtidCard = "%" + this.txtSrcEmp.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLIST", sectioncode, txtEmpname, company, div, deptid, "", "", "", "");
            this.ddlPEmpName.DataTextField = "empname";
            this.ddlPEmpName.DataValueField = "empid";
            this.ddlPEmpName.DataSource = ds1.Tables[0];
            this.ddlPEmpName.DataBind();



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
       
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
       
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            
            this.ShowView();
            
        }
        private void ShowView()
        {

            this.txtml.Text=data();

        }
        protected string data()
        {
            var empid = this.ddlPEmpName.SelectedValue.ToString();
            var empNames = (this.ddlPEmpName.SelectedItem.ToString()).Split('-');

            var type = this.ddlLttrType.SelectedValue;
            string comcod = this.GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTERFORMAT", empid, type, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds3 != null && ds3.Tables[0].Rows.Count != 0)
            {
                DataTable ds = stam((string)ds3.Tables[0].Rows[0]["lettdesc"]);
                Session["lettdesc"] = ds.Rows[0]["strval"].ToString();
                return ds.Rows[0]["strval"].ToString();
            }
            else
            {
                string HtmlText = "";

                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTERFORMAT", "000000000000", type, "", "", "", "", "", "", "");
                if (ds3 != null && ds3.Tables[0].Rows.Count != 0)
                {
                    DataTable dtm = (DataTable)ds1.Tables[0];
                    DataView dv = dtm.DefaultView;
                    dv.RowFilter = ("gcod like '0302001%'");
                    DataTable dt = dv.ToTable();

                    DataView dv1 = dtm.DefaultView;
                    dv1.RowFilter = ("gcod like '01003%'");
                    DataTable dt1 = dv1.ToTable();


                    string lettdesc = "";
                    string sal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0.00; (#,##0.00); ");
                    string Inword = ASTUtility.Trans(Convert.ToDouble(sal), 2);
                    DataTable ds = stam((string)ds3.Tables[0].Rows[0]["lettdesc"]);
                    lettdesc = ds.Rows[0]["strval"].ToString().Replace("{EmpName}", empNames[1]);
                    lettdesc = lettdesc.Replace("{Date}", (DateTime.Now).ToString("dd-MMM-yyyy"));
                    lettdesc = lettdesc.Replace("{EmpDegi}", dt.Rows[0]["gdesc"].ToString());
                    lettdesc = lettdesc.Replace("{EmpJoin}", Convert.ToDateTime( dt1.Rows[0]["gdesc"]).ToString("dd-MMM-yyyy"));
                    lettdesc = lettdesc.Replace("{EmpDept}", ds1.Tables[2].Rows[0]["empdeptdesc"].ToString());
                    lettdesc = lettdesc.Replace("{EmpSal}", sal);
                    lettdesc = lettdesc.Replace("{EmpSalInword}", Inword);
                    
                    HtmlText = lettdesc;
                }

                return HtmlText;
            }
        }
        public DataTable stam(string xmlData)
        {
            
            XElement x = XElement.Parse(xmlData);
            DataTable dt = new DataTable();
            XElement setup = (from p in x.Descendants() select p).First();

            foreach (XElement xe in setup.Descendants()) // build your DataTable
                dt.Columns.Add(new DataColumn(xe.Name.ToString(), typeof(string))); // add columns to your dt

            var all = from p in x.Descendants(setup.Name.ToString()) select p;

            foreach (XElement xe in all)
            {
                DataRow dr = dt.NewRow();
                foreach (XElement xe2 in xe.Descendants())
                    dr[xe2.Name.ToString()] = xe2.Value; //add in the values
                dt.Rows.Add(dr);
            }

            return dt;
        }
        private void hrPrint()
        {
            

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            var empid = this.ddlPEmpName.SelectedValue.ToString();
            string strval = this.txtml.Text;
            int a = strval.Length;
            var type = this.ddlLttrType.SelectedValue;
            var date = DateTime.Now.ToString();
            string comcod = this.GetCompCode();
            DataSet DsStrval = new DataSet();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("strval");
            DataRow dr1 = tbl.NewRow();
            dr1["strval"] = strval;
            tbl.Rows.Add(dr1);
            DsStrval.Tables.Add(tbl);
            HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPLOADLETTER", DsStrval, null, null, empid, type,"", date);
        }


    }
}