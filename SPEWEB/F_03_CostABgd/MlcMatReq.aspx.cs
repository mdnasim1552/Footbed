using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using AjaxControlToolkit;
using System.IO;

namespace SPEWEB.F_03_CostABgd
{
    public partial class MlcMatReq : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        Common CommonClass = new Common();
        ProcessAccess proc1 = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                TabName.Value = Request.Form[TabName.UniqueID];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Materials Requirement Against Order";
                this.CommonButton();
                this.Get_SupplierInfo();
                //this.GET_Region();
                //this.GET_Nom_Recomended_info();
                this.GetComponentList();
                //this.Get_material();
                this.GetProcess();
                this.GetLCCode();

                this.SelectView();
                if (this.Request.QueryString["actcode"].Length > 0)
                {
                    this.ddlmlccode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                }
                string type = this.Request.QueryString["Type"].ToString();
                if (type == "Entry")
                {
                    this.RadioButtonList1.SelectedIndex = 0;
                    RadioButtonList1_SelectedIndexChanged(null, null);
                }
                if (GetComeCode() == "5301")
                {
                    this.lblscondate.Visible = false;
                    this.TxtConfirmDate.Visible = false;
                }
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkbtnHisprice_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "Entry")
            {

                this.Multiview.ActiveViewIndex = 0;

            }
            else
            {
                this.lblarticle.Visible = false;
                //  this.ddlArticle.Visible = false;
                // this.ddlcolor.Visible = false;
                this.lblcolor.Visible = false;
                this.lbtncopyto.Visible = false;
                //  this.OkBtn.Visible = false;
                this.OkBtn_Click(null, null);
                this.Multiview.ActiveViewIndex = 1;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            }
        }

        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            if (this.Request.QueryString["Type"].ToString() == "Entry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Forward";
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).OnClientClick = "return confirm('Do you want to forwared?')";
            }
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";
            if (this.Request.QueryString["Type"].ToString() == "Approve")
            {

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do you want to approve?')";
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetLCCode()
        {
            string comcod = GetComeCode();
            string txtsrch = "%";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "DTLLCLIST", "", txtsrch, "", "", "", "", "", "", ""); ;
            if (ds1 == null)
                return;
            this.ddlmlccode.DataTextField = "actdesc1";
            this.ddlmlccode.DataValueField = "actcode";
            this.ddlmlccode.DataSource = ds1.Tables[0];
            this.ddlmlccode.DataBind();
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlmlccode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlmlccode.Enabled = false;
                this.ddlmlccode_SelectedIndexChanged(null, null);

            }
        }
        protected void OkBtn_Click(object sender, EventArgs e)
        {

            if (this.OkBtn.Text == "Ok")
            {
                this.OkBtn.Text = "New";
                this.ddlmlccode.Enabled = false;
                this.ddlcolor.Enabled = false;
                this.ddlArticle.Enabled = false;
                if (this.Request.QueryString["Type"].ToString() == "Approve")
                {
                    this.GetBomApproveData();
                }
                else
                {
                    this.GetBudgetInfo();
                    this.Data_Bind();
                }

                this.lblcolorwise.Visible = true;
                this.lblDircost.Visible = true;
                this.lblBomCost.Visible = true;
                this.DataPanel.Visible = true;
            }
            else
            {
                this.OkBtn.Text = "Ok";
                this.DataPanel.Visible = false;
                this.ddlcolor.Enabled = true;
                this.ddlArticle.Enabled = true;
                Session.Remove("tblimport");
                Session.Remove("tbllocal");
                Session.Remove("tbladdimport");
                Session.Remove("tbladdlocal");
                this.gvtopshet.DataSource = null;
                this.gvtopshet.DataBind();
                this.gvtotalbom.DataSource = null;
                this.gvtotalbom.DataBind();
                this.gvDirectCost.DataSource = null;
                this.gvDirectCost.DataBind();
                this.lblcolorwise.Visible = false;
                this.lblDircost.Visible = false;
                this.lblBomCost.Visible = false;


            }
        }
        private void GetBudgetInfo()
        {
            string comcod = this.GetComeCode();
            string mMLCCOD = this.ddlmlccode.SelectedValue.ToString();
            string styleid = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            string dayid = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_MATETIAL_AGAINST_ORDER", mMLCCOD, styleid, colorid, dayid, "", "", "", "");
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "GET_ORDER_PACKING_DETAILS", mMLCCOD, dayid, styleid, "", "", "", "");
            if (ds1 == null || ds2 == null)
                return;
            Session["tblimport"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            Session["tbllocal"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder> lst = ds1.Tables[3].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            Session["tblpackingdetails"] = ds2;
            Session["tbladdlocal"] = lst.FindAll(p => p.reqtype == "ADDLOCAL");
            Session["tbladdimport"] = lst.FindAll(p => p.reqtype == "ADDIMPORT");
            Session["tblcommoncost"] = ds1.Tables[4].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            if (ds1.Tables[2].Rows.Count != 0)
            {
                this.BuyerName.Text = ds1.Tables[2].Rows[0]["buyername"].ToString();
                this.Txtprddate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["estprddat"]).ToString("dd-MMM-yyyy");
                this.txtOrdDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["orddate"]).ToString("dd-MMM-yyyy");
                this.txtInspctDate.Text = ds1.Tables[2].Rows[0]["inspdat"].ToString() == "" ? DateTime.Now.ToString("dd-MMM-yyyy") : ds1.Tables[2].Rows[0]["inspdat"].ToString();
                this.lblttlorderqty.Text = Convert.ToDouble(ds1.Tables[2].Rows[0]["ordrqty"]).ToString("#,##0;(#,##0); ");
                this.lblcatedesc.Text = Convert.ToString(ds1.Tables[2].Rows[0]["catedesc"]);
                this.lblConfirm.Text = Convert.ToDouble(ds1.Tables[2].Rows[0]["conprice"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblartcle.Text = Convert.ToString(ds1.Tables[2].Rows[0]["artno"]);
                this.lblstyle.Text = Convert.ToString(ds1.Tables[2].Rows[0]["catedesc"]);
                this.lblcolorName.Text = this.ddlcolor.SelectedItem.ToString().Replace("(R)", "");
                this.lblOrderno.Text = Convert.ToString(ds1.Tables[2].Rows[0]["ordrno"]);
                this.TxtNotes.Text = Convert.ToString(ds1.Tables[2].Rows[0]["notes"]);
                this.TxtConfirmDate.Text = ds1.Tables[2].Rows[0]["confirmdate"].ToString() == "" ? "01-Jan-1900" : ds1.Tables[2].Rows[0]["confirmdate"].ToString();

            }
            //this.Data_Bind();
            this.Get_Fgsize();
        }

        public void GetBomApproveData()
        {
            string comcod = this.GetComeCode();
            string mMLCCOD = this.ddlmlccode.SelectedValue.ToString();
            string styleid = (this.ddlArticle.SelectedValue.ToString().Substring(0, 12) == "000000000000") ? "03%" : this.ddlArticle.SelectedValue.ToString().Substring(0, 12) + "%";
            string colorid = (this.ddlcolor.SelectedValue.ToString().Substring(0, 12) == "000000000000") ? "71%" : this.ddlcolor.SelectedValue.ToString().Substring(0, 12) + "%";
            string dayid = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_ORDERWISE_TOTAL_BOM", mMLCCOD, styleid, colorid, dayid, "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            Session["tblbomtotal"] = ds1.Tables[1];
            Session["tblbomtopshet"] = ds1.Tables[0];
            Session["tbldircost"] = ds1.Tables[2];
            this.gvtotalbom.DataSource = ds1.Tables[1];
            this.gvtotalbom.DataBind();
            this.gvtopshet.DataSource = ds1.Tables[0];
            this.gvtopshet.DataBind();
            this.gvDirectCost.DataSource = ds1.Tables[2];
            this.gvDirectCost.DataBind();
            this.FooterCalculation();
        }
        private void Get_SupplierInfo()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_SUPPLIER_INFORMATION", "", "", "", "", "", "", ""); ;

            Session["tblSupplier"] = ds1.Tables[0];
            Session["tblorigin"] = ds1.Tables[1];
            Session["tblnominated"] = ds1.Tables[2];
        }
        public void Get_Suppiler()
        {

            DataTable dt = (DataTable)Session["tblSupplier"];
            this.ddlSupliAddiImp.DataTextField = "ssirdesc";
            this.ddlSupliAddiImp.DataValueField = "ssircode";
            this.ddlSupliAddiImp.DataSource = dt;
            this.ddlSupliAddiImp.DataBind();
            //this.ddlResourcesCost_SelectedIndexChanged(null, null);

        }
        public void Get_Fgsize()
        {

            DataSet ds = (DataSet)Session["tblpackingdetails"];

            DataRow dr1 = ds.Tables[1].NewRow();
            dr1["SizeID"] = "000000000000";
            dr1["SizeDesc"] = "----None---";
            ds.Tables[1].Rows.Add(dr1);
            DataView dv1 = ds.Tables[1].DefaultView;

            this.ddlfgsize.DataTextField = "SizeDesc";
            this.ddlfgsize.DataValueField = "SizeID";
            this.ddlfgsize.DataSource = dv1;
            this.ddlfgsize.SelectedValue = "000000000000";
            this.ddlfgsize.DataBind();

        }
        public void Get_Suppiler_Loc()
        {

            DataTable dt = (DataTable)Session["tblSupplier"];
            this.ddlSupliAddiLoc.DataTextField = "ssirdesc";
            this.ddlSupliAddiLoc.DataValueField = "ssircode";
            this.ddlSupliAddiLoc.DataSource = dt;
            this.ddlSupliAddiLoc.DataBind();

        }


        //private void GET_Region()
        //{
        //    string comcod = this.GetComeCode();
        //    DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_REGION_INFORMATION", "", "", "", "", "", "", ""); ;
        //    ViewState["tblorigin"] = ds1.Tables[0];

        //}


        //private void GET_Nom_Recomended_info()
        //{
        //    string comcod = this.GetComeCode();
        //    DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_NOM_RECM_INF", "", "", "", "", "", "", ""); ;
        //    ViewState["tblnominated"] = ds1.Tables[0];
        //}


        private void Data_Bind()
        {
            var list = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tblimport"];
            var addimportmat = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdimport"];
            var addlocalmat = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdlocal"];
            var commoncost = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tblcommoncost"];
            if (this.GetComeCode() != "5301")
            {
                gvMat.Columns[9].Visible = false;
                gvmatlocal.Columns[8].Visible = false;
                gvaddimport.Columns[9].Visible = false;
                gvaddimport.Columns[8].Visible = false;
                


            }
            else
            {
                gvaddimport.Columns[17].Visible = false;
                gvaddimport.Columns[18].Visible = false;
            }

            if (list.Count == 0)
            {
                return;
            }
            if (this.Request.QueryString["Type"].ToString() == "Approve")
            {
                //gvMat.Columns[9].Visible = true;
                gvMat.Columns[10].Visible = true;
                gvMat.Columns[11].Visible = true;
                if (GetComeCode() == "5301")
                {
                    gvMat.Columns[11].Visible = true;
                }

                gvmatlocal.Columns[9].Visible = true;
                gvmatlocal.Columns[10].Visible = true;

            }
            var localmat = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbllocal"];

            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    gvMat.DataSource = list.Concat(addimportmat).ToList();
                    gvMat.DataBind();

                    break;

                case "1":
                    // var localbom=localmat.Concat(addlocalmat);
                    this.gvmatlocal.DataSource = HiddenSameData(localmat).Concat(addlocalmat).ToList();
                    this.gvmatlocal.DataBind();
                    break;
                case "2":
                    this.Get_Suppiler();
                    this.gvaddimport.DataSource = addimportmat;
                    this.gvaddimport.DataBind();
                    break;
                case "3":
                    this.Get_Suppiler_Loc();
                    this.gvaddlocal.DataSource = addlocalmat;
                    this.gvaddlocal.DataBind();
                    break;
                case "4":
                    this.gvCommonCost.DataSource = commoncost;
                    this.gvCommonCost.DataBind();
                    break;

            }



            //((Label)this.gvMat.FooterRow.FindControl("LblFqty")).Text= (list.Select(p => p.itmqty).Sum() == 0.00) ? "0" : list.Select(p => p.itmqty).Sum().ToString("#,##0;(#,##0); ");
            //((Label)this.gvMat.FooterRow.FindControl("LblFamt")).Text = (list.Select(p => p.itmamt).Sum() == 0.00) ? "0" : list.Select(p => p.itmamt).Sum().ToString("#,##0;(#,##0); ");





        }
        protected void gvMat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSupplier"];
            DataTable dtregion = (DataTable)Session["tblorigin"];
            DataTable dtnominated = (DataTable)Session["tblnominated"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                DropDownList ddlorigin = (DropDownList)e.Row.FindControl("ddlOrigin");
                ddlorigin.DataTextField = "gdesc";
                ddlorigin.DataValueField = "gcod";
                ddlorigin.DataSource = dtregion;
                ddlorigin.DataBind();
                ddlorigin.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "origin"));

                DropDownList ddlnominated = (DropDownList)e.Row.FindControl("ddlnominated");
                ddlnominated.DataTextField = "gdesc";
                ddlnominated.DataValueField = "gcod";
                ddlnominated.DataSource = dtnominated;
                ddlnominated.DataBind();
                ddlnominated.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nrcod"));
                DropDownList ddlpurtype = (DropDownList)e.Row.FindControl("ddlpurtype");
                ddlpurtype.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype"));
                if ((DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString() != "IMPORT")
                {
                    //((DropDownList)e.Row.FindControl("ddlsupplier")).Enabled = false;
                    ((DropDownList)e.Row.FindControl("ddlOrigin")).Enabled = false;
                    ((DropDownList)e.Row.FindControl("ddlnominated")).Enabled = false;
                    ((DropDownList)e.Row.FindControl("ddlpurtype")).Enabled = false;
                    ((LinkButton)e.Row.FindControl("lbtnupload")).Visible = false;
                    ((HyperLink)e.Row.FindControl("LInkbtsizeup")).Visible = false;
                }
                if (((HyperLink)e.Row.FindControl("lbtnsizedet")).NavigateUrl.ToString() != "")
                {
                    e.Row.BackColor = System.Drawing.Color.LawnGreen;
                }
            }

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();

        }
        private void Save_Value()
        {
            try
            {
                string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    var list = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tblimport"];
                    for (int i = 0; i < list.Count; i++)
                    {
                        //string ssircode = ((DropDownList)gvMat.Rows[i].FindControl("ddlsupplier")).SelectedValue.ToString();
                        string suparticle = ((TextBox)gvMat.Rows[i].FindControl("txtsuparticle")).Text.Trim();
                        string origin = ((DropDownList)gvMat.Rows[i].FindControl("ddlOrigin")).SelectedValue.ToString();
                        string nrcod = ((DropDownList)gvMat.Rows[i].FindControl("ddlnominated")).SelectedValue.ToString();
                        string remarks = ((TextBox)gvMat.Rows[i].FindControl("txtgvRemrks")).Text.Trim();
                        string composition = ((TextBox)gvMat.Rows[i].FindControl("txtgvComposition")).Text.Trim();
                        string purtype = ((DropDownList)gvMat.Rows[i].FindControl("ddlpurtype")).SelectedValue.Trim();
                        double mItmQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvMat.Rows[i].FindControl("txtgvItmQty01")).Text.Trim()));
                        double mItmQtymin = Convert.ToDouble(list[i].rstdqty - (list[i].rstdqty * 2 / 100));
                        double mItmQtypls = Convert.ToDouble(list[i].rstdqty + (list[i].rstdqty * 2 / 100));
                        double mainqty = Convert.ToDouble(list[i].rstdqty);
                            if ((mItmQty < mItmQtymin || mItmQty > mItmQtypls) && mainqty > 99 && GetComeCode() != "5301")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Order Qty Mismatch,Please Reload The Page');", true);
                                return;
                            }
                            else
                            {
                                list[i].suparticle = suparticle;
                                list[i].origin = origin;
                                list[i].nrcod = nrcod;
                                list[i].remarks = remarks;
                                list[i].cmposition = composition;
                                list[i].purtype = purtype;
                                list[i].rstdqty = mItmQty;

                            }
                    }


                    Session["tblimport"] = list;
                    break;

                case "1":
                    var loclist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbllocal"];
                    for (int i = 0; i < loclist.Count; i++)
                    {
                        //string ssircode = ((DropDownList)gvmatlocal.Rows[i].FindControl("ddlsupplierList")).SelectedValue.ToString();
                        string origin = ((DropDownList)gvmatlocal.Rows[i].FindControl("ddlOriginName")).SelectedValue.ToString();
                        string nrcod = ((DropDownList)gvmatlocal.Rows[i].FindControl("ddlnominated1")).SelectedValue.ToString();
                        string remarks = ((TextBox)gvmatlocal.Rows[i].FindControl("txtgvRemrk")).Text.Trim();

                        double addiCons = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddlocal.Rows[i].FindControl("txtgvaddiCons")).Text.Trim()));
                        double addiwestpc = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddlocal.Rows[i].FindControl("txtgvaddiwestpc")).Text.Trim()));
                        string purtype = ((DropDownList)gvmatlocal.Rows[i].FindControl("ddlpurtypelocal")).SelectedValue.Trim().ToString();
                        double mItmQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvmatlocal.Rows[i].FindControl("txtgvrstdqty")).Text.Trim()));
                        //double mItmRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMat.Rows[i].FindControl("txtgvItmRat01")).Text.Trim()));

                        //loclist[i].ssircode = ssircode;
                        loclist[i].origin = origin;
                        loclist[i].nrcod = nrcod;
                        loclist[i].addicons = addiCons;
                        loclist[i].addiwestpc = addiwestpc;
                        loclist[i].remarks = remarks;
                        loclist[i].rstdqty = mItmQty;
                        loclist[i].purtype = purtype;

                    }
                    Session["tbllocal"] = loclist;
                    break;
                case "2":
                    // --------------------------------------save for additional import----------------------------------------
                    var tbladdimport = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdimport"];
                    for (int i = 0; i < gvaddimport.Rows.Count; i++)
                    {
                        //string ssircode = ((DropDownList)gvaddimport.Rows[i].FindControl("ddlimportsupplier")).SelectedValue.ToString();
                        string suparticle = ((TextBox)gvaddimport.Rows[i].FindControl("Addtxtsuparticle")).Text.Trim();
                        string origin = ((DropDownList)gvaddimport.Rows[i].FindControl("ddlimportOrigin")).SelectedValue.ToString();
                        string nrcod = ((DropDownList)gvaddimport.Rows[i].FindControl("ddlimpnominated")).SelectedValue.ToString();
                        string composition = ((TextBox)gvaddimport.Rows[i].FindControl("txtgvCompositionadd")).Text.Trim();
                        double addiCons = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddimport.Rows[i].FindControl("txtgvaddiCons")).Text.Trim()));
                        double addiwestpc = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddimport.Rows[i].FindControl("txtgvaddiwestpc")).Text.Trim()));
                        string remarks = ((TextBox)gvaddimport.Rows[i].FindControl("txtgvRemrksAddIm")).Text.Trim();
                        string purtype = ((DropDownList)gvaddimport.Rows[i].FindControl("ddlpurtypeaddimport")).SelectedValue.Trim();

                        double mItmQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddimport.Rows[i].FindControl("txtgvtorderadd")).Text.Trim()));
                        double mItmRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvaddimport.Rows[i].FindControl("txtgvimpRate")).Text.Trim()));

                        //tbladdimport[i].ssirdesc = ssircode;
                        tbladdimport[i].suparticle = suparticle;
                        tbladdimport[i].origin = origin;
                        tbladdimport[i].nrcod = nrcod;
                        tbladdimport[i].remarks = remarks;
                        tbladdimport[i].rstdqty = mItmQty;
                        tbladdimport[i].rate = mItmRat;
                        tbladdimport[i].stdamt = mItmQty * mItmRat;
                        tbladdimport[i].cmposition = composition;
                        tbladdimport[i].addicons = addiCons;
                        tbladdimport[i].addiwestpc = addiwestpc;
                        tbladdimport[i].purtype = purtype;
                    }
                    Session["tbladdimport"] = tbladdimport;
                    break;
                case "3":
                    var tbladdlocal = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdlocal"];
                    for (int i = 0; i < gvaddlocal.Rows.Count; i++)
                    {
                        //string ssircode = ((DropDownList)gvaddlocal.Rows[i].FindControl("ddlsupplierList")).SelectedValue.ToString();
                        string origin = ((DropDownList)gvaddlocal.Rows[i].FindControl("ddlOriginNameADD")).SelectedValue.ToString();
                        string nrcod = ((DropDownList)gvaddlocal.Rows[i].FindControl("ddlnominated1loc")).SelectedValue.ToString();
                        string remarks = ((TextBox)gvaddlocal.Rows[i].FindControl("txtgvRemrkloc")).Text.Trim();
                        string purtype = ((DropDownList)gvaddlocal.Rows[i].FindControl("ddlpurtypeaddlocal")).SelectedValue.Trim().ToString();
                        double addiCons = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddlocal.Rows[i].FindControl("txtgvlocaddiCons")).Text.Trim()));
                        double addiwestpc = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddlocal.Rows[i].FindControl("txtgvlocaddiwestpc")).Text.Trim()));
                        double mItmQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvaddlocal.Rows[i].FindControl("txtgvrstdqtyloc")).Text.Trim()));
                        double mItmRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvaddlocal.Rows[i].FindControl("txtgvRateloc")).Text.Trim()));

                        //tbladdlocal[i].ssircode = ssircode;
                        tbladdlocal[i].origin = origin;
                        tbladdlocal[i].nrcod = nrcod;
                        tbladdlocal[i].remarks = remarks;
                        tbladdlocal[i].rstdqty = mItmQty;
                        tbladdlocal[i].rate = mItmRat;
                        tbladdlocal[i].addicons = addiCons;
                        tbladdlocal[i].addiwestpc = addiwestpc;
                        tbladdlocal[i].stdamt = mItmQty * mItmRat;
                        tbladdlocal[i].purtype = purtype;

                    }
                    Session["tbladdlocal"] = tbladdlocal;
                    break;
                case "4":

                    break;

            }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }

        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save_Value();
                string comcod = GetComeCode();
                string mLCCode = this.ddlmlccode.SelectedValue.ToString();
                string styleid = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
                string color = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
                string dayid = this.Request.QueryString["genno"].ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string estprddat = Convert.ToDateTime(this.Txtprddate.Text).ToString("dd-MMM-yyyy");
                string inspctdat = Convert.ToDateTime(this.txtInspctDate.Text).ToString("dd-MMM-yyyy");
                string confirmdate = Convert.ToDateTime(this.TxtConfirmDate.Text).ToString("dd-MMM-yyyy");

                var list = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tblimport"];
                var loclist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbllocal"];
                var tbladdimport = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdimport"];
                var tbladdlocal = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdlocal"];

                string notes = this.TxtNotes.Text.ToString();
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(ASITUtility03.ListToDataTable(list));
                ds.Tables.Add(ASITUtility03.ListToDataTable(loclist));
                ds.Tables.Add(ASITUtility03.ListToDataTable(tbladdimport));
                ds.Tables.Add(ASITUtility03.ListToDataTable(tbladdlocal));
                ds.Tables[0].TableName = "tblimport";
                ds.Tables[1].TableName = "tbllocal";
                ds.Tables[2].TableName = "tbladdimport";
                ds.Tables[3].TableName = "tbladdlocal";
                //var xx = ds.GetXml();
                DataTable dt = ASITUtility03.ListToDataTable(list);
                if (mLCCode != list[0].mlccode.ToString() && list[0].mlccode.ToString().Trim() != "" )
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Something Went Wrong! Please reload your Page');", true);
                    return;
                }
                var listNAddi = list.Concat(tbladdimport);
                if (listNAddi.Where(c=> c.mlccode == null).ToList().Count > 0 || listNAddi.Where(c => c.mlccode.Length==0).ToList().Count > 0 ||  dayid != list[0].dayid.ToString().Trim())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Something Went Wrong! Please reload your Page');", true);
                    return;
                }
                if (listNAddi.Where(c => c.mlccode != mLCCode).ToList().Count > 0 || dayid != list[0].dayid.ToString().Trim())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Something Went Wrong! Please reload your Page');", true);
                    return;
                }
                //var xml = ds.GetXml();
                bool result = proc1.UpdateXmlTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "UPDATE_MAT_COST", ds, null, null, mLCCode, userid, Terminal, Sessionid, Posteddat, styleid, color, dayid, estprddat, notes, inspctdat, confirmdate);

                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                }

                if (true == true && ConstantInfo.LogStatus == true)
                {

                    string eventtype = "BOM Update/Edit";
                    string eventdesc = "Update BOM from BOM Details, Details Code" + mLCCode+ styleid+ color+ dayid;
                    string eventdesc2 = "Order No- " + mLCCode;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        
                }
                GetBudgetInfo();
                Data_Bind();
                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "Approve")
            {
                this.PrintBomApprove();
            }
            else
            {
                PrintBOM();
            }

        }
        private void PrintBomApprove()
        {

            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string MasterLc = this.ddlmlccode.SelectedItem.Text.Trim().Substring(13);
            string styleid = this.ddlArticle.SelectedValue.ToString(); //== "000000000000") ? "03%" : this.ddlArticle.SelectedValue.ToString() + "%";
            string colorid = this.ddlcolor.SelectedValue.ToString();  //== "000000000000") ? "71%" : this.ddlcolor.SelectedValue.ToString() + "%";

            //Session["tblbomtotal"] = ds1.Tables[1];
            //Session["tblbomtopshet"] = ds1.Tables[0];
            //Session["tbldircost"] = ds1.Tables[2];

            DataTable dt1 = (DataTable)Session["tblbomtopshet"];
            DataTable dt2 = (DataTable)Session["tblbomtotal"];
            DataTable dt3 = (DataTable)Session["tbldircost"];

            var lst = dt1.DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.BomApproval01>();
            var lst1 = dt2.DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.BomApproval02>();
            var lst2 = dt3.DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.BomApproval03>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptBomApproved", lst, lst1, lst2);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("styleid", styleid));
            rpt1.SetParameters(new ReportParameter("MasterLc", "Master L/C: " + MasterLc));
            rpt1.SetParameters(new ReportParameter("colorid", colorid));
            rpt1.SetParameters(new ReportParameter("Rpttitle", "MATERIALS REQUIREMENT AGAINST ORDER"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        public void PrintBOM()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string mMLCCOD = this.ddlmlccode.SelectedValue.ToString();
            string styleid1 = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            string dayid = this.Request.QueryString["genno"].ToString();
            string rtype = dayid != "00000000" ? "Reorder" : "";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", (comcod == "5301") ? "GET_BOM_MATERIALINFO_FOR_PRINT" : "GET_BOM_MATERIALINFO_FOR_PRINT_FB", mMLCCOD, dayid, styleid1, colorid, "", "", "", "");


            List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder> lst = ds1.Tables[3].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            var loclist = ds1.Tables[1].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>();
            var addimport = lst.FindAll(p => p.reqtype == "ADDIMPORT");
            var addlocal = lst.FindAll(p => p.reqtype == "ADDLOCAL");


            LocalReport rpt1 = new LocalReport();
            string OrdDate = this.txtOrdDate.Text.ToString();
            string type = "";
            string heading = "";
            string buyername = "";
            string frwdbyuser = "";
            string approvedby = "";
            string styledesc = "";
            string colordesc = "";
            string artno = "";
            string ordrqty = "";
            string catedesc = "";
            string orderno = "";
            string orddate = "";
            string aprvdat = "";
            string frwddat = "";
            string estprodate = "";
            string lforma = "";
            string notes = "";
            string samsize = "";
            string sizernge = "";
            string brand = "";
            string bomid = "";
            string season = "";
            string inspdate = "";
            string exdate = "";
            if (ds1.Tables[2].Rows.Count != 0)
            {
                type = this.TabName.Value.ToString();
                heading = "";
                buyername = ds1.Tables[2].Rows[0]["buyername"].ToString();
                frwdbyuser = ds1.Tables[2].Rows[0]["frwdbyuser"].ToString();
                approvedby = ds1.Tables[2].Rows[0]["approvedby"].ToString();
                styledesc = ds1.Tables[2].Rows[0]["styledesc"].ToString();
                colordesc = ds1.Tables[2].Rows[0]["colordesc"].ToString();
                artno = ds1.Tables[2].Rows[0]["artno"].ToString();
                ordrqty = Convert.ToDouble(ds1.Tables[2].Rows[0]["ordrqty"]).ToString("#,##0;(#,##0); ");
                catedesc = ds1.Tables[2].Rows[0]["catedesc"].ToString();
                orderno = ds1.Tables[2].Rows[0]["orderno"].ToString();
                orddate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["orddate"]).ToString("dd-MMM-yyyy");
                estprodate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["estprddat"]).ToString("dd-MMM-yyyy");
                aprvdat = ds1.Tables[2].Rows[0]["aprvdat"].ToString();
                lforma = ds1.Tables[2].Rows[0]["lforma"].ToString();
                lforma = ds1.Tables[2].Rows[0]["lforma"].ToString();
                samsize = ds1.Tables[2].Rows[0]["samsize"].ToString();
                sizernge = ds1.Tables[2].Rows[0]["sizernge"].ToString();
                brand = ds1.Tables[2].Rows[0]["brand"].ToString();
                bomid = ds1.Tables[2].Rows[0]["bomid"].ToString();
                season = ds1.Tables[2].Rows[0]["season"].ToString();
                inspdate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["inspdate"]).ToString("dd-MMM-yyyy"); 
                exdate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["shipmntdat"]).ToString("dd-MMM-yyyy"); 

                notes = ds1.Tables[2].Rows[0]["notes"].ToString();
                type = this.RadioButtonList1.SelectedValue.ToString();
            }

            if (type == "0")
            {
                //list = list.Concat(addimport);
                heading = "BILL OF MATERIALS";
                switch (comcod)
                {
                    case "5305":
                    case "5306":
                        string mlccod = this.Request.QueryString["actcode"].ToString();
                        string styleid = this.Request.QueryString["sircode"].ToString().Substring(0, 12);

                        string date = System.DateTime.Now.ToString("dd-MMM-yyyy"); ;
                        DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, rtype, orddate, styleid, "", colorid, "", "");
                        DataSet ds3 = (DataSet)Session["tblpackingdetails"];
                        int count = ds3.Tables[1].Rows.Count - 2;
                        sizernge = ds3.Tables[1].Rows[0]["sizedesc"].ToString() + "-" + ds3.Tables[1].Rows[count]["sizedesc"].ToString();

                        List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = ds2.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
                        List <SPEENTITY.C_01_Mer.GetOrderWithCat > lst3 = ds3.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
                        List <SPEENTITY.C_01_Mer.EclassConsumptionSamSize> lst4 = ds1.Tables[5].DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();


                        lst2.AddRange(lst3);

                        rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptMatReqImportFB", list.ToList(), lst4, lst2);
                        rpt1.EnableExternalImages = true;
                        for (int i = 0; i < ds2.Tables[1].Rows.Count; i++)
                        {
                            rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), ds2.Tables[1].Rows[i]["SizeDesc"].ToString()));

                        }
                        string image = new Uri(Server.MapPath(list[0].imgurl.ToString())).AbsoluteUri;
                        rpt1.SetParameters(new ReportParameter("ImgUrl", image));
                        rpt1.SetParameters(new ReportParameter("lforma", lforma));
                        rpt1.SetParameters(new ReportParameter("notes", notes));
                        rpt1.SetParameters(new ReportParameter("samsize", samsize));
                        rpt1.SetParameters(new ReportParameter("sizernge", sizernge));
                        rpt1.SetParameters(new ReportParameter("brand", brand));
                        rpt1.SetParameters(new ReportParameter("bomid", bomid));
                        rpt1.SetParameters(new ReportParameter("season", season));
                        rpt1.SetParameters(new ReportParameter("inspdate", inspdate));
                        rpt1.SetParameters(new ReportParameter("exdate", exdate));
                        rpt1.SetParameters(new ReportParameter("aprvdat", aprvdat));
                        break;
                    default:

                        rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptMatReqImport", list.Concat(addimport).ToList(), null, null);
                        break;
                }
            }
            else
            {
                heading = "MATERIAL REQUREMENT AGAINST ORDER- LOCAL PURCHASE";
                rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptMatReqLocal", loclist.Concat(addlocal).ToList(), null, null);
            }



            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("RptCustname", buyername));
            rpt1.SetParameters(new ReportParameter("RptOrdName", orderno));
            rpt1.SetParameters(new ReportParameter("OrdQty", ordrqty));
            rpt1.SetParameters(new ReportParameter("ProDate", estprodate));
            rpt1.SetParameters(new ReportParameter("style", styledesc));
            rpt1.SetParameters(new ReportParameter("color", colordesc));
            rpt1.SetParameters(new ReportParameter("article", artno));
            rpt1.SetParameters(new ReportParameter("category", catedesc));
            rpt1.SetParameters(new ReportParameter("RptTitle", heading)); // 
            rpt1.SetParameters(new ReportParameter("OrdDate", orddate));
            rpt1.SetParameters(new ReportParameter("approvedby", approvedby + "\n" + aprvdat));
            rpt1.SetParameters(new ReportParameter("frwdbyuser", frwdbyuser + "\n" + frwddat)); // 
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("RptCustname", buyername));
            //rpt1.SetParameters(new ReportParameter("RptOrdName", ""));
            //rpt1.SetParameters(new ReportParameter("OrdQty", ""));
            //rpt1.SetParameters(new ReportParameter("ProDate", estprodate));
            //rpt1.SetParameters(new ReportParameter("style", styledesc));
            //rpt1.SetParameters(new ReportParameter("color", colordesc));
            //rpt1.SetParameters(new ReportParameter("article", artno));
            //rpt1.SetParameters(new ReportParameter("category", catedesc));
            //rpt1.SetParameters(new ReportParameter("RptTitle", heading)); // 
            //rpt1.SetParameters(new ReportParameter("OrdDate", OrdDate));
            //rpt1.SetParameters(new ReportParameter("approvedby", approvedby + "\n" + aprvdat));
            //rpt1.SetParameters(new ReportParameter("frwdbyuser", frwdbyuser + "\n" + frwddat)); // 

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvmatlocal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSupplier"];
            DataTable dtregion = (DataTable)Session["tblorigin"];
            DataTable dtnominated = (DataTable)Session["tblnominated"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList ddlcate = (DropDownList)e.Row.FindControl("ddlsupplierList");
                //ddlcate.DataTextField = "ssirdesc";
                //ddlcate.DataValueField = "ssircode";
                //ddlcate.DataSource = dt;
                //ddlcate.DataBind();
                //ddlcate.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode"));

                DropDownList ddlorigin = (DropDownList)e.Row.FindControl("ddlOriginName");
                ddlorigin.DataTextField = "gdesc";
                ddlorigin.DataValueField = "gcod";
                ddlorigin.DataSource = dtregion;
                ddlorigin.DataBind();
                ddlorigin.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "origin"));

                DropDownList ddlnominated = (DropDownList)e.Row.FindControl("ddlnominated1");
                ddlnominated.DataTextField = "gdesc";
                ddlnominated.DataValueField = "gcod";
                ddlnominated.DataSource = dtnominated;
                ddlnominated.DataBind();
                ddlnominated.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nrcod"));
                DropDownList ddlpurtype = (DropDownList)e.Row.FindControl("ddlpurtypelocal");
                ddlpurtype.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype"));
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype")) == "")
                {
                    ddlpurtype.SelectedValue = "L";
                }

                if ((DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString() != "LOCAL")
                {
                    //((DropDownList)e.Row.FindControl("ddlsupplierList")).Enabled = false;
                    ((DropDownList)e.Row.FindControl("ddlOriginName")).Enabled = false;
                    ((DropDownList)e.Row.FindControl("ddlnominated1")).Enabled = false;
                    ((DropDownList)e.Row.FindControl("ddlpurtypelocal")).Enabled = false;
                    ((LinkButton)e.Row.FindControl("lblgvSizeupload")).Visible = false;
                    ((HyperLink)e.Row.FindControl("LInkbtsize")).Visible = false;
                }

                if (((HyperLink)e.Row.FindControl("lbtnsizedetLoc")).NavigateUrl.ToString() != "")
                {
                    e.Row.BackColor = System.Drawing.Color.LawnGreen;
                }
            }
        }

        private List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder> HiddenSameData(List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder> lst)
        {

            string procode = "";
            var list22 = lst.OrderBy(m => m.procode).ThenBy(m => m.rsircode).ThenBy(m => m.spcfcode).ToList();
            foreach (SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder c1 in list22)
            {
                if (procode == c1.procode.ToString())
                {
                    c1.procname = "";
                }
                procode = c1.procode.ToString();

            }
            Session["tbllocal"] = list22;
            return list22;

        }

        protected void lnkbtnLedger_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string dayid = this.Request.QueryString["genno"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string actcode = this.Request.QueryString["actcode"].ToString();
            string styleid = (this.ddlArticle.SelectedValue.ToString() == "000000000000") ? "03%" : this.ddlArticle.SelectedValue.ToString().Substring(0, 12) + "%";
            string colorid = (this.ddlcolor.SelectedValue.ToString() == "000000000000") ? "71%" : this.ddlcolor.SelectedValue.ToString().Substring(0, 12) + "%";
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "APPROVED_BOM", actcode, userid, AppDat, trmid, sessionid, styleid, colorid, dayid);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('BOM Not Approved');", true);


                return;

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('BOM Approved');", true);




            //Common.LogStatus("Diagnosis Complite", "QC Qualified", "Recived No: ", centrid + " - " + wrRecvno);
        }

        protected void gvaddimport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSupplier"];
            DataTable dtregion = (DataTable)Session["tblorigin"];
            DataTable dtnominated = (DataTable)Session["tblnominated"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList ddlcate = (DropDownList)e.Row.FindControl("ddlimportsupplier");
                //ddlcate.DataTextField = "ssirdesc";
                //ddlcate.DataValueField = "ssircode";
                //ddlcate.DataSource = dt;
                //ddlcate.DataBind();
                //ddlcate.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode"));

                DropDownList ddlorigin = (DropDownList)e.Row.FindControl("ddlimportOrigin");
                ddlorigin.DataTextField = "gdesc";
                ddlorigin.DataValueField = "gcod";
                ddlorigin.DataSource = dtregion;
                ddlorigin.DataBind();
                ddlorigin.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "origin"));

                DropDownList ddlnominated = (DropDownList)e.Row.FindControl("ddlimpnominated");
                ddlnominated.DataTextField = "gdesc";
                ddlnominated.DataValueField = "gcod";
                ddlnominated.DataSource = dtnominated;
                ddlnominated.DataBind();
                ddlnominated.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nrcod"));
                DropDownList ddlpurtype = (DropDownList)e.Row.FindControl("ddlpurtypeaddimport");
                ddlpurtype.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype"));
                if (((HyperLink)e.Row.FindControl("LInkbtnimport")).NavigateUrl.ToString() != "")
                {
                    e.Row.BackColor = System.Drawing.Color.LawnGreen;
                }
            }
        }
        private void GetComponentList()
        {
            string comcod = this.GetComeCode();

            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            ddlComponent.DataTextField = "resdesc";
            ddlComponent.DataValueField = "rescode";
            ddlComponent.DataSource = ds2.Tables[0];
            ddlComponent.DataBind();

        }
        private void Get_material()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ONLY_MATERIAL_LIST ", "04%", "", "", "", "", "", "", "", "");

            this.ddlResourcesCost.DataSource = ds3.Tables[0];
            this.ddlResourcesCost.DataTextField = "sirdesc";
            this.ddlResourcesCost.DataValueField = "sircode";
            this.ddlResourcesCost.DataBind();

            this.ddlResources.DataSource = ds3.Tables[0];
            this.ddlResources.DataTextField = "sirdesc";
            this.ddlResources.DataValueField = "sircode";
            this.ddlResources.DataBind();
            this.ddlResourcesCost_SelectedIndexChanged(null, null);
        }
        protected void ddlResourcesCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mResCode = this.ddlResourcesCost.SelectedValue.ToString() + "%";
            this.DdlSpecres.Items.Clear();
            string comcod = this.GetComeCode();
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION ", mResCode, "", "", "", "", "", "", "", "");

            DataView dv1 = ds3.Tables[0].DefaultView;

            DataTable dt = dv1.ToTable(true, "spcfcod", "spcfdesc");
            dt.Rows.Add("000000000000", "None");
            //if (dt.Rows.Count > 1)
            //{
            //    dt.Rows[0].Delete();
            //}


            Session["tblresRes"] = ds3.Tables[0];

            this.DdlSpecres.DataTextField = "spcfdesc";
            this.DdlSpecres.DataValueField = "spcfcod";
            this.DdlSpecres.DataSource = dt;
            this.DdlSpecres.DataBind();
            this.DdlSpecres.SelectedValue = "000000000000";
            this.DdlSpecres_SelectedIndexChanged(null, null);
        }
        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {

            var addimport = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdimport"];
            string rescod = this.ddlResourcesCost.SelectedValue.ToString();
            string SupliAddiCode = this.ddlSupliAddiImp.SelectedValue.ToString();
            string SupliAddiDesc = this.ddlSupliAddiImp.SelectedItem.ToString();
            string Specification = this.DdlSpecres.SelectedValue.ToString();
            string fgsize = this.ddlfgsize.SelectedValue.ToString();
            string compcode = this.ddlComponent.SelectedValue.ToString();
            string compname = this.ddlComponent.SelectedItem.ToString();


            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            //foreach (ListItem item in ddlComponent.Items)
            //{
            //    if (item.Selected)
            //    {

            //compcode = item.Value;
            //compname = item.Text;

            var checklist = addimport.FindAll(p => p.compcode == compcode && p.rsircode == rescod && p.spcfcode == Specification && p.fgsize == fgsize);
            // DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and  compcode='" + compcode + "'" + "' and spcfcode='" + Specification + "'");
            if (checklist.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:Selected Component Already Added');", true);

                return;
            }
            else
            {
                DataTable dt = (DataTable)Session["tblresRes"];
                string rsirdesc = (((DataTable)Session["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirdesc2"].ToString();
                string runit = (((DataTable)Session["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirunit"].ToString();
                string color = (((DataTable)Session["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["desc2"].ToString();

                string spcfdesc = (((DataTable)Session["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["spcfdesc"].ToString();
                addimport.Add(new SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder("", "", "", rescod, rsirdesc, Specification, spcfdesc, compcode,
                   compname, color, "", 0.00, 0.00, runit, 0.00, SupliAddiCode, SupliAddiDesc, "", "00000", "", "", "", "ADDIMPORT", "00000", "", "", fgsize, mlccod,""));
            }

            Session["tbladdimport"] = addimport;
            this.Data_Bind();
        }


        protected void gvaddlocal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSupplier"];
            DataTable dtregion = (DataTable)Session["tblorigin"];
            DataTable dtnominated = (DataTable)Session["tblnominated"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlcate = (DropDownList)e.Row.FindControl("ddlsupplierListloc");
                ddlcate.DataTextField = "ssirdesc";
                ddlcate.DataValueField = "ssircode";
                ddlcate.DataSource = dt;
                ddlcate.DataBind();
                ddlcate.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode"));

                DropDownList ddlorigin = (DropDownList)e.Row.FindControl("ddlOriginNameADD");
                ddlorigin.DataTextField = "gdesc";
                ddlorigin.DataValueField = "gcod";
                ddlorigin.DataSource = dtregion;
                ddlorigin.DataBind();
                ddlorigin.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "origin"));

                DropDownList ddlnominated = (DropDownList)e.Row.FindControl("ddlnominated1loc");
                ddlnominated.DataTextField = "gdesc";
                ddlnominated.DataValueField = "gcod";
                ddlnominated.DataSource = dtnominated;
                ddlnominated.DataBind();
                ddlnominated.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nrcod"));
                DropDownList ddlpurtype = (DropDownList)e.Row.FindControl("ddlpurtypeaddlocal");
                ddlpurtype.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype"));
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype")) == "")
                {
                    ddlpurtype.SelectedValue = "L";
                }
                if (((HyperLink)e.Row.FindControl("LInkbtn")).NavigateUrl.ToString() != "")
                {
                    e.Row.BackColor = System.Drawing.Color.LawnGreen;
                }
            }
        }

        protected void LbtnAddRestoLocal_Click(object sender, EventArgs e)
        {
            var addlocal = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdlocal"];
            string rescod = this.ddlResources.SelectedValue.ToString().Substring(0, 12);
            string Specification = this.ddlResources.SelectedValue.ToString().Substring(12, 12); ;
            string SupliAddicode = this.ddlSupliAddiLoc.SelectedValue.ToString();
            string SupliAddidesc = this.ddlSupliAddiLoc.SelectedItem.ToString();
            string procode = this.ddldept.SelectedValue.ToString();
            string procname = this.ddldept.SelectedItem.ToString();

            string mlccod = this.ddlmlccode.SelectedValue.ToString();

            //foreach (ListItem item in ddlComponent.Items)
            //{
            //    if (item.Selected)
            //    {

            //compcode = item.Value;
            //compname = item.Text;

            var checklist = addlocal.FindAll(p => p.procode == procode && p.rsircode == rescod && p.spcfcode == Specification);
            // DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and  compcode='" + compcode + "'" + "' and spcfcode='" + Specification + "'");
            if (checklist.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:Selected Component Already Added');", true);


                return;
            }
            else
            {

                string rsirdesc = (((DataTable)Session["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirdesc2"].ToString();
                string runit = (((DataTable)Session["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirunit"].ToString();

                string spcfdesc = (((DataTable)Session["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["spcfdesc"].ToString();
                addlocal.Add(new SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder("", procode, procname, rescod, rsirdesc, Specification, spcfdesc, "",
                   "", "", "", 0.00, 0.00, runit, 0.00, SupliAddicode, SupliAddidesc, "", "00000", "", "", "", "ADDLOCAL", "00000", "", "","",mlccod,""));
            }


            Session["tbladdlocal"] = addlocal;
            this.Data_Bind();
        }
        protected void GetProcess()
        {

            string comcod = this.GetComeCode();
            string inqno = this.Request.QueryString["actcode"].ToString();
            string filter = "%";
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GETPROCESSCODE", filter, "", "", "", "", "", "", "", "");
            this.ddldept.DataSource = ds3.Tables[0];
            this.ddldept.DataTextField = "resdesc";
            this.ddldept.DataValueField = "rescode";
            this.ddldept.DataBind();
            Session["tblcodeType"] = ds3.Tables[0];
            ds3.Dispose();

        }

        protected void lblgvSizeupload_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvaddlocal.Rows[index].FindControl("lblgvrsircodeloc")).Text.ToString();
            string spcfcode = ((Label)this.gvaddlocal.Rows[index].FindControl("lblgvspcfcodlocal")).Text.ToString();
            string process = ((Label)this.gvaddlocal.Rows[index].FindControl("lblgvprocode")).Text.ToString();
            string supplier = ((DropDownList)this.gvaddlocal.Rows[index].FindControl("ddlsupplierListloc")).SelectedValue.ToString();

            this.lblMaterial.Text = material;
            this.lblSpcfcod.Text = spcfcode;
            this.lblprocesscod.Text = process;
            this.lblsupplier.Text = supplier;
            this.lblreqtype.Text = "ADDLOCAL";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            string comcod = this.GetComeCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string Url = "";
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string material = this.lblMaterial.Text.Trim().ToString();
            string spcfcod = this.lblSpcfcod.Text.Trim().ToString();
            string procod = this.lblprocesscod.Text.Trim().ToString();
            string supplier = this.lblsupplier.Text.Trim().ToString();
            string reqtype = this.lblreqtype.Text.Trim().ToString();
            string dayid = this.Request.QueryString["genno"].ToString();
            if (AsyncFileUpload1.HasFile)
            {
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/BOMSIZE/") + random + extension);

                Url = "~/Upload/BOMSIZE/" + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                //dt.Rows.Add(comcod, Url);

            }
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "UPDATE_SIZE_ALLOCATION_FROM_BOM", mlccod, material, spcfcod, procod, supplier, reqtype, Url, dayid);
            if (result)
            {
                this.GetBudgetInfo();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


            }



        }

        protected void lblgvSizeuploadimport_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvaddimport.Rows[index].FindControl("addlblgvItmNo1")).Text.ToString();
            string spcfcode = ((Label)this.gvaddimport.Rows[index].FindControl("lblgvspcfcodIMport")).Text.ToString();
            string process = ((Label)this.gvaddimport.Rows[index].FindControl("lblgvprocodeimport")).Text.ToString();
            string supplier = ((DropDownList)this.gvaddimport.Rows[index].FindControl("ddlimportsupplier")).SelectedValue.ToString();

            this.lblMaterial.Text = material;
            this.lblSpcfcod.Text = spcfcode;
            this.lblprocesscod.Text = process;
            this.lblsupplier.Text = supplier;
            this.lblreqtype.Text = "ADDIMPORT";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }

        protected void lblgvSizeupload_Click1(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvmatlocal.Rows[index].FindControl("lblgvrsircode")).Text.ToString();
            string spcfcode = ((Label)this.gvmatlocal.Rows[index].FindControl("lblspcfcod")).Text.ToString();
            string process = ((Label)this.gvmatlocal.Rows[index].FindControl("lblprocode")).Text.ToString();
            string supplier = ((DropDownList)this.gvmatlocal.Rows[index].FindControl("ddlsupplierList")).SelectedValue.ToString();

            this.lblMaterial.Text = material;
            this.lblSpcfcod.Text = spcfcode;
            this.lblprocesscod.Text = process;
            this.lblsupplier.Text = supplier;
            this.lblreqtype.Text = "LOCAL";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvMat.Rows[index].FindControl("lblgvItmNo1")).Text.ToString();
            string spcfcode = ((Label)this.gvMat.Rows[index].FindControl("lblgvspcfcode")).Text.ToString();
            string process = ((Label)this.gvMat.Rows[index].FindControl("lblcomponent")).Text.ToString();
            //string supplier = ((DropDownList)this.gvMat.Rows[index].FindControl("ddlsupplier")).SelectedValue.ToString();

            this.lblMaterial.Text = material;
            this.lblSpcfcod.Text = spcfcode;
            this.lblprocesscod.Text = process;
            //this.lblsupplier.Text = supplier;
            this.lblreqtype.Text = "IMPORT";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccod = this.Request.QueryString["actcode"].ToString();
            string dayid = this.Request.QueryString["genno"].ToString();
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_ORDER_WISE_STYLE_INFO", mlccod, dayid, "", "", "", "", "", "", "");
            Session["tblstylecolor"] = ds3.Tables[0];
            DataView dv = ds3.Tables[0].DefaultView;
            dv.RowFilter = "refcod='000000000000' AND dayid='" + dayid + "'";
            DataTable dt = dv.ToTable(true, "gencod", "gendata");
            if (this.Request.QueryString["Type"].ToString() == "Approve")
            {
                dt.Rows.Add(new Object[] { "00000000000000000000", "All Style" });
            }

            this.ddlArticle.DataSource = dt;
            this.ddlArticle.DataTextField = "gendata";
            this.ddlArticle.DataValueField = "gencod";
            this.ddlArticle.DataBind();
            //if (this.Request.QueryString["Type"].ToString() == "Approve")
            //{
            //    this.ddlArticle.SelectedValue = "000000000000";
            //}
            if (this.Request.QueryString["sircode"].ToString().Length > 0)
            {
                this.ddlArticle.SelectedValue = this.Request.QueryString["sircode"].ToString().Substring(0, 12) + dayid;
            }

            ddlArticle_SelectedIndexChanged(null, null);
        }

        protected void ddlArticle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblstylecolor"];
            string style = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string dayid = this.Request.QueryString["genno"].ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "refcod='" + style + "' AND gencod not like '03%' AND dayid='" + dayid + "'";
            DataTable dt1 = dv.ToTable(true, "gencod", "gendata");
            if (this.Request.QueryString["Type"].ToString() == "Approve")
            {
                dt1.Rows.Add(new Object[] { "000000000000", "All Color" });
            }
            this.ddlcolor.DataSource = dt1;
            this.ddlcolor.DataTextField = "gendata";
            this.ddlcolor.DataValueField = "gencod";
            this.ddlcolor.DataBind();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "refcod='" + style + "' AND gencod not like '03%'";
            this.ddlFromcolor.DataSource = dv1.ToTable(true, "gencod", "gendata");
            this.ddlFromcolor.DataTextField = "gendata";
            this.ddlFromcolor.DataValueField = "gencod";
            this.ddlFromcolor.DataBind();

            //if (this.Request.QueryString["Type"].ToString() == "Approve")
            //{
            //    this.ddlcolor.SelectedValue = "000000000000";
            //}
            if (this.Request.QueryString["sircode"].ToString().Length > 0)
            {
                this.ddlcolor.SelectedValue = this.Request.QueryString["sircode"].ToString().Substring(12, 12) + dayid;
                this.OkBtn_Click(null, null);
            }
            //string colorid = this.ddlcolor.SelectedValue.ToString();
            //DataView dv1 = dv.ToTable(true, "gencod", "gendata").DefaultView;
            //dv1.RowFilter = "gencod<>'"+ colorid + "'";



        }

        protected void lbtncopyto_Click(object sender, EventArgs e)
        {
            if (lbtncopyto.Text == "Copy From")
            {
                lbtncopyto.Text = "Hide";
                this.ddlFromcolor.Visible = true;
                this.lbtnCopyBom.Visible = true;
                return;
            }
            else
            {
                lbtncopyto.Text = "Copy From";
                this.ddlFromcolor.Visible = false;
                this.lbtnCopyBom.Visible = false;
                return;
            }
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.Multiview1.ActiveViewIndex = 0;
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

                    this.Data_Bind();
                    break;

                case "1":
                    this.Multiview1.ActiveViewIndex = 1;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

                    this.Data_Bind();
                    break;
                case "2":
                    this.Multiview1.ActiveViewIndex = 2;
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

                    this.Data_Bind();
                    break;
                case "3":
                    this.Multiview1.ActiveViewIndex = 3;
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    this.Save_Value();
                    this.Data_Bind();
                    break;
                case "4":
                    this.Multiview1.ActiveViewIndex = 4;
                    //this.RadioButtonList1.Items[4].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

                    this.Data_Bind();
                    break;

            }
        }
        protected void lbtnCopyBom_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string styleid = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            string fromcolor = this.ddlFromcolor.SelectedValue.ToString().Substring(0, 12);
            string fromday = this.ddlFromcolor.SelectedValue.ToString().Substring(12, 8);
            string dayid = this.Request.QueryString["genno"].ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "COPY_BOM_COLOR_WISE", mlccod, styleid, colorid, fromcolor, userid, Terminal, Sessionid, Posteddat, dayid, fromday);
            if (result)
            {
                this.lbtncopyto_Click(null, null);
                this.GetBudgetInfo();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Copied Successfully');", true);


            }
        }

        protected void txtgvItmDesccost_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvMat.Rows[index].FindControl("lblgvItmNo1")).Text.ToString();
            string spcfcode = ((Label)this.gvMat.Rows[index].FindControl("lblgvspcfcode")).Text.ToString();
            string compcode = ((Label)this.gvMat.Rows[index].FindControl("lblcomponent")).Text.ToString();
            string reqtype = ((Label)this.gvMat.Rows[index].FindControl("lblgvReqType")).Text.ToString();

            this.lblHelper.Text = compcode + material + spcfcode;
            this.lblRtype.Text = reqtype;
            DataSet result = proc1.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_WISE_SPECIFICATION", material);
            DataSet colorinfo = this.proc1.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_COLOR_CODE", "", "", "", "", "", "", "");
            Session["tblcolor"] = colorinfo.Tables[0];
            if (result.Tables[0].Rows.Count == 0 || result == null)
            {
                return;
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = result.Tables[0];
            this.ddlSpecification.DataBind();

            this.ddlModalColor.DataTextField = "gdesc";
            this.ddlModalColor.DataValueField = "gcod";
            this.ddlModalColor.DataSource = colorinfo;
            this.ddlModalColor.DataBind();

            this.TxtThikness.Text = "";
            this.txtlength.Text = "";
            this.txtbrand.Text = "";
            this.txtRemarks.Text = "";
            this.ModalMultiview.ActiveViewIndex = 0;
            this.ModalHead.Text = " Change Specifications";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }

        protected void LbtnChngSpcf_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblcolor"];
            DataView dv = dt.DefaultView;
            string lccode = this.ddlmlccode.SelectedValue.ToString();
            string style = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string color = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            string compcode = this.lblHelper.Text.ToString().Substring(0, 12);
            string material = this.lblHelper.Text.ToString().Substring(12, 12);
            string spcfcode = this.lblHelper.Text.ToString().Substring(24, 12);
            string tospcfcod = this.ddlSpecification.SelectedValue.ToString();
            string reqtype = this.lblRtype.Text.ToString();
            string dayid = this.Request.QueryString["genno"].ToString();
            if (this.txtlength.Text.ToString() != "" || this.TxtThikness.Text.ToString() != "")
            {
                string thikness = this.TxtThikness.Text.ToString();
                string len = this.txtlength.Text.ToString();
                string brand = this.txtbrand.Text.ToString();
                string remarks = this.txtRemarks.Text.ToString();
                string newcolor = this.ddlModalColor.SelectedValue.ToString();
                string newcolorname = this.ddlModalColor.SelectedItem.ToString();
                dv.RowFilter = "gcod = '" + newcolor + "'";
                string colcode = dv.ToTable().Rows[0]["colcode"].ToString();

                DataSet ds = proc1.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_NEW_SPECIFICATIONS", material, thikness, len, newcolorname, brand, remarks, colcode);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Error!!!');", true);


                    return;
                }

                tospcfcod = ds.Tables[0].Rows[0]["spcfcod"].ToString();
            }
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "CHANGE_SPECIFICATION_FOR_BOM", lccode, style, color, compcode, material, spcfcode, tospcfcod, dayid, reqtype);
            if (result == true)
            {
                this.GetBudgetInfo();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Specification Change Successfully');", true);


            }
        }

        protected void txtgvItem_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvmatlocal.Rows[index].FindControl("lblgvrsircode")).Text.ToString();
            string spcfcode = ((Label)this.gvmatlocal.Rows[index].FindControl("lblspcfcod")).Text.ToString();
            string dptcode = ((Label)this.gvmatlocal.Rows[index].FindControl("lblprocode")).Text.ToString();
            string reqtype = ((Label)this.gvmatlocal.Rows[index].FindControl("lblgvreqtypeLocal")).Text.ToString();

            this.lblHelper.Text = dptcode + material + spcfcode;
            this.lblRtype.Text = reqtype;
            DataSet result = proc1.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_WISE_SPECIFICATION", material);
            if (result.Tables[0].Rows.Count == 0 || result == null)
            {
                return;
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = result.Tables[0];
            this.ddlSpecification.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }
        public void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblbomtotal"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "grp='A'";
            dt = dv.ToTable();
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvtotalbom.FooterRow.FindControl("LblFPercnt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvtotalbom.FooterRow.FindControl("LblFAmount5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmamt)", "")) ? 0.00 : dt.Compute("sum(itmamt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");

            DataTable dt1 = (DataTable)Session["tblbomtopshet"];
            if (dt1.Rows.Count == 0)
                return;

            ((Label)this.gvtopshet.FooterRow.FindControl("LblFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ? 0.00 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtopshet.FooterRow.FindControl("LblFAmount6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(bomcost)", "")) ? 0.00 : dt1.Compute("sum(bomcost)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvtopshet.FooterRow.FindControl("LblFRevenue")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(revenue)", "")) ? 0.00 : dt1.Compute("sum(revenue)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvtopshet.FooterRow.FindControl("LblFProfLoss")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(profloss)", "")) ? 0.00 : dt1.Compute("sum(profloss)", ""))).ToString("#,##0.00;(#,##0.00); ");

            DataTable dt2 = (DataTable)Session["tbldircost"];
            if (dt2.Rows.Count == 0)
                return;
            ((Label)this.gvDirectCost.FooterRow.FindControl("LblFAmount7")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(stdamt)", "")) ? 0.00 : dt2.Compute("sum(stdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvDirectCost.FooterRow.FindControl("LblFPercnt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(percnt)", "")) ? 0.00 : dt2.Compute("sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }

        protected void gvtotalbom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode"));
                if (sircode == "000000000000")
                {
                    e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                    e.Row.Font.Bold = true;
                }
            }
        }
        protected void gvaddimport_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var addimportmat = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdimport"];


            string comcod = this.GetComeCode();
            int index = (this.gvaddimport.PageIndex) * this.gvaddimport.PageSize + e.RowIndex;
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string dayid = this.Request.QueryString["genno"].ToString();
            string style = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            string compcode = addimportmat[index].compcode.ToString();
            string rsircode = addimportmat[index].rsircode.ToString();
            string spcfcode = addimportmat[index].spcfcode.ToString();
            string ssircode = addimportmat[index].ssircode.ToString();

            addimportmat.RemoveAt(index);
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "DELETE_ITEM_FROM_BOM", mlccod, style, colorid, compcode, rsircode, spcfcode, ssircode, "ADDIMPORT", dayid);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);


            }

            Session["tbladdimport"] = addimportmat;
            this.Data_Bind();
        }



        protected void gvaddlocal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            var addlocalmat = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbladdlocal"];

            string comcod = this.GetComeCode();
            int index = (this.gvaddlocal.PageIndex) * this.gvaddlocal.PageSize + e.RowIndex;
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string dayid = this.Request.QueryString["genno"].ToString();
            string style = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            string compcode = addlocalmat[index].compcode.ToString();
            string rsircode = addlocalmat[index].rsircode.ToString();
            string spcfcode = addlocalmat[index].spcfcode.ToString();
            string ssircode = addlocalmat[index].ssircode.ToString();

            addlocalmat.RemoveAt(index);
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "DELETE_ITEM_FROM_BOM", mlccod, style, colorid, compcode, rsircode, spcfcode, ssircode, "ADDLOCAL", dayid);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);


            }

            Session["tbladdlocal"] = addlocalmat;
            this.Data_Bind();
        }
        private void lnkbtnHisprice_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string dayid = this.Request.QueryString["genno"].ToString();
            string style = this.ddlArticle.SelectedValue.ToString().Substring(0, 12);
            string stylename = this.ddlArticle.SelectedItem.ToString();
            string colorid = this.ddlcolor.SelectedValue.ToString().Substring(0, 12);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "COLORWISE_BOM_FORWARD", mlccod, style, colorid, userid, trmid, sessionid, AppDat, dayid);
            if (result)
            {
                string esubject = "Request To Approve Bill of Material (BOM)";
                string url = "http://202.0.94.49/F_34_Mgt/RptAdminInterface.aspx";
                string bodyContent = "Dear Sir, </br>A New BOM Forwared to Approve, Please click  <b> <a href='" + url +
                                "' target='_blank'>" + stylename + " </a> </b> on the link to Approve";

                if (CommonClass.ConfimMail("0101015", esubject, url, bodyContent) == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Forward Successfully');", true);


                }

            }

        }


        protected void lbtnSaveSupplier_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string supplier = "990101001%";
            if (TabName.Value == "tab2primary" || TabName.Value == "AdditionImport")
            {
                supplier = "990100101%";
            }
            string suppliername = this.TxtSupllier.Text.ToString();
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_NEW_SUPPLIER", supplier, suppliername, "");
            if (result)
            {
                this.Get_SupplierInfo();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);


            }
        }

        protected void LbtnAddSupplier_Click(object sender, EventArgs e)
        {
            this.ModalMultiview.ActiveViewIndex = 1;
            this.ModalHead.Text = " Add New Supplier";
            this.TxtSupllier.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }

        protected void Lbtnaddcomp_Click(object sender, EventArgs e)
        {
            this.ModalMultiview.ActiveViewIndex = 2;
            this.ModalHead.Text = " Add New Component Name";
            this.txtcompname.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }

        protected void LbtnSaveComp_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string component = this.txtcompname.Text.ToString();
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_NEW_SUPPLIER", "045%", component, "");
            if (result)
            {
                this.GetComponentList();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);


            }
        }

        protected void LbtnSaveDept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string Deptname = this.txtDeptname.Text.ToString();
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_NEW_SUPPLIER", "490100101%", Deptname, "");
            if (result)
            {
                this.GetProcess();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);


            }
        }

        protected void LbtnAddDept_Click(object sender, EventArgs e)
        {
            this.ModalMultiview.ActiveViewIndex = 3;
            this.ModalHead.Text = " Add New Process Name";
            this.txtDeptname.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }


        protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;

            int rowIndex = row.RowIndex;
            DataView dv1;
            DropDownList ddlgval;
            string SupplierCode = ((DropDownList)this.gvMat.Rows[rowIndex].FindControl("ddlsupplier")).SelectedValue.ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "SUPPERSONALINFO", SupplierCode, "", "", "", "", "", "", "", "");
            ddlgval = ((DropDownList)this.gvMat.Rows[rowIndex].FindControl("ddlOrigin"));
            dv1 = ds1.Tables[0].DefaultView;
            dv1.RowFilter = ("gcod = 71080");
            ddlgval.SelectedValue = (dv1.ToTable().Rows[0]["gdesc1"].ToString()) == "" ? "00000" : dv1.ToTable().Rows[0]["gdesc1"].ToString();
        }
        protected void gvMat_RowEditing(object sender, GridViewEditEventArgs e)
        {




        }

        protected void txtgvssirdesc_Click(object sender, EventArgs e)
        {

            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            this.lblhiddenIndex.Text = clickedRow.RowIndex.ToString();
            DataTable dt = (DataTable)Session["tblSupplier"];
            ddlsupplier.DataTextField = "ssirdesc";
            ddlsupplier.DataValueField = "ssircode";
            ddlsupplier.DataSource = dt;
            ddlsupplier.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "myModalSupli();", true);
        }
        protected void saveSupplier_Click(object sender, EventArgs e)
        {
            int lblhiddenIndex = Convert.ToInt32(this.lblhiddenIndex.Text);

            string mLCCode = this.ddlmlccode.SelectedValue.ToString();
            var list = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tblimport"];
            DataTable dt = ASITUtility03.ListToDataTable(list);
            if (mLCCode != list[0].mlccode.ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Something Went Wrong! Please reload your Page');", true);
                return;
            }
            list[lblhiddenIndex].ssircode = this.ddlsupplier.SelectedValue;
            list[lblhiddenIndex].ssirdesc = this.ddlsupplier.SelectedItem.ToString();
            Session["tblimport"] = list;
            Data_Bind();
        }

        protected void saveSupplierLoc_Click(object sender, EventArgs e)
        {
            int lblhiddenIndex = Convert.ToInt32(this.lblhiddenIndex.Text);

            var list = (List<SPEENTITY.C_03_CostABgd.EClassLC.EClassMatReqAgainsOrder>)Session["tbllocal"];
            DataTable dt = ASITUtility03.ListToDataTable(list);
            list[lblhiddenIndex].ssircode = this.ddlsupplierList.SelectedValue;
            list[lblhiddenIndex].ssirdesc = this.ddlsupplierList.SelectedItem.ToString();
            Session["tbllocal"] = list;
            Data_Bind();
        }

        protected void lblgvLocssirdesc_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            this.lblhiddenIndex.Text = clickedRow.RowIndex.ToString();
            DataTable dt = (DataTable)Session["tblSupplier"];
            ddlsupplierList.DataTextField = "ssirdesc";
            ddlsupplierList.DataValueField = "ssircode";
            ddlsupplierList.DataSource = dt;
            ddlsupplierList.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "myModalSupliLoc();", true);
        }

        protected void DdlSpecres_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string matsircode = this.ddlResourcesCost.SelectedValue;
            string matspecfcod = this.DdlSpecres.SelectedValue;
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSUPLRRESLISTMATWISE", matsircode, matspecfcod, "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            this.ddlSupliAddiImp.SelectedValue = ds1.Tables[0].Rows[0]["ssircode"].ToString();
        }

        protected void lblProcess0_Click(object sender, EventArgs e)
        {
            this.Get_material();
        }
    }

    

}
