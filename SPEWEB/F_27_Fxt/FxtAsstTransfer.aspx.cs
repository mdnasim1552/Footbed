using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;


namespace SPEWEB.F_27_Fxt
{
    public partial class FxtAsstTransfer : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Transfer";
                this.GetDeparment();
                this.GetUser();

                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");

                //this.Load_Project_From_Combo();
                // this.tableintosession();
                this.Load_Dates_And_Trans_No();
            }
        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            //  Load_Project_Res_Combo();
        }

        protected void Load_Dates_And_Trans_No()
        {
            string comcod = this.GetComCode();
            this.txtCurTransDate.Text = DateTime.Today.ToString("dd-MM-yyyy");//XXXXXXXXXXXXXX
            this.Get_Trnsno();
        }

        private void GetAsset()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXASSTLIST", "%", "", "", "", "", "", "", "", "");
            //this.ddlasset.DataSource = ds1.Tables[0];
            //this.ddlasset.DataTextField = "sirdesc1";
            //this.ddlasset.DataValueField = "sircode";
            //this.ddlasset.DataBind();
        }

        private void GetUser()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETEMPTIDNAME", "%", "", "", "", "", "", "", "", "");

            ddlfuser.DataTextField = "empname";
            ddlfuser.DataValueField = "empid";
            ddlfuser.DataSource = ds1.Tables[0];
            ddlfuser.DataBind();

            ddltouser.DataTextField = "empname";
            ddltouser.DataValueField = "empid";
            ddltouser.DataSource = ds1.Tables[0];
            ddltouser.DataBind();
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetDeparment()
        {
            string comcod = this.GetComCode();
            // string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", "%", "", "", "", "", "", "", "", "");

            this.ddlflocation.DataTextField = "fxtgdesc";
            this.ddlflocation.DataValueField = "fxtgcod";
            this.ddlflocation.DataSource = ds1.Tables[0];
            this.ddlflocation.DataBind();

            this.ddltolocation.DataTextField = "fxtgdesc";
            this.ddltolocation.DataValueField = "fxtgcod";
            this.ddltolocation.DataSource = ds1.Tables[0];
            this.ddltolocation.DataBind();
        }
        protected void lbtnTrans_Click(object sender, EventArgs e)
        {
            //this.GetPreTrnNm();
            //string comcod = this.GetComCode();
            //string frmloct = this.ddlflocation.SelectedValue.ToString();
            //string frmuser = this.ddlfuser.SelectedValue.ToString();
            //string material =this.ddlmaterial.SelectedValue.ToString();
            //string toloct =this.ddltolocation.SelectedValue.ToString();
            //string touser = this.ddltouser.SelectedValue.ToString();
            //string Date1 = this.lblDate.Text.ToString();

            //string curdate = this.GetStdDate(this.lblDate.Text.ToString().Trim());
            //string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            //DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "", frmloct, frmuser, material, toloct, touser, Date1, tansno, "", "");

            //this.lblmsg1.Text = "Updated Successfully";
        }
        protected void ddlfuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string location = ddlflocation.SelectedValue.ToString();
            string fuser = ddlfuser.SelectedValue.ToString();
            string comcod = this.GetComCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETALLOCATEDASSET", location, fuser, "", "", "", "", "", "", "");

            this.ddlmaterial.DataSource = ds1.Tables[0];
            this.ddlmaterial.DataTextField = "sirdesc1";
            this.ddlmaterial.DataValueField = "sircode";
            this.ddlmaterial.DataBind();
        }
        protected void ddlflocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlfuser_SelectedIndexChanged(null, null);
        }

        protected void GetPreTrnNm()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mTrnsDAT = this.GetStdDate(this.txtCurTransDate.Text.Trim());

            DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETTRANSID", mTrnsDAT,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxtrnsno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnsno1"].ToString().Substring(0, 6);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnsno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnsno1";
                this.ddlPrevISSList.DataValueField = "maxtrnsno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();
            }
        }

        private void Get_Trnsno()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETTRANSID", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrnsdt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnsno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnsno1"].ToString().Substring(6);
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session_update();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                ProcessAccess pa = new ProcessAccess();
                DataTable dt = (DataTable)ViewState["tblmattrns"];
                if (ddlPrevISSList.Items.Count == 0)
                {
                    this.GetPreTrnNm();
                }
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
                //  string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
                //  string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rsircode = dt.Rows[i]["rsircode"].ToString().Trim();
                    string fpactcod = dt.Rows[i]["fpactcod"].ToString().Trim();
                    string fempid = dt.Rows[i]["fempid"].ToString().Trim();
                    string tpactcod = dt.Rows[i]["tpactcod"].ToString().Trim();
                    string tuser = dt.Rows[i]["tuser"].ToString().Trim();
                    string qty = dt.Rows[i]["qty"].ToString().Trim();

                    bool result = pa.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "ASSETTRANSFER02", tansno, curdate, fpactcod, fempid, tpactcod, tuser, rsircode, qty, "", "", "", "", "", "", "");
                }//INSERTORUPDATEASSTTRASNSINFO
                this.lblmsg1.Text = "Updated Successfully";
                this.lblmsg1.Visible = true;
            }
            catch (Exception ex)
            {
                this.lblmsg1.Text = "Error: " + ex.Message;

            }

        }

        private void Session_update()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                dt1.Rows[i]["qty"] = qty;
            }
            ViewState["tblmattrns"] = dt1;

        }

        protected void lnkselect_Click(object sender, EventArgs e)
        {
            if ((DataTable)ViewState["tblmattrns"] == null)
                tableintosession();

            DataTable dt = (DataTable)ViewState["tblmattrns"];

            string frmloct = this.ddlflocation.SelectedValue.ToString();
            string frmuser = this.ddlfuser.SelectedValue.ToString();
            string material = this.ddlmaterial.SelectedValue.ToString();
            string toloct = this.ddltolocation.SelectedValue.ToString();
            string touser = this.ddltouser.SelectedValue.ToString();

            //  string rescode = this.ddlreslist.SelectedValue.ToString().Trim();
            // string spcfcod = this.ddlResSpcf.SelectedValue.ToString();

            // DataTable dt1 = (DataTable)Session["projectreslist"];
            //  DataRow[] projectrow1 = dt1.Select("rsircode = '" + rescode + "'");
            // DataRow[] projectrow2 = dt.Select("rsircode = '" + rescode + " and spcfcod = " + spcfcod + "'");

            DataRow drforgrid = dt.NewRow();

            drforgrid["rsircode"] = material;
            drforgrid["fpactcod"] = frmloct;
            drforgrid["fempid"] = frmuser;
            drforgrid["tpactcod"] = toloct;
            drforgrid["tuser"] = touser;
            drforgrid["resdesc"] = this.ddlmaterial.SelectedItem.ToString().Substring(13);
            drforgrid["tpactdesc"] = this.ddltolocation.SelectedItem.ToString();
            drforgrid["tempnam"] = this.ddltouser.SelectedItem.ToString();
            drforgrid["sirunit"] = "";// projectrow1[0]["sirunit"];
            drforgrid["stk"] = 1;
            drforgrid["qty"] = 1;// projectrow1[0]["qty"];
            dt.Rows.Add(drforgrid);
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();
        }

        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("rsircode", Type.GetType("System.String"));
            dttemp.Columns.Add("fpactcod", Type.GetType("System.String"));
            dttemp.Columns.Add("fempid", Type.GetType("System.String"));
            dttemp.Columns.Add("tpactcod", Type.GetType("System.String"));
            dttemp.Columns.Add("tuser", Type.GetType("System.String"));
            dttemp.Columns.Add("resdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("tpactdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("tempnam", Type.GetType("System.String"));
            dttemp.Columns.Add("sirunit", Type.GetType("System.String"));
            dttemp.Columns.Add("stk", Type.GetType("System.Double"));
            dttemp.Columns.Add("qty", Type.GetType("System.Double"));
            ViewState["tblmattrns"] = dttemp;
        }

        private void Data_Bind()
        {
            // this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = (DataTable)ViewState["tblmattrns"];
            this.grvacc.DataBind();
            this.grvacc.Visible = true;

            //  this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            //  ((LinkButton)this.grvacc.FooterRow.FindControl("lnkupdate")).Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            // this.FooterCalCulation();
        }


        protected void lbtnFindResgp_Click(object sender, EventArgs e)
        {

        }
        protected void lnkselectgp_Click(object sender, EventArgs e)
        {

        }

        protected void lnkselectgpAll_Click(object sender, EventArgs e)
        {

        }
        protected void ddlreslistgp_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            int rowindex = e.RowIndex;
            dt.Rows[rowindex].Delete();

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmattrns");
            ViewState["tblmattrns"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlgrd.Visible = true;
                this.ddlflocation.Enabled = false;
                this.ddlfuser.Enabled = false;
                ddlfuser_SelectedIndexChanged(null, null);
                this.ImgbtnFindRes_Click(null, null);
            }
            else
            {
                this.tableintosession();
                this.ddlflocation.Enabled = true;
                this.ddlfuser.Enabled = true;
                this.lblmsg1.Visible = false;
                this.txtCurTransDate.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.Get_Trnsno();
                this.pnlgrd.Visible = false;
                this.lblmsg1.Text = "";
                //this.lblVoucherNo.Text = "";
                lbtnOk.Text = "Ok";
            }
        }

    }
}