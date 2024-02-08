using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_09_Commer
{
    public partial class LcReceive : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Purdata = new ProcessAccess();
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


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LC Receive Form";
                this.txtreceivedate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtChlDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.Load_Location();
                this.GetLcNumber();
                this.GetStore();
                if (this.Request.QueryString["Type"].ToString() == "Edit")
                {
                    this.txtreceivedate.Enabled = false;
                }
            }
            this.CommonButton();
        }

        private void CommonButton()
        {

            // ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Delete Selected Item";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).OnClientClick = "return confirm('Do you want to Remove Selected Item?')";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).CssClass = "btn btn-info btn-sm";

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }
        protected void Load_Location()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMATLOCATION", "", "%", "", "", "", "", "", "", "");

            ViewState["tblLocation"] = ds1.Tables[0];
            if (ds1 == null)
                return;




        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkbtnHisprice_Click);


        }

        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Item Expand")
            {
                this.gvRecItem.Visible = true;
                this.LbtnReqItemShow.Text = "Item Collapse";
            }
            else
            {
                this.gvRecItem.Visible = false;
                this.LbtnReqItemShow.Text = "Item Expand";
            }
        }

        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }
        }
        protected void chkheadl_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvReceive.Rows.Count; i++)
            {
                if (((CheckBox)this.dgvReceive.HeaderRow.FindControl("chkhead")).Checked)
                {
                    ((CheckBox)this.dgvReceive.Rows[i].FindControl("chkCol")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.dgvReceive.Rows[i].FindControl("chkCol")).Checked = false;
                }
            }
        }
        private void lnkbtnHisprice_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_09_Commer.EClassLCMGT> rcvdata = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
            List<SPEENTITY.C_09_Commer.EClassLCMGT> tempList = new List<SPEENTITY.C_09_Commer.EClassLCMGT>();
            string comcod = this.GetCompCode();
            for (int i = 0; i < this.dgvReceive.Rows.Count; i++)
            {
                if (((CheckBox)this.dgvReceive.Rows[i].FindControl("chkCol")).Checked)
                {
                    tempList.Add(rcvdata[i]);

                }
            }
            foreach (var item in tempList)
            {
                rcvdata.Remove(item);
            }
            ViewState["TblReceive"] = rcvdata;


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Successfully');", true);
            this.Data_Bind();
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comadd = hst["comadd1"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");


                string preno = this.txtgrrno.Text.ToString();
                string lcno2 = this.Request.QueryString["actcode"].ToString();
                DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_PRE_RCVINFO", lcno2, preno, "", "", "", "", "", "", "");

                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.BO_AllLCInfo.LCQCPrint>();
                LocalReport rpt1 = new LocalReport();
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptLCRecInfo", lst, null, null);
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd1", comadd));
                rpt1.SetParameters(new ReportParameter("username", username));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("RptTitle", "Material Received Report"));
                rpt1.SetParameters(new ReportParameter("date", "Date: " + printdate));
                rpt1.SetParameters(new ReportParameter("reqno", preno));
                rpt1.SetParameters(new ReportParameter("chalandate", "" + lst[0].chalandate.ToString("dd-MMM-yyyy")));
                rpt1.SetParameters(new ReportParameter("chalanno", lst[0].chalanno));
                rpt1.SetParameters(new ReportParameter("rcvdate", lst[0].rcvdate.ToString("dd-MMM-yyyy")));
                rpt1.SetParameters(new ReportParameter("stordesc", lst[0].stordesc));
                rpt1.SetParameters(new ReportParameter("rcvno", lst[0].actdesc));

                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);


            }
        }
        public void GetLcNumber()
        {
            string comcod = this.GetCompCode();
            string SlcNO = "%%";
            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RETRIVE_LC_VALUE", SlcNO, "14", "ACINF", "", "", "", "", "", ""); // table Desc 3
            this.ddlLcCode.DataTextField = "actdesc";
            this.ddlLcCode.DataValueField = "actcode";
            this.ddlLcCode.DataSource = ds1.Tables[0];
            this.ddlLcCode.DataBind();
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlLcCode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.LbtnOk_Click(null, null);
            }

        }

        private void LoadRCVNO()
        {
            string comcod = this.GetCompCode();
            string storid = this.ddlStorid.SelectedValue.ToString();
            string grrdat = this.txtreceivedate.Text.ToString().Substring(0, 11);
            DataSet ds = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETRCVNO", grrdat, storid, "", "", "", "", "", "", "");
            this.ddlPreGrn.DataTextField = "rcvno";
            this.ddlPreGrn.DataValueField = "rcvno";
            this.ddlPreGrn.DataSource = ds.Tables[0];
            this.ddlPreGrn.DataBind();
            this.txtgrrno.Text = ds.Tables[0].Rows[0]["rcvno"].ToString();
        }
        protected void LbtnOk_Click(object sender, EventArgs e)
        {
            this.ddlLcCode.Enabled = false;
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.PreGrn();
                this.Get_Pre_RCVINFO();
            }

        }

        protected void ddlLcCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetStore();
        }
        private void GetStore()
        {
            try
            {
                string lcno2 = this.ddlLcCode.SelectedValue.ToString();
                string comcod = this.GetCompCode();
                string LcCode = ASTUtility.Left(this.ddlLcCode.SelectedValue.ToString(), 4);
                string LccodeType = (LcCode == "1401") ? "actcode like '15%'"
                    : (LcCode == "1402") ? "actcode like '17%'"
                    : (LcCode == "1403") ? "actcode like '11%'"
                    : "actcode like '1[157]%'";
                DataSet ds5 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RETRIVELCSTORE1", LccodeType, "", "", "",
                    "", "", "", "", "");
                this.ddlStorid.DataTextField = "actdesc1";
                this.ddlStorid.DataValueField = "actcode";
                this.ddlStorid.DataSource = ds5.Tables[0];
                this.ddlStorid.DataBind();

                if (this.Request.QueryString["centrid"].Length > 0)
                {
                    this.ddlStorid.SelectedValue = this.Request.QueryString["centrid"].ToString();
                    this.ddlStorid.Enabled = false;
                }
            }
            catch (Exception ex)
            {

            }


        }
        protected void imgbtnPreGrn_Click(object sender, EventArgs e)
        {

            this.PreGrn();
        }
        private void PreGrn()
        {
            string comcod = this.GetCompCode();
            string strcode = (this.Request.QueryString["centrid"].Length > 0) ? this.Request.QueryString["centrid"].ToString() : this.ddlStorid.SelectedValue.ToString();
            string filter2 = "%" + this.txtgrrno.Text.Trim() + "%";
            DataSet ds5 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETPRERCV", strcode, filter2, "", "", "", "", "", "", ""); //table Desc 2

            if (ds5.Tables[0].Rows.Count == 0)
                return;

            this.lnkReceive.Visible = false;

            this.ddlPreGrn.DataTextField = "rcvno1";
            this.ddlPreGrn.DataValueField = "rcvno";
            this.ddlPreGrn.DataSource = ds5.Tables[0];
            this.ddlPreGrn.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPreGrn.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
        }
        protected void lnkReceive_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.LoadRCVNO();
            if (this.ddlStorid.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Store Id.');", true);


                return;
            }
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string rcvdat = this.txtreceivedate.Text.Trim().Substring(0, 11);
            string grrno = this.txtgrrno.Text.Trim();
            DataSet ds6 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "ORDERRECEIVE", lcno2, rcvdat, grrno, "", "", "", "", "", "");
            var List = ds6.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.EClassLCMGT>();



            //   ViewState["tbllccost"] = ds6.Tables[1];
            ViewState["TblReceive"] = List;

            List<SumClass> result = new List<SumClass>();
            if (List.Count > 0)
            {

                result = List
         .GroupBy(l => new { l.rescod, l.spcfcod })
         .Select(cl => new SumClass
         {
             rescod = cl.First().rescod,
             resdesc = cl.First().resdesc,
             spcfcod = cl.First().spcfcod,
             spcfdesc = cl.First().spcfdesc,
             color = cl.First().color,
             size = cl.First().size,
             unit = cl.First().unit,
             remainordr = cl.Sum(c => c.remainordr),
             rcvqty = cl.Sum(c => c.rcvqty),
             ordrqty = cl.Sum(c => c.ordrqty),
         }).ToList();
            }
            Session["TblReceivesum"] = result;

            Data_Bind();

        }

        class SumClass
        {
            public string rescod { get; set; }
            public string resdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public string unit { get; set; }
            public double rcvqty { get; set; }
            public double ordrqty { get; set; }
            public double balqty { get; set; }
            public double remainordr { get; set; }

        }

        private void Data_Bind()
        {
            try
            {
                var rcvdata = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];

                this.dgvReceive.DataSource = rcvdata;
                this.dgvReceive.DataBind();

                this.Rcv_Footcal();

                var rcvdatasum = (List<SumClass>)Session["TblReceivesum"];

                this.gvRecItem.DataSource = rcvdatasum;
                this.gvRecItem.DataBind();

                if (rcvdatasum.Count > 0)
                {
                    ((Label)this.gvRecItem.FooterRow.FindControl("lgvRSumRecqty")).Text = (rcvdatasum.Select(p => p.rcvqty).Sum() == 0.00) ? "0" : rcvdatasum.Select(p => p.rcvqty).Sum().ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvRecItem.FooterRow.FindControl("gvLblTtl1")).Text = (rcvdatasum.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : rcvdatasum.Select(p => p.ordrqty).Sum().ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvRecItem.FooterRow.FindControl("gvLblTtl2")).Text = (rcvdatasum.Select(p => p.remainordr).Sum() == 0.00) ? "0" : rcvdatasum.Select(p => p.remainordr).Sum().ToString("#,##0.00;(#,##0.00); ");

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }

        }
        private void Rcv_Footcal()
        {
            try
            {

                var list1 = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
                if (list1.Count == 0)
                    return;
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFordqty")).Text = (list1.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : list1.Select(p => p.ordrqty).Sum().ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgrvFFreeqty1")).Text = (list1.Select(p => p.freeqty).Sum() == 0.00) ? "0" : list1.Select(p => p.freeqty).Sum().ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFreuptlast")).Text = (list1.Select(p => p.rcvuptolast).Sum() == 0.00) ? "0" : list1.Select(p => p.rcvuptolast).Sum().ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrmainord")).Text = (list1.Select(p => p.remainordr).Sum() == 0.00) ? "0" : list1.Select(p => p.remainordr).Sum().ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrcvQty")).Text = (list1.Select(p => p.rcvqty).Sum() == 0.00) ? "0" : list1.Select(p => p.rcvqty).Sum().ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFShipQty")).Text = (list1.Select(p => p.shipqty).Sum() == 0.00) ? "0" : list1.Select(p => p.shipqty).Sum().ToString("#,##0.00;(#,##0.00); ");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);


            }

        }


        private void Get_Pre_RCVINFO()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            if (this.ddlPreGrn.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Pre No');", true);


                return;
            }
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string preno = this.ddlPreGrn.SelectedValue.ToString();

            DataSet ds6 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_PRE_RCVINFO", lcno2, preno, "", "", "", "", "", "", "");
            var list = ds6.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.EClassLCMGT>();
            ViewState["TblReceive"] = list;

            this.txtreceivedate.Text = Convert.ToDateTime(list[0].rcvdate).ToString("dd-MMM-yyyy");
            this.txtgrrno.Text = list[0].rcvno.ToString();
            this.txtChalanNo.Text = list[0].chalanno.ToString();
            this.txtChlDate.Text = Convert.ToDateTime(list[0].chalandate).ToString("dd-MMM-yyyy");
            this.ddlStorid.SelectedValue = list[0].storid.ToString();
            Data_Bind();
        }
        private void Save_Rec_Value()
        {
            var list = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
            int RowIndex = 0;

            //  double tocost = Convert.ToDouble("0" + ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFcurlcCost")).Text);
            for (int i = 0; i < this.dgvReceive.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgvReceive.Rows[i].FindControl("txtgvrcvQty")).Text.Trim()));
                string shipqty = ((TextBox)this.dgvReceive.Rows[i].FindControl("txtgvShipQty")).Text.Trim();
                shipqty = shipqty == "" ? "0.00" : shipqty.Substring(0, 1) == "(" ? "-" + shipqty.Substring(1, shipqty.Length - 2) : shipqty;

                double ShipQty = Convert.ToDouble(shipqty);

                string loction = ((DropDownList)this.dgvReceive.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                RowIndex = this.dgvReceive.PageIndex * this.dgvReceive.PageSize + i;
                list[RowIndex].rcvqty = Qty;
                list[RowIndex].rcvqty = Qty;
                list[RowIndex].shipqty = ShipQty;
                list[RowIndex].location = loction;
            }

            ViewState["TblReceive"] = list;
        }

        protected void dgvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox gvrcvQty = (TextBox)e.Row.FindControl("txtgvrcvQty");

                double balqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rcvqty"));


                if (balqty == 0.00)
                {

                    gvrcvQty.ToolTip = "Balance Qty Zero";
                }
                else
                {

                }

                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlval");
                //string reqno = ((Label)e.Row.FindControl("lblgvReqnomain")).Text.Trim();
                string rescod = ((Label)e.Row.FindControl("lblgvResCode1")).Text.Trim();
                string spcfcod = ((Label)e.Row.FindControl("lblgvSpcode")).Text.Trim();

                // DataTable tbl1 = (DataTable)ViewState["tblMRR"];

                var list = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];

                var list1 = list.FindAll(p => p.rescod == rescod && p.spcfcod == spcfcod);


                //DataRow[] dr = tbl1.Select("reqno='" + reqno + "' and  rsircode = '" + rescod + "' and spcfcod = '" + spcfcod + "'");

                DataTable dt = (DataTable)ViewState["tblLocation"];

                ddllocation.DataTextField = "gdesc";
                ddllocation.DataValueField = "gcod";
                ddllocation.DataSource = dt;
                ddllocation.DataBind();
                if (list1.Count != 0)
                {
                    ddllocation.SelectedValue = list1[0].location.ToString();
                }

            }
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Type"] == "Entry") ? userid : "";
            string Posttrmid = (this.Request.QueryString["Type"] == "Entry") ? Terminal : "";
            string PostSession = (this.Request.QueryString["Type"] == "Entry") ? Sessionid : "";
            string Posteddat = (this.Request.QueryString["Type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : "01-Jan-1900";
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            string storid = this.ddlStorid.SelectedValue.ToString();
            string rcvdate = this.txtreceivedate.Text.Substring(0, 11).ToString();
            string chalanno = this.txtChalanNo.Text.Trim();
            string chalandate = this.txtChlDate.Text.Substring(0, 11).ToString();
            this.Save_Rec_Value();
            this.LoadRCVNO();
            string rcvno = this.txtgrrno.Text.Trim();

            string Edit = "";
            if (ViewState["TblReceive"] != null)
            {
                var list = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
                DataTable tbl4 = ASITUtility03.ListToDataTable(list);
                //  DataTable dt = ((DataTable)ViewState["tbllccost"]).Copy();

                DataSet ds1 = new DataSet("ds1");




                ds1.Tables.Add(tbl4);
                //ds1.Tables.Add(dt);
                ds1.Tables[0].TableName = "tbl1";
                //ds1.Tables[1].TableName = "tbl2";



                string lotno = tbl4.Rows[0]["lotno"].ToString();
                string expdat = Convert.ToDateTime(tbl4.Rows[0]["expdate"]).ToString("dd-MMM-yyyy");
                string lcno = this.ddlLcCode.SelectedValue.ToString();
                string expdat1 = Convert.ToDateTime((expdat == "" ? "1-Jan-1900" : expdat)).ToString("dd-MMM-yyyy");
                if (chalanno.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Empty Challan No Not Allow');", true);


                    return;
                }
                DataSet ds112 = Purdata.GetTransInfoNew(comcod, "SP_ENTRY_LC_INFO", "UPDATELCDETAILS", ds1, null, null, Edit, rcvdate, storid, lotno, expdat1, PostedByid, PostSession, Posttrmid, Posteddat, lcno, rcvno, chalanno, chalandate, "", "");
                if (ds112.Tables[0].Rows.Count != 0)
                {
                    this.txtgrrno.Text = ds112.Tables[0].Rows[0]["memonum"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


                    //  ((LinkButton)this.dgvReceive.FooterRow.FindControl("lnkFinalUpdateR")).Enabled = false;

                    //
                }


            }

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Rec_Value();
            this.Data_Bind();
        }

        protected void LbtnRecItemCalculate_Click(object sender, EventArgs e)
        {
            var listsum = (List<SumClass>)Session["TblReceiveSum"];

            for (int i = 0; i < this.gvRecItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRecItem.Rows[i].FindControl("lgvISRecqty")).Text.Trim()));
                listsum[i].rcvqty = Qty;
                listsum[i].balqty = listsum[i].remainordr - Qty;

                if (Qty == 0)
                    continue;

                var list = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];

                foreach (var item in list.FindAll(r => r.rescod + r.spcfcod == listsum[i].rescod + listsum[i].spcfcod).ToList())
                {

                    if (item.remainordr < Qty)
                    {
                        item.rcvqty = item.remainordr;
                        Qty = Qty - item.remainordr;
                    }
                    else
                    {
                        item.rcvqty = Qty;
                        Qty = 0;
                        break;
                    }
                }


                ViewState["TblReceive"] = list;

            }
            Session["TblReceiveSum"] = listsum;
            this.Data_Bind();
        }
        protected void lgvISBalqtyCalculate(object sender, EventArgs e)
        {
            var listsum = (List<SumClass>)Session["TblReceiveSum"];

            for (int i = 0; i < this.gvRecItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(((TextBox)this.gvRecItem.Rows[i].FindControl("lgvISBalqty")).Text.Trim());
                //listsum[i].rcvqty = Qty;
                //listsum[i].balqty = listsum[i].remainordr - Qty;

                if (Qty > -1)
                    continue;

                var list = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
                var rdata = list.FindAll (r => r.rescod + r.spcfcod == listsum[i].rescod + listsum[i].spcfcod);
                var fdata = rdata.OrderByDescending(c => c.rcvqty).First();

                foreach (var item in rdata)
                {

                    if (item.bomid == fdata.bomid)
                    {
                        item.rcvqty = item.remainordr + ( - Qty);
                        Qty = Qty - item.remainordr;
                    }
                    
                }


                ViewState["TblReceive"] = list;

            }
            Session["TblReceiveSum"] = listsum;
            this.Data_Bind();
        }
    }
}