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
using System.IO;
using SPELIB;
using System.Text.RegularExpressions;

namespace SPEWEB.F_33_Doc
{
    public partial class DocUpload : System.Web.UI.Page
    {
      //  ProcessAccess ImgData = new ProcessAccess("ASITDOC");
        ProcessAccess Docobj = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text ="Document Upload Panel";
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.TxtOrdDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.TxtDelDate.Text = DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy");
                this.GetSupplier();
                this.GetDocType();
                

                if(this.Request.QueryString["Type"].ToString()=="Edit")
                {
                    this.GetPrevDoc();
                   
                }
                this.GetIDNo();
                this.ShowDocInfo();
            }

            if (imgFileUpload.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string random = ASTUtility.RandNumber(1, 9999999).ToString();
                Upload = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Upload\\Doc" + "\\" + random + Path.GetExtension(Upload);
                imgFileUpload.PostedFile.SaveAs(savelocation);
                string Url = "~/Upload/Doc/" + random + Path.GetExtension(Upload);
                string Docno = ASTUtility.Right(this.lblCurMSRNo1.Text, 10).ToString();
                string doctype = this.DDlDocType.SelectedValue.ToString();
                DataTable dt = (DataTable)ViewState["Docfiles"];
                if (dt.Rows.Count == 0)
                {
                    this.lbtnUpdate_Click(null,null);
                }
                bool result = Docobj.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "UPDATE_DOC_INFO", "DOCINFA", Docno, doctype, Url, usrid);
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Document Update Successfully');", true);
                    ShowDocInfo();
                    return;

                }

            }
        }

        public static bool isValidAmount(string value)
        {

            // This Pattern is use to verify the email
            string strRegex = @"^[0-9]+$";

            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);

            if (re.IsMatch(value))
                return (true);
            else
                return (false);
        }
   
    private void GetDocType()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Docobj.GetTransInfo(comcod, "SP_ENTRY_DOC", "GET_DOC_GCOD", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.DDlDocType.DataTextField = "gdesc";
            this.DDlDocType.DataValueField = "gcod";
            this.DDlDocType.DataSource = ds1.Tables[0];
            this.DDlDocType.DataBind();
        }
        private void ShowDocInfo()
        {
            string comcod = this.GetCompCode();
            string Docno = ASTUtility.Right(this.lblCurMSRNo1.Text, 10).ToString();
            DataSet ds1 = Docobj.GetTransInfo(comcod, "SP_ENTRY_DOC", "SHOW_DOC_INFO", Docno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.TxtRefno.Text = ds1.Tables[0].Rows[0]["refno"].ToString();
                this.TxtOrdDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["ordrdat"]).ToString("dd-MMM-yyyy");
                this.TxtDelDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["deldat"]).ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["docdat"]).ToString("dd-MMM-yyyy");
                this.Txtvalue.Text = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["ordval"]).ToString();
                this.TxtRemarks.Text = ds1.Tables[0].Rows[0]["remarks"].ToString();
                this.DdlSupplier.SelectedValue = ds1.Tables[0].Rows[0]["ssircode"].ToString();
                string symbol = (ds1.Tables[0].Rows[0]["currency"].ToString().Length == 0) ? "Not Set" : ds1.Tables[0].Rows[0]["currency"].ToString();
                this.CurSymbol.InnerHtml = symbol;
            }

            ViewState["Docfiles"] = ds1.Tables[1];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt =(DataTable)ViewState["Docfiles"];
            if (dt.Rows.Count==0)
            {
                this.myTabContent.InnerHtml = "<div class='alert alert-danger'>Nothing to display</div>";
                this.gvFileGrid.DataSource = null;
                this.gvFileGrid.DataBind();
                return;
            }
            this.gvFileGrid.DataSource = dt;
            this.gvFileGrid.DataBind();
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
                             "<a class=\"nav-link "+ cssclss + "\" data-toggle=\"tab\" href=\"#T"+ dr["gcod"].ToString() + "\"> DOC-"+i+"</a>" +
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

                        midcontent = "<img class='img img-responsive' src='"+ResolveUrl(dr["fileurl"].ToString())+"'><br> <a target='_blank' href='"+ ResolveUrl(dr["fileurl"].ToString()) + "' class='btn btn-sm btn-info'>Preview/Download</a>";
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
                            "<section class=\"card card-figure\">"+
                      "<figure class=\"figure\">"+                       
                        "<div class=\"figure-img\">"+
                          "<img class=\"img-fluid\" src="+ ResolveUrl("~/Images/Excel-Icon.png") +" alt=\"Card image cap\">" +
                          "<div class=\"figure-action\">"+
                            "<a href = "+ResolveUrl(dr["fileurl"].ToString())+" class=\"btn btn-block btn-sm btn-primary\">Download</a>" +
                          "</div>"+
                        "</div>"+                   
                        "<figcaption class=\"figure-caption\">"+
                          "<h6 class=\"figure-title\">"+
                           " <a href = " + ResolveUrl(dr["fileurl"].ToString()) + " > "+ dr["gdesc"].ToString() + "</a>" +
                          "</h6>"+
                          "<p class=\"text-muted mb-0\"> Updated On: "+ dr["rowdat"].ToString() +", Posted by: "+ dr["usrname"].ToString() + "</p>" +
                        "</figcaption>"+                       
                      "</figure>"+
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
        }
        private void GetPrevDoc(string docno="")
        {
            string comcod = this.GetCompCode();
            string srch = (docno.Length == 0) ? "%" : docno + "%";
            DataSet ds1 = Docobj.GetTransInfo(comcod, "SP_ENTRY_DOC", "GET_PREV_DOCLIST", srch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.DdlPrevDoc.DataTextField = "docdesc";
            this.DdlPrevDoc.DataValueField = "docno";
            this.DdlPrevDoc.DataSource = ds1.Tables[0];
            this.DdlPrevDoc.DataBind();
            if (this.Request.QueryString["genno"].ToString().Length > 0)
            {
                this.DdlPrevDoc.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
            else if (docno.Length != 0)
            {
                this.DdlPrevDoc.SelectedValue = docno;
            }

        }
        private void GetSupplier()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Docobj.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["Suplist"] = ds1.Tables[0];
            this.DdlSupplier.DataTextField = "ssirdesc1";
            this.DdlSupplier.DataValueField = "ssircode";
            this.DdlSupplier.DataSource = ds1.Tables[0];
            this.DdlSupplier.DataBind();
            this.DdlSupplier_SelectedIndexChanged(null,null);

        }
        private string GetCompCode()
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
        private void GetIDNo()
        {
            if (this.DdlPrevDoc.Items.Count > 0)
            {
                this.lblCurMSRNo1.Text = "DOC-NO-" + this.DdlPrevDoc.SelectedValue.ToString();
                return;
            }
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtDate.Text.Trim();
            DataSet ds1 = Docobj.GetTransInfo(comcod, "SP_ENTRY_DOC", "GETLASTID", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.lblCurMSRNo1.Text = "DOC-NO-"+ds1.Tables[0].Rows[0]["maxidno"].ToString();

            }

        }
        

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string userid = hst["usrid"].ToString();
                string posteddat = System.DateTime.Now.ToString();
                string comcod = this.GetCompCode();                
                this.GetIDNo();
                string refno = this.TxtRefno.Text.ToString();
                string remarks = this.TxtRemarks.Text;
                string supplier = this.DdlSupplier.SelectedValue.ToString();
                string Docdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
               
             
                
                string OrdDate = Convert.ToDateTime(this.TxtOrdDate.Text).ToString("dd-MMM-yyyy");// get order date
                
                string DelDate = Convert.ToDateTime(this.TxtDelDate.Text).ToString("dd-MMM-yyyy"); // get delivery date

                string ordtext = this.Txtvalue.Text;
                if (isValidAmount(ordtext) == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You given Invalid Value');", true);
                    return;

                }
                
               
                double ordvalue = Convert.ToDouble("0"+ ordtext);

                if(refno.Length==0 || supplier=="" || supplier == "000000000000" || ordvalue==0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Mandatory Fields Are Required');", true);
                    return;
                }
                


                string Docno = ASTUtility.Right(this.lblCurMSRNo1.Text,10).ToString();



                bool result = Docobj.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "UPDATE_DOC_INFO", "DOCINFB", Docno, supplier, refno, OrdDate, DelDate, ordvalue.ToString(), remarks, Docdate, posteddat,userid);
               if (result)
                {
                    GetPrevDoc(Docno);

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Document Update Successfully');", true);
                    return;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong!!');", true);
                    return;

                }

               

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message.ToString() +"');", true);

            }

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void DdlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["Suplist"];
            DataRow[] dr = dt.Select("ssircode = '"+this.DdlSupplier.SelectedValue.ToString()+"'");

            this.CurSymbol.InnerHtml = (dr[0]["currency"].ToString().Length>0)? dr[0]["currency"].ToString():"Not Set";

        }
    }
}