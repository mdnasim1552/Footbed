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
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPEENTITY.C_15_Pro;

namespace SPEWEB.F_15_Pro
{
    public partial class MatRetInWIP : System.Web.UI.Page
    {
        //UserManSales objUserService = new UserManSales();
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS RETURN IN WORK IN PROCESS (WIP)";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSeason();
                this.DdlSeason_SelectedIndexChanged(null, null);
                this.GetProMaterial();
                this.GetlastRetNum();
                string qType = this.Request.QueryString["Type"].ToString();
                if (qType == "Approved")
                {
                    this.PreviousList();
                }

                this.CommonButton();
            }
        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;      
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Delete Selected Item";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).OnClientClick = "return confirm('Do you want to Remove Selected Item?')";
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).CssClass = "btn btn-info btn-sm";
  
            string qType = this.Request.QueryString["Type"].ToString();
            if (qType == "Entry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Text = "Approved";

            }

        }

        public void GetlastRetNum()
        {
            if (this.lbtnOk.Text.Trim() == "New") return;

            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet Ds = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "LASTMRETNO", date, "", "", "", "", "", "", "", "");  //table desc 2
            if (Ds.Tables[0].Rows.Count > 0)
            {
                this.txtRetnum.Text = Ds.Tables[0].Rows[0]["maxno"].ToString();
            }


            //string comcod = this.GetCompCode();
            //string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string mCPRNo = "NEWRET";

            //if (this.ddlPrevList.Items.Count > 0)
            //    mCPRNo = this.ddlPrevList.SelectedValue.ToString();

            //if (mCPRNo == "NEWRET")
            //{   
            //    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "LASTMRETNO", date, "", "", "", "", "", "", "", "");  //table desc 2

            //    if (ds2 == null)
            //        return;

            //    this.txtRetnum.Text = ds2.Tables[0].Rows[0]["maxno"].ToString();

            //this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
            //this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);

            //this.ddlPrevList.DataTextField = "maxno";
            //this.ddlPrevList.DataValueField = "maxno";
            //this.ddlPrevList.DataSource = ds2.Tables[0];
            //this.ddlPrevList.DataBind();
            //}
        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";
            }

            this.DdlSeason_SelectedIndexChanged(null, null);
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblordstyle"] = ds1.Tables[0];
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();

            this.ddlwip.DataTextField = "styledesc2";
            this.ddlwip.DataValueField = "stylecode2";
            this.ddlwip.DataSource = dt1;
            this.ddlwip.DataBind();
        }

        private void GetProMaterial()
        {
            string comcod = this.GetCompCode();
            string coderange = "04%";
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD ", coderange, "", "", "", "", "", "", "", "");
            Session["tblmatgroup"] = ds3.Tables[1];
            this.ddlMatList.DataSource = ds3.Tables[0];
            this.ddlMatList.DataTextField = "sircode";
            this.ddlMatList.DataValueField = "sircode1";
            this.ddlMatList.DataBind();
            this.ddlMatList_SelectedIndexChanged(null, null);

        }

        protected void ddlMatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mathead = this.ddlMatList.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblmatgroup"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";
            this.ddlMatGroup.DataTextField = "sircode";
            this.ddlMatGroup.DataValueField = "sircode1";
            this.ddlMatGroup.DataSource = dv.ToTable();
            this.ddlMatGroup.DataBind();

            //if (mathead != "0400%")
            //   ddlOthersBookSegment_SelectedIndexChanged(null, null);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(Approved_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(LbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkbtnDelete_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Approved_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            string retissuno = this.ddlPrevList.SelectedValue.ToString();
            string Date = this.txtCurDate.Text.ToString();

            List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> lst = (List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>)ViewState["meterialinfo"];
            if (lst.Count == 0)
                return;

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MATISSUE_RETURN", "APPRETURNMAT", retissuno, Date, usrid, sessionid, trmid, PostedDat);
            if (!result)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            
            //((Label)this.Master.FindControl("lblmsg")).Text = "Approved";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Approved');", true);
        }

        //private void GetWIPName()
        //{
        //    string comcod = this.GetCompCode();
        //    DataSet Ds = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "GETWIPSTOREISSUE", "", "", "", "", "", "", "", "", "");  //table desc 2
        //    if (Ds == null)
        //        return;

        //    var wipinfolist = Ds.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist>();
        //    ViewState["wipinfo"] = wipinfolist;

        //    IEnumerable<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist> Query12 = (from wip in wipinfolist
        //                                                                             //where transport.actcode.Equals(Centercode)
        //                                                                         orderby wip.bactcode ascending
        //                                                                         select wip).GroupBy(n => new { n.bactcode, n.bactdesc }).Select(g => g.FirstOrDefault());
        //    this.ddlwip.DataTextField = "bactdesc";
        //    this.ddlwip.DataValueField = "bactcode";
        //    this.ddlwip.DataSource = Query12.ToList();
        //    this.ddlwip.DataBind();
        //    this.ddlwip_SelectedIndexChanged(null, null);

        //}

        private void GetStore()
        {
            string wipNo = this.ddlwip.SelectedValue.ToString();
            List<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist> lst = (List<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist>)ViewState["wipinfo"];

            List<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist> lststore = lst.FindAll((p => p.bactcode == wipNo));

            IEnumerable<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist> Query12 = (from store in lststore
                                                                                     //where transport.actcode.Equals(Centercode)
                                                                                 orderby store.actcode ascending
                                                                                 select store).GroupBy(n => new { n.actcode, n.actdesc }).Select(g => g.FirstOrDefault());


            this.ddlStore.DataTextField = "actdesc";
            this.ddlStore.DataValueField = "actcode";
            this.ddlStore.DataSource = Query12.ToList();
            this.ddlStore.DataBind();
            this.ddlStore_SelectedIndexChanged(null, null);

        }

        private void GetIssueNo()
        {
            string wipNo = this.ddlwip.SelectedValue.ToString();
            string store = this.ddlStore.SelectedValue.ToString();
            List<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist> lst = (List<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist>)ViewState["wipinfo"];

            List<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist> lstissue = lst.FindAll((p => p.bactcode == wipNo && p.actcode == store));
            IEnumerable<SPEENTITY.C_15_Pro.MatReturnwip.wipinfolist> Query12 = (from issue in lstissue
                                                                                     //where transport.actcode.Equals(Centercode)
                                                                                 orderby issue.actcode ascending
                                                                                 select issue).GroupBy(n => new { n.misuno }).Select(g => g.FirstOrDefault());

            this.ddlissuno.DataTextField = "misuno1";
            this.ddlissuno.DataValueField = "misuno";
            this.ddlissuno.DataSource = Query12.ToList();
            this.ddlissuno.DataBind();


        }

        protected void ddlPrevList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string retissuno = this.ddlPrevList.SelectedValue;
            List<SPEENTITY.C_15_Pro.MatReturnwip.RetmetList> lst = (List<SPEENTITY.C_15_Pro.MatReturnwip.RetmetList>)ViewState["tblretiss"];

            List<SPEENTITY.C_15_Pro.MatReturnwip.RetmetList> lstissue = lst.FindAll((p => p.retissuno == retissuno));

            this.ddlissuno.DataTextField = "misuno1";
            this.ddlissuno.DataValueField = "misuno";
            this.ddlissuno.DataSource = lstissue;
            this.ddlissuno.DataBind();

            this.ddlStore.DataTextField = "actdesc";
            this.ddlStore.DataValueField = "actcode";
            this.ddlStore.DataSource = lstissue;
            this.ddlStore.DataBind();

            this.ddlwip.DataTextField = "bactdesc";
            this.ddlwip.DataValueField = "bactcode";
            this.ddlwip.DataSource = lstissue;
            this.ddlwip.DataBind();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");

            string retissuno = this.txtRetnum.Text;
            string retissdat = this.txtCurDate.Text.ToString();
            string wip = this.ddlwip.SelectedItem.ToString();
            string store = this.ddlStore.SelectedItem.ToString();
            string matissuno = this.ddlissuno.SelectedItem.ToString();
            var lst = (List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>)ViewState["meterialinfo"];
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_07_Inv.RptRetMat", lst, null, null);

            //rpt1.SetParameters(new ReportParameter("Rptusirdesc", usirdesc));

            //rpt1.SetParameters(new ReportParameter("rptDt", rptDt));

            //rpt1.SetParameters(new ReportParameter("comadd", comadd));

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("retissuno", retissuno));
            rpt1.SetParameters(new ReportParameter("wip", wip));
            rpt1.SetParameters(new ReportParameter("store", store));
            rpt1.SetParameters(new ReportParameter("matissuno", matissuno));
            rpt1.SetParameters(new ReportParameter("retissdat", retissdat));
            //    rpt1.SetParameters(new ReportParameter("EmpNam", " Employee Name: " + empname));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Material Return IN Work In Process"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.ddlwip.Enabled = true;
                this.ddlStore.Enabled = true;
                this.ddlissuno.Enabled = true;
                this.DdlSeason.Enabled = true;
                this.ddlMatList.Enabled = true;
                this.ddlMatGroup.Enabled = true;
                this.txtCurDate.Enabled = true;
                this.imgPreVious.Enabled = true;
                this.ddlPrevList.Enabled = true;
                this.ddlPrevList.Items.Clear();
                this.GetlastRetNum();

                this.gvmetinf.EditIndex = -1;
                this.gvmetinf.DataSource = null;
                this.gvmetinf.DataBind();
                this.lbtnOk.Text = "Ok";

                return;
            }

            this.ddlwip.Enabled = false;
            this.ddlStore.Enabled = false;
            this.ddlissuno.Enabled = false;
            this.imgPreVious.Enabled = false;
            this.ddlPrevList.Enabled = false;
            this.DdlSeason.Enabled = false;
            this.ddlMatList.Enabled = false;
            this.ddlMatGroup.Enabled = false;
            this.txtCurDate.Enabled = false;

            this.lbtnOk.Text = "New";
            this.Get_Info();

        }

        protected void Get_Info()
        {
            //   string Centrid = this.ddlStore.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurDate.Text;
            string issuno = this.ddlissuno.SelectedValue.ToString();
            string mReturn = "NEWMRET";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mReturn = this.ddlPrevList.SelectedValue.ToString();

            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "GETPRMETERIALINFO", issuno, mReturn);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>();

                if (lst == null)
                    return;
                ViewState["meterialinfo"] = HiddenSameData(lst);
            }
            if (mReturn == "NEWMRET")
            {

                string date = this.txtCurDate.Text;
                DataSet Ds = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "LASTMRETNO", date, "", "", "", "", "", "", "", "");  //table desc 2
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    this.txtRetnum.Text = Ds.Tables[0].Rows[0]["maxno"].ToString();
                }

                if (Ds.Tables[0].Rows.Count == 0)
                    return;

                this.GeTData();
                return;
            }

            //List<MFGOBJ.C_22_Sal.EClassReturn.EClassReturnNo> lstord = objUserService.GetReturnNo(Centrid, mReturn);
            //if (lstord == null)
            //    return;


            this.txtRetnum.Text = ds1.Tables[1].Rows[0]["retissuno"].ToString();
            //this.txtCurNo2.Text = lstord[0].retmemo1.ToString().Substring(6, 5);

            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["retissdat"]).ToString("dd-MMM-yyyy");
            //this.txtBillNarr.Text = lst[0].remarks.ToString();


            this.Data_Bind();

        }

        public void GeTData()
        {
            string comcod = this.GetCompCode();
            string issuno = this.ddlissuno.SelectedValue.ToString();
            string season = this.DdlSeason.SelectedValue.ToString();
            string matlist = (this.ddlMatList.SelectedValue.Substring(0, 4).ToString()=="0400") ? "04%" : this.ddlMatList.SelectedValue.Substring(0,4).ToString() + "%";
            string matgroup = (this.ddlMatGroup.SelectedValue.Substring(0, 7).ToString()=="0400000") ? "04%" : this.ddlMatGroup.SelectedValue.Substring(0, 7).ToString()+"%";
            string batchcode = "";
            foreach (ListItem item in ddlwip.Items)
            {
                if (item.Selected)
                {

                    batchcode += item.Value;
                }
            }
            batchcode = (ASTUtility.Left(batchcode, 12) == "000000000000") ? "%" : batchcode;
            DataSet Ds = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "GETMETERIALINFO", batchcode, matlist, matgroup, "", "", "", "", "", "");  //table desc 2

            if (Ds == null)
                return;

            var meteriallist = Ds.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>();
            ViewState["meterialinfo"] = meteriallist;
            this.Data_Bind();
        }

        protected void Data_Bind()
        {

            try
            {
                List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> lst = (List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>)ViewState["meterialinfo"];
                
                this.gvmetinf.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                this.gvmetinf.DataSource = lst;
                this.gvmetinf.DataBind();

                this.FooterCalCulation(lst);
                // this.txtOvDis.Text = lst[0].ovdis.ToString("#,##0;(#,##0); ");
            }
            catch (Exception e)
            {

                //((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //((Label)this.Master.FindControl("lblmsg")).Text = e.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ e.Message + "');", true);

            }
        }

        private void FooterCalCulation(List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> lst)
        {

            if (lst.Count == 0)
                return;

            ((Label)this.gvmetinf.FooterRow.FindControl("LblFgvRetQty")).Text = (lst.Select(p => p.rqty).Sum() == 0.00) ? "" : lst.Select(p => p.rqty).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvmetinf.FooterRow.FindControl("LblFgvRetamt")).Text = (lst.Select(p => p.ramt).Sum() == 0.00) ? "" : lst.Select(p => p.ramt).Sum().ToString("#,##0;(#,##0); ");
            //((Label)this.gvRetInfo.FooterRow.FindControl("lblgvFinvdis")).Text = (lst.Select(p => p.invdis).Sum() == 0.00) ? "" : lst.Select(p => p.invdis).Sum().ToString("#,##0;(#,##0); ");
            //((Label)this.gvRetInfo.FooterRow.FindControl("lblgvrFetindis")).Text = (lst.Select(p => p.retindis).Sum() == 0.00) ? "" : lst.Select(p => p.retindis).Sum().ToString("#,##0;(#,##0); ");
            //((Label)this.gvRetInfo.FooterRow.FindControl("lblgvFInvvat")).Text = (lst.Select(p => p.invvat).Sum() == 0.00) ? "" : lst.Select(p => p.invvat).Sum().ToString("#,##0;(#,##0); ");
            //((Label)this.gvRetInfo.FooterRow.FindControl("lblgvFRetvat")).Text = (lst.Select(p => p.retvat).Sum() == 0.00) ? "" : lst.Select(p => p.retvat).Sum().ToString("#,##0;(#,##0); ");


            //double ovDis = lst[0].ovdis;
            //double RetAmt = lst.Select(p => p.retamt).Sum();
            //double InvAmt = lst.Select(p => p.invamt).Sum();
            ////  this.txtRetOvDis.Text = ((InvAmt == 0) ? 0.00 : (ovDis * RetAmt) / InvAmt).ToString("#,##0;(#,##0); ");


        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            // this.lblmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string retissuno = this.txtRetnum.Text.ToString();
            string retissdat = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            string bactcode = this.ddlwip.SelectedValue.ToString();
            string storeid = this.ddlStore.SelectedValue.ToString();
            string missueno = this.ddlissuno.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            this.SaveValue();
            if (this.ddlPrevList.Items.Count == 0)
                this.GetlastRetNum();
            DataTable dt = ASITUtility03.ListToDataTable((List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>)ViewState["meterialinfo"]);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";

            var temp = ds1.GetXml();
            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "SAVEANDUPDATERETMET", ds1, null, null, retissuno, retissdat, usrid, PostedDat, "", "", "", "", "", "", "", "");

            if (result)
            {
                 ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
            }

        }

        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }

        protected void PreviousList()
        {

            string comcod = this.GetCompCode();
            string date = this.txtCurDate.Text;
            DataSet Ds = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "GETPRERETURN", date, "", "", "", "", "", "", "", "");  //table desc 2
            if (Ds == null)
                return;
            var retmetlist = Ds.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.MatReturnwip.RetmetList>();
            ViewState["tblretiss"] = retmetlist;

            IEnumerable<SPEENTITY.C_15_Pro.MatReturnwip.RetmetList> Query12 = (from retlist in retmetlist
                                                                                    //where transport.actcode.Equals(Centercode)
                                                                                orderby retlist.retissuno ascending
                                                                                select retlist).GroupBy(n => new { n.retissuno }).Select(g => g.FirstOrDefault());

            this.ddlPrevList.Visible = true;
            this.imgPreVious.Visible = true;
            this.ddlPrevList.DataTextField = "retissuno";
            this.ddlPrevList.DataValueField = "retissuno";
            this.ddlPrevList.DataSource = Query12.ToList();
            this.ddlPrevList.DataBind();
            this.ddlPrevList_SelectedIndexChanged(null, null);

        }


        //protected void lbtnTotal_Click(object sender, EventArgs e)
        //{
        //    this.SaveValue();
        //    Data_Bind();
        //}


        protected void ddlwip_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetStore();
        }


        private List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> HiddenSameData(List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> lst)
        {
            //if (lst.Count == 0)
            //    return lst;

            //int i = 0;
            ////string rescode = "";
            //string sdelno = "";
            //foreach (MFGOBJ.MatReturnwip.meteriallist c1 in lst)
            //{
            //    if (i == 0)
            //    {
            //        sdelno = c1.sdelno;
            //        i++;
            //        continue;

            //    }
            //    else if (c1.sdelno == sdelno)
            //    {
            //        c1.sdelno1 = "";
            //        //c1.sorderqty = 0.00;
            //        //c1.utodelqty = 0.00;
            //        //c1.balqty = 0.00;
            //    }
            //    sdelno = c1.sdelno;

            //}

            return lst;

        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetIssueNo();
        }
        protected void gvmetinf_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // this.SaveValue();
            this.gvmetinf.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void LbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        public void SaveValue()
        {
            List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> lst = (List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>)ViewState["meterialinfo"];

            int index;
            for (int j = 0; j < this.gvmetinf.Rows.Count; j++)
            {
                double Rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvmetinf.Rows[j].FindControl("lblinvrate")).Text.Trim()));
                double Retqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvmetinf.Rows[j].FindControl("txtgvRetqty")).Text.Trim()));

                double InvAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvmetinf.Rows[j].FindControl("lblgvInvamt")).Text.Trim()));
                double RetAmt = Retqty * Rate;
                index = (this.gvmetinf.PageIndex) * this.gvmetinf.PageSize + j;

                if (Retqty > lst[index].qty)
                {
                    //((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    //((Label)this.Master.FindControl("lblmsg")).Text = "Sorry! Return Qty Should be less then Or Equal Issue Qty ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry! Return Qty Should be less then Or Equal Issue Qty');", true);

                    return;
                }

                lst[index].rqty = Retqty;
                lst[index].ramt = RetAmt;

            }
            ViewState["meterialinfo"] = lst;
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> list1 = (List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>)ViewState["meterialinfo"];

            for (int i = 0; i < gvmetinf.Rows.Count; i++)
            {
                if (((CheckBox)gvmetinf.Rows[i].FindControl("chkbxMaterial")).Checked)
                {
                    //list1.RemoveAt(i);
                    list1[i].rqty = 0;
                }
            }

            this.DeleteMatFromWipReturn(list1);

            ViewState["meterialinfo"] = list1.Where(m => m.rqty != 0).ToList();
            this.Data_Bind();
        }

        private void DeleteMatFromWipReturn(List<MatReturnwip.meteriallist> list1)
        {
            string comcod = this.GetCompCode();
            string returnno = this.txtRetnum.Text.Trim();

            foreach (var material in list1)
            {
                if (material.rqty == 0)
                {
                    DataSet Ds = purData.GetTransInfo(comcod, "SP_ENTRY_MATISSUE_RETURN", "DELETE_MATERIAL_RETURN", returnno, material.rsircode, material.spcfcod, material.misuno, "", "", "", "", "");
                }
            }
        }

        protected void btnClearF_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist> list1 = (List<SPEENTITY.C_15_Pro.MatReturnwip.meteriallist>)ViewState["meterialinfo"];

            foreach (var item in list1)
            {
                item.rqty = 0;
            }
            ViewState["meterialinfo"] = list1;
            this.Data_Bind();
        }

        
    }
}