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

namespace SPEWEB.F_15_Pro
{
    public partial class EntryProTarget : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION TARGET INFORMATION";
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void lbtnPrevList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETPREVLIST", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.Items.Clear();
            this.ddlPrevList.DataTextField = "prono1";
            this.ddlPrevList.DataValueField = "prono";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();
            ds1.Dispose();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "New")
            {

                this.lbtnPrevList.Visible = true;
                this.ddlPrevList.Visible = true;
                this.ddlPrevList.Items.Clear();
                this.lblCurNo1.Text = "PLN" + "00" + "-";
                this.txtCurDate.Enabled = true;
                this.txtrefno.Text = "";
                this.gvprotar.DataSource = null;
                this.gvprotar.DataBind();
                this.lbtnOk.Text = "Ok";
                this.lblmsg01.Text = "";
                return;
            }

            this.lbtnPrevList.Visible = false;
            this.ddlPrevList.Visible = false;
            this.lbtnOk.Text = "New";
            this.Get_Plan_Info();



        }
        private void GetLastPLandNo()
        {
            string comcod = this.GetCompCode();
            string plndate = this.txtCurDate.Text;
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETLASTPLANNO", plndate, "", "", "", "", "", "", "", "");
            this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxprono1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxprono1"].ToString().Substring(6, 5);
        }

        private void Get_Plan_Info()
        {


            string comcod = this.GetCompCode();

            string CurDate1 = this.txtCurDate.Text;
            string mPLNNo = "NEWPLN";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mPLNNo = this.ddlPrevList.SelectedValue.ToString();
            }


            ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETPROTARGET", mPLNNo, "",
                              "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblprotar"] = this.HiddenSameData(ds1.Tables[0]);


            if (mPLNNo == "NEWPLN")
            {
                this.GetLastPLandNo();
                this.Data_Bind();

                return;

            }

            //this.lblfloorno.Visible = true;
            //this.ddlfloorno.Visible = true;
            //this.txtsrchItemName.Visible = true;
            //this.imgbtnSearchItemList.Visible = true;
            //this.ddlitemlist.Visible = true;
            //this.lbtnAllLab.Visible = true ;
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["prono1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["prono1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prodate"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.Data_Bind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string flrcode = dt1.Rows[0]["flrcode"].ToString();
            string linecode = dt1.Rows[0]["linecode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcode"].ToString() == flrcode && dt1.Rows[j]["linecode"].ToString() == linecode)
                {

                    dt1.Rows[j]["flrdesc"] = "";
                    dt1.Rows[j]["linedesc"] = "";


                }

                else
                {

                    if (dt1.Rows[j]["flrcode"].ToString() == flrcode)
                        dt1.Rows[j]["flrdesc"] = "";

                    if (dt1.Rows[j]["linecode"].ToString() == linecode)
                        dt1.Rows[j]["linedesc"] = "";
                }





                flrcode = dt1.Rows[j]["flrcode"].ToString();
                linecode = dt1.Rows[j]["linecode"].ToString();


            }



            return dt1;

        }
        private void Data_Bind()
        {
            DateTime datefrm, dateto;
            datefrm = Convert.ToDateTime(this.txtCurDate.Text);
            dateto = Convert.ToDateTime(this.txtCurDate.Text).AddDays(7);

            for (int i = 8; i < 15; i++)
            {
                if (datefrm > dateto)
                    break;

                this.gvprotar.Columns[i].HeaderText = datefrm.ToString("dd.MM.yyyy");
                datefrm = datefrm.AddDays(1);

            }


            this.gvprotar.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvprotar.DataSource = (DataTable)Session["tblprotar"];




            this.gvprotar.DataBind();
            this.FooterCalculation();
            for (int i = 0; i < gvprotar.Rows.Count; i++)
            {
                string linecode = ((Label)gvprotar.Rows[i].FindControl("lgvlinecode")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvprotar.Rows[i].FindControl("lbtnadd");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = linecode;
            }

        }

        private void FooterCalculation()
        {
            DataTable dt = ((DataTable)Session["tblprotar"]).Copy();
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvprotar.FooterRow.FindControl("lblgvFcapacity")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(capacity)", "")) ?
                0.00 : dt.Compute("Sum(capacity)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lblgvFTarqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ?
                0.00 : dt.Compute("Sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty1)", "")) ?
               0.00 : dt.Compute("Sum(qty1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty2)", "")) ?
              0.00 : dt.Compute("Sum(qty2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty3)", "")) ?
              0.00 : dt.Compute("Sum(qty3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty4)", "")) ?
              0.00 : dt.Compute("Sum(qty4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty5)", "")) ?
              0.00 : dt.Compute("Sum(qty5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty6)", "")) ?
              0.00 : dt.Compute("Sum(qty6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty7)", "")) ?
              0.00 : dt.Compute("Sum(qty7)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprotar.FooterRow.FindControl("lgvFmachine")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(macno)", "")) ?
                0.00 : dt.Compute("Sum(macno)", ""))).ToString("#,##0;(#,##0); ");


        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }



        protected void gvprotar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvprotar.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }






        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }





        private void printDuesCollection()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptDuesCollection();
            //DataTable dt1 = (DataTable)ViewState["tbltosusold"];
            //TextObject rpttxtCompName = rptRcvList.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rpttxtCompName.Text = comnam;



            //TextObject txtsize = rptRcvList.ReportDefinition.ReportObjects["txtsize"] as TextObject;
            //txtsize.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            //TextObject txtaptcost = rptRcvList.ReportDefinition.ReportObjects["txtaptcost"] as TextObject;
            //txtaptcost.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            //TextObject txtparking = rptRcvList.ReportDefinition.ReportObjects["txtparking"] as TextObject;
            //txtparking.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
            //TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Monthly Installment Due -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            //TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            //rpttxttoduesupto.Text = "Receivable Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            //TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            //rpttxtpredues.Text = "Previous Due up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            //TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            //rpttxtcurrentdues.Text = "Current  Due " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");






            //TextObject txtuserinfo = rptRcvList.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptRcvList.SetDataSource((DataTable)Session["tblAccRec"]);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Received List Info";
            //    string eventdesc = "Print Report MR";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptRcvList.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptRcvList;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }


        protected void gvprotar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvprotar.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvprotar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvprotar.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            string mlccod = ((Label)this.gvprotar.Rows[e.NewEditIndex].FindControl("lgvmlccode")).Text.Trim();
            string stylecode = ((Label)this.gvprotar.Rows[e.NewEditIndex].FindControl("lgvstylecode")).Text.Trim();
            string colorid = ((Label)this.gvprotar.Rows[e.NewEditIndex].FindControl("lblgvcolorid")).Text.Trim();
            string sizeid = ((Label)this.gvprotar.Rows[e.NewEditIndex].FindControl("lblgvsizeid")).Text.Trim();
            int rowindex = (gvprotar.PageSize) * (this.gvprotar.PageIndex) + e.NewEditIndex;
            DropDownList ddl1 = (DropDownList)this.gvprotar.Rows[e.NewEditIndex].FindControl("ddlOrder");
            ViewState["gindex"] = e.NewEditIndex;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            ViewState.Remove("tblordstyle");
            string Searchorder = "%" + ((TextBox)gvprotar.Rows[e.NewEditIndex].FindControl("txtsrchorder")).Text.Trim() + "%";
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETORDERNO", Searchorder, "", "", "", "", "", "", "", "");
            ddl1.DataTextField = "mlcdesc";
            ddl1.DataValueField = "mlccod";
            ddl1.DataSource = ds1.Tables[1];
            ddl1.DataBind();
            ddl1.SelectedValue = mlccod;
            ViewState["tblordstyle"] = ds1.Tables[0];


            string mlccode1 = ddl1.SelectedValue.ToString();
            DataTable dt1 = ds1.Tables[0].Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable();
            string Searchstyle = "%" + ((TextBox)gvprotar.Rows[e.NewEditIndex].FindControl("txtsrchstyle")).Text.Trim() + "%";
            DropDownList ddl2 = (DropDownList)this.gvprotar.Rows[e.NewEditIndex].FindControl("ddlstyle");
            ddl2.DataTextField = "styledesc1";
            ddl2.DataValueField = "stylecode1";
            ddl2.DataSource = dt1;
            ddl2.DataBind();
            ddl2.SelectedValue = stylecode + colorid + sizeid;


        }
        protected void gvprotar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblprotar"];
            int rowindex = (int)ViewState["gindex"];
            dt.Rows[rowindex]["mlccod"] = ((DropDownList)this.gvprotar.Rows[rowindex].FindControl("ddlOrder")).SelectedValue.ToString();
            dt.Rows[rowindex]["mlcdesc"] = ((DropDownList)this.gvprotar.Rows[rowindex].FindControl("ddlOrder")).SelectedItem.Text;
            string styleinfo = ((DropDownList)this.gvprotar.Rows[rowindex].FindControl("ddlstyle")).SelectedValue.ToString();
            dt.Rows[rowindex]["stylecode"] = styleinfo.Substring(0, 12);
            dt.Rows[rowindex]["colorid"] = styleinfo.Substring(12, 12);
            dt.Rows[rowindex]["sizeid"] = styleinfo.Substring(24, 12);
            dt.Rows[rowindex]["styledesc"] = ((DropDownList)this.gvprotar.Rows[rowindex].FindControl("ddlstyle")).SelectedItem.Text;
            this.gvprotar.EditIndex = -1;
            Session["tblprotar"] = dt;
            this.Data_Bind();

        }

        protected void ibtnSrchregis_Click(object sender, ImageClickEventArgs e)
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddl2 = (DropDownList)this.gvprotar.Rows[rowindex].FindControl("ddlregistd");
            string SearchProject = "%" + ((TextBox)gvprotar.Rows[rowindex].FindControl("txtSerachreg")).Text.Trim() + "%";
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_REGISTRATION", "GETREGCODE", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "regdesc";
            ddl2.DataValueField = "regcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();


        }


        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblprotar"];



            for (int i = 0; i < this.gvprotar.Rows.Count; i++)
            {

                dt.Rows[i]["qty1"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();
                dt.Rows[i]["qty2"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvqty2")).Text.Trim()).ToString();
                dt.Rows[i]["qty3"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvqty3")).Text.Trim()).ToString();
                dt.Rows[i]["qty4"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvqty4")).Text.Trim()).ToString();
                dt.Rows[i]["qty5"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvqty5")).Text.Trim()).ToString();
                dt.Rows[i]["qty6"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvqty6")).Text.Trim()).ToString();
                dt.Rows[i]["qty7"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvqty7")).Text.Trim()).ToString();
                dt.Rows[i]["tqty"] = Convert.ToDouble(dt.Rows[i]["qty1"]) + Convert.ToDouble(dt.Rows[i]["qty2"]) + Convert.ToDouble(dt.Rows[i]["qty3"])
              + Convert.ToDouble(dt.Rows[i]["qty4"]) + Convert.ToDouble(dt.Rows[i]["qty5"]) + Convert.ToDouble(dt.Rows[i]["qty6"]) + Convert.ToDouble(dt.Rows[i]["qty7"]);
            }



            Session["tblprotar"] = dt;


        }
        protected void ibtnSrchorder_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ibtnSrchstele_Click(object sender, ImageClickEventArgs e)
        {

        }



        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = Convert.ToInt16(ViewState["gindex"]);
            DropDownList ddl1 = (DropDownList)this.gvprotar.Rows[index].FindControl("ddlOrder");
            string mlccode1 = ddl1.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable();
            string Searchstyle = "%" + ((TextBox)gvprotar.Rows[index].FindControl("txtsrchstyle")).Text.Trim() + "%";
            DropDownList ddl2 = (DropDownList)this.gvprotar.Rows[index].FindControl("ddlstyle");
            ddl2.DataTextField = "styledesc1";
            ddl2.DataValueField = "stylecode1";
            ddl2.DataSource = dt1;
            ddl2.DataBind();

        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        private bool Check_Overflow()
        {
            DataTable dt1 = (DataTable)Session["tblprotar"];
            var item =
    dt1.AsEnumerable()
    .Select(x =>
    new
    {
        linecode = x["linecode"],
        capacity = x["capacity"],
        qty1 = x["qty1"],
        qty2 = x["qty2"],
        qty3 = x["qty3"],
        qty4 = x["qty4"],
        qty5 = x["qty5"],
        qty6 = x["qty6"],
        qty7 = x["qty7"],



    }
    )
    .GroupBy(s => new { s.linecode, s.capacity })
    .Select(g =>
    new
    {
        linecode = g.Key.linecode,
        capacity = Convert.ToDouble(g.Key.capacity),
        qty1 = g.Sum(x => Math.Round(Convert.ToDouble(x.qty1), 2)),
        qty2 = g.Sum(x => Math.Round(Convert.ToDouble(x.qty2), 2)),
        qty3 = g.Sum(x => Math.Round(Convert.ToDouble(x.qty3), 2)),
        qty4 = g.Sum(x => Math.Round(Convert.ToDouble(x.qty4), 2)),
        qty5 = g.Sum(x => Math.Round(Convert.ToDouble(x.qty5), 2)),
        qty6 = g.Sum(x => Math.Round(Convert.ToDouble(x.qty6), 2)),
        qty7 = g.Sum(x => Math.Round(Convert.ToDouble(x.qty7), 2)),

    }
    ).ToList();
            DataTable dt = ASITUtility03.ListToDataTable(item);
            string comcod = this.GetCompCode();
            string mPRONO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();

            for (int i = 0; i < dt.Rows.Count; i++)
            {



                string linecode = dt.Rows[i]["linecode"].ToString();
                string capacity = dt.Rows[i]["capacity"].ToString();


                int j;

                DateTime date;
                date = Convert.ToDateTime(this.txtCurDate.Text);
                // dateto = Convert.ToDateTime(this.txtCurDate.Text).AddDays(7);

                for (j = 1; j <= 7; j++)
                {
                    string dayid = date.ToString("yyyyMMdd");
                    double qty = Convert.ToDouble(dt.Rows[i]["qty" + j.ToString()].ToString());

                    DataSet ds = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "CHECK_PRODUCTION_TARGET", linecode, capacity, dayid, qty.ToString(), mPRONO, "", "");


                    if (ds.Tables[0].Rows[0]["msg"].ToString() != "")
                    {
                        this.lblmsg01.Text = ds.Tables[0].Rows[0]["msg"].ToString();
                        return false;
                    }

                    date = date.AddDays(1);
                }


            }
            return true;
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {



            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)Session["tblprotar"];
                //DataSet ds1 = new DataSet("ds1");
                //ds1.Tables.Add(dt1);         
                //ds1.Tables[0].TableName = "tbl1";
                //bool ds = ProData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRODUCTION","CHECK_DATE_WISE_TARGET", ds1,null,null);
                if (this.Check_Overflow() == false)
                {
                    return;
                }
                string mPRONO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
                string mPRODAT = this.txtCurDate.Text.Trim();
                string billref = this.txtrefno.Text.Trim();
                bool result = true;
                //DateTime datefrm, dateto;
                //datefrm = Convert.ToDateTime(this.txtCurDate.Text);
                //dateto = Convert.ToDateTime(this.txtCurDate.Text).AddDays(7);
                // int daycount =DTimeSpan(

                for (int i = 0; i < dt1.Rows.Count; i++)
                {



                    string linecode = dt1.Rows[i]["linecode"].ToString();


                    string mlccod = dt1.Rows[i]["mlccod"].ToString();

                    if (mlccod.Length > 0)
                    {
                        string stylecode = dt1.Rows[i]["stylecode"].ToString();
                        string colorid = dt1.Rows[i]["colorid"].ToString();
                        string sizeid = dt1.Rows[i]["sizeid"].ToString();
                        int j;

                        DateTime date;
                        date = Convert.ToDateTime(this.txtCurDate.Text);
                        // dateto = Convert.ToDateTime(this.txtCurDate.Text).AddDays(7);

                        for (j = 1; j <= 7; j++)
                        {
                            string dayid = date.ToString("yyyyMMdd");
                            double qty = Convert.ToDouble(dt1.Rows[i]["qty" + j.ToString()].ToString());

                            result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "INSORUPPROTARINF", mPRONO, dayid, linecode, mlccod, stylecode, mPRODAT, billref, qty.ToString(), date.ToString("dd-MMM-yyyy"), colorid, sizeid, "", "", "", "");


                            if (result == false)
                            {
                                this.lblmsg01.Text = "Updated Failed";
                                return;
                            }

                            date = date.AddDays(1);
                        }







                    }

                }

                this.lblmsg01.Text = "Updated Successfully";






            }
            catch (Exception ex)
            {
                this.lblmsg01.Text = "Errp:" + ex.Message;
            }
        }
        protected void lbtnadd_Click(object sender, EventArgs e)
        {

            string linecode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            DataTable dt = ((DataTable)Session["tblprotar"]).Copy();

            DataRow[] dr = dt.Select("linecode='" + linecode + "'");
            DataRow dr1 = dt.NewRow();
            dr1["flrcode"] = dr[0]["flrcode"].ToString();
            dr1["linecode"] = dr[0]["linecode"].ToString();
            dr1["mlccod"] = dr[0]["mlccod"].ToString();
            dr1["stylecode"] = dr[0]["stylecode"].ToString();

            dr1["flrdesc"] = "";
            dr1["linedesc"] = dr[0]["linedesc"].ToString();
            dr1["mlcdesc"] = dr[0]["mlcdesc"].ToString();
            dr1["styledesc"] = dr[0]["styledesc"].ToString(); ;
            dr1["capacity"] = dr[0]["capacity"].ToString();
            dr1["macno"] = dr[0]["macno"].ToString();
            dr1["tqty"] = 0.00;
            dr1["qty1"] = 0.00;
            dr1["qty2"] = 0.00;
            dr1["qty3"] = 0.00;
            dr1["qty4"] = 0.00;
            dr1["qty5"] = 0.00;
            dr1["qty6"] = 0.00;
            dr1["qty7"] = 0.00;
            dt.Rows.Add(dr1);


            DataView dv = dt.DefaultView;
            dv.Sort = ("flrcode, linecode, mlccod, stylecode");
            dt = dv.ToTable();

            Session["tblprotar"] = dt;
            this.Data_Bind();

        }
    }
}