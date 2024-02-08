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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using static SPEENTITY.C_10_Procur.EClassProcur;

namespace SPEWEB.F_11_RawInv
{
    public partial class Material_Issue : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS ISSUE STATUS";
                this.GetResCodeleb2();
                this.SelectResCodeLeb2();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetDeparment();
                this.GetEmployeeList();
                this.CommonButton();
                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.PreList();
                }

            }
        }
        private void CommonButton()
        {
            

            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            if (this.Request.QueryString["Type"] == "Approve")
            {
                
                    if (Request.QueryString.AllKeys.Contains("actcode") && this.Request.QueryString["actcode"] == "False")
                    {
                        ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                    }
                    else
                    {
                        ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                        ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
                        ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
                        ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do you want to Approve?');";
                    }
                
                

            }

            if (this.Request.QueryString["Type"] == "Audit")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Audit";
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Audit Checked?');";

            }

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void lnkbtnLedger_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy");

            string isuno = this.Request.QueryString["genno"].ToString();
            string type = this.Request.QueryString["Type"].ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "APPROVEINDENTISSUE", isuno, usrid, sessionid, trmid, posteddat, type);
            if (result == true)
            {
                Response.Redirect(prevPage);
                // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Successfully Approved');", true);
            }

        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../F_11_RawInv/Material_Issue.aspx?Type=Entry&genno=");
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_07_Inv/Material_Issue.aspx?Type=Entry&genno=" + "', target='_self');</script>";
        }
        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }
        private void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg1.Text = "You have no permission";
            //    return;
            //}

            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            
            DataTable dt = (DataTable)ViewState["tblIssue"];
            string curdate = this.txtCurDate.Text.ToString().Trim();
            DateTime Bdate = this.GetBackDate();
            bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(curdate));
            if (!dcon)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                return;
            }
            if (this.ddlPreList.Items.Count == 0)
                this.GetLSDNo();
            string Issueno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();
            string Refno = this.txtrefno.Text.ToString();
            if (Refno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Ref. No. Should Not Be Empty');", true);
                return;
            }

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "CHECKEDDUPREFNO", Refno, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0) ;


            else
            {
                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("issueno <>'" + Issueno + "'");
                DataTable dt1 = dv1.ToTable();
                if (dt1.Rows.Count == 0)
                    ;
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Found Duplicate Ref. No.');", true);
                    //this.ddlPrevReqList.Items.Clear();
                    return;
                }
            }
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string RefNar = this.txtRemarks.Text.Trim();
            bool result;
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "INSORUPTXTTTOEMPINF", "indissueb", Issueno, curdate, Refno, PostedByid, Posttrmid, PostSession, Posteddat, pactcode, RefNar, "", "", "", "");


            foreach (DataRow dr in dt.Rows)
            {
                string rsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string deptcode = dr["deptcode"].ToString().Trim();
                string issueqty = dr["issueqty"].ToString().Trim();
                //string refundqty = dr["refundqty"].ToString().Trim();
                string issueamt = dr["issueamt"].ToString().Trim();
                string empid = dr["empid"].ToString().Trim();
                string remarks = dr["remarks"].ToString().Trim();
                string conqty = dr["conqty"].ToString().Trim();
                string conunt = dr["conunt"].ToString().Trim();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "INSORUPTXTTTOEMPINF", "indissuea", Issueno, rsircode, spcfcod,
                   deptcode, issueqty, issueamt, empid, remarks, conqty, conunt, "", "", "", "", "");
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            this.txtCurDate.Enabled = false;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = (hst["usrid"].ToString());
            string comcod = this.GetCompCode();

            // DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "%%", "FxtAst", "", userid, "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblStoreType"] = ds2.Tables[0];
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();


        }

        private void GetResCodeleb2()
        {
            Session.Remove("tblresleb2");

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_RESCODE_LEVEL2_ISSUE", "", "", userid, "", "", "", "", "", "");
            Session["tblresleb2"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void SelectResCodeLeb2()
        {
            DataTable dt = ((DataTable)Session["tblresleb2"]).Copy();

            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "sircode";
            this.ddlcatagory.DataSource = dt;
            this.ddlcatagory.DataBind();
        }
        private void GetMatList()
        {
            string comcod = this.GetCompCode();
            string mProject = this.ddlProject.SelectedValue.ToString();
            string CompGroup = this.ddlcatagory.SelectedValue.ToString() == "0000" ? "%": this.ddlcatagory.SelectedValue.ToString() + "%";
            string mSrchTxt = "%";
            string date = this.txtCurDate.Text.Trim();
            DataTable dt = (DataTable)ViewState["tblStoreType"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + mProject + "'");
            dt = dv.ToTable();
            string Codetype = dt.Rows[0]["acttype"].ToString();
            string SearchInfo = "";
            if (Codetype.Length > 0)
            {

                int len;
                string[] ar = Codetype.Split('/');
                foreach (string ar1 in ar)
                {


                    if (ar1.Contains("-"))
                    {
                        len = ar1.IndexOf("-");
                        SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                    }
                    else
                    {
                        len = ar1.Length;

                        SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }


            DataSet ds1 = purData.GetTransInfoNew(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETMATLIST",null,null,null, mProject, mSrchTxt, date, CompGroup, "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlResList.Items.Clear();
                // this.ddlResSpcf.Items.Clear();
                return;
            }
            ViewState["tblMat"] = ds1.Tables[0];

            ViewState["tblSpcf"] = ds1.Tables[1];
            this.ddlResList.DataTextField = "rsirdesc";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[2];
            this.ddlResList.DataBind();
            this.ddlResList_SelectedIndexChanged(null, null);
        }



        private void GetSpecification()
        {
            string mResCode = this.ddlResList.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblMat"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "'";
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();
        }

        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].Rows.Add(comcod, "000000000000", "Department");
            ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");


            this.ddlDeptCode.DataTextField = "fxtgdesc";
            this.ddlDeptCode.DataValueField = "fxtgcod";
            this.ddlDeptCode.DataSource = ds1.Tables[0];
            this.ddlDeptCode.DataBind();
            this.ddlDeptCode.SelectedValue = "AAAAAAAAAAAA";
        }
        protected void GetUnitName()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_NAME", "", "", "", "", "", "", "");
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_CONVRT_INF", "", "", "", "", "", "", "");
            ViewState["UnitsRate"] = ds.Tables[0];
            this.ddlunit.DataTextField = "gdesc";
            this.ddlunit.DataValueField = "gcod";
            this.ddlunit.DataSource = ds1.Tables[0];
            this.ddlunit.DataBind();

        }
        private void GetEmployeeList()
        {
            string comcod = this.GetCompCode();


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETEMPNAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlEmpList.Items.Clear();
                return;
            }


            this.ddlEmpList.DataTextField = "sirdesc";
            this.ddlEmpList.DataValueField = "sircode";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            ds1.Dispose();

        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetMatList();
        }
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();

            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }


            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable matlist = (DataTable)ViewState["tblMat"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            DataView dv1 = matlist.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "' ";
            DataTable dt = dv1.ToTable();
            if (dt.Rows.Count > 0 && dt.Rows[0]["untcod"].ToString() != "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                DataTable stdunit = (DataTable)ViewState["UnitsRate"];
                DataView dv = stdunit.DefaultView;
                dv.RowFilter = "bcod = '" + dt.Rows[0]["untcod"].ToString() + "' ";
                DataTable newtabl = dv.ToTable();
                if (newtabl.Rows.Count == 0)
                {
                    DataRow dr1 = newtabl.NewRow();

                    dr1["comcod"] = this.GetCompCode(); ;
                    dr1["bcod"] = dt.Rows[0]["untcod"].ToString();
                    dr1["bcodesc"] = dt.Rows[0]["rsirunit"].ToString();
                    dr1["ccod"] = dt.Rows[0]["untcod"].ToString();
                    dr1["ccodesc"] = dt.Rows[0]["rsirunit"].ToString();
                    dr1["conrat"] = 1.00;

                    newtabl.Rows.Add(dr1);
                }
                this.ddlunit.DataTextField = "ccodesc";
                this.ddlunit.DataValueField = "ccod";
                this.ddlunit.DataSource = newtabl;
                this.ddlunit.DataBind();
                this.ddlunit.SelectedValue = dt.Rows[0]["untcod"].ToString();
            }
            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Not set Material Unit";
            }

            this.ImgbtnSpecification_Click(null, null);

        }

        private void PreList()
        {


            string comcod = this.GetCompCode();
            string curdate = this.txtCurDate.Text.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETPREISSUELIST", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPreList.DataTextField = "issueno1";
            this.ddlPreList.DataValueField = "issueno";
            this.ddlPreList.DataSource = ds1.Tables[0];
            this.ddlPreList.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                string genno = this.Request.QueryString["genno"].ToString();

                this.ddlPreList.SelectedValue = genno;
                this.lbtnOk_Click(null, null);
            }


        }

        protected void ImgbtnFindPrevious_Click(object sender, EventArgs e)
        {
            this.PreList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.ddlProject.Visible = true;
                this.ddlProject.Enabled = false;
                this.ddlDeptCode.Enabled = false;
                this.ddlEmpList.Enabled = false;
                this.PanelSub.Visible = true;
                this.PnlRemarks.Visible = true;
                this.ImgbtnFindPrevious.Visible = false;
                this.ddlPreList.Visible = false;
                GetUnitName();
                this.GetMatList();
                this.GetIssuenfo();


                if (this.Request.QueryString["Type"] == "Entry")
                {
                    if (this.ddlPreList.Items.Count == 0)
                    {
                        if (this.ddlDeptCode.SelectedValue.ToString() == "AAAAAAAAAAAA")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Department');", true);
                            return;
                        }
                    }

                }

                return;
            }
           
            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.ddlProject.Enabled = true;
            this.ddlDeptCode.Enabled = true;
            this.ddlEmpList.Enabled = true;
            this.txtCurDate.Enabled = true;
            this.PanelSub.Visible = false;
            this.PnlRemarks.Visible = false;
            this.ImgbtnFindPrevious.Visible = true;
            this.ddlPreList.Visible = true;
            this.ddlPreList.Items.Clear();
            this.ddlResList.Items.Clear();
            //this.ddlResSpcf.Items.Clear();
            this.gvIssue.DataSource = null;
            this.gvIssue.DataBind();
        }

        protected void GetLSDNo()
        {

            string comcod = GetCompCode();
            string mIssueNo = "NEWISU";
            if (this.ddlPreList.Items.Count > 0)
                mIssueNo = this.ddlPreList.SelectedValue.ToString();

            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();


            if (mIssueNo == "NEWISU")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETISSUENO", date,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxissueno1"].ToString().Substring(0, 6);
                    this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxissueno1"].ToString().Substring(6, 5);
                    this.ddlPreList.DataTextField = "maxissueno1";
                    this.ddlPreList.DataValueField = "maxissueno";
                    this.ddlPreList.DataSource = ds2.Tables[0];
                    this.ddlPreList.DataBind();
                }
            }

        }

        private void GetIssuenfo()
        {


            ViewState.Remove("tblIssue");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mISUNo = "NEWISU";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mISUNo = this.ddlPreList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETISSUEINFO", mISUNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblIssue"] = ds1.Tables[0];


            if (mISUNo == "NEWISU")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETISSUENO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxissueno1"].ToString().Trim().Substring(0, 6);
                this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxissueno1"].ToString().Trim().Substring(6);
                return;
            }



            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlDeptCode.SelectedValue = ds1.Tables[1].Rows[0]["deptcode"].ToString();
            this.ddlEmpList.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();

            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["issuedat"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["issueno1"].ToString().Trim().Substring(0, 6);
            this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["issueno1"].ToString().Trim().Substring(6);
            this.Data_Bind();
        }


        private void Data_Bind()
        {

            this.gvIssue.DataSource = (DataTable)ViewState["tblIssue"];
            this.gvIssue.DataBind();
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblIssue"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.gvIssue.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(issueamt)", "")) ?
            0.00 : dt1.Compute("sum(issueamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }




        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblIssue"];
            DataTable dt2 = (DataTable)ViewState["UnitsRate"];
            for (int i = 0; i < this.gvIssue.Rows.Count; i++)
            {
                double gvdata = ASTUtility.StrPosOrNagative("0"+((Label)this.gvIssue.Rows[i].FindControl("lblgvstkqty")).Text.Trim());
                if (gvdata <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You do not have sufficient stock');", true);
                    return;
                }
                double stcqty =  ASTUtility.StrPosOrNagative("0" + gvdata.ToString());
                double issueqty = Convert.ToDouble("0" + ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvissueqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((Label)this.gvIssue.Rows[i].FindControl("lblgvstkrate")).Text.Trim());
                if (rate <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Issue-Rate must be more than 0');", true);
                    return;
                }

                string Remarks = ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvremarks")).Text.Trim();
                //double conqty = Convert.ToDouble("0" + ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvconqty")).Text.Trim());

                double conqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvconqty")).Text.Trim()));

                string bucod = ((Label)this.gvIssue.Rows[i].FindControl("lblConunt")).Text.Trim();
                string cucod = ((Label)this.gvIssue.Rows[i].FindControl("lbluntcod")).Text.Trim();

                DataRow[] dr3 = dt2.Select("bcod = '" + bucod + "' and ccod='" + cucod + "'");

                if (stcqty < issueqty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid Issue Quantity');", true);
                    return;
                }
                int rowindex = ((this.gvIssue.PageIndex) * (this.gvIssue.PageSize)) + i;

                dt1.Rows[rowindex]["issueqty"] = (dr3.Count() == 0) ? conqty : conqty * Convert.ToDouble(dr3[0]["conrat"]);
                dt1.Rows[rowindex]["issueamt"] = (dr3.Count() == 0) ? conqty * rate : (conqty * Convert.ToDouble(dr3[0]["conrat"])) * rate;
                dt1.Rows[rowindex]["remarks"] = Remarks;
                dt1.Rows[rowindex]["conqty"] = conqty;

            }
            ViewState["tblIssue"] = dt1;
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }




        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            //this.Panel2.Visible = true;
            this.SaveValue();

            DataTable tbl1 = (DataTable)ViewState["tblIssue"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string conunit = this.ddlunit.SelectedValue.ToString();
            string Empcode = this.ddlDeptCode.SelectedValue.ToString();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "' and  deptcode='" + Empcode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();

                dr1["comcod"] = this.GetCompCode(); ;
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
                dr1["empid"] = this.ddlEmpList.SelectedValue.ToString();
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["untcod"] = dr3[0]["untcod"];
                dr1["conqty"] = 0;
                dr1["conunt"] = conunit;
                dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
                dr1["stkqty"] = dr3[0]["stkqty"];
                dr1["stkrate"] = dr3[0]["stkrate"]; ;
                dr1["issueqty"] = 0;
                dr1["issueamt"] = 0;
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }

            ViewState["tblIssue"] = tbl1;
            this.Data_Bind();

        }
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblIssue"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string Specification = this.ddlResSpcf.SelectedValue.ToString();
            string Empcode = this.ddlEmpList.SelectedValue.ToString();
            DataTable tbl2 = (DataTable)ViewState["tblMat"];

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                DataRow[] dr3 = tbl1.Select("rsircode = '" + tbl2.Rows[i]["rsircode"].ToString() + "'");
                if (dr3.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["comcod"] = this.GetCompCode(); ;
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"];
                    dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                    dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
                    dr1["rsirdesc"] = tbl2.Rows[i]["rsirdesc"];
                    dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                    dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
                    dr1["empid"] = this.ddlEmpList.SelectedValue.ToString();
                    dr1["conqty"] = 0;
                    dr1["conunt"] = this.ddlunit.SelectedValue.ToString();
                    dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"];
                    dr1["untcod"] = tbl2.Rows[i]["untcod"];
                    dr1["stkqty"] = tbl2.Rows[i]["stkqty"];
                    dr1["stkrate"] = tbl2.Rows[i]["stkrate"];
                    dr1["issueqty"] = 0;
                    dr1["issueamt"] = 0;
                    dr1["remarks"] = "";

                    tbl1.Rows.Add(dr1);
                }


            }

            ViewState["tblIssue"] = tbl1;
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string curdate = this.txtCurDate.Text.ToString().Trim();
            string mISUNo = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();
            string empname = ddlEmpList.SelectedItem.Text;
            string storename = ddlProject.SelectedItem.Text;
            string deptname = ddlDeptCode.SelectedItem.Text;

            DataTable dt = (DataTable)ViewState["tblIssue"];
            var list = dt.DataTableToList<SPEENTITY.C_11_RawInv.EclassMaterialIssue>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptMaterialIssueStatus", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Material Issue Status"));
            rpt1.SetParameters(new ReportParameter("comlogo", comLogo));
            rpt1.SetParameters(new ReportParameter("empname", empname));
            rpt1.SetParameters(new ReportParameter("storename", storename));
            rpt1.SetParameters(new ReportParameter("deptname", deptname));
            rpt1.SetParameters(new ReportParameter("curdate", curdate));
            rpt1.SetParameters(new ReportParameter("isuno", mISUNo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string curdate = this.txtCurDate.Text.ToString().Trim();
            //string mISUNo = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();

            //DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETISSUEINFO", mISUNo, curdate, "", "", "", "", "", "", "");



            //ReportDocument rptChallan = new RMGiRPT.R_07_Inv.RptIssueChallaBR();

            //TextObject txtCompany = rptChallan.ReportDefinition.ReportObjects["Company"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtCompAdd = rptChallan.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //txtCompAdd.Text = comadd;

            //TextObject txtrptHeader = rptChallan.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtrptHeader.Text = "DELIVERY CHALLAN";

            //TextObject txtDelNo = rptChallan.ReportDefinition.ReportObjects["txtDelNo"] as TextObject;
            //txtDelNo.Text = ASTUtility.Left(mISUNo, 3) + mISUNo.Substring(7, 2) + "-" + ASTUtility.Right(mISUNo, 3); // "DO" + sdelno.Substring(3);

            //TextObject txtDate = rptChallan.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["issuedat"]).ToString("dd-MMM-yyyy");


            //TextObject txtCust = rptChallan.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //txtCust.Text = ds.Tables[1].Rows[0]["empname"].ToString();
            //TextObject txtCustadd = rptChallan.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //txtCustadd.Text = ds.Tables[1].Rows[0]["territory"].ToString();
            //TextObject txtPhone = rptChallan.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //txtPhone.Text = ds.Tables[1].Rows[0]["phone"].ToString();


            ////TextObject txtuserinfo = rptChallan.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////txtuserinfo.Text = ASTUtility.Concat("", "", "");

            //TextObject txtFComp = rptChallan.ReportDefinition.ReportObjects["txtFComp"] as TextObject;
            //txtFComp.Text = "For " + comnam;



            //rptChallan.SetDataSource(ds.Tables[0]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptChallan.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptChallan;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             "PDF" + "', target='_blank');</script>";

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Issue Challan";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Issue No : " + mISUNo;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

        }



        protected void gvIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblIssue"];

            int rowindex = (this.gvIssue.PageSize) * (this.gvIssue.PageIndex) + e.RowIndex;

            string rescode = ((Label)this.gvIssue.Rows[e.RowIndex].FindControl("lblgvMatCode")).Text.Trim();
            string spcfcod = ((Label)this.gvIssue.Rows[e.RowIndex].FindControl("lblgvspcfcod")).Text.Trim();
            dt.Rows[rowindex].Delete();
            string comcod = this.GetCompCode();
            string curdate = this.txtCurDate.Text.ToString().Trim();
            DateTime Bdate = this.GetBackDate();
            string Issueno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();
            bool res = purData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "REMOVEMATFROMISSUE", Issueno, rescode, spcfcod, "", "", "", "", "", "");
            if (res == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Material Deleted Sucessfully');", true);
            }

            ViewState["tblIssue"] = dt;
            this.Data_Bind();
        }

        protected void gvIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string untcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "untcod")).ToString();
                string conunt = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conunt")).ToString();
                if (untcod != conunt)
                {
                    ((Label)e.Row.FindControl("lblgvConunit")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblgvunit")).ForeColor = System.Drawing.Color.Red;

                }

                HyperLink proDesc = (HyperLink)e.Row.FindControl("lbgrcod");
                Label rsircode = (Label)e.Row.FindControl("lblgvMatCode");
                Label spcfcod = (Label)e.Row.FindControl("lblgvspcfcod");
                string date = this.txtCurDate.Text.ToString();

                proDesc.NavigateUrl = "~/F_11_RawInv/RptIndProStock?Type=MatHis&sircode=" + (rsircode.Text + spcfcod.Text) + "&date=" + date + "&dayid=" + date;

            }
        }

        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMatList();
        }
    }
}