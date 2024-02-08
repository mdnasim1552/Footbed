using Microsoft.Reporting.WinForms;
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

namespace SPEWEB.F_09_Commer
{
    public partial class LcQcRecv : System.Web.UI.Page
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
                this.Load_Location();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LC Qc Form";
                this.txtreceivedate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.TxtQcDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLcNumber();
                if (this.Request.QueryString["Type"].ToString() == "Approve")
                {
                    this.TxtQcDate.Visible = true;
                    this.txtreceivedate.Visible = false;
                }
                this.CommonButton();

                ViewState["AdjustClicked"] = false;
            }

        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


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
        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Expand")
            {
                this.gvRecItem.Visible = true;
                this.LbtnReqItemShow.Text = "Collapse";
            }
            else
            {
                this.gvRecItem.Visible = false;
                this.LbtnReqItemShow.Text = "Expand";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Rec_Value();
            this.Data_Bind();
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            //this.lmsg.Visible = true;
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
            string type = this.Request.QueryString["Type"].ToString();
            string rcvdate = this.txtreceivedate.Text.Substring(0, 11).ToString();
            string qcdate = (type == "Approve") ? Convert.ToDateTime(this.TxtQcDate.Text).ToString("dd-MMM-yyyy") : "01-Jan-1900";
            string reportLevel = this.ddlReportLevel.SelectedValue.ToString();
            this.SaveReciveItem();
            this.Save_Rec_Value();
         

            if (this.ddlPreGrn.Items.Count == 0)
                LoadGRRNo();

            string grrno = this.txtgrrno.Text.Trim().ToString();
            if (ViewState["TblReceive"] != null)
            {
                var lst = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
                DataTable tbl4 = ASITUtility03.ListToDataTable(lst); //((DataTable)ViewState["TblReceive"]).Copy();           

                DataSet ds1 = new DataSet("ds1");
                var listsum = (List<SumReceiveItems>)Session["TblReceiveSum"];
                DataTable tbl5 = ASITUtility03.ListToDataTable(listsum); //((DataTable)ViewState["TblReceive"]).Copy();           

             
                ds1.Tables.Add(tbl4);
                ds1.Tables.Add(tbl5);
                //ds1.Tables.Add(dt);
                ds1.Tables[0].TableName = "tbl1";
                ds1.Tables[1].TableName = "tbl2";
                var temp = ds1.GetXml();
                DataSet ds112 = Purdata.GetTransInfoNew(comcod, "SP_ENTRY_LC_INFO", "UPDATE_LC_QC", ds1, null, null, rcvdate, PostedByid, PostSession, Posttrmid, Posteddat, grrno, type, qcdate, reportLevel, "", "");
                if (ds112.Tables[0].Rows.Count != 0)
                {
                    this.txtgrrno.Text = ds112.Tables[0].Rows[0]["memonum"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);

                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Successfully');", true);
                    //((LinkButton)this.dgvReceive.FooterRow.FindControl("lnkFinalUpdateR")).Enabled = false;

                }


            }

        }
        private void LoadGRRNo()
        {
            string comcod = this.GetCompCode();
            string storid = this.Request.QueryString["centrid"].ToString();
            string grrdat = this.txtreceivedate.Text.ToString().Substring(0, 11);
            DataSet ds = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETGRRNO", grrdat, storid, "", "", "", "", "", "", "");
            this.ddlPreGrn.DataTextField = "grrno";
            this.ddlPreGrn.DataValueField = "grrno";
            this.ddlPreGrn.DataSource = ds.Tables[0];
            this.ddlPreGrn.DataBind();
            this.txtgrrno.Text = ds.Tables[0].Rows[0]["grrno"].ToString();
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
                this.ddlLcCode_SelectedIndexChanged(null, null);
                this.LbtnOk_Click(null, null);
            }

        }


        protected void LbtnOk_Click(object sender, EventArgs e)
        {
            this.ddlLcCode.Enabled = false;
            this.ddlrcvno.Enabled = false;
            this.Get_info();

        }
        protected void imgbtnPreGrn_Click(object sender, EventArgs e)
        {

            this.PreGrn();
        }
        private void PreGrn()
        {
            string comcod = this.GetCompCode();
            string store = "";
            if (this.Request.QueryString["centrid"].Length > 0)
            {
                store = this.Request.QueryString["centrid"].ToString();
            }
            string filter2 = "%" + this.txtgrrno.Text.Trim() + "%";
            DataSet ds5 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETPREGRN", store, filter2, "", "", "", "", "", "", ""); //table Desc 2

            if (ds5.Tables[0].Rows.Count == 0)
                return;
            this.ddlPreGrn.DataTextField = "grrno1";
            this.ddlPreGrn.DataValueField = "grrno";
            this.ddlPreGrn.DataSource = ds5.Tables[0];
            this.ddlPreGrn.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPreGrn.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
        }

        private void Get_info()
        {


            ViewState.Remove("TblReceive");
            string comcod = this.GetCompCode();
            string storid = this.Request.QueryString["centrid"].ToString();
            string grrno = "NEWGRN";
            if (this.ddlPreGrn.Items.Count > 0)
            {
                this.txtreceivedate.Enabled = false;
                grrno = this.ddlPreGrn.SelectedValue.ToString();
            }

            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_LCQC_INFO", storid, grrno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["TblReceive"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.EClassLCMGT>();
            Session["TblReceivesum"] = ds1.Tables[1].DataTableToList<SumReceiveItems>();

            if (grrno == "NEWGRN")
            {
                string grrdat = this.txtreceivedate.Text.ToString().Substring(0, 11);
                DataSet ds = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETGRRNO", grrdat, storid, "", "", "", "", "", "", "");
                //this.ddlPreGrn.DataTextField = "grrno";
                //this.ddlPreGrn.DataValueField = "grrno";
                //this.ddlPreGrn.DataSource = ds.Tables[0];
                //this.ddlPreGrn.DataBind();
                this.txtgrrno.Text = ds.Tables[0].Rows[0]["grrno"].ToString();
                this.GetRecv_info();
                return;
            }
                        
            string a = ds1.Tables[0].Rows[0]["reporttype"].ToString().Trim();
            this.ddlrcvno.SelectedValue = ds1.Tables[0].Rows[0]["rcvno"].ToString();
            this.ddlReportLevel.SelectedValue = ds1.Tables[0].Rows[0]["reporttype"].ToString().Trim();
            this.txtreceivedate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["rcvdate"]).ToString("dd-MMM-yyyy");
            this.txtgrrno.Text = ds1.Tables[0].Rows[0]["grrno"].ToString();
            this.TxtQcDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["rcvdate"]).ToString("dd-MMM-yyyy");
            this.txtchnlno.Text = ds1.Tables[0].Rows[0]["chalanno"].ToString();
            this.txtchnldate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["chalandate"]).ToString("dd-MMM-yyyy");
            this.Data_Bind();
        }

        private void GetRecv_info()
        {

            string comcod = this.GetCompCode();
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string rcvno = this.ddlrcvno.SelectedValue.ToString();

            DataSet ds6 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_LC_RECEIVED", lcno2, rcvno, "", "", "", "", "", "", "");
            var lst = ds6.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.EClassLCMGT>();
            ViewState["TblReceive"] = lst;
            if (ds6 == null || ds6.Tables[0].Rows.Count == 0)
                return;
            this.txtchnlno.Text = ds6.Tables[0].Rows[0]["chalanno"].ToString();
            this.TxtQcDate.Text = Convert.ToDateTime(ds6.Tables[0].Rows[0]["rcvdate"]).ToString("dd-MMM-yyyy");

            this.txtchnldate.Text = Convert.ToDateTime(ds6.Tables[0].Rows[0]["chalandate"]).ToString("dd-MMM-yyyy");

            List<SumReceiveItems> result = (List<SumReceiveItems>)Session["TblReceivesum"];
            if (result.Count == 0)
            {
              
                if (lst.Count > 0)
                {

                    result = lst
             .GroupBy(l => new { l.rescod, l.spcfcod })
             .Select(cl => new SumReceiveItems
             {
                 rescod = cl.First().rescod,
                 resdesc = cl.First().resdesc,
                 spcfcod = cl.First().spcfcod,
                 spcfdesc = cl.First().spcfdesc,
                 color = cl.First().color??"",
                 size = cl.First().size??"",
                 unit = cl.First().unit,
                 passqty = cl.Sum(c => c.qcqty),
                 rcvqty = cl.Sum(c => c.rcvqty),
                 qcqty = cl.Sum(c => c.qcqty),
                 qcstatus = "0",
                 chckdetails = "",
                 chckmethod = "AQL",
                 finding = "",
                 remarks = "",
             }).ToList();
                }
                Session["TblReceivesum"] = result;
            }
          

            Data_Bind();

        }

        private void Data_Bind()
        {
            try
            {
                var rcvdata = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
                if (this.Request.QueryString["Type"].ToString() == "Approve")
                {
                    this.dgvReceive.Columns[17].Visible = true;
                }
                this.dgvReceive.Columns[19].Visible = false;

                this.dgvReceive.DataSource = rcvdata;
                this.dgvReceive.DataBind();

                this.Rcv_Footcal();

                var rcvdatasum = (List<SumReceiveItems>)Session["TblReceivesum"];

                this.gvRecItem.DataSource = rcvdatasum;
                this.gvRecItem.DataBind();

                if (rcvdatasum.Count > 0)
                {
                    ((Label)this.gvRecItem.FooterRow.FindControl("lgvRSumRecqty")).Text = (rcvdatasum.Select(p => p.passqty).Sum() == 0.00) ? "0" : rcvdatasum.Select(p => p.passqty).Sum().ToString("#,##0.00;(#,##0.00); ");

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

                var lst = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
                if (lst.Count == 0)
                    return;
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFordqty")).Text = (lst.Select(p => p.rcvqty).Sum() == 0.00) ? "0" : lst.Select(p => p.rcvqty).Sum().ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFreuptlast")).Text = (lst.Select(p => p.preqcqty).Sum() == 0.00) ? "0" : lst.Select(p => p.preqcqty).Sum().ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrmainord")).Text = (lst.Select(p => p.remqty).Sum() == 0.00) ? "0" : lst.Select(p => p.remqty).Sum().ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrcvQty")).Text = (lst.Select(p => p.qcqty).Sum() == 0.00) ? "0" : lst.Select(p => p.qcqty).Sum().ToString("#,##0.00;(#,##0.00); ");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);

               
            }

        }

        private void Save_Rec_Value()
        {
            var lst = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
            int RowIndex = 0;

            //  double tocost = Convert.ToDouble("0" + ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFcurlcCost")).Text);
            for (int i = 0; i < this.dgvReceive.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble("0" + ((TextBox)this.dgvReceive.Rows[i].FindControl("txtgvrcvQty")).Text.Trim());
                double remqty = Convert.ToDouble("0" + ((Label)this.dgvReceive.Rows[i].FindControl("lblgvrmainord")).Text.Trim());
                string expdate = Convert.ToDateTime(((TextBox)this.dgvReceive.Rows[i].FindControl("txtexpeirdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string loction = ((DropDownList)this.dgvReceive.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                RowIndex = this.dgvReceive.PageIndex * this.dgvReceive.PageSize + i;
                if (Qty > remqty && remqty != 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Qc Qty Must Less Then or Equal to Remeining Qty');", true);
                    return;
                }
                if (Qty == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check Your Qc Quantity');", true);
                    return;
                }
                lst[RowIndex].qcqty = Qty;
                lst[RowIndex].expdate = Convert.ToDateTime(expdate);
                lst[RowIndex].remarks = ((TextBox)this.dgvReceive.Rows[i].FindControl("txtremarks")).Text.Trim();
                lst[RowIndex].rackno = ((TextBox)this.dgvReceive.Rows[i].FindControl("txtgvRack")).Text.Trim();
                lst[RowIndex].location = loction;
            }

            ViewState["TblReceive"] = lst;
        }
        protected void dgvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlval");
                string rescod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescod")).ToString();
                string spcfcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString();

                TextBox gvrcvQty = (TextBox)e.Row.FindControl("txtgvrcvQty");

                double balqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "qcqty"));



                var rcvdata = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];

                DataTable tbl1 = ASITUtility03.ListToDataTable(rcvdata);


                DataRow[] dr = tbl1.Select("rescod = '" + rescod + "' and spcfcod = '" + spcfcod + "'");

                DataTable dt = (DataTable)ViewState["tblLocation"];
                DataView dv = new DataView(dt);
                dv.Sort = "gcod ASC";
                //dt = dv.ToTable();

                ddllocation.DataTextField = "gdesc";
                ddllocation.DataValueField = "gcod";
                ddllocation.DataSource = dv.ToTable();
                ddllocation.DataBind();

                if (dr.Count() != 0)
                {
                    ddllocation.SelectedValue = dr[0]["location"].ToString();
                }
                else
                {
                    ddllocation.SelectedValue = "00000";
                }


                if (balqty == 0.00)
                {

                    //gvrcvQty.Enabled = false;
                    gvrcvQty.ToolTip = "Balance Qty Zero";
                }
                else
                {

                }
            }
        }


        protected void ddlLcCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_LC_RCVNO", actcode, "", "", "", "", "", ""); // table Desc 3
            this.ddlrcvno.DataTextField = "rcvno1";
            this.ddlrcvno.DataValueField = "rcvno";
            this.ddlrcvno.DataSource = ds1.Tables[0];
            this.ddlrcvno.DataBind();
            if (this.Request.QueryString["Type"].ToString() == "Entry")
            {
                if (this.Request.QueryString["genno"].Length > 0)
                {
                    this.ddlrcvno.SelectedValue = this.Request.QueryString["genno"].ToString();
                }
            }
            else
            {
                this.PreGrn();
            }

        }


        protected void LbtnQcUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            // string reqno = ((Label)this.dgvReceive.Rows[index].FindControl("lblgvReqnomain")).Text.ToString();
            string rsircode = ((Label)this.dgvReceive.Rows[index].FindControl("lblgvResCode1")).Text.ToString();
            string spcfcod = ((Label)this.dgvReceive.Rows[index].FindControl("lblgvSpcode")).Text.ToString();
            string lcrcvno = this.Request.QueryString["genno"].ToString();
            string actcode = this.Request.QueryString["actcode"].ToString();
            string syspon = ((Label)this.dgvReceive.Rows[index].FindControl("lblgrvpono")).Text.ToString();
            string bomid = ((Label)this.dgvReceive.Rows[index].FindControl("lblGvBOMid")).Text.ToString();
            
            DataSet result = Purdata.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "MATQCDETAILSINFO", actcode, lcrcvno, rsircode, spcfcod, "LC", syspon, bomid);
            if (result == null)
            {
                return;
            }
            ViewState["tblqcdata"] = result.Tables[0];

            this.gvqcDeails.DataSource = result.Tables[0];
            this.gvqcDeails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenQCModal();", true);
        }

        protected void LbtnUpdateQcDetails_Click(object sender, EventArgs e)
        {
            this.Save_Value_QC();
            DataTable tbl1 = (DataTable)ViewState["tblqcdata"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(tbl1);
            ds.Tables[0].TableName = "tblqcdata";
            string comcod = this.GetCompCode();
            //TextBox rcvQty = ((TextBox)this.dgvReceive.Rows[index].FindControl("txtgvrcvQty"));
            //rcvQty.Text = ds.Tables[0].Rows[0]["passqty"].ToString();
            string xml = ds.GetXml();
            bool result = Purdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "UPDATE_QC_DETAILS_INFO", ds, null, null, "LC");
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
                return;

            }
        }
        public void Save_Value_QC()
        {
            DataTable tbl1 = (DataTable)ViewState["tblqcdata"];
            for (int j = 0; j < this.gvqcDeails.Rows.Count; j++)
            {
                double qcqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvCckqty")).Text.Trim()));
                double passqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvpassqty")).Text.Trim()));

                string method = ((DropDownList)this.gvqcDeails.Rows[j].FindControl("ddlcheckmethod")).SelectedItem.ToString();
                string unit = ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvUom")).Text.Trim();
                string check_details = ((TextBox)this.gvqcDeails.Rows[j].FindControl("txtgvCheckDetails")).Text.Trim();
                string findings = ((TextBox)this.gvqcDeails.Rows[j].FindControl("lblgvFindings")).Text.Trim();
                string remarks = ((TextBox)this.gvqcDeails.Rows[j].FindControl("txtgvRemarks")).Text.Trim().ToString();
                //string status = (((CheckBox)this.gvqcDeails.Rows[j].FindControl("ChckStatus")).Checked) ? "True" : "False";
                bool status = Convert.ToBoolean( ((DropDownList)this.gvqcDeails.Rows[j].FindControl("ddlPassFail")).SelectedValue.ToString() == "1" ? "true" : "false");
               
                tbl1.Rows[j]["chckmethod"] = method;
                tbl1.Rows[j]["unit"] = unit;
                tbl1.Rows[j]["qcqty"] = qcqty;
                tbl1.Rows[j]["passqty"] = passqty;
                tbl1.Rows[j]["chckdetails"] = check_details;
                tbl1.Rows[j]["finding"] = findings;
                tbl1.Rows[j]["remarks"] = remarks;
                tbl1.Rows[j]["qcstatus"] = status;

            }


            ViewState["tblqcdata"] = tbl1;
        }

        protected void gvqcDeails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList method = (DropDownList)e.Row.FindControl("ddlcheckmethod");
                method.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "chckmethod"));
                //CheckBox qcstatus = (CheckBox)e.Row.FindControl("ChckStatus");
                //qcstatus.Checked = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qcstatus")).ToString() == "True") ? true : false;

                DropDownList ddlstatus = ((DropDownList)e.Row.FindControl("ddlPassFail"));
                ddlstatus.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qcstatus"));

            }
        }
    
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintMatInspctRpt();
        }

        private void PrintMatInspctRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = this.TxtQcDate.Text;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string lcrcvno = this.ddlrcvno.SelectedValue;
            string chnlno = this.txtchnlno.Text.Trim();
            string LcCode = this.Request.QueryString["actcode"].ToString();
            string centrid = this.Request.QueryString["centrid"].ToString();
            

            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "IQCDETAILSINFO", lcrcvno, chnlno, LcCode, centrid, "", "", "", "", "");

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.BO_AllLCInfo.RptMatInspctReport>();

            LocalReport rpt1 = new LocalReport();

            string prntType = this.ddlReportLevel.SelectedValue;
            if (prntType == "1")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIncomingMatInspctFrmt1", list, null, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("rptTitle", "Incoming Materials Inspection Report"));
                rpt1.SetParameters(new ReportParameter("rptType", "Leather/Non-leather"));
                rpt1.SetParameters(new ReportParameter("notabene", "NB: QA Team is responsible for ensure quality only for new incoming leather/non-leather materials. Not for stock. if any stock Material issue with mixing new material; then store responsible personnel should ensure the similar material are issuing in one order."));
            }
            else if (prntType == "2")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIncomingMatInspctFrmt2", list, null, null);
                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("rptTitle", "Incoming Materials Inspection Report"));
                rpt1.SetParameters(new ReportParameter("rptType", "Label & Hang Tag"));
            }
            else if (prntType == "3")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIncomingMatInspctFrmt3", list, null, null);
                rpt1.EnableExternalImages = true;

                string chalanno = ds1.Tables[0].Rows[0]["chalanno"].ToString();
                string rcvdate = ds1.Tables[0].Rows[0]["rcvdate"].ToString();
                string suppliername = ds1.Tables[0].Rows[0]["ssirdesc"].ToString();

                rpt1.SetParameters(new ReportParameter("rptTitle", "Outsole Inspection Report"));
                rpt1.SetParameters(new ReportParameter("notabene", "Note: Total Problem Qnty :-"));
                rpt1.SetParameters(new ReportParameter("chalanno", chalanno));
                rpt1.SetParameters(new ReportParameter("rcvdate", rcvdate));
                rpt1.SetParameters(new ReportParameter("supplier", suppliername));
                rpt1.SetParameters(new ReportParameter("lc", lcrcvno));
            }
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("date", date));
            
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void LbtnRecItemCalculate_Click(object sender, EventArgs e)
        {
            ViewState["AdjustClicked"] = true;
            LbtnToClear_Click(null, null);
            this.Data_Bind();
        }

        private void SaveReciveItem()
        {
            var listsum = (List<SumReceiveItems>)Session["TblReceiveSum"];

            for (int i = 0; i < this.gvRecItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRecItem.Rows[i].FindControl("lgvISRecqty")).Text.Trim()));
                double qcQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRecItem.Rows[i].FindControl("lgvSRecBalqty")).Text.Trim()));
                string unit =  ((TextBox)this.gvRecItem.Rows[i].FindControl("lblgvQcUnit")).Text.Trim().ToString();
                string chckdetails = ((TextBox)this.gvRecItem.Rows[i].FindControl("lblgvchckdetails")).Text.Trim().ToString();
                string finding = ((TextBox)this.gvRecItem.Rows[i].FindControl("lblgvFindings")).Text.Trim().ToString();
                string remarks = ((TextBox)this.gvRecItem.Rows[i].FindControl("lblgvQcRemarks")).Text.Trim().ToString();
                string qcstatus = ((DropDownList)this.gvRecItem.Rows[i].FindControl("ddlQcStatus")).SelectedValue.Trim().ToString();
                string chekmethod = ((DropDownList)this.gvRecItem.Rows[i].FindControl("ddlQccheckmethod")).SelectedValue.Trim().ToString();                             

                listsum[i].passqty = Qty;
                listsum[i].qcqty = qcQty;
                listsum[i].unit = unit;
                listsum[i].chckdetails = chckdetails;
                listsum[i].chckmethod = chekmethod;
                listsum[i].finding = finding;
                listsum[i].remarks = remarks;
                listsum[i].qcstatus = qcstatus;

                if (Qty == 0)
                    continue;

                var list = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];

                foreach (var item in list.FindAll(r => r.rescod + r.spcfcod == listsum[i].rescod + listsum[i].spcfcod).ToList())
                {

                    if (item.remqty < Qty)
                    {
                        item.qcqty = item.remqty;
                        Qty = Qty - item.remqty;
                    }
                    else
                    {
                        item.qcqty = Qty;
                        Qty = 0;
                        break;
                    }
                }


                ViewState["TblReceive"] = list;

            }
            Session["TblReceiveSum"] = listsum;
        }

        protected void gvRecItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList archivebtn = (DropDownList)e.Row.FindControl("ddlQccheckmethod");
                archivebtn.SelectedValue =(DataBinder.Eval(e.Row.DataItem, "chckmethod")).ToString();

                DropDownList ddlqcsatus = (DropDownList)e.Row.FindControl("ddlQcStatus");
                ddlqcsatus.SelectedValue = (DataBinder.Eval(e.Row.DataItem, "qcstatus")).ToString();
            }
        }
        protected void LbtnToClear_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_09_Commer.EClassLCMGT> list = (List<SPEENTITY.C_09_Commer.EClassLCMGT>)ViewState["TblReceive"];
            foreach (var item in list)
            {
                item.qcqty = 0;
            }
            ViewState["TblReceive"] = list;

            if ((bool)ViewState["AdjustClicked"])
            {
                this.SaveReciveItem();
            }

            this.Data_Bind();

            ViewState["AdjustClicked"] = false;
        }
    }

    class SumReceiveItems
    {
        public string rescod { get; set; }
        public string resdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string unit { get; set; }
        public double rcvqty { get; set; }
        public double passqty { get; set; }
        public double qcqty { get; set; }
        public string chckdetails { get; set; }

        public string finding { get; set; }

        public string remarks { get; set; }

        public string chckmethod { get; set; }
        public string qcstatus { get; set; }




    }
}