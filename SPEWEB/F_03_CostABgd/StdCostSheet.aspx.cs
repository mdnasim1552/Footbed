using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SPELIB;

namespace SPEWEB.F_03_CostABgd
{
    public partial class StdCostSheet : System.Web.UI.Page
    {
        //ProcessAccess proc1 =    new ProcessAccess("ASITMFCTURE");
        ProcessAccess proc1 = new ProcessAccess();
        static string prevPage = String.Empty;
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
                this.GetAccCode();
                this.GetMatList();
                this.SelectMatList();
                ((Label)this.Master.FindControl("lblTitle")).Text = " Standard Cost Sheet ";
                //this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                if (this.Request.QueryString["InputType"].ToString() == "CostAnnaQty" || this.Request.QueryString["InputType"].ToString() == "CostAnnaSemiQty")
                {
                    ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                    //rbtnList1.Items.Remove("Report");
                    this.gvCost.Columns[7].Visible = false;
                    this.gvCost.Columns[8].Visible = false;

                    this.gvRptCost.Columns[5].Visible = false;
                    this.gvRptCost.Columns[6].Visible = false;
                    this.gvRptCost.Columns[7].Visible = false;
                    this.gvRptCost.Columns[8].Visible = false;
                    this.gvRptCost.Columns[9].Visible = false;
                    this.gvRptCost.Columns[10].Visible = false;
                    this.gvRptCost.Columns[11].Visible = false;

                }
                this.CommonButton();

                
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;lnkbtnNew
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
        }

        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Image2.Visible = false;
            //this.Image3.Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int index = this.rbtnList1.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.imgbtnsearchres_Click(null, null);
                    this.revenueinfo();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case 1:
                    this.imgbtnsearchProcess_Click(null, null);
                    this.imgbtnResourceCost_Click(null, null);
                    this.ShowCost();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case 2:

