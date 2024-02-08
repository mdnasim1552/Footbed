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

namespace SPEWEB.F_10_Procur
{
    public partial class PurProcessEdit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = " Purchase Resource Edit ";
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.CommonButton();
                this.GetReqno01();
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
            DataTable dt = (DataTable)ViewState["tblpurchase"];
            string mreqno = this.ddlReqNo01.SelectedValue.ToString();
            bool result = false;
            foreach (DataRow dr2 in dt.Rows)

            {
                string reqno = dr2["reqno"].ToString();
                string rsircode = dr2["rsircode"].ToString();
                //string rsirdesc = dr2["rsirdesc"].ToString();
                string spcfcod = dr2["spcfcod"].ToString();
                string bomid = dr2["bomid"].ToString();
                double qty = Convert.ToDouble(dr2["qty"].ToString());
                string rate = Convert.ToDecimal(dr2["rate"]).ToString();
                string srate = Convert.ToDecimal(dr2["srate"]).ToString();
                string ssircode = dr2["ssircode"].ToString();
                string spcfcodo = dr2["spcfcodo"].ToString();
                string ssircodeo = dr2["ssircodeo"].ToString();
                string rsircodeo = dr2["rsircodeo"].ToString();
                string aprovno = dr2["aprovno"].ToString();
                string mrrno = dr2["mrrno"].ToString();
                string orderno = dr2["orderno"].ToString();




                result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "INSERTUPDATEWRKORDER", reqno, rsircodeo, spcfcod, qty.ToString(), rate, srate, ssircode, spcfcodo, ssircodeo, mreqno, aprovno, mrrno, orderno, bomid, rsircode, "", "", "", "", "", "");

                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


            }
            if (result == true && ConstantInfo.LogStatus == true)
            {
                string eventtype = "Requisition Force Edit";
                string eventdesc = "Update Reqisition From Force Edit";
                string eventdesc2 = "Req No- " + mreqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            // this.GetRateQtyChangeMsg();

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
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
        //protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        //{
        //    this.GetReqno01();
        //}

        private void GetReqno01()
        {

            Session.Remove("tblreq");
            string comcod = this.GetCompCode();
            string pactcode = "000000000000";
            string date = this.txtCurReqDate.Text;
            string Mrfno = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
            this.ddlReqNo01.DataTextField = "reqno1";
            this.ddlReqNo01.DataValueField = "reqno";
            this.ddlReqNo01.DataSource = ds1.Tables[0];
            this.ddlReqNo01.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SHOWREQINFORMATION", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.gvpurorder.DataSource = null;
                this.gvpurorder.DataBind();
                return;
            }
            ViewState["tblpurchase"] = this.HiddenSameData(ds1.Tables[0]);

            lblvalvounum.Text = ds1.Tables[1].Rows[0]["billvounum"].ToString();
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string grp = dt1.Rows[0]["grp"].ToString();
            string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                    dt1.Rows[j]["grpdesc"] = "";

                grp = dt1.Rows[j]["grp"].ToString();

            }
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblpurchase"];

            if (dt.Rows.Count == 0)
            {
                return;
            }

            this.gvpurorder.DataSource = dt;
            this.gvpurorder.DataBind();

            //((LinkButton)this.gvpurorder.FooterRow.FindControl("lbtnUpdate")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000");
            //this.gvpurorder.Columns[9].Visible= (this.lblvalvounum.Text.Trim() == "00000000000000");



        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblpurchase"];
            for (int i = 0; i < gvpurorder.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvreqty01")).Text.Trim());
                double srate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvsuprate")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvAppRate01")).Text.Trim());
                tbl1.Rows[i]["qty"] = qty;
                tbl1.Rows[i]["srate"] = srate;
                tbl1.Rows[i]["rate"] = rate;
                tbl1.Rows[i]["amt"] = qty * rate;
            }

            ViewState["tblpurchase"] = tbl1;
        }


       
        protected void gvpurorder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvpurorder.EditIndex = -1;
            this.Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SetPosition()", true);
        }
        protected void gvpurorder_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvpurorder.EditIndex = e.NewEditIndex;
            this.Data_Bind();


            // Supplier3
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%%";
            string mResCode = ((Label)this.gvpurorder.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            string mSupCode = ((Label)this.gvpurorder.Rows[e.NewEditIndex].FindControl("lblgvResCod1")).Text.Trim();
            string mSpcfCod = ((Label)this.gvpurorder.Rows[e.NewEditIndex].FindControl("lblgvSpcfCod")).Text.Trim();


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count != 0)
            {

                DropDownList ddl1 = (DropDownList)this.gvpurorder.Rows[e.NewEditIndex].FindControl("ddlSupname");
                ddl1.DataTextField = "ssirdesc1";
                ddl1.DataValueField = "ssircode";
                ddl1.DataSource = ds1.Tables[0];
                ddl1.DataBind();
                ddl1.SelectedValue = mSupCode;
            }
            // Specification

            DropDownList ddlspeci = (DropDownList)this.gvpurorder.Rows[e.NewEditIndex].FindControl("ddlspecification");
            ddlspeci.DataTextField = "spcfdesc";
            ddlspeci.DataValueField = "spcfcod";
            ddlspeci.DataSource = ds1.Tables[1];
            ddlspeci.DataBind();
            ddlspeci.SelectedValue = mSpcfCod;


            //Materials
            DropDownList ddlmatn = (DropDownList)this.gvpurorder.Rows[e.NewEditIndex].FindControl("ddlmaterialname");
            ddlmatn.DataTextField = "rsirdesc";
            ddlmatn.DataValueField = "rsircode";
            ddlmatn.DataSource = ds1.Tables[2];
            ddlmatn.DataBind();
            ddlmatn.SelectedValue = mResCode;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SetPosition()", true);

        }
        protected void gvpurorder_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataTable tbl1 = (DataTable)ViewState["tblpurchase"];

            string spcfcod = (((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedValue.ToString().Length == 0) ? "000000000000" : ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedValue.ToString();
            string spcfdesc = (spcfcod == "000000000000") ? "" : ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedItem.Text.Trim();

            string mSSIRCODE = (((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString().Length == 0) ? "000000000000" : ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString();
            string mSSIRDesc = (mSSIRCODE == "000000000000") ? "" : ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedItem.Text.Trim();

            string msircode = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlmaterialname")).SelectedValue.ToString();
            string msirdesc = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlmaterialname")).SelectedItem.Text.Trim();
            // string mAPROVQTY = Convert.ToDouble("0" + ((TextBox)this.gvAprovInfo.Rows[e.RowIndex].FindControl("txtgvNewOrderQty")).Text.Trim()).ToString();

            //  string mAPROVRATE = Convert.ToDouble("0" + ((TextBox)this.gvAprovInfo.Rows[e.RowIndex].FindControl("txtgvNewApprovRate")).Text.Trim()).ToString();
            // string mAPROVRATE = Convert.ToDouble("0" + ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lgvNewApprovRate")).Text.Trim()).ToString();
            int index = (this.gvpurorder.PageIndex) * this.gvpurorder.PageSize + e.RowIndex;

            double qty = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvreqty01")).Text.Trim());
            double srate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvsuprate")).Text.Trim());
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvAppRate01")).Text.Trim());

            if (UpdateAllRow.Checked)
            {
                var aSpcfcod = tbl1.Rows[index]["spcfcod"].ToString();
                var aRsircode = tbl1.Rows[index]["rsircode"].ToString();
                var abomid = tbl1.Rows[index]["bomid"].ToString();
                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    var sSpcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                    var sRsircode = tbl1.Rows[i]["rsircode"].ToString();
                    var sbomid = tbl1.Rows[i]["bomid"].ToString();
                    if (aSpcfcod == sSpcfcod && aRsircode == sRsircode && abomid == sbomid)
                    {
                        tbl1.Rows[i]["spcfcod"] = spcfcod;
                        tbl1.Rows[i]["spcfdesc"] = spcfdesc;
                        tbl1.Rows[i]["ssircode"] = mSSIRCODE;
                        tbl1.Rows[i]["ssirdesc"] = mSSIRDesc;
                        tbl1.Rows[i]["rsircode"] = msircode;
                        tbl1.Rows[i]["rsirdesc"] = msirdesc;


                        tbl1.Rows[i]["qty"] = qty;
                        tbl1.Rows[i]["srate"] = srate;
                        tbl1.Rows[i]["rate"] = rate;
                        tbl1.Rows[i]["amt"] = qty * rate;

                        ViewState["tblpurchase"] = tbl1;
                        this.gvpurorder.EditIndex = -1;
                        this.Data_Bind();
                    }
                }
            }
            else
            {
                tbl1.Rows[index]["spcfcod"] = spcfcod;
                tbl1.Rows[index]["spcfdesc"] = spcfdesc;
                tbl1.Rows[index]["ssircode"] = mSSIRCODE;
                tbl1.Rows[index]["ssirdesc"] = mSSIRDesc;
                tbl1.Rows[index]["rsircode"] = msircode;
                tbl1.Rows[index]["rsirdesc"] = msirdesc;


                tbl1.Rows[index]["qty"] = qty;
                tbl1.Rows[index]["srate"] = srate;
                tbl1.Rows[index]["rate"] = rate;
                tbl1.Rows[index]["amt"] = qty * rate;

                ViewState["tblpurchase"] = tbl1;
                this.gvpurorder.EditIndex = -1;
                this.Data_Bind();
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SetPosition()", true);
        }
        protected void gvpurorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //LinkButton btnDelMat = (LinkButton)e.Row.FindControl("btnDelMat");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {




                string grpdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpdesc")).ToString().Trim();
                string genno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "genno")).ToString().Trim();

                if (genno.Substring(0, 3) != "REQ")
                {

                    //btnDelMat.Visible = false;
                }


                if (grpdesc == "")
                    return;

                if (grpdesc == "1. Requisition" || grpdesc == "2. Market Survey" || grpdesc == "3. Purchase Order" || grpdesc == "4. Materials Received" || grpdesc == "5. Bill Confirmation" || grpdesc == "7. Lc Opening" || grpdesc == "8. Lc Recieved")
                {
                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";


                }




            }


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink hlnkgvdesc = (HyperLink)e.Row.FindControl("hlnkgvdesc");



            //    string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();

            //    if (Code == "")
            //        return;

            //    if (ASTUtility.Right(Code, 3)!= "000")
            //    {
            //        //hlnkgvdesc.Style.Add("color", "blue");
            //        string sirdesc= Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();
            //        hlnkgvdesc.NavigateUrl = "~/F_17_Acc/LinkSpecificCodeBook.aspx?sircode=" + Code + "&sirdesc=" + sirdesc;



            //    }




            //}
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
        protected void ddlmaterialname_SelectedIndexChanged(object sender, EventArgs e)
        {
            string suppSirCode = "";
            string speciSirCode = "";
            DropDownList ddlgval;
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            int index = row.RowIndex;
            DataView dv1;
            try
            {
                string rsircode = ((DropDownList)this.gvpurorder.Rows[index].FindControl("ddlmaterialname")).SelectedValue.ToString();

                //Materials
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mSrchTxt = "%%";


                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETSlECTEDSUPLIST", mSrchTxt, rsircode, "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    ((DropDownList)this.gvpurorder.Rows[index].FindControl("ddlSupname")).Items.Clear();
                    ((DropDownList)this.gvpurorder.Rows[index].FindControl("ddlspecification")).Items.Clear();
                    ((DropDownList)this.gvpurorder.Rows[index].FindControl("ddlSupname")).DataBind();
                    ((DropDownList)this.gvpurorder.Rows[index].FindControl("ddlspecification")).DataBind();

                    return;
                }
                else
                {
                    DataTable dtsupp = ds1.Tables[0];

                    DataTable dtspcific = ds1.Tables[1];

                    if (suppSirCode != rsircode)
                    {
                        suppSirCode = rsircode;
                        dv1 = dtsupp.DefaultView;
                        // dv1.RowFilter = ("ssircode =" + suppSirCode);
                        ddlgval = ((DropDownList)this.gvpurorder.Rows[index].FindControl("ddlSupname"));
                        ddlgval.DataTextField = "ssirdesc";
                        ddlgval.DataValueField = "ssircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();

                    }

                    if (speciSirCode != rsircode)
                    {
                        speciSirCode = rsircode;
                        dv1 = dtspcific.DefaultView;
                        // dv1.RowFilter = ("spcfcod =" + speciSirCode);
                        ddlgval = ((DropDownList)this.gvpurorder.Rows[index].FindControl("ddlspecification"));
                        ddlgval.DataTextField = "spcfdesc";
                        ddlgval.DataValueField = "spcfcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();

                    }
                }


            }
            catch (Exception ex)
            {
                //throw ex;
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Servey First!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                //return;
            }

        }
    }
}