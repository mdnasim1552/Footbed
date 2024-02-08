using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_09_Commer
{
    public partial class RptPoShipmentLog : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "PO Shipment Log";
                CommonButton();
                GetData();
                Bind_GV_MatDesc();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnSaveMatDesc_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }


        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
        }

        private void GetData()
        {
            Session.Remove("tblpostatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string orderNo = Request.QueryString["orderNo"].ToString();
            string supCod = Request.QueryString["supCod"].ToString() == "000000000000" ? "%" : Request.QueryString["supCod"].ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GET_PO_DETAILS", supCod, orderNo);
            if (ds1 == null)
                return;
            Session["tblpostatus"] = ds1.Tables[0];
        }

        protected void Bind_GV_MatDesc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            var orderNo = Request.QueryString["orderNo"].ToString();
            DataTable dt = ((DataTable)Session["tblpostatus"]).Copy();
            string ssircode = dt.Rows[0]["ssircode"].ToString();
            string reqno = dt.Rows[0]["reqno"].ToString();
            string msrno = dt.Rows[0]["msrno"].ToString();

            if (orderNo.Substring(0, 3) == "POR")
                this.HyInkPrintPO.NavigateUrl = $"~/F_10_Procur/PuchasePrint?Type=OrderPrint&comcod={comcod}&orderno={orderNo}";
            else
                this.HyInkPrintPO.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ImportApp&comcod=" + comcod + "&reqno=" + reqno + "&supcode=" + ssircode + "&msrno=" + msrno + "&dayid=" + orderNo;

            this.GV_MatDesc.DataSource = dt;
            this.GV_MatDesc.DataBind();




            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GET_PO_SHIPMENT_HISTORY", orderNo);

            if (ds1 == null)
                return;



            dt = ((DataTable)ds1.Tables[0]).Copy();
            if (dt.Rows.Count > 0)
            {
                ViewState["tblShipmentHistory"] = ds1.Tables[0];

                var newDt = ds1.Tables[0].AsEnumerable()
                              .GroupBy(r => r.Field<string>("challanno"))
                              .Select(g =>
                              {
                                  var row = ds1.Tables[0].NewRow();

                                  row["challanno"] = g.Key;
                                  row["syspon"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["syspon"];
                                  row["comcod"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["comcod"];
                                  row["challandate"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["challandate"];
                                  row["expecteddeldate"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["expecteddeldate"];
                                  row["logno"] = "";
                                  row["rsircode"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["rsircode"];
                                  row["sirdesc"] = "Click to breakdown";
                                  row["spcfcod"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["spcfcod"];
                                  row["spcfdesc"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["spcfdesc"];
                                  row["shipqty"] = g.Where(r => r["challanno"] == g.Key.ToString()).Sum(r => r.Field<decimal>("shipqty"));
                                  row["flag"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["flag"];
                                  return row;
                              }).CopyToDataTable();

                ViewState["tblShipmentHistoryView"] = newDt;
                this.Gv_Data_Bind("gvShipmentHistory", newDt);
            }
            else
            {
                ViewState.Remove("tblShipmentHistoryView");
            }


        }

        protected void gvSHLbtnClick_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string challanno = ((Label)this.gvShipmentHistory.Rows[index].FindControl("gvSHLblchallanno")).Text.ToString();

            DataTable mainlist = (DataTable)ViewState["tblShipmentHistory"];
            DataTable viewlist = ((DataTable)ViewState["tblShipmentHistoryView"]).Copy();

            string flag = ((Label)gvShipmentHistory.Rows[index].FindControl("gvSHlblFlag")).Text.ToString();


            DataView dv = new DataView();
            dv = mainlist.DefaultView;
            dv.RowFilter = ("challanno ='" + challanno + "'");
            mainlist = dv.ToTable();
            if (flag == "")
                return;
            if (Convert.ToBoolean(flag) == false)
            {



                foreach (DataRow dr2 in viewlist.Rows)
                {
                    if (dr2["challanno"].ToString() != challanno)
                    {
                        dr2["flag"] = false;

                    }
                    else
                    {
                        dr2["flag"] = true;
                    }
                }


                foreach (DataRow dr in mainlist.Rows)
                {
                    DataRow drs = viewlist.NewRow();
                    drs["comcod"] = dr["comcod"].ToString();
                    drs["challanno"] = challanno;
                    drs["logno"] = dr["logno"].ToString();
                    drs["syspon"] = dr["syspon"].ToString();
                    drs["challandate"] = dr["challandate"].ToString();
                    drs["expecteddeldate"] = dr["expecteddeldate"].ToString();
                    drs["rsircode"] = dr["rsircode"].ToString();
                    drs["sirdesc"] = dr["sirdesc"].ToString();
                    drs["spcfdesc"] = dr["spcfdesc"].ToString();
                    drs["spcfcod"] = dr["spcfcod"].ToString();
                    drs["sirunit"] = dr["sirunit"].ToString();
                    drs["shipqty"] = dr["shipqty"].ToString();
                    drs["flag"] = dr["flag"];
                    viewlist.Rows.Add(drs);

                }


            }
            else
            {
                foreach (DataRow dr2 in viewlist.Rows)
                {
                    if (dr2["challanno"].ToString() == challanno)
                    {
                        dr2["flag"] = false;

                    }

                }
            }


            this.Gv_Data_Bind("gvShipmentHistory", viewlist);


        }

        private void Gv_Data_Bind(string gvname, DataTable datatable)
        {
            if (datatable == null)
                return;

            switch (gvname)
            {
                case "gvShipmentHistory":
                    bool flag = false;
                    if (datatable != null && datatable.Rows.Count > 0)
                        flag = datatable.AsEnumerable().Any(row => "True" == row.Field<String>("flag"));
                    else
                        return;

                    DataTable dt = datatable.AsEnumerable()
                    .OrderBy(r => r.Field<string>("challanno"))
                    .CopyToDataTable();
                    this.gvShipmentHistory.DataSource = dt;
                    this.gvShipmentHistory.DataBind();

                    if (flag)
                    {
                        this.gvShipmentHistory.Columns[5].Visible = true;
                        this.gvShipmentHistory.Columns[2].Visible = true;
                        //this.gvShipmentHistory.Columns[5].Visible = true;
                        this.gvShipmentHistory.Columns[8].Visible = true;
                        this.gvShipmentHistory.Columns[9].Visible = true;
                    }
                    else
                    {
                        this.gvShipmentHistory.Columns[5].Visible = false;
                        this.gvShipmentHistory.Columns[2].Visible = false;
                        //this.gvShipmentHistory.Columns[5].Visible = false;
                        this.gvShipmentHistory.Columns[8].Visible = false;
                        this.gvShipmentHistory.Columns[9].Visible = false;
                    }
                    break;
            }
        }

        protected void btnSaveMatDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string postedbyid = hst["usrid"].ToString();
            string postrmid = hst["compname"].ToString();
            string postseson = hst["session"].ToString();
            string challanNo = this.txtChallanNo.Text.Trim();
            string challanDate = this.txtChallanDate.Text.Trim();
            string expectedDelDate = this.txtExpDeliveryDate.Text.Trim();
            string orderno = Request.QueryString["orderNo"].ToString();
            string note = this.txtNote.Text.Trim();

            var ds = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GET_LOG_NO", orderno, challanNo);
            string maxlogno = ds.Tables[0].Rows[0]["maxlogno"].ToString();
            string chalanexist = ds.Tables[0].Rows[0]["chalanexist"].ToString();
            if (chalanexist == "Y")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Challan number already exist.');", true);
                return;
            }

            if (challanNo == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Challan number is required.');", true);
                return;
            }

            if (expectedDelDate == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Faild to save. Expected date is required.');", true);
                return;
            }


            foreach (GridViewRow row in GV_MatDesc.Rows)
            {
                string matcode = ((Label)row.Cells[4].FindControl("lblgvMatCode")).Text.Trim();
                string spcfcode = ((Label)row.Cells[6].FindControl("lblgvSpcfCode")).Text.Trim();
                string shipQty = ((TextBox)row.Cells[13].FindControl("txtShipQty")).Text.Trim();
                if (shipQty == "")
                    continue;
                string ssircode = ((Label)row.Cells[6].FindControl("lblgvmdssircode")).Text.Trim();


                var isSuccessful = MktData.UpdateTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SAVE_PO_SHIP_LOG", orderno, matcode, spcfcode, expectedDelDate, challanNo, challanDate, note, shipQty, maxlogno, ssircode, postedbyid, postrmid, postseson);
                //var isSuccessful = MktData.UpdateTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SAVE_ORDER_WISE_MATERIAL_LIST", orderno, matcode, spcfcode, expectedDelDate, challanNo, challanDate, expectedDelDate, note, shipQty);

                if (!isSuccessful)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
                    return;
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);
            Bind_GV_MatDesc();

        }

        protected void gvShipmentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "flag")) == true)
                {
                    e.Row.BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    ((LinkButton)e.Row.FindControl("gvSHLblsirdesc")).ForeColor = System.Drawing.Color.White;
                    ((LinkButton)e.Row.FindControl("lnkbtnDelconsum")).Visible = false;

                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lnkbtnDelconsum")).Visible = true;
                }
            }

        }

        protected void lnkbtnDelconsum_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int index = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string logno = ((Label)gvShipmentHistory.Rows[index].FindControl("gvlbllogno")).Text.ToString();
            string rsircode = ((Label)gvShipmentHistory.Rows[index].FindControl("gvlblrsircode")).Text.ToString();
            string spcfcod = ((Label)gvShipmentHistory.Rows[index].FindControl("gvlblspcfcod")).Text.ToString();

        
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "DELETE_SHIPMENT_ITEMS", logno, rsircode, spcfcod);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }
           
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Successfully');", true);
            Bind_GV_MatDesc();


        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintPOShipLog();
        }

        private void PrintPOShipLog()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)ViewState["tblShipmentHistory"];
            var list1 = dt.DataTableToList<SPEENTITY.C_09_Commer.BO_AllLCInfo.POShipLog>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptPOShipmentLog", list1, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", compadd));
            rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "PO Shipment Log"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(comnam, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}