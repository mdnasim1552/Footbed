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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_15_Pro
{
    public partial class LCMatTransfer : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            if (this.ddlprjlistfrom.Items.Count == 0)
            {
                this.Load_Project_From_Combo();
                this.tableintosession();
                this.Load_Dates_And_Trans_No();
            }

        }

        protected void Load_Dates_And_Trans_No()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");//XXXXXXXXXXXXXX
            this.Last_trn_no();

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("rsircode", Type.GetType("System.String"));
            dttemp.Columns.Add("rsirdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("sirunit", Type.GetType("System.String"));
            dttemp.Columns.Add("qty", Type.GetType("System.Double"));
            dttemp.Columns.Add("rate", Type.GetType("System.Double"));
            dttemp.Columns.Add("amount", Type.GetType("System.Double"));
            Session["sessionforgrid"] = dttemp;

        }



        protected void Load_Project_From_Combo()
        {
            Session.Remove("prlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LCMATTRANSFER", "GETLCINFO", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlistfrom.DataTextField = "actdesc";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            Session["prlist"] = ds1.Tables[0];
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }

        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["prlist"];
            string actcode = this.ddlprjlistfrom.SelectedValue.ToString();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode not in ('" + actcode + "')";
            this.ddlprjlistto.DataTextField = "actdesc";
            this.ddlprjlistto.DataValueField = "actcode";
            this.ddlprjlistto.DataSource = dv1.ToTable();
            this.ddlprjlistto.DataBind();

        }

        protected void Load_Project_Res_Combo()
        {
            Session.Remove("tblsir");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string prjfrom = this.ddlprjlistfrom.SelectedValue.ToString();
            //string prjcode = this.ddlprjlistfrom.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LCMATTRANSFER", "RESLIST", prjfrom, "%%", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlreslist.DataTextField = "sirdesc";
            this.ddlreslist.DataValueField = "sircode";
            this.ddlreslist.DataSource = ds1.Tables[0];
            this.ddlreslist.DataBind();
            Session["tblsir"] = ds1.Tables[0];

        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.Session_update();
            string rsircode = this.ddlreslist.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rsircode + "'");

            if (projectrow2.Length > 0)
            {
                return;

            }

            DataRow drforgrid = dt.NewRow();
            drforgrid["rsircode"] = rsircode;
            drforgrid["rsirdesc"] = this.ddlreslist.SelectedItem.Text.Trim();
            drforgrid["sirunit"] = ((DataTable)Session["tblsir"]).Select("sircode='" + rsircode + "'")[0]["sirunit"].ToString();
            drforgrid["qty"] = 0;
            drforgrid["rate"] = Convert.ToDouble(((DataTable)Session["tblsir"]).Select("sircode='" + rsircode + "'")[0]["itmrat"].ToString());
            drforgrid["amount"] = 0;
            dt.Rows.Add(drforgrid);
            Session["sessionforgrid"] = dt;
            this.grvacc_DataBind();

        }

        private void Session_update()
        {
            DataTable dt1 = (DataTable)Session["sessionforgrid"];
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                double amt = qty * rate;
                dt1.Rows[i]["qty"] = qty;
                dt1.Rows[i]["rate"] = rate;
                dt1.Rows[i]["amount"] = amt;
            }
            Session["sessionforgrid"] = dt1;

        }


        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["sessionforgrid"];
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
            this.FooterVal();

        }

        private void FooterVal()
        {
            DataTable dt = (DataTable)Session["sessionforgrid"];
            ((Label)this.grvacc.FooterRow.FindControl("lblFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ?
                             0.00 : dt.Compute("Sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); - ");
        }





        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session_update();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                ProcessAccess pa = new ProcessAccess();
                DataTable dt = (DataTable)Session["sessionforgrid"];
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
                string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
                string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string trsircode = dt.Rows[i]["rsircode"].ToString().Trim();
                    string tunit = dt.Rows[i]["sirunit"].ToString().Trim();
                    string tqty = dt.Rows[i]["qty"].ToString().Trim();
                    string tamt = Convert.ToDouble(dt.Rows[i]["amount"]).ToString();
                    bool result = pa.UpdateTransInfo(comcod, "SP_ENTRY_LCMATTRANSFER", "INSORUPLCTRNSINF", tansno, fromprj, toprj, trsircode,
                         tqty, curdate, tamt, "", "", "", "", "", "", "", "");
                }
                this.lblmsg.Text = "Updated Successfully";

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error: " + ex.Message;

            }

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlgrd.Visible = true;
                this.lblddlProjectFrom.Visible = true;
                this.lblddlProjectTo.Visible = true;
                this.ddlprjlistfrom.Visible = false;
                this.ddlprjlistto.Visible = false;
                this.ddlPrevISSList.Visible = false;


                if (this.ddlPrevISSList.Items.Count > 0)
                {
                    string trnno = this.ddlPrevISSList.SelectedValue.ToString().Trim();
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();

                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_LCMATTRANSFER", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
                    DataTable dt1 = ds.Tables[0];
                    this.ddlprjlistfrom.SelectedValue = ds.Tables[1].Rows[0]["tfmlccod"].ToString();
                    this.ddlprjlistto.SelectedValue = ds.Tables[1].Rows[0]["ttmlccod"].ToString();
                    Session["sessionforgrid"] = dt1;
                    this.grvacc_DataBind();
                    this.Load_Cur_Trans_NO();
                    // this.Load_Project_Res_Combo();
                }
                else
                {
                    this.Get_Trnsno();

                }
                this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text.Trim();
                this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text.Trim();
                this.Load_Project_Res_Combo();


            }
            else
            {
                ///Session.Remove("sessionforgrid");
                Session["sessionforgrid"] = null;
                this.tableintosession();
                this.lblmsg.Text = "";
                this.lblddlProjectFrom.Visible = false;
                this.lblddlProjectTo.Visible = false;
                this.ddlprjlistfrom.Visible = true;
                this.ddlprjlistto.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.Last_trn_no();
                this.pnlgrd.Visible = false;
                lbtnOk.Text = "Ok";
                this.ddlPrevISSList.Items.Clear();
            }

        }

        private void Get_Trnsno()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_LCMATTRANSFER", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6);


        }

        protected void Load_Cur_Trans_NO()
        {
            this.lblCurTransNo1.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(0, 5);
            this.txtCurTransNo2.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(6, 5);
            string curdate = Convert.ToDateTime(this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(12, this.ddlPrevISSList.SelectedItem.ToString().Trim().Length - 12)).ToString("dd.MM.yyyy");
            //curdate = this.GetStdDate(curdate);
            if (curdate.Substring(2, 1).ToString().Trim() == "-")
            {
                this.txtCurTransDate.Text = "0" + curdate.Trim();
            }
            else
                this.txtCurTransDate.Text = curdate;

        }

        protected void Last_trn_no()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_LCMATTRANSFER", "LASTTRANSFETNO", "MAXTRNNO", "", "", "", "", "", "", "", "");
            DataTable dt1 = ds.Tables[0];
            if (dt1.Rows[0]["maxtrnno"].ToString().Trim() == "XXXXXXXXXXXXXX")
            {
                this.lblCurTransNo1.Text = "TRN" + DateTime.Today.ToString("dd.MM.yyyy").Substring(3, 2);
                this.txtCurTransNo2.Text = "00001";
                this.lblLastTransDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblLastTransNo.Text = "TRN" + DateTime.Today.ToString("dd.MM.yyyy").Substring(3, 2) + "00000";
            }
            else
            {
                this.lblCurTransNo1.Text = dt1.Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 5);

                string trnno = (Convert.ToInt32(dt1.Rows[0]["maxtrnno1"].ToString().Trim().Substring(6, 5)) + 1).ToString();
                for (int i = 1; trnno.Length < 5; i++)
                {
                    trnno = "0" + trnno;
                }
                this.txtCurTransNo2.Text = trnno;

                this.lblLastTransDate.Text = Convert.ToDateTime(dt1.Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
                this.lblLastTransNo.Text = dt1.Rows[0]["maxtrnno1"].ToString().Trim();
            }
        }
        protected void lbtnPrevTransList_Click(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();
        }
        protected void Load_Prev_Trans_List()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LCMATTRANSFER", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");
            Session["prevtranslist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk_Click(null, null);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["sessionforgrid"];
            ReportDocument rptmattrans = new RMGiRPT.R_15_Pro.RptMaterialTrnsfer();
            string prjfrm = this.ddlprjlistfrom.SelectedItem.Text.Trim().Substring(13);
            string prjto = this.ddlprjlistto.SelectedItem.Text.Trim().Substring(13);
            TextObject rptCname = rptmattrans.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptProjectNameft = rptmattrans.ReportDefinition.ReportObjects["ProjectNamef"] as TextObject;
            rptProjectNameft.Text = "Transfer From: " + prjfrm;
            TextObject rptProjectNameto = rptmattrans.ReportDefinition.ReportObjects["ProjectNamet"] as TextObject;
            rptProjectNameto.Text = "Transfer To: " + prjto;
            TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtCurTransDate.Text)).ToString("MMMM dd, yyyy");
            TextObject rpttrnno = rptmattrans.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
            rpttrnno.Text = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + "-" + this.txtCurTransNo2.Text.Trim();
            TextObject txtuserinfo = rptmattrans.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptmattrans.SetDataSource(dt1);
            Session["Report1"] = rptmattrans;
            lbljavascript.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_update();
            this.grvacc_DataBind();
        }
    }



}