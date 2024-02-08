using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SPEENTITY;
using SPELIB;
using SPERDLC;
using System.IO;
using System.Drawing;

namespace SPEWEB.F_15_Pro
{
    public partial class ProductionPlan : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
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

                ViewState.Remove("IssueItem");
                ViewState.Remove("tblt01");
                ViewState.Remove("resource");
                ViewState.Remove("tblProduct");
                //this.Table1();

                this.txtrcvdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtExDate.Text = System.DateTime.Today.AddMonths(3).ToString("dd-MMM-yyyy");
                imgbtnbatchsrc_Click(null, null);

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Entry") ? " PRODUCTION Entry " : " PRODUCTION Entry- Semi Product";
                
                this.CommonButton();
                this.GetLineFloor();
                ((CheckBox)this.Master.FindControl("chkBoxN")).Text = "From Process";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                if (this.Request.QueryString["Type"].ToString() != "EntrySemi")
                {
                    
                    //this.gvProdu.Columns[8].Visible = false;
                    //this.gvProdu.Columns[9].Visible = false;
                    //this.gvProdu.Columns[10].Visible = false;
                  
                    //this.gvProdu.Columns[14].Visible = true;
                    //this.gvProdu.Columns[4].HeaderText = "Prod. Line";
                    //this.gvProdu.Columns[11].HeaderText = "Manpower";

                    //if (comcod != "8701")
                    //{
                    //    this.gvProdu.Columns[11].Visible = false;
                    //}


                }
                else
                {
                   
                    //this.gvProdu.Columns[4].HeaderText = "Machinery";
                    //this.gvProdu.Columns[11].HeaderText = "M/C Utilize Time (Hour)";
                    //this.gvProdu.Columns[14].Visible = false;
                    
                }
                if (this.Request.QueryString["actcode"].ToString() != "")
                {
                    this.lnkOk_Click(null, null);
                }

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);


        }
        private void CommonButton()
        {
            //

           

            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("infcod", Type.GetType("System.String"));
            tblt01.Columns.Add("infdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("infunit", Type.GetType("System.String"));
            tblt01.Columns.Add("qty", Type.GetType("System.Double"));
            tblt01.Columns.Add("rate", Type.GetType("System.Double"));
            ViewState["tblt01"] = tblt01;
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (this.lnkOk.Text == "Ok")
            {
                if (this.txtrcvdate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input Production Date!!!');", true);
                    return;
                }
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                switch (comcod)
                {
                    case "8701":
                    case "8702":
                    case "8703":
                    case "8704":
                    case "8705":
                        //this.gvProdu.Columns[4].Visible = true;
                        this.Pnladditional.Visible = true;
                        break;
                    default:
                        //this.gvProdu.Columns[4].Visible = false;
                        this.Pnladditional.Visible = false;
                        break;

                }
                this.imgbtnProduc_Click(null, null);
                // this.Table1();
                this.GetProdinfo();
                this.lodeGData();
                this.ddlbatchno.Enabled = false;
                this.PnlProcess.Visible = true;
                this.PnlProCode.Visible = true;
                if (type == "EntrySemi")
                {
                    //this.Label3.Visible = true;
                    this.LblLine.Text = "Machinery";
                    this.MachineryPanl.Visible = true;
                    this.LInePanel.Visible = false;
                }
                else
                {

                    //this.Label3.Visible = false;
                    this.LblLine.Text = "Prod. Line";
                    this.pnlHpro.Visible = true;


                    this.MachineryPanl.Visible = false;

                    switch (comcod)
                    {
                        case "8701":
                        case "8702":
                        case "8703":
                        case "8704":
                        case "8705":
                            //  this.GetHourlyProdInf();
                            this.LInePanel.Visible = true;
                            break;
                        default:

                            break;

                    }

                }

               
                this.GETPRDHOur();

                this.lnkOk.Text = "New";
                this.lnkOk.Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
                this.ImgbtnFindProList.Visible = false;
                this.ddlPrevPro.Visible = false;
            }
            else
            {

            }

        }

        private void lnkbtnNew_Click(object sender, EventArgs e)
        {
            this.lnkOk.Visible = true;
            this.lnkOk.Text = "Ok";
            this.PnlProcess.Visible = false;
            this.PnlProCode.Visible = false;

            this.dgv1.DataSource = null;
            this.dgv1.DataBind();
            this.gvProdu.DataSource = null;
            this.gvProdu.DataBind();
            this.ddlbatchno.Enabled = true;
            this.DropDownList1.Items.Clear();
            this.txtprodFqty.Text = "";
            this.lblvalwastage.Text = "";
            this.txtactual.Text = "";
            this.txtProrat.Text = "";
            this.txtBatch.Text = "";
            this.txtprodid.Text = "";
            this.gvHourlyProd.DataSource = null;
            this.gvHourlyProd.DataBind();
            this.pnlHpro.Visible = false;
            this.ImgbtnFindProList.Visible = true;
            this.ddlPrevPro.Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = true;
            this.txtrcvdate.Enabled = true;
        }
        private void lodeGData()
        {

            ViewState.Remove("resource");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //this.Table1();
            string batchno = this.ddlbatchno.SelectedValue.ToString();
            string procode = "%%";
            string date = this.txtrcvdate.Text.Substring(0, 11);

            //DataSet ds6 = proc1.DataRelation("SP_INV_RPT_SUM", ConstantInfo.ComCod, "STD_COSTSHEET01", "SIRINF", batchno, procode, "", "", "");
            DataSet ds6 = accData.GetTransInfo(comcod, "SP_INV_RPT_SUM", "STD_COSTSHEET01", batchno, procode, date, "", "", "", "", "", "");
            ViewState["resource"] = ds6.Tables[0];
            this.LoadSCGv();
        }


        private void Table1()
        {
            //ViewState.Remove("IssueItem");
            //DataTable dtl = new DataTable();
            ////dtl.Columns.Add("STORID", Type.GetType("System.String"));
            //dtl.Columns.Add("PSTATUSDESC", Type.GetType("System.String"));
            //dtl.Columns.Add("SHIFT", Type.GetType("System.String"));
            //dtl.Columns.Add("PSTATUS", Type.GetType("System.String"));
            //dtl.Columns.Add("PROCODE", Type.GetType("System.String"));
            //dtl.Columns.Add("PRODESC", Type.GetType("System.String"));
            //dtl.Columns.Add("MACHINERY", Type.GetType("System.String"));
            //dtl.Columns.Add("MACHID", Type.GetType("System.String"));
            //dtl.Columns.Add("bgdqty", Type.GetType("System.Double"));
            //dtl.Columns.Add("PROQTY", Type.GetType("System.Double"));
            //dtl.Columns.Add("PRORATE", Type.GetType("System.Double"));
            //dtl.Columns.Add("PROAMT", Type.GetType("System.Double"));
            //dtl.Columns.Add("MANQTY", Type.GetType("System.Double"));
            //dtl.Columns.Add("BIQTY1", Type.GetType("System.Double"));
            //dtl.Columns.Add("BIQTY2", Type.GetType("System.Double"));
            //dtl.Columns.Add("BIQTY3", Type.GetType("System.Double"));
            //dtl.Columns.Add("UNITWEIGHT", Type.GetType("System.Double"));

            //ViewState["IssueItem"] = dtl;

        }
        private void GetProdinfo()
        {
            ViewState.Remove("IssueItem");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string proddat = Convert.ToDateTime(this.txtrcvdate.Text.Trim()).ToString("dd-MMM-yyyy");

            string Prdno = "NEWPR";
            if (this.ddlPrevPro.Items.Count > 0)
            {
                this.txtrcvdate.Enabled = false;
                Prdno = this.ddlPrevPro.SelectedValue.ToString();
                this.txtprodid.Text = Prdno;
            }
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODUCTIONINF", Prdno, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            ViewState["IssueItem"] = ds1.Tables[0];
            if (ds1.Tables[0].Rows.Count > 0)
            {
                DataView view = new DataView(ds1.Tables[0]);
                DataTable wip = view.ToTable(true, "production", "wipname");

                this.ddlbatchno.DataTextField = "wipname";
                this.ddlbatchno.DataValueField = "production";
                this.ddlbatchno.DataSource = wip;
                this.ddlbatchno.DataBind();

                this.txtrcvdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["prodate"]).ToString("dd-MMM-yyyy");
            }
            if (Prdno == "NEWPR")
            {
                ds1 = accData.GetTransInfo(comcod, "SP_INVGEN_INFO", "GETPRODID", proddat, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.txtprodid.Text = ds1.Tables[0].Rows[0]["prodid"].ToString();
                }
                this.gvProdu_DataBind();

                return;

            }

            this.gvProdu_DataBind();
        }

        private void LoadSCGv()
        {
            DataTable dt1 = (DataTable)ViewState["resource"];
            this.dgv1.DataSource = dt1;
            this.dgv1.DataBind();

        }
        protected void imgbtnbatchsrc_Click(object sender, EventArgs e)
        {
            if (this.lnkOk.Text == "New")
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter3 = (this.Request.QueryString["actcode"].ToString().Length == 0) ? "%" + this.txtbatchsrc.Text.Trim() + "%" : this.Request.QueryString["actcode"].ToString() + "%";
            string EntryType = (this.Request.QueryString["Type"] == "Entry") ? "" : "Semi";
            DataSet ds6 = accData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RTV_PRODUCTIONCOD", filter3, EntryType, "", "", "", "", "", "", "");
            this.ddlbatchno.DataTextField = "actdesc1";
            this.ddlbatchno.DataValueField = "actcode";
            this.ddlbatchno.DataSource = ds6.Tables[0];
            this.ddlbatchno.DataBind();
        }


        protected void ddlprocode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            ViewState.Remove("Tblpropln");
            try
            {
                if (ViewState["Tblpropln"] == null)
                {
                    DataTable tbl2 = new DataTable();
                    tbl2.Columns.Add("code", Type.GetType("System.String"));
                    tbl2.Columns.Add("desc", Type.GetType("System.String"));
                    tbl2.Columns.Add("unit", Type.GetType("System.String"));
                    tbl2.Columns.Add("qty", Type.GetType("System.Double"));
                    tbl2.Columns.Add("stdprice", Type.GetType("System.Double"));
                    tbl2.Columns.Add("amt", Type.GetType("System.Double"));
                    tbl2.Columns.Add("perc", Type.GetType("System.Double"));
                    ViewState["Tblpropln"] = tbl2;
                }
                DataTable tbl3 = (DataTable)ViewState["Tblpropln"];
                for (int i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    string code = ((Label)this.dgv1.Rows[i].FindControl("lblgvcode")).Text;
                    string desc = ((Label)this.dgv1.Rows[i].FindControl("lblgvcodeDescription")).Text;
                    string unit = ((Label)this.dgv1.Rows[i].FindControl("lblgvunit")).Text;
                    double qty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvqty")).Text);
                    double stdpric = Convert.ToDouble("0" + ((Label)this.dgv1.Rows[i].FindControl("lblgvstdpri")).Text);
                    double amt = Convert.ToDouble("0" + ((Label)this.dgv1.Rows[i].FindControl("lblgvamt")).Text);
                    double perc = Convert.ToDouble("0" + ((Label)this.dgv1.Rows[i].FindControl("lblgvper")).Text.Replace("%", ""));
                    DataRow dr1 = tbl3.NewRow();
                    dr1["code"] = code;
                    dr1["desc"] = desc;
                    dr1["unit"] = unit;
                    dr1["qty"] = qty;
                    dr1["stdprice"] = stdpric;
                    dr1["amt"] = amt;
                    dr1["perc"] = perc;
                    tbl3.Rows.Add(dr1);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
        private void GETPRODNO()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Prodno = "NEWPR";
            if (this.ddlPrevPro.Items.Count > 0)
                Prodno = this.ddlPrevPro.SelectedValue.ToString();

            string proddat = this.txtrcvdate.Text.ToString().Substring(0, 11);
            if (Prodno == "NEWPR")
            {
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODID", proddat, "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.txtprodid.Text = ds1.Tables[0].Rows[0]["prodid"].ToString();

                    this.ddlPrevPro.DataTextField = "prodid";
                    this.ddlPrevPro.DataValueField = "prodid";
                    this.ddlPrevPro.DataSource = ds1.Tables[0];
                    this.ddlPrevPro.DataBind();
                }
            }




            //try
            //{
            //    if (this.txtprodid.Text == "")
            //    {

            //        string proddat = this.txtrcvdate.Text.ToString().Substring(0, 11);
            //        // DataSet tbl3 = proc1.DataRelation("SP_INVGEN_INFO", ConstantInfo.ComCod, "GETPRODID", "", proddat, "", "", "", "");
            //        DataSet tbl3 = accData.GetTransInfo(comcod, "SP_INVGEN_INFO", "GETPRODID", proddat, "", "", "", "", "", "", "", "");
            //        this.txtprodid.Text = tbl3.Tables[0].Rows[0]["prodid"].ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.lblmsg.Text = "Error:" + ex.Message;
            //}
        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.gvProdu_DataBind();
        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            this.Save_Value();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string issuedat = this.txtrcvdate.Text.ToString().Substring(0, 11);

            //DateTime Bdate = Common.GetBackDate();
            //bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(issuedat));
            //if (!dcon)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Entry Date is equal or less Current Date');", true);
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //    ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
            //    return;
            //}


            // DataSet tbl4 = proc1.DataRelation("SP_INV_LC_INFO", ConstantInfo.ComCod, "GETBTCNO01", "", issuedat, storid, "", "", "");
            //DataSet tbl4 = accData.GetTransInfo(comcod, "SP_INV_LC_INFO", "GETBTCNO01", issuedat, "", "", "", "", "", "", "", "");
            //this.txtBatch.Text = tbl4.Tables[0].Rows[0]["btcno"].ToString();

            //--------------
            //----------------- update Varif
            //for (int i = 0; i < this.gvProdu.Rows.Count; i++)
            //{
            //    string procode = ((Label)gvProdu.Rows[i].FindControl("lblprocode")).Text.Trim().ToString();
            //    string proqty = "0" + ((TextBox)gvProdu.Rows[i].FindControl("gvtxtQty")).Text.Replace(",", "").Trim();
            //    string production = this.ddlbatchno.SelectedValue.ToString();
            //    string batch = this.txtBatch.Text;
            //    string prodate = this.txtrcvdate.Text.ToString().Substring(0, 11);
            //    string tol = "0.0";
            //    //batchno,procod,proqty,proddat,tole
            //    // DataSet ds4 = proc1.UpdateDataSet("SP_INVGEN_INFO", ConstantInfo.ComCod, "UPDAPRODINFO", "VARINF", batch, procode, production, proqty, prodate, tol, "", "", "", "", "", "", "", "");
            //    DataSet ds4 = accData.GetTransInfo(comcod, "SP_INVGEN_INFO", "UPDAPRODINFO", batch, procode, production, proqty, prodate, tol, "", "", "");
            //    if (ds4 == null)
            //    {
            //        this.lblmsg.Text = "Error:" + accData.ErrorObject["Msg"].ToString();
            //        //this.lblmsg.ForeColor = System.Drawing.Color.Red;               
            //        return;
            //    }
            //    this.lblmsg.Text = "Record Update Successfully.";
            //    //this.lblmsg.ForeColor = System.Drawing.Color.Green;           
            //}

            if (this.ddlPrevPro.Items.Count == 0)
                this.GETPRODNO();


            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid; //(this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = Terminal; //(this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = Sessionid; //(this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;


            string expdate = Convert.ToDateTime(this.txtExDate.Text).ToString("dd-MMM-yyyy");
            string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            for (int i = 0; i < this.gvProdu.Rows.Count; i++)
            {

                
                string procode = ((Label)gvProdu.Rows[i].FindControl("lblprocode")).Text.ToString();
                string spcfcod = ((Label)gvProdu.Rows[i].FindControl("LBlSpcfcod")).Text.ToString();

                string batch = this.txtBatch.Text;
                string production = this.ddlbatchno.SelectedValue.ToString();
                string prodid = this.txtprodid.Text.ToString();
                string storid1 = "000000000000";// ((Label)gvProdu.Rows[i].FindControl("lblstorid")).Text.ToString();
                string prodate = this.txtrcvdate.Text.ToString().Substring(0, 11);
                string proqty = "0" + ((TextBox)gvProdu.Rows[i].FindControl("gvtxtQty")).Text.Replace(",", "").Trim();
                string rate = "0" + ((TextBox)gvProdu.Rows[i].FindControl("gvtxtRat")).Text.Replace(",", "").Trim();
                string machid = "";/*((Label)gvProdu.Rows[i].FindControl("lblmachid")).Text.ToString();*/

                string RunnerAmt = "0"; /*+ ((TextBox)gvProdu.Rows[i].FindControl("gvtxtRunnerAmt")).Text.Replace(",", "").Trim();*/
                string rejamt = "0";/* + ((TextBox)gvProdu.Rows[i].FindControl("gvtxtRejAmt")).Text.Replace(",", "").Trim();*/
                string purgamt = "0"; /*+ ((TextBox)gvProdu.Rows[i].FindControl("gvtxtpurgAmt")).Text.Replace(",", "").Trim();*/
                string shift = ""; /*((Label)gvProdu.Rows[i].FindControl("lblShift")).Text.ToString();*/
                string status = "";/*((Label)gvProdu.Rows[i].FindControl("lbllstatus")).Text.ToString();*/
                string manqty = "0"; /*+ ((TextBox)gvProdu.Rows[i].FindControl("gvtxtmanqty")).Text.Replace(",", "").Trim();*/
                string UnitWeught = "0"; /*+ ((Label)gvProdu.Rows[i].FindControl("gvlblUnitWeught")).Text.Replace(",", "").Trim();*/
                if (Convert.ToDouble("0" + rate) > 0)
                {
                    bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "UPDATEPRODINFO", procode+ spcfcod, batch, production, prodid, storid1, prodate, proqty,
                        rate, PostedByid, Posttrmid, PostSession, expdate, machid, shift, posteddat, RunnerAmt, rejamt, status, manqty, purgamt, UnitWeught);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + accData.ErrorObject["Msg"] + "');", true);

                        return;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Issue Updated');", true);

                }
            }
            this.lodeGData();

            this.lnkOk.Enabled = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = false;
            /// this.lnkFinalUpdate0.Enabled = false;

        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvcodeDescription");
                Label acresdesc = (Label)e.Row.FindControl("lblgvunit");
                Label lbldram = (Label)e.Row.FindControl("lblgvamt");
                // Label lblcramt = (Label)e.Row.FindControl("txtgvCram");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "resdesc")).ToString();
                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescod")).ToString() + "00";


                //string sub = code.Substring(0, 12);
                // string sub1 = Substring(9, 12, code);

                if (code == "")
                {
                    return;
                }
                if (code == "Sub-Total" || code == "Grand Total" || code == "Balance" || code == "Transfer")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    //lblcramt.Font.Bold = true;

                }
                //if (code == "Grand Total")
                //{

                //    acrescode.Font.Bold = true;
                //    acresdesc.Font.Bold = true;
                //    lbldram.Font.Bold = true;
                //    //lblcramt.Font.Bold = true;

                //}
                //if (code == "Balance")
                //{

                //    acrescode.Font.Bold = true;
                //    acresdesc.Font.Bold = true;
                //    lbldram.Font.Bold = true;

                //}
                //if (code1.Length > 0)
                //{
                if (code1.Substring(0, 2).ToString() == "49")
                {
                    acrescode.Font.Bold = true;
                    acrescode.ForeColor = System.Drawing.Color.Blue;
                }
                // }
                //if (sub1.ToString() == "000")
                //{
                //    acrescode.Font.Bold = true;
                //    acrescode.ForeColor = System.Drawing.Color.Red;
                //}


                // this.lblTamt.Text=

            }
        }


        protected void imgbtnProduc_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblProduct");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Bactcode = this.ddlbatchno.SelectedValue.ToString();
            string CallType = (ASTUtility.Left(comcod, 1) == "6") ? "RETRIVEPRODUCT1" : "RETRIVEPRODUCT";
            string Process = (((CheckBox)this.Master.FindControl("chkBoxN")).Checked == true) ? "Process" : "";
            string ActQty = (this.chkAct.Checked) ? "Actual" : "";
            DataSet ds8 = accData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", CallType, Bactcode, Process, ActQty, "", "", "", "", "", "");
            this.DropDownList1.DataTextField = "rsirdesc";
            this.DropDownList1.DataValueField = "rsircode1";
            this.DropDownList1.DataSource = ds8.Tables[0];
            this.DropDownList1.DataBind();
            ViewState["tblProduct"] = ds8.Tables[0];
            ds8.Dispose();
            this.DropDownList1_SelectedIndexChanged(null, null);


        }
        protected void gvProdu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["IssueItem"];
            //string rescode = ((Label)this.gvProdu.Rows[e.RowIndex].FindControl("lblprocode")).Text.Trim();
            //string machid = ((Label)this.gvProdu.Rows[e.RowIndex].FindControl("lblmachid")).Text.Trim();

            int rowindex = (this.gvProdu.PageSize) * (this.gvProdu.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("PROCODE<>''");
            ViewState["IssueItem"] = dv.ToTable();
            this.gvProdu_DataBind();

        }

        protected void lnkAddToTab_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            if (txtprodFqty.Text == "")
            {
                txtprodFqty.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Enter Product Quantity');", true);

                return;
            }
            if (txtProrat.Text == "")
            {
                txtProrat.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Enter Product Rate');", true);

                return;
            }
            if (this.DropDownList1.Text == "")
            {
                txtProrat.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Select Product');", true);

                return;
            }
            //if (this.ddlmach.Text == "")
            //{
            //    ddlmach.Focus();
            //    lblmsg.Text = "Select Machinery";
            //    return;
            //}
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
           
            string procod = this.DropDownList1.SelectedValue.ToString().Substring(0,12);
            string spcfcod = this.DropDownList1.SelectedValue.ToString().Substring(12,12);
            string prodesc = this.DropDownList1.SelectedItem.ToString();
            string machid = this.ddlmach.SelectedValue.ToString();
            string Machinery = this.ddlmach.SelectedItem.ToString();


            string pstatusdesc = (this.Request.QueryString["Type"].ToString() == "EntrySemi") ? "None" : this.ddlstatus.SelectedItem.ToString();
            string shift = (this.Request.QueryString["Type"].ToString() == "EntrySemi") ? this.ddlshift.SelectedValue.ToString() : this.ddlhour.SelectedValue.ToString();
            string pstatus = (this.Request.QueryString["Type"].ToString() == "EntrySemi") ? "00000" : this.ddlstatus.SelectedValue.ToString();
            string proqty = this.txtactual.Text; //this.txtprodFqty.Text;
            string prorat = this.txtProrat.Text;
            string uweight = Convert.ToDecimal("0" + this.lbluweight.Text).ToString();


            DataTable tbl2 = (DataTable)ViewState["tblProduct"];
            DataTable dt2 = (DataTable)ViewState["IssueItem"];
            DataRow[] dr1 = dt2.Select("PROCODE='" + procod + "' and spcfcod='"+ spcfcod + "' and SHIFT='" + shift + "' and  MACHID='" + machid + "' and  PSTATUS='" + pstatus + "'");
            if (dr1.Length == 1)
            {
                dr1[0]["proqty"] = 0.00;
                dr1[0]["bgdqty"] = proqty;
                dr1[0]["proamt"] = (Convert.ToDouble(proqty) * Convert.ToDouble(prorat));
                dr1[0]["manqty"] = 0.00;

                dr1[0]["biqty1"] = 0.00;
                dr1[0]["biqty2"] = 0.00;
                dr1[0]["biqty3"] = 0.00;
                dr1[0]["unitweight"] = Convert.ToDecimal(uweight);
            }
            else
            {

                DataRow dr = dt2.NewRow();

                //dr["STORID"] = storid;
                dr["PSTATUSDESC"] = pstatusdesc;
                dr["SHIFT"] = shift;
                dr["PSTATUS"] = pstatus;
                dr["PROCODE"] = procod;
                dr["spcfcod"] = spcfcod;
                dr["PRODESC"] = prodesc;
                dr["MACHINERY"] = Machinery;
                dr["MACHID"] = machid;
                dr["proqty"] = 0.00;
                dr["bgdqty"] = proqty;
                dr["PRORATE"] = prorat;
                dr["PROAMT"] = (Convert.ToDouble(proqty) * Convert.ToDouble(prorat)).ToString();
                dr["manqty"] = 0.00;
                dr["biqty1"] = 0.00;
                dr["biqty2"] = 0.00;
                dr["biqty3"] = 0.00;
                dr["unitweight"] = Convert.ToDecimal(uweight);

                dt2.Rows.Add(dr);

                if (this.Request.QueryString["Type"].ToString() == "EntrySemi")
                {
                    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODFROMANALYSISWITHCONCERN", procod, spcfcod, "", "", "", "", "", "", "");


                    DataTable dtfromanalysis = ds1.Tables[0];


                    if (dtfromanalysis.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtfromanalysis.Rows.Count; i++)
                        {
                            string rsircode = dtfromanalysis.Rows[i]["rsircode"].ToString();
                            string spcfcode = dtfromanalysis.Rows[i]["spcfcod"].ToString();
                            DataRow[] dr3 = dt2.Select("PROCODE='" + rsircode + "' and spcfcod='" + spcfcode + "' and SHIFT='" + shift + "' and  MACHID='" + machid + "' and  PSTATUS='" + pstatus + "'"); //dt2.Select("PROCODE='" + rsircode + "'");
                            if (dr3.Length == 0)
                            {
                                DataRow dr2 = dt2.NewRow();
                                //dr2["STORID"] = storid;
                                dr2["PSTATUSDESC"] = pstatusdesc;
                                dr2["SHIFT"] = shift;
                                dr2["PSTATUS"] = pstatus;
                                dr2["spcfcod"] = (((DataTable)ViewState["tblProduct"]).Select("rsircode1='" + rsircode+ spcfcode + "'"))[0]["spcfcod"].ToString();
                                dr2["PROCODE"] = (((DataTable)ViewState["tblProduct"]).Select("rsircode1='" + rsircode + spcfcode + "'"))[0]["rsircode"].ToString();
                                dr2["PRODESC"] = (((DataTable)ViewState["tblProduct"]).Select("rsircode1='" + rsircode + spcfcode + "'"))[0]["rsirdesc"].ToString();
                                dr2["proqty"] = 0.00; ///(((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["maxrate"].ToString();
                                dr2["bgdqty"] = Convert.ToDouble((((DataTable)ViewState["tblProduct"]).Select("rsircode1='" + procod+ spcfcod + "'"))[0]["proqty"]) * Convert.ToDouble(dtfromanalysis.Rows[i]["rfqty"]);//Convert.ToDouble(dr3[0]["qty"] * dtfromanalysis.Rows[i]["rfqty"].ToString()).ToString();
                                dr2["PRORATE"] = Convert.ToDouble((((DataTable)ViewState["tblProduct"]).Select("rsircode1='" + rsircode+ spcfcode + "'"))[0]["bgdrat"]);//Convert.ToDouble(dr3[0]["bgdrat"]).ToString();
                                dr2["PROAMT"] = Convert.ToDouble((((DataTable)ViewState["tblProduct"]).Select("rsircode1='" + rsircode+ spcfcode + "'"))[0]["qty"]) * Convert.ToDouble((((DataTable)ViewState["tblProduct"]).Select("rsircode='" + rsircode + "'"))[0]["bgdrat"]); //(Convert.ToDouble(dr3[0]["qty"]) * Convert.ToDouble(dr3[0]["bgdrat"])).ToString();
                                dr2["MACHINERY"] = Machinery;
                                dr2["MACHID"] = machid;

                                dr2["manqty"] = 0.00;
                                dr2["biqty1"] = 0.00;
                                dr2["biqty2"] = 0.00;
                                dr2["biqty3"] = 0.00;
                                dr2["unitweight"] = 0.00;

                                dt2.Rows.Add(dr2);
                            }
                            else
                            {


                                DataRow[] drst = dt2.Select("PROCODE='" + rsircode + "' and SHIFT='" + shift + "' and  MACHID='" + machid + "' and  PSTATUS='" + pstatus + "'"); ////dt2.Select("PROCODE='" + rsircode + "'");

                                double oldqty = Convert.ToDouble(drst[0]["bgdqty"]);

                                double newqty = Convert.ToDouble((((DataTable)ViewState["tblProduct"]).Select("rsircode='" + procod + "'"))[0]["proqty"]) * Convert.ToDouble(dtfromanalysis.Rows[i]["rfqty"]);

                                drst[0]["bgdqty"] = oldqty + newqty;


                            }
                        }

                    }
                }
            }

            //DataView dv = dt2.DefaultView;
            //dv.Sort = "MACHID ASC";

            dt2.DefaultView.Sort = "procode, MACHID";
            dt2 = dt2.DefaultView.ToTable();

            ViewState["IssueItem"] = dt2;

            gvProdu_DataBind();
            gvProdu.Visible = true;
            //this.txtprodFqty.Text = "";
            //this.txtactual.Text = "";
            //this.lblvalwastage.Text = "";


        }

        protected void lnkAddToTabAll_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            if (txtprodFqty.Text == "")
            {
                txtprodFqty.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Enter Product Quantity');", true);
                return;
            }
            if (txtProrat.Text == "")
            {
                txtProrat.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Enter Product Rate');", true);
                return;
            }
            if (this.DropDownList1.Text == "")
            {
                txtProrat.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Select Product');", true);
                return;
            }

            
            //double proqty1 = 0.00;
            string procod = this.DropDownList1.SelectedValue.ToString().Substring(0,12);
            string spcfcod = this.DropDownList1.SelectedValue.ToString().Substring(12, 12);
            
            string prodesc = this.DropDownList1.SelectedItem.ToString();
            string proqty = this.txtactual.Text; //this.txtprodFqty.Text;
            string prorat = this.txtProrat.Text;
            string machid = this.ddlmach.SelectedValue.ToString();
            string Machinery = this.ddlmach.SelectedItem.ToString();
            //string storid = this.ddlStorid.SelectedValue.ToString();
            string pstatusdesc = (this.Request.QueryString["Type"].ToString() == "EntrySemi") ? "" : this.ddlstatus.SelectedItem.ToString();
            string shift = (this.Request.QueryString["Type"].ToString() == "EntrySemi") ? this.ddlshift.SelectedValue.ToString() : this.ddlhour.SelectedValue.ToString();
            string pstatus = (this.Request.QueryString["Type"].ToString() == "EntrySemi") ? "000" : this.ddlstatus.SelectedValue.ToString();
            DataTable tbl2 = (DataTable)ViewState["tblProduct"];

            DataTable dt2 = (DataTable)ViewState["IssueItem"];
            DataRow[] dr1 = dt2.Select("PROCODE='" + procod + "' and spcfcod='"+ spcfcod + "' and SHIFT='" + shift + "' and pstatus='" + pstatus + "' and  MACHID='" + machid + "'");
            if (dr1.Length == 1)
            {
                dr1[0]["proqty"] = 0.00;
                dr1[0]["bgdqty"] = proqty;
                dr1[0]["proamt"] = (Convert.ToDouble(proqty) * Convert.ToDouble(prorat));
                dr1[0]["manqty"] = 0.00;

                dr1[0]["biqty1"] = 0.00;
                dr1[0]["biqty2"] = 0.00;
                dr1[0]["biqty3"] = 0.00;
                dr1[0]["unitweight"] = 0.00;

            }
            else
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr2 = dt2.NewRow();
                    //dr2["STORID"] = storid;
                    dr2["PSTATUSDESC"] = pstatusdesc;
                    dr2["SHIFT"] = shift;
                    dr2["PSTATUS"] = pstatus;
                    dr2["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr2["PROCODE"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr2["PRODESC"] = tbl2.Rows[i]["rsirdesc"].ToString();
                    dr2["proqty"] = 0.00;
                    dr2["bgdqty"] = Convert.ToDouble(tbl2.Rows[i]["qty"]).ToString();
                    dr2["PRORATE"] = Convert.ToDouble(tbl2.Rows[i]["bgdrat"]).ToString();
                    dr2["PROAMT"] = (Convert.ToDouble(tbl2.Rows[i]["qty"]) * Convert.ToDouble(tbl2.Rows[i]["bgdrat"])).ToString();
                    dr2["MACHINERY"] = Machinery;
                    dr2["MACHID"] = machid;
                    dr2["manqty"] = 0.00;

                    dr2["biqty1"] = 0.00;
                    dr2["biqty2"] = 0.00;
                    dr2["biqty3"] = 0.00;
                    dr2["unitweight"] = Convert.ToDouble(tbl2.Rows[i]["unitweight"]).ToString();

                    dt2.Rows.Add(dr2);
                }

            }

            ViewState["IssueItem"] = dt2;

            gvProdu_DataBind();
            gvProdu.Visible = true;
            this.txtprodFqty.Text = "";
            this.txtactual.Text = "";
            this.lblvalwastage.Text = "";

        }
        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["IssueItem"];
            int TblRowIndex2;
            
            for (int j = 0; j < this.gvProdu.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvProdu.PageSize) * (this.gvProdu.PageIndex) + j;

                //dr2[0][""] = 0.00;

                double lblgvRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProdu.Rows[j].FindControl("gvtxtRat")).Text.Trim()));
                double txtgvActQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProdu.Rows[j].FindControl("gvtxtQty")).Text.Trim()));
                //double manqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProdu.Rows[j].FindControl("gvtxtmanqty")).Text.Trim()));

                //double biqty1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProdu.Rows[j].FindControl("gvtxtRunnerAmt")).Text.Trim()));
                //double biqty2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProdu.Rows[j].FindControl("gvtxtRejAmt")).Text.Trim()));
                //double biqty3 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProdu.Rows[j].FindControl("gvtxtpurgAmt")).Text.Trim()));



                double tAmount = txtgvActQty * lblgvRate;

                if (txtgvActQty == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Qc Qty Not More then Product Qty');", true);
                    break;

                }

                ((TextBox)this.gvProdu.Rows[j].FindControl("gvtxtAmt")).Text = tAmount.ToString("#,##0.00;(#,##0.00); ");



                tbl1.Rows[TblRowIndex2]["proqty"] = txtgvActQty;
                tbl1.Rows[TblRowIndex2]["PROAMT"] = tAmount;
                //tbl1.Rows[TblRowIndex2]["manqty"] = manqty;

                //tbl1.Rows[TblRowIndex2]["biqty1"] = biqty1;
                //tbl1.Rows[TblRowIndex2]["biqty2"] = biqty2;
                //tbl1.Rows[TblRowIndex2]["biqty3"] = biqty3;
            }
            ViewState["IssueItem"] = tbl1;
        }

        private void gvProdu_DataBind()
        {
            DataTable tbl = (DataTable)ViewState["IssueItem"];

            tbl.DefaultView.Sort = "procode, MACHID";
            tbl = tbl.DefaultView.ToTable();

            gvProdu.DataSource = (tbl);
            gvProdu.DataBind();
            if (tbl == null)
                return;

            for (int i = 0; i < gvProdu.Rows.Count; i++)
            {
                string Code = ((Label)gvProdu.Rows[i].FindControl("lblprocode")).Text.Trim();

                if (ASTUtility.Left(Code, 7) == "0101901")
                {
                    this.gvProdu.Rows[i].BackColor = Color.SkyBlue;
                    this.gvProdu.Rows[i].ForeColor = Color.Black;
                }
            }


            if (tbl.Rows.Count > 0)
            {
                ((Label)this.gvProdu.FooterRow.FindControl("gvlblFbgdqty")).Text =
                       Convert.ToDouble((Convert.IsDBNull(tbl.Compute("sum(bgdqty)", "")) ? 0.00 :
                       tbl.Compute("sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvProdu.FooterRow.FindControl("gvlblFQty")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl.Compute("sum(proqty)", "")) ? 0.00 :
                        tbl.Compute("sum(proqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvProdu.FooterRow.FindControl("gvlblTotalAmt")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl.Compute("sum(PROAMT)", "")) ? 0.00 :
                        tbl.Compute("sum(PROAMT)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {


            string rsircode = this.DropDownList1.SelectedValue.ToString();
            DataTable dt2 = (DataTable)ViewState["tblProduct"];
            DataRow[] dr1 = dt2.Select("rsircode1='" + rsircode + "'");
            if (dr1.Length > 0)
            {
                this.txtProrat.Text = Convert.ToDouble(dr1[0]["bgdrat"]).ToString();
                this.txtprodFqty.Text = Math.Round(Convert.ToDouble(dr1[0]["qty"]), 2).ToString();
                this.txtactual.Text = Math.Round(Convert.ToDouble(dr1[0]["proqty"]), 2).ToString();
                this.lblvalwastage.Text = Math.Round(Convert.ToDouble(dr1[0]["wasqty"]), 2).ToString();
                this.lbluweight.Text = Convert.ToDouble(dr1[0]["unitweight"]).ToString("#,##0.000000;(#,##0.000000); ");

            }
            else
            {
                this.txtProrat.Text = "";
                this.txtactual.Text = "";
                this.lblvalwastage.Text = "";
                this.lbluweight.Text = "";
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void gvProdu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //string procode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "procode")).ToString();
                //if (procode.Substring(0, 7) != "0101001")
                //{
                //    TextBox Runner = (TextBox)e.Row.FindControl("gvtxtRunnerAmt");
                //    Runner.Enabled = false;
                //    TextBox rejamt = (TextBox)e.Row.FindControl("gvtxtRejAmt");
                //    rejamt.Enabled = false;
                //    TextBox purgamt = (TextBox)e.Row.FindControl("gvtxtpurgAmt");
                //    purgamt.Enabled = false;

                //}


            }
        }

        private void GetLineFloor()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = (this.Request.QueryString["Type"] == "EntrySemi") ? "Semi" : "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETLINEMACH", Type, "", "", "", "", "", "", "", "");
            ddlmach.DataTextField = "sirdesc";
            ddlmach.DataValueField = "sircode";
            ddlmach.DataSource = ds1.Tables[0];
            ddlmach.DataBind();
            if (ASTUtility.Left(comcod, 2) != "87")
            {
                this.ddlmach.SelectedValue = "000000000000";
            }
        }
        private void GETPRDHOur()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODHOUR", "", "", "", "", "", "", "", "", "");
            //this.ddlhour.DataTextField = "gdesc";
            //this.ddlhour.DataValueField = "gcod";
            //this.ddlhour.DataSource = ds1.Tables[2];
            //this.ddlhour.DataBind();
            //if (ASTUtility.Left(comcod, 2) != "87")
            //{
            //    this.ddlhour.SelectedValue = "00000";
            //}


            //this.ddlshift.DataTextField = "gdesc";
            //this.ddlshift.DataValueField = "gcod";
            //this.ddlshift.DataSource = ds1.Tables[0];
            //this.ddlshift.DataBind();
            //if (ASTUtility.Left(comcod, 2) != "87")
            //{
            //    this.ddlshift.SelectedValue = "00000";
            //}

            //this.ddlstatus.DataTextField = "gdesc";
            //this.ddlstatus.DataValueField = "gcod";
            //this.ddlstatus.DataSource = ds1.Tables[1];
            //this.ddlstatus.DataBind();
            //if (ASTUtility.Left(comcod, 2) != "87")
            //{
            //    this.ddlstatus.SelectedValue = "00000";
            //}
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pstatus = dt1.Rows[0]["pstatus"].ToString();
            string prodcode = dt1.Rows[0]["prodcode"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pstatus"].ToString() == pstatus || dt1.Rows[j]["prodcode"].ToString() == prodcode)
                {

                    pstatus = dt1.Rows[j]["pstatus"].ToString();
                    prodcode = dt1.Rows[j]["prodcode"].ToString();
                    dt1.Rows[j]["prodesc"] = "";
                    //dt1.Rows[j]["section"] = "";
                }

                else
                {
                    pstatus = dt1.Rows[j]["pstatus"].ToString();
                    prodcode = dt1.Rows[j]["prodcode"].ToString();
                }

            }

            return dt1;
        }
        
        protected void ImgbtnFindProList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdat = Convert.ToDateTime(this.txtrcvdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPREPRODLIST", curdat, "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            this.ddlPrevPro.DataTextField = "prodesc";
            this.ddlPrevPro.DataValueField = "prodid";
            this.ddlPrevPro.DataSource = ds2.Tables[0];
            this.ddlPrevPro.DataBind();

        }


    }

}