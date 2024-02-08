using Microsoft.Reporting.WinForms;
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
    public partial class StoreMtReqTrnsGatePass : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                ((Label)this.Master.FindControl("lblTitle")).Text = "Gate Pass";

                this.txtCurAprovDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                if ("" != this.Request.QueryString["genno"].ToString().Trim())
                {
                    lbtnOk_Click(null, null);
                    this.ImgbtnFindRes_Click(null, null);
                }
                CommonButton();
            }
        }
        private void CommonButton()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            //  ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = (type == "approve" ? true : false);
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Approve";
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("OnClientClick", "return confirm('do you want to approve?')");

            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ////((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Send Mail";
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Text = "Recalculate";

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnLedger_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdatePurAprov_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnResFooterTotal_Click);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tblgetPass"];
            //
            string mGetpNo = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();

            DataTable dtGetPass = (DataTable) ViewState["tblgetPass"];
            DataTable dtGetPass2 = (DataTable) Session["tblgetPassSInfo"];

            var lst = dtGetPass.DataTableToList<SPEENTITY.C_11_RawInv.RptMatTransfer>();

            string orderqty = lst.Sum(x => x.getpqty).ToString("#,##0.00;(#,##0.00); ");

            string receiver = "";
            string receiveradd = "";
            string getpdat = "";
            if (dtGetPass2.Rows.Count > 0)
            {
                receiver = dtGetPass2.Rows[0]["receiver"].ToString();
                receiveradd = dtGetPass2.Rows[0]["recvadd"].ToString();
                getpdat = Convert.ToDateTime(dtGetPass2.Rows[0]["getpdat"]).ToString("dd-MMM-yyyy");
            }

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_11_RawInv.RptMatTransfer", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptitle", "GATE PASS"));
            Rpt1.SetParameters(new ReportParameter("serialno", mGetpNo));
            Rpt1.SetParameters(new ReportParameter("orderqty", orderqty));
            Rpt1.SetParameters(new ReportParameter("receiver", receiver));
            Rpt1.SetParameters(new ReportParameter("receiveradd", receiveradd));
            Rpt1.SetParameters(new ReportParameter("getpdat", getpdat));
            //Rpt1.SetParameters(new ReportParameter("date", fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //if (ds1 == null)
            //    return;



            //string fpactdesc = dt1.Rows[0]["tfpactdesc"].ToString();
            //string tpactdesc = dt1.Rows[0]["ttpactdesc"].ToString();
            //string mtrref = dt1.Rows[0]["mtrref"].ToString();
            //string mtrdat = Convert.ToDateTime(dt1.Rows[0]["mtrdat"]).ToString("dd.MM.yyyy");
            //ReportDocument rptFassttran = new MFGRPT.R_07_Inv.RptMaterialTrnsGatepass();

            //TextObject rptCname = rptFassttran.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtadd = rptFassttran.ReportDefinition.ReportObjects["txtCompanyadd"] as TextObject;
            //txtadd.Text = comadd;
            //TextObject rptProjectNameft = rptFassttran.ReportDefinition.ReportObjects["ProjectNamef"] as TextObject;
            //rptProjectNameft.Text = fpactdesc + "\n" + ds1.Tables[0].Rows[0]["tfpactadd"].ToString();

            //TextObject rpttxtmgatepno = rptFassttran.ReportDefinition.ReportObjects["rpttxtmgatepno"] as TextObject;
            //rpttxtmgatepno.Text = this.txtGatemPassNo.Text.Trim();
            //TextObject rptdate = rptFassttran.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = this.txtCurAprovDate.Text;


            //TextObject txttoproj = rptFassttran.ReportDefinition.ReportObjects["txttoproj"] as TextObject;
            //txttoproj.Text = tpactdesc + "\n" + ds1.Tables[0].Rows[0]["ttpactadd"].ToString();


            //TextObject txtmtrref = rptFassttran.ReportDefinition.ReportObjects["txtmtrref"] as TextObject;
            //txtmtrref.Text = "MTRF No: " + mtrref;
            //TextObject txtmtrdat = rptFassttran.ReportDefinition.ReportObjects["txtmtrdat"] as TextObject;
            //txtmtrdat.Text = mtrdat;
            //TextObject txtnarration = rptFassttran.ReportDefinition.ReportObjects["txtnarration"] as TextObject;
            //txtnarration.Text = this.txtgetpNarr.Text.Trim();

            //TextObject txtuserinfo = rptFassttran.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptFassttran.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptFassttran.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptFassttran;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnPrevAprovList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPREGETPASSNO", CurDate1,
                          "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.Items.Clear();
            this.ddlPrevList.DataTextField = "getpno1";
            this.ddlPrevList.DataValueField = "getpno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.lbtnPrevAprovList.Visible = true;
                this.ddlPrevList.Visible = true;
                this.lblGatePassNo1.Text = "GPN" + DateTime.Today.ToString("MM") + "-";
                this.txtCurAprovDate.Enabled = true;
               
              //  this.txtResSearch.Text = "";
                this.ddlPrevList.Items.Clear();
                this.ddlResList.Items.Clear();
                this.ddlResourcelist.Items.Clear();
                this.ddlSpecification.Items.Clear();
               
                this.txtgetpNarr.Text = "";
                this.gvAprovInfo.DataSource = null;
                this.gvAprovInfo.DataBind();
                this.Panel1.Visible = false;
                this.lbtnOk.Text = "Ok";

                return;
            }

            this.lbtnPrevAprovList.Visible = false;
            this.ddlPrevList.Visible = false;
            
            this.Panel1.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Pass_Info();
        }


        private void VisibleEntry()
        {

            //this.lCurAppdate.Visible = true;
            //this.lcurApprNo.Visible = true ;
            //this.txtCurAprovDate.Visible = true;
            //this.lblCurAprovNo1.Visible = true;
            //this.txtCurAprovNo2.Visible = true;

            //this.lbtnAprove.Visible = true;
            //this.lblResList.Visible = false;
            //this.lblResList0.Visible = false;
            //this.lblResList1.Visible = false;
            //this.txtResSearch.Visible = false;
            //this.txtSupSearch.Visible = false;
            //this.ImgbtnFindRes.Visible = false;
            //this.ImgbtnFindSup.Visible = false;
            //this.lbtnSelectRes.Visible = false;
            //this.lbtnSelectAll.Visible = false;
            //this.lblSpecification.Visible = false;
            //this.ddlSpecification.Visible = false;
            //this.ddlPayType.Visible = false;
            //this.ddlResList.Visible = false;
            //this.ddlSupList.Visible = false;
            //this.lblResList2.Visible = false;
            //this.ddlResourcelist.Visible = false;

        }
        protected void GetGetPassNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mGPassNo = "NEWGPASS";
            if (this.ddlPrevList.Items.Count > 0)
            {
                mGPassNo = this.ddlPrevList.SelectedValue.ToString();
            }

            if (mGPassNo == "NEWGPASS")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETLASTGETPNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblGatePassNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtGatePassNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                    this.ddlPrevList.DataTextField = "maxno1";
                    this.ddlPrevList.DataValueField = "maxno";
                    this.ddlPrevList.DataSource = ds1.Tables[0];
                    this.ddlPrevList.DataBind();
                }

            }
        }



        protected void Get_Pass_Info()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mGPassNo = "NEWGPASS";
            if (this.ddlPrevList.Items.Count > 0)
            {
                mGPassNo = this.ddlPrevList.SelectedValue.ToString();
                this.txtCurAprovDate.Enabled = false;
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPURGERPASSINFO", mGPassNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblgetPass"] = this.HiddenSameData(ds1.Tables[0]);

            Session["tblgetPassSInfo"] = ds1.Tables[1];
            //this.lbtnResFooterTotal_Click(null, null);


            if (mGPassNo == "NEWGPASS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETLASTGETPNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblGatePassNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtGatePassNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblGatePassNo1.Text = ds1.Tables[1].Rows[0]["getpno1"].ToString().Substring(0, 6);
            this.txtGatePassNo2.Text = ds1.Tables[1].Rows[0]["getpno1"].ToString().Substring(6, 5);
            this.txtGatemPassNo.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.txtCurAprovDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["getpdat"]).ToString("dd.MM.yyyy");
            this.txtgetpNarr.Text = ds1.Tables[1].Rows[0]["getpnar"].ToString();
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblgetPass"];

            this.gvAprovInfo.DataSource = dt;
            this.gvAprovInfo.DataBind();
        }

        private void FooterCalculation()
        {
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            if (tbl1.Rows.Count == 0)
                return;
            ((Label)this.gvAprovInfo.FooterRow.FindControl("lblgvFooterTAprovAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(aprovamt)", "")) ?
                    0.00 : tbl1.Compute("Sum(aprovamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "mtreqno";
            dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            // string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else

                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }


        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SerchText = "%%";
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETMTREQLIST", CurDate1, SerchText, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ViewState["tblsp"] = ds1.Tables[0];
            ViewState["tblRes"] = ds1.Tables[1];
            this.ddlResList.DataTextField = "textfield";
            this.ddlResList.DataValueField = "valuefiled";
            this.ddlResList.DataSource = ds1.Tables[2];
            this.ddlResList.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlResList.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
            this.ddlResList_SelectedIndexChanged(null, null);

        }

        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblRes"];
            string reqno = this.ddlResList.SelectedValue.ToString();

            DataView dv = dtres.DefaultView;
            this.ddlResourcelist.Items.Clear();
            dv.RowFilter = "mtreqno in ('" + reqno + "')";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";
            DataTable dtd = dv.ToTable();
            this.ddlResourcelist.DataTextField = "rsirdesc";
            this.ddlResourcelist.DataValueField = "rsircode";
            this.ddlResourcelist.DataSource = dv.ToTable();
            this.ddlResourcelist.DataBind();
            this.ddlResourcelist_SelectedIndexChanged(null, null);
        }
        protected void ddlResourcelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblsp"];
            string reqno = this.ddlResList.SelectedValue.ToString();
            string rsircode = this.ddlResourcelist.SelectedValue.ToString();
            DataView dv = dtres.DefaultView;

            dv.RowFilter = "mtreqno='" + reqno + "' and  rsircode='" + rsircode + "'";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";

            this.ddlSpecification.DataTextField = "textfield";
            this.ddlSpecification.DataValueField = "valuefiled";
            this.ddlSpecification.DataSource = dv.ToTable();
            this.ddlSpecification.DataBind();

            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.lbtnSelectAll_Click(null, null);
            }

        }




        protected void Session_tblAprov_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvAprovInfo.Rows.Count; j++)
            {



                double getpqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvaprovedQty")).Text.Trim()));
                double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvAprovInfo.Rows[j].FindControl("lblgvaprovRate")).Text.Trim()));
                double getpamt = getpqty * rate;
                TblRowIndex2 = (this.gvAprovInfo.PageIndex) * this.gvAprovInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["getpqty"] = getpqty;
                tbl1.Rows[TblRowIndex2]["getpamt"] = getpamt;
            }

            ViewState["tblgetPass"] = tbl1;
        }
        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {


            this.Session_tblAprov_Update();
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            string mReqNo = this.ddlSpecification.SelectedValue.ToString().Substring(0, 14);
            //string mProgNo = this.ddlResList.SelectedValue.ToString().Substring(14, 14);
            string mResCode = this.ddlSpecification.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCod = this.ddlSpecification.SelectedValue.ToString().Substring(26, 12);

            DataRow[] dr2 = tbl1.Select("mtreqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                        "' and spcfcod = '" + mSpcfCod + "'");

            if (dr2.Length == 0)
            {

                // (getpno , getpno1, mtreqno, mtreqno1, rsircode, spcfcod , rsirdesc, spcfdesc, rsirunit, mtrfqty, getpqty, rate ,getpamt, mtrdat, mtrref

                DataRow dr1 = tbl1.NewRow();
                dr1["mtreqno"] = mReqNo;
                dr1["rsircode"] = mResCode;
                dr1["spcfcod"] = mSpcfCod;

                DataTable tbl2 = (DataTable)ViewState["tblsp"];
                DataRow[] dr3 = tbl2.Select("mtreqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                        "' and spcfcod = '" + mSpcfCod + "'");
                dr1["mtreqno1"] = dr3[0]["mtreqno1"];
                dr1["mtrref"] = dr3[0]["mtrref"];
                dr1["mtrdat"] = dr3[0]["mtrdat"];
                dr1["tfpactcode"] = dr3[0]["tfpactcode"];
                dr1["ttpactcode"] = dr3[0]["ttpactcode"];
                dr1["tfpactdesc"] = dr3[0]["tfpactdesc"];
                dr1["ttpactdesc"] = dr3[0]["ttpactdesc"];
                dr1["rsirdesc"] = dr3[0]["rsirdesc"];
                dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["mtrfqty"] = dr3[0]["mtrfqty"];
                dr1["getpqty"] = dr3[0]["balqty"];
                dr1["balqty"] = dr3[0]["balqty"];
                dr1["rate"] = dr3[0]["mtrfrat"];
                dr1["getpamt"] = dr3[0]["mtrfamt"];
                dr1["forder"] = dr3[0]["forder"];
                dr1["torder"] = dr3[0]["torder"];
                tbl1.Rows.Add(dr1);
            }

            ViewState["tblgetPass"] = this.HiddenSameData(tbl1);
            this.Data_Bind();



        }

        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {


            this.Session_tblAprov_Update();
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];

            string mReqNo = this.ddlSpecification.SelectedValue.ToString().Substring(0, 14);
            //string mProgNo = this.ddlResList.SelectedValue.ToString().Substring(14, 14);
            string mResCode = this.ddlSpecification.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCod = this.ddlSpecification.SelectedValue.ToString().Substring(26, 12);


            DataTable tbl2 = (DataTable)ViewState["tblsp"];
            DataView dv1 = tbl2.DefaultView;

            dv1.RowFilter = "mtreqno in('" + mReqNo + "')";
            tbl2 = dv1.ToTable();

            DataRow[] dr2 = tbl1.Select("mtreqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                        "' and spcfcod = '" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();

                    dr1["mtreqno"] = mReqNo;
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();


                    dr1["mtreqno1"] = tbl2.Rows[i]["mtreqno1"].ToString();
                    dr1["orderno"] = "";
                    dr1["mtrref"] = tbl2.Rows[i]["mtrref"].ToString();
                    dr1["mtrdat"] = tbl2.Rows[i]["mtrdat"].ToString();
                    dr1["tfpactcode"] = tbl2.Rows[i]["tfpactcode"].ToString();


                    dr1["tfpactdesc"] = tbl2.Rows[i]["tfpactdesc"].ToString();
                    dr1["ttpactdesc"] = tbl2.Rows[i]["ttpactdesc"].ToString();

                    dr1["rsirdesc"] = tbl2.Rows[i]["rsirdesc"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["mtrfqty"] = tbl2.Rows[i]["mtrfqty"].ToString();
                    dr1["getpqty"] = tbl2.Rows[i]["balqty"].ToString();
                    dr1["balqty"] = tbl2.Rows[i]["balqty"].ToString();
                    dr1["rate"] = tbl2.Rows[i]["mtrfrat"].ToString();
                    dr1["getpamt"] = tbl2.Rows[i]["mtrfamt"].ToString();
                    dr1["forder"] = tbl2.Rows[i]["forder"].ToString();
                    dr1["torder"] = tbl2.Rows[i]["torder"].ToString();
                    dr1["color"] = tbl2.Rows[i]["color"].ToString();
                    
                    tbl1.Rows.Add(dr1);
                }

                ViewState["tblgetPass"] = this.HiddenSameData(tbl1);
                this.Data_Bind();
            }




        }



        private DataTable HiddenSameData1(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblAprov_Update();
            this.gvAprovInfo.PageIndex = ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.Data_Bind();
        }
        protected void lbtnUpdatePurAprov_Click(object sender, EventArgs e)
        {

            string tmsg = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();


            this.Session_tblAprov_Update();

            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            DataRow[] dr = tbl1.Select("getpqty>0");
            if (dr.Length == 0)
            {

                tmsg = "Please Input Order Qty";               
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                return;
            }
            if (this.ddlPrevList.Items.Count == 0)
                this.GetGetPassNo();



            //log Report
            string mmGetpdat = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mGetpNo = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();
            string getpref = this.txtGatemPassNo.Text.ToString();
            string mtrnar = this.txtgetpNarr.Text.ToString();



            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string PostedDate = System.DateTime.Today.ToString("dd-MMM-yyyy");



            //For Balace Req Qty

            if (this.Request.QueryString["Type"].ToString().Trim() == "Entry")
            {

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mtREQNO = tbl1.Rows[i]["mtreqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mspcfcod = tbl1.Rows[i]["rsircode"].ToString();
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "BALMTREQQTY", mtREQNO, mRSIRCODE, mspcfcod, "", "", "", "", "", "");
                    if (ds.Tables[0].Rows.Count == 0) continue;
                    else if (Convert.ToDouble(ds.Tables[0].Rows[0]["balqty"]) <= 0)
                    {
                        tmsg = "There is no balance qty in Requisition";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                        return; 
                    }

                }

            }


            ////////

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPREQGPASS", "PURREQGPB", mGetpNo, mmGetpdat, getpref, mtrnar,
                        PostedByid, Posttrmid, PostSession, PostedDate, "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
      
                tmsg = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                return;
            }


            foreach (DataRow dr1 in tbl1.Rows)
            {

                string mtREQNO = dr1["mtreqno"].ToString();

                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(dr1["mtrdat"].ToString()), Convert.ToDateTime(mmGetpdat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved Date is equal or greater Requisition Date');", true);
                    return;
                }




                string mRSIRCODE = dr1["rsircode"].ToString();
                string mSPCFCOD = dr1["spcfcod"].ToString();
                double getpqty = Convert.ToDouble(dr1["getpqty"]);
                string getpamt = Convert.ToDouble(dr1["getpamt"]).ToString();
                double mtrfqty = Convert.ToDouble(dr1["getpqty"]);


                // comcod, getpno,mtreqno, rsircode, spcfcod,  qty, amt

                if (mtrfqty >= getpqty)
                {

                    if (getpqty > 0)
                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPREQGPASS", "PURREQGPA",
                                    mGetpNo, mtREQNO, mRSIRCODE, mSPCFCOD, getpqty.ToString(), getpamt, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        tmsg = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);
                        return;

                       
                    }

                }

                else
                {

                    tmsg = "Order Qty Less then or Equal Balance Qty";                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);

                    return;

                }

            }
            this.txtCurAprovDate.Enabled = false;
            
            tmsg = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Order Process";
                string eventdesc = "Update Process";
                string eventdesc2 = mGetpNo;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }




        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }




        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblAprov_Update();
            this.Data_Bind();

            string tmsg = "Data Recalculate";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);
        }







        protected void gvAprovInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAprovInfo.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvAprovInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //this.gvAprovInfo.EditIndex = e.NewEditIndex;
            //this.gvAprovInfo_DataBind();

            //// Supplier
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string mResCode = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            //string mSupCode = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvResCod1")).Text.Trim();
            //string mSpcfCod = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvSpcfCod")).Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;

            //DropDownList ddl1 = (DropDownList)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("ddlSupname");
            //ddl1.DataTextField = "ssirdesc1";
            //ddl1.DataValueField = "ssircode";
            //ddl1.DataSource =ds1.Tables[0];
            //ddl1.DataBind();
            //ddl1.SelectedValue = mSupCode;

            //// Specification

            //DropDownList ddlspeci = (DropDownList)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("ddlspecification");
            //ddlspeci.DataTextField = "spcfdesc";
            //ddlspeci.DataValueField = "spcfcod";
            //ddlspeci.DataSource = ds1.Tables[1];
            //ddlspeci.DataBind();
            //ddlspeci.SelectedValue = mSpcfCod;





        }



        protected void gvAprovInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)ViewState["tblgetPass"];
            string mAPROVNO = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();
            string reqno = ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lblgvReqNo")).Text.Trim();
            string rescode = ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "DELETREQGATELIST", mAPROVNO, reqno, rescode, "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {
                int rowindex = e.RowIndex;
                dt.Rows[rowindex].Delete();
            }


            DataView dv = dt.DefaultView;
            ViewState.Remove("tblgetPass");
            //            dv.RowFilter = ("rsircode<>''");
            ViewState["tblgetPass"] = dv.ToTable();
            this.Data_Bind();



        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
        //    string tmsg = "";
        //    DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string mAPROVNO = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();
        //    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEPURPROGMAM", mAPROVNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        //    if (!result)
        //    {

        //        tmsg = purData.ErrorObject["Msg"].ToString();
        //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + tmsg + "');", true);

        //        return;
        //    }

        //    tmsg = "Data Updated successfully";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + tmsg + "');", true);

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Order Process";
        //        string eventdesc = "Delete Process";
        //        string eventdesc2 = mAPROVNO;
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }
        }


    }
}