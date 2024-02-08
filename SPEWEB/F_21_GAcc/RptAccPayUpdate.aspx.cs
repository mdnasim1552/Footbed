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
    public partial class RptAccPayUpdate : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ViewSection();
                this.GetBankName();
            }

        }





        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void ViewSection()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "GroupWiseChqIssued":
                    this.GetGroupName();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }


        }
        protected void imgbtnSrchBank_Click(object sender, ImageClickEventArgs e)
        {
            this.GetBankName();
        }

        private void GetBankName()
        {

            string comcod = this.GetCompCode();
            string SeachBankName = "%" + this.txtserchBankName.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKNAME", SeachBankName, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlBankName.Items.Clear();
                return;
            }

            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["comcod"] = this.GetCompCode();
            dr1["actcode"] = "000000000000";
            dr1["actdesc"] = "All Bank";
            dr1["actdesc1"] = "000000000000- All Bank";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.Sort = ("actcode");
            dt = dv.ToTable();

            this.ddlBankName.DataTextField = "actdesc1";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = dt;
            this.ddlBankName.DataBind();
            ds1.Dispose();
        }

        private void GetGroupName()
        {

            string comcod = this.GetCompCode();
            string SearhcGroup = "%" + this.txtserchGrpName.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETGROUPNAME", SearhcGroup, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlGroupDesc.DataTextField = "sirdesc";
            this.ddlGroupDesc.DataValueField = "sircode";
            this.ddlGroupDesc.DataSource = ds1.Tables[0];
            this.ddlGroupDesc.DataBind();
            this.ddlGroupDesc.SelectedValue = "000000000000";
            ds1.Dispose();
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.ShowChqIssued();
                    break;

                case "GroupWiseChqIssued":
                    this.ShowgrpwiseChqIssued();
                    break;
            }



        }


        private void ShowChqIssued()
        {


            try
            {
                Session.Remove("tblpenchq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string voutype = "PV%";


                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "REPORTPERPOSDATECHQ", frmdate, todate, voutype, "", "", "", BankName, "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblpenchq"] = this.HiddenSameDate(ds1.Tables[0]);
                this.Data_Bind();



            }
            catch (Exception ex)
            {

            }

        }

        private void ShowgrpwiseChqIssued()
        {

            try
            {
                Session.Remove("tblpenchq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string voutype = "PV%";

                string GroupCode = ((this.ddlGroupDesc.SelectedValue.ToString() == "000000000000") ? "" : this.ddlGroupDesc.SelectedValue.ToString().Substring(0, 2)) + "%";
                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "RPTGROUPWISEPDCHEQUE", frmdate, todate, voutype, GroupCode, BankName, "", "", "", "");
                if (ds1 == null)
                {
                    this.gvgrpchqissued.DataSource = null;
                    this.gvgrpchqissued.DataBind();
                    return;
                }
                Session["tblpenchq"] = ds1.Tables[0];
                this.Data_Bind();



            }
            catch (Exception ex)
            {

            }




        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpenchq"];
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.dgv1.DataSource = dt;
                    this.dgv1.DataBind();
                    Session["Report1"] = dgv1;
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    this.CalculatrGridTotal();
                    break;

                case "GroupWiseChqIssued":
                    this.gvgrpchqissued.DataSource = dt;
                    this.gvgrpchqissued.DataBind();
                    Session["Report1"] = gvgrpchqissued;
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.gvgrpchqissued.HeaderRow.FindControl("hlbtnbtbCdataExelgp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    this.CalculatrGridTotal();
                    break;
            }








        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string pactcode = dt1.Rows[0]["actcode"].ToString();
            string cactcode = dt1.Rows[0]["cactcode"].ToString();
            int j;
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();

                }

            }



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["actcode"].ToString() == pactcode) && (dt1.Rows[j]["cactcode"].ToString() == cactcode))
                {
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["cactdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["actcode"].ToString() == pactcode)
                        dt1.Rows[j]["actdesc"] = "";
                    if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                        dt1.Rows[j]["cactdesc"] = "";
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                }

            }
            return dt1;


            //grpcode = dt1.Rows[0]["grpcode"].ToString();
            //            string actcode = dt1.Rows[0]["actcode"].ToString();
            //            for (j = 1; j < dt1.Rows.Count; j++)
            //            {
            //                if (dt1.Rows[j]["grpcode"].ToString() == grpcode && dt1.Rows[j]["actcode"].ToString() == actcode)
            //                {
            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();
            //                    dt1.Rows[j]["grpdesc"] = "";
            //                    dt1.Rows[j]["actdesc"] = "";

            //                }

            //                else
            //                {


            //                    if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
            //                    {
            //                        dt1.Rows[j]["grpdesc"] = "";
            //                    }
            //                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
            //                    {
            //                        dt1.Rows[j]["actdesc"] = "";
            //                    }

            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();

            //                }

            //            }
            //        break;

            //}


            //  return dt1;



        }
        protected void CalculatrGridTotal()
        {

            DataTable dt = (DataTable)Session["tblpenchq"];
            DataTable dt1 = dt.Copy();
            DataView dv = new DataView();
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":


                    dv = dt1.DefaultView;
                    dv.RowFilter = ("typesum='ZZZZ'");
                    dt1 = dv.ToTable();
                    ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cramt)", "")) ? 0 : dt1.Compute("sum(cramt)", ""))).ToString("#,##0;-#,##0; ");
                    break;

                case "GroupWiseChqIssued":
                    dv = dt1.DefaultView;
                    dv.RowFilter = ("typesum='TTTT'");
                    dt1 = dv.ToTable();
                    ((Label)this.gvgrpchqissued.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ? 0 : dt1.Compute("sum(payam)", ""))).ToString("#,##0;-#,##0; ");
                    break;
            }



        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.PrintChequeIssued();

                    break;

                case "GroupWiseChqIssued":
                    this.PrintChequeIssuedGrpWise();
                    break;
            }




        }

        private void PrintChequeIssued()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblpenchq"];

            //ReportDocument rptsale = new RMGiRPT.R_21_GAcc.rptPostDatCheque();
            ////TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            ////rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptDate.Text = "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintChequeIssuedGrpWise()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblpenchq"];
            //ReportDocument rptsale = new RMGiRPT.R_21_GAcc.RptChqIssuedGrpWise();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptDate.Text = "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            //TextObject txtGranTotal = rptsale.ReportDefinition.ReportObjects["txtGranTotal"] as TextObject;
            //txtGranTotal.Text = ((Label)this.gvgrpchqissued.FooterRow.FindControl("lgvFpayam")).Text;
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                //Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }






        protected void imgbtnSrchGroup_Click(object sender, ImageClickEventArgs e)
        {
            this.GetGroupName();
        }
        protected void gvgrpchqissued_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label resdesc = (Label)e.Row.FindControl("lgvresdescgp");
                Label amt = (Label)e.Row.FindControl("lgvpayam");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (code == "TTTT")
                {
                    resdesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    resdesc.Style.Add("text-align", "right");

                }


            }
        }
    }
}