using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
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
    public partial class StoreMtTrnsReqEntry : System.Web.UI.Page
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
                //  DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                switch (Type)
                {
                    case "Entry":
                        ((Label)this.Master.FindControl("lblTitle")).Text = "Create Requsition";
                        break;
                    case "LoanEntry":
                        ((Label)this.Master.FindControl("lblTitle")).Text = "Create Loan Requsition";
                        break;
                    case "RetEntry":
                        ((Label)this.Master.FindControl("lblTitle")).Text = "Create Return Requsition";
                        break;
                    case "JobTrans":
                        ((Label)this.Master.FindControl("lblTitle")).Text = "Job Work Materials Transfer";
                        break;

                }



                this.ddlLoanNo.Enabled = false;
                this.lbtnSync.Enabled = false;
                this.ddlLoanNo.Items.Insert(0, new ListItem("Select Loan No", "00000000000000"));
                this.GetLCCode();
                this.Load_Order_From_Combo();

                if (this.ddlprjlistfrom.Items.Count == 0)
                {

                    this.Load_Dates_And_Trans_No();
                    this.getLablData();
                    this.GetStoreList();
                    this.GetLCCode();
                    this.Load_Project_From_Combo();
                    this.Load_Order_From_Combo();

                    this.tableintosession();
                }



                if ("Edit" == this.Request.QueryString["Type"].ToString().Trim())
                {

                    SelectType();
                }
                if ("approve" == this.Request.QueryString["Type"].ToString().Trim())
                {
                    SelectType();

                }

                CommonButton();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnLedger_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkupdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnktotal_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void getLablData()
        {
            string type = "";
            if (this.Request.QueryString["genno"].Length > 0)
            {
                type = this.Request.QueryString["genno"].ToString();
                type = ASTUtility.Left(type, 3).ToString();
            }
            else
            {
                type = this.lblCurTransNo1.Text.Trim().ToString();
                type = ASTUtility.Left(type, 3).ToString();
            }


            if (type == "MTR")
            {
                //this.cusRef_field.InnerText = "MTRF No:";
                //this.FromDate_field.InnerText = "From Store";
                //this.toStore_field.InnerText = "To Store";
                this.loanpnl.Visible = false;
                this.loanpnl2.Visible = true;
                this.loanpnl3.Visible = true;



            }
            else if (type == "LNR")
            {
                //this.cusRef_field.InnerText = "LNRF No:";
                //this.FromDate_field.InnerText = "Loan for Store";
                //this.toStore_field.InnerText = "Loan From Company";
                this.divDdlprjlistfrom.Visible = false;
                this.loanpnl.Visible = true;
                this.loanpnl2.Visible = false;
                this.loanpnl3.Visible = true;
            }
            else if (type == "RTR")
            {
                this.divDdlprjlistto.Visible = false;
                this.loanpnl.Visible = true;
                this.loanpnl2.Visible = true;
                this.loanpnl3.Visible = false;
                this.divDdlLoanNo.Visible = true;
                this.grvacc.Columns[3].Visible = false;
                this.grvacc.Columns[4].Visible = false;
                this.grvacc.Columns[5].Visible = true;
                this.grvacc.Columns[6].Visible = false;
                this.grvacc.Columns[7].Visible = true;
            }
            else
            {
                this.divDdlprjlistto.Visible = false;
                this.loanpnl.Visible = true;
                this.loanpnl2.Visible = true;
                this.loanpnl3.Visible = false;

            }
        }

        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "approve":
                //lbtnOk_Click(null, null);
                //break;
                case "Edit":
                    string ReqNo = this.Request.QueryString["genno"].ToString().Trim();
                    string date = this.Request.QueryString["date"].ToString().Trim();
                    this.txtCurTransDate.Text = date;


                    this.GetStoreList();




                    lbtnPrevTransList_Click(null, null);
                    this.ddlPrevISSList.SelectedValue = ReqNo;
                    lbtnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }
        public bool RateEditable()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst["usrid"].ToString() == hst["comcod"].ToString() + "001")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetMatTrns()
        {
            string qtype = this.Request.QueryString["Type"].ToString() == "Entry" ? "MTR" :
                           this.Request.QueryString["Type"].ToString() == "LoanEntry" ? "LNR" :
                           this.Request.QueryString["Type"].ToString() == "JobTrans" ? "JMT" :
                           this.Request.QueryString["Type"].ToString() == "RetEntry" ? "RTR" : "";


            string comcod = GetCompCode();
            //string mREQNO = "NEWISS";
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mREQDAT = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString();
            //if (mREQNO == "NEWISS")
            //{
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTMTRNO", mREQDAT,
                   qtype, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxmtrno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 6);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnno1";
                this.ddlPrevISSList.DataValueField = "maxmtrno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();
            }

        }

        protected void Load_Dates_And_Trans_No()
        {


            string comcod = this.GetCompCode();
            this.txtCurTransDate.Text = GetStdDate(DateTime.Today.ToString("dd-MMM-yyyy"));//XXXXXXXXXXXXXX
            this.Last_trn_no();


        }
        protected string GetStdDate(string Date1)
        {
            //Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd-MMM-yyyy") : Date1);
            //string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(4, 3))] + "-" + Date1.Substring(8, 4);
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
        private void GetLCCode()
        {
            string comcod = GetCompCode();
            string type = this.lblCurTransNo1.Text.Trim().ToString();
            type = type == ""? "": ASTUtility.Left(type, 3).ToString();
            string txtsrch = "%";
            if (type == "JMT")
            {
             txtsrch = type;

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "DTLLCLIST", "", txtsrch, "", "", "", "", "", "", "");
            
            if (ds1 == null)
                return;


            Session["mlcorderList"] = ds1.Tables[0];


        }
        private void GetStoreList()
        {
            string typecode = "";
            if (this.Request.QueryString["genno"].Length > 0)
            {
                string type = this.Request.QueryString["genno"].ToString();
                type = ASTUtility.Left(type, 3).ToString();

                typecode = (type == "MTR" ? "15%" :
                               type == "LNR" ? "230400010001%" :
                               type == "RTR" ? "15%" : "");
            }
            else
            {
                string type = this.lblCurTransNo1.Text.Trim().ToString();
                type = ASTUtility.Left(type, 3).ToString();

                typecode = (type == "MTR" ? "15%" :
                               type == "LNR" ? "230400010001%" :
                               type == "RTR" ? "15%" : type == "JMT" ? "15%" : "");

            }


            string comcod = this.GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_ACINF_DATA", typecode, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            Session["projectlist"] = ds1.Tables[0];

        }
        protected void Load_Project_From_Combo()
        {

            DataTable dt = (DataTable)Session["projectlist"];

            this.ddlprjlistfrom.DataTextField = "actdesc1";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = dt;
            this.ddlprjlistfrom.DataBind();
            this.ddlprjlistfrom.Items.Insert(0, new ListItem("Select Store", "000000000000"));

            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }
        protected void Load_Project_To_Combo()
        {

            string typecode = "";
            string type = "";
            if (this.Request.QueryString["genno"].Length > 0)
            {
                type = this.Request.QueryString["genno"].ToString();
                type = ASTUtility.Left(type, 3).ToString();

                typecode = (type == "MTR" ? "15%" :
                               type == "LNR" ? "15%" :
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
                string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetSuplliers", "99%", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                this.ddlCompany.DataTextField = "sirdesc1";
                this.ddlCompany.DataValueField = "sircode";
                this.ddlCompany.DataSource = ds1.Tables[0];
                this.ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Supplier", "000000000000"));


            }



            if (type == "LNR" || type == "RTR")
            {
                string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GET_ACINF_DATA", typecode, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                this.ddlprjlistto.DataTextField = "actdesc1";
                this.ddlprjlistto.DataValueField = "actcode";
                this.ddlprjlistto.DataSource = ds1.Tables[0];
                this.ddlprjlistto.DataBind();
                if (type != "RTR")
                {
                    ddlprjlistto.Items.Insert(0, new ListItem("Select Store", "000000000000"));

                }

            }
            else
            {
                DataTable dt = (DataTable)Session["projectlist"];
                DataRow[] projectrow = dt.Select("actcode <> '" + this.ddlprjlistfrom.SelectedValue.ToString().Trim() + "'");
                string actcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "actcode not in ('" + actcode + "')";
                DataTable dt1 = dv1.ToTable();

                this.ddlprjlistto.DataTextField = "actdesc1";
                this.ddlprjlistto.DataValueField = "actcode";
                this.ddlprjlistto.DataSource = dt1;
                this.ddlprjlistto.DataBind();
                ddlprjlistto.Items.Insert(0, new ListItem("Select Store", "000000000000"));

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

        protected void Load_Project_Res_Combo()
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string mlccode = this.ddlmlccode.SelectedValue.ToString().Trim();

            string FindResDesc = "%";
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            string stockcheck = (this.CheckStckChkr.Checked == true) ? "Y" : "N";
            if (Request.QueryString["Type"] == "RetEntry")
                stockcheck = "Y";

            string type = this.Request.QueryString["Type"].ToString().Trim();

            string qtype = this.Request.QueryString["Type"].ToString() == "Entry" ? "MTR" :
                           this.Request.QueryString["Type"].ToString() == "LoanEntry" ? "LNR" :
                           this.Request.QueryString["Type"].ToString() == "RetEntry" ? "RTR" :
                           this.Request.QueryString["Type"].ToString() == "JobTrans" ? "JMT" : "";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetProjResList", ProjectCode, curdate, FindResDesc, stockcheck, qtype, mlccode, "", "", "");
            Session["projectreslist"] = ds1.Tables[0];
            ViewState["tblspcf"] = ds1.Tables[1];

            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for Store');", true);
                return;
            }

            DataView dv = ds1.Tables[0].DefaultView;
            dv.Sort = "rsircode";
            DataTable dt = dv.ToTable(true, "rsircode", "resdesc");
            this.ddlreslist.DataTextField = "resdesc";
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
            DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = ("mspcfcod = '" + mResCode + "'");
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string actcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string rescode = this.ddlreslist.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();

            string fmlccode = this.ddlmlccode.SelectedValue.ToString().Trim();
            string tmlccode = this.ddlmlccOrderTo.SelectedValue.ToString().Trim();
            string forder = this.ddlmlccode.SelectedItem.ToString().Trim();
            string torder = this.ddlmlccOrderTo.SelectedItem.ToString().Trim();

            if (this.Request.QueryString["Type"].ToString() == "RetEntry")
                tmlccode = "000000000000";
            else if (this.Request.QueryString["Type"].ToString() == "LoanEntry")
                fmlccode = "000000000000";


            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)Session["projectreslist"];
            DataRow[] projectrow1 = dt1.Select("rsircode = '" + rescode + "' and spcfcod ='" + spcfcod + "'");
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");

            if (projectrow2.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();
                drforgrid["comcod"] = projectrow1[0]["comcod"];
                drforgrid["rsircode"] = projectrow1[0]["rsircode"];
                drforgrid["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                drforgrid["resdesc"] = projectrow1[0]["resdesc"];
                drforgrid["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text;
                drforgrid["sirunit"] = projectrow1[0]["sirunit"];
                drforgrid["qty"] = projectrow1[0]["qty"];
                drforgrid["rate"] = projectrow1[0]["rate"];
                drforgrid["amt"] = projectrow1[0]["amt"];
                drforgrid["balqty"] = projectrow1[0]["balqty"];
                drforgrid["fmlc"] = fmlccode;
                drforgrid["tmlc"] = tmlccode;
                drforgrid["forder"] = forder;
                drforgrid["torder"] = torder;
                drforgrid["spcf"] = projectrow1[0]["spcf"];
                drforgrid["size"] = projectrow1[0]["size"];
                drforgrid["color"] = projectrow1[0]["color"];

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
                double qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim()));
                double rat = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + i;
                dt1.Rows[rowindex]["qty"] = qty;
                double damt = qty * rat;
                dt1.Rows[i]["rate"] = rat;
                dt1.Rows[i]["amt"] = damt;
            }
            ViewState["tblmattrns"] = dt1;
        }

        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
            string tmsg = "Data Recalculate";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);
        }

        protected void lnkupdate_Click(object sender, EventArgs e)

        {


            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string tmsg = "";
            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string mtreqdat = this.txtCurTransDate.Text.ToString().Trim();
            if (ddlPrevISSList.Items.Count == 0)
                this.GetMatTrns();
            string mtreqno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + mtreqdat.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
            string mtrref = this.txtrefno.Text.ToString();
            string mtrnar = this.txtReqNarr.Text.ToString();



            if (mtrref.Length == 0)
            {
                this.txtrefno.Focus();
                tmsg = "Ref. No. Should Not Be Empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                //this.lblmsg1.Text = "Ref. No. Should Not Be Empty";
                return;
            }

            bool result = false;
            string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string supp = this.ddlCompany.SelectedValue.ToString();


            result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQB", mtreqno, mtreqdat, fromprj, toprj, mtrref, mtrnar, PostedByid, Posttrmid,

                       PostSession, Posteddat, supp, "", "", "", "", "", "", "", "", "", "", "");


            if (!result)
            {
                tmsg = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                return;

            }



            foreach (DataRow dr in dt.Rows)
            {

                string trsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string tqty = dr["qty"].ToString().Trim();
                string trate = dr["rate"].ToString().Trim();
                string tamt = dr["amt"].ToString().Trim();
                string fmlccode = dr["fmlc"].ToString().Trim();
                string tmlccode = dr["tmlc"].ToString().Trim();
                string refno = dr["refno"].ToString().Trim();


                result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQA", mtreqno, trsircode, spcfcod, tqty,
               tamt, fmlccode, tmlccode, refno, "", "", "", "", "", "", "", "", "", "", "", "");


                if (!result)
                {
                    // this.lblmsg1.Text = "Updated Fail";

                    tmsg = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
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

            // this.lblmsg1.Text = "Updated Successfully";


            tmsg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);

            this.txtCurTransDate.Enabled = false;
            string qtype = this.Request.QueryString["Type"].ToString();
            //if (qtype == "approve")
            //{
            //    lnkbtnLedger_Click(null,null);
            //}


            if (ConstantInfo.LogStatus == true)
            {
                string eventdesc2 = "";
                string eventtype = "Materials Transfer";
                string eventdesc = "Update New QTY & RATE";
                if (this.Request.QueryString["Type"].ToString() == "RetEntry")
                    eventdesc2 = "From " + this.ddlprjlistfrom.SelectedItem.ToString() + " - " + mtreqno;
                else if (this.Request.QueryString["Type"].ToString() == "LoanEntry")
                    eventdesc2 = " To " + this.ddlprjlistto.SelectedItem.ToString() + " - " + mtreqno;
                else
                    eventdesc2 = "From " + this.ddlprjlistfrom.SelectedItem.ToString() + " To " + this.ddlprjlistto.SelectedItem.ToString() + " - " + mtreqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.grvacc.Columns[6].FooterText.Length > 0)
                this.grvacc.Columns[6].FooterText = "";
            if (lbtnOk.Text.Trim() == "Ok")
            {
                //if(ddlprjlistfrom.SelectedValue=="" && ddlprjlistto.SelectedValue == "" )
                //{
                //    this.ddlprjlistfrom.Focus();
                //  string  tmsg = "Please select from store to store";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                //    return;
                //}

                lbtnOk.Text = "New";
                this.pnlgrd.Visible = true;
                //this.lblddlProjectFrom.Visible = true;
                //this.lblddlProjectTo.Visible = true;
                this.ddlprjlistfrom.Enabled = false;
                this.ddlprjlistto.Enabled = false;
                this.ddlCompany.Enabled = false;
                this.ddlmlccode.Enabled = false;
                this.ddlmlccOrderTo.Enabled = false;
                this.lbtnSync.Enabled = true;




                this.lbtnPrevTransList.Visible = false;
                this.ddlPrevISSList.Visible = false;
                //this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
                //this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;
                this.ImgbtnFindRes_Click(null, null);

                this.GetMatTransfer();
            }
            else
            {

                this.ddlprjlistfrom.Enabled = true;
                this.ddlprjlistto.Enabled = true;
                this.lbtnPrevTransList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.lbtnSync.Enabled = false;
                //this.lblddlProjectFrom.Visible = false;
                //this.lblddlProjectTo.Visible = false;
                this.txtCurTransDate.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.Last_trn_no();
                ////this.pnlgrd.Visible = false; // error find
                this.txtrefno.Text = "";
                //  this.lblmsg1.Text = "";
                this.lblVoucherNo.Text = "";
                lbtnOk.Text = "Ok";
                this.ddlPrevISSList.Items.Clear();


            }


        }



        private void GetMatTransfer()
        {


            ViewState.Remove("tblmattrns");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mTRNNo = "NEWTRNS";
            string type = this.Request.QueryString["Type"].ToString().Trim();

            string qtype = this.Request.QueryString["Type"].ToString() == "Entry" ? "MTR" :
                           this.Request.QueryString["Type"].ToString() == "LoanEntry" ? "LNR" :
                           this.Request.QueryString["Type"].ToString() == "RetEntry" ? "RTR" :
                           this.Request.QueryString["Type"].ToString() == "JobTrans" ? "JMT" : "";

            if (type == "approve")
            {

                mTRNNo = this.Request.QueryString["genno"].ToString().Trim();
            }
            if (type == "Edit")
            {

                mTRNNo = this.Request.QueryString["genno"].ToString().Trim();
            }
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurTransDate.Enabled = false;
                mTRNNo = this.ddlPrevISSList.SelectedValue.ToString();

            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mTRNNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];


            if (mTRNNo == "NEWTRNS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTMTRNO", CurDate1, qtype, "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurTransNo1.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 6);
                this.txtCurTransNo2.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(6);
                return;
            }


            //  this.ddlprjlistfrom.SelectedValue = ds1.Tables[1].Rows[0]["tfpactcode"].ToString();
            //    this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
            //  this.ddlprjlistfrom_SelectedIndexChanged(null, null);
            this.ddlprjlistto.SelectedValue = ds1.Tables[1].Rows[0]["ttpactcode"].ToString();
            this.ddlCompany.SelectedValue = ds1.Tables[1].Rows[0]["actcode"].ToString();
            // this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;

            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mtrdat"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["mtrref"].ToString();
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["mtrnar"].ToString();
            this.lblCurTransNo1.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(6);
            // this.lblVoucherNo.Text = ds1.Tables[1].Rows[0]["vounum"].ToString().Trim();
            this.Data_Bind();
        }


        private void Data_Bind()
        {

            this.grvacc.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = (DataTable)ViewState["tblmattrns"];
            this.grvacc.DataBind();

            string type = this.Request.QueryString["Type"].ToString();
            if (this.GetCompCode() == "8201" && type == "Entry")
            {
                this.grvacc.Columns[6].Visible = false;
            }
            this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            //((LinkButton)this.grvacc.FooterRow.FindControl("lnkupdate")).Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            this.FooterCalCulation();


        }

        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;




        }

        protected void Last_trn_no()
        {
            string qtype = this.Request.QueryString["Type"].ToString() == "Entry" ? "MTR" :
                           this.Request.QueryString["Type"].ToString() == "LoanEntry" ? "LNR" :
                           this.Request.QueryString["Type"].ToString() == "RetEntry" ? "RTR" :
                           this.Request.QueryString["Type"].ToString() == "JobTrans" ? "JMT" : "";


            string comcod = this.GetCompCode();
            string date = this.txtCurTransDate.Text;
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTMTRNO", date, qtype, "", "", "", "", "", "", "");
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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetPrevMTRList", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.DataTextField = "mtreqno1";
            this.ddlPrevISSList.DataValueField = "mtreqno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

            if (this.Request.QueryString["genno"].Length > 0)
            {
                string genno = this.Request.QueryString["genno"].ToString();
                DataTable dt = ds1.Tables[0];
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "mtreqno in ('" + genno + "')";
                DataTable dt1 = dv1.ToTable();


                this.ddlPrevISSList.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.ddlprjlistfrom.SelectedValue = dt1.Rows[0]["TFPACTCODE"].ToString();
                this.ddlprjlistto.SelectedValue = dt1.Rows[0]["TTPACTCODE"].ToString();
            }


        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvacc.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk_Click(null, null);
            }
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt1 = (DataTable)ViewState["tblmattrns"];

            // General Part
            string mtreqno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + this.txtCurTransDate.Text.ToString().Trim().Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mtreqno, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            var lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.MtrReqDetails>();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Reqno = ds1.Tables[0].Rows[0]["mtreqno"].ToString();
            string tfpactdesc = ds1.Tables[1].Rows[0]["tfpactdesc"].ToString();
            string ttpactdesc = ds1.Tables[1].Rows[0]["ttpactdesc"].ToString();
            string ordertrns = tfpactdesc + " To " + ttpactdesc;



            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptMtrReqInfo", lst1, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("from", tfpactdesc));
            rpt1.SetParameters(new ReportParameter("to", ttpactdesc));
            rpt1.SetParameters(new ReportParameter("Reqno", Reqno));
            rpt1.SetParameters(new ReportParameter("date", CurDate1));
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Entry":
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Material Requsition"));
                    break;
                case "LoanEntry":
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Create Loan Requsition"));
                    break;
                case "RetEntry":
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Create Return Requsition"));
                    break;

            }

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_Blank');</script>";


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
            //string FrmPrjCode = this.ddlprjlistfrom.SelectedValue.ToString();
            //string ToPrjCode = this.ddlprjlistto.SelectedValue.ToString();
            string MatCode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvMatCode")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "DELMATREQUITIONBYID", mISUNO, MatCode, "", "", "", "", "", "", "", "", "", "", "");

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

        protected void lbtntoProject_Click(object sender, EventArgs e)
        {
            this.Load_Project_To_Combo();

        }
        protected void lbtnfrmproject_Click(object sender, EventArgs e)
        {
            this.Load_Project_From_Combo();
        }

        private void CommonButton()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            //  ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = (type == "approve" ? true : false);
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Approve";
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("OnClientClick", "return confirm('do you want to approve?')");

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Text = "Recalculate";

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }

        protected void lnkbtnLedger_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string mtreqdat = this.txtCurTransDate.Text.ToString().Trim();

            //string mtreqno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + mtreqdat.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
            string mtreqno = this.Request.QueryString["genno"].ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "APPROVE_MATERIAL_TRANSFER_REQUISITION", mtreqno, PostedByid, Posteddat, "", "", "", "", "", "", "", "");
            if (result)
            {
                //this.lblmsg1.Text = "Material Transfer Requistion Approve";

                string tmsg = "Material Transfer Requistion Approve";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);

                return;
            }

        }

        protected void Load_Order_From_Combo()
        {
            DataTable dt = (DataTable)Session["mlcorderList"];

            this.ddlmlccode.DataTextField = "actdesc1";
            this.ddlmlccode.DataValueField = "actcode";
            this.ddlmlccode.DataSource = dt;
            this.ddlmlccode.DataBind();

            ddlmlccode.Items.Insert(0, new ListItem(" Select Order", "000000000000"));

            this.ddlmlccode_SelectedIndexChanged(null, null);

        }



        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["mlcorderList"];


            DataRow[] projectrow = dt.Select("actcode <> '" + this.ddlmlccode.SelectedValue.ToString().Trim() + "'");
            string actcode = this.ddlmlccode.SelectedValue.ToString().Trim();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode not in ('" + actcode + "')";
            DataTable dt1 = dv1.ToTable();

            this.ddlmlccOrderTo.DataTextField = "actdesc1";
            this.ddlmlccOrderTo.DataValueField = "actcode";
            this.ddlmlccOrderTo.DataSource = dt1;
            this.ddlmlccOrderTo.DataBind();

            ddlmlccOrderTo.Items.Insert(0, new ListItem(" Select Order", "000000000000"));


        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcode = this.GetCompCode();
            string suppliercode = this.ddlCompany.SelectedValue + '%';
            DataSet ds1 = purData.GetTransInfo(comcode, "SP_ENTRY_PURCHASE_05", "Get_Supplier_Wise_Loan_No", suppliercode, "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count <= 0)
                return;

            this.ddlLoanNo.DataTextField = "MTREQNO1";
            this.ddlLoanNo.DataValueField = "mtreqno";
            this.ddlLoanNo.DataSource = ds1.Tables[0];
            this.ddlLoanNo.DataBind();
            this.ddlLoanNo.Items.Insert(0, new ListItem("Select Loan No", "000000000000"));
            this.ddlLoanNo.Enabled = true;
        }

        protected void lbtnSync_Click(object sender, EventArgs e)
        {
            string comcode = this.GetCompCode();
            string loanno = this.ddlLoanNo.SelectedValue;
            string fmlccode = this.ddlmlccode.SelectedValue.ToString().Trim();
            string forder = this.ddlmlccode.SelectedItem.ToString().Trim();
            string torder = this.ddlmlccOrderTo.SelectedItem.ToString().Trim();


            DataSet ds = purData.GetTransInfo(comcode, "SP_ENTRY_PURCHASE_05", "Get_Loan_No_Wise_Mat", loanno);
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
                return;
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)Session["projectreslist"];
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DataRow[] projectrow1 = dt1.Select("rsircode = '" + row["sircode"].ToString() + "' and spcfcod ='" + row["specfcod"].ToString() + "'");
                DataRow[] projectrow2 = dt.Select("rsircode = '" + row["sircode"].ToString() + "' and spcfcod = '" + row["specfcod"].ToString() + "'");

                if (projectrow2.Length == 0 && projectrow1.Length != 0)
                {
                    DataRow drforgrid = dt.NewRow();
                    drforgrid["comcod"] = projectrow1[0]["comcod"];
                    drforgrid["rsircode"] = projectrow1[0]["rsircode"];
                    drforgrid["spcfcod"] = projectrow1[0]["spcfcod"];
                    drforgrid["resdesc"] = projectrow1[0]["resdesc"];
                    drforgrid["spcfdesc"] = projectrow1[0]["spcfdesc"];
                    drforgrid["sirunit"] = projectrow1[0]["sirunit"];
                    drforgrid["qty"] = projectrow1[0]["qty"];
                    drforgrid["rate"] = projectrow1[0]["rate"];
                    drforgrid["amt"] = projectrow1[0]["amt"];
                    drforgrid["balqty"] = projectrow1[0]["balqty"];
                    drforgrid["fmlc"] = fmlccode;
                    drforgrid["tmlc"] = "000000000000";
                    drforgrid["forder"] = forder;
                    drforgrid["torder"] = torder;
                    drforgrid["refno"] = loanno;
                    drforgrid["spcf"] = projectrow1[0]["spcf"];
                    drforgrid["size"] = projectrow1[0]["size"];
                    drforgrid["color"] = projectrow1[0]["color"];
                    dt.Rows.Add(drforgrid);
                }
            }
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();

        }
    }
}