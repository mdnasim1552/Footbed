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
    public partial class ConsumptionSizeAdd : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Size Wise Material Assortment ";
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.CommonButton();
                this.GetSesson();
                //this.Load_Project_Combo();
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

            this.ddlSeason.DataTextField = "gdesc";
            this.ddlSeason.DataValueField = "gcod";
            this.ddlSeason.DataSource = ds1.Tables[0];
            this.ddlSeason.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();

            this.ddlSeason.SelectedValue = season;

            this.Load_Project_Combo();
            //DdlSeason_SelectedIndexChanged(null, null);
        }


        private void lnkFiUpdate_Click(object sender, EventArgs e)
        {
           
            string comcod = this.GetCompCode();
            this.SaveValue();

            DataTable dt = (DataTable)ViewState["tblsizemat"];
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            string ordqty = Convert.ToDouble("0" + this.ordqty.Text).ToString();
            string detailscod = this.ddlStyle.SelectedValue.ToString();
            //string remarks = ((Label)this.gvSizeMat.FindControl("Stxtgvrmrks")).Text.Trim();

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
                string stdamt = dr2["stdamt"].ToString();
                string oldqty = dr2["oldqty"].ToString();
                string bomqty = dr2["bomqty"].ToString();
                string bomamt = dr2["bomamt"].ToString();
                string rate = dr2["rate"].ToString();
                string sizeid = dr2["fgsize"].ToString();
                string remarks = dr2["remarks"].ToString();


                result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_MER_PROANALYSIS", "UPDATE_SIZE_WISE_MATERIAL_ASSORMENT", mlccod, dayid, prodcode, colorid, procode, compcode, rsircode, spcfcode, rstdqty, stdamt, oldqty, bomqty, bomamt, rate, sizeid, ordqty, remarks);

                if (result == false)
                {
                       ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                    return;
                }


            }
            if (result == true && ConstantInfo.LogStatus == true)
            {

                string eventtype = "Size wise Material Assortment";
                string eventdesc = "Update Size wise Material Assortment , Details Code" + detailscod;
                string eventdesc2 = "Order No- " + orderno;
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
                string remarks = ((TextBox)this.gvSizeMat.Rows[j].FindControl("Stxtgvrmrks")).Text.Trim();


                DataRow[] dr = tblsizes.Select("sizeid = '" + fgsize + "'");

                DataView dv = dt.DefaultView;
                dv.RowFilter = "procode='" + procode + "' and compcode='" + compcode + "' and rsircode='" + sircode + "'";
                double sum = Convert.ToDouble("0" + dv.ToTable().Compute("Sum(rstdqty)", String.Empty));
                double oldactqty = Convert.ToDouble(dv.ToTable().Rows[0]["oldqty"]);
                string strQty = Convert.ToDouble(((TextBox)this.gvSizeMat.Rows[j].FindControl("Stxtgvreqty01")).Text.Trim()=="" ? "0.00" : ((TextBox)this.gvSizeMat.Rows[j].FindControl("Stxtgvreqty01")).Text.Trim()).ToString("#,##0.000000;(#,##0.000000); ");
                string strrate = Convert.ToDouble(((TextBox)this.gvSizeMat.Rows[j].FindControl("Stxtgvrate")).Text.Trim() == "" ? "0.00" : ((TextBox)this.gvSizeMat.Rows[j].FindControl("Stxtgvrate")).Text.Trim()).ToString("#,##0.000000;(#,##0.000000); ");
                string strbomqty = Convert.ToDouble(((TextBox)this.gvSizeMat.Rows[j].FindControl("StxtgvBomqty")).Text.Trim() == "" ? "0.00" : ((TextBox)this.gvSizeMat.Rows[j].FindControl("StxtgvBomqty")).Text.Trim()).ToString("#,##0.000000;(#,##0.000000); ");
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + strQty));
                double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + strrate));
                double bomqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + strbomqty));

                if (bomqty == 0)
                {
                    if (fgsize == "000000000000")
                    {
                        bomqty = orderval * Qty;
                    }
                    else
                    {
                        bomqty =Convert.ToDouble(dr[0]["ordrqty"]) * Qty;
                    }
                   
                }


                //if (Qty + sum > oldactqty)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You Given exceed value');", true);

                //      // return;
                //}
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                }
                index = (this.gvSizeMat.PageIndex) * this.gvSizeMat.PageSize + j;
                dt.Rows[index]["rstdqty"] = Qty;
                dt.Rows[index]["rate"] = rate;
                dt.Rows[index]["stdamt"] = Qty * rate;
                dt.Rows[index]["bomqty"] = Math.Ceiling(bomqty);
                dt.Rows[index]["bomamt"] = Math.Ceiling(bomqty) * rate;
                dt.Rows[index]["fgsize"] = fgsize;
                dt.Rows[index]["remarks"] = remarks;

            }


            Session["tblsizemat"] = dt;


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
            string season = this.ddlSeason.SelectedValue == "00000" ? "%" : this.ddlSeason.SelectedValue+"%";
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
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.Multiview.ActiveViewIndex = 0;
                    break;
                case "1":
                    this.Multiview.ActiveViewIndex = 1;
                    break;
                default:
                    this.Multiview.ActiveViewIndex = 0;
                    break;
            }
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
            ViewState.Remove("tblmerchand");
            string comcod = this.GetCompCode();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_ORDER_CON_VS_BOM_WITH_SIZEBLE_MAT", orderno, styleid, colorid, dayid, "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.gvdetails.DataSource = null;
                this.gvdetails.DataBind();
                return;
            }
            string date = (dayid == "00000000") ? "01-Jan-1900" : Convert.ToDateTime(dayid.Substring(4,2)+"/"+ dayid.Substring(6,2)+"/"+dayid.Substring(0,4)).ToString("dd-MMM-yyyy");
            ViewState["tblmerchand"] = ds1.Tables[0];
            ViewState["tblmat"] = ds1.Tables[1];
            ViewState["tblsizemat"] = ds1.Tables[2];
            ViewState["tblsizes"] = ds1.Tables[3];
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
            DataTable dt = (DataTable)ViewState["tblmerchand"];

            if (dt.Rows.Count == 0)
            {
                this.gvdetails.DataSource = null;
                this.gvdetails.DataBind();
                return;
            }

            this.gvdetails.DataSource = dt;
            this.gvdetails.DataBind();

           

         

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
            dv1.RowFilter = "refcod = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();

            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
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

                        DataRow[] dr = dtmat.Select("refcod = '" + procode + compcode + sircode + "' and spcfcod='" + spcfcod + "'");
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
                        dr1["oldqty"] = dr[0]["rstdqty"].ToString();
                        dr1["rstdqty"] = dr[0]["rstdqty"].ToString();
                        dr1["stdamt"] = 0.00;
                        dr1["rate"] = dr[0]["rate"].ToString();
                        dr1["rsirunit"] = dr[0]["rsirunit"].ToString();
                        dr1["bomqty"] = 0.00;
                        dr1["bomamt"] = 0.00;
                        dr1["fgsize"] = fgsizeid;
                        dr1["fgsizedesc"] = fgsizedesc;
                        dr1["remarks"] = "";
                        dt.Rows.Add(dr1);
                        }
                    }
                }
            }
            


            ViewState["tblsizemat"] = dt;
            this.Data_Bind();

        }


        protected void gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool sizeble = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "sizeble"));
                double bomqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bomqty"));
                LinkButton LbtnRevisedZero = (LinkButton)e.Row.FindControl("LbtnRevisedZero");

                
                if (sizeble == true)
                {
                    LbtnRevisedZero.Visible = true;
                    e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                }
                if (bomqty == 0)
                {
                    
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }



            }
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
                DdlFGSize.DataTextField = "sizedesc";
                DdlFGSize.DataValueField = "sizeid";
                DdlFGSize.DataSource = tblsizes;
                DdlFGSize.DataBind();
                DdlFGSize.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fgsize"));

            }
        }

        protected void LbtnRevisedZero_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string compcode = ((Label)this.gvdetails.Rows[index].FindControl("lblgvCompCod")).Text.ToString();
            string rsircode = ((Label)this.gvdetails.Rows[index].FindControl("lblgvResCod")).Text.ToString();
            string fgsize = ((Label)this.gvdetails.Rows[index].FindControl("LblFgSizes")).Text.ToString();            
            string spcfcod = ((Label)this.gvdetails.Rows[index].FindControl("lblgvSpcfCod")).Text.ToString();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "UPDATE_BOM_QTY_REVISED", mlccod, styleid,colorid,dayid, compcode, rsircode, spcfcod, fgsize, "", "");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Revised successfully');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong');", true);

            }

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
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "REMOVE_ITEMS_FROM_MATERIAL_ASSORTMENT", mlccod, styleid, colorid, dayid, procode,compcode, rsircode, spcfcod, fgsize, "", "");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Removed successfully');", true);
                dt.Rows[rowindex].Delete();
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "mlccod <> '' and comcod <> ''";
                ViewState["tblsizemat"] = dv1.ToTable();
                this.Data_Bind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong');", true);

            }
            
        }

        protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_Combo();
        }
    }
}