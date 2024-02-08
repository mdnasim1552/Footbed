using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;


namespace SPEWEB.F_11_RawInv
{
    public partial class StockAdjstmnt : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    //prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Material Stock Adjustment";
                GetProject();


                if (this.ddlmlccod.Items.Count == 0)
                {
                    this.GetSesson();
                }
                if (this.Request.QueryString["Type"] == "Approve" || (this.Request.QueryString["Type"] == "Entry" && this.Request.QueryString["genno"].Length != 0))
                {
                    this.ddlProject.SelectedValue = this.Request.QueryString["centrid"].ToString();
                    this.txtFDate.Text = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
                    this.ImgbtnFindReq_Click(null,null);
                    if(this.Request.QueryString["Type"] == "Approve")
                    {
                        this.propanel.Visible = false;

                    }
                }
                else
                {
                    //  this.Addwip.Visible = true;

                }

                CommonButton();
            }
        }
        private void CommonButton()
        {
            
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            if (this.Request.QueryString["Type"] == "Approve")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approve";
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkupdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnktotal_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

        }


        private void lnktotal_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                GetMatList();
                this.propanel.Visible = true;
                this.ddlProject.Enabled = false;
                this.ddlmlccod.Enabled = false;
                this.DdlSeason.Enabled = false;
                this.ddlStyle.Enabled = false;
                this.ddlPrevReqList.Visible = false;
                this.ImgbtnFindReq.Visible = false;

                this.GetSotckAdjData();
                this.Narationpanel.Visible = true;
                // this.ddlPrType_SelectedIndexChanged(null,null);
            }
            else
            {
                lbtnOk.Text = "Ok";
                this.txtReqNarr.Text = "";
                this.Narationpanel.Visible = false;
                this.propanel.Visible = false;
                this.ddlProject.Enabled = true;
                this.txtFDate.Enabled = true;
                this.DdlSeason.Enabled = true;
                this.ddlStyle.Enabled = true;
                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ddlmlccod.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                this.ImgbtnFindReq.Visible = true;

            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetProject()
        {

            // DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");


            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "%15%", "FxtAst", "", userid, "", "", "", "");

            if (ds1 == null)
                return;
            ViewState["tblStoreType"] = ds1.Tables[0];
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }

        private void GetMatList()
        {
            string comcod = this.GetCompCode();
            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = "%";
            string date = this.txtFDate.Text.Trim();
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

                        SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = '" + ar1 + "' ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }


            //  DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETMATLIST", mProject, mSrchTxt, date, "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.ddlProduct.Items.Clear();
            //    // this.ddlResSpcf.Items.Clear();
            //    return;
            //}
            //ViewState["tblMat"] = ds1.Tables[0];

            //ViewState["tblSpcf"] = ds1.Tables[1];
            //this.ddlProduct.DataTextField = "rsirdesc";
            //this.ddlProduct.DataValueField = "rsircode";
            //this.ddlProduct.DataSource = ds1.Tables[2];
            //this.ddlProduct.DataBind();
            //  this.ddlProduct_SelectedIndexChanged(null, null);
            // this.ImgbtnSpecification_Click(null, null);
            //this.GetSpecification();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "MATCODELIST", mProject, date, mSrchTxt, SearchInfo, "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for budget');", true);
                return;
            }
            ViewState["tblMat"] = ds1.Tables[0];
            //ViewState["tblSpcf"] = ds1.Tables[1];

            DataTable dt1 = ds1.Tables[0].DefaultView.ToTable(true, "rsircode", "resdesc1");

            this.ddlProduct.DataTextField = "resdesc1";
            this.ddlProduct.DataValueField = "rsircode";
            this.ddlProduct.DataSource = dt1;// ds1.Tables[0];
            this.ddlProduct.DataBind();
            this.ImgbtnSpecification_Click(null, null);


        }
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            string mResCode = this.ddlProduct.SelectedValue.ToString();
            //// string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            //this.ddlResSpcf.Items.Clear();
            //DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            //DataView dv1 = tbl1.DefaultView;
            //dv1.RowFilter = "rsircode = '" + mResCode + "' or spcfcod = '000000000000'";
            //DataTable dt = dv1.ToTable();

            //if (dt.Rows.Count > 1)
            //{
            //    dt.Rows[0].Delete();
            //}


            //this.ddlResSpcf.DataTextField = "spcfdesc";
            //this.ddlResSpcf.DataValueField = "spcfcod";
            //this.ddlResSpcf.DataSource = dt;
            //this.ddlResSpcf.DataBind();

            string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "SIRINF_MAT_SPCF_LIST", mResCode, "", "", "", "", "");

                this.ddlResSpcf.DataTextField = "spcfdesc";
                this.ddlResSpcf.DataValueField = "spcfcod";
                this.ddlResSpcf.DataSource = ds1.Tables[0];
                this.ddlResSpcf.DataBind();

                ViewState["tblSpcf"] = ds1.Tables[0];


            }

        protected void Load_SP()
        {


        }
        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();          
            string mrfno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            string CurDate1 = this.txtFDate.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_STOCK_MANAGEMENT", "GETPREV_ADJ_LIST", CurDate1,mrfno, "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "adjno1";
            this.ddlPrevReqList.DataValueField = "adjno";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();


            if (this.Request.QueryString["genno"].ToString().Length != 0)
            {
                this.ddlPrevReqList.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.lbtnOk_Click(null, null);
              
            }

        }
        private void GetSotckAdjData()
        {
            string comcod = this.GetCompCode();
            string centrid = this.ddlProject.SelectedValue.ToString();
            string dates = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string batchno = this.ddlmlccod.SelectedValue.ToString();
            string mAdjNo = "NEWADJ";
            if (this.ddlPrevReqList.Items.Count > 0)
            {
                this.txtFDate.Enabled = false;
                mAdjNo = this.ddlPrevReqList.SelectedValue.ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_STOCK_MANAGEMENT", "GET_STOCK_ADJS_INFO", mAdjNo, "", "", "", "", "");
            List<SPEENTITY.C_11_RawInv.EclassResources> lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.EclassResources>();
            ViewState["stockadjst"] = lst;
            if (mAdjNo == "NEWADJ")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_STOCK_MANAGEMENT", "GETLAST_ADJ_NO", dates, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurReqNo1.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
                }                
                return;
            }
            if (lst.Count == 0)
                return;

            this.lblCurReqNo1.Text = ds1.Tables[1].Rows[0]["adjno1"].ToString().Substring(0, 6);
            this.txtCurReqNo2.Text = ds1.Tables[1].Rows[0]["adjno1"].ToString().Substring(6, 5);
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["adjref"].ToString();
            this.txtFDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["adjdate"]).ToString("dd-MMM-yyyy");
            this.txtFDate.Enabled = false;
            this.Data_Bind();
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            List<SPEENTITY.C_11_RawInv.EclassResources> lst = (List<SPEENTITY.C_11_RawInv.EclassResources>)ViewState["stockadjst"];
            string centrid = this.ddlProject.SelectedValue.ToString();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string mlcdesc = this.ddlmlccod.SelectedItem.ToString();

            string dayid = this.ddlStyle.SelectedValue.ToString() == "000000000000" ? "00000000000000000000000000000000" : this.ddlStyle.SelectedValue.ToString().Substring(24,8); 
            string rescode = this.ddlProduct.SelectedValue.ToString();
            string resedesc = this.ddlProduct.SelectedItem.ToString();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            string spcfdesc = this.ddlResSpcf.SelectedItem.ToString();
            var checklit = lst.FindAll(p => p.rsircode == rescode && p.spcfcode == spcfcod);
            if (checklit.Count == 0)
            {
                lst.Add(new SPEENTITY.C_11_RawInv.EclassResources(mlccod,mlcdesc, dayid, rescode, resedesc, spcfcod, spcfdesc, 0.00, 0.00, 0.00));
            }


            ViewState["stockadjst"] = lst;
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            List<SPEENTITY.C_11_RawInv.EclassResources> lst = (List<SPEENTITY.C_11_RawInv.EclassResources>)ViewState["stockadjst"];
            if (lst.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            this.grvacc.DataSource = lst;
            this.grvacc.DataBind();

        }
        private void Save_Value()
        {
            List<SPEENTITY.C_11_RawInv.EclassResources> lst = (List<SPEENTITY.C_11_RawInv.EclassResources>)ViewState["stockadjst"];
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                //ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));//
                double qty = ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim()));
                double rat = ASTUtility.StrPosOrNagative(((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim()); //ASTUtility.StrPosOrNagative((Label)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim();//ASTUtility.StrPosOrNagative(this.txtBalance.Text.Trim());

                lst[i].resqty = qty;
                lst[i].rate = rat;
                lst[i].amt = rat * qty;

            }
            ViewState["stockadjst"] = lst;
        }

        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_11_RawInv.EclassResources> lst = (List<SPEENTITY.C_11_RawInv.EclassResources>)ViewState["stockadjst"];
            int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.RowIndex;
            lst.RemoveAt(rowindex);
            ViewState["stockadjst"] = lst;
            this.Data_Bind();
        }
        private void lnkupdate_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"] == "Approve")
            {
                this.ApproveStockAdjs();
            }
            else
            {
                this.UpdateStockAdjust();
            }


        }
        protected void GetReqNo()
        {
            string comcod = this.GetCompCode();
            string mREQNO = "NEWREQ";
            if (this.ddlPrevReqList.Items.Count > 0)
                mREQNO = this.ddlPrevReqList.SelectedValue.ToString();

            string mREQDAT = this.txtFDate.Text.Trim();
            if (mREQNO == "NEWREQ")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_STOCK_MANAGEMENT", "GETLAST_ADJ_NO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);

                    this.ddlPrevReqList.DataTextField = "maxreqno1";
                    this.ddlPrevReqList.DataValueField = "maxreqno";
                    this.ddlPrevReqList.DataSource = ds2.Tables[0];
                    this.ddlPrevReqList.DataBind();
                }
            }

        }
        private void UpdateStockAdjust()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            this.Save_Value();
            List<SPEENTITY.C_11_RawInv.EclassResources> lst = (List<SPEENTITY.C_11_RawInv.EclassResources>)ViewState["stockadjst"];
            var checklist = lst.FindAll(p => p.resqty == 0);
            if (checklist.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Qty is not 0');", true);
                return;
            }

            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
         
            string adjno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtFDate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();


            string centrid = this.ddlProject.SelectedValue.ToString();

            string dates = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string naration = this.txtReqNarr.Text.Trim().ToString();          
            DataTable dt1 = ASITUtility03.ListToDataTable(lst);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";
            bool result = purData.UpdateXmlTransInfo(comcod, "SP_STOCK_MANAGEMENT", "UPDATE_STOCK_ADJUST", ds1, null, null, adjno, centrid, dates, naration, PostedDat, PostedByid, PostedSession, Postedtrmid);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
                return;
            }
        }
        private void ApproveStockAdjs()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string centrid = this.ddlProject.SelectedValue.ToString();
            string dates = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string adjno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtFDate.Text.Trim().Substring(7, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_STOCK_MANAGEMENT", "APPROVE_STOCK_ADJUST", centrid, dates, adjno, PostedDat, PostedByid, PostedSession, Postedtrmid);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

                return;
            }
        }
        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];

            string season = Request.QueryString["genno"].ToString().Length == 0 ? hst["season"].ToString() : Request.QueryString["genno"].ToString();

            this.DdlSeason.SelectedValue = season;

            DdlSeason_SelectedIndexChanged(null, null);
        }
        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBatch();
        }
        private void GetBatch()
        {

            string comcod = GetCompCode();
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 =purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblordstyle"] = ds1.Tables[0];
            ds1.Tables[1].Rows.Add("", "000000000000", "None");

            this.ddlmlccod.DataTextField = "mlcdesc";
            this.ddlmlccod.DataValueField = "mlccod";
            this.ddlmlccod.DataSource = ds1.Tables[1];
            this.ddlmlccod.SelectedValue = "000000000000";
            this.ddlmlccod.DataBind();

            this.ddlmlccod_SelectedIndexChanged(null, null);

        }

        protected void ddlmlccod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlmlccod.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode1");

            dt1.Rows.Add ("None", "000000000000");
            this.ddlStyle.DataTextField = "styledesc2";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();

            //if (Request.QueryString["dayid"].ToString().Length > 0)
            //{
            //    this.ddlStyle.SelectedValue = Request.QueryString["dayid"].ToString();

            //}

        }
        protected void LbtnBatch_Click(object sender, EventArgs e)
        {
            this.GetBatch();
        }

        protected void ddlPrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.ddlPrType.SelectedValue.ToString();
            if (type == "MAT")
            {

                this.GetMatList();
            }
            else
            {

                this.Load_SP();
            }

        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnSpecification_Click(null, null);
        }
    }
}