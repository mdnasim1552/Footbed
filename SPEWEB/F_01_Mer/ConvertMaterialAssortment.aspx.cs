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
using SPEENTITY;
using SPELIB;

namespace SPEWEB.F_01_Mer
{
    public partial class ConvertMaterialAssortment : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Convertable Materials Assortment ";
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.CommonButton();
                this.GetSesson();
                //Load_Project_Combo();
                load_OutsoleList();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFiUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }


        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
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
            
            DdlSeason_SelectedIndexChanged(null, null);
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_Combo();
        }

        private void lnkFiUpdate_Click(object sender, EventArgs e)
        {           
            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblsizemat"];
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            string ordqty = Convert.ToDouble("0" + this.ordqty.Text).ToString();
            string detailscod = this.ddlStyle.SelectedValue.ToString();
            bool result = false;
            foreach (DataRow dr2 in dt.Rows)
            {
                string mlccod = dr2["mlccod"].ToString();
                string dayid = dr2["dayid"].ToString();
                string procode = dr2["procode"].ToString();
                string compcode = dr2["compcode"].ToString();
                string prodcode = dr2["prodcode"].ToString();
                string colorid = dr2["colorid"].ToString();
                string rsircode = dr2["rsircode"].ToString();
                string spcfcode = dr2["spcfcode"].ToString();
                string rstdqty = dr2["rstdqty"].ToString();
                string sizeid = dr2["fgsize"].ToString();
                string ctype = dr2["ctype"].ToString();
                result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_COST_AND_BUDGET", "UPDATE_CONVERTABLE_MATERIAL_ASSORMENT", mlccod, dayid, prodcode, colorid, procode, compcode, rsircode, spcfcode, sizeid, rstdqty, ctype);

                if (result == false)
                {
                       ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);

                    return;
                }


            }
            if (result == true && ConstantInfo.LogStatus == true)
            {

                string eventtype = "Convertable Material Assortment";
                string eventdesc = "Update Convertable Material Assortment analysis, Details Code" + detailscod;
                string eventdesc2 = "Order No- " + orderno+", Order Qty:"+ ordqty;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }
            // this.GetRateQtyChangeMsg();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

          
        }
        protected void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblsizemat"];
            double orderval = Convert.ToDouble("0" + this.ordqty.Text.Trim());
            int index;

            DataTable tblsizes = (DataTable)ViewState["tblsizes"];
            for (int j = 0; j < this.gvSizeMat.Rows.Count; j++)
            {
                string compcode = ((Label)this.gvSizeMat.Rows[j].FindControl("SlblgvCompCod")).Text.Trim();
                string procode = ((Label)this.gvSizeMat.Rows[j].FindControl("SlblgvProcde")).Text.Trim();
                string sircode = ((Label)this.gvSizeMat.Rows[j].FindControl("SlblgvResCod")).Text.Trim();


                string fgsize = ((DropDownList)this.gvSizeMat.Rows[j].FindControl("DdlFGSize")).SelectedValue.Trim();


                DataRow[] dr = tblsizes.Select("sizeid = '" + fgsize + "'");

                //DataView dv = dt.DefaultView;
                //dv.RowFilter = "procode='" + procode + "' and compcode='" + compcode + "' and rsircode='" + sircode + "'";

                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvSizeMat.Rows[j].FindControl("Stxtgvreqty01")).Text.Trim()));


                index = (this.gvSizeMat.PageIndex) * this.gvSizeMat.PageSize + j;
                dt.Rows[index]["rstdqty"] = Qty;
                if (dt.Rows[index]["ctype"].ToString() == "IN")
                    {
                        dt.Rows[index]["fgsize"] = fgsize;
                    }
                
                

            }


            Session["tblsizemat"] = dt;


        }
        private void load_OutsoleList()
        {
            string comcod = this.GetCompCode();
            string filter1 = "%%";
            string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string  SearchInfo = "sircode like '04%'";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "MATCODELIST", "000000000000", curdate, "%", SearchInfo, "", "", "", "", "");

            this.ddlResList1.DataTextField = "resdesc1";
            this.ddlResList1.DataValueField = "rescod";
            this.ddlResList1.DataSource = ds1.Tables[0];
            this.ddlResList1.DataBind();
            ViewState["Material"] = ds1.Tables[0];
            //ViewState["tblSpcf"] = ds1.Tables[1];
            ddlResList1_SelectedIndexChanged(null, null);
        }
        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void Load_Project_Combo()
        {
            this.ddlOrderList.Items.Clear();
            string comcod = this.GetCompCode();
            string FindProject = "%";
  
            string season = DdlSeason.SelectedItem.Value == "00000" ? "%" : DdlSeason.SelectedItem.Value + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", FindProject, "%", season, "", "", "", "", "", "");
            //   DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "mlcdesc";
            this.ddlOrderList.DataValueField = "mlccod";
            this.ddlOrderList.DataSource = ds1.Tables[1];
            this.ddlOrderList.DataBind();
            ViewState["tblordstyle"] = ds1.Tables[0];

            this.ddlOrderList_SelectedIndexChanged(null, null);

        }
        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlOrderList.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            if (dt1 == null)
                return;
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode1");
            this.ddlStyle.DataTextField = "styledesc2";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();


            this.ddlStyle_SelectedIndexChanged(null, null);
        }
        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataSet result = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_WISE_INFO", mlccod, styleid, colorid, dayid, "", "", "", "", "");

            if (result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Not Found');", true);

            
                return;
            }
            this.BuyerName.Text = result.Tables[0].Rows[0]["buyername"].ToString();
            this.ordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");

        }
     
        protected void ImgbtnFindOrder_Click(object sender, EventArgs e)
        {
            Load_Project_Combo();
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

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
           
            string comcod = this.GetCompCode();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_ORDER_CONVERTABLE_MAT", orderno, styleid, colorid, dayid, "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.gvSizeMat.DataSource = null;
                this.gvSizeMat.DataBind();
                return;
            }
            string date = (dayid == "00000000") ? "01-Jan-1900" : Convert.ToDateTime(dayid.Substring(4,2)+"/"+ dayid.Substring(6,2)+"/"+dayid.Substring(0,4)).ToString("dd-MMM-yyyy");
            ds1.Tables[2].Rows.Add(this.GetCompCode(), "000000000000", "000000000000", "720000000000","Common", "000000000000", "0.00");

            ViewState["tblmat"] = ds1.Tables[0];
            ViewState["tblsizemat"] = ds1.Tables[1];
            ViewState["tblsizes"] = ds1.Tables[2];
            this.HypMainorder.NavigateUrl= "~/F_01_Mer/MerChanPrint?Type=OrderPrint&mlccod="+ orderno + "&date="+ date + "&styleid="+ styleid +"&printtype=PDF";
            this.HypBOM.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod="+ orderno + "&Ptype=import&dayid="+ dayid + "&sircode="+ styleid + colorid+ "&format=PDF";

            this.BindDropdown();
            this.Data_Bind();
        }
        private void BindDropdown()
        {
            //////////////////add material in dropdown
            DataTable dtmat = (DataTable)ViewState["tblmat"];

            if (dtmat.Rows.Count == 0)
            {
                return;
            }
            DataView dv = dtmat.DefaultView;

            this.ddlResList.DataTextField = "rsirdesc";
            this.ddlResList.DataValueField = "refcod";
            this.ddlResList.DataSource = dv.ToTable(true, "refcod", "rsirdesc");
            this.ddlResList.DataBind();

            ddlResList_SelectedIndexChanged(null, null);

            //// size  list
            DataTable tblsizes = (DataTable)ViewState["tblsizes"];

            if (tblsizes.Rows.Count == 0)
            {
                return;
            }

            this.ddlSizes.DataTextField = "sizedesc";
            this.ddlSizes.DataValueField = "sizeid";
            this.ddlSizes.DataSource = tblsizes;
            this.ddlSizes.DataBind();
        }

        private void Data_Bind()
        {
            //DataTable dt = (DataTable)ViewState["tblmerchand"];

            //if (dt.Rows.Count == 0)
            //{
            //    this.gvdetails.DataSource = null;
            //    this.gvdetails.DataBind();
            //    return;
            //}

            //this.gvdetails.DataSource = dt;
            //this.gvdetails.DataBind();

           

         

            //// size material list
            DataTable sizemat = ((DataTable)ViewState["tblsizemat"]).Copy();

            if (sizemat.Rows.Count > 0)
            {
                this.gvSizeMat.DataSource = sizemat;
                this.gvSizeMat.DataBind();
            }
            else
            {
                this.gvSizeMat.DataSource = null;
                this.gvSizeMat.DataBind();
            }

           

          
        }

        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblmat"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "refcod = '" + mResCode + "' or spcfcode = '000000000000'";
            DataTable dt = dv1.ToTable();

            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcode";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();

        }

        protected void lbtnAddMat_Click(object sender, EventArgs e)
        {
            string batchcode = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataTable dt = (DataTable)ViewState["tblsizemat"];
            DataTable dtmat = (DataTable)ViewState["tblmat"];

            foreach (ListItem item in ddlResSpcf.Items)
            {
                if (item.Selected)
                {
                    string spcfcod = item.Value.ToString();
                    string spcfdesc = item.Text.ToString();
                    string procode = this.ddlResList.SelectedValue.ToString().Substring(0, 12);
                    string compcode = this.ddlResList.SelectedValue.ToString().Substring(12, 12);
                    string sircode = this.ddlResList.SelectedValue.ToString().Substring(24, 12);
                    string sirdesc = this.ddlResList.SelectedItem.ToString();
                    string fgsizeid = this.ddlSizes.SelectedValue.ToString();
                    string fgsizedesc = this.ddlSizes.SelectedItem.ToString();
                   

                    DataRow[] dr3 = dt.Select("procode = '" + procode + "'  and compcode = '" + compcode + "' and rsircode = '" + sircode + "'  and spcfcode='" + spcfcod + "' and fgsize='" + fgsizeid + "'");
                    if (dr3.Length == 0)
                    {

                        DataRow[] dr = dtmat.Select("refcod = '" + procode + compcode + sircode + "' and spcfcode='" + spcfcod + "'");
                        if (dr.Length > 0)
                        {                       
                        DataRow dr1 = dt.NewRow();
                        dr1["mlccod"] = batchcode;
                        dr1["dayid"] = dayid;
                        dr1["prodcode"] = styleid;
                        dr1["colorid"] = colorid;
                        dr1["procode"] = procode;
                        dr1["compcode"] = compcode;
                        dr1["rsircode"] = sircode;
                        dr1["rsirdesc"] = sirdesc;
                        dr1["spcfcode"] = spcfcod;
                        dr1["spcfdesc"] = spcfdesc;                      
                        dr1["rstdqty"] = dr[0]["bomqty"].ToString();                        
                        dr1["rsirunit"] = dr[0]["rsirunit"].ToString();                      
                        dr1["fgsize"] = "000000000000";
                        dr1["fgsizedesc"] = "";
                        dr1["ctype"] = "OUT";
                            dt.Rows.Add(dr1);
                        }
                    }
                }
            }
            


            ViewState["tblsizemat"] = dt;
            this.Data_Bind();

        }
        protected void lbtnAddMatIn_Click(object sender, EventArgs e)
        {
            string batchcode = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataTable dt = (DataTable)ViewState["tblsizemat"];
            DataTable dtmat = (DataTable)ViewState["Material"];
            DataTable tblsizes = (DataTable)ViewState["tblsizes"];
            foreach (ListItem item in ddlResSpcf1.Items)
            {
                if (item.Selected)
                {
                    string spcfcod = item.Value.ToString();
                    string spcfdesc = item.Text.ToString();
                    string procode = this.ddlResList.SelectedValue.ToString().Substring(0, 12);
                    string compcode = this.ddlResList.SelectedValue.ToString().Substring(12, 12);
                    string sircode = this.ddlResList1.SelectedValue.ToString();
                    string sirdesc = this.ddlResList1.SelectedItem.ToString();
                    string fgsizeid = this.ddlSizes.SelectedValue.ToString();
                    string fgsizedesc = this.ddlSizes.SelectedItem.ToString();


                    DataRow[] dr3 = dt.Select("procode = '" + procode + "'  and compcode = '" + compcode + "' and rsircode = '" + sircode + "'  and spcfcode='" + spcfcod + "' and fgsize='" + fgsizeid + "'");
                    if (dr3.Length == 0)
                    {
                        DataRow[] drmat = dtmat.Select("rescod='" + sircode + "'"); 
                        DataRow[] dr = tblsizes.Select("mlccod = '" + batchcode + "' and refcod='" + styleid + "' and sizeid='" + fgsizeid + "'");
                        if (dr.Length > 0)
                        {
                            DataRow dr1 = dt.NewRow();
                            dr1["mlccod"] = batchcode;
                            dr1["dayid"] = dayid;
                            dr1["prodcode"] = styleid;
                            dr1["colorid"] = colorid;
                            dr1["procode"] = procode;
                            dr1["compcode"] = compcode;
                            dr1["rsircode"] = sircode;
                            dr1["rsirdesc"] = sirdesc;
                            dr1["spcfcode"] = spcfcod;
                            dr1["spcfdesc"] = spcfdesc;
                            dr1["rstdqty"] = dr[0]["ordrqty"].ToString();
                            dr1["rsirunit"] = drmat[0]["rsirunit"].ToString();
                            dr1["fgsize"] = fgsizeid;
                            dr1["fgsizedesc"] = fgsizedesc;
                            dr1["ctype"] = "IN";
                            dt.Rows.Add(dr1);
                        }
                    }
                }
            }



            ViewState["tblsizemat"] = dt;
            this.Data_Bind();

        }
        protected void lbtnAddMatALL_Click(object sender, EventArgs e)
        {
            string batchcode = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataTable dt = (DataTable)ViewState["tblsizemat"];
            DataTable dtmat = (DataTable)ViewState["Material"];
            DataTable tblsizes = (DataTable)ViewState["tblsizes"];
            foreach (ListItem item in ddlResSpcf1.Items)
            {
                if (item.Selected)
                {
                    string spcfcod = item.Value.ToString();
                    string spcfdesc = item.Text.ToString();
                    string procode = this.ddlResList.SelectedValue.ToString().Substring(0, 12);
                    string compcode = this.ddlResList.SelectedValue.ToString().Substring(12, 12);
                    string sircode = this.ddlResList1.SelectedValue.ToString();
                    string sirdesc = this.ddlResList1.SelectedItem.ToString();
                    string fgsizeid = this.ddlSizes.SelectedValue.ToString();
                    string fgsizedesc = this.ddlSizes.SelectedItem.ToString();
                    DataTable tblsizes2 = (DataTable)ViewState["tblsizes"];
                    for (int i=0; i < tblsizes2.Rows.Count; i++)
                    {
                        DataRow[] dr3 = dt.Select("procode = '" + procode + "'  and compcode = '" + compcode + "' and rsircode = '" + sircode + "'  and spcfcode='" + spcfcod + "' and fgsize='" + tblsizes2.Rows[i]["sizeid"].ToString() + "'");
                        if (dr3.Length == 0)
                        {
                            DataRow[] drmat = dtmat.Select("rescod='" + sircode + "'");
                            DataRow[] dr = tblsizes.Select("mlccod = '" + batchcode + "' and refcod='" + styleid + "' and sizeid='" + tblsizes2.Rows[i]["sizeid"].ToString() + "'");
                            if (dr.Length > 0)
                            {
                                DataRow dr1 = dt.NewRow();
                                dr1["mlccod"] = batchcode;
                                dr1["dayid"] = dayid;
                                dr1["prodcode"] = styleid;
                                dr1["colorid"] = colorid;
                                dr1["procode"] = procode;
                                dr1["compcode"] = compcode;
                                dr1["rsircode"] = sircode;
                                dr1["rsirdesc"] = sirdesc;
                                dr1["spcfcode"] = spcfcod;
                                dr1["spcfdesc"] = spcfdesc;
                                dr1["rstdqty"] = dr[0]["ordrqty"].ToString();
                                dr1["rsirunit"] = drmat[0]["rsirunit"].ToString();
                                dr1["fgsize"] = tblsizes2.Rows[i]["sizeid"].ToString();
                                dr1["fgsizedesc"] = fgsizedesc;
                                dr1["ctype"] = "IN";
                                dt.Rows.Add(dr1);
                            }
                        }
                    }
                }
            }



            ViewState["tblsizemat"] = dt;
            this.Data_Bind();

        }
        protected void gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void gvSizeMat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tblsizes = (DataTable)ViewState["tblsizes"];

            if (tblsizes.Rows.Count == 0)
            {
                return;
            }
                   
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList DdlFGSize = (DropDownList)e.Row.FindControl("DdlFGSize");

                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ctype")) == "IN")
                {
                    e.Row.BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.ForeColor = System.Drawing.Color.White;

                    DdlFGSize.DataTextField = "sizedesc";
                    DdlFGSize.DataValueField = "sizeid";
                    DdlFGSize.DataSource = tblsizes;
                    DdlFGSize.DataBind();
                    DdlFGSize.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fgsize"));
                }
                else
                {
                    DdlFGSize.Visible = false;
                    //(e.Row.Cells[1].Controls[0] as Button).Visible = false;
                    e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
                }
            }
        }

        protected void LbtnRevisedZero_Click(object sender, EventArgs e)
        {
            //string comcod = GetCompCode();
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;
            //string mlccod = this.ddlOrderList.SelectedValue.ToString();
            //string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            //string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            //string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            //string compcode = ((Label)this.gvdetails.Rows[index].FindControl("lblgvCompCod")).Text.ToString();
            //string rsircode = ((Label)this.gvdetails.Rows[index].FindControl("lblgvResCod")).Text.ToString();
            //string fgsize = ((Label)this.gvdetails.Rows[index].FindControl("LblFgSizes")).Text.ToString();            
            //string spcfcod = ((Label)this.gvdetails.Rows[index].FindControl("lblgvSpcfCod")).Text.ToString();
            //bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "UPDATE_BOM_QTY_REVISED", mlccod, styleid,colorid,dayid, compcode, rsircode, spcfcod, fgsize, "", "");
            //if (result)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Revised successfully');", true);

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong');", true);

            //}

        }

        protected void gvSizeMat_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblsizemat"];
            string comcod = this.GetCompCode();
            int rowindex = (this.gvSizeMat.PageSize) * (this.gvSizeMat.PageIndex) + e.RowIndex;
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string procode = dt.Rows[rowindex]["procode"].ToString();
            string compcode = dt.Rows[rowindex]["compcode"].ToString();
            string rsircode = dt.Rows[rowindex]["rsircode"].ToString();
            string fgsize = dt.Rows[rowindex]["fgsize"].ToString();
            string spcfcod = dt.Rows[rowindex]["spcfcode"].ToString();
            string ctype = dt.Rows[rowindex]["ctype"].ToString();
            if (ctype != "OUT")
            {
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "REMOVE_ITEMS_FROM_CONV_MATERIAL_ASSORTMENT", mlccod, styleid, colorid, dayid, procode, compcode, rsircode, spcfcod, fgsize, "", "");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Removed successfully');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong');", true);

                }
                dt.Rows[rowindex].Delete();
                ViewState["tblsizemat"] = dt;
                this.Data_Bind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('TPR Not eligible for delete');", true);
            }
            
            
            
        }

        protected void ddlResList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mResCode = this.ddlResList1.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "SIRINF_MAT_SPCF_LIST", mResCode, "", "", "", "");

            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Specification Found For This Item');", true);
                return;
            }

            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Specification Found For This Item');", true);
                return;
            }

            this.ddlResSpcf1.Items.Clear();
            ViewState["tblSpcf"] = ds1.Tables[0];

            this.ddlResSpcf1.DataTextField = "spcfdesc";
            this.ddlResSpcf1.DataValueField = "spcfcod";
            this.ddlResSpcf1.DataSource = ds1.Tables[0];
            this.ddlResSpcf1.DataBind();
        }

    }
}