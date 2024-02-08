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
using Microsoft.Reporting.WinForms;
using SPERDLC;


namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmpSettlement : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess HRData = new ProcessAccess();
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE FINAL SETTLEMENT";
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                CommonButton();
                this.GetEmployeeName();
                if (this.Request.QueryString["actcode"].ToString().Length != 0)
                {
                    this.lbtnOk_Click(null, null);
                }
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do You want to Approve?')";
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void CommonButton()
        {

        //    ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).ToolTip = "Click to Approve";
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).ToolTip = "Click to Final Update";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).ToolTip = "Click to Recalculate";
            ((LinkButton)this.Master.FindControl("btnClose")).ToolTip = "Click to Close Current Page";


        }
        private void GetEmployeeName()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_SEPERATED_EMP", "", "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            this.ddlEmpName.DataTextField = "empname1";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            if (this.Request.QueryString["actcode"].ToString().Length != 0)
            {
                this.ddlEmpName.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }

            ViewState["empdata"] = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>();
            ds3.Dispose();

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPerNumber()
        {

        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetEmployeeName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlEmpName.Enabled = false;
                this.MultiView1.Visible = true;
                this.SelectIndex();
                this.ShowSettlementInfo();
                this.ShowImage();
                return;
            }

            this.lbtnOk.Text = "Ok";        
            this.MultiView1.Visible = false;
            this.ddlEmpName.Enabled = true;
            this.txtCurDate.Enabled = true;
            this.divRefNo.Visible = false;
        }
        private void ShowImage()
        {
            string comcod = this.GetComeCode();
            var emplist = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            string idcard = "";
            string empid = this.ddlEmpName.SelectedValue.ToString();
            if (emplist.Any())
                idcard = emplist.FindAll(l => l.empid == empid)[0].idno.ToString();

            string empImg = "";
            switch (comcod)
            {
                case "5305"://FB
                    empImg = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                    break;

                case "5306"://Footbed
                    empImg = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                    break;

                default:
                    empImg = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                    break;
            }
            this.EmpImg.ImageUrl = empImg;

            #region Image Retrieve from DB
            //string comcod = this.GetComeCode();
            //ProcessAccess HRData = new ProcessAccess();
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //Session.Remove("tblEmpimg");

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "SHOWIMG", empid, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{

            //    return;
            //}

            //Session["tblEmpimg"] = ds1.Tables[0];
            ////this.EmpImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgEmp";

            //byte[] image = (byte[])ds1.Tables[0].Rows[0]["empimage"];
            //this.EmpImg.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(image);
            #endregion
        }
        private void SelectIndex()
        {
            string comcod = GetComeCode();
            switch (comcod)
            {
                case "5305"://FB Footwear
                case "5306"://Footbed Footwear
                    this.MultiView1.ActiveViewIndex=1;
                    break;

                default:
                    this.MultiView1.ActiveViewIndex=0;
                    break;
            }
        }

        private void ShowSettlementInfo()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string rpttype = this.rbtnstatement.SelectedIndex.ToString();
            var emplist = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            string callType = this.GetCallType();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", callType, empid, rpttype, "", "", "", "", "", "");
            if(ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail(" + HRData.ErrorObject.ToString() + "');", true);
                return;
            }
            ViewState["tblsttlmnt"] = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            
            var shorempdata = emplist.FindAll(d => d.empid == empid);
            if (rpttype == "0")
            {
                switch (comcod)
                {
                    case "5305":
                    case "5306":
                        ViewState["tblsttcomp"] = ds3.Tables[2].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSettCompanyInfo>();
                        this.settEmpInfo.Visible = true;
                        this.settempname.Text = shorempdata[0].empname.ToString();
                        this.settempid.Text = shorempdata[0].idno.ToString();
                        this.settempdept.Text = shorempdata[0].deptname.ToString();
                        this.settempdesig.Text = shorempdata[0].designation.ToString();
                        this.settempsection.Text = shorempdata[0].section.ToString();
                        this.settempjoindate.Text = shorempdata[0].joindat.ToString("dd-MMM-yyyy");
                        this.settempsepdate.Text = shorempdata[0].retdat.ToString("dd-MMM-yyyy");
                        this.settempeffdate.Text = shorempdata[0].effectdate.ToString("dd-MMM-yyyy");
                        this.settempserlen.Text = shorempdata[0].servleng.ToString();
                        this.settempdaycon.Text = shorempdata[0].daysconmonth.ToString();
                        this.settempnotperiod.Text = shorempdata[0].noticeduration.ToString();
                        this.txtrefno.Text = shorempdata[0].refno ?? "";
                        break;

                    default:
                        this.bngst.Visible = false;
                        this.engst.Visible = true;
                        this.lblname.Text = shorempdata[0].empname.ToString();
                        this.lbldesig.Text = shorempdata[0].designation.ToString();
                        this.lblidcard.Text = shorempdata[0].idno.ToString();
                        this.lblsection.Text = shorempdata[0].deptname.ToString();
                        this.lblseptype.Text = shorempdata[0].septypedesc.ToString();
                        this.lbljoin.Text = shorempdata[0].joindat.ToString("dd-MMM-yyyy");
                        this.lblsep.Text = shorempdata[0].retdat.ToString("dd-MMM-yyyy");
                        this.lblservlen.Text = shorempdata[0].servleng.ToString();
                        break;
                }            

            }
            else
            {
                this.bngst.Visible = true;
                this.engst.Visible = false;
                this.lblnam.Text = shorempdata[0].empname.ToString();
                this.lbldesg.Text = shorempdata[0].designation.ToString();
                this.lblid.Text = shorempdata[0].idno.ToString();
                this.lblsec.Text = shorempdata[0].deptname.ToString();
                this.lbljobtype.Text = shorempdata[0].septypedesc.ToString();
                this.lbljdate.Text = shorempdata[0].joindat.ToString("dd-MMM-yyyy");
                this.lblsepdate.Text = shorempdata[0].retdat.ToString("dd-MMM-yyyy");
                this.lbljonlen.Text = shorempdata[0].servleng.ToString();
                this.lbldate.Text = this.txtCurDate.Text;
                this.txtrefno.Text = shorempdata[0].refno ?? "";
            }

            this.Data_Bind();
            if (ds3.Tables[1].Rows.Count > 0)
            {
                this.divRefNo.Visible = true;
                this.txtCurDate.Text = Convert.ToDateTime(ds3.Tables[1].Rows[0]["billdate"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                this.divRefNo.Visible = false;
            }

        }

        private string GetCallType()
        {
            string comcod = this.GetComeCode();
            string callType = "";
            switch (comcod)
            {
                case "5305":
                case "5306":
                    callType ="GET_EMP_SETTLEMENT_INFO_FB";
                    break;

                default:
                    callType="GET_EMP_SETTLEMENT_INFO";
                    break;
            }
            return callType;
        }

        private void Data_Bind()
        {
            try
            {
                string comcod = this.GetComeCode();
                var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
                switch (comcod)
                {
                    case "5305":
                    case "5306":
                        this.gvSettWages.DataSource = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351"); //Salary Info Grid
                        this.gvSettWages.DataBind();

                        this.gvSettEarn.DataSource = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352"); //Benefits & Dues Grid
                        this.gvSettEarn.DataBind();

                        this.gvSettDeduct.DataSource = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "353"); //Deduction Grid
                        this.gvSettDeduct.DataBind();

                        this.FooterCalculation();
                        break;

                    default:
                        this.gvsettlemntcredit.DataSource = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
                        this.gvsettlemntcredit.DataBind();
                        this.gvsttlededuct.DataSource = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");
                        this.gvsttlededuct.DataBind();
                        this.FooterCalculation();
                        break;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }          
        }

        private void Save_Value()
        {
            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    for (int i = 0; i < this.gvSettEarn.Rows.Count; i++)
                    {
                        string hrgcod = ((Label)gvSettEarn.Rows[i].FindControl("lblgvearnhrgcod")).Text.ToString();
                        double numofday = Convert.ToDouble("0" + ((TextBox)gvSettEarn.Rows[i].FindControl("txtgvearnnumofday")).Text.Trim());
                        double perday = Convert.ToDouble("0" + ((TextBox)gvSettEarn.Rows[i].FindControl("txtgvearnperday")).Text.Trim());
                        double ttlamt = Convert.ToDouble("0" + ((TextBox)gvSettEarn.Rows[i].FindControl("txtgvearntotal")).Text.Trim());
                        string earnremarks = ((TextBox)gvSettEarn.Rows[i].FindControl("lblgvearnremarks")).Text.ToString();

                        var index = sttlmntinfo.FindIndex(p => p.hrgcod == hrgcod);
                        sttlmntinfo[index].numofday = numofday;
                        sttlmntinfo[index].perday = perday;
                        sttlmntinfo[index].ttlamt = (numofday * perday == 0) ? ttlamt : numofday * perday;
                        sttlmntinfo[index].remarks = earnremarks;
                    }

                    for (int i = 0; i < this.gvSettDeduct.Rows.Count; i++)
                    {
                        string hrgcod = ((Label)gvSettDeduct.Rows[i].FindControl("lblgvdedhrgcod")).Text.ToString();
                        double numofday = Convert.ToDouble("0" + ((TextBox)gvSettDeduct.Rows[i].FindControl("txtgvdednumofday")).Text.Trim());
                        double perday = Convert.ToDouble("0" + ((TextBox)gvSettDeduct.Rows[i].FindControl("txtgvdedperday")).Text.Trim());
                        double ttlamt = Convert.ToDouble("0" + ((TextBox)gvSettDeduct.Rows[i].FindControl("txtgvdedtotal")).Text.Trim());
                        string dedremarks = ((TextBox)gvSettDeduct.Rows[i].FindControl("lblgvdeducremarks")).Text.ToString();

                        var index2 = sttlmntinfo.FindIndex(p => p.hrgcod == hrgcod);
                        sttlmntinfo[index2].numofday = numofday;
                        sttlmntinfo[index2].perday = perday;
                        sttlmntinfo[index2].ttlamt = (numofday * perday == 0) ? ttlamt : numofday * perday;
                        sttlmntinfo[index2].remarks = dedremarks;
                    }
                    ViewState["tblsttlmnt"] = sttlmntinfo;
                    break;

                default:
                    for (int i = 0; i < this.gvsettlemntcredit.Rows.Count; i++)
                    {
                        string hrgcod = ((Label)gvsettlemntcredit.Rows[i].FindControl("lblhrgcod")).Text.ToString();
                        double numofday = Convert.ToDouble("0" + ((TextBox)gvsettlemntcredit.Rows[i].FindControl("txtnumofday")).Text.Trim());
                        double perday = Convert.ToDouble("0" + ((TextBox)gvsettlemntcredit.Rows[i].FindControl("txtperday")).Text.Trim());
                        double ttlamt = Convert.ToDouble("0" + ((TextBox)gvsettlemntcredit.Rows[i].FindControl("TtlAmout")).Text.Trim());

                        var index = sttlmntinfo.FindIndex(p => p.hrgcod == hrgcod);
                        sttlmntinfo[index].numofday = numofday;
                        sttlmntinfo[index].perday = perday;
                        sttlmntinfo[index].ttlamt = (numofday * perday == 0) ? ttlamt : numofday * perday;
                    }

                    for (int i = 0; i < this.gvsttlededuct.Rows.Count; i++)
                    {
                        string hrgcod = ((Label)gvsttlededuct.Rows[i].FindControl("lblhrgcod")).Text.ToString();
                        double numofday = Convert.ToDouble("0" + ((TextBox)gvsttlededuct.Rows[i].FindControl("txtnumofday")).Text.Trim());
                        double perday = Convert.ToDouble("0" + ((TextBox)gvsttlededuct.Rows[i].FindControl("txtperday")).Text.Trim());
                        double ttlamt = Convert.ToDouble("0" + ((TextBox)gvsttlededuct.Rows[i].FindControl("TtlAmout")).Text.Trim());

                        var index2 = sttlmntinfo.FindIndex(p => p.hrgcod == hrgcod);
                        sttlmntinfo[index2].numofday = numofday;
                        sttlmntinfo[index2].perday = perday;
                        sttlmntinfo[index2].ttlamt = (numofday * perday == 0) ? ttlamt : numofday * perday;
                    }
                    ViewState["tblsttlmnt"] = sttlmntinfo;
                    break;
            }           
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            int index = this.rbtnstatement.SelectedIndex;
            if (index == 0)
            {
                this.PrintEmpSettlementEng();
            }
            else
            {
                this.PrintEmpSettlementBan();
            }

        }

        private void PrintEmpSettlementEng()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5305"://FB Footwear
                case "5306"://Footbed Footwear
                    this.PrintEmpSettEngFB();
                    break;

                default:
                    this.PrintEmpSettEng();
                    break;
            }
        }

        private void PrintEmpSettEngFB()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string ddlEmpId = this.ddlEmpName.SelectedValue.ToString();
            var emplist = ((List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"]).FindAll(p => p.empid==ddlEmpId);
            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
            var sttlcompinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSettCompanyInfo>)ViewState["tblsttcomp"]; 

            var list1 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");//Salary Info
            var list2 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");//Benefits & Dues
            var list3 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "353");//Deduction

            string txtDate = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            string comnam = sttlcompinfo[0].companyname.ToString();
            string empName = emplist[0].empname.ToString();
            string noticeduration = emplist[0].noticeduration.ToString();
            string empId = emplist[0].idno.ToString();
            string empDesig = emplist[0].designation.ToString();
            string empDept = emplist[0].deptname.ToString();
            string empSection = emplist[0].section.ToString();
            string joinDate = emplist[0].joindat.ToString("dd-MMM-yyyy");
            string sepDate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            string effDate = emplist[0].effectdate.ToString("dd-MMM-yyyy");
            string serLength = emplist[0].servleng.ToString();
            string daysConMonth = emplist[0].daysconmonth.ToString();
            var gssal = (list1.FindAll(s => s.hrgcod == "35101").Sum(l =>l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var bssal = (list1.FindAll(s => s.hrgcod == "35102").Sum(l =>l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var onedaygssal = (list1.FindAll(s => s.hrgcod == "35105").Sum(l =>l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var onedaybssal = (list1.FindAll(s => s.hrgcod == "35106").Sum(l =>l.amount)).ToString("#,##0.00;(#,##0.00); ");
            double netAmount = (list2.Sum(p => p.ttlamt) - list3.Sum(p => p.ttlamt));
            double netpay = Convert.ToDouble(((netAmount < 0) ? netAmount * -1 : netAmount).ToString("#,##0;(#,##0); "));
  
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattelmentFB", list1, list2, list3);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "FINAL SETTLEMENT BILL"));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("gssal", gssal));
            Rpt1.SetParameters(new ReportParameter("bssal", bssal));
            Rpt1.SetParameters(new ReportParameter("onedaygssal", onedaygssal));
            Rpt1.SetParameters(new ReportParameter("onedaybssal", onedaybssal));
            Rpt1.SetParameters(new ReportParameter("netAmount", netAmount.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("tkInWords", ASTUtility.Trans(netpay, 2)));
            Rpt1.SetParameters(new ReportParameter("empName", empName));
            Rpt1.SetParameters(new ReportParameter("empId", empId));
            Rpt1.SetParameters(new ReportParameter("empDesig", empDesig));
            Rpt1.SetParameters(new ReportParameter("empDept", empDept));
            Rpt1.SetParameters(new ReportParameter("empSection", empSection));
            Rpt1.SetParameters(new ReportParameter("joinDate", joinDate));
            Rpt1.SetParameters(new ReportParameter("sepDate", sepDate));
            Rpt1.SetParameters(new ReportParameter("effDate", effDate));
            Rpt1.SetParameters(new ReportParameter("serLength", serLength));
            Rpt1.SetParameters(new ReportParameter("daysConMonth", daysConMonth));
            Rpt1.SetParameters(new ReportParameter("noticeduration", noticeduration));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + 
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            
        }

        private void PrintEmpSettEng()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var emplist = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];




            var list1 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
            var list2 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");


            string billDate = emplist[0].billdate.ToString("dd-MMM-yyyy");
            string name = emplist[0].empname.ToString();
            string Desgin = emplist[0].designation.ToString();
            string Id = emplist[0].idno.ToString();
            string Section = emplist[0].deptname.ToString();
            string jobseperation = emplist[0].septypedesc.ToString();
            string joining = emplist[0].joindat.ToString("dd-MMM-yyyy");
            string sepdate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            var netamount = (sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)).ToString("#,##0;(#,##0); ");
            string servicelength = emplist[0].servleng.ToString();

            double netpay = Convert.ToDouble(netamount);



            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattelment", list1, list2, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Employee Final Sattelment"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("netamount", netamount));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            // for Show EmplInfo
            rpt1.SetParameters(new ReportParameter("billDate", billDate));
            rpt1.SetParameters(new ReportParameter("name", name));
            rpt1.SetParameters(new ReportParameter("Desgin", Desgin));
            rpt1.SetParameters(new ReportParameter("Id", Id));
            rpt1.SetParameters(new ReportParameter("Section", Section));
            rpt1.SetParameters(new ReportParameter("jobseperation", jobseperation));
            rpt1.SetParameters(new ReportParameter("joining", joining));
            rpt1.SetParameters(new ReportParameter("sepdate", sepdate));
            rpt1.SetParameters(new ReportParameter("servicelength", servicelength));
            rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(netpay, 2)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintEmpSettlementBan()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5305"://FB Footwear
                case "5306"://Footbed Footwear
                    this.PrintEmpSettBanFB();
                    break;

                default:
                    this.PrintEmpSettBanGen();
                    break;
            }
           
        }

        private void PrintEmpSettBanFB()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string ddlEmpId = this.ddlEmpName.SelectedValue.ToString();
            var emplist = ((List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"]).FindAll(p => p.empid == ddlEmpId);
            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
            //var sttlcompinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSettCompanyInfo>)ViewState["tblsttcomp"];

            var list1 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");//Salary Info
            var list2 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");//Benefits & Dues
            var list3 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "353");//Deduction

            string txtDate = (ASITUtility02.NumBn(txtCurDate.Text.Substring(0, 2)) + "-" + ASITUtility02.GetMonthName(txtCurDate.Text.Substring(3, 3)) + "-" + ASITUtility02.NumBn(txtCurDate.Text.Substring(6))).ToString();
            //string comnam = sttlcompinfo[0].companyname.ToString();
            string empName = emplist[0].empnamebn.ToString();
            string noticeduration = emplist[0].noticedurban.ToString();
            string empId = emplist[0].idno.ToString();
            string empDesig = emplist[0].designationbn.ToString();
            string empDept = emplist[0].deptnamebn.ToString();
            string empSection = emplist[0].sectionbn.ToString();
            string joinDate = emplist[0].joindat.ToString("dd-MMM-yyyy");
            string sepDate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            string effDate = emplist[0].effectdate.ToString("dd-MMM-yyyy");
            string serLength = emplist[0].servlenban.ToString();
            string daysConMonth = emplist[0].daysconmonth.ToString();
            var gssal = (list1.FindAll(s => s.hrgcod == "35101").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var bssal = (list1.FindAll(s => s.hrgcod == "35102").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var onedaygssal = (list1.FindAll(s => s.hrgcod == "35105").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            var onedaybssal = (list1.FindAll(s => s.hrgcod == "35106").Sum(l => l.amount)).ToString("#,##0.00;(#,##0.00); ");
            double netAmount = (list2.Sum(p => p.ttlamt) - list3.Sum(p => p.ttlamt));
            double netpay = Convert.ToDouble(((netAmount < 0) ? netAmount * -1 : netAmount).ToString("#,##0;(#,##0); "));

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattlementBanFB", list1, list2, list3);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comcod", comcod));
            Rpt1.SetParameters(new ReportParameter("compName", comnambn));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "চূড়ান্ত নিষ্পতি বিল"));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("gssal", gssal));
            Rpt1.SetParameters(new ReportParameter("bssal", bssal));
            Rpt1.SetParameters(new ReportParameter("onedaygssal", onedaygssal));
            Rpt1.SetParameters(new ReportParameter("onedaybssal", onedaybssal));
            Rpt1.SetParameters(new ReportParameter("netAmount", netAmount.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("tkInWords", ASTUtility.TransBN(netpay, 2).Replace("(","").Replace(")","")));
            Rpt1.SetParameters(new ReportParameter("empName", empName));
            Rpt1.SetParameters(new ReportParameter("empId", empId));
            Rpt1.SetParameters(new ReportParameter("empDesig", empDesig));
            Rpt1.SetParameters(new ReportParameter("empDept", empDept));
            Rpt1.SetParameters(new ReportParameter("empSection", empSection));
            Rpt1.SetParameters(new ReportParameter("joinDate", joinDate));
            Rpt1.SetParameters(new ReportParameter("sepDate", sepDate));
            Rpt1.SetParameters(new ReportParameter("effDate", effDate));
            Rpt1.SetParameters(new ReportParameter("serLength", serLength));
            Rpt1.SetParameters(new ReportParameter("daysConMonth", daysConMonth));
            Rpt1.SetParameters(new ReportParameter("noticeduration", noticeduration));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "প্রস্তুতকারী" : "অফিসার মানব সম্পদ ও পে রোল"));
            Rpt1.SetParameters(new ReportParameter("sign4", comcod == "5305" ? "উপ-ব্যবস্থাপক (সাস্টেনিবিলিটি এন্ড সি এস আর)" : "ফ্যাক্টরী ইনচার্জ"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintEmpSettBanGen()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var emplist = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
            var list1 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
            var list2 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");
            string billDate = emplist[0].billdate.ToString("dd-MMM-yyyy");
            string name = emplist[0].empname.ToString();
            string Desgin = emplist[0].designation.ToString();
            string Id = emplist[0].idno.ToString();
            string Section = emplist[0].deptname.ToString();
            string jobseperation = emplist[0].septypedesc.ToString();
            string joining = emplist[0].joindat.ToString("dd-MMM-yyyy");
            string sepdate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            var netamount = (sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
            string servicelength = emplist[0].servleng.ToString();
            double netpay = Convert.ToDouble(netamount);

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattelmentBangla", list1, list2, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "চূড়ান্ত নিষ্পত্তিকরন বিল"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("netamount", netamount));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            rpt1.SetParameters(new ReportParameter("billDate", billDate));
            rpt1.SetParameters(new ReportParameter("name", name));
            rpt1.SetParameters(new ReportParameter("Desgin", Desgin));
            rpt1.SetParameters(new ReportParameter("Id", Id));
            rpt1.SetParameters(new ReportParameter("Section", Section));
            rpt1.SetParameters(new ReportParameter("jobseperation", jobseperation));
            rpt1.SetParameters(new ReportParameter("joining", joining));
            rpt1.SetParameters(new ReportParameter("sepdate", sepdate));
            rpt1.SetParameters(new ReportParameter("servicelength", servicelength));
            rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(netpay, 2)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + 
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save_Value();
                var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
                var newList = sttlmntinfo.Select(l => new {
                    l.hrgcod,
                    l.hrgdesc,
                    l.amount,
                    l.numofday,
                    l.perday,
                    l.ttlamt,
                    l.remarks
                }).ToList();


                string comcod = this.GetComeCode();
                string empid = this.ddlEmpName.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string refno = this.txtrefno.Text.Trim();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string PostedByid = hst["usrid"].ToString();
                string Posttrmid = hst["compname"].ToString();
                string PostSession = hst["session"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                DataTable dt = ASITUtility03.ListToDataTable(newList);
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tbl1";
                bool result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSERT_UPDATE_EMP_SETTLEMNT", ds, null, null, empid, curdate, refno, PostedByid, Posttrmid, PostSession, Posteddat, "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject.ToString() + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Final Settlement Updated Successfully');", true);               

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message+"');", true);              

            }

        }

        private void FooterCalculation()
        {
            string comcod = this.GetComeCode();
            var sttlmntinfo = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
            switch (comcod)
            {
                case "5305":
                case "5306":
                    //((Label)this.gvSettWages.FooterRow.FindControl("lblgvwagesfttl")).Text = sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.amount).ToString("#,##0.00;(#,##0.00); ");                
                    ((Label)this.gvSettEarn.FooterRow.FindControl("lblgvearnfttl")).Text = sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSettDeduct.FooterRow.FindControl("lblgvdedfttl")).Text = sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "353").Sum(p => p.ttlamt).ToString("#,##0.00;(#,##0.00); ");
                    this.lblnetamt.Text = (sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt) - sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "353").Sum(p => p.ttlamt)).ToString("#,##0;(#,##0); ");
                    break;

                default:
                    ((Label)this.gvsettlemntcredit.FooterRow.FindControl("lblfttlamt")).Text = sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvsttlededuct.FooterRow.FindControl("lblfttlamt")).Text = sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt).ToString("#,##0.00;(#,##0.00); ");
                    this.NetAmount.Text = (sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
                    break;
            }           
        }

        private void lnkbtnLedger_Click(object sender, EventArgs e)
        {
            
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "APPROVE_SETTLEMENT_INFO", empid, PostedByid, Posttrmid, PostSession, Posteddat, "", "", "", "", "");
            if (!result)
                return;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Final Settlement Approved Successfully');", true);

            
            ////this.GetEmployeeName();
            //this.ddlEmpName.Items.Clear();
            //this.lbtnOk_Click(null,null);

        }

        protected void gvsettlemntcredit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string hrgcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrgcod")).ToString();

                if (hrgcod == "35101" || hrgcod == "35104")
                {
                    ((TextBox)e.Row.FindControl("txtnumofday")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtperday")).Enabled = false;
                    ((TextBox)e.Row.FindControl("TtlAmout")).Enabled = false;
                }

            }
        }

        protected void rbtnstatement_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowSettlementInfo();
        }
    }
}