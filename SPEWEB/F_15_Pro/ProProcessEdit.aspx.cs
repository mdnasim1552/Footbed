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

namespace SPEWEB.F_15_Pro
{
    public partial class ProProcessEdit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = " Production Process Edit ";
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.CommonButton();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFiUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);


        }

        private void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblProddata"];
            string mreqno = this.ddlReqNo01.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            foreach (DataRow dr2 in dt.Rows)

            {
                string ppnno = dr2["ppnno"].ToString();
                string orderno = dr2["orderno"].ToString();
                string styleid = dr2["styleid"].ToString();
                string colorid = dr2["colorid"].ToString();
                string sizeid = dr2["sizeid"].ToString();
                double qty = Convert.ToDouble(dr2["qty"].ToString());
                string rejectionqty = Convert.ToDouble(dr2["rejectionqty"].ToString()).ToString();
                string repairqty = Convert.ToDouble(dr2["repairqty"].ToString()).ToString();
                string fprostep = dr2["fprostep"].ToString();
                string tprostep = dr2["tprostep"].ToString();



                bool result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PRODPROCESS", "FORCE_UPDATE_PRODUCTION_PROCESS_INFO", mreqno, ppnno, orderno, styleid, colorid, sizeid, qty.ToString(), rejectionqty, repairqty, fprostep, tprostep, userid, Posteddat, "", "", "", "", "");

                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                if (result == true && ConstantInfo.LogStatus == true)
                {

                    string eventtype = "Production Force Edit";
                    string eventdesc = "Update Production From Force Edit";
                    string eventdesc2 = "Pro Req No- " + mreqno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                }

            }

           // this.GetRateQtyChangeMsg();

           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private void CommonButton()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("AlertArea")).Visible = false;
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
        protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        {
            this.GetReqno01();
        }

        private void GetReqno01()
        {

            Session.Remove("tblreq");
            string comcod = this.GetCompCode();

            string date = this.txtCurReqDate.Text;
            string Mrfno = "%" + this.txtSrcRequisition01.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GET_ALL_PROD_REQ_NO", Mrfno, "", "", "", "", "", "");
            this.ddlReqNo01.DataTextField = "reqno1";
            this.ddlReqNo01.DataValueField = "reqno";
            this.ddlReqNo01.DataSource = ds1.Tables[0];
            this.ddlReqNo01.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblProddata");
            string comcod = this.GetCompCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GET_PRODUCTION_PROCESS_INFO", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.gvProdProess.DataSource = null;
                this.gvProdProess.DataBind();
                return;
            }
            ViewState["tblProddata"] = this.HiddenSameData(ds1.Tables[0]);

            // lblvalvounum.Text = ds1.Tables[1].Rows[0]["billvounum"].ToString();
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string ppnno = dt1.Rows[0]["ppnno"].ToString();
            string grpdesc = dt1.Rows[0]["ppndesc"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ppnno"].ToString() == ppnno)
                    dt1.Rows[j]["ppndesc"] = "";

                ppnno = dt1.Rows[j]["ppnno"].ToString();

            }
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblProddata"];

            if (dt.Rows.Count == 0)
            {
                return;
            }

            this.gvProdProess.DataSource = dt;
            this.gvProdProess.DataBind();




        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblProddata"];
            for (int i = 0; i < gvProdProess.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvProdProess.Rows[i].FindControl("txtgvreqty01")).Text.Trim());
                double rejectionqty = Convert.ToDouble("0" + ((TextBox)this.gvProdProess.Rows[i].FindControl("txtgvRejection")).Text.Trim());
                double repairqty = Convert.ToDouble("0" + ((TextBox)this.gvProdProess.Rows[i].FindControl("txtgvRepairQty")).Text.Trim());

                tbl1.Rows[i]["qty"] = qty;
                tbl1.Rows[i]["rejectionqty"] = rejectionqty;
                tbl1.Rows[i]["repairqty"] = repairqty;
            }

            ViewState["tblProddata"] = tbl1;
        }

        protected void gvProdProess_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvProdProess.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvProdProess_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvProdProess.EditIndex = e.NewEditIndex;
            this.Data_Bind();



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string fromprocess = ((Label)this.gvProdProess.Rows[e.NewEditIndex].FindControl("lblFromProcessCode")).Text.Trim();
            string toprocesscode = ((Label)this.gvProdProess.Rows[e.NewEditIndex].FindControl("lblToProcessCode")).Text.Trim();


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DropDownList ddl1 = (DropDownList)this.gvProdProess.Rows[e.NewEditIndex].FindControl("ddlFromProcess");
            ddl1.DataTextField = "prodesc";
            ddl1.DataValueField = "procode";
            ddl1.DataSource = ds1.Tables[0];
            ddl1.DataBind();
            ddl1.SelectedValue = fromprocess;

            // Specification

            DropDownList ddlspeci = (DropDownList)this.gvProdProess.Rows[e.NewEditIndex].FindControl("ddlCurProces");
            ddlspeci.DataTextField = "prodesc";
            ddlspeci.DataValueField = "procode";
            ddlspeci.DataSource = ds1.Tables[0];
            ddlspeci.DataBind();
            ddlspeci.SelectedValue = toprocesscode;
        }
        protected void gvProdProess_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataTable tbl1 = (DataTable)ViewState["tblProddata"];

            string fprostep = ((DropDownList)this.gvProdProess.Rows[e.RowIndex].FindControl("ddlFromProcess")).SelectedValue.ToString();
            string fromprocess = ((DropDownList)this.gvProdProess.Rows[e.RowIndex].FindControl("ddlFromProcess")).SelectedItem.Text.Trim();

            string toprocess = ((DropDownList)this.gvProdProess.Rows[e.RowIndex].FindControl("ddlCurProces")).SelectedValue.ToString();
            string toprocessdesc = ((DropDownList)this.gvProdProess.Rows[e.RowIndex].FindControl("ddlCurProces")).SelectedItem.Text.Trim();
            int index = (this.gvProdProess.PageIndex) * this.gvProdProess.PageSize + e.RowIndex;

            double qty = Convert.ToDouble("0" + ((TextBox)this.gvProdProess.Rows[e.RowIndex].FindControl("txtgvreqty01")).Text.Trim());
            double rejecttion = Convert.ToDouble("0" + ((TextBox)this.gvProdProess.Rows[e.RowIndex].FindControl("txtgvRejection")).Text.Trim());
            double repairqty = Convert.ToDouble("0" + ((TextBox)this.gvProdProess.Rows[e.RowIndex].FindControl("txtgvRepairQty")).Text.Trim());


            tbl1.Rows[index]["fprostep"] = fprostep;
            tbl1.Rows[index]["fromprocess"] = fromprocess;
            tbl1.Rows[index]["tprostep"] = toprocess;
            tbl1.Rows[index]["toprocess"] = toprocessdesc;


            tbl1.Rows[index]["qty"] = qty;
            tbl1.Rows[index]["rejectionqty"] = rejecttion;
            tbl1.Rows[index]["repairqty"] = repairqty;
            //tbl1.Rows[index]["amt"] = qty * rate;

            ViewState["tblProddata"] = tbl1;
            this.gvProdProess.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvProdProess_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {





                string sizeid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sizeid")).ToString().Trim();

                if (sizeid.Substring(0, 3) != "REQ")
                {

                    //btnDelMat.Visible = false;
                }

                e.Row.ForeColor = this.GetColorInfo(sizeid);





            }



        }

        public System.Drawing.Color GetColorInfo(string sizeid)
        {
            switch (sizeid)
            {
                case "720100101001":
                    return System.Drawing.Color.BlueViolet;
                    break;
                case "720100101002":
                    return System.Drawing.Color.ForestGreen;
                    break;
                case "720100101003":
                    return System.Drawing.Color.GreenYellow;
                    break;
                case "720100101004":
                    return System.Drawing.Color.DarkRed;
                    break;
                case "720100101005":
                    return System.Drawing.Color.LightPink;
                    break;
                case "720100101006":
                    return System.Drawing.Color.Chocolate;
                    break;
                case "720100101007":
                    return System.Drawing.Color.DeepPink;
                    break;
                case "720100101008":
                    return System.Drawing.Color.LightCoral;
                    break;
                case "720100101009":
                    return System.Drawing.Color.OrangeRed;
                    break;
                case "720100101010":
                    return System.Drawing.Color.LightSkyBlue;
                    break;
                case "720100101011":
                    return System.Drawing.Color.ForestGreen;
                    break;
                case "720100101012":
                    return System.Drawing.Color.Magenta;
                    break;
                case "720100101013":
                    return System.Drawing.Color.MediumSeaGreen;
                    break;
                case "720100101014":
                    return System.Drawing.Color.PaleGreen;
                    break;
                case "720100101015":
                    return System.Drawing.Color.Goldenrod;
                    break;
                default:
                    return System.Drawing.Color.LawnGreen;
                    break;
            }



        }
        protected void btnDelMat_Click(object sender, EventArgs e)
        {

            //string comcod = this.GetCompCode();
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";


            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            //if (!Convert.ToBoolean(dr1[0]["delete"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}

            //int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string reqno = ((DataTable)ViewState["tblpurchase"]).Rows[RowIndex]["reqno"].ToString();

            //string rsircode = ((DataTable)ViewState["tblpurchase"]).Rows[RowIndex]["rsircode"].ToString();
            //string spcfcod = ((DataTable)ViewState["tblpurchase"]).Rows[RowIndex]["spcfcod"].ToString();




            //bool result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMATPURCHASE", reqno, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            //if (!result)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
            //    return;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            //    lbtnOk_Click(null, null);
            //}


        }

        protected void gvProdProess_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblProddata"];
            string styleid = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            string colorid = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblgvResCod1")).Text.Trim();
            string fprostep = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblFromProcessCode")).Text.Trim();
            string tprostep = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblToProcessCode")).Text.Trim();
            string sizeid = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblgvSpcfCod")).Text.Trim();
            string preqno = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblprocessreqno")).Text.Trim();
            string ppnno = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblprocessppnno")).Text.Trim();
            string orderno = ((Label)this.gvProdProess.Rows[e.RowIndex].FindControl("lblprocessordrno")).Text.Trim();

            bool result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PRODPROCESS", "DELETE_PRODUCTION_PROCESS_Edit", styleid, colorid, fprostep, tprostep, sizeid, preqno, ppnno, orderno, "", "", "", "", "", "", "");
            if (!result)
                return;
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            this.XmlDataInsert(orderno, preqno, ds);

            int index = (this.gvProdProess.PageIndex) * this.gvProdProess.PageSize + e.RowIndex;
            dt.Rows[index].Delete();
            ViewState["tblProddata"] = dt;
            this.Data_Bind();

        }

        private string XmlDataInsert(string Reqno, string Apprno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            ds.Tables[0].Columns.Add("delbyid", typeof(string));
            ds.Tables[0].Columns.Add("delseson", typeof(string));
            ds.Tables[0].Columns.Add("deldate", typeof(DateTime));

            ds.Tables[0].Rows[0]["delbyid"] = usrid;
            ds.Tables[0].Rows[0]["delseson"] = session;
            ds.Tables[0].Rows[0]["deldate"] = Date;


            ds1.Merge(ds.Tables[0]);
            ds1.Tables[0].TableName = "tbl1";

            bool resulta = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno, Apprno);

            if (!resulta)
            {

                //return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Deleted";
                ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green;";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
    }


}