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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;


namespace SPEWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class RptgroupAttendance : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //((Label)this.Master.FindControl("lblTitle")).Text = "Group Attandance Report";

                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Diagnosis Entry";

                //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
                //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
                //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

                //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
                //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
                this.ShowGroupAttendance();

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler();
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkDiagEnrty_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            //  ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).Checked += new EventHandler(chkBoxN_Click);
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowGroupAttendance();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void gvRptAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvRptAttn.PageIndex = e.NewPageIndex;
            //this.Data_Bind();

        }
        private void ShowGroupAttendance()
        {
            string comcod = this.GetCompCode();
            string todydate = this.txtFdate.Text;

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETGROUPATTENDENCE", todydate, "", "", "", "", "", "", "", "");
            if (ds.Tables[0].Rows.Count == 0 || ds == null)
            {

                return;
            }

            ViewState["tblgroupAttendace"] = ds.Tables[0];
            ViewState["tblgroupAttenPersen"] = ds.Tables[1];

            this.Data_Bind();
        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblgroupAttendace"];
            DataTable dt2 = (DataTable)ViewState["tblgroupAttenPersen"];

            this.gvRptAttn.DataSource = dt;
            this.gvRptAttn.DataBind();





            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "drawChart();", true);

            this.gvAttPersent.DataSource = dt2;
            this.gvAttPersent.DataBind();


            double tostaff = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ttlstap)", "")) ? 0.00 : dt.Compute("Sum(ttlstap)", "")));
            double present = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(present)", "")) ? 0.00 : dt.Compute("Sum(present)", "")));
            double late = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(late)", "")) ? 0.00 : dt.Compute("Sum(late)", "")));
            double eleave = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(earlyLev)", "")) ? 0.00 : dt.Compute("Sum(earlyLev)", "")));
            double onlaeve = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(onlev)", "")) ? 0.00 : dt.Compute("Sum(onlev)", "")));
            double absent = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(absnt)", "")) ? 0.00 : dt.Compute("Sum(absnt)", "")));
            double prepostaff = 0.00, latepostaff = 0.00, eleavepostaff = 0.00, onlaevepostaff = 0.00, absentpostaff = 0.00;
            prepostaff = (present * 100) / tostaff;
            latepostaff = (late * 100) / tostaff;
            eleavepostaff = (eleave * 100) / tostaff;
            onlaevepostaff = (onlaeve * 100) / tostaff;
            absentpostaff = (absent * 100) / tostaff;
            this.txtpresent.Text = prepostaff.ToString("#,##0;(#,##0); ");
            this.txtlate.Text = latepostaff.ToString("#,##0;(#,##0); ");
            this.txtearlylev.Text = eleavepostaff.ToString("#,##0;(#,##0); ");
            this.txtonleave.Text = onlaevepostaff.ToString("#,##0;(#,##0); ");
            this.txtabsent.Text = absentpostaff.ToString("#,##0;(#,##0); ");




            //((Label)this.gvAttPersent.FooterRow.FindControl("lblstaf")).Text = tostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblprs")).Text = prepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblfotlate")).Text = latepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lbleleave")).Text = eleavepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblol")).Text = onlaevepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblabs")).Text = absentpostaff.ToString("#,##0;(#,##0); ");
        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void gvRptAttn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvcomname = (HyperLink)e.Row.FindControl("hlnkgvcomname");

                HyperLink lblgvPresent = (HyperLink)e.Row.FindControl("lblgvPresent");
                HyperLink lblgvLate = (HyperLink)e.Row.FindControl("lblgvLate");

                HyperLink lgvOnleav = (HyperLink)e.Row.FindControl("lgvOnleav");
                HyperLink lblgverLate = (HyperLink)e.Row.FindControl("lblgverLate");
                HyperLink lblgvAbst = (HyperLink)e.Row.FindControl("lblgvAbst");


                string code = "930100101001";// Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dpt")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }


                hlnkgvcomname.Font.Bold = true;
                hlnkgvcomname.Style.Add("color", "blue");

                hlnkgvcomname.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkLateElLeaveAAbs.aspx?Type=LELLAndAbsent&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") + "&code=" + code;

                lblgvPresent.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkAttDetails.aspx?Type=pempname&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") + "&code=" + code;
                lblgvLate.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkAttDetails.aspx?Type=lempname&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") + "&code=" + code;
                lblgverLate.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkAttDetails.aspx?Type=elempname&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") + "&code=" + code;

                lgvOnleav.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkAttDetails.aspx?Type=olempname&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") + "&code=" + code;
                lblgvAbst.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkAttDetails.aspx?Type=aempname&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") + "&code=" + code;




            }
        }
    }
}