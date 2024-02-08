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
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_11_RawInv
{
    public partial class SamplingMatIssue : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
              
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
                this.GetOrderNumber();
                this.GetStore();
                this.GetStock();
                this.GetSample();                
               // this.GetUnitName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIAL ISSUE INFORMATION";


            }
            
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFinalProUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(LbtnReq_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_DataBind();
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
           ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do you want to make new Foreign Purchase Requisition?')";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Make Purchase Req";
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Calculation";


        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void GetOrderNumber()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
          
            string qactcode = this.Request.QueryString["actcode"] ?? "";
            //string qgenno= this.Request.QueryString["genno"] ?? "";
            string serch1 = (qactcode.Length>0? qactcode + "%": "%"+this.txtSrcPro.Text.Trim() + "1661%");


            

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETORDNOFORSAMANDORDER", serch1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlStoreName.DataTextField = "actdesc";
            this.ddlStoreName.DataValueField = "actcode";
            this.ddlStoreName.DataSource = ds1.Tables[0];
            this.ddlStoreName.DataBind();
            if (qactcode.Length > 0)
            {
               // this.ddlStoreName.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.lbtnOk_Click(null, null);
            }
            
        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetOrderNumber();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETPURISUINFO", misuno, misudate,
                    "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblMatIssue"];
            var rptlist = dt.DataTableToList<SPEENTITY.C_11_RawInv.EclassMaterialIssue>();
            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_11_RawInv.RptMaterialIssue", rptlist, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("rptitle", "Material Issue"));
            Rpt1a.SetParameters(new ReportParameter("isuno", misuno));
            Rpt1a.SetParameters(new ReportParameter("isudat", misudate));
            Rpt1a.SetParameters(new ReportParameter("isuref", ds1.Tables[1].Rows[0]["misuref"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("rmrks", ds1.Tables[1].Rows[0]["rmrks"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("postedbyname", ds1.Tables[1].Rows[0]["postedbyname"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("aprvbyname", ds1.Tables[1].Rows[0]["postedbyid"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.lblStoredesc.Text = this.ddlStoreName.SelectedItem.Text.Trim();
                this.ddlStoreName.Visible = false;
                this.lblStoredesc.Visible = true;
                this.PnlProRemarks.Visible = true;                
               //  this.ddlStore.Enabled = false;
               // this.ddlStoreName.Enabled = false;
             //   this.lblStoredesc.Visible = false;
                this.ibtnPreIssueList.Visible = false;
                this.ddlPrevIssueList.Visible = false;
                this.ddlpagesize.Visible = true;
                this.lblPage.Visible = true;
                this.txtCurDate.Enabled = true;
                if (ddlPrevIssueList.Items.Count == 0)
                {
                    this.panel11.Visible = true;
                }
                else
                {
                    this.lblReqList.Visible = false;
                    //this.txtResSearch.Visible = false;
                    //this.ImgbtnFindMatList.Visible = false;
                    this.ddlReqList.Visible = false;
                    this.lbtnSelectReqList.Visible = false;
                    //this.ImgbtnFindReqList.Visible = false;
                    this.panel11.Visible = true;
                }
                //this.lblmsg.Text = "";
                this.ShowIsuInfo();
               
               
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.panel11.Visible = false;
            this.lblStoredesc.Text = "";
            this.txtRemarks.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtlSuRefNo.Text = "";
            this.lblCurNo1.Text = "";
            this.lblCurNo2.Text = "";
            this.ddlPrevIssueList.Items.Clear();
            this.ddlStoreName.Visible = true;
           // this.ddlStore.Enabled = false;
            this.lblStoredesc.Visible = false;
            //this.lblPreList.Visible = true;
            //this.txtSrcIssueNo.Visible = true;
            this.ibtnPreIssueList.Visible = true;
            this.ddlPrevIssueList.Visible = true;
            this.PnlProRemarks.Visible = false;
            this.gvMatIssue.DataSource = null;
            this.ddlpagesize.Visible = false;
            this.lblPage.Visible = false;
            this.txtCurDate.Enabled = true;
            this.gvMatIssue.DataBind();
            this.ddlReqList.Items.Clear();
            this.lblReqList.Visible = true;            
            this.ddlReqList.Visible = true;            
            this.lbtnSelectReqList.Visible = true;
            this.ddlMatList.Items.Clear();
            Session.Remove("tblMatIssue");
        }

        private void ShowIsuInfo()
        {

            string comcod = this.GetComeCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();
            string mReqNo = "NEWREQ";
            if (this.ddlPrevIssueList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mReqNo = this.ddlPrevIssueList.SelectedValue.ToString();
            }
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETSAMPLEPURISUINFO", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblMatIssue"] = HiddenSameData(ds1.Tables[0]);
            Session["tblUserReq"] = ds1.Tables[1];

            //if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval")
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        this.GetApprQty();
            //    }
            //}

            if (mReqNo == "NEWREQ")
            {
                ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETISSUENO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.txtlSuRefNo.Text = ds1.Tables[1].Rows[0]["misuref"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["misuno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["misuno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["misudate"]).ToString("dd-MMM-yyyy");
            this.ddlStoreName.SelectedValue = ds1.Tables[1].Rows[0]["actcode"].ToString();
            // this.ddlStore.SelectedValue = (ds1.Tables[1].Rows[0]["storid"].ToString() != "") ? ds1.Tables[1].Rows[0]["storid"].ToString() : "000000000000";
            this.lblStoredesc.Text = this.ddlStoreName.SelectedItem.Text.Trim();
            this.txtRemarks.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
            this.Data_DataBind();

        }
        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            int i = 0;
            string compcode = dt.Rows[0]["compcode"].ToString();

            foreach (DataRow dr1 in dt.Rows)
            {
                if (i == 0)
                {


                    compcode = dr1["compcode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["compcode"].ToString() == compcode)
                {

                    dr1["compdesc"] = "";

                }


                compcode = dr1["compcode"].ToString();
            }



            return dt;




           
        }

        private void Data_DataBind()
        {
            this.gvMatIssue.DataSource = null;
            this.gvMatIssue.DataBind();

            DataTable dt = (DataTable)Session["tblMatIssue"];
            this.gvMatIssue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatIssue.DataSource = dt;
            this.gvMatIssue.DataBind();

        }
        
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            DataTable dt = (DataTable)Session["tblMatIssue"];
            int rowindex;
            for (int i = 0; i < this.gvMatIssue.Rows.Count; i++)
            {
              //  string untcod = dt.Rows[i]["untcod"].ToString();
               // string conunt = dt.Rows[i]["conunt"].ToString();
                //DataTable tblunit = (DataTable)ViewState["UnitsRate"];
                double conrate = 0;
             


                double issueqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("txtgvissueqty")).Text.Trim());
               // double isuqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("lgvIsuQty")).Text.Trim());

                double isurate = Convert.ToDouble(ASTUtility.ExprToValue(((Label)this.gvMatIssue.Rows[i].FindControl("lgvIsuRate")).Text.Trim()));
              //  double isueqty = (conrate == 0) ? isuqty : (Convert.ToDouble(conqty) / conrate);

                rowindex = (this.gvMatIssue.PageIndex) * this.gvMatIssue.PageSize + i;
             //   dt.Rows[rowindex]["conqty"] = conqty;
                // dt.Rows[rowindex]["balqty"] = Convert.ToDouble("0"+dt.Rows[rowindex]["balqty"])-conqty;
                dt.Rows[rowindex]["isuqty"] = issueqty;
                dt.Rows[rowindex]["isurate"] = isurate;
            }
            //  ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = true;
            Session["tblMatIssue"] = dt;
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_DataBind();

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {

        }

        protected void ibtnPreIssueList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETPREVMISSUELIST", CurDate1, "Normal", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevIssueList.Items.Clear();
            this.ddlPrevIssueList.DataTextField = "misuno1";
            this.ddlPrevIssueList.DataValueField = "misuno";
            this.ddlPrevIssueList.DataSource = ds1.Tables[0];
            this.ddlPrevIssueList.DataBind();
        }

        protected void lbtnSelectReqList_Click(object sender, EventArgs e)
        {

            
            string compcode = this.ddlGroup.SelectedValue.ToString();
            string matcod = this.ddlMatList.SelectedValue.ToString();
            string spcfcod = this.ddlspcflist.SelectedValue.ToString();

          
            DataTable tbl1 = (DataTable)Session["tblMatIssue"];
            DataTable dt2 = (DataTable)Session["tblsampinfo"];
            DataTable dtstk = (DataTable)Session["tblstock"];
            DataRow[] dr3 = dt2.Select("compcode='" + compcode + "' and rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'");
            DataRow[] dr4 = tbl1.Select("compcode='" + compcode + "' and rsircode='" + matcod + "' and spcfcod='" + spcfcod +  "'");

            //DataRow[] drs = dtstk.Select("rsircode ='" + matcod + "' and spcfcod='" + spcfcod + "'");
            //if (drs.Length > 0)
            //{


            //    if (Convert.ToDouble(drs[0]["stkqty"]) > 0)
            //    {
                    if (dr4.Length > 0)
                    {

                    }
                    else
                    {


                        DataRow dr1 = tbl1.NewRow();
                        dr1["inqno"] = dr3[0]["inqno"].ToString();
                        dr1["inqno1"] = dr3[0]["inqno1"].ToString();
                        dr1["bactcode"] = dr3[0]["bactcode"].ToString();
                        dr1["bactdesc"] = dr3[0]["bactdesc"].ToString();
                        dr1["grpid"] = dr3[0]["grpid"].ToString();
                        dr1["compcode"] = dr3[0]["compcode"].ToString();
                        dr1["compdesc"] = this.ddlGroup.SelectedItem.Text;
                        dr1["rsircode"] = dr3[0]["rsircode"].ToString();

                        dr1["rsirdesc"] = dr3[0]["rsirdesc"].ToString();
                        dr1["spcfcod"] = dr3[0]["spcfcod"].ToString();
                        dr1["spcfdesc"] = dr3[0]["spcfdesc"].ToString();
                        dr1["rsirunit"] = dr3[0]["rsirunit"].ToString();
                        dr1["untcod"] = dr3[0]["untcod"].ToString();
                        dr1["samqty"] = dr3[0]["samqty"].ToString();
                        dr1["conqty"] = 0.00;
                        dr1["balqty"] = dr3[0]["balqty"].ToString();
                        dr1["isuqty"] = dr3[0]["balqty"].ToString();
                        dr1["isurate"] = dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'").Length == 0 ? 0.00 : (dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'"))[0]["stkrate"];
                        dr1["stockqty"] = dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'").Length == 0 ? 0.00 : (dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'"))[0]["stkqty"];
                         dr1["ttlconqty"] = dr3[0]["ttlconqty"].ToString();
                        // dr1["actcode"] = dr3[0]["actcode"].ToString();
                        // dr1["actdesc"] = dr3[0]["actdesc"].ToString();
                        //dr1["reqqty"] = dr3[0]["acqty"].ToString();
                        tbl1.Rows.Add(dr1);
                    }
                //}

            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Stock Is Not Available');", true);

                  
            //        return;
            //    }



            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Stock Is Not Available');", true);

             

            //    return;
            //}

            DataView dv1 = tbl1.DefaultView;
            dv1.Sort = ("grpid, rsircode");

            Session["tblMatIssue"] = dv1.ToTable();
            Session["tblMatIssue"] = HiddenSameData((DataTable)Session["tblMatIssue"]);
            this.Data_DataBind();

            //Session["tblMatIssue"] = HiddenSameData(tbl1);

            //this.Data_DataBind();
        }


        protected void lnkSelectAll_Click(object sender, EventArgs e)
        {

            string inqno = this.ddlReqList.SelectedValue.ToString();   
            DataTable dt = (DataTable)Session["tblMatIssue"];
            DataTable dt2 = (DataTable)Session["tblsampinfo"];
            DataTable dtstk = (DataTable)Session["tblstock"];
            //DataRow[] dr3 = dt2.Select("grp='" + grp + "' and rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'");
            DataRow[] dr1 = dt.Select("inqno='" + inqno +"'");            
            if (dr1.Length == 0)
            {

                foreach (DataRow dr2 in dt2.Rows)

                {
                    string grp = dr2["grp"].ToString();
                    string matcod = dr2["rsircode"].ToString();
                    string spcfcod = dr2["spcfcod"].ToString();
                    //DataRow []drs= dtstk.Select("rsircode ='" + matcod + "' and spcfcod='" + spcfcod + "'");
                    //if (drs.Length>0 && Convert.ToDouble(drs[0]["stkqty"]) > 0)
                    //{
                    DataRow dra = dt.NewRow();
                    dra["inqno"] = dr2["inqno"].ToString();
                    dra["inqno1"] = dr2["inqno1"].ToString();
                    dra["bactcode"] = dr2["bactcode"].ToString();
                    dra["bactdesc"] = dr2["bactdesc"].ToString();
                    dra["grpid"] = dr2["grpid"].ToString();
                    dra["compcode"] = dr2["compcode"].ToString();
                    dra["compdesc"] = dr2["compdesc"].ToString();
                    dra["rsircode"] = dr2["rsircode"].ToString();
                    dra["rsirdesc"] = dr2["rsirdesc"].ToString();
                    dra["spcfcod"] = dr2["spcfcod"].ToString();
                    dra["spcfdesc"] = dr2["spcfdesc"].ToString();
                    dra["rsirunit"] = dr2["rsirunit"].ToString();
                    dra["untcod"] = dr2["untcod"].ToString();
                    dra["samqty"] = dr2["samqty"].ToString();
                    dra["conqty"] = 0.00;
                    dra["balqty"] = dr2["balqty"].ToString();
                    dra["isuqty"] = (Convert.ToDouble(ASTUtility.ExprToValue("0"+ dr2["balqty"]))<0)? "0.00": dr2["balqty"].ToString();
                    dra["ttlconqty"] = dr2["ttlconqty"].ToString();
                        dra["isurate"] = dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'").Length == 0 ? 0.00 : (dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'"))[0]["stkrate"];
                    dra["stockqty"] = dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'").Length == 0 ? 0.00 : (dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'"))[0]["stkqty"];
                        
                        dt.Rows.Add(dra);
                        
                    //}

                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Stock Is Not Available');", true);

                      
                    //    return;
                    //}

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Item Already exist in the List');", true);

              
                return;
            }





            DataView dv1 = dt.DefaultView;
            dv1.Sort = ("grpid, rsircode");
            Session["tblMatIssue"] = dv1.ToTable();
            Session["tblMatIssue"] = HiddenSameData((DataTable)Session["tblMatIssue"]);
            this.Data_DataBind();



        }
        protected void gvMatIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvMatIssue.PageIndex = e.NewPageIndex;
            this.Data_DataBind();

        }
        private void GetStore()
        {

           
            string comcod = this.GetComeCode();        
            DataSet ds5 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETSTORE", "", "", "", "", "", "", "", "", "");
            this.ddlStore.DataTextField = "actdesc1";
            this.ddlStore.DataValueField = "actcode";
            this.ddlStore.DataSource = ds5.Tables[0];
            this.ddlStore.DataBind();
            ds5.Dispose();

        }
        private void GetStock()
        {
            Session.Remove("tblstock");
            string comcod = this.GetComeCode();
            string store = this.ddlStore.SelectedValue.ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETSTORESTOCK", store, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                Session["tblstock"] = null;
                return;
            
            }
            Session["tblstock"] = ds1.Tables[0];
            ds1.Dispose();
          //  ds5.Dispose();


        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetStock();
        }
        protected void imgbtnStorid_Click(object sender, EventArgs e)
        {
            this.GetStore();
           

        }
        private void GetSample()
        {
            Session.Remove("tblsampinfo");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string iStore = this.ddlStoreName.SelectedValue.ToString();
            string iDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");           
            string SearchInfo = "";
            string Dprno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            string reqtype = this.Request.QueryString["reptype"].ToString() + "%";
            string isunum = "";
            if (this.ddlPrevIssueList.Items.Count > 0)
            {
                isunum = this.ddlPrevIssueList.SelectedValue.ToString();
            }
            string date = this.txtCurDate.Text.Trim();
            //DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "MATCODELIST", iStore, iDate, SearchInfo, Dprno, reqtype, isunum, "", "", "");
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETSAMPLEINFO", Dprno, date, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblsampinfo"] = ds1.Tables[0];
            //Session["tblsammat"] = ds1.Tables[2];
            //Session["tblgroup"] = ds1.Tables[3];
            Session["tblsample"] = ds1.Tables[1];
            this.ddlReqList.DataTextField = "inqno1";
            this.ddlReqList.DataValueField = "inqno";
            this.ddlReqList.DataSource = ds1.Tables[1];
            this.ddlReqList.DataBind();
            this.ddlReqList_SelectedIndexChanged(null, null);




        }

        private void GetGroup()
        {
            DataTable dt = (DataTable)Session["tblsampinfo"];
            string mInqno = this.ddlReqList.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("inqno='" + mInqno + "'");           
            this.ddlGroup.DataTextField = "compdesc";
            this.ddlGroup.DataValueField = "compcode";
            this.ddlGroup.DataSource = dv.ToTable();
            this.ddlGroup.DataBind();
            this.ddlGroup_SelectedIndexChanged(null, null);



        }
        private void GetMaterial()
        {

            string mInqno = this.ddlReqList.SelectedValue.ToString();
            string compcode= this.ddlGroup.SelectedValue.ToString();
            DataTable dt1 = (DataTable)Session["tblsampinfo"];
            DataTable dt2 = new DataTable();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("inqno='" + mInqno+"' and compcode='"+ compcode + "'");
            //dt2 = dv.ToTable(true, "rsirdesc1", "rsircode");
            this.ddlMatList.DataTextField = "rsirdesc";
            this.ddlMatList.DataValueField = "rsircode";
            this.ddlMatList.DataSource = dv.ToTable();
            this.ddlMatList.DataBind();
            ddlMatList_SelectedIndexChanged(null, null);       
        }

        private void GetSpecification() 
        {

            DataTable matlist = (DataTable)Session["tblsampinfo"];
            string mInqno = this.ddlReqList.SelectedValue.ToString();
            string compcode = this.ddlGroup.SelectedValue.ToString();
            string mResCode = this.ddlMatList.SelectedValue.ToString();
            DataView dv2 = matlist.DefaultView;
            dv2.RowFilter = ("inqno='" + mInqno + "' and compcode='" + compcode +  "' and rsircode='" + mResCode + "'");          
            this.ddlspcflist.DataTextField = "spcfdesc";
            this.ddlspcflist.DataValueField = "spcfcod";
            this.ddlspcflist.DataSource = dv2.ToTable(); 
            this.ddlspcflist.DataBind();

        }

        protected void ddlMatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();
           
        }
        protected void ddlReqList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsample"];            

            this.GetGroup();
            if (dt.Rows.Count == 0)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "inqno= '" + this.ddlReqList.SelectedValue.ToString() + "'";
            this.TxtSamSize.Text = dv.ToTable().Rows[0]["samsize"].ToString();
            this.TxtTargqtu.Text = dv.ToTable().Rows[0]["samqty"].ToString();
            this.TxtUnit.Text = dv.ToTable().Rows[0]["unitdesc"].ToString();
            if (Convert.ToDouble(dv.ToTable().Rows[0]["fgrcvqty"]) == 0)
            {
                this.TxtFgQty.Text = Convert.ToDouble(dv.ToTable().Rows[0]["samqty"].ToString()).ToString("#,##;(#,##)");

            }
            else
            {
                this.TxtFgQty.Text = Convert.ToDouble(dv.ToTable().Rows[0]["fgrcvqty"]).ToString("#,##;(#,##)");

            }

        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterial();
        }


        protected void GetUnitName()
        {
            string comcod = this.GetComeCode();
            DataSet ds = PurData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_CONVRT_INF", "", "", "", "", "", "", "");
            Session["UnitsRate"] = ds.Tables[0];
        }
    
        protected void GetPerMatIssu()
        {
            string comcod = this.GetComeCode();
            string mREQNO = "NEWISU";
            if (this.ddlPrevIssueList.Items.Count > 0)
                mREQNO = this.ddlPrevIssueList.SelectedValue.ToString();

            string mISUDAT = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();
            if (mREQNO == "NEWISU")
            {
                DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETISSUENO", mISUDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxisuno"].ToString();
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
                    this.ddlPrevIssueList.DataTextField = "maxisuno1";
                    this.ddlPrevIssueList.DataValueField = "maxisuno";
                    this.ddlPrevIssueList.DataSource = ds2.Tables[0];
                    this.ddlPrevIssueList.DataBind();
                }
            }
        }

        protected void lbtnFinalProUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                this.SaveValue();
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)Session["tblMatIssue"];
                DataRow[] drs = dt.Select(" isuqty>stockqty ");
                if (drs.Length>0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Stock Is Not Available');", true);


                    return;
                }
                if (ddlPrevIssueList.Items.Count == 0)
                {
                    this.GetPerMatIssu();
                }

                string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string actcode = this.ddlStoreName.SelectedValue.ToString();
                //   string storid = this.ddlStore.SelectedValue.ToString();

                string Remarks = this.txtRemarks.Text.Trim();
                string misuref = this.txtlSuRefNo.Text.Trim();
                bool result;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                DataTable dtuser = (DataTable)Session["tblUserReq"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
                string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
                //string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
                //string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : (tblEditByid == "") ? userid : tblEditByid;
                string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : (tblEditDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblEditDat;
                string qreqtype = this.Request.QueryString["reptype"].ToString() ?? "";
                string reqtype = (qreqtype.Length > 0) ? qreqtype : "Normal";



                result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "UPDATEPURMISSUEINFO", "MATISSUEB", misuno, misudate,
                      Remarks, misuref, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, reqtype, "", "");

                //result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "MATISSUEB", misuno, actcode, misudate,
                //      Remarks, misuref, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ PurData.ErrorObject["Msg"].ToString() + "');", true);
                    
                    return;
                }

                string storeid = this.ddlStore.SelectedValue.ToString();
               
                     
                foreach (DataRow dr1 in  dt.Rows)
                {
                    
                    string inqno = dr1["inqno"].ToString();
                    double isuqty = Convert.ToDouble(dr1["isuqty"].ToString());
                    double isuamt = isuqty * Convert.ToDouble(dr1["isurate"].ToString());
                    string orderno = dr1["bactcode"].ToString();
                    string compcode = dr1["compcode"].ToString();
                    string rsircode = dr1["rsircode"].ToString();
                    string spcfcod = dr1["spcfcod"].ToString();
                    string conqty = dr1["conqty"].ToString();
                    string conunt = "";
                   
                    if (isuqty > 0)
                    {
                        result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "UPDATEPURMISSUEINFO", "MATISSUEA", misuno, inqno, orderno, rsircode, isuqty.ToString(), spcfcod, conunt, conqty.ToString(), storeid, compcode, isuamt.ToString(), "", "");
                        if (!result)
                            return;
                    }

                }

               
                string sdino = this.ddlReqList.SelectedValue.ToString();
                string fgqty =Convert.ToDouble("0"+this.TxtFgQty.Text.Trim()).ToString();
                result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAMPLE_FG_RECEIVE", sdino, fgqty);

                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);

               

            }
        }

       
        protected void ImgbtnFindMatList_Click(object sender, EventArgs e)
        {

        }





        protected void gvMatIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                double isuqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "isuqty"));
                double stockqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "stockqty"));
                if (isuqty > stockqty || stockqty==0 || stockqty<0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;
                    e.Row.ToolTip = "Stock Empty";

                }
               

            }
        }

        protected void LbtnBatchUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string ddlStore = this.ddlStore.SelectedValue.ToString();
            string sircode = ((Label)this.gvMatIssue.Rows[index].FindControl("lblrsircode")).Text.ToString();
            string spcfcod = ((Label)this.gvMatIssue.Rows[index].FindControl("lblspcfcod")).Text.ToString();


            DataSet result = PurData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETSTKBALFIFO", misudate, ddlStore, sircode, spcfcod, misuno);
            if (result == null)
            {
                return;
            }
            Session["tblBatchData"] = result.Tables[0];
            this.gvBatchDetails.DataSource = result.Tables[0];
            this.gvBatchDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenBatchModal();", true);
        }

        private void SaveValue_Issue_Pipo()
        {
            DataTable dt = (DataTable)Session["tblBatchData"];
            int rowindex;
            for (int i = 0; i < this.gvBatchDetails.Rows.Count; i++)
            {
                string lateappsta = (((CheckBox)this.gvBatchDetails.Rows[i].FindControl("chkack")).Checked == true) ? "True" : "False";
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvBatchDetails.Rows[i].FindControl("txtgvqty")).Text.Trim());

                rowindex = (this.gvBatchDetails.PageIndex) * this.gvBatchDetails.PageSize + i;
                dt.Rows[rowindex]["qty"] = qty;
                dt.Rows[rowindex]["approved"] = lateappsta;

            }
            Session["tblBatchData"] = dt;
        }
        protected void ModalUpdateBtn_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetComeCode();
            this.SaveValue_Issue_Pipo();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblBatchData"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("approved='True'");
            dt = dv.ToTable();
            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");

            string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string rsircode = dt.Rows[0]["rsircode"].ToString();
            string spcfcod = dt.Rows[0]["spcfcod"].ToString();

            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tbl1";


            bool result = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "UPDATEBATCHNO", ds, null, null, misuno, rsircode, spcfcod, userid, Terminal, Sessionid, Posteddat);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

           




        }


        protected void gvMatIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblMatIssue"];
            int index = (this.gvMatIssue.PageIndex) * this.gvMatIssue.PageSize + e.RowIndex;
            dt.Rows.RemoveAt(index);
            Session["tblMatIssue"] = dt;
            this.Data_DataBind();
        }

        protected void lnkbtndel_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblMatIssue"];
            string comcod = this.GetComeCode();
            int index = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string inqno = this.ddlReqList.SelectedValue.ToString();
            string compcode = dt.Rows[index]["compcode"].ToString();
            string rescode = dt.Rows[index]["rsircode"].ToString();
            string spcfcode = dt.Rows[index]["spcfcod"].ToString();
            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            bool result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "DELISSUEDMAT", inqno, compcode, rescode, spcfcode, misuno);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+ PurData.ErrorObject["Msg"].ToString() + "');", true);

               
                return;
            }
            dt.Rows[index].Delete();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Deleted Successfully');", true);


            DataView dv = dt.DefaultView;
            Session["tblMatIssue"] = dv.ToTable();
            this.Data_DataBind();
        }

        protected void LbtnReq_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = this.GetComeCode();
            string store = this.ddlStore.SelectedValue.ToString();
            string date = this.txtCurDate.Text.Trim();
            string remarks = this.txtRemarks.Text.Trim();
            string sdino = this.ddlReqList.SelectedValue.ToString();
            string refno = this.txtlSuRefNo.Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string postedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable reqtble = ((DataTable)Session["tblMatIssue"]).Clone();
            for (int i = 0; i < this.gvMatIssue.Rows.Count; i++)
            {
                bool chk = ((CheckBox)this.gvMatIssue.Rows[i].FindControl("ChckReq")).Checked;
                DataRow dr = ((DataTable)Session["tblMatIssue"]).Rows[i];
                if (chk)
                {
                    reqtble.ImportRow(dr);
                }                
            }

            if (reqtble.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('There has no Item selected for Requisition');", true);

                return;
            }

            DataSet ds = new DataSet("ds1");
             ds.Tables.Add(reqtble);
            ds.Tables[0].TableName = "tbl1";
            string xdat = ds.GetXml();
            bool result = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "MATERIAL_REQUISITION_FROM_SAMPLING", ds, null,null, store, date, remarks, sdino, userid, Terminal, Sessionid, postedDate, refno);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Requisition Create Successfully');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong');", true);

            }

        }

        protected void ChkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
                       
            int i;
            if (((CheckBox)this.gvMatIssue.HeaderRow.FindControl("ChkAllEmp")).Checked)
            {

                for (i = 0; i < this.gvMatIssue.Rows.Count; i++)
                {
                    ((CheckBox)this.gvMatIssue.Rows[i].FindControl("ChckReq")).Checked = true;
                   

                }

            }

            else
            {
                for (i = 0; i < this.gvMatIssue.Rows.Count; i++)
                {
                    ((CheckBox)this.gvMatIssue.Rows[i].FindControl("ChckReq")).Checked = false;
                  
                }
            }
          
        }


        protected void LbtnAllDprSingleMat_Click(object sender, EventArgs e)
        {
            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string matcod = this.ddlMatList.SelectedValue.ToString();
            string spcfcod = this.ddlspcflist.SelectedValue.ToString();
            string compcode = this.ddlGroup.SelectedValue.ToString();        
            DataTable tbl1 = (DataTable)Session["tblMatIssue"];
            DataTable tblunit = (DataTable)ViewState["UnitsRate"];
            DataTable dt2 = (DataTable)Session["tblsampinfo"];
            DataTable dtstk = (DataTable)Session["tblstock"];
            DataRow[] dr3 = dt2.Select(" rsircode='" + matcod + "' and spcfcod='" + spcfcod + "' and compcode='" + compcode + "'");

            foreach (DataRow item in dr3)
            {


                DataRow[] dr4 = tbl1.Select("inqno='" + item["inqno"] + "' and rsircode='" + matcod + "' and spcfcod='" + spcfcod + "' and compcode='" + compcode + "'");
                if (dr4.Length > 0)
                {
                    if (dr4[0]["rsircode"].ToString() == matcod && dr4[0]["spcfcod"].ToString() == spcfcod)
                    {
                        return;
                    }
                }
                else
                {

                   
                    DataRow dr1 = tbl1.NewRow();
                    dr1["inqno"] = dr3[0]["inqno"].ToString();
                    dr1["inqno1"] = dr3[0]["inqno1"].ToString();
                    dr1["bactcode"] = dr3[0]["bactcode"].ToString();
                    dr1["bactdesc"] = dr3[0]["bactdesc"].ToString();
                    dr1["grpid"] = dr3[0]["grpid"].ToString();
                    dr1["compcode"] = dr3[0]["compcode"].ToString();
                    dr1["compdesc"] = this.ddlGroup.SelectedItem.Text;
                    dr1["rsircode"] = dr3[0]["rsircode"].ToString();
                    dr1["rsirdesc"] = dr3[0]["rsirdesc"].ToString();
                    dr1["spcfcod"] = dr3[0]["spcfcod"].ToString();
                    dr1["spcfdesc"] = dr3[0]["spcfdesc"].ToString();
                    dr1["rsirunit"] = dr3[0]["rsirunit"].ToString();
                    dr1["untcod"] = dr3[0]["untcod"].ToString();
                    dr1["samqty"] = dr3[0]["samqty"].ToString();
                    dr1["conqty"] = 0.00;
                    dr1["balqty"] = dr3[0]["balqty"].ToString();
                    dr1["isuqty"] = dr3[0]["balqty"].ToString();
                    dr1["isurate"] = dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'").Length == 0 ? 0.00 : (dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'"))[0]["stkrate"];
                    dr1["stockqty"] = dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'").Length == 0 ? 0.00 : (dtstk.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'"))[0]["stkqty"];
                    dr1["ttlconqty"] = dr3[0]["ttlconqty"].ToString();
                    tbl1.Rows.Add(dr1);
                }
            }
            Session["tblMatIssue"] = tbl1;
            Session["tblMatIssue"] = HiddenSameData((DataTable)Session["tblMatIssue"]);
            this.Data_DataBind();
            adjustment();
        }

        private void adjustment()
        {
            DataTable dt = (DataTable)Session["tblMatIssue"];
            if (dt.Rows.Count > 0)
            {
                var newDt = dt.AsEnumerable()
                                      .GroupBy(r => new {                                         
                                          rsircode = r.Field<string>("rsircode"),
                                          spcfcod = r.Field<string>("spcfcod"),
                                          compcode = r.Field<string>("compcode")
                                      })
                                      .Select(g =>
                                      {
                                          var row = dt.NewRow();                                         
                                          row["rsircode"] = g.Key.rsircode;
                                          row["spcfcod"] = g.Key.spcfcod;
                                          row["compcode"] = g.Key.compcode;
                                          row["compdesc"] = g.First()["compdesc"];                                        
                                          row["rsirdesc"] = g.First()["rsirdesc"];
                                          row["spcfdesc"] = g.First()["spcfdesc"];
                                          row["rsirunit"] = g.First()["rsirunit"];
                                          row["stockqty"] = g.First()["stockqty"];
                                          row["isuqty"] = g.Sum(r => r.Field<decimal>("isuqty"));
                                          row["ttlconqty"] = g.Sum(r => r.Field<decimal>("ttlconqty"));
                                          row["conqty"] = g.Sum(r => r.Field<decimal>("conqty"));


                                          return row;
                                      }).CopyToDataTable();

                Session["tblissueHelper"] = newDt;

                this.gvtblIsueHelper_DataBind();
            }
        }


        private void gvtblIsueHelper_DataBind()
        {
            DataTable dtisue = (DataTable)Session["tblissueHelper"];

            this.gvIsuItem.DataSource = dtisue;
            this.gvIsuItem.DataBind();

            //((Label)this.gvIsuItem.FooterRow.FindControl("lgvRSumSMRRQty")).Text = (listsum.Select(p => p.mrrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.mrrqty).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalBalQty")).Text = (listsum.Select(p => p.orderbal).Sum() == 0.00) ? "0" : listsum.Select(p => p.orderbal).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalReceived")).Text = (listsum.Select(p => p.recup).Sum() == 0.00) ? "0" : listsum.Select(p => p.recup).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalOrdQty")).Text = (listsum.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.ordrqty).Sum().ToString("#,##0.00;(#,##0.00); ");

        }
        protected void LbtnRecItemCalculate_Click(object sender, EventArgs e)
        {
            DataTable dtRec = (DataTable)Session["tblissueHelper"];
            //    var listsum = dtRec.DataTableToList<SumClass>();
            var sum = 0.00;

            for (int i = 0; i < this.gvIsuItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvIsuItem.Rows[i].FindControl("txtgvSMRRQty")).Text.Trim()));
                sum += Qty;
                dtRec.Rows[i]["conqty"] = Qty;

                if (Qty == 0)
                    continue;
                DataTable dt = (DataTable)Session["tblMatIssue"];

                DataRow[] dr3 = dt.Select("rsircode='" + dtRec.Rows[i]["rsircode"] + "' and spcfcod='" + dtRec.Rows[i]["spcfcod"] + "' and compcode='" + dtRec.Rows[i]["compcode"] + "'");

                foreach (DataRow item in dr3)
                {


                    if (Convert.ToDouble(item["balqty"]) < Qty)
                    {
                        item["isuqty"] = item["balqty"];
                        Qty = Qty - Convert.ToDouble(item["balqty"]);
                    }
                    else
                    {
                        item["isuqty"] = Qty;
                        Qty = 0;
                        break;
                    }
                }
                Session["tblMatIssue"] = dt;

            }
            Session["tblissueHelper"] = dtRec;
            this.Data_DataBind();
            this.gvtblIsueHelper_DataBind();
        }

        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Expand")
            {
                this.gvIsuItem.Visible = true;
                this.LbtnReqItemShow.Text = "Collapse";
            }
            else
            {
                this.gvIsuItem.Visible = false;
                this.LbtnReqItemShow.Text = "Expand";
            }
        }

        protected void LbtnToClear_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblMatIssue"];
            foreach (DataRow item in dt.Rows)
            {
                item["isuqty"] = 0;
            }
            Session["tblMatIssue"] = dt;
            this.Data_DataBind();
        }
    }
}