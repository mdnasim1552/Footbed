using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_11_RawInv
{
    public partial class PurInterComMatTransfer : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
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
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblprintstk")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIAL TRANSFER INFORMATION";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lblFromCmpName.Text = hst["comnam"].ToString();
                this.txtfdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttdate.Text = this.txtfdate.Text;
                this.rbtnList1.SelectedIndex = 0;
                this.GetResCodeleb2();

                this.GetProjectlist();
                this.Resourcelist();
                this.GetHeadAccout();
                this.GetToCompany();
                this.GetToProject();
                this.GetToHeadAccount();


                this.GetfVoucherNo();
                this.GettVoucherNo();
                this.TableCreate();

                this.CommonButton();

            }

        }
        private void CommonButton()
        {

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;


        }
        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblpmattrans"];
            int rowindex;
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                double amt = qty * rate;


                rowindex = (grvacc.PageIndex) * grvacc.PageSize + i;
                dt.Rows[rowindex]["qty"] = qty;
                dt.Rows[rowindex]["rate"] = rate;
                dt.Rows[rowindex]["amt"] = amt;



            }
            ViewState["tblpmattrans"] = dt;

        }
        private void Data_Bind()
        {


            this.grvacc.DataSource = (DataTable)ViewState["tblpmattrans"];
            this.grvacc.DataBind();

            // this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            //((LinkButton)this.grvacc.FooterRow.FindControl("lnkupdate")).Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblpmattrans"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(btnNew_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnktotal_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            this.lblmsg.Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lblmsg.Text = "You have no permission";
                return;
            }

            // this.lbtnRefresh_Click(null, null);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = hst["usrid"].ToString();
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string comcod = this.GetComcode();
            this.GetfVoucherNo();
            this.GettVoucherNo();
            string voudat = Convert.ToDateTime(this.txtfdate.Text).ToString("dd-MMM-yyyy");
            string vounum1 = this.lblfVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                            this.lblfVoucherNo.Text.Trim().Substring(2, 2) + this.lblfVoucherNo.Text.Trim().Substring(5);
            string voudat2 = Convert.ToDateTime(this.txttdate.Text).ToString("dd-MMM-yyyy");
            string vounum2 = this.lbltVoucherNo.Text.Trim().Substring(0, 2) + voudat2.Substring(7, 4) +
                           this.lbltVoucherNo.Text.Trim().Substring(2, 2) + this.lbltVoucherNo.Text.Trim().Substring(5);
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo1 = this.txtSrinfo.Text.Trim();
            string srinfo2 = this.txttSrinfo.Text.Trim();
            string vounarration1, vounarration2;//, vouno, voutype, cactcode = "000000000000";
                                                //double trnamt;
            vounarration1 = this.txtNarration.Text.Trim();
            vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
          

            try
            {
                DataTable dt = (DataTable)ViewState["tblpmattrans"];
                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dt);
                ds1.Tables[0].TableName = "tbl1";

                bool result = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER_2", "INSERUPDATEINTCOMPMATTRANS", ds1, null, null, voudat, PostedByid, Postedtrmid,
                    PostedSession, Posteddat, refnum, srinfo1, vounarration1, vounarration2);

                if (!result)
                {
                    this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                else
                {
                    this.lblmsg.Text = "Update Successfully.";
                }
                this.lblmsg.Text = "Update Successfully.";

                this.lbtnRefresh.Enabled = false;

                this.txtfdate.Enabled = false;

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Inter Company Payment Voucher";
                string eventdesc = "Update Voucher";
                string eventdesc2 = vounum2;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblANMgsBox")).Text = "";
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            ViewState.Remove("tblpmattrans");
            this.TableCreate();

            this.lblmsg.Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = true;
            this.grvacc.DataSource = null;
            this.grvacc.DataBind();


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = (this.rbtnList1.SelectedIndex == 0) ? hst["comcod"].ToString() : this.ddlToCompany.SelectedValue.ToString();
                string comnam = (this.rbtnList1.SelectedIndex == 0) ? hst["comnam"].ToString() : this.ddlToCompany.SelectedItem.Text;
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string voudat = Convert.ToDateTime(this.txtfdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string vounum = (this.rbtnList1.SelectedIndex == 0) ? (this.lblfVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                    this.lblfVoucherNo.Text.Trim().Substring(2, 2) + this.lblfVoucherNo.Text.Trim().Substring(5)) : (this.lbltVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                this.lbltVoucherNo.Text.Trim().Substring(2, 2) + this.lbltVoucherNo.Text.Trim().Substring(5));

                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                if (dt.Rows.Count == 0)
                    return;
                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                DataTable dt1 = _ReportDataSet.Tables[1];
                string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                string refnum = dt1.Rows[0]["refnum"].ToString();
                string voutype = dt1.Rows[0]["voutyp"].ToString();
                string venar = dt1.Rows[0]["venar"].ToString();
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
              }
            catch (Exception ex)
            {
                this.lblmsg.Visible = true;
                this.lblmsg.Text = "Error:" + ex.Message;
            }
        }

        private void TableCreate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("comcod", Type.GetType("System.String"));
            dt.Columns.Add("comnam", Type.GetType("System.String"));
            dt.Columns.Add("intfactcode", Type.GetType("System.String"));
            dt.Columns.Add("intfactdesc", Type.GetType("System.String"));
            dt.Columns.Add("pactcode", Type.GetType("System.String"));
            dt.Columns.Add("pactdesc", Type.GetType("System.String"));
            dt.Columns.Add("intactcode", Type.GetType("System.String"));
            dt.Columns.Add("intactdesc", Type.GetType("System.String"));
            dt.Columns.Add("tpactcode", Type.GetType("System.String"));
            dt.Columns.Add("tpactdesc", Type.GetType("System.String"));
            dt.Columns.Add("rsircode", Type.GetType("System.String"));
            dt.Columns.Add("spcfcod", Type.GetType("System.String"));
            dt.Columns.Add("rsirdesc", Type.GetType("System.String"));
            dt.Columns.Add("sirunit", Type.GetType("System.String"));
            dt.Columns.Add("spcfdesc", Type.GetType("System.String"));
            dt.Columns.Add("qty", Type.GetType("System.Double"));
            dt.Columns.Add("rate", Type.GetType("System.Double"));
            dt.Columns.Add("amt", Type.GetType("System.Double"));
            ViewState["tblpmattrans"] = dt;


        }
        private void GettVoucherNo()
        {

            try
            {
                string comcod = this.ddlToCompany.SelectedValue.ToString();
                this.lblmsg.Text = "";

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(ASTUtility.DateFormat(this.txttdate.Text.Trim())))
                {
                    this.lblmsg.Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }
                string VNo = "JV";
                string entrydate = ASTUtility.DateFormat(this.txttdate.Text.Trim());
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];

                this.lbltVoucherNo.Text = dt4.Rows[0]["couvounum"].ToString().Substring(0, 2) + dt4.Rows[0]["couvounum"].ToString().Substring(6, 2) + '-' + dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }

        }
        private void GetfVoucherNo()
        {

            try
            {
                string comcod = this.GetComcode();
                this.lblmsg.Text = "";

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(ASTUtility.DateFormat(this.txtfdate.Text.Trim())))
                {
                    // this.lblmsg.Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }
                string VNo = "JV";
                string entrydate = ASTUtility.DateFormat(this.txtfdate.Text.Trim());
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                this.lblfVoucherNo.Text = dt4.Rows[0]["couvounum"].ToString().Substring(0, 2) + dt4.Rows[0]["couvounum"].ToString().Substring(6, 2) + '-' + dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }

        }
        private void GetToHeadAccount()
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            string filter = "%" ;
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETHEADACCOUNTLIB", filter, "", "", "", "", "", "", "", "");
            this.ddlAcctoHead.DataSource = ds1.Tables[0];
            this.ddlAcctoHead.DataTextField = "actdesc";
            this.ddlAcctoHead.DataValueField = "actcode";
            this.ddlAcctoHead.DataBind();

        }
        private void GetToProject()
        {
            string Fpactcode = this.ddlprjlistfrom.SelectedValue.ToString();

            string comcod = this.ddlToCompany.SelectedValue.ToString();

            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "%", "FxtAst", "", userid, "", "", "", "", "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjectFromList", "%", "", "", "", "", "", "", "", "");
            Session["projectlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlprjlistto.DataTextField = "actdesc1";
            this.ddlprjlistto.DataValueField = "actcode";
            this.ddlprjlistto.DataSource = ds1.Tables[0];
            this.ddlprjlistto.DataBind();

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("actcode='" + Fpactcode + "'");
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count != 0)
            {
                this.ddlprjlistto.SelectedValue = Fpactcode;
            }


        }


        private void GetToCompany()
        {
            string comcod = this.GetComcode();
            string txtSech = ASTUtility.Right(this.ddlAccHead.SelectedValue.ToString(), 4);
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETTOCOMPANY", txtSech, "", "", "", "", "", "", "", "");
            this.ddlToCompany.DataSource = ds1.Tables[0];
            this.ddlToCompany.DataTextField = "comnam";
            this.ddlToCompany.DataValueField = "comcod";
            this.ddlToCompany.DataBind();


        }
        private void GetHeadAccout()  // accountcode
        {


            string comcod = this.GetComcode();
            string filter = "%%";// this.txtsercacc.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETHEADACCOUNT", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();

        }

        private string GetComcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Resourcelist();
        }
        private void GetResCodeleb2()
        {
            string comcod = this.GetComcode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_RESCODE_LEVEL2_ISSUE", "", "", userid, "", "", "", "", "", "");
       
            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "sircode";
            this.ddlcatagory.DataSource = ds1.Tables[0];
            this.ddlcatagory.DataBind();
        }
        private void Resourcelist()
        {
            string comcod = this.GetComcode();
            string ProjectCode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string FindResDesc = "%";
            string curdate = this.txtfdate.Text.ToString().Trim();
            string balcon = "";

            DataTable dt = (DataTable)ViewState["projectlist"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + ProjectCode + "'");
            dt = dv.ToTable();
            string groupcod = this.ddlcatagory.SelectedValue.ToString().Trim();
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
            else
            {
                SearchInfo = "sircode like '"+ groupcod + "%'";
            }



            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "MATCODELIST", ProjectCode, curdate, "%", SearchInfo, "", "", "", "", "");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPROJRESLIST02", ProjectCode, curdate, "", SearchInfo, "", "", "", "", "");
            
            ViewState["tblMat"] = ds1.Tables[0];


            //Session["projectreslist"] = ds1.Tables[0];
            //ViewState["tblspcf"] = ds1.Tables[1];

            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for Store');", true);
                return;
            }
            DataTable t1 = ds1.Tables[0].Copy();
            t1 = t1.DefaultView.ToTable(true, "rsircode", "resdesc");

            this.ddlreslist.DataTextField = "resdesc";
            this.ddlreslist.DataValueField = "rsircode";
            this.ddlreslist.DataSource = t1;
            this.ddlreslist.DataBind();

            //DataView dv1 = ds1.Tables[0].DefaultView;
            //dv1.Sort = "rsircode";
            //DataTable dt2 = dv1.ToTable(true, "rsircode", "resdesc");
            //this.ddlreslist.DataTextField = "resdesc";
            //this.ddlreslist.DataValueField = "rsircode";
            //this.ddlreslist.DataSource = dt2;
            //this.ddlreslist.DataBind();
            ds1.Dispose();
            this.GetSpecification();
        }

        private void GetSpecification()
        {
            string mResCode = this.ddlreslist.SelectedValue.ToString();//.Substring(0, 9);
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblMat"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "'";
            DataTable dt = dv1.ToTable();
            dt = dt.Copy();
            dt = dt.DefaultView.ToTable(true, "spcfcod", "spcfdesc");
            dt.Rows.Add("000000000000", "None");

            DataView _dv = new DataView(dt);
            _dv.Sort = "spcfcod ASC";
            dt = _dv.ToTable();
            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }

            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


            //string mResCode = this.ddlreslist.SelectedValue.ToString().Substring(0, 9);
            //this.ddlResSpcf.Items.Clear();
            //DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            //DataView dv1 = tbl1.DefaultView;
            //dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            //DataTable dt = dv1.ToTable();
            //this.ddlResSpcf.DataTextField = "spcfdesc";
            //this.ddlResSpcf.DataValueField = "spcfcod";
            //this.ddlResSpcf.DataSource = dt;
            //this.ddlResSpcf.DataBind();

        }

        private void GetProjectlist()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComcode();
            string userid = hst["usrid"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "%", "FxtAst", "", userid, "", "", "", "", "");
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjectFromList", "%%", "", "", "", "", "", "", "", "");
            ViewState["projectlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlprjlistfrom.DataTextField = "actdesc1";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            this.Resourcelist();
        }
        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Resourcelist();
            this.GetToProject();
        }

        protected void ddlreslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();
        }

        protected void ddlAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetToCompany();
            this.ddlToCompany_SelectedIndexChanged(null, null);
        }

        protected void lnkselect_Click(object sender, EventArgs e)
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            string pactcode = this.ddlprjlistfrom.SelectedValue.ToString();
            string rescode = this.ddlreslist.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblpmattrans"];
            DataTable dt1 = (DataTable)ViewState["tblMat"];
            DataRow[] dr1 = dt.Select("comcod = '" + comcod + "' and pactcode = '" + pactcode + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            double qty = Convert.ToDouble("0" + this.txtfqty.Text.Trim());
            if (dr1.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();
                drforgrid["comcod"] = comcod;  //comnam
                drforgrid["comnam"] = this.ddlToCompany.SelectedItem.Text;  //
                drforgrid["intfactcode"] = this.ddlAccHead.SelectedValue.ToString();
                drforgrid["intfactdesc"] = this.ddlAccHead.SelectedItem.Text.Substring(7);
                drforgrid["pactcode"] = pactcode;
                drforgrid["pactdesc"] = this.ddlprjlistfrom.SelectedItem.Text.Substring(7);
                drforgrid["intactcode"] = this.ddlAcctoHead.SelectedValue.ToString();
                drforgrid["intactdesc"] = this.ddlAcctoHead.SelectedItem.Text.Substring(7);
                drforgrid["tpactcode"] = this.ddlprjlistto.SelectedValue.ToString();

                drforgrid["rsircode"] = rescode;
                drforgrid["rsirdesc"] = this.ddlreslist.SelectedItem.Text.Substring(7);
                drforgrid["spcfcod"] = spcfcod;
                drforgrid["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text;
                DataRow[] dr = dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
                drforgrid["sirunit"] = "";//(dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["sirunit"].ToString();
                drforgrid["qty"] = qty;
                drforgrid["rate"] = (dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"].ToString();
                drforgrid["amt"] = qty * Convert.ToDouble((dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"]);
                dt.Rows.Add(drforgrid);
            }

            else
            {
                dr1[0]["qty"] = qty;
                dr1[0]["rate"] = (dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"].ToString();
                dr1[0]["amt"] = qty * Convert.ToDouble((dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"]);
            }
            ViewState["tblpmattrans"] = dt;
            this.Data_Bind();
        }

        protected void ddlToCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetToProject();
            this.GetToHeadAccount();
            this.GettVoucherNo();
        }

        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblpmattrans"];

            string comcod = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvcomcod")).Text.Trim();
            string intfactcode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvintfactcode")).Text.Trim();
            string pactcode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvpactcode")).Text.Trim();
            string rsircode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvrsircode")).Text.Trim();
            string intactcode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvintactcode")).Text.Trim();





            int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "comcod='" + comcod + "' or intfactcode= '" + intfactcode + "' or pactcode= '" + pactcode + "' or rsircode= '" + rsircode + "'";
            ViewState.Remove("tblpmattrans");
            ViewState["tblpmattrans"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            this.GetfVoucherNo();
            this.GettVoucherNo();
        }
    }
}