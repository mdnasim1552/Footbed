using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccMonthlyBgd : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess _process = new ProcessAccess();

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

                ((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Budget Information View/Edit";

                this.txtCurDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                //this.txtCurDate.ReadOnly = true;
                this.lbtnOk.Text = "New";
                this.MultiView1.ActiveViewIndex = 0;
                this.GetLastBgdInfo();
                this.GetBudgetInfo();
                this.lbtnPrevBudget.Visible = false;
                this.ddlPrevBgdList.Visible = false;
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.lblmsg.Text = "";
            this.ddlPrevBgdList.Items.Clear();
            //this.txtCurDate.ReadOnly = false ;
            this.MultiView1.ActiveViewIndex = -1;
            this.lbtnPrevBudget.Visible = true;
            this.ddlPrevBgdList.Visible = true;


        }
        private void GetBudgetInfo()
        {
            Session.Remove("AccTbl01");
            string comcod = this.GetCompCode();
            this.txtCurDate.Text = (this.ddlPrevBgdList.Items.Count > 0) ? Convert.ToDateTime(this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(12)).ToString("dd-MMM-yyyy") : System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.lblCurBgdNo1.Text = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(0, 6) : this.lblCurBgdNo1.Text;
            this.txtCurBgdNo2.Text = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(6, 5) : this.txtCurBgdNo2.Text;
            string mBGDNo = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedValue.ToString() : this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtCurDate.Text.Trim(), 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim(); ;

            string filter = this.txtFilter.Text.Trim() + "%";
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBUDGETINFO", mBGDNo, filter,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["AccTbl01"] = ds1.Tables[0];
            this.dgv2_DataBind();
            this.TotalCalculation1();

            //this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(0, 6);
            //this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(6, 5);
            //this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            //this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            //this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            //this.txtISSNarr.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
            //this.grvissue_DataBind();



        }

        private string GetLastBgdInfo()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETLASTBGDNO", CurDate1, "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count > 0)
            {

                this.lblCurBgdNo1.Text = ds1.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(0, 6);
                this.txtCurBgdNo2.Text = ds1.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(6, 5);
            }

            return ds1.Tables[0].Rows[0]["maxbgdno"].ToString();

        }

        protected void lbtnPrevBudget_Click(object sender, EventArgs e)
        {
            this.GetPreBgdNum();
        }
        private void GetPreBgdNum()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPREBGDNUM", CurDate1, "", "", "", "", "", "", "", "");
            this.ddlPrevBgdList.DataTextField = "bgdnum1";
            this.ddlPrevBgdList.DataValueField = "bgdnum";
            this.ddlPrevBgdList.DataSource = ds1.Tables[0];
            this.ddlPrevBgdList.DataBind();
            ds1.Dispose();



        }

        protected void ibtnAccCode_Click(object sender, EventArgs e)
        {
            this.GetBudgetInfo();
        }





        private void SessionUpdate()
        {

            DataTable tblt01 = (DataTable)Session["AccTbl01"];
            int TblRowIndex;

            for (int i = 0; i < this.dgv2.Rows.Count; i++)
            {
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                //string dgvTrnRemarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                TblRowIndex = (dgv2.PageIndex) * dgv2.PageSize + i;

                tblt01.Rows[TblRowIndex]["Dr"] = dgvTrnDrAmt;
                tblt01.Rows[TblRowIndex]["Cr"] = dgvTrnCrAmt;
                //  tblt01.Rows[TblRowIndex]["Remarks"] = dgvTrnRemarks;
            }
            Session["AccTbl01"] = tblt01;
        }
        private void SessionUpdate2()
        {

            DataTable tblt02 = (DataTable)Session["AccTbl02"];
            int TblRowIndex2;

            for (int j = 0; j < this.dgv3.Rows.Count; j++)
            {
                double dgvTrnRate;
                double dgvTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtQty")).Text.Trim()));
                // double dgvTrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtRate")).Text.Trim()));
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text.Trim()));
                if (dgvTrnDrAmt == 0 && dgvTrnCrAmt == 0)
                {
                    dgvTrnRate = 0;
                }
                else
                {
                    dgvTrnRate = (dgvTrnQty == 0 ? 0.00 : (dgvTrnDrAmt + dgvTrnCrAmt) / dgvTrnQty);
                }
                ((Label)this.dgv3.Rows[j].FindControl("gvlblRate")).Text = dgvTrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text = dgvTrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text = dgvTrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                TblRowIndex2 = (dgv3.PageIndex) * dgv3.PageSize + j;
                tblt02.Rows[TblRowIndex2]["qty"] = dgvTrnQty;
                tblt02.Rows[TblRowIndex2]["rate"] = dgvTrnRate;
                tblt02.Rows[TblRowIndex2]["Dr"] = dgvTrnDrAmt;
                tblt02.Rows[TblRowIndex2]["Cr"] = dgvTrnCrAmt;

            }
            Session["AccTbl02"] = tblt02;

            this.dgv3_DataBind();
        }
        protected void dgv2_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "this.className='normalrow'");

                //e.Row.Attributes.Add("onmouseover", "this.className='highlightrow'");
                //e.Row.Attributes.Add("onmouseout", "this.className='normalrow'");
            }

        }
        protected void gvlnkLevel_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 1;
            Session.Remove("AccTbl02");
            this.SessionUpdate();
        }
        private void ShowResource()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string acccode01 = this.txtActcode.Text.Trim().Substring(0, 12);
            string filter2 = this.txtResSearch.Text.Trim() + "%";
            string voudat = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            string bgdnum = this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(voudat, 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim();
            DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBGDRES", filter2, acccode01, bgdnum, "", "", "", "", "", "");
            Session["AccTbl02"] = ds2.Tables[0];
            this.dgv3_DataBind();
        }

        protected void gvlnkFTotal_Click(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        private void TotalCalculation2()
        {
            this.SessionUpdate2();

        }
        protected void ibtnDetailsCode_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowResource();
        }


        protected void dgv2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session.Remove("RowIndex");
            if (e.CommandName == "")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                if (row.RowType.ToString() == "DataRow")
                {
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    int RowIndex = gvr.RowIndex;
                    Session["RowIndex"] = RowIndex;
                    this.ShowActCode();
                    gvr.BackColor = System.Drawing.Color.Blue;

                }
            }
        }
        private void ShowActCode()
        {
            int rowin = (int)Session["RowIndex"];
            int rowin1 = (dgv2.PageIndex * dgv2.PageSize) + rowin;
            this.txtActcode.Text = ((Label)this.dgv2.Rows[rowin].FindControl("lblAccdesc")).Text;
            this.ShowResource();
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {

            this.MultiView1.ActiveViewIndex = 0;
            this.GetBudgetInfo();
            this.lblmsg.Text = "";
        }
        private void UpdateTable02()
        {
            this.SessionUpdate2();
            string comcod = this.GetCompCode();
            string voudat = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string bgdnum = this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(voudat, 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim();
            string actcode = this.txtActcode.Text.Trim().Substring(0, 12);
            DataTable tblt03 = (DataTable)Session["AccTbl02"];

            for (int i = 0; i < tblt03.Rows.Count; i++)
            {
                string rescode = tblt03.Rows[i]["rescode"].ToString();
                string bgdqty = tblt03.Rows[i]["qty"].ToString();
                double Dramt = Convert.ToDouble(tblt03.Rows[i]["Dr"]);
                double Cramt = Convert.ToDouble(tblt03.Rows[i]["Cr"]);
                string bgdamt = Convert.ToString(Dramt - Cramt);


                //if ((Dramt - Cramt) != 0)
                //{
                bool resulta = _process.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSERTORUPBGDINF", bgdnum, actcode,
                        rescode, voudat, bgdqty, bgdamt, Dramt.ToString(), "", "", "", "", "", "", "", "");
                if (!resulta)
                {
                    this.lblmsg.Text = _process.ErrorObject["Msg"].ToString();
                    return;
                }
                //}     
            }

            this.lblmsg.Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Monthly Budget";
                string eventdesc = "Update Details Budget";
                string eventdesc2 = bgdnum;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            //---------------Check Dr. and Cr------//
            this.TotalCalculation1();
            DataTable tblt07 = (DataTable)Session["AccTbl01"];
            for (int i = 0; i < tblt07.Rows.Count; i++)
            {
                double Dramt01 = Convert.ToDouble(tblt07.Rows[i]["Dr"]);
                double Cramt01 = Convert.ToDouble(tblt07.Rows[i]["Cr"]);
                if (Dramt01 > 0 && Cramt01 > 0)
                {
                    this.lblmsg.Text = "Choose Only Dr. Or Cr. Amount.";
                    return;
                }
                //else
                //{


                //}
            }
            //-------------------------//
            //this.TotalCalculation1();
            this.UpdateTable01();


        }
        private void UpdateTable01()
        {
            try
            {

                string comcod = this.GetCompCode();
                string voudat = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string bgdnum = this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(voudat, 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim();
                string rescode = "000000000000";
                string bgdqty = "0";


                DataTable tblt05 = (DataTable)Session["AccTbl01"];
                for (int i = 0; i < tblt05.Rows.Count; i++)
                {
                    string actcode = tblt05.Rows[i]["actcode"].ToString();
                    string actlev = tblt05.Rows[i]["actelev"].ToString();
                    double Dramt = Convert.ToDouble(tblt05.Rows[i]["Dr"]);
                    double Cramt = Convert.ToDouble(tblt05.Rows[i]["Cr"]);
                    string bgdamt = Convert.ToString(Dramt - Cramt);
                    //if ((Dramt - Cramt) != 0 && actlev != "2")
                    if (actlev != "2")
                    {

                        bool resulta = _process.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSERTORUPBGDINF", bgdnum, actcode, rescode, voudat, bgdqty,
                                 bgdamt, Dramt.ToString(), "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            this.lblmsg.Text = _process.ErrorObject["Msg"].ToString();
                            return;
                        }

                    }
                }
                this.lblmsg.Text = "Updated Successfully";
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Monthly Budget";
                    string eventdesc = "Update Budget";
                    string eventdesc2 = bgdnum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception e)
            {
                this.lblmsg.Text = "Error:" + e.Message;
            }


        }
        protected void LnkfTotal_Click(object sender, EventArgs e)
        {
            this.TotalCalculation1();
        }
        private void TotalCalculation1()
        {
            this.SessionUpdate();
            DataTable tblt06 = (DataTable)Session["AccTbl01"];
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Dr)", "")) ?
            0.00 : tblt06.Compute("Sum(Dr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Cr)", "")) ?
            0.00 : tblt06.Compute("Sum(Cr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }


        protected void dgv2ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate();
            this.dgv2.PageIndex = ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).SelectedIndex;
            this.dgv2_DataBind();
            this.TotalCalculation1();


        }
        protected void dgv2_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["AccTbl01"];
            this.dgv2.DataSource = tbl1;
            this.dgv2.DataBind();
            if (tbl1.Rows.Count == 0)
                return;

            ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.dgv2.PageSize);
            ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = true;
            ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).SelectedIndex = this.dgv2.PageIndex;
        }



        protected void dgv3_DataBind()
        {
            DataTable tblt03 = (DataTable)Session["AccTbl02"];
            this.dgv3.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.dgv3.DataSource = tblt03;
            this.dgv3.DataBind();
            if (tblt03.Rows.Count == 0)
                return;
            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftDramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Dr)", "")) ?
            0.00 : tblt03.Compute("Sum(Dr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftCramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Cr)", "")) ?
            0.00 : tblt03.Compute("Sum(Cr)", ""))).ToString("#,##0.00;(#,##0.00);  ");

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        protected void dgv3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3.PageIndex = e.NewPageIndex;
            this.dgv3_DataBind();
        }
        protected void lnkbtnUpdateRes_Click(object sender, EventArgs e)
        {
            this.UpdateTable02();

        }


    }
}