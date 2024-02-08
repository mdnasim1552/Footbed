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
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_87_Tra
{
    public partial class HREmpTransfer : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtpatplacedate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE TRANSFER INFORMATION";
                this.CommonButton();
                this.Get_Trnsno();
                this.GetWorkStation();
                this.GetWorkStation1();
                this.GetAllOrganogramList();
                this.GetSingEmpName();
                this.GenLogInfo();
                this.GetDesignation();
                this.GetEmpLine();
            }

            if (ViewState["tblTrans"] == null)
            {
                this.tableintosession();

            }
        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkupdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    // Create an event handler for the master page's contentCallEvent event
        //    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        //    //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        //}
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

        private void GetWorkStation1()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");


            this.ddlWstation1.DataTextField = "actdesc";
            this.ddlWstation1.DataValueField = "actcode";
            this.ddlWstation1.DataSource = lst;
            this.ddlWstation1.DataBind();

            this.ddlWstation1_SelectedIndexChanged(null, null);

        }


        private void GetSingEmpName()
        {

            string comcod = this.GetCompCode();
            DataSet ds3 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAME", "940[1-2]%", "%", "%", "%", "1", "%", "", "", "");
            this.ddlsign.DataTextField = "empname1";
            this.ddlsign.DataValueField = "empid";
            this.ddlsign.DataSource = ds3.Tables[0];
            this.ddlsign.DataBind();
        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        protected void ddlWstation1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllOrganogramList();
            this.GetDivision1();
        }



        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);

            if (lst1.Count == 0)
            {
                lst1.Add(new BO_ClassManPower.HrSirInf("000000000000","All"));
            }
           
            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();

            this.ddlDivision_SelectedIndexChanged(null, null);

        }

        private void GetDivision1()
        {


            string wstation1 = this.ddlWstation1.SelectedValue.ToString();//940100000000


            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];


            var lst2 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation1.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation1);
            if (lst2.Count == 0)
            {
                lst2.Add(new BO_ClassManPower.HrSirInf("000000000000", "All"));
            }

            this.ddlDivision1.DataTextField = "actdesc";
            this.ddlDivision1.DataValueField = "actcode";
            this.ddlDivision1.DataSource = lst2;
            this.ddlDivision1.DataBind();

            //this.ddlDivision.SelectedValue = "";
            this.ddlDivision1_SelectedIndexChanged(null, null);

        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDivision1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList1();
        }
        private void GetDeptList()
        {
            string wstation = (this.ddlDivision.SelectedValue.ToString() == "") ? "000000000000" : this.ddlDivision.SelectedValue.ToString();//940100000000


            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

           
            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);

            if (lst1.Count == 0)
            {
                lst1.Add(new BO_ClassManPower.HrSirInf("000000000000", "All"));
            }

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();


            this.ddlDept_SelectedIndexChanged(null, null);

        }
        private void GetDeptList1()
        {

            string wstation1 = this.ddlDivision1.SelectedValue.ToString();//940100000000 
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst2 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation1.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation1);
            if (lst2.Count == 0)
            {
                lst2.Add(new BO_ClassManPower.HrSirInf("000000000000", "All"));
            }
            this.ddlDept1.DataTextField = "actdesc";
            this.ddlDept1.DataValueField = "actcode";
            this.ddlDept1.DataSource = lst2;
            this.ddlDept1.DataBind();

            this.ddlDept1_SelectedIndexChanged(null, null);

        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
        }
        protected void ddlDept1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList1();
        }
        private void GetSectionList()
        {
            
            string wstation = (this.ddlDept.SelectedValue.ToString() == "") ? "000000000000" : this.ddlDept.SelectedValue.ToString();//940100000000
            string wstation1 = (this.ddlDept1.SelectedValue.ToString() == "") ? "000000000000" : this.ddlDept1.SelectedValue.ToString();//940100000000


            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            if (lst1.Count == 0)
            {
                lst1.Add(new BO_ClassManPower.HrSirInf("000000000000", "All"));
            }



            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();


            this.ddlSection_SelectedIndexChanged(null, null);


        }
        private void GetSectionList1()
        {

            string wstation1 = this.ddlDept1.SelectedValue.ToString();//940100000000


            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst2 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation1.Substring(0, 9) && x.actcode != wstation1);
            if (lst2.Count == 0)
            {
                lst2.Add(new BO_ClassManPower.HrSirInf("000000000000", "All"));
            }
            this.ddlSection1.DataTextField = "actdesc";
            this.ddlSection1.DataValueField = "actcode";
            this.ddlSection1.DataSource = lst2;
            this.ddlSection1.DataBind();

            this.ddlSection1_SelectedIndexChanged(null, null);


        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Employee_List();
        }
        protected void ddlSection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  this.Employee_List1();
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void GetPrvNm()
        {

            string comcod = GetCompCode();
            //string mREQNO = "NEWISS";
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
            string mREQDAT = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds2 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", mREQDAT,"", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxtrnno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnno1";
                this.ddlPrevISSList.DataValueField = "maxtrnno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();
            }

        }
        private void Get_Trnsno()
        {

            string comcod = this.GetCompCode();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6);
        }



        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("empid", Type.GetType("System.String"));
            dttemp.Columns.Add("idcardno", Type.GetType("System.String"));
            dttemp.Columns.Add("empname", Type.GetType("System.String"));
            dttemp.Columns.Add("desig", Type.GetType("System.String"));
            dttemp.Columns.Add("desigid", Type.GetType("System.String"));
            dttemp.Columns.Add("tfcompany", Type.GetType("System.String"));
            dttemp.Columns.Add("tfseccode", Type.GetType("System.String"));
            dttemp.Columns.Add("ttcompany", Type.GetType("System.String"));
            dttemp.Columns.Add("ttseccode", Type.GetType("System.String"));

            dttemp.Columns.Add("tfcomdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("tfsecdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("ttcomdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("ttsecdesc", Type.GetType("System.String"));

            dttemp.Columns.Add("pplacedate", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("rmrks", Type.GetType("System.String"));
            dttemp.Columns.Add("infor", Type.GetType("System.String"));
            dttemp.Columns.Add("spnote", Type.GetType("System.String"));
            dttemp.Columns.Add("address", Type.GetType("System.String"));
            dttemp.Columns.Add("sempid", Type.GetType("System.String"));
            dttemp.Columns.Add("sempname", Type.GetType("System.String"));
            dttemp.Columns.Add("ttdesigid", Type.GetType("System.String"));
            dttemp.Columns.Add("ttdesig", Type.GetType("System.String"));
            dttemp.Columns.Add("tflinecode", Type.GetType("System.String"));
            dttemp.Columns.Add("tflinedesc", Type.GetType("System.String"));
            dttemp.Columns.Add("ttlinecode", Type.GetType("System.String"));
            dttemp.Columns.Add("ttlinedesc", Type.GetType("System.String"));
            ViewState["tblTrans"] = dttemp;
        }
        



        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            #region Crystal Print
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string company = this.ddlWstation.SelectedItem.Text.Trim();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tblTrans"];

            //if (this.rbtTrnstype.SelectedIndex == 0)
            //{
            //    ReportDocument rptmattrans = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptHREmpTransfer();
            //    TextObject rptCname = rptmattrans.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //    rptCname.Text = company;
            //    TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
            //    rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtCurTransDate.Text)).ToString("MMMM dd, yyyy");
            //    TextObject rpttrnno = rptmattrans.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
            //    rpttrnno.Text = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + "-" + this.txtCurTransNo2.Text.Trim();
            //    TextObject rpttxtinformation = rptmattrans.ReportDefinition.ReportObjects["txtinformation"] as TextObject;
            //    rpttxtinformation.Text = this.txtfmaters.Text.Trim();
            //    TextObject rpttxtspnote = rptmattrans.ReportDefinition.ReportObjects["txtspnote"] as TextObject;
            //    rpttxtspnote.Text = this.txtspnote.Text.Trim();
            //    TextObject txtuserinfo = rptmattrans.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptmattrans.SetDataSource(dt1);

            //    //string comcod = this.GetComeCode();
            //    string comcod = hst["comcod"].ToString();
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptmattrans.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptmattrans;
            //}
            //else
            //{

            //    string date = Convert.ToDateTime(dt1.Rows[0]["pplacedate"]).ToString("dd MMMM, 2010");
            //    ReportDocument rptmattrans = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptHREmpTransfer01();
            //    TextObject rptCname = rptmattrans.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //    rptCname.Text = company;
            //    TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
            //    rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtCurTransDate.Text)).ToString("MMMM dd, yyyy");
            //    TextObject rpttrnno = rptmattrans.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
            //    rpttrnno.Text = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + "-" + this.txtCurTransNo2.Text.Trim();
            //    TextObject rpttxtinformation = rptmattrans.ReportDefinition.ReportObjects["txtinformation"] as TextObject;
            //    rpttxtinformation.Text = "Management has decided to transfer you in the following department/project.You  are requested  to confirm your reporting in the following place on or before " + date + ".";

            //    rptmattrans.SetDataSource(dt1);
            //    string comcod = hst["comcod"].ToString();
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptmattrans.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptmattrans;


            //}

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion 
        }

        protected void Load_Prev_Trans_List()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlCompany.Visible = true;
                this.pnlToCompany.Visible = true;
                this.pnlremarks.Visible = true;
                this.txtCurTransDate.Enabled = true;

                if (this.ddlPrevISSList.Items.Count > 0)
                {
                    this.txtCurTransDate.Enabled = false;
                    string trnno = this.ddlPrevISSList.SelectedValue.ToString().Trim();
                    string comcod = this.GetCompCode();
                    DataSet ds = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
                    if(ds == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                        return;
                    }
                    ViewState["tblTrans"] = ds.Tables[0];
                    this.txtfmaters.Text = ds.Tables[0].Rows[0]["infor"].ToString();
                    this.txtspnote.Text = ds.Tables[0].Rows[0]["spnote"].ToString();
                    this.grvacc_DataBind();
                    this.Load_Cur_Trans_NO();
                }
                else
                {
                    this.Get_Trnsno();
                }

                this.divPrevList.Visible = false; 
            }
            else
            {
                this.txtfmaters.Text = "";
                this.txtspnote.Text = "";
                this.ddlPrevISSList.Items.Clear();
                this.txtCurTransDate.Enabled = true;
                this.divPrevList.Visible = true;
                ViewState["tblTrans"] = null;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.pnlCompany.Visible = false;
                this.pnlToCompany.Visible = false;
                this.pnlremarks.Visible = false;
                lbtnOk.Text = "Ok";
            }

        }

        protected void grvacc_DataBind()
        {
            try
            {
                this.grvacc.DataSource = (DataTable)ViewState["tblTrans"];
                this.grvacc.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message.ToString() +"');", true);
            }            
        }

        protected void Employee_List()
        {
            Session.Remove("tblemp");
            string comcod = this.GetCompCode();
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" || this.ddlDivision.SelectedValue.ToString() == "") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000" || this.ddlDept.SelectedValue.ToString() == "") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000" || this.ddlSection.SelectedValue.ToString() == "") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string txtEmpname =  "%" + this.TxtSearch.Text.ToString() + "%" ;
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLIST", Section, txtEmpname, wstation, division, Deptid, "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            Session["tblemp"] = ds1.Tables[0];


        }

        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {

            string empid = this.ddlEmpList.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.txtEmpDesignation.Text = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
            }

        }

        protected void Load_Cur_Trans_NO()
        {
            this.lblCurTransNo1.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(0, 5);
            this.txtCurTransNo2.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(6, 5);
            string curdate = Convert.ToDateTime(this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(14)).ToString("dd.MM.yyyy");

            if (curdate.Substring(2, 1).ToString().Trim() == "-")
            {
                this.txtCurTransDate.Text = "0" + curdate.Trim();
            }
            else
                this.txtCurTransDate.Text = curdate;

        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.Employee_List();
        }

        protected void lnkselect_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveValue();
                string empid = this.ddlEmpList.SelectedValue.ToString().Trim();
                DataTable dt = (DataTable)ViewState["tblTrans"];
                DataRow[] projectrow2 = dt.Select("empid = '" + empid + "'");
                if (projectrow2.Length > 0)
                {
                    return;

                }

                DataRow drforgrid = dt.NewRow();
                drforgrid["empid"] = empid;
                drforgrid["idcardno"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["idcardno"];
                drforgrid["empname"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["empname"].ToString();
                drforgrid["desigid"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desigid"];
                drforgrid["desig"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desig"];

                drforgrid["tfcompany"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"];
                drforgrid["tfseccode"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["refno"];
                drforgrid["tfcomdesc"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["companydesc"];
                drforgrid["tfsecdesc"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["refdesc"];

                drforgrid["ttcompany"] = this.ddlWstation1.SelectedValue.ToString();
                drforgrid["ttseccode"] = this.ddlSection1.SelectedValue.ToString();
                drforgrid["ttcomdesc"] = this.ddlWstation1.SelectedItem.Text.ToString();
                drforgrid["ttsecdesc"] = this.ddlSection1.SelectedItem.Text.ToString();

                drforgrid["pplacedate"] = this.GetStdDate(this.txtpatplacedate.Text.Trim());
                drforgrid["sempid"] = this.ddlsign.SelectedValue.ToString();
                drforgrid["sempname"] = this.ddlsign.SelectedItem.Text.ToString();

                drforgrid["ttdesigid"] = this.ddlDesignation.SelectedValue.ToString();
                drforgrid["ttdesig"] = this.ddlDesignation.SelectedItem.Text.ToString();
                drforgrid["tflinecode"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["linecode"];
                drforgrid["tflinedesc"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["linedesc"];
                drforgrid["ttlinecode"] = this.ddlEmpLine.SelectedValue.ToString();
                drforgrid["ttlinedesc"] = this.ddlEmpLine.SelectedItem.Text.ToString();
                dt.Rows.Add(drforgrid);
                ViewState["tblTrans"] = dt;
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblTrans"];
            int TblRowIndex;
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {

                string txtremarks = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvremarks")).Text;
                TblRowIndex = (grvacc.PageIndex) * grvacc.PageSize + i;

                dt.Rows[TblRowIndex]["rmrks"] = txtremarks;

            }
            ViewState["tblTrans"] = dt;


        }

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;

                this.SaveValue();
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)ViewState["tblTrans"];
                if (ddlPrevISSList.Items.Count == 0)
                {
                    this.GetPrvNm();
                }
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
                string information = this.txtfmaters.Text.Trim();
                string spnote = this.txtspnote.Text.Trim();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string fromsec = dt.Rows[i]["tfseccode"].ToString();
                    string tosec = dt.Rows[i]["ttseccode"].ToString();
                    string remarks = dt.Rows[i]["rmrks"].ToString();
                    string date = Convert.ToDateTime(dt.Rows[i]["pplacedate"]).ToString();
                    string desigid = dt.Rows[i]["desigid"].ToString();
                    string signatory = dt.Rows[i]["sempid"].ToString();
                    string ttdesigid = dt.Rows[i]["ttdesigid"].ToString();
                    string ttdesig = dt.Rows[i]["ttdesig"].ToString();
                    string tflinecode = dt.Rows[i]["tflinecode"].ToString();
                    string ttlinecode = dt.Rows[i]["ttlinecode"].ToString();

                    bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSORUPHREMPTNSINF", tansno, fromsec, tosec, empid,
                         curdate, remarks, information, spnote, date, desigid, PostedByid, PostSession, Posttrmid, Posteddat, ttdesigid, tflinecode, ttlinecode, signatory, ttdesig);
                    if(!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() +"');", true);
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Transfered Successfully');", true);
               

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
                
            }

        }
        private void GenLogInfo()
        {
            string comcod = this.GetCompCode();
            string empid =  this.ddlEmpList.SelectedValue.ToString();
            DataSet ds5 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAGGLOG", empid, "", "", "", "", "", "", "", "");
            if (ds5 == null)
                return;

            Session["UserLog"] = ds5.Tables[0];
        }
        private void GetDesignation()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetEmpGradelist(comcod);
            lst = lst.FindAll(x => x.hrgcod != "00000");
            this.ddlDesignation.DataTextField = "hrgdesc";
            this.ddlDesignation.DataValueField = "hrgcod";
            this.ddlDesignation.DataSource = lst;
            this.ddlDesignation.DataBind();

        }
        private void GetEmpLine()
        {
            string comcod = GetCompCode();
            DataSet ds3 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.ddlEmpLine.DataTextField = "hrgdesc";
            this.ddlEmpLine.DataValueField = "hrgcod";
            this.ddlEmpLine.DataSource = ds3;
            this.ddlEmpLine.DataBind();
            this.ddlEmpLine.SelectedValue = "00000";
            ds3.Dispose();
        }
        protected void lbtnPrevTransList_Click1(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();
        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            DataTable dt = (DataTable)ViewState["tblTrans"];
            dt.Rows[index].Delete();
            dt.AcceptChanges();
            ViewState["tblTrans"] = dt;
            grvacc_DataBind();
        }

        protected void ImgbtnFindComp_Click(object sender, EventArgs e)
        {
            Session.Remove("tblemp");
            string comcod = this.GetCompCode();
            string txtEmpname ="%"+ this.TxtSearch.Text.ToString()+"%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLIST", "%", txtEmpname, "%", "%", "%", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            Session["tblemp"] = ds1.Tables[0];

            string empid = this.ddlEmpList.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlWstation.SelectedValue = dt.Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddlWstation_SelectedIndexChanged(null, null);
                this.ddlDivision.SelectedValue = dt.Select("empid='" + empid + "'")[0]["divcode"].ToString();
                this.ddlDivision_SelectedIndexChanged(null, null);
                this.ddlDept.SelectedValue = dt.Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlDept_SelectedIndexChanged(null, null);
                this.ddlSection.SelectedValue = dt.Select("empid='" + empid + "'")[0]["refno"].ToString();
                this.txtEmpDesignation.Text = dt.Select("empid='" + empid + "'")[0]["desig"].ToString();
            }

        }
    }
}