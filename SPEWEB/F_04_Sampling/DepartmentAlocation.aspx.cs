using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_22_Sal;
using AjaxControlToolkit;
using System.IO;

namespace SPEWEB.F_04_Sampling
{
    public partial class DepartmentAlocation : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        Common CommonClass = new Common();

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
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Department Allocation Sheet";
                  CommonButton();
                this.GetInqnumber();
                this.getCurList();
                Select_View();
            }
            
         
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(Con_Cost_Approved);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

           // ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click);
        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string styleid = this.Request.QueryString["genno"].ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list1.FindAll(s => s.styleid == styleid);
            string images = (list2[0].images.ToString().Length > 0) ? list2[0].images.ToString() : "";
            string comcod = this.GetCompCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string Url = "";

            string inqno = this.Request.QueryString["actcode"].ToString();
            var filePath = Server.MapPath(images);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (AsyncFileUpload1.HasFile)
            {
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);
                Url = "~/Upload/SAMPLE/" + random + extension;
            }
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "REPLACE_SAMPLE_IMAGE", inqno, styleid, Url);
            if (result)
            {
                this.Uploadedimg.ImageUrl = Url;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }
        }
        private void Select_View()
        {
                this.ShowConsump();
             
                MultiView1.ActiveViewIndex = 0;           
        }
        public void CommonButton()
        {
         

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

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetInqnumber()
        {
            string comcod = this.GetCompCode();
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string season = "%%";
            string agent = "%%";
            string customer = "%%";
            string category = "%%";
            string samptype = "%%";

            //DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ_LIST", "01-Jan-1900", todate);
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ_LIST", season, agent, customer, category, samptype);

            this.ddlinqno.DataTextField = "inqno2";
            this.ddlinqno.DataValueField = "inqno";
            this.ddlinqno.DataSource = ds1.Tables[0];
            this.ddlinqno.DataBind();
            DataView dv = ds1.Tables[0].DefaultView;
      
        
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlinqno.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }
            ddlinqno_SelectedIndexChanged(null, null);


        }
        private void getCurList()
        {

           
        }
      private void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            
           
        }
      

      
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Validation Error!!!');", true);

               
                return;
            }
            this.lnkbtnRecalculate_Click(null, null);
            this.UpdateCost();
          
        }
        private void UpdateCost()
        {
                    



            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            { //
                string id = dt.Rows[i]["id"].ToString();

                string procode= dt.Rows[i]["procode"].ToString();


                bool  result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "UPDATE_COSTING_DEPARTMENT_ALLOCATION", procode, inqno, id);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Merdata.ErrorObject["Msg"].ToString() + "');", true);



                        return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


              

            }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlStyle.Enabled = false;
                this.ddlinqno.Enabled = false;
                this.GetProcess();
                this.Get_ColorSize();
                
            }

            this.lbtnOk.Text = "Ok";
            this.ddlStyle.Enabled = true;
            this.ddlinqno.Enabled = true;
        }
        private void Get_ColorSize()
        {
            string comcod = this.GetCompCode();
            string styleid = this.ddlStyle.SelectedValue.ToString();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_ACTUAL_COLOR_SIZE", inqno, styleid, "", "", "", "", "", "", "");
            List<SPEENTITY.C_01_Mer.EclassOrderDetails> color = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            color.Add(new SPEENTITY.C_01_Mer.EclassOrderDetails("000000000000", "Common", "Y", "", "", ""));
            List<SPEENTITY.C_01_Mer.EclassOrderDetails> consize = ds1.Tables[3].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            consize.Add(new SPEENTITY.C_01_Mer.EclassOrderDetails("", "", "", "000000000000", "Common", "Y"));
            ViewState["tblcolor"] = color;
            ViewState["tblconsize"] = consize;
            this.Bind_color_size();
        }
        private void Bind_color_size()
        {
            List<SPEENTITY.C_01_Mer.EclassOrderDetails> color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
            this.ddlcolor.DataTextField = "colordesc";
            this.ddlcolor.DataValueField = "colorid";
            this.ddlcolor.DataSource = color.FindAll(p => p.colorselect == "Y");
            this.ddlcolor.DataBind();
            this.ddlcolor.SelectedValue = "000000000000";

            List<SPEENTITY.C_01_Mer.EclassOrderDetails> consize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblconsize"];
            this.ddlconsize.DataTextField = "sizedesc";
            this.ddlconsize.DataValueField = "sizeid";
            this.ddlconsize.DataSource = consize.FindAll(p => p.sizeselect == "Y");
            this.ddlconsize.DataBind();
            this.ddlconsize.SelectedValue = "000000000000";


        }
        protected void ddlinqno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqno, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                return;
            }
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassSampleInquiry>();
            ViewState["tblinquery"] = list1;
            this.ddlStyle.DataTextField = "styledesc";
            this.ddlStyle.DataValueField = "styleid";
            this.ddlStyle.DataSource = list1;
            this.ddlStyle.DataBind();



            this.txtbuyer.Text = ds1.Tables[1].Rows[0]["buyername"].ToString();
            this.txtbuyerid.Text = ds1.Tables[1].Rows[0]["buyerid"].ToString();

            this.txtmerchand.Text = ds1.Tables[1].Rows[0]["postedbyname"].ToString();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlStyle.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.lbtnOk_Click(null, null);
            }
            this.ddlStyle_SelectedIndexChanged(null, null);

            //this.ddlStyle_SelectedIndexChanged(null,null);
        }
        protected void GetProcess()
        {

            string comcod = this.GetCompCode();
            string inqno = this.Request.QueryString["actcode"].ToString();
            string styleid = this.Request.QueryString["genno"].ToString();
           
             string   type = "DEPT";
         
            string filter = "%";
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GETPROCESSCODE", filter, inqno, styleid, type, "", "", "", "", "");
            this.ddlProcess.DataSource = ds3.Tables[0];
            this.ddlProcess.DataTextField = "resdesc";
            this.ddlProcess.DataValueField = "rescode";
            this.ddlProcess.DataBind();
            ViewState["tblcodeType"] = ds3.Tables[0];
            ds3.Dispose();
        }
        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list1.FindAll(s => s.styleid == styleid);
            this.txtCategory.Text = list2[0].catedesc.ToString();
            this.txtArtno.Text = list2[0].artno.ToString();
            this.txtcolor.Text = list2[0].color.ToString();
          
            this.txtsizernge.Text = list2[0].sizernge.ToString();
            this.txtconsize.Text = list2[0].consize.ToString();
            this.txtseason.Text = list2[0].seasondesc.ToString();
            this.txtsamplesize.Text = list2[0].samsize.ToString();
            this.txtbrand.Text = list2[0].brandesc.ToString();
            this.Uploadedimg.ImageUrl = (list2[0].images.ToString().Length > 0) ? list2[0].images.ToString() : "~/images/no_img_preview.png";
            this.hypLnkbtn.NavigateUrl = list2[0].images.ToString();

            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_INFO_FOR_CONSUMPTION", inqno, styleid, "", "", "", "", "", "", "");


           
            string type = this.Request.QueryString["Type"].ToString();
           

            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            this.RefMarName.Text = ds3.Tables[0].Rows[0]["marchand"].ToString();
            this.txtlastrefno.Text = ds3.Tables[0].Rows[0]["lstrefno"].ToString();           
            this.txtconstruction.Text = ds3.Tables[0].Rows[0]["constrction"].ToString();
            this.txtsampleno.Text = ds3.Tables[0].Rows[0]["smplno"].ToString();
            
            //this.Data_Bind();
        }
      
      
      

     
        private void ShowConsump()
        {
            ViewState.Remove("tblstdcost");
            string comcod = this.GetCompCode();
            string lccode = this.ddlinqno.SelectedValue.ToString();          
            string  processcode = "%";            
            string prodcode = this.ddlStyle.SelectedValue.ToString();          

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_INFO", prodcode, processcode, lccode, "", "", "", "");
         
            ViewState["tblstdcost"] = ds4.Tables[0];
         
            this.Data_Bind();
          

        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblstdcost"];
            this.gvCost.DataSource = dt;
                this.gvCost.DataBind();
              
              

        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];

            string process = this.ddlProcess.SelectedValue.ToString();
 
                    for (int i = 0; i < this.gvCost.Rows.Count; i++)
                {

                    CheckBox checkbox =  ((CheckBox)this.gvCost.Rows[i].FindControl("chkCol"));

                if (checkbox.Checked == true)
                {
                    dt.Rows[i]["procode"] = process;
                }


           }
                


        ViewState["tblstdcost"] = dt;
  

        }
      
        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tblcodeType = ((DataTable)ViewState["tblcodeType"]).Copy();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

               string prcode= Convert.ToString(DataBinder.Eval(e.Row.DataItem, "procode"));
                DropDownList dept = (DropDownList)e.Row.FindControl("DdlDepartmnet");
                dept.DataTextField = "resdesc";
                dept.DataValueField = "rescode";
                dept.DataSource = tblcodeType;
                dept.DataBind();
                dept.SelectedValue = prcode;
               
                
            }
          
        }

        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
    }
}
