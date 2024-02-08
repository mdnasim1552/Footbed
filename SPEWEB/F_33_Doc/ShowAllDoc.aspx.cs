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

namespace SPEWEB.F_33_Doc
{
    public partial class ShowAllDoc : System.Web.UI.Page
    {
        ProcessAccess DocData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.GetDocument();

            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetDocument()
        {
            
            string comcod =this.GetComCode();        
            DataSet ds1 = DocData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GET_PREV_DOCLIST", "%", "", "", "", "", "", "", "", "");
            this.DdlDocNo.DataTextField = "docdesc";
            this.DdlDocNo.DataValueField = "docno";
            this.DdlDocNo.DataSource = ds1.Tables[0];
            this.DdlDocNo.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.DdlDocNo.SelectedValue = Common.Base64Decode(this.Request.QueryString["genno"].ToString());
                this.lbtnOk_Click(null,null);
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }


        private void ShowReport()
        {
            Session.Remove("tbAllDoc");
            string comcod = this.GetComCode();
            string docno = this.DdlDocNo.SelectedValue.ToString();
            DataSet ds2 = DocData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GET_DOCLOG_INFORMATION", docno, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                return;
            }
            string bindactivity = String.Empty;
          DataTable dt= ds2.Tables[0].DefaultView.ToTable(true, "eventdat");
            foreach (DataRow dr in dt.Rows)
            {
               
                bindactivity += "<h2 class=\"section-title\"> " + dr["eventdat"].ToString() + " </h2>";
                bindactivity += "<ul class=\"timeline\">";
                DataRow[] dr1 = ds2.Tables[0].Select("eventdat='"+ dr["eventdat"].ToString() + "'");
                foreach (DataRow dr2 in dr1)
                {
                    bindactivity += "<li class=\"timeline-item\">"+
                          "<div class=\"timeline-figure\">"+
                        "<span class=\"tile tile-circle tile-sm\">"+
                          "<i class=\"far fa-calendar-alt fa-lg\"></i>"+
                        "</span>"+
                      "</div>"+                    
                      "<div class=\"timeline-body\">"+                     
                        "<div class=\"media\">"+                   
                          "<div class=\"media-body\">"+
                            "<h6 class=\"timeline-heading\">"+
                              "<a href = \"#\" class=\"text-link\">"+ dr2["title"].ToString() + "</a>"+
                            "</h6>"+
                            "<p class=\"mb-0\">"+
                              "<a href = \"#\" class='font-italic' > "+ dr2["usrname"].ToString() + "</a> >>  "+dr2["comments"].ToString() +"</p>" +                           
                          "</div>"+
                          "<div class=\"d-none d-sm-block\">" +
                            "<span class=\"timeline-date\"> &nbsp; &nbsp;" + Convert.ToDateTime(dr2["rowdat"]).ToString("dd-MMM-yyyy hh:mm tt")+"</span>" +
                          "</div>" +
                        "</div>" +
                      "</div>" +
                    "</li>";
                }
                
                bindactivity += "</ul>";
            }

            this.ActivityLog.InnerHtml = bindactivity;
        }




        protected void lnkDownload_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Download")
            //{
            //    string savelocation = Server.MapPath("Docs") + @"\";
            //    string[] filePaths = Directory.GetFiles(savelocation);
            //    foreach (string filePath in filePaths)
            //        File.Delete(filePath);
            //    //string fileName = string.Empty;
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = this.gvDoc.Rows[index];
            //    string ImgID = this.gvDoc.DataKeys[index].Value.ToString();

            //    DataTable dt = (DataTable)Session["tbAllDoc"];
            //    DataRow[] dr = dt.Select(" id='" + ImgID + "' ");

            //    string fileName = dr[0]["filename"].ToString();
            //    byte[] documentBinary = (byte[])dr[0]["data"];
            //    FileStream fStream = new FileStream(Server.MapPath("Docs") + @"\" + fileName, FileMode.Create);
            //    fStream.Write(documentBinary, 0, documentBinary.Length);
            //    fStream.Close();
            //    fStream.Dispose();

            //    Response.Redirect(@"Docs\" + fileName);

            //    //lbljavascript.Text = @"<script>window.open('~/F_99_Doc/Docs/" + fileName + "', target='_blank');</script>";


            //}
        }
     
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowReport();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}