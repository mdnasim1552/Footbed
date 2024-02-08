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
using SPEENTITY;
using System.IO;
using AjaxControlToolkit;

namespace SPEWEB.F_01_Mer
{
    public partial class SampleInquiry : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();

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

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sample Inquiry Entry";
                CommonButton();

                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //  this.GetLastInqNumber();
                this.Get_BuyerName();
                this.Get_CategoryName();
                this.GetSeason();
                this.Get_Brand();
                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.ibtnPreList_Click(null, null);
                    this.ddlPreList.SelectedValue = this.Request.QueryString["genno"].ToString();
                    this.lbtnOk_Click(null, null);
                }
                //  this.Get_Styles();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(Inquiry_Approved);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            //((LinkButton)this.Master.FindControl("")).Visible = true;

        }


        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void GetLastInqNumber()
        {

            string comcod = this.GetComCode();
            string date = this.txtDatefrom.Text;
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GETLASTINQNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6);

            this.ddlPreList.DataTextField = "maxperno1";
            this.ddlPreList.DataValueField = "maxperno";
            this.ddlPreList.DataSource = ds3.Tables[0];
            this.ddlPreList.DataBind();

        }
        private void Get_BuyerName()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            ddlbuyer.DataTextField = "sirdesc";
            ddlbuyer.DataValueField = "sircode";
            ddlbuyer.DataSource = ds2.Tables[0];
            ddlbuyer.DataBind();
        }
        private void Get_CategoryName()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SAMPLE_CATEGORY", "", "", "", "", "", "", "", "", "");

            ViewState["tblcategory"] = ds2.Tables[0];
        }
        private void GetSeason()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SEASON_NAME", "", "", "", "", "", "", "", "", "");

            ViewState["tblSeason"] = ds2.Tables[0];
        }
        private void Get_Brand()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BRAND_NAME", "", "", "", "", "", "", "", "", "");

            ViewState["tblBrand"] = ds2.Tables[0];
        }
        private void Get_Styles()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_STYLE_INFO", "", "", "", "", "", "", "", "", "");

        }

        public void CommonButton()
        {
         


            if (this.Request.QueryString["Type"] == "Approv")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approval";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do You want to Approve?')";
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;

            }

            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.ibtnPreList.Visible = false;
                this.ddlPreList.Visible = false;
                this.ShowInquiry();
                return;
            }
            this.lbtnOk.Text = "Ok";
            // this.lblmsg.Text = "";
            this.ddlPreList.Items.Clear();
            this.gvSampleInq.DataSource = null;
            this.gvSampleInq.DataBind();

            this.ibtnPreList.Visible = true;
            this.ddlPreList.Visible = true;
            this.txtDatefrom.Enabled = true;
            this.GetLastInqNumber();
        }
        public string GetArticle()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst["comcod"].ToString() == "5301")
            {
                return "Edison Article";
            }
            else
            {
                return "Sys. Gen. Article";

            }
        }
        private void ShowInquiry()
        {

            string comcod = this.GetComCode();
            ViewState.Remove("tblinquery");
            string CurDate1 = this.txtDatefrom.Text.Trim();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtDatefrom.Enabled = false;
                mPerNo = this.ddlPreList.SelectedValue.ToString();
            }

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", mPerNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                // ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            ViewState["tblinquery"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassSampleInquiry>();


            if (ds1.Tables[1] != null && ds1.Tables[1].Rows.Count > 0)
            {
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["inqno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["inqno1"].ToString().Substring(6, 5);
                this.txtDatefrom.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["inqdat"]).ToString("dd-MMM-yyyy");
                this.ddlbuyer.SelectedValue = ds1.Tables[1].Rows[0]["buyerid"].ToString();


            }

            this.Data_Bind();

        }
        protected void ibtnPreList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GETPRE_INQUIRY_NO", curdate, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            this.ddlPreList.DataTextField = "inqno1";
            this.ddlPreList.DataValueField = "inqno";
            this.ddlPreList.DataSource = ds2.Tables[0];
            this.ddlPreList.DataBind();
            ds2.Dispose();

        }
        private void Data_Bind()
        {
            var inqtbl = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];


            this.gvSampleInq.DataSource = inqtbl;
            this.gvSampleInq.DataBind();

            this.FooterCal();
        }
        private void Save_Value()
        {
            var inqtbl = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            for (int i = 0; i < this.gvSampleInq.Rows.Count; i++)
            {
                string category = ((DropDownList)gvSampleInq.Rows[i].FindControl("ddlcategory")).SelectedValue.ToString();
                string styledesc = ((DropDownList)gvSampleInq.Rows[i].FindControl("ddlcategory")).SelectedItem.ToString();
                string color = ((TextBox)gvSampleInq.Rows[i].FindControl("txtgColor")).Text.ToString();
                string artno = ((TextBox)gvSampleInq.Rows[i].FindControl("txtgArtno")).Text.ToString();
                string samsize = ((TextBox)gvSampleInq.Rows[i].FindControl("txtgSize")).Text.ToString();
                string unit = ((TextBox)gvSampleInq.Rows[i].FindControl("txtgvUnit")).Text.ToString();
                string consize = ((TextBox)gvSampleInq.Rows[i].FindControl("txtgconSize")).Text.ToString();
                string sizernge = ((TextBox)gvSampleInq.Rows[i].FindControl("txtgSizernge")).Text.ToString();
                double ordqty = Convert.ToDouble("0" + ((TextBox)gvSampleInq.Rows[i].FindControl("txtgvQtyc")).Text.Trim());
                string season = ((DropDownList)gvSampleInq.Rows[i].FindControl("ddlSeason")).SelectedValue.ToString();
                string brand = ((DropDownList)gvSampleInq.Rows[i].FindControl("ddlbrand")).SelectedValue.ToString();

                inqtbl[i].category = category;
                inqtbl[i].styledesc = styledesc;
                inqtbl[i].color = color;
                inqtbl[i].artno = artno;
                inqtbl[i].samsize = samsize;
                inqtbl[i].consize = consize;
                inqtbl[i].sizernge = sizernge;
                inqtbl[i].ordqty = ordqty;
                inqtbl[i].season = season;
                inqtbl[i].brand = brand;
                inqtbl[i].sirunit = unit;

            }
            ViewState["tblinquery"] = inqtbl;
        }
        private void FooterCal()
        {
            var inqtbl = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            if (inqtbl == null || inqtbl.Count == 0)
            {
                return;
            }

            ((Label)this.gvSampleInq.FooterRow.FindControl("lblFoterRev")).Text = ((inqtbl.Sum(p => p.ordqty) == 0) ? 0 : inqtbl.Sum(p => p.ordqty)).ToString("#,##0;(#,##0); ");

        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                this.Save_Value();
                string comcod = this.GetComCode();
                if (this.ddlPreList.Items.Count == 0)
                    this.GetLastInqNumber();
                string buyer = this.ddlbuyer.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
                string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

                var inqtbl = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
                DataTable dt = ASITUtility03.ListToDataTable(inqtbl);

                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tbl1";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string PostedByid = hst["usrid"].ToString();
                string Posttrmid = hst["compname"].ToString();
                string PostSession = hst["session"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "INSERT_UPDATE_SAMPLE_INQUIRY", ds, null, null, inqno, buyer, curdate, PostedByid, Posttrmid, PostSession, Posteddat);
                if (!result)
                    return;


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message+"');", true);

            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqnum = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = "";
            string ToFrDate = "(From :" + fdate + " To " + tdate + ")";
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqnum, printdate,
                     "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                // ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataTable dt = ds1.Tables[0];


            var lst = new List<SPEENTITY.C_01_Mer.EclassSampleInquiry>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_01_Mer.EclassSampleInquiry();

                obj1.styleid = dr1["styleid"].ToString();
                obj1.styledesc = dr1["styledesc"].ToString();
                obj1.ordqty = Convert.ToDouble(dr1["ordqty"]);
                obj1.color = dr1["color"].ToString();
                //obj1.xmldata = dr1["xmldata"].ToString();   
                obj1.artno = dr1["artno"].ToString();
                obj1.catedesc = dr1["catedesc"].ToString();
                obj1.category = dr1["category"].ToString();
                obj1.comcod = dr1["comcod"].ToString();
                obj1.consize = dr1["consize"].ToString();
                obj1.samsize = dr1["samsize"].ToString();
                obj1.sirunit = dr1["sirunit"].ToString();
                obj1.seasondesc = dr1["seasondesc"].ToString();
                obj1.brandesc = dr1["brandesc"].ToString();
                obj1.attchmnt = dr1["attchmnt"].ToString();
                obj1.sizernge = dr1["sizernge"].ToString();
                //string att = obj1.attchmnt;
                obj1.attchmnt = (dr1["attchmnt"].ToString().Length == 0 ? "" : new Uri(Server.MapPath(dr1["attchmnt"].ToString())).AbsoluteUri);


                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;


                lst.Add(obj1);
            }


            LocalReport rpt1 = new LocalReport();

           
            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptSampleEntry", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("buyer", ds1.Tables[1].Rows[0]["buyername"].ToString()));
            rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds1.Tables[1].Rows[0]["inqdat"]).ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("inqnum", "INQUERY NO: " + ds1.Tables[1].Rows[0]["inqno1"]));
            rpt1.SetParameters(new ReportParameter("rpttitle", "SAMPLE INQUIRY ENTRY"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void gvSampleInq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable category = (DataTable)ViewState["tblcategory"];
            DataTable season = (DataTable)ViewState["tblSeason"];
            DataTable brand = (DataTable)ViewState["tblBrand"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlcate = (DropDownList)e.Row.FindControl("ddlcategory");
                ddlcate.DataTextField = "gdesc";
                ddlcate.DataValueField = "gcod";
                ddlcate.DataSource = category;
                ddlcate.DataBind();
                ddlcate.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "category"));
                DropDownList ddlses = (DropDownList)e.Row.FindControl("ddlSeason");
                ddlses.DataTextField = "gdesc";
                ddlses.DataValueField = "gcod";
                ddlses.DataSource = season;
                ddlses.DataBind();
                ddlses.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "season"));

                DropDownList ddlbrand = (DropDownList)e.Row.FindControl("ddlbrand");
                ddlbrand.DataTextField = "gdesc";
                ddlbrand.DataValueField = "gcod";
                ddlbrand.DataSource = brand;
                ddlbrand.DataBind();
                ddlbrand.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "brand"));


                if (e.Row.RowIndex > 0)
                {
                    e.Row.FindControl("lbAddMore").Visible = false;
                }
                if (this.Request.QueryString["Type"].ToString() != "Approv")
                {
                    e.Row.FindControl("LbtnApprove").Visible = false;
                }


            }

        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComCode();
            var inqinfo = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string Url = "";
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string styleid = this.lblStylecode.Text.Trim().ToString();
            string uploadtype = this.ddluploadtype.SelectedValue.ToString();
            if (AsyncFileUpload1.HasFile)
            {

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);

                Url = "~/Upload/SAMPLE/" + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                //dt.Rows.Add(comcod, Url);
                this.Uploadedimg.ImageUrl = Url;
            }
            var index = inqinfo.FindIndex(s => s.styleid == styleid);
            if (uploadtype == "samimg")
            {
                inqinfo[index].images = Url;
            }
            else
            {
                inqinfo[index].attchmnt = Url;
            }

            ViewState["tblinquery"] = inqinfo;
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "UPDATE_STYLEWISE_SAMPLE_IMAGES", inqno, styleid, Url, uploadtype);
            if (result)
            {

                this.ShowInquiry();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


            }




        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }



        protected void lblgvItmCodc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");

            string styleid = ((Label)this.gvSampleInq.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqno, "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Save Iquiry before upload');", true);

                return;
            }

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_ACTUAL_COLOR_SIZE", inqno, styleid, "", "", "", "", "", "", "");
            ViewState["tblcolor"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            ViewState["tblsizerange"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            ViewState["tblsamsize"] = ds1.Tables[2].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            ViewState["tblconsize"] = ds1.Tables[3].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            this.lblStylecode.Text = styleid;

            var inqinfo = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            var inqinfo1 = inqinfo.FindAll(c => c.styleid == styleid);
            this.Uploadedimg.ImageUrl = inqinfo1[0].images.ToString();
            this.ModalDataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        private void ModalDataBind()
        {
            var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
            this.gvColor.DataSource = color;
            this.gvColor.DataBind();

            var sizerange = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsizerange"];
            this.gvSize.DataSource = sizerange;
            this.gvSize.DataBind();

            var samsize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsamsize"];
            this.gvsamsize.DataSource = samsize;
            this.gvsamsize.DataBind();

            var consize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblconsize"];
            this.gvconsize.DataSource = consize;
            this.gvconsize.DataBind();
        }

        private void Modal_Save_Value()
        {
            var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
            for (int i = 0; i < this.gvColor.Rows.Count; i++)
            {
                string colordesc = ((TextBox)gvColor.Rows[i].FindControl("txtColorDesc")).Text.ToString();
                string colorselect = (((CheckBox)gvColor.Rows[i].FindControl("gvChkColor1")).Checked == true) ? "Y" : "n";
                color[i].colordesc = colordesc;
                color[i].colorselect = colorselect;
            }
            ViewState["tblcolor"] = color;
            //----------------------for size range---------------------------------
            var sizerange = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsizerange"];
            for (int i = 0; i < this.gvSize.Rows.Count; i++)
            {
                string sizedesc = ((TextBox)gvSize.Rows[i].FindControl("txtgvSizeDesc")).Text.ToString();
                string sizeselect = (((CheckBox)gvSize.Rows[i].FindControl("gvChkSize1")).Checked == true) ? "Y" : "n";
                sizerange[i].sizedesc = sizedesc;
                sizerange[i].sizeselect = sizeselect;
            }
            ViewState["tblsizerange"] = sizerange;
            //----------------------for sample size ---------------------------------
            var samsize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsamsize"];
            for (int i = 0; i < this.gvsamsize.Rows.Count; i++)
            {
                string samsizedesc = ((TextBox)gvsamsize.Rows[i].FindControl("txtgvsamSizeDesc")).Text.ToString();
                string samsizeselect = (((CheckBox)gvsamsize.Rows[i].FindControl("gvsamChkSize1")).Checked == true) ? "Y" : "n";
                samsize[i].sizedesc = samsizedesc;
                samsize[i].sizeselect = samsizeselect;
            }
            ViewState["tblsamsize"] = samsize;
            //----------------------for Consumption size ---------------------------------
            var consize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblconsize"];
            for (int i = 0; i < this.gvconsize.Rows.Count; i++)
            {
                string consizedesc = ((TextBox)gvconsize.Rows[i].FindControl("txtgvconSizeDesc")).Text.ToString();
                string consizeselect = (((CheckBox)gvconsize.Rows[i].FindControl("gvconChkSize1")).Checked == true) ? "Y" : "n";
                consize[i].sizedesc = consizedesc;
                consize[i].sizeselect = consizeselect;
            }
            ViewState["tblconsize"] = consize;

        }

        protected void lblbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Modal_Save_Value();
                string comcod = this.GetComCode();
                string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
                string styleid = lblStylecode.Text.Trim().ToString();
                string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                var inqtbl = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];


                var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
                var sizerange = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsizerange"];
                var samsize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsamsize"];
                var consize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblconsize"];
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(ASITUtility03.ListToDataTable(color));
                ds.Tables.Add(ASITUtility03.ListToDataTable(sizerange));
                ds.Tables.Add(ASITUtility03.ListToDataTable(samsize));
                ds.Tables.Add(ASITUtility03.ListToDataTable(consize));
                ds.Tables[0].TableName = "tblcolor";
                ds.Tables[1].TableName = "tblsizerange";
                ds.Tables[2].TableName = "tblsamsize";
                ds.Tables[3].TableName = "tblconsize";
                bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "UPDATE_ACTUAL_COLOR_SIZE", ds, null, null, inqno, styleid);
                if (result)

                    this.ShowInquiry();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message +"');", true);

               

            }
        }
        protected void AddMore_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> lst = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];

            int lastnum = lst.Count() + 1;
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string comcod = lst[RowIndex].comcod.ToString();
            string styleid = lst[RowIndex].styleid.ToString().Substring(0, 9) + ASTUtility.Right("00" + lastnum.ToString(), 3);
            string styledesc = lst[RowIndex].styledesc.ToString();
            string sirunit = lst[RowIndex].sirunit.ToString();
            string category = lst[RowIndex].category.ToString();
            string catedesc = lst[RowIndex].catedesc.ToString();
            string artno = lst[RowIndex].artno.ToString();
            string color = lst[RowIndex].color.ToString();
            string sizernge = lst[RowIndex].sizernge.ToString();
            string samsize = lst[RowIndex].samsize.ToString();
            string consize = lst[RowIndex].consize.ToString();
            string images = lst[RowIndex].images.ToString();
            double ordqty = Convert.ToDouble(lst[RowIndex].ordqty);
            string attchmnt = lst[RowIndex].attchmnt.ToString();
            string season = lst[RowIndex].season.ToString();
            string seasondesc = lst[RowIndex].seasondesc.ToString();
            string autoartcle = lst[RowIndex].autoartcle.ToString();
            string brand = lst[RowIndex].brand.ToString();
            string brandesc = lst[RowIndex].brandesc.ToString();
            lst.Add(new SPEENTITY.C_01_Mer.EclassSampleInquiry(comcod, styleid, styledesc, sirunit, category, catedesc, artno, color, sizernge, samsize, consize, images, attchmnt, ordqty, season, seasondesc, autoartcle, brand, brandesc));
            ViewState["tblinquery"] = lst;
            this.Data_Bind();

        }

        protected void gvSampleInq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> lst = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string comcod = this.GetComCode();
            int index = (this.gvSampleInq.PageIndex) * this.gvSampleInq.PageSize + e.RowIndex;
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string category = lst[index].category.ToString();
            string styleid = lst[index].styleid.ToString();
            lst.RemoveAt(index);
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "DELETE_SAMPLE_FROM_INQUIRY", inqno, styleid, category);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);
   
            }

            ViewState["tblinquery"] = lst;
            this.Data_Bind();
        }


        protected void Inquiry_Approved(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Enabled = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string InqNO = this.Request.QueryString["genno"].ToString();

            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "APPROVED_INQ", null, null, null, InqNO, userid, AppDat, trmid, sessionid);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Inquiry Not Approved');", true);

                //((Label)this.Master.FindControl("lblmsg")).Text = "Inquiry Not Approved";
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;

            }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Inquiry Approved');", true);

            //((Label)this.Master.FindControl("lblmsg")).Text = "Inquiry Approved";
            //((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green";
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

            //Common.LogStatus("Diagnosis Complite", "QC Qualified", "Recived No: ", centrid + " - " + wrRecvno);
        }

        protected void SaveBuyer_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string buyername = this.TxtBuyer.Text.Trim().ToString();
            string address = this.TxtAddress.Text.Trim().ToString();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "SAVE_NEW_BUYER", buyername, address);
            if (result)
            {
                this.Get_BuyerName();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Save Successfully');", true);

          
            }
        }

        protected void LbtnSaveSeason_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string season = this.txtSeason.Text.Trim().ToString();
            DataTable dt =(DataTable)ViewState["tblSeason"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "gdesc='" + season + "'";
            if (dv.ToTable().Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail(' Seasson Already Exist');", true);
                return;
            }
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "SAVE_NEW_SEASON", season);
            if (result)
            {
                this.GetSeason();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Save Seasson Successfully');", true);
             
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail(' Seasson Already Exist');", true);

            }
        }

        protected void LbtnApprove_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> lst = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            string comcod = this.GetComCode();
            string inqno = this.Request.QueryString["genno"].ToString();
            string styleid = lst[RowIndex].styleid;
            string category = lst[RowIndex].category;
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "APPROVE_SINGLE_SAMPLE", inqno, styleid, category);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Approve Successfully');", true);

               
            }
        }

        protected void lbtnSaveBrand_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string brand = this.txtBrandName.Text.Trim().ToString();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "SAVE_NEW_BRAND", brand);
            if (result)
            {
                this.Get_Brand();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Save Successfully');", true);

             
            }
        }

        protected void LbtnSaveStyle_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string style = this.txtStyleName.Text.Trim().ToString();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "SAVE_CATEGORY_NAME", style);
            if (result)
            {
                this.Get_CategoryName();
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Save Successfully');", true);

               
            }
        }
    }

}