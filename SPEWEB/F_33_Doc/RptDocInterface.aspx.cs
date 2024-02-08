using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using AjaxControlToolkit;
using System.Net.Mail;

namespace SPEWEB.F_33_Doc
{
    public partial class RptDocInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Document Management Interface";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy"); //Convert.ToDateTime("01-Jan-2019").ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();
                this.PnlInt.Visible = true;
                this.DocInterFace();               
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
               
             

            }

       
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            //  ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


      
      
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int day = ASTUtility.Datediffday(Convert.ToDateTime(this.txtFDate.Text), Convert.ToDateTime(this.txtdate.Text));
            if (day != 0)
                return;
            txtdate_TextChanged(null, null);


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

     

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.DocInterFace();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.DocInterFace();
        }
        private void DocInterFace()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();

            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GET_DOC_MGT_INTERFACES", Date1, Date2, usrid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbldocdata"] = ds1.Tables[0];
            ViewState["tbldocProcess"] = ds1.Tables[1];

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[2].Rows[0]["dstatus"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Status </div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[2].Rows[0]["appprocess"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Approval Process</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[2].Rows[0]["finalapp"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> Final Approval </div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[2].Rows[0]["completed"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> Completed</div></div></div>";

         
            RadioButtonList1_SelectedIndexChanged(null, null);


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string supuser = this.GetCompCode() + "001";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tbldocdata"];
            DataTable dtprocess = (DataTable)ViewState["tbldocProcess"];

            DataTable Tempdt = new DataTable();
            DataView Tempdv = new DataView();
            switch (value)
            {
                case "0": ///All Doc list
                    this.pnlallInqList.Visible = true;
                    this.pnlApprovalProcess.Visible = false;
                    this.PanlFinalApproval.Visible = false;
                    this.PanlOrdAcRej.Visible = false;                   
                    this.Data_Bind("gvDocStatus", dt);
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1": ///Approval Process
                    this.pnlallInqList.Visible = false;
                    this.pnlApprovalProcess.Visible = true;
                    this.PanlFinalApproval.Visible = false;
                    this.PanlOrdAcRej.Visible = false;
                    Tempdt = dtprocess.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("curstep<>'0000000' and tstep<>'9999999' and curstep<>'9901099'");
                    this.Data_Bind("gvdocprocess", Tempdv.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2": ///Final Approval
                    this.pnlallInqList.Visible = false;
                    this.pnlApprovalProcess.Visible = false;
                    this.PanlFinalApproval.Visible = true;
                    this.PanlOrdAcRej.Visible = false;
                  
                    Tempdt = dtprocess.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("curstep='9901099'");
                    this.Data_Bind("gvFinalApprvl", Tempdv.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3": ///Completed
                    this.pnlallInqList.Visible = false;
                    this.pnlApprovalProcess.Visible = false;
                    this.PanlFinalApproval.Visible = false;
                    this.PanlOrdAcRej.Visible = true;

                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("curstep='9999999'");
                    //Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved=''");
                    this.Data_Bind("gvCompleted", Tempdv.ToTable());
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
               
       


            }
        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvDocStatus":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvDocStatus.DataSource = dt;
                    this.gvDocStatus.DataBind();
                    break;
                case "gvdocprocess":
                    this.gvdocprocess.DataSource = (dt);
                    this.gvdocprocess.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvFinalApprvl":
                    this.gvFinalApprvl.DataSource = (dt);
                    this.gvFinalApprvl.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;  //
                case "gvCompleted":
                    this.gvCompleted.DataSource = (dt);
                    this.gvCompleted.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
             
            }
        }
        protected void gvDocStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                LinkButton LbtnForward = (LinkButton)e.Row.FindControl("LbtnForward");
                //HyperLink InprPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                //HyperLink HypCondir = (HyperLink)e.Row.FindControl("HypCondir");
                //HyperLink HypConcom = (HyperLink)e.Row.FindControl("HypConcom");            
                //HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                //HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");



                string curstep = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "curstep")).Trim().ToString();

                string docno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "docno")).Trim().ToString();
               

               if(curstep!="0000000")
                {
                    e.Row.Attributes.CssStyle.Add("background-color", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "filcolor")).Trim().ToString()); // = System.Drawing.Color.LightSkyBlue;
                    lnkEdit.Visible = false;
                    LbtnForward.Visible = false;
                }
                else
                {
                    lnkEdit.NavigateUrl = "~/F_33_Doc/DocUpload?Type=Edit&genno=" + docno;
                    lnkEdit.Visible = true;
                    LbtnForward.Visible = true;
                }
                   
                   // lnkCheck.NavigateUrl = "~/F_01_Mer/SampleInquiry?Type=Approv&genno=" + inqno;
                //}
                //else
                //{
                //    lnkEdit.Text = "<span class='glyphicon glyphicon-lock'></span>";
                //    lnkEdit.CssClass = "btn btn-xs btn-none";
                //    lnkEdit.ToolTip = "Approved";

                //    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                //    lnkCheck.CssClass = "btn btn-xs btn-none";
                //    lnkCheck.ToolTip = "Locked";
               // }
                      
            }
        }

        protected void gvConSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnCons = (HyperLink)e.Row.FindControl("lbtnCons");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");
                HyperLink HyConsPrint = (HyperLink)e.Row.FindControl("HyConsPrint");
                HyperLink HyCommConsPrint = (HyperLink)e.Row.FindControl("HyCommConsPrint");

                LinkButton btnDelInq = (LinkButton)e.Row.FindControl("btnDelInq");


                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string dconstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dconstatus"));
                string conusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conusrid"));

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();



                if (conusrid == "")
                {
                    lbtnCons.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=Entry&actcode=" + inqno + "&genno=" + styleid;

                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    lbtnCons.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=Entry&actcode=" + inqno + "&genno=" + styleid;
                    lnkCheck.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=ConApp&actcode=" + inqno + "&genno=" + styleid;

                    HyConsPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=ConSheetPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                    HyCommConsPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommConSheetPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;

                }

                if (dconstatus == "Ok")
                {

                    btnDelInq.Enabled = false;
                }


                //Session["Report1"] = gvConSheet;
                //((HyperLink)this.gvConSheet.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvPreCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtnCost = (HyperLink)e.Row.FindControl("lbtnCost");
                HyperLink lnkCheck = (HyperLink)e.Row.FindControl("lnkCheck");

                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                LinkButton btnDelCons = (LinkButton)e.Row.FindControl("btnDelCons");


                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();

                string conapp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conapp"));
                string pcosusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pcosusrid"));
                string dprecostst = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dprecostst"));


                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();



                if (pcosusrid == "")
                {
                    lbtnCost.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=PreCosting&actcode=" + inqno + "&genno=" + styleid;

                    lnkCheck.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkCheck.CssClass = "btn btn-xs btn-none";
                    lnkCheck.ToolTip = "Data Not Found";
                }

                else
                {
                    lbtnCost.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=PreCosting&actcode=" + inqno + "&genno=" + styleid;
                    lnkCheck.NavigateUrl = "~/F_01_Mer/ConsumptionSheet?Type=PreCostingApp&actcode=" + inqno + "&genno=" + styleid;

                }
                // // if (dprecostst == "Ok")
                //   {

                //  btnDelCons.Enabled = false; // this button enable as per sumon vi req. when consumption completed and forwarded to CBD and CBD Complete  so that button should be disable
                // }

                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
            }

        }
        protected void gvOrdAcRej_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HyProdPlan = (HyperLink)e.Row.FindControl("HyProdPlan");

                HyperLink HyPreCostPrint = (HyperLink)e.Row.FindControl("HyPreCostPrint");
                HyperLink HyCommPreCostPrint = (HyperLink)e.Row.FindControl("HyCommPreCostPrint");

                string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).Trim().ToString();
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();

                string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                HyProdPlan.NavigateUrl = "~/F_15_Pro/ProdPlanTopSheet?Type=Datewise";
                HyProdPlan.ToolTip = "Production Plan";

                HyPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
                HyCommPreCostPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=CommPreCostPrint&inqno=" + inqno + "&styleid=" + styleid + "&printtype=" + printType;
            }

        }
       

        protected void btnDelInq_Click(object sender, EventArgs e)
        {
            ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
            string url = "ConsumptionSheet?Type=ConApp";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string inqno = ((Label)this.gvdocprocess.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_MERCHAN_INTERFACE", "REV_ALL_MARCHAND_PROCESS", "INQ", inqno);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Not Sucessfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Reverse Sucessfully";
            ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }

     
        protected void LbtnForward_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string curtime = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            string DocNo = ((Label)this.gvDocStatus.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            string fstep = ((Label)this.gvDocStatus.Rows[index].FindControl("lblfstep")).Text.ToString();
            string tstep = ((Label)this.gvDocStatus.Rows[index].FindControl("lbltstep")).Text.ToString();
            string frwtime = System.DateTime.Now.ToString("dd-MMM-yyyy")+" "+((Label)this.gvDocStatus.Rows[index].FindControl("blFrwtime")).Text.ToString();
            bool timeflag =Convert.ToBoolean(((Label)this.gvDocStatus.Rows[index].FindControl("LBlTimeFlag")).Text);
            if(timeflag == true && Convert.ToDateTime(curtime)> Convert.ToDateTime(frwtime))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Time Over! You are Not Eligible to Forward');", true);
                return;
            }

            string usrid = hst["usrid"].ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "UPDATE_DOC_TRANS_LOG", DocNo, fstep, tstep, usrid, "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Forward Successfully');", true);
                return;
            }
            else
            {
                ConfimMail(tstep, "PO Approval Notifications Email,", "", "Dear Sir, Hope you are doing well, You have new file /PO to approve. Please Check your software interfaces and Approve this PO. Thank you");
                DocInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Forward Successfully');", true);
                return;
            }
        }
        private void ShowDocFiles(string DocNo) {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_DOC", "SHOW_DOC_INFO", DocNo, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            else if (ds1.Tables[1].Rows.Count == 0)
            {
                this.FileTabs.InnerHtml = "";
                this.myTabContent.InnerHtml = "<div class='alert alert-danger'>Nothing to display</div>";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
                return;
            }
            DataTable dt = ds1.Tables[1];
            string tabs = String.Empty;
            string content = String.Empty;
            int i = 1;
            string cssclss = String.Empty;
            this.myTabContent.InnerHtml = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 1)
                {
                    cssclss = "active show";
                }
                else
                {
                    cssclss = "";
                }

                tabs += "<li class=\"nav-item\">" +
                             "<a class=\"nav-link " + cssclss + "\" data-toggle=\"tab\" href=\"#T" + dr["gcod"].ToString() + "\"> DOC-" + i + "</a>" +
                        "</li>";
                // make content visible//
                string extension = Path.GetExtension(dr["fileurl"].ToString());
                string midcontent = String.Empty;
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":

                        midcontent = "<img class='img img-responsive' src='" + ResolveUrl(dr["fileurl"].ToString()) + "'><br> <a target='_blank' href='" + ResolveUrl(dr["fileurl"].ToString()) + "' class='btn btn-sm btn-info'>Preview/Download</a>";
                        break;
                    case ".PDF":
                    case ".pdf":
                        midcontent = "<embed style='height:400px; width:100%;' src='" + ResolveUrl(dr["fileurl"].ToString()) + "'><br><a target='_blank' href='" + ResolveUrl(dr["fileurl"].ToString()) + "' class='btn btn-sm btn-info'>Preview/Download</a>";
                        //  string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"400px\" height=\"400px\"></object>";
                        //   embedpdf.Text = string.Format(embed, ResolveUrl("~/ftpdown/" + dr["fileurl"].ToString().Trim()));
                        //   p.Controls.Add(embedpdf);
                        break;
                    case ".xls":
                    case ".xlsx":
                        midcontent =
                            "<section class=\"card card-figure\">" +
                      "<figure class=\"figure\">" +
                        "<div class=\"figure-img\">" +
                          "<img class=\"img-fluid\" src=" + ResolveUrl("~/Images/Excel-Icon.png") + " alt=\"Card image cap\">" +
                          "<div class=\"figure-action\">" +
                            "<a href = " + ResolveUrl(dr["fileurl"].ToString()) + " class=\"btn btn-block btn-sm btn-primary\">Download</a>" +
                          "</div>" +
                        "</div>" +
                        "<figcaption class=\"figure-caption\">" +
                          "<h6 class=\"figure-title\">" +
                           " <a href = " + ResolveUrl(dr["fileurl"].ToString()) + " > " + dr["gdesc"].ToString() + "</a>" +
                          "</h6>" +
                          "<p class=\"text-muted mb-0\"> Updated On: " + dr["rowdat"].ToString() + ", Posted by: " + dr["usrname"].ToString() + "</p>" +
                        "</figcaption>" +
                      "</figure>" +
                    "</section>";
                        break;
                    case ".doc":
                    case ".docx":
                        midcontent =
                              "<section class=\"card card-figure\">" +
                        "<figure class=\"figure\">" +
                          "<div class=\"figure-img\">" +
                            "<img class=\"img-fluid\" src=" + ResolveUrl("~/Images/word.png") + " alt=\"Card image cap\">" +
                            "<div class=\"figure-action\">" +
                              "<a href = " + ResolveUrl(dr["fileurl"].ToString()) + " class=\"btn btn-block btn-sm btn-primary\">Download</a>" +
                            "</div>" +
                          "</div>" +
                          "<figcaption class=\"figure-caption\">" +
                            "<h6 class=\"figure-title\">" +
                             " <a href = " + ResolveUrl(dr["fileurl"].ToString()) + " > " + dr["gdesc"].ToString() + "</a>" +
                            "</h6>" +
                            "<p class=\"text-muted mb-0\"> Updated On: " + dr["rowdat"].ToString() + ", Posted by: " + dr["usrname"].ToString() + "</p>" +
                          "</figcaption>" +
                        "</figure>" +
                      "</section>";
                        break;
                    default:
                        midcontent = "<img class='img  img-thumbnail' src='" + ResolveUrl("~/Content/Theme/images/avatars/human_avatar.png") + "'><br><a target='_blank' href='" + ResolveUrl(dr["fileurl"].ToString()) + "' class='btn btn-sm btn-info'>Preview/Download</a>";
                        break;

                }
                content += "<div class=\"tab-pane fade " + cssclss + "\" id='T" + dr["gcod"].ToString() + "'>" +
                 //dr["gcod"].ToString()
                 midcontent
                + "</div>";
                i++;
            }


            this.FileTabs.InnerHtml = tabs;
            this.myTabContent.InnerHtml = content;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }


        protected void txtgvDocno_Click(object sender, EventArgs e)
        {
            
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string DocNo = ((Label)this.gvDocStatus.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            ShowDocFiles(DocNo);
        }

        protected void gvdocprocess_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txtgvProcessDocno_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string DocNo = ((Label)this.gvdocprocess.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            ShowDocFiles(DocNo);
        }

        protected void LbtnForwardProcess_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string curtime = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            string DocNo = ((Label)this.gvdocprocess.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            string curstep = ((Label)this.gvdocprocess.Rows[index].FindControl("lblfstep")).Text.ToString();
            string Tostep = ((Label)this.gvdocprocess.Rows[index].FindControl("lbltstep")).Text.ToString();
            string comments = ((TextBox)this.gvdocprocess.Rows[index].FindControl("TxtgvComments")).Text.ToString();

            string frwtime = System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + ((Label)this.gvdocprocess.Rows[index].FindControl("blFrwtime")).Text.ToString();
            bool timeflag = Convert.ToBoolean(((Label)this.gvdocprocess.Rows[index].FindControl("LBlTimeFlag")).Text);
            if (timeflag == true && Convert.ToDateTime(curtime) > Convert.ToDateTime(frwtime))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Time Over! You are Not Eligible to Forward');", true);
                return;
            }
            string usrid = hst["usrid"].ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "UPDATE_DOC_TRANS_LOG", DocNo, curstep, Tostep, usrid, comments);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Forward Successfully');", true);
                return;
            }
            else
            {
                ConfimMail(Tostep, "PO Approval Notifications Email,", "", "Dear Sir, Hope you are doing well, You have new file /PO to approve. Please Check your software interfaces and Approve this PO. Thank you");
                DocInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Forward Successfully');", true);
                return;
            }
        }

        protected void LbtnFinalProcess_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string DocNo = ((Label)this.gvFinalApprvl.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            string curstep = ((Label)this.gvFinalApprvl.Rows[index].FindControl("lblfstep")).Text.ToString();
            string Tostep = ((Label)this.gvFinalApprvl.Rows[index].FindControl("lbltstep")).Text.ToString();
            string comments = ((TextBox)this.gvFinalApprvl.Rows[index].FindControl("TxtgvComments")).Text.ToString();

            string usrid = hst["usrid"].ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "UPDATE_DOC_TRANS_LOG", DocNo, curstep, Tostep, usrid, comments);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Forward Successfully');", true);
                return;
            }
            else
            {
                ConfimMail(Tostep, "PO Approval Notifications Email,", "", "Dear Sir, Hope you are doing well, You have new file /PO to approve. Please Check your software interfaces and Approve this PO. Thank you");
                DocInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Forward Successfully');", true);
                return;
            }
        }

        protected void txtgvCompletedDocno_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string DocNo = ((Label)this.gvCompleted.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            ShowDocFiles(DocNo);
        }

        protected void txtgvFinalAppDocno_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string DocNo = ((Label)this.gvFinalApprvl.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            ShowDocFiles(DocNo);
        }

        protected void LbtnBackward_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string DocNo = ((Label)this.gvdocprocess.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            string curstep = ((Label)this.gvdocprocess.Rows[index].FindControl("lblfstep")).Text.ToString();
            string prevstep = ((Label)this.gvdocprocess.Rows[index].FindControl("lblPrevStep")).Text.ToString();
            string comments = ((TextBox)this.gvdocprocess.Rows[index].FindControl("TxtgvComments")).Text.ToString();

            string usrid = hst["usrid"].ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "UPDATE_DOC_TRANS_LOG", DocNo, curstep, prevstep, usrid, comments);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Backward Successfully');", true);
                return;
            }
            else
            {
                DocInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Backward Successfully');", true);
                return;
            }
        }

        protected void LbtnFinalAppBackward_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string DocNo = ((Label)this.gvFinalApprvl.Rows[index].FindControl("lblgvDocId")).Text.ToString();
            string curstep = ((Label)this.gvFinalApprvl.Rows[index].FindControl("lblfstep")).Text.ToString();
            string prevstep = ((Label)this.gvFinalApprvl.Rows[index].FindControl("lblPrevStep")).Text.ToString();
            string comments = ((TextBox)this.gvFinalApprvl.Rows[index].FindControl("TxtgvComments")).Text.ToString();

            string usrid = hst["usrid"].ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "UPDATE_DOC_TRANS_LOG", DocNo, curstep, prevstep, usrid, comments);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Backward Successfully');", true);
                return;
            }
            else
            {
                DocInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Backward Successfully');", true);
                return;
            }
        }

        public bool ConfimMail(string tostep, string esubject, string url, string bodyContent)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString().Trim();
            string address = hst["comadd1"].ToString().Trim();
            string usrSession = hst["session"].ToString().Trim();

            string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);

            DataSet ds = this.accData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SMSEMAILSETUP", usrid, "", "", "", "", "", "", "", "");

            string useremail = string.Empty;
            string useremail1 = string.Empty;

            string compname = string.Empty;


            string condate = string.Empty;
            //  string endingdate = string.Empty;

            //SMTP
            string hostname = ds.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(ds.Tables[0].Rows[0]["portno"].ToString());
            SmtpClient client = new SmtpClient(hostname, portnumber);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = false;

            string frmemail = (ds.Tables[1].Rows[0]["mailpass"].ToString() == "" || ds.Tables[1].Rows.Count == 0) ? ds.Tables[0].Rows[0]["frmmail"].ToString() : ds.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = (ds.Tables[1].Rows[0]["mailpass"].ToString() == "" || ds.Tables[1].Rows.Count == 0) ? ds.Tables[0].Rows[0]["mailpass"].ToString() : ds.Tables[1].Rows[0]["mailpass"].ToString(); ;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            DataTable dt = ds.Tables[2];

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(frmemail);
            useremail = "safi@pintechltd.com";
            DataSet result = accData.GetTransInfo(comcod, "SP_ENTRY_DOC", "STEP_WISE_USERINFO", tostep,"");
            if (result == null || result.Tables[0].Rows.Count==0)
            {
                return false;
            }
            if (result.Tables[0].Rows[0]["usrmail"].ToString() == "")
            {
                return false;
            }
            else
            {
                useremail = result.Tables[0].Rows[0]["usrmail"].ToString();
            }
            msg.To.Add(new MailAddress(useremail));
            condate = DateTime.Today.ToString("dd.MM.yyyy");
            string body = string.Empty;
          


            msg.Subject = esubject;
            msg.IsBodyHtml = true;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/mail.html")))

            {

                body = reader.ReadToEnd();

            }
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            body = body.Replace("{msghead}", esubject);
            body = body.Replace("{bodyContent}", bodyContent);
            body = body.Replace("{logo}", ComLogo);
            body = body.Replace("{address}", address);

            msg.Body = body;


            try
            {
                client.Send(msg);
                return true;

            }
            catch (Exception ex)
            {
                //
                return false;
            }


        }
    }
}