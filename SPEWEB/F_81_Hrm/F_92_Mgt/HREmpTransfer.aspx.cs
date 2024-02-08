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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class HREmpTransfer : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess("ASITHRM");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtpatplacedate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.Get_Trnsno();
                // this.tableintosession();
            }

            if (Session["sessionforgrid"] == null)
            {
                this.tableintosession();

            }
        }

        protected void GetPrvNm()
        {

            string comcod = GetComCode();
            //string mREQNO = "NEWISS";
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
            string mREQDAT = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", mREQDAT,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxtrnno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 6);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnno1";
                this.ddlPrevISSList.DataValueField = "maxtrnno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();
            }

        }
        private void Get_Trnsno()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6);
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("empid", Type.GetType("System.String"));
            dttemp.Columns.Add("idcardno", Type.GetType("System.String"));
            dttemp.Columns.Add("empname", Type.GetType("System.String"));
            dttemp.Columns.Add("desig", Type.GetType("System.String"));
            dttemp.Columns.Add("desigid", Type.GetType("System.String"));
            dttemp.Columns.Add("tfprjcode", Type.GetType("System.String"));
            dttemp.Columns.Add("ttprjcode", Type.GetType("System.String"));
            dttemp.Columns.Add("tfprjdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("ttprjdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("pplacedate", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("rmrks", Type.GetType("System.String"));
            dttemp.Columns.Add("infor", Type.GetType("System.String"));
            dttemp.Columns.Add("spnote", Type.GetType("System.String"));
            dttemp.Columns.Add("address", Type.GetType("System.String"));
            Session["sessionforgrid"] = dttemp;
        }
        protected void Load_Project_From_Combo()
        {
            Session.Remove("prlist");
            string comcod = this.GetComCode();
            string pactCode = this.ddlCompany.SelectedValue.ToString().Trim().Substring(0, 2);
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", pactCode, "%%", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlistfrom.DataTextField = "actdesc";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            Session["prlist"] = ds1.Tables[0];
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string company = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["sessionforgrid"];

            if (this.rbtTrnstype.SelectedIndex == 0)
            {
                ReportDocument rptmattrans = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptHREmpTransfer();
                TextObject rptCname = rptmattrans.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                rptCname.Text = company;
                TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
                rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtCurTransDate.Text)).ToString("MMMM dd, yyyy");
                TextObject rpttrnno = rptmattrans.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
                rpttrnno.Text = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + "-" + this.txtCurTransNo2.Text.Trim();
                TextObject rpttxtinformation = rptmattrans.ReportDefinition.ReportObjects["txtinformation"] as TextObject;
                rpttxtinformation.Text = this.txtfmaters.Text.Trim();
                TextObject rpttxtspnote = rptmattrans.ReportDefinition.ReportObjects["txtspnote"] as TextObject;
                rpttxtspnote.Text = this.txtspnote.Text.Trim();
                TextObject txtuserinfo = rptmattrans.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rptmattrans.SetDataSource(dt1);

                //string comcod = this.GetComeCode();
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptmattrans.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptmattrans;
            }
            else
            {

                string date = Convert.ToDateTime(dt1.Rows[0]["pplacedate"]).ToString("dd MMMM, 2010");
                ReportDocument rptmattrans = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptHREmpTransfer01();
                TextObject rptCname = rptmattrans.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                rptCname.Text = company;
                TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
                rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtCurTransDate.Text)).ToString("MMMM dd, yyyy");
                TextObject rpttrnno = rptmattrans.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
                rpttrnno.Text = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + "-" + this.txtCurTransNo2.Text.Trim();
                TextObject rpttxtinformation = rptmattrans.ReportDefinition.ReportObjects["txtinformation"] as TextObject;
                rpttxtinformation.Text = "Management has decided to transfer you in the following department/project.You  are requested  to confirm your reporting in the following place on or before " + date + ".";

                rptmattrans.SetDataSource(dt1);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptmattrans.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptmattrans;


            }

            lbljavascript.Text = "<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlprj.Visible = true;
                this.pnlremarks.Visible = true;
                this.txtCurTransDate.Enabled = true;
                this.GetCompany();
                if (this.ddlPrevISSList.Items.Count > 0)
                {
                    this.txtCurTransDate.Enabled = false;
                    string trnno = this.ddlPrevISSList.SelectedValue.ToString().Trim();
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
                    Session["sessionforgrid"] = ds.Tables[0];
                    this.txtfmaters.Text = ds.Tables[0].Rows[0]["infor"].ToString();
                    this.txtspnote.Text = ds.Tables[0].Rows[0]["spnote"].ToString();
                    this.grvacc_DataBind();
                    this.Load_Cur_Trans_NO();
                }
                else
                {
                    this.Get_Trnsno();

                }

                if (this.ddlprjlistfrom.Items.Count == 0)
                {

                    this.Load_Project_From_Combo();

                }
                this.lbtnPrevTransList.Visible = false;
                this.ddlPrevISSList.Visible = false;


            }
            else
            {


                this.lblmsg.Text = "";
                this.txtfmaters.Text = "";
                this.txtspnote.Text = "";
                this.ddlPrevISSList.Items.Clear();
                this.lbtnPrevTransList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                Session["sessionforgrid"] = null;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.pnlprj.Visible = false;
                this.pnlremarks.Visible = false;
                lbtnOk.Text = "Ok";

            }

        }

        protected void grvacc_DataBind()
        {

            this.grvacc.DataSource = (DataTable)Session["sessionforgrid"];
            this.grvacc.DataBind();
        }

        protected void Employee_List()
        {
            Session.Remove("tblemp");
            string comcod = this.GetComCode();
            string prjfrom = this.ddlprjlistfrom.SelectedValue.ToString();
            string emplist = this.txtsrchEmp.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "GETEMPLIST", prjfrom, emplist, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            Session["tblemp"] = ds1.Tables[0];

        }
        protected void Load_Cur_Trans_NO()
        {
            this.lblCurTransNo1.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(0, 5);
            this.txtCurTransNo2.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(6, 5);
            string curdate = Convert.ToDateTime(this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(12, this.ddlPrevISSList.SelectedItem.ToString().Trim().Length - 12)).ToString("dd.MM.yyyy");

            if (curdate.Substring(2, 1).ToString().Trim() == "-")
            {
                this.txtCurTransDate.Text = "0" + curdate.Trim();
            }
            else
                this.txtCurTransDate.Text = curdate;

        }


        protected void ibtnEmpList_Click(object sender, ImageClickEventArgs e)
        {
            this.Employee_List();
        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string empid = this.ddlEmpList.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            DataRow[] projectrow2 = dt.Select("empid = '" + empid + "'");
            if (projectrow2.Length > 0)
            {
                return;

            }

            DataRow drforgrid = dt.NewRow();
            drforgrid["empid"] = empid;
            drforgrid["idcardno"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["idcardno"];
            drforgrid["empname"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["empname"].ToString().Substring(7);
            drforgrid["desigid"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desigid"];
            drforgrid["desig"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desig"];
            drforgrid["tfprjcode"] = this.ddlprjlistfrom.SelectedValue.ToString();
            drforgrid["tfprjdesc"] = this.ddlprjlistfrom.SelectedItem.Text.Substring(13);
            drforgrid["ttprjcode"] = this.ddlprjlistto.SelectedValue.ToString();
            drforgrid["ttprjdesc"] = this.ddlprjlistto.SelectedItem.Text.Substring(13);
            drforgrid["pplacedate"] = this.GetStdDate(this.txtpatplacedate.Text.Trim());
            //drforgrid["Comname"] = this.ddlCompany.SelectedItem.Text.Trim();
            dt.Rows.Add(drforgrid);
            Session["sessionforgrid"] = dt;
            this.grvacc_DataBind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["sessionforgrid"];
            int TblRowIndex;
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {

                string txtremarks = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvremarks")).Text;
                TblRowIndex = (grvacc.PageIndex) * grvacc.PageSize + i;

                dt.Rows[TblRowIndex]["rmrks"] = txtremarks;

            }
            Session["sessionforgrid"] = dt;


        }

        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToProject();
            this.Employee_List();



        }
        private void ToProject()
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

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveValue();
                string comcod = this.GetComCode();
                DataTable dt = (DataTable)Session["sessionforgrid"];
                if (ddlPrevISSList.Items.Count == 0)
                {
                    this.GetPrvNm();
                }
                //string curdate = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
                string information = this.txtfmaters.Text.Trim();
                string spnote = this.txtspnote.Text.Trim();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string fromprj = dt.Rows[i]["tfprjcode"].ToString();
                    string toprj = dt.Rows[i]["ttprjcode"].ToString();
                    string remarks = dt.Rows[i]["rmrks"].ToString();
                    string date = Convert.ToDateTime(dt.Rows[i]["pplacedate"]).ToString();
                    string desigid = dt.Rows[i]["desigid"].ToString();
                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "INSORUPHREMPTNSINF", tansno, fromprj, toprj, empid,
                         curdate, remarks, information, spnote, date, desigid, "", "", "", "", "");
                }
                this.lblmsg.Text = "Updated Successfully";

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error: " + ex.Message;

            }

        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
            //ds1.Dispose();

        }
        protected void imgbtnCompany_Click(object sender, ImageClickEventArgs e)
        {
            this.GetCompany();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_From_Combo();
        }
    }
}