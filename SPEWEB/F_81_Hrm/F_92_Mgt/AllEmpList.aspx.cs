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
using SPELIB;
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class AllEmpList : System.Web.UI.Page
    {
        //page create nahid 20160927
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                Session.Remove("tblEmpstatus");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                GetWorkStation();
                GetAllOrganogramList();

                GetJobLocation();


                ((Label)this.Master.FindControl("lblTitle")).Text = "All Employe Details";


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private string GetCompCode()
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
        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string jobLocCode = "87";
            string wtype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            //var lst1 = lst;

            //SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType { hrgcod = "87000", hrgdesc = "All Location" };
            //lst1.Add(all);



            this.ddlJob.DataTextField = "hrgdesc";
            this.ddlJob.DataValueField = "hrgcod";
            this.ddlJob.DataSource = lst;
            this.ddlJob.DataBind();
            //if (wtype == "9403")
            //{
            //    this.ddlJob.SelectedValue = "87002";

            //}
            //else
            //{
            //    this.ddlJob.SelectedValue = "87000";

            //}
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
            this.ddlSection_SelectedIndexChanged(null, null);
            //this.GetEmpList();
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


        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpList();
        }





        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
        }
        private void GetEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string JobLocation = (this.ddlJob.SelectedValue.ToString() == "00000") ? "%" : this.ddlJob.SelectedValue.ToString() + "%";
            
            //string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            //string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTALLEMPLISTWITHPIC", wstation, division, "%%", Deptid, Section,  "", "", "",JobLocation);
            if (ds4 == null)
            {

                return;
            }

            Session["tblEmpstatus"] = (ds4.Tables[0]);
            this.LoadData();





            DataTable dt2 = ds4.Tables[1];
            if (dt2.Rows.Count == 0)
                return;


            TextBox[] txtarray = { txtdpt1, txtdpt2, txtdpt3, txtdpt4, txtdpt5, txtdpt6, txtdpt7, txtdpt8, txtdpt9, txtdpt10, txtdpt11, txtdpt12, txtdpt13,
                             txtdpt14, txtdpt15, txtdpt16, txtdpt17, txtdpt18, txtdpt19, txtdpt20, txtdpt21, txtdpt22, txtdpt23, txtdpt24, txtdpt25,
                             txtdpt26, txtdpt27, txtdpt28, txtdpt29, txtdpt30, txtdpt31, txtdpt32, txtdpt33, txtdpt34, txtdpt35, txtdpt36,txtdpt37,txtdpt38,
                             txtdpt39, txtdpt40, txtdpt41, txtdpt42, txtdpt43, txtdpt44, txtdpt45, txtdpt46, txtdpt47, txtdpt48, txtdpt49, txtdpt50,
                             txtdpt51, txtdpt52, txtdpt53, txtdpt54, txtdpt55, txtdpt56, txtdpt57, txtdpt58, txtdpt59, txtdpt60};

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                txtarray[i].Text = Convert.ToDouble((Convert.IsDBNull(dt2.Rows[i]["nosec"]) ? 0 : (dt2.Rows[i]["nosec"]))).ToString();
            }


            //Section Name

            TextBox[] txtarray2 = { TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8, TextBox9, TextBox10, TextBox11, TextBox12, TextBox13,
                              TextBox14, TextBox15, TextBox16, TextBox17, TextBox18, TextBox19, TextBox20, TextBox21, TextBox22, TextBox23, TextBox24, TextBox25,
                              TextBox26, TextBox27, TextBox28, TextBox29, TextBox30, TextBox31, TextBox32, TextBox33, TextBox34, TextBox35, TextBox36, TextBox37,
                              TextBox38, TextBox39, TextBox40, TextBox41, TextBox42, TextBox43, TextBox44, TextBox45, TextBox46, TextBox47, TextBox48, TextBox49,
                              TextBox50, TextBox51, TextBox52, TextBox53, TextBox54, TextBox55, TextBox56, TextBox57, TextBox58, TextBox59, TextBox60};

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                txtarray2[i].Text = ((Convert.IsDBNull(dt2.Rows[i]["section"]) ? 0 : (dt2.Rows[i]["section"]))).ToString();
            }

            double nosec = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(nosec)", "")) ? 0.00 : dt2.Compute("sum(nosec)", "")));


            this.txtTtlStaff.Text = nosec.ToString("#,##0;(#,##0.00);");








        }
        private void LoadData()
        {

            DataTable dt2 = (DataTable)Session["tblEmpstatus"];
            ListViewEmpAll.DataSource = dt2;
            ListViewEmpAll.DataBind();
        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem listViewDataItem = e.Item as ListViewDataItem;
                HtmlGenericControl divControl = e.Item.FindControl("EmpAll") as HtmlGenericControl;
                DataRowView dataRow = ((DataRowView)listViewDataItem.DataItem);
                divControl.Attributes.Add("ID", dataRow["idcardno"].ToString());


            }

        }

    }
}