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

namespace SPEWEB.F_11_RawInv
{
    public partial class MaterialsTransfer : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS TRANSFER";

                this.txtCurTransDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");// GetStdDate(DateTime.Today.ToString("dd-MMM-yyyy"));//XXXXXXXXXXXXXX


                this.GetLCCode();
                CommonButton();


                if (this.ddlprjlistfrom.Items.Count == 0)
                {
                    string geno = this.Request.QueryString["genno"] ?? "";
                    if (geno.Length > 0 && this.Request.QueryString["Type"].ToString() == "Material")
                    {
                        this.GetStorrNames();
                        this.lbtnOk_Click(null, null);
                        this.Load_Dates_And_Trans_No();
                        this.Load_Project_From_Combo();
                        this.ddlprjlistfrom.Enabled = false;
                        this.ddlprjlistto.Enabled = false;
                        this.Order.Visible = false;
                        this.Material.Visible = false;
                        this.specification.Visible = false;
                        this.addBtn.Visible = false;

                    }
                    else
                    {
                        this.Load_Dates_And_Trans_No();
                        this.Load_Project_From_Combo();
                    }
                    this.tableintosession();
                }

                this.lblReqNarr.Visible = false;
                this.txtNarr.Visible = false;
            }
            
        }

        public void CommonButton()
        {
            //fasfdf
            // ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            // ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //   ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
         //   ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetStorrNames()
        {
            string comcod = GetCompCode();
            string gpasno = this.Request.QueryString["genno"].ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETGATEPASSSTORLIST", gpasno,
                      "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["fromstortostore"] = ds2.Tables[0];

            
            
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
           // ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string tmsg = "";



            //DataTable dtuser = (DataTable)Session["UserLog"];
            //string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            //string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            //string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            //string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            //string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
            //string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");





            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            if (ddlPrevISSList.Items.Count == 0)
                this.GetMatTrns();
            string transno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
            string Refno = this.txtrefno.Text.ToString();




            if (Refno.Length == 0)
            {
                 tmsg = "Ref. No. Should Not Be Empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                //this.lblmsg1.Text = "Ref. No. Should Not Be Empty";
                return;
            }

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CHECKEDDUPREFNO", Refno, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                //  tmsg = "Ref. No. Already Exists";
                //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                ////this.lblmsg1.Text = "Ref. No. Should Not Be Empty";
              //  return;

            }



            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("trnno <>'" + transno + "'");
                DataTable dt1 = dv1.ToTable();
                if (dt1.Rows.Count == 0)
                    ;
                else
                {
                      tmsg = "Found Duplicate Ref. No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                    //this.lblmsg1.Text = "Ref. No. Should Not Be Empty";
                    return;

                   
                }
            }


            string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim() == "" ? "000000000000" : this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim() == "" ? "000000000000" : this.ddlprjlistto.SelectedValue.ToString().Trim();
            string supcode = this.ddlCompany.SelectedValue.ToString().Trim();
            string narration = this.txtNarr.Text.Trim();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_03", "UpdateTransferInf", "prjtransinfb", transno, fromprj, toprj, curdate, Refno, Posteddat, PostedByid, Posttrmid, PostSession, supcode, "", "", "", "");


            foreach (DataRow dr in dt.Rows)
            {

                string trsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string mlccod = dr["mlccod"].ToString().Trim();
                string tqty = dr["qty"].ToString().Trim();
                string trate = dr["rate"].ToString().Trim();
                string tamt = dr["amt"].ToString().Trim();

                string tmlccod = dr["tmlc"].ToString().Trim();
                string getpno = dr["getpno"].ToString().Trim();
                string mtreqno = dr["mtreqno"].ToString().Trim();
                

                result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_03", "UpdateTransferInf", "prjtransinf", transno, trsircode,
                   spcfcod, mlccod, tqty, tamt, tmlccod, getpno, mtreqno, "", "");
                if (!result)
                {
                    
                    string tmsgs = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsgs + "');", true);
                    //this.lblmsg1.Text = "Ref. No. Should Not Be Empty";
                    return;
                }
            }

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //  string trsircode=dt.Rows[i]["rsircode"].ToString().Trim();
            //  string tunit=dt.Rows[i]["sirunit"].ToString().Trim();
            //  string tqty=dt.Rows[i]["qty"].ToString().Trim();
            //  string trate=dt.Rows[i]["rate"].ToString().Trim();
            //  string tamt = dt.Rows[i]["amt"].ToString().Trim();

            //  bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UpdateTransferInf", transno, fromprj, toprj, trsircode,
            //      tunit, tqty, trate, tamt, curdate, Refno, PostedByid, Posttrmid, PostSession, Posteddat, "");
            //}

              tmsg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);

            
            this.txtCurTransDate.Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Transfer";
                string eventdesc = "Update New QTY & RATE";
                string eventdesc2 = "From " + this.ddlprjlistfrom.SelectedItem.ToString() + " To " + this.ddlprjlistto.SelectedItem.ToString() + " - " + transno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetMatTrns()
        {

            string comcod = GetCompCode();
            string mTRNNO = "NEWTRNS";

            if (this.ddlPrevISSList.Items.Count > 0)
                mTRNNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mTRNDAT =this.txtCurTransDate.Text.Trim().ToString();
            if (mTRNNO == "NEWTRNS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", mTRNDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 6);
                    this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxtrnno1";
                    this.ddlPrevISSList.DataValueField = "maxtrnno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();
                }
            }






        }

        protected void Load_Dates_And_Trans_No()
        {


            string comcod = this.GetCompCode();
            this.txtCurTransDate.Text = this.txtCurTransDate.Text.Trim().ToString();
            this.Last_trn_no();


        }
        protected string GetStdDate(string Date1)
        {
            //Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd-MMM-yyyy") : Date1);
           // string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
           // Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(4, 3))] + "-" + Date1.Substring(8, 4);
            return Date1;

        }
        protected void tableintosession()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("rsircode", Type.GetType("System.String"));
            dttemp.Columns.Add("spcfcod", Type.GetType("System.String"));
            dttemp.Columns.Add("resdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("spcfdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("sirunit", Type.GetType("System.String"));
            dttemp.Columns.Add("qty", Type.GetType("System.String"));
            dttemp.Columns.Add("rate", Type.GetType("System.String"));
            dttemp.Columns.Add("amt", Type.GetType("System.Double"));
            dttemp.Columns.Add("reqno", Type.GetType("System.String"));

            Session["sessionforgrid"] = dttemp;

        }

        protected void Load_Project_From_Combo()
        {
            
            DataTable dt = (DataTable)ViewState["fromstortostore"];
            if (dt == null)
            {
                return;
            }
            string type = dt.Rows.Count>0? dt.Rows[0]["mtreqno"].ToString():"%";
            string typecode = "";
            if (type.Length > 0)
            {   
                type = ASTUtility.Left(type, 3).ToString();
                typecode = (type == "MTR" ? "15%" : 
                    type == "LNR" ? "23040001%" : 
                    type == "JMT" ? "15%" :
                    type == "RTR" ? "15%" : "");
            }
            else
            {                 
                type = ASTUtility.Left(type, 3).ToString();
                typecode = (type == "MTR" ? "15%" : 
                    type == "LNR" ? "23040001%" :
                    type == "JMT" ? "15%" :
                    type == "RTR" ? "15%" : "");
            }

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_ACINF_DATA", typecode, "", "", "", "", "", "", "", "");
            Session["projectlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;
            this.ddlprjlistfrom.DataTextField = "actdesc1";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            if (type.Length>0)
            {
                this.ddlprjlistfrom.SelectedValue = dt.Rows.Count <= 0?"": dt.Rows[0]["tfpactcode"].ToString();
                
            }
            
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }
        protected void Load_Project_To_Combo()
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["fromstortostore"];
            string type = dt.Rows.Count > 0 ? dt.Rows[0]["mtreqno"].ToString() : "";
            string typecode = "";
            if (type.Length > 0)
            { 
                type = ASTUtility.Left(type, 3).ToString();
               typecode = (type == "MTR" ? "15%" :
                               type == "LNR" ? "15%" :
                               type == "JMT" ? "15%" :
                               type == "RTR" ? "230400010001%" : "");

            }
            else
            {
                type = this.lblCurTransNo1.Text.Trim().ToString();
                type = ASTUtility.Left(type, 3).ToString();
             
                typecode = (type == "MTR" ? "15%" :
                            type == "LNR" ? "15%" :
                            type == "RTR" ? "230400010001%" : "");
            }

            if (type == "LNR" || type == "RTR" || type == "JMT")
            {

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetSuplliers", "99%", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.ddlCompany.DataTextField = "sirdesc1";
                this.ddlCompany.DataValueField = "sircode";
                this.ddlCompany.DataSource = ds1.Tables[0];
                this.ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("<-- Select -->", "000000000000"));
                this.ddlCompany.SelectedValue= dt.Rows.Count > 0 ? dt.Rows[0]["actcode"].ToString() : "000000000000";
            }



            if (type == "LNR" || type == "RTR" || type == "JMT")
            {
                
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_ACINF_DATA", typecode, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                this.ddlprjlistto.DataTextField = "actdesc1";
                this.ddlprjlistto.DataValueField = "actcode";
                this.ddlprjlistto.DataSource = ds1.Tables[0];
                this.ddlprjlistto.DataBind();

            }
            else
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_ACINF_DATA", typecode, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                DataTable dt1s= ds1.Tables[0];
                //this.ddlprjlistto.DataTextField = "actdesc1";
                //this.ddlprjlistto.DataValueField = "actcode";
                //this.ddlprjlistto.DataSource = ds1.Tables[0];
                //this.ddlprjlistto.DataBind();

                //DataRow[] projectrow = dt.Select("ttpactcode <> '" + this.ddlprjlistfrom.SelectedValue.ToString().Trim() + "'");
                string actcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
                DataView dv1 = dt1s.DefaultView;
                dv1.RowFilter = "actcode not in ('" + actcode + "')";
                DataTable dt1 = dv1.ToTable();

                this.ddlprjlistto.DataTextField = "actdesc1";
                this.ddlprjlistto.DataValueField = "actcode";
                this.ddlprjlistto.DataSource = dt1;
                this.ddlprjlistto.DataBind();
            }

            ddlprjlistto.Items.Insert(0, new ListItem("<-- Select -->", ""));
            if (type.Length > 0)
            {
                this.ddlprjlistto.SelectedValue = dt.Rows.Count <= 0?"": dt.Rows[0]["ttpactcode"].ToString();

            }


        }
        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_To_Combo();

        }
        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Load_Project_Res_Combo();
        }

        private string CompBalConMat()
        {

            string comcod = this.GetCompCode();
            string conbal = "";
            switch (comcod)
            {
                case "3301":
                case "1301":
                case "3101":
                    conbal = "notcon";
                    break;

                default:
                    conbal = "GetProjResList";
                    break;


            }

            return conbal;

        }

        protected void Load_Project_Res_Combo()
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string mlccod = this.ddlmlccode.SelectedValue.ToString().Trim() + "%";
            string FindResDesc = "%";
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            string balcon = this.CompBalConMat();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjResList", ProjectCode, curdate, FindResDesc, balcon, mlccod, "", "", "", "");
            ViewState["projectreslist"] = ds1.Tables[0];
            // ViewState["tblspcf"] = ds1.Tables[1];

            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.ddlreslist.DataSource = null;
                this.ddlreslist.DataBind();
                this.ddlResSpcf.DataSource = null;
                this.ddlResSpcf.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for Store');", true);
                return;
            }


            DataView dv = ds1.Tables[0].DefaultView;
            dv.Sort = "rsircode";
            DataTable dt = dv.ToTable(true, "rsircode", "sirdesc");
            this.ddlreslist.DataTextField = "sirdesc";
            this.ddlreslist.DataValueField = "rsircode";
            this.ddlreslist.DataSource = dt;
            this.ddlreslist.DataBind();
            ds1.Dispose();


            this.GetSpecification();
        }

        private void GetSpecification()
        {
            string mResCode = this.ddlreslist.SelectedValue.ToString();
            //string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["projectreslist"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "'";
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {

            string rescode = this.ddlreslist.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)ViewState["projectreslist"];
            DataRow[] projectrow1 = dt1.Select("rsircode = '" + rescode + "'");
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rescode + " and spcfcod = " + spcfcod + "'");

            if (projectrow2.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();

                drforgrid["rsircode"] = projectrow1[0]["rsircode"];
                drforgrid["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                drforgrid["resdesc"] = projectrow1[0]["sirdesc"];
                drforgrid["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text;
                drforgrid["sirunit"] = projectrow1[0]["sirunit"];
                drforgrid["mlccod"] = projectrow1[0]["mlccod"];
                drforgrid["mlcdesc"] = projectrow1[0]["mlcdesc"];
                drforgrid["qty"] = projectrow1[0]["qty"];
                drforgrid["stkqty"] = projectrow1[0]["stkqty"];
                drforgrid["rate"] = projectrow1[0]["rate"];
                drforgrid["amt"] = projectrow1[0]["amt"];


                dt.Rows.Add(drforgrid);
            }
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();



        }



        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblmattrns"];
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {

                double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                double rat = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + i;
                dt1.Rows[rowindex]["qty"] = qty;
                double damt = qty * rat;
                dt1.Rows[i]["rate"] = rat;
                dt1.Rows[i]["amt"] = damt;
            }
            ViewState["tblmattrns"] = dt1;
        }




        private void GetLCCode()
        {
            string comcod = GetCompCode();
            string txtsrch = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "DTLLCLIST", "", txtsrch, "", "", "", "", "", "", "");
            ;
            if (ds1 == null)
                return;
            this.ddlmlccode.DataTextField = "actdesc";
            this.ddlmlccode.DataValueField = "actcode";
            this.ddlmlccode.DataSource = ds1.Tables[0];
            this.ddlmlccode.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.grvacc.Columns[6].FooterText.Length > 0)
                this.grvacc.Columns[6].FooterText = "";
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                // this.pnlgrd.Visible = !(this.chkGatePass.Checked);
                this.pnlGatePass.Visible = (this.chkGatePass.Checked);
                this.ddlGatePass.Enabled = false;
                this.Order.Visible = true;
                this.lnkselect0.Visible = true;
                this.Material.Visible = true;
                this.specification.Visible = true;
                this.ddlprjlistfrom.Enabled = false;
                this.ddlCompany.Enabled = false;
                this.ddlprjlistto.Enabled = false;
                this.lbtnPrevTransList.Visible = false;
                this.ddlPrevISSList.Visible = false;
                this.chkGatePass.Visible = false;
                this.lblpage.Visible = true;
                this.ddlpagesize.Visible = true;

                string geno = this.Request.QueryString["genno"] ?? "";
                if (geno.Length > 0 && this.Request.QueryString["Type"].ToString() == "Material")
                {
                    this.GetGatepassData();
                }
                else  
                {
                    

                    this.GetMatTransfer();
                    this.ImgbtnFindRes_Click(null, null);
                }
            }
            else
            {
                this.lnkselect0.Visible = false;
                this.lblpage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.ddlprjlistfrom.Enabled = true;
                this.ddlprjlistto.Enabled = true;
                this.lbtnPrevTransList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.Order.Visible = false;
                this.Material.Visible = false;
                this.specification.Visible = false;
                this.txtCurTransDate.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.Last_trn_no();
                // this.pnlgrd.Visible = false;
                this.chkGatePass.Visible = false;
                this.chkGatePass.Checked = false;
                this.chkGatePass_CheckedChanged(null, null);
                this.pnlGatePass.Visible = false;
                this.ddlGatePass.Enabled = true;
                this.txtrefno.Text = "";
                this.txtNarr.Text = "";
                //    this.lblmsg1.Text = "";
                //   this.lblVoucherNo.Text = "";
                lbtnOk.Text = "Ok";
                this.ddlPrevISSList.Items.Clear();
                this.ddlGatePass.Items.Clear();

            }


        }



        private void GetMatTransfer()
        {


            ViewState.Remove("tblmattrns");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mTRNNo = "NEWTRNS";
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurTransDate.Enabled = false;
                mTRNNo = this.ddlPrevISSList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "PrevTransferInfo", mTRNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];



            if (this.ddlGatePass.Items.Count > 0)
            {
                string gatepno = this.ddlGatePass.SelectedValue.ToString();
                DataTable dtres = (DataTable)ViewState["tblgatepno"];
                DataRow[] dr = dtres.Select("getpno='" + gatepno + "'");
                this.ddlprjlistfrom.SelectedValue = dr[0]["tfpactcode"].ToString();

                this.ddlprjlistfrom_SelectedIndexChanged(null, null);
                this.ddlprjlistto.SelectedValue = dr[0]["ttpactcode"].ToString();





            }
            if (mTRNNo == "NEWTRNS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurTransNo1.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 6);
                this.txtCurTransNo2.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(6);
                return;
            }

            //this.pnlgrd.Visible=ds1.Tables[0].Rows[0]["getpno"].ToString().Trim().Length>0?false:true;
            this.ddlprjlistfrom.SelectedValue = ds1.Tables[1].Rows[0]["tfpactcode"].ToString();

            this.ddlprjlistfrom_SelectedIndexChanged(null, null);
            this.ddlprjlistto.SelectedValue = ds1.Tables[1].Rows[0]["ttpactcode"].ToString();


            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["tdate"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            //  this.txtNarr.Text = ds1.Tables[1].Rows[0]["narration"].ToString();
            this.lblCurTransNo1.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(6);
            //this.lblVoucherNo.Text = ds1.Tables[1].Rows[0]["vounum"].ToString().Trim();
            this.Data_Bind();
        }


    

        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];

            if (dt1 == null)
                return;
            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;




        }




        protected void Last_trn_no()
        {

            string comcod = this.GetCompCode();
            string date = this.txtCurTransDate.Text;
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", date, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;
            this.lblCurTransNo1.Text = ds.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(6);

        }
        protected void lbtnPrevTransList_Click(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();
        }
        protected void Load_Prev_Trans_List()
        {

            string comcod = this.GetCompCode();
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvacc.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintMatTransferGen();





        }


        
        private void PrintMatTransferGen()
        {


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string mISUNO = this.lblCurTransNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurTransDate.Text.Trim()), 4) + this.lblCurTransNo1.Text.Trim().Substring(3, 2) + this.txtCurTransNo2.Text.Trim();
            string FrmPrjCode = this.ddlprjlistfrom.SelectedValue.ToString();
            string ToPrjCode = this.ddlprjlistto.SelectedValue.ToString();
            string MatCode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvMatCode")).Text.Trim();
            string scpfcod = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvSpcfCod")).Text.Trim();
            string mlccod = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvMlccod")).Text.Trim();


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELMATTRANSFER", mISUNO, MatCode, scpfcod, mlccod, "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmattrns");
            ViewState["tblmattrns"] = dv.ToTable();
            this.Data_Bind();

        }
        protected void ddlreslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();
        }
        protected void chkGatePass_CheckedChanged(object sender, EventArgs e)
        {
            this.ddlGatePasslabel.Visible = this.chkGatePass.Checked;

            this.ddlGatePass.Visible = this.chkGatePass.Checked;
        }

        protected void lbtnGatePassNo_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblgatepinfo");
            ViewState.Remove("tblRes");
            string comcod = this.GetCompCode();
            string SerchText = "%%";
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMTREQGPASSLIST", CurDate1, SerchText, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ViewState["tblgatepinfo"] = ds1.Tables[0];
            ViewState["tblRes"] = ds1.Tables[1];
            ViewState["tblgatepno"] = ds1.Tables[2];
            this.ddlGatePass.DataTextField = "textfield";
            this.ddlGatePass.DataValueField = "valuefiled";
            this.ddlGatePass.DataSource = ds1.Tables[2];
            this.ddlGatePass.DataBind();

        }
        protected void lbtnFindResgp_Click(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblRes"];
            string gatepno = this.ddlGatePass.SelectedValue.ToString();
            DataView dv = dtres.DefaultView;
            dv.RowFilter = "getpno in ('" + gatepno + "')";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";
            DataTable dtd = dv.ToTable();
            this.ddlreslistgp.DataTextField = "rsirdesc";
            this.ddlreslistgp.DataValueField = "rsircode";
            this.ddlreslistgp.DataSource = dv.ToTable();
            this.ddlreslistgp.DataBind();
            this.ddlreslistgp_SelectedIndexChanged(null, null);
        }
        protected void lnkselectgp_Click(object sender, EventArgs e)
        {

            //string gatepno = this.ddlResSpcfgp.SelectedValue.ToString().Substring(0, 14);
            ////string mProgNo = this.ddlResList.SelectedValue.ToString().Substring(14, 14);
            //string rescode = this.ddlResSpcfgp.SelectedValue.ToString().Substring(14, 12);
            //string spcfcod = this.ddlResSpcfgp.SelectedValue.ToString().Substring(26, 12);

            //DataTable dt = (DataTable)ViewState["tblmattrns"];
            //DataTable dt1 = (DataTable)ViewState["tblgatepinfo"];
            //DataRow[] drgp = dt1.Select("getpno = '" + gatepno + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            //DataRow[] dr1 = dt.Select("getpno = '" + gatepno + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");

            //if (dr1.Length == 0)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["mtreqno"] = drgp[0]["mtreqno"];
            //    dr["mtrref"] = drgp[0]["mtrref"];
            //    dr["getpno"] = drgp[0]["getpno"];
            //    dr["getpref"] = drgp[0]["getpref"];

            //    dr["rsircode"] = drgp[0]["rsircode"];
            //    dr["spcfcod"] = drgp[0]["spcfcod"];
            //    dr["resdesc"] = drgp[0]["rsirdesc"];
            //    dr["spcfdesc"] = drgp[0]["spcfdesc"];
            //    dr["sirunit"] = drgp[0]["rsirunit"];
            //    dr["mtrfqty"] = drgp[0]["mtrfqty"];
            //    dr["qty"] = drgp[0]["getpqty"];
            //    dr["rate"] = drgp[0]["rate"];
            //    dr["amt"] = drgp[0]["getpamt"];
            //    dr["reqno"] = "";
            //    dt.Rows.Add(dr);
            //}
            //ViewState["tblmattrns"] = dt;
            //this.Data_Bind();

        }

        protected void lnkselectgpAll_Click(object sender, EventArgs e)
        {


            // string gatepno = this.ddlResSpcfgp.SelectedValue.ToString().Substring(0, 14);

            // DataTable dt = (DataTable)ViewState["tblmattrns"];
            // DataTable dt1 = (DataTable)ViewState["tblgatepinfo"];
            //// DataRow[] drgp = dt1.Select("getpno = '" + gatepno + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            // DataRow[] dr1 = dt.Select("getpno = '" + gatepno + "'");
            // DataView dvg = dt1.DefaultView;
            // dvg.RowFilter = ("getpno = '" + gatepno + "'");
            // DataTable dtg = dvg.ToTable();

            // if (dr1.Length == 0)
            // {


            //     foreach (DataRow dr2 in dtg.Rows)
            //     {
            //         DataRow dr = dt.NewRow();
            //         dr["mtreqno"] = dr2["mtreqno"];
            //         dr["mtrref"] = dr2["mtrref"];
            //         dr["getpno"] = dr2["getpno"];
            //         dr["getpref"] = dr2["getpref"];
            //         dr["rsircode"] = dr2["rsircode"];
            //         dr["spcfcod"] = dr2["spcfcod"];
            //         dr["resdesc"] = dr2["rsirdesc"];
            //         dr["spcfdesc"] = dr2["spcfdesc"];
            //         dr["sirunit"] = dr2["rsirunit"];
            //         dr["mtrfqty"] = dr2["mtrfqty"];
            //         dr["qty"] = dr2["getpqty"];
            //         dr["rate"] = dr2["rate"];
            //         dr["amt"] = dr2["getpamt"];
            //         dr["reqno"] = "";
            //         dt.Rows.Add(dr);            
            //     }


            // }
            // ViewState["tblmattrns"] = dt;
            // this.Data_Bind();
        }
        protected void ddlreslistgp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dtres = (DataTable)ViewState["tblgatepinfo"];
            //string gatepno = this.ddlGatePass.SelectedValue.ToString();
            //string rsircode = this.ddlreslistgp.SelectedValue.ToString();
            //DataView dv = dtres.DefaultView;

            //dv.RowFilter = "getpno='" + gatepno + "' and  rsircode='" + rsircode + "'";
            ////dv.RowFilter = "prcod not in('" + ProdCode + "')";

            //this.ddlResSpcfgp.DataTextField = "textfield";
            //this.ddlResSpcfgp.DataValueField = "valuefiled";
            //this.ddlResSpcfgp.DataSource = dv.ToTable();
            //this.ddlResSpcfgp.DataBind();

        }

        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_Res_Combo();
        }

       private void GetGatepassData()
        {
            string fstore = this.ddlprjlistfrom.SelectedValue.ToString();
            string tstore = this.ddlprjlistto.SelectedValue.ToString();
            string genno = this.Request.QueryString["genno"].ToString();
            string comcod = this.GetCompCode();
            string curdate = this.txtCurTransDate.Text.ToString().Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPASSDATARECOVED", genno, curdate,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = (DataTable)ViewState["tblmattrns"];
            this.grvacc.DataBind();

            //this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            //((LinkButton)this.grvacc.FooterRow.FindControl("lnkupdate")).Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            this.FooterCalCulation();


        }
    }
}