                    this.RptCost();
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

            }
        }

        protected void lbtnRecalculate_Click(object sender, EventArgs e)
        {
            string type = this.rbtnList1.SelectedIndex.ToString();
            switch (type)
            {
                case "0":
                    this.lnkCalculat_Click(null, null);
                    break;
                case "1":
                    this.lnkTotalCost_Click(null, null);
                    break;
            }

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            string type = this.rbtnList1.SelectedIndex.ToString();
            switch (type)
            {
                case "0":
                    this.lnkUpdateRev_Click(null,null);
                    break;
                case "1":
                    this.lnkUpdateCost_Click(null, null);
                    break;
            }

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkOpen_Click(object sender, EventArgs e)
        {
            if (ASTUtility.Left(this.GetComeCode(), 2) == "61")
            {
                return;
            }

            if (this.lnkOpen.Text == "Ok")
            {
                this.lnkOpen.Text = "New";
                this.ddlcatagory.Enabled = false;
                this.ddlprocode.Enabled = false;
                this.Panel2.Visible = true;
                if (this.Request.QueryString["InputType"].ToString().Trim() == "CostAnnaRpt")
                {
                    this.Panel2.Visible = false;
                    this.RptCost();
                    this.MultiView1.ActiveViewIndex = 2;
                }
                else
                {
                    this.Panel2.Visible = true;
                    this.rbtnList1.SelectedIndex = 2;
                    this.rbtnList1_SelectedIndexChanged(null, null);
                }
                return;
            }

            
            this.lnkOpen.Text = "Ok";
            
            // this.txtdate.ReadOnly = false;
            //this.Image2.Visible = false;
            //this.Image3.Visible = false;
            this.ddlprocode.Enabled = true;
            this.Panel2.Visible = false;
            this.rbtnList1.SelectedIndex = -1;
            this.MultiView1.ActiveViewIndex = -1;
            this.gvrevenue.DataSource = null;
            this.gvrevenue.DataBind();


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (ASTUtility.Left(this.GetComeCode(), 2) == "61")
            {
                return;
            }
            this.printcstsheetAll();
        }

        private void printcstsheetAll()
        {

            //int intpro = this.lblProDesc.Text.Trim().Length;
            //string product = this.lblProDesc.Text.Trim().Substring(14, intpro - 14);

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblstdcost"];

            //string ItmCod = this.ddlprocode.SelectedValue.ToString().Trim();
            //DataRow[] dr1 = ((DataTable)Session["tblItmCod"]).Select("rescode='" + ItmCod + "'");

            //var lst = dt.DataTableToList<MFGOBJ.C_03_StdCost.ProBudget.StdCostSheetAll>();

            //string prodesc = lst[0].prodesc.ToString();
            //string Packno = dr1[0]["desc2"].ToString().Trim();
            //string Batchno = dr1[0]["desc1"].ToString().Trim();
            //double Tprate = Convert.ToDouble(lst[0].rate);
            //string Qty = Convert.ToDouble(dr1[0]["qty"]).ToString("#,##0;(#,##0); ");


            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //LocalReport rptscs = new LocalReport();
            //rptscs = MFGRPTRDLC.RptSetupClass1.GetLocalReport("RD_03_StdCost.StandardCostSheetAll", lst, null, null);

            //rptscs.EnableExternalImages = true;
            //rptscs.SetParameters(new ReportParameter("comnam", comnam));
            //rptscs.SetParameters(new ReportParameter("RptTitle", "Cost Sheet"));
            //rptscs.SetParameters(new ReportParameter("prodesc", product));
            //rptscs.SetParameters(new ReportParameter("Packno", Packno));
            //rptscs.SetParameters(new ReportParameter("Batchno", Batchno));
            //rptscs.SetParameters(new ReportParameter("Tprate", Tprate.ToString("#,##0.00; (#,##0.00); ")));
            //rptscs.SetParameters(new ReportParameter("qty", Qty));
            //rptscs.SetParameters(new ReportParameter("ComLogo", ComLogo));



            //Session["Report1"] = rptscs;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void printCstSheet()
        {
            //DataTable dtc = (DataTable)Session["tblstdcost"];
            //string qty = Convert.ToDouble(dtc.Rows[0]["qty"]).ToString("#,##0;(#,##0); ");
            //Convert.ToDouble(dtc.Rows[0]["qty"]).ToString("#,##0.00;(#,##0.00); ");
            //int intpro = this.lblProDesc.Text.Trim().Length;
            //string product = this.lblProDesc.Text.Trim().Substring(14, intpro-14);

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt=(DataTable)Session["tblstdcost"];



            //ReportDocument rptscs = new ReportDocument();
            //if (this.ddlPrint.SelectedIndex == 0)
            //{
            //    rptscs = new MFGRPT.R_03_StdCost.rptStdCost();
            //}
            //else
            //{
            //    rptscs = new MFGRPT.R_03_StdCost.rptStdCostAll();
            //}

            //TextObject rptCompName = rptscs.ReportDefinition.ReportObjects["compName"] as TextObject;
            //rptCompName.Text = comnam;

            //string ItmCod = this.ddlprocode.SelectedValue.ToString().Trim();
            //DataRow[] dr1 = ((DataTable)Session["tblItmCod"]).Select("rescode='" + ItmCod + "'");

            //TextObject txtBatchno = rptscs.ReportDefinition.ReportObjects["txtPack"] as TextObject;
            //txtBatchno.Text = dr1[0]["desc2"].ToString().Trim();

            //TextObject txtPack = rptscs.ReportDefinition.ReportObjects["txtBatchno"] as TextObject;
            //txtPack.Text = dr1[0]["desc1"].ToString().Trim();

            //TextObject txtTP = rptscs.ReportDefinition.ReportObjects["txtTP"] as TextObject;
            //txtTP.Text = Convert.ToDouble(dt.Rows[0]["Rate"]).ToString("#,##0.00;(#,##0.00); ");

            //TextObject txtStQty = rptscs.ReportDefinition.ReportObjects["txtStQty"] as TextObject;
            //txtStQty.Text = Convert.ToDouble(dr1[0]["qty"]).ToString("#,##0;(#,##0); ");


            //this.lblQty.Text = "Qty. :" + Convert.ToDouble(dr1[0]["qty"]).ToString("#,##0.") + "  ";
            //this.txtDesc.Text = ((dr1[0]["desc1"].ToString().Trim()).Length == 0) ? "" : dr1[0]["mdesc1"].ToString().Trim() + " :" + dr1[0]["desc1"].ToString().Trim() + " , " + dr1[0]["mdesc2"].ToString().Trim() + " :" + dr1[0]["desc2"].ToString().Trim();
            //this.lblUnit.Text = "Unit : " + dr1[0]["resunit"].ToString().Trim();





            //TextObject BatchNo = rptscs.ReportDefinition.ReportObjects["TxtProduct"] as TextObject;
            //BatchNo.Text = product;  //+ ASTUtility.Right(ddlprocode.Text.Trim(), 2);
            //TextObject ConQty = rptscs.ReportDefinition.ReportObjects["TxtQty"] as TextObject;
            //ConQty.Text = qty;
            //rptscs.SetDataSource((DataTable)Session["tblstdcost"]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptscs.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptscs;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void imgbtnsearchres_Click(object sender, EventArgs e)
        {
            Session.Remove("tblres");
            string comcod = this.GetComeCode();
            string Type = (this.Request.QueryString["InputType"] == "CostAnnaSemi") ? "Semi" : "";
            string filter =  "%";
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "GETBIPRODUCTCODE", filter, Type, "", "", "", "", "", "", "");
            this.ddlProduct.DataSource = ds3.Tables[0];
            this.ddlProduct.DataTextField = "resdesc";
            this.ddlProduct.DataValueField = "rescode";
            this.ddlProduct.DataBind();
            Session["tblres"] = ds3.Tables[0];
            ds3.Dispose();

        }
        protected void imgbtnsearch_Click(object sender, EventArgs e)
        {
            if (ASTUtility.Left(this.GetComeCode(), 2) == "61")
            {
                return;
            }
            this.GetAccCode();
        }

        private void revenueinfo()
        {
            Session.Remove("tblstdcost");
            string comcod = this.GetComeCode();
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            string processcode = "000000000000";
            DataSet ds4 = proc1.GetTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "SHOWSTDCOSTINFO", prodcode, processcode, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvrevenue.DataSource = null;
                this.gvrevenue.DataBind();
                return;
            }


            DataTable dt = ds4.Tables[0];
            Session["tblstdcost"] = dt;

            // this.txtdate.Text =(dt.Rows.Count>0)? Convert.ToDateTime(ds4.Tables[0].Rows[0]["stddate"]).ToString("dd-MMM-yyyy"):System.DateTime.Today.ToString("dd-MMM-yyyy");
            // this.txtdate.ReadOnly = true;
            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.addmainpro();
            }
            this.Data_Bind();
        }

        private void ShowCost()
        {
            Session.Remove("tblstdcost");
            string comcod = this.GetComeCode();
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            string processcode = this.ddlProcess.SelectedValue.ToString();

            DataSet ds4 = proc1.GetTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "SHOWSTDCOSTINFO", prodcode, processcode, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvCost.DataSource = null;
                this.gvCost.DataBind();
                return;
            }
            Session["tblstdcost"] = ds4.Tables[0];
            this.Data_Bind();
        }

        private void RptCost()
        {
            Session.Remove("tblstdcost");
            string comcod = this.GetComeCode();
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            DataSet ds4 = proc1.GetTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "RPTSTDCOSTSHEET", prodcode, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {

                this.gvRptCost.DataSource = null;
                this.gvRptCost.DataBind();
                return;
            }
            Session["tblstdcost"] = HiddenSameData(ds4.Tables[0]);
            //this.lbltxtvalueProfit.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["profit"]).ToString("#,##0.00;(#,##0.00); ") + (Convert.ToDouble(ds4.Tables[1].Rows[0]["profit"])>0? "%": "");
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            string grp = dt1.Rows[0]["grp"].ToString();
            string procode = dt1.Rows[0]["procode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["procode"].ToString() == procode)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["prodesc"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["procode"].ToString() == procode)
                    {

                        dt1.Rows[j]["prodesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        grp = dt1.Rows[j]["grp"].ToString();
                        dt1.Rows[j]["grpdesc"] = "";
                    }


                    procode = dt1.Rows[j]["procode"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();

                }

            }
            return dt1;

        }

        protected void lnkAddTable_Click(object sender, EventArgs e)
        {

            DataTable tbl2 = (DataTable)Session["tblstdcost"];
            string processcode = "000000000000";
            string rescod = this.ddlProduct.SelectedValue.ToString();

            DataRow[] dr = tbl2.Select("procode='" + processcode + "' and  rescode='" + rescod + "'");

            if (dr.Length > 0)
            {
                return;
            }
            else
            {
                DataRow dr1 = tbl2.NewRow();
                dr1["procode"] = processcode;
                dr1["rescode"] = rescod;
                dr1["resdesc"] = this.ddlProduct.SelectedItem.ToString().Substring(13);
                dr1["resunit"] = (((DataTable)Session["tblres"]).Select("rescode='" + rescod + "'"))[0]["resunit"].ToString();
                dr1["qty"] = 0;
                dr1["rate"] = (((DataTable)Session["tblres"]).Select("rescode='" + rescod + "'"))[0]["rate"].ToString();
                dr1["amt"] = 0;
                dr1["percnt"] = 0;
                tbl2.Rows.Add(dr1);
            }
            Session["tblstdcost"] = tbl2;
            this.Data_Bind();
        }

        protected void lnkCalculat_Click(object sender, EventArgs e)
        {
            this.Calculation();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblstdcost"];
            int index = this.rbtnList1.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.gvrevenue.DataSource = dt;
                    this.gvrevenue.DataBind();
                    this.FooterCalculation(dt);
                    break;

                case 1:
                    this.gvCost.DataSource = dt;
                    this.gvCost.DataBind();
                    this.FooterCalculation(dt);
                    break;


                case 2:
                    this.gvRptCost.DataSource = dt;
                    this.gvRptCost.DataBind();
                    break;
            }
            if (this.Request.QueryString["InputType"].ToString().Trim() == "CostAnnaRpt")
            {
                this.gvRptCost.DataSource = dt;
                this.gvRptCost.DataBind();
            }

        }

        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;

            int index = this.rbtnList1.SelectedIndex;
            switch (index)
            {
                case 0:

                    ((Label)this.gvrevenue.FooterRow.FindControl("lblgvfamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvrevenue.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
                    //((TextBox)this.gvrevenue.Rows[0].FindControl("txtgvqty")).ReadOnly = true;
                    break;

                case 1:
                    ((Label)this.gvCost.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvCost.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");

                    break;
            }

        }

        private void Calculation()
        {
            DataTable dt = (DataTable)Session["tblstdcost"];
            double qty, rate, amt;
            for (int i = 0; i < this.gvrevenue.Rows.Count; i++)
            {
                qty = Convert.ToDouble("0" + ((TextBox)this.gvrevenue.Rows[i].FindControl("txtgvqty")).Text.Trim());
                rate = Convert.ToDouble("0" + ((TextBox)this.gvrevenue.Rows[i].FindControl("txtgvqrate")).Text.Trim());
                amt = qty * rate;
                dt.Rows[i]["qty"] = qty;
                dt.Rows[i]["rate"] = rate;
                dt.Rows[i]["amt"] = amt;
            }

            double netamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["percnt"] = (netamt == 0) ? 0.00 : (Convert.ToDouble(dt.Rows[i]["amt"].ToString()) * 100) / netamt;

            }

            this.Data_Bind();

        }

        protected void lnkUpdateRev_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblstdcost"];
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            string proscode = "000000000000";
            //string date = this.txtdate.Text.Trim();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string rescod = dt.Rows[i]["rescode"].ToString();
                string spcfcode = dt.Rows[i]["spcfcod"].ToString();
                string resqty = dt.Rows[i]["qty"].ToString();
                string resamt = dt.Rows[i]["amt"].ToString();
                string percnt = dt.Rows[i]["percnt"].ToString();
                bool result = proc1.UpdateTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "SCOSTUPDATE", prodcode, proscode, rescod, resqty, resamt, percnt, spcfcode, "0", "0", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + proc1.ErrorObject["Msg"].ToString() + "');", true);


                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
            }

        }

        protected void imgbtnsearchProcess_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string filter =  "%";
            string Process = (this.Request.QueryString["InputType"] == "CostAnna" || this.Request.QueryString["InputType"] == "CostAnnaQty") ? "41%" : "04%";
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "GETPROCESSCODE", filter, Process, "", "", "", "", "", "", "");
            this.ddlProcess.DataSource = ds3.Tables[0];
            this.ddlProcess.DataTextField = "resdesc";
            this.ddlProcess.DataValueField = "rescode";
            this.ddlProcess.DataBind();
            ViewState["tblcodeType"] = ds3.Tables[0];
            ds3.Dispose();
            this.imgbtnResourceCost_Click(null, null);
        }

        protected void imgbtnResourceCost_Click(object sender, EventArgs e)
        {
            Session.Remove("tblres");
            string comcod = this.GetComeCode();
            string filter =  "%";

            string rescode = this.ddlProcess.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblcodeType"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rescode='" + rescode + "'");
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

                        SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }


            SearchInfo = (SearchInfo.Length == 0) ? "left(sircode, 2) between 01 and 06 " : SearchInfo;




            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "GETRESCODE_02", SearchInfo, "", "", "", "", "", "", "", "");
            this.ddlResourcesCost.DataSource = ds3.Tables[0];
            this.ddlResourcesCost.DataTextField = "resdesc";
            this.ddlResourcesCost.DataValueField = "rescode";
            this.ddlResourcesCost.DataBind();
            ViewState["tblresRes"] = ds3.Tables[0];
            ViewState["tblSpcf"] = ds3.Tables[1];
            ds3.Dispose();
            this.ddlResourcesCost_SelectedIndexChanged(null, null);

        }

        protected void ddlResourcesCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mResCode = this.ddlResourcesCost.SelectedValue.ToString();
            this.ddlSpcfcode.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();

            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }


            this.ddlSpcfcode.DataTextField = "spcfdesc";
            this.ddlSpcfcode.DataValueField = "spcfcod";
            this.ddlSpcfcode.DataSource = dt;
            this.ddlSpcfcode.DataBind();
        }

        protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnResourceCost_Click(null, null);
            //this.Image2.Visible = false;
            //this.Image3.Visible = false;
            
            this.ShowCost();

        }

        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {
            DataTable tbl2 = (DataTable)Session["tblstdcost"];
            string processcode = this.ddlProcess.SelectedValue.ToString();
            string rescod = this.ddlResourcesCost.SelectedValue.ToString();
            string Specification = this.ddlSpcfcode.SelectedValue.ToString();

            DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and spcfcod='" + Specification + "'");
            if (dr.Length > 0)
            {
                return;
            }
            else
            {
                DataRow dr1 = tbl2.NewRow();
                dr1["procode"] = processcode;
                dr1["rescode"] = rescod;
                dr1["resdesc"] = this.ddlResourcesCost.SelectedItem.ToString().Substring(13);
                dr1["spcfcod"] = this.ddlSpcfcode.SelectedValue.ToString();
                dr1["spcfdesc"] = this.ddlSpcfcode.SelectedItem.Text.Trim();
                dr1["resunit"] = (((DataTable)ViewState["tblresRes"]).Select("rescode='" + rescod + "'"))[0]["resunit"].ToString();
                dr1["conqty"] = 0;
                dr1["westpc"] = (((DataTable)ViewState["tblresRes"]).Select("rescode='" + rescod + "'"))[0]["allowance"].ToString();
                dr1["qty"] = 0;
                dr1["rate"] = (((DataTable)ViewState["tblresRes"]).Select("rescode='" + rescod + "'"))[0]["rate"].ToString();
                dr1["amt"] = 0;
                dr1["percnt"] = 0;
                tbl2.Rows.Add(dr1);
            }
            Session["tblstdcost"] = tbl2;
            this.Data_Bind();
        }
        protected void lnkAddAll_Click(object sender, EventArgs e)
        {
            string processcode = this.ddlProcess.SelectedValue.ToString();
            string rescode = this.ddlResourcesCost.SelectedValue.ToString();
            string Specification = this.ddlSpcfcode.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tblstdcost"];
            DataTable tbl2 = (DataTable)ViewState["tblresRes"];
            DataRow[] dr = tbl1.Select("rescode='" + rescode + "' and spcfcod='" + Specification + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["procode"] = processcode;
                    dr1["rescode"] = tbl2.Rows[i]["rescode"].ToString();
                    dr1["resdesc"] = tbl2.Rows[i]["resdesc"].ToString();
                    dr1["spcfcod"] = this.ddlSpcfcode.SelectedValue.ToString();
                    dr1["spcfdesc"] = this.ddlSpcfcode.SelectedItem.ToString();
                    dr1["resunit"] = tbl2.Rows[i]["resunit"].ToString();
                    dr1["conqty"] = 0;
                    dr1["westpc"] = 0;
                    dr1["qty"] = 0.00;
                    dr1["rate"] = tbl2.Rows[i]["rate"].ToString();
                    dr1["amt"] = 0.00;
                    tbl1.Rows.Add(dr1);
                }
                Session["tblstdcost"] = tbl1;
            }
            this.Data_Bind();
        }

        protected void lnkTotalCost_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblstdcost"];
            double qty, rate, amt;
            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                double conqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                double wper = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvwestpc")).Text.Trim());

                double netQty = conqty + (conqty * (wper / 100));

                ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text = netQty.ToString("#,##0.000000;(#,##0.000000); ");


                qty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text.Trim());
                rate = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqrateCost")).Text.Trim());
                double amtgrid = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvamtCost")).Text.Trim());
                amt = qty * rate;

                dt.Rows[i]["conqty"] = conqty;
                dt.Rows[i]["westpc"] = wper;
                dt.Rows[i]["qty"] = qty;
                dt.Rows[i]["rate"] = rate;
                dt.Rows[i]["amt"] = (qty * rate > 0) ? amt : amtgrid;


                //qty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text.Trim());
                //rate = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqrateCost")).Text.Trim());
                //double amtgrid = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvamtCost")).Text.Trim());
                //amt = qty * rate;
                //dt.Rows[i]["qty"] = qty;
                //dt.Rows[i]["rate"] = rate;
                //dt.Rows[i]["amt"] = (qty * rate > 0) ? amt : amtgrid;
            }

            double netqty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", "")));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["percnt"] = (netqty == 0) ? 0.00 : (Convert.ToDouble(dt.Rows[i]["qty"].ToString()) * 100) / netqty;

            }

            Session["tblres"] = dt;
            this.Data_Bind();

        }
        protected void lnkUpdateCost_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["InputType"].ToString() == "CostAnna" || this.Request.QueryString["InputType"].ToString() == "CostAnnaSemi")
            {
                this.UpdateCost();
            }
            else
            {
                this.UpdateQty();
            }
        }
        private void UpdateCost()
        {

            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblstdcost"];
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            string proscode = this.ddlProcess.SelectedValue.ToString();
            //string date = this.txtdate.Text.Trim();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string rescod = dt.Rows[i]["rescode"].ToString();
                string spcfcod = dt.Rows[i]["spcfcod"].ToString();
                string conqty = dt.Rows[i]["conqty"].ToString();
                string westpc = dt.Rows[i]["westpc"].ToString();
                string resqty = dt.Rows[i]["qty"].ToString();
                string resamt = dt.Rows[i]["amt"].ToString();
                string percnt = dt.Rows[i]["percnt"].ToString();
                if (percnt == "")
                    percnt = "0.0";
                bool result = proc1.UpdateTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "SCOSTUPDATE", prodcode, proscode, rescod, resqty, resamt, percnt, spcfcod, conqty, westpc, "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + proc1.ErrorObject["Msg"].ToString() + "');", true);

                    //this.Image3.Visible = true;
                    //this.Image2.Visible = false;
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

            }

        }

        private void UpdateQty()
        {

            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblstdcost"];
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            string proscode = this.ddlProcess.SelectedValue.ToString();
            //string date = this.txtdate.Text.Trim();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string rescod = dt.Rows[i]["rescode"].ToString();
                string spcfcod = dt.Rows[i]["spcfcod"].ToString();
                string resqty = dt.Rows[i]["qty"].ToString();

                //if (resqty == "")
                //percnt = "0.0";
                bool result = proc1.UpdateTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "SCOSTUPDATEQTY", prodcode, proscode, rescod, resqty, spcfcod, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + proc1.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

            }

        }
        protected void gvRptCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label resdesc = (Label)e.Row.FindControl("lblgvrptDescription");
                Label amt = (Label)e.Row.FindControl("lblgvrptamtCost");
                Label percnt = (Label)e.Row.FindControl("lblgvrptpercnt");
                Label avgamt = (Label)e.Row.FindControl("lblavgamt");
                Label maxamt = (Label)e.Row.FindControl("lblgvmaxamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    resdesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    avgamt.Font.Bold = true;
                    maxamt.Font.Bold = true;
                    percnt.Font.Bold = true;
                    resdesc.Attributes.Add("text-align", "right");

                    resdesc.Style.Add("color", "blue");
                    amt.Style.Add("color", "blue");
                    avgamt.Style.Add("color", "blue");
                    maxamt.Style.Add("color", "blue");
                }

            }
        }
        protected void ddlprocode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ItmCod = this.ddlprocode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = ((DataTable)Session["tblItmCod"]).Select("rescode='" + ItmCod + "'");
            this.lblQty.Text = "Qty. :" + Convert.ToDouble(dr1[0]["qty"]).ToString("#,##0.") + "  ";
            this.txtDesc.Text = ((dr1[0]["desc1"].ToString().Trim()).Length == 0) ? "" : dr1[0]["mdesc1"].ToString().Trim() + " :" + dr1[0]["desc1"].ToString().Trim() + " , " + dr1[0]["mdesc2"].ToString().Trim() + " :" + dr1[0]["desc2"].ToString().Trim();
            this.lblUnit.Text = "Unit : " + dr1[0]["resunit"].ToString().Trim();
        }

        protected void addmainpro()
        {
            DataTable tbl2 = (DataTable)Session["tblstdcost"];
            string processcode = "000000000000";
            string rescod = this.ddlprocode.SelectedValue.ToString();

            // string ItmCod = this.ddlprocode.SelectedValue.ToString().Trim(); 
            DataRow[] dr01 = ((DataTable)Session["tblItmCod"]).Select("rescode='" + rescod + "'");
            DataRow dr1 = tbl2.NewRow();
            dr1["procode"] = processcode;
            dr1["rescode"] =ASTUtility.Left(rescod,12);
            dr1["spcfcod"] =ASTUtility.Right(rescod,12);//(((DataTable)ViewState["tblItmCod"]).Select("rescode='" + rescod+ "' and spcfcod='" + rescod  + "'"))[0]["rate"].ToString();
            dr1["resdesc"] = this.ddlprocode.SelectedItem.ToString();
            dr1["resunit"] = dr01[0]["resunit"].ToString().Trim();
            dr1["qty"] = Convert.ToDouble(dr01[0]["qty"]).ToString("#,##0.");
            dr1["rate"] = Convert.ToDouble(dr01[0]["rate"]).ToString("#,##0.");
            dr1["amt"] = 0;
            dr1["percnt"] = 0;
            tbl2.Rows.Add(dr1);
            Session["tblstdcost"] = tbl2;

        }

        //protected void gvrevenue_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //   // ((TextBox)this.gvrevenue.Rows[0].FindControl("txtgvqty")).ReadOnly = true;
        //}
        protected void gvrevenue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblstdcost"];
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            string proscode = "000000000000";

            string rescod = ((Label)this.gvrevenue.Rows[e.RowIndex].FindControl("lblgvcode")).Text.Trim();
            bool result = proc1.UpdateTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "DELETEREVITEME", prodcode, proscode, rescod, "000000000000", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "rescode not in('" + rescod + "')";
            Session.Remove("tblstdcost");
            Session["tblstdcost"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void gvCost_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblstdcost"];
            string prodcode = this.ddlprocode.SelectedValue.ToString();
            string proscode = this.ddlProcess.SelectedValue.ToString();

            string rescod = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcodeCost")).Text.Trim();
            string spcfcod = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblSpcfcode")).Text.Trim();
            bool result = proc1.UpdateTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "DELETEREVITEME", prodcode, proscode, rescod, spcfcod, "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "rescode not in('" + rescod + "')" + " or spcfcod not in('" + spcfcod + "')";
            Session.Remove("tblstdcost");
            Session["tblstdcost"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }


        private void GetAccCode()
        {
            if (this.lnkOpen.Text == "New")
                return;
            if (ASTUtility.Left(this.GetComeCode(), 2) == "61")
            {
                return;
            }

            string comcod = this.GetComeCode();
            string Process = (this.Request.QueryString["InputType"] == "CostAnna" || this.Request.QueryString["InputType"] == "CostAnnaQty") ? "41%" : "04%";
            string CompGroup = this.ddlcatagory.SelectedValue.ToString() == "0000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            string filter = CompGroup+"%";

            if (this.Request.QueryString["actcode"].Length > 12)
            {
                filter = this.Request.QueryString["actcode"].ToString().Substring(0, 12) + "%";
            }
     

            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_INV_STDANA_SEMIFG", "GETMAINPROCODE", Process, filter, "", "", "", "", "", "", "");
            
            this.ddlprocode.DataSource = ds2.Tables[0];
            this.ddlprocode.DataTextField = "resdesc";
            this.ddlprocode.DataValueField = "rescode";
            this.ddlprocode.DataBind();
            ds2.Dispose();

            if (this.Request.QueryString["actcode"].Length > 12)
            {
                ddlprocode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.lnkOpen_Click(null,null); 
            }

            Session["tblItmCod"] = ds2.Tables[0];
            this.ddlprocode_SelectedIndexChanged(null, null);
        }

        private void GetMatList()
        {
            Session.Remove("tblresleb2");

            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.proc1.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_RESCODE_LEVEL2_ISSUE", "04%", "", userid, "", "", "", "", "", "");
            Session["tblresleb2"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void SelectMatList()
        {
            DataTable dt = ((DataTable)Session["tblresleb2"]).Copy();

            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "sircode";
            this.ddlcatagory.DataSource = dt;
            this.ddlcatagory.DataBind();
        }

        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetAccCode();
        }
    }
}