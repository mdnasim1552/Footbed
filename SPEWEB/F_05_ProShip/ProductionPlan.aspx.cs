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

namespace SPEWEB.F_05_ProShip
{
    public partial class ProductionPlan : System.Web.UI.Page
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
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.TxtChngdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                
                this.GetFloorSetupInfo();
                this.Load_Project_Combo();

                this.CommonButton();
                
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION PLAN";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFiUpdate_Click);

            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            if (this.Session_tbExPlan_Update())
            {
                this.Data_Bind();
            }

        }

        private void CommonButton()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true; ;

            //   ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Load_Project_Combo()
        {


            this.ddlOrderList.Items.Clear();
            string comcod = this.GetCompCode();
            string FindProject = "%" + this.txtOrderSearch.Text.Trim() + "1601%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", FindProject, "%", "%", "", "", "", "", "", "");
            //   DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "mlcdesc";
            this.ddlOrderList.DataValueField = "mlccod";
            this.ddlOrderList.DataSource = ds1.Tables[1];
            this.ddlOrderList.DataBind();
            ViewState["tblordstyle"] = ds1.Tables[0];
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlOrderList.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }
            this.ddlOrderList_SelectedIndexChanged(null, null);

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {


        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnPrevList.Visible = true;
                this.ddlPrevList.Visible = true;
                this.ddlOrderList.Visible = true;
                this.lblddlOrder.Visible = false;
                this.ddlStyle.Enabled = true;

                this.ddlOrderList.Enabled = true;
                this.gvsizes.DataSource = null;
                this.gvsizes.DataBind();
                this.lblCurNo1.Text = "PLN" + "00" + "-";
                this.txtCurDate.Enabled = true;
                this.lbtnOk.Text = "Ok";
                return;
            }


            this.lblddlOrder.Text = (this.ddlOrderList.Items.Count == 0 ? "XXX" : this.ddlOrderList.SelectedItem.Text.Trim());
            this.ddlOrderList.Visible = false;
            this.lblddlOrder.Visible = true;
            this.ddlOrderList.Enabled = false;
            this.lbtnOk.Text = "New";
            this.ddlStyle.Enabled = false;
            this.lbtnPrevList.Visible = false;
            this.ddlPrevList.Visible = false;
            this.Get_Receive_Info();
            this.GetRequisitionList();

        }

        protected bool Session_tbExPlan_Update()
        {

            string genno = this.Request.QueryString["genno"].ToString();
            DataTable tbl1 = (DataTable)ViewState["tblproplan"];
            int TblRowIndex2;
            for (int i = 0; i < this.gvsizes.Rows.Count; i++)
            {
                string prddate = Convert.ToDateTime(((TextBox)this.gvsizes.Rows[i].FindControl("TxtStrdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string linecode = ((DropDownList)this.gvsizes.Rows[i].FindControl("ddlline")).SelectedValue.ToString();
                double s1 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF1")).Text.Trim());
                double s2 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF2")).Text.Trim());
                double s3 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF3")).Text.Trim());
                double s4 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF4")).Text.Trim());
                double s5 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF5")).Text.Trim());
                double s6 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF6")).Text.Trim());
                double s7 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF7")).Text.Trim());
                double s8 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF8")).Text.Trim());
                double s9 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF9")).Text.Trim());
                double s10 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF10")).Text.Trim());
                double s11 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF11")).Text.Trim());
                double s12 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF12")).Text.Trim());
                double s13 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF13")).Text.Trim());
                double s14 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF14")).Text.Trim());
                double s15 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF15")).Text.Trim());
                double s16 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF16")).Text.Trim());
                double s17 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF17")).Text.Trim());
                double s18 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF18")).Text.Trim());
                double s19 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF19")).Text.Trim());
                double s20 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF20")).Text.Trim());
                double s21 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF21")).Text.Trim());
                double s22 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF22")).Text.Trim());
                double s23 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF23")).Text.Trim());
                double s24 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF24")).Text.Trim());
                double s25 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF25")).Text.Trim());
                double s26 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF26")).Text.Trim());
                double s27 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF27")).Text.Trim());
                double s28 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF28")).Text.Trim());
                double s29 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF29")).Text.Trim());
                double s30 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF30")).Text.Trim());
                double s31 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF31")).Text.Trim());
                double s32 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF32")).Text.Trim());
                double s33 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF33")).Text.Trim());
                double s34 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF34")).Text.Trim());
                double s35 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF35")).Text.Trim());
                double s36 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF36")).Text.Trim());
                double s37 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF37")).Text.Trim());
                double s38 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF38")).Text.Trim());
                double s39 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF39")).Text.Trim());
                double s40 = Convert.ToDouble("0" + ((TextBox)gvsizes.Rows[i].FindControl("txtgvF40")).Text.Trim());
                TblRowIndex2 = (this.gvsizes.PageIndex) * this.gvsizes.PageSize + i;

                tbl1.Rows[TblRowIndex2]["prddate"] = prddate;
                tbl1.Rows[TblRowIndex2]["linecode"] = linecode;
                tbl1.Rows[TblRowIndex2]["s1"] = s1;
                tbl1.Rows[TblRowIndex2]["s2"] = s2;
                tbl1.Rows[TblRowIndex2]["s3"] = s3;
                tbl1.Rows[TblRowIndex2]["s4"] = s4;
                tbl1.Rows[TblRowIndex2]["s5"] = s5;
                tbl1.Rows[TblRowIndex2]["s6"] = s6;
                tbl1.Rows[TblRowIndex2]["s7"] = s7;
                tbl1.Rows[TblRowIndex2]["s8"] = s8;
                tbl1.Rows[TblRowIndex2]["s9"] = s9;
                tbl1.Rows[TblRowIndex2]["s10"] = s10;
                tbl1.Rows[TblRowIndex2]["s11"] = s11;
                tbl1.Rows[TblRowIndex2]["s12"] = s12;
                tbl1.Rows[TblRowIndex2]["s13"] = s13;
                tbl1.Rows[TblRowIndex2]["s14"] = s14;
                tbl1.Rows[TblRowIndex2]["s15"] = s15;
                tbl1.Rows[TblRowIndex2]["s16"] = s16;
                tbl1.Rows[TblRowIndex2]["s17"] = s17;
                tbl1.Rows[TblRowIndex2]["s18"] = s18;
                tbl1.Rows[TblRowIndex2]["s19"] = s19;
                tbl1.Rows[TblRowIndex2]["s20"] = s20;
                tbl1.Rows[TblRowIndex2]["s21"] = s21;
                tbl1.Rows[TblRowIndex2]["s22"] = s22;
                tbl1.Rows[TblRowIndex2]["s23"] = s23;
                tbl1.Rows[TblRowIndex2]["s24"] = s24;
                tbl1.Rows[TblRowIndex2]["s25"] = s25;
                tbl1.Rows[TblRowIndex2]["s26"] = s26;
                tbl1.Rows[TblRowIndex2]["s27"] = s27;
                tbl1.Rows[TblRowIndex2]["s28"] = s28;
                tbl1.Rows[TblRowIndex2]["s29"] = s29;
                tbl1.Rows[TblRowIndex2]["s30"] = s30;
                tbl1.Rows[TblRowIndex2]["s31"] = s31;
                tbl1.Rows[TblRowIndex2]["s32"] = s32;
                tbl1.Rows[TblRowIndex2]["s33"] = s33;
                tbl1.Rows[TblRowIndex2]["s34"] = s34;
                tbl1.Rows[TblRowIndex2]["s35"] = s35;
                tbl1.Rows[TblRowIndex2]["s36"] = s36;
                tbl1.Rows[TblRowIndex2]["s37"] = s37;
                tbl1.Rows[TblRowIndex2]["s38"] = s38;
                tbl1.Rows[TblRowIndex2]["s39"] = s39;
                tbl1.Rows[TblRowIndex2]["s40"] = s40;
                tbl1.Rows[TblRowIndex2]["totalqty"] = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                    s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);
            }

            ViewState["tblproplan"] = tbl1;
            DataTable allsize = (DataTable)ViewState["tbl_plansize"];
            DataView dv = ((DataTable)Session["PlanSummary"]).Copy().DefaultView;
            dv.RowFilter = "slnum = '" + genno + "'  ";
            DataTable curplan = dv.ToTable();
            int k = 1; ;
            for (int i = 1; i <= 40; i++)
            {
                int n = Convert.ToInt32(curplan.Rows[0]["s" + i]);
                if (n == 0)
                {
                    curplan.Columns.Remove("s" + i);


                }
                else
                {


                    curplan.Columns["s" + i].ColumnName = "s" + k;
                    k++;
                }
            }

            DataTable dt = curplan;

            for (int j = 1; j < allsize.Rows.Count + 1; j++)
            {
                double qty = Convert.ToDouble(tbl1.AsEnumerable().Sum(row => row.Field<decimal>("s" + j)));

                double qty1 = Convert.ToDouble(curplan.AsEnumerable().Sum(row => row.Field<decimal>("s" + j)));
                double plantotal = Convert.ToDouble(tbl1.AsEnumerable().Sum(row => row.Field<decimal>("totalqty")));

                double asrttotal = Convert.ToDouble(curplan.AsEnumerable().Sum(row => row.Field<decimal>("totalqty")));

                if (plantotal > asrttotal)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Plan Allocation Overflow');", true);


                    return false;
                }
                else if (plantotal != asrttotal)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Plan Allocation Not Match with " + asrttotal + "');", true);
                    return false;
                }
                if (this.GetCompCode() != "5301")
                {
                    if (qty > qty1 || qty != qty1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Size Allocation Overflow');", true);
                        return false;
                    }

                }
            }

            return true;
        }

        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblproplan"];
          
            if (tbl1.Rows.Count == 0)
                return;
            this.gvsizes.DataSource = tbl1;
            this.gvsizes.DataBind();
            this.FooterCalculation();

        }

        protected void lbtnPrevList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETPREVLIST", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.Items.Clear();
            this.ddlPrevList.DataTextField = "prono1";
            this.ddlPrevList.DataValueField = "prono";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();
            ds1.Dispose();
        }

        private void GetFloorSetupInfo()
        {
            //
            string comcod = GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPRODSETUPINFO", "", "", "", "", "", "", "", "", "");
            ViewState["tblline"] = ds1.Tables[1];


        }
        protected void Get_Receive_Info()
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string toddate = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
            string slnum = this.Request.QueryString["genno"].ToString();

            string linecode = this.Request.QueryString["centrid"].ToString();

            string CurDate1 = this.txtCurDate.Text;
            //  string mPLNNo = "NEWPLN";
            DataSet ds1 = new DataSet();
            //   if (this.ddlPrevList.Items.Count > 0)
            //    {
            //   this.txtCurDate.Enabled = false;
            //        mPLNNo = this.ddlPrevList.SelectedValue.ToString();
            //   }
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSHEET_WISE_DAILYPLANSHEET", mlccod, styleid, colorid, dayid, toddate, slnum, linecode, "");

            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data not Found');", true);

                return;
            }

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = i + 1;// Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));
                this.gvsizes.Columns[columid + 9].Visible = true;
                this.gvsizes.Columns[columid + 9].HeaderText = ds1.Tables[1].Rows[i]["sizedesc"].ToString().Trim();
            }
            this.gvsizes.EditIndex = -1;
            ViewState["tblproplan"] = ds1.Tables[0];
            ViewState["tbl_plansize"] = ds1.Tables[1];

            this.OrderDetailsInformation();
            this.Data_Bind();
            if (ds1.Tables[2].Rows.Count == 0)
            {

                this.lblCurNo1.Text = "PLN" + Convert.ToDateTime(toddate).ToString("MM");
                this.lblCurNo2.Text = Convert.ToDateTime(toddate).ToString("dd");
                return;

            }


            this.lblCurNo1.Text = ds1.Tables[2].Rows[0]["prono1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[2].Rows[0]["prono1"].ToString().Substring(6, 2);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["prodate"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[2].Rows[0]["refno"].ToString();
            //  this.Data_Bind();



        }

        private void OrderDetailsInformation()
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string toddate = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
            string slnum = this.Request.QueryString["genno"].ToString();
            string type = (dayid != "00000000") ? "Reorder" : "";
            string date = (dayid == "00000000") ? "01-Jan-1900" : Convert.ToDateTime(dayid.Substring(4, 2) + "/" + dayid.Substring(6, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, type, date, styleid, "", "", "", ""); ;

            for (int i = 5; i < 45; i++)
                this.gv1.Columns[i].Visible = false;

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gv1.Columns[columid + 4].Visible = true;
                this.gv1.Columns[columid + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            ViewState["tblOrderQty"] = lst;
            this.gv1.DataSource = lst;
            this.gv1.DataBind();
            this.FooterCal();

            DataSet result = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERWISE_SIZE_INFORMATION", mlccod, styleid, colorid, dayid, toddate, slnum);
            if (result == null)
            {
                return;
            }
            int j = 1;
            Session["PlanSummary"] = result.Tables[2];
            for (int i = 0; i < result.Tables[1].Rows.Count; i++)
            {

                int columid = j++;// Convert.ToInt32(ASTUtility.Right(result.Tables[1].Rows[i]["sizeid"].ToString(), 2));               

                this.gvPlanSummary.Columns[columid + 2].Visible = true;
                this.gvPlanSummary.Columns[columid + 2].HeaderText = result.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }

            this.gvPlanSummary.DataSource = result.Tables[2];
            this.gvPlanSummary.DataBind();

        }
        private void FooterCal()
        {
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            if (list == null || list.Count == 0)
            {
                return;
            }

        ((Label)this.gv1.FooterRow.FindControl("FLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ");
            // ((Label)this.gv1.FooterRow.FindControl("FLblgvColTotal")).Text = ((list.Sum(p => p.colqty) == 0) ? 0 : list.Sum(p => p.colqty)).ToString("#,##0;(#,##0); ");


        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session_tbExPlan_Update()) { this.Data_Bind(); }
            //  this.gvShiMentInfo.PageIndex = ((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;

        }

        protected void FooterCalculation()
        {

            DataTable tbl1 = (DataTable)ViewState["tblproplan"];
            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS1")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s1)", "")) ?
                    0.00 : tbl1.Compute("Sum(s1)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS2")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s2)", "")) ?
                    0.00 : tbl1.Compute("Sum(s2)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS3")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s3)", "")) ?
                    0.00 : tbl1.Compute("Sum(s3)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS4")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s4)", "")) ?
                    0.00 : tbl1.Compute("Sum(s4)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS5")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s5)", "")) ?
                    0.00 : tbl1.Compute("Sum(s5)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS6")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s6)", "")) ?
                    0.00 : tbl1.Compute("Sum(s6)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS7")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s7)", "")) ?
                    0.00 : tbl1.Compute("Sum(s7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS8")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s8)", "")) ?
                    0.00 : tbl1.Compute("Sum(s8)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS9")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s9)", "")) ?
                    0.00 : tbl1.Compute("Sum(s9)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS10")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s10)", "")) ?
                    0.00 : tbl1.Compute("Sum(s10)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS11")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s11)", "")) ?
                    0.00 : tbl1.Compute("Sum(s11)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS12")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s12)", "")) ?
                    0.00 : tbl1.Compute("Sum(s12)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS13")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s13)", "")) ?
                    0.00 : tbl1.Compute("Sum(s13)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS14")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s14)", "")) ?
                    0.00 : tbl1.Compute("Sum(s14)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS15")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s15)", "")) ?
                    0.00 : tbl1.Compute("Sum(s15)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS16")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s16)", "")) ?
                    0.00 : tbl1.Compute("Sum(s16)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS17")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s17)", "")) ?
                    0.00 : tbl1.Compute("Sum(s17)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS18")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s18)", "")) ?
                    0.00 : tbl1.Compute("Sum(s18)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS19")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s19)", "")) ?
                    0.00 : tbl1.Compute("Sum(s19)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS20")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s20)", "")) ?
                    0.00 : tbl1.Compute("Sum(s20)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS21")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s21)", "")) ?
                    0.00 : tbl1.Compute("Sum(s21)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS22")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s22)", "")) ?
                    0.00 : tbl1.Compute("Sum(s22)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS23")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s23)", "")) ?
                    0.00 : tbl1.Compute("Sum(s23)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS24")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s24)", "")) ?
                    0.00 : tbl1.Compute("Sum(s24)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS25")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s25)", "")) ?
                    0.00 : tbl1.Compute("Sum(s25)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS26")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s26)", "")) ?
                    0.00 : tbl1.Compute("Sum(s26)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS27")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s27)", "")) ?
                    0.00 : tbl1.Compute("Sum(s27)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS28")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s28)", "")) ?
                    0.00 : tbl1.Compute("Sum(s28)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS29")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s29)", "")) ?
                    0.00 : tbl1.Compute("Sum(s2)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS30")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s30)", "")) ?
                    0.00 : tbl1.Compute("Sum(s30)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS31")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s31)", "")) ?
                    0.00 : tbl1.Compute("Sum(s31)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS32")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s32)", "")) ?
                    0.00 : tbl1.Compute("Sum(s32)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS33")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s33)", "")) ?
                    0.00 : tbl1.Compute("Sum(s33)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS34")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s34)", "")) ?
                    0.00 : tbl1.Compute("Sum(s34)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS5")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s5)", "")) ?
                    0.00 : tbl1.Compute("Sum(s5)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS36")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s36)", "")) ?
                    0.00 : tbl1.Compute("Sum(s36)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS37")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s37)", "")) ?
                    0.00 : tbl1.Compute("Sum(s37)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS38")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s38)", "")) ?
                    0.00 : tbl1.Compute("Sum(s38)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS39")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s39)", "")) ?
                    0.00 : tbl1.Compute("Sum(s39)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsizes.FooterRow.FindControl("lblgvFS40")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(s40)", "")) ?
                    0.00 : tbl1.Compute("Sum(s40)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvsizes.FooterRow.FindControl("FLblgvTotal")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(totalqty)", "")) ?
                    0.00 : tbl1.Compute("Sum(totalqty)", ""))).ToString("#,##0;(#,##0); ");

        }




        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlOrderList.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc1", "stylecode1");
            this.ddlStyle.DataTextField = "styledesc1";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();
            if (this.Request.QueryString["sircode"].Length > 0)
            {
                this.ddlStyle.SelectedValue = this.Request.QueryString["sircode"].ToString();
                this.lbtnOk_Click(null, null);

            }

        }
        protected void ImgbtnFindOrder_Click(object sender, EventArgs e)
        {
            Load_Project_Combo();
        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            if (this.Session_tbExPlan_Update() == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Size Allocation Overflow');", true);

                return;
            }
            DataTable tbl1 = (DataTable)ViewState["tblproplan"];
            string mPRONO = "PLN" + Convert.ToDateTime(this.Request.QueryString["date"]).ToString("yyyyMMdd");// this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string mPRODAT = this.txtCurDate.Text.Trim();
            string billref = this.txtrefno.Text.Trim();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string odayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string slnum = this.Request.QueryString["genno"].ToString();
            bool result = false;
            DataTable sizes = (DataTable)ViewState["tbl_plansize"];
            String[] SizeID1 = new string[] { };
            var listsizeid = SizeID1.ToList();

            DataRow[] linedr = tbl1.Select("linecode = '000000000000' and offday ='false'");
            if (linedr.Count() > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Hey! You missing Line Selection');", true);
                return;

            }
            int k = 0;
            foreach (DataRow dr in sizes.Rows)
            {
                listsizeid.Add(dr["sizeid"].ToString());
            }

            SizeID1 = listsizeid.ToArray();
            for (int i = 0; i < gvsizes.Rows.Count; i++)
            {
                string prddate = Convert.ToDateTime(((TextBox)gvsizes.Rows[i].FindControl("TxtStrdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string linecode = ((DropDownList)gvsizes.Rows[i].FindControl("ddlline")).SelectedValue.ToString();

                String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                               "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                               "720100101013", "720100101014", "720100101015" , "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                                  "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                                  "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                                  "720100101038", "720100101039", "720100101040"};
                String[] OrderQty = {
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF1")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF2")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF3")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF4")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF5")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF6")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF7")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF8")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF9")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF10")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF11")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF12")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF13")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF14")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF15")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF16")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF17")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF18")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF19")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF20")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF21")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF22")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF23")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF24")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF25")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF26")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF27")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF28")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF29")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF30")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF31")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF32")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF33")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF34")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF35")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF36")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF37")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF38")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF39")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF40")).Text.Trim(),
            };


                for (int j = 0; j < SizeID1.Length; j++)
                {
                    if (Convert.ToDouble(OrderQty[j]) > 0)
                    {
                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_TRIALORDERWISE_PRODUCTION_PLAN", mPRONO, mlccod, styleid, colorid,
                                       SizeID1[j], OrderQty[j], linecode, prddate, mPRODAT, billref, odayid, slnum, "", "", "");
                    }

                }


            }

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }



        }




        protected void gvsizes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tblline = (DataTable)ViewState["tblline"];
            // HyperLink HypPlanDetails = (HyperLink)e.Row.FindControl("HypPlanDetails");
            string comcod = this.GetCompCode();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlline = (DropDownList)e.Row.FindControl("ddlline");
                ddlline.DataTextField = "sirdesc";
                ddlline.DataValueField = "sircode";
                ddlline.DataSource = tblline;
                ddlline.DataBind();
                ddlline.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "linecode"));

                DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "LINE_AND_DAYWISE_ALLOCATED_ITEMS", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "linecode")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prddate")), "", "", "", "", "");
                if (ds4 != null)
                {
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        ((Label)e.Row.FindControl("LblAlocation")).Text = Convert.ToDouble(ds4.Tables[0].Rows[0]["tqty"]).ToString("#,##0;");
                        ((Label)e.Row.FindControl("LblLinecapacity")).Text = "LC: " + Convert.ToDouble(ds4.Tables[0].Rows[0]["capcity"]).ToString("#,##0;");


                    }

                }

                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approved")) == "Ok")
                {
                    e.Row.BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Enabled = false;

                }

                if (Convert.ToBoolean((DataBinder.Eval(e.Row.DataItem, "offday"))) == true)
                {
                    e.Row.BackColor = System.Drawing.Color.MediumVioletRed;
                    ((Label)e.Row.FindControl("lblgvTrialOrder")).ForeColor = System.Drawing.Color.White;
                    ((TextBox)e.Row.FindControl("TxtStrdate")).ForeColor = System.Drawing.Color.White;
                    ((Label)e.Row.FindControl("LblDay")).ForeColor = System.Drawing.Color.White;

                    e.Row.FindControl("LbtnApprove").Visible = false;
                    e.Row.FindControl("ddlline").Visible = false;
                    e.Row.FindControl("LbtnCopy").Visible = false;
                    e.Row.Enabled = false;
                    // HypPlanDetails.Enabled = true;
                    e.Row.FindControl("LblReason").Visible = true;
                    ((Label)e.Row.FindControl("LblReason")).Text = DataBinder.Eval(e.Row.DataItem, "reason").ToString();
                }
                //((CheckBox)e.Row.FindControl("CbLineDate")).Enabled = true;
                //((TextBox)e.Row.FindControl("TxtChngdate")).Enabled = true;
                //HypPlanDetails.NavigateUrl = "~/F_05_ProShip/MasterCalendarSetup?Type=plancalendar&sircode="
                //    + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "linecode")) + 
                //    "&date="+ Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "prddate")).ToString("dd-MMM-yyyy") + 
                //    "&dayid="+ Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "prddate")).ToString("dd-MMM-yyyy");
                //HypPlanDetails.Enabled = true;
            }


        }

        protected void LbtnApprove_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = ((Label)this.gvsizes.Rows[index].FindControl("lblgvStyleID")).Text.ToString();
            string colorid = ((Label)this.gvsizes.Rows[index].FindControl("lblgvColorID")).Text.ToString();
            string date = Convert.ToDateTime(((TextBox)this.gvsizes.Rows[index].FindControl("TxtStrdate")).Text).ToString("yyyyMMdd");
            string mPRONO = "PLN" + Convert.ToDateTime(this.Request.QueryString["date"]).ToString("yyyyMMdd");// this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            string odayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string slnum = this.Request.QueryString["genno"].ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string linecode = ((DropDownList)this.gvsizes.Rows[index].FindControl("ddlline")).SelectedValue.ToString();

            DataSet result = purData.GetTransInfoNew(comcod, "SP_ENTRY_EXPORT_PLAN", "APPROVE_PRODUCTION_PLAN", null, null, null, mPRONO, mlccod, styleid, colorid,
                                  date, odayid, AppDat, userid, slnum, linecode);
            if (result != null)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + result.Tables[0].Rows[0]["msg"].ToString() + "');", true);

                this.Get_Receive_Info();
            }

        }
        protected void gvsizes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblproplan"];
            string sORDERNO = this.ddlOrderList.SelectedValue.ToString().Trim();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string date = Convert.ToDateTime(((TextBox)this.gvsizes.Rows[e.RowIndex].FindControl("TxtStrdate")).Text).ToString("dd-MMM-yyyy");
            string slnum = this.Request.QueryString["genno"].ToString();
            string mPRONO = "PLN" + Convert.ToDateTime(this.Request.QueryString["date"]).ToString("yyyyMMdd");// this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();

            string linecode = ((DropDownList)this.gvsizes.Rows[e.RowIndex].FindControl("ddlline")).SelectedValue.ToString();

            //lblgvLine
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "DELETE_PRODUCTION_PLAN", sORDERNO, styleid, mPRONO, date, colorid, dayid, slnum, linecode, "", "", "", "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Success');", true);


                int rowindex = (this.gvsizes.PageSize) * (this.gvsizes.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Unable to Delete Due to Requistion');", true);
            }

            DataView dv = dt.DefaultView;
            ViewState["tblproplan"] = dv.ToTable();
            this.Data_Bind();
        }



        protected void ApproveAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sORDERNO = this.ddlOrderList.SelectedValue.ToString().Trim();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string slnum = this.Request.QueryString["genno"].ToString();
            string mPRONO = "PLN" + Convert.ToDateTime(this.Request.QueryString["date"]).ToString("yyyyMMdd");// this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "APPROVE_PLAN_ALL", sORDERNO, mPRONO, styleid, colorid, dayid, slnum, userid, AppDat, "", "", "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Approved All Successfully');", true);
                this.Get_Receive_Info();
            }
        }

        protected void lnkbtnAddNew_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblproplan"];
            if (tbl1.Rows.Count == 0)
                return;

            int count = tbl1.Rows.Count;
            DataRow lastrow = tbl1.Rows[tbl1.Rows.Count - 1];
            lastrow["offday"] = "false";
            //lastrow["prddate"] = Convert.ToDateTime(lastrow["prddate"]);
            tbl1.ImportRow(lastrow);

            this.Data_Bind();

        }

        protected void gvPlanSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string slnum = this.Request.QueryString["genno"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string cslnum = ((Label)e.Row.FindControl("PllblgvSl")).Text.ToString();

                if (cslnum == slnum)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }
        }

        protected void LbtnCopy_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string date = ((TextBox)this.gvsizes.Rows[index].FindControl("TxtStrdate")).Text.ToString();
            
            DataTable tbl1 = (DataTable)ViewState["tblproplan"];
            if (tbl1.Rows.Count == 0)
                return;
            DataRow[] dr = tbl1.Select("prddate = '" + date + "'");
            tbl1.ImportRow(dr[0]);

            DataView dv = tbl1.DefaultView;
            dv.Sort = "prddate";
            tbl1 = dv.ToTable();

            ViewState["tblproplan"] = tbl1;
            this.Data_Bind();
        }

        protected void gvsizes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            DataTable dtsizes = (DataTable)ViewState["tbl_plansize"];
          
            DataTable PlanSummary = (DataTable)Session["PlanSummary"];
            string slnum = this.Request.QueryString["genno"].ToString();

            DataView dv = PlanSummary.DefaultView;
            dv.RowFilter = "slnum='" + slnum + "'";
            PlanSummary = dv.ToTable();
            //check if the row is the header row
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //create the first row
                GridViewRow extraHeader1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                extraHeader1.BackColor = System.Drawing.Color.LightSalmon;

                TableCell cell1 = new TableCell();
                cell1.ColumnSpan = 8;
                cell1.Text = "#Current Plan Total Size wise Breakdown >>";
                cell1.BackColor = System.Drawing.Color.LightSkyBlue;
                extraHeader1.Cells.Add(cell1);

                double total = 0;
                int i = 1;
                foreach (DataRow item in dtsizes.Rows)
                {
                    total += Convert.ToDouble(PlanSummary.Rows[0]["s" + i]);
                    TableCell cell2 = new TableCell();
                    cell2.ColumnSpan = 1;
                    cell2.HorizontalAlign = HorizontalAlign.Right;
                    cell2.Text = Convert.ToDouble(PlanSummary.Rows[0]["s" + i]).ToString("#,##0");
                    cell2.BorderColor = System.Drawing.Color.LightSkyBlue;
                    cell2.BackColor = System.Drawing.Color.WhiteSmoke;
                    extraHeader1.Cells.Add(cell2);
                    i++;
                }
                TableCell cell3 = new TableCell();
                cell3.Font.Bold = true;
                cell3.ColumnSpan = 4;
                cell3.Text = "Total: " + total.ToString("#,##0");
                cell3.BackColor = System.Drawing.Color.LightSkyBlue;
                extraHeader1.Cells.Add(cell3);

                gv.Controls[0].Controls.AddAt(0, extraHeader1);
            }
        }

        protected void GetLineChangeList()
        {
            DataTable dt = (DataTable)ViewState["tblline"];
            this.DdlChangeLine.DataValueField = "sircode";
            this.DdlChangeLine.DataTextField = "sirdesc";
            this.DdlChangeLine.DataSource = dt;
            this.DdlChangeLine.DataBind();
        }

        protected void CbLineTrnsfr_CheckedChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrole= (hst["userrole"].ToString());

            if (usrole == "05" || usrole == "97")
            {
                if (CbLineTrnsfr.Checked)
                {
                    LblChangeLine.Visible = true;
                    DdlChangeLine.Visible = true;
                    LnkbtnConfrm.Visible = true;
                    this.GetLineChangeList();
                }
                else
                {
                    LblChangeLine.Visible = false;
                    DdlChangeLine.Visible = false;
                    LnkbtnConfrm.Visible = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You are not authorized to change the Line.');", true);
                return;
            }

            
        }

        protected void CbLineDate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            GridViewRow index = (GridViewRow)cbx.NamingContainer;

            if (((CheckBox)this.gvsizes.Rows[index.RowIndex].FindControl("CbLineDate")).Checked)
            {
                ((TextBox)this.gvsizes.Rows[index.RowIndex].FindControl("TxtChngdate")).Visible = true;
            }
            else
            {
                ((TextBox)this.gvsizes.Rows[index.RowIndex].FindControl("TxtChngdate")).Visible = false;
            }
        }

        protected void LnkbtnConfrm_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0,12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string slnum = this.Request.QueryString["genno"].ToString();
            string toddate = this.Request.QueryString["date"].ToString();
            string exlinecode = this.Request.QueryString["centrid"].ToString();
            string newlinecode = this.DdlChangeLine.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "PRODUCTION_PLAN_LINE_TRANSFER", mlccod, styleid, colorid, dayid, slnum, toddate, exlinecode, newlinecode, "", "", "", "", "");

            this.Data_Bind();

            if (result == true && ConstantInfo.LogStatus == true)
            {
                string actcode = Request.QueryString["actcode"].ToString();
                string fromline = Request.QueryString["centrid"].ToString();
                string style = Request.QueryString["sircode"].ToString();
                string slnumb = Request.QueryString["genno"].ToString();
                string eventtype = "Plan Line Change";
                string eventdesc = "Plan Line transfer from "+ fromline + " Line to "+ this.DdlChangeLine.SelectedValue.ToString();
                string eventdesc2 = "Trial Order: " + actcode+", Style: " + style + " Sl: " + slnumb;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                Response.Redirect("~/F_05_ProShip/ProductionPlan?Type=Entry&actcode=" + mlccod + "&sircode=" + styleid + colorid + dayid + "&date=" + toddate + "&genno=" + slnum + "&centrid=" + newlinecode);
            }

        }

        private void GetRequisitionList()
        {
            try
            {
                string comcod = this.GetCompCode();
                string OrderNo = this.ddlOrderList.SelectedValue.ToString();
                string dayid = this.ddlStyle.SelectedValue.ToString().Trim().Substring(24, 8);
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDER_WISE_PRODUCTION_REQ_LIST", OrderNo, dayid);

                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                {
                    Session["tbExPlan"] = null;
                    gvShiMentInfo2.DataSource = null;
                    gvShiMentInfo2.DataBind();
                    this.gvProcessStat.DataSource =null;
                    this.gvProcessStat.DataBind();
                    return;
                }

                Session["tbExPlan"] = ds1.Tables[0];
                gvShiMentInfo2.DataSource = ds1.Tables[0];
                gvShiMentInfo2.DataBind();

                if (ds1.Tables[1].Rows.Count > 0)
                {
                    Session["tbProcessStat"] = ds1.Tables[1];
                    this.gvProcessStat.DataSource = ds1.Tables[1];
                    this.gvProcessStat.DataBind();
                }

                ((Label)this.gvShiMentInfo2.FooterRow.FindControl("gvsilftrTtlFgQty")).Text = Convert.ToDouble(ds1.Tables[0].Compute("Sum(rsqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvShiMentInfo2.FooterRow.FindControl("gvsilftrTtlMatQty")).Text = Convert.ToDouble(ds1.Tables[0].Compute("Sum(matqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
            }
            catch(Exception ex)
            {

            }
        }

        protected void LbtnIssueMulti_Click(object sender, EventArgs e)
        {
            string order = "";
            string reqno = "";
            string reqdat = "";

            for (int i = 0; i < this.gvShiMentInfo2.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvShiMentInfo2.Rows[i].FindControl("chkPrntCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblBatchCode")).Text;
                    string dayid = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblDayId")).Text;
                    string order2 = mlccod + dayid;
                    reqdat = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblReqDate")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvlblsiReqNo")).Text;

                }
            }

            string url = ResolveClientUrl("~/F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=" + reqno + "&actcode=" + order.Substring(0, 12) + "&reptype=NORMAL");
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
        }

        protected void lnkbtnPrintCombined_Click(object sender, EventArgs e)
        {
            string order = "";
            string reqno = "";
            string reqdat = "";

            for (int i = 0; i < this.gvShiMentInfo2.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvShiMentInfo2.Rows[i].FindControl("chkPrntCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblBatchCode")).Text;
                    string dayid = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblDayId")).Text;
                    string order2 = mlccod + dayid;
                    reqdat = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblReqDate")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvlblsiReqNo")).Text;

                }
            }

            string printFormat = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;
            string url = ResolveClientUrl("~//F_15_Pro/Print.aspx?Type=PrintReqMulti&mlccod=" + order + "&reqno=" + reqno + "&pbdate=" + reqdat + "&printfrmt=" + printFormat);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
        }

        protected void gvShiMentInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string slnum = this.Request.QueryString["genno"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string cslnum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slnum"));

                if (cslnum == slnum)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }
        }

        protected void LbtnUpdateDateChange_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrole = (hst["userrole"].ToString());

            if (usrole == "05" || usrole == "97" )
            {
                string tardate = Convert.ToDateTime(this.Lbltardate.Text.Trim()).ToString("dd-MMM-yyyy");
                string changetodate = Convert.ToDateTime(this.TxtChngdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string prdline = this.Lbllinecode.Text.Trim().ToString();
                string comcod = this.GetCompCode();
                string mlccod = this.ddlOrderList.SelectedValue.ToString();
                string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
                string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
                string odayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
                string slnum = this.Request.QueryString["genno"].ToString();
                string shipmentdate = this.Request.QueryString["date"].ToString();
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO",
                    "PRODUCTION_PLAN_DATE_TRANSFER", mlccod, styleid, colorid, odayid, slnum, prdline, shipmentdate, tardate, changetodate, "", "", "", "");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Date Changed Successfully');", true);
                    if (result == true && ConstantInfo.LogStatus == true)
                    {
                            
                        string eventtype = "Plan Date Change";
                        string eventdesc = "Plan Date transfer from " + tardate + " to " + changetodate;
                        string eventdesc2 = "Trial Order: " + mlccod + ", Style: " + styleid+ colorid+ odayid + " Slnum: " + slnum;

                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    }
               }
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeModal( );", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You are not authorized to change the Line.');", true);
                return;
            }
        }
    }
